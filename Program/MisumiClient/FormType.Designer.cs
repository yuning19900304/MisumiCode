namespace Misumi_Client
{
    partial class FormType
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
            this.btnGMW = new System.Windows.Forms.Button();
            this.btnGPW = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnGMW
            // 
            this.btnGMW.Location = new System.Drawing.Point(36, 23);
            this.btnGMW.Name = "btnGMW";
            this.btnGMW.Size = new System.Drawing.Size(202, 99);
            this.btnGMW.TabIndex = 0;
            this.btnGMW.Text = "GMW";
            this.btnGMW.UseVisualStyleBackColor = true;
            this.btnGMW.Click += new System.EventHandler(this.btnGMW_Click);
            // 
            // btnGPW
            // 
            this.btnGPW.Location = new System.Drawing.Point(36, 135);
            this.btnGPW.Name = "btnGPW";
            this.btnGPW.Size = new System.Drawing.Size(202, 99);
            this.btnGPW.TabIndex = 1;
            this.btnGPW.Text = "GPW";
            this.btnGPW.UseVisualStyleBackColor = true;
            this.btnGPW.Click += new System.EventHandler(this.btnGPW_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(36, 268);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(202, 129);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 409);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnGPW);
            this.Controls.Add(this.btnGMW);
            this.Name = "FormType";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormType";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGMW;
        private System.Windows.Forms.Button btnGPW;
        private System.Windows.Forms.Button button1;
    }
}