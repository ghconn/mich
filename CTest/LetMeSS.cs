using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTest
{
    public class LetMeSS
    {
        public static void KK(string s)
        {
            var encodings = new Encoding[] { Encoding.ASCII, Encoding.Default/*等于gb2312*/, Encoding.Unicode, Encoding.UTF32, Encoding.UTF7, Encoding.UTF8, Encoding.GetEncoding("gbk")/*等于gb2312*/, Encoding.GetEncoding("gb2312") };

            foreach(var encoding in encodings)
            {
                foreach(var encoding2 in encodings)
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.Write(encoding.EncodingName);
                    Console.ResetColor();
                    Console.Write(" get bytes, ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(encoding2.EncodingName);
                    Console.ResetColor();
                    Console.Write(" GetString\n");
                    Console.WriteLine(encoding2.GetString(encoding.GetBytes(s)));
                    //Console.WriteLine(encoding.EncodingName + " get bytes, " + encoding2.EncodingName + " GetString\n" + encoding2.GetString(encoding.GetBytes(s)));
                }
                Console.WriteLine();
            }
        }

        public static void _main()
        {
            LetMeSS.KK("鍘熷瀷/涓撳淇℃伅绠＄悊-鍚堜綔鍗忚涔?html");
        }
    }
}
