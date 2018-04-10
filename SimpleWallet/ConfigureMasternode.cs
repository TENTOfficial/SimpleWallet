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
    public partial class ConfigureMasternode : Form
    {
        public Types.ConfigureResult result = Types.ConfigureResult.FAIL;
        Types.Masternode mn;
        bool isNew = false;
        String oldName = "";
        Api api = Api.Instance;
        public ConfigureMasternode(Types.Masternode mn, bool isNew)
        {
            InitializeComponent();
            mn = new Types.Masternode(mn);

            tbName.Text = oldName = mn.alias;
            tbIP.Text = mn.ipAddress;
            tbPrivKey.Text = mn.privKey;
            tbOutPuts.Text = mn.txHash + " " + mn.index;
            this.isNew = isNew;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void keyUp_Action(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                configure();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        void configure(bool enable = true)
        {
            if (String.IsNullOrEmpty(tbName.Text))
            {
                MessageBox.Show("Alias name could not be empty");
                return;
            }
            if (String.IsNullOrEmpty(tbIP.Text))
            {
                MessageBox.Show("IP address could not be empty");
                return;
            }
            if (String.IsNullOrEmpty(tbPrivKey.Text))
            {
                MessageBox.Show("Private key could not be empty");
                return;
            }
            if (String.IsNullOrEmpty(tbOutPuts.Text))
            {
                MessageBox.Show("Outputs could not be empty");
                return;
            }

            List<String> split = tbOutPuts.Text.Split(' ').ToList();
            split.RemoveAll(String.IsNullOrEmpty);
            if (split.Count < 2)
            {
                MessageBox.Show("Outputs is wrong");
                return;
            }

            result = api.editComfigureFile("ENABLE",tbName.Text, tbIP.Text, tbPrivKey.Text, split[0], split[1], oldName, isNew);

            if (result == Types.ConfigureResult.FAIL)
            {
                MessageBox.Show("Configure error, please check the parameters and the config files"); //this should not happen
            }
            else if (result == Types.ConfigureResult.OK)
            {
                MessageBox.Show("Configure success");
                this.Close();
            }
            else if (result == Types.ConfigureResult.REINDEX)
            {
                MessageBox.Show("Configure success. However your data need reindex to work with masternode");
                this.Close();
            }
            else if (result == Types.ConfigureResult.DUPLICATE)
            {
                MessageBox.Show("Configure error, duplicate alias name");
            }
        }

        private void btnConfigure_Click(object sender, EventArgs e)
        {
            configure();
        }
    }
}
