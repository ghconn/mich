using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace scheduler
{
    /// <summary>
    /// 间隔时间单位
    /// </summary>
    public enum IntervalTimeUnit
    {
        [Description("秒")]
        秒 = 0,
        [Description("分")]
        分 = 1,
        [Description("时")]
        时 = 2,
        [Description("天")]
        天 = 3,
        [Description("星期")]
        星期 = 4,
        [Description("月")]
        月 = 5
    }
    /// <summary>
    /// 服务名称
    /// </summary>
    public enum ServeName
    {
        SetUnderway, SetOver
    }
    /// <summary>
    /// 运行状态
    /// </summary>
    public enum RunningStatus
    {
        [Description("运行")]
        运行 = 0,
        [Description("停止")]
        停止 = 1
    }

    /// <summary>
    /// 日志状态
    /// </summary>
    public enum LogLevel
    {
        [Description("已经停止执行")]
        已经停止执行 = -1,
        [Description("开始执行")]
        开始执行 = 0,
        [Description("执行成功")]
        执行成功 = 1,
        [Description("执行失败")]
        执行失败 = 2,
        [Description("停止执行")]
        停止执行 = 3
    }
}
