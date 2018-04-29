using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace CTest
{
    public class WebServer
    {
        public static int port = 12345;
        static Socket _serverSocket;
        //根目录，暂时不支持同时设置多个
        public static string rootPath = @"E:\web";

        public static bool isRunning;

        public static void Start()
        {
            _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _serverSocket.Bind(new IPEndPoint(IPAddress.Any, port));
            _serverSocket.Listen(1000);

            isRunning = true;

            Console.WriteLine("Serving HTTP on port" + port + "...");

            new Thread(OnStart).Start();
        }

        private static void OnStart()
        {
            while (isRunning)
            {
                try
                {
                    Socket socket = _serverSocket.Accept();//todo:
                    ThreadPool.QueueUserWorkItem(AcceptSocket, socket);
                }
                catch { }
            }
        }

        private static void AcceptSocket(object state)
        {
            if (isRunning)
            {
                Socket socket = state as Socket;
                HttpProcessor processor = new HttpProcessor(rootPath, socket);
                processor.ProcessRequest();
            }
        }

    }

    public class HttpProcessor
    {
        private Socket _socket;
        private string _rootPath;
        private bool _isClosed;
        private Request request;

        private static readonly Dictionary<string, string> staticFileContentType = new Dictionary<string, string>()
                                                                   {
                                                                       {"htm", "text/html"},
                                                                       {"html", "text/html"},
                                                                       {"xml", "text/xml"},
                                                                       {"txt", "text/plain"},
                                                                       {"css", "text/css"},
                                                                       {"js", "application/x-javascript"},
                                                                       {"png", "image/png"},
                                                                       {"gif", "image/gif"},
                                                                       {"jpg", "image/jpg"},
                                                                       {"jpeg", "image/jpeg"},
                                                                       {"zip", "application/zip"}
                                                                   };

        public HttpProcessor(string rootPath, Socket socket)
        {
            _rootPath = rootPath;
            _socket = socket;
        }

        public void ProcessRequest()
        {
            try
            {
                request = GetRequest();
                if (request != null)
                {
                    string staticContentType = GetStaticContentType(request);
                    if (!string.IsNullOrEmpty(staticContentType))
                    {
                        WriteFileResponse(request.FilePath, staticContentType);
                    }
                    else
                    {
                        WriteCustomResponse();
                    }
                }
                else
                {
                    SendErrorResponse(400);
                }
            }
            finally
            {
                Close();
            }
        }

        #region GetRequest
        private Request GetRequest()
        {
            try
            {
                string requestString = ReceiveString();
                if (!string.IsNullOrEmpty(requestString))
                {
                    Console.WriteLine(requestString);

                    Request request = new Request(requestString);
                    request.ParseHeaders();
                    IPEndPoint remoteEndPoint = (IPEndPoint)_socket.RemoteEndPoint;
                    request.RemoteEndPoint = remoteEndPoint;
                    IPEndPoint localEndPoint = (IPEndPoint)_socket.LocalEndPoint;
                    request.LocalEndPoint = localEndPoint;

                    return request;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return null;
        }

        private string ReceiveString()
        {
            int bufferSize = 0x8000;
            byte[] receiveBytes = new byte[0];
            while (true)
            {
                byte[] bytes = ReceiveBytes(bufferSize);
                if (bytes == null || bytes.Length == 0)
                    break;
                if (receiveBytes.Length == 0)
                    receiveBytes = bytes;
                else
                {
                    int receiveBytesLength = receiveBytes.Length + bytes.Length;
                    byte[] dst = new byte[receiveBytesLength];
                    Buffer.BlockCopy(receiveBytes, 0, dst, 0, receiveBytes.Length);
                    Buffer.BlockCopy(bytes, 0, dst, receiveBytes.Length, bytes.Length);
                }
            }

            string re = Encoding.UTF8.GetString(receiveBytes);
            return re;
        }

        private byte[] ReceiveBytes(int length)
        {
            int available = GetRequestAvailable();
            available = available > length ? length : available;
            byte[] buffer = null;
            if (available > 0)
            {
                buffer = new byte[available];
                int count = _socket.Receive(buffer, available, SocketFlags.None);
                if (count < available)
                {
                    byte[] dst = new byte[count];
                    if (count > 0)
                        Buffer.BlockCopy(buffer, 0, dst, 0, count);
                    buffer = dst;
                }
            }

            return buffer;
        }

        public int GetRequestAvailable()
        {
            int available = 0;
            try
            {
                if (_socket.Available == 0)
                {
                    _socket.Poll(10000, SelectMode.SelectRead);
                    if (_socket.Available == 0 && _socket.Connected)
                        _socket.Poll(10000, SelectMode.SelectRead);
                }

                available = _socket.Available;
            }
            catch
            {
                //ignore
            }

            return available;
        }
        #endregion

        #region ProcessResponse
        private void WriteFileResponse(string filePath, string contentType)
        {
            string fullPath = Path.Combine(_rootPath, filePath.TrimStart('/'));
            if (!File.Exists(fullPath))
                SendErrorResponse(404);
            else
            {
                try
                {
                    using (FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
                    {
                        int length = (int)fs.Length;
                        byte[] buffer = new byte[length];
                        int contentLength = fs.Read(buffer, 0, length);
                        string headers = BuildHeader(200,
                                                     new Dictionary<string, IEnumerable<string>>() { { "Content-Type", new string[] { contentType } } },
                                                     contentLength, false);
                        _socket.Send(Encoding.UTF8.GetBytes(headers));
                        _socket.Send(buffer, 0, contentLength, SocketFlags.None);
                    }
                }
                finally
                {
                    Close();
                }
            }
        }

        private string BuildHeader(int statusCode, Dictionary<string, IEnumerable<string>> headers, int contentLength, bool keepAlive)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("HTTP/1.1 {0} {1}\r\n", statusCode, HttpWorkerRequest.GetStatusDescription(statusCode));
            builder.AppendFormat("Server: K-one Server/1.0\r\n");
            builder.AppendFormat("Date: {0}\r\n", DateTime.Now.ToUniversalTime().ToString("R", DateTimeFormatInfo.InvariantInfo));
            if (contentLength > 0)
                builder.AppendFormat("Content-Length: {0}\r\n", contentLength);
            if (keepAlive)
                builder.Append("Connection: keep-alive\r\n");
            if (headers != null)
            {
                foreach (KeyValuePair<string, IEnumerable<string>> pair in headers)
                {
                    foreach (var s in pair.Value)
                        builder.AppendFormat("{0}: {1}\r\n", pair.Key, s);
                }
            }
            builder.Append("\r\n");

            return builder.ToString();
        }

        private void SendErrorResponse(int statusCode)
        {
            SendResponse(statusCode, $"<h1>{statusCode} {((HttpStatusCode)statusCode).ToString()}</h1>");
        }

        public void SendResponse(int statusCode, string body, Dictionary<string, IEnumerable<string>> headers = null, bool keepAlive = false)
        {
            SendResponse(statusCode, Encoding.UTF8.GetBytes(body), headers, keepAlive);
        }

        public void SendResponse(int statusCode, byte[] responseBodyBytes, Dictionary<string, IEnumerable<string>> headers = null, bool keepAlive = false)
        {
            SendHeaders(statusCode, headers, responseBodyBytes.Length, keepAlive);
            _socket.Send(responseBodyBytes);

            if (!keepAlive)
                Close();
        }

        public void SendResponse(byte[] data)
        {
            _socket.Send(data);
        }

        public void SendHeaders(int statusCode, Dictionary<string, IEnumerable<string>> headers, int contentLength, bool keepAlive)
        {
            string header = BuildHeader(statusCode, headers, contentLength, keepAlive);
            _socket.Send(Encoding.UTF8.GetBytes(header));
        }

        private void WriteCustomResponse()
        {
            //todo:根据request构造response内容

            #region 当客户端未提供cookie["xxx"]访问localhost:12345/list时返回未授权
            if (request.FilePath == "/list")
            {
                var xxx = GetCookie("xxx");
                if (xxx == null)
                {
                    SendErrorResponse(401);
                    return;
                }
            }
            #endregion

            #region 例:返回纯文本格式数据和两个cookie
            var cookies = new[] { "xxx=WR_5vsacF0BVcqicZlp1; path=/; HttpOnly", "token=222; expires=Fri, 1 Dec 2017 01:51:18 GMT; path=/t/" };
            var dict = new Dictionary<string, IEnumerable<string>>();
            dict.Add("Set-Cookie", cookies);
            dict.Add("Content-Type", new[] { "text/plain; charset=utf-8" });
            var body = "如果不只有header，要返回的内容替换这里";
            var bodytobytes = Encoding.UTF8.GetBytes(body);
            var header = BuildHeader(200, dict, bodytobytes.Length, true);
            _socket.Send(Encoding.UTF8.GetBytes(header));
            _socket.Send(bodytobytes);
            #endregion
        }
        #endregion

        private string GetCookie(string name)
        {
            if (request == null)
                throw new Exception("null reference");
            try
            {
                var cookies = request.Headers["Cookie"] + ";";
                var match = System.Text.RegularExpressions.Regex.Match(cookies, $"{name}=(\\w*);");
                if (match.Success)
                {
                    var re = match.Groups[1].Value;
                    return re;
                }
                return null;
            }
            catch { return null; }
        }

        private string GetStaticContentType(Request request)
        {
            try
            {
                var ext = Path.GetExtension(request.FilePath).TrimStart('.');
                return staticFileContentType[ext];
            }
            catch { return null; }
        }

        private void Close()
        {
            try
            {
                if (!_isClosed)
                {
                    _socket.Shutdown(SocketShutdown.Both);
                    _socket.Close();

                    _isClosed = true;
                }
            }
            catch
            {
                //ignore
            }
        }
    }

    public class Request
    {
        public string RawUrl { get; private set; }

        public string Protocol { get; private set; }

        public string FilePath { get; private set; }

        public string QueryString { get; private set; }

        public string HttpMethod { get; private set; }

        public NameValueCollection Headers { get; private set; }

        public IPEndPoint RemoteEndPoint { get; set; }

        public IPEndPoint LocalEndPoint { get; set; }

        public string Body { get; private set; }

        private string _rawRequestHeaders;

        private bool _parsed;

        public Request(string requestHeaders)
        {
            _rawRequestHeaders = requestHeaders;
        }

        public void ParseHeaders()
        {
            if (!_parsed)
            {
                DoParse();
                _parsed = true;
            }
        }

        private void DoParse()
        {
            string[] lines = _rawRequestHeaders.Split(new[] { "\r\n" }, StringSplitOptions.None);

            string[] action = lines[0].Split(' ');
            HttpMethod = action[0];
            RawUrl = action[1];
            Protocol = action[2];

            string[] path = RawUrl.Split('?');

            FilePath = HttpUtility.UrlDecode(path[0]);
            if (path.Length == 2)
                QueryString = path[1];

            Headers = new NameValueCollection();

            bool isCompletedHeader = false;

            for (var i = 1; i < lines.Length; i++)
            {
                var line = lines[i];

                if (string.IsNullOrEmpty(line))
                {
                    isCompletedHeader = true;
                    continue;
                }

                if (isCompletedHeader)
                {
                    //header后面是一个空白行，空白行后面所有内容是Body
                    Body = string.Join("\r\n", lines.Where((l, index) => index >= i));
                    break;
                }
                else
                {
                    int iSeparator = line.IndexOf(":");
                    Headers.Add(line.Substring(0, iSeparator), line.Remove(0, iSeparator + 1).TrimStart());
                }
            }
        }
    }
}
