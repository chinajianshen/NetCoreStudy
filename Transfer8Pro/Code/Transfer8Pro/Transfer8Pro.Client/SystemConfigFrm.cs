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
using Transfer8Pro.Core.Infrastructure;
using Transfer8Pro.Core.Service;
using Transfer8Pro.Entity;
using Transfer8Pro.Utils;

namespace Transfer8Pro.Client
{
    public partial class SystemConfigFrm : BaseFrm
    {
        private SystemConfigService systemConfigBll = null;
        public SystemConfigFrm()
        {
            InitializeComponent();
            systemConfigBll = new SystemConfigService();
        }

        private void SystemConfigFrm_Load(object sender, EventArgs e)
        {
            BindData();
        }

        private void BindData()
        {
            base.MyAsync(() =>
            {
                base.UIAction(() =>
                {
                    try
                    {
                        base.ShowLoading();
                        List<KVEntity> comboxList = new List<KVEntity>();
                        comboxList.Add(new KVEntity { K = "1分", V = "0/1" });
                        comboxList.Add(new KVEntity { K = "2分", V = "0/2" });
                        comboxList.Add(new KVEntity { K = "5分", V = "0/5" });
                        comboxList.Add(new KVEntity { K = "10分", V = "0/10" });
                        comboxList.Add(new KVEntity { K = "30分", V = "0/30" });

                        BindControl.BindComboBox(cbxDataExport, comboxList);
                        BindControl.BindComboBox(cbxFtpUpload, DeepCopyUtil.DeepCopyByBinary(comboxList));
                        BindControl.BindComboBox(cbxHeartbeat, DeepCopyUtil.DeepCopyByBinary(comboxList));
                        BindControl.BindComboBox(cbxConfigSync, DeepCopyUtil.DeepCopyByBinary(comboxList));

                        List<SystemConfigEntity> list = systemConfigBll.GetSystemConfigList().Where(item => item.SysConfigID != (int)SystemConfigs.SystemVersion).ToList(); ;
                        if (list.Count == 0)
                        {
                            base.HideLoading();
                            base.ShowErrorMessage("检测到系统配置表异常，请联系开卷客服");
                            return;
                        }

                        foreach (SystemConfigEntity systemConfig in list)
                        {
                            if (systemConfig.SysConfigID == (int)SystemConfigs.DataExportService)
                            {
                                CronExpressionEntity cronExpression = QuartzHelper.ResolveCronExpression(systemConfig.Cron);
                                cbxDataExport.SelectedValue = cronExpression.Minute != "*" ? cronExpression.Minute : cronExpression.Second;
                                continue;
                            }

                            if (systemConfig.SysConfigID == (int)SystemConfigs.FtpUpoladService)
                            {
                                CronExpressionEntity cronExpression = QuartzHelper.ResolveCronExpression(systemConfig.Cron);
                                cbxFtpUpload.SelectedValue = cronExpression.Minute != "*" ? cronExpression.Minute : cronExpression.Second;
                                continue;
                            }

                            if (systemConfig.SysConfigID == (int)SystemConfigs.HeartbeatService)
                            {
                                CronExpressionEntity cronExpression = QuartzHelper.ResolveCronExpression(systemConfig.Cron);
                                cbxHeartbeat.SelectedValue = cronExpression.Minute != "*" ? cronExpression.Minute : cronExpression.Second;

                                continue;
                            }

                            if (systemConfig.SysConfigID == (int)SystemConfigs.EncryptKey)
                            {
                                if (!string.IsNullOrEmpty(systemConfig.ExSetting01))
                                {
                                    txtEncryptKey.Text = systemConfig.ExSetting01;
                                    txtEncryptKey.Enabled = false;
                                    continue;
                                }
                            }

                            if (systemConfig.SysConfigID == (int)SystemConfigs.ConfigSynStatus)
                            {
                                CronExpressionEntity cronExpression = QuartzHelper.ResolveCronExpression(systemConfig.Cron);
                                cbxConfigSync.SelectedValue = cronExpression.Minute != "*" ? cronExpression.Minute : cronExpression.Second;

                                continue;
                            }

                            //if (systemConfig.SysConfigID == (int)SystemConfigs.OpenbookSysType)
                            //{
                            //    if (systemConfig.Status == 1)
                            //    {
                            //        rdoDaySys.Checked = true;
                            //    }
                            //    else if (systemConfig.Status == 2)
                            //    {
                            //        rdoT8Sys.Checked = true;
                            //    }

                            //    if (systemConfig.Status != 0)
                            //    {
                            //        rdoDaySys.Enabled = false;
                            //        rdoT8Sys.Enabled = false;
                            //    }
                            //    continue;
                            //}
                        }
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            base.MyAsync(() =>
            {
                base.UIAction(() =>
                {
                    if (ValidateData())
                    {
                        try
                        {
                            base.ShowLoading("保存中...");
                            List<SystemConfigEntity> systemConfigList = new List<SystemConfigEntity>();
                            List<SystemConfigEntity> list = systemConfigBll.GetSystemConfigList().Where(item => item.SysConfigID != (int)SystemConfigs.SystemVersion).ToList();

                            foreach (SystemConfigEntity systemConfig in list)
                            {
                                if (systemConfig.SysConfigID == (int)SystemConfigs.DataExportService)
                                {
                                    systemConfig.Cron = GenerateCronString(cbxDataExport);
                                    continue;
                                }

                                if (systemConfig.SysConfigID == (int)SystemConfigs.FtpUpoladService)
                                {
                                    systemConfig.Cron = GenerateCronString(cbxFtpUpload);
                                    continue;
                                }

                                if (systemConfig.SysConfigID == (int)SystemConfigs.HeartbeatService)
                                {
                                    systemConfig.Cron = GenerateCronString(cbxHeartbeat);
                                    continue;
                                }

                                if (systemConfig.SysConfigID == (int)SystemConfigs.EncryptKey)
                                {
                                    systemConfig.Status = 1;
                                    systemConfig.ExSetting01 = txtEncryptKey.Text.Trim();
                                    continue;
                                }

                                if (systemConfig.SysConfigID == (int)SystemConfigs.ConfigSynStatus)
                                {
                                    systemConfig.Cron = GenerateCronString(cbxConfigSync);
                                    continue;
                                }

                                //if (systemConfig.SysConfigID == (int)SystemConfigs.OpenbookSysType)
                                //{
                                //    if (rdoDaySys.Checked)
                                //    {
                                //        systemConfig.Status = 1;
                                //    }
                                //    else
                                //    {
                                //        systemConfig.Status = 2;
                                //    }
                                //    continue;
                                //}
                            }

                            bool isSuccess = systemConfigBll.UpdateList(list);
                            base.HideLoading();
                            if (isSuccess)
                            {
                                base.ShowMessage("系统配置保存成功!请点击[服务管理-重启作业服务]菜单，使设置立即生效");
                                this.DialogResult = DialogResult.OK;
                                this.Close();
                            }
                            else
                            {
                                base.ShowMessage("系统配置保存失败");
                            }
                        }
                        catch(Exception ex)
                        {
                            LogUtil.WriteLog(ex);
                            base.HideLoading();
                            base.ShowErrorMessage(ex.Message);
                        }                       
                    }
                });
            });           
        }

        private string GenerateCronString(ComboBox cbx)
        {
            string value = cbx.SelectedValue.ToString();
            string title = ((KVEntity)cbx.SelectedItem).K;
            string cronString = "{0} {1} * * * ? * ";
            if (title.Contains("秒"))
            {
                cronString = string.Format(cronString, value, "*");
            }
            else
            {
                cronString = string.Format(cronString, "0", value);
            }
            return cronString;
        }

        private bool ValidateData()
        {
            if (string.IsNullOrEmpty(txtEncryptKey.Text))
            {
                base.ShowErrorMessage("请设置客户密钥");
                return false;
            }

            Guid guid;
            if (!Guid.TryParse(txtEncryptKey.Text.Trim(), out guid))
            {
                base.ShowErrorMessage("客户密钥格式不正确，请联系开卷客服人员");
                return false;
            }
            return true;
        }

    }
}
