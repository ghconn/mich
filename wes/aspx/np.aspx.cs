using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceProcess;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wes.aspx
{
    public partial class np : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Write(this.f());
            //Response.Write(new SqlDataSource().f());
            //ServiceController service = new ServiceController("测试服务");

            //try
            //{
            //    if (service.Status == ServiceControllerStatus.Running)
            //    {
            //        service.Stop();
            //        service.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(30));
            //    }
            //    service.Start();
            //    service.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(30));
            //}
            //catch (Exception)
            //{

            //}
            
            //List<string> ls = new List<string>();
            //var s = "123";
            //Response.Write(ls.IndexOf(s));
            
            //string amount = "000";
            //if (amount.EndsWith("00"))
            //    amount = amount.Substring(0, amount.Length - 2);
            //Response.Write(amount);
            //Response.Write(string.Join("<br />", tpc.ftphelper.GetFileList(new Uri("ftp://192.168.2.206/2"), "yfticbc", "yfticbc", System.Text.Encoding.GetEncoding("gb2312"))));
            //Response.Write(tpc.ftphelper.IsExistF(new Uri("ftp://192.168.2.206/DLYFT20160126.txt"), "yfticbc", "yfticbc"));
            //tpc.ftphelper.UploadFtp("F:\\yu", "123.txt", "192.168.2.206", "666", "yfticbc", "yfticbc");
        }
    }
    class b { }
    static class A
    {
        public static int f(this object o)
        {
            return o.ToString().Sum(c => c);
        }
    }
}