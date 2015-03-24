using System.ComponentModel;
using System.Windows.Forms;

namespace MadNeptun.DistributedSystemManager.VisualSimulator.NetworkConstructor
{
    partial class NetworkConstructorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbNodes = new System.Windows.Forms.ListBox();
            this.lbConnections = new System.Windows.Forms.ListBox();
            this.btnAddDrawable = new System.Windows.Forms.Button();
            this.btnRemoveDrawable = new System.Windows.Forms.Button();
            this.btnAddConnection = new System.Windows.Forms.Button();
            this.btnRemoveConnection = new System.Windows.Forms.Button();
            this.drawingPanel = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnSaveNetwork = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbNodes
            // 
            this.lbNodes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbNodes.FormattingEnabled = true;
            this.lbNodes.Location = new System.Drawing.Point(3, 3);
            this.lbNodes.Name = "lbNodes";
            this.lbNodes.Size = new System.Drawing.Size(204, 173);
            this.lbNodes.TabIndex = 0;
            // 
            // lbConnections
            // 
            this.lbConnections.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbConnections.FormattingEnabled = true;
            this.lbConnections.Location = new System.Drawing.Point(3, 3);
            this.lbConnections.Name = "lbConnections";
            this.lbConnections.Size = new System.Drawing.Size(204, 277);
            this.lbConnections.TabIndex = 1;
            // 
            // btnAddDrawable
            // 
            this.btnAddDrawable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddDrawable.Location = new System.Drawing.Point(3, 185);
            this.btnAddDrawable.Name = "btnAddDrawable";
            this.btnAddDrawable.Size = new System.Drawing.Size(102, 23);
            this.btnAddDrawable.TabIndex = 2;
            this.btnAddDrawable.Text = "Add Node";
            this.btnAddDrawable.UseVisualStyleBackColor = true;
            this.btnAddDrawable.Click += new System.EventHandler(this.btnAddDrawable_Click);
            // 
            // btnRemoveDrawable
            // 
            this.btnRemoveDrawable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveDrawable.Location = new System.Drawing.Point(105, 185);
            this.btnRemoveDrawable.Name = "btnRemoveDrawable";
            this.btnRemoveDrawable.Size = new System.Drawing.Size(102, 23);
            this.btnRemoveDrawable.TabIndex = 3;
            this.btnRemoveDrawable.Text = "Remove";
            this.btnRemoveDrawable.UseVisualStyleBackColor = true;
            this.btnRemoveDrawable.Click += new System.EventHandler(this.btnRemoveDrawable_Click);
            // 
            // btnAddConnection
            // 
            this.btnAddConnection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddConnection.Location = new System.Drawing.Point(3, 292);
            this.btnAddConnection.Name = "btnAddConnection";
            this.btnAddConnection.Size = new System.Drawing.Size(102, 23);
            this.btnAddConnection.TabIndex = 4;
            this.btnAddConnection.Text = "Add Connection";
            this.btnAddConnection.UseVisualStyleBackColor = true;
            this.btnAddConnection.Click += new System.EventHandler(this.btnAddConnection_Click);
            // 
            // btnRemoveConnection
            // 
            this.btnRemoveConnection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveConnection.Location = new System.Drawing.Point(105, 292);
            this.btnRemoveConnection.Name = "btnRemoveConnection";
            this.btnRemoveConnection.Size = new System.Drawing.Size(102, 23);
            this.btnRemoveConnection.TabIndex = 5;
            this.btnRemoveConnection.Text = "Remove";
            this.btnRemoveConnection.UseVisualStyleBackColor = true;
            this.btnRemoveConnection.Click += new System.EventHandler(this.btnRemoveConnection_Click);
            // 
            // drawingPanel
            // 
            this.drawingPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.drawingPanel.Location = new System.Drawing.Point(228, 12);
            this.drawingPanel.Name = "drawingPanel";
            this.drawingPanel.Size = new System.Drawing.Size(510, 579);
            this.drawingPanel.TabIndex = 6;
            this.drawingPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.drawingPanel_Paint);
            this.drawingPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.drawingPanel_MouseDown);
            this.drawingPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.drawingPanel_MouseMove);
            this.drawingPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.drawingPanel_MouseUp);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 12);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lbNodes);
            this.splitContainer1.Panel1.Controls.Add(this.btnRemoveDrawable);
            this.splitContainer1.Panel1.Controls.Add(this.btnAddDrawable);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnRemoveConnection);
            this.splitContainer1.Panel2.Controls.Add(this.lbConnections);
            this.splitContainer1.Panel2.Controls.Add(this.btnAddConnection);
            this.splitContainer1.Size = new System.Drawing.Size(210, 533);
            this.splitContainer1.SplitterDistance = 211;
            this.splitContainer1.TabIndex = 7;
            // 
            // btnSaveNetwork
            // 
            this.btnSaveNetwork.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveNetwork.Location = new System.Drawing.Point(12, 551);
            this.btnSaveNetwork.Name = "btnSaveNetwork";
            this.btnSaveNetwork.Size = new System.Drawing.Size(105, 40);
            this.btnSaveNetwork.TabIndex = 8;
            this.btnSaveNetwork.Text = "Save";
            this.btnSaveNetwork.UseVisualStyleBackColor = true;
            this.btnSaveNetwork.Click += new System.EventHandler(this.btnSaveNetwork_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.Location = new System.Drawing.Point(117, 551);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(105, 40);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // NetworkConstructorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 601);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSaveNetwork);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.drawingPanel);
            this.MinimumSize = new System.Drawing.Size(766, 639);
            this.Name = "NetworkConstructorForm";
            this.Text = "NetworkConstructorForm";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ListBox lbNodes;
        private ListBox lbConnections;
        private Button btnAddDrawable;
        private Button btnRemoveDrawable;
        private Button btnAddConnection;
        private Button btnRemoveConnection;
        private Panel drawingPanel;
        private SplitContainer splitContainer1;
        private Button btnSaveNetwork;
        private Button btnCancel;
    }
}