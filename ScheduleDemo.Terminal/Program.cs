using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScheduleDemo.Processor;

namespace ScheduleDemo.Terminal
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var interval = ConfigurationManager.AppSettings["Interval"];

                var process = new QuartzJobScheduler()
                {
                    IntervalInSeconds = Int32.Parse(interval)
                };

                process.Start();
                Console.ReadLine();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
