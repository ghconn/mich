using Quartz;
using Quartz.Impl.Triggers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scheduler.task
{
    public class demo : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var seheduler = await QuartzMethod.GetScheduler();
            var jobDetail = await seheduler.GetJobDetail(new Quartz.JobKey("test", "taskg1"));
            // var t = jobDetail.GetType(); // JobDetailImpl

            var trigger = await seheduler.GetTrigger(new Quartz.TriggerKey("class1", "group1"));
            //Console.WriteLine(trigger.GetNextFireTimeUtc()); // 获取下一次执行时间//可以比对cron表达式是否符合预期
            //var t = trigger.GetType();//Quartz.Impl.Triggers.CronTriggerImpl
            var triggerImpl = trigger as CronTriggerImpl;
            // triggerImpl.CronExpressionString // 获取任务的cron表达式

            await Task.Run(() => Console.WriteLine(DateTime.Now));
        }
    }
}
