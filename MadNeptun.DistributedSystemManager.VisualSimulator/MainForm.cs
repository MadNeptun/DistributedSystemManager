using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using MadNeptun.DistributedSystemManager.Core.AbstractEntities;
using MadNeptun.DistributedSystemManager.Core.Objects;
using MadNeptun.DistributedSystemManager.VisualSimulator.ExampleAlgorithms;
using MadNeptun.DistributedSystemManager.VisualSimulator.NetworkConstructor;
namespace MadNeptun.DistributedSystemManager.VisualSimulator
{
    public partial class MainForm : Form
    {
        public static ObservableCollection<object> VisitedNodes { get; private set; }

        private bool _movingObject;

        private Drawable _currentlyDraggedItem;

        private BaseNetwork _customNetwork;

        public MainForm()
        {
            InitializeComponent();
            VisitedNodes = new ObservableCollection<object>();
            VisitedNodes.CollectionChanged += _visitedNodes_CollectionChanged;
            chlInitNodes.ThreeDCheckBoxes = false;
            chlInitNodes.CheckOnClick = true;
            LoadPredefinedAlgorithm();
            LoadPredefinedNetworks();       
            Objects = new List<Drawable>();
            Connections = new List<KeyValuePair<int, int>>();
            ObjectsEditCache = new List<Drawable>();
            ConnectionsEditCache = new List<KeyValuePair<int, int>>();
            LoadDrawableStructureForPredefinedNetworks();
            RedrawPanel(Objects, Connections);
        }

