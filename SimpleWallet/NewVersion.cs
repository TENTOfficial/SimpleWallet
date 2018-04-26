using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace SimpleWallet
{
    public partial class NewVersion : Form
    {
        String fearutes = "";
        Types.Version version = new Types.Version();
        Executor exec = Executor.Instance;
        public bool isFinished = false;

        public NewVersion(Types.Version version)
        {
            InitializeComponent();
            this.version = version;
            exec.downloadProgressChange += NewVersion_ProgressChange;
            exec.downloadProgressDone += NewVersion_ProgressDone;
        }

        private void NewVersion_Load(object sender, EventArgs e)
        {
            this.Text = "New version of SimpleWallet is available";
            rtbFeatures.Text = version.content;
        }


        void NewVersion_ProgressChange(object sender, ReceivedDataEventArgs e)
        {
            pbDownloading.Invoke(new Action(() => pbDownloading.Value = e.progress * 100));
        }

        void NewVersion_ProgressDone(object sender, ReceivedDataEventArgs e)
        {
            if(e.isCancel == false)
            {
                try
                {
                    isFinished = true;
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.CreateNoWindow = false;
                    startInfo.UseShellExecute = true;
                    startInfo.FileName = Path.GetTempPath() + "\\SimpleWallet.exe";
                    Process.Start(startInfo);
                }
                catch (Exception ex) { }
                this.Invoke(new Action(() => this.Close()));
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ((Button)sender).Enabled = false;
            pbDownloading.Visible = true;
            Task.Run(() => exec.downloadNewVersion(version.link, "SimpleWallet.exe")).Wait();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NewVersion_FormClosing(object sender, FormClosingEventArgs e)
        {
            exec.stopNewVersion();
        }
    }
}
