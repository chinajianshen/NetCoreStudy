using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Transfer8Pro.Client.Core;
using Transfer8Pro.Core.Service;
using Transfer8Pro.Entity;
using Transfer8Pro.Utils;

namespace Transfer8Pro.Client
{
    public partial class TaskLogDetailFrm : BaseFrm
    {
        private string _taskID;
        public TaskLogDetailFrm()
        {
            InitializeComponent();

            pagerCtrl1.PageSize = 50;
            pagerCtrl1.OnNextPageClicked += PagerCtrl1_OnNextPageClicked;
            pagerCtrl1.OnPreviousPageClicked += PagerCtrl1_OnPreviousPageClicked;
        }

        public TaskLogDetailFrm(string taskID) : this()
        {
            _taskID = taskID;
        }

        private void TaskLogDetailFrm_Load(object sender, EventArgs e)
        {
            base.MyAsync(() =>
            {
                base.UIAction(() =>
                {
                    try
                    {
                        base.ShowLoading();

                        if (string.IsNullOrEmpty(_taskID))
                        {
                            base.ShowErrorMessage("系统参数丢失");
                            this.Close();
                            return;
                        }
                        BindDataGridView();

                        base.HideLoading();
                    }
                    catch(Exception ex)
                    {
                        LogUtil.WriteLog(ex);
                        base.HideLoading();
                        base.ShowErrorMessage(ex.Message);
                        this.Close();
                    }
                });
            });          
        }

        private void dgvList_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            e.Row.HeaderCell.Value = $"{e.Row.Index+1}";
        }

        private void BindDataGridView()
        {
            BindControl.InitDataGridView(dgvList);
            dgvList.CellMouseDown += DgvList_CellMouseDown;
            loadGridData(1);
        }

        private void DgvList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex > -1 && e.ColumnIndex > -1)
            {
                dgvList.Rows[e.RowIndex].ContextMenuStrip = contextMenuStrip1;
                dgvList.ClearSelection();
                dgvList.Rows[e.RowIndex].Selected = true;
                dgvList.CurrentCell = dgvList.Rows[e.RowIndex].Cells[e.ColumnIndex];

                string errorContent = dgvList.Rows[e.RowIndex].Cells["ErrorContent"].Value.ToString();
                if (string.IsNullOrEmpty(errorContent))
                {
                    复制异常信息ToolStripMenuItem.Enabled = false;
                }
                else
                {
                    复制异常信息ToolStripMenuItem.Enabled = true;
                }
            }
        }

        public void loadGridData(int pageIndex)
        {
            TaskLogDetailViewEntity prms = BuildPrms();
            ParamtersForDBPageEntity<TaskLogDetailViewEntity> pageData = new TaskLogService().GetTaskDetailLogList(prms, pageIndex, pagerCtrl1.PageSize);


            if (pageIndex == 1)
            {
                pagerCtrl1.TotalPages = pageData.Total;
            }


            fillGridRowData(pageData.DataList.ToList());
        }

        private TaskLogDetailViewEntity BuildPrms()
        {
            TaskLogDetailViewEntity taskEntity = new TaskLogDetailViewEntity();
            taskEntity.TaskID = _taskID;
            return taskEntity;          
        }

        private void fillGridRowData(List<TaskLogDetailViewEntity> list)
        {
            dgvList.Rows.Clear();
            foreach (TaskLogDetailViewEntity task in list)
            {
                int index = dgvList.Rows.Add();
                DataGridViewRow row = dgvList.Rows[index];

                if (task.TaskExecutedStatus == global::TaskExecutedStatus.失败)
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                }

                row.Cells["TaskID"].Value = task.TaskID;
                row.Cells["TaskName"].Value = task.TaskName;


                row.Cells["DataType"].Tag = (int)task.DataType;
                row.Cells["DataType"].Value = task.DataType.ToString();

                row.Cells["CycleType"].Tag = (int)task.CycleType;
                row.Cells["CycleType"].Value = task.CycleType.ToString();

                if (task.StartTime != DateTime.MinValue)
                {
                    row.Cells["StartTime"].Value = task.StartTime.ToString("yyyy-MM-dd HH:mm:ss");
                }

                if (task.EndTime != DateTime.MinValue)
                {
                    row.Cells["EndTime"].Value = task.EndTime.ToString("yyyy-MM-dd HH:mm:ss");
                }

                row.Cells["ElapsedTime"].Value = task.ElapsedTime;

                row.Cells["TaskExecutedStatus"].Tag = (int)task.TaskExecutedStatus;
                row.Cells["TaskExecutedStatus"].Value = task.TaskExecutedStatus.ToString();

                row.Cells["ErrorContent"].Value = task.ErrorContent;
                row.Cells["ErrorContent"].ToolTipText = task.ErrorContent;


                row.Cells["FileName"].Value = task.FileName;

                if (task.CreateTime != DateTime.MinValue)
                {
                    row.Cells["CreateTime"].Value = task.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
                }
            }
        }      

        private void PagerCtrl1_OnPreviousPageClicked(object sender, MyEventArgs<int> e)
        {
            base.MyAsync(() =>
            {
                base.UIAction(() =>
                {
                    base.ShowLoading();
                    loadGridData(e.Value1 + 1);
                    base.HideLoading();
                });
            });
        }

        private void PagerCtrl1_OnNextPageClicked(object sender, MyEventArgs<int> e)
        {          
            base.MyAsync(() =>
            {
                base.UIAction(() =>
                {
                    base.ShowLoading();
                    loadGridData(e.Value1 + 1);
                    base.HideLoading();
                });
            });
        }

        private void 复制异常信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvList.CurrentRow != null)
            {
                string taskID = dgvList.CurrentRow.Cells["ErrorContent"].Value.ToString();
                Clipboard.SetDataObject(taskID);
                base.ShowMessage("异常信息复制到剪贴板");
            }
            else
            {
                base.ShowMessage("未获取到当前行数据");
            }
        }
    }
}
