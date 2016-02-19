using System;
using Common.Logging;
using Common.Logging.Simple;
using Quartz;
using Quartz.Impl;

namespace ScheduleDemo.Processor
{
    public class QuartzJobScheduler : IJobScheduler
    {
        private readonly ILog _logger = LogManager.GetLogger<ConsoleOutLogger>();
        private IScheduler _scheduler;

        public int IntervalInSeconds { get; set; }
        
        public QuartzJobScheduler()
        {
            LogManager.Adapter = new ConsoleOutLoggerFactoryAdapter { Level = LogLevel.Info };
            InitializeJobs();
        }

        private void InitializeJobs()
        {
            try
            {
                if (IntervalInSeconds <= 0)
                {
                    IntervalInSeconds = 10;
                }

                _scheduler = StdSchedulerFactory.GetDefaultScheduler();

                var job = JobBuilder.Create<HelloJob>()
                    .WithIdentity("CleanMarbleJob", "ShopMasterGroup")
                    .Build();

                var trigger = TriggerBuilder.Create()
                    .WithIdentity("CleanMarbleTrigger", "ShopMasterGroup")
                    .StartNow()
                    .WithSimpleSchedule(x => x.WithIntervalInSeconds(IntervalInSeconds).RepeatForever())
                    .Build();

                _scheduler.ScheduleJob(job, trigger);
            }
            catch (Exception e)
            {
                _logger.Error("Server initialization failed:" + e.Message, e);
                throw;
            }
        }

        public void Start()
        {
            try
            {
                _scheduler.Start();
            }
            catch (Exception ex)
            {
                _logger.Fatal(string.Format("Scheduler start failed: {0}", ex.Message), ex);
                throw;
            }

            _logger.Info("Scheduler started successfully");
        }

        public void Resume()
        {
            _scheduler.ResumeAll();
        }

        public void Pause()
        {
            _scheduler.Standby();
        }


        public void Continue()
        {
           if (!_scheduler.IsStarted)
                _scheduler.Start();
        }

        public void Stop()
        {
            _logger.Info("Sample Windows Service stopping");
            _scheduler.Shutdown();
            _logger.Info("Sample Windows Service stopped");
        }
    }

    public interface IJobScheduler
    {
        void Start();
        void Stop();
    }

    public class HelloJob: IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("A new marble has arrived.");
        }
    }
}
