namespace Misumi_Client.Mold
{
    partial class KMB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KMB));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblRefer = new System.Windows.Forms.Label();
            this.lblOldOrder = new System.Windows.Forms.Label();
            this.PicCancel = new System.Windows.Forms.PictureBox();
            this.btn_Next = new System.Windows.Forms.PictureBox();
            this.txtModelType = new System.Windows.Forms.TextBox();
            this.lblType = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.KMBText = new System.Windows.Forms.Label();
            this.PicKMB = new System.Windows.Forms.PictureBox();
            this.lblPanFont = new System.Windows.Forms.Label();
            this.lblChoice = new System.Windows.Forms.Label();
            this.lbl1 = new System.Windows.Forms.Label();
            this.lbl2 = new System.Windows.Forms.Label();
            this.lbl3 = new System.Windows.Forms.Label();
            this.lblA = new System.Windows.Forms.Label();
            this.lblB = new System.Windows.Forms.Label();
            this.lblC = new System.Windows.Forms.Label();
            this.lblAC = new System.Windows.Forms.Label();
            this.lblBC = new System.Windows.Forms.Label();
            this.lblAB = new System.Windows.Forms.Label();
            this.lblABC = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Next)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicKMB)).BeginInit();
            this.SuspendLayout();
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
            this.panel1.Location = new System.Drawing.Point(-1, 394);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(721, 76);
            this.panel1.TabIndex = 44;
            // 
            // lblRefer
            // 
            this.lblRefer.AutoSize = true;
            this.lblRefer.Font = new System.Drawing.Font("MS PGothic", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblRefer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.lblRefer.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblRefer.Location = new System.Drawing.Point(160, 46);
            this.lblRefer.Margin = new System.Windows.Forms.Padding(0);
            this.lblRefer.Name = "lblRefer";
            this.lblRefer.Size = new System.Drawing.Size(68, 19);
            this.lblRefer.TabIndex = 31;
            this.lblRefer.Text = "(参考):";
            // 
            // lblOldOrder
            // 
            this.lblOldOrder.AutoSize = true;
            this.lblOldOrder.BackColor = System.Drawing.Color.Transparent;
            this.lblOldOrder.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lblOldOrder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.lblOldOrder.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblOldOrder.Location = new System.Drawing.Point(234, 46);
            this.lblOldOrder.Margin = new System.Windows.Forms.Padding(0);
            this.lblOldOrder.Name = "lblOldOrder";
            this.lblOldOrder.Size = new System.Drawing.Size(81, 19);
            this.lblOldOrder.TabIndex = 30;
            this.lblOldOrder.Text = "OldModel";
            this.lblOldOrder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PicCancel
            // 
            this.PicCancel.Location = new System.Drawing.Point(24, 21);
            this.PicCancel.Name = "PicCancel";
            this.PicCancel.Size = new System.Drawing.Size(86, 32);
            this.PicCancel.TabIndex = 28;
            this.PicCancel.TabStop = false;
            this.PicCancel.Click += new System.EventHandler(this.PicCancel_Click);
            // 
            // btn_Next
            // 
            this.btn_Next.Location = new System.Drawing.Point(608, 21);
            this.btn_Next.Name = "btn_Next";
            this.btn_Next.Size = new System.Drawing.Size(86, 32);
            this.btn_Next.TabIndex = 25;
            this.btn_Next.TabStop = false;
            this.btn_Next.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // txtModelType
            // 
            this.txtModelType.BackColor = System.Drawing.Color.White;
            this.txtModelType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtModelType.Enabled = false;
            this.txtModelType.Font = new System.Drawing.Font("MS PGothic", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtModelType.Location = new System.Drawing.Point(230, 10);
            this.txtModelType.Margin = new System.Windows.Forms.Padding(0);
            this.txtModelType.Multiline = true;
            this.txtModelType.Name = "txtModelType";
            this.txtModelType.ReadOnly = true;
            this.txtModelType.Size = new System.Drawing.Size(320, 28);
            this.txtModelType.TabIndex = 17;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Font = new System.Drawing.Font("MS PGothic", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblType.Location = new System.Drawing.Point(168, 14);
            this.lblType.Margin = new System.Windows.Forms.Padding(0);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(60, 20);
            this.lblType.TabIndex = 16;
            this.lblType.Text = "Type:";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.KMBText);
            this.panel3.Location = new System.Drawing.Point(-1, -1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(721, 35);
            this.panel3.TabIndex = 43;
            // 
            // KMBText
            // 
            this.KMBText.AutoSize = true;
            this.KMBText.Font = new System.Drawing.Font("MS PGothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.KMBText.Location = new System.Drawing.Point(23, 11);
            this.KMBText.Name = "KMBText";
            this.KMBText.Size = new System.Drawing.Size(153, 12);
            this.KMBText.TabIndex = 0;
            this.KMBText.Text = "■加工面を選択してください。";
            this.KMBText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PicKMB
            // 
            this.PicKMB.Location = new System.Drawing.Point(25, 73);
            this.PicKMB.Name = "PicKMB";
            this.PicKMB.Size = new System.Drawing.Size(210, 209);
            this.PicKMB.TabIndex = 45;
            this.PicKMB.TabStop = false;
            this.PicKMB.Tag = "N";
            // 
            // lblPanFont
            // 
            this.lblPanFont.AutoSize = true;
            this.lblPanFont.Font = new System.Drawing.Font("MS PGothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPanFont.Location = new System.Drawing.Point(247, 73);
            this.lblPanFont.Name = "lblPanFont";
            this.lblPanFont.Size = new System.Drawing.Size(56, 22);
            this.lblPanFont.TabIndex = 46;
            this.lblPanFont.Text = "面数";
            // 
            // lblChoice
            // 
            this.lblChoice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lblChoice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblChoice.Font = new System.Drawing.Font("MS PGothic", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblChoice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(102)))), ((int)(((byte)(0)))));
            this.lblChoice.Location = new System.Drawing.Point(373, 74);
            this.lblChoice.Name = "lblChoice";
            this.lblChoice.Padding = new System.Windows.Forms.Padding(7, 5, 0, 0);
            this.lblChoice.Size = new System.Drawing.Size(123, 31);
            this.lblChoice.TabIndex = 47;
            this.lblChoice.Text = "加工面選択";
            // 
            // lbl1
            // 
            this.lbl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl1.Font = new System.Drawing.Font("MS PGothic", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl1.Location = new System.Drawing.Point(251, 104);
            this.lbl1.Name = "lbl1";
            this.lbl1.Padding = new System.Windows.Forms.Padding(7, 35, 0, 0);
            this.lbl1.Size = new System.Drawing.Size(123, 91);
            this.lbl1.TabIndex = 48;
            this.lbl1.Text = "1面";
            // 
            // lbl2
            // 
            this.lbl2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl2.Font = new System.Drawing.Font("MS PGothic", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl2.Location = new System.Drawing.Point(251, 194);
            this.lbl2.Name = "lbl2";
            this.lbl2.Padding = new System.Windows.Forms.Padding(7, 35, 0, 0);
            this.lbl2.Size = new System.Drawing.Size(123, 91);
            this.lbl2.TabIndex = 49;
            this.lbl2.Text = "2面";
            // 
            // lbl3
            // 
            this.lbl3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl3.Font = new System.Drawing.Font("MS PGothic", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl3.Location = new System.Drawing.Point(251, 284);
            this.lbl3.Name = "lbl3";
            this.lbl3.Padding = new System.Windows.Forms.Padding(7, 5, 0, 0);
            this.lbl3.Size = new System.Drawing.Size(123, 31);
            this.lbl3.TabIndex = 50;
            this.lbl3.Text = "3面";
            // 
            // lblA
            // 
            this.lblA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblA.Font = new System.Drawing.Font("MS PGothic", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblA.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(197)))), ((int)(((byte)(255)))));
            this.lblA.Location = new System.Drawing.Point(373, 104);
            this.lblA.Name = "lblA";
            this.lblA.Padding = new System.Windows.Forms.Padding(40, 5, 0, 0);
            this.lblA.Size = new System.Drawing.Size(123, 31);
            this.lblA.TabIndex = 51;
            this.lblA.Tag = "A";
            this.lblA.Text = "A面";
            this.lblA.Click += new System.EventHandler(this.lblA_Click);
            // 
            // lblB
            // 
            this.lblB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblB.Font = new System.Drawing.Font("MS PGothic", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(197)))), ((int)(((byte)(255)))));
            this.lblB.Location = new System.Drawing.Point(373, 134);
            this.lblB.Name = "lblB";
            this.lblB.Padding = new System.Windows.Forms.Padding(40, 5, 0, 0);
            this.lblB.Size = new System.Drawing.Size(123, 31);
            this.lblB.TabIndex = 52;
            this.lblB.Tag = "B";
            this.lblB.Text = "B面";
            this.lblB.Click += new System.EventHandler(this.lblA_Click);
            // 
            // lblC
            // 
            this.lblC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblC.Font = new System.Drawing.Font("MS PGothic", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblC.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(197)))), ((int)(((byte)(255)))));
            this.lblC.Location = new System.Drawing.Point(373, 164);
            this.lblC.Name = "lblC";
            this.lblC.Padding = new System.Windows.Forms.Padding(40, 5, 0, 0);
            this.lblC.Size = new System.Drawing.Size(123, 31);
            this.lblC.TabIndex = 53;
            this.lblC.Tag = "C";
            this.lblC.Text = "C面";
            this.lblC.Click += new System.EventHandler(this.lblA_Click);
            // 
            // lblAC
            // 
            this.lblAC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAC.Font = new System.Drawing.Font("MS PGothic", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAC.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(197)))), ((int)(((byte)(255)))));
            this.lblAC.Location = new System.Drawing.Point(373, 254);
            this.lblAC.Name = "lblAC";
            this.lblAC.Padding = new System.Windows.Forms.Padding(35, 5, 0, 0);
            this.lblAC.Size = new System.Drawing.Size(123, 31);
            this.lblAC.TabIndex = 56;
            this.lblAC.Tag = "AC";
            this.lblAC.Text = "AC面";
            this.lblAC.Click += new System.EventHandler(this.lblA_Click);
            // 
            // lblBC
            // 
            this.lblBC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBC.Font = new System.Drawing.Font("MS PGothic", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBC.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(197)))), ((int)(((byte)(255)))));
            this.lblBC.Location = new System.Drawing.Point(373, 224);
            this.lblBC.Name = "lblBC";
            this.lblBC.Padding = new System.Windows.Forms.Padding(35, 5, 0, 0);
            this.lblBC.Size = new System.Drawing.Size(123, 31);
            this.lblBC.TabIndex = 55;
            this.lblBC.Tag = "BC";
            this.lblBC.Text = "BC面";
            this.lblBC.Click += new System.EventHandler(this.lblA_Click);
            // 
            // lblAB
            // 
            this.lblAB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAB.Font = new System.Drawing.Font("MS PGothic", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(197)))), ((int)(((byte)(255)))));
            this.lblAB.Location = new System.Drawing.Point(373, 194);
            this.lblAB.Name = "lblAB";
            this.lblAB.Padding = new System.Windows.Forms.Padding(35, 5, 0, 0);
            this.lblAB.Size = new System.Drawing.Size(123, 31);
            this.lblAB.TabIndex = 54;
            this.lblAB.Tag = "AB";
            this.lblAB.Text = "AB面";
            this.lblAB.Click += new System.EventHandler(this.lblA_Click);
            // 
            // lblABC
            // 
            this.lblABC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblABC.Font = new System.Drawing.Font("MS PGothic", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblABC.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(197)))), ((int)(((byte)(255)))));
            this.lblABC.Location = new System.Drawing.Point(373, 284);
            this.lblABC.Name = "lblABC";
            this.lblABC.Padding = new System.Windows.Forms.Padding(28, 5, 0, 0);
            this.lblABC.Size = new System.Drawing.Size(123, 31);
            this.lblABC.TabIndex = 57;
            this.lblABC.Tag = "ABC";
            this.lblABC.Text = "ABC面";
            this.lblABC.Click += new System.EventHandler(this.lblA_Click);
            // 
            // KMB
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(719, 469);
            this.Controls.Add(this.lblABC);
            this.Controls.Add(this.lblAC);
            this.Controls.Add(this.lblBC);
            this.Controls.Add(this.lblAB);
            this.Controls.Add(this.lblC);
            this.Controls.Add(this.lblB);
            this.Controls.Add(this.lblA);
            this.Controls.Add(this.lbl3);
            this.Controls.Add(this.lbl2);
            this.Controls.Add(this.lbl1);
            this.Controls.Add(this.lblChoice);
            this.Controls.Add(this.lblPanFont);
            this.Controls.Add(this.PicKMB);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Font = new System.Drawing.Font("MS PGothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(735, 507);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(735, 507);
            this.Name = "KMB";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KMB";
            this.Load += new System.EventHandler(this.KMB_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Next)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicKMB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PicKMB;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtModelType;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label KMBText;
        private System.Windows.Forms.Label lblPanFont;
        private System.Windows.Forms.Label lblChoice;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.Label lbl3;
        private System.Windows.Forms.Label lblA;
        private System.Windows.Forms.Label lblB;
        private System.Windows.Forms.Label lblC;
        private System.Windows.Forms.Label lblAC;
        private System.Windows.Forms.Label lblBC;
        private System.Windows.Forms.Label lblAB;
        private System.Windows.Forms.Label lblABC;
        private System.Windows.Forms.PictureBox btn_Next;
        private System.Windows.Forms.PictureBox PicCancel;
        private System.Windows.Forms.Label lblOldOrder;
        private System.Windows.Forms.Label lblRefer;
    }
}