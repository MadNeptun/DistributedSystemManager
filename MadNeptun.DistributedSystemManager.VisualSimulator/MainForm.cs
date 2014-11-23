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
        BaseNetwork _customNetwork;
        public MainForm()
        {
            InitializeComponent();
            LoadPredefinedAlorithm();
            LoadPredefinedNetworks();
        }

        protected void node_OnNodeMessage(object sender, NodeMessageEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate () {
                lbLog.Items.Add(e.Message);
            });

        }

        protected void LoadPredefinedAlorithm()
        {
            cbAlgorithms.Items.Clear();
            cbAlgorithms.Items.Add(new ExampleAlgorithms.Broadcast());
            cbAlgorithms.SelectedIndex = 0;
            rbFromList.Checked = true;
        }

        protected void LoadPredefinedNetworks()
        {
            cbPredefinedNetworks.Items.Clear();
            cbPredefinedNetworks.Items.Add(new ExampleNetworks.Ring());
            cbPredefinedNetworks.SelectedIndex = 0;    
            rbPredefinedNetwork.Checked = true;
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            BaseNetwork network = rbPredefinedNetwork.Checked ? (BaseNetwork)cbPredefinedNetworks.SelectedItem : (BaseNetwork)_customNetwork;
            DistributedAlgorithm algorithm = rbFromList.Checked ? (DistributedAlgorithm)cbAlgorithms.SelectedItem : (DistributedAlgorithm)cbClassFromDll.SelectedItem;
            NodesManager.Instance.Nodes.Clear();
            NodesManager.Instance.Nodes.AddRange(network.GetNetwork(algorithm, new NetworkSimulator(), node_OnNodeMessage));
            var message = new MadNeptun.DistributedSystemManager.Core.Objects.Message() { Value = "AAA" };
            NodesManager.Instance.PerformInit(NodesManager.Instance.Nodes.First().GetId(), message);
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            lbLog.Items.Clear();
        }

    }
}
