using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Misumi_Client.Common;
using System.Web;

namespace Misumi_Client
{
    public partial class FormType : Form
    {
        public FormType()
        {
            InitializeComponent();
        }

        private void btnGMW_Click(object sender, EventArgs e)
        {
            StartForm start = new StartForm("GMW");
            start.Show();
            this.Hide();
        }

        private void btnGPW_Click(object sender, EventArgs e)
        {
            StartForm start = new StartForm("GPW");
            start.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            SendInfoToJap();
        }

        /// <summary>
        /// CAD下载履历回传
        /// </summary>
        private void SendInfoToJap()
        {
            StringBuilder sb = new StringBuilder();
            //sb.Append("?productName=" + Model.Series_Name);
            //sb.Append("&productId=" + Model.Series_Code);
            //sb.Append("&productPageUrl=" + Model.Page_Path);
            //sb.Append("&productImgUrl=" + Model.Main_Photo);
            //sb.Append("&partNumber=" + Model.NewOrder);
            //sb.Append("&makerCd=" + Model.Brd_Code);
            //sb.Append("&makerName=" + Model.Brd_Name);
            //sb.Append("&dl_format=" + dl_Format);


            //string sb1 = "";
            //sb1 += "?productName=";
            //sb1 += "&productId=10300000120";
            //sb1 += "&productPageUrl=http://jp.misumi-ec.com/ec/ItemDetail/10300000120.html";
            //sb1 += "&productImgUrl=http://jp.misumi-ec.com/item/10300000120/main.jpg";
            //sb1 += "&partNumber=SFJ3-10";
            //sb1 += "&makerCd=MSM";
            //sb1 += "&makerName=ミスミ";
            //sb1 += "&dl_format=STEP";


            string param = "";
            string productName = UrlEncode("シャフトストレート");
            string productId = UrlEncode("10300000120");
            string productPageUrl = UrlEncode("http://jp.misumi-ec.com/ec/ItemDetail/10300000120.html");
            string productImgUrl = UrlEncode("http://jp.misumi-ec.com/item/10300000120/main.jpg");
            string partNumber = UrlEncode("SFJ3-10");
            string makerCd = UrlEncode("MSM");
            string makerName = UrlEncode("ミスミ");
            string dl_format = UrlEncode("STEP");

            param = "?productName=" + productName + "&productId=" + productId + "&productPageUrl=" + productPageUrl + "&productImgUrl=" + productImgUrl + "&partNumber=" + partNumber + "&makerCd=" + makerCd + "&makerName=" + makerName + "&dl_format=" + dl_format;

            //string URL = Model.DoMain + CommonHelper.getSetConfig("ReturnURL");
            string URL = "http://jp.misumi-ec.com" + CommonHelper.getSetConfig("ReturnURL");

            //HttpWeb.Submit.GET(URL + param);
            HttpWeb.Submit.Post(URL, param);

            //WebBrowser web = new WebBrowser();
            //web.Navigate(URL + param);
            //HttpWeb.WriteFile(URL + param);
        }

        /// <summary>
        /// URL编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UrlEncode(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                StringBuilder sb = new StringBuilder();
                byte[] byStr = System.Text.Encoding.UTF8.GetBytes(str); //默认是System.Text.Encoding.Default.GetBytes(str)
                for (int i = 0; i < byStr.Length; i++)
                {
                    sb.Append(@"%" + Convert.ToString(byStr[i], 16));
                }
                return (sb.ToString());
            }
            else
                return "";
        }
    }
}
