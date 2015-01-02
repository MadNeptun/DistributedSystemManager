using MadNeptun.DistributedSystemManager.Core.Objects;
using MadNeptun.DistributedSystemManager.VisualSimulator.NetworkConstructor;
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
        private bool _movingObject = false;

        private Drawable _currentlyDraggedItem;

        private BaseNetwork _customNetwork;

        public MainForm()
        {
            InitializeComponent();
            LoadPredefinedAlgorithm();
            LoadPredefinedNetworks();
            Objects = new List<Drawable>();
            Connections = new List<KeyValuePair<int, int>>();
            ObjectsEditCache = new List<Drawable>();
            ConnectionsEditCache = new List<KeyValuePair<int, int>>();
            LoadDrawableStructureForPredefinedNetworks();
            RedrawPanel(Objects, Connections);
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

            var networkclasses = Assembly.GetCallingAssembly().GetTypes().Where(t => t.IsClass && t.BaseType != null && t.BaseType == typeof(BaseNetwork) && t != typeof(NetworkConstructor.CustomNetwork)).ToList();
            
            foreach(Type type in networkclasses)
            {
                cbPredefinedNetworks.Items.Add(Activator.CreateInstance(type));
            }

            cbPredefinedNetworks.SelectedIndex = 0;    
            rbPredefinedNetwork.Checked = true;
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            BaseNetwork network = rbPredefinedNetwork.Checked ? (BaseNetwork)cbPredefinedNetworks.SelectedItem : _customNetwork;
            DistributedAlgorithm algorithm = rbFromList.Checked ? (DistributedAlgorithm)cbAlgorithms.SelectedItem : (DistributedAlgorithm)Activator.CreateInstance((Type)((ComboBoxItem)cbClassFromDll.SelectedItem).Value);
            NodesManager.Instance.Nodes.Clear();
            NodesManager.Instance.Nodes.AddRange(network.GetNetwork(algorithm, new NetworkSimulator(), node_OnNodeMessage));
            var message = new MadNeptun.DistributedSystemManager.Core.Objects.Message() { Value = txtMessage.Text };
            NodesManager.Instance.PerformInit(((Node)cbInitNode.SelectedItem).GetId(), message);
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            lbLog.Items.Clear();
        }

        private void btnPickDll_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Select assembly file";
                IEnumerable<Type> validTypes = new List<Type>();
                if(dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    try
                    {
                        var assembly = Assembly.LoadFrom(dialog.FileName);
                        validTypes = assembly.GetTypes().Where(c => c.BaseType == typeof(MadNeptun.DistributedSystemManager.Core.Objects.DistributedAlgorithm));
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("File you selected is corrupted or incorrect.");
                    }
                    if (validTypes.Count() == 0)
                        MessageBox.Show("There are no valid types in loaded file.");
                    else
                    {
                        cbClassFromDll.Items.Clear();
                        cbClassFromDll.Items.AddRange(validTypes.Select(t => new ComboBoxItem() { Name = t.Name, Value = t }).ToArray());
                        cbClassFromDll.SelectedIndex = 0;
                        rbFromFile.Checked = true;
                    }
                }
            }
        }

        private class ComboBoxItem
        {
            public object Value { get; set; }
            public string Name { get; set; }

            public override string ToString()
            {
                return Name;
            }
        }

        private List<Drawable> Objects { get; set; }

        private List<KeyValuePair<int, int>> Connections { get; set; }

        private List<Drawable> ObjectsEditCache { get; set; }

        private List<KeyValuePair<int, int>> ConnectionsEditCache { get; set; }

        private void btnEditCustomNetwork_Click(object sender, EventArgs e)
        {
            using (NetworkConstructor.NetworkConstructorForm dialog = new NetworkConstructor.NetworkConstructorForm(ObjectsEditCache, ConnectionsEditCache))
            {
                if(dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    ObjectsEditCache = Objects = dialog.Objects;
                    ConnectionsEditCache = Connections = dialog.Connections;
                    _customNetwork = new NetworkConstructor.CustomNetwork(Objects, Connections);
                    txtCustomNetworStatus.Text = String.Format("Nodes: {0} Edges: {1}", Objects.Count, Connections.Count);
                    rbCustomNetwork.Checked = true;
                    LoadCustomNetworkToNodePicker();
                    RedrawPanel(Objects, Connections);
                }
            }
        }

        private void rbPredefinedNetwork_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPredefinedNetwork.Checked)
            {
                cbInitNode.Items.Clear();
                cbInitNode.Items.AddRange(ListOfNodesFromNetwork((BaseNetwork)cbPredefinedNetworks.SelectedItem).ToArray());
                cbInitNode.SelectedIndex = 0;
                btnRun.Enabled = true;
                LoadDrawableStructureForPredefinedNetworks();
                RedrawPanel(Objects, Connections);
            }
        }

        private void rbCustomNetwork_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCustomNetwork.Checked)
            {
                LoadCustomNetworkToNodePicker();
                RedrawPanel(Objects, Connections);
            }
        }

        private void LoadCustomNetworkToNodePicker()
        {
            cbInitNode.Items.Clear();
            if (_customNetwork != default(BaseNetwork))
            {
                cbInitNode.Items.AddRange(ListOfNodesFromNetwork(_customNetwork).ToArray());
                if (cbInitNode.Items.Count > 0)
                {
                    cbInitNode.SelectedIndex = 0;
                    btnRun.Enabled = true;
                }
                else
                    btnRun.Enabled = false;
            }
            else
                btnRun.Enabled = false;
        }

        /// <summary>
        /// Only to get structure, not intended for real use
        /// </summary>
        /// <param name="network"></param>
        /// <returns></returns>
        private List<Node> ListOfNodesFromNetwork(BaseNetwork network)
        {
            return network.GetNetwork(new ExampleAlgorithms.Broadcast(), new NetworkSimulator(), node_OnNodeMessage);
        }

        private void RedrawPanel(List<Drawable> objects, List<KeyValuePair<int,int>> connections)
        {
            var g = displayPanel.CreateGraphics();
            g.FillRectangle(new SolidBrush(Color.White), displayPanel.DisplayRectangle);
            var pen = new Pen(Color.Black, 2);
            connections.ForEach(d => DrawConnection(d.Key, d.Value, pen, objects));
            objects.ForEach(n => n.Draw(displayPanel.CreateGraphics()));
        }

        private void DrawConnection(int s, int e, Pen p, List<Drawable> objects)
        {
            var start = objects.First(d => d.Id == s).CenterPoint;
            var end = objects.First(d => d.Id == e).CenterPoint;
            displayPanel.CreateGraphics().DrawLine(p, start, end);
        }

        private KeyValuePair<List<Drawable>,List<KeyValuePair<int,int>>> GetDrawableStructureFromNodes(List<Node> nodes)
        {
            List<Drawable> objects = new List<Drawable>();
            List<KeyValuePair<int, int>> connections = new List<KeyValuePair<int, int>>();
            Random r = new Random();
            
            for(int i = 1; i < nodes.Count + 1; i++)
            {
                DrawableNode drawableNode = new DrawableNode(new Point(r.Next(60, displayPanel.Width-60), r.Next(60, displayPanel.Height-60)), i);
                objects.Add(drawableNode);
                connections.AddRange(nodes[i-1].Neighbors.Select(n => new KeyValuePair<int, int>(i,nodes.IndexOf(nodes.First(p=>p.GetId().Id == n.Id))+1)).ToArray());
            }
            return new KeyValuePair<List<Drawable>, List<KeyValuePair<int, int>>>(objects, connections);
        }

        private void cbPredefinedNetworks_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDrawableStructureForPredefinedNetworks();
            RedrawPanel(Objects, Connections);
        }

        private void LoadDrawableStructureForPredefinedNetworks()
        {
            var structure = GetDrawableStructureFromNodes(ListOfNodesFromNetwork((BaseNetwork)cbPredefinedNetworks.SelectedItem));
            Objects = structure.Key;
            Connections = structure.Value;
        }

        private void displayPanel_MouseDown(object sender, MouseEventArgs e)
        {
            var start = e.Location;
            _currentlyDraggedItem = Objects.FirstOrDefault(d => d.WasHit(start));
            _movingObject = _currentlyDraggedItem != default(Drawable);
        }

        private void displayPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (_movingObject)
            {
                _currentlyDraggedItem = default(Drawable);
                _movingObject = false;
            }
        }

        private void displayPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (_movingObject)
            {
                _currentlyDraggedItem.CenterPoint = e.Location;
                RedrawPanel(Objects,Connections);
            }
        }

        private void displayPanel_Paint(object sender, PaintEventArgs e)
        {
            RedrawPanel(Objects, Connections);
        }
    }
}
