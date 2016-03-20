using System;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceProcess;
using System.Threading;
using MadNeptun.DistributedSystemManager.Core.AbstractEntities;
using MadNeptun.DistributedSystemManager.Core.Objects;
using MadNeptun.DistributedSystemManager.Service.ComunicationService;

namespace MadNeptun.DistributedSystemManager.Service
{
    public partial class NetworkService : ServiceBase
    {
        private Node<int, string> _networkNode;

        private Timer _backgroundOperationsTimer;

        private ServiceHost _serviceHost;

        public NetworkService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            ConfigurationManager.LoadConfiguartion(args);     
            _networkNode = new Node<int, string>(
                new NodeId<int> { Id = ConfigurationManager.Instance.NodeId }, 
                (DistributedAlgorithm<int, string>)Activator.CreateInstance(ConfigurationManager.Instance.GetAlgorithmType()), 
                (NetworkComponent<int, string>)Activator.CreateInstance(ConfigurationManager.Instance.GetNetworkComponentType()));
            _networkNode.OnNodeMessage += _networkNode_OnNodeMessage;
            _networkNode.Neighbors.AddRange(ConfigurationManager.Instance.GetNeigborhood());
            _networkNode.GetNetworkComponent().Run(ConfigurationManager.Instance.NetworkComponentConfiguration);
            _backgroundOperationsTimer = new Timer(delegate { BackgroundOperations(); }, null, 0, (int)ConfigurationManager.Instance.BackgroundOperationInterval * 60 * 1000);

            if (_serviceHost != null)
                _serviceHost.Close();
            
            _serviceHost = new SCSServiceHost(_networkNode, ConfigurationManager.Instance.SystemServiceUrl);
            _serviceHost.Open();
        }

        void _networkNode_OnNodeMessage(object sender, NodeMessageEventArgs<int, string> e)
        {
           TrySendLogToAdministratorPanel(string.Format("Message sent by node: {0} {1}", e.Sender.Id, e.Message));
        }

        private void TrySendLogToAdministratorPanel(string log)
        {
            try
            {
                var adminPanel = new Log.LogServiceClient("BasicHttpBinding_ILogService");
                adminPanel.RecieveLog(_networkNode.GetId().Id, log);
                adminPanel.Close();
            }
            catch (Exception ex)
            {
               EventLogHelper.WriteErrorToEventLog(ex);
            }
        }

        protected override void OnStop()
        {
            if (_serviceHost != null)
            {
                _serviceHost.Close();
                _serviceHost = null;
            }
            _backgroundOperationsTimer.Dispose();
            _backgroundOperationsTimer = null;
            _networkNode.GetNetworkComponent().ShutDown();
        }

        private void BackgroundOperations()
        {
            //put here code that should be executed periodicaly on local node
        }
    }
}
