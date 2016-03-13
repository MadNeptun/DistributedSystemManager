using System;
using MadNeptun.DistributedSystemManager.Core.AbstractEntities;
using MadNeptun.DistributedSystemManager.Core.Objects;

namespace MadNeptun.DistributedSystemManager.Service.ComunicationService
{
    class SystemCommunicationService : ISystemCommunicationService
    {

        private Node<int, string> _node; 
        public SystemCommunicationService(Node<int, string> node)
        {
            _node = node;
        }

        public void Receive(Message<string> message, NodeId<int> sender)
        {
            _node.GetNetworkComponent().Receive(message, sender);
        }

        public void Init(string message)
        {
            _node.ExecuteInit(new Message<string> { Value = message });
        }


        public void ClearExpiredAlorithms()
        {
            _node.ClearExpiredAlgorithms();  
        }

        public bool Alive()
        {
            return true;
        }

        public void Reconfigure(string[] configuration)
        {
            ConfigurationManager.LoadConfiguartion(configuration);

            _node.SetAlgorithm(
                (DistributedAlgorithm<int, string>)
                    Activator.CreateInstance(ConfigurationManager.Instance.GetAlgorithmType()));

            _node.SetNetworkComponent(
                (NetworkComponent<int, string>)
                    Activator.CreateInstance(ConfigurationManager.Instance.GetNetworkComponentType()));

            _node.Neighbors.Clear();
            _node.Neighbors.AddRange(ConfigurationManager.Instance.GetNeigborhood());

            _node.GetNetworkComponent().Run(ConfigurationManager.Instance.NetworkComponentConfiguration);
        }

        public string[] GetConfiguration()
        {
            return ConfigurationManager.Instance.RawConfiguration;
        }

        public void SaveFileOnNode(string fullPathToSaveFile, byte[] file)
        {
            System.IO.File.WriteAllBytes(fullPathToSaveFile, file);
        }
    }
}
