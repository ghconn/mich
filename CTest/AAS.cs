using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace CTest
{
    class AAS
    {
        static int i;//volatile

        #region way1
        public static void way1()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var t1 = new Thread(a);
            t1.Start();
            var t2 = new Thread(b);
            t2.Start();

            //in fact,just need to use one worker thread

            t1.Join();
            t2.Join();

            sw.Stop();

            Interlocked.Add(ref i, 5);
            Console.WriteLine("merge" + i);
            Console.WriteLine("ts:" + sw.Elapsed.TotalSeconds);
            Console.ReadKey();
        } 

        static void a()
        {
            Interlocked.Increment(ref i);
            var sec = new Random(Guid.NewGuid().GetHashCode()).Next(1, 5);
            Console.WriteLine("a" + sec);
            Thread.Sleep(sec * 1000);
        }
        static void b()
        {
            Interlocked.Add(ref i, 3);
            var sec = new Random(Guid.NewGuid().GetHashCode()).Next(1, 5);
            Console.WriteLine("b" + sec);
            Thread.Sleep(sec * 1000);
        }
        #endregion

        #region way2
        static readonly object _locker = new object();
        public static void way2()//public static async void way2()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var t1= Task.Run(() => a2());
            var t2 = Task.Run(() => b2());

            //in fact,just need to use one worker thread

            t1.Wait();//await t1;
            t2.Wait();//await t2;
            Interlocked.Add(ref i, 5);
            Console.WriteLine("merge" + i);
            Console.WriteLine("ts:" + sw.Elapsed.TotalSeconds);
            Console.ReadKey();
        }

        static void a2()
        {
            Interlocked.Increment(ref i);
            var sec = new Random(Guid.NewGuid().GetHashCode()).Next(1, 5);
            Console.WriteLine("a" + sec);
            Thread.Sleep(sec * 1000);
        }
        static void b2()
        {
            Interlocked.Add(ref i, 3);
            var sec = new Random(Guid.NewGuid().GetHashCode()).Next(1, 5);
            Console.WriteLine("b" + sec);
            Thread.Sleep(sec * 1000);
        }
        #endregion
    }
}
