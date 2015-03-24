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

        public override List<Node<string, string>> GetNetwork(DistributedAlgorithm<string, string> algorithm, NetworkComponent<string, string> component, Node<string, string>.NodeMessage function)
        {
            var result = new List<Node<string, string>>();

            for (var i = 1; i < 8; i++)
            {
                var tempNode = new Node<string, string>(new NodeId<string>() { Id = i.ToString() }, (DistributedAlgorithm<string, string>)Activator.CreateInstance(algorithm.GetType()), (NetworkComponent<string, string>)Activator.CreateInstance(component.GetType()));
                tempNode.Neighbors.Add(new NodeId<string>() { Id = i == 7 ? "1" : (i + 1).ToString() });
                tempNode.Neighbors.Add(new NodeId<string>() { Id = i == 1 ? "7" : (i - 1).ToString() });
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
