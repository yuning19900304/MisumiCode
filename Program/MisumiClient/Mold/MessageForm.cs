using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Misumi_Client.Common;

namespace Misumi_Client.Mold
{
    public partial class MessageForm : Form
    {
        LanguageManager LM;
        string MSG1 = "";
        string MSG2 = "";
        string BtnContent = "";
        public MessageForm(string msg1, string msg2, string btnContent)
        {
            InitializeComponent();
            LM = new LanguageManager("MessageBox");
            MSG1 = msg1;
            MSG2 = msg2;
            BtnContent = btnContent;
        }

        private void MessageForm_Load(object sender, EventArgs e)
        {
            try
            {
                this.lblMsg1.Text = LM.SetLanguage(MSG1);
                this.lblMsg2.Text = LM.SetLanguage(MSG2);
                int FinalWidth = this.lblMsg1.Location.X + lblMsg1.Width + 30;//最终需要宽度=Msg横坐标+Msg长度+Msg具体右边边框的具体
                int SetupWidth = this.lblMsg2.Location.X + this.lblMsg2.Width + 30;//安装包名称需要宽度
                if (FinalWidth > pnMsg.Width)
                    this.Width = this.Width + (FinalWidth - pnMsg.Width);
                else
                    if (FinalWidth < SetupWidth)//并且最终需要宽度<安装包名称需要宽度，那么安装安装包名称的宽度为标准计算
                        this.Width = this.Width - (pnMsg.Width - SetupWidth);

                if (LanguageManager.ShowLan == LanguageManager.CurrentLanguage.JPN)
                {
                    this.btnInstall.Font = new Font("微软雅黑", 8);
                    this.btnCancel.Font = new Font("微软雅黑", 8);
                }
                this.Text = LM.SetLanguage("strMsgTitle");
                this.btnInstall.Text = LM.SetLanguage(BtnContent);
                this.btnCancel.Text = LM.SetLanguage("BtnCancel");
            }
            catch (Exception)
            {
            }
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            Process.Start("http://mex.misumi-ec.com/mex/GMW/GMWClient/MISUMI_CAD_Setup.exe");
            this.Close();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
