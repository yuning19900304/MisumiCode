using System;
using Misumi_Client.MoldWebService;
using Misumi_Client.PressWebService;

namespace Misumi_Client.Common
{
    public class ModelObject : IDisposable
    {
        private MisumiWebService moldService;
        /// <summary>
        /// Mold服务
        /// </summary>
        public MisumiWebService MoldService
        {
            get { return moldService; }
            set { moldService = value; }
        }

        private GlobalPressWebService pressService;
        /// <summary>
        /// Press服务
        /// </summary>
        public GlobalPressWebService PressService
        {
            get { return pressService; }
            set { pressService = value; }
        }

        private string serviceUrl;
        /// <summary>
        /// MisumiService的URl
        /// </summary>
        public string ServiceUrl
        {
            get { return serviceUrl; }
            set { serviceUrl = value; }
        }
        private string className;
        /// <summary>
        /// 大类名称
        /// </summary>
        public string ClassName
        {
            get { return className; }
            set { className = value; }
        }

        private string newOrder = "";
        /// <summary>
        /// 产生的新的订单号
        /// </summary>
        public string NewOrder
        {
            get { return newOrder; }
            set { newOrder = value; }
        }

        private bool isSpecial;
        /// <summary>
        /// 标识是否是特例处理
        /// </summary>
        public bool IsSpecial
        {
            get { return isSpecial; }
            set { isSpecial = value; }
        }

        #region 统合——>MoldExpress参数
        private string product_ID;
        /// <summary>
        /// 产品ID
        /// </summary>
        public string Product_ID
        {
            get { return product_ID; }
            set { product_ID = value; }
        }
        private string userName;
        /// <summary>
        /// 用户名(统和参数customer_cd)
        /// </summary>
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        private string e_CatalogType;
        /// <summary>
        /// 保存统合网站传递过来的Type类型
        /// </summary>
        public string E_CatalogType
        {
            get { return e_CatalogType; }
            set { e_CatalogType = value; }
        }
        private string modelType;
        /// <summary>
        /// Mex零件类型
        /// </summary>
        public string ModelType
        {
            get { return modelType; }
            set { modelType = value; }
        }
        private string oldOrder;
        /// <summary>
        /// 统合页面订单号（统和PN参数）
        /// </summary>
        public string OldOrder
        {
            get { return oldOrder; }
            set { oldOrder = value; }
        }
        private string main_Photo;
        /// <summary>
        /// 商品写真URL
        /// </summary>
        public string Main_Photo
        {
            get { return main_Photo; }
            set { main_Photo = value; }
        }
        private string page_Path;
        /// <summary>
        /// 商品页的URL
        /// </summary>
        public string Page_Path
        {
            get { return page_Path; }
            set { page_Path = value; }
        }
        private string brd_Code;
        /// <summary>
        /// 品牌编码
        /// </summary>
        public string Brd_Code
        {
            get { return brd_Code; }
            set { brd_Code = value; }
        }
        private string brd_Name;
        /// <summary>
        /// 品牌名称
        /// </summary>
        public string Brd_Name
        {
            get { return brd_Name; }
            set { brd_Name = value; }
        }
        private string series_Code;
        /// <summary>
        /// 系列编码
        /// </summary>
        public string Series_Code
        {
            get { return series_Code; }
            set { series_Code = value; }
        }
        private string series_Name;
        /// <summary>
        /// 系列名称
        /// </summary>
        public string Series_Name
        {
            get { return series_Name; }
            set { series_Name = value; }
        }
        private string doMain;
        /// <summary>
        /// 域
        /// </summary>
        public string DoMain
        {
            get { return doMain; }
            set { doMain = value; }
        }
        private string region;
        /// <summary>
        /// 分公司、地区
        /// </summary>
        public string Region
        {
            get { return region; }
            set { region = value; }
        }
        private string uRLLanguage;
        /// <summary>
        /// URL语言
        /// </summary>
        public string URLLanguage
        {
            get { return uRLLanguage; }
            set { uRLLanguage = value; }
        }
        private string showLanguage;
        /// <summary>
        /// 界面显示语言
        /// </summary>
        public string ShowLanguage
        {
            get { return showLanguage; }
            set { showLanguage = value; }
        }
        #endregion


        #region ocx数据相关

        private string keZiType;
        /// <summary>
        /// 刻字类型
        /// </summary>
        public string KeZiType
        {
            get { return keZiType; }
            set { keZiType = value; }
        }
        private string orderTemplate;
        /// <summary>
        /// Order模板名称
        /// </summary>
        public string OrderTemplate
        {
            get { return orderTemplate; }
            set { orderTemplate = value; }
        }
        private string alterationSheet;
        /// <summary>
        /// 追加工Sheet名称
        /// </summary>
        public string AlterationSheet
        {
            get { return alterationSheet; }
            set { alterationSheet = value; }
        }
        private string ocxPicAPath;
        /// <summary>
        /// ocx的追加工小图路径
        /// </summary>
        public string OcxPicAPath
        {
            get { return ocxPicAPath; }
            set { ocxPicAPath = value; }
        }

        private string page_C;
        /// <summary>
        /// Page_C
        /// </summary>
        public string Page_C
        {
            get { return page_C; }
            set { page_C = value; }
        }

