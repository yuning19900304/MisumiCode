using System;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Misumi_Client.Mold;

namespace Misumi_Client.Common
{
    public static class CommonHelper
    {
        public static string InternetCache = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);//放大图片存放路径
        public static string MoldLanguagePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\MISUMI\Mold\";//用户选择语言配置文件存放路径
        public static string PressLanguagePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\MISUMI\Press\";//用户选择语言配置文件存放路径
        public static string MisumiDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\MISUMI\Data\";
        public static string AppPath = Application.StartupPath.ToString();
        public static string MoldOcx = AppPath + @"\Mold.msm";
        public static string PressOcx = AppPath + @"\Press.msm";
        public static string ViewFlag = AppPath + @"\3DView.msm";
        /// <summary>
        /// Check服务器的URl是否存在
        /// </summary>
        /// <param name="UrlPath"></param>
        /// <returns></returns>
        public static bool CheckUrl(string UrlPath)
        {
            try
            {
                HttpWebRequest HWR = (HttpWebRequest)WebRequest.Create(UrlPath);
                HttpWebResponse HTWS = (HttpWebResponse)HWR.GetResponse();
                if (HTWS.ContentLength != 0)
                {
                    HTWS.Close();
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 获取该服务器上目录，将图片存在与服务器相对应的目录下
        /// </summary>
        /// <param name="strPic2D"></param>
        /// <returns></returns>
        public static string DownServerPic(string UrlPath)
        {
            int Index = UrlPath.IndexOf("Interface");
            string LocalPath = UrlPath.Substring(Index - 1).Replace('/', '\\');
            DownLoadResource(UrlPath, InternetCache + LocalPath);
            return InternetCache + LocalPath;
        }

        /// <summary>
        /// 下载到Miusmidata\Interface文件夹内
        /// </summary>
        /// <param name="strPic2D"></param>
        /// <returns></returns>
        public static string DownFiles(string UrlPath, string SavePath)
        {
            int Index = UrlPath.IndexOf("Interface");
            string LocalPath = UrlPath.Substring(Index - 1);
            DownLoadResource(UrlPath, SavePath + LocalPath);
            return SavePath + LocalPath;
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="strUrl">URL</param>
        /// <param name="strPath">SavePath</param>
        /// <returns></returns>
        public static void DownLoadResource(string strUrl, string strPath)
        {
            WebClient webC = new WebClient();
            try
            {
                string FolderPath = strPath.Substring(0, strPath.Replace('\\', '/').LastIndexOf('/'));
                if (!Directory.Exists(FolderPath))
                    Directory.CreateDirectory(FolderPath);

                //if (!File.Exists(strPath))如果下载错误的hsf文件之后
                if (Path.GetExtension(strPath) == ".zip")
                {
                    webC.DownloadFile(strUrl, strPath);
                }
                else
                {
                    if (!File.Exists(strPath))
                    {
                        webC.DownloadFile(strUrl, strPath);
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 调用系统默认的打开文件方式，打开文件
        /// </summary>
        /// <param name="pro"></param>
        /// <param name="FileName"></param>
        public static void CallSystemProcess(Process pro, string FileName)
        {
            try
            {
                pro.StartInfo.FileName = FileName;
                //pro.StartInfo.Arguments = "rundll32.exe C://Windows//System32//shimgvw.dll,ImageView_FullScreen";  //设置系统运行参数，最大化窗口图片
                //pro.StartInfo.UseShellExecute = true;//是否使用shell执行程序，系统默认为true，可以不设，若设置必须为true；
                //pro.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                pro.Start();
                pro.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }


        /// <summary>
        /// 读取配置文件
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public static string getSetConfig(string keyName)
        {
            return ConfigurationManager.AppSettings[keyName].ToString();
        }

        /// <summary>
        /// 创建文件夹路径
        /// </summary>
        /// <param name="path"></param>
        public static void createDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        /// <summary>
        /// 删除指定的文件
        /// </summary>
        /// <param name="filePath"></param>
        public static void DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        /// <summary>
        /// 遍历当前母控件下所有的子控件
        /// </summary>
        /// <param name="controls"></param>
        public static void GetControls(Control.ControlCollection controls)
        {
            foreach (Control con in controls)
            {
                if (con is PictureBox)
                {
                    GrayImage(con as PictureBox);
                    con.Enabled = false;
                }
                else if (con.Controls.Count > 0)
                    GetControls(con.Controls);
            }
        }

        /// <summary>
        /// 图片置灰
        /// </summary>
        /// <param name="pb"></param>
        public static void GrayImage(PictureBox pb)
        {
            if (pb.Image != null)
            {
                Bitmap src = new Bitmap(pb.Image);
                Bitmap bitmap = new Bitmap(src.Width, src.Height);
                Color pixel;
                for (int x = 0; x < src.Width; x++)
                {
                    for (int y = 0; y < src.Height; y++)
                    {
                        pixel = src.GetPixel(x, y);
                        int r, g, b, Result = 0;
                        r = pixel.R;
                        g = pixel.G;
                        b = pixel.B;

                        int iType = 0;
                        switch (iType)
                        {
                            case 0://平均值法
                                Result = ((r + g + b) / 3);
                                break;
                            case 1:
                                Result = r < g ? r : g;
                                Result = Result < b ? Result : b;
                                break;
                            case 2://加权平均值
                                Result = ((int)(0.7 * r) + (int)(0.2 * g) + (int)(0.1 * b));
                                break;
                        }
                        bitmap.SetPixel(x, y, Color.FromArgb(Result, Result, Result));
                    }
                }
                pb.Image = bitmap;
            }
        }

        public static int movedPoint_X, movedPoint_Y;//鼠标移动后点的坐标
        //选取区域的大小
        const int rect_W = 80;
        const int rect_H = 120;
        public static double SpaceWidth, SpaceHeight = 0;//缩放后的空白宽度、高度

        /// <summary>
        /// 画选取区域的阴影部分
        /// </summary>
        /// <param name="e"></param>
        /// <param name="_x"></param>
        /// <param name="_y"></param>
        public static void DrawRectangle(PictureBox PicBox, Bitmap bmp, double Small_W, double Small_H, PaintEventArgs e, out int _x, out int _y)
        {
            Graphics g = e.Graphics;
            SpaceWidth = (PicBox.Width - Convert.ToInt32(Small_W)) / 2;
            SpaceHeight = (PicBox.Height - Convert.ToInt32(Small_H)) / 2;//空白高度
            /*画长方形*/
            _x = movedPoint_X - rect_W / 2;//长方形X坐标
            _y = movedPoint_Y - rect_H / 2;//长方形Y坐标
            _x = _x < 0 ? 0 : _x;//如果X坐标小于0，设置为从0开始
            _y = _y < 0 ? 0 : _y;
            int _xStart, _xEnd, _yStart, _yEnd = 0;//阴影部分的起始横纵坐标

            #region 限制阴影部分不能移出图片范围
            if (bmp.Height <= bmp.Width && Convert.ToDouble(PicBox.Height) / PicBox.Width < Convert.ToDouble(bmp.Height) / bmp.Width)
            {
                _xStart = Convert.ToInt32(SpaceWidth);
                _xEnd = Convert.ToInt32(SpaceWidth + Small_W) - rect_W;
                _x = _x <= _xStart ? _xStart : _x;
                _x = _x >= _xEnd ? _xEnd : _x;
                _y = _y >= PicBox.Height - rect_H - 1 ? PicBox.Height - rect_H - 1 : _y;//不会移出下边框
            }
            else if (bmp.Height <= bmp.Width && Convert.ToDouble(PicBox.Height) / PicBox.Width > Convert.ToDouble(bmp.Height) / bmp.Width)
            {
                _x = _x >= PicBox.Width - rect_W - 1 ? PicBox.Width - rect_W - 1 : _x; //不会移出右侧边框
                _yStart = Convert.ToInt32(SpaceHeight);
                _yEnd = Convert.ToInt32(SpaceHeight + Small_H) - rect_H;
                _y = _y <= _yStart ? _yStart : _y;
                _y = _y >= _yEnd ? _yEnd : _y;
            }
            else if (bmp.Height >= bmp.Width && Convert.ToDouble(PicBox.Height) / PicBox.Width > Convert.ToDouble(bmp.Height) / bmp.Width)
            {
                _x = _x >= PicBox.Width - rect_W - 1 ? PicBox.Width - rect_W - 1 : _x;
                _yStart = Convert.ToInt32(SpaceHeight);
                _yEnd = Convert.ToInt32(SpaceHeight + Small_H) - rect_H;
                _y = _y <= _yStart ? _yStart : _y;
                _y = _y >= _yEnd ? _yEnd : _y;
            }
            else if (bmp.Height >= bmp.Width && Convert.ToDouble(PicBox.Height) / PicBox.Width < Convert.ToDouble(bmp.Height) / bmp.Width)
            {
                _xStart = Convert.ToInt32(SpaceWidth);
                _xEnd = Convert.ToInt32(SpaceWidth + Small_W) - rect_W;
                _x = _x <= _xStart ? _xStart : _x;
                _x = _x >= _xEnd ? _xEnd : _x;
                _y = _y >= PicBox.Height - rect_H - 3 ? PicBox.Height - rect_H - 3 : _y;
            }
            #endregion

            Rectangle rect = new Rectangle(_x, _y, rect_W, rect_H);
            LinearGradientBrush baseBackground = new LinearGradientBrush(rect,
                   Color.FromArgb(128, 236, 236, 236),
                   Color.FromArgb(128, 236, 236, 236),
                   270, false);
            g.FillRectangle(baseBackground, rect);
            //画笔颜色
            Pen pen = new Pen(Color.FromArgb(255, 0, 0));
            pen.DashStyle = DashStyle.Dash;//虚线
            g.DrawLine(pen, new Point(_x, _y), new Point(_x + rect_W, _y));
            g.DrawLine(pen, new Point(_x, _y), new Point(_x, _y + rect_H));
            g.DrawLine(pen, new Point(_x, _y + rect_H), new Point(_x + rect_W, _y + rect_H));
            g.DrawLine(pen, new Point(_x + rect_W, _y), new Point(_x + rect_W, _y + rect_H));
        }

        /// <summary>
        /// 图片高度小于图片宽度
        /// </summary>
        /// <param name="_x">矩形X坐标</param>
        /// <param name="_y">矩形Y坐标</param>
        /// <param name="bmp">克隆图片</param>
        /// <param name="rate_W">宽度缩小比例</param>
        /// <param name="Small_H">缩小的图片高度</param>
        /// <param name="Fact_X">原始图片实际的X坐标</param>
        /// <param name="Fact_Y">原始图片实际的Y坐标</param>
        /// <param name="Fact_W">原始图片实际的宽度</param>
        /// <param name="Fact_H">原始图片实际的高度</param>
        public static void TuGaoXiaoYuTuKuan(PictureBox PicBox, int _x, int _y, Bitmap bmp, double rate_W, ref double Small_H, ref double Fact_X, ref double Fact_Y, ref double Fact_W, ref double Fact_H)
        {
            try
            {
                Fact_Y = Convert.ToInt32(rate_W * (_y - SpaceHeight));
                if (_y <= SpaceHeight)//如果方框y坐标小于空白高度，那么实际y坐标从0开始
                    Fact_Y = 0;
                Fact_X = Convert.ToInt32(rate_W * _x);
                if (Fact_Y > 0)
                {
                    Fact_H = Convert.ToInt32(rect_H * bmp.Height / Small_H);//切图的高度(方框高度/缩小图片高度=实际切图高度/实际图片高度)
                    if (Fact_H + Fact_Y > bmp.Height)
                        Fact_H = bmp.Height - Fact_Y;
                    Fact_W = Convert.ToInt32(rect_W * bmp.Width / PicBox.Width);
                    if (Fact_W + Fact_X > bmp.Width)
                        Fact_W = bmp.Width - Fact_X;
                }
                else
                {
                    //实际小切图高度(rect_H)=方框高度-空白高度-方框y坐标
                    double Smallrect_H = 120 - (SpaceHeight - _y);
                    Fact_H = Convert.ToInt32(Smallrect_H * bmp.Height / Small_H);//切图的高度(方框高度/缩小图片高度=实际切图高度/实际图片高度)
                    if (Fact_H + Fact_Y > bmp.Height)
                        Fact_H = bmp.Height - Fact_Y;
                    Fact_W = Convert.ToInt32(rect_W * bmp.Width / PicBox.Width);
                    if (Fact_W + Fact_X > bmp.Width)
                        Fact_W = bmp.Width - Fact_X;
                }
            }
            catch (Exception ex)
            {
                MoldForm.Model.MoldService.WriteApplicationErrorAsync(ex.Message, ex.Source, ex.StackTrace, MoldForm.Model.Region);
            }
        }
        /// <summary>
        /// 图片高度大于图片宽度
        /// </summary>
        /// <param name="_x">矩形X坐标</param>
        /// <param name="_y">矩形Y坐标</param>
        /// <param name="bmp">克隆图片</param>
        /// <param name="rate_H">高度缩小比例</param>
        /// <param name="Small_W">缩小的图片宽度</param>
        /// <param name="Fact_X">原始图片实际的X坐标</param>
        /// <param name="Fact_Y">原始图片实际的Y坐标</param>
        /// <param name="Fact_W">原始图片实际的宽度</param>
        /// <param name="Fact_H">原始图片实际的高度</param>
        public static void TuGaoDaYuTuKuan(PictureBox PicBox, int _x, int _y, Bitmap bmp, double rate_H, ref double Small_W, ref double Fact_X, ref double Fact_Y, ref double Fact_W, ref double Fact_H)
        {
            try
            {
                Fact_X = Convert.ToInt32(rate_H * (_x - SpaceWidth));

                if (_x <= SpaceWidth)
                    Fact_X = 0;
                Fact_Y = Convert.ToInt32(rate_H * _y);
                if (Fact_X > 0)
                {
                    Fact_W = Convert.ToInt32(rect_W * bmp.Width / Small_W);//切图的宽度(方框宽度/缩小图片宽度=实际切图宽度/实际图片宽度)
                    if (Fact_W + Fact_X > bmp.Width)
                        Fact_W = bmp.Width - Fact_X;
                    Fact_H = Convert.ToInt32(rect_H * bmp.Height / PicBox.Height);
                    if (Fact_H + Fact_Y > bmp.Height)
                        Fact_H = bmp.Height - Fact_Y;
                }
                else
                {
                    //实际小切图宽度(rect_W)=方框宽度-空白宽度-方框x坐标
                    double Smallrect_W = 80 - (SpaceWidth - _x);
                    Fact_W = Convert.ToInt32(Smallrect_W * bmp.Width / Small_W);//切图的宽度(方框宽度/缩小图片宽度=实际切图宽度/实际图片宽度)
                    if (Fact_W + Fact_X > bmp.Width)
                        Fact_W = bmp.Width - Fact_X;
                    Fact_H = Convert.ToInt32(rect_H * bmp.Height / PicBox.Height);
                    if (Fact_H + Fact_Y > bmp.Height)
                        Fact_H = bmp.Height - Fact_Y;
                }
            }
            catch (Exception ex)
            {
                MoldForm.Model.MoldService.WriteApplicationErrorAsync(ex.Message, ex.Source, ex.StackTrace, MoldForm.Model.Region);
            }
        }


        public static StringBuilder StrOrder;
        private static string TempOrder = "";
        private static string Num = "";
        public static void InstanceOrder()
        {
            StrOrder = new StringBuilder();
        }
        /// <summary>
        /// 判断小数点，组合Order
        /// </summary>
        /// <param name="Older"></param>
        public static void GetNewOrder(string Older)
        {
            try
            {
                if (Older != "" && Older != "Temporary")
                {
                    //截取小数点索引之前的字符串，直接拼接为新order的
                    int DianIndex = Older.IndexOf('.');
                    if (DianIndex > 0)
                    {
                        StrOrder.Append(Older.Substring(0, DianIndex));

                        //截取剩余部分的字符串，作为后续判断的字符串
                        TempOrder = Older.Substring(DianIndex, Older.Length - DianIndex);
                        int GangIndex = TempOrder.IndexOf('-');//获取‘-’的索引位置
                        if (GangIndex > 0)
                        {
                            Num = "0" + TempOrder.Substring(0, GangIndex);//组合成小数字符串

                            if (Convert.ToDouble(Num) > 0)//转换为double类型，并判断是否大于0
                            {//如果大于0，那么追加到NewOrder中
                                StrOrder.Append(Convert.ToDouble(Num).ToString().Substring(1, Convert.ToDouble(Num).ToString().Length - 1));
                            }
                            TempOrder = TempOrder.Substring(GangIndex, TempOrder.Length - GangIndex);
                            if (TempOrder.Contains("."))//递归
                                GetNewOrder(TempOrder);
                            else
                                StrOrder.Append(TempOrder);
                        }
                        else
                        {//如果还有小数点并且已经是最后一个参数，就直接判断该参数
                            Num = "0" + TempOrder;
                            if (Convert.ToDouble(Num) > 0)//转换为double类型，并判断是否大于0
                            {//如果大于0，那么追加到NewOrder中
                                StrOrder.Append(Convert.ToDouble(Num).ToString().Substring(1, Convert.ToDouble(Num).ToString().Length - 1));
                            }
                        }
                    }
                    else
                        StrOrder.Append(Older);
                }
            }
            catch
            {
                StrOrder.Append(Older);
            }
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="values"></param>
        public static void WriteLog(string FileName, string values)
        {
            string direcotryPath = MisumiDataPath + DateTime.Now.ToString("yyyyMM") + "\\";
            Directory.CreateDirectory(direcotryPath);
            File.AppendAllText(direcotryPath + DateTime.Now.ToString("yyyyMMdd") + "_" + FileName + ".txt", values + "\r\n", Encoding.UTF8);
        }

        /// <summary>
        /// 将参数写入文档，方便测试
        /// </summary>
        /// <param name="values"></param>
        public static void WriteAcisValue(string values)
        {
            string direcotryPath = MisumiDataPath + DateTime.Now.ToString("yyyyMM") + "\\";
            Directory.CreateDirectory(direcotryPath);
            File.AppendAllText(direcotryPath + DateTime.Now.ToString("yyyyMMdd") + ".txt", values + "\r\n", Encoding.UTF8);
        }
    }
}
