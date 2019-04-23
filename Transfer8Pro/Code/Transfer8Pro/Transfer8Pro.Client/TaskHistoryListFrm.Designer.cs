namespace Transfer8Pro.Client
{
    partial class TaskHistoryListFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskHistoryListFrm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cbxTaskExecutedStatus = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxCycleType = new System.Windows.Forms.ComboBox();
            this.cbxDataType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTaskID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.TaskID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TaskName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CycleType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EndTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ElapsedTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TaskExecutedStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModifyTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pagerCtrl1 = new Transfer8Pro.Client.Controls.PagerCtrl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.复制任务IDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查看明细ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1217, 631);
            this.splitContainer1.SplitterDistance = 111;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnReset);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.cbxTaskExecutedStatus);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbxCycleType);
            this.groupBox1.Controls.Add(this.cbxDataType);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtTaskID);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1193, 98);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(647, 58);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 16;
            this.btnReset.Text = "重置";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(647, 25);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 15;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cbxTaskExecutedStatus
            // 
            this.cbxTaskExecutedStatus.FormattingEnabled = true;
            this.cbxTaskExecutedStatus.Location = new System.Drawing.Point(66, 65);
            this.cbxTaskExecutedStatus.Name = "cbxTaskExecutedStatus";
            this.cbxTaskExecutedStatus.Size = new System.Drawing.Size(236, 20);
            this.cbxTaskExecutedStatus.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "执行状态";
            // 
            // cbxCycleType
            // 
            this.cbxCycleType.FormattingEnabled = true;
            this.cbxCycleType.Location = new System.Drawing.Point(395, 64);
            this.cbxCycleType.Name = "cbxCycleType";
            this.cbxCycleType.Size = new System.Drawing.Size(215, 20);
            this.cbxCycleType.TabIndex = 12;
            // 
            // cbxDataType
            // 
            this.cbxDataType.FormattingEnabled = true;
            this.cbxDataType.Location = new System.Drawing.Point(395, 26);
            this.cbxDataType.Name = "cbxDataType";
            this.cbxDataType.Size = new System.Drawing.Size(215, 20);
            this.cbxDataType.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(335, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "时间类型";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(335, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "数据类型";
            // 
            // txtTaskID
            // 
            this.txtTaskID.Location = new System.Drawing.Point(64, 29);
            this.txtTaskID.Name = "txtTaskID";
            this.txtTaskID.Size = new System.Drawing.Size(238, 21);
            this.txtTaskID.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "TaskID";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dgvList);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.pagerCtrl1);
            this.splitContainer2.Size = new System.Drawing.Size(1217, 516);
            this.splitContainer2.SplitterDistance = 466;
            this.splitContainer2.TabIndex = 0;
            // 
            // dgvList
            // 
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TaskID,
            this.TaskName,
            this.DataType,
            this.CycleType,
            this.StartTime,
            this.EndTime,
            this.ElapsedTime,
            this.TaskExecutedStatus,
            this.ModifyTime,
            this.CreateTime});
            this.dgvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvList.Location = new System.Drawing.Point(0, 0);
            this.dgvList.Name = "dgvList";
            this.dgvList.RowTemplate.Height = 23;
            this.dgvList.Size = new System.Drawing.Size(1217, 466);
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
            this.TaskName.MinimumWidth = 150;
            this.TaskName.Name = "TaskName";
            this.TaskName.Width = 200;
            // 
            // DataType
            // 
            this.DataType.HeaderText = "数据类型";
            this.DataType.Name = "DataType";
            // 
            // CycleType
            // 
            this.CycleType.HeaderText = "日期类型";
            this.CycleType.Name = "CycleType";
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
            // 
            // ModifyTime
            // 
            this.ModifyTime.HeaderText = "修改时间";
            this.ModifyTime.Name = "ModifyTime";
            // 
            // CreateTime
            // 
            this.CreateTime.HeaderText = "创建时间";
            this.CreateTime.Name = "CreateTime";
            // 
            // pagerCtrl1
            // 
            this.pagerCtrl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pagerCtrl1.CurrentPageNo = 0;
            this.pagerCtrl1.Location = new System.Drawing.Point(708, 4);
            this.pagerCtrl1.Name = "pagerCtrl1";
            this.pagerCtrl1.PageSize = 20;
            this.pagerCtrl1.Size = new System.Drawing.Size(486, 42);
            this.pagerCtrl1.TabIndex = 0;
            this.pagerCtrl1.TotalPages = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.复制任务IDToolStripMenuItem,
            this.查看明细ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(138, 48);
            // 
            // 复制任务IDToolStripMenuItem
            // 
            this.复制任务IDToolStripMenuItem.Name = "复制任务IDToolStripMenuItem";
            this.复制任务IDToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.复制任务IDToolStripMenuItem.Text = "复制任务ID";
            this.复制任务IDToolStripMenuItem.Click += new System.EventHandler(this.复制任务IDToolStripMenuItem_Click);
            // 
            // 查看明细ToolStripMenuItem
            // 
            this.查看明细ToolStripMenuItem.Name = "查看明细ToolStripMenuItem";
            this.查看明细ToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.查看明细ToolStripMenuItem.Text = "查看明细";
            this.查看明细ToolStripMenuItem.Click += new System.EventHandler(this.查看明细ToolStripMenuItem_Click);
            // 
            // TaskHistoryListFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1217, 631);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TaskHistoryListFrm";
            this.ShowInTaskbar = false;
            this.Text = "任务执行历史";
            this.Load += new System.EventHandler(this.TaskHistoryListFrm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TextBox txtTaskID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxCycleType;
        private System.Windows.Forms.ComboBox cbxDataType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbxTaskExecutedStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnReset;
        private Controls.PagerCtrl pagerCtrl1;
        private System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 查看明细ToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn TaskID;
        private System.Windows.Forms.DataGridViewTextBoxColumn TaskName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataType;
        private System.Windows.Forms.DataGridViewTextBoxColumn CycleType;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn EndTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn ElapsedTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn TaskExecutedStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModifyTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreateTime;
        private System.Windows.Forms.ToolStripMenuItem 复制任务IDToolStripMenuItem;
    }
}