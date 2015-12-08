using System;
using System.Drawing;
using System.Windows.Forms;
using Misumi_Client.Common;
using Misumi_Client.Press;

namespace Misumi_Client.PressSpecial
{
    public partial class SpunchD_L : Form
    {
        public string WebUrl = "";
        public string strShape1 = "";
        ModelObject Model;
        LanguageManager LM;
        public SpunchD_L(ModelObject model)
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
        private void SpunchD_L_Load(object sender, EventArgs e)
        {
            LanguageManager.SetFont(this.Controls, 9);
            this.txtModelType.Font = new Font("Arial", 14, FontStyle.Bold);
            this.punchD_LTitle.Text = LM.SetLanguage("punchD_LTitle");
            this.lblType.Font = new Font("Arial", 14, FontStyle.Bold);
            this.lblType.Text = LM.SetLanguage("lblType");
            string PicPath = LM.SetLanguage("PicPath");
            this.lblRefer.Text = LM.SetLanguage("lblRefer");
            this.lblRefer.Font = new Font("Arial", 14, FontStyle.Bold);
            this.lblRefer.Location = new Point(this.lblType.Location.X + lblType.Width - lblRefer.Width, lblRefer.Location.Y);
            this.lblOldOrder.Text = Model.OldOrder;
            this.lblOldOrder.Font = new Font("Arial", 14, FontStyle.Bold);

            this.txtModelType.Text = Model.E_CatalogType;
            this.Pic14.LoadAsync(WebUrl + PicPath + "SPunch/punchD14.jpg");
            this.Pic21.LoadAsync(WebUrl + PicPath + "SPunch/punchD21.jpg");

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
                MessageBox.Show(LM.SetLanguage("SpecialMsg"), "punchD_L", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void panel_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            SpecialComm.DrawPanel(sender, e);
        }

        private void Pic14_Click(object sender, EventArgs e)
        {
            PictureBox ShapePic = (PictureBox)sender;
            SpecialComm.ShowPanel(ShapePic, panelShape1);
            strShape1 = ShapePic.Tag.ToString();
            this.txtModelType.Text = Model.E_CatalogType.Replace("□1", strShape1);
        }

        private void PicCancel_Click(object sender, EventArgs e)
        {
            Model.E_CatalogType = Model.ModelType;//把统合Web类型，重新赋值
            this.Hide();
            SpunchD punchD = new SpunchD(Model);
            punchD.Show();
        }
    }
}
