using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Misumi_Client.Common;
using Misumi_Client.Press;

namespace Misumi_Client.PressSpecial
{
    public partial class S2_10C : Form
    {
        public string WebUrl = "";
        public string strShape1 = "";
        ModelObject Model;
        LanguageManager LM;
        Panel panelShape1 = new Panel();
        public S2_10C(ModelObject model)
        {
            Model = model;
            WebUrl = model.PressService.Url;
            WebUrl = WebUrl.Substring(0, WebUrl.LastIndexOf("/") + 1);
            InitializeComponent();
            LM = new LanguageManager("Special");
            this.PicCancel.Image = Image.FromFile(LM.SetLanguage("BtnBack"));
            this.btn_Next.Image = Image.FromFile(LM.SetLanguage("BtnNext"));
        }

        private void S2_10C_Load(object sender, EventArgs e)
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

            this.txtModelType.Text = Model.E_CatalogType.Replace("(2-10)C", "□□");
            this.Pic2C.LoadAsync(WebUrl + PicPath + "2_10C/2C.jpg");
            this.Pic3C.LoadAsync(WebUrl + PicPath + "2_10C/3C.jpg");
            this.Pic4C.LoadAsync(WebUrl + PicPath + "2_10C/4C.jpg");
            this.Pic5C.LoadAsync(WebUrl + PicPath + "2_10C/5C.jpg");
            this.Pic6C.LoadAsync(WebUrl + PicPath + "2_10C/6C.jpg");
            this.Pic7C.LoadAsync(WebUrl + PicPath + "2_10C/7C.jpg");
            this.Pic8C.LoadAsync(WebUrl + PicPath + "2_10C/8C.jpg");
            this.Pic9C.LoadAsync(WebUrl + PicPath + "2_10C/9C.jpg");
            this.Pic10C.LoadAsync(WebUrl + PicPath + "2_10C/10C.jpg");

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
                MessageBox.Show(LM.SetLanguage("SpecialMsg"), "2-10C", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void Pic2C_Click(object sender, EventArgs e)
        {
            PictureBox ShapePic = (PictureBox)sender;
            SpecialComm.ShowPanel(ShapePic, panelShape1);
            strShape1 = ShapePic.Tag.ToString();
            this.txtModelType.Text = Model.E_CatalogType.Replace("(2-10)C", "") + strShape1;
        }
    }
}
