using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Globalization;

namespace Misumi_Client.Common
{
    public class LanguageManager
    {
        public static CurrentLanguage ShowLan;
        public static XmlNode LanRoot;
        private XmlNodeList Nodes;

        /// <summary>
        /// 读取窗体的配置语言资源
        /// </summary>
        /// <param name="formName">窗体名称(xml文件节点一致)</param>
        public LanguageManager(string formName)
        {
            if (LanRoot != null)
            {
                XmlNode XmlForm = LanRoot.SelectSingleNode(formName);
                Nodes = XmlForm.SelectNodes("lan");
            }
        }

        /// <summary>
        /// 设置语言文字
        /// </summary>
        /// <param name="ContonlName"></param>
        /// <returns></returns>
        public string SetLanguage(string ContonlName)
        {
            foreach (XmlNode node in Nodes)
            {
                if (node.Attributes[0].Value == ContonlName)
                {
                    return node.Attributes[ShowLan.ToString()].Value;
                }
            }
            return "";
        }

        public static string GetLanguage(string WebLan)
        {
            string LanguageFlat = "";
            if (File.Exists(CommonHelper.MisumiDataPath + "Language.xml"))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(CommonHelper.MisumiDataPath + "Language.xml");
                XmlNode note = doc.SelectSingleNode("LanguageIndex");
                LanguageFlat = note.InnerXml;
            }
            else
            {
                if (!string.IsNullOrEmpty(WebLan))
                    LanguageFlat = WebLan;
                else
                {
                    if (CultureInfo.InstalledUICulture.NativeName.Contains("中文"))
                        LanguageFlat = "CHS";
                    else if (CultureInfo.InstalledUICulture.NativeName.Contains("日本"))
                        LanguageFlat = "JPN";
                    else if (CultureInfo.InstalledUICulture.NativeName.Contains("한국어"))
                        LanguageFlat = "KOR";
                    else if (CultureInfo.InstalledUICulture.NativeName.Contains("繁體"))
                        LanguageFlat = "CHT";
                    else
                        LanguageFlat = "EN";
                }
            }
            switch (LanguageFlat)
            {
                case "JPN":
                case "http://jp.misumi-ec.com":
                case "http://www.stg.misumi-ec.com":
                    LanguageManager.ShowLan = LanguageManager.CurrentLanguage.JPN;
                    break;
                case "CHS":
                case "http://cn.misumi-ec.com":
                    LanguageManager.ShowLan = LanguageManager.CurrentLanguage.CHS;
                    break;
                case "EN":
                case "http://us.misumi-ec.com":
                case "http://uk.misumi-ec.com":
                    LanguageManager.ShowLan = LanguageManager.CurrentLanguage.EN;
                    break;
                case "CHT":
                case "http://tw.misumi-ec.com":
                    LanguageManager.ShowLan = LanguageManager.CurrentLanguage.CHT;
                    break;
                case "KOR":
                case "http://kr.misumi-ec.com":
                    LanguageManager.ShowLan = LanguageManager.CurrentLanguage.KOR;
                    break;
                case "TAI":
                case "http://th.misumi-ec.com":
                    LanguageManager.ShowLan = LanguageManager.CurrentLanguage.TAI;
                    break;
                default:
                    LanguageManager.ShowLan = LanguageManager.CurrentLanguage.EN;
                    break;
            }
            return LanguageManager.ShowLan.ToString();
        }

        /// <summary>
        /// 多语言枚举
        /// </summary>
        public enum CurrentLanguage
        {
            CHS = 0,
            EN = 1,
            JPN = 2,
            CHT = 3,
            KOR = 4,
            TAI = 5
        }

        /// <summary>
        /// 设置窗体显示字体
        /// </summary>
        /// <param name="controls"></param>
        public static void SetFont(Control.ControlCollection controls, int FontSize)
        {
            SetControlsFont(controls, FontSize, FontStyle.Regular);
        }

        /// <summary>
        /// 设置窗体显示字体，是否加粗
        /// </summary>
        /// <param name="controls"></param>
        public static void SetFont(Control.ControlCollection controls, int FontSize, FontStyle style)
        {
            SetControlsFont(controls, FontSize, style);
        }

        /// <summary>
        /// 设置控件字体
        /// </summary>
        /// <param name="controls"></param>
        /// <param name="FontSize"></param>
        /// <param name="style"></param>
        public static void SetControlsFont(Control.ControlCollection controls, int FontSize, FontStyle style)
        {
            foreach (Control control in controls)
            {
                if (control is ComboBox)
                    continue;
                if (control.HasChildren)
                    SetFont(control.Controls, FontSize);
                else
                {
                    switch (ShowLan)
                    {
                        case CurrentLanguage.CHS:
                            control.Font = new Font(new FontFamily("SimHei"), FontSize, style);
                            break;
                        case CurrentLanguage.EN:
                            control.Font = new Font(new FontFamily("Arial"), FontSize, style);
                            break;
                        case CurrentLanguage.JPN:
                            control.Font = new Font(new FontFamily("MS PGothic"), FontSize, style);
                            break;
                        case CurrentLanguage.CHT:
                            control.Font = new Font(new FontFamily("MingLiU"), FontSize, style);
                            break;
                        case CurrentLanguage.KOR:
                            control.Font = new Font(new FontFamily("GulimChe"), FontSize, style);
                            break;
                        case CurrentLanguage.TAI:
                            control.Font = new Font(new FontFamily("Tahoma"), FontSize, style);
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 设置单个控件显示字体
        /// </summary>
        /// <param name="controls"></param>
        public static void SetFont(Control control, int FontSize)
        {
            SetControlFont(control, FontSize, FontStyle.Regular);
        }

        /// <summary>
        /// 设置单个控件显示字体
        /// </summary>
        /// <param name="controls"></param>
        public static void SetFont(Control control, int FontSize, FontStyle style)
        {
            SetControlFont(control, FontSize, style);
        }

        /// <summary>
        /// 设置单个控件的字体样式
        /// </summary>
        /// <param name="control"></param>
        /// <param name="FontSize"></param>
        private static void SetControlFont(Control control, int FontSize, FontStyle style)
        {
            switch (ShowLan)
            {
                case CurrentLanguage.CHS:
                    control.Font = new Font(new FontFamily("SimHei"), FontSize, style);
                    break;
                case CurrentLanguage.EN:
                    control.Font = new Font(new FontFamily("Arial"), FontSize, style);
                    break;
                case CurrentLanguage.JPN:
                    control.Font = new Font(new FontFamily("MS PGothic"), FontSize, style);
                    break;
                case CurrentLanguage.CHT:
                    control.Font = new Font(new FontFamily("MingLiU"), FontSize, style);
                    break;
                case CurrentLanguage.KOR:
                    control.Font = new Font(new FontFamily("GulimChe"), FontSize, style);
                    break;
                case CurrentLanguage.TAI:
                    control.Font = new Font(new FontFamily("Tahoma"), FontSize, style);
                    break;
            }
        }
    }
}
