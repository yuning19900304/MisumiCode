using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Misumi_Client.Common;

namespace Misumi_Client.Mold
{
    public partial class HIP3 : Form
    {
        string WebUrl = "";
        string strShape3 = "";
        LanguageManager LM;
        ModelObject Model;
        public HIP3(ModelObject model)
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

        Panel panelShape3 = new Panel();
        private void HIP3_Load(object sender, EventArgs e)
        {
            LanguageManager.SetFont(this.Controls, 9);
            this.txtModelType.Font = new Font("Arial", 14, FontStyle.Bold);
            this.lblType.Font = new Font("Arial", 14, FontStyle.Bold);
            this.Text = LM.SetLanguage("HIPTitle");
            this.HIPText3.Text = LM.SetLanguage("HIPText3");
            this.HIPStep3.Text = LM.SetLanguage("HIPStep3");
            this.lblRefer.Text = LM.SetLanguage("lblRefer");
            this.lblRefer.Font = new Font("Arial", 14, FontStyle.Bold);
            this.lblRefer.Location = new Point(this.lblType.Location.X + lblType.Width - lblRefer.Width, lblRefer.Location.Y);
            this.lblOldOrder.Text = Model.OldOrder;
            this.lblOldOrder.Font = new Font("Arial", 14, FontStyle.Bold);
            string PicPath = LM.SetLanguage("PicPath");
            this.txtModelType.Text = Model.E_CatalogType + "□";
            this.PicT.LoadAsync(WebUrl + PicPath + "HIP/T.jpg");
            this.PicU.LoadAsync(WebUrl + PicPath + "HIP/U.jpg");
            this.PicN.LoadAsync(WebUrl + PicPath + "HIP/N.jpg");
            this.HIPText3_1.Text = LM.SetLanguage("HIPText3_1");
            panelShape3.Paint += new PaintEventHandler(panel_Paint);
        }

        private void PicT_Click(object sender, EventArgs e)
        {
            PictureBox ShapePic = (PictureBox)sender;
            SpecialComm.ShowPanel(ShapePic, panelShape3);
            strShape3 = ShapePic.Tag.ToString();
            this.txtModelType.Text = Model.E_CatalogType + strShape3;
        }

        private void btn_Next_Click(object sender, EventArgs e)
        {
            if (strShape3 != "")
            {
                Model.ModelType = this.txtModelType.Text;
                this.Hide();
                MoldLoad start = new MoldLoad(Model);
                start.Show();
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
            Model.E_CatalogType = Model.E_CatalogType.Substring(0, Model.E_CatalogType.Length - 2);
            this.Hide();
            HIP2 hip2 = new HIP2(Model);
            hip2.Show();
        }
    }
}
