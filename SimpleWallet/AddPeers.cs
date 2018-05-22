using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SimpleWallet
{
    public partial class AddPeers : Form
    {
        public bool isAdded = false;
        public AddPeers()
        {
            InitializeComponent();
        }

        private void btnAddPeers_Click(object sender, EventArgs e)
        {
            String text = File.ReadAllText(Types.cfLocation);
            if(!text.Contains(rtbAddPeers.Text))
            {
                File.AppendAllText(Types.cfLocation, "\n" + rtbAddPeers.Text);
                isAdded = true;
                this.Close();
            }
        }
    }
}
