using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MadNeptun.DistributedSystemManager.VisualSimulator.NetworkConstructor
{
    internal partial class NetworkConstructorForm : Form
    {

        private bool _movingObject = false;

        Drawable _currentlyDraggedItem;

        public List<Drawable> Objects { get; protected set; }

        public List<KeyValuePair<int, int>> Connections { get; protected set; }

        public NetworkConstructorForm(List<Drawable> objects, List<KeyValuePair<int, int>> connections)
        {
            InitializeComponent();
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            lbNodes.Items.Clear();
            lbNodes.Items.AddRange(objects.ToArray());
            lbConnections.Items.Clear();
            lbConnections.Items.AddRange(connections.Cast<object>().ToArray());
        }

        private void drawingPanel_Paint(object sender, PaintEventArgs e)
        {
            RedrawPanel();
        }

        private void drawingPanel_MouseDown(object sender, MouseEventArgs e)
        {
            var start = e.Location;
            _currentlyDraggedItem = lbNodes.Items.Cast<Drawable>().ToList().FirstOrDefault(d => d.WasHit(start));
            _movingObject = _currentlyDraggedItem != default(Drawable);
        }

        private void drawingPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (_movingObject)
            {
                _currentlyDraggedItem = default(Drawable);
                _movingObject = false;
            }
        }

        private void drawingPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if(_movingObject)
            {
                _currentlyDraggedItem.CenterPoint = e.Location;
                RedrawPanel();
            }
        }

        private void RedrawPanel()
        {
            var g = drawingPanel.CreateGraphics();
            g.FillRectangle(new SolidBrush(Color.White), drawingPanel.DisplayRectangle);
            var pen = new Pen(Color.Black, 2);
            lbConnections.Items.Cast<KeyValuePair<int, int>>().ToList().ForEach(d => DrawConnection(d.Key, d.Value, pen));
            lbNodes.Items.Cast<Drawable>().ToList().ForEach(n => n.Draw(drawingPanel.CreateGraphics()));
        }

        private void DrawConnection(int s, int e, Pen p)
        {
            var start = lbNodes.Items.Cast<Drawable>().First(d => d.Id == s).CenterPoint;
            var end = lbNodes.Items.Cast<Drawable>().First(d => d.Id == e).CenterPoint;
            drawingPanel.CreateGraphics().DrawLine(p, start, end);
        }

        private void btnRemoveDrawable_Click(object sender, EventArgs e)
        {
            if (lbNodes.Items.Count > 0 && lbNodes.SelectedItem != null)
            {
                var deleteItem = (Drawable)lbNodes.SelectedItem;    
                var connsToRemove = lbConnections.Items.Cast<KeyValuePair<int, int>>().Where(i => i.Key == deleteItem.Id || i.Value == deleteItem.Id).ToArray();
                foreach(var connToRemove in connsToRemove)
                    lbConnections.Items.Remove(connToRemove);
                lbNodes.Items.RemoveAt(lbNodes.SelectedIndex);  
            }
            RedrawPanel();
        }

        private void btnRemoveConnection_Click(object sender, EventArgs e)
        {
            if (lbConnections.Items.Count > 0 && lbConnections.SelectedItem != null)
            {
                KeyValuePair<int,int> item = (KeyValuePair<int,int>)lbConnections.SelectedItem;
                lbConnections.Items.RemoveAt(lbConnections.SelectedIndex);
            }
            RedrawPanel();
        }

        private void btnAddDrawable_Click(object sender, EventArgs e)
        {
            var node = new DrawableNode(new Point(60, 60));
            lbNodes.Items.Add(node);
            RedrawPanel();
        }

        private void btnAddConnection_Click(object sender, EventArgs e)
        {
            using(AddConnectionForm dialog = new AddConnectionForm(lbNodes.Items.Cast<Drawable>()))
            {
                if(dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    lbConnections.Items.Add(new KeyValuePair<int, int>(dialog.Start,dialog.End));
                }
            }
            RedrawPanel();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSaveNetwork_Click(object sender, EventArgs e)
        {
            Objects = lbNodes.Items.Cast<Drawable>().ToList();
            Connections = lbConnections.Items.Cast<KeyValuePair<int, int>>().ToList();
            DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
