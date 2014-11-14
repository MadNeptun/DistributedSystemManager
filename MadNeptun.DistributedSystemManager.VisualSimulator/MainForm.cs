using MadNeptun.DistributedSystemManager.Core.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MadNeptun.DistributedSystemManager.VisualSimulator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            SetupNodes();
        }

        void node_OnNodeMessage(object sender, NodeMessageEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate () {
                lbLog.Items.Add(e.Message);
            });

        }

        private void SetupNodes()
        {
            NodesManager.Instance.Nodes.Clear();
            Node node1 = new Node(new NodeId() { Id = "1" }, new Broadcast(), new NetworkSimulator());
            node1.Neighbors.Add(new NodeId() { Id = "2" });
            node1.Neighbors.Add(new NodeId() { Id = "8" });
            node1.OnNodeMessage += node_OnNodeMessage;
            NodesManager.Instance.Nodes.Add(node1);
            Node node2 = new Node(new NodeId() { Id = "2" }, new Broadcast(), new NetworkSimulator());
            node2.Neighbors.Add(new NodeId() { Id = "1" });
            node2.Neighbors.Add(new NodeId() { Id = "3" });
            node2.Neighbors.Add(new NodeId() { Id = "6" });
            node2.OnNodeMessage += node_OnNodeMessage;
            NodesManager.Instance.Nodes.Add(node2);
            Node node3 = new Node(new NodeId() { Id = "3" }, new Broadcast(), new NetworkSimulator());
            node3.Neighbors.Add(new NodeId() { Id = "2" });
            node3.Neighbors.Add(new NodeId() { Id = "4" });
            node3.OnNodeMessage += node_OnNodeMessage;
            NodesManager.Instance.Nodes.Add(node3);
            Node node4 = new Node(new NodeId() { Id = "4" }, new Broadcast(), new NetworkSimulator());
            node4.Neighbors.Add(new NodeId() { Id = "3" });
            node4.Neighbors.Add(new NodeId() { Id = "5" });
            node4.OnNodeMessage += node_OnNodeMessage;
            NodesManager.Instance.Nodes.Add(node4);
            Node node5 = new Node(new NodeId() { Id = "5" }, new Broadcast(), new NetworkSimulator());
            node5.Neighbors.Add(new NodeId() { Id = "4" });
            node5.Neighbors.Add(new NodeId() { Id = "6" });
            node5.OnNodeMessage += node_OnNodeMessage;
            NodesManager.Instance.Nodes.Add(node5);
            Node node6 = new Node(new NodeId() { Id = "6" }, new Broadcast(), new NetworkSimulator());
            node6.Neighbors.Add(new NodeId() { Id = "5" });
            node6.Neighbors.Add(new NodeId() { Id = "7" });
            node6.Neighbors.Add(new NodeId() { Id = "2" });
            node6.OnNodeMessage += node_OnNodeMessage;
            NodesManager.Instance.Nodes.Add(node6);
            Node node7 = new Node(new NodeId() { Id = "7" }, new Broadcast(), new NetworkSimulator());
            node7.Neighbors.Add(new NodeId() { Id = "6" });
            node7.Neighbors.Add(new NodeId() { Id = "8" });
            node7.OnNodeMessage += node_OnNodeMessage;
            NodesManager.Instance.Nodes.Add(node7);
            Node node8 = new Node(new NodeId() { Id = "8" }, new Broadcast(), new NetworkSimulator());
            node8.Neighbors.Add(new NodeId() { Id = "7" });
            node8.Neighbors.Add(new NodeId() { Id = "1" });
            node8.OnNodeMessage += node_OnNodeMessage;
            NodesManager.Instance.Nodes.Add(node8);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetupNodes();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NodesManager.Instance.Nodes[0].ExecuteInit(new MadNeptun.DistributedSystemManager.Core.Objects.Message() { Value = "a" });
        }

        private void button3_Click(object sender, EventArgs e)
        {
            lbLog.Items.Clear();
        }
    }
}
