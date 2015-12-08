namespace Misumi_Client.Mold
{
    partial class TNP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TNP));
            this.panel3 = new System.Windows.Forms.Panel();
            this.TPNText = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblRefer = new System.Windows.Forms.Label();
            this.lblOldOrder = new System.Windows.Forms.Label();
            this.PicCancel = new System.Windows.Forms.PictureBox();
            this.btn_Next = new System.Windows.Forms.PictureBox();
            this.txtModelType = new System.Windows.Forms.TextBox();
            this.lblType = new System.Windows.Forms.Label();
            this.PicThree = new System.Windows.Forms.PictureBox();
            this.PicTwo = new System.Windows.Forms.PictureBox();
            this.PicOne = new System.Windows.Forms.PictureBox();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Next)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicThree)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicTwo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicOne)).BeginInit();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.TPNText);
            this.panel3.Location = new System.Drawing.Point(-1, -1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(547, 35);
            this.panel3.TabIndex = 5;
            // 
            // TPNText
            // 
            this.TPNText.AutoSize = true;
            this.TPNText.Location = new System.Drawing.Point(12, 10);
            this.TPNText.Name = "TPNText";
            this.TPNText.Size = new System.Drawing.Size(165, 12);
            this.TPNText.TabIndex = 0;
            this.TPNText.Text = "■正面形状を選択してください。";
            this.TPNText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblRefer);
            this.panel1.Controls.Add(this.lblOldOrder);
            this.panel1.Controls.Add(this.PicCancel);
            this.panel1.Controls.Add(this.btn_Next);
            this.panel1.Controls.Add(this.txtModelType);
            this.panel1.Controls.Add(this.lblType);
            this.panel1.Location = new System.Drawing.Point(-1, 362);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(547, 76);
            this.panel1.TabIndex = 6;
            // 
            // lblRefer
            // 
            this.lblRefer.AutoSize = true;
            this.lblRefer.Font = new System.Drawing.Font("MS PGothic", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblRefer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.lblRefer.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblRefer.Location = new System.Drawing.Point(149, 46);
            this.lblRefer.Margin = new System.Windows.Forms.Padding(0);
            this.lblRefer.Name = "lblRefer";
            this.lblRefer.Size = new System.Drawing.Size(68, 19);
            this.lblRefer.TabIndex = 44;
            this.lblRefer.Text = "(参考):";
            // 
            // lblOldOrder
            // 
            this.lblOldOrder.AutoSize = true;
            this.lblOldOrder.BackColor = System.Drawing.Color.Transparent;
            this.lblOldOrder.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lblOldOrder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.lblOldOrder.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblOldOrder.Location = new System.Drawing.Point(224, 46);
            this.lblOldOrder.Margin = new System.Windows.Forms.Padding(0);
            this.lblOldOrder.Name = "lblOldOrder";
            this.lblOldOrder.Size = new System.Drawing.Size(81, 19);
            this.lblOldOrder.TabIndex = 38;
            this.lblOldOrder.Text = "OldModel";
            this.lblOldOrder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PicCancel
            // 
            this.PicCancel.Location = new System.Drawing.Point(21, 21);
            this.PicCancel.Name = "PicCancel";
            this.PicCancel.Size = new System.Drawing.Size(86, 32);
            this.PicCancel.TabIndex = 35;
            this.PicCancel.TabStop = false;
            this.PicCancel.Click += new System.EventHandler(this.PicCancel_Click);
            // 
            // btn_Next
            // 
            this.btn_Next.Location = new System.Drawing.Point(437, 21);
            this.btn_Next.Name = "btn_Next";
            this.btn_Next.Size = new System.Drawing.Size(86, 32);
            this.btn_Next.TabIndex = 18;
            this.btn_Next.TabStop = false;
            this.btn_Next.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // txtModelType
            // 
            this.txtModelType.BackColor = System.Drawing.Color.White;
            this.txtModelType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtModelType.Font = new System.Drawing.Font("MS PGothic", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtModelType.Location = new System.Drawing.Point(220, 10);
            this.txtModelType.Multiline = true;
            this.txtModelType.Name = "txtModelType";
            this.txtModelType.ReadOnly = true;
            this.txtModelType.Size = new System.Drawing.Size(200, 28);
            this.txtModelType.TabIndex = 17;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Font = new System.Drawing.Font("MS PGothic", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblType.Location = new System.Drawing.Point(157, 14);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(60, 20);
            this.lblType.TabIndex = 16;
            this.lblType.Text = "Type:";
            // 
            // PicThree
            // 
            this.PicThree.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PicThree.Location = new System.Drawing.Point(363, 33);
            this.PicThree.Name = "PicThree";
            this.PicThree.Size = new System.Drawing.Size(183, 208);
            this.PicThree.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PicThree.TabIndex = 17;
            this.PicThree.TabStop = false;
            this.PicThree.Tag = "";
            this.PicThree.Click += new System.EventHandler(this.PicOne_Click);
            // 
            // PicTwo
            // 
            this.PicTwo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PicTwo.Location = new System.Drawing.Point(181, 33);
            this.PicTwo.Name = "PicTwo";
            this.PicTwo.Size = new System.Drawing.Size(183, 208);
            this.PicTwo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PicTwo.TabIndex = 16;
            this.PicTwo.TabStop = false;
            this.PicTwo.Tag = "";
            this.PicTwo.Click += new System.EventHandler(this.PicOne_Click);
            // 
            // PicOne
            // 
            this.PicOne.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PicOne.Location = new System.Drawing.Point(-1, 33);
            this.PicOne.Name = "PicOne";
            this.PicOne.Size = new System.Drawing.Size(183, 208);
            this.PicOne.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PicOne.TabIndex = 15;
            this.PicOne.TabStop = false;
            this.PicOne.Tag = "";
            this.PicOne.Click += new System.EventHandler(this.PicOne_Click);
            // 
            // TNP
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(545, 437);
            this.Controls.Add(this.PicThree);
            this.Controls.Add(this.PicTwo);
            this.Controls.Add(this.PicOne);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Font = new System.Drawing.Font("MS PGothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(561, 475);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(561, 475);
            this.Name = "TNP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TNP";
            this.Load += new System.EventHandler(this.TNP_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Next)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicThree)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicTwo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicOne)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label TPNText;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtModelType;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.PictureBox PicOne;
        private System.Windows.Forms.PictureBox PicTwo;
        private System.Windows.Forms.PictureBox PicThree;
        private System.Windows.Forms.PictureBox btn_Next;
        private System.Windows.Forms.PictureBox PicCancel;
        private System.Windows.Forms.Label lblOldOrder;
        private System.Windows.Forms.Label lblRefer;
    }
}