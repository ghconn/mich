using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace scheduler
{
    public class CronCommon
    {

        /// <summary>
        /// 使用Cron表达式设定时间
        /// </summary>
        /// <param name="time">间隔时间，XX后面的单位是没有存的</param>
        /// <returns></returns>
        public static string CronSchedule(string time)
        {
            //默认间隔5分钟
            string cronStr = "0 */5 * * * ?";
            string[] strTimes = time.Split('|');
            string[] specificTimes = strTimes[1].Split('-');
            switch (int.Parse(strTimes[0]))
            {
                case (int)IntervalTimeUnit.秒://(秒|XX秒) 例：0|5  间隔五秒
                    cronStr = string.Format("*/{0} * * * * ?", specificTimes[0]); break;
                case (int)IntervalTimeUnit.分://(分|XX分) 例：1|5  间隔五分
                    cronStr = string.Format("0 */{0} * * * ?", specificTimes[0]); break;
                case (int)IntervalTimeUnit.时://(时|XX时) 例：2|5  间隔五小时
                    cronStr = string.Format("0 0 */{0} * * ?", specificTimes[0]); break;
                case (int)IntervalTimeUnit.天://天|XX时-XX分-XX秒 例：3|12-10-0 每天的12点10分执行
                    cronStr = string.Format("{2} {1} {0} * * ?", specificTimes[0], specificTimes[1], specificTimes[2]); break;
                case (int)IntervalTimeUnit.星期://星期|星期XX-XX时-XX分-XX秒 例：4|1-12-10-0 每周星期一的12点10分执行
                    string weekStr = "";
                    switch (specificTimes[0])
                    {
                        case "1":
                            weekStr = "MON"; break;
                        case "2":
                            weekStr = "Tue"; break;
                        case "3":
                            weekStr = "Wed"; break;
                        case "4":
                            weekStr = "Thu"; break;
                        case "5":
                            weekStr = "Fri"; break;
                        case "6":
                            weekStr = "Sat"; break;
                        case "7":
                            weekStr = "Sun"; break;
                    }
                    cronStr = string.Format("{3} {2} {1} ? * {0}", weekStr, specificTimes[1], specificTimes[2], specificTimes[3]);
                    break;
                case (int)IntervalTimeUnit.月://月|XX号-XX时-XX分-XX秒 例：4|1-12-10-0 每月1号的12点10分执行
                    cronStr = string.Format("{3} {2} {1} {0} * ?", specificTimes[0], specificTimes[1], specificTimes[2], specificTimes[3]);
                    break;
            }
            return cronStr;
        }

    }
}
