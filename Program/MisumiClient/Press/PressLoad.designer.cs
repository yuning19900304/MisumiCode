namespace  Misumi_Client.Press
{
    partial class PressLoad
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
            this.BGW = new System.ComponentModel.BackgroundWorker();
            this.PicWait = new System.Windows.Forms.PictureBox();
            this.labMessage = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PicWait)).BeginInit();
            this.SuspendLayout();
            // 
            // BGW
            // 
            this.BGW.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BGW_DoWork);
            this.BGW.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BGW_RunWorkerCompleted);
            // 
            // PicWait
            // 
            this.PicWait.BackColor = System.Drawing.Color.Transparent;
            this.PicWait.Image = global::Misumi_Client.Properties.Resources.loading;
            this.PicWait.Location = new System.Drawing.Point(9, 8);
            this.PicWait.Margin = new System.Windows.Forms.Padding(0);
            this.PicWait.Name = "PicWait";
            this.PicWait.Size = new System.Drawing.Size(29, 25);
            this.PicWait.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PicWait.TabIndex = 2;
            this.PicWait.TabStop = false;
            // 
            // labMessage
            // 
            this.labMessage.AutoSize = true;
            this.labMessage.Font = new System.Drawing.Font("MS PMincho", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labMessage.Location = new System.Drawing.Point(50, 12);
            this.labMessage.Name = "labMessage";
            this.labMessage.Size = new System.Drawing.Size(380, 19);
            this.labMessage.TabIndex = 3;
            this.labMessage.Text = "データを生成しています。少々お待ちください。";
            // 
            // StartLoad
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(500, 42);
            this.Controls.Add(this.labMessage);
            this.Controls.Add(this.PicWait);
            this.Font = new System.Drawing.Font("MS PGothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(300, 42);
            this.Name = "StartLoad";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.StartLoad_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PicWait)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker BGW;
        private System.Windows.Forms.PictureBox PicWait;
        private System.Windows.Forms.Label labMessage;
    }
}