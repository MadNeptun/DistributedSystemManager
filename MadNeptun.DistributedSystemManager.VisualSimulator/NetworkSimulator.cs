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
            if(data.Count() > 0)
            {
                data.AsParallel().WithDegreeOfParallelism(data.Count).ForAll(t => t.GetNetworkComponent().Recieve(message, sender));
            }

        }

        public override void Recieve(Message message, NodeId sender)
        {
            this.InformNode(new MessageRecievedEventArgs() { Message = message, NodeId = sender });
        }
    }
}
