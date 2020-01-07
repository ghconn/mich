using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mdl.Attributes
{
    public class TitleAttribute : Attribute
    {
        public string Title;
        public TitleAttribute(string name)
        {
            Title = name;
        }
    }
}
