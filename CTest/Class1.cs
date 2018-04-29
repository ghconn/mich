using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTest
{
    public class Class1
    {
        //当对Class1有任何调用时，以下代码执行顺序为
        public int i = 1;//4(如果是new一个Class1的实例，否则这句没有执行)
        static int c = 5;//1
        public const string s = "abc";//0
        static Class1()
        {
            Console.WriteLine("static");//3
        }

        public Class1()
        {
            //Console.WriteLine(1);//6(如果是new一个Class1的实例，否则这句没有执行)
        }

        int a = 2;//5(如果是new一个Class1的实例，否则这句没有执行)
        public static int b = 3;//2

        public void f()
        {
            b++;
        }

        public string ss { get; set; }
    }

    public class ClassA : cb
    {
        public int AI()
        {
            return base.I;
        }

        public Class1[] C1 { get; set; }
    }
    public class ClassB : cb
    {
        public int BI()
        {
            base.I = 12;
            return base.I;
        }
    }

    public class cb
    {
        public int I
        {
            get; set;
        }
    }

    public class D<T> : IEnumerable, IEnumerable<T>
    {
        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

}
