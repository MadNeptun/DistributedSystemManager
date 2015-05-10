using System;
using System.Collections.Generic;
using System.Linq;
using MadNeptun.DistributedSystemManager.Core.AbstractEntities;

namespace MadNeptun.DistributedSystemManager.Core.Objects
{
    public class Node<TIdType, TValue>
    {

        public Node(NodeId<TIdType> id, DistributedAlgorithm<TIdType, TValue> algorithm, NetworkComponent<TIdType, TValue> component, double algorithmExpireTimeInHours = 1.0)
        {
            _id = id;
            SetAlgorithm(algorithm);
            SetNetworkComponent(component);
            _neighbors = new List<NodeId<TIdType>>();
            _expireTimeInHours = algorithmExpireTimeInHours;
        }

        public delegate void NodeMessage(object sender, NodeMessageEventArgs<TValue> e);

        public event NodeMessage OnNodeMessage;

        private readonly NodeId<TIdType> _id;

        private readonly List<NodeId<TIdType>> _neighbors;

        private readonly object _lockObject = new object();

        private DistributedAlgorithm<TIdType, TValue> _algorithmTemplate;

        private readonly double _expireTimeInHours;

        private List<KeyValuePair<Guid, DateTime>> _algorithmEntryTime = new List<KeyValuePair<Guid, DateTime>>(); 
 
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
                {
                    _algorithmEntryTime.Add(new KeyValuePair<Guid, DateTime>(message.ExecutionId,DateTime.Now));
                    _algorithms.Add(message.ExecutionId,
                        (DistributedAlgorithm<TIdType, TValue>) Activator.CreateInstance(_algorithmTemplate.GetType()));
                }
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

        public void ClearExpiredAlgorithms()
        {
            lock (_lockObject)
            {
                var list = new List<Guid>();
                foreach (var entry in _algorithmEntryTime)
                {
                    if (entry.Value.AddHours(_expireTimeInHours) < DateTime.Now)
                    {
                        list.Add(entry.Key);
                        _algorithms.Remove(entry.Key);
                    }
                }
                _algorithmEntryTime = _algorithmEntryTime.Where(i => !list.Contains(i.Key)).ToList();
            }
        }

        public void ExecuteInit(Message<TValue> initMessage)
        {
            var guid = Guid.NewGuid();
            initMessage.ExecutionId = guid;
            _algorithmEntryTime.Add(new KeyValuePair<Guid, DateTime>(guid,DateTime.Now));
            _algorithms.Add(guid, (DistributedAlgorithm<TIdType,TValue>)Activator.CreateInstance(_algorithmTemplate.GetType()));
            SendMessage(_algorithms[initMessage.ExecutionId].Init(initMessage, _neighbors));
            ClearExpiredAlgorithms();
        }

        public override string ToString()
        {
            return "Node Id: " + _id.Id;
        }
        
    }
}
