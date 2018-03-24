namespace SimpleWallet
{
    partial class Debug
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Debug));
            this.tcDebug = new System.Windows.Forms.TabControl();
            this.tpDebug = new System.Windows.Forms.TabPage();
            this.tpPeers = new System.Windows.Forms.TabPage();
            this.rtbPeers = new System.Windows.Forms.RichTextBox();
            this.tcDebug.SuspendLayout();
            this.tpPeers.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcDebug
            // 
            this.tcDebug.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tcDebug.Controls.Add(this.tpDebug);
            this.tcDebug.Controls.Add(this.tpPeers);
            this.tcDebug.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tcDebug.Location = new System.Drawing.Point(2, 3);
            this.tcDebug.Name = "tcDebug";
            this.tcDebug.SelectedIndex = 0;
            this.tcDebug.Size = new System.Drawing.Size(646, 396);
            this.tcDebug.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tcDebug.TabIndex = 0;
            // 
            // tpDebug
            // 
            this.tpDebug.Location = new System.Drawing.Point(4, 25);
            this.tpDebug.Name = "tpDebug";
            this.tpDebug.Padding = new System.Windows.Forms.Padding(3);
            this.tpDebug.Size = new System.Drawing.Size(638, 367);
            this.tpDebug.TabIndex = 0;
            this.tpDebug.Text = "Debug";
            this.tpDebug.UseVisualStyleBackColor = true;
            // 
            // tpPeers
            // 
            this.tpPeers.BackColor = System.Drawing.SystemColors.ControlDark;
            this.tpPeers.Controls.Add(this.rtbPeers);
            this.tpPeers.Location = new System.Drawing.Point(4, 25);
            this.tpPeers.Name = "tpPeers";
            this.tpPeers.Padding = new System.Windows.Forms.Padding(3);
            this.tpPeers.Size = new System.Drawing.Size(638, 367);
            this.tpPeers.TabIndex = 1;
            this.tpPeers.Text = "Peers";
            // 
            // rtbPeers
            // 
            this.rtbPeers.Location = new System.Drawing.Point(0, 0);
            this.rtbPeers.Name = "rtbPeers";
            this.rtbPeers.Size = new System.Drawing.Size(638, 367);
            this.rtbPeers.TabIndex = 0;
            this.rtbPeers.Text = "";
            // 
            // Debug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 399);
            this.Controls.Add(this.tcDebug);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Debug";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Debug";
            this.Load += new System.EventHandler(this.Debug_Load);
            this.tcDebug.ResumeLayout(false);
            this.tpPeers.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcDebug;
        private System.Windows.Forms.TabPage tpDebug;
        private System.Windows.Forms.TabPage tpPeers;
        private System.Windows.Forms.RichTextBox rtbPeers;
    }
}