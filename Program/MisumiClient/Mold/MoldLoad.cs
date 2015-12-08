using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;
using Misumi_Client.Common;
using Misumi_Client.Mold;
using System.Diagnostics;
using System.Threading;

namespace Misumi_Client.Mold
{
    public partial class MoldLoad : Form
    {
        private const string ClassID = "CLSID\\{72F3C90C-7D04-46F7-BACE-943CFB109B90}\\";
        ModelObject Model;
        private LanguageManager LM;
        [DllImport(@"\MoldHandle\MisumiInterfaceMold_Jap_Web.ocx")]
        public static extern int DllRegisterServer();//注册时用
        [DllImport(@"\MoldHandle\MisumiInterfaceMold_Jap_Web.ocx")]
        public static extern int DllUnregisterServer();//取消注册时用

        private static string LanguagePath = CommonHelper.getSetConfig("strLanguageFile");

        public MoldLoad(ModelObject model)
        {
            Model = model;
            InitializeComponent();
            LM = new LanguageManager("StartLoad");
        }

        private void StartLoad_Load(object sender, EventArgs e)
        {
            this.labMessage.Text = LM.SetLanguage("WaitMsg");
            LanguageManager.SetFont(this.Controls, 14);
            this.Width = this.labMessage.Width + 100;
            BGW.RunWorkerAsync();
        }

        string MexType = "";
        private void BGW_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string strWebUrl = Model.MoldService.Url;
                Model.ServiceUrl = strWebUrl.Substring(0, strWebUrl.LastIndexOf("/") + 1);

                Thread DownThread = new Thread(new ThreadStart(DownLanguage));
                DownThread.Start();

