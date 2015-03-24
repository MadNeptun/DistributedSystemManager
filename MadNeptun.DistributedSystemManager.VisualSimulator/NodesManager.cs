using System.Collections.Generic;
using System.Linq;
using MadNeptun.DistributedSystemManager.Core.Objects;

namespace MadNeptun.DistributedSystemManager.VisualSimulator
{
    class NodesManager
    {
        private static NodesManager _instance;

        public static NodesManager Instance
        {
            get { return _instance ?? (_instance = new NodesManager()); }
        }

        private NodesManager()
        {
            _nodes = new List<Node<string,string>>();
        }

        private readonly List<Node<string, string>> _nodes;

        public List<Node<string, string>> Nodes { get { return _nodes; } }

        public void PerformInit(NodeId<string> node, Message<string> msg)
        {
            Nodes.First(n => n.GetId().Id == node.Id).ExecuteInit(msg);
        }


    }
}
