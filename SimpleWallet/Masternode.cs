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
    public partial class Masternode : Form
    {
        public Masternode(String text, String data)
        {
            InitializeComponent();
            lbText.Text = text;
            tbText.Text = data;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(tbText.Text);
            MessageBox.Show("Copied");
        }
    }
}
