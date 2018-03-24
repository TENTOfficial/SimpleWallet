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
    public partial class SendMany : Form
    {
        String fee = "";
        String from = "";
        List<String> to = new List<String>();
        bool defaultFee = false;
        public bool result = false;
        Api api = Api.Instance;

        public SendMany(String from)
        {
            InitializeComponent();
            this.from = from;
        }

        Dictionary<String, String> sendMultiple(String from, List<String> to, String fee, bool defaultFee)
        {
            return api.sendManyCoin(from, to, fee, defaultFee);
        }

        private void rtbAddress_Enter(object sender, EventArgs e)
        {
            if (rtbAddress.Text == "address1,amount1\naddress2,amount2\naddress3,amount3")
            {
                rtbAddress.Text = "";
                rtbAddress.ForeColor = Color.Black;
            }
        }

        private void rtbAddress_Leave(object sender, EventArgs e)
        {
            if (rtbAddress.Text == "")
            {
                rtbAddress.Text = "address1,amount1\naddress2,amount2\naddress3,amount3";
                rtbAddress.ForeColor = Color.Gray;
            }
        }

        private void cbDefaultFee_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                tbFee.Enabled = false;
            }
            else
            {
                tbFee.Enabled = true;
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            Dictionary<String, String> strDict = new Dictionary<String, String>();
            fee = tbFee.Text;
            to = rtbAddress.Text.Split('\n').ToList();
            defaultFee = cbDefaultFee.Checked;

            ((Button)sender).Enabled = false;
            strDict = Task.Run(() => sendMultiple(from, to, fee, defaultFee)).Result;

            if (Api.checkResult(strDict))
            {
                result = true;
                MessageBox.Show("Success");
                rtbAddress.Text = "";
            }
            else
            {
                result = false;
                MessageBox.Show(Api.getMessage(strDict));
            }
            ((Button)sender).Enabled = true;
            rtbAddress.Focus();
        }
    }
}
