using System;
using System.Collections.Generic;
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

            var process = new ScheduleProcess
            {
                Enabled = true
            };

            while (true)
            {
                var keyPressed = Console.ReadKey();
                if (keyPressed.Key == ConsoleKey.Q)
                {
                    process.Enabled = false;
                }

                if (keyPressed.Key == ConsoleKey.Enter)
                {
                    process.Enabled = true;
                    process.Start();
                }
            }
        }
    }
}
