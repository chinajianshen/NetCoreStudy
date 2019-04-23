using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
    public partial class DownloadConfigFrm : BaseFrm
    {
        HttpHelper httpHelper = new HttpHelper();

        public DownloadConfigFrm()
        {
            InitializeComponent();
        }

        private void btnCheckUploadData_Click(object sender, EventArgs e)
        {
            if (!Validata())
            {
                return;
            }

            btnCheckUploadData.Enabled = false;
            btnCheckUploadData.Tag = btnCheckUploadData.Text;
            btnCheckUploadData.Text = "检测中...";

            base.MyAsync(() =>
            {
                string openbookWebApi = Common.DecryptConfigKey("OpenBookWebAPI");
                if (string.IsNullOrEmpty(openbookWebApi))
                {
                    base.UIAction(() =>
                    {
                        base.ShowMessage("系统未配置开卷WepApi接口地址");
                        btnCheckUploadData.Enabled = true;
                        btnCheckUploadData.Text = btnCheckUploadData.Tag.ToString();
                    });
                    return;
                }

                try
                {
                    base.UIAction(() =>
                    {
                        string msg;
                        if (CheckUploadConfig(openbookWebApi,out msg))
                        {
                            base.ShowMessage("检测到客户上传配置数据，可以下载配置");
                        }
                        else
                        {                         
                            base.ShowMessage($"未检测到客户配置数据{(!string.IsNullOrEmpty(msg) ? $"异常信息：{msg}" : "")}");
                        }

                        btnCheckUploadData.Enabled = true;
                        btnCheckUploadData.Text = btnCheckUploadData.Tag.ToString();
                    });
                }
                catch (Exception ex)
                {
                    base.UIAction(() =>
                    {
                        base.ShowMessage(ex.Message);
                        btnCheckUploadData.Enabled = true;
                        btnCheckUploadData.Text = btnCheckUploadData.Tag.ToString();
                    });
                }
            });
        }     

        private void btnDownloadData_Click(object sender, EventArgs e)
        {
            if (!Validata())
            {
                return;
            }

            if (MessageBox.Show("下载配置数据，将删除当前所有业务数据，您确定要下载配置数据吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
            {
                return;
            }

            btnDownloadData.Enabled = false;
            btnDownloadData.Tag = btnDownloadData.Text;
            btnDownloadData.Text = "下载中...";

            base.MyAsync(() =>
            {
                string openbookWebApi = Common.DecryptConfigKey("OpenBookWebAPI");
                if (string.IsNullOrEmpty(openbookWebApi))
                {
                    base.UIAction(() =>
                    {
                        base.ShowMessage("系统未配置开卷WepApi接口地址");
                        btnDownloadData.Enabled = true;
                        btnDownloadData.Text = btnDownloadData.Tag.ToString();
                    });
                    return;
                }

                try
                {
                    base.UIAction(() =>
                    {
                        string msg;
                        if (!CheckUploadConfig(openbookWebApi,out msg))
                        {
                            base.ShowMessage($"未检测到客户配置数据{(!string.IsNullOrEmpty(msg) ? $"异常信息：{msg}":"")}");
                            btnDownloadData.Enabled = true;
                            btnDownloadData.Text = btnDownloadData.Tag.ToString();
                            return;
                        }

                        Dictionary<string, string> dic = new Dictionary<string, string>();
                        dic.Add("signature", ApiHelper.GenerateApiSignature());

                        string ftpUserName = HttpHelper.UrlEncodeUnicode(RijndaelCrypt.Encrypt(txtFtpUserName.Text.Trim()));
                        string encryptString = HttpHelper.UrlEncodeUnicode(Common.EncryptData(txtFtpUserName.Text.Trim(), txtEncryptKey.Text.Trim()));

                        string url = $"{openbookWebApi.Trim(new char[] { '/', '\\' })}";
                        url = $"{url}/configdownload/downallconfig";
                        dynamic result = JsonObj<dynamic>.FromJson(httpHelper.Post(url, $"k={ftpUserName}&v={encryptString}", url, Encoding.UTF8, dic));


                        if (result.Status == 1)
                        {
                            string encryptData = (string)result.Data;
                            string decryptData = Common.DecryptData(encryptData, txtEncryptKey.Text.Trim());
                            dynamic configData = JsonObj<dynamic>.FromJson(decryptData);

                            List<SystemConfigEntity> sysConfigList = JsonObj<SystemConfigEntity>.FromJson3(JsonObj.ToJson(configData.SysConfig));
                            List<FtpConfigEntity> ftpConfigList = JsonObj<FtpConfigEntity>.FromJson3(JsonObj.ToJson(configData.FtpConfig));
                            List<TaskEntity> taskConfigList = JsonObj<TaskEntity>.FromJson3(JsonObj.ToJson(configData.TaskConfig));

                            bool isSuccess = new InitDataBaseService().SaveDownloadConfig(sysConfigList, ftpConfigList, taskConfigList);
                            if (isSuccess)
                            {
                                base.ShowMessage("下载成功");
                                this.Close();
                                return;
                            }
                            else
                            {
                                base.ShowMessage("下载失败");
                            }
                        }
                        else
                        {
                            base.ShowMessage($"下载配置数据失败,异常信息：{result.Msg}");
                        }

                        btnDownloadData.Enabled = true;
                        btnDownloadData.Text = btnDownloadData.Tag.ToString();
                    });                  
                }
                catch (Exception ex)
                {
                    base.UIAction(() =>
                    {
                        base.ShowMessage(ex.Message);
                        btnDownloadData.Enabled = true;
                        btnDownloadData.Text = btnDownloadData.Tag.ToString();
                    });
                }
            });
        }

        private bool Validata()
        {
            if (string.IsNullOrEmpty(txtEncryptKey.Text))
            {
                base.ShowMessage("客户密钥为必填项");
                txtEncryptKey.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtFtpUserName.Text))
            {
                base.ShowMessage("FTP账号为必填项");
                txtFtpUserName.Focus();
                return false;
            }
            return true;
        }

        private bool CheckUploadConfig(string apiBaseUrl,out string msg)
        {
            msg = string.Empty;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("signature", ApiHelper.GenerateApiSignature());

            string ftpUserName = HttpHelper.UrlEncodeUnicode(RijndaelCrypt.Encrypt(txtFtpUserName.Text.Trim()));
            string encryptString = HttpHelper.UrlEncodeUnicode(Common.EncryptData(txtFtpUserName.Text.Trim(), txtEncryptKey.Text.Trim()));

            string url = $"{apiBaseUrl.Trim(new char[] { '/', '\\' })}";
            url = $"{url}/configdownload/checkuploadconfig";
            dynamic result = JsonObj<dynamic>.FromJson(httpHelper.Post(url, $"k={ftpUserName}&v={encryptString}", url, Encoding.UTF8, dic));

            if (result.Status == 1)
            {
                return true;
            }
            else
            {
                msg = result.Msg;
                return false;
            }
        }
    }
}
