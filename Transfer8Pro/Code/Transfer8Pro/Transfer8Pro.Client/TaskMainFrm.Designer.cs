namespace Transfer8Pro.Client
{
    partial class TaskMainFrm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskMainFrm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.服务管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置服务ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重启作业服务ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.任务管理TToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新建任务ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.任务列表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.任务执行历史ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fTP上传历史FToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fTP上传历史列表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.配置CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fTP配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.系统配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.系统初始化ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.系统升级ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.检查新版本ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.开机启动ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加开机自动启动ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.取消开机自动启动ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.显示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.隐藏ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.下载配置数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.服务管理ToolStripMenuItem,
            this.任务管理TToolStripMenuItem,
            this.fTP上传历史FToolStripMenuItem,
            this.配置CToolStripMenuItem,
            this.系统升级ToolStripMenuItem,
            this.开机启动ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1233, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 服务管理ToolStripMenuItem
            // 
            this.服务管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设置服务ToolStripMenuItem,
            this.重启作业服务ToolStripMenuItem});
            this.服务管理ToolStripMenuItem.Name = "服务管理ToolStripMenuItem";
            this.服务管理ToolStripMenuItem.Size = new System.Drawing.Size(83, 21);
            this.服务管理ToolStripMenuItem.Text = "服务管理(&S)";
            // 
            // 设置服务ToolStripMenuItem
            // 
            this.设置服务ToolStripMenuItem.Name = "设置服务ToolStripMenuItem";
            this.设置服务ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.设置服务ToolStripMenuItem.Text = "设置服务";
            this.设置服务ToolStripMenuItem.Click += new System.EventHandler(this.设置服务ToolStripMenuItem_Click);
            // 
            // 重启作业服务ToolStripMenuItem
            // 
            this.重启作业服务ToolStripMenuItem.Name = "重启作业服务ToolStripMenuItem";
            this.重启作业服务ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.重启作业服务ToolStripMenuItem.Text = "重启作业服务";
            this.重启作业服务ToolStripMenuItem.Click += new System.EventHandler(this.重启作业服务ToolStripMenuItem_Click);
            // 
            // 任务管理TToolStripMenuItem
            // 
            this.任务管理TToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新建任务ToolStripMenuItem,
            this.任务列表ToolStripMenuItem,
            this.任务执行历史ToolStripMenuItem});
            this.任务管理TToolStripMenuItem.Name = "任务管理TToolStripMenuItem";
            this.任务管理TToolStripMenuItem.Size = new System.Drawing.Size(83, 21);
            this.任务管理TToolStripMenuItem.Text = "任务管理(&T)";
            // 
            // 新建任务ToolStripMenuItem
            // 
            this.新建任务ToolStripMenuItem.Name = "新建任务ToolStripMenuItem";
            this.新建任务ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.新建任务ToolStripMenuItem.Text = "新建任务";
            this.新建任务ToolStripMenuItem.Click += new System.EventHandler(this.新建任务ToolStripMenuItem_Click);
            // 
            // 任务列表ToolStripMenuItem
            // 
            this.任务列表ToolStripMenuItem.Name = "任务列表ToolStripMenuItem";
            this.任务列表ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.任务列表ToolStripMenuItem.Text = "任务列表";
            this.任务列表ToolStripMenuItem.Click += new System.EventHandler(this.任务列表ToolStripMenuItem_Click);
            // 
            // 任务执行历史ToolStripMenuItem
            // 
            this.任务执行历史ToolStripMenuItem.Name = "任务执行历史ToolStripMenuItem";
            this.任务执行历史ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.任务执行历史ToolStripMenuItem.Text = "任务执行历史";
            this.任务执行历史ToolStripMenuItem.Click += new System.EventHandler(this.任务执行历史ToolStripMenuItem_Click);
            // 
            // fTP上传历史FToolStripMenuItem
            // 
            this.fTP上传历史FToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fTP上传历史列表ToolStripMenuItem});
            this.fTP上传历史FToolStripMenuItem.Name = "fTP上传历史FToolStripMenuItem";
            this.fTP上传历史FToolStripMenuItem.Size = new System.Drawing.Size(102, 21);
            this.fTP上传历史FToolStripMenuItem.Text = "FTP上传历史(&F)";
            // 
            // fTP上传历史列表ToolStripMenuItem
            // 
            this.fTP上传历史列表ToolStripMenuItem.Name = "fTP上传历史列表ToolStripMenuItem";
            this.fTP上传历史列表ToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.fTP上传历史列表ToolStripMenuItem.Text = "FTP上传历史列表";
            this.fTP上传历史列表ToolStripMenuItem.Click += new System.EventHandler(this.fTP上传历史列表ToolStripMenuItem_Click);
            // 
            // 配置CToolStripMenuItem
            // 
            this.配置CToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fTP配置ToolStripMenuItem,
            this.系统配置ToolStripMenuItem,
            this.系统初始化ToolStripMenuItem,
            this.下载配置数据ToolStripMenuItem});
            this.配置CToolStripMenuItem.Name = "配置CToolStripMenuItem";
            this.配置CToolStripMenuItem.Size = new System.Drawing.Size(60, 21);
            this.配置CToolStripMenuItem.Text = "配置(&C)";
            // 
            // fTP配置ToolStripMenuItem
            // 
            this.fTP配置ToolStripMenuItem.Name = "fTP配置ToolStripMenuItem";
            this.fTP配置ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.fTP配置ToolStripMenuItem.Text = "FTP配置";
            this.fTP配置ToolStripMenuItem.Click += new System.EventHandler(this.fTP配置ToolStripMenuItem_Click);
            // 
            // 系统配置ToolStripMenuItem
            // 
            this.系统配置ToolStripMenuItem.Name = "系统配置ToolStripMenuItem";
            this.系统配置ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.系统配置ToolStripMenuItem.Text = "系统配置";
            this.系统配置ToolStripMenuItem.Click += new System.EventHandler(this.系统配置ToolStripMenuItem_Click);
            // 
            // 系统初始化ToolStripMenuItem
            // 
            this.系统初始化ToolStripMenuItem.Name = "系统初始化ToolStripMenuItem";
            this.系统初始化ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.系统初始化ToolStripMenuItem.Text = "系统初始化";
            this.系统初始化ToolStripMenuItem.Click += new System.EventHandler(this.系统初始化ToolStripMenuItem_Click);
            // 
            // 系统升级ToolStripMenuItem
            // 
            this.系统升级ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.检查新版本ToolStripMenuItem});
            this.系统升级ToolStripMenuItem.Name = "系统升级ToolStripMenuItem";
            this.系统升级ToolStripMenuItem.Size = new System.Drawing.Size(85, 21);
            this.系统升级ToolStripMenuItem.Text = "系统升级(&U)";
            // 
            // 检查新版本ToolStripMenuItem
            // 
            this.检查新版本ToolStripMenuItem.Name = "检查新版本ToolStripMenuItem";
            this.检查新版本ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.检查新版本ToolStripMenuItem.Text = "检查新版本";
            this.检查新版本ToolStripMenuItem.Click += new System.EventHandler(this.检查新版本ToolStripMenuItem_Click);
            // 
            // 开机启动ToolStripMenuItem
            // 
            this.开机启动ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.添加开机自动启动ToolStripMenuItem,
            this.取消开机自动启动ToolStripMenuItem});
            this.开机启动ToolStripMenuItem.Name = "开机启动ToolStripMenuItem";
            this.开机启动ToolStripMenuItem.Size = new System.Drawing.Size(84, 21);
            this.开机启动ToolStripMenuItem.Text = "开机启动(&A)";
            // 
            // 添加开机自动启动ToolStripMenuItem
            // 
            this.添加开机自动启动ToolStripMenuItem.Name = "添加开机自动启动ToolStripMenuItem";
            this.添加开机自动启动ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.添加开机自动启动ToolStripMenuItem.Text = "设置开机自动启动";
            this.添加开机自动启动ToolStripMenuItem.Click += new System.EventHandler(this.添加开机自动启动ToolStripMenuItem_Click);
            // 
            // 取消开机自动启动ToolStripMenuItem
            // 
            this.取消开机自动启动ToolStripMenuItem.Name = "取消开机自动启动ToolStripMenuItem";
            this.取消开机自动启动ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.取消开机自动启动ToolStripMenuItem.Text = "取消开机自动启动";
            this.取消开机自动启动ToolStripMenuItem.Click += new System.EventHandler(this.取消开机自动启动ToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 732);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1233, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "开卷采集系统";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.显示ToolStripMenuItem,
            this.隐藏ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 70);
            // 
            // 显示ToolStripMenuItem
            // 
            this.显示ToolStripMenuItem.Name = "显示ToolStripMenuItem";
            this.显示ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.显示ToolStripMenuItem.Text = "显示";
            this.显示ToolStripMenuItem.Click += new System.EventHandler(this.显示ToolStripMenuItem_Click);
            // 
            // 隐藏ToolStripMenuItem
            // 
            this.隐藏ToolStripMenuItem.Name = "隐藏ToolStripMenuItem";
            this.隐藏ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.隐藏ToolStripMenuItem.Text = "隐藏";
            this.隐藏ToolStripMenuItem.Click += new System.EventHandler(this.隐藏ToolStripMenuItem_Click);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // 下载配置数据ToolStripMenuItem
            // 
            this.下载配置数据ToolStripMenuItem.Name = "下载配置数据ToolStripMenuItem";
            this.下载配置数据ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.下载配置数据ToolStripMenuItem.Text = "下载配置数据";
            this.下载配置数据ToolStripMenuItem.Click += new System.EventHandler(this.下载配置数据ToolStripMenuItem_Click);
            // 
            // TaskMainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1233, 754);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "TaskMainFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "传8客户端";
            this.MinimumSizeChanged += new System.EventHandler(this.TaskMainFrm_MinimumSizeChanged);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TaskMainFrm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TaskMainFrm_FormClosed);
            this.Load += new System.EventHandler(this.TaskMainFrm_Load);
            this.Shown += new System.EventHandler(this.TaskMainFrm_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem 服务管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 任务管理TToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 配置CToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fTP上传历史FToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fTP配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设置服务ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新建任务ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 任务列表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fTP上传历史列表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 系统配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 任务执行历史ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 系统初始化ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 重启作业服务ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 系统升级ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 检查新版本ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 开机启动ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 添加开机自动启动ToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 显示ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 隐藏ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 取消开机自动启动ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 下载配置数据ToolStripMenuItem;
    }
}