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

        private DistributedAlgorithm<TIdType, TValue> _algorithm;

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
            SendMessage(_algorithm.RecieveMessage(message, sender, _neighbors));
        }

        private void SendMessage(OperationResult<TIdType, TValue> sendData)
        {
            //TriggerNodeMessage(this._id.Id + " Message sent to " + sendData.SendTo.Count + " nodes.");
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

        public void SetAlgorithm(DistributedAlgorithm<TIdType, TValue> algorithm, bool silentMode = true)
        {
            _algorithm = algorithm;
        }

        public NetworkComponent<TIdType, TValue> GetNetworkComponent()
        {
            return _networkComponent;
        }

        public void SetNetworkComponent(NetworkComponent<TIdType, TValue> component, bool silentMode = true)
        {
            _networkComponent = component;
            Subscribe();
        }

        public void ExecuteInit(Message<TValue> initMessage)
        {
            SendMessage(_algorithm.Init(initMessage, _neighbors));    
        }

        public override string ToString()
        {
            return "Node Id: " + _id.Id;
        }
        
    }
}
