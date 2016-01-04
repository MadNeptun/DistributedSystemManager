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

        public override List<Node<int, string>> GetNetwork(DistributedAlgorithm<int, string> algorithm, NetworkComponent<int, string> component, Node<int, string>.NodeMessage function)
        {
            var result = new List<Node<int, string>>();

            var tempNode = new Node<int, string>(new NodeId<int> { Id = 1 }, (DistributedAlgorithm<int, string>)Activator.CreateInstance(algorithm.GetType()), (NetworkComponent<int, string>)Activator.CreateInstance(component.GetType()));
            tempNode.Neighbors.Add(new NodeId<int> { Id = 2 });
            tempNode.Neighbors.Add(new NodeId<int> { Id = 3 });
            tempNode.OnNodeMessage += function;
            result.Add(tempNode);

            var tempNode1 = new Node<int, string>(new NodeId<int> { Id = 2 }, (DistributedAlgorithm<int, string>)Activator.CreateInstance(algorithm.GetType()), (NetworkComponent<int, string>)Activator.CreateInstance(component.GetType()));
            tempNode1.Neighbors.Add(new NodeId<int> { Id = 1 });
            tempNode1.Neighbors.Add(new NodeId<int> { Id = 3 });
            tempNode1.OnNodeMessage += function;
            result.Add(tempNode1);

            var tempNode2 = new Node<int, string>(new NodeId<int> { Id = 3 }, (DistributedAlgorithm<int, string>)Activator.CreateInstance(algorithm.GetType()), (NetworkComponent<int, string>)Activator.CreateInstance(component.GetType()));
            tempNode2.Neighbors.Add(new NodeId<int> { Id = 2 });
            tempNode2.Neighbors.Add(new NodeId<int> { Id = 1 });
            tempNode2.Neighbors.Add(new NodeId<int> { Id = 4 });
            tempNode2.Neighbors.Add(new NodeId<int> { Id = 8 });
            tempNode2.OnNodeMessage += function;
            result.Add(tempNode2);

            var tempNode3 = new Node<int, string>(new NodeId<int> { Id = 4 }, (DistributedAlgorithm<int, string>)Activator.CreateInstance(algorithm.GetType()), (NetworkComponent<int, string>)Activator.CreateInstance(component.GetType()));
            tempNode3.Neighbors.Add(new NodeId<int> { Id = 3 });
            tempNode3.Neighbors.Add(new NodeId<int> { Id = 5 });
            tempNode3.Neighbors.Add(new NodeId<int> { Id = 6 });
            tempNode3.Neighbors.Add(new NodeId<int> { Id = 7 });
            tempNode3.OnNodeMessage += function;
            result.Add(tempNode3);

            var tempNode4 = new Node<int, string>(new NodeId<int> { Id = 5 }, (DistributedAlgorithm<int, string>)Activator.CreateInstance(algorithm.GetType()), (NetworkComponent<int, string>)Activator.CreateInstance(component.GetType()));
            tempNode4.Neighbors.Add(new NodeId<int> { Id = 4 });
            tempNode4.Neighbors.Add(new NodeId<int> { Id = 6 });
            tempNode4.OnNodeMessage += function;
            result.Add(tempNode4);

            var tempNode5 = new Node<int, string>(new NodeId<int> { Id = 6 }, (DistributedAlgorithm<int, string>)Activator.CreateInstance(algorithm.GetType()), (NetworkComponent<int, string>)Activator.CreateInstance(component.GetType()));
            tempNode5.Neighbors.Add(new NodeId<int> { Id = 4 });
            tempNode5.Neighbors.Add(new NodeId<int> { Id = 5 });
            tempNode5.OnNodeMessage += function;
            result.Add(tempNode5);

            var tempNode6 = new Node<int, string>(new NodeId<int> { Id = 7 }, (DistributedAlgorithm<int, string>)Activator.CreateInstance(algorithm.GetType()), (NetworkComponent<int, string>)Activator.CreateInstance(component.GetType()));
            tempNode6.Neighbors.Add(new NodeId<int> { Id = 4 });
            tempNode6.Neighbors.Add(new NodeId<int> { Id = 8 });
            tempNode6.Neighbors.Add(new NodeId<int> { Id = 9 });
            tempNode6.OnNodeMessage += function;
            result.Add(tempNode6);

            var tempNode7 = new Node<int, string>(new NodeId<int> { Id = 8 }, (DistributedAlgorithm<int, string>)Activator.CreateInstance(algorithm.GetType()), (NetworkComponent<int, string>)Activator.CreateInstance(component.GetType()));
            tempNode7.Neighbors.Add(new NodeId<int> { Id = 3 });
            tempNode7.Neighbors.Add(new NodeId<int> { Id = 7 });
            tempNode7.Neighbors.Add(new NodeId<int> { Id = 9 });
            tempNode7.OnNodeMessage += function;
            result.Add(tempNode7);

            var tempNode8 = new Node<int, string>(new NodeId<int> { Id = 9 }, (DistributedAlgorithm<int, string>)Activator.CreateInstance(algorithm.GetType()), (NetworkComponent<int, string>)Activator.CreateInstance(component.GetType()));
            tempNode8.Neighbors.Add(new NodeId<int> { Id = 7 });
            tempNode8.Neighbors.Add(new NodeId<int> { Id = 8 });
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
