using mdl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wes.aspx
{
    public partial class bl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string[] s = { "abc", "abc" };
            //Response.Write(NotHasRpt(s));
            //Response.Write(System.IO.Path.GetExtension(@"d:/1.txt"));
            //var s = tpc.HttpService.Get("http://www.baidu.com");
            //Response.Write(s);
            //point[] pts = new point[] { new point() { x = 2, y = 3 }, new point() { x = 2, y = 3 }, new point() { x = 3, y = 2 } };
            //Response.Write(NotHasRpt(pts, new MyCompareer<point>()));

            //Response.Write(Regex.Split("ssss(23)", "\\(\\d+\\)")[0]);
            //Response.Write(Regex.Match("td:nth-child(23)", "\\d+").Value);

            //var s = System.Reflection.Assembly.Load("bp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
            
        }

        public bool NotHasRpt<T>(IEnumerable<T> enumrable, IEqualityComparer<T> equality = null)
        {
            return enumrable.Distinct(equality).Count() == enumrable.Count();
        }
    }
}