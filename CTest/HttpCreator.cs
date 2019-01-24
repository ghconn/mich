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

            try
            {
                //获取服务端返回
                var response = (HttpWebResponse)request.GetResponse();

                try
                {
                    //获取服务端返回数据
                    StreamReader sr = new StreamReader(response.GetResponseStream(), encoding);
                    result = sr.ReadToEnd().Trim();
                    sr.Close();
                }
                catch
                {
                    result = "";
                }

                return response;
            }
            catch
            {

            }

            result = "";
            return null;
        }
    }
    
}
