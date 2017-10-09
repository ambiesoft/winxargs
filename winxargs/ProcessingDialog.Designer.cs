namespace winxargs
{
    partial class ProcessingDialog
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtOut = new System.Windows.Forms.TextBox();
            this.txtErr = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtOut
            // 
            this.txtOut.Location = new System.Drawing.Point(12, 23);
            this.txtOut.Multiline = true;
            this.txtOut.Name = "txtOut";
            this.txtOut.ReadOnly = true;
            this.txtOut.Size = new System.Drawing.Size(532, 105);
            this.txtOut.TabIndex = 0;
            // 
            // txtErr
            // 
            this.txtErr.Location = new System.Drawing.Point(12, 141);
            this.txtErr.Multiline = true;
            this.txtErr.Name = "txtErr";
            this.txtErr.ReadOnly = true;
            this.txtErr.Size = new System.Drawing.Size(532, 105);
            this.txtErr.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(452, 269);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // ProcessingDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 304);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtErr);
            this.Controls.Add(this.txtOut);
            this.Name = "ProcessingDialog";
            this.Text = "ProcessingDialog";
            this.Load += new System.EventHandler(this.ProcessingDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtOut;
        private System.Windows.Forms.TextBox txtErr;
        private System.Windows.Forms.Button btnOK;
    }
}