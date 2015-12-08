namespace  Misumi_Client
{
    partial class StartForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartForm));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.lbl = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.cbo1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtType = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboDoMain = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.Column1,
            this.Column2});
            this.dataGridView1.Location = new System.Drawing.Point(0, 120);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 50;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(510, 302);
            this.dataGridView1.TabIndex = 100;
            this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "a";
            this.Column3.FillWeight = 50F;
            this.Column3.HeaderText = "序号";
            this.Column3.Name = "Column3";
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "e";
            this.Column1.FillWeight = 240.5628F;
            this.Column1.HeaderText = "大类名称";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "o";
            this.Column2.FillWeight = 77.18058F;
            this.Column2.HeaderText = "零件类型";
            this.Column2.Name = "Column2";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(405, 428);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(102, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "启动MEX界面";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Location = new System.Drawing.Point(2, 9);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(107, 12);
            this.lbl.TabIndex = 2;
            this.lbl.Text = "网页选择的order：";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(115, 6);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(392, 21);
            this.textBox1.TabIndex = 3;
            this.textBox1.Text = "SBBPE20-50";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "用户名：";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(115, 33);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(132, 21);
            this.textBox2.TabIndex = 3;
            this.textBox2.Text = "Misumi";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "ClassName：";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(432, 89);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Search";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // cbo1
            // 
            this.cbo1.DisplayMember = "e";
            this.cbo1.FormattingEnabled = true;
            this.cbo1.Location = new System.Drawing.Point(115, 60);
            this.cbo1.Name = "cbo1";
            this.cbo1.Size = new System.Drawing.Size(392, 20);
            this.cbo1.TabIndex = 5;
            this.cbo1.SelectedIndexChanged += new System.EventHandler(this.cbo1_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "modelType：";
            // 
            // txtType
            // 
            this.txtType.Location = new System.Drawing.Point(115, 89);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(311, 21);
            this.txtType.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(256, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "域名：";
            // 
            // cboDoMain
            // 
            this.cboDoMain.DisplayMember = "d";
            this.cboDoMain.FormattingEnabled = true;
            this.cboDoMain.Items.AddRange(new object[] {
            "http://jp.misumi-ec.com",
            "http://www.stg.misumi-ec.com",
            "http://cn.misumi-ec.com",
            "http://us.misumi-ec.com",
            "http://uk.misumi-ec.com",
            "http://sg.misumi-ec.com",
            "http://th.misumi-ec.com",
            "http://kr.misumi-ec.com",
            "http://in.misumi-ec.com",
            "http://tw.misumi-ec.com",
            "http://my.misumi-ec.com"});
            this.cboDoMain.Location = new System.Drawing.Point(290, 33);
            this.cboDoMain.Name = "cboDoMain";
            this.cboDoMain.Size = new System.Drawing.Size(217, 20);
            this.cboDoMain.TabIndex = 8;
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 453);
            this.Controls.Add(this.cboDoMain);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtType);
            this.Controls.Add(this.cbo1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.lbl);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "StartForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mold EX-Press Online";
            this.Load += new System.EventHandler(this.StartForm_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.StartForm_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox cbo1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboDoMain;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    }
}