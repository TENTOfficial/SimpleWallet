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
using Newtonsoft.Json;

namespace SimpleWallet
{
    public partial class SendingCoin : Form
    {
        public Dictionary<String, String> data = new Dictionary<String, String>();
        String labelText = "";
        Api api = Api.Instance;
        public bool result = false;
        BackgroundWorker sendBgr = new BackgroundWorker();
        Types.TransactionType type = Types.TransactionType.SEND_COIN;
        public SendingCoin(Dictionary<String, String> data, Types.TransactionType type = Types.TransactionType.SEND_COIN)
        {
            InitializeComponent();

            sendBgr.WorkerSupportsCancellation = true;
            sendBgr.DoWork += new DoWorkEventHandler(this.sendBgr_DoWork);
            sendBgr.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.sendBgr_RunWorkerCompleted);

            this.data = data;
            this.type = type;
        }

        public SendingCoin(String labelText, Types.TransactionType type = Types.TransactionType.SEND_COIN)
        {
            InitializeComponent();

            sendBgr.WorkerSupportsCancellation = true;
            sendBgr.DoWork += new DoWorkEventHandler(this.sendBgr_DoWork);
            sendBgr.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.sendBgr_RunWorkerCompleted);

            this.labelText = labelText;
        }

        private void SendingCoin_Load(object sender, EventArgs e)
        {
            sendBgr.RunWorkerAsync();

            if(type == Types.TransactionType.SEND_COIN)
            {
                lbChecking.Text = "Checking transaction";
            }
            else
            {
                lbChecking.Text = "Shielding coin";
            }
            
        }

        void checkTransactions()
        {
            Dictionary<String, String> strDict = new Dictionary<String, String>();
            strDict["result"] = "checking";
            while (Api.getResult(strDict) == "checking")
            {
                if(Api.getResult(data) == "fail")
                {
                    return;
                }
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

        void checkShield()
        {
            Dictionary<String, String> strDict = new Dictionary<String, String>();
            strDict["result"] = "checking";
            while (Api.getResult(strDict) == "checking")
            {
                if (Api.getResult(data) == "fail")
                {
                    return;
                }
                dynamic parse = JsonConvert.DeserializeObject<Types.ShieldData>(data["message"]);
                strDict = api.checkTransaction(parse.opid);
                Thread.Sleep(200);
                pbTransaction.Invoke(new Action(() =>
                {
                    if (pbTransaction.Value == pbTransaction.Maximum)
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
            if (type == Types.TransactionType.SEND_COIN)
            {
                Task.Run(() => checkTransactions()).Wait();
            }
            else if (type == Types.TransactionType.SHIELD_COIN)
            {
                Task.Run(() => checkShield()).Wait();
            }
        }

    }
}
