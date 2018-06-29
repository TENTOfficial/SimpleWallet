using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SimpleWallet
{
    public class ReceivedDataEventArgs : EventArgs
    {
        public bool shouldDelete { get; set; }
        public int progress { get; set; }
        public bool isCancel { get; set; }
        public String fileName { get; set; }

        public ReceivedDataEventArgs(bool shouldDelete, int progress, bool isCancel, String fileName)
       {
           this.shouldDelete = shouldDelete;
           this.progress = progress;
           this.isCancel = isCancel;
           this.fileName = fileName;
       }
    }

    public delegate void ReceivedDataEventHandler(object sender,
                                                  ReceivedDataEventArgs e);

    public class DeamonErrorEventArgs : EventArgs
    {
        public String errMessage { get; set; }

        public DeamonErrorEventArgs(String errMessage)
        {
            this.errMessage = errMessage;
        }
    }

    public delegate void DeamonErrorEventHandler(object sender,
                                                  DeamonErrorEventArgs e);

    public class DaemonEventArgs : EventArgs
    {
        public bool Stop { get; set; }

        public DaemonEventArgs(bool Stop)
        {
            this.Stop = Stop;
        }
    }

    public delegate void DaemonEventHandler(object sender,
                                                  DaemonEventArgs e);
    public class Types
    {
        public static String version = "SnowGem Simple Wallet - Version 2.0.3";
        public static int time = 1529647706;
        public static String dataLocation = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                        "\\Snowgem";
        public static String simpleWalletLocation = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                        "\\Snowgem\\simplewallet";
        public static String mnLocation = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                        "\\Snowgem\\masternode.conf";
        public static String cfLocation = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                 + "\\Snowgem\\snowgem.conf";
        public static String logLocation = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                 + "\\Snowgem\\debug.log";
        public static String startCommandsFile = simpleWalletLocation + "\\commands.dat";
        public static String addressLabel = simpleWalletLocation + "\\addressLabel.dat";
        public static String mnCache = dataLocation + "\\mncache.dat";
        public static String masternodeSave = simpleWalletLocation + "\\masternodesave.dat";
        public static String outputsSave = simpleWalletLocation + "\\outputs.dat";
        public static String enableBackup = simpleWalletLocation + "\\autoBackup.dat";
        public Dictionary<bool, String> boolDict = new Dictionary<bool, String>();
        public Dictionary<int, String> intDict = new Dictionary<int, String>();
        public Dictionary<String, String> strDict = new Dictionary<String, String>();
        public static String verifyingKeyMD5 = "21e8b499aa84b5920ca0cea260074f34";
        public static String provingKeyMD5 = "af23e521697ed69d8b8a6b9c53e48300";
        public enum MasternodeType
        {
            NONE = 0,
            ON,
            OFF
        }

        public enum GetAllDataType
        {
            ALL = 0,
            WITH_BALANCE,
            WITH_TRANSACTIONS,
            NONE,
            END
        }

        public enum TransactionType
        {
            SEND_COIN = 0,
            SHIELD_COIN,
            IMPORT_KEY,
            END
        }

        public enum StepSync
        {
            GET_BEST_HASH = 0,
            GET_BEST_TIME,
            END
        }

        public enum StepBalance
        {
            GET_CONFIRMED_BALANCE = 0,
            GET_UNCONFIRM_BALANCE,
            GET_BALANCE,
            GET_Z_ADDRESS,
            UPDATE_DATA,
            LIST_TRANSACTION,
            GET_CONNECTION_COUNT,
            END
        }

        public enum UpdateData
        {
            TIME = 0,
            HASH,
            END
        }

        public enum OutputType
        {
            DAEMOND = 0,
            SYNC,
            BALANCE,
            MASTERNODE,
            GET_TRANSACTION,
            DEBUG,
            OTHERS
        }

        public enum DownloadFileType
        {
            VERIFYING = 0,
            PROVING = 1
        }

        public enum DebugType
        {
            DEBUG = 0,
            PEERS = 1
        }

        public enum ConfigureResult
        {
            OK = 0,
            FAIL = 1,
            REINDEX = 2,
            DUPLICATE
        }

        public enum CtxMenuType
        {
            WALLET = 0,
            TRANSACTIONS,
            ADDRESS_BOOK,
            MASTERNODE,
            MASTERNODE_GLOBAL
        }

        public class RightClickData
        {
            public bool isClicked { get; set; }
            public Types.CtxMenuType type { get; set; }
            public int rowIdx { get; set; }
            public int colIdx { get; set; }
            public int x { get; set; }
            public int y { get; set; }
        }

        public class ListAddressBook
        {
            public List<AddressBook> addressbook { get; set; }
        }

        public class ShieldData
        {
            public float remainingUTXOs { get; set; }
            public float remainingValue { get; set; }
            public float shieldingUTXOs { get; set; }
            public float shieldingValue { get; set; }
            public String opid { get; set; }
        }

        public class Version
        {
            public String version { get; set; }
            public int time { get; set; }
            public String link { get; set; }
            public String content { get; set; }
        }

        public class AllData
        {
            public String connectionCount { get; set; }
            public String besttime { get; set; }
            public String bestblockhash { get; set; }
            public String transparentbalance { get; set; }
            public String privatebalance { get; set; }
            public String lockedbalance { get; set; }
            public String totalbalance { get; set; }
            public String unconfirmedbalance { get; set; }
            public String immaturebalance { get; set; }
            public List<Dictionary<String, String>> addressbalance { get; set; }
            public List<Transaction> listtransactions { get; set; }
        }

        public class BlockFormat
        {
            public String hash { get; set; }
            public String confirmations { get; set; }
            public String height { get; set; }
            public String version { get; set; }
            public String merkleroot { get; set; }
            public String time { get; set; }
            public String nonce { get; set; }
            public String solution { get; set; }
            public String bits { get; set; }
            public String difficulty { get; set; }
            public String chainwork { get; set; }
            public String previousblockhash { get; set; }
        }

        public class WalletFormat
        {
            public String address { get; set; }
            public String account { get; set; }
            public String amount { get; set; }
            public String confirmations { get; set; }
        }

        public class TransactionStatus
        {
            public String id { get; set; }
            public String status { get; set; }
        }

        public class Transaction
        {
            public String address { get; set; }
            public String category { get; set; }
            public String amount { get; set; }
            public String confirmations { get; set; }
            public String time { get; set; }
            public String txid { get; set; }
        }

        public class TransactionConverted
        {
            public String category { get; set; }
            public String confirmations { get; set; }
            public String amount { get; set; }
            public String time { get; set; }
            public String address { get; set; }
            public String txid { get; set; }

            public TransactionConverted(String category, String confirmations, String amount, String time, String address, String txid)
            {
                this.category = category;
                this.confirmations = confirmations;
                this.amount = amount;
                this.time = time;
                this.address = address;
                this.txid = txid;
            }
        }

        public class AddressBook
        {
            public AddressBook(String label, String address)
            {
                this.address = address;
                this.label = label;
            }
            public String label { get; set; }
            public String address { get; set; }
        }

        public class BlockChainInfo
        {
            public String bestblockhash { get; set; }
        }

        public class PeerInfo
        {
            public String addr { get; set; }
        }

        public class Info
        {
            public String blocks { get; set; }
        }

        public class Outputs
        {
            public String txhash { get; set; }
            public int outputidx { get; set; }
        }

        public class OutputsList
        {
            public List<Outputs> outputslist { get; set; }
        }

        public class Masternode
        {
            public Masternode(string status, string alias, string ipAddress, string privKey, string txHash, string txindex)
            {
                this.status = status;
                this.alias = alias;
                this.privKey = privKey;
                this.ipAddress = ipAddress;
                this.txHash = txHash;
                this.index = txindex;
            }
            public Masternode()
            {
                this.status = this.alias = this.privKey = this.ipAddress = this.txHash = this.index = "";
            }

            public Masternode(Masternode temp)
            {
                this.status = temp.status;
                this.alias = temp.alias;
                this.privKey = temp.privKey;
                this.ipAddress = temp.ipAddress;
                this.txHash = temp.txHash;
                this.index = temp.index;
            }

            public Masternode(List<String> temp)
            {
                this.status = temp[0];
                this.alias = temp[1];
                this.ipAddress = temp[2];
                this.privKey = temp[3];
                this.txHash = temp[4];
                this.index = temp[5];
            }

            public String ToString()
            {
                return (status == "ENABLE" ? "" : "#") + alias + " " + ipAddress + " " + privKey + " " + txHash + " " + index;
            }

            public String status { get; set; }
            public String alias { get; set; }
            public String privKey { get; set; }
            public String ipAddress { get; set; }
            public String txHash { get; set; }
            public String index { get; set; }
        }
        public class MasternodeList
        {
            public List<MasternodeDetail> masternodelist { get; set; }
        }

        public class MasternodeDetail
        {
            public MasternodeDetail(MasternodeDetail temp)
            {
                if (temp != null)
                {
                    this.rank = temp.rank;
                    this.addr = temp.addr;
                    this.version = temp.version;
                    this.status = temp.status;
                    this.activetime = temp.activetime;
                    this.lastseen = temp.lastseen;
                    this.lastpaid = temp.lastpaid;
                    this.txhash = temp.txhash;
                    this.network = temp.network;
                    this.outidx = temp.outidx;
                    this.ip = temp.ip;
                }
            }
            public int rank { get; set; }
            public String addr { get; set; }
            public int version { get; set; }
            public String status { get; set; }
            public int activetime { get; set; }
            public int lastseen { get; set; }
            public int lastpaid { get; set; }
            public String txhash { get; set; }
            public String network { get; set; }
            public int outidx { get; set; }
            public String ip { get; set; }
        }

        public class MasternodeDetailConverted
        {
            public String rank { get; set; }
            public String addr { get; set; }
            public String version { get; set; }
            public String status { get; set; }
            public String activetime { get; set; }
            public String lastseen { get; set; }
            public String lastpaid { get; set; }
            public String txhash { get; set; }
            public String ip { get; set; }

            public MasternodeDetailConverted(String rank, String addr, String version, 
                String status, String activetime, String lastseen, String lastpaid, String txhash, String ip)
            {
                this.rank = rank;
                this.addr = addr;
                this.version = version;
                this.status = status;
                this.activetime = activetime;
                this.lastseen = lastseen;
                this.lastpaid = lastpaid;
                this.txhash = txhash;
                this.ip = ip;
            }
        }

        public class StartMNResponse
        {
            public String overall { get; set; }
            public List<Detail> detail { get; set; }
        }

        public class Detail
        {
            public String alias { get; set; }
            public String result { get; set; }
            public String error { get; set; }
        }
    }
}
