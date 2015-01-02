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
    internal partial class AddConnectionForm : Form
    {
        public int Start { get; protected set; }

        public int End { get; protected set; }

        public AddConnectionForm(IEnumerable<Drawable> drawables)
        {
            InitializeComponent();
            cbStartElement.Items.AddRange(drawables.ToArray());
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void btnAddConnection_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Start = ((Drawable)cbStartElement.SelectedItem).Id;
            End = ((Drawable)cbEndElement.SelectedItem).Id;
            this.Close();
        }

        private void cbStartElement_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbEndElement.Enabled = true;
            cbEndElement.Items.Clear();
            cbEndElement.Items.AddRange(cbStartElement.Items.Cast<Drawable>().Where(d => d.Id != ((Drawable)cbStartElement.SelectedItem).Id).ToArray());
            if(cbEndElement.Items.Count > 0)
                cbEndElement.SelectedIndex = 0;
        }

        private void cbEndElement_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnAddConnection.Enabled = true;
        }
    }
}
