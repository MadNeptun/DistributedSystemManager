using System;
using System.Collections.Generic;
using System.Linq;
using MadNeptun.DistributedSystemManager.Core.AbstractEntities;
using MadNeptun.DistributedSystemManager.Core.Objects;

namespace MadNeptun.DistributedSystemManager.VisualSimulator.NetworkConstructor
{
    internal class CustomNetwork : BaseNetwork
    {
        private readonly List<Drawable> _objs;

        private readonly List<KeyValuePair<int, int>> _conns;

        public CustomNetwork(List<Drawable> objects, List<KeyValuePair<int,int>> connections)
        {
            _objs = objects;
            _conns = connections;
        }

        public override string Name()
        {
            return "Custom network";
        }

        public override List<Node<int, string>> GetNetwork(DistributedAlgorithm<int, string> algorithm, NetworkComponent<int, string> component, Node<int, string>.NodeMessage function)
        {
            var result = new List<Node<int, string>>();

            foreach(var obj in _objs)
            {
                var node = new Node<int, string>(new NodeId<int>() { Id = obj.Id }, (DistributedAlgorithm<int, string>)Activator.CreateInstance(algorithm.GetType()), (NetworkComponent<int, string>)Activator.CreateInstance(component.GetType()));
                node.Neighbors.AddRange(_conns.Where(d => d.Key == obj.Id).Select(d => new NodeId<int>() { Id = d.Value }));
                node.OnNodeMessage += function;
                result.Add(node);
            }

            return result;
        }
    }
}
