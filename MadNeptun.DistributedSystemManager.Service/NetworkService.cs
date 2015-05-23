using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Threading;
using MadNeptun.DistributedSystemManager.Core;
using MadNeptun.DistributedSystemManager.Core.AbstractEntities;
using MadNeptun.DistributedSystemManager.Core.Objects;

namespace MadNeptun.DistributedSystemManager.Service
{
    public partial class NetworkService : ServiceBase
    {
        private Thread _backgroundOperationThread;

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
                (NetworkComponent<int, string>)Activator.CreateInstance(component.MakeGenericType(new[] { typeof(int), typeof(string) })),
                ConfigurationManager.Instance.ExpireTime);
            _networkNode.Neighbors.AddRange(ConfigurationManager.Instance.GetNeigborhood());
            _networkNode.GetNetworkComponent().Run(ConfigurationManager.Instance.NetworkComponentConfiguration);
            _backgroundOperationThread = new Thread(BackgroundThreadMethod);
            _backgroundOperationThread.Start();
        }

        private void BackgroundThreadMethod()
        {
            //TODO rewrite to sth more elegant
            while (true)
            {
                Thread.Sleep(30*60*1000);
                _networkNode.ClearExpiredAlgorithms();
            }
        }

        protected override void OnStop()
        {
            try
            {
                _backgroundOperationThread.Abort();
            }
            catch (ThreadAbortException)
            {
                //TODO rewrite to sth more elegant
            }
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
                case Method.ClearUnused:
                    _networkNode.ClearExpiredAlgorithms();
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
