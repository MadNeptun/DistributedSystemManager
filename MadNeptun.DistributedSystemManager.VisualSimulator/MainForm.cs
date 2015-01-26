﻿using MadNeptun.DistributedSystemManager.Core.Objects;
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
using System.Collections.ObjectModel;
namespace MadNeptun.DistributedSystemManager.VisualSimulator
{
    public partial class MainForm : Form
    {
        public static ObservableCollection<object> _visitedNodes { get; private set; }

        private bool _movingObject = false;

        private Drawable _currentlyDraggedItem;

        private BaseNetwork _customNetwork;

        public MainForm()
        {
            InitializeComponent();
            _visitedNodes = new ObservableCollection<object>();
            _visitedNodes.CollectionChanged += _visitedNodes_CollectionChanged;
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

        void _visitedNodes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if(e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                bool changeColor = false;
                var p = (KeyValuePair<int,Color>)e.NewItems[e.NewItems.Count - 1];
                if (_visitedNodes == null || _visitedNodes.Count == 0)
                    changeColor = true;
                else
                {
                    changeColor = _visitedNodes.Take(_visitedNodes.Count-1).Cast<KeyValuePair<int, Color>>().ToList().Where(h => h.Key == p.Key).Count() == 0;
                }
                if(changeColor)
                {
                    Drawable d = Objects.FirstOrDefault(o => o.Id == p.Key);
                    if (d != null)
                        d.BgColor = p.Value;   
                }
                RedrawPanel(Objects, Connections);
            }
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
            try
            {
                Objects.ForEach(o => o.BgColor = Color.Orange);
                _visitedNodes = new ObservableCollection<object>();
                _visitedNodes.CollectionChanged += _visitedNodes_CollectionChanged;
                BaseNetwork network = rbPredefinedNetwork.Checked ? (BaseNetwork)cbPredefinedNetworks.SelectedItem : _customNetwork;
                DistributedAlgorithm algorithm = rbFromList.Checked ? (DistributedAlgorithm)cbAlgorithms.SelectedItem : (DistributedAlgorithm)Activator.CreateInstance((Type)((ComboBoxItem)cbClassFromDll.SelectedItem).Value);
                NodesManager.Instance.Nodes.Clear();
                NodesManager.Instance.Nodes.AddRange(network.GetNetwork(algorithm, new NetworkSimulator(), node_OnNodeMessage));
                NodesManager.Instance.Nodes.ForEach(n => ((NetworkSimulator)n.GetNetworkComponent()).CurrentNodeId = n.GetId());
                var messages = txtMessage.Text.Split(';');              
                RedrawPanel(Objects, Connections);
                for (int i = 0; i < chlInitNodes.CheckedItems.Count; i++ )
                {
                    Objects.First(p => p.Id.ToString() == ((Node)chlInitNodes.CheckedItems[i]).GetId().Id).BgColor = Colors[i % 8];
                    var thread = new System.Threading.Thread(ExecuteInit);
                    var message = i < messages.Length ? messages[i] : messages[messages.Length - 1];
                    thread.Start(new List<object>() { i, message, Colors[i % 8] });
                    //thread.Join(1);
                }
                   
                     
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error has occured. More information: " + ex.ToString());
            }
        }

        internal static List<Color> Colors = new List<Color>() { Color.Green, Color.Red, Color.Blue, Color.Yellow, Color.Pink, Color.Olive, Color.Gold, Color.Fuchsia };

        private void ExecuteInit(object args)
        {
            var arguments = (List<object>)args;
            MainForm._visitedNodes.Add(new KeyValuePair<int, Color>(Int32.Parse(((Node)chlInitNodes.CheckedItems[(int)arguments[0]]).GetId().Id), (Color)arguments[2]));
            NodesManager.Instance.PerformInit(((Node)chlInitNodes.CheckedItems[(int)arguments[0]]).GetId(), new MadNeptun.DistributedSystemManager.Core.Objects.Message() { Value = (string)arguments[1] });
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

        internal static List<Drawable> Objects { get; private set; }

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
            if (rbCustomNetwork.Checked)
            {
                LoadCustomNetworkToNodePicker();
                Objects = ObjectsEditCache;
                Connections = ConnectionsEditCache;
                RedrawPanel(Objects, Connections);
            }
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
            var g = displayPanel.CreateGraphics();
            g.DrawLine(p, start, end);

            double stepX = ((double)(start.X - end.X)) / 16;
            double stepY = ((double)(start.Y - end.Y)) / 16;
            
            var tip = new Point((int)(end.X+4*stepX),(int)(end.Y+4*stepY));
            int edgX = 0;
            int edgY = 0;

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
