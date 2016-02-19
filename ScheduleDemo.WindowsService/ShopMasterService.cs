using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using ScheduleDemo.Processor;

namespace ScheduleDemo.WindowsService
{
    public partial class ShopMasterService : ServiceBase
    {
        private readonly ILog _logger = LogManager.GetLogger<ShopMasterService>();
        private IScheduleProcess _scheduleProcess;

        public ShopMasterService()
        {
            InitializeComponent();
        }

        public void OnDebug()
        {
            OnStart(null);
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                var interval = ConfigurationManager.AppSettings["Interval"];

                _scheduleProcess = new QuartzJobScheduler()
                {
                    IntervalInSeconds = Int32.Parse(interval)
                };

                _scheduleProcess.Start();

            }
            catch (Exception e)
            {
                _logger.Fatal(e);
            }
        }

        protected override void OnStop()
        {
            try
            {
                _scheduleProcess.Stop();
            }
            catch (Exception e)
            {
                _logger.Fatal(e);
            }
        }
    }
}