        void _visitedNodes_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action != NotifyCollectionChangedAction.Add) return;
            bool changeColor;
            var p = (KeyValuePair<int,Color>)e.NewItems[e.NewItems.Count - 1];
            lock (VisitedNodes)
            {
                changeColor = true;
                for (var i = 0; i < VisitedNodes.Count - 1;i++ )
                {
                    if (VisitedNodes[i] == null) continue;
                    if(((KeyValuePair<int, Color>)VisitedNodes[i]).Key == p.Key)
                        changeColor = false;
                }
            }
            if(changeColor)
            {
                var d = Objects.FirstOrDefault(o => o.Id == p.Key);
                if (d != null)
                    d.BgColor = p.Value;   
            }
            RedrawPanel(Objects, Connections);
        }

        private void node_OnNodeMessage(object sender, NodeMessageEventArgs<int,string> e)
        {
            Invoke((MethodInvoker)delegate {
                lbLog.Items.Add(e.Sender.Id.ToString()+" > "+e.Reciever.Id.ToString()+" : "+e.Message);
            });

        }

        private void LoadPredefinedAlgorithm()
        {
            cbAlgorithms.Items.Clear();

            var algorithclasses = Assembly.GetCallingAssembly().GetTypes().Where(t => t.IsClass && t.BaseType != null && t.BaseType == typeof(DistributedAlgorithm<int, string>)).ToList();

            foreach (var type in algorithclasses)
            {
                cbAlgorithms.Items.Add(Activator.CreateInstance(type));
            }

            cbAlgorithms.SelectedIndex = 0;
            rbFromList.Checked = true;
        }

        private void LoadPredefinedNetworks()
        {
            cbPredefinedNetworks.Items.Clear();

            var networkclasses = Assembly.GetCallingAssembly().GetTypes().Where(t => t.IsClass && t.BaseType != null && t.BaseType == typeof(BaseNetwork) && t != typeof(CustomNetwork)).ToList();
            
            foreach(Type type in networkclasses)
            {
                cbPredefinedNetworks.Items.Add(Activator.CreateInstance(type));
            }

            cbPredefinedNetworks.SelectedIndex = 0;    
            rbPredefinedNetwork.Checked = true;
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            try
            {
                Objects.ForEach(o => o.BgColor = Color.Orange);
                VisitedNodes = new ObservableCollection<object>();
                VisitedNodes.CollectionChanged += _visitedNodes_CollectionChanged;
                BaseNetwork network = rbPredefinedNetwork.Checked ? (BaseNetwork)cbPredefinedNetworks.SelectedItem : _customNetwork;
                DistributedAlgorithm<int, string> algorithm = rbFromList.Checked ? (DistributedAlgorithm<int, string>)cbAlgorithms.SelectedItem : (DistributedAlgorithm<int, string>)Activator.CreateInstance((Type)((ComboBoxItem)cbClassFromDll.SelectedItem).Value);
                NodesManager.Instance.Nodes.Clear();
                NodesManager.Instance.Nodes.AddRange(network.GetNetwork(algorithm, new NetworkSimulator(), node_OnNodeMessage));
                NodesManager.Instance.Nodes.ForEach(n => ((NetworkSimulator)n.GetNetworkComponent()).CurrentNodeId = n.GetId());
                var messages = txtMessage.Text.Split(';');              
                RedrawPanel(Objects, Connections);
                for (int i = 0; i < chlInitNodes.CheckedItems.Count; i++ )
                {
                    Objects.First(p => p.Id == ((Node<int, string>)chlInitNodes.CheckedItems[i]).GetId().Id).BgColor = Colors[i % 8];
                    var thread = new Thread(ExecuteInit);
                    var message = i < messages.Length ? messages[i] : messages[messages.Length - 1];
                    thread.Start(new List<object>() { i, message, Colors[i % 8] });
                }
                   
                     
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error has occured. More information: " + ex.ToString());
            }
        }

        internal static readonly List<Color> Colors = new List<Color>() { Color.Green, Color.Red, Color.Blue, Color.Yellow, Color.Pink, Color.Olive, Color.Gold, Color.Fuchsia };

        private void ExecuteInit(object args)
        {
            var arguments = (List<object>)args;
            VisitedNodes.Add(new KeyValuePair<int, Color>(((Node<int, string>)chlInitNodes.CheckedItems[(int)arguments[0]]).GetId().Id, (Color)arguments[2]));
            NodesManager.Instance.PerformInit(((Node<int, string>)chlInitNodes.CheckedItems[(int)arguments[0]]).GetId(), new Message<string>() { Value = (string)arguments[1] });
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            lbLog.Items.Clear();
            Objects.ForEach(o => o.BgColor = Color.Orange);
            RedrawPanel(Objects, Connections);
        }

        private void btnPickDll_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Select assembly file";
                IEnumerable<Type> validTypes = new List<Type>();
                if(dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var assembly = Assembly.LoadFrom(dialog.FileName);
                        validTypes = assembly.GetTypes();
                        validTypes = validTypes.Where(c => c.BaseType == typeof(DistributedAlgorithm<int,string>));
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("File you selected is corrupted or incorrect: "+ex.Message);
                    }
                    if (!validTypes.Any())
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

        internal static List<Drawable> Objects { get; private set; }

        private List<KeyValuePair<int, int>> Connections { get; set; }

        private List<Drawable> ObjectsEditCache { get; set; }

        private List<KeyValuePair<int, int>> ConnectionsEditCache { get; set; }

        private void btnEditCustomNetwork_Click(object sender, EventArgs e)
        {
            using (var dialog = new NetworkConstructorForm(ObjectsEditCache, ConnectionsEditCache))
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;
                ObjectsEditCache = Objects = dialog.Objects;
                ConnectionsEditCache = Connections = dialog.Connections;
                _customNetwork = new CustomNetwork(Objects, Connections);
                txtCustomNetworStatus.Text = String.Format("Nodes: {0} Edges: {1}", Objects.Count, Connections.Count);
                rbCustomNetwork.Checked = true;
                LoadCustomNetworkToNodePicker();
                RedrawPanel(Objects, Connections);
            }
        }

        private void rbPredefinedNetwork_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPredefinedNetwork.Checked)
            {
                chlInitNodes.Items.Clear();
                chlInitNodes.Items.AddRange(ListOfNodesFromNetwork((BaseNetwork)cbPredefinedNetworks.SelectedItem).ToArray());
                chlInitNodes.SetItemChecked(0, true);
                btnRun.Enabled = true;
                LoadDrawableStructureForPredefinedNetworks();
                RedrawPanel(Objects, Connections);
            }
        }

        private void rbCustomNetwork_CheckedChanged(object sender, EventArgs e)
        {
            if (!rbCustomNetwork.Checked) return;
            LoadCustomNetworkToNodePicker();
            Objects = ObjectsEditCache;
            Connections = ConnectionsEditCache;
            RedrawPanel(Objects, Connections);
        }

        private void LoadCustomNetworkToNodePicker()
        {
            chlInitNodes.Items.Clear();
            if (_customNetwork != default(BaseNetwork))
            {
                chlInitNodes.Items.AddRange(ListOfNodesFromNetwork(_customNetwork).ToArray());
                if (chlInitNodes.Items.Count > 0)
                {
                    chlInitNodes.SetItemChecked(0,true);
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
        private List<Node<int,string>> ListOfNodesFromNetwork(BaseNetwork network)
        {
            return network.GetNetwork(new Broadcast(), new NetworkSimulator(), node_OnNodeMessage);
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
            var g = displayPanel.CreateGraphics();
            g.DrawLine(p, start, end);

            var stepX = ((double)(start.X - end.X)) / 16;
            var stepY = ((double)(start.Y - end.Y)) / 16;
            
            var tip = new Point((int)(end.X+4*stepX),(int)(end.Y+4*stepY));
            var edgX = 0;
            var edgY = 0;

            if (start.X < end.X)
                edgX = -3;
            else if (start.X > end.X)
                edgX = 3;

            if (start.Y < end.Y)
                edgY = -3;
            else if (start.Y > end.Y)
                edgY = 3;

            g.FillRectangle(p.Brush, tip.X + edgX, tip.Y + edgY, 6, 6);
        }

        private KeyValuePair<List<Drawable>, List<KeyValuePair<int, int>>> GetDrawableStructureFromNodes(List<Node<int, string>> nodes)
        {
            var objects = new List<Drawable>();
            var connections = new List<KeyValuePair<int, int>>();
            var r = new Random();
            
            for(var i = 1; i < nodes.Count + 1; i++)
            {
                var drawableNode = new DrawableNode(new Point(r.Next(60, displayPanel.Width-60), r.Next(60, displayPanel.Height-60)), i);
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
            if (!_movingObject) return;
            _currentlyDraggedItem = default(Drawable);
            _movingObject = false;
        }

        private void displayPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_movingObject) return;
            _currentlyDraggedItem.CenterPoint = e.Location;
            RedrawPanel(Objects,Connections);
        }

        private void displayPanel_Paint(object sender, PaintEventArgs e)
        {
            RedrawPanel(Objects, Connections);
        }
    }
}
