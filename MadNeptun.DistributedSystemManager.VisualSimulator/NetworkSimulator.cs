using MadNeptun.DistributedSystemManager.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MadNeptun.DistributedSystemManager.VisualSimulator
{
    class NetworkSimulator : NetworkComponent
    {
        public override void Send(Message message, List<NodeId> recievers, NodeId sender)
        {
            var data = NodesManager.Instance.Nodes.
                Where(n => recievers.Select(s => s.Id).Contains(n.GetId().Id)).ToList();
            if (data.Count() > 0)
            {

                foreach (var node in data)
                {
                    Thread t = new Thread(Act);
                    t.Start(new List<object>() { node.GetNetworkComponent(), sender, message });
                    t.Join(10); //todo find a workaround
                }
            }

        }

        private static void Act(object args)
        {
            var data = (List<object>)args;
            ((NetworkComponent)data[0]).Recieve((Message)data[2], (NodeId)data[1]);
        }

        public override void Recieve(Message message, NodeId sender)
        {
            this.InformNode(new MessageRecievedEventArgs() { Message = message, NodeId = sender });
        }
    }
}
