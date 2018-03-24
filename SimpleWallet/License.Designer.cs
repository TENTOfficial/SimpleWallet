namespace SimpleWallet
{
    partial class License
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(License));
            this.rtbLicense = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rtbLicense
            // 
            this.rtbLicense.Location = new System.Drawing.Point(17, 16);
            this.rtbLicense.Margin = new System.Windows.Forms.Padding(4);
            this.rtbLicense.Name = "rtbLicense";
            this.rtbLicense.ReadOnly = true;
            this.rtbLicense.Size = new System.Drawing.Size(571, 334);
            this.rtbLicense.TabIndex = 0;
            this.rtbLicense.Text = "";
            // 
            // License
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 366);
            this.Controls.Add(this.rtbLicense);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "License";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "License";
            this.Load += new System.EventHandler(this.License_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbLicense;
    }
}