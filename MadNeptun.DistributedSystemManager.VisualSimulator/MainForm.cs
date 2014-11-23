using MadNeptun.DistributedSystemManager.Core.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
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
            LoadPredefinedAlgorithm();
            LoadPredefinedNetworks();
        }

        protected void node_OnNodeMessage(object sender, NodeMessageEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate () {
                lbLog.Items.Add(e.Message);
            });

        }

        protected void LoadPredefinedAlgorithm()
        {
            cbAlgorithms.Items.Clear();

            var algorithclasses = Assembly.GetCallingAssembly().GetTypes().Where(t => t.IsClass && t.BaseType != null && t.BaseType == typeof(DistributedAlgorithm)).ToList();

            foreach (Type type in algorithclasses)
            {
                cbAlgorithms.Items.Add(Activator.CreateInstance(type));
            }

            cbAlgorithms.SelectedIndex = 0;
            rbFromList.Checked = true;
        }

        protected void LoadPredefinedNetworks()
        {
            cbPredefinedNetworks.Items.Clear();

            var networkclasses = Assembly.GetCallingAssembly().GetTypes().Where(t => t.IsClass && t.BaseType != null && t.BaseType == typeof(BaseNetwork)).ToList();
            
            foreach(Type type in networkclasses)
            {
                cbPredefinedNetworks.Items.Add(Activator.CreateInstance(type));
            }

            cbPredefinedNetworks.SelectedIndex = 0;    
            rbPredefinedNetwork.Checked = true;
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            BaseNetwork network = rbPredefinedNetwork.Checked ? (BaseNetwork)cbPredefinedNetworks.SelectedItem : (BaseNetwork)_customNetwork;
            DistributedAlgorithm algorithm = rbFromList.Checked ? (DistributedAlgorithm)cbAlgorithms.SelectedItem : (DistributedAlgorithm)cbClassFromDll.SelectedItem;
            NodesManager.Instance.Nodes.Clear();
            NodesManager.Instance.Nodes.AddRange(network.GetNetwork(algorithm, new NetworkSimulator(), node_OnNodeMessage));
            var message = new MadNeptun.DistributedSystemManager.Core.Objects.Message() { Value = "1" };
            NodesManager.Instance.PerformInit(NodesManager.Instance.Nodes.First().GetId(), message);
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            lbLog.Items.Clear();
        }

    }
}
