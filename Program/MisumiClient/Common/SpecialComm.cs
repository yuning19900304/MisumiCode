using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Net;

namespace Misumi_Client.Common
{
    public static class SpecialComm
    {
        #region 特例处理画面中的颜色常量
        const int A = 128;
        const int R = 254;
        const int G = 204;
        const int B = 228;
        const int Angle = 270;
        #endregion

        public static Image LoadUrlImage(string UrlImg)
        {
            return Image.FromStream(WebRequest.Create(UrlImg).GetResponse().GetResponseStream());
        }

        public static void ShowPanel(PictureBox ShapePic, Panel panel)
        {
            panel.Width = ShapePic.Width;
            panel.Height = ShapePic.Height;
            panel.BringToFront();
            ShapePic.Controls.Add(panel);
        }

        /// <summary>
        /// 画Panel遮罩层
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void DrawPanel(object sender, PaintEventArgs e)
        {
            // 背景设成透明还是必要的，控件上的图形在后面画
            ((Panel)sender).BackColor = Color.Transparent;
            Rectangle rect = e.ClipRectangle;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // 画控件上的图形，这里以一个线性渐变的图为例。
            // 注意此处，128是透明度，范围是0-255。
            // 如果控件上放一个不透明的图片，则须先将其处理为透明的。
            LinearGradientBrush baseBackground = new LinearGradientBrush(rect,
                        Color.FromArgb(A, R, G, B),
                        Color.FromArgb(A, R, G, B),
                        Angle, false);

            e.Graphics.FillRectangle(baseBackground, rect);
            e.Graphics.Flush();
        }
    }
}
