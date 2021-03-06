﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MadNeptun.DistributedSystemManager.VisualSimulator.NetworkConstructor
{
    internal partial class NetworkConstructorForm : Form
    {

        private bool _movingObject;

        Drawable _currentlyDraggedItem;

        public List<Drawable> Objects { get; protected set; }

        public List<KeyValuePair<int, int>> Connections { get; protected set; }

        public NetworkConstructorForm(List<Drawable> objects, List<KeyValuePair<int, int>> connections)
        {
            InitializeComponent();
            DialogResult = DialogResult.Cancel;
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
            var g = drawingPanel.CreateGraphics();
            g.DrawLine(p, start, end);
            double stepX = ((double)(start.X - end.X)) / 16;
            double stepY = ((double)(start.Y - end.Y)) / 16;

            var tip = new Point((int)(end.X + 4 * stepX), (int)(end.Y + 4 * stepY));
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
                if(dialog.ShowDialog() == DialogResult.OK)
                {
                    lbConnections.Items.Add(new KeyValuePair<int, int>(dialog.Start,dialog.End));
                }
            }
            RedrawPanel();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSaveNetwork_Click(object sender, EventArgs e)
        {
            Objects = lbNodes.Items.Cast<Drawable>().ToList();
            Connections = lbConnections.Items.Cast<KeyValuePair<int, int>>().ToList();
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
