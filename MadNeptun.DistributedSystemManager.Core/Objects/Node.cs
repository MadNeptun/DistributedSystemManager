using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MadNeptun.DistributedSystemManager.Core.Objects
{
    public class Node
    {

        public Node(NodeId id, DistributedAlgorithm algorithm, NetworkComponent component)
        {
            _id = id;
            SetAlgorithm(algorithm);
            SetNetworkComponent(component);
            _neighbors = new List<NodeId>();
        }

        public delegate void NodeMessage(object sender, NodeMessageEventArgs e);

        public event NodeMessage OnNodeMessage;

        private NodeId _id;

        private List<NodeId> _neighbors;

        private DistributedAlgorithm _algorithm;

        private NetworkComponent _networkComponent;

        public List<NodeId> Neighbors
        { get { return _neighbors; } }

        private void Subscribe()
        {
            _networkComponent.OnMessageRecieved += _networkComponent_OnMessageRecieved;
        }

        private void _networkComponent_OnMessageRecieved(object sender, MessageRecievedEventArgs e)
        {
            RecieveMessage(e.Message, e.NodeId);
        } 

        private void RecieveMessage(Message message, NodeId sender)
        {
            TriggerNodeMessage(this._id.Id+" Message '"+message.Value+"' recieved from node " + sender.Id+".");
            SendMessage(_algorithm.RecieveMessage(message, sender, _neighbors));
        }

        private void SendMessage(OperationResult sendData)
        {
            //TriggerNodeMessage(this._id.Id + " Message sent to " + sendData.SendTo.Count + " nodes.");
            _networkComponent.Send(sendData.Message, sendData.SendTo, this._id);   
        }

        private void TriggerNodeMessage(string message)
        {
            if (OnNodeMessage != null)
            {
                OnNodeMessage(this, new NodeMessageEventArgs() { Message = message });
            }
        }

        public NodeId GetId()
        {
            return _id;
        }

        public void SetAlgorithm(DistributedAlgorithm algorithm)
        {
            _algorithm = algorithm;
            TriggerNodeMessage("Algorithm changed "+_id.Id+".");
        }

        public NetworkComponent GetNetworkComponent()
        {
            return _networkComponent;
        }

        public void SetNetworkComponent(NetworkComponent component)
        {
            _networkComponent = component;
            Subscribe();
            TriggerNodeMessage("Network component changed "+_id.Id+".");
        }

        public void ExecuteInit(Message initMessage)
        {
            TriggerNodeMessage("Init performed " + _id.Id + ".");
            SendMessage(_algorithm.Init(initMessage, _neighbors));    
        }
        
    }
}
