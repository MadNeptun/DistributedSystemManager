using System;
using System.Collections.Generic;
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

        public delegate void NodeMessage(object sender, NodeMessageEventArgs<TValue> e);

        public event NodeMessage OnNodeMessage;

        private readonly NodeId<TIdType> _id;

        private readonly List<NodeId<TIdType>> _neighbors;

        private readonly object _lockObject = new object();

        private DistributedAlgorithm<TIdType, TValue> _algorithmTemplate;

        private readonly Dictionary<Guid, DistributedAlgorithm<TIdType, TValue>> _algorithms = new Dictionary<Guid,DistributedAlgorithm<TIdType,TValue>>();

        private NetworkComponent<TIdType, TValue> _networkComponent;

        public List<NodeId<TIdType>> Neighbors
        { get { return _neighbors; } }

        private void Subscribe()
        {
            _networkComponent.OnMessageRecieved += _networkComponent_OnMessageRecieved;
        }

        private void _networkComponent_OnMessageRecieved(object sender, MessageRecievedEventArgs<TIdType, TValue> e)
        {
            RecieveMessage(e.Message, e.NodeId);
        }

        private void RecieveMessage(Message<TValue> message, NodeId<TIdType> sender)
        {
            lock (_lockObject)
            {
                if (!_algorithms.ContainsKey(message.ExecutionId))
                    _algorithms.Add(message.ExecutionId,
                        (DistributedAlgorithm<TIdType, TValue>) Activator.CreateInstance(_algorithmTemplate.GetType()));
            }
            SendMessage(_algorithms[message.ExecutionId].RecieveMessage(message, sender, _neighbors));
        }

        private void SendMessage(OperationResult<TIdType, TValue> sendData)
        {
            _networkComponent.Send(sendData.Message, sendData.SendTo, _id);   
        }

        private void TriggerNodeMessage(TValue message)
        {
            if (OnNodeMessage != null)
            {
                OnNodeMessage(this, new NodeMessageEventArgs<TValue>() { Message = message });
            }
        }

        public NodeId<TIdType> GetId()
        {
            return _id;
        }

        private void SetAlgorithm(DistributedAlgorithm<TIdType, TValue> algorithm)
        {
            _algorithmTemplate = algorithm;
            _algorithms.Clear();
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

        public void ExecuteInit(Message<TValue> initMessage)
        {
            var guid = Guid.NewGuid();
            initMessage.ExecutionId = guid;
            _algorithms.Add(guid, (DistributedAlgorithm<TIdType,TValue>)Activator.CreateInstance(_algorithmTemplate.GetType()));
            SendMessage(_algorithms[initMessage.ExecutionId].Init(initMessage, _neighbors));    
        }

        public override string ToString()
        {
            return "Node Id: " + _id.Id;
        }
        
    }
}
