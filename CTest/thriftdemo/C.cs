using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thrift.Protocol;
using Thrift.Transport;

namespace CTest.thriftdemo
{
    public class C
    {
        public static void _main()
        {
            TTransport transport = new TSocket("localhost", 7911);
            TProtocol protocol = new TBinaryProtocol(transport);
            MyDemo.Client client = new MyDemo.Client(protocol);
            transport.Open();
            Console.WriteLine("Client calls .....");

            var i1 = client.testM1(1, 2);
            Console.WriteLine(i1);
            var s2 = client.testM2(null);
            Console.WriteLine(string.Join(",", s2));
        }
    }
}
