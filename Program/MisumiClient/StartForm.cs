using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using Misumi_Client.Common;
using Misumi_Client.MoldWebService;
using Misumi_Client.Mold;
using Misumi_Client.Press;
using Misumi_Client.PressWebService;
using Misumi_Client.PressSpecial;

namespace Misumi_Client
{
    public partial class StartForm : Form
    {
        string FormType = "";
        public StartForm(string FormValue)
        {
            InitializeComponent();
            FormType = FormValue;
            cboDoMain.SelectedIndex = 0;
        }
        
        MisumiWebService GMWS = new MisumiWebService();
        GlobalPressWebService GPWS = new GlobalPressWebService();
        /// <summary>
        /// 点击进入按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count > 0)
            {
                string modeltype = this.dataGridView1.SelectedRows[0].Cells[2].Value.ToString().Trim();
                string ClassName = this.dataGridView1.SelectedRows[0].Cells[1].Value.ToString().Trim();

                ModelObject Model = new ModelObject();
                Model.E_CatalogType = modeltype;
                Model.ModelType = modeltype.ToUpper();
                Model.OldOrder = textBox1.Text.Trim();
                Model.UserName = textBox2.Text.Trim();
                Model.ClassName = ClassName;
                Model.Region = Program.ReturnRegion(cboDoMain.SelectedItem.ToString());//公司简称
                
                XmlDocument doc = new XmlDocument();
                doc.Load("Language.xml");
                LanguageManager.LanRoot = doc.SelectSingleNode("LanguageLibrary");
                Model.ShowLanguage = LanguageManager.GetLanguage(cboDoMain.Text);
                //Form start = null;
                if (FormType == "GMW")
                {
                    Model.MoldService = GMWS;
                    MoldSpecialHandle msh = new MoldSpecialHandle(Model);
                    msh.ReturnFormBySpecailType().Show();
                }
                else
                {
                    Model.PressService = GPWS;
                    PressSpecialHandle psh = new PressSpecialHandle(Model);
                    psh.ReturnFormBySpecailType().Show();
                }
            }
            else
                MessageBox.Show("请选择一行数据，作为你要测试的对象！");
        }
        DataTable dt;
        private void StartForm_Load(object sender, EventArgs e)
        {
            if (FormType == "GMW")
            {
                this.cbo1.DataSource = GMWS.GetClassAll();
                dt = GMWS.GetAll();
            }
            else
            {
                this.cbo1.DataSource = GPWS.GetClassAll();
                dt = GPWS.GetAll();
            }
            if (dt != null)
            {
                dt.Rows[0].Delete();
                this.dataGridView1.DataSource = dt;
            }
        }

        /// <summary>
        /// 
        /// 点击查询按钮，通过大类查询大类下的小类
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            DataRow[] row = dt.Select("o like '" + txtType.Text.ToUpper() + "%'");
            DataTable dt1 = new DataTable();
            dt1 = dt.Clone();
            for (int i = 0; i < row.Length; i++)
            {
                dt1.ImportRow((DataRow)row[i]);
            }
            this.dataGridView1.DataSource = dt1;
        }

        /// <summary>
        /// datagridview每行显示序号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dataGridView1.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString(Convert.ToString(e.RowIndex + 1, System.Globalization.CultureInfo.CurrentUICulture),
                e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }

        private void StartForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                DataRow[] row = dt.Select("o like '" + txtType.Text.ToUpper() + "%'");
                DataTable dt1 = new DataTable();
                dt1 = dt.Clone();
                for (int i = 0; i < row.Length; i++)
                {
                    dt1.ImportRow((DataRow)row[i]);
                }
                this.dataGridView1.DataSource = dt1;
            }
        }

        private void cbo1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dt != null)
            {
                DataRow[] row = dt.Select("e like '%" + cbo1.Text + "%'");
                DataTable dt1 = new DataTable();
                dt1 = dt.Clone();
                for (int i = 0; i < row.Length; i++)
                {
                    dt1.ImportRow((DataRow)row[i]);
                }
                this.dataGridView1.DataSource = dt1;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.dataGridView1.CurrentRow.Selected = true;
        }
    }
}
