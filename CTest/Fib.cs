using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTest
{
    public class Fib
    {
        public static ulong _main(int n) // n最大为93，否则结果超出ulong.MaxValue
        {
            ulong[] item = { 1ul, 1ul };
            Console.WriteLine(1 + "\t" + 1);
            Console.WriteLine(1 + "\t" + 2);
            for (var i = 3; i <= n; i++)
            {
                var tmp = item[0];
                item[0] = item[1];
                item[1] = tmp + item[1];
                Console.WriteLine(ulong.MaxValue);
                Console.WriteLine(item[1] + "\t" + i);
                Console.WriteLine();
            }
            return item[1];
        }

        public static double dbmain(int n) // n最大为1476，否则结果超出double.MaxValue
        {
            double[] item = { 1d, 1d };
            Console.WriteLine(1 + "\t" + 1);
            Console.WriteLine(1 + "\t" + 2);
            for (var i = 3; i <= n; i++)
            {
                var tmp = item[0];
                item[0] = item[1];
                item[1] = tmp + item[1];
                Console.WriteLine(double.MaxValue);
                Console.WriteLine(item[1] + "\t" + i);
                Console.WriteLine();
            }
            return item[1];
        }
    }
}
