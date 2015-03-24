using System.ComponentModel;
using System.Windows.Forms;

namespace MadNeptun.DistributedSystemManager.VisualSimulator
{
    partial class MainForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.cbClassFromDll = new System.Windows.Forms.ComboBox();
            this.rbFromFile = new System.Windows.Forms.RadioButton();
            this.btnPickDll = new System.Windows.Forms.Button();
            this.cbAlgorithms = new System.Windows.Forms.ComboBox();
            this.rbFromList = new System.Windows.Forms.RadioButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtCustomNetworStatus = new System.Windows.Forms.TextBox();
            this.cbPredefinedNetworks = new System.Windows.Forms.ComboBox();
            this.rbCustomNetwork = new System.Windows.Forms.RadioButton();
            this.rbPredefinedNetwork = new System.Windows.Forms.RadioButton();
            this.btnEditCustomNetwork = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lbLog = new System.Windows.Forms.ListBox();
            this.displayPanel = new System.Windows.Forms.Panel();
            this.chlInitNodes = new System.Windows.Forms.CheckedListBox();
            this.panel1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(317, 520);
            this.panel1.TabIndex = 0;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.chlInitNodes);
            this.panel6.Controls.Add(this.label2);
            this.panel6.Controls.Add(this.label1);
            this.panel6.Controls.Add(this.txtMessage);
            this.panel6.Controls.Add(this.btnClearLog);
            this.panel6.Controls.Add(this.btnRun);
            this.panel6.Location = new System.Drawing.Point(3, 140);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(308, 159);
            this.panel6.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Init message:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(119, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Init node:";
            // 
            // txtMessage
            // 
            this.txtMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtMessage.Location = new System.Drawing.Point(6, 76);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(164, 73);
            this.txtMessage.TabIndex = 7;
            this.txtMessage.Text = "1";
            // 
            // btnClearLog
            // 
            this.btnClearLog.Location = new System.Drawing.Point(4, 32);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(109, 23);
            this.btnClearLog.TabIndex = 5;
            this.btnClearLog.Text = "Clear log";
            this.btnClearLog.UseVisualStyleBackColor = true;
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(4, 5);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(109, 23);
            this.btnRun.TabIndex = 4;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.cbClassFromDll);
            this.panel5.Controls.Add(this.rbFromFile);
            this.panel5.Controls.Add(this.btnPickDll);
            this.panel5.Controls.Add(this.cbAlgorithms);
            this.panel5.Controls.Add(this.rbFromList);
            this.panel5.Location = new System.Drawing.Point(3, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(308, 62);
            this.panel5.TabIndex = 8;
            // 
            // cbClassFromDll
            // 
            this.cbClassFromDll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbClassFromDll.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbClassFromDll.FormattingEnabled = true;
            this.cbClassFromDll.Location = new System.Drawing.Point(23, 3);
            this.cbClassFromDll.Name = "cbClassFromDll";
            this.cbClassFromDll.Size = new System.Drawing.Size(201, 21);
            this.cbClassFromDll.TabIndex = 5;
            // 
            // rbFromFile
            // 
            this.rbFromFile.AutoSize = true;
            this.rbFromFile.Location = new System.Drawing.Point(3, 6);
            this.rbFromFile.Name = "rbFromFile";
            this.rbFromFile.Size = new System.Drawing.Size(14, 13);
            this.rbFromFile.TabIndex = 0;
            this.rbFromFile.TabStop = true;
            this.rbFromFile.UseVisualStyleBackColor = true;
            // 
            // btnPickDll
            // 
            this.btnPickDll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPickDll.Location = new System.Drawing.Point(230, 3);
            this.btnPickDll.Name = "btnPickDll";
            this.btnPickDll.Size = new System.Drawing.Size(75, 23);
            this.btnPickDll.TabIndex = 3;
            this.btnPickDll.Text = "Select Dll";
            this.btnPickDll.UseVisualStyleBackColor = true;
            this.btnPickDll.Click += new System.EventHandler(this.btnPickDll_Click);
            // 
            // cbAlgorithms
            // 
            this.cbAlgorithms.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbAlgorithms.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAlgorithms.FormattingEnabled = true;
            this.cbAlgorithms.Location = new System.Drawing.Point(23, 30);
            this.cbAlgorithms.Name = "cbAlgorithms";
            this.cbAlgorithms.Size = new System.Drawing.Size(281, 21);
            this.cbAlgorithms.TabIndex = 4;
            // 
            // rbFromList
            // 
            this.rbFromList.AutoSize = true;
            this.rbFromList.Location = new System.Drawing.Point(3, 33);
            this.rbFromList.Name = "rbFromList";
            this.rbFromList.Size = new System.Drawing.Size(14, 13);
            this.rbFromList.TabIndex = 1;
            this.rbFromList.TabStop = true;
            this.rbFromList.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.txtCustomNetworStatus);
            this.panel4.Controls.Add(this.cbPredefinedNetworks);
            this.panel4.Controls.Add(this.rbCustomNetwork);
            this.panel4.Controls.Add(this.rbPredefinedNetwork);
            this.panel4.Controls.Add(this.btnEditCustomNetwork);
            this.panel4.Location = new System.Drawing.Point(3, 71);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(308, 63);
            this.panel4.TabIndex = 7;
            // 
            // txtCustomNetworStatus
            // 
            this.txtCustomNetworStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCustomNetworStatus.Location = new System.Drawing.Point(24, 5);
            this.txtCustomNetworStatus.Name = "txtCustomNetworStatus";
            this.txtCustomNetworStatus.ReadOnly = true;
            this.txtCustomNetworStatus.Size = new System.Drawing.Size(200, 20);
            this.txtCustomNetworStatus.TabIndex = 7;
            // 
            // cbPredefinedNetworks
            // 
            this.cbPredefinedNetworks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPredefinedNetworks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPredefinedNetworks.FormattingEnabled = true;
            this.cbPredefinedNetworks.Location = new System.Drawing.Point(23, 31);
            this.cbPredefinedNetworks.Name = "cbPredefinedNetworks";
            this.cbPredefinedNetworks.Size = new System.Drawing.Size(281, 21);
            this.cbPredefinedNetworks.TabIndex = 6;
            this.cbPredefinedNetworks.SelectedIndexChanged += new System.EventHandler(this.cbPredefinedNetworks_SelectedIndexChanged);
            // 
            // rbCustomNetwork
            // 
            this.rbCustomNetwork.AutoSize = true;
            this.rbCustomNetwork.Location = new System.Drawing.Point(4, 8);
            this.rbCustomNetwork.Name = "rbCustomNetwork";
            this.rbCustomNetwork.Size = new System.Drawing.Size(14, 13);
            this.rbCustomNetwork.TabIndex = 5;
            this.rbCustomNetwork.TabStop = true;
            this.rbCustomNetwork.UseVisualStyleBackColor = true;
            this.rbCustomNetwork.CheckedChanged += new System.EventHandler(this.rbCustomNetwork_CheckedChanged);
            // 
            // rbPredefinedNetwork
            // 
            this.rbPredefinedNetwork.AutoSize = true;
            this.rbPredefinedNetwork.Location = new System.Drawing.Point(4, 34);
            this.rbPredefinedNetwork.Name = "rbPredefinedNetwork";
            this.rbPredefinedNetwork.Size = new System.Drawing.Size(14, 13);
            this.rbPredefinedNetwork.TabIndex = 4;
            this.rbPredefinedNetwork.TabStop = true;
            this.rbPredefinedNetwork.UseVisualStyleBackColor = true;
            this.rbPredefinedNetwork.CheckedChanged += new System.EventHandler(this.rbPredefinedNetwork_CheckedChanged);
            // 
            // btnEditCustomNetwork
            // 
            this.btnEditCustomNetwork.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditCustomNetwork.Location = new System.Drawing.Point(229, 3);
            this.btnEditCustomNetwork.Name = "btnEditCustomNetwork";
            this.btnEditCustomNetwork.Size = new System.Drawing.Size(75, 23);
            this.btnEditCustomNetwork.TabIndex = 3;
            this.btnEditCustomNetwork.Text = "Edit";
            this.btnEditCustomNetwork.UseVisualStyleBackColor = true;
            this.btnEditCustomNetwork.Click += new System.EventHandler(this.btnEditCustomNetwork_Click);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.lbLog);
            this.panel3.Location = new System.Drawing.Point(3, 305);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(308, 212);
            this.panel3.TabIndex = 6;
            // 
            // lbLog
            // 
            this.lbLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbLog.FormattingEnabled = true;
            this.lbLog.Location = new System.Drawing.Point(0, 3);
            this.lbLog.Name = "lbLog";
            this.lbLog.ScrollAlwaysVisible = true;
            this.lbLog.Size = new System.Drawing.Size(304, 199);
            this.lbLog.TabIndex = 0;
            // 
            // displayPanel
            // 
            this.displayPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.displayPanel.Location = new System.Drawing.Point(336, 13);
            this.displayPanel.Name = "displayPanel";
            this.displayPanel.Size = new System.Drawing.Size(462, 519);
            this.displayPanel.TabIndex = 1;
            this.displayPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.displayPanel_Paint);
            this.displayPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.displayPanel_MouseDown);
            this.displayPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.displayPanel_MouseMove);
            this.displayPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.displayPanel_MouseUp);
            // 
            // chlInitNodes
            // 
            this.chlInitNodes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chlInitNodes.FormattingEnabled = true;
            this.chlInitNodes.Location = new System.Drawing.Point(176, 10);
            this.chlInitNodes.Name = "chlInitNodes";
            this.chlInitNodes.Size = new System.Drawing.Size(120, 139);
            this.chlInitNodes.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 544);
            this.Controls.Add(this.displayPanel);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(826, 582);
            this.Name = "MainForm";
            this.Text = "Distributed System Simulator";
            this.panel1.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private Panel displayPanel;
        private ComboBox cbClassFromDll;
        private ComboBox cbAlgorithms;
        private Button btnPickDll;
        private RadioButton rbFromList;
        private RadioButton rbFromFile;
        private Panel panel5;
        private Panel panel4;
        private Panel panel3;
        private ListBox lbLog;
        private TextBox txtCustomNetworStatus;
        private ComboBox cbPredefinedNetworks;
        private RadioButton rbCustomNetwork;
        private RadioButton rbPredefinedNetwork;
        private Button btnEditCustomNetwork;
        private Panel panel6;
        private Button btnClearLog;
        private Button btnRun;
        private TextBox txtMessage;
        private Label label2;
        private Label label1;
        private CheckedListBox chlInitNodes;
    }
}

