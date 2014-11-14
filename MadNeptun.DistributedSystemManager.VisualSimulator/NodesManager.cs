using MadNeptun.DistributedSystemManager.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadNeptun.DistributedSystemManager.VisualSimulator
{
    class NodesManager
    {
        private static NodesManager _instance;

        public static NodesManager Instance
        {
            get
            {
                if (_instance == null) 
                    _instance = new NodesManager();
                return _instance;
            }
        }

        private NodesManager()
        {
            _nodes = new List<Node>();
        }

        private List<Node> _nodes;

        public List<Node> Nodes { get { return _nodes; } }

        public void PerformInit(NodeId node, Message msg)
        {
            Nodes.First(n => n.GetId().Id == node.Id).ExecuteInit(msg);
        }


    }
}
