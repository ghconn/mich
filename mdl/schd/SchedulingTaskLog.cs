using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace mdl.schd
{
    public class SchedulingTaskLog
    {
        ///<summary>
        ///主键
        ///</summary>
        [Description("主键")]
        public Int64 Id { get; set; }
        ///<summary>
        ///任务名称
        ///</summary>
        [Description("任务名称")]
        public String TaskName { get; set; }
        ///<summary>
        ///执行时间
        ///</summary>
        [Description("执行时间")]
        public DateTime ExecuteTime { get; set; }
        ///<summary>
        ///执行结果
        ///</summary>
        [Description("执行结果")]
        public String ExecuteResult { get; set; }
        ///<summary>
        ///异常消息
        ///</summary>
        [Description("异常消息")]
        public String ExceptionMsg { get; set; }
    }
}
