using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using tpc;
using System.Net;
using System.Xml.Serialization;
using System.Xml;
using System.Runtime.Remoting.Messaging;
using System.Collections;
using System.Threading;
using Thrift.Transport;
using Thrift.Protocol;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Reflection.Emit;
using System.Linq.Expressions;
using System.Reflection;
using System.Diagnostics;
using System.DirectoryServices;
using System.Data;
using System.ComponentModel;
using scheduler;
using System.Runtime.Serialization.Json;
using System.Net.Mail;
using System.Globalization;
using mdl;
using mdl.Attributes;
using mdl.Interface;
using RestSharp;
using System.Security.Cryptography;
using System.Net.Http;
using Quartz;
using MediaToolkit.Model;
using MediaToolkit;

namespace CTest
{
    class Program
    {
        static readonly object _locker = new object();
        static async Task Main(string[] args)
        {
            await Task.Delay(0);

            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            var timestamp1 = (int)(DateTime.Now - startTime).TotalSeconds;
            Console.WriteLine("sl-{0}", timestamp1);

            #region Marshal intptr
            //string sTestMarshalIntPtr = "abc";
            //var intptr = Marshal.StringToCoTaskMemAuto(sTestMarshalIntPtr);
            //Console.WriteLine(intptr); 
            #endregion

            #region regex.replace unicode \s
            //var s = "v 1.　　3";
            //var s2 = Regex.Replace(s, @"\s", ""); 

            /*var json = @"申请人不在餐厅用
餐，因此无法对本次审核的审核结果作签字确认";

            Console.WriteLine(value: Regex.Replace(input: json, pattern: "\\s", replacement: ""));*/
            #endregion

            #region post xml
            //var result = "";
            //StoreTest.Create("http://localhost:12439/ashx/api.ashx?action=xml", "post", "", "<abc>123</abc>", "text/xml", null, null, null, out result);
            //Console.WriteLine(result); 
            #endregion

            #region socker udp
            //Task.Run(() =>
            //{
            //    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            //    var ipendpoint = new IPEndPoint(IPAddress.Any, 888);
            //    socket.Bind(ipendpoint);
            //    EndPoint endpoint = (EndPoint)ipendpoint;
            //    byte[] recv = new byte[65536];
            //    var length = socket.ReceiveFrom(recv, ref endpoint);
            //    var s_recv = Encoding.UTF8.GetString(recv, 0, length);
            //    Console.WriteLine(s_recv);
            //    socket.Close();
            //}); 
            #endregion

            #region 220538.19f
            //var f = 220538.19f;
            //Console.WriteLine(f);
            //var s = $@"123,
            //                    {f},{1}";
            //Console.WriteLine(s); 
            #endregion

            #region webserver
            //WebServer.rootPath = @"C:\Users\cspactera\source\repos\datahub\page";
            //WebServer.port = 8999;
            //WebServer.Start(); 
            #endregion

            #region datetime.parse
            //Console.WriteLine(DateTime.Parse("Thu, 30 Nov 2017 06:35:34 GMT").ToString("yyyy-MM-dd HH:mm")); 
            #endregion

            #region onsiteapp
            //string url = $"https://onsite.huaxiadnb.cn/admin/logic/api100.ashx?action=getordersamplebyid";


            //var res = HttpCreator.Create(url, "post", null, "oiId=4353309", null, null, null, null, null, out string re);
            //Console.WriteLine(re); 
            #endregion

            #region
            //var postData = File.ReadAllText("2.json");
            //var url = "https://ude-stg.opal.dnb.com/v2/entity/Business/CHN/job/42f5f43eede14f27a8675e93dfd6cfc1";
            //var dict = new Dictionary<string, string>() { { "Authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzUxMiIsImtpZCI6InVvYSJ9.eyJpc3MiOiJzc2p3dCIsInN1YiI6MTg5LCJpYXQiOjE1ODQ1MDIyNjcsImV4cCI6MTU4NDU0NTQ2NywiZW1haWwiOiJqaWFuZ2tldmluQGh1YXhpYWRuYi5jb20iLCJnaXZlbl9uYW1lIjoiVXNlciBBcGkgLSIsImZhbWlseV9uYW1lIjoiVURFIFRlc3QgS2V5IiwibmFtZSI6IlVzZXIgQXBpIC0gVURFIFRlc3QgS2V5IiwicHJlZmVycmVkX3VzZXJuYW1lIjoiYXBpa2V5Iiwicm9sZXMiOlsiQkxUX1VTRVJ8QUFBQUFJQT0iLCJVRE9OX1JFQURFUnxBQUFBQUlBPSJdLCJwZXJtcyI6WyJCSUZMSVNUX0JJRkxJU1RYTUxfR0VUIiwiQklGTElTVF9CSUZMSVNUWE1MX0dFVHxBQUFBQUlBPSIsIkJMVF9BUFB8QUFBQUFJQT0iLCJCTFRfQ0hBTkdFSU5TVEFOQ0VfR0VUfEFBQUFBSUE9IiwiQkxUX0ZJTEVGT1JNQVRfR0VUfEFBQUFBSUE9IiwiQkxUX0pPQl9HRVR8QUFBQUFJQT0iLCJCTFRfSk9CSU5TVEFOQ0VfQ0xFQVJXT1JLSU5HQ09QWV9ERUxFVEV8QUFBQUFJQT0iLCJCTFRfSk9CSU5TVEFOQ0VfR0VUfEFBQUFBSUE9IiwiQkxUX0pPQklOU1RBTkNFX1BVU0hUT1BST0RfUE9TVHxBQUFBQUlBPSIsIkJMVF9SRVFVRVNUSk9CSU5TVEFOQ0VVUExPQURVUkxfUE9TVHxBQUFBQUlBPSIsIkJMVF9SVUxFX0dFVHxBQUFBQUlBPSIsIkJMVF9XT1JLRkxPV19HRVR8QUFBQUFJQT0iLCJHRU9SRUZfQ09VTlRSSUVTX0dFVCIsIkdFT1JFRl9DT1VOVFJJRVNfR0VUfEFRQUFBQT09IiwiR0VPUkVGX1JFR0lPTlNfR0VUIiwiR0VPUkVGX1JFR0lPTlNfR0VUfEFRQUFBQT09IiwiVURPTl9EVU5TRE9DX0RFV1NXT1JLSU5HQ09QWV9HRVQiLCJVRE9OX0RVTlNET0NfREVXU1dPUktJTkdDT1BZX0dFVHxBQUFBQUlBPSIsIlVET05fRFVOU0RPQ19QUk9EVUNUSU9OX0dFVCIsIlVET05fRFVOU0RPQ19QUk9EVUNUSU9OX0dFVHxBQUFBQUlBPSIsIlVNTV9BUFAiLCJVTU1fQVBQfEFBQUFBSUE9Il19.ir_3jsFZ3aiD8DEMiCL4YyQGcX_UAZO3FoFPvxVWRkuWhOQzAOuYf46DoDo3Lmnymy9z-bE91wcxHQVE0otp6XSDe5B248XmjgBHidn80Jg3hwtctLRSZR-VAIwPEqFRrPOxrieyVYO2W58j47lqUb2vzphlr9nmJrcx-LWOIrWWTp1SY2RrCW8Hq_sdKtTFEeGvfCAwn5PTFIfQrzY6B8GFmRkxX0uOwR1WoApp9hjrVXbZEYUcQe7pisjak5CvfQUs2SeJDIWxNPQ9km9w6oRmgW9C3DJjr0sevraBMXsTEQF5q2NpV6e4OWo5WvFLOIkTz0F2bAlzV2_7Ak79HQ" } };
            //var accept = new List<string>() { "application/json", "charset=utf-8" };

            //HttpCreator.Create(url, "post", null, postData, "raw", null, null, accept, dict, out string str);

            //Console.WriteLine(str);
            #endregion

            #region thrift client
            //try
            //{
            //    TTransport transport = new TSocket("localhost", 8888);
            //    transport.Open();
            //    TProtocol protocol = new TBinaryProtocol(transport);
            //    AgentServices.Client client = new AgentServices.Client(protocol);
            //    var socket = (TSocket)client.InputProtocol.Transport;
            //    var ipendpoint = (IPEndPoint)socket.TcpClient.Client.LocalEndPoint;
            //    var ipaddress = ipendpoint.Address.ToString();
            //    var token = client.Token(ipaddress);
            //    var logret = client.ExplicitForms_Login(token, "10101", "123qwe,.");

            //    Console.WriteLine(logret.Error);
            //    Console.WriteLine(logret.Result);

            //    if (logret.Result)
            //    {
            //        var list = client.GetList(token);
            //        Console.WriteLine(list.Count);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex);
            //}
            #endregion

            #region json deser ignore case is automatic
            //var x = File.ReadAllText(@"E:\1.txt");
            //var json = j.DeserializeJsonToList<IAS>(x);
            //for (var i = 0; i < json.Count; i++)
            //{
            //    var m = json[i];
            //    if (m.partNumber - 1 != i)
            //    {
            //        Console.WriteLine(i);
            //    }
            //}
            //Console.WriteLine(json.Count);
            #endregion

            #region schedule
            //schddemo8();
            //_ = ScheduleJobChangeTrigger(); 
            #endregion




            #region pause
            Console.ReadKey();
            #endregion

        }

