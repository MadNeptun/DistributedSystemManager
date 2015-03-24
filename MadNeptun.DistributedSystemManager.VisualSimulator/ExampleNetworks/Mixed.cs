using System;
using System.Collections.Generic;
using MadNeptun.DistributedSystemManager.Core.AbstractEntities;
using MadNeptun.DistributedSystemManager.Core.Objects;

namespace MadNeptun.DistributedSystemManager.VisualSimulator.ExampleNetworks
{
    class Mixed : BaseNetwork
    {
        public override string Name()
        {
            return "Mixed - bidirectional - 9 nodes";
        }

        public override List<Node<string, string>> GetNetwork(DistributedAlgorithm<string, string> algorithm, NetworkComponent<string, string> component, Node<string, string>.NodeMessage function)
        {
            var result = new List<Node<string, string>>();

            var tempNode = new Node<string, string>(new NodeId<string>() { Id = "1" }, (DistributedAlgorithm<string, string>)Activator.CreateInstance(algorithm.GetType()), (NetworkComponent<string, string>)Activator.CreateInstance(component.GetType()));
            tempNode.Neighbors.Add(new NodeId<string>() { Id = "2" });
            tempNode.Neighbors.Add(new NodeId<string>() { Id = "3" });
            tempNode.OnNodeMessage += function;
            result.Add(tempNode);

            var tempNode1 = new Node<string, string>(new NodeId<string>() { Id = "2" }, (DistributedAlgorithm<string, string>)Activator.CreateInstance(algorithm.GetType()), (NetworkComponent<string, string>)Activator.CreateInstance(component.GetType()));
            tempNode1.Neighbors.Add(new NodeId<string>() { Id = "1" });
            tempNode1.Neighbors.Add(new NodeId<string>() { Id = "3" });
            tempNode1.OnNodeMessage += function;
            result.Add(tempNode1);

            var tempNode2 = new Node<string, string>(new NodeId<string>() { Id = "3" }, (DistributedAlgorithm<string, string>)Activator.CreateInstance(algorithm.GetType()), (NetworkComponent<string, string>)Activator.CreateInstance(component.GetType()));
            tempNode2.Neighbors.Add(new NodeId<string>() { Id = "2" });
            tempNode2.Neighbors.Add(new NodeId<string>() { Id = "1" });
            tempNode2.Neighbors.Add(new NodeId<string>() { Id = "4" });
            tempNode2.Neighbors.Add(new NodeId<string>() { Id = "8" });
            tempNode2.OnNodeMessage += function;
            result.Add(tempNode2);

            var tempNode3 = new Node<string, string>(new NodeId<string>() { Id = "4" }, (DistributedAlgorithm<string, string>)Activator.CreateInstance(algorithm.GetType()), (NetworkComponent<string, string>)Activator.CreateInstance(component.GetType()));
            tempNode3.Neighbors.Add(new NodeId<string>() { Id = "3" });
            tempNode3.Neighbors.Add(new NodeId<string>() { Id = "5" });
            tempNode3.Neighbors.Add(new NodeId<string>() { Id = "6" });
            tempNode3.Neighbors.Add(new NodeId<string>() { Id = "7" });
            tempNode3.OnNodeMessage += function;
            result.Add(tempNode3);

            var tempNode4 = new Node<string, string>(new NodeId<string>() { Id = "5" }, (DistributedAlgorithm<string, string>)Activator.CreateInstance(algorithm.GetType()), (NetworkComponent<string, string>)Activator.CreateInstance(component.GetType()));
            tempNode4.Neighbors.Add(new NodeId<string>() { Id = "4" });
            tempNode4.Neighbors.Add(new NodeId<string>() { Id = "6" });
            tempNode4.OnNodeMessage += function;
            result.Add(tempNode4);

            var tempNode5 = new Node<string, string>(new NodeId<string>() { Id = "6" }, (DistributedAlgorithm<string, string>)Activator.CreateInstance(algorithm.GetType()), (NetworkComponent<string, string>)Activator.CreateInstance(component.GetType()));
            tempNode5.Neighbors.Add(new NodeId<string>() { Id = "4" });
            tempNode5.Neighbors.Add(new NodeId<string>() { Id = "5" });
            tempNode5.OnNodeMessage += function;
            result.Add(tempNode5);

            var tempNode6 = new Node<string, string>(new NodeId<string>() { Id = "7" }, (DistributedAlgorithm<string, string>)Activator.CreateInstance(algorithm.GetType()), (NetworkComponent<string, string>)Activator.CreateInstance(component.GetType()));
            tempNode6.Neighbors.Add(new NodeId<string>() { Id = "4" });
            tempNode6.Neighbors.Add(new NodeId<string>() { Id = "8" });
            tempNode6.Neighbors.Add(new NodeId<string>() { Id = "9" });
            tempNode6.OnNodeMessage += function;
            result.Add(tempNode6);

            var tempNode7 = new Node<string, string>(new NodeId<string>() { Id = "8" }, (DistributedAlgorithm<string, string>)Activator.CreateInstance(algorithm.GetType()), (NetworkComponent<string, string>)Activator.CreateInstance(component.GetType()));
            tempNode7.Neighbors.Add(new NodeId<string>() { Id = "3" });
            tempNode7.Neighbors.Add(new NodeId<string>() { Id = "7" });
            tempNode7.Neighbors.Add(new NodeId<string>() { Id = "9" });
            tempNode7.OnNodeMessage += function;
            result.Add(tempNode7);

            var tempNode8 = new Node<string, string>(new NodeId<string>() { Id = "9" }, (DistributedAlgorithm<string, string>)Activator.CreateInstance(algorithm.GetType()), (NetworkComponent<string, string>)Activator.CreateInstance(component.GetType()));
            tempNode8.Neighbors.Add(new NodeId<string>() { Id = "7" });
            tempNode8.Neighbors.Add(new NodeId<string>() { Id = "8" });
            tempNode8.OnNodeMessage += function;
            result.Add(tempNode8);

            return result;
        }

        public override string ToString()
        {
            return Name();
        }
    }
}
