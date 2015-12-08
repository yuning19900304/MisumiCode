namespace Misumi_Client.PressSpecial
{
    partial class S2_4C
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(S2_4C));
            this.panel3 = new System.Windows.Forms.Panel();
            this.MNTText = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblRefer = new System.Windows.Forms.Label();
            this.lblOldOrder = new System.Windows.Forms.Label();
            this.PicCancel = new System.Windows.Forms.PictureBox();
            this.btn_Next = new System.Windows.Forms.PictureBox();
            this.txtModelType = new System.Windows.Forms.TextBox();
            this.lblType = new System.Windows.Forms.Label();
            this.Pic4C = new System.Windows.Forms.PictureBox();
            this.Pic3C = new System.Windows.Forms.PictureBox();
            this.Pic2C = new System.Windows.Forms.PictureBox();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Next)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pic4C)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pic3C)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pic2C)).BeginInit();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.MNTText);
            this.panel3.Location = new System.Drawing.Point(-1, -1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(600, 35);
            this.panel3.TabIndex = 6;
            // 
            // MNTText
            // 
            this.MNTText.AutoSize = true;
            this.MNTText.Font = new System.Drawing.Font("MS PGothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MNTText.Location = new System.Drawing.Point(8, 10);
            this.MNTText.Name = "MNTText";
            this.MNTText.Size = new System.Drawing.Size(141, 12);
            this.MNTText.TabIndex = 0;
            this.MNTText.Text = "■形状を選択してください。";
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
            this.panel1.Location = new System.Drawing.Point(-1, 371);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(600, 76);
            this.panel1.TabIndex = 23;
            // 
            // lblRefer
            // 
            this.lblRefer.AutoSize = true;
            this.lblRefer.Font = new System.Drawing.Font("MS PGothic", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblRefer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.lblRefer.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblRefer.Location = new System.Drawing.Point(127, 46);
            this.lblRefer.Margin = new System.Windows.Forms.Padding(0);
            this.lblRefer.Name = "lblRefer";
            this.lblRefer.Size = new System.Drawing.Size(68, 19);
            this.lblRefer.TabIndex = 41;
            this.lblRefer.Text = "(参考):";
            // 
            // lblOldOrder
            // 
            this.lblOldOrder.AutoSize = true;
            this.lblOldOrder.BackColor = System.Drawing.Color.Transparent;
            this.lblOldOrder.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lblOldOrder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.lblOldOrder.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblOldOrder.Location = new System.Drawing.Point(201, 46);
            this.lblOldOrder.Margin = new System.Windows.Forms.Padding(0);
            this.lblOldOrder.Name = "lblOldOrder";
            this.lblOldOrder.Size = new System.Drawing.Size(81, 19);
            this.lblOldOrder.TabIndex = 40;
            this.lblOldOrder.Text = "OldModel";
            this.lblOldOrder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PicCancel
            // 
            this.PicCancel.Location = new System.Drawing.Point(26, 21);
            this.PicCancel.Name = "PicCancel";
            this.PicCancel.Size = new System.Drawing.Size(86, 32);
            this.PicCancel.TabIndex = 33;
            this.PicCancel.TabStop = false;
            this.PicCancel.Click += new System.EventHandler(this.PicCancel_Click);
            // 
            // btn_Next
            // 
            this.btn_Next.Location = new System.Drawing.Point(486, 21);
            this.btn_Next.Name = "btn_Next";
            this.btn_Next.Size = new System.Drawing.Size(86, 32);
            this.btn_Next.TabIndex = 20;
            this.btn_Next.TabStop = false;
            this.btn_Next.Click += new System.EventHandler(this.btn_Next_Click);
            // 
            // txtModelType
            // 
            this.txtModelType.BackColor = System.Drawing.Color.White;
            this.txtModelType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtModelType.Enabled = false;
            this.txtModelType.Font = new System.Drawing.Font("MS PGothic", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtModelType.Location = new System.Drawing.Point(197, 10);
            this.txtModelType.Multiline = true;
            this.txtModelType.Name = "txtModelType";
            this.txtModelType.ReadOnly = true;
            this.txtModelType.Size = new System.Drawing.Size(265, 28);
            this.txtModelType.TabIndex = 17;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Font = new System.Drawing.Font("MS PGothic", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblType.Location = new System.Drawing.Point(134, 14);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(60, 20);
            this.lblType.TabIndex = 16;
            this.lblType.Text = "Type:";
            // 
            // Pic4C
            // 
            this.Pic4C.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pic4C.Location = new System.Drawing.Point(238, 33);
            this.Pic4C.Name = "Pic4C";
            this.Pic4C.Size = new System.Drawing.Size(120, 80);
            this.Pic4C.TabIndex = 50;
            this.Pic4C.TabStop = false;
            this.Pic4C.Tag = "4C";
            this.Pic4C.Click += new System.EventHandler(this.Pic2C_Click);
            // 
            // Pic3C
            // 
            this.Pic3C.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pic3C.Location = new System.Drawing.Point(119, 33);
            this.Pic3C.Name = "Pic3C";
            this.Pic3C.Size = new System.Drawing.Size(120, 80);
            this.Pic3C.TabIndex = 49;
            this.Pic3C.TabStop = false;
            this.Pic3C.Tag = "3C";
            this.Pic3C.Click += new System.EventHandler(this.Pic2C_Click);
            // 
            // Pic2C
            // 
            this.Pic2C.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pic2C.Location = new System.Drawing.Point(0, 33);
            this.Pic2C.Name = "Pic2C";
            this.Pic2C.Size = new System.Drawing.Size(120, 80);
            this.Pic2C.TabIndex = 48;
            this.Pic2C.TabStop = false;
            this.Pic2C.Tag = "2C";
            this.Pic2C.Click += new System.EventHandler(this.Pic2C_Click);
            // 
            // S2_4C
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(598, 446);
            this.Controls.Add(this.Pic4C);
            this.Controls.Add(this.Pic3C);
            this.Controls.Add(this.Pic2C);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Font = new System.Drawing.Font("MS PGothic", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "S2_4C";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "2-4C";
            this.Load += new System.EventHandler(this.S2_4C_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Next)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pic4C)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pic3C)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pic2C)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label MNTText;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblRefer;
        private System.Windows.Forms.Label lblOldOrder;
        private System.Windows.Forms.PictureBox PicCancel;
        private System.Windows.Forms.PictureBox btn_Next;
        private System.Windows.Forms.TextBox txtModelType;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.PictureBox Pic4C;
        private System.Windows.Forms.PictureBox Pic3C;
        private System.Windows.Forms.PictureBox Pic2C;
    }
}