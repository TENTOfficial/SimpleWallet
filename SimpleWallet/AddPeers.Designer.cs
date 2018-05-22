namespace SimpleWallet
{
    partial class AddPeers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddPeers));
            this.rtbAddPeers = new System.Windows.Forms.RichTextBox();
            this.btnAddPeers = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rtbAddPeers
            // 
            this.rtbAddPeers.Dock = System.Windows.Forms.DockStyle.Top;
            this.rtbAddPeers.Location = new System.Drawing.Point(0, 0);
            this.rtbAddPeers.Name = "rtbAddPeers";
            this.rtbAddPeers.Size = new System.Drawing.Size(457, 219);
            this.rtbAddPeers.TabIndex = 0;
            this.rtbAddPeers.Text = "";
            // 
            // btnAddPeers
            // 
            this.btnAddPeers.Location = new System.Drawing.Point(195, 225);
            this.btnAddPeers.Name = "btnAddPeers";
            this.btnAddPeers.Size = new System.Drawing.Size(75, 23);
            this.btnAddPeers.TabIndex = 1;
            this.btnAddPeers.Text = "Add";
            this.btnAddPeers.UseVisualStyleBackColor = true;
            this.btnAddPeers.Click += new System.EventHandler(this.btnAddPeers_Click);
            // 
            // AddPeers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 256);
            this.Controls.Add(this.btnAddPeers);
            this.Controls.Add(this.rtbAddPeers);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddPeers";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AddPeers";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbAddPeers;
        private System.Windows.Forms.Button btnAddPeers;
    }
}