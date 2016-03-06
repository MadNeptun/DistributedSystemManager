using System;
using System.Collections.Generic;
using System.Linq;
using MadNeptun.DistributedSystemManager.Core.AbstractEntities;

namespace MadNeptun.DistributedSystemManager.Core.Objects
{
    public class Node<TIdType, TValue>
    {

        public Node(NodeId<TIdType> id, DistributedAlgorithm<TIdType, TValue> algorithm, NetworkComponent<TIdType, TValue> component)
        {
            _id = id;
            SetAlgorithm(algorithm);
            SetNetworkComponent(component);
            _neighbors = new List<NodeId<TIdType>>();
        }

        public delegate void NodeMessage(object sender, NodeMessageEventArgs<TIdType,TValue> e);

        public event NodeMessage OnNodeMessage;

        private readonly NodeId<TIdType> _id;

        private readonly List<NodeId<TIdType>> _neighbors;

        private readonly object _lockObject = new object();

        private DistributedAlgorithm<TIdType, TValue> _algorithmTemplate;

        private List<KeyValuePair<Guid, DateTime>> _algorithmEntryTime = new List<KeyValuePair<Guid, DateTime>>();

        private DistributedAlgorithm<TIdType, TValue> _algorithm;

        private NetworkComponent<TIdType, TValue> _networkComponent;

        public List<NodeId<TIdType>> Neighbors
        { get { return _neighbors; } }

        private void Subscribe()
        {
            _networkComponent.OnMessageReceived += _networkComponent_OnMessageReceived;
        }

        private void _networkComponent_OnMessageReceived(object sender, MessageReceivedEventArgs<TIdType, TValue> e)
        {
            ReceiveMessage(e.Message, e.NodeId);
        }

        private void ReceiveMessage(Message<TValue> message, NodeId<TIdType> sender)
        {
            
            TriggerNodeMessage(message.Value,sender);
            SendMessage(_algorithm.ReceiveMessage(message, sender, _neighbors, _id));
        }

        private void SendMessage(OperationResult<TIdType, TValue> sendData)
        {
            _networkComponent.Send(sendData.SendTo, _id);   
        }

        private void TriggerNodeMessage(TValue message, NodeId<TIdType> sender)
        {
            if (OnNodeMessage != null)
            {
                OnNodeMessage(this, new NodeMessageEventArgs<TIdType,TValue> { Message = message, Sender = sender, Receiver = _id });
            }
        }

        public NodeId<TIdType> GetId()
        {
            return _id;
        }

        private void SetAlgorithm(DistributedAlgorithm<TIdType, TValue> algorithm)
        {
            _algorithmTemplate = algorithm;
            lock (_lockObject)
            {
                _algorithm = (DistributedAlgorithm<TIdType, TValue>)Activator.CreateInstance(_algorithmTemplate.GetType());
            }
        }

        public NetworkComponent<TIdType, TValue> GetNetworkComponent()
        {
            return _networkComponent;
        }

        private void SetNetworkComponent(NetworkComponent<TIdType, TValue> component)
        {
            _networkComponent = component;
            Subscribe();
        }

        public void ClearExpiredAlgorithms()
        {
            lock (_lockObject)
            {
                _algorithm = (DistributedAlgorithm<TIdType, TValue>)Activator.CreateInstance(_algorithmTemplate.GetType());
            }
        }

        public void ExecuteInit(Message<TValue> initMessage)
        {
            var guid = Guid.NewGuid();
            _algorithmEntryTime.Add(new KeyValuePair<Guid, DateTime>(guid,DateTime.Now));
            _algorithm = (DistributedAlgorithm<TIdType,TValue>)Activator.CreateInstance(_algorithmTemplate.GetType());
            SendMessage(_algorithm.Init(initMessage, _neighbors, _id));
            TriggerNodeMessage(initMessage.Value, _id);
        }

        public override string ToString()
        {
            return "Node Id: " + _id.Id;
        }
        
    }
}
