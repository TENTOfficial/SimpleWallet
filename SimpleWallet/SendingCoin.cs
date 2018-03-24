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
    public partial class SendingCoin : Form
    {
        public Dictionary<String, String> data = new Dictionary<String, String>();
        Api api = Api.Instance;
        public bool result = false;
        BackgroundWorker sendBgr = new BackgroundWorker();

        public SendingCoin(Dictionary<String, String> data)
        {
            InitializeComponent();

            sendBgr.WorkerSupportsCancellation = true;
            sendBgr.DoWork += new DoWorkEventHandler(this.sendBgr_DoWork);
            sendBgr.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.sendBgr_RunWorkerCompleted);

            this.data = data;
        }

        private void SendingCoin_Load(object sender, EventArgs e)
        {
            sendBgr.RunWorkerAsync();
        }

        void checkTransactions()
        {
            Dictionary<String, String> strDict = new Dictionary<String, String>();
            strDict["result"] = "checking";
            while (Api.getResult(strDict) == "checking")
            {
                strDict = api.checkTransaction(data["message"]);
                Thread.Sleep(200);
                pbTransaction.Invoke(new Action(() => 
                    {
                        if(pbTransaction.Value == pbTransaction.Maximum)
                        {
                            pbTransaction.Value = 0;
                        }
                        else
                        {
                            pbTransaction.Value += 1;
                        }
                    }));
            }
            data = strDict;
        }

        void sendBgr_RunWorkerCompleted(Object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }

        void sendBgr_DoWork(System.Object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Task.Run(() => checkTransactions()).Wait();
        }

    }
}
