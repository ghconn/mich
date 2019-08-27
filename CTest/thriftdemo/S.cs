using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thrift.Server;
using Thrift.Transport;

namespace CTest.thriftdemo
{
    public class S
    {
        public void Start()
        {
            TServerSocket serverTransport = new TServerSocket(7911, 0, false);
            MyDemo.Processor processor = new MyDemo.Processor(new SImpl());
            TServer server = new TSimpleServer(processor, serverTransport);
            Console.WriteLine("Starting server on port 7911 ...");
            server.Serve();
        }
    }

    public class SImpl : MyDemo.ISync
    {
        public int testM1(int num1, int num2)
        {
            return 100;
        }

        public List<string> testM2(string s1)
        {
            return new List<string>() { "abc","123"};
        }

        public void testM3(Dictionary<string, string> dict1)
        {
            throw new NotImplementedException();
        }

        public void testM4(List<Kevp> kvp)
        {
            throw new NotImplementedException();
        }
    }
}
