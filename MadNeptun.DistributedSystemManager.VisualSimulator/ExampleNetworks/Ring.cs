using System;
using System.Collections.Generic;
using MadNeptun.DistributedSystemManager.Core.AbstractEntities;
using MadNeptun.DistributedSystemManager.Core.Objects;

namespace MadNeptun.DistributedSystemManager.VisualSimulator.ExampleNetworks
{
    class Ring : BaseNetwork
    {
        public override string Name()
        {
            return "Ring - bidirectional - 7 nodes";
        }

        public override List<Node<int, string>> GetNetwork(DistributedAlgorithm<int, string> algorithm, NetworkComponent<int, string> component, Node<int, string>.NodeMessage function)
        {
            var result = new List<Node<int, string>>();

            for (var i = 1; i < 8; i++)
            {
                var tempNode = new Node<int, string>(new NodeId<int>() { Id = i }, (DistributedAlgorithm<int, string>)Activator.CreateInstance(algorithm.GetType()), (NetworkComponent<int, string>)Activator.CreateInstance(component.GetType()));
                tempNode.Neighbors.Add(new NodeId<int>() { Id = i == 7 ? 1 : (i + 1) });
                tempNode.Neighbors.Add(new NodeId<int>() { Id = i == 1 ? 7 : (i - 1) });
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
