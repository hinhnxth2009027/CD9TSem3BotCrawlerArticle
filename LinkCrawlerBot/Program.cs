using System;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;

namespace LinkCrawlerBot
{
    class Program
    {
        static  async Task Main(string[] args)
        {
            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = await factory.GetScheduler();
            
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<BotHandlerSource>()
                .WithIdentity("job1", "group1")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(60)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);

            await Task.Delay(TimeSpan.FromDays(1));

            await scheduler.Shutdown();

            Console.WriteLine("..........................");
            Console.ReadKey();
        }
    }
}