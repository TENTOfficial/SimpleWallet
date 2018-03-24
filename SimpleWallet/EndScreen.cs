using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace SimpleWallet
{
    public partial class EndScreen : Form
    {
        BackgroundWorker endWallet = new BackgroundWorker();
        Api api = Api.Instance;
        Executor exec = Executor.Instance;
        bool isStop = false;

        private static EndScreen instance = null;
        private static readonly object padlock = new object();

        public static EndScreen Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new EndScreen();
                        }
                    }
                }
                return instance;
            }
        }

        EndScreen()
        {
            InitializeComponent();

            endWallet.WorkerSupportsCancellation = true;
            endWallet.DoWork += new DoWorkEventHandler(this.endWallet_DoWork);
            endWallet.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.endWallet_RunWorkerCompleted);

            exec.daemondHdl += DaemondStatus;
        }

        void endWallet_RunWorkerCompleted(Object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }

        void endWallet_DoWork(System.Object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            int count = 0;
            while(true)
            {
                if (isStop && count > 40)
                {
                    break;
                }
                count++;
                Thread.Sleep(50);
            }
        }
		
        void DaemondStatus(object sender, DaemonEventArgs e)
        {
            isStop = e.Stop;
        }

        private async void EndScreen_Load(object sender, EventArgs e)
        {
            await Task.Run(() => api.stopWallet());
			endWallet.RunWorkerAsync();
        }
    }
}
