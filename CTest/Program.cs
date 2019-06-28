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
using Fiddler;
using System.DirectoryServices;
using System.Data;
using System.ComponentModel;
using CTest.D;
using scheduler;
using System.Runtime.Serialization.Json;
using mdl.Mail;

namespace CTest
{

    class Program
    {
        static readonly object _locker = new object();
        static void Main(string[] args)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            var timestamp1 = (int)(DateTime.Now - startTime).TotalSeconds;
            Console.WriteLine("sl{0}", timestamp1);

            string sTestMarshalIntPtr = "abc";
            var intptr = Marshal.StringToCoTaskMemAuto(sTestMarshalIntPtr);
            Console.WriteLine(intptr);

            //var s = "v 1.　　3";
            //var s2 = Regex.Replace(s, @"\s", "");

            //DateTime.Parse("Sat, 30-Nov-2019 01:35:58 GMT");

            //var result = "";
            //StoreTest.Create("http://localhost:12439/ashx/api.ashx?action=xml", "post", "", "<abc>123</abc>", "text/xml", null, null, null, out result);
            //Console.WriteLine(result);

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

            //调度demo
            //schddemo();

            var f = 220538.19f;
            Console.WriteLine(f);
            var s = $@"123,
                                {f},{1}";
            Console.WriteLine(s);

            var now = DateTime.Now.ToString("yyyy-MM-dd");
            Console.WriteLine(now.Replace("-", "").Substring(4, 4));

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

            #region pause
            Console.ReadKey();
            #endregion

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
        
        static async Task<int> d()
        {
            await Task.Delay(5 * 1000);
            Console.WriteLine("d in ok");
            return 1;
            
            //var y = d();
            //Console.WriteLine("d go on.");
            //Console.WriteLine(y.Result);
        }

        /// <summary>
        /// 反序列化集合对象
        /// </summary>
        public static T[] JsonDeserializeToArrayData<T>(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T[]));
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

        static async void schddemo()
        {
            await QuartzMethod.AddScheduleJob(new mdl.schd.SchedulingTask()
            {
                EndTime = DateTime.Now.AddSeconds(30),
                Id = 1,
                IntervalTime = "0|2",
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

        static void sm()
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
            var body = $"<p>您好！</p><p>以下是Bayer Opera编号<span style='background:#FFFF00'>{eventNo}<span>的活动最新信息，";
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
            body += "<img src='cid:" + Convert.ToBase64String(Encoding.Default.GetBytes("hxdbs.jpg")) + "' alt=''/>";
            var m2 = new ConfigMail()
            {
                Body = body,
                From = "boring01@foxmail.com",
                To = new string[] { "kan_yon@foxmail.com" },
                Attachments = new string[] { },
                Resources = new string[] { "C:\\Users\\cspactera\\source\\repos\\MICH\\bp\\pic\\conn.jpg" }
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
}


namespace CTest.D
{

    public class MealInfo
    {
        public long mealItemID { get; set; }
        public string eventNo { get; set; }
        public string division { get; set; }
        public string requesterCwid { get; set; }
        public string requesterName { get; set; }
        public string requesterPhone { get; set; }
        public string city { get; set; }
        public string restaurantName { get; set; }
        public string restaurantLocation { get; set; }
        public string orderStatus { get; set; }

        public string diningTime { get; set; }
        public string lastModifyTime { get; set; }
    }

    public class A : iC
    {
        public string X { get; set; }
    }
    public class B : iC
    {
        public string X { get; set; }
        public string Y { get; set; }
    }

    public interface iC
    {

    }

    public static class ModelObjectEx
    {
        public static void MapTo(this iC ic, iC ic2)
        {
            var type = ic2.GetType();
            var type_ic = ic.GetType();

            var ps = type.GetProperties();
            foreach(var p in ps)
            {
                var p_ic = type_ic.GetProperty(p.Name);
                if (p_ic != null)
                {
                    p.SetValue(ic2, p_ic.GetValue(ic));
                }
            }
        }
    }

    public class Program
    {
        public void _main()
        {
            var x = new A() { X = "uuu" };
            var y = new B();
            x.MapTo(y);
            Console.WriteLine("y.X=" + y.X);
        }
    }
}