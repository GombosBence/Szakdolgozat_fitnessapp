using Quartz;
using Quartz.Impl;

namespace azuretest.ScheduledTasks
{
    public class Scheduler
    {

        public static async Task Main()
        {
            var schedulerFactory = new StdSchedulerFactory();

            var scheduler = await schedulerFactory.GetScheduler();

            //create Job

            var job = JobBuilder.Create<DailyJob>()
                        .WithIdentity("dailyjob", "group1")
                        .Build();

            //trigger

            var trigger = TriggerBuilder.Create()
                            .WithIdentity("dailyTrigger", "group1")
                            .WithDailyTimeIntervalSchedule(x => x
                            .OnEveryDay()
                            .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay( 8 , 0 ))
                            .WithIntervalInHours(24))
                            .Build();


            await scheduler.ScheduleJob(job, trigger);
            await scheduler.Start();

            await Task.Delay(Timeout.Infinite);
            await scheduler.Shutdown();
        }


    }
}
