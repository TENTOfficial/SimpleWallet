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
using System.IO;
using System.Net;
using System.Timers;

namespace SimpleWallet
{
    public partial class SplashScreen : Form
    {
        Start start = new Start();
        Executor exec = Executor.Instance;
        Api api = Api.Instance;
        EndScreen scr = EndScreen.Instance;

        BackgroundWorker startWallet = new BackgroundWorker();
        const String verifyingKey = "sprout-verifying.key";
        const String provingKey = "sprout-proving.key";
        String currStatus = "Checking version...";

        Int32 unixTimestamp = 0;
        bool installNewVersion = false;
        bool veriStatus = false;
        bool provStatus = false;
        bool shouldRestart = false;
        String command = "";

        bool checkParamsDone = false;
        Dictionary<String, String> result = new Dictionary<String, String>();
        System.Timers.Timer aTimer = new System.Timers.Timer(500);

        public SplashScreen()
        {
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Start();
            InitializeComponent();

            lbLog.Parent = pictureBox1;
            lbLog.BackColor = Color.Transparent;

            exec.progressChange += Download_ProgressChange;
            exec.progressDone += Download_ProgressDone;
            exec.errorOccurs += ErrorOccurs;

            startWallet.WorkerSupportsCancellation = true;
            startWallet.DoWork += new DoWorkEventHandler(this.startWallet_DoWork);
            startWallet.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.startWallet_RunWorkerCompleted);

        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            Int32 curr = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            lbCurrentStatus.Invoke(new Action(() => lbCurrentStatus.Text = "(" + (curr - unixTimestamp) + ") " + currStatus));
        }

        void startWallet_RunWorkerCompleted(Object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (installNewVersion)
            {
                this.Close();
            }
            else
            {
                if (Api.checkResult(result))
                {
                    aTimer.Stop();
                    this.Hide();
                    start.ShowDialog();
                    if (shouldRestart || start.shouldRestart == true)
                    {
                        Task.Run(() => Application.Restart());
                    }
                    else
                        this.Close();
                }
                else
                {
                    MessageBox.Show(Api.getMessage(result));
                }
            }
        }

        void startWallet_DoWork(System.Object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            bool init = initialization(e);
            if (init)
            {
                installNewVersion = false;
                //get data
                String data = api.getAllData(Types.GetAllDataType.ALL);
                dynamic parse = JsonConvert.DeserializeObject<Types.AllData>(data);

                start.bestHash = parse.bestblockhash;
                start.bestTime = parse.besttime;
                start.connections = parse.connectionCount;
                start.totalbalance = parse.totalbalance;
                start.unconfirmedbalance = parse.unconfirmedbalance;
                start.privatebalance = parse.privatebalance;
                start.lockedbalance = parse.lockedbalance;
                start.transparentbalance = parse.transparentbalance;
                start.listtransactions = new List<Types.Transaction>(parse.listtransactions);
                List<Dictionary<String, String>> addressbalance = parse.addressbalance;
                if (addressbalance != null && addressbalance.Count > 0)
                {
                    start.walletDic = new Dictionary<String, String>(parse.addressbalance[0]);
                }
            }
            else
            {
                installNewVersion = true;
            }
        }

        private void SplashScreen_Load(object sender, EventArgs e)
        {
            if (File.Exists(Types.startCommandsFile))
            {
                command = File.ReadAllText(Types.startCommandsFile);
                File.Delete(Types.startCommandsFile);
            }

            String walletDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
            "\\Snowgem\\wallet.dat";

            if (!File.Exists(walletDir))
            {
                MessageBox.Show("Remember to backup your wallet.dat file and private keys before using the wallet", "ATTENTION!!!");
            }

            startWallet.RunWorkerAsync();
        }

