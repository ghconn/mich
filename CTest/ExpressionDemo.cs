using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CTest
{
    public class ExpressionDemo
    {
        public static List<T> ExpressionTree<T, V>(List<T> list, string propertyName, V propertyValue)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "x");
            ParameterExpression value = Expression.Parameter(typeof(V), "propertyValue");
            MethodInfo setter = typeof(T).GetMethod("set_" + propertyName);
            MethodCallExpression call = Expression.Call(parameter, setter, value);
            //var lambda = Expression.Lambda(call, parameter, value);//这种方式极慢
            var lambda = Expression.Lambda<Action<T, V>>(call, parameter, value);//(x, propertyValue) => x.set_xxx(propertyValue)
            var exp = lambda.Compile();
            for (var i = 0; i < list.Count; i++)
            {
                //exp.DynamicInvoke(list[i], propertyValue);//这种方式极慢
                exp(list[i], propertyValue);
            }

            return list;
        }

        public static List<T> SetAllProperty<T>(List<T> list, string propertyName, object propertyValue)
        {
            if (null == list || null == propertyName || null == propertyValue)
            {
                return null;
            }
            foreach(var l in list)
            {
                var properties = l.GetType().GetProperties();
                foreach (var p in properties)
                {
                    if (!p.CanRead || !p.CanWrite)
                        continue;
                    if (p.Name == propertyName)
                        p.SetValue(l, propertyValue);
                }
            }
            return list;
        }

        public static void _main()
        {
            var l = Enumerable.Range(0, 1000000).Select((i) => new Class1()).ToList();
            Stopwatch sw = new Stopwatch();

            sw.Restart();
            l = ExpressionDemo.SetAllProperty(l, "ss", "0");
            sw.Stop();

            Console.WriteLine("ms:" + sw.ElapsedMilliseconds);//1300ms//453ms

            sw.Restart();
            l = ExpressionDemo.ExpressionTree(l, "ss", "1");
            sw.Stop();

            Console.WriteLine("ms:" + sw.ElapsedMilliseconds);//89ms//25ms
        }
    }
}
