namespace PdaPrint
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.PrintQRCode = new System.Windows.Forms.Button();
            this.BtnClear = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // PrintQRCode
            // 
            this.PrintQRCode.Font = new System.Drawing.Font("Tahoma", 28F, System.Drawing.FontStyle.Regular);
            this.PrintQRCode.ForeColor = System.Drawing.Color.Green;
            this.PrintQRCode.Location = new System.Drawing.Point(3, 44);
            this.PrintQRCode.Name = "PrintQRCode";
            this.PrintQRCode.Size = new System.Drawing.Size(233, 100);
            this.PrintQRCode.TabIndex = 0;
            this.PrintQRCode.Text = "打印二维码";
            this.PrintQRCode.Click += new System.EventHandler(this.PrintQRCode_Click);
            // 
            // BtnClear
            // 
            this.BtnClear.Font = new System.Drawing.Font("Tahoma", 28F, System.Drawing.FontStyle.Regular);
            this.BtnClear.ForeColor = System.Drawing.Color.Red;
            this.BtnClear.Location = new System.Drawing.Point(3, 150);
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(233, 113);
            this.BtnClear.TabIndex = 1;
            this.BtnClear.Text = "清    空";
            this.BtnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular);
            this.textBox1.Location = new System.Drawing.Point(3, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(233, 35);
            this.textBox1.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(239, 266);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.BtnClear);
            this.Controls.Add(this.PrintQRCode);
            this.Name = "Form1";
            this.Text = "HM-A300打印二维码";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button PrintQRCode;
        private System.Windows.Forms.Button BtnClear;
        private System.Windows.Forms.TextBox textBox1;
    }
}

