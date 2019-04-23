namespace Transfer8Pro.Client
{
    partial class TaskLogDetailFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskLogDetailFrm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.TaskID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TaskName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CycleType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EndTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ElapsedTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TaskExecutedStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ErrorContent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pagerCtrl1 = new Transfer8Pro.Client.Controls.PagerCtrl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.复制异常信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pagerCtrl1);
            this.splitContainer1.Size = new System.Drawing.Size(1275, 632);
            this.splitContainer1.SplitterDistance = 582;
            this.splitContainer1.TabIndex = 0;
            // 
            // dgvList
            // 
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TaskID,
            this.TaskName,
            this.DataType,
            this.CycleType,
            this.FileName,
            this.StartTime,
            this.EndTime,
            this.ElapsedTime,
            this.TaskExecutedStatus,
            this.ErrorContent,
            this.CreateTime});
            this.dgvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvList.Location = new System.Drawing.Point(0, 0);
            this.dgvList.Name = "dgvList";
            this.dgvList.RowTemplate.Height = 23;
            this.dgvList.Size = new System.Drawing.Size(1275, 582);
            this.dgvList.TabIndex = 0;
            this.dgvList.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dgvList_RowStateChanged);
            // 
            // TaskID
            // 
            this.TaskID.HeaderText = "任务ID";
            this.TaskID.MinimumWidth = 150;
            this.TaskID.Name = "TaskID";
            this.TaskID.Width = 200;
            // 
            // TaskName
            // 
            this.TaskName.HeaderText = "任务名称";
            this.TaskName.MinimumWidth = 50;
            this.TaskName.Name = "TaskName";
            this.TaskName.Width = 150;
            // 
            // DataType
            // 
            this.DataType.HeaderText = "数据类型";
            this.DataType.Name = "DataType";
            this.DataType.Width = 80;
            // 
            // CycleType
            // 
            this.CycleType.HeaderText = "日期类型";
            this.CycleType.Name = "CycleType";
            this.CycleType.Width = 80;
            // 
            // FileName
            // 
            this.FileName.HeaderText = "文件名称";
            this.FileName.Name = "FileName";
            this.FileName.Width = 150;
            // 
            // StartTime
            // 
            this.StartTime.HeaderText = "执行开始时间";
            this.StartTime.Name = "StartTime";
            this.StartTime.Width = 120;
            // 
            // EndTime
            // 
            this.EndTime.HeaderText = "执行结束时间";
            this.EndTime.Name = "EndTime";
            this.EndTime.Width = 120;
            // 
            // ElapsedTime
            // 
            this.ElapsedTime.HeaderText = "执行时间";
            this.ElapsedTime.Name = "ElapsedTime";
            // 
            // TaskExecutedStatus
            // 
            this.TaskExecutedStatus.HeaderText = "执行结果";
            this.TaskExecutedStatus.Name = "TaskExecutedStatus";
            this.TaskExecutedStatus.Width = 80;
            // 
            // ErrorContent
            // 
            this.ErrorContent.HeaderText = "异常信息";
            this.ErrorContent.MinimumWidth = 20;
            this.ErrorContent.Name = "ErrorContent";
            this.ErrorContent.Width = 200;
            // 
            // CreateTime
            // 
            this.CreateTime.HeaderText = "创建时间";
            this.CreateTime.Name = "CreateTime";
            // 
            // pagerCtrl1
            // 
            this.pagerCtrl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pagerCtrl1.CurrentPageNo = 0;
            this.pagerCtrl1.Location = new System.Drawing.Point(771, 4);
            this.pagerCtrl1.Name = "pagerCtrl1";
            this.pagerCtrl1.PageSize = 20;
            this.pagerCtrl1.Size = new System.Drawing.Size(473, 42);
            this.pagerCtrl1.TabIndex = 0;
            this.pagerCtrl1.TotalPages = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.复制异常信息ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(149, 26);
            // 
            // 复制异常信息ToolStripMenuItem
            // 
            this.复制异常信息ToolStripMenuItem.Name = "复制异常信息ToolStripMenuItem";
            this.复制异常信息ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.复制异常信息ToolStripMenuItem.Text = "复制异常信息";
            this.复制异常信息ToolStripMenuItem.Click += new System.EventHandler(this.复制异常信息ToolStripMenuItem_Click);
            // 
            // TaskLogDetailFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1275, 632);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TaskLogDetailFrm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "日志明细";
            this.Load += new System.EventHandler(this.TaskLogDetailFrm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvList;
        private Controls.PagerCtrl pagerCtrl1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 复制异常信息ToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn TaskID;
        private System.Windows.Forms.DataGridViewTextBoxColumn TaskName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataType;
        private System.Windows.Forms.DataGridViewTextBoxColumn CycleType;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn EndTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn ElapsedTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn TaskExecutedStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn ErrorContent;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreateTime;
    }
}