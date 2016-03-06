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
    }
}
