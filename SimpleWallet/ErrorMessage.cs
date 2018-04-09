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
    public partial class ErrorMessage : Form
    {
        String message = "";
        public ErrorMessage(String message)
        {
            InitializeComponent();
            this.message = message;
        }

        private void ErrorMessage_Load(object sender, EventArgs e)
        {
            rtbMessage.Text = message;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rtbMessage_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }
    }
}
