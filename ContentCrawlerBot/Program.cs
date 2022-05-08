using System;
using System.Text;
using System.Threading.Tasks;
using ContentCrawlerBot.Queue;
using Newtonsoft.Json.Linq;
using Quartz;
using Quartz.Impl;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ContentCrawlerBot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = await factory.GetScheduler();
            
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<BotHandlerArticle>()
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

            // some sleep to show what's happening
            await Task.Delay(TimeSpan.FromDays(1));
            
            await scheduler.Shutdown();

            Console.WriteLine("..........................");
            Console.ReadKey();
        }
    }
}