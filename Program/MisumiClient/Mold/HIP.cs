using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Misumi_Client.Common;

namespace Misumi_Client.Mold
{
    public partial class HIP : Form
    {
        string WebUrl = "";
        string strShape1 = "";
        ModelObject Model;
        LanguageManager LM;

        public HIP(ModelObject model)
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
        private void HIP_Load(object sender, EventArgs e)
        {
            LanguageManager.SetFont(this.Controls, 9);
            this.lblType.Font = new Font("Arial", 14, FontStyle.Bold);
            this.txtModelType.Font = new Font("Arial", 14, FontStyle.Bold);
            this.Text = LM.SetLanguage("HIPTitle");
            this.HIPText1.Text = LM.SetLanguage("HIPText1");
            this.HIPStep1.Text = LM.SetLanguage("HIPStep1");

            this.lblRefer.Text = LM.SetLanguage("lblRefer");
            this.lblRefer.Font = new Font("Arial", 14, FontStyle.Bold);
            this.lblRefer.Location = new Point(this.lblType.Location.X + lblType.Width - lblRefer.Width, lblRefer.Location.Y);
            this.lblOldOrder.Text = Model.OldOrder;
            this.lblOldOrder.Font = new Font("Arial", 14, FontStyle.Bold);

            string PicPath = LM.SetLanguage("PicPath");
            this.txtModelType.Text = Model.E_CatalogType;
            this.Pic0D.LoadAsync(WebUrl + PicPath + "HIP/0D.jpg");
            this.Pic1D.LoadAsync(WebUrl + PicPath + "HIP/1D.jpg");
            this.Pic2D.LoadAsync(WebUrl + PicPath + "HIP/2D.jpg");
            this.Pic2DA.LoadAsync(WebUrl + PicPath + "HIP/2DA.jpg");
            this.Pic3D.LoadAsync(WebUrl + PicPath + "HIP/3D.jpg");
            this.Pic3DA.LoadAsync(WebUrl + PicPath + "HIP/3DA.jpg");
            this.Pic4DA.LoadAsync(WebUrl + PicPath + "HIP/4DA.jpg");
            this.Pic4DB.LoadAsync(WebUrl + PicPath + "HIP/4DB.jpg");
            this.Pic5DA.LoadAsync(WebUrl + PicPath + "HIP/5DA.jpg");
            this.Pic5DB.LoadAsync(WebUrl + PicPath + "HIP/5DB.jpg");
            this.PicBiaoGe1.LoadAsync(WebUrl + PicPath + "HIP/biaoge1.jpg");
            panelShape1.Paint += new PaintEventHandler(panel_Paint);
        }

        private void Pic0D_Click(object sender, EventArgs e)
        {
            PictureBox ShapePic = (PictureBox)sender;
            SpecialComm.ShowPanel(ShapePic, panelShape1);
            strShape1 = ShapePic.Tag.ToString();
            this.txtModelType.Text = Model.E_CatalogType + strShape1 + "□□□";
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (strShape1 != "")
            {
                Model.E_CatalogType = Model.E_CatalogType + strShape1;
                this.Hide();
                HIP2 hip2 = new HIP2(Model);
                hip2.Show();
            }
            else
            {
                MessageBox.Show(LM.SetLanguage("SpecialMsg"), "HIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
