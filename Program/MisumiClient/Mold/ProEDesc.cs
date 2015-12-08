using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Misumi_Client.Common;

namespace Misumi_Client.Mold
{
    public partial class ProEDesc : Form
    {
        string ProEFlag = CommonHelper.MisumiDataPath + @"\ProE.msm";
        LanguageManager LM;
        public ProEDesc()
        {
            try
            {
                InitializeComponent();
                LM = new LanguageManager("MenuWin");
                LanguageManager.SetFont(this.Controls, 10);
                this.Text = LM.SetLanguage("ProETitle");
                this.lblProEContext.Text = LM.SetLanguage("lblProEContext");
                this.chkShow.Text = LM.SetLanguage("ChkTxt");
                this.btnAllDownLoad.Text = LM.SetLanguage("BtnAllDownLoad");
                this.btnOnly.Text = LM.SetLanguage("BtnOnly");
                this.btnCancel.Text = LM.SetLanguage("BtnCancel");
            }
            catch (Exception)
            {
            }
        }

        private void chkShow_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShow.Checked && !File.Exists(ProEFlag))//如果选中，并且没有标识文件
            {
                FileStream fs = File.Create(ProEFlag);
                fs.Dispose();
            }
            else if (!chkShow.Checked)
            {
                try
                {
                    File.Delete(ProEFlag);
                }
                catch (Exception)
                {
                    Thread.Sleep(1000);
                    File.Delete(ProEFlag);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
