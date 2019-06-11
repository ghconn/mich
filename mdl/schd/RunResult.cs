using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mdl.schd
{
    /// <summary>
    /// 任务运行结果
    /// </summary>
    public class RunResult
    {
        /// <summary>
        /// 状态
        /// </summary>
        public Int16 Status { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string Msg { get; set; }
    }
}
