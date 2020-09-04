using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace CTest
{
    class HttpCreator
    {
        public static HttpWebResponse Create(string url, string method, string postData, CookieContainer cookieContainer, out string result)
        {
            return Create(url, method, "", postData, null, null, cookieContainer, null, null, out result);
        }

        public static HttpWebResponse Create(string url, string method, string postData, string contentType, CookieContainer cookieContainer, out string result)
        {
            return Create(url, method, "", postData, contentType, null, cookieContainer, null, null, out result);
        }

        public static HttpWebResponse Create(string url, string method, string referrer, string postData, string contentType, Encoding encoding, CookieContainer cookieContainer, IEnumerable<string> acceptContentType, IDictionary<string, string> extraHeader, out string result, string host = null)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method;

            if (!string.IsNullOrEmpty(referrer))
                request.Referer = referrer;

            if (acceptContentType != null)
            {
                request.Accept = string.Join(",", acceptContentType);
            }

            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            if (cookieContainer != null)
            {
                request.CookieContainer = cookieContainer;
            }

            if (extraHeader != null && extraHeader.Count > 0)
            {
                foreach (var current in extraHeader)
                {
                    request.Headers.Add(current.Key, current.Value);
                }
            }

            if (host != null)
            {
                request.Host = host;
            }

            request.ContentType = "application/x-www-form-urlencoded";
            if (!string.IsNullOrEmpty(contentType))
            {
                request.ContentType = contentType;//application/vnd.citrix.requesttoken+xml
            }

            if (method == "post" && !string.IsNullOrEmpty(postData))
            {
                byte[] data = Encoding.UTF8.GetBytes(postData);
                request.ContentLength = data.Length;

                var reqStream = request.GetRequestStream();
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();
            }
            else if (method == "post")
            {
                request.ContentLength = 0L;
            }

            //using (Stream requestStream = request.GetRequestStream())
            //{
            //    RequestToken rt = new RequestToken();
            //    rt.RequestedLifetime = "0.08:00:00";
            //    rt.ServiceId = "2d352736-ec08-4aab-af4b-0f08a3926db7";
            //    rt.ServiceUrl = "http://ctxdev-sf.dev.cloud.com/Citrix/SF/resources/v2";
            //    rt.Write(requestStream);
            //}

            //获取服务端返回
            var response = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream(), encoding);
            result = sr.ReadToEnd().Trim();
            sr.Close();

            return response;
        }

        /// <summary>
        /// Http上传文件
        /// </summary>
        public static string HttpUploadFile(string url, string fullname, CookieContainer cookieContainer)
        {
            // 设置参数
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.CookieContainer = cookieContainer;
            request.AllowAutoRedirect = true;
            request.Method = "POST";
            string boundary = DateTime.Now.Ticks.ToString("X"); // 随机分隔线
            request.ContentType = "multipart/form-data;charset=utf-8;boundary=" + boundary;
            byte[] itemBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "\r\n");
            byte[] endBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");

            string fileName = Path.GetFileName(fullname);

            //请求头部信息 
            StringBuilder sbHeader = new StringBuilder(string.Format("Content-Disposition:form-data;name=\"file\";filename=\"{0}\"\r\nContent-Type:application/octet-stream\r\n\r\n", fileName));
            byte[] postHeaderBytes = Encoding.UTF8.GetBytes(sbHeader.ToString());

            FileStream fs = new FileStream(fullname, FileMode.Open, FileAccess.Read);
            byte[] bArr = new byte[fs.Length];
            fs.Read(bArr, 0, bArr.Length);
            fs.Close();

            Stream postStream = request.GetRequestStream();
            postStream.Write(itemBoundaryBytes, 0, itemBoundaryBytes.Length);
            postStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
            postStream.Write(bArr, 0, bArr.Length);
            postStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
            postStream.Close();

            //发送请求并获取相应回应数据
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            //直到request.GetResponse()程序才开始向目标网页发送Post请求
            Stream instream = response.GetResponseStream();
            StreamReader sr = new StreamReader(instream, Encoding.UTF8);
            //返回结果网页（html）代码
            string content = sr.ReadToEnd();
            return content;
        }


        /// <summary>
        /// Http上传文件
        /// </summary>
        public static string HttpUploadFile(string url, string fileName, byte[] data, CookieContainer cookieContainer)
        {
            // 设置参数
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.CookieContainer = cookieContainer;
            request.AllowAutoRedirect = true;
            request.Method = "POST";
            string boundary = DateTime.Now.Ticks.ToString("X"); // 随机分隔线
            request.ContentType = "multipart/form-data;charset=utf-8;boundary=" + boundary;
            byte[] itemBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "\r\n");
            byte[] endBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");

            //请求头部信息 
            StringBuilder sbHeader = new StringBuilder(string.Format("Content-Disposition:form-data;name=\"file\";filename=\"{0}\"\r\nContent-Type:application/octet-stream\r\n\r\n", fileName));
            byte[] postHeaderBytes = Encoding.UTF8.GetBytes(sbHeader.ToString());

            Stream postStream = request.GetRequestStream();
            postStream.Write(itemBoundaryBytes, 0, itemBoundaryBytes.Length);
            postStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
            postStream.Write(data, 0, data.Length);
            postStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
            postStream.Close();

            //发送请求并获取相应回应数据
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            //直到request.GetResponse()程序才开始向目标网页发送Post请求
            Stream instream = response.GetResponseStream();
            StreamReader sr = new StreamReader(instream, Encoding.UTF8);
            //返回结果网页（html）代码
            string content = sr.ReadToEnd();
            return content;
        }
    }
    
}
