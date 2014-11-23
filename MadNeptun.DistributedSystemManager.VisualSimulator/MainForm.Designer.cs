namespace MadNeptun.DistributedSystemManager.VisualSimulator
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.panel2 = new System.Windows.Forms.Panel();
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
            this.panel6.Controls.Add(this.btnClearLog);
            this.panel6.Controls.Add(this.btnRun);
            this.panel6.Location = new System.Drawing.Point(3, 140);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(308, 109);
            this.panel6.TabIndex = 9;
            // 
            // btnClearLog
            // 
            this.btnClearLog.Location = new System.Drawing.Point(4, 32);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(75, 23);
            this.btnClearLog.TabIndex = 5;
            this.btnClearLog.Text = "Clear log";
            this.btnClearLog.UseVisualStyleBackColor = true;
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(4, 3);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(75, 23);
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
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lbLog);
            this.panel3.Location = new System.Drawing.Point(3, 251);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(308, 266);
            this.panel3.TabIndex = 6;
            // 
            // lbLog
            // 
            this.lbLog.FormattingEnabled = true;
            this.lbLog.Location = new System.Drawing.Point(0, 4);
            this.lbLog.Name = "lbLog";
            this.lbLog.ScrollAlwaysVisible = true;
            this.lbLog.Size = new System.Drawing.Size(304, 251);
            this.lbLog.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Location = new System.Drawing.Point(336, 13);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(462, 519);
            this.panel2.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 544);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(826, 582);
            this.Name = "MainForm";
            this.Text = "Distributed System Simulator";
            this.panel1.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cbClassFromDll;
        private System.Windows.Forms.ComboBox cbAlgorithms;
        private System.Windows.Forms.Button btnPickDll;
        private System.Windows.Forms.RadioButton rbFromList;
        private System.Windows.Forms.RadioButton rbFromFile;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ListBox lbLog;
        private System.Windows.Forms.TextBox txtCustomNetworStatus;
        private System.Windows.Forms.ComboBox cbPredefinedNetworks;
        private System.Windows.Forms.RadioButton rbCustomNetwork;
        private System.Windows.Forms.RadioButton rbPredefinedNetwork;
        private System.Windows.Forms.Button btnEditCustomNetwork;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button btnClearLog;
        private System.Windows.Forms.Button btnRun;
    }
}

