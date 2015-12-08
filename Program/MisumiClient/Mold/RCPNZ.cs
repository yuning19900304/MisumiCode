using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Net;
using System.Windows.Forms;
using Misumi_Client.Common;

namespace Misumi_Client.Mold
{
    public partial class RCPNZ : Form
    {
        public string WebUrl = "";
        public string strShape1 = "";
        public string strShape2 = "";
        ModelObject Model;
        LanguageManager LM;
        public RCPNZ(ModelObject model)
        {
            Model = model;
            WebUrl = model.MoldService.Url;
            WebUrl = WebUrl.Substring(0, WebUrl.LastIndexOf("/") + 1);
            InitializeComponent();

            LM = new LanguageManager("Special");
            this.PicCancel.Image = Image.FromFile(LM.SetLanguage("BtnBack"));
            this.btn_Next.Image = Image.FromFile(LM.SetLanguage("BtnNext"));
            this.Text = Model.E_CatalogType;
        }
        Panel panelShape1 = new Panel();
        Panel panelShape2 = new Panel();
        private void RCPNZForm_Load(object sender, EventArgs e)
        {
            LanguageManager.SetFont(this.Controls, 9);
            this.txtModelType.Font = new Font("Arial", 14, FontStyle.Bold);
            this.lblType.Font = new Font("Arial", 14, FontStyle.Bold);
            string PicPath = LM.SetLanguage("PicPath");
            this.lblType.Text = LM.SetLanguage("lblType");
            this.RCPNZText1.Text = LM.SetLanguage("TPNText");
            this.RCPNZShape1.Text = LM.SetLanguage("RCPNZShape1");
            this.RCPNZText2.Text = LM.SetLanguage("RCPNZText2");
            this.RCPNZShape2.Text = LM.SetLanguage("RCPNZShape2");

            this.lblRefer.Text = LM.SetLanguage("lblRefer");
            this.lblRefer.Font = new Font("Arial", 14, FontStyle.Bold);
            this.lblRefer.Location = new Point(this.lblType.Location.X + lblType.Width - lblRefer.Width, lblRefer.Location.Y);
            this.lblOldOrder.Text = Model.OldOrder;
            this.lblOldOrder.Font = new Font("Arial", 14, FontStyle.Bold);

            this.txtModelType.Text = Model.E_CatalogType + "□□";
            if (Model.E_CatalogType.Contains("RCPNZ") || Model.E_CatalogType.Contains("RCPL")
             || Model.E_CatalogType.Contains("RCPN") || Model.E_CatalogType.Contains("RCPLZ"))
            {
                this.PicN.LoadAsync(WebUrl + PicPath + "RCPNZ/N.jpg");
                this.PicI.LoadAsync(WebUrl + PicPath + "RCPNZ/I.jpg");
                this.PicT.LoadAsync(WebUrl + PicPath + "RCPNZ/T.jpg");
                this.PicK.LoadAsync(WebUrl + PicPath + "RCPNZ/K.jpg");
                this.PicL.LoadAsync(WebUrl + PicPath + "RCPNZ/L.jpg");
                this.PicA.LoadAsync(WebUrl + PicPath + "RCPNZ/AShape.jpg");
                this.PicB.LoadAsync(WebUrl + PicPath + "RCPNZ/BShape.jpg");
                this.PicC.LoadAsync(WebUrl + PicPath + "RCPNZ/CShape.jpg");
                panelShape1.Paint += new PaintEventHandler(panel_Paint);
                panelShape2.Paint += new PaintEventHandler(panel_Paint);
            }
            else if (Model.E_CatalogType.Contains("RCPK"))
            {
                this.PicN.LoadAsync(WebUrl + PicPath + "RCPK/N.jpg");
                this.PicI.LoadAsync(WebUrl + PicPath + "RCPK/I.jpg");
                this.PicT.LoadAsync(WebUrl + PicPath + "RCPK/T.jpg");
                this.PicK.LoadAsync(WebUrl + PicPath + "RCPK/K.jpg");
                this.PicL.LoadAsync(WebUrl + PicPath + "RCPK/L.jpg");
                this.PicA.LoadAsync(WebUrl + PicPath + "RCPK/AShape.jpg");
                this.PicB.LoadAsync(WebUrl + PicPath + "RCPK/BShape.jpg");
                this.PicC.LoadAsync(WebUrl + PicPath + "RCPK/CShape.jpg");
                panelShape1.Paint += new PaintEventHandler(panel_Paint);
                panelShape2.Paint += new PaintEventHandler(panel_Paint);
            }
            else if (Model.E_CatalogType.Contains("RFAL") || Model.E_CatalogType.Contains("RFAN"))
            {
                this.PicN.Image = SpecialComm.LoadUrlImage(WebUrl + PicPath + "RFA/N.jpg");
                this.PicI.Image = SpecialComm.LoadUrlImage(WebUrl + PicPath + "RFA/I.jpg");
                this.PicT.Image = SpecialComm.LoadUrlImage(WebUrl + PicPath + "RFA/Y.jpg");
                this.PicT.Tag = "Y";
                this.PicK.Visible = false;
                this.PicL.Visible = false;
                this.PicA.Image = SpecialComm.LoadUrlImage(WebUrl + PicPath + "RFA/AShape.jpg");
                this.PicB.Image = SpecialComm.LoadUrlImage(WebUrl + PicPath + "RFA/BShape.jpg");
                this.PicC.Image = SpecialComm.LoadUrlImage(WebUrl + PicPath + "RFA/CShape.jpg");
                panelShape1.Paint += new PaintEventHandler(panel_Paint);
                panelShape2.Paint += new PaintEventHandler(panel_Paint);
                if (Model.MoldService.GetSpecialNum(Model.E_CatalogType, Model.Region) == "0")
                    CommonHelper.GetControls(this.Controls);
            }
        }

