namespace SimpleWallet
{
    partial class AddressBook
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddressBook));
            this.dtgAddressBook = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addressBookBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnNewAddressBook = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtgAddressBook)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.addressBookBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dtgAddressBook
            // 
            this.dtgAddressBook.AutoGenerateColumns = false;
            this.dtgAddressBook.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgAddressBook.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.dtgAddressBook.DataSource = this.addressBookBindingSource;
            this.dtgAddressBook.Location = new System.Drawing.Point(13, 13);
            this.dtgAddressBook.Name = "dtgAddressBook";
            this.dtgAddressBook.Size = new System.Drawing.Size(899, 442);
            this.dtgAddressBook.TabIndex = 0;
            this.dtgAddressBook.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dtgAddressBook_MouseClick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "label";
            this.dataGridViewTextBoxColumn1.HeaderText = "label";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "address";
            this.dataGridViewTextBoxColumn2.HeaderText = "address";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // addressBookBindingSource
            // 
            this.addressBookBindingSource.DataSource = typeof(SimpleWallet.Types.AddressBook);
            // 
            // btnNewAddressBook
            // 
            this.btnNewAddressBook.Location = new System.Drawing.Point(732, 462);
            this.btnNewAddressBook.Name = "btnNewAddressBook";
            this.btnNewAddressBook.Size = new System.Drawing.Size(180, 27);
            this.btnNewAddressBook.TabIndex = 12;
            this.btnNewAddressBook.Text = "New address label";
            this.btnNewAddressBook.UseVisualStyleBackColor = true;
            this.btnNewAddressBook.Click += new System.EventHandler(this.btnNewAddressBook_Click);
            // 
            // AddressBook
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 501);
            this.Controls.Add(this.btnNewAddressBook);
            this.Controls.Add(this.dtgAddressBook);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AddressBook";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AddressBook";
            this.Load += new System.EventHandler(this.AddressBook_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgAddressBook)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.addressBookBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dtgAddressBook;
        private System.Windows.Forms.DataGridViewTextBoxColumn labelDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn addressDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.BindingSource addressBookBindingSource;
        private System.Windows.Forms.Button btnNewAddressBook;
    }
}