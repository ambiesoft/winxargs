namespace winxargs
{
    partial class ConfigDialog
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
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtFilename = new System.Windows.Forms.TextBox();
            this.txtArgumentsBefore = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSuffix = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cmbConfig = new System.Windows.Forms.ComboBox();
            this.btnBrowseFile = new System.Windows.Forms.Button();
            this.txtArgumentsAfter = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(24, 62);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(237, 19);
            this.txtName.TabIndex = 2;
            // 
            // txtFilename
            // 
            this.txtFilename.Location = new System.Drawing.Point(24, 98);
            this.txtFilename.Name = "txtFilename";
            this.txtFilename.Size = new System.Drawing.Size(237, 19);
            this.txtFilename.TabIndex = 4;
            // 
            // txtArgumentsBefore
            // 
            this.txtArgumentsBefore.Location = new System.Drawing.Point(24, 151);
            this.txtArgumentsBefore.Name = "txtArgumentsBefore";
            this.txtArgumentsBefore.Size = new System.Drawing.Size(237, 19);
            this.txtArgumentsBefore.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "&Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "&Filename:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 137);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "&Arguments before:";
            // 
            // txtPrefix
            // 
            this.txtPrefix.Location = new System.Drawing.Point(24, 200);
            this.txtPrefix.Name = "txtPrefix";
            this.txtPrefix.Size = new System.Drawing.Size(237, 19);
            this.txtPrefix.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 186);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "&Prefix for each item";
            // 
            // txtSuffix
            // 
            this.txtSuffix.Location = new System.Drawing.Point(24, 237);
            this.txtSuffix.Name = "txtSuffix";
            this.txtSuffix.Size = new System.Drawing.Size(237, 19);
            this.txtSuffix.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 222);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "&Suffix for each item";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(324, 320);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(116, 21);
            this.btnOK.TabIndex = 14;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(446, 320);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(116, 21);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // cmbConfig
            // 
            this.cmbConfig.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbConfig.FormattingEnabled = true;
            this.cmbConfig.Location = new System.Drawing.Point(24, 11);
            this.cmbConfig.Name = "cmbConfig";
            this.cmbConfig.Size = new System.Drawing.Size(237, 20);
            this.cmbConfig.TabIndex = 0;
            this.cmbConfig.SelectionChangeCommitted += new System.EventHandler(this.cmbConfig_SelectionChangeCommitted);
            this.cmbConfig.SelectedIndexChanged += new System.EventHandler(this.cmbConfig_SelectedIndexChanged);
            this.cmbConfig.Enter += new System.EventHandler(this.cmbConfig_Enter);
            // 
            // btnBrowseFile
            // 
            this.btnBrowseFile.Location = new System.Drawing.Point(267, 94);
            this.btnBrowseFile.Name = "btnBrowseFile";
            this.btnBrowseFile.Size = new System.Drawing.Size(30, 23);
            this.btnBrowseFile.TabIndex = 5;
            this.btnBrowseFile.Text = "&...";
            this.btnBrowseFile.UseVisualStyleBackColor = true;
            this.btnBrowseFile.Click += new System.EventHandler(this.btnBrowseFile_Click);
            // 
            // txtArgumentsAfter
            // 
            this.txtArgumentsAfter.Location = new System.Drawing.Point(23, 284);
            this.txtArgumentsAfter.Name = "txtArgumentsAfter";
            this.txtArgumentsAfter.Size = new System.Drawing.Size(237, 19);
            this.txtArgumentsAfter.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 269);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "&Arguments after:";
            // 
            // ConfigDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(574, 353);
            this.Controls.Add(this.btnBrowseFile);
            this.Controls.Add(this.cmbConfig);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSuffix);
            this.Controls.Add(this.txtPrefix);
            this.Controls.Add(this.txtArgumentsAfter);
            this.Controls.Add(this.txtArgumentsBefore);
            this.Controls.Add(this.txtFilename);
            this.Controls.Add(this.txtName);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ConfigDialog";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConfigDialog_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtFilename;
        private System.Windows.Forms.TextBox txtArgumentsBefore;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPrefix;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSuffix;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cmbConfig;
        private System.Windows.Forms.Button btnBrowseFile;
        private System.Windows.Forms.TextBox txtArgumentsAfter;
        private System.Windows.Forms.Label label6;
    }
}