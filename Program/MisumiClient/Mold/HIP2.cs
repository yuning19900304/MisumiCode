using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Misumi_Client.Common;

namespace Misumi_Client.Mold
{
    public partial class HIP2 : Form
    {
        string WebUrl = "";
        string strShape2 = "";
        LanguageManager LM;
        ModelObject Model;

        public HIP2(ModelObject model)
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


        Panel panelShape2 = new Panel();
        private void HIP2_Load(object sender, EventArgs e)
        {
            LanguageManager.SetFont(this.Controls, 9);
            this.lblType.Font = new Font("Arial", 14, FontStyle.Bold);
            this.txtModelType.Font = new Font("Arial", 14, FontStyle.Bold);
            this.Text = LM.SetLanguage("HIPTitle");
            this.HIPText2.Text = LM.SetLanguage("HIPText2");
            this.HIPStep2.Text = LM.SetLanguage("HIPStep2");

            this.lblRefer.Text = LM.SetLanguage("lblRefer");
            this.lblRefer.Font = new Font("Arial", 14, FontStyle.Bold);
            this.lblRefer.Location = new Point(this.lblType.Location.X + lblType.Width - lblRefer.Width, lblRefer.Location.Y);
            this.lblOldOrder.Text = Model.OldOrder;
            this.lblOldOrder.Font = new Font("Arial", 14, FontStyle.Bold);

            string PicPath = LM.SetLanguage("PicPath");
            this.txtModelType.Text = Model.E_CatalogType + "□□□";
            this.Pic0H.LoadAsync(WebUrl + PicPath + "HIP/0H.png");
            this.Pic4H.LoadAsync(WebUrl + PicPath + "HIP/4H.jpg");
            this.Pic6H.LoadAsync(WebUrl + PicPath + "HIP/6H.jpg");
            this.PicBiaoGe2.LoadAsync(WebUrl + PicPath + "HIP/biaoge2.jpg");
            this.PicBiaoGe3.LoadAsync(WebUrl + PicPath + "HIP/biaoge3.jpg");
            panelShape2.Paint += new PaintEventHandler(panel_Paint);
        }

        private void Pic4H_Click(object sender, EventArgs e)
        {
            PictureBox ShapePic = (PictureBox)sender;
            SpecialComm.ShowPanel(ShapePic, panelShape2);
            strShape2 = ShapePic.Tag.ToString();
            this.txtModelType.Text = Model.E_CatalogType + strShape2 + "□";
        }

        private void btn_Next_Click(object sender, EventArgs e)
        {
            if (strShape2 != "")
            {
                Model.E_CatalogType = Model.E_CatalogType + strShape2;
                this.Hide();
                HIP3 hip3 = new HIP3(Model);
                hip3.Show();
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
            Model.E_CatalogType = Model.E_CatalogType.Substring(0, Model.E_CatalogType.IndexOf('-') + 1);
            this.Hide();
            HIP hip = new HIP(Model);
            hip.Show();
        }
    }
}
