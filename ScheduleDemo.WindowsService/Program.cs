using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScheduleDemo.WindowsService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            var service = new ShopMasterService();
            service.OnDebug();
            Thread.Sleep(Timeout.Infinite);
//            ServiceBase[] ServicesToRun;
//            ServicesToRun = new ServiceBase[] 
//            { 
//                new ShopMasterService() 
//            };
//            ServiceBase.Run(ServicesToRun);
        }
    }
}
