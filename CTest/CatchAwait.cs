using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTest
{
    public class CatchAwait
    {
        public static void _Main1()
        {
            try
            {
                _ = HasExceptionAsyncFunc();
            }
            catch
            {
                Console.WriteLine("_Main1 Catched Exception"); // 无法Catch
            }
        }

        public static void _Main2()
        {
            _ = X();
        }

        static async Task X()
        {
            try
            {
                var n = await HasExceptionAsyncFunc();
            }
            catch
            {
                Console.WriteLine("_Main2 Catched Exception"); // 正常Catch
            }
        }

        static async Task<int> HasExceptionAsyncFunc()
        {
            await Task.Delay(10);
            return new int[] { 0 }[2];
        }
    }
}
