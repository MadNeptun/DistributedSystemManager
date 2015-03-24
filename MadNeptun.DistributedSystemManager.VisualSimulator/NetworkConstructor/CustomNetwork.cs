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

        public override List<Node<string, string>> GetNetwork(DistributedAlgorithm<string, string> algorithm, NetworkComponent<string, string> component, Node<string, string>.NodeMessage function)
        {
            var result = new List<Node<string, string>>();

            foreach(var obj in _objs)
            {
                var node = new Node<string, string>(new NodeId<string>() { Id = obj.Id.ToString() }, (DistributedAlgorithm<string, string>)Activator.CreateInstance(algorithm.GetType()), (NetworkComponent<string, string>)Activator.CreateInstance(component.GetType()));
                node.Neighbors.AddRange(_conns.Where(d => d.Key == obj.Id).Select(d => new NodeId<string>() { Id = d.Value.ToString() }));
                node.OnNodeMessage += function;
                result.Add(node);
            }

            return result;
        }
    }
}
