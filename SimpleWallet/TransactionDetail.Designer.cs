namespace SimpleWallet
{
    partial class TransactionDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TransactionDetail));
            this.rtbTransaction = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rtbTransaction
            // 
            this.rtbTransaction.Location = new System.Drawing.Point(13, 13);
            this.rtbTransaction.Name = "rtbTransaction";
            this.rtbTransaction.ReadOnly = true;
            this.rtbTransaction.Size = new System.Drawing.Size(568, 280);
            this.rtbTransaction.TabIndex = 0;
            this.rtbTransaction.Text = "";
            // 
            // TransactionDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 305);
            this.Controls.Add(this.rtbTransaction);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "TransactionDetail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "TransactionDetail";
            this.Load += new System.EventHandler(this.TransactionDetail_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbTransaction;
    }
}