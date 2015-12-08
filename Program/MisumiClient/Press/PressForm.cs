using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using Misumi_Client.Common;
using Misumi_Client.Mold;
using System.Web;

namespace Misumi_Client.Press
{
    public partial class PressForm : Form
    {
        #region 声明全局变量
        public static ModelObject Model;
        public static string LanguagePicFolder;//各个语言的图片素材的文件夹
        LanguageManager LM;
        string strWebUrl;
        short LanguageIndex = 2;
        const string OcxTemporary = "Temporary";
        string strLanguagePath = CommonHelper.PressLanguagePath;
        string[] URLLink = null;
        PictureBox Pic_Show = new PictureBox();

        [DllImport(@"\PressHandle\UILanguage.dll", EntryPoint = "SetPath", CharSet = CharSet.Ansi)]
        public static extern void SetPath(string strPath);
        [DllImport(@"\PressHandle\UILanguage.dll", EntryPoint = "InitLanguage", CharSet = CharSet.Ansi)]
        public static extern void InitLanguage(int iLanguage);
        [DllImport(@"\PressHandle\InputTextForming.dll", EntryPoint = "InputTextForming", CharSet = CharSet.Ansi)]
        public extern static IntPtr InputTextForming(string ModelType, string ClassName, string order, string strDataPin, string languagePath, int language, string cText, int iTextNumber);
        #endregion

        public PressForm(ModelObject model)
        {
            try
            {
                Model = model;
                Model.PressService.Timeout = 300000;//设置代理超时为5分钟
                strWebUrl = Model.ServiceUrl;
                LM = new LanguageManager("MoldForm");
                InitializeComponent();
                this.PicRightPanel.Controls.Add(Pic_Show);
                Pic_Show.Visible = false;
            }
            catch (Exception ex)
            {
                Model.PressService.WriteApplicationErrorAsync(ex.Message, ex.Source, ex.StackTrace, Model.Region);
            }
        }

