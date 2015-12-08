using System;
using System.Windows.Forms;
using Misumi_Client.Common;

namespace Misumi_Client.Mold
{
    public partial class CADInfo : Form
    {
        LanguageManager LM;
        public CADInfo()
        {
            InitializeComponent();
            LM = new LanguageManager("CADInfo");
            LanguageManager.SetFont(this.Controls, 10);
        }

        private void CADInfo_Load(object sender, EventArgs e)
        {
            this.Text = LM.SetLanguage("Title");
            this.richTextBox1.LoadFile(Application.StartupPath + LM.SetLanguage("Content"), RichTextBoxStreamType.PlainText);
        }
    }
}
