using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTest.D
{
    public class MealInfo
    {
        public long mealItemID { get; set; }
        public string eventNo { get; set; }
        public string division { get; set; }
        public string requesterCwid { get; set; }
        public string requesterName { get; set; }
        public string requesterPhone { get; set; }
        public string city { get; set; }
        public string restaurantName { get; set; }
        public string restaurantLocation { get; set; }
        public string orderStatus { get; set; }

        public string diningTime { get; set; }
        public string lastModifyTime { get; set; }
    }

    public class A : iC
    {
        public string X { get; set; }
    }
    public class B : iC
    {
        public string X { get; set; }
        public string Y { get; set; }
    }

    public interface iC
    {

    }

    public static class ModelObjectEx
    {
        public static void MapTo(this iC ic, iC ic2)
        {
            var type = ic2.GetType();
            var type_ic = ic.GetType();

            var ps = type.GetProperties();
            foreach (var p in ps)
            {
                var p_ic = type_ic.GetProperty(p.Name);
                if (p_ic != null)
                {
                    p.SetValue(ic2, p_ic.GetValue(ic));
                }
            }
        }
    }

    public class Program
    {
        public void _main()
        {
            var x = new A() { X = "uuu" };
            var y = new B();
            x.MapTo(y);
            Console.WriteLine("y.X=" + y.X);
        }
    }
}
