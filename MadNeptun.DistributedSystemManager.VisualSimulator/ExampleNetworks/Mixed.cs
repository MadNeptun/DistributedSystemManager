using MadNeptun.DistributedSystemManager.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MadNeptun.DistributedSystemManager.VisualSimulator.ExampleNetworks
{
    class Mixed : BaseNetwork
    {
        public override string Name()
        {
            return "Mixed - bidirectional - 9 nodes";
        }

        public override List<Core.Objects.Node> GetNetwork(Core.Objects.DistributedAlgorithm algorithm, Core.Objects.NetworkComponent component, Core.Objects.Node.NodeMessage function)
        {
            var result = new List<Node>();

            var tempNode = new Node(new NodeId() { Id = "1" }, (DistributedAlgorithm)Activator.CreateInstance(algorithm.GetType()), (NetworkComponent)Activator.CreateInstance(component.GetType()));
            tempNode.Neighbors.Add(new NodeId() { Id = "2" });
            tempNode.Neighbors.Add(new NodeId() { Id = "3" });
            tempNode.OnNodeMessage += function;
            result.Add(tempNode);

            tempNode = new Node(new NodeId() { Id = "2" }, (DistributedAlgorithm)Activator.CreateInstance(algorithm.GetType()), (NetworkComponent)Activator.CreateInstance(component.GetType()));
            tempNode.Neighbors.Add(new NodeId() { Id = "1" });
            tempNode.Neighbors.Add(new NodeId() { Id = "3" });
            tempNode.OnNodeMessage += function;
            result.Add(tempNode);

            tempNode = new Node(new NodeId() { Id = "3" }, (DistributedAlgorithm)Activator.CreateInstance(algorithm.GetType()), (NetworkComponent)Activator.CreateInstance(component.GetType()));
            tempNode.Neighbors.Add(new NodeId() { Id = "2" });
            tempNode.Neighbors.Add(new NodeId() { Id = "1" });
            tempNode.Neighbors.Add(new NodeId() { Id = "4" });
            tempNode.Neighbors.Add(new NodeId() { Id = "8" });
            tempNode.OnNodeMessage += function;
            result.Add(tempNode);

            tempNode = new Node(new NodeId() { Id = "4" }, (DistributedAlgorithm)Activator.CreateInstance(algorithm.GetType()), (NetworkComponent)Activator.CreateInstance(component.GetType()));
            tempNode.Neighbors.Add(new NodeId() { Id = "3" });
            tempNode.Neighbors.Add(new NodeId() { Id = "5" });
            tempNode.Neighbors.Add(new NodeId() { Id = "6" });
            tempNode.Neighbors.Add(new NodeId() { Id = "7" });
            tempNode.OnNodeMessage += function;
            result.Add(tempNode);

            tempNode = new Node(new NodeId() { Id = "5" }, (DistributedAlgorithm)Activator.CreateInstance(algorithm.GetType()), (NetworkComponent)Activator.CreateInstance(component.GetType()));
            tempNode.Neighbors.Add(new NodeId() { Id = "4" });
            tempNode.Neighbors.Add(new NodeId() { Id = "6" });
            tempNode.OnNodeMessage += function;
            result.Add(tempNode);

            tempNode = new Node(new NodeId() { Id = "6" }, (DistributedAlgorithm)Activator.CreateInstance(algorithm.GetType()), (NetworkComponent)Activator.CreateInstance(component.GetType()));
            tempNode.Neighbors.Add(new NodeId() { Id = "4" });
            tempNode.Neighbors.Add(new NodeId() { Id = "5" });
            tempNode.OnNodeMessage += function;
            result.Add(tempNode);

            tempNode = new Node(new NodeId() { Id = "7" }, (DistributedAlgorithm)Activator.CreateInstance(algorithm.GetType()), (NetworkComponent)Activator.CreateInstance(component.GetType()));
            tempNode.Neighbors.Add(new NodeId() { Id = "4" });
            tempNode.Neighbors.Add(new NodeId() { Id = "8" });
            tempNode.Neighbors.Add(new NodeId() { Id = "9" });
            tempNode.OnNodeMessage += function;
            result.Add(tempNode);

            tempNode = new Node(new NodeId() { Id = "8" }, (DistributedAlgorithm)Activator.CreateInstance(algorithm.GetType()), (NetworkComponent)Activator.CreateInstance(component.GetType()));
            tempNode.Neighbors.Add(new NodeId() { Id = "3" });
            tempNode.Neighbors.Add(new NodeId() { Id = "7" });
            tempNode.Neighbors.Add(new NodeId() { Id = "9" });
            tempNode.OnNodeMessage += function;
            result.Add(tempNode);

            tempNode = new Node(new NodeId() { Id = "9" }, (DistributedAlgorithm)Activator.CreateInstance(algorithm.GetType()), (NetworkComponent)Activator.CreateInstance(component.GetType()));
            tempNode.Neighbors.Add(new NodeId() { Id = "7" });
            tempNode.Neighbors.Add(new NodeId() { Id = "8" });
            tempNode.OnNodeMessage += function;
            result.Add(tempNode);

            return result;
        }

        public override string ToString()
        {
            return Name();
        }
    }
}
