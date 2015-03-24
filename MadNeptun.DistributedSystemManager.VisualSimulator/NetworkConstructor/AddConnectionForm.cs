using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MadNeptun.DistributedSystemManager.VisualSimulator.NetworkConstructor
{
    internal partial class AddConnectionForm : Form
    {
        public int Start { get; private set; }

        public int End { get; private set; }

        public AddConnectionForm(IEnumerable<Drawable> drawables)
        {
            InitializeComponent();
            cbStartElement.Items.AddRange(drawables.ToArray());
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnAddConnection_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
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
