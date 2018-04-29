using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mdl
{
    public class ent<T>
    {
        public List<T> t { get; set; }

        public List<DateTime> dts { get; set; }

        public int I { get; set; }

        public int[] Ls { get; set; }

        public IDictionary<string, T> dict { get; set; }

        public IDictionary<string, string> dict2 { get; set; }
    }
}
