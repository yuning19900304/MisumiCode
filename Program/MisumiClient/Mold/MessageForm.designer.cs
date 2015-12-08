namespace Misumi_Client.Mold
{
    partial class MessageForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageForm));
            this.pnMsg = new System.Windows.Forms.Panel();
            this.lblMsg2 = new System.Windows.Forms.Label();
            this.lblMsg1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnInstall = new System.Windows.Forms.Button();
            this.pnMsg.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnMsg
            // 
            this.pnMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnMsg.BackColor = System.Drawing.Color.White;
            this.pnMsg.Controls.Add(this.lblMsg2);
            this.pnMsg.Controls.Add(this.lblMsg1);
            this.pnMsg.Controls.Add(this.pictureBox1);
            this.pnMsg.Location = new System.Drawing.Point(0, 0);
            this.pnMsg.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnMsg.Name = "pnMsg";
            this.pnMsg.Size = new System.Drawing.Size(501, 103);
            this.pnMsg.TabIndex = 0;
            // 
            // lblMsg2
            // 
            this.lblMsg2.AutoSize = true;
            this.lblMsg2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMsg2.Location = new System.Drawing.Point(67, 51);
            this.lblMsg2.Name = "lblMsg2";
            this.lblMsg2.Size = new System.Drawing.Size(349, 17);
            this.lblMsg2.TabIndex = 1;
            this.lblMsg2.Text = "After installation, please restart the application.Thank you. ";
            // 
            // lblMsg1
            // 
            this.lblMsg1.AutoSize = true;
            this.lblMsg1.Location = new System.Drawing.Point(67, 34);
            this.lblMsg1.Name = "lblMsg1";
            this.lblMsg1.Size = new System.Drawing.Size(387, 17);
            this.lblMsg1.TabIndex = 1;
            this.lblMsg1.Text = "In order to use 3D  preview  function ,please \"Install\" the DLL file.";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Location = new System.Drawing.Point(26, 34);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(34, 34);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.AutoSize = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Ignore;
            this.btnCancel.Location = new System.Drawing.Point(403, 113);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(86, 30);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnInstall
            // 
            this.btnInstall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInstall.AutoSize = true;
            this.btnInstall.DialogResult = System.Windows.Forms.DialogResult.Ignore;
            this.btnInstall.Location = new System.Drawing.Point(311, 113);
            this.btnInstall.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Size = new System.Drawing.Size(86, 30);
            this.btnInstall.TabIndex = 1;
            this.btnInstall.Text = "Install";
            this.btnInstall.UseVisualStyleBackColor = true;
            this.btnInstall.Click += new System.EventHandler(this.btnInstall_Click);
            // 
            // MessageForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(501, 156);
            this.Controls.Add(this.btnInstall);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.pnMsg);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MessageForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.MessageForm_Load);
            this.pnMsg.ResumeLayout(false);
            this.pnMsg.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnMsg;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnInstall;
        private System.Windows.Forms.Label lblMsg2;
        private System.Windows.Forms.Label lblMsg1;
    }
}