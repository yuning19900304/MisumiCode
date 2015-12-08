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

namespace Misumi_Client.Press
{
    public partial class PressLoad : Form
    {
        private const string ClassID = "CLSID\\{96D44969-B414-4BA6-A895-70B13E274FD2}\\";
        ModelObject Model;
        private LanguageManager LM;
        [DllImport(@"\PressHandle\MisumiInterface_GPL_Web.ocx")]
        public static extern int DllRegisterServer();//注册时用
        [DllImport(@"\PressHandle\MisumiInterface_GPL_Web.ocx")]
        public static extern int DllUnregisterServer();//取消注册时用

        private static string LanguagePath = CommonHelper.getSetConfig("strLanguageFile");

        public PressLoad(ModelObject model)
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

        private void BGW_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string strWebUrl = Model.PressService.Url;
                Model.ServiceUrl = strWebUrl.Substring(0, strWebUrl.LastIndexOf("/") + 1);

                Thread DownThread = new Thread(new ThreadStart(DownLanguage));
                DownThread.Start();

                if (Model.ModelType != "")
                {
                    if (Model.ModelType != "NO")//查无此类型
                    {
                        //if (Model.PressService.NoAcis(Model.ModelType))
                        //{
                        //    MexEnd();
                        //}
                        //判断是否是小径
                        string MexType = Model.PressService.QuillList(Model.ModelType, Model.Product_ID);
                        if (!string.IsNullOrEmpty(MexType))
                            Model.ModelType = MexType;

                        if (!string.IsNullOrEmpty(Model.PressService.GetClassName(Model.ModelType)))//在TypeInfo表里也没有查找到该数据
                        {
                            RegistryKey RkOcx = Registry.ClassesRoot.OpenSubKey(ClassID);
                            if (!File.Exists(CommonHelper.PressOcx))//如果不存在标识文件，就注册ocx
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
                            //CommonHelper.WriteAcisValue("NoMexType测试：BGW_DoWork3");
                            MexEnd();
                        }
                    }
                    else
                    {
                        //CommonHelper.WriteAcisValue("NoMexType测试：BGW_DoWork2");
                        MexEnd();
                    }
                }
                else
                {
                    //CommonHelper.WriteAcisValue("NoMexType测试：BGW_DoWork1");
                    MexEnd();
                }
            }
            catch (Exception ex)
            {
                //CommonHelper.WriteAcisValue("NoMexType测试：BGW_DoWork1 ex");
                Model.PressService.WriteApplicationErrorAsync(ex.Message, ex.Source, ex.StackTrace, Model.Region);
                MexEnd();
            }
        }

        private void BGW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (Model != null)
            {
                if (Model.ModelType.ToUpper().Contains("LONG"))
                    Model.ModelType = Model.ModelType.ToUpper();

                PressForm pressform = new PressForm(Model);
                pressform.Show();
                this.Hide();
            }
            else
            {
                //CommonHelper.WriteAcisValue("NoMexType测试：BGW_RunWorkerCompleted");
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
                    File.Create(CommonHelper.PressOcx);
                }
                else
                {
                    Model.PressService.WriteApplicationErrorAsync("注册ocx失败", Model.UserName, "", Model.Region);
                    FormHide();
                    if (MessageBox.Show(LM.SetLanguage("OcxErrorMsg"), LM.SetLanguage("strMsgTitle"), MessageBoxButtons.OK, MessageBoxIcon.Warning) == DialogResult.OK)
                        System.Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                Model.PressService.WriteApplicationErrorAsync(ex.Message, ex.Source, ex.StackTrace, Model.Region);
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
                Model.PressService.WriteApplicationErrorAsync(ex.Message, ex.Source, ex.StackTrace, Model.Region);
            }
        }
        #endregion

        #region 获取图片信息
        private void GetPicInfo()
        {
            try
            {
                Model.ClassName = Model.PressService.GetClassName(Model.ModelType);
                if (Model.ClassName != "")
                {
                    Model.Pic2D = Model.ServiceUrl + Model.PressService.GetPicPDFByType(Model.ClassName, Model.ModelType, "2DBIG", Model.ShowLanguage);
                    Model.Pic_AA = Model.ServiceUrl + Model.PressService.GetPic_AA(Model.ClassName, Model.ModelType, Model.ShowLanguage);
                    Model.PicPhoto = Model.ServiceUrl + Model.PressService.GetPicPDFByType(Model.ClassName, Model.ModelType, "PHOTO", Model.ShowLanguage);
                    Model.PicEC = Model.ServiceUrl + Model.PressService.GetPicPDFByType(Model.ClassName, Model.ModelType, "EC", Model.ShowLanguage);
                    Model.PicT = Model.ServiceUrl + Model.PressService.GetPicPDFByType(Model.ClassName, Model.ModelType, "T", Model.ShowLanguage);
                    Model.Pic_A_Path = Model.PressService.GetPic_A_Path(Model.ClassName, Model.ModelType, Model.ShowLanguage);
                }
                else
                {
                    //CommonHelper.WriteAcisValue("NoMexType测试：GetPicInfo");
                    Model = null;
                    Model.PressService.WriteApplicationErrorAsync("Model is null", "ModelType is " + Model.ModelType, "Time", Model.Region);
                    MexEnd();
                }
            }
            catch (Exception ex)
            {
                Model.PressService.WriteApplicationErrorAsync(ex.Message, ex.Source, ex.StackTrace, Model.Region);
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
                    string GeneralInfo = Model.PressService.GetGeneralInfoByCondition(Model.ClassName, Model.ModelType, Model.ShowLanguage);
                    string[] strValue = GeneralInfo.Split('@');
                    if (strValue.Length == 3)
                    {
                        Model.OrderTemplate = strValue[0];
                        Model.AlterationSheet = strValue[1];
                        Model.Pic_ANum = strValue[2];
                        if (Model.Pic_A_Path != "empty")
                        {
                            //通过Webservice获取的Pic_A的值，是以下表示，需要处理
                            //Interface/Dlg3/Fit/Main/JPN_mo12mf01_A/JPN_mo12mf01_0069_A
                            Model.OcxPicAPath = CommonHelper.InternetCache + @"\" + Model.Pic_A_Path.Substring(0, Model.Pic_A_Path.LastIndexOf('/') + 1).Replace("/", @"\");
                        }
                        else
                            Model.OcxPicAPath = CommonHelper.InternetCache + @"\";
                    }
                    Model.Page_C = Model.PressService.GetPage_C(Model.ClassName, Model.ModelType);
                    if (Model.Page_C.Length < 4)
                        switch (Model.Page_C.Length)
                        {
                            case 1:
                                Model.Page_C = "000" + Model.Page_C;
                                break;
                            case 2:
                                Model.Page_C = "00" + Model.Page_C;
                                break;
                            case 3:
                                Model.Page_C = "0" + Model.Page_C;
                                break;
                        }

                    Model.BaseSizeHeader = Model.PressService.GetBaseSizeHeader(Model.ModelType, Model.ClassName).Trim();
                    string strHeaderLang = Model.PressService.GetHeaderLang(Model.ModelType, Model.ClassName, Model.BaseSizeHeader, Model.ShowLanguage).Trim();
                    //返回值=1：无语言表  返回值=0：其它异常
                    Model.BaseLangHeader = strHeaderLang == "1" ? Model.BaseSizeHeader : strHeaderLang;

                    Model.BaseList = Model.PressService.GetBaseSizeOrReprocessData(Model.ModelType, Model.ClassName, true, Model.ShowLanguage);
                    Model.ReproList = Model.PressService.GetBaseSizeOrReprocessData(Model.AlterationSheet, Model.ClassName, false, Model.ShowLanguage);
                }
            }
            catch (Exception ex)
            {
                Model.PressService.WriteApplicationErrorAsync(ex.Message, ex.Source, ex.StackTrace, Model.Region);
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

            SavePath = CommonHelper.PressLanguagePath;
            OcxLanguage = Path.Combine(CommonHelper.PressLanguagePath, LanguagePath);

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
                if (fi.Length != Model.PressService.ReturnLanSize())//判断本地language文件和服务器是否一致，否则重新下载
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
