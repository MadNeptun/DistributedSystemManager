using System.ServiceProcess;

namespace MadNeptun.DistributedSystemManager.Service
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new NetworkService() 
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
