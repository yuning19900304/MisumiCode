using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Misumi_Client.Common;

namespace Misumi_Client.Mold
{
    public partial class NXDesc : Form
    {
        string NxFlag = CommonHelper.MisumiDataPath + @"\NX.msm";
        LanguageManager LM;
        public NXDesc()
        {
            try
            {
                InitializeComponent();
                LM = new LanguageManager("MenuWin");
                LanguageManager.SetFont(this.Controls, 10);
                this.Text = LM.SetLanguage("NxTitle");
                this.lblNxContext.Text = LM.SetLanguage("lblNxContext");
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
            if (chkShow.Checked && !File.Exists(NxFlag))//如果选中，并且没有标识文件
            {
                FileStream fs = File.Create(NxFlag);
                fs.Dispose();
            }
            else if (!chkShow.Checked)
            {
                try
                {
                    File.Delete(NxFlag);
                }
                catch (Exception)
                {
                    Thread.Sleep(1000);
                    File.Delete(NxFlag);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
