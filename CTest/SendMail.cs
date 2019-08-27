using mdl.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tpc;

namespace CTest
{
    public class SendMail
    {
        public static void _main()
        {
            var h2 = new ConfigHost()
            {
                Server = "smtp.qq.com",
                Port = 25,
                Username = "boring01@foxmail.com",
                Password = "skazklxkuethbcde",
                EnableSsl = false
            };
            var eventNo = "xxx";
            var sDate = "";
            var sTime = "";
            var sStatu = "";
            var checkType = "";
            var oiRestaurantName = "";
            var oiLocation = "";
            var province = "";
            var city = "";
            var oiConName = "";
            var oiConPhone = "";
            var oiExecuteName = "";
            var body = $"<p>您好！</p><p>以下是Bayer Opera编号<span style='background:#FFFF00'>{eventNo}</span>的活动最新信息，";
            body += "请在执行审核前确认以下<span style='text-decoration:underline'>红色变更信息</span>，如无法执行，请即刻反馈项目负责人，谢谢。</p>";
            body += "<div>活动基本信息：</div>";
            body += $"<div>Opera编号：{eventNo}</div>";
            body += $"<div>活动日期：{sDate}</div>";
            body += $"<div>活动时间：{sTime}</div>";
            body += $"<div>活动状态：{sStatu}</div>";
            body += $"<div>审核形式：{checkType}</div>";
            body += $"<div>餐厅名：{oiRestaurantName}</div>";
            body += $"<div>餐厅地址：{province} / {city} / {oiLocation}</div>";
            body += $"<div>活动申请人：{oiConName} / {oiConPhone}</div>";
            body += $"<div>HDBC执行人员：{oiExecuteName}</div>";
            body += "<p></p>";
            body += "<p></p>";
            body += "<p><span style='font-weight:600;font-size:13px'>Classification: </span><span style='font-weight:600;color:#004F71;font-size:13px'>Internal Use Only</span></p>";
            body += "<img src='cid:" + Convert.ToBase64String(Encoding.Default.GetBytes("2.jpg")) + "' alt=''/>";
            var m2 = new ConfigMail()
            {
                Body = body,
                From = "boring01@foxmail.com",
                To = new string[] { "kan_yon@foxmail.com" },
                Attachments = new string[] { },
                Resources = new string[] { "C:\\Users\\cspactera\\Pictures\\2.jpg" }
            };

            var agent = new NetMail();
            Console.WriteLine("start");
            try
            {
                agent.CreateHost(h2);
                m2.Subject = $"<活动变更提醒> Bayer Opera编号{eventNo}， " + DateTime.Now.ToString("MM/dd HH:mm");
                agent.CreateMultiMail(m2);
                agent.SendMail();

                Console.WriteLine("success");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.InnerException?.Message);
            }

            Console.WriteLine("end");
        }
    }
}