        private void PressForm_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Model.ModelType))
            {
                //CommonHelper.WriteAcisValue("NoMexType测试：PressForm_Load");
                ShowMessageBox(LM.SetLanguage("NoMexType"), false);
                System.Environment.Exit(0);
            }
            SetApplyByLanguage();
            URLLink = Model.PressService.GetURLLink(Model.Region).Split('@');
            try
            {
                initPic();//加载图片
            }
            catch (Exception ex)
            {
                Model.PressService.WriteApplicationErrorAsync(ex.Message, ex.Source, ex.StackTrace, Model.Region);
                ShowMessageBox(LM.SetLanguage("initPicError"), false);
            }
            try
            {
                //刻字
                Thread t = new Thread(new ThreadStart(KeZi));
                t.Start();
            }
            catch (Exception ex)
            {
                Model.PressService.WriteApplicationErrorAsync(ex.Message, ex.Source, ex.StackTrace, Model.Region);
                ShowMessageBox(LM.SetLanguage("initDataError"), false);
            }
            this.Activate();
        }


        /// <summary>
        /// 根据语言设置主窗体上的控件应用属性
        /// </summary>
        private void SetApplyByLanguage()
        {
            try
            {
                LanguagePicFolder = LM.SetLanguage("LanguagePicFolder");//设置应用程序所用图片的全局路径变量
                LanguageIndex = Convert.ToInt16(LanguageManager.ShowLan);//转换Language标识(ocx用)

                switch (LanguageManager.ShowLan)
                {
                    case LanguageManager.CurrentLanguage.CHS:
                        SetFormFont("SimHei", 14);
                        break;
                    case LanguageManager.CurrentLanguage.EN:
                        SetFormFont("Arial", 11);
                        break;
                    case LanguageManager.CurrentLanguage.JPN:
                        SetFormFont("MS PGothic", 14);
                        break;
                    case LanguageManager.CurrentLanguage.CHT:
                        SetFormFont("MingLiU", 14);
                        break;
                    case LanguageManager.CurrentLanguage.KOR:
                        SetFormFont("GulimChe", 10);
                        this.lblOrder.Location = new Point(4, 13);
                        this.lblPreviewShow.Font = new Font(new FontFamily("GulimChe"), 8);
                        this.lblCAD3.Font = new Font(new FontFamily("GulimChe"), 7);
                        break;
                    case LanguageManager.CurrentLanguage.TAI:
                        SetFormFont("Tahoma", 10);
                        break;
                    default:
                        break;
                }

                this.lblOldOrder.Text = Model.OldOrder; //显示页面传递的order订单号

                this.Text = Model.ModelType + " | " + LM.SetLanguage("PressTitle");
                this.lblOrder.Text = LM.SetLanguage("lblOrder");
                this.label4.Text = LM.SetLanguage("label4");
                this.label15.Text = LM.SetLanguage("label15");
                this.PicCopy.Image = Image.FromFile(GetPicPath("Copy.jpg"));
                this.PicTabHelp.Image = Image.FromFile(GetPicPath("Help.jpg"));
                this.lblShowLan.Text = LM.SetLanguage("lblShowLan");
                this.lblShowLan.Location = new Point(txtNewOrder.Location.X + txtNewOrder.Width - lblShowLan.Width + 3, lblShowLan.Location.Y);
                this.cboChangeLanguage.SelectedIndex = LanguageIndex;
                this.lklbMagnify.Text = LM.SetLanguage("lklbMagnify");

                this.PicTab2D.Image = Image.FromFile(GetPicPath("2D_on.jpg"));
                this.PicTabAdd.Image = Image.FromFile(GetPicPath("Add.jpg"));
                this.PicTab3D.Image = Image.FromFile(GetPicPath("3D.jpg"));
                this.PicTabCAD.Image = Image.FromFile(GetPicPath("CAD.jpg"));
                this.picTabPhoto.Image = Image.FromFile(GetPicPath("Photo_on.jpg"));
                this.PicTabEC.Image = Image.FromFile(GetPicPath("EC.jpg"));
                this.PicTabT.Image = Image.FromFile(GetPicPath("T.jpg"));

                this.btn3DBulid.Text = LM.SetLanguage("Btn3D");
                if (LanguageManager.ShowLan == LanguageManager.CurrentLanguage.EN)
                    this.btn3DBulid.Font = new Font("Arial", 7);
                this.lblPreviewShow.Text = LM.SetLanguage("PreviewInfo");
            }
            catch (Exception ex)
            {
                Model.PressService.WriteApplicationErrorAsync(ex.Message, ex.Source, ex.StackTrace, Model.Region);
            }
        }

        #region 随用户选择显示语言而改变整个窗体的文字
        bool IsFirstLoad = false;
        /// <summary>
        /// 切换语言
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboChangeLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsFirstLoad)//如果是第一次加载窗体，该事件不做处理
            {
                switch (cboChangeLanguage.SelectedIndex)
                {
                    case 0:
                        LanguageManager.ShowLan = LanguageManager.CurrentLanguage.CHS;
                        ChangeLanFont();
                        break;
                    case 1:
                        LanguageManager.ShowLan = LanguageManager.CurrentLanguage.EN;
                        ChangeLanFont();
                        break;
                    case 2:
                        LanguageManager.ShowLan = LanguageManager.CurrentLanguage.JPN;
                        ChangeLanFont();
                        break;
                    case 3:
                        LanguageManager.ShowLan = LanguageManager.CurrentLanguage.CHT;
                        ChangeLanFont();
                        break;
                    case 4:
                        LanguageManager.ShowLan = LanguageManager.CurrentLanguage.KOR;
                        ChangeLanFont();
                        break;
                    case 5:
                        LanguageManager.ShowLan = LanguageManager.CurrentLanguage.TAI;
                        ChangeLanFont();
                        break;
                }
                XmlDocument xmldoc = new XmlDocument();
                XmlDeclaration xmldec = xmldoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                xmldoc.AppendChild(xmldec);

                XmlElement xmlele = xmldoc.CreateElement("LanguageIndex");
                xmlele.InnerXml = LanguageManager.ShowLan.ToString();
                xmldoc.AppendChild(xmlele);//更改配置文件中的语言显示标识
                if (!Directory.Exists(CommonHelper.MisumiDataPath))
                    Directory.CreateDirectory(CommonHelper.MisumiDataPath);

                xmldoc.Save(CommonHelper.MisumiDataPath + "Language.xml");
            }
            else
                IsFirstLoad = true;
        }


        /// <summary>
        /// 根据选择的字体重新加载本窗体
        /// </summary>
        /// <param name="FontName"></param>
        private void ChangeLanFont()
        {
            Model.ShowLanguage = LanguageManager.ShowLan.ToString();
            PressLoad start = new PressLoad(Model);
            start.Show();
            this.Dispose();
            GC.Collect();
            this.Hide();
            this.Close();
        }

        /// <summary>
        /// 设置整个窗体的控件的字体与字体大小
        /// </summary>
        /// <param name="FontName"></param>
        /// <param name="FontSize"></param>
        private void SetFormFont(string FontName, int FontSize)
        {
            LanguageManager.SetFont(this.Controls, 9);
            this.lblOrder.Font = new Font(new FontFamily(FontName), FontSize, FontStyle.Bold);//Order文本框左侧的文字
            this.lblOldOrder.Font = new Font(new FontFamily("Arial"), 12, FontStyle.Bold);
            this.lblCAD2.Font = new Font(new FontFamily(FontName), 9, FontStyle.Bold);//CAD文件格式加粗
        }
        #endregion

        /// <summary>
        /// 刻字：TCPCT/VCF1M/OCP3M
        /// </summary>
        private void KeZi()
        {
            try
            {
                string strDataPin = "0";
                //参数示例：1@R L@R L@R L@R L@R L@R L
                string InputText = "";
                if (Model.ModelType.LastIndexOf("M") == Model.ModelType.Length - 1 && Model.ModelType != "HCM")
                {
                    InputText = Model.ModelType.Substring(0, Model.ModelType.Length - 2);
                    InputText = InputText + "□M";
                }
                else
                    InputText = Model.ModelType;

                string KeZiValue = Model.PressService.KeZi(InputText);
                short iTextNumber = 0;
                string ctext = "";
                string[] KeZiValues = KeZiValue.Split('@');
                if (KeZiValues.Length >= 7)
                {
                    iTextNumber = Convert.ToInt16(KeZiValues[0]);
                    switch (LanguageManager.ShowLan)
                    {
                        case LanguageManager.CurrentLanguage.CHS:
                            ctext = KeZiValues[1];
                            break;
                        case LanguageManager.CurrentLanguage.EN:
                            ctext = KeZiValues[2];
                            break;
                        case LanguageManager.CurrentLanguage.JPN:
                            ctext = KeZiValues[3];
                            break;
                        case LanguageManager.CurrentLanguage.CHT:
                            ctext = KeZiValues[4];
                            break;
                        case LanguageManager.CurrentLanguage.KOR:
                            ctext = KeZiValues[5];
                            break;
                        case LanguageManager.CurrentLanguage.TAI:
                            ctext = KeZiValues[6];
                            break;
                        default:
                            ctext = KeZiValues[2];
                            break;
                    }
                }
                IntPtr intptr = InputTextForming(Model.ModelType, Model.ClassName, Model.OrderTemplate, strDataPin, strLanguagePath, LanguageIndex, ctext, iTextNumber);
                Model.KeZiType = Marshal.PtrToStringAnsi(intptr);

                if (!string.IsNullOrEmpty(Model.KeZiType) && Model.KeZiType != "ABCDEFGHIJKLMNOPQRSTUVWXYZ")
                {
                    if (iTextNumber == 0)//如果为0，则不需要刻字类型，直接赋值类型
                        Model.KeZiType = Model.ModelType;
                    initData();//加载数据
                }
                else
                {
                    ShowMessageBox(LM.SetLanguage("strKeZiError"), false);
                    KeZi();
                }
            }
            catch (Exception ex)
            {
                Model.PressService.WriteApplicationErrorAsync(ex.Message, ex.Source, ex.StackTrace, Model.Region);
            }
        }

        /// <summary>
        /// 加载所需图片、PDF等资源
        /// </summary>
        private void initPic()
        {
            try
            {
                if (CommonHelper.CheckUrl(Model.Pic2D))//2D图纸显示
                    Pic2D.LoadAsync(Model.Pic2D);
                else
                    Pic2D.LoadAsync(Model.NoImgBig);//NoImage
                Pic2D.LoadCompleted += new System.ComponentModel.AsyncCompletedEventHandler(Pic2D_LoadCompleted);
                if (CommonHelper.CheckUrl(Model.PicPhoto)) //照片
                    PicRightPanel.LoadAsync(Model.PicPhoto);
                else
                    PicRightPanel.LoadAsync(Model.NoImgSmall);
            }
            catch (Exception ex)
            {
                Model.PressService.WriteApplicationErrorAsync(ex.Message, ex.Source, ex.StackTrace, Model.Region);
                MessageBox.Show(ex.Message);
                return;
            }
        }


        /// <summary>
        /// 加载数据
        /// </summary>
        private void initData()
        {
            try
            {
                SetPath(strLanguagePath);
                InitLanguage(LanguageIndex);

                //设置型号名、大类名、order模版名称、追加工sheet名称、所有ocx显示的追加工小图片路径、追加工编号【0069】、语言表路径、语言类别：2代表日文
                axMisumiInterface_GPL_Web1.SetGeneralInfo(Model.KeZiType, Model.ClassName, Model.OrderTemplate, Model.Page_C, Model.OcxPicAPath, Model.Pic_ANum, strLanguagePath, LanguageIndex);
                //CommonHelper.WriteAcisValue("\r\nSetGeneralInfo函数");
                //CommonHelper.WriteAcisValue(Model.KeZiType);
                //CommonHelper.WriteAcisValue(Model.ClassName);
                //CommonHelper.WriteAcisValue(Model.OrderTemplate);
                //CommonHelper.WriteAcisValue(Model.Page_C);
                //CommonHelper.WriteAcisValue(Model.OcxPicAPath);
                //CommonHelper.WriteAcisValue(Model.Pic_ANum);
                //CommonHelper.WriteAcisValue(strLanguagePath);
                //CommonHelper.WriteAcisValue(LanguageIndex.ToString());
                axMisumiInterface_GPL_Web1.SetBaseSizeHeader(Model.BaseSizeHeader, Model.BaseLangHeader);
                //CommonHelper.WriteAcisValue("\r\nSetBaseSizeHeader函数");
                //CommonHelper.WriteAcisValue(Model.BaseSizeHeader);
                //CommonHelper.WriteAcisValue(Model.BaseLangHeader);
                //CommonHelper.WriteAcisValue("\r\nSetBaseSizeRecord函数");
                //遍历基本尺寸数据
                foreach (string baseValue in Model.BaseList)
                {
                    axMisumiInterface_GPL_Web1.SetBaseSizeRecord(baseValue.Trim());
                    //CommonHelper.WriteAcisValue(baseValue);
                }
                //CommonHelper.WriteAcisValue("\r\nSetAlterationRecord函数");
                //遍历追加工表数据
                foreach (string reproValue in Model.ReproList)
                {
                    axMisumiInterface_GPL_Web1.SetAlterationRecord(reproValue.Trim());
                    //CommonHelper.WriteAcisValue(reproValue);
                }

                axMisumiInterface_GPL_Web1.InitData();

                Thread DownPicThread = new Thread(new ThreadStart(DownAddPic));
                DownPicThread.Start();
                timer1.Start();
                this.Activate();
            }
            catch (Exception ex)
            {
                Model.PressService.WriteApplicationErrorAsync(ex.Message, ex.Source, ex.StackTrace, Model.Region);
            }
        }

        bool IsClickCAD = false;
        private void tab2D3D_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.tab2D3D.SelectedIndex == 1)
                {
                    this.lklbMagnifyAdd.Text = LM.SetLanguage("lklbMagnify");
                    if (CommonHelper.CheckUrl(Model.Pic_AA))//追加加工
                        picAlterations.LoadAsync(Model.Pic_AA);
                    else
                        picAlterations.LoadAsync(Model.NoImgBig);
                    picAlterations.LoadCompleted += new System.ComponentModel.AsyncCompletedEventHandler(picAlterations_LoadCompleted);
                }
                else if (this.tab2D3D.SelectedIndex == 3)
                {
                    if (!IsClickCAD)
                    {
                        IsClickCAD = true;//第一次切换后，再次点击不在重新加载
                        this.lblCAD1.Text = LM.SetLanguage("lblCAD1");
                        this.lblCAD2.Text = LM.SetLanguage("lblCAD2");
                        this.lblDownDesc.Text = LM.SetLanguage("strCreateDataWait");
                        this.lblCAD3.Text = LM.SetLanguage("lblCAD3");
                        this.lblCAD4.Text = LM.SetLanguage("lblCAD4");
                        this.lblCADCondition.Text = LM.SetLanguage("lblCADCondition");
                        this.lblCAD5.Text = LM.SetLanguage("lblCAD5");
                        this.lblCAD51.Text = LM.SetLanguage("lblCAD51");
                        this.PicFormatDesc.Image = Image.FromFile(GetPicPath("PCAD_PlDesc.jpg"));
                        this.lblCAD6.Text = LM.SetLanguage("lblCAD6");
                        this.linkWeb.Text = LM.SetLanguage("linkWeb");

                        //语言显示的是当前界面语言，但是时间是区域办理时间
                        //this.lblCADTitle.Text = LM.SetLanguage("lblCADTitle");
                       // this.lblCADFont.Text = LM.SetLanguage("lblCADFont");
                       // this.lblCADHandle.Text = LM.SetLanguage("lblCADHandle");
                       // this.lblCAD_TelNum.Text = LM.SetLanguage("lblCAD_TelNum");
                       // this.lblCAD_FaxNum.Text = LM.SetLanguage("lblCAD_FaxNum");
                        //this.lblCADTime.Text = LM.SetLanguage("lblCADTime");


                       // this.lblQCTTitle.Text = LM.SetLanguage("lblQCTTitle");
                       //this.lblQCTFont.Text = LM.SetLanguage("lblQCTFont");
                       // this.lblQCTHandle.Text = LM.SetLanguage("lblQCTHandle");
                        //this.lblQCT_TelNum.Text = LM.SetLanguage("lblQCT_TelNum");
                        //this.lblQCT_FaxNum.Text = LM.SetLanguage("lblQCT_FaxNum");
                        //this.lblQCTTime.Text = LM.SetLanguage("lblQCTTime");

                        //语言显示的是当前界面语言，但是时间是区域办理时间
                        this.lblQCTTitle.Text = Model.PressService.GetF_QTCAD("1", Model.ShowLanguage);
                        this.lblQCTFont.Text = Model.PressService.GetF_QTCAD("2", Model.ShowLanguage);
                        this.lblQCTHandle.Text = Model.PressService.GetF_QTCAD("3", Model.ShowLanguage);
                        this.lblCADTitle.Text = Model.PressService.GetF_QTCAD("7", Model.ShowLanguage);
                        this.lblCADFont.Text = Model.PressService.GetF_QTCAD("8", Model.ShowLanguage);
                        this.lblCADHandle.Text = Model.PressService.GetF_QTCAD("6", Model.ShowLanguage);


                        //根据区域显示当前区域的联系方式
                        string[] Contacts = Model.PressService.GetM_QTCAD(Model.Region, Model.ShowLanguage).Split('#');
                        

                        this.lblQCT_TelNum.Text = Contacts[0];
                        this.lblQCT_FaxNum.Text = Contacts[1];
                        this.lblCAD_TelNum.Text = Contacts[2];
                        this.lblCAD_FaxNum.Text = Contacts[3];
                        this.lblQCTTime.Text = Contacts[4];
                        this.lblCADTime.Text = Contacts[5];

                        this.linkNx.Text = LM.SetLanguage("linkNx");
                        this.lblCADNew.Text = LM.SetLanguage("lblCADNew");
                        this.linkThere.Text = LM.SetLanguage("linkThere");
                        this.lblJuHao.Text = LM.SetLanguage("lblJuHao");
                        this.linkProE.Text = LM.SetLanguage("linkProE");
                        this.lblCADNewProE.Text = LM.SetLanguage("lblCADNew");
                        this.linkThereProE.Text = LM.SetLanguage("linkThere");

                        int DifferNum = 0;
                        if (LanguageManager.ShowLan == LanguageManager.CurrentLanguage.CHS)
                            DifferNum = 2;
                        this.lblCADCondition.Location = new Point(this.lblCAD4.Location.X + this.lblCAD4.Width - DifferNum, this.lblCADCondition.Location.Y);
                        this.lblCAD5.Location = new Point(this.lblCADCondition.Location.X + this.lblCADCondition.Width - DifferNum, this.lblCAD5.Location.Y);

                        #region 计算QCT控件坐标
                        SetControlLocation(lblQCTTel, lblQCTFont);
                        SetControlLocation(lblQCT_TelNum, lblQCTTel);
                        lblQCTFax.Location = new Point(lblQCTTel.Location.X, lblQCTFax.Location.Y);  //传真的坐标和电话的X坐标一致
                        SetControlLocation(lblQCT_FaxNum, lblQCTFax);
                        SetControlLocation(lblQCTTime, lblQCTHandle);
                        #endregion

                        #region 计算CAD控件坐标
                        SetControlLocation(lblCADTel, lblCADFont);
                        SetControlLocation(lblCAD_TelNum, lblCADTel);
                        lblCADFax.Location = new Point(lblCADTel.Location.X, lblCADFax.Location.Y);
                        SetControlLocation(lblCAD_FaxNum, lblCADFax);
                        SetControlLocation(lblCADTime, lblCADHandle);
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                Model.PressService.WriteApplicationErrorAsync(ex.Message, ex.Source, ex.StackTrace, Model.Region);
            }
        }

        /// <summary>
        /// 根据前边的控件长度、坐标，计算自己的控件坐标
        /// </summary>
        /// <param name="lblSelf">控件自己</param>
        /// <param name="lblPrev">上一个控件</param>
        private void SetControlLocation(Label lblSelf, Label lblPrev)
        {
            lblSelf.Location = new Point(lblPrev.Location.X + lblPrev.Width, lblSelf.Location.Y);
        }

        /// <summary>
        /// 下载当前型号所有的ocx显示追加工小图片
        /// </summary>
        /// <param name="TableName"></param>
        private void DownAddPic()
        {
            string[] names = Model.PressService.GetReprocessNames(Model.AlterationSheet, Model.ClassName);
            for (int i = 0; i < names.Length; i++)
            {
                switch (names[i].Length)
                {
                    case 1:
                        names[i] = "00" + names[i];
                        break;
                    case 2:
                        names[i] = "0" + names[i];
                        break;
                }
                string AddPicUrl = strWebUrl + Model.Pic_A_Path + names[i] + "_fit.jpg";
                if (CommonHelper.CheckUrl(AddPicUrl))
                    CommonHelper.DownServerPic(AddPicUrl);
            }
        }

        #region 3DView
        /// <summary>
        /// 加载hoops插件
        /// </summary>
        private bool LoadHoops()
        {
            try
            {
                this.axHoops3dStreamCtrl1.Size = new System.Drawing.Size(502, 286);
                this.axHoops3dStreamCtrl1.SetLanguage(LanguageIndex);
                if (!File.Exists(CommonHelper.ViewFlag))
                    File.Create(CommonHelper.ViewFlag);
                return true;
            }
            catch (Exception ex)
            {
                Model.PressService.WriteApplicationErrorAsync(ex.Message, ex.Source, ex.StackTrace, Model.Region);
                //MessageForm MsgFrom = new MessageForm("strInstall", "strInstallReset", "BtnInstall");
                //MsgFrom.ShowDialog();
                return false;
            }
        }

        /// <summary>
        /// 3D建模按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn3DBulid_Click(object sender, EventArgs e)
        {
            CreateAcisModel();
        }
        private void PicTab3D_Click(object sender, EventArgs e)
        {
            CreateAcisModel();
        }
        DialogResult result = DialogResult.OK;
        private void CreateAcisModel()
        {
            if (LoadHoops())
            {
                if (GetAcisParam())//如果参数正确，在进行3D文件生成并预览
                    AcisCreate();
            }
        }

        #region 检查本地3Dview插件的版本信息
        /// <summary>
        /// 检查本地3Dview插件的版本信息
        /// </summary>
        private void Check3DViewVersion()
        {
            if (File.Exists(CommonHelper.MisumiDataPath + "3Dview.ini"))
            {
                string[] SerVersion = Model.PressService.Get3DViewVersion().Split('.');
                string[] LocalVersion = null;
                using (FileStream fs = new FileStream(CommonHelper.MisumiDataPath + "3Dview.ini", FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(fs, true))
                    {
                        sr.ReadLine();//第一行是路径不需要
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            LocalVersion = line.Split('.');//获取第二行版本信息参数
                        }
                    }
                }
                if (LocalVersion == null)
                {
                    MessageForm MsgFrom = new MessageForm("strUpdate", "strUpdateReset", "BtnUpdate");
                    result = MsgFrom.ShowDialog();
                }
                else if (SerVersion.Length == 4 && LocalVersion.Length == 4)
                {
                    for (int i = 0; i < SerVersion.Length; i++)//遍历版本号比较大小
                    {
                        if (Convert.ToInt32(SerVersion[i]) > Convert.ToInt32(LocalVersion[i]))
                        {
                            MessageForm MsgFrom = new MessageForm("strUpdate", "strUpdateReset", "BtnUpdate");
                            result = MsgFrom.ShowDialog();
                        }
                    }
                }
            }
        }
        #endregion


        /// <summary>
        /// 获取建模参数
        /// </summary>
        private bool GetAcisParam()
        {
            this.axMisumiInterface_GPL_Web1.UniteParaAndRepro();//获取当前用户选择参数
            int Result = 0;// this.axMisumiInterface_GPL_Web1.GetOrderCorrect();
            if (Result == -1)//-1:表示没有选择参数
            {
                ShowMessageBox(LM.SetLanguage("strChoiceParam"), false);
                return false;
            }
            else if (Result == 1)//1:基本数据错误
            {
                ShowMessageBox(LM.SetLanguage("strBaseSizeError"), false);
                return false;
            }
            else if (Result == 2)//2:追加工数据错误
            {
                ShowMessageBox(LM.SetLanguage("strAlterError"), false);
                return false;
            }
            else if (Result == 3)//3:数据都错误
            {
                ShowMessageBox(LM.SetLanguage("strBaseSizeError"), false);
                return false;
            }
            Model.AcisNameList = this.axMisumiInterface_GPL_Web1.GetParaName();//获取建模所用字符串的变量名集合
            Model.AcisValueList = this.axMisumiInterface_GPL_Web1.GetParaValue();//获取建模所用字符串的变量值的集合
            Model.NewOrder = Model.AcisValueList.Split(';')[1].Trim();
            if (Model.NewOrder == OcxTemporary)
            {
                ShowMessageBox(LM.SetLanguage("strBaseSizeError"), false);
                return false;
            }
            if (Model.ModelType.Contains("LONG"))
                Model.ModelType = Model.ModelType.Replace("LONG", "Long");
            if (Model.ModelType == "RWR" || Model.ModelType == "C-MBSH" || Model.ModelType == "C-MBJH" || Model.ModelType == "C-MBSM" || Model.ModelType == "C-MBJM")
            {
                Model.ClassName = "STRIPPER GUIDE PINS & BUSHINGS";
            }
            Model.AcisData = Model.PressService.GetAcisData(Model.ClassName, Model.ModelType);
            return true;
        }

        private void AcisCreate()
        {
            try
            {
                this.tab2D3D.SelectedIndex = 2;
                //改变Tab标签图片背景
                this.PicTab3D.Image = Image.FromFile(GetPicPath("3D_on.jpg"));
                this.PicTabAdd.Image = Image.FromFile(GetPicPath("Add.jpg"));
                this.PicTab2D.Image = Image.FromFile(GetPicPath("2D.jpg"));
                this.PicTabCAD.Image = Image.FromFile(GetPicPath("CAD.jpg"));
                WaitBox wait = new WaitBox((obj, args) =>
                {
                    CreateAcisHsf();
                }, 50, LM.SetLanguage("strDataCreating"), false, false);
                wait.ShowDialog(this);
            }
            catch (Exception ex)
            {
                Model.PressService.WriteApplicationErrorAsync(ex.Message, ex.Source, ex.StackTrace, Model.Region);
                return;
            }
        }

        /// <summary>
        /// 建模函数
        /// </summary>
        private void CreateAcisHsf()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("——————————————————————————————————————————————————————————————————————————————————————————————————————————————");
            sb.AppendLine("Time:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            sb.AppendLine("AcisData:" + Model.AcisData);
            sb.AppendLine("ClassName:" + Model.ClassName);
            sb.AppendLine("AcisNameList:" + Model.AcisNameList);
            sb.AppendLine("AcisValueList:" + Model.AcisValueList);
            sb.AppendLine("UserName:" + Model.UserName);
            try
            {
                string FileName = Model.PressService.CreateAcisHsf(Model.AcisData, Model.ClassName, Model.AcisNameList, Model.AcisValueList, Model.UserName);
                if (Path.GetExtension(FileName).ToLower() == ".zip")
                {
                    sb.AppendLine("FileName:" + FileName);
                    FileName = FileName.Replace('/', '\\');
                    string filepath = CommonHelper.InternetCache + @"\Files\" + Model.NewOrder + ".zip";
                    CommonHelper.DownLoadResource(strWebUrl + FileName, filepath); //将文件下载到IE临时文件夹\files\路径下
                    ClientCreateZip zip = new ClientCreateZip();//解压缩
                    zip.ZipPath = filepath;
                    zip.UnZipDirectory = CommonHelper.InternetCache + @"\Files\";
                    zip.StartUnZip();
                    this.axHoops3dStreamCtrl1.Filename = filepath.Replace(".zip", ".hsf");//hoops浏览器显示hsf文件
                    #region 预览3D模型，添加一行处理数
                    Model.PressService.AddSecurityLogAsync(Model.UserName, Model.Region, Model.ModelType, "Previews", Model.NewOrder);
                    #endregion
                }
                else
                {
                    Model.PressService.WriteApplicationErrorAsync("\r\n失败模型：" + Model.ModelType, "\r\n附加数据：" + Model.AcisData + "\r\n模型变量名集合：" + Model.AcisNameList, "\r\n模型变量值集合：" + Model.AcisValueList, Model.Region);
                    ShowMessageBox(LM.SetLanguage("strCreateFileError"), false);
                }

            }
            catch (Exception ex)
            {
                Model.PressService.WriteApplicationErrorAsync(ex.Message + "\r\n建模错误的类型：" + Model.ModelType, ex.Source, ex.StackTrace, Model.Region);
                ShowMessageBox(LM.SetLanguage("strServerFileError"), false);
            }
            sb.AppendLine("——————————————————————————————————————————————————————————————————————————————————————————————————————————————");
            CommonHelper.WriteAcisValue(sb.ToString());
        }
        #endregion


        #region CAD数据下载
        /// <summary>
        /// 设置CAD选项卡页中Pan的显示状态
        /// </summary>
        /// <param name="Desc">PlDesc</param>
        /// <param name="Info">PanInfo</param>
        /// <param name="Down">PlDownload</param>
        /// <param name="plContactY">PanInfo的Y坐标</param>
        /// <param name="WordHeight">承接文字的底面板的高度</param>
        private void SetPanVisible(bool Desc, bool Contact, bool Down, int plContactY, int WordHeight)
        {
            try
            {
                PicFormatDesc.Visible = Desc;
                plContact.Visible = Contact;
                PlDownload.Visible = Down;
                plContact.Location = new Point(0, plContactY);
                plCADWord.Size = new Size(470, WordHeight);
            }
            catch (Exception ex)
            {
                Model.PressService.WriteApplicationErrorAsync(ex.Message, ex.Source, ex.StackTrace, Model.Region);
            }
        }

        #region 限制下拉列表选项
        int FileIndex = 0;
        void cboFileType_MouseWheel(object sender, MouseEventArgs e)
        {
            IsWheel = true;
            FileIndex = this.cboFileType.SelectedIndex;
        }
        bool IsWheel = false;
        private void cboFileType_MouseDown(object sender, MouseEventArgs e)
        {
            IsWheel = false;
        }
        /// <summary>
        /// 根据下拉列表中的type，显示相应的Pan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboFileType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.PicCreate.Image = Image.FromFile(GetPicPath("Create.jpg"));
                this.PicCreate.Enabled = true;
                if (IsWheel)
                    cboFileType.SelectedIndex = FileIndex;
                if (cboFileType.SelectedIndex == 0 || cboFileType.SelectedIndex == 5)
                {
                    CommonHelper.GrayImage(this.PicCreate);
                    this.PicCreate.Enabled = false;
                }
                else
                    SetPanVisible(true, true, false, 155, 400);
            }
            catch (Exception ex)
            {
                Model.PressService.WriteApplicationErrorAsync(ex.Message, ex.Source, ex.StackTrace, Model.Region);
            }
        }
        #endregion


        string FlatLog = "";  //Webservice生成标识类型
        string dl_Format = "";//履历返回标识
        /// <summary>
        /// 生成按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {

                if (GetAcisParam())
                {
                    string Type = cboFileType.Text.ToString().Trim().ToLower();
                    switch (Type)
                    {
                        case "step":
                            FlatLog = "STEP";
                            dl_Format = "STEP";
                            ShowWaitForm("step", Model.NewOrder, false);
                            break;
                        case "iges":
                            FlatLog = "IGES";
                            dl_Format = "IGES";
                            ShowWaitForm("iges", Model.NewOrder, false);
                            break;
                        case "acis(sat)":
                            FlatLog = "SAT";
                            dl_Format = "SAT";
                            ShowWaitForm("sat", Model.NewOrder, false);
                            break;
                        case "dwg":
                            FlatLog = "DWG";
                            dl_Format = "DWG >=14";
                            ShowWaitForm("dwg", Model.NewOrder, false);
                            break;
                        case "dxf":
                            FlatLog = "DXF";
                            dl_Format = "DXF V12";
                            ShowWaitForm("dxf", Model.NewOrder, false);
                            break;
                        case "parasolid":
                            FlatLog = "XT";
                            dl_Format = "parasolid";
                            ShowWaitForm("XT", Model.NewOrder, false);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Model.PressService.WriteApplicationErrorAsync(ex.Message, ex.Source, ex.StackTrace, Model.Region);
                MessageBox.Show(ex.Message.ToString());
            }
        }

        /// <summary>
        /// CAD下载文件类型生成等待窗体
        /// </summary>
        /// <param name="fileType">文件类型（扩展名）</param>
        /// <param name="fileName">文件名（组合文件名,省略掉扩展名）</param>
        /// <param name="IsNeed">ProE/NX的追加菜单标识</param>
        private void ShowWaitForm(string fileType, string fileName, bool IsNeed)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Time:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            sb.AppendLine("ModelType:" + Model.ModelType);
            sb.AppendLine("fileType:" + fileType);
            sb.AppendLine("UserName:" + Model.UserName);
            sb.AppendLine("AcisParam:"+Model.CreateAcisParam);
            try
            {
                plCADWord.Visible = false;
                PlDownload.Visible = true;
                Application.DoEvents();//主线程显示等待图片

                Model.CADFileName = Model.PressService.GetCADDataDownload(fileType, fileName, Model.UserName, Model.CreateAcisParam);
                Model.CADDataFilePath = strWebUrl + Model.CADFileName;
                if (Model.CADFileName == "empty") //如果没有转化成功为空
                {
                    ShowMessageBox(LM.SetLanguage("strCreateFileError"), true);
                    RecoveryCADLayout();
                    SetPanVisible(true, true, false, 155, 400);
                }
                else
                {
                    Model.PressService.AddSecurityLogAsync(Model.UserName, Model.Region, Model.ModelType, FlatLog, Model.NewOrder);
                    DownCADData();
                    SetPanVisible(true, true, false, 155, 400);
                }
            }
            catch (Exception ex)
            {
                Model.PressService.WriteApplicationErrorAsync(ex.Message, ex.Source, ex.StackTrace, Model.Region);
                ShowMessageBox(LM.SetLanguage("strServerFileError"), false);
            }
            sb.AppendLine("CADFileName:" + Model.CADFileName);
            sb.AppendLine("CADDataFilePath:" + Model.CADDataFilePath);
            sb.AppendLine("——————————————————————————————————————————————————————————————————————————————————————————————————————————————");
            CommonHelper.WriteAcisValue(sb.ToString());
        }

        /// <summary>
        /// 下载CAD数据
        /// </summary>
        private void DownCADData()
        {
            try
            {
                //只保留label控件，并更改颜色、文字
                this.PicDownWait.Visible = false;
                this.PicMisumi.Visible = false;
                this.lblDownDesc.Text = LM.SetLanguage("strCreateComplete");
                this.lblDownDesc.ForeColor = Color.Black;

                if (CommonHelper.CheckUrl(Model.CADDataFilePath))
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "ZIP|*.zip";//文件格式
                    sfd.FileName = Model.CADFileName.Substring(Model.CADFileName.LastIndexOf('/') + 1, Model.CADFileName.Length - Model.CADFileName.LastIndexOf('/') - 1);
                    //获取文件名称
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        WebClient client = new WebClient();
                        WaitBox wait = new WaitBox((obj, args) =>
                        {
                            client.DownloadFile(Model.CADDataFilePath, sfd.FileName);

                            ClientCreateZip zip = new ClientCreateZip();//解压缩
                            zip.ZipPath = sfd.FileName;
                            zip.UnZipDirectory = sfd.FileName.Replace(".zip", "");
                            zip.StartUnZip();
                            Process.Start(sfd.FileName.Replace(".zip", ""));
                        }, 600, LM.SetLanguage("strDownloading"), false, false);
                        wait.ShowDialog(this);
                        //多线程处理下载履历回传
                        Thread SendThread = new Thread(new ThreadStart(SendInfoToJap));
                        SendThread.SetApartmentState(ApartmentState.STA);
                        SendThread.Start();

                        StartKiller();//自动关闭提示框
                        ShowMessageBox(LM.SetLanguage("strDownComplate"), true);
                        wait.Dispose();
                        client.Dispose();
                    }
                    //当下载完后，显示文字说明面板，隐藏下载面板
                    RecoveryCADLayout();
                }
                else
                {
                    ShowMessageBox(LM.SetLanguage("strNoFile"), false);
                }
            }
            catch (Exception ex)
            {
                Model.PressService.WriteApplicationErrorAsync(ex.Message, ex.Source, ex.StackTrace, Model.Region);
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 下载完成后，恢复原状
        /// </summary>
        private void RecoveryCADLayout()
        {
            this.plCADWord.Visible = true;
            this.PlDownload.Visible = false;

            this.PicDownWait.Visible = true;
            this.PicMisumi.Visible = true;
            this.lblDownDesc.Text = LM.SetLanguage("strCreateDataWait");
            this.lblDownDesc.ForeColor = Color.Red;
        }
        /// <summary>
        /// CAD下载履历回传
        /// </summary>
        private void SendInfoToJap()
        {
            try
            {
                string productName = UrlEncode(Model.Series_Name);
                string productId = UrlEncode(Model.Series_Code);
                string productPageUrl = UrlEncode(Model.Page_Path);
                string productImgUrl = UrlEncode(Model.Main_Photo);
                string partNumber = UrlEncode(Model.NewOrder);
                string makerCd = UrlEncode(Model.Brd_Code);
                string makerName = UrlEncode(Model.Brd_Name);
                string dl_format = UrlEncode(dl_Format);

                #region 测试
                //string productName = UrlEncode("シャフトストレート");
                //string productId = UrlEncode("10300000120");
                //string productPageUrl = UrlEncode("http://www.stg.misumi-ec.com/ec/ItemDetail/10300000120.html");
                //string productImgUrl = UrlEncode("http://www.stg.misumi-ec.com/item/10300000120/main.jpg");
                //string partNumber = UrlEncode(Model.NewOrder);
                //string makerCd = UrlEncode("MSM");
                //string makerName = UrlEncode("ミスミ");
                //string dl_format = UrlEncode(dl_Format);
                //Model.DoMain = "http://www.stg.misumi-ec.com";
                #endregion

                string URL = Model.DoMain + CommonHelper.getSetConfig("ReturnURL") + "?productName=" + productName + "&productId=" + productId + "&productPageUrl=" + productPageUrl + "&productImgUrl=" + productImgUrl + "&partNumber=" + partNumber + "&makerCd=" + makerCd + "&makerName=" + makerName + "&dl_format=" + dl_format;
                WebBrowser web = new WebBrowser();
                web.Navigate(URL);
            }
            catch (Exception ex)
            {
                Model.PressService.WriteApplicationErrorAsync(ex.Message, ex.Source, ex.StackTrace, Model.Region);
            }
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
        #endregion

        #region 功能函数
        /// <summary>
        /// 在用户按下键盘enter事件时，将光标定位到txt文本框中，避免打开图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void axMisumiInterface_GPL_Web1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtNewOrder.Focus();
            }
        }

        public delegate void MyInvoke(string s, bool c);
        public void setlblNewModelValue(string value, bool IsOrder)
        {
            try
            {
                if (this.txtNewOrder.InvokeRequired)
                {
                    MyInvoke invoke = new MyInvoke(setlblNewModelValue);
                    this.Invoke(invoke, new object[] { value });
                }
                else
                {
                    if (IsOrder)
                    {
                        if (value.Length >= 45)
                        {
                            this.txtNewOrder.Font = new Font(new FontFamily("Arial"), 9, FontStyle.Bold);
                            this.txtNewOrder.Location = new Point(155, 9);
                        }
                        else
                        {
                            this.txtNewOrder.Font = new Font(new FontFamily("Arial"), 12, FontStyle.Bold);
                        }
                    }
                    else
                    {
                        if (value.Length >= 25)
                        {
                            this.txtNewOrder.Location = new Point(155, 12);
                            this.txtNewOrder.Height = 20;
                            LanguageManager.SetFont(this.txtNewOrder, 10, FontStyle.Bold);
                        }
                        else
                        {
                            this.txtNewOrder.Location = new Point(155, 9);
                            this.txtNewOrder.Height = 26;
                            LanguageManager.SetFont(this.txtNewOrder, 12, FontStyle.Bold);
                        }
                    }
                    if (CultureInfo.InstalledUICulture.Name == "zh-CN")
                        this.txtNewOrder.Text = value.Replace("_", @"\");
                    else if (CultureInfo.InstalledUICulture.Name == "ja-JP")
                        this.txtNewOrder.Text = value.Replace("_", "¥");
                    else
                        this.txtNewOrder.Text = value;
                }
            }
            catch (Exception ex)
            {
                Model.PressService.WriteApplicationErrorAsync(ex.Message, ex.Source, ex.StackTrace, Model.Region);
            }
        }

        /// <summary>
        /// 提示框
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="isTrue">true:正常信息，false：错误信息</param>
        public void ShowMessageBox(string msg, bool isTrue)
        {
            if (isTrue)
                MessageBox.Show(msg, LM.SetLanguage("strMsgTitle"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show(msg, LM.SetLanguage("strMsgTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// 组合各个语言的图片素材获取路径
        /// </summary>
        /// <param name="PicName"></param>
        /// <returns></returns>
        private string GetPicPath(string PicName)
        {
            return Application.StartupPath + LanguagePicFolder + PicName;
        }

        /// <summary>
        /// 退出应用程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PressForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.Dispose();
                Model.Dispose();
                GC.Collect();
                System.Environment.Exit(0);
            }
            catch (Exception ex)
            {
                Model.PressService.WriteApplicationErrorAsync(ex.Message, ex.Source, ex.StackTrace, Model.Region);
                System.Environment.Exit(0);
            }
        }
        #endregion

        #region 点击Tab菜单图片，切换选项卡图片
        private void PicTab2D_Click(object sender, EventArgs e)
        {
            this.PicTab2D.Image = Image.FromFile(GetPicPath("2D_on.jpg"));
            this.tab2D3D.SelectedIndex = 0;
            this.PicTabAdd.Image = Image.FromFile(GetPicPath("Add.jpg"));
            this.PicTab3D.Image = Image.FromFile(GetPicPath("3D.jpg"));
            this.PicTabCAD.Image = Image.FromFile(GetPicPath("CAD.jpg"));
        }

        private void PicTabAdd_Click(object sender, EventArgs e)
        {
            this.PicTabAdd.Image = Image.FromFile(GetPicPath("Add_on.jpg"));
            this.tab2D3D.SelectedIndex = 1;
            this.PicTab2D.Image = Image.FromFile(GetPicPath("2D.jpg"));
            this.PicTab3D.Image = Image.FromFile(GetPicPath("3D.jpg"));
            this.PicTabCAD.Image = Image.FromFile(GetPicPath("CAD.jpg"));
        }

        private void PicTabCAD_Click(object sender, EventArgs e)
        {
            try
            {
                PicCreate.Image = Image.FromFile(GetPicPath("Create.jpg"));
                this.PicTabCAD.Image = Image.FromFile(GetPicPath("CAD_on.jpg"));
                this.tab2D3D.SelectedIndex = 3;
                this.cboFileType.SelectedIndex = 1;
                this.PicTabAdd.Image = Image.FromFile(GetPicPath("Add.jpg"));
                this.PicTab2D.Image = Image.FromFile(GetPicPath("2D.jpg"));
                this.PicTab3D.Image = Image.FromFile(GetPicPath("3D.jpg"));

                PicFormatDesc.Visible = true;
                PlDownload.Visible = false;

                this.tb_CAD_DL.Focus();
                cboFileType.MouseWheel += new MouseEventHandler(cboFileType_MouseWheel);//CAD文件格式下拉列表跟随鼠标滚动而滚动
            }
            catch (Exception ex)
            {
                Model.PressService.WriteApplicationErrorAsync(ex.Message, ex.Source, ex.StackTrace, Model.Region);
            }
        }
        private void tb_CAD_DL_MouseClick(object sender, MouseEventArgs e)
        {
            this.tb_CAD_DL.Focus();
        }

        private void picTabPhoto_Click(object sender, EventArgs e)
        {
            this.picTabPhoto.Image = Image.FromFile(GetPicPath("Photo_on.jpg"));
            this.PicTabEC.Image = Image.FromFile(GetPicPath("EC.jpg"));
            this.PicTabT.Image = Image.FromFile(GetPicPath("T.jpg"));
            if (CommonHelper.CheckUrl(Model.PicPhoto))
                PicRightPanel.LoadAsync(Model.PicPhoto);
            else
                PicRightPanel.LoadAsync(Model.NoImgSmall);
        }

        private void PicTabEC_Click(object sender, EventArgs e)
        {
            this.picTabPhoto.Image = Image.FromFile(GetPicPath("Photo.jpg"));
            this.PicTabEC.Image = Image.FromFile(GetPicPath("EC_on.jpg"));
            this.PicTabT.Image = Image.FromFile(GetPicPath("T.jpg"));
            if (CommonHelper.CheckUrl(Model.PicEC))
                PicRightPanel.LoadAsync(Model.PicEC);
            else
                PicRightPanel.LoadAsync(Model.NoImgSmall);
        }

        private void PicTabT_Click(object sender, EventArgs e)
        {
            this.picTabPhoto.Image = Image.FromFile(GetPicPath("Photo.jpg"));
            this.PicTabEC.Image = Image.FromFile(GetPicPath("EC.jpg"));
            this.PicTabT.Image = Image.FromFile(GetPicPath("T_on.jpg"));
            if (CommonHelper.CheckUrl(Model.PicT))
                PicRightPanel.LoadAsync(Model.PicT);
            else
                PicRightPanel.LoadAsync(Model.NoImgSmall);
        }
        #endregion

        bool IsError = false;//标识order是否错误
        /// <summary>
        /// 随时获取ocx最新order，并更新到界面上
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                this.axMisumiInterface_GPL_Web1.UniteParaAndRepro();//获取当前用户选择参数
                if (this.axMisumiInterface_GPL_Web1.GetOrder() != "")//当用户选择参数行，才进行处理
                    if (this.axMisumiInterface_GPL_Web1.GetOrderCorrect())//如果Order正确
                    {
                        this.btn3DBulid.Enabled = true;
                        string Order = axMisumiInterface_GPL_Web1.GetOrder();
                        if (Order != OcxTemporary)
                        {
                            CommonHelper.InstanceOrder();
                            CommonHelper.GetNewOrder(Order);
                            setlblNewModelValue(CommonHelper.StrOrder.ToString(), true);//委托异步修改主线程的控件，显示新的order订单
                        }
                        IsError = false;
                    }
                    else if (!this.axMisumiInterface_GPL_Web1.GetOrderCorrect())
                    {
                        this.btn3DBulid.Enabled = false;
                        if (!string.IsNullOrEmpty(this.axMisumiInterface_GPL_Web1.GetOrder()) && this.axMisumiInterface_GPL_Web1.GetOrder() != "empty")
                            setlblNewModelValue(this.axMisumiInterface_GPL_Web1.GetOrder(), false);
                        else
                            setlblNewModelValue(LM.SetLanguage("strChoiceParam"), false);
                    }
            }
            catch (Exception ex)
            {
                timer1.Stop();
                Model.PressService.WriteApplicationErrorAsync(ex.Message, ex.Source, ex.StackTrace, Model.Region);
            }
        }

        #region 1秒后，自动关闭copy成功提示框
        [DllImport("user32.dll", EntryPoint = "FindWindow", CharSet = CharSet.Auto)]
        private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int PostMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        public const int WM_CLOSE = 0x10;
        public static string lpWindowsName = "";
        private void PicCopy_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtNewOrder.Text != "")
                {
                    Clipboard.SetText(this.txtNewOrder.Text);
                    StartKiller();
                    ShowMessageBox(LM.SetLanguage("CopyMsg"), true);
                }
            }
            catch (Exception ex)
            {
                Model.PressService.WriteApplicationErrorAsync(ex.Message, ex.Source, ex.StackTrace, Model.Region);
            }
        }

        private void StartKiller()
        {
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000; //秒启动 
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            KillMessageBox();
            //停止Timer 
            ((System.Windows.Forms.Timer)sender).Stop();
        }

        private void KillMessageBox()
        {
            //按照MessageBox的标题，找到MessageBox的窗口 
            IntPtr ptr = FindWindow(null, LM.SetLanguage("strMsgTitle"));
            if (ptr != IntPtr.Zero)
            {
                //找到则关闭MessageBox窗口 
                PostMessage(ptr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            }
        }
        #endregion

        #region 计算当窗体的分割线移动时，Tab图片显示变化
        private int PicWidth1 = 115;
        private int PicWidth2 = 230;
        private int PicWidth3 = 345;
        private int PicWidth4 = 460;
        private int ScSmallWidth = 510;

        private void ScSmall_SplitterMoved(object sender, SplitterEventArgs e)
        {
            int smallWidth = this.ScSmall.Panel1.Width;
            if (smallWidth >= PicWidth4 && smallWidth < ScSmallWidth)
            {
                this.label39.Visible = false;//空白图片
            }
            else if (smallWidth >= PicWidth3 && smallWidth < PicWidth4)
            {
                this.label39.Visible = false;
                this.PicTabCAD.Visible = false;
            }
            else if (smallWidth >= PicWidth2 && smallWidth < PicWidth3)
            {
                this.label39.Visible = false;
                this.PicTabCAD.Visible = false;
                this.PicTab3D.Visible = false;
            }
            else if (smallWidth >= PicWidth1 && smallWidth < PicWidth2)
            {
                this.label39.Visible = false;
                this.PicTabCAD.Visible = false;
                this.PicTab3D.Visible = false;
                this.PicTabAdd.Width = smallWidth - PicWidth1;
            }
            else if (smallWidth < PicWidth1)
            {
                this.label39.Visible = false;
                this.PicTabCAD.Visible = false;
                this.PicTab3D.Visible = false;
                this.PicTabAdd.Visible = false;
                this.PicTab2D.Visible = false;
            }

            if (smallWidth >= ScSmallWidth)
            {
                this.PicTab2D.Visible = true;
                this.PicTabAdd.Visible = true;
                this.PicTabAdd.Width = PicWidth1;
                this.PicTab3D.Visible = true;
                this.PicTabCAD.Visible = true;
                this.label39.Visible = true;
            }
            else if (smallWidth >= PicWidth4 && smallWidth < ScSmallWidth)
            {
                this.PicTab2D.Visible = true;
                this.PicTabAdd.Visible = true;
                this.PicTabAdd.Width = PicWidth1;
                this.PicTab3D.Visible = true;
                this.PicTabCAD.Visible = true;
            }
            else if (smallWidth >= PicWidth3 && smallWidth < PicWidth4)
            {
                this.PicTab2D.Visible = true;
                this.PicTabAdd.Visible = true;
                this.PicTabAdd.Width = PicWidth1;
                this.PicTab3D.Visible = true;
            }
            else if (smallWidth >= PicWidth2 && smallWidth < PicWidth3)
            {
                this.PicTab2D.Visible = true;
                this.PicTabAdd.Visible = true;
                this.PicTabAdd.Width = PicWidth1;
            }
            else if (smallWidth >= PicWidth1 && smallWidth < PicWidth2)
            {
                this.PicTab2D.Visible = true;
                this.PicTabAdd.Visible = true;
                this.PicTabAdd.Width = smallWidth - PicWidth1;
            }
            this.picTabPhoto.Location = new Point(smallWidth + 6, this.picTabPhoto.Location.Y);
            this.PicTabEC.Location = new Point(picTabPhoto.Location.X + 60, this.picTabPhoto.Location.Y);
            this.PicTabT.Location = new Point(PicTabEC.Location.X + 60, this.picTabPhoto.Location.Y);
        }
        #endregion

        #region 外链接
        /// <summary>
        /// 外形图放大
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lklbMagnify_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    Process pro = new Process();  //建立新的系统进程
                    Model.Pic2DBIG = strWebUrl + Model.PressService.GetPicPDFByType(Model.ClassName, Model.ModelType, "2DBIG", Model.ShowLanguage);  //设置需要打开的文件
                    if (CommonHelper.CheckUrl(Model.Pic2DBIG))
                        CommonHelper.CallSystemProcess(pro, CommonHelper.DownServerPic(Model.Pic2DBIG));
                    else
                        ShowMessageBox(LM.SetLanguage("strNoFile"), false);
                }
            }
            catch (Exception ex)
            {
                Model.PressService.WriteApplicationErrorAsync(ex.Message, ex.Source, ex.StackTrace, Model.Region);
                ShowMessageBox(LM.SetLanguage("strNoFile"), false);
                return;
            }
        }

        /// <summary>
        /// 追加加工全图放大
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lklbMagnifyPDF_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process pro = new Process();  //建立新的系统进程
                if (CommonHelper.CheckUrl(Model.Pic_AA))
                    CommonHelper.CallSystemProcess(pro, CommonHelper.DownServerPic(Model.Pic_AA));
                else
                    ShowMessageBox(LM.SetLanguage("strNoFile"), false);
            }
            catch (Exception ex)
            {
                Model.PressService.WriteApplicationErrorAsync(ex.Message, ex.Source, ex.StackTrace, Model.Region);
                ShowMessageBox(LM.SetLanguage("strNoFile"), false);
                return;
            }
        }

        private void lblCADCondition_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CADInfo cad = new CADInfo();
            cad.Show();
        }

        /// <summary>
        /// 使用方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PicTabHelp_Click(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(LM.SetLanguage("PicTabHelp")))
            //    Process.Start(LM.SetLanguage("PicTabHelp"));
            if (URLLink.Length > 0)
                Process.Start(URLLink[0]);
        }
        /// <summary>
        /// web咨询链接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkWeb_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (URLLink.Length > 0)
                Process.Start(URLLink[1]);
        }
        /// <summary>
        /// Nx/ProE最新版链接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkNxProELink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (URLLink.Length > 0)
                Process.Start(URLLink[2]);
        }
        #endregion

        #region 2D图放大镜效果
        /// <summary>
        /// 当2D图片加载完成时，处理2D放大图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Pic2D_LoadCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            Pic2D.Paint += new PaintEventHandler(Pic2D_Paint);
            Pic_Show.Paint += new PaintEventHandler(Pic_Show_Paint);
        }

        double Fact_X = 0;//原始图片实际的X坐标
        double Fact_Y = 0;//原始图片实际的Y坐标
        double Fact_W = 0;
        double Fact_H = 0;
        Bitmap bmp = null; //临时内存图片
        Rectangle destRect; //显示图片的矩形
        Rectangle srcRect; //源图片的矩形
        bool isMove = false;
        private void Pic2D_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                if (Pic2D.Image != null)
                {
                    if (isMove == true)
                    {
                        int _x;
                        int _y;
                        bmp = (Bitmap)Pic2D.Image;

                        //缩放比例
                        double rate_W = Convert.ToDouble(bmp.Width) / Pic2D.Width;//图片的宽度/控件的宽度=缩小比例
                        double rate_H = Convert.ToDouble(bmp.Height) / Pic2D.Height;
                        //缩放后的图片宽度、高度
                        double Small_H = Convert.ToDouble(bmp.Height) / rate_W;//图片的高度/缩小比例=实际的缩小高度（因为高度是随着宽度比例缩小的）
                        double Small_W = Convert.ToDouble(bmp.Width) / rate_H;

                        CommonHelper.DrawRectangle(Pic2D, bmp, Small_W, Small_H, e, out _x, out _y);
                        /*裁剪图片*/
                        if (Pic_Show.Image != null)
                        {
                            Pic_Show.Image.Dispose();
                        }

                        //高度小于宽度，按照宽度比例缩小
                        if (bmp.Height <= bmp.Width && Convert.ToDouble(Pic2D.Height) / Pic2D.Width < Convert.ToDouble(bmp.Height) / bmp.Width)
                        {
                            CommonHelper.TuGaoDaYuTuKuan(Pic2D, _x, _y, bmp, rate_H, ref Small_W, ref Fact_X, ref Fact_Y, ref Fact_W, ref Fact_H);
                        }
                        else if (bmp.Height <= bmp.Width && Convert.ToDouble(Pic2D.Height) / Pic2D.Width > Convert.ToDouble(bmp.Height) / bmp.Width)
                        {
                            CommonHelper.TuGaoXiaoYuTuKuan(Pic2D, _x, _y, bmp, rate_W, ref Small_H, ref Fact_X, ref Fact_Y, ref Fact_W, ref Fact_H);
                        }
                        else if (bmp.Height >= bmp.Width && Convert.ToDouble(Pic2D.Height) / Pic2D.Width > Convert.ToDouble(bmp.Height) / bmp.Width)
                        {
                            CommonHelper.TuGaoXiaoYuTuKuan(Pic2D, _x, _y, bmp, rate_W, ref Small_H, ref Fact_X, ref Fact_Y, ref Fact_W, ref Fact_H);
                        }
                        else if (bmp.Height >= bmp.Width && Convert.ToDouble(Pic2D.Height) / Pic2D.Width < Convert.ToDouble(bmp.Height) / bmp.Width)
                        {
                            CommonHelper.TuGaoDaYuTuKuan(Pic2D, _x, _y, bmp, rate_H, ref Small_W, ref Fact_X, ref Fact_Y, ref Fact_W, ref Fact_H);
                        }
                        if (Fact_W > 0 && Fact_H > 0)
                        {
                            //Bitmap bmp2 = bmp.Clone(new Rectangle(Convert.ToInt32(Fact_X), Convert.ToInt32(Fact_Y), Convert.ToInt32(Fact_W), Convert.ToInt32(Fact_H)), Pic2D.Image.PixelFormat);
                            //Pic_Show.Image = bmp2;
                            Pic_Show.SizeMode = PictureBoxSizeMode.Zoom;
                            Pic_Show.Dock = DockStyle.Fill;
                            Pic_Show.Visible = true;
                            Pic_Show.BringToFront();
                            isMove = false;
                        }
                        else
                            Pic_Show.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Model.PressService.WriteApplicationErrorAsync(ex.Message, ex.Source, ex.StackTrace, Model.Region);
                Pic_Show.Visible = false;
                Pic2D.Refresh();
            }
        }
        void Pic_Show_Paint(object sender, PaintEventArgs e)
        {
            DrawImageRectanct(e);
        }

        private void DrawImageRectanct(PaintEventArgs e)
        {
            if (bmp != null)
            {
                // Create rectangle for displaying image.
                destRect = new Rectangle(0, 0, Pic_Show.Width, Pic_Show.Height);
                // Create rectangle for source image.
                srcRect = new Rectangle(Convert.ToInt32(Fact_X), Convert.ToInt32(Fact_Y), Convert.ToInt32(Fact_W), Convert.ToInt32(Fact_H));
                e.Graphics.DrawImage(bmp, destRect, srcRect, GraphicsUnit.Pixel);
            }
        }

        /// <summary>
        /// 当用户将鼠标划过2D图时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pic2D_MouseMove(object sender, MouseEventArgs e)
        {
            Pic2D.Focus();
            isMove = true;
            CommonHelper.movedPoint_X = e.X;
            CommonHelper.movedPoint_Y = e.Y;
            Pic2D.Refresh();
            Pic_Show.Refresh();
        }
        /// <summary>
        /// 当鼠标移出2D图片时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pic2D_MouseLeave(object sender, EventArgs e)
        {
            isMove = false;
            Pic_Show.Visible = false;
            Pic2D.Refresh();//重新刷新，去掉矩形阴影
            Pic_Show.Refresh();
        }
        #endregion

        #region 追加工图片放大镜效果
        void picAlterations_LoadCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            picAlterations.Paint += new PaintEventHandler(picAlterations_Paint);
        }
        bool isAddMove = false;
        private void picAlterations_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                if (picAlterations.Image != null)
                {
                    if (isAddMove == true)
                    {
                        int _x;
                        int _y;
                        bmp = (Bitmap)picAlterations.Image;
                        //缩放比例
                        double rate_W = Convert.ToDouble(bmp.Width) / picAlterations.Width;//图片的宽度/控件的宽度=缩小比例
                        double rate_H = Convert.ToDouble(bmp.Height) / picAlterations.Height;

                        //缩放后的图片宽度、高度
                        double Small_H = Convert.ToDouble(bmp.Height) / rate_W;//图片的高度/缩小比例=实际的缩小高度（因为高度是随着宽度比例缩小的）
                        double Small_W = Convert.ToDouble(bmp.Width) / rate_H;

                        CommonHelper.DrawRectangle(picAlterations, bmp, Small_W, Small_H, e, out _x, out _y);

                        /*裁剪图片*/
                        if (Pic_Show.Image != null)
                        {
                            Pic_Show.Image.Dispose();
                        }
                        if (bmp.Height <= bmp.Width && Convert.ToDouble(picAlterations.Height) / picAlterations.Width < Convert.ToDouble(bmp.Height) / bmp.Width)
                        {
                            CommonHelper.TuGaoDaYuTuKuan(picAlterations, _x, _y, bmp, rate_H, ref Small_W, ref Fact_X, ref Fact_Y, ref Fact_W, ref Fact_H);
                        }
                        else if (bmp.Height <= bmp.Width && Convert.ToDouble(picAlterations.Height) / picAlterations.Width > Convert.ToDouble(bmp.Height) / bmp.Width)
                        {
                            CommonHelper.TuGaoXiaoYuTuKuan(picAlterations, _x, _y, bmp, rate_W, ref Small_H, ref Fact_X, ref Fact_Y, ref Fact_W, ref Fact_H);
                        }
                        else if (bmp.Height >= bmp.Width && Convert.ToDouble(picAlterations.Height) / picAlterations.Width > Convert.ToDouble(bmp.Height) / bmp.Width)
                        {
                            CommonHelper.TuGaoXiaoYuTuKuan(picAlterations, _x, _y, bmp, rate_W, ref Small_H, ref Fact_X, ref Fact_Y, ref Fact_W, ref Fact_H);
                        }
                        else if (bmp.Height >= bmp.Width && Convert.ToDouble(picAlterations.Height) / picAlterations.Width < Convert.ToDouble(bmp.Height) / bmp.Width)
                        {
                            CommonHelper.TuGaoDaYuTuKuan(picAlterations, _x, _y, bmp, rate_H, ref Small_W, ref Fact_X, ref Fact_Y, ref Fact_W, ref Fact_H);
                        }
                        if (Fact_W > 0 && Fact_H > 0)
                        {
                            //Bitmap bmp2 = bmp.Clone(new Rectangle(Convert.ToInt32(Fact_X), Convert.ToInt32(Fact_Y), Convert.ToInt32(Fact_W), Convert.ToInt32(Fact_H)), Pic2D.Image.PixelFormat);
                            //Pic_Show.Image = bmp2;
                            Pic_Show.SizeMode = PictureBoxSizeMode.Zoom;
                            Pic_Show.Dock = DockStyle.Fill;
                            Pic_Show.Visible = true;
                            Pic_Show.BringToFront();
                            isAddMove = false;
                        }
                        else
                            Pic_Show.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Model.PressService.WriteApplicationErrorAsync(ex.Message, ex.Source, ex.StackTrace, Model.Region);
                Pic_Show.Visible = false;
                picAlterations.Refresh();
            }
        }

        private void picAlterations_MouseMove(object sender, MouseEventArgs e)
        {
            picAlterations.Focus();
            isAddMove = true;
            CommonHelper.movedPoint_X = e.X;
            CommonHelper.movedPoint_Y = e.Y;
            picAlterations.Refresh();
            Pic_Show.Refresh();
        }

        private void picAlterations_MouseLeave(object sender, EventArgs e)
        {
            isAddMove = false;
            Pic_Show.Visible = false;
            picAlterations.Refresh();
            Pic_Show.Refresh();
        }
        #endregion
    }
}