                if (!string.IsNullOrEmpty(Model.ModelType))
                {
                    if (!Model.IsSpecial)//如果不是特例处理，那么就进行对应关系表的查询。
                    {
                        MexType = Model.MoldService.GetMexType(Model.ModelType);
                        if (string.IsNullOrEmpty(MexType))//查无此类型
                            MexEnd();
                        else if (MexType == "削除モレ")//该类型为消除
                            MexEnd();
                        else if (MexType == "規格廃止")
                            MexEnd();
                        else if (MexType == "他事業部商品")
                            MexEnd();
                        else if (MexType == "MEX未添加")
                            MexEnd();
                        else
                            Model.ModelType = MexType;
                    }
                    if (!string.IsNullOrEmpty(Model.MoldService.GetClassName(Model.ModelType)))
                    {
                        RegistryKey RkOcx = Registry.ClassesRoot.OpenSubKey(ClassID);
                        if (!File.Exists(CommonHelper.MoldOcx))//如果不存在标识文件，就注册ocx
                        {
                            if (RkOcx != null)
                                DllUnregisterServer();
                            RegisterOcx();
                        }
                        if (!File.Exists(CommonHelper.ViewFlag))
                            Register3DView();
                        GetPicInfo();
                        GetOcxData();
                    }
                    else
                    {
                        MexEnd();
                    }
                }
                else
                {
                    MexEnd();
                }
            }
            catch (Exception ex)
            {
                Model.MoldService.WriteApplicationErrorAsync(ex.Message, ex.Source, ex.StackTrace, Model.Region);
                MexEnd();
            }
        }

        private void BGW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (Model != null)
            {
                MoldForm app = new MoldForm(Model);
                app.Show();
                this.Hide();
            }
            else
            {
                MexEnd();
            }
        }

        #region 隐藏窗口
        private delegate void HideDelete();
        private void FormHide()
        {
            if (this.InvokeRequired)
            {
                HideDelete hd = new HideDelete(FormHide);
                this.Invoke(hd);
            }
            else
                this.Hide();
        }
        /// <summary>
        /// 提示没有该类，并结束此次进程
        /// </summary>
        public void MexEnd()
        {
            FormHide();
            MessageBox.Show(LM.SetLanguage("NoMexType"), LM.SetLanguage("strMsgTitle"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            System.Environment.Exit(0);
        }
        #endregion

        #region 注册OCX
        private void RegisterOcx()
        {
            try
            {
                int IsRegister = DllRegisterServer();
                if (IsRegister >= 0)
                {
                    File.Create(CommonHelper.MoldOcx);
                }
                else
                {
                    Model.MoldService.WriteApplicationErrorAsync("注册ocx失败", Model.UserName, "", Model.Region);
                    FormHide();
                    if (MessageBox.Show(LM.SetLanguage("OcxErrorMsg"), LM.SetLanguage("strMsgTitle"), MessageBoxButtons.OK, MessageBoxIcon.Warning) == DialogResult.OK)
                        System.Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                Model.MoldService.WriteApplicationErrorAsync(ex.Message, ex.Source, ex.StackTrace, Model.Region);
            }
        }
        #endregion

        #region 注册3DView插件
        private void Register3DView()
        {
            Process pro = new Process();
            try
            {
                pro.StartInfo.FileName = "Regsvr32.exe";
                pro.StartInfo.Arguments = "/s " + "\"" + CommonHelper.AppPath + @"\3DView\MISUMI_3DVIEW.dll" + "\"";//路径中不能有空格
                pro.Start();

                pro.WaitForExit(5000); //or the wait time you want
                //Now we need to see if the process was successful  
                if (!pro.HasExited)
                    pro.Kill();
                pro.Dispose();
            }
            catch (Exception ex)
            {
                pro.Kill();
                Model.MoldService.WriteApplicationErrorAsync(ex.Message, ex.Source, ex.StackTrace, Model.Region);
            }
        }
        #endregion

        #region 获取图片信息
        private void GetPicInfo()
        {
            try
            {
                Model.ClassName = Model.MoldService.GetClassName(Model.ModelType);
                if (Model.ClassName != "")
                {
                    Model.Pic2D = Model.ServiceUrl + Model.MoldService.GetPicPDFByType(Model.ClassName, Model.ModelType, "2DBIG", Model.ShowLanguage);
                    Model.Pic_AA = Model.ServiceUrl + Model.MoldService.GetPic_AA(Model.ClassName, Model.ModelType, Model.ShowLanguage);
                    Model.PicPhoto = Model.ServiceUrl + Model.MoldService.GetPicPDFByType(Model.ClassName, Model.ModelType, "PHOTO", Model.ShowLanguage);
                    Model.PicEC = Model.ServiceUrl + Model.MoldService.GetPicPDFByType(Model.ClassName, Model.ModelType, "EC", Model.ShowLanguage);
                    Model.PicT = Model.ServiceUrl + Model.MoldService.GetPicPDFByType(Model.ClassName, Model.ModelType, "T", Model.ShowLanguage);
                    Model.Pic_A_Path = Model.MoldService.GetPic_A(Model.ClassName, Model.ModelType, Model.ShowLanguage);
                }
                else
                {
                    Model = null;
                }
            }
            catch (Exception ex)
            {
                Model.MoldService.WriteApplicationErrorAsync(ex.Message, ex.Source, ex.StackTrace, Model.Region);
            }
        }
        #endregion

        #region 获取ocx所需的数据信息
        private void GetOcxData()
        {
            try
            {
                if (!string.IsNullOrEmpty(Model.ClassName) && !string.IsNullOrEmpty(Model.ModelType))
                {
                    string GeneralInfo = Model.MoldService.GetGeneralInfoByCondition(Model.ClassName, Model.ModelType);
                    string[] strValue = GeneralInfo.Split('@');
                    if (strValue.Length == 2)
                    {
                        Model.OrderTemplate = strValue[0];
                        Model.AlterationSheet = strValue[1];
                        if (Model.Pic_A_Path != "empty")
                        {
                            //通过Webservice获取的Pic_A的值，是以下表示，需要处理
                            //Interface/Dlg3/Fit/Main/JPN_mo12mf01_A/JPN_mo12mf01_0069_A
                            Model.OcxPicAPath = CommonHelper.InternetCache + @"\" + Model.Pic_A_Path.Substring(0, Model.Pic_A_Path.LastIndexOf('/') + 1).Replace("/", @"\");
                        }
                    }
                    Model.OcxPicANum = Model.MoldService.GetPic_ANum(Model.ClassName, Model.ModelType, Model.ShowLanguage);
                    Model.BaseSizeHeader = Model.MoldService.GetBaseSizeHeader(Model.ModelType, Model.ClassName).Trim();
                    string strHeaderLang = Model.MoldService.GetHeaderLang(Model.ModelType, Model.ClassName, Model.BaseSizeHeader, Model.ShowLanguage).Trim();
                    //返回值=1：无语言表  返回值=0：其它异常
                    Model.BaseLangHeader = strHeaderLang == "1" ? Model.BaseSizeHeader : strHeaderLang;

                    Model.BaseList = Model.MoldService.GetBaseSizeOrReprocessData(Model.ModelType, Model.ClassName, true, Model.ShowLanguage);
                    Model.ReproList = Model.MoldService.GetBaseSizeOrReprocessData(Model.AlterationSheet, Model.ClassName, false, Model.ShowLanguage);
                }
            }
            catch (Exception ex)
            {
                Model.MoldService.WriteApplicationErrorAsync(ex.Message, ex.Source, ex.StackTrace, Model.Region);
            }
        }
        #endregion

        #region 下载Ocx所需的Language语言文件
        /// <summary>
        /// 因为ocx需要language配置文件，所以在程序启动前，后台进程下载程序所需要的language文件
        /// </summary>
        private void DownLanguage()
        {
            string OcxLanguage = "";
            string SavePath = "";

            SavePath = CommonHelper.MoldLanguagePath;
            OcxLanguage = Path.Combine(CommonHelper.MoldLanguagePath, LanguagePath);

            if (!File.Exists(OcxLanguage))//下载language.xls文件（ocx需要）
            {
                if (CommonHelper.CheckUrl(Model.ServiceUrl + LanguagePath))
                {
                    CommonHelper.DownFiles(Model.ServiceUrl + LanguagePath, SavePath);
                }
            }
            else
            {
                FileInfo fi = new FileInfo(OcxLanguage);
                if (fi.Length != Model.MoldService.ReturnLanSize())//判断本地language文件和服务器是否一致，否则重新下载
                {
                    if (CommonHelper.CheckUrl(Model.ServiceUrl + LanguagePath))
                    {
                        File.Delete(OcxLanguage);
                        CommonHelper.DownFiles(Model.ServiceUrl + LanguagePath, SavePath);
                    }
                }
            }
        }
        #endregion
    }
}
