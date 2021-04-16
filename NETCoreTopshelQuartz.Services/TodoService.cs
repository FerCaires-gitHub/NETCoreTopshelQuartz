using Microsoft.Extensions.Logging;
using NETCoreTopshelQuartz.Domain.Services;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;

namespace NETCoreTopshelQuartz.Services
{
    public class TodoService : ITodoService
    {
        private readonly IJobFactory _jobFactory;

        public TodoService( IJobFactory jobFactory)
        {
            _jobFactory = jobFactory;
        }

        public void OnStart()
        {
            var schedulerFactory = new StdSchedulerFactory();
            var scheduler = schedulerFactory.GetScheduler().Result;
            scheduler.JobFactory = _jobFactory;
            

            var jobDetails = JobBuilder.Create<TodoJob>()
                .WithIdentity(JobKey.Create("Todo job", "Todo processing"))
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity(new TriggerKey("Do the job", "Todo processing"))
                .StartNow()
                //.WithCronSchedule("0/1 0 0 ? * * *")
                .WithSimpleSchedule(scheduler => {
                    scheduler.WithIntervalInSeconds(15);
                    scheduler.RepeatForever();
                })
                .Build();

            scheduler.ScheduleJob(jobDetails, trigger).Wait();
            scheduler.Start().Wait();
        }

        public void OnStop()
        {
            Console.WriteLine("Stopping Service");
        }
    }
}
