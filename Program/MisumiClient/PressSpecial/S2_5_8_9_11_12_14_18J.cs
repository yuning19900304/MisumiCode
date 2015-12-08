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
    public partial class S2_5_8_9_11_12_14_18J : Form
    {
        public string WebUrl = "";
        public string strShape1 = "";
        ModelObject Model;
        LanguageManager LM;
        Panel panelShape1 = new Panel();
        public S2_5_8_9_11_12_14_18J(ModelObject model)
        {
            Model = model;
            WebUrl = model.PressService.Url;
            WebUrl = WebUrl.Substring(0, WebUrl.LastIndexOf("/") + 1);
            InitializeComponent();
            LM = new LanguageManager("Special");
            this.PicCancel.Image = Image.FromFile(LM.SetLanguage("BtnBack"));
            this.btn_Next.Image = Image.FromFile(LM.SetLanguage("BtnNext"));
        }

        private void S2_5_8_9_11_12_14_18J_Load(object sender, EventArgs e)
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

            this.txtModelType.Text = Model.E_CatalogType.Replace("_J", "□□").Replace("(2-18)J", "□□");
            this.Pic2J.LoadAsync(WebUrl + PicPath + "18J/2J.jpg");
            this.Pic3J.LoadAsync(WebUrl + PicPath + "18J/3J.jpg");
            this.Pic4J.LoadAsync(WebUrl + PicPath + "18J/4J.jpg");
            this.Pic5J.LoadAsync(WebUrl + PicPath + "18J/5J.jpg");
            this.Pic8J.LoadAsync(WebUrl + PicPath + "18J/8J.jpg");
            this.Pic9J.LoadAsync(WebUrl + PicPath + "18J/9J.jpg");
            this.Pic11J.LoadAsync(WebUrl + PicPath + "18J/11J.jpg");
            this.Pic12J.LoadAsync(WebUrl + PicPath + "18J/12J.jpg");
            this.Pic14J.LoadAsync(WebUrl + PicPath + "18J/14J.jpg");
            this.Pic15J.LoadAsync(WebUrl + PicPath + "18J/15J.jpg");
            this.Pic16J.LoadAsync(WebUrl + PicPath + "18J/16J.jpg");
            this.Pic17J.LoadAsync(WebUrl + PicPath + "18J/17J.jpg");
            this.Pic18J.LoadAsync(WebUrl + PicPath + "18J/18J.jpg");

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
                MessageBox.Show(LM.SetLanguage("SpecialMsg"), "2-5_8_9_11_12_14-18J", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void Pic2J_Click(object sender, EventArgs e)
        {
            PictureBox ShapePic = (PictureBox)sender;
            SpecialComm.ShowPanel(ShapePic, panelShape1);
            strShape1 = ShapePic.Tag.ToString();
            this.txtModelType.Text = Model.E_CatalogType.Replace("-J", "").Replace("(2-18)J", "") + strShape1;
        }
    }
}
