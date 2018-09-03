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

namespace CTest
{

    class Program
    {
        static readonly object _locker = new object();
        static void Main(string[] args)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            var timestamp1 = (int)(DateTime.Now - startTime).TotalSeconds;
            string t = "sl" + timestamp1;
            Console.WriteLine(t);

            string s = "abc";
            var intptr = Marshal.StringToCoTaskMemAuto(s);
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
                yield return new mdl.point((Single)c, (Single)c);
                Console.WriteLine(1);
            }
            Console.WriteLine(0);
            yield break;
        }

        static async void d()
        {
            var i = await Task.Run(() => { Thread.Sleep(5000); return 1; });
            Console.WriteLine(i.ToString());
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

        static void storeagentthriftclienttest()
        {
            Process p = new Process();
            p.StartInfo = new ProcessStartInfo(@"E:\0Source\SFWeb\trunk\SFForm\StoreAgentTest\bin\Debug\StoreAgentTest.exe");
            var i = 0;
            while (i++ < 10)
            {
                p.Start();
            }

            Console.ReadKey();

            var ps = Process.GetProcessesByName("StoreAgentTest");
            foreach (var sa in ps)
            {
                sa.Kill();
                sa.WaitForExit(1000);
            }
        }
    }
}