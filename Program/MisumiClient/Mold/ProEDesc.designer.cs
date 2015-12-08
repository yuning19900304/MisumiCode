namespace Misumi_Client.Mold
{
    partial class ProEDesc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProEDesc));
            this.btnOnly = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAllDownLoad = new System.Windows.Forms.Button();
            this.chkShow = new System.Windows.Forms.CheckBox();
            this.lblProEContext = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOnly
            // 
            this.btnOnly.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btnOnly.Location = new System.Drawing.Point(158, 192);
            this.btnOnly.Name = "btnOnly";
            this.btnOnly.Size = new System.Drawing.Size(190, 23);
            this.btnOnly.TabIndex = 14;
            this.btnOnly.Text = "CADデータのみダウンロード";
            this.btnOnly.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(354, 192);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "キャンセル";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAllDownLoad
            // 
            this.btnAllDownLoad.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btnAllDownLoad.Location = new System.Drawing.Point(11, 192);
            this.btnAllDownLoad.Name = "btnAllDownLoad";
            this.btnAllDownLoad.Size = new System.Drawing.Size(141, 23);
            this.btnAllDownLoad.TabIndex = 12;
            this.btnAllDownLoad.Text = "EXE同梱ダウンロード";
            this.btnAllDownLoad.UseVisualStyleBackColor = true;
            // 
            // chkShow
            // 
            this.chkShow.AutoSize = true;
            this.chkShow.Location = new System.Drawing.Point(165, 150);
            this.chkShow.Name = "chkShow";
            this.chkShow.Size = new System.Drawing.Size(15, 14);
            this.chkShow.TabIndex = 10;
            this.chkShow.UseVisualStyleBackColor = true;
            this.chkShow.CheckedChanged += new System.EventHandler(this.chkShow_CheckedChanged);
            // 
            // lblProEContext
            // 
            this.lblProEContext.Font = new System.Drawing.Font("MS PGothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblProEContext.Location = new System.Drawing.Point(8, 8);
            this.lblProEContext.Name = "lblProEContext";
            this.lblProEContext.Size = new System.Drawing.Size(437, 139);
            this.lblProEContext.TabIndex = 16;
            // 
            // ProEDesc
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(454, 232);
            this.Controls.Add(this.lblProEContext);
            this.Controls.Add(this.btnOnly);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAllDownLoad);
            this.Controls.Add(this.chkShow);
            this.Font = new System.Drawing.Font("MS PGothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(470, 270);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(470, 270);
            this.Name = "ProEDesc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ProEネイティブデータをご利用頂くお客様へ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOnly;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAllDownLoad;
        private System.Windows.Forms.CheckBox chkShow;
        private System.Windows.Forms.Label lblProEContext;
    }
}