        private void PicOne_Click(object sender, EventArgs e)
        {
            PictureBox ShapePic = (PictureBox)sender;
            SpecialComm.ShowPanel(ShapePic, panelShape1);
            strShape1 = ShapePic.Tag.ToString();
            if (strShape1 == "N")
            {
                this.txtModelType.Text = Model.E_CatalogType + strShape1;
                PicA.Enabled = false;
                PicB.Enabled = false;
                PicC.Enabled = false;
                panelShape2.Visible = false;
            }
            else
            {
                PicA.Enabled = true;
                PicB.Enabled = true;
                PicC.Enabled = true;
                panelShape2.Visible = true;
                if (strShape2 != "")
                    this.txtModelType.Text = Model.E_CatalogType + strShape1 + strShape2;
                else
                    this.txtModelType.Text = Model.E_CatalogType + strShape1 + "□";
            }
        }

        private void PicTwo_Click(object sender, EventArgs e)
        {
            PictureBox ShapePic = (PictureBox)sender;
            SpecialComm.ShowPanel(ShapePic, panelShape2);
            strShape2 = ShapePic.Tag.ToString();
            if (strShape1 == "N")
                this.txtModelType.Text = Model.E_CatalogType + strShape1;
            else
                if (strShape1 != "")
                    this.txtModelType.Text = Model.E_CatalogType + strShape1 + strShape2;
                else
                    this.txtModelType.Text = Model.E_CatalogType + "□" + strShape2;
        }

        void panel_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            SpecialComm.DrawPanel(sender, e);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (strShape1 == "N")
            {
                Model.ModelType = this.txtModelType.Text;
                this.Hide();
                MoldLoad start = new MoldLoad(Model);
                start.Show();
            }
            else if (strShape1 != "N" && strShape1 != "" && strShape2 != "")
            {
                Model.ModelType = this.txtModelType.Text;
                this.Hide();
                MoldLoad start = new MoldLoad(Model);
                start.Show();
            }
            else
            {
                MessageBox.Show(LM.SetLanguage("SpecialMsg"), "RCPNZ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PicCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Model.Dispose();
            System.Environment.Exit(0);
        }
    }
}
