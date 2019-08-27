using CTest.D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTest
{
    /// <summary>
    /// 方法的引用类型参数，加不加ref关键都是传递引用给形参，此处演示加不加ref有什么区别
    /// </summary>
    public class RefKey
    {
        public static void _main()
        {
            A a = new A();
            a.X = "a";
            Console.WriteLine("开始：" + a.X);
            ReferenceTest(a);
            Console.WriteLine("传递引用，在方法内被修改：" + a.X);
            ReferenceRePointTest(a);
            Console.WriteLine("传递引用，方法内重新指向，并修改：" + a.X);
            RefKeyTest(ref a);
            Console.WriteLine("带ref关键字传递引用，在方法内被修改：" + a.X);
            RefKeyRePointTest(ref a);
            Console.WriteLine("带ref关键字传递引用，方法内重新指向，并修改：" + a.X);
        }

        static void ReferenceTest(A a)
        {
            a.X = "b";
        }

        static void ReferenceRePointTest(A a)
        {
            a = new A();
            a.X = "c";
        }

        static void RefKeyTest(ref A a)
        {
            a.X = "d";
        }

        static void RefKeyRePointTest(ref A a)
        {
            a = new A();
            a.X = "e";
        }
    }
}
