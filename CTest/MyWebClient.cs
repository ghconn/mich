using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CTest
{
    public class MyWebClient : WebClient
    {
        private long start { get; set; }
        private long end { get; set; }

        public MyWebClient(long start, long end) : base()
        {
            this.start = start;
            this.end = end;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            HttpWebRequest webRequest = WebRequest.CreateHttp(address);
            webRequest.AddRange(start, end);
            return webRequest;
        }
    }
}
