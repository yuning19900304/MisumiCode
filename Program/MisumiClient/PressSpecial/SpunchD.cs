using System;
using System.Drawing;
using System.Windows.Forms;
using Misumi_Client.Common;
using Misumi_Client.Press;
using System.Collections.Generic;

namespace Misumi_Client.PressSpecial
{
    public partial class SpunchD : Form
    {
        public string WebUrl = "";
        public string strShape1 = "";
        ModelObject Model;
        LanguageManager LM;
        public SpunchD(ModelObject model)
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
        private void SpunchD_Load(object sender, EventArgs e)
        {
            LanguageManager.SetFont(this.Controls, 9);
            this.txtModelType.Font = new Font("Arial", 14, FontStyle.Bold);
            this.punchDTitle.Text = LM.SetLanguage("punchDTitle");
            this.lblType.Font = new Font("Arial", 14, FontStyle.Bold);
            this.lblType.Text = LM.SetLanguage("lblType");
            string PicPath = LM.SetLanguage("PicPath");
            this.lblRefer.Text = LM.SetLanguage("lblRefer");
            this.lblRefer.Font = new Font("Arial", 14, FontStyle.Bold);
            this.lblRefer.Location = new Point(this.lblType.Location.X + lblType.Width - lblRefer.Width, lblRefer.Location.Y);
            this.lblOldOrder.Text = Model.OldOrder;
            this.lblOldOrder.Font = new Font("Arial", 14, FontStyle.Bold);
            this.lstNum.Font = new Font("Arial", 12, FontStyle.Bold);
            this.lblD.Font = new Font("Arial", 12);

            Model.ModelType = Model.E_CatalogType;//保存一下Web类型，用户返回本页用
            this.txtModelType.Text = Model.E_CatalogType.Replace("(4-7)", "□").Replace("(1-7)", "□").Replace("(6-7)", "□").Replace("(2-7)", "□").Replace("-", "□");
            this.Pic2B.LoadAsync(WebUrl + PicPath + "SPunch/punchD.jpg");

            BindListBox();
        }

        List<string> listItem = new List<string>();
        Dictionary<string, string> PunchList = new Dictionary<string, string>();
        private void BindListBox()
        {
            string[] punchs = Model.PressService.GetPunchList(Model.E_CatalogType);
            for (int i = 0; i < punchs.Length; i++)
            {
                string[] Values = punchs[i].Split('@');
                if (Values.Length == 2)
                {
                    listItem.Add(Values[0]);
                    if (!PunchList.ContainsKey(Values[0]))//有重复4现象
                        PunchList.Add(Values[0], Values[1]);
                }
            }

            lstNum.DataSource = listItem;
        }

        private void btn_Next_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtModelType.Text))
            {
                MessageBox.Show(LM.SetLanguage("SpecialMsg"), "punchD", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (this.txtModelType.Text.Contains("□") && !this.txtModelType.Text.Contains("AVJ"))//如果包含方框并且也不是AVJ
            {
                Model.E_CatalogType = this.txtModelType.Text;
                this.Hide();
                SpunchD_L punchD_L = new SpunchD_L(Model);
                punchD_L.Show();
            }
            else if (this.txtModelType.Text.Contains("□") && this.txtModelType.Text.Contains("AVJ"))//如果包含方框并且是AVJ
            {
                Model.E_CatalogType = this.txtModelType.Text;
                this.Hide();
                SpunchL_R punchL_R = new SpunchL_R(Model);
                punchL_R.Show();
            }
            else
            {
                Model.ModelType = this.txtModelType.Text;
                this.Hide();
                PressLoad start = new PressLoad(Model);
                start.Show();
            }
        }

        private void PicCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Model.Dispose();
            System.Environment.Exit(0);
        }

        private void lstNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox box = (ListBox)sender;
            if (box.SelectedValue.Equals("4"))
            {
                this.txtModelType.Text = Model.E_CatalogType.Replace("(L)", "").Replace("(4-7)", "□1").Replace("(1-7)", "□1").Replace("(6-7)", "□1").Replace("(2-7)", "□1").Replace("(", "□").Replace(")", "");
            }
            else if (Model.E_CatalogType.Contains("AVJ") && box.SelectedValue.Equals("25"))
            {
                this.txtModelType.Text = Model.E_CatalogType.Replace("-", "□");
            }
            else
                this.txtModelType.Text = PunchList[box.SelectedValue.ToString()];
        }
    }
}
