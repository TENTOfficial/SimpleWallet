namespace SimpleWallet
{
    partial class QrCode
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QrCode));
            this.tbQrCode = new System.Windows.Forms.TextBox();
            this.pbQrCode = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbQrCode)).BeginInit();
            this.SuspendLayout();
            // 
            // tbQrCode
            // 
            this.tbQrCode.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tbQrCode.Location = new System.Drawing.Point(0, 303);
            this.tbQrCode.Name = "tbQrCode";
            this.tbQrCode.ReadOnly = true;
            this.tbQrCode.Size = new System.Drawing.Size(328, 20);
            this.tbQrCode.TabIndex = 1;
            // 
            // pbQrCode
            // 
            this.pbQrCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbQrCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbQrCode.Dock = System.Windows.Forms.DockStyle.Top;
            this.pbQrCode.Location = new System.Drawing.Point(0, 0);
            this.pbQrCode.Name = "pbQrCode";
            this.pbQrCode.Size = new System.Drawing.Size(328, 300);
            this.pbQrCode.TabIndex = 0;
            this.pbQrCode.TabStop = false;
            // 
            // QrCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 323);
            this.Controls.Add(this.tbQrCode);
            this.Controls.Add(this.pbQrCode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "QrCode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "QrCode";
            this.Load += new System.EventHandler(this.QrCode_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbQrCode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbQrCode;
        private System.Windows.Forms.TextBox tbQrCode;
    }
}