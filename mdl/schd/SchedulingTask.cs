using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace mdl.schd
{
    /// <summary>
    /// 调度任务表
    /// </summary> 
    public class SchedulingTask
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
        ///任务组名
        ///</summary>
        [Description("任务组名")]
        public String TaskGroupName { get; set; }
        ///<summary>
        ///任务类全名称
        ///</summary>
        [Description("任务类全名称")]
        public String TaskClassFullName { get; set; }
        ///<summary>
        ///执行间隔时间
        ///</summary>
        [Description("执行间隔时间")]
        public String IntervalTime { get; set; }
        ///<summary>
        ///运行状态(运行、停止)
        ///</summary>
        [Description("运行状态(运行、停止)")]
        public Int16 RunningStatus { get; set; }
        ///<summary>
        ///任务描述
        ///</summary>
        [Description("任务描述")]
        public String TaskDescription { get; set; }
        ///<summary>
        ///触发器名称
        ///</summary>
        [Description("触发器名称")]
        public String TriggerName { get; set; }
        ///<summary>
        ///触发器组名
        ///</summary>
        [Description("触发器组名")]
        public String TriggerGroupName { get; set; }
        ///<summary>
        ///任务开始时间
        ///</summary>
        [Description("任务开始时间")]
        public DateTime? StartTime { get; set; }
        ///<summary>
        ///任务结束时间
        ///</summary>
        [Description("任务结束时间")]
        public DateTime? EndTime { get; set; }

    }
}
