using System;
using System.IO;
using System.Reflection;
using System.ServiceProcess;
using MadNeptun.DistributedSystemManager.Core;
using MadNeptun.DistributedSystemManager.Core.AbstractEntities;
using MadNeptun.DistributedSystemManager.Core.Objects;

namespace MadNeptun.DistributedSystemManager.Service
{
    public partial class NetworkService : ServiceBase
    {
        private Node<int, string> _networkNode; 

        public NetworkService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            ConfigurationManager.LoadConfiguartion(args);
            var assembly = Assembly.LoadFrom(ConfigurationManager.Instance.ImplementationDllPath);
            var algorithm = assembly.GetType(ConfigurationManager.Instance.AlgorithmClassName);
            var component = assembly.GetType(ConfigurationManager.Instance.NetworkComponentName);
            _networkNode = new Node<int, string>(
                new NodeId<int>(){ Id = ConfigurationManager.Instance.NodeId }, 
                (DistributedAlgorithm<int, string>)Activator.CreateInstance(algorithm.MakeGenericType(new[] { typeof(int), typeof(string) })), 
                (NetworkComponent<int, string>)Activator.CreateInstance(component.MakeGenericType(new[] { typeof(int), typeof(string) }))
                );
            _networkNode.GetNetworkComponent().Run(ConfigurationManager.Instance.NetworkComponentConfiguration);
        }

        protected override void OnStop()
        {
            _networkNode.GetNetworkComponent().ShutDown();
        }

        protected override void OnCustomCommand(int command)
        {
            switch ((Method)command)
            {
                case Method.Init:
                    ExecuteInit(
                        File.ReadAllText(Path.Combine(ConfigurationManager.Instance.ComunicationExchangeFolder,
                            ConfigurationManager.Instance.InitFile)));
                    break;
                default: base.OnCustomCommand(command);
                    break;
            }
        }

        private void ExecuteInit(string message)
        {
            _networkNode.ExecuteInit(new Message<string>() { Value = message });
        }
    }
}
