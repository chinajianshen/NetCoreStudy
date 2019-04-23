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
    public partial class FtpUploadListFrm : BaseFrm
    {
        public FtpUploadListFrm()
        {
            InitializeComponent();
            pagerCtrl1.OnNextPageClicked += PagerCtrl1_OnNextPageClicked;
            pagerCtrl1.OnPreviousPageClicked += PagerCtrl1_OnPreviousPageClicked;
        }

        private void PagerCtrl1_OnPreviousPageClicked(object sender, Entity.MyEventArgs<int> e)
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

        private void PagerCtrl1_OnNextPageClicked(object sender, Entity.MyEventArgs<int> e)
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

        private void FtpUploadListFrm_Load(object sender, EventArgs e)
        {
            base.MyAsync(() =>
            {
                base.UIAction(() =>
                {
                    base.ShowLoading();
                    BindDrowDownList();
                    BindDataGridView();
                    base.HideLoading();
                });
            });           
        }

        private void BindDrowDownList()
        {
            BindControl.BindComboBox(cbxUploadStatus, Common.GetFtpUplpadStatusList());
            BindControl.BindComboBox(cbxDataType, Common.GetDataTypeList());
            BindControl.BindComboBox(cbxCycleType, Common.GetCycleTypeList());
        }

        private void BindDataGridView()
        {
            BindControl.InitDataGridView(dgvList);
            dgvList.CellClick += DgvList_CellClick;
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
            }
        }

        private void DgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {

            }
        }

        public override void loadGridData(int pageIndex)
        {
            FtpUploadLogEntity prms = BuildPrms();
            ParamtersForDBPageEntity<FtpUploadLogEntity> pageData = new FtpUploadLogService().GetFtpUploadList(prms, pageIndex, pagerCtrl1.PageSize);


            if (pageIndex == 1)
            {
                pagerCtrl1.TotalPages = pageData.Total;
            }


            fillGridRowData(pageData.DataList.ToList());
        }

        private FtpUploadLogEntity BuildPrms()
        {
            FtpUploadLogEntity ftpUploadLogEntity = new FtpUploadLogEntity();         

            if (!string.IsNullOrEmpty(txtFileName.Text))
            {
                ftpUploadLogEntity.FileName = txtFileName.Text.Trim();
            }

            if (cbxUploadStatus.SelectedIndex != 0)
            {
                ftpUploadLogEntity.FtpUploadStatus = (FtpUploadStatus)cbxUploadStatus.SelectedValue.ToString().ToInt();
            }
            
            if (cbxDataType.SelectedIndex != 0)
            {
                ftpUploadLogEntity.DataType = (DataTypes)cbxDataType.SelectedValue.ToString().ToInt();
            }

            if (cbxCycleType.SelectedIndex != 0)
            {
                ftpUploadLogEntity.CycleType = (CycleTypes)cbxCycleType.SelectedValue.ToString().ToInt();
            }
            return ftpUploadLogEntity;
        }

        private void fillGridRowData(List<FtpUploadLogEntity> list)
        {
            dgvList.Rows.Clear();
            foreach (FtpUploadLogEntity task in list)
            {
                int index = dgvList.Rows.Add();
                DataGridViewRow row = dgvList.Rows[index];

                if (task.FtpUploadStatus == global::FtpUploadStatus.上传失败)
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                }

                row.Cells["FtpUploadID"].Value = task.FtpUploadID;              
                row.Cells["FileName"].Value = task.FileName;

                if (task.UploadStartTime != DateTime.MinValue)
                {
                    row.Cells["UploadStartTime"].Value = task.UploadStartTime.ToString("yyyy-MM-dd HH:mm:ss");
                }

                if (task.UploadEndTime != DateTime.MinValue)
                {
                    row.Cells["UploadEndTime"].Value = task.UploadEndTime.ToString("yyyy-MM-dd HH:mm:ss");
                }

                row.Cells["ElapsedTime"].Value = task.ElapsedTime;

                row.Cells["FtpUploadStatus"].Tag = (int)task.FtpUploadStatus;
                row.Cells["FtpUploadStatus"].Value = task.FtpUploadStatus.ToString();

                row.Cells["CycleType"].Tag = (int)task.CycleType;
                row.Cells["CycleType"].Value = task.CycleType.ToString();

                row.Cells["DataType"].Tag = (int)task.DataType;
                row.Cells["DataType"].Value = task.DataType.ToString();

                if (task.CreateTime != DateTime.MinValue)
                {
                    row.Cells["CreateTime"].Value = task.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
                }
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            loadGridData(1);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            base.MyAsync(() =>
            {
                base.UIAction(() =>
                {
                    base.ShowLoading();
                    txtFileName.Text = "";
                    cbxUploadStatus.SelectedIndex = 0;
                    cbxDataType.SelectedIndex = 0;
                    cbxCycleType.SelectedIndex = 0;
                    loadGridData(1);
                    base.HideLoading();
                });
            });           
        }

        private void dgvList_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            e.Row.HeaderCell.Value = $"{e.Row.Index + 1}";
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvList.CurrentRow != null)
            {
                DataGridViewRow currrow = dgvList.CurrentRow;
                int ftpUploadID = currrow.Cells["FtpUploadID"].Value.ToString().ToInt();
                bool isSuccess = new FtpUploadLogService().Delete(ftpUploadID);
                if (isSuccess)
                {
                    base.ShowMessage("删除成功");
                    loadGridData(1);
                }
                else
                {
                    base.ShowMessage("删除失败");
                }
            }
            else
            {
                base.ShowMessage("未获取到当前行数据");
            }
        }
    }
}
