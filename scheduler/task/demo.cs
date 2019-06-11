using Quartz;
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
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Run(() => Console.WriteLine(DateTime.Now));
        }
    }
}
