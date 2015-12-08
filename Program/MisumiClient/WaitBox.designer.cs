namespace  Misumi_Client
{
    partial class WaitBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.labMessage = new System.Windows.Forms.Label();
            this.PicWait = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.PicWait)).BeginInit();
            this.SuspendLayout();
            // 
            // labMessage
            // 
            this.labMessage.AutoSize = true;
            this.labMessage.Font = new System.Drawing.Font("MS PMincho", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labMessage.Location = new System.Drawing.Point(43, 12);
            this.labMessage.Name = "labMessage";
            this.labMessage.Size = new System.Drawing.Size(380, 19);
            this.labMessage.TabIndex = 0;
            this.labMessage.Text = "データを生成しています。少々お待ちください。";
            // 
            // PicWait
            // 
            this.PicWait.BackColor = System.Drawing.Color.Transparent;
            this.PicWait.Image = global::Misumi_Client.Properties.Resources.loading;
            this.PicWait.Location = new System.Drawing.Point(10, 9);
            this.PicWait.Margin = new System.Windows.Forms.Padding(0);
            this.PicWait.Name = "PicWait";
            this.PicWait.Size = new System.Drawing.Size(29, 25);
            this.PicWait.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PicWait.TabIndex = 1;
            this.PicWait.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // WaitBox
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(264, 42);
            this.Controls.Add(this.PicWait);
            this.Controls.Add(this.labMessage);
            this.Font = new System.Drawing.Font("MS PMincho", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "WaitBox";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Shown += new System.EventHandler(this.WaitBox_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.PicWait)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labMessage;
        private System.Windows.Forms.PictureBox PicWait;
        private System.Windows.Forms.Timer timer1;
    }
}