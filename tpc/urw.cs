using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace tpc
{
    public class urw : IHttpModule
    {

        public void Dispose()
        {
            //
        }

        public void Init(HttpApplication httpapp)
        {
            httpapp.BeginRequest += context_BeginRequest;
            httpapp.Error += context_Error;
        }

        private void context_Error(object sender, EventArgs e)
        {
            HttpApplication httpapp = (HttpApplication)sender;
            var response = httpapp.Response;
            response.Write("<html>");
            response.Write("<head><title>出错了！</title></head>");
            response.Write("<body style=\"font-size:14px;\">");
            response.Write("出错了:<br />");
            response.Write("<p>");
            response.Write(httpapp.Server.GetLastError().ToString().Replace("\r\n", "<br/>"));
            response.Write("</p>");
            response.Write("</body></html>");
            response.End();
        }

        private void context_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication httpapp = (HttpApplication)sender;
            //var r1 = httpapp.Response;
            //var r2 = httpapp.Context.Response;
            //r1==r2 true
            string path = httpapp.Request.Path;
            //var regex = new Regex("/api100/", RegexOptions.Compiled);
            //var match = regex.Match(path);
            //if (match.Success)
            //{

            //}
            if (path.StartsWith("/api100/"))
            {
                var urlkeyvalues = path.Replace("/api100/", "").Split('/');
                Dictionary<string, string> dict = new Dictionary<string, string>();
                for (var i = 0; i < urlkeyvalues.Length; i += 2)
                {
                    dict.Add(urlkeyvalues[i], urlkeyvalues[i + 1]);
                }
                string qs = "?";
                qs += string.Join("&", dict.Select(kvp => kvp.Key + "=" + kvp.Value));
                httpapp.Context.RewritePath("/ashx/api.ashx" + qs);
            }
        }

    }
}
