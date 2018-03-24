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
    public partial class ImportKey : Form
    {
        Api api = Api.Instance;
        public ImportKey()
        {
            InitializeComponent();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Importing process will take alot of time, please be patient");
            Dictionary<String, String> strDict = api.importPrivateKey(tbPrivateKey.Text, tbLabel.Text);
            if (Api.checkResult(strDict))
            {
                MessageBox.Show("Import success");
                this.Close();
            }
            else
            {
                MessageBox.Show(strDict["message"]);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Start.privKey = "";
            this.Close();
        }
    }
}
