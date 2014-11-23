using MadNeptun.DistributedSystemManager.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MadNeptun.DistributedSystemManager.VisualSimulator.ExampleNetworks
{
    class Tree : BaseNetwork
    {
        public override string Name()
        {
            return "Tree - bidirectional - 8 nodes";
        }

        public override List<Core.Objects.Node> GetNetwork(Core.Objects.DistributedAlgorithm algorithm, Core.Objects.NetworkComponent component, Core.Objects.Node.NodeMessage function)
        {
            var result = new List<Node>();

            var tempNode = new Node(new NodeId() { Id = "1" }, (DistributedAlgorithm)Activator.CreateInstance(algorithm.GetType()), (NetworkComponent)Activator.CreateInstance(component.GetType()));
            tempNode.Neighbors.Add(new NodeId() { Id = "2" });
            tempNode.OnNodeMessage += function;
            result.Add(tempNode);

            var tempNode1 = new Node(new NodeId() { Id = "2" }, (DistributedAlgorithm)Activator.CreateInstance(algorithm.GetType()), (NetworkComponent)Activator.CreateInstance(component.GetType()));
            tempNode1.Neighbors.Add(new NodeId() { Id = "1" });
            tempNode1.Neighbors.Add(new NodeId() { Id = "3" });
            tempNode1.Neighbors.Add(new NodeId() { Id = "4" });
            tempNode1.Neighbors.Add(new NodeId() { Id = "5" });
            tempNode1.OnNodeMessage += function;
            result.Add(tempNode1);

            var tempNode2 = new Node(new NodeId() { Id = "3" }, (DistributedAlgorithm)Activator.CreateInstance(algorithm.GetType()), (NetworkComponent)Activator.CreateInstance(component.GetType()));
            tempNode2.Neighbors.Add(new NodeId() { Id = "2" });
            tempNode2.OnNodeMessage += function;
            result.Add(tempNode2);

            var tempNode3 = new Node(new NodeId() { Id = "4" }, (DistributedAlgorithm)Activator.CreateInstance(algorithm.GetType()), (NetworkComponent)Activator.CreateInstance(component.GetType()));
            tempNode3.Neighbors.Add(new NodeId() { Id = "2" });
            tempNode3.Neighbors.Add(new NodeId() { Id = "8" });
            tempNode3.OnNodeMessage += function;
            result.Add(tempNode3);

            var tempNode4 = new Node(new NodeId() { Id = "5" }, (DistributedAlgorithm)Activator.CreateInstance(algorithm.GetType()), (NetworkComponent)Activator.CreateInstance(component.GetType()));
            tempNode4.Neighbors.Add(new NodeId() { Id = "2" });
            tempNode4.Neighbors.Add(new NodeId() { Id = "6" });
            tempNode4.Neighbors.Add(new NodeId() { Id = "7" });
            tempNode4.OnNodeMessage += function;
            result.Add(tempNode4);

            var tempNode5 = new Node(new NodeId() { Id = "6" }, (DistributedAlgorithm)Activator.CreateInstance(algorithm.GetType()), (NetworkComponent)Activator.CreateInstance(component.GetType()));
            tempNode5.Neighbors.Add(new NodeId() { Id = "5" });
            tempNode5.OnNodeMessage += function;
            result.Add(tempNode5);

            var tempNode6 = new Node(new NodeId() { Id = "7" }, (DistributedAlgorithm)Activator.CreateInstance(algorithm.GetType()), (NetworkComponent)Activator.CreateInstance(component.GetType()));
            tempNode6.Neighbors.Add(new NodeId() { Id = "5" });
            tempNode6.OnNodeMessage += function;
            result.Add(tempNode6);

            var tempNode7 = new Node(new NodeId() { Id = "8" }, (DistributedAlgorithm)Activator.CreateInstance(algorithm.GetType()), (NetworkComponent)Activator.CreateInstance(component.GetType()));
            tempNode7.Neighbors.Add(new NodeId() { Id = "4" });
            tempNode7.OnNodeMessage += function;
            result.Add(tempNode7);

            return result;
        }

        public override string ToString()
        {
            return Name();
        }
    }
}
