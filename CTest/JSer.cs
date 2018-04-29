using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTest
{
    public class JSer
    {
        static void _Main(string[] args)
        {
            mdl.ent<ClassA> ent = new mdl.ent<ClassA>();
            ent.t = new List<ClassA> { new ClassA() { C1 = new Class1[] { new Class1(), new Class1() } }, new ClassA() { C1 = new Class1[] { new Class1() } } };
            ent.dts = new List<DateTime>() { DateTime.Now, DateTime.Today };
            ent.Ls = new int[] { 1, 2, 3, 4, 5 };
            ent.dict = new Dictionary<string, ClassA>() { { "abc", new ClassA() { C1 = new Class1[] { new Class1(), new Class1() { ss = "KY" } } } }, { "zxc", new ClassA() { C1 = new Class1[] { new Class1(), new Class1() { ss = "SS" } } } } };
            ent.dict2 = new Dictionary<string, string>() { { "abc", "123" }, { "xxx", "456" } };

            Stopwatch sw = new Stopwatch();
            sw.Start();
            var val = JSer.ComplexObjToString(ent);
            Console.WriteLine(val);
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
        }

        public static string ObjToString(object obj, params string[] properties)
        {
            var re = "{";
            if (properties.Length != 0)
            {
                re += string.Join(",", properties.Select(p => $"\"{p}\":\"{obj.GetType().GetProperty(p).GetValue(obj)}\""));
            }
            else
            {
                re += string.Join(",", obj.GetType().GetProperties().Select(p => $"\"{p.Name}\":\"{p.GetValue(obj)}\""));
            }
            re += "}";
            return re;
        }

        public static string ComplexObjToString(object obj)
        {
            var re = new StringBuilder();
            if (obj.GetType().IsArray || (obj.GetType().IsGenericType && obj.GetType().GetInterface("IEnumerable") != null))
            {
                re.Append(ArrayAndGenericToString(obj));
            }
            else
            {
                re.Append(SimpleObjToString(obj));
            }
            return re.ToString();
        }

        static StringBuilder ArrayAndGenericToString(object obj)
        {
            var re = new StringBuilder("[");

            IEnumerable ie = (IEnumerable)obj;
            foreach (var element in ie)
            {
                var type = element?.GetType();
                if (element == null)
                {
                    re.Append("null,");
                }
                else if (type == typeof(int) || type == typeof(Single) || type == typeof(Int16) || type == typeof(long) || type == typeof(uint) || type == typeof(byte) || type == typeof(sbyte) || type == typeof(ulong) || type == typeof(double))
                {
                    re.Append($"{element},");
                }
                else if (type == typeof(string) || type == typeof(DateTime))
                {
                    re.Append($"\"{element}\",");
                }
                else if (type == typeof(Boolean))
                {
                    re.Append($"{element.ToString().ToLower()},");
                }
                else
                {
                    re.Append(ComplexObjToString(element)).Append(",");
                }
            }

            if (re[re.Length - 1] == ',')
                re.Remove(re.Length - 1, 1);
            re.Append("]");
            return re;
        }

        static StringBuilder SimpleObjToString(object obj)
        {
            var re = new StringBuilder("{");

            foreach (var p in obj.GetType().GetProperties())
            {
                var val = p.GetValue(obj);
                var type = val?.GetType();
                if (val == null)
                {
                    re.Append($"\"{p.Name}\":null,");
                }
                else if (type == typeof(int) || type == typeof(Single) || type == typeof(Int16) || type == typeof(long) || type == typeof(uint) || type == typeof(byte) || type == typeof(sbyte) || type == typeof(ulong) || type == typeof(double))
                {
                    re.Append($"\"{p.Name}\":{val},");
                }
                else if (type == typeof(string) || type == typeof(DateTime))
                {
                    re.Append($"\"{p.Name}\":\"{val}\",");
                }
                else if (type == typeof(Boolean))
                {
                    re.Append($"\"{p.Name}\":{val.ToString().ToLower()},");
                }
                else
                {
                    re.Append($"\"{p.Name}\":").Append(ComplexObjToString(val)).Append(',');
                }
            }

            if (re[re.Length - 1] == ',')
                re.Remove(re.Length - 1, 1);
            re.Append("}");
            return re;
        }
    }
}
