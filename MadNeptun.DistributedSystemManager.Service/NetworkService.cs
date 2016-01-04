﻿using System;
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
            var assembly = Assembly.LoadFrom(ConfigurationManager.Instance.ImplementationDllPath);
            var algorithm = assembly.GetTypes().First(p => p.Name == ConfigurationManager.Instance.AlgorithmClassName);
            var component = assembly.GetTypes().First(p => p.Name == ConfigurationManager.Instance.NetworkComponentName);
            _networkNode = new Node<int, string>(
                new NodeId<int> { Id = ConfigurationManager.Instance.NodeId }, 
                (DistributedAlgorithm<int, string>)Activator.CreateInstance(algorithm), 
                (NetworkComponent<int, string>)Activator.CreateInstance(component));
            _networkNode.Neighbors.AddRange(ConfigurationManager.Instance.GetNeigborhood());
            _networkNode.GetNetworkComponent().Run(ConfigurationManager.Instance.NetworkComponentConfiguration);
            _backgroundOperationsTimer = new Timer(delegate { BackgroundOperations(); }, null, 0, (int)ConfigurationManager.Instance.BackgroundOperationInterval * 60 * 1000);

            if (_serviceHost != null)
                _serviceHost.Close();

            _serviceHost = new ServiceHost(typeof(SystemCommunicationService));
            _serviceHost.Open();
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
