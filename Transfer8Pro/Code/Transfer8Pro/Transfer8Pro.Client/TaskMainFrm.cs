using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Transfer8Pro.Core.Infrastructure;
using Transfer8Pro.Core.Service;
using Transfer8Pro.Utils;
using Transfer8Pro.Entity;
using Microsoft.Win32;
using Transfer8Pro.Core;

namespace Transfer8Pro.Client
{
    public partial class TaskMainFrm : BaseFrm
    {
        public TaskMainFrm()
        {
            InitializeComponent();
            base.SetMdiParent(this);
        }

        private void TaskMainFrm_Load(object sender, EventArgs e)
        {
            base.MyAsync(() => {
                base.UIAction(new MethodInvoker(() =>
                {
                    InitData();
                }));
            });          
        }


        private void InitData()
        {

            this.ShowInTaskbar = false;

            if (IsAutoStart())
            {
                this.WindowState = FormWindowState.Minimized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
            
            SetAuotoRunMenu(IsSetAutoStart());
            SystemConfigEntity systemConfig = new SystemConfigService().FindSystemConfig((int)SystemConfigs.SystemVersion);
            if (systemConfig != null)
            {
                //正式发布，DEBUG模式注销这两行
                OBAppUpdateLib.AppUpdateMananger.Init(Common.DecryptConfigKey("UpdateServer"), "Transfer8ProClient");
                OBAppUpdateLib.AppUpdateMananger.OnUpdated += AppUpdateMananger_OnUpdated;
                OBAppUpdateLib.AppUpdateMananger.StartCheckOnce(systemConfig.ExSetting01, systemConfig.ExSetting02);
            }           

            if (!base.OpenFormByMdiParent(typeof(TaskListFrm).Name))
            {
                TaskListFrm frm = new TaskListFrm();
                frm.MdiParent = this;
                frm.WindowState = FormWindowState.Maximized;
                frm.Show();
            }
        }

        private void TaskMainFrm_Shown(object sender, EventArgs e)
        {
            base.MyAsync(() =>
            {
                base.UIAction(new MethodInvoker(() =>
                {
                    base.CheckSystemConfiguration();
                }));
            });
          
        }

        private void SetAuotoRunMenu(bool isSetAutoStart)
        {
            if (isSetAutoStart)
            {
                取消开机自动启动ToolStripMenuItem.Enabled = true;
                添加开机自动启动ToolStripMenuItem.Enabled = false;
            }
            else
            {
                取消开机自动启动ToolStripMenuItem.Enabled = false;
                添加开机自动启动ToolStripMenuItem.Enabled = true;
            }
        }

        private void AppUpdateMananger_OnUpdated(object sender, OBAppUpdateLib.MyEventArgs<bool, string> e)
        {
            if (e.Value1)
            {
                //版本更新成功
                new SystemConfigService().UpdateConfigVersion((int)SystemConfigs.SystemVersion, e.Value2);
            }
            else
            {
                //版本暂不更新
                new SystemConfigService().IgnoreConfigVersion((int)SystemConfigs.SystemVersion, e.Value2);
            }
        }

        private void 设置服务ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!base.CheckSystemConfiguration())
            {
                return;
            }

            if (!base.OpenFormByMdiParent(typeof(ServiceManagerFrm).Name))
            {
                ServiceManagerFrm frm = new ServiceManagerFrm();
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
            }
        }

        private void 新建任务ToolStripMenuItem_Click(object sender, EventArgs e)
        {           
            if (!base.OpenFormByMdiParent(typeof(AddTaskFrm).Name))
            {
                AddTaskFrm frm = new AddTaskFrm();
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
            }
        }

        private void 任务列表ToolStripMenuItem_Click(object sender, EventArgs e)
        {          
            if (!base.OpenFormByMdiParent(typeof(TaskListFrm).Name))
            {
                TaskListFrm frm = new TaskListFrm();
                frm.MdiParent = this;
                frm.WindowState = FormWindowState.Maximized;
                frm.Show();
            }
        }

