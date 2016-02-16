using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScheduleDemo.Processor
{
    public class ScheduleProcess
    {
        public bool Enabled { get; set; }

        public void Start()
        {
//            var producer = new ProduceDelegate(Poll);
//            producer.Invoke();
            var t = new Thread(Poll);
            t.Start();
        }

        private void Poll()
        {
            var counter = 0;

            while (true)
            {
                if (!Enabled)
                    break;

                var count = counter.ToString(CultureInfo.InvariantCulture).PadLeft(6, '0');
                Console.WriteLine("Started {0}", count);
                counter++;

                Thread.Sleep(1000);
            }

            Console.WriteLine();
            Console.WriteLine("Inactive");
        }


        private delegate void ProduceDelegate();
    }
}
