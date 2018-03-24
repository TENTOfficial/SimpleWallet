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
    public partial class License : Form
    {
        String license = "";
        public License(String license)
        {
            InitializeComponent();
            this.license = license;
        }

        private void License_Load(object sender, EventArgs e)
        {
            rtbLicense.Text = license;
        }
    }
}
