using System;
using System.Drawing;
using System.Windows.Forms;
using Misumi_Client.Common;
using Misumi_Client.Press;

namespace Misumi_Client.PressSpecial
{
    public partial class S3_28K_T : Form
    {
        public string WebUrl = "";
        public string strShape1 = "";
        ModelObject Model;
        LanguageManager LM;
        public S3_28K_T(ModelObject model)
        {
            Model = model;
            WebUrl = model.PressService.Url;
            WebUrl = WebUrl.Substring(0, WebUrl.LastIndexOf("/") + 1);
            InitializeComponent();
            LM = new LanguageManager("Special");
            this.PicCancel.Image = Image.FromFile(LM.SetLanguage("BtnBack"));
            this.btn_Next.Image = Image.FromFile(LM.SetLanguage("BtnNext"));
        }

        Panel panelShape1 = new Panel();
        private void S3_28K_T_Load(object sender, EventArgs e)
        {
            LanguageManager.SetFont(this.Controls, 9);
            this.txtModelType.Font = new Font("Arial", 14, FontStyle.Bold);
            this.MNTText.Text = LM.SetLanguage("MN10LText");
            this.lblType.Font = new Font("Arial", 14, FontStyle.Bold);
            this.lblType.Text = LM.SetLanguage("lblType");
            string PicPath = LM.SetLanguage("PicPath");
            this.lblRefer.Text = LM.SetLanguage("lblRefer");
            this.lblRefer.Font = new Font("Arial", 14, FontStyle.Bold);
            this.lblRefer.Location = new Point(this.lblType.Location.X + lblType.Width - lblRefer.Width, lblRefer.Location.Y);
            this.lblOldOrder.Text = Model.OldOrder;
            this.lblOldOrder.Font = new Font("Arial", 14, FontStyle.Bold);

            this.txtModelType.Text = Model.E_CatalogType.Replace("(3-28)", "□");
            this.Pic3K.LoadAsync(WebUrl + PicPath + "S27_29K/3K.jpg");
            this.Pic4K.LoadAsync(WebUrl + PicPath + "S27_29K/4K.jpg");
            this.Pic5K.LoadAsync(WebUrl + PicPath + "S27_29K/5K.jpg");
            this.Pic6K.LoadAsync(WebUrl + PicPath + "S27_29K/6K.jpg");
            this.Pic8K.LoadAsync(WebUrl + PicPath + "S27_29K/8K.jpg");
            this.Pic9K.LoadAsync(WebUrl + PicPath + "S27_29K/9K.jpg");
            this.Pic10K.LoadAsync(WebUrl + PicPath + "S27_29K/10K.jpg");
            this.Pic13K.LoadAsync(WebUrl + PicPath + "S27_29K/13K.jpg");
            this.Pic14K.LoadAsync(WebUrl + PicPath + "S27_29K/14K.jpg");
            this.Pic15K.LoadAsync(WebUrl + PicPath + "S27_29K/15K.jpg");
            this.Pic17K.LoadAsync(WebUrl + PicPath + "S27_29K/17K.jpg");
            this.Pic18K.LoadAsync(WebUrl + PicPath + "S27_29K/18K.jpg");
            this.Pic20K.LoadAsync(WebUrl + PicPath + "S27_29K/20K.jpg");
            this.Pic21K.LoadAsync(WebUrl + PicPath + "S27_29K/21K.jpg");
            this.Pic22K.LoadAsync(WebUrl + PicPath + "S27_29K/22K.jpg");
            this.Pic27K.LoadAsync(WebUrl + PicPath + "S27_29K/27K.jpg");
            this.Pic28K.LoadAsync(WebUrl + PicPath + "S27_29K/28K.jpg");
            

            panelShape1.Paint += new PaintEventHandler(panel_Paint);
        }

        private void btn_Next_Click(object sender, EventArgs e)
        {
            if (strShape1 != "")
            {
                Model.ModelType = this.txtModelType.Text;
                this.Hide();
                PressLoad start = new PressLoad(Model);
                start.Show();
            }
            else
            {
                MessageBox.Show(LM.SetLanguage("SpecialMsg"), "3-28K-T", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void Pic3K_Click(object sender, EventArgs e)
        {
            PictureBox ShapePic = (PictureBox)sender;
            SpecialComm.ShowPanel(ShapePic, panelShape1);
            strShape1 = ShapePic.Tag.ToString();
            this.txtModelType.Text = Model.E_CatalogType.Replace("(3-28)K", "") + strShape1;
        }
    }
}
