namespace Transfer8Pro.Client
{
    partial class DownloadConfigFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DownloadConfigFrm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDownloadData = new System.Windows.Forms.Button();
            this.btnCheckUploadData = new System.Windows.Forms.Button();
            this.txtFtpUserName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEncryptKey = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDownloadData);
            this.panel1.Controls.Add(this.btnCheckUploadData);
            this.panel1.Controls.Add(this.txtFtpUserName);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtEncryptKey);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(453, 174);
            this.panel1.TabIndex = 0;
            // 
            // btnDownloadData
            // 
            this.btnDownloadData.Location = new System.Drawing.Point(234, 110);
            this.btnDownloadData.Name = "btnDownloadData";
            this.btnDownloadData.Size = new System.Drawing.Size(105, 28);
            this.btnDownloadData.TabIndex = 5;
            this.btnDownloadData.Text = "下载配置数据";
            this.btnDownloadData.UseVisualStyleBackColor = true;
            this.btnDownloadData.Click += new System.EventHandler(this.btnDownloadData_Click);
            // 
            // btnCheckUploadData
            // 
            this.btnCheckUploadData.Location = new System.Drawing.Point(106, 110);
            this.btnCheckUploadData.Name = "btnCheckUploadData";
            this.btnCheckUploadData.Size = new System.Drawing.Size(105, 28);
            this.btnCheckUploadData.TabIndex = 4;
            this.btnCheckUploadData.Text = "检测配置数据";
            this.btnCheckUploadData.UseVisualStyleBackColor = true;
            this.btnCheckUploadData.Click += new System.EventHandler(this.btnCheckUploadData_Click);
            // 
            // txtFtpUserName
            // 
            this.txtFtpUserName.Location = new System.Drawing.Point(89, 65);
            this.txtFtpUserName.Name = "txtFtpUserName";
            this.txtFtpUserName.Size = new System.Drawing.Size(313, 21);
            this.txtFtpUserName.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "FTP账号：";
            // 
            // txtEncryptKey
            // 
            this.txtEncryptKey.Location = new System.Drawing.Point(89, 25);
            this.txtEncryptKey.Name = "txtEncryptKey";
            this.txtEncryptKey.Size = new System.Drawing.Size(313, 21);
            this.txtEncryptKey.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "客户密钥：";
            // 
            // DownloadConfigFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 174);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "DownloadConfigFrm";
            this.ShowInTaskbar = false;
            this.Text = "下载配置数据";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDownloadData;
        private System.Windows.Forms.Button btnCheckUploadData;
        private System.Windows.Forms.TextBox txtFtpUserName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtEncryptKey;
        private System.Windows.Forms.Label label1;
    }
}