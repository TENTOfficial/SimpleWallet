using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleWallet
{
    public partial class NewLabel : Form
    {
        public String name = "";
        public String address = "";
        public bool edit = false;
        public NewLabel(String name = "", String address = "")
        {
            this.name = name;
            this.address = address;
            edit = false;
            InitializeComponent();
        }

        private void NewLabel_Load(object sender, EventArgs e)
        {
            tbName.Text = name;
            tbAddress.Text = address;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            doAction();
        }

        private void tbName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                doAction();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        void doAction()
        {
            edit = true;
            name = tbName.Text;
            address = tbAddress.Text;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
