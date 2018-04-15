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
    public partial class Message : Form
    {
        String message = "";
        String title = "";
        public Message(String title, String message)
        {
            InitializeComponent();
            this.message = message;
            this.title = title;
        }

        private void ErrorMessage_Load(object sender, EventArgs e)
        {
            rtbMessage.Text = message;
            this.Text = title;
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