        public static void MediaToolkitTest()
        {
            var inputFile = new MediaFile { Filename = @"E:\tool\(合并)Git-2.27.0-64-bit-1.exe" };

            using (var engine = new Engine())
            {
                engine.GetMetadata(inputFile);
            }

            Console.WriteLine(inputFile.Metadata.Duration);
        }

        public static async Task GetSizeFile()
        {
            await SplitF.Split(@"C:\Users\kan_y\Videos\花木兰刘亦菲.mp4", @"C:\Users\kan_y\Videos\花木兰刘亦菲-1g.mp4", 1024 * 1024 * 1024);
        }

        public static string GetVideoMetadata(string filename)
        {
            if (Path.GetExtension(filename).ToLower() == ".gif")
            {
                return null;
            }
            StreamReader sr;
            ProcessStartInfo s = new ProcessStartInfo();
            // s.RedirectStandardInput = true;
            // s.RedirectStandardOutput = true;
            s.RedirectStandardError = true;
            s.UseShellExecute = false;
            s.FileName = Path.Combine(@"E:\tool\ffmpeg-18639", "ffmpeg.exe");
            s.Arguments = $"-i \"{filename}\"";
            s.WindowStyle = ProcessWindowStyle.Hidden;
            s.CreateNoWindow = true;
            Process p = Process.Start(s);
            // sr = p.StandardOutput;
            sr = p.StandardError;

            var output = sr.ReadToEnd();
            sr.Close();
            p.WaitForExit();
            p.Close();

            var lines = output.Split('\n');
            var duration_line = lines.FirstOrDefault(o => Regex.Match(o, "Duration:").Success);
            if (string.IsNullOrEmpty(duration_line))
            {
                return null;
            }
            var duration = Regex.Match(duration_line, "\\d{2}:\\d{2}:\\d{2}").Value;
            var bitrate = Regex.Match(duration_line, "bitrate:\\s*(\\d*)\\skb/s").Groups[1].Value;
            int.TryParse(bitrate, out int i_bitrate);

            long totalsecond = 0L;
            var duration_split = duration.Split(':');
            var hour = int.Parse(duration_split[0]);
            var minute = int.Parse(duration_split[1]);
            var second = int.Parse(duration_split[2]);

            totalsecond += hour * 60 * 60;
            totalsecond += minute * 60;
            totalsecond += second;

            if (i_bitrate == 0 && totalsecond < 20)
            {
                return null;
            }

            var video_line = lines.FirstOrDefault(o => Regex.Match(o, "Stream.*Video:").Success);
            if (string.IsNullOrEmpty(video_line))
            {
                return null;
            }
            var videoinfo = Regex.Replace(video_line, "Stream.*Video:", "").Trim().Split(',');
            if (videoinfo.Length < 2)
            {
                return null;
            }
            var format = videoinfo[0];
            var colorModel = videoinfo.Length > 5 ? videoinfo[1].Trim() : "";
            var frameSize = videoinfo.FirstOrDefault(o => Regex.Match(o, "^\\s*\\d{2,6}x\\d{2,6}").Success);
            if (!string.IsNullOrEmpty(frameSize))
            {
                frameSize = Regex.Match(frameSize, "^\\s*\\d{2,6}x\\d{2,6}").Value.Trim();
            }
            else
            {
                frameSize = "0x0";
            }
            var fps = videoinfo.FirstOrDefault(o => Regex.Match(o, "\\stbr$").Success);
            if (!string.IsNullOrEmpty(fps))
            {
                fps = Regex.Replace(fps, "\\stbr", "").Trim();
            }
            double.TryParse(fps, out double d_fps);
            Console.WriteLine(bitrate);
            Console.WriteLine(duration); 
            Console.WriteLine(totalsecond);
            Console.WriteLine(format);
            Console.WriteLine(frameSize);
            Console.WriteLine(fps);

            return "";
        }

