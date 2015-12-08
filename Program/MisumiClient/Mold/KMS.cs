using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Misumi_Client.Common;

namespace Misumi_Client.Mold
{
    public partial class KMS : Form
    {
        public string WebUrl = "";
        public string strShape1 = "";
        ModelObject Model;
        LanguageManager LM;
        public KMS(ModelObject model)
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
        private void KMS_Load(object sender, EventArgs e)
        {
            LanguageManager.SetFont(this.Controls, 9);
            this.txtModelType.Font = new Font("Arial", 14, FontStyle.Bold);
            this.lblType.Font = new Font("Arial", 14, FontStyle.Bold);
            this.KMSText.Text = LM.SetLanguage("KMBText");
            this.lblRefer.Text = LM.SetLanguage("lblRefer");
            this.lblRefer.Font = new Font("Arial", 14, FontStyle.Bold);
            this.lblRefer.Location = new Point(this.lblType.Location.X + lblType.Width - lblRefer.Width, lblRefer.Location.Y);
            this.lblOldOrder.Text = Model.OldOrder;
            this.lblOldOrder.Font = new Font("Arial", 14, FontStyle.Bold);
            string PicPath = LM.SetLanguage("PicPath");
            this.PicKMS.LoadAsync(WebUrl + PicPath + "KMS/KMS.jpg");
            this.PicL.LoadAsync(WebUrl + PicPath + "KMS/L.jpg");
            this.PicK.LoadAsync(WebUrl + PicPath + "KMS/K.jpg");

            this.txtModelType.Text = Model.E_CatalogType;
            panelShape1.Paint += new PaintEventHandler(panel_Paint);
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
                MessageBox.Show(LM.SetLanguage("SpecialMsg"), "KMS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PicL_Click(object sender, EventArgs e)
        {
            PictureBox ShapePic = (PictureBox)sender;

            ShowPanel(ShapePic, panelShape1);
            strShape1 = ShapePic.Tag.ToString();
            this.txtModelType.Text = Model.E_CatalogType + "-" + strShape1;
        }


        private void ShowPanel(PictureBox ShapePic, Panel panel)
        {
            panel.Width = ShapePic.Width;
            if (ShapePic.Tag.ToString() == "L")
            {
                panel.Height = ShapePic.Height - 18;
            }
            else
            {
                panel.Height = ShapePic.Height - 34;
            }
            panel.BringToFront();
            ShapePic.Controls.Add(panel);
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