        bool initialization(System.ComponentModel.DoWorkEventArgs e)
        {
            //check new version>
            WebClient client = new WebClient();
            String downloadedString = client.DownloadString("https://data.snowgem.org/simplewallet/version.json");
            Types.Version parse = JsonConvert.DeserializeObject<Types.Version>(downloadedString);
            if (Types.time < parse.time)
            {
                NewVersion nVer = new NewVersion(parse);
                nVer.ShowDialog();
                if (nVer.isFinished)
                {
                    return false;
                }
            }

            api.checkConfig();
            currStatus = "Checking params...";
            //lbCurrentStatus.Invoke(new Action(() => lbCurrentStatus.Text = currStatus));
            bool downloadFile = exec.checkParamsFile(verifyingKey, provingKey);
            if (downloadFile)
            {
                pbProcess.Invoke(new Action(() => pbProcess.Visible = true));
                currStatus = "Downloading params";
                //lbCurrentStatus.Invoke(new Action(() => lbCurrentStatus.Text = currStatus));
                downloadFile = Task.Run(() => exec.downloadParams(verifyingKey, Types.DownloadFileType.VERIFYING)).Result;
                if (!downloadFile)
                {
                    veriStatus = true;
                }
                downloadFile |= Task.Run(() => exec.downloadParams(provingKey, Types.DownloadFileType.PROVING)).Result;
                if (downloadFile)
                {
                    checkParamsDone = false;
                }
                else
                {
                    provStatus = true;
                }
            }
            else
            {
                veriStatus = true;
                provStatus = true;
                checkParamsDone = true;
                if (start.isEnableAutoBackup())
                    command += " -backupwallet";
                Task.Run(() =>
                        api.startWallet(command));
            }
            Thread.Sleep(1000);
            int countdot = 0;
            while (true)
            {
                if (e.Cancel)
                {
                    break;
                }
                if (checkParamsDone)
                {
                    Dictionary<String, String> data = api.checkWallet();
                    if (Api.checkResult(data))
                    {
                        if (Api.getResult(data) == "success")
                        {
                            break;
                        }
                        else if (Api.getResult(data) == "fail")
                        {
                            currStatus = data["message"];
                        }
                        else
                        {
                            currStatus = data["message"];
                        }
                    }
                }
                else
                {
                    if (countdot < 3)
                    {
                        currStatus = "Downloading params";
                    }
                    else if (countdot < 6)
                    {
                        currStatus = "Downloading params.";
                    }
                    else if (countdot < 9)
                    {
                        currStatus = "Downloading params..";
                    }
                    else if (countdot < 12)
                    {
                        currStatus = "Downloading params...";
                    }
                    else
                    {
                        countdot = 0;
                    }
                }
                countdot++;

                Thread.Sleep(200);
            }

            return true;
        }


        void ErrorOccurs(object sender, DeamonErrorEventArgs e)
        {
            if (!e.errMessage.Contains("Snowgem is probably already running"))
            {
                api.stopWallet();

                if (e.errMessage.Contains("-reindex"))
                {
                    MessageBox.Show("Your data need reindex", "Error!!!");
                    shouldRestart = true;
                    File.WriteAllText(Types.startCommandsFile, "-reindex");
                }
                MessageBox.Show(e.errMessage, "Error!!!");
                this.Invoke(new Action(() => this.Close()));
            }
        }

        void Download_ProgressChange(object sender, ReceivedDataEventArgs e)
        {
            pbProcess.Invoke(new Action(() => pbProcess.Value = e.progress * 100));   
        }

        void pbIncrease()
        {
            pbProcess.Invoke(new Action(() =>
                {
                    if (pbProcess.Value + 100 >= pbProcess.Maximum)
                        pbProcess.Value = 0;
                    pbProcess.Value += 100;
                }));   
        }

        void Download_ProgressDone(object sender, ReceivedDataEventArgs e)
        {
            if(!e.isCancel && e.fileName == verifyingKey)
            {
                veriStatus = true;
            }
            else if(!e.isCancel && e.fileName == provingKey)
            {
                provStatus = true;
            }
            if (veriStatus && provStatus)
            {
                checkParamsDone = true;
                if (start.isEnableAutoBackup())
                    command += " -backupwallet";
                Task.Run(() =>
                    api.startWallet(command));
            }

            if(e.isCancel)
            {
                startWallet.CancelAsync();
            }
        }

        private void pbProcess_MouseHover(object sender, EventArgs e)
        {
            //toolt
        }

        private void SplashScreen_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

    }
}
