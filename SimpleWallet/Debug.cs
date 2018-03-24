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
            }
            else if(type == Types.DebugType.PEERS)
            {
                tcDebug.SelectTab(tpPeers);
                rtbPeers.Text = peers;
            }
        }
    }
}
