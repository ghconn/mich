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

            var f = 220538.19f;
            Console.WriteLine(f);
            var s = $@"123,
                                {f},{1}";
            Console.WriteLine(s);

            //

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
    }
}