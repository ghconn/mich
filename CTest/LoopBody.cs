using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTest
{
    public class LoopBody
    {
         static List<int> arr = new List<int>();
        public static void _Main()
        {
            while (true)
            {
                Console.WriteLine("被除数");
                var m = int.Parse(Console.ReadLine());
                Console.WriteLine("除数");
                var n = int.Parse(Console.ReadLine());
                Console.WriteLine("计算小数点后面第几位");
                var nth = int.Parse(Console.ReadLine());
                Console.WriteLine($"小数点后面第{nth}位是:" + modnth(m, n, nth));
                Console.WriteLine("循环体:" + string.Join(",", arr));
                Console.WriteLine("循环体长度:" + arr.Count);
                Console.WriteLine();
                Console.WriteLine();
            }
        }

        #region nth arr
        static int modnth(int m, int n, int nth)
        {
            //index:从第几位开始出现循环体(从0开始)
            var index = loopbodyofrationalnumber(m, n);
            //如果不是从第一位开始出现循环体
            if (index > 0)
            {
                //循环体前面部分的小数
                Console.WriteLine("mixture:" + string.Join(",", arr.GetRange(0, index)));
                Console.WriteLine("从第" + (index + 1) + "位出现循环体");
            }
            else
            {
                Console.WriteLine("从第1位出现循环体");
            }
            //nth:要传入的参数,小数部分后面第几位(传递参数从1开始)
            nth = nth - 1;
            //re:要计算的结果1,小数体后面第nth位的值
            int re = 0;
            //如果nth在循环体开始之前
            if (nth < index)
            {
                re = arr[nth];
                //如果被除尽，定义循环体为0
                if (index == arr.Count)
                {
                    arr.Clear();
                    arr.Add(0);
                }
                else
                {
                    arr = arr.GetRange(index, arr.Count - index);//去掉前面部分，只保留循环体
                }
                return re;
            }
            else
            {
                //如果被除尽，定义循环体为0
                if (index == arr.Count)
                {
                    arr.Clear();
                    arr.Add(0);
                }
                else
                {
                    arr = arr.GetRange(index, arr.Count - index);//去掉前面部分，只保留循环体
                    var temp = nth - index;
                    re = arr[temp % arr.Count];//找到第nth位的值
                }
            }
            return re;
        }

        static int loopbodyofrationalnumber(int m, int n)
        {
            //如果被除尽
            if (m == 0 || m == n)
            {
                arr = arr.Select(i => i / n).ToList();
                return arr.Count;
            }
            if (m > n)
            {
                return loopbodyofrationalnumber(m % n, n);
            }
            else
            {
                //当除数乘以10首次重复时，即得到循环体
                var curr = m * 10 / n;
                if (!arr.Contains(m * 10))
                {
                    arr.Add(m * 10);
                    return loopbodyofrationalnumber(m * 10 % n, n);
                }
                else
                {
                    var index = arr.IndexOf(m * 10);

                    arr = arr.Select(i => i / n).ToList();
                    return index;
                }
            }
        }
        #endregion
    }
}