        private void fTP配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!base.OpenFormByMdiParent(typeof(FtpConfigFrm).Name))
            {
                FtpConfigFrm frm = new FtpConfigFrm();
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
            }
        }

        private void fTP上传历史列表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!base.OpenFormByMdiParent(typeof(FtpUploadListFrm).Name))
            {
                FtpUploadListFrm frm = new FtpUploadListFrm();
                frm.MdiParent = this;
                frm.WindowState = FormWindowState.Maximized;
                frm.Show();
            }
        }

        private void TaskMainFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            TaskManagerService.Stop();
            base.CloseMdiChildForm();
            Application.ExitThread();
            Application.Exit();
            this.Close();
            this.Dispose();
        }

        private void 系统配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SystemConfigFrm frm = new SystemConfigFrm();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }

        private void 任务执行历史ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!base.OpenFormByMdiParent(typeof(TaskHistoryListFrm).Name))
            {
                TaskHistoryListFrm frm = new TaskHistoryListFrm();
                frm.MdiParent = this;
                frm.WindowState = FormWindowState.Maximized;
                frm.Show();
            }
        }

        private void 系统初始化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!base.CheckSystemConfiguration())
            {
                return;
            }
            if (MessageBox.Show("系统初始化将清除所有业务数据，您确定要初始化系统吗？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                if (new InitDataBaseService().InitDbData())
                {
                    base.ShowMessage("系统初始化成功");
                    base.CheckSystemConfiguration();
                }
                else
                {
                    base.ShowErrorMessage("系统初始化失败，请联系开卷客服人员");
                }
            }
        }

        private void 重启作业服务ToolStripMenuItem_Click(object sender, EventArgs e)
        {           
            if (MessageBox.Show("您确定要重启作业服务吗？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                base.MyAsync(() =>
                {
                    base.UIAction(() =>
                    {
                        base.ShowLoading("重启作业服务中...");
                        TaskManagerService.ReStart();
                        base.HideLoading();
                        base.ShowMessage("已重启作业服务");
                    });
                });
               
            }
        }

        private void 检查新版本ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OBAppUpdateLib.AppUpdateMananger.Init(Common.DecryptConfigKey("UpdateServer"), "Transfer8ProClient");
            OBAppUpdateLib.AppUpdateMananger.OnUpdated += AppUpdateMananger_OnUpdated;
            OBAppUpdateLib.AppUpdateMananger.StartConstraintUpdate();
        }

        private void 添加开机自动启动ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsSetAutoStart())
            {
                base.ShowMessage("您已经设置开机自动启动");
                return;
            }

            if (MessageBox.Show("您确定要将开卷采集系统添加到电脑开机自动启动吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                SetAutoStart(true);
                SetAuotoRunMenu(true);             
            }
        }

        private void 取消开机自动启动ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("您确定要取消开机自动启动吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                SetAutoStart(false);
                SetAuotoRunMenu(false);               
            }
        }

        private void 显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }

        private void 隐藏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("你确定要退出开卷采集系统吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                TaskManagerService.Stop();
                base.CloseMdiChildForm();
                notifyIcon1.Visible = false;
                Application.ExitThread();
                Application.Exit();
                this.Close();
                this.Dispose();
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }

        private void TaskMainFrm_MinimumSizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                this.notifyIcon1.Visible = true;
            }
        }

        private void TaskMainFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;    //取消"关闭窗口"事件
                this.WindowState = FormWindowState.Minimized;    //使关闭时窗口向右下角缩小的效果              
                notifyIcon1.Visible = true;
                this.Hide();
                return;
            }
        }

        /// <summary>  
        /// 修改程序在注册表中的键值  
        /// </summary>  
        /// <param name="isAuto">true:开机启动,false:不开机自启</param> 
        private void SetAutoStart(bool isAuto)
        {
            try
            {
                if (isAuto == true)
                {
                    RegistryKey R_local = Registry.CurrentUser;// Registry.LocalMachine;// Registry.CurrentUser;
                    RegistryKey R_run = R_local.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                    R_run.SetValue("OpenBookT8Client", $"\"{Application.ExecutablePath}\" -autostart");
                    R_run.Close();
                    R_local.Close();
                    MessageBox.Show("开卷采集系统已设置为开机自动启动", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    RegistryKey R_local = Registry.CurrentUser;//RegistryKey R_local = Registry.CurrentUser;
                    RegistryKey R_run = R_local.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                    R_run.DeleteValue("OpenBookT8Client", false);
                    R_run.Close();
                    R_local.Close();
                    MessageBox.Show("开卷采集系统已取消开机自动启动", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("您需要管理员权限修改", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 是否设置开机自动启动
        /// </summary>
        /// <returns></returns>
        private bool IsSetAutoStart()
        {
            try
            {
                RegistryKey loca_chek = Registry.CurrentUser;
                RegistryKey run_Check = loca_chek.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                var registryItem = run_Check.GetValue("OpenBookT8Client");
                run_Check.Close();
                loca_chek.Close();

                if (registryItem != null && !string.IsNullOrEmpty(registryItem.ToString()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                base.ShowErrorMessage(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 是否是开机自动启动程序
        /// </summary>
        /// <returns></returns>
        private bool IsAutoStart()
        {
            string[] strArgs = Environment.GetCommandLineArgs();
            if (strArgs != null)
            {
                LogUtil.WriteLog("[strArgs]:" + string.Join(",", strArgs));
            }
            if (strArgs.Length >= 2 && strArgs[1].Equals("-autostart"))
            {
                return true;
            }
            return false;
        }

        private void 下载配置数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DownloadConfigFrm frm = new DownloadConfigFrm();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }
    }
}
