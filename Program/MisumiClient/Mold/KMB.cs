using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Misumi_Client.Common;

namespace Misumi_Client.Mold
{
    public partial class KMB : Form
    {
        public string WebUrl = "";
        public string strShape1 = "";
        LanguageManager LM;
        ModelObject Model;
        public KMB(ModelObject model)
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
        private void KMB_Load(object sender, EventArgs e)
        {
            LanguageManager.SetFont(this.Controls, 14, FontStyle.Bold);
            LanguageManager.SetFont(this.KMBText, 9);
            this.lblType.Font = new Font("Arial", 14, FontStyle.Bold);
            if (LanguageManager.ShowLan == LanguageManager.CurrentLanguage.EN)
            {
                LanguageManager.SetFont(this.lblPanFont, 9, FontStyle.Bold);
                LanguageManager.SetFont(this.lblChoice, 7, FontStyle.Bold);
            }
            else if (LanguageManager.ShowLan == LanguageManager.CurrentLanguage.KOR)
                LanguageManager.SetFont(this.lblChoice, 12, FontStyle.Bold);
            else if (LanguageManager.ShowLan == LanguageManager.CurrentLanguage.TAI)
            {
                LanguageManager.SetFont(this.lblPanFont, 10, FontStyle.Bold);
                LanguageManager.SetFont(this.lblChoice, 8, FontStyle.Bold);
            }
            this.txtModelType.Font = new Font("Arial", 14, FontStyle.Bold);
            this.KMBText.Text = LM.SetLanguage("KMBText");
            this.lblRefer.Text = LM.SetLanguage("lblRefer");
            this.lblRefer.Font = new Font("Arial", 14, FontStyle.Bold);
            this.lblRefer.Location = new Point(this.lblType.Location.X + lblType.Width - lblRefer.Width, lblRefer.Location.Y);
            this.lblOldOrder.Text = Model.OldOrder;
            this.lblOldOrder.Font = new Font("Arial", 14, FontStyle.Bold);
            string PicPath = LM.SetLanguage("PicPath");
            this.PicKMB.LoadAsync(WebUrl + PicPath + "KMB/KMB.jpg");
            this.lblPanFont.Text = LM.SetLanguage("lblPanFont");
            this.lblChoice.Text = LM.SetLanguage("lblChoice");
            string Mian = LM.SetLanguage("Mian");
            this.lbl1.Text = "1" + Mian;
            this.lbl2.Text = "2" + Mian;
            this.lbl3.Text = "3" + Mian;
            this.lblA.Text = "A" + Mian;
            this.lblB.Text = "B" + Mian;
            this.lblC.Text = "C" + Mian;
            this.lblAB.Text = "AB" + Mian;
            this.lblBC.Text = "BC" + Mian;
            this.lblAC.Text = "AC" + Mian;
            this.lblABC.Text = "ABC" + Mian;

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
                MessageBox.Show(LM.SetLanguage("SpecialMsg"), "KMB", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblA_Click(object sender, EventArgs e)
        {
            Label ShapeLbl = (Label)sender;

            ShowPanel(ShapeLbl, panelShape1);
            strShape1 = ShapeLbl.Tag.ToString();
            this.txtModelType.Text = Model.E_CatalogType + "-" + strShape1;
        }

        private void ShowPanel(Label ShapeLbl, Panel panel)
        {
            panel.Width = ShapeLbl.Width;
            panel.Height = ShapeLbl.Height;
            panel.BringToFront();
            ShapeLbl.Controls.Add(panel);
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