        public static void LqTake()
        {
            var lst = new List<xx>() { new xx() { label = "a", value = "b" }, new xx() { label = "c", value = "d" } };
            var x = lst.Skip(1).Take(1).ToList();
            lst = lst.Take(1).ToList();
            Console.WriteLine(x.Count);
            var z = lst.First();
            lst.Clear();
            lst.Add(z);
            Console.WriteLine(lst.Count);
        }

        public static string SHA256Encrypt(string message)
        {
            byte[] tmpByte;
            using (SHA256 sha256 = new SHA256Managed())
            {
                tmpByte = sha256.ComputeHash(Encoding.GetEncoding("gbk").GetBytes(message));
                return string.Concat(tmpByte.Select(b => b.ToString("x2")));
            }
        }

        public static async Task ByteWriteTest()
        {
            MemoryStream ms = new MemoryStream();
            var bytes = Enumerable.Range(1, 501).Select(i => (byte)i).ToArray();
            var section_length = 100;

            for (var bts_position = 0; bts_position < bytes.Length; bts_position += section_length)
            {
                var ms2 = new MemoryStream();
                var maxlength = section_length;
                if (bts_position + section_length > bytes.Length)
                {
                    maxlength = bytes.Length - bts_position;
                }
                await ms2.WriteAsync(bytes, bts_position, maxlength);
                Console.WriteLine(string.Join(",", ms2.ToArray()));
                await ms.WriteAsync(bytes, bts_position, maxlength);
                Console.WriteLine(maxlength);
            }
        }

        public static void GetVideoMetadata()
        {
            /*
FFmpeg version SVN-r18639, Copyright (c) 2000-2009 Fabrice Bellard, et al.
  configuration: --enable-memalign-hack --enable-postproc --enable-gpl --enable-libfaac --enable-libfaad --enable-libgsm --enable-libmp3lame --enable-libvorbis --enable-libtheora --enable-libx264 --enable-libxvid --disable-ffserver --enable-avisynth --enable-pthreads
  libavutil     50. 3. 0 / 50. 3. 0
  libavcodec    52.27. 0 / 52.27. 0
  libavformat   52.32. 0 / 52.32. 0
  libavdevice   52. 2. 0 / 52. 2. 0
  libswscale     0. 7. 1 /  0. 7. 1
  libpostproc   51. 2. 0 / 51. 2. 0
  built on Apr 21 2009 13:44:38, gcc: 4.2.4 (TDM-1 for MinGW)

Seems stream 0 codec frame rate differs from container frame rate: 47.95 (20000000/417083) -> 23.98 (24000/1001)
Input #0, mov,mp4,m4a,3gp,3g2,mj2, from '1.txt':
  Duration: 01:55:08.71, start: 0.000000, bitrate: 9 kb/s
    Stream #0.0(und): Video: h264, yuv420p, 1920x1080 [PAR 1:1 DAR 16:9], 23.98 tbr, 23.98 tbn, 47.95 tbc
    Stream #0.1(und): Audio: aac, 48000 Hz, stereo, s16
At least one output file must be specified
             * 
             * 
             * 
             * 
FFmpeg version SVN-r18639, Copyright (c) 2000-2009 Fabrice Bellard, et al.
  configuration: --enable-memalign-hack --enable-postproc --enable-gpl --enable-libfaac --enable-libfaad --enable-libgsm --enable-libmp3lame --enable-libvorbis --enable-libtheora --enable-libx264 --enable-libxvid --disable-ffserver --enable-avisynth --enable-pthreads
  libavutil     50. 3. 0 / 50. 3. 0
  libavcodec    52.27. 0 / 52.27. 0
  libavformat   52.32. 0 / 52.32. 0
  libavdevice   52. 2. 0 / 52. 2. 0
  libswscale     0. 7. 1 /  0. 7. 1
  libpostproc   51. 2. 0 / 51. 2. 0
  built on Apr 21 2009 13:44:38, gcc: 4.2.4 (TDM-1 for MinGW)

Seems stream 0 codec frame rate differs from container frame rate: 59.94 (60000/1001) -> 29.97 (60000/2002)
Input #0, mov,mp4,m4a,3gp,3g2,mj2, from 'C:\Users\kan_y\Videos\Captures\Feedback Hub 2020-10-09 17-39-59.mp4':
  Duration: 00:00:24.81, start: 0.000000, bitrate: 4380 kb/s
    Stream #0.0(und): Video: h264, yuv420p, 1024x804 [PAR 1:1 DAR 256:201], 29.97 tbr, 30k tbn, 59.94 tbc
    Stream #0.1(und): Audio: aac, 48000 Hz, stereo, s16
At least one output file must be specified
             */
            StreamReader sr;
            ProcessStartInfo s = new ProcessStartInfo();
            // s.RedirectStandardInput = true;
            // s.RedirectStandardOutput = true;
            s.RedirectStandardError = true;
            s.UseShellExecute = false;
            s.FileName = @"E:\tool\ffmpeg-18639\ffmpeg.exe";
            s.Arguments = @"-i C:\Users\kan_y\Videos\花木兰刘亦菲-1.txt";
            // s.Arguments = @"-i ""C:\Users\kan_y\Videos\Captures\Feedback Hub 2020-10-09 17-39-59.mp4""";
            // s.Arguments = @"-i C:\Users\kan_y\Pictures\x5.jpg";
            Process p = Process.Start(s);
            // sr = p.StandardOutput;
            sr = p.StandardError;

            var output = sr.ReadToEnd();
            Console.WriteLine(output);
            sr.Close();
            p.WaitForExit();
            p.Close();

            var lines = output.Split('\n');
            var duration_line = lines.FirstOrDefault(o => Regex.Match(o, "Duration:").Success);
            var duration = Regex.Match(duration_line, "\\d{2}:\\d{2}:\\d{2}").Value;
            var bitrate = Regex.Match(duration_line, "bitrate:\\s*(\\d*)\\skb/s").Groups[1].Value;
            Console.WriteLine(duration);
            Console.WriteLine(bitrate);

            var video_line = lines.FirstOrDefault(o => Regex.Match(o, "Stream.*Video:").Success);
            var videoinfo = Regex.Replace(video_line, "Stream.*Video:", "").Trim().Split(',');
            var format = videoinfo[0];
            var colorModel = videoinfo[1].Trim();
            var frameSize = videoinfo.FirstOrDefault(o => Regex.Match(o, "\\d{1,6}x\\d{1,6}").Success);
            frameSize = Regex.Match(frameSize, "\\d{1,6}x\\d{1,6}").Value;
            var fps = videoinfo.FirstOrDefault(o => Regex.Match(o, "\\stbr$").Success);
            fps = Regex.Replace(fps, "\\stbr", "").Trim();
            Console.WriteLine(format);
            Console.WriteLine(colorModel);
            Console.WriteLine(frameSize);
            Console.WriteLine(fps);
        }

