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
    public partial class TransactionDetail : Form
    {
        String data = "";
        public TransactionDetail(string data)
        {
            InitializeComponent();
            this.data = data;
        }

        private void TransactionDetail_Load(object sender, EventArgs e)
        {
            rtbTransaction.Text = data;
        }
    }
}
