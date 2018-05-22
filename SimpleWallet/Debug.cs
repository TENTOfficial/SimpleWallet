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
    public partial class Debug : Form
    {
        Types.DebugType type;
        Api api = Api.Instance;
        String peers = "";
        public Debug(Types.DebugType type, String peers = "")
        {
            InitializeComponent();
            this.type = type;
            this.peers = peers;
        }

        private void Debug_Load(object sender, EventArgs e)
        {
            if(type == Types.DebugType.DEBUG)
            {
                tcDebug.SelectTab(tpDebug);
                tbCommand.Focus();
            }
            else if(type == Types.DebugType.PEERS)
            {
                tcDebug.SelectTab(tpPeers);
                rtbPeers.Text = peers;
            }
        }

        private void btnAddPeers_Click(object sender, EventArgs e)
        {

        }

        private void tbCommand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                string txt = ((TextBox)sender).Text;
                if(!String.IsNullOrEmpty(txt))
                {
                    String balance = Task.Run(() => api.getDebug(txt)).Result;
                    rtbLog.Text += balance + "\n\n";
                    ((TextBox)sender).Text = "";
                }
            }
            else if (e.KeyData == Keys.Escape)
            {
                this.Close();
            }
        }

        private void rtbLog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
