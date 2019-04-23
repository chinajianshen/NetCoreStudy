namespace Transfer8Pro.Client
{
    partial class ServiceManagerFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServiceManagerFrm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblDataStatus = new System.Windows.Forms.Label();
            this.lblFtpStatus = new System.Windows.Forms.Label();
            this.btnDataService = new System.Windows.Forms.Button();
            this.btnFtpService = new System.Windows.Forms.Button();
            this.btnHeartbeat = new System.Windows.Forms.Button();
            this.lblHeartbeatStatus = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnDataSync = new System.Windows.Forms.Button();
            this.lblDataSync = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(32, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "数据导出服务：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(35, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "FTP上传服务：";
            // 
            // lblDataStatus
            // 
            this.lblDataStatus.AutoSize = true;
            this.lblDataStatus.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDataStatus.ForeColor = System.Drawing.Color.Red;
            this.lblDataStatus.Location = new System.Drawing.Point(144, 29);
            this.lblDataStatus.Name = "lblDataStatus";
            this.lblDataStatus.Size = new System.Drawing.Size(52, 14);
            this.lblDataStatus.TabIndex = 2;
            this.lblDataStatus.Text = "未启动";
            // 
            // lblFtpStatus
            // 
            this.lblFtpStatus.AutoSize = true;
            this.lblFtpStatus.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblFtpStatus.ForeColor = System.Drawing.Color.Red;
            this.lblFtpStatus.Location = new System.Drawing.Point(144, 83);
            this.lblFtpStatus.Name = "lblFtpStatus";
            this.lblFtpStatus.Size = new System.Drawing.Size(52, 14);
            this.lblFtpStatus.TabIndex = 3;
            this.lblFtpStatus.Text = "未启动";
            // 
            // btnDataService
            // 
            this.btnDataService.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnDataService.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDataService.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnDataService.Location = new System.Drawing.Point(229, 17);
            this.btnDataService.Name = "btnDataService";
            this.btnDataService.Size = new System.Drawing.Size(142, 40);
            this.btnDataService.TabIndex = 4;
            this.btnDataService.Tag = "0";
            this.btnDataService.Text = "启动";
            this.btnDataService.UseVisualStyleBackColor = false;
            this.btnDataService.Click += new System.EventHandler(this.btnDataService_Click);
            // 
            // btnFtpService
            // 
            this.btnFtpService.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnFtpService.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnFtpService.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnFtpService.Location = new System.Drawing.Point(229, 71);
            this.btnFtpService.Name = "btnFtpService";
            this.btnFtpService.Size = new System.Drawing.Size(142, 40);
            this.btnFtpService.TabIndex = 5;
            this.btnFtpService.Tag = "0";
            this.btnFtpService.Text = "启动";
            this.btnFtpService.UseVisualStyleBackColor = false;
            this.btnFtpService.Click += new System.EventHandler(this.btnFtpService_Click);
            // 
            // btnHeartbeat
            // 
            this.btnHeartbeat.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnHeartbeat.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnHeartbeat.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnHeartbeat.Location = new System.Drawing.Point(229, 123);
            this.btnHeartbeat.Name = "btnHeartbeat";
            this.btnHeartbeat.Size = new System.Drawing.Size(142, 40);
            this.btnHeartbeat.TabIndex = 8;
            this.btnHeartbeat.Tag = "0";
            this.btnHeartbeat.Text = "启动";
            this.btnHeartbeat.UseVisualStyleBackColor = false;
            this.btnHeartbeat.Click += new System.EventHandler(this.btnHeartbeat_Click);
            // 
            // lblHeartbeatStatus
            // 
            this.lblHeartbeatStatus.AutoSize = true;
            this.lblHeartbeatStatus.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblHeartbeatStatus.ForeColor = System.Drawing.Color.Red;
            this.lblHeartbeatStatus.Location = new System.Drawing.Point(144, 135);
            this.lblHeartbeatStatus.Name = "lblHeartbeatStatus";
            this.lblHeartbeatStatus.Size = new System.Drawing.Size(52, 14);
            this.lblHeartbeatStatus.TabIndex = 7;
            this.lblHeartbeatStatus.Text = "未启动";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(53, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 14);
            this.label4.TabIndex = 6;
            this.label4.Text = "心跳服务：";
            // 
            // btnDataSync
            // 
            this.btnDataSync.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnDataSync.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDataSync.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnDataSync.Location = new System.Drawing.Point(229, 177);
            this.btnDataSync.Name = "btnDataSync";
            this.btnDataSync.Size = new System.Drawing.Size(142, 40);
            this.btnDataSync.TabIndex = 11;
            this.btnDataSync.Tag = "0";
            this.btnDataSync.Text = "启动";
            this.btnDataSync.UseVisualStyleBackColor = false;
            this.btnDataSync.Click += new System.EventHandler(this.btnDataSync_Click);
            // 
            // lblDataSync
            // 
            this.lblDataSync.AutoSize = true;
            this.lblDataSync.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDataSync.ForeColor = System.Drawing.Color.Red;
            this.lblDataSync.Location = new System.Drawing.Point(144, 190);
            this.lblDataSync.Name = "lblDataSync";
            this.lblDataSync.Size = new System.Drawing.Size(52, 14);
            this.lblDataSync.TabIndex = 10;
            this.lblDataSync.Text = "未启动";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(25, 189);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 14);
            this.label5.TabIndex = 9;
            this.label5.Text = "配置同步服务：";
            // 
            // ServiceManagerFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 359);
            this.Controls.Add(this.btnDataSync);
            this.Controls.Add(this.lblDataSync);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnHeartbeat);
            this.Controls.Add(this.lblHeartbeatStatus);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnFtpService);
            this.Controls.Add(this.btnDataService);
            this.Controls.Add(this.lblFtpStatus);
            this.Controls.Add(this.lblDataStatus);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ServiceManagerFrm";
            this.ShowInTaskbar = false;
            this.Text = "服务管理";
            this.Load += new System.EventHandler(this.ServiceManagerFrm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblDataStatus;
        private System.Windows.Forms.Label lblFtpStatus;
        private System.Windows.Forms.Button btnDataService;
        private System.Windows.Forms.Button btnFtpService;
        private System.Windows.Forms.Button btnHeartbeat;
        private System.Windows.Forms.Label lblHeartbeatStatus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnDataSync;
        private System.Windows.Forms.Label lblDataSync;
        private System.Windows.Forms.Label label5;
    }
}