using System;
using System.Drawing;
using System.Windows.Forms;
using Misumi_Client.Common;
using Misumi_Client.Press;

namespace Misumi_Client.PressSpecial
{
    public partial class S2_6A : Form
    {
        public string WebUrl = "";
        public string strShape1 = "";
        ModelObject Model;
        LanguageManager LM;
        public S2_6A(ModelObject model)
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
        private void S2_6A_Load(object sender, EventArgs e)
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

            this.txtModelType.Text = Model.E_CatalogType.Replace("(2-6)", "□").Replace("(2-4･6)", "□");
            this.Pic2A.LoadAsync(WebUrl + PicPath + "S2_6A/2A.jpg");
            this.Pic3A.LoadAsync(WebUrl + PicPath + "S2_6A/3A.jpg");
            this.Pic4A.LoadAsync(WebUrl + PicPath + "S2_6A/4A.jpg");
            this.Pic6A.LoadAsync(WebUrl + PicPath + "S2_6A/6A.jpg");

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
                MessageBox.Show(LM.SetLanguage("SpecialMsg"), "2-6A", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void Pic2A_Click(object sender, EventArgs e)
        {
            PictureBox ShapePic = (PictureBox)sender;
            SpecialComm.ShowPanel(ShapePic, panelShape1);
            strShape1 = ShapePic.Tag.ToString();
            this.txtModelType.Text = Model.E_CatalogType.Replace("(2-6)A", "").Replace("(2-4･6)A", "") + strShape1;
        }
    }
}
