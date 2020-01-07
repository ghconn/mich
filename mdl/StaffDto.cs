using mdl.Attributes;
using mdl.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mdl
{
    public class StaffDto : ExportTemplate
    {
        [Title("xxx")]
        public string Name { get; set; }
        public string Name2 { get; set; }
    }
}
