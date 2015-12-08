using System;
using System.Windows.Forms;
using Misumi_Client.Common;

namespace Misumi_Client
{
    public partial class WaitBox : Form
    {
        #region 属性
        private int _MaxWaitTime; //最大等待时间
        private int _WaitTime;  //当前等待时间
        private bool _CancelEnable;  //显示退出按钮
        private IAsyncResult _AsyncResult;
        private EventHandler<EventArgs> _Method;
        /// <summary>
        /// 控制界面显示的特性
        /// </summary>
        public int TimeSpan { get; set; }
        public bool FormEffectEnable { get; set; }
        #endregion

        public WaitBox(EventHandler<EventArgs> method, int maxWaitTime, string waitMessage, bool cancelEnable, bool timerVisable)
        {
            maxWaitTime *= 1000;
            Initialize(method, maxWaitTime, waitMessage, cancelEnable, timerVisable);
        }

        #region Initialize
        private void Initialize(EventHandler<EventArgs> method, int maxWaitTime, string waitMessage, bool cancelEnable, bool timerVisable)
        {
            InitializeComponent();
            this.labMessage.Text = waitMessage;

            LanguageManager.SetFont(this.Controls, 14);

            this.Width = this.labMessage.Width + 100;
            TimeSpan = 500;
            _CancelEnable = cancelEnable;
            _MaxWaitTime = maxWaitTime;
            _WaitTime = 0;
            _Method = method;
            this.timer1.Interval = TimeSpan;
        }
        #endregion

        #region Events
        private void timer1_Tick(object sender, EventArgs e)
        {
            _WaitTime += TimeSpan;
            if (!this._AsyncResult.IsCompleted)
            {
                if (_WaitTime > _MaxWaitTime)
                {
                    this.timer1.Stop();
                    this.Close();
                }
            }
            else
            {
                this.timer1.Stop();
                this.Close();
            }
        }

        private void WaitBox_Shown(object sender, EventArgs e)
        {
            _AsyncResult = _Method.BeginInvoke(null, null, null, null);
            this.timer1.Start();
        }
        #endregion
    }
}
