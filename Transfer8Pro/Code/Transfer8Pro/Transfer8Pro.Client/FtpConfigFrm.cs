using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Transfer8Pro.Core;
using Transfer8Pro.Core.Infrastructure;
using Transfer8Pro.Core.Service;
using Transfer8Pro.Entity;
using Transfer8Pro.Utils;

namespace Transfer8Pro.Client
{
    public partial class FtpConfigFrm : BaseFrm
    {
        private FtpService ftpBll = null;
        private SystemConfigService systemConfigBll = null;

        public FtpConfigFrm()
        {
            InitializeComponent();
            ftpBll = new FtpService();
            systemConfigBll = new SystemConfigService();
        }

        private void FtpConfigFrm_Load(object sender, EventArgs e)
        {
            base.MyAsync(() =>
            {
                base.UIAction(new MethodInvoker(() =>
                {
                    BindData();
                }));
            });
        }

        private void btnFtpConnCheck_Click(object sender, EventArgs e)
        {
            CheckFtpConnect();
        }

        private void btnScanDirectory_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog saveFileDialog = new FolderBrowserDialog();
            saveFileDialog.Description = "设置FTP导出文件夹";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtExportFileDirectory.Text = saveFileDialog.SelectedPath;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            base.MyAsync(() =>
            {
                base.UIAction(() =>
                {
                    try
                    {                       
                        if (ValidateData())
                        {
                            base.ShowLoading("保存中...");
                            List<FtpConfigEntity> ftpConfigList = new List<FtpConfigEntity>();
                            ftpConfigList.Add(BuildFtpConfigEntity());
                            if (ValidateDataTwo())
                            {
                                try
                                {
                                    FtpConfigEntity ftpConfig = BuildFtpConfigEntityTwo();
                                    FtpHelper.ConnectFtpServer(ftpConfig);
                                }
                                catch (Exception ex)
                                {
                                    base.HideLoading();
                                    base.ShowErrorMessage(ex.Message);
                                    return;
                                }

                                ftpConfigList.Add(BuildFtpConfigEntityTwo());
                            }


                            bool isSuccess = ftpBll.InsertOrUpdate(ftpConfigList);
                            base.HideLoading();
                            if (isSuccess)
                            {
                                base.ShowMessage("FTP配置成功！请点击[服务管理-重启作业服务]菜单，使设置立即生效");
                                this.Close();
                            }
                            else
                            {
                                base.ShowErrorMessage("FTP配置失败");
                            }
                        }
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

        private void btnFtpConnCheckTwo_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtServerAddressTwo.Text))
            {
                base.ShowMessage("FTP地址为必填项");
                return;
            }

            if (string.IsNullOrEmpty(txtServerDirectoryTwo.Text))
            {
                base.ShowMessage("FTP目录名为必填项");
                return;
            }

            if (string.IsNullOrEmpty(txtUserNameTwo.Text))
            {
                base.ShowMessage("账号为必填项");
                return;
            }

            if (string.IsNullOrEmpty(txtUserPasswordTwo.Text))
            {
                base.ShowMessage("密码为必填项");
                return;
            }
            try
            {
                base.ShowLoading("检测中...");
                FtpConfigEntity ftpConfig = BuildFtpConfigEntityTwo();
                FtpHelper.ConnectFtpServer(ftpConfig);
                base.HideLoading();
                base.ShowMessage("FTP服务器连接成功");

            }
            catch (Exception ex)
            {
                LogUtil.WriteLog(ex);
                base.HideLoading();
                base.ShowErrorMessage(ex.Message);
            }
        }

        private bool CheckFtpConnect()
        {
            if (ValidateData(true))
            {
                try
                {
                    base.ShowLoading("检测中...");
                    FtpConfigEntity ftpConfig = BuildFtpConfigEntity();
                    FtpHelper.ConnectFtpServer(ftpConfig);
                    base.HideLoading();
                    base.ShowMessage("FTP服务器连接成功");

                    return true;
                }
                catch (Exception ex)
                {
                    LogUtil.WriteLog(ex);
                    base.HideLoading();
                    base.ShowErrorMessage(ex.Message);
                    return false;
                }
            }

            return false;
        }

        private FtpConfigEntity BuildFtpConfigEntity()
        {
            FtpConfigEntity ftpConfig = new FtpConfigEntity();
            ftpConfig.FtpID = txtServerAddress.Tag.ToString().ToInt();
            ftpConfig.ServerAddress = txtServerAddress.Text.Trim();
            ftpConfig.ServerDirectory = txtServerDirectory.Text.Trim();
            ftpConfig.UserName = txtUserName.Text.Trim();
            ftpConfig.UserPassword = txtUserPassword.Text.Trim();
            ftpConfig.ExportFileDirectory = txtExportFileDirectory.Text.Trim();
            ftpConfig.FtpType = 1;
            return ftpConfig;
        }

        private FtpConfigEntity BuildFtpConfigEntityTwo()
        {
            FtpConfigEntity ftpConfig = new FtpConfigEntity();
            ftpConfig.FtpID = txtServerAddressTwo.Tag.ToString().ToInt();
            ftpConfig.ServerAddress = txtServerAddressTwo.Text.Trim();
            ftpConfig.ServerDirectory = txtServerDirectoryTwo.Text.Trim();
            ftpConfig.UserName = txtUserNameTwo.Text.Trim();
            ftpConfig.UserPassword = txtUserPasswordTwo.Text.Trim();
            ftpConfig.ExportFileDirectory = txtExportFileDirectory.Text.Trim();
            ftpConfig.FtpType = 2;
            return ftpConfig;
        }

        private void BindData()
        {
            try
            {
                base.ShowLoading();
                SystemConfigEntity sysConfigEncryKey = systemConfigBll.FindSystemConfig((int)SystemConfigs.EncryptKey);
                if (sysConfigEncryKey == null)
                {
                    base.HideLoading();
                    base.ShowErrorMessage("系统配置表异常，请联系开卷客服人员");
                    this.Close();
                    return;
                }

                if (string.IsNullOrEmpty(sysConfigEncryKey.ExSetting01))
                {
                    base.HideLoading();
                    base.ShowErrorMessage("您未配置客户密钥");
                    this.Close();
                    return;
                }

                FtpConfigEntity ftpConfig = ftpBll.GetFirstFtpInfo();
                if (ftpConfig != null)
                {
                    txtServerAddress.Tag = ftpConfig.FtpID;
                    txtServerAddress.Text = ftpConfig.ServerAddress;
                    txtServerDirectory.Text = ftpConfig.ServerDirectory;
                    txtUserName.Text = ftpConfig.UserName;
                    txtUserPassword.Text = ftpConfig.UserPassword;
                    txtExportFileDirectory.Text = ftpConfig.ExportFileDirectory;
                }
                else
                {
                    txtServerAddress.Tag = 0;
                    //SystemConfigEntity systemConfig = new SystemConfigService().FindSystemConfig((int)SystemConfigs.OpenbookSysType);
                    int openbookSysType = Common.DecryptConfigKey("OpenBookSystemType").ToInt();

                    if (openbookSysType == 1)
                    {
                        txtServerDirectory.Text = "data";
                    }
                    else if (openbookSysType == 2)
                    {
                        txtServerDirectory.Text = "upload";
                    }
                    else
                    {
                        txtServerDirectory.Text = "";
                    }

                    txtExportFileDirectory.Text = Path.Combine(AppPath.DataFolder, "FtpExportDirectory");
                    if (!Directory.Exists(txtExportFileDirectory.Text))
                    {
                        Directory.CreateDirectory(txtExportFileDirectory.Text);
                    }
                }

                FtpConfigEntity ftpConfigSecond = ftpBll.GetSecondFtpInfo();
                if (ftpConfigSecond != null)
                {
                    txtServerAddressTwo.Tag = ftpConfigSecond.FtpID;
                    txtServerAddressTwo.Text = ftpConfigSecond.ServerAddress;
                    txtServerDirectoryTwo.Text = ftpConfigSecond.ServerDirectory;
                    txtUserNameTwo.Text = ftpConfigSecond.UserName;
                    txtUserPasswordTwo.Text = ftpConfigSecond.UserPassword;
                }
                else
                {
                    txtServerAddressTwo.Tag = 0;
                    int openbookSysType = Common.DecryptConfigKey("OpenBookSystemType").ToInt();

                    if (openbookSysType == 1)
                    {
                        txtServerDirectoryTwo.Text = "data";
                    }
                    else if (openbookSysType == 2)
                    {
                        txtServerDirectoryTwo.Text = "upload";
                    }
                    else
                    {
                        txtServerDirectoryTwo.Text = "";
                    }
                }
                base.HideLoading();
            }
            catch (Exception ex)
            {
                LogUtil.WriteLog(ex);
                base.HideLoading();
                base.ShowErrorMessage(ex.Message);
            }
        }

        private bool ValidateData(bool isFtpConnectCheck = false)
        {
            if (string.IsNullOrEmpty(txtServerAddress.Text))
            {
                base.ShowErrorMessage("FTP地址为必填项");
                return false;
            }

            if (string.IsNullOrEmpty(txtServerDirectory.Text))
            {
                base.ShowErrorMessage("FTP目录名为必填项");
                return false;
            }

            if (string.IsNullOrEmpty(txtUserName.Text))
            {
                base.ShowErrorMessage("账号为必填项");
                return false;
            }

            if (string.IsNullOrEmpty(txtUserPassword.Text))
            {
                base.ShowErrorMessage("密码为必填项");
                return false;
            }

            if (!isFtpConnectCheck)
            {
                if (string.IsNullOrEmpty(txtExportFileDirectory.Text))
                {
                    base.ShowErrorMessage("导出文件夹配置为必选项");
                    return false;
                }

                if (!Directory.Exists(txtExportFileDirectory.Text))
                {
                    base.ShowErrorMessage("导出文件夹路径不存在，请检查文件路径地址");
                    return false;
                }
            }

            return true;
        }

        private bool ValidateDataTwo()
        {
            if (!string.IsNullOrEmpty(txtServerAddressTwo.Text) && !string.IsNullOrEmpty(txtServerDirectoryTwo.Text) && !string.IsNullOrEmpty(txtUserNameTwo.Text) && !string.IsNullOrEmpty(txtUserPasswordTwo.Text))
            {
                return true;
            }
            return false;
        }
    }
}
