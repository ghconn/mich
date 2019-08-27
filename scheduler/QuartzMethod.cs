﻿using mdl.schd;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace scheduler
{
    public class QuartzMethod
    {
        /// <summary>
        /// 任务计划
        /// </summary>
        public static IScheduler scheduler = null;
        public static async Task<IScheduler> GetScheduler()
        {

            if (scheduler != null)
            {
                return scheduler;
            }
            else
            {
                NameValueCollection props = new NameValueCollection
                {
                    { "quartz.serializer.type", "binary" }
                };
                ISchedulerFactory schedf = new StdSchedulerFactory(props);
                IScheduler sched = await schedf.GetScheduler();
                return sched;
            }
        }
        /// <summary>
        /// 添加任务计划
        /// </summary>
        /// <returns></returns>
        public static async Task<RunResult> AddScheduleJob2(SchedulingTask taskModel)
        {
            var status = new RunResult();
            try
            {
                scheduler = await GetScheduler();
                //检查任务是否已存在
                var jk = new JobKey(taskModel.TaskName, taskModel.TaskGroupName);
                //如果不存在
                if (!await scheduler.CheckExists(jk))
                {
                    //使用Cron表达式计算触发间隔时间
                    var cronSchedule = taskModel.IntervalTime;

                    Type jobType = Type.GetType(taskModel.TaskClassFullName);
                    // 定义这个工作，并将其绑定到我们的IJob实现类
                    IJobDetail job = new JobDetailImpl(taskModel.TaskName, taskModel.TaskGroupName, jobType) { Description = taskModel.TaskDescription };
                    ITrigger trigger = TriggerBuilder.Create()
                        .WithIdentity(taskModel.TriggerName, taskModel.TriggerGroupName)
                        .StartAt((DateTimeOffset)taskModel.StartTime) //指定开始时间
                        .EndAt((DateTimeOffset)taskModel.EndTime)//指定结束时间
                        .WithCronSchedule(cronSchedule)//使用Cron表达式
                        .ForJob(taskModel.TaskName, taskModel.TaskGroupName) //通过JobKey识别作业
                        .Build();                // 告诉Quartz使用我们的触发器来安排作业

                    await scheduler.ScheduleJob(job, trigger);
                    await scheduler.Start();
                    // await Task.Delay(TimeSpan.FromSeconds(5));
                    status.Status = 1;
                    status.Msg = "任务计划运行成功";
                }
                else
                {
                    status.Status = 1;
                    status.Msg = "任务计划运行正常";
                }
            }
            catch (Exception ex)
            {
                status.Status = 2;
                status.Msg = "任务计划运行异常，异常信息：" + ex.Message;
            }
            return status;

        }
        /// <summary>
        /// 添加任务计划
        /// </summary>
        /// <returns></returns>
        public static async Task<RunResult> AddScheduleJob(SchedulingTask taskModel)
        {
            var status = new RunResult();
            try
            {
                scheduler = await GetScheduler();
                //检查任务是否已存在
                var jk = new JobKey(taskModel.TaskName, taskModel.TaskGroupName);
                //如果不存在
                if (!await scheduler.CheckExists(jk))
                {
                    //使用Cron表达式计算触发间隔时间
                    var cronSchedule = CronCommon.CronSchedule(taskModel.IntervalTime);

                    Type jobType = Type.GetType(taskModel.TaskClassFullName);
                    // 定义这个工作，并将其绑定到我们的IJob实现类
                    IJobDetail job = new JobDetailImpl(taskModel.TaskName, taskModel.TaskGroupName, jobType) { Description = taskModel.TaskDescription };
                    ITrigger trigger = TriggerBuilder.Create()
                        .WithIdentity(taskModel.TriggerName, taskModel.TriggerGroupName)
                        .StartAt((DateTimeOffset)taskModel.StartTime) //指定开始时间
                        .EndAt((DateTimeOffset)taskModel.EndTime)//指定结束时间
                        .WithCronSchedule(cronSchedule)//使用Cron表达式
                        .ForJob(taskModel.TaskName, taskModel.TaskGroupName) //通过JobKey识别作业
                        .Build();                // 告诉Quartz使用我们的触发器来安排作业

                    await scheduler.ScheduleJob(job, trigger);
                    await scheduler.Start();
                    // await Task.Delay(TimeSpan.FromSeconds(5));
                    status.Status = 1;
                    status.Msg = "任务计划运行成功";
                }
                else
                {
                    status.Status = 1;
                    status.Msg = "任务计划运行正常";
                }
            }
            catch (Exception ex)
            {
                status.Status = 2;
                status.Msg = "任务计划运行异常，异常信息：" + ex.Message;
            }
            return status;

        }
        /// <summary>
        /// 停止指定任务计划
        /// </summary>
        /// <param name="jobGroup"></param>
        /// <param name="jobName"></param>
        /// <returns></returns>
        public static async Task<RunResult> StopScheduleJob(string jobGroup, string jobName)
        {
            var status = new RunResult();
            try
            {
                scheduler = await GetScheduler();
                //检查任务是否已存在
                var jk = new JobKey(jobName, jobGroup);
                //如果存在
                if (await scheduler.CheckExists(jk))
                {
                    //删除已经存在任务
                    await scheduler.DeleteJob(jk);
                    status.Status = 1;
                    status.Msg = "停止任务计划成功";
                }
                else
                {
                    status.Status = -1;
                    status.Msg = "此任务计划已经停止";

                }
                return status;
            }
            catch (Exception ex)
            {
                status.Status = 0;
                status.Msg = "停止任务计划失败,异常信息：" + ex.Message;
                return status;
            }
        }
        
    }
}