        private string ocxPicANum;
        /// <summary>
        /// 追加工小图片的编号OcxPicANum
        /// </summary>
        public string OcxPicANum
        {
            get { return ocxPicANum; }
            set { ocxPicANum = value; }
        }

        private string baseSizeHeader;
        /// <summary>
        /// 基本尺寸表头
        /// </summary>
        public string BaseSizeHeader
        {
            get { return baseSizeHeader; }
            set { baseSizeHeader = value; }
        }
        private string baseLangHeader;
        /// <summary>
        /// 基本尺寸语言表头
        /// </summary>
        public string BaseLangHeader
        {
            get { return baseLangHeader; }
            set { baseLangHeader = value; }
        }
        private string reproTableName;
        /// <summary>
        /// 追加工表名
        /// </summary>
        public string ReproTableName
        {
            get { return reproTableName; }
            set { reproTableName = value; }
        }
        private string[] baseList;
        /// <summary>
        /// 基本尺寸ocx数据
        /// </summary>
        public string[] BaseList
        {
            get { return baseList; }
            set { baseList = value; }
        }
        private string[] reproList;
        /// <summary>
        /// 追加工ocx数据
        /// </summary>
        public string[] ReproList
        {
            get { return reproList; }
            set { reproList = value; }
        }
        #endregion


        #region 建模相关
        private bool isCreateAcis;
        /// <summary>
        /// 是否建模
        /// </summary>
        public bool IsCreateAcis
        {
            get { return isCreateAcis; }
            set { isCreateAcis = value; }
        }
        private string acisNameList;
        /// <summary>
        /// 建模的变量名集合
        /// </summary>
        public string AcisNameList
        {
            get { return acisNameList; }
            set { acisNameList = value; }
        }
        private string acisValueList;
        /// <summary>
        /// 建模变量名的值集合
        /// </summary>
        public string AcisValueList
        {
            get { return acisValueList; }
            set { acisValueList = value; }
        }
        /// <summary>
        /// 组合建模参数集合，CAD数据生成之前需要用
        /// </summary>
        public string CreateAcisParam
        {
            get
            {
                if (!string.IsNullOrEmpty(this.AcisData) && !string.IsNullOrEmpty(ClassName) && !string.IsNullOrEmpty(AcisNameList) && !string.IsNullOrEmpty(AcisValueList))
                    return AcisData + "!" + ClassName + "!" + AcisNameList + "!" + AcisValueList;
                else
                    return "";
            }
        }

        private string acisData;
        /// <summary>
        /// 建模追加数据
        /// </summary>
        public string AcisData
        {
            get { return acisData; }
            set { acisData = value; }
        }
        #endregion


        #region 图片相关
        private string pic2D;
        /// <summary>
        /// 2D图纸
        /// </summary>
        public string Pic2D
        {
            get { return pic2D; }
            set { pic2D = value; }
        }
        private string pic2DBIG;
        /// <summary>
        /// 2D放大图
        /// </summary>
        public string Pic2DBIG
        {
            get { return pic2DBIG; }
            set { pic2DBIG = value; }
        }
        private string pic_AA;
        /// <summary>
        /// 追加加工图片
        /// </summary>
        public string Pic_AA
        {
            get { return pic_AA; }
            set { pic_AA = value; }
        }
        private string picPhoto;
        /// <summary>
        /// 照片
        /// </summary>
        public string PicPhoto
        {
            get { return picPhoto; }
            set { picPhoto = value; }
        }
        private string picEC;
        /// <summary>
        /// 辅助资料
        /// </summary>
        public string PicEC
        {
            get { return picEC; }
            set { picEC = value; }
        }
        private string picT;
        /// <summary>
        /// 前段形状
        /// </summary>
        public string PicT
        {
            get { return picT; }
            set { picT = value; }
        }
        private string pic_A_Path;
        /// <summary>
        /// ocx追加工小图下载路径
        /// </summary>
        public string Pic_A_Path
        {
            get { return pic_A_Path; }
            set { pic_A_Path = value; }
        }

        private string pic_ANum;
        /// <summary>
        /// 追加工小图片编号
        /// </summary>
        public string Pic_ANum
        {
            get { return pic_ANum; }
            set { pic_ANum = value; }
        }

        /// <summary>
        /// 小图无
        /// </summary>
        public string NoImgSmall
        {
            get { return ServiceUrl + CommonHelper.getSetConfig("strNoImgSmall"); }
        }
        /// <summary>
        /// 大图无
        /// </summary>
        public string NoImgBig
        {
            get { return ServiceUrl + CommonHelper.getSetConfig("strNoImgBig"); }
        }
        #endregion


        #region CAD数据相关
        private string cADFileName;
        /// <summary>
        /// 保存数据文件名
        /// </summary>
        public string CADFileName
        {
            get { return cADFileName; }
            set { cADFileName = value; }
        }

        private string cADDataFilePath;
        /// <summary>
        /// 保存服务器CAD数据文件的路径
        /// </summary>
        public string CADDataFilePath
        {
            get { return cADDataFilePath; }
            set { cADDataFilePath = value; }
        }
        #endregion

        public void Dispose()
        {
            Dispose(true);
        }
        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="isDisposing">如果应释放托管资源，为 true；否则为 false</param>
        protected virtual void Dispose(bool isDisposing)
        {
            // Don't dispose more than one 
            if (this == null)
                return;
            if (isDisposing)
            {
                GC.SuppressFinalize(this);
            }
        }
    }
}
