using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Diagnostics;
using Newtonsoft.Json;
using System.IO;
using System.Net;

namespace SimpleWallet
{
    public class VerticalTabControl : TabControl
    {
        public VerticalTabControl()
        {
            this.Alignment = TabAlignment.Left;
            this.DrawMode = TabDrawMode.OwnerDrawFixed;
            this.SizeMode = TabSizeMode.Fixed;
            this.ItemSize = new Size(this.Font.Height * 3 / 2, 75);
        }
        public override Font Font
        {
            get { return base.Font; }
            set
            {
                base.Font = value;
                this.ItemSize = new Size(value.Height * 3 / 2, base.ItemSize.Height);
            }
        }
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            using (SolidBrush _textBrush = new SolidBrush(this.ForeColor))
            {
                TabPage _tabPage = this.TabPages[e.Index];
                Rectangle _tabBounds = this.GetTabRect(e.Index);

                if (e.State != DrawItemState.Selected) e.DrawBackground();
                else
                {
                    using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, Color.Transparent, Color.Transparent, 90f))
                    {
                        e.Graphics.FillRectangle(brush, e.Bounds);
                    }
                }

                StringFormat _stringFlags = new StringFormat();
                _stringFlags.Alignment = StringAlignment.Center;
                _stringFlags.LineAlignment = StringAlignment.Center;
                e.Graphics.DrawString(_tabPage.Text, this.Font, _textBrush, _tabBounds, new StringFormat(_stringFlags));
            }
        }
    }

    public class TransparentLabel : Label
    {
        public TransparentLabel()
        {
            this.transparentBackColor = Color.Blue;
            this.opacity = 0;
            this.BackColor = Color.Transparent;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            if (Parent != null)
            {
                using (var bmp = new Bitmap(Parent.Width, Parent.Height))
                {
                    Parent.Controls.Cast<Control>()
                          .Where(c => Parent.Controls.GetChildIndex(c) > Parent.Controls.GetChildIndex(this))
                          .Where(c => c.Bounds.IntersectsWith(this.Bounds))
                          .OrderByDescending(c => Parent.Controls.GetChildIndex(c))
                          .ToList()
                          .ForEach(c => c.DrawToBitmap(bmp, c.Bounds));


                    e.Graphics.DrawImage(bmp, -Left, -Top);
                    using (var b = new SolidBrush(Color.FromArgb(this.Opacity, this.TransparentBackColor)))
                    {
                        e.Graphics.FillRectangle(b, this.ClientRectangle);
                    }
                    e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                    TextRenderer.DrawText(e.Graphics, this.Text, this.Font, this.ClientRectangle, this.ForeColor, Color.Transparent);
                }
            }
        }

        private int opacity;
        public int Opacity
        {
            get { return opacity; }
            set
            {
                if (value >= 0 && value <= 255)
                    opacity = value;
                this.Invalidate();
            }
        }

        public Color transparentBackColor;
        public Color TransparentBackColor
        {
            get { return transparentBackColor; }
            set
            {
                transparentBackColor = value;
                this.Invalidate();
            }
        }

        [Browsable(false)]
        public override Color BackColor
        {
            get
            {
                return Color.Transparent;
            }
            set
            {
                base.BackColor = Color.Transparent;
            }
        }
    }

    public class RoundButton : Button
    {
        public RoundButton()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            GraphicsPath grPath = new GraphicsPath();
            grPath.AddEllipse(0, 0, ClientSize.Width - 1, ClientSize.Height - 1);
            this.Region = new System.Drawing.Region(grPath);
            base.OnPaint(e);
        }
    }

    public class TransparentButton : Button
    {
        public TransparentButton()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.BackColor = Color.Transparent;
        }
    }

    class Api
    {
        const int START_CHECK_NUMBER_COUN = 5;
        Executor exec = Executor.Instance;
        public String currentDaemondStatus = "";
        public String appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                 + "\\Snowgem\\";
        private static Api instance = null;
        private static readonly object padlock = new object();

        Api()
        {
        }

        public static Api Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new Api();
                        }
                    }
                }
                return instance;
            }
        }

        public List<Types.AddressBook> readAddressBook()
        {
            List<Types.AddressBook> rtn = new List<Types.AddressBook>();
            if (!File.Exists(Types.addressLabel))
                File.Create(Types.addressLabel).Close();
            String data = File.ReadAllText(Types.addressLabel);
            dynamic parse = JsonConvert.DeserializeObject<Types.ListAddressBook>(data);
            if (parse != null && parse.addressbook != null)
            {
                rtn = new List<Types.AddressBook>(parse.addressbook);
            }
            rtn = rtn.OrderBy(o => o.label).ToList();
            return rtn;
        }

        String getRandomString(int length)
        {
            String rtn = "";
            int temp = 0;
            Random rand = new Random();
            for (int i = 0; i < length; i++ )
            {
                temp = rand.Next(65, 90);
                rtn += (char)temp;
            }
            return rtn;
        }

        public void checkConfig()
        {
            if (!Directory.Exists(appdata))
            {
                Directory.CreateDirectory(appdata);
            }

            String filename = appdata + "\\snowgem.conf";
            if (!File.Exists(filename))
            {
                File.Create(filename).Close();
                String rpcUser = "rpcuser=" + getRandomString(30);
                String rpcPass = "rpcpassword=" + getRandomString(30);
                String node = "addnode=explorer.snowgem.org\naddnode=insight.snowgem.org";
                String port = "port=16113\nrpcport=16112\ntxindex=1";
                String finalStr = rpcUser + "\n" + rpcPass + "\n" + node + "\n" + port;
                File.WriteAllText(filename, finalStr);
            }
            else
            {
                String text = File.ReadAllText(filename);
                if(!text.Contains("rpcuser"))
                {
                    String rpcUser = "rpcuser=" + getRandomString(30);
                    text += "\n" + rpcUser;
                }
                if (!text.Contains("rpcpassword"))
                {
                    String rpcPassword = "rpcpassword=" + getRandomString(30);
                    text += "\n" + rpcPassword;

                    if (text[0] == '\n')
                    {
                        text = text.Remove(0, 1);
                    }
                    File.WriteAllText(filename, text);
                }
            }
        }

        public Types.ConfigureResult editComfigureFile(String aliasName, String IP, String privKey, String txHash, String txIndex, String oldName, bool isNew = true)
        {
            Types.ConfigureResult result = Types.ConfigureResult.OK;
            String appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                             + "\\Snowgem";
            String confFile = appdata + "\\snowgem.conf";
            String mnFile = appdata + "\\masternode.conf";

            List<Types.Masternode> mns = getMasternodes();
            if (oldName != aliasName && mns.FindIndex(f => f.alias == aliasName) != -1)
            {
                result = Types.ConfigureResult.DUPLICATE;
            }
            else
            {
                try
                {
                    if (!File.Exists(confFile))
                    {
                        result = Types.ConfigureResult.FAIL;
                    }
                    else
                    {
                        //config file
                        List<String> text = File.ReadAllLines(confFile).ToList();
                        text.RemoveAll(String.IsNullOrEmpty);
                        int index = text.FindIndex(x => x.StartsWith("port"));
                        if (index != -1)
                        {
                            text.RemoveAt(index);
                        }
                        text.Add("port=16113");
                        index = text.FindIndex(x => x.StartsWith("listen"));
                        if (index != -1)
                        {
                            text.RemoveAt(index);
                        }
                        text.Add("listen=1");
                        index = text.FindIndex(x => x.StartsWith("server"));
                        if (index != -1)
                        {
                            text.RemoveAt(index);
                        }
                        text.Add("server=1");
                        index = text.FindIndex(x => x.StartsWith("masternode="));
                        if (index != -1)
                        {
                            text.RemoveAt(index);
                        }
                        text.Add("masternode=1");
                        index = text.FindIndex(x => x.StartsWith("masternodeaddr"));
                        if (index != -1)
                        {
                            text.RemoveAt(index);
                        }
                        text.Add("masternodeaddr=" + IP + ":16113");
                        index = text.FindIndex(x => x.StartsWith("externalip"));
                        if (index != -1)
                        {
                            text.RemoveAt(index);
                        }
                        text.Add("externalip=" + IP + ":16113");
                        index = text.FindIndex(x => x.StartsWith("masternodeprivkey"));
                        if (index != -1)
                        {
                            text.RemoveAt(index);
                        }
                        text.Add("masternodeprivkey=" + privKey);
                        index = text.FindIndex(x => x.StartsWith("txindex"));
                        if (index != -1)
                        {
                            text.RemoveAt(index);
                        }
                        else
                        {
                            result = Types.ConfigureResult.REINDEX;
                        }
                        text.Add("txindex=1");

                        File.WriteAllLines(confFile, text.ToArray());

                        if (!isNew)
                        {
                            index = mns.FindIndex(f => f.alias == oldName);
                            if(index > -1)
                            {
                                mns.RemoveAt(index);
                            }
                        }
                        mns.Add(new Types.Masternode(aliasName, IP + ":16113", privKey, txHash, txIndex));

                        List<String> data = new List<string>();
                        foreach(Types.Masternode m in mns)
                        {
                            data.Add(m.ToString());
                        }

                        File.WriteAllLines(mnFile, data);

                    }
                }
                catch (Exception ex)
                {
                    result = Types.ConfigureResult.FAIL;
                }
            }
            return result;
        }

        public List<Types.Masternode> getMasternodes()
        {
            List<Types.Masternode> rtn = new List<Types.Masternode>();
            String appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                 + "\\Snowgem";
            String mnFile = appdata + "\\masternode.conf";

            List<String> data = File.ReadAllLines(mnFile).ToList();
            data.RemoveAll(String.IsNullOrEmpty);
            foreach (String s in data)
            {
                if (s[0] == '#')
                    continue;
                List<String> temp = s.Split(' ').ToList();
                temp.RemoveAll(String.IsNullOrEmpty);
                rtn.Add(new Types.Masternode(temp));
            }
            rtn.Sort(delegate(Types.Masternode x, Types.Masternode y)
            {
                return x.alias.CompareTo(y.alias);
            });
            return rtn;
        }

        public Dictionary<String, String> startWallet(String command)
        {
            Dictionary<String, String> strDict = new Dictionary<String, String>();
            if(!File.Exists("snowgem.exe"))
            {
                strDict["message"] = "Could not find \"snowgem.exe\" file in the folder";
                strDict["result"] = "fail";
            }
            String result = Task.Run(() => exec.executeStart(command)).Result;
            strDict["message"] = result;
            strDict["result"] = "success";
            return strDict;
        }

        public Dictionary<String, String> checkWallet()
        {
            Dictionary<String, String> strDict = new Dictionary<String, String>();
            List<String> command = new List<String> { "help" };
            String daemondStatus = exec.executeOthers(command, "");
            List<String> response = daemondStatus.Split('\n').ToList();
            response.RemoveAll(String.IsNullOrEmpty);
            if (response.Count > 0)
            {
                if (response[0].Contains("(code 1)"))
                {
                    strDict["message"] = daemondStatus;
                    strDict["result"] = "fail";
                }
                if (response.Count > START_CHECK_NUMBER_COUN)
                {
                    strDict["message"] = daemondStatus;
                    strDict["result"] = "success";
                }
                else
                {
                    strDict["message"] = response[response.Count - 1];
                    strDict["result"] = "checking";
                }
            }
            return strDict;
        }

        public void stopWallet()
        {
            exec.executeStop(); 
        }

        public String getAllData(Types.GetAllDataType type)
        {
            String data = "";
            List<String> command = new List<String> { "getalldata", ((int)type).ToString() };
            String ret = Task.Run(() => exec.executeSync(command, data)).Result;
            return ret;
        }

        public String getPeerInfo()
        {
            String data = "\"addr\"";
            List<String> command = new List<String> { "getpeerinfo" };
            String ret = Task.Run(() => exec.executeOthers(command, data)).Result;
            return ret;
        }

        public String getNetworkHeight()
        {
            String data = "";
            List<String> command = new List<String> { "getinfo" };
            String ret = Task.Run(() => exec.executeOthers(command, data)).Result;
            return ret;
        }

        public String getBestBlockhash()
        {
            String data = "bestblockhash";
            List<String> command = new List<String> { "getblockchaininfo" };
            String ret = Task.Run(() => exec.executeSync(command, data)).Result;
            return ret;
        }

        public String getBestTime(String blockHash)
        {
            String data = "time";
            List<String> command = new List<String> { "getblockheader", blockHash};
            String ret = Task.Run(() => exec.executeSync(command, data)).Result;
            return ret;
        }

        public String getBalance()
        {
            String data = "";
            List<String> command = new List<String> { "getbalance"};
            String ret = Task.Run(() => exec.executeBalance(command, data)).Result;
            return ret;
        }

        public String getUnconfirmedBalance()
        {
            String data = "";
            List<String> command = new List<String> { "getunconfirmedbalance" };
            String ret = Task.Run(() => exec.executeBalance(command, data)).Result;
            return ret;
        }

        public String getTAddress()
        {
            String data = "";
            List<String> command = new List<String> { "getaddressesbyaccount \"\"" };
            String ret = Task.Run(() => exec.executeBalance(command, data)).Result;
            return ret;
        }

        public String getZAddress()
        {
            String data = "";
            List<String> command = new List<String> { "z_listaddresses" };
            String ret = Task.Run(() => exec.executeBalance(command, data)).Result;
            return ret;
        }

        public String checkConnections()
        {
            String data = "";
            List<String> command = new List<String> { "getconnectioncount" };
            String ret = Task.Run(() => exec.executeBalance(command, data)).Result;
            return ret;
        }

        public String getAddressGroupings()
        {
            String data = "";
            List<String> command = new List<String> { "listaddressgroupings" };
            String ret = Task.Run(() => exec.executeBalance(command, data)).Result;
            return ret;
        }

        public String getAddressBalance(String address)
        {
            String data = "";
            List<String> command = new List<String> { "z_getbalance", address };
            String ret = Task.Run(() => exec.executeBalance(command, data)).Result;
            return ret;
        }

        public String newAddress()
        {
            String data = "";
            List<String> command = new List<String> { "getnewaddress" };
            String ret = Task.Run(() => exec.executeBalance(command, data)).Result;
            return ret;
        }

        public String newZAddress()
        {
            String data = "";
            List<String> command = new List<String> { "z_getnewaddress" };
            String ret = Task.Run(() => exec.executeBalance(command, data)).Result;
            return ret;
        }

        public String getMNPrivKey()
        {
            String data = "";
            List<String> command = new List<String> { "masternode ", "genkey" };
            String ret = Task.Run(() => exec.executeOthers(command, data)).Result;
            return ret;
        }

        public String getMNOutputs()
        {
            String data = "";
            List<String> command = new List<String> { "masternode ", "outputs" };
            String ret = Task.Run(() => exec.executeOthers(command, data)).Result;
            return ret;
        }

        public String getMasternodeList()
        {
            String data = "";
            List<String> command = new List<String> { "masternode ", "list" };
            String ret = Task.Run(() => exec.executeMasternode(command, data)).Result;
            return ret;
        }

        public Dictionary<String, String> checkWallet(String wallet)
        {
            Dictionary<String, String> strDict = new Dictionary<String, String>();
            if (wallet.StartsWith("z"))
            {
                strDict["result"] = "success";
                strDict["message"] = "";
                return strDict;
            }
            
            String data = "";
            List<String> command = new List<String> { "getaccount ", wallet };
            String ret = Task.Run(() => exec.executeBalance(command, data)).Result;
            if (ret.Contains("Invalid Snowgem address"))
            {
                strDict["result"] = "fail";
            }
            else
            {
                strDict["result"] = "success";
            }
            strDict["message"] = ret;
            return strDict;
        }

        public Dictionary<String, String> backupWallet()
        {
            Dictionary<String, String> strDict = new Dictionary<String, String>();
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "dat Files|*.dat";
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    String filename = dialog.FileName;
                    String walletDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                        "\\Snowgem\\wallet.dat";
                    File.Copy(walletDir, filename, true);
                    strDict["message"] = "Backup success";
                    strDict["result"] = "success";
                }
                catch(Exception ex)
                {
                    strDict["message"] = ex.Message;
                    strDict["result"] = "fail";
                }
            }
            else if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                strDict["message"] = "You've just clicked on Cancel button";
                strDict["result"] = "fail";
            }
            else
            {
                strDict["message"] = "Please put your file location";
                strDict["result"] = "fail";
            }
            return strDict;
        }

        public Dictionary<String, String> exportPrivateKeys(String filename)
        {
            Dictionary<String, String> strDict = new Dictionary<String, String>();
            String data = "";
            List<String> command = new List<String> { "z_exportwallet ", filename };
            String ret = Task.Run(() => exec.executeOthers(command, data)).Result;

            strDict["message"] = ret;
            if (ret.Contains("error"))
            {
                strDict["result"] = "fail";
            }
            else
            {
                strDict["result"] = "success";
            }
            return strDict;
        }

        public Dictionary<String, String> importPrivateKeys()
        {
            Dictionary<String, String> strDict = new Dictionary<String, String>();
            String data = "";
            String ret = "";
            List<String> command = new List<String>();

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                command.Add("z_importwallet " + "\"" + dialog.FileName + "\"");
                ret = Task.Run(() => exec.executeOthers(command, data)).Result;
            }
            else
            {
                strDict["message"] = "Please put your file location";
                strDict["result"] = "fail";
            }
            if (ret.Contains("error"))
            {
                strDict["message"] = ret;
                strDict["result"] = "fail";
            }
            else
            {
                strDict["message"] = "Import success";
                strDict["result"] = "success";
            }
            return strDict;
        }

        public Dictionary<String, String> exportPrivateKey(String address)
        {
            Dictionary<String, String> strDict = new Dictionary<String, String>();
            String data = "";
            List<String> command = new List<String> { "dumpprivkey", address };
            String ret = Task.Run(() => exec.executeOthers(command, data)).Result;
            if (ret.Contains("error"))
            {
                strDict["result"] = "fail";
            }
            else
            {
                strDict["result"] = "success";
            }
            strDict["message"] = ret;
            return strDict;
        }

        public Dictionary<String, String> importPrivateKey(String key, String label)
        {
            Dictionary<String, String> strDict = new Dictionary<String, String>();
            String data = "";
            List<String> command = new List<String> { "importprivkey", key, label };
            String ret = Task.Run(() => exec.executeOthers(command, data)).Result;
            if (ret.Contains("error"))
            {
                strDict["result"] = "fail";
                strDict["message"] = ret;
            }
            else
            {
                strDict["result"] = "success";
                strDict["message"] = "Import success";
            }
            return strDict;
        }

        public Dictionary<String, String> sendCoin(String from, String to, String amount, String fee, bool defaultFee)
        {
            Dictionary<String, String> strDict = new Dictionary<String, String>();
            List<String> command = new List<String> { "z_sendmany", from, "\"[{\\\"address\\\":\\\"" + to + "\\\",\\\"amount\\\":" + amount + "}]\"",
                "1", defaultFee ? "0.0001" : fee
            };
            String data = "";
            String ret = Task.Run(() => exec.executeOthers(command, data)).Result;
            strDict["message"] = ret;
            if (ret.Contains("error"))
            {
                strDict["result"] = "fail";
            }
            else
            {
                strDict["result"] = "success";
            }
            return strDict;
        }

        public Dictionary<String, String> shieldCoin(String from, String to, String utxo, String fee, bool defaultFee)
        {
            if(Convert.ToDouble(utxo) > 500)
            {
                utxo = "500";
            }

            Dictionary<String, String> strDict = new Dictionary<String, String>();
            List<String> command = new List<String> { "z_shieldcoinbase", from, to, defaultFee ? "0.0001" : fee, utxo};
            String data = "";
            String ret = Task.Run(() => exec.executeOthers(command, data)).Result;
            strDict["message"] = ret;
            if (ret.Contains("error"))
            {
                strDict["result"] = "fail";
            }
            else
            {
                strDict["result"] = "success";
            }
            return strDict;
        }

        public Dictionary<String, String> sendManyCoin(String from, List<String> to, String fee, bool defaultFee)
        {
            Dictionary<String, String> strDict = new Dictionary<String, String>();
            String sendInfo = "";
            sendInfo += "\"" + from + "\" " + "\"[";
            foreach (String t in to)
            {
                String[] split = t.Split(',');
                sendInfo += "{\\\"address\\\":\\\"" + split[0] + "\\\",\\\"amount\\\":" + split[1] + "},";
            }

            sendInfo = sendInfo.Remove(sendInfo.Length - 1, 1);

            sendInfo += "]\"";

            if (defaultFee)
            {
                sendInfo += " 1 " + "0.0001";
            }
            else
            {
                sendInfo += " 1 " + fee;
            }


            String data = "";
            List<String> command = new List<String> { "z_sendmany", sendInfo };

            String ret = Task.Run(() => exec.executeOthers(command, data)).Result;

            strDict["message"] = ret;
            if (ret.Contains("error"))
            {
                strDict["result"] = "fail";
            }
            else
            {
                strDict["result"] = "success";
            }
            return strDict;
        }

        public Dictionary<String, String> checkTransaction(String opid)
        {
            Dictionary<String, String> strDict = new Dictionary<String, String>();
            String data = "";
            List<String> command = new List<String> { "z_getoperationstatus", "\"[\\\"" + opid + "\\\"]\"" };
            String ret = Task.Run(() => exec.executeOthers(command, data)).Result;

            if (ret.StartsWith("["))
            {
                ret = ret.TrimStart('[');
                ret = ret.TrimEnd(']');
                strDict["message"] = ret;
                try
                {
                    dynamic parse = JsonConvert.DeserializeObject<Types.TransactionStatus>(ret);
                    if (parse.status == "success")
                    {
                        strDict["result"] = "success";
                        return strDict;
                    }
                    else if (parse.status == "failed")
                    {
                        strDict["result"] = "fail";
                        return strDict;
                    }
                }
                catch (Exception ex)
                { }
            }
            strDict["result"] = "checking";
            return strDict;
        }

        public bool isDaemonRunning()
        {
            return !exec.getDeamondClosedStatus();
        }

        public String getListTransactions()
        {
            String data = "";
            List<String> command = new List<String> { "listtransactions", "\"\"", "100" };
            String ret = Task.Run(() => exec.executeBalance(command, data)).Result;
            return ret;
        }

        public String getTransaction(string txid)
        {
            String data = "";
            List<String> command = new List<String> { "gettransaction", txid };
            String ret = Task.Run(() => exec.executeGetTransaction(command, data)).Result;
            return ret;
        }

        public String startMasternode(String name)
        {
            String data = "";
            List<String> command = new List<String> { "startmasternode ", "alias", "false", name};
            String ret = Task.Run(() => exec.executeMasternode(command, data)).Result;
            return ret;
        }

        public String startAlias(String name)
        {
            String data = "";
            List<String> command = new List<String> { "startalias ", name };
            String ret = Task.Run(() => exec.executeMasternode(command, data)).Result;
            return ret;
        }

        public Types.MasternodeType isMasternodeEnable()
        {
            List<String> data = File.ReadAllLines(Types.cfLocation).ToList();
            int index = data.FindIndex(x => x.StartsWith("masternode="));
            if (index >= 0)
            {
                String[] split = data[index].Split('=');
                if(split.Length >=2)
                {
                    if (split[1] == "1")
                        return Types.MasternodeType.ON;
                    else
                        return Types.MasternodeType.OFF;
                }
            }
            return Types.MasternodeType.NONE;
        }
        public static bool checkResult(Dictionary<String, String> result)
        {
            try
            {
                return result["result"] == "success" || result["result"] == "checking" ? true : false;
            }
            catch (Exception ex)
            {
                return true;
            }
            
        }

        public static String getMessage(Dictionary<String, String> result)
        {
            try
            {
                return result["message"];
            }
            catch(Exception ex)
            {
                return "";
            }
        }

        public static String getResult(Dictionary<String, String> result)
        {
            try
            {
                return result["result"];
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public bool CheckProcess(String name)
        {
            foreach (Process clsProcess in Process.GetProcesses())
            {
                String temp = clsProcess.ProcessName.ToLower();
                if (temp.Contains(name.ToLower()))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
