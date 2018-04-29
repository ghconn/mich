using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wes.ashx
{
    /// <summary>
    /// pie 的摘要说明
    /// </summary>
    public class pie : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            var _pie = new mdl.chartpie<int>();
            var dataset = new mdl.dataset<int>();
            dataset.data = new int[] { 60, 50, 20, 32, 99, 12 };
            dataset.backgroundColor = new string[] { "#F7464A", "#46BFBD", "#FDB45C", "#949FB1", "#4D5360", "#02B300" };
            var dataset2 = new mdl.dataset<int>();
            dataset2.data = new int[] { 13, 16, 19, 22, 25, 28 };
            dataset2.backgroundColor = new string[] { "#F7464A", "#46BFBD", "#FDB45C", "#949FB1", "#4D5360", "#02B300" };
            _pie.datasets = new List<mdl.dataset<int>>();
            _pie.datasets.Add(dataset);
            _pie.datasets.Add(dataset2);
            _pie.labels = new string[] { "产品1", "产品2", "产品3", "产品4", "产品5", "产品6" };
            context.Response.Write(tpc.j.SerializeObject(_pie));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}