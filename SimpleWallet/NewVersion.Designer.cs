namespace SimpleWallet
{
    partial class NewVersion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewVersion));
            this.rtbFeatures = new System.Windows.Forms.RichTextBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pbDownloading = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // rtbFeatures
            // 
            this.rtbFeatures.Location = new System.Drawing.Point(13, 12);
            this.rtbFeatures.Name = "rtbFeatures";
            this.rtbFeatures.ReadOnly = true;
            this.rtbFeatures.Size = new System.Drawing.Size(450, 196);
            this.rtbFeatures.TabIndex = 1;
            this.rtbFeatures.Text = "";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(91, 226);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(112, 23);
            this.btnUpdate.TabIndex = 2;
            this.btnUpdate.Text = "Update now";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(271, 226);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(112, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pbDownloading
            // 
            this.pbDownloading.Location = new System.Drawing.Point(13, 210);
            this.pbDownloading.Maximum = 10000;
            this.pbDownloading.Name = "pbDownloading";
            this.pbDownloading.Size = new System.Drawing.Size(450, 10);
            this.pbDownloading.Step = 1;
            this.pbDownloading.TabIndex = 3;
            this.pbDownloading.Visible = false;
            // 
            // NewVersion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 253);
            this.Controls.Add(this.pbDownloading);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.rtbFeatures);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewVersion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New version of SimpleWallet is available";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NewVersion_FormClosing);
            this.Load += new System.EventHandler(this.NewVersion_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbFeatures;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ProgressBar pbDownloading;
    }
}