using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Net;
using System.Windows.Forms;
using Misumi_Client.Common;

namespace Misumi_Client.Mold
{
    public partial class TNP : Form
    {
        public string WebUrl = "";
        public string strShape1 = "";
        ModelObject Model;
        LanguageManager LM;
        public TNP(ModelObject model)
        {
            Model = model;
            WebUrl = model.MoldService.Url;
            WebUrl = WebUrl.Substring(0, WebUrl.LastIndexOf("/") + 1);
            InitializeComponent();

            LM = new LanguageManager("Special");
            this.PicCancel.Image = Image.FromFile(LM.SetLanguage("BtnBack"));
            this.btn_Next.Image = Image.FromFile(LM.SetLanguage("BtnNext"));
            this.lblType.Text = LM.SetLanguage("lblType");
        }

        Panel panelShape1 = new Panel();
        private void TNP_Load(object sender, EventArgs e)
        {
            LanguageManager.SetFont(this.Controls, 9);
            this.txtModelType.Font = new Font("Arial", 14, FontStyle.Bold);
            this.lblType.Font = new Font("Arial", 14, FontStyle.Bold);
            this.TPNText.Text = LM.SetLanguage("TPNText");
            this.lblRefer.Text = LM.SetLanguage("lblRefer");
            this.lblRefer.Font = new Font("Arial", 14, FontStyle.Bold);
            this.lblRefer.Location = new Point(this.lblType.Location.X + lblType.Width - lblRefer.Width, lblRefer.Location.Y);
            this.lblOldOrder.Text = Model.OldOrder;
            this.lblOldOrder.Font = new Font("Arial", 14, FontStyle.Bold);
            string PicPath = LM.SetLanguage("PicPath");
            this.Text = Model.E_CatalogType;
            if (Model.E_CatalogType.Contains("TNPC"))
            {
                this.PicOne.Image = SpecialComm.LoadUrlImage(WebUrl + PicPath + "TNP/TNPC10.gif");
                this.PicOne.Tag = "TNPC10";
                this.PicTwo.Image = SpecialComm.LoadUrlImage(WebUrl + PicPath + "TNP/TNPC30.gif");
                this.PicTwo.Tag = "TNPC30";
                this.PicThree.Visible = false;
            }
            else if (Model.E_CatalogType.Contains("TNPF"))
            {
                this.PicOne.Image = SpecialComm.LoadUrlImage(WebUrl + PicPath + "TNP/TNPF15.gif");
                this.PicOne.Tag = "TNPF15";
                this.PicTwo.Image = SpecialComm.LoadUrlImage(WebUrl + PicPath + "TNP/TNPF20.gif");
                this.PicTwo.Tag = "TNPF20";
                this.PicThree.Image = SpecialComm.LoadUrlImage(WebUrl + PicPath + "TNP/TNPF25.gif");
                this.PicThree.Tag = "TNPF25";
            }
            else if (Model.E_CatalogType.Contains("TNPS"))
            {
                this.PicOne.Image = SpecialComm.LoadUrlImage(WebUrl + PicPath + "TNP/TNPS2A.gif");
                this.PicOne.Tag = "TNPS2A";
                this.PicTwo.Image = SpecialComm.LoadUrlImage(WebUrl + PicPath + "TNP/TNPS10.gif");
                this.PicTwo.Tag = "TNPS10";
                this.PicThree.Visible = false;
            }
            this.txtModelType.Text = Model.E_CatalogType;
            panelShape1.Paint += new PaintEventHandler(panel_Paint);

            if (Model.MoldService.GetSpecialNum(Model.E_CatalogType, Model.Region) == "0")
                CommonHelper.GetControls(this.Controls);
        }

        private void PicOne_Click(object sender, EventArgs e)
        {
            PictureBox ShapePic = (PictureBox)sender;

            SpecialComm.ShowPanel(ShapePic, panelShape1);
            strShape1 = ShapePic.Tag.ToString();
            this.txtModelType.Text = strShape1;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (strShape1 != "")
            {
                Model.ModelType = this.txtModelType.Text;
                this.Hide();
                MoldLoad start = new MoldLoad(Model);
                start.Show();
            }
            else
            {
                MessageBox.Show(LM.SetLanguage("SpecialMsg"), "TNP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void panel_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            SpecialComm.DrawPanel(sender, e);
        }

        private void PicCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Model.Dispose();
            System.Environment.Exit(0);
        }
    }
}
