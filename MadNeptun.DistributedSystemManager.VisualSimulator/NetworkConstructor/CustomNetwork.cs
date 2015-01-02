using MadNeptun.DistributedSystemManager.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MadNeptun.DistributedSystemManager.VisualSimulator.NetworkConstructor
{
    internal class CustomNetwork : BaseNetwork
    {
        private List<Drawable> _objs;

        private List<KeyValuePair<int, int>> _conns;

        public CustomNetwork(List<Drawable> objects, List<KeyValuePair<int,int>> connections)
        {
            _objs = objects;
            _conns = connections;
        }

        public override string Name()
        {
            return "Custom network";
        }

        public override List<Node> GetNetwork(DistributedAlgorithm algorithm, NetworkComponent component, Node.NodeMessage function)
        {
            List<Core.Objects.Node> result = new List<Core.Objects.Node>();

            foreach(Drawable obj in _objs)
            {
                var node = new Node(new Core.Objects.NodeId() { Id = obj.Id.ToString() }, (DistributedAlgorithm)Activator.CreateInstance(algorithm.GetType()), (NetworkComponent)Activator.CreateInstance(component.GetType()));
                node.Neighbors.AddRange(_conns.Where(d => d.Key == obj.Id).Select(d => new NodeId() { Id = d.Value.ToString() }));
                node.OnNodeMessage += function;
                result.Add(node);
            }

            return result;
        }
    }
}
