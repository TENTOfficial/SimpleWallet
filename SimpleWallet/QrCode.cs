using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QRCoder;

namespace SimpleWallet
{
    public partial class QrCode : Form
    {
        Image bm = null;
        String address = "";

        private void RenderQrCode()
        {
            QRCodeGenerator.ECCLevel eccLevel = QRCodeGenerator.ECCLevel.M;
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(address, eccLevel))
                {
                    using (QRCode qrCode = new QRCode(qrCodeData))
                    {

                        pbQrCode.BackgroundImage = qrCode.GetGraphic(10, Color.Black, Color.White,
                            null, 1);

                        this.pbQrCode.Size = new System.Drawing.Size(pbQrCode.Width, pbQrCode.Height);
                        //Set the SizeMode to center the image.
                        this.pbQrCode.SizeMode = PictureBoxSizeMode.CenterImage;

                        pbQrCode.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                }
            }
        }

        public QrCode(String address)
        {
            InitializeComponent();
            this.address = address;
        }

        private void QrCode_Load(object sender, EventArgs e)
        {
            RenderQrCode();
            tbQrCode.Text = address;
        }
    }
}
