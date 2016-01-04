using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel.Dispatcher;
using System.Text;
using MadNeptun.DistributedSystemManager.Core.AbstractEntities;
using MadNeptun.DistributedSystemManager.Core.Objects;

namespace MadNeptun.DistributedSystemManager.Service.ComunicationService
{
    class SystemCommunicationService : ISystemCommunicationService<int, string>
    {

        private Node<int, string> _node; 
        public SystemCommunicationService(Node<int, string> node)
        {
            _node = node;
        }

        public void Receive(Core.Objects.Message<string> message, Core.Objects.NodeId<int> sender)
        {
            _node.GetNetworkComponent().Receive(message, sender);
        }

        public void Init(string message)
        {
            _node.ExecuteInit(new Message<string>() { Value = message });
        }


        public void ClearExpiredAlorithms()
        {
            _node.ClearExpiredAlgorithms();  
        }
    }
}