        public static void GetStringThenGetBytes()
        {
            var s = new byte[] { 1, 2, 3, 60, 6, 7, 102, 230 };
            var x = Encoding.UTF8.GetString(s, 0, s.Length);
            byte[] y = Encoding.UTF8.GetBytes(x);
            Console.WriteLine($"s.Length:{s.Length}\ty.Length:{y.Length}");
            for (var i = 0; i < s.Length; i++)
            {
                if (s[i] != y[i])
                {
                    Console.WriteLine(s[i] + "," + y[i]);
                    Console.WriteLine("-------------");
                }
            }
        }

        public static string GenNonStr(int length)
        {
            char[] verification = new char[length];
            char[] dictionary = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
                '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
            };
            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                verification[i] = dictionary[random.Next(dictionary.Length - 1)];
            }
            return new string(verification);
        }

        public static string SHA256(string fullname)
        {
            FileStream stream = new FileStream(fullname, FileMode.Open);

            SHA256Managed Sha256 = new SHA256Managed();
            byte[] by = Sha256.ComputeHash(stream);

            return BitConverter.ToString(by).Replace("-", "").ToLower();
        }

        public static string FileMd5(string fullname)
        {
            try
            {
                FileStream file = new FileStream(fullname, FileMode.Open);
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                Console.WriteLine(sb.ToString());
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
            }
        }

        public static string FileSHA256(string fullname)
        {
            FileStream stream = new FileStream(fullname, FileMode.Open);

            SHA256Managed Sha256 = new SHA256Managed();
            byte[] by = Sha256.ComputeHash(stream);

            var x = BitConverter.ToString(by).Replace("-", "").ToLower();
            Console.WriteLine(x);
            return BitConverter.ToString(by).Replace("-", "").ToLower();
        }

        static void upload()
        {
            var client = new RestClient("http://172.18.132.140:18080/common/uploadMultipartFile");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Cookie", "SESSION=OWMyMjg0NDktYzBkZS00OGMwLWI5NWUtN2I2ZjY1YWQ3MmU4");
            request.AddParameter("bucketName", "com.huacemedia.temp");
            request.AddParameter("keyName", "Git-2.27.0-64-bit.exe");
            request.AddParameter("uploadId", "0aU3Ga.OJt7_0RYjOKLgPnOj1rltdbHGw71WzQlFySdQuOg4rfs.yjNhgzf77wCM1di57e.vBj0SbBApgSSAhv6tYE93LmmYn2nUTzSNgDORys0HHF_qB6CEWAkT1nviygQoYmWZzu0K1O0W1rHk7g--");
            request.AddParameter("partNumber", "1");
            request.AddFile("file", "/Users/kan_y/Downloads/Git-2.27.0-64-bit-0.exe");
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
        }

        static void testupload()
        {
            // 登录
            var cookieContainer = new CookieContainer();
            HttpCreator.Create("http://172.18.132.140:18080/user/userLogin", "post", "{ \"account\": \"1\", \"password\": \"1\"}", "application/json", cookieContainer, out string result);
            Console.WriteLine(result);
            // 初始化上传接口
            var url_uploadinit = "http://172.18.132.140:18080/common/initUploadFileTask";
            var data_uploadinit = "{\"bucketName\": \"com.huacemedia.temp\", \"contentType\": \"video/mpeg4\", \"keyName\": \"Git-2.27.0-64-bit.exe\"}";// drive.png
            HttpCreator.Create(url_uploadinit, "post", data_uploadinit, "application/json", cookieContainer, out result);
            Console.WriteLine(result);
            // uploadId
            var uploadId = tpc.j.DeserializeJObject(result, "data", "uploadId");
            // 分段上传接口
            var bucketName = "com.huacemedia.temp";
            var fullname = @"E:\tool\Git-2.27.0-64-bit.exe"; // 上传的完整文件（未分割前）的完整文件名 // drive.png
            var keyName = "Git-2.27.0-64-bit.exe";
            var sectionsize = 5 * 1024 * 1024L;
            FileStream fs = new FileStream(fullname, FileMode.Open, FileAccess.Read);
            // 保存上传成功时返回的ETag
            var lst = new List<IAS>();
            for (long i = 0, j = 1; i < fs.Length; i += sectionsize, j++)
            {
                #region bytes长度
                long length = sectionsize;
                if (i + length > fs.Length)
                {
                    length = fs.Length - i;
                }
                if(length == 0)
                {
                    break;
                }
                #endregion
                byte[] bts = new byte[length];
                fs.Read(bts, 0, (int)length);
                var partNumber = j;
                var url_upload = $"http://172.18.132.140:18080/common/uploadMultipartFile?bucketName={bucketName}&keyName={keyName}&partNumber={partNumber}&uploadId={uploadId}";
                result = HttpCreator.HttpUploadFile(url_upload, keyName, bts, cookieContainer);
                lst.Add(new IAS() { partNumber = (int)j, etag = tpc.j.DeserializeJObject(result, "data", "etag") });

                Console.WriteLine($"上传第{j}部分,result:{result}");
            }

            Console.WriteLine("上传完成，开始提交完成request。。");
            var url_complete = "http://172.18.132.140:18080/common/completeMultipartUploadFile";
            var data_complete = $@"{{""bucketName"": ""{bucketName}"", ""keyName"": ""{keyName}"", ""partETagResList"": {j.SerializeObject(lst)}, ""uploadId"": ""{uploadId}""}}";
            HttpCreator.Create(url_complete, "post", data_complete, "application/json", cookieContainer, out result);
            Console.WriteLine(result);
        }

        static void testdownload()
        {
            var bucketName = "com.huacemedia.temp";
            var keyName = "Git-2.27.0-64-bit.exe";
            int i = 0;
            long getlength;
            var sectionsize = 48066136;// 5 * 1024 * 1024; // 一段的大小
            do
            {
                var start = i * sectionsize;
                var end = (i + 1) * sectionsize - 1; // api用的是结束位置，而不是length，这里要-1
                var url_download = $"http://172.18.132.140:18080/common/downloadMultipartFile?bucketName={bucketName}&end={end}&keyName={keyName}&start={start}";
                var dest_name = $@"E:\tool\xx{i}.png";
                HttpCreator.HttpDownload(url_download, dest_name, out getlength);
                i++;
                Console.WriteLine($"下载{getlength}字节.");
            } while (getlength == sectionsize);

            Console.WriteLine("done.");
            //// 如果下载出错，里面是错误信息
            // File.Copy(dest_name, dest_name + ".txt");
        }

        public async static Task StartNewMultiAsync(string bucketName, string keyName, long size, int workthreadnum, string destName, Action<long, int, string> action, Action callback, int sectionSize = 1 * 1024)
        {
            var files = await Task.WhenAll(Enumerable.Range(1, workthreadnum).Select(i =>
            {
                var tasksize = size / workthreadnum;
                long start = (i - 1) * tasksize;
                long end = start + tasksize;
                if (i == workthreadnum)
                {
                    end = size;
                }
                return Task.Run(async () => await WorkAsync(bucketName, keyName, i, start, end, action, callback, sectionSize));
            }));

            var lst = files.OrderBy(s =>
            {
                var num = System.Text.RegularExpressions.Regex.Match(s, "-(\\d+)\\.temp$").Groups[1].Value;
                return int.Parse(num);
            }).ToList();
            var c = 1;
            await SplitF.Merge(destName, lst, c);
            callback?.Invoke();
        }
        async static Task<string> WorkAsync(string bucketName, string keyName, int thread_nth, long taskstart, long taskend, Action<long, int, string> action, Action callback, int sectionSize)
        {
            int sectionNum = 0;
            long getlength;
            long sectionsize = sectionSize * 1024; // 一段的大小
            var tempfile = Directory.GetCurrentDirectory() + "\\tempFiles\\" + Guid.NewGuid().ToString() + "-" + thread_nth + ".temp";
            do
            {
                long start = taskstart + sectionNum * sectionsize;
                long end = taskstart + (sectionNum + 1) * sectionsize - 1; // api用的是结束位置，而不是length，这里要-1
                if (end > taskend - 1) // api中，下载到taskend位置的字节，只需要等于taskend - 1
                {
                    end = taskend - 1;
                }
                var url_download = $"http://172.18.132.140:18080/common/downloadMultipartFile?bucketName={bucketName}&end={end}&keyName={keyName}&start={start}";
                getlength = await HttpCreator.HttpDownload(url_download, tempfile);
                sectionNum++;

                action?.BeginInvoke(end + 1, thread_nth, tempfile, null, null);
            } while (getlength == sectionsize && (taskstart + sectionNum * sectionsize < taskend));

            return tempfile;
        }

        static void JObjectTest()
        {
            var s = @"{
                    ""message"": ""ok"",
                    ""code"": ""SUCCESS"",
                    ""data"": {
                        ""uploadId"": ""wpATz0qb8XrHMpP9odBgsJw4jUL1r8l15FN9oeG01f8azUIT_2dpShZdaCCDVCOn5SGozj7GcYTTTahGZ1mytRk2486d3PShcogsdLmFnZggMc.X490m1D6yQb04a7_a3TBSKfCRmuBa9jcnD7E84Q--""
                    },
                    ""success"": true
            }";
            var o = tpc.j.DeserializeJObject(s, "data", "uploadId");
            Console.WriteLine(o);
        }

        static IEnumerator<mdl.point> set()
        {
            foreach (var c in "abcdefghijkl")
            {
                yield return new mdl.point(c, c);
                Console.WriteLine(1);
            }
            Console.WriteLine(0);
            yield break;
        }
        
        static async void testd()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var t = d();
            //await Task.Delay(500);
            //Console.WriteLine(t.IsCompleted);
            var r = await t;
            sw.Stop();
            Console.WriteLine(r.Length);
            Console.WriteLine(sw.ElapsedMilliseconds);
        }

        static async Task<byte[]> d()
        {
            var bts = new byte[1024 * 1024 * 1000];
            await new FileStream("C:\\tool\\cn_sql_server_2016_enterprise_x64_dvd_8699450.iso", FileMode.Open).ReadAsync(bts, 0, bts.Length);
            return bts;
        }

        /// <summary>
        /// 反序列化集合对象
        /// </summary>
        public static T[] JsonDeserializeToArrayData<T>(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T[]));//非集合对象把T[]改成T
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T[] arrayObj = (T[])ser.ReadObject(ms);
            return arrayObj;
        }


        public static string ConcatToXml(Dictionary<string, string> variables)
        {
            var declaration = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            var xml = declaration + "<root>";
            foreach (var v in variables)
            {
                xml += $"<{v.Key}>{v.Value}</{v.Key}>";
            }
            xml += "</root>";
            return xml;
        }
        
        static void my()
        {
            var cc = new CookieContainer();
            //cc.Add(new Cookie() { Domain = "localhost", Path = "/", Name = "mycrm_company", Value = "11b11db4-e907-4f1f-8835-b9daab6e1f23" });
            //cc.Add(new Cookie() { Domain = "localhost", Path = "/", Name = "mycrm_isendcompany", Value = "1" });
            var re = "";

            var user = "admin";
            var psw = "c4ca4238a0b923820dcc509a6f75849b";//ende.MD5_32("1");
            var posturl = $"http://localhost:8091/Default_Login.aspx?rdnum={Guid.NewGuid()}";
            var postdata = $"usercode={user}&validateCode=&psw={psw}&MyCurrentCompany=&rememberusercode=";
            HttpCreator.Create(posturl, "post", "", postdata, "", null, cc, null, null, out re);

            Console.WriteLine(re);
            Console.WriteLine("\n");

            var serviceurl = $"http://localhost:8091/fygl/pub/PUB_XMLHTTP.aspx?ywtype=ChkExistFirstCost&Year=2018";
            HttpCreator.Create(serviceurl, "post", "", "", "", null, cc, null, null, out re);

            Console.WriteLine(re);
        }

        static void createdatatable()
        {
            var dt = new DataTable();
            dt.Columns.Add(new DataColumn("m"));
            dt.Columns.Add(new DataColumn("year"));
            dt.Columns.Add(new DataColumn("scope"));

            dt.Rows.Add("a", "18", "20.2");
            dt.Rows.Add("b", "18", "2.6");
            dt.Rows.Add("a", "18", "22.2");
            dt.Rows.Add("a", "19", "29.66");
            dt.Rows.Add("b", "18", "20.16");
            dt.Rows.Add("b", "18", "12.6");

            var rows = dt.Select("m='a' and year=" + 18);
            Console.WriteLine(rows.Length);
            //var sumf = rows.Sum(row => Single.Parse(row["scope"].ToString()));
            //Console.WriteLine($"sum:{sumf}");
        }

        static void reqxpt()
        {
            /*
             * 首先将目录”/service/Mysoft.Slxt.Service.Student/“添加到web.config
             * <add key="PageWithoutCheckSession" value="/Default.aspx,/xxx/" />
             * value里面
             */
            string re;
            var postdata = "8049ee9f-85ff-e811-97c5-fc017c5b3e8e";
            HttpCreator.Create("http://localhost/service/Mysoft.Slxt.Service.Student/GetApproveState.aspx", "post", "", postdata, "application/json", Encoding.GetEncoding("gb2312"), null, null, new Dictionary<string, string>() { { "x-charset", "utf-8" }, { "Accept-Charset", "utf-8" } }, out re);
            Console.WriteLine(re);
        }
        
        [DllImport("rpcrt4.dll", SetLastError = true)]
        private static extern int UuidCreateSequential(out Guid guid);
        const int RPC_S_OK = 0;
        
        /// <summary>
        /// 获取新的<b>有序Guid</b>，用于数据的主键，不要使用Guid.NewGuid方法获取。
        /// </summary>
        /// <returns></returns>
        public static Guid SeqGuid()
        {
            Guid guid;
            int result = UuidCreateSequential(out guid);
            if (result == RPC_S_OK) return guid;
            return Guid.NewGuid();
        }



        static void regt()
        {
            //var currtext = File.ReadAllText("1.aspx");
            //var ms = Regex.Matches(currtext, "src\\s*=\\s*[\"']?(.+)\\.js", RegexOptions.IgnoreCase);
            //foreach (Match m in ms)
            //{
            //    var v = m.Groups[1].Value;
            //    Console.WriteLine(v);
            //}

            //ms = Regex.Matches(currtext, "RefJsFileHtml\\s*\\(\\s*\"(.+)\\.js", RegexOptions.IgnoreCase);
            //foreach (Match m in ms)
            //{
            //    var v = m.Groups[1].Value;
            //    Console.WriteLine(v);
            //}


            //
            var text = @"var re = openXMLHTTP(""DSGL_XMLHTTP.aspx?guid="" + tr.oid, ""DeleteDSProvider"");
                if (re == ""order"") {";
            var text2 = @"                var re = openXMLHTTP(""DSGL_XMLHTTP.aspx?guid="" + tr.oid, ""DeleteDSProvider""
            kldsld);sfsdfsdf
                if (re == ""order"") {";

            var m2 = Regex.Match(text, "\\(.*?,\\s*(.*?)[\\s,\\r\\n\\)]");//在"[]"里的圆括号加不加"\\"没区别？
            Console.WriteLine(m2.Groups[1].Value.Replace("\"", ""));

            //var m2 = Regex.Match(text, "[\\)]");
            //Console.WriteLine(m2.Success);
        }

        static void testCommerceApi()
        {

            var dict = new Dictionary<string, string>()
            {
                { "Authorization", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoibHZmIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiJsdmYiLCJVc2VySW5mbyI6IntcIlVzZXJJZFwiOjksXCJVc2VyTmFtZVwiOlwibHZmXCIsXCJNb2JpbGVQaG9uZVwiOlwiMTU5MjIyMzY2OTZcIixcIkxvZ2luTmFtZVwiOlwibHZmXCIsXCJPcGVuSWRcIjpudWxsfSIsImp0aSI6IjkiLCJleHAiOjE1NTYyNzY0NDIsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjQ0MzQwIiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzNDAifQ.zp8Jw8xQuxcPTVLc1axUJchmb0UYBOYOoNQJr3ybZVo"}
            };

            Stopwatch sw = new Stopwatch();
            sw.Start();
            string re;
            var response = HttpCreator.Create("http://47.99.73.152:5000/api/Commerce/ChangeCommerce/1", "post", "", "", "", null, null, null, dict, out re);
            sw.Stop();
            Console.WriteLine("ts:" + sw.Elapsed.TotalSeconds + " re:" + re);
        }

        static async void schddemo2()
        {
            await QuartzMethod.AddScheduleJob2(new mdl.schd.SchedulingTask()
            {
                EndTime = DateTime.Now.AddYears(30),
                Id = 1,
                IntervalTime = "*/10 0-10 10 * * ?",//10点0分到10分每隔10秒
                RunningStatus = 1,
                StartTime = DateTime.Now,
                TaskClassFullName = "scheduler.task.demo",
                TaskDescription = "desc",
                TaskGroupName = "taskg1",
                TaskName = "test",
                TriggerGroupName = "group1",
                TriggerName = "class1"
            });
        }

        static async void schddemo3()
        {
            await QuartzMethod.AddScheduleJob2(new mdl.schd.SchedulingTask()
            {
                EndTime = DateTime.Now.AddYears(30),
                Id = 1,
                IntervalTime = "*/10 * * * * ?",//每隔10秒
                RunningStatus = 1,
                StartTime = DateTime.Now,
                TaskClassFullName = "scheduler.task.demo",
                TaskDescription = "desc",
                TaskGroupName = "taskg1",
                TaskName = "test",
                TriggerGroupName = "group1",
                TriggerName = "class1"
            });
        }

        static async void schddemo4()
        {
            await QuartzMethod.AddScheduleJob2(new mdl.schd.SchedulingTask()
            {
                EndTime = DateTime.Now.AddYears(30),
                Id = 1,
                IntervalTime = "0 */1 * * * ?",//每隔1分
                RunningStatus = 1,
                StartTime = DateTime.Now,
                TaskClassFullName = "scheduler.task.demo",
                TaskDescription = "desc",
                TaskGroupName = "taskg1",
                TaskName = "test",
                TriggerGroupName = "group1",
                TriggerName = "class1"
            });
        }

        static async void schddemo5()
        {
            await QuartzMethod.AddScheduleJob2(new mdl.schd.SchedulingTask()
            {
                EndTime = DateTime.Now.AddYears(30),
                Id = 1,
                IntervalTime = "0 */1 17-18 * * ?",//每天17到18点每隔1分（可以17到17）
                RunningStatus = 1,
                StartTime = DateTime.Now,
                TaskClassFullName = "scheduler.task.demo",
                TaskDescription = "desc",
                TaskGroupName = "taskg1",
                TaskName = "test",
                TriggerGroupName = "group1",
                TriggerName = "class1"
            });
        }

        static async void schddemo6()
        {
            await QuartzMethod.AddScheduleJob2(new mdl.schd.SchedulingTask()
            {
                EndTime = DateTime.Now.AddYears(30),
                Id = 1,
                IntervalTime = "6 */1 * * * ?",//每隔1分在6秒时执行
                RunningStatus = 1,
                StartTime = DateTime.Now,
                TaskClassFullName = "scheduler.task.demo",
                TaskDescription = "desc",
                TaskGroupName = "taskg1",
                TaskName = "test",
                TriggerGroupName = "group1",
                TriggerName = "class1"
            });
        }

        static async void schddemo7()
        {
            await QuartzMethod.AddScheduleJob2(new mdl.schd.SchedulingTask()
            {
                EndTime = DateTime.Now.AddYears(30),
                Id = 1,
                IntervalTime = "0 28 18 16 * ?",//每月16号18点28分0秒执行
                RunningStatus = 1,
                StartTime = DateTime.Now,
                TaskClassFullName = "scheduler.task.demo",
                TaskDescription = "desc",
                TaskGroupName = "taskg1",
                TaskName = "test",
                TriggerGroupName = "group1",
                TriggerName = "class1"
            });
        }

        static async void schddemo8()
        {
            await QuartzMethod.AddScheduleJob2(new mdl.schd.SchedulingTask()
            {
                EndTime = DateTime.Now.AddYears(30),
                Id = 1,
                IntervalTime = "0 0 0 ? * MON",
                RunningStatus = 1,
                StartTime = DateTime.Now,
                TaskClassFullName = "scheduler.task.demo",
                TaskDescription = "desc",
                TaskGroupName = "taskg1",
                TaskName = "test",
                TriggerGroupName = "group1",
                TriggerName = "class1"
            });
        }

        static async void schddemo()
        {
            await QuartzMethod.AddScheduleJob(new mdl.schd.SchedulingTask()
            {
                EndTime = DateTime.Now.AddYears(30),
                Id = 1,
                IntervalTime = "0|29",
                RunningStatus = 1,
                StartTime = DateTime.Now,
                TaskClassFullName = "scheduler.task.demo",
                TaskDescription = "desc",
                TaskGroupName = "taskg1",
                TaskName = "test",
                TriggerGroupName = "group1",
                TriggerName = "class1"
            });
        }

        public static async Task ScheduleJobChangeTrigger()
        {
            var scheduler = await QuartzMethod.GetScheduler();
            var trigger = await scheduler.GetTrigger(new Quartz.TriggerKey("class1", "group1"));
            //var t = trigger.GetType();//Quartz.Impl.Triggers.CronTriggerImpl
            var triggerImpl = trigger as Quartz.Impl.Triggers.CronTriggerImpl;
            // triggerImpl.CronExpressionString // 获取任务的cron表达式
            var jobDetail = await scheduler.GetJobDetail(new Quartz.JobKey("test", "taskg1"));
            // var t = jobDetail.GetType(); // JobDetailImpl
            var jobImpl = jobDetail as Quartz.Impl.JobDetailImpl;
            Console.WriteLine("下次执行时间：" + trigger.GetNextFireTimeUtc().Value.ToLocalTime());

            var newTrigger = TriggerBuilder.Create()
                            .WithIdentity("class1", "group1")
                            .StartAt(DateTimeOffset.Now) //指定开始时间
                            .EndAt(DateTimeOffset.Now.AddYears(100))//指定结束时间
                            .WithCronSchedule("*/10 * * * * ?", action => action.WithMisfireHandlingInstructionDoNothing())//使用Cron表达式//第一次启动时不执行
                            .ForJob(jobDetail) //通过JobKey识别作业
                            .Build();                // 告诉Quartz使用我们的触发器来安排作业

            await scheduler.RescheduleJob(triggerImpl.Key, newTrigger);
            var trigger2 = await scheduler.GetTrigger(new Quartz.TriggerKey("class1", "group1"));
            Console.WriteLine("更改计划后，下次执行时间：" + trigger2.GetNextFireTimeUtc().Value.ToLocalTime());
        }

        static void notes()
        {
            var jsonstr = @"[
              {
                ""mealItemID"": 1411,
                ""eventNo"": ""17010610328"",
                ""division"": ""PH"",
                ""requesterCwid"": ""GBGYH"",
                ""requesterName"": ""李XX"",
                ""requesterPhone"": ""13818723757"",
                ""diningTime"": ""2019-06-17T11:08:16.6512947+08:00"",
                ""city"": ""上海"",
                ""restaurantName"": ""渝信川菜招商局店"",
                ""restaurantLocation"": ""成都北路333号招商局广场3楼近威海路"",
                ""orderStatus"": ""Ordered"",
                ""lastModifyTime"": ""2019-06-17T11:08:16.6512988+08:00""
              }
            ]";
        }

        static void GetAttr()
        {
            var type = typeof(StaffDto);
            var propertyInfos = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in propertyInfos)
            {
                var proDescrition = property.GetCustomAttributes<TitleAttribute>();
                if (proDescrition.Count() > 0)
                {
                    Console.WriteLine("字段名：{0}，字段描述内容：{1}", property.Name, proDescrition.First().Title);
                }
            }
        }

        static void ExcelExport()
        {
            var list = new List<StaffDto>();
            var lst = new List<List<StaffDto>>();
            list.Add(new StaffDto() { Name = "a", Name2 = "b" });
            lst.Add(list);
            var excelFileHelper = new tpc.office.ExcelFileHelper();
            var url = excelFileHelper.CreateWorkbook(lst, new List<string>() { "sh1" }, "看看");
            Console.WriteLine(url);
        }

        static void ExpectUnionTest()
        {
            var lst1 = Enumerable.Range(1, 5).Select(i => new { p = i.ToString(), p2 = "abc" }).ToList();
            var lst2 = Enumerable.Range(3, 5).Select(i => new { p = i.ToString(), p2 = "abc" }).ToList();
            Console.WriteLine(string.Join(",", lst1.Union(lst2))); // 匿名对象list取并集成功
            Console.WriteLine(string.Join(",", lst1.Except(lst2).Concat(lst2))); // 同上

            var lst3 = Enumerable.Range(1, 5).Select(i => new { p = i.ToString(), p2 = new ClassA() }).ToList();
            var lst4 = Enumerable.Range(3, 5).Select(i => new { p = i.ToString(), p2 = new ClassA() }).ToList();
            Console.WriteLine(string.Join(",", lst3.Union(lst4))); // p2引用类型，未定义比较器，不能合并
            Console.WriteLine(string.Join(",", lst3.Except(lst4).Concat(lst4))); // p2引用类型，未定义比较器，不能合并
        }

        static void ForLoop()
        {
            var l = 12022;
            for (var i = 1; i < l;)
            {
                var j = i;
                var max_j = i + 100;
                for (; j < max_j && j < l; j++)
                {
                    Console.Write(j + ",");
                }
                Console.WriteLine();
                i = j;
            }
        }

        #region 
        public static void E_000(int a)
        {

        }

        public static Action<int> func_000()
        {
            return E_000;
        }
        #endregion
    }

    public enum MemberCheckStatus
    {
        [Description("待审核")]
        WaitForAudit = 0,
        [Description("正常")]
        Normal = 1,
        [Description("未通过")]
        NotPass = 2,
        [Description("冻结")]
        Frozen = 3,
        [Description("会员临期")]
        TimeClosed = 4,
        [Description("未注册")]
        NotRegister = 5
    }
    

    public class xx
    {
        public string value { get; set; }
        public string label { get; set; }
        public List<xx> children { get; set; }
    }

    public class yy
    {
        public string label { get; set; }
        public DateTime dt { get; set; }


        //var json = "[{\"label\":\"a\",\"dt\":\"2020-03-30 11:47:00\"},{\"label\":\"b\",\"dt\":\"2020-04-01 11:47:00\"},{\"label\":\"c\",\"dt\":\"2020-03-31 11:47:00\"}]";
        //var xxList = j.ParseModel<List<yy>>(json);
    }
    public class Re_Statu
    {
        public bool isSucceeded { get; set; }
        public string message { get; set; }
    }

    public class IAS
    {
        public int partNumber { get; set; }
        public string etag { get; set; }
    }
}