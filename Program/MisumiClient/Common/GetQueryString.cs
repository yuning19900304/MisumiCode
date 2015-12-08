using System;
using System.Collections.Specialized;
using System.Deployment.Application;
using System.Web;
using System.Reflection;
using System.IO;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Text;

namespace Misumi_Client.Common
{
    /// <summary>
    /// 解析URL传递参数
    /// </summary>
    public class GetQueryString
    {
        /// <summary>
        /// 返回URL键值对参数
        /// </summary>
        /// <returns></returns>
        public NameValueCollection GetQueryStringParameters()
        {
            NameValueCollection namevalueTable = new NameValueCollection();
            string queryString = "";
            if (ApplicationDeployment.IsNetworkDeployed)//如果是ClickOnce应用程序
            {
                Uri uri = ApplicationDeployment.CurrentDeployment.ActivationUri;
                if (uri != null)
                {
                    Program.UrlPath = uri.ToString().Trim();
                    queryString = uri.Query;//获取URi查询信息
                    namevalueTable = HttpUtility.ParseQueryString(queryString);
                    StringBuilder sb = new StringBuilder();
                    sb.Append("URL参数：" + queryString);
                    sb.AppendLine("——————————————————————————————————————————————————————————————————————————————————————————————————————————————");
                    CommonHelper.WriteAcisValue(sb.ToString());
                }
            }
            return namevalueTable;
        }

        /// <summary>
        /// 设置添加删除程序图标
        /// </summary>
        public void SetAddRemoveProgramsIcon()
        {
            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed && !File.Exists(CommonHelper.AppPath + "Ico.msm"))
            {
                try
                {
                    Assembly code = Assembly.GetExecutingAssembly();
                    AssemblyDescriptionAttribute asdescription =
                     (AssemblyDescriptionAttribute)Attribute.GetCustomAttribute(code, typeof(AssemblyDescriptionAttribute));
                    string assemblyDescription = asdescription.Description;
                    //the icon is included in this program
                    string iconSourcePath = Path.Combine(Application.StartupPath, "GMW.ico");
                    if (!File.Exists(iconSourcePath))
                        return;

                    RegistryKey myUninstallKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Uninstall");
                    string[] mySubKeyNames = myUninstallKey.GetSubKeyNames();
                    for (int i = 0; i < mySubKeyNames.Length; i++)
                    {
                        RegistryKey myKey = myUninstallKey.OpenSubKey(mySubKeyNames[i], true);
                        object ShortcutFileName = myKey.GetValue("ShortcutFileName");
                        if (ShortcutFileName != null && ShortcutFileName.ToString() == assemblyDescription)//匹配当前程序的说明
                        {
                            myKey.SetValue("DisplayIcon", iconSourcePath);//修改控制面板程序列表的图标
                            File.Create(CommonHelper.AppPath + "Ico.msm");
                            break;
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        /// <summary>
        /// 删除开始菜单组
        /// </summary>
        public void RemoveStartMenu()
        {
            string ProgramPath = Environment.GetFolderPath(Environment.SpecialFolder.Programs);
            if (Directory.Exists(Path.Combine(ProgramPath, "MISUMI")))
            {
                Directory.Delete(Path.Combine(ProgramPath, "MISUMI"), true);
            }
        }
    }
}
