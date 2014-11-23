using MadNeptun.DistributedSystemManager.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadNeptun.DistributedSystemManager.VisualSimulator.ExampleNetworks
{
    class Ring : BaseNetwork
    {
        public override string Name()
        {
            return "Ring - bidirectional - 7 nodes";
        }

        public override List<Node> GetNetwork(DistributedAlgorithm algorithm, NetworkComponent component, MadNeptun.DistributedSystemManager.Core.Objects.Node.NodeMessage function)
        {
            var result = new List<Node>();

            for (int i = 1; i < 8; i++)
            {
                var tempNode = new Node(new NodeId() { Id = i.ToString() }, (DistributedAlgorithm)Activator.CreateInstance(algorithm.GetType()), (NetworkComponent)Activator.CreateInstance(component.GetType()));
                tempNode.Neighbors.Add(new NodeId() { Id = i == 7 ? "1" : (i + 1).ToString() });
                tempNode.Neighbors.Add(new NodeId() { Id = i == 1 ? "7" : (i - 1).ToString() });
                tempNode.OnNodeMessage += function;
                result.Add(tempNode);
            }

            return result;
        }

        public override string ToString()
        {
            return Name();
        }
    }
}
