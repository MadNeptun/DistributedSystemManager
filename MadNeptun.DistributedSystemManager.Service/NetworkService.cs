using System.ServiceProcess;

namespace MadNeptun.DistributedSystemManager.Service
{
    public partial class NetworkService : ServiceBase
    {
        public NetworkService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
        }

        protected override void OnStop()
        {
        }
    }
}
