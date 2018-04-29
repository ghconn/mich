using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wes.hl
{
    public class @in:IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Write("123");
            //context.Response.Write(context.Request.QueryString["v"]);
        }
        public bool IsReusable
        {
            get { return false; }
        }

    }
}