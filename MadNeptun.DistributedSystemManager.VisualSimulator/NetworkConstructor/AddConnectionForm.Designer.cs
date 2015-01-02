namespace MadNeptun.DistributedSystemManager.VisualSimulator.NetworkConstructor
{
    partial class AddConnectionForm
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
            this.btnAddConnection = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbStartElement = new System.Windows.Forms.ComboBox();
            this.cbEndElement = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnAddConnection
            // 
            this.btnAddConnection.Enabled = false;
            this.btnAddConnection.Location = new System.Drawing.Point(60, 66);
            this.btnAddConnection.Name = "btnAddConnection";
            this.btnAddConnection.Size = new System.Drawing.Size(107, 23);
            this.btnAddConnection.TabIndex = 0;
            this.btnAddConnection.Text = "Add Connection";
            this.btnAddConnection.UseVisualStyleBackColor = true;
            this.btnAddConnection.Click += new System.EventHandler(this.btnAddConnection_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(173, 66);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(107, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Start element";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "End element";
            // 
            // cbStartElement
            // 
            this.cbStartElement.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStartElement.FormattingEnabled = true;
            this.cbStartElement.Location = new System.Drawing.Point(91, 12);
            this.cbStartElement.Name = "cbStartElement";
            this.cbStartElement.Size = new System.Drawing.Size(189, 21);
            this.cbStartElement.TabIndex = 4;
            this.cbStartElement.SelectedIndexChanged += new System.EventHandler(this.cbStartElement_SelectedIndexChanged);
            // 
            // cbEndElement
            // 
            this.cbEndElement.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEndElement.Enabled = false;
            this.cbEndElement.FormattingEnabled = true;
            this.cbEndElement.Location = new System.Drawing.Point(91, 39);
            this.cbEndElement.Name = "cbEndElement";
            this.cbEndElement.Size = new System.Drawing.Size(189, 21);
            this.cbEndElement.TabIndex = 5;
            this.cbEndElement.SelectedIndexChanged += new System.EventHandler(this.cbEndElement_SelectedIndexChanged);
            // 
            // AddConnectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 96);
            this.Controls.Add(this.cbEndElement);
            this.Controls.Add(this.cbStartElement);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAddConnection);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(300, 134);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 134);
            this.Name = "AddConnectionForm";
            this.Text = "AddConnectionForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAddConnection;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbStartElement;
        private System.Windows.Forms.ComboBox cbEndElement;
    }
}