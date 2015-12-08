using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using Misumi_Client.Common;
using Misumi_Client.MoldWebService;
using Misumi_Client.Mold;
using Misumi_Client.PressWebService;
using Misumi_Client.PressSpecial;

namespace Misumi_Client
{
    static class Program
    {
        public static string UrlPath = "";
        private static bool IsGMW = false;
        private static MisumiWebService GMWS = new MisumiWebService();
        private static GlobalPressWebService GPWS = new GlobalPressWebService();
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ModelObject modelObject = new ModelObject();
            try
            {
                Process process = RuningInstance();
                if (process != null)//如果存在相同的进程，结束掉之前的进程，
                {
                    process.Kill();
                }

                GetQueryString gqs = new GetQueryString();
                gqs.SetAddRemoveProgramsIcon();
                gqs.RemoveStartMenu();
                NameValueCollection nvc = gqs.GetQueryStringParameters();

                XmlDocument doc = new XmlDocument();
                doc.Load("Language.xml");
                LanguageManager.LanRoot = doc.SelectSingleNode("LanguageLibrary");

                //CommonHelper.WriteAcisValue("NoMexType测试：nvc.Count=" + nvc.Count);

                if (nvc.Count == 12)//接收网页传递的参数
                {
                    //CommonHelper.WriteAcisValue("NoMexType测试：接收网页传递的参数");
                    modelObject.Product_ID = nvc.Get(0);
                    modelObject.UserName = nvc.Get(1);
                    modelObject.OldOrder = nvc.Get(2);//参数pn
                    modelObject.Main_Photo = nvc.Get(3);
                    modelObject.Page_Path = nvc.Get(4);
                    modelObject.Brd_Code = nvc.Get(5);
                    modelObject.Brd_Name = nvc.Get(6);
                    modelObject.Series_Code = nvc.Get(7);
                    modelObject.Series_Name = nvc.Get(8);
                    modelObject.E_CatalogType = nvc.Get(9);//统合网站类型
                    modelObject.DoMain = nvc.Get(10);
                    modelObject.Region = ReturnRegion(modelObject.DoMain.ToLower());//公司简称

                    //CommonHelper.WriteAcisValue("NoMexType测试：modelObject.Series_Code=" + modelObject.Series_Code);
                    //CommonHelper.WriteAcisValue("NoMexType测试：modelObject.Product_ID=" + modelObject.Product_ID);
                    //CommonHelper.WriteAcisValue("NoMexType测试：modelObject.UserName=" + modelObject.UserName);
                    //CommonHelper.WriteAcisValue("NoMexType测试：modelObject.OldOrder=" + modelObject.OldOrder);
                    //CommonHelper.WriteAcisValue("NoMexType测试：modelObject.Main_Photo=" + modelObject.Main_Photo);
                    //CommonHelper.WriteAcisValue("NoMexType测试：modelObject.Page_Path=" + modelObject.Page_Path);
                    //CommonHelper.WriteAcisValue("NoMexType测试：modelObject.Brd_Code=" + modelObject.Brd_Code);
                    //CommonHelper.WriteAcisValue("NoMexType测试：modelObject.Brd_Name=" + modelObject.Brd_Name);
                    //CommonHelper.WriteAcisValue("NoMexType测试：modelObject.Series_Code=" + modelObject.Series_Code);
                    //CommonHelper.WriteAcisValue("NoMexType测试：modelObject.Series_Name=" + modelObject.Series_Name);
                    //CommonHelper.WriteAcisValue("NoMexType测试：modelObject.E_CatalogType=" + modelObject.E_CatalogType);
                    //CommonHelper.WriteAcisValue("NoMexType测试：modelObject.DoMain=" + modelObject.DoMain);

                    if (modelObject.Series_Code.Trim().Substring(0,4)=="1102")
                    {
                        modelObject.MoldService = GMWS;
                        IsGMW = true;
                    }
                    else
                        modelObject.PressService = GPWS;

                    modelObject.ShowLanguage = LanguageManager.GetLanguage(modelObject.DoMain.ToLower());//第一次启动时，根据域名判断显示语言
                    
                    if (IsGMW)
                    {
                        MoldSpecialHandle msh = new MoldSpecialHandle(modelObject);
                        Application.Run(msh.ReturnFormBySpecailType());
                    }
                    else
                    {
                        PressSpecialHandle psh = new PressSpecialHandle(modelObject);
                        Application.Run(psh.ReturnFormBySpecailType());
                    }
                }
                else//如果参数不对，给用户一个提示
                {

                    if (nvc.Count <= 0)
                    {
                        if (IsGMW)
                            modelObject.MoldService.WriteApplicationErrorAsync("没有获取到参数", "", "", null);
                        else
                        {
                            //string[] args = new string[4];
                            // args[0] = "没有获取到参数";
                            //WebServiceHelper.InvokeGPWWebService("WriteApplicationError", args);
                            modelObject.PressService.WriteApplicationErrorAsync("没有获取到参数", "", "", null);

                        }
                        LanguageManager LM = new LanguageManager("StartLoad");
                        LanguageManager.GetLanguage("");
                        MessageBox.Show(LM.SetLanguage("StartMenuMsg"), LM.SetLanguage("strMsgTitle"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (nvc.Count > 0 && nvc.Count < 13)
                    {
                        if (IsGMW)
                            modelObject.MoldService.WriteApplicationErrorAsync("统合参数不正确", "URL参数：" + UrlPath, "", null);
                        else
                            modelObject.PressService.WriteApplicationErrorAsync("统合参数不正确", "URL参数：" + UrlPath, "", null);
                    }
                    System.Environment.Exit(0);

                    //// 测试开始

                    //modelObject.Product_ID = "10100110250"; //nvc.Get(0);
                    //modelObject.UserName = "654059";// nvc.Get(1);
                    //modelObject.OldOrder = "";// nvc.Get(2);//参数pn
                    //modelObject.Main_Photo = "/material/press/MSM1/PHOTO/10100110250.jpg";// nvc.Get(3);
                    //modelObject.Page_Path = "http://cn.stg.misumi-ec.com/vona2/detail/110100110250/"; // nvc.Get(4);
                    //modelObject.Brd_Code = "MSM1"; //nvc.Get(5);
                    //modelObject.Brd_Name = "MISUMI"; //nvc.Get(6);
                    //modelObject.Series_Code = "110100110250";
                    //modelObject.Series_Name = "防废料回跳型凹模 -直杆型･经济型-"; //nvc.Get(8);
                    //modelObject.E_CatalogType = "SR-EMSD"; //nvc.Get(9);//统合网站类型
                    //modelObject.DoMain = "http://cn.stg.misumi-ec.com";// nvc.Get(10);
                    //modelObject.Region = ReturnRegion(("http://cn.stg.misumi-ec.com").ToLower());  //ReturnRegion(modelObject.DoMain.ToLower());//公司简称

                    //CommonHelper.WriteAcisValue("NoMexType测试：modelObject.Series_Code=" + modelObject.Series_Code);
                    //CommonHelper.WriteAcisValue("NoMexType测试：modelObject.Product_ID=" + modelObject.Product_ID);
                    //CommonHelper.WriteAcisValue("NoMexType测试：modelObject.UserName=" + modelObject.UserName);
                    //CommonHelper.WriteAcisValue("NoMexType测试：modelObject.OldOrder=" + modelObject.OldOrder);
                    //CommonHelper.WriteAcisValue("NoMexType测试：modelObject.Main_Photo=" + modelObject.Main_Photo);
                    //CommonHelper.WriteAcisValue("NoMexType测试：modelObject.Page_Path=" + modelObject.Page_Path);
                    //CommonHelper.WriteAcisValue("NoMexType测试：modelObject.Brd_Code=" + modelObject.Brd_Code);
                    //CommonHelper.WriteAcisValue("NoMexType测试：modelObject.Brd_Name=" + modelObject.Brd_Name);
                    //CommonHelper.WriteAcisValue("NoMexType测试：modelObject.Series_Code=" + modelObject.Series_Code);
                    //CommonHelper.WriteAcisValue("NoMexType测试：modelObject.Series_Name=" + modelObject.Series_Name);
                    //CommonHelper.WriteAcisValue("NoMexType测试：modelObject.E_CatalogType=" + modelObject.E_CatalogType);
                    //CommonHelper.WriteAcisValue("NoMexType测试：modelObject.DoMain=" + modelObject.DoMain);

                    //if (modelObject.Series_Code.Trim().Substring(0,4)=="1102")
                    //{
                    //    modelObject.MoldService = GMWS;
                    //    IsGMW = true;
                    //}
                    //else
                    //    modelObject.PressService = GPWS;

                    //modelObject.ShowLanguage = LanguageManager.GetLanguage(modelObject.DoMain.ToLower());//第一次启动时，根据域名判断显示语言

                    //if (IsGMW)
                    //{
                    //    MoldSpecialHandle msh = new MoldSpecialHandle(modelObject);
                    //    Application.Run(msh.ReturnFormBySpecailType());
                    //}
                    //else
                    //{
                    //    PressSpecialHandle psh = new PressSpecialHandle(modelObject);
                    //    Application.Run(psh.ReturnFormBySpecailType());
                    //}

                    // 测试end

                    //Application.Run(new FormType());//测试窗体

                    //MisumiWebService GMWS = new MisumiWebService();
                    //ModelObject Model = new ModelObject();
                    //Model.E_CatalogType = "EPV-L";
                    //Model.ModelType = "EPV-L";
                    //Model.OldOrder = "SBBPE20-50";
                    //Model.UserName = "MISUMI";
                    //Model.ClassName = "STRAIGHT EJECTOR PINS";
                    //Model.Region = Program.ReturnRegion("http://jp.misumi-ec.com");//公司简称
                    //Model.MoldService = GMWS;
                    //Model.ShowLanguage = LanguageManager.CurrentLanguage.JPN.ToString();
                    //MoldSpecialHandle msh = new MoldSpecialHandle(Model);
                    //Application.Run(msh.ReturnFormBySpecailType());
                }
            }
            catch (Exception ex)
            {
                GMWS.WriteApplicationErrorAsync(ex.Message + "URL参数：" + UrlPath, ex.Source, ex.StackTrace, modelObject.Region);
                System.Environment.Exit(0);
            }
        }

        #region 根据URL传递的域名返回所在地
        /// <summary>
        /// 根据域名返回所在地
        /// </summary>
        /// <param name="Domain"></param>
        /// <returns></returns>
        public static string ReturnRegion(string Domain)
        {
            string Region = "";
            switch (Domain)
            {
                case "http://jp.misumi-ec.com":
                case "http://www.stg.misumi-ec.com":
                    Region = "MJP";
                    break;
                case "http://cn.misumi-ec.com":
                case "http://cn.stg.misumi-ec.com":
                    Region = "SH2";
                    break;
                case "http://uk.misumi-ec.com":
                    Region = "GRM";
                    break;
                case "http://us.misumi-ec.com":
                case "http://us.stg.misumi-ec.com":
                    Region = "USA";
                    break;
                case ""://印度尼西亚
                    Region = "SH2";
                    break;
                case "http://sg.misumi-ec.com":
                    Region = "SEA";
                    break;
                case "http://th.misumi-ec.com":
                case "http://th.stg.misumi-ec.com":
                    Region = "THA";
                    break;
                case "http://kr.misumi-ec.com":
                case "http://kr.stg.misumi-ec.com":
                    Region = "KOR";
                    break;
                case "http://tw.misumi-ec.com":
                    Region = "TIW";
                    break;
                case "http://in.misumi-ec.com":
                    Region = "IND";
                    break;
                case "http://my.misumi-ec.com":
                    Region = "MYS";
                    break;
            }
            return Region;
        }
        #endregion

        #region 只允许运行一个窗口
        /// 该函数设置由不同线程产生的窗口的显示状态  
        /// </summary>  
        /// <param name="hWnd">窗口句柄</param>  
        /// <param name="cmdShow">指定窗口如何显示。查看允许值列表，请查阅ShowWlndow函数的说明部分</param>  
        /// <returns>如果函数原来可见，返回值为非零；如果函数原来被隐藏，返回值为零</returns>  
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        /// <summary>  
        ///  该函数将创建指定窗口的线程设置到前台，并且激活该窗口。键盘输入转向该窗口，并为用户改各种可视的记号。  
        ///  系统给创建前台窗口的线程分配的权限稍高于其他线程。   
        /// </summary>  
        /// <param name="hWnd">将被激活并被调入前台的窗口句柄</param>  
        /// <returns>如果窗口设入了前台，返回值为非零；如果窗口未被设入前台，返回值为零</returns>  
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);


        private const int SW_SHOWNOMAL = 1;
        private static void HandleRunningInstance(Process instance)
        {
            ShowWindowAsync(instance.MainWindowHandle, SW_SHOWNOMAL);//显示  
            SetForegroundWindow(instance.MainWindowHandle);//当到最前端  
        }

        private static Process RuningInstance()
        {
            Process currentProcess = Process.GetCurrentProcess();
            Process[] Processes = Process.GetProcessesByName(currentProcess.ProcessName);
            foreach (Process process in Processes)
            {
                if (process.Id != currentProcess.Id)
                {
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == currentProcess.MainModule.FileName)
                    {
                        return process;
                    }
                }
            }
            return null;
        }
        #endregion
    }
}
