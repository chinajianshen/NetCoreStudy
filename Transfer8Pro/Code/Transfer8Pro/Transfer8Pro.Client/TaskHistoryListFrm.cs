using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Transfer8Pro.Client.Core;
using Transfer8Pro.Core;
using Transfer8Pro.Core.Service;
using Transfer8Pro.Entity;
using Transfer8Pro.Utils;

namespace Transfer8Pro.Client
{
    public partial class TaskHistoryListFrm : BaseFrm
    {
        private string _TaskID;
        public TaskHistoryListFrm()
        {
            InitializeComponent();

            pagerCtrl1.OnNextPageClicked += PagerCtrl1_OnNextPageClicked;
            pagerCtrl1.OnPreviousPageClicked += PagerCtrl1_OnPreviousPageClicked;
        }

        private void TaskHistoryListFrm_Load(object sender, EventArgs e)
        {
            base.MyAsync(() =>
            {
                base.UIAction(() =>
                {
                    try
                    {
                        base.ShowLoading();
                        BindDrowDownList();
                        BindDataGridView();
                        base.HideLoading();
                    }
                    catch (Exception ex)
                    {
                        LogUtil.WriteLog(ex);
                        base.HideLoading();
                        base.ShowErrorMessage(ex.Message);
                    }
                });
            });
        }

        public TaskHistoryListFrm(string taskID) : this()
        {
            _TaskID = taskID;
            txtTaskID.Text = taskID;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            base.MyAsync(() =>
            {
                base.UIAction(() =>
                {
                    base.ShowLoading();
                    loadGridData(1);
                    base.HideLoading();
                });
            });
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            base.MyAsync(() =>
            {
                base.UIAction(() =>
                {
                    base.ShowLoading();
                    txtTaskID.Text = "";
                    cbxCycleType.SelectedIndex = 0;
                    cbxDataType.SelectedIndex = 0;
                    cbxTaskExecutedStatus.SelectedIndex = 0;
                    loadGridData(1);
                    base.HideLoading();
                });
            });
        }
        public override void loadGridData(int pageIndex)
        {
            TaskLogViewEntity prms = BuildPrms();
            ParamtersForDBPageEntity<TaskLogViewEntity> pageData = new TaskLogService().GetTaskLogList(prms, pageIndex, pagerCtrl1.PageSize);


            if (pageIndex == 1)
            {
                pagerCtrl1.TotalPages = pageData.Total;
            }


            fillGridRowData(pageData.DataList.ToList());
        }

        private void fillGridRowData(List<TaskLogViewEntity> list)
        {
            dgvList.Rows.Clear();
            foreach (TaskLogViewEntity task in list)
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

                if (task.CreateTime != DateTime.MinValue)
                {
                    row.Cells["CreateTime"].Value = task.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
                }

                if (task.ModifyTime != DateTime.MinValue)
                {
                    row.Cells["ModifyTime"].Value = task.ModifyTime.ToString("yyyy-MM-dd HH:mm:ss");
                }
            }
        }

        private TaskLogViewEntity BuildPrms()
        {
            TaskLogViewEntity taskEntity = new TaskLogViewEntity();

            if (!string.IsNullOrEmpty(txtTaskID.Text))
            {
                taskEntity.TaskID = txtTaskID.Text.Trim();
            }

            if (cbxCycleType.SelectedIndex > 0)
            {
                taskEntity.CycleType = (CycleTypes)cbxCycleType.SelectedValue.ToString().ToInt();
            }

            if (cbxDataType.SelectedIndex > 0)
            {
                taskEntity.DataType = (DataTypes)cbxDataType.SelectedValue.ToString().ToInt();
            }

            if (cbxTaskExecutedStatus.SelectedIndex > 0)
            {
                taskEntity.TaskExecutedStatus = (TaskExecutedStatus)cbxTaskExecutedStatus.SelectedValue.ToString().ToInt();
            }

            return taskEntity;
        }

        private void BindDataGridView()
        {
            BindControl.InitDataGridView(dgvList);
            dgvList.CellClick += DgvList_CellClick; ;
            dgvList.CellMouseDown += DgvList_CellMouseDown; ;
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
            }
        }

        private void DgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {

            }
        }

        private void BindDrowDownList()
        {
            BindControl.BindComboBox(cbxDataType, Common.GetDataTypeList());
            BindControl.BindComboBox(cbxCycleType, Common.GetCycleTypeList());
            BindControl.BindComboBox(cbxTaskExecutedStatus, Common.GetTaskExecutedStatusList());
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

        private void dgvList_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            e.Row.HeaderCell.Value = $"{e.Row.Index + 1}";
        }

        private void 查看明细ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvList.CurrentRow != null)
            {
                string taskID = dgvList.CurrentRow.Cells["TaskID"].Value.ToString();
                TaskLogDetailFrm frm = new TaskLogDetailFrm(taskID);
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
            }
            else
            {
                base.ShowMessage("未获取到当前行数据");
            }

        }

        private void 复制任务IDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvList.CurrentRow != null)
            {
                string taskID = dgvList.CurrentRow.Cells["TaskID"].Value.ToString();
                Clipboard.SetDataObject(taskID);
                base.ShowMessage("TaskID复制到剪贴板");
            }
            else
            {
                base.ShowMessage("未获取到当前行数据");
            }
        }
    }
}
