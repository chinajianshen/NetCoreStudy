using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using Transfer8Pro.Core;
using Transfer8Pro.Core.Service.OB;
using Transfer8Pro.Entity;
using Transfer8Pro.Utils;

namespace Transfer8Pro.WebAPI.Modules
{
    public class ConfigDownloadModule: BaseModule
    {
        public ConfigDownloadModule() : base("configdownload")
        {
            Post["/checkuploadconfig"] = _ =>
            {
                string encryptKey = HttpContext.Current.Request.Form["k"];
                string encryptValue = HttpContext.Current.Request.Form["v"];

                if (string.IsNullOrEmpty(encryptKey) || string.IsNullOrEmpty(encryptValue))
                {
                    return ErrorResult("参数数据不完整");
                }

                string fname = RijndaelCrypt.Decrypt(encryptKey);
                string ftpEncryptKey;
                if (!base.IsExistsFtpUserName(fname, out ftpEncryptKey))
                {
                    return ErrorResult($"参数k值[{encryptKey}]系统中不存在");
                }

                string obDecryptValue = Common.EncryptData(fname, ftpEncryptKey);
                if (encryptValue != obDecryptValue)
                {
                    return ErrorResult($"参数v值[{encryptValue}]数据解密验证失败");
                }

                bool isExists = new ClientUploadService().CheckUploadConfig(fname);
                if (isExists)
                {
                    return OkResult();
                }
                else
                {
                    return ErrorResult();
                }               
            };

            Post["/downallconfig"] = _ =>
            {
                string encryptKey = HttpContext.Current.Request.Form["k"];
                string encryptValue = HttpContext.Current.Request.Form["v"];

                if (string.IsNullOrEmpty(encryptKey) || string.IsNullOrEmpty(encryptValue))
                {
                    return ErrorResult("参数数据不完整");
                }

                string fname = RijndaelCrypt.Decrypt(encryptKey);
                string ftpEncryptKey;
                if (!base.IsExistsFtpUserName(fname, out ftpEncryptKey))
                {
                    return ErrorResult($"参数k值[{encryptKey}]系统中不存在");
                }

                string obDecryptValue = Common.EncryptData(fname, ftpEncryptKey);
                if (encryptValue != obDecryptValue)
                {
                    return ErrorResult($"参数v值[{encryptValue}]数据解密验证失败");
                }

                #region 系统配置
                SystemConfigEntity prmsSystemEntity = new SystemConfigEntity { FtpUserName = fname };
                List<SystemConfigEntity> systemConfigList = new ClientUploadService().DownloadSystemConfigList(prmsSystemEntity);
                #endregion

                #region FTP配置
                FtpConfigViewEntity prmsFtpEntity = new FtpConfigViewEntity { FtpUserName = fname };
                List<FtpConfigEntity> ftpList = new ClientUploadService().DownloadFtpConfigList(prmsFtpEntity);
                #endregion

                #region 任务配置
                TaskEntity prmsTaskEntity = new TaskEntity { FtpUserName = fname };
                List<TaskEntity> taskList = new ClientUploadService().DownloadTaskList(new TaskEntity { FtpUserName = fname });
                #endregion

                dynamic result = new ExpandoObject();
                result.SysConfig = systemConfigList;
                result.FtpConfig = ftpList;
                result.TaskConfig = taskList;

                string json = JsonObj<dynamic>.ToJson(result);
                string encryptList = Common.EncryptData(json, ftpEncryptKey);
                return OkResult("", encryptList);                               
            };

            Post["/sysconfig"] = _ =>
            {
                string encryptKey = HttpContext.Current.Request.Form["k"];
                string encryptValue = HttpContext.Current.Request.Form["v"];

                if (string.IsNullOrEmpty(encryptKey) || string.IsNullOrEmpty(encryptValue))
                {
                    return ErrorResult("参数数据不完整");
                }

                string fname = RijndaelCrypt.Decrypt(encryptKey);
                string ftpEncryptKey;
                if (!base.IsExistsFtpUserName(fname, out ftpEncryptKey))
                {
                    return ErrorResult($"参数k值[{encryptKey}]系统中不存在");
                }

                string obDecryptValue = Common.EncryptData(fname, ftpEncryptKey);
                if (encryptValue != obDecryptValue)
                {
                    return ErrorResult($"参数v值[{encryptValue}]数据解密验证失败");
                }

                SystemConfigEntity prmsEntity = new SystemConfigEntity { FtpUserName = fname };
                List<SystemConfigEntity> list = new ClientUploadService().DownloadSystemConfigList(prmsEntity);

                if (list != null && list.Count > 0)
                {
                    string json = JsonObj<SystemConfigEntity>.ToJson(list);
                    string encryptList = Common.EncryptData(json, ftpEncryptKey);

                    return OkResult("",encryptList);
                }
                else
                {
                    return ErrorResult("系统未找到客户系统配置数据");
                }               
            };

            Post["/ftpconfig"] = _ =>
            {
                string encryptKey = HttpContext.Current.Request.Form["k"];
                string encryptValue = HttpContext.Current.Request.Form["v"];

                if (string.IsNullOrEmpty(encryptKey) || string.IsNullOrEmpty(encryptValue))
                {
                    return ErrorResult("参数数据不完整");
                }

                string fname = RijndaelCrypt.Decrypt(encryptKey);
                string ftpEncryptKey;
                if (!base.IsExistsFtpUserName(fname, out ftpEncryptKey))
                {
                    return ErrorResult($"参数k值[{encryptKey}]系统中不存在");
                }

                string obDecryptValue = Common.EncryptData(fname, ftpEncryptKey);
                if (encryptValue != obDecryptValue)
                {
                    return ErrorResult($"参数v值[{encryptValue}]数据解密验证失败");
                }

                FtpConfigViewEntity prmsEntity = new FtpConfigViewEntity { FtpUserName = fname };
                List<FtpConfigEntity> list = new ClientUploadService().DownloadFtpConfigList(prmsEntity);

                if (list != null && list.Count > 0)
                {
                    string json = JsonObj<FtpConfigEntity>.ToJson(list);
                    string encryptList = Common.EncryptData(json, ftpEncryptKey);

                    return OkResult("", encryptList);
                }
                else
                {
                    return ErrorResult("系统未找到客户FTP配置数据");
                }
            };

            Post["/taskconfig"] = _ =>
            {
                string encryptKey = HttpContext.Current.Request.Form["k"];
                string encryptValue = HttpContext.Current.Request.Form["v"];

                if (string.IsNullOrEmpty(encryptKey) || string.IsNullOrEmpty(encryptValue))
                {
                    return ErrorResult("参数数据不完整");
                }

                string fname = RijndaelCrypt.Decrypt(encryptKey);
                string ftpEncryptKey;
                if (!base.IsExistsFtpUserName(fname, out ftpEncryptKey))
                {
                    return ErrorResult($"参数k值[{encryptKey}]系统中不存在");
                }

                string obDecryptValue = Common.EncryptData(fname, ftpEncryptKey);
                if (encryptValue != obDecryptValue)
                {
                    return ErrorResult($"参数v值[{encryptValue}]数据解密验证失败");
                }

                TaskEntity prmsEntity = new TaskEntity { FtpUserName = fname };
                List<TaskEntity> list = new ClientUploadService().DownloadTaskList(prmsEntity);

                if (list != null && list.Count > 0)
                {
                    string json = JsonObj<TaskEntity>.ToJson(list);
                    string encryptList = Common.EncryptData(json, ftpEncryptKey);

                    return OkResult("", encryptList);
                }
                else
                {
                    return ErrorResult("系统未找到客户任务配置数据");
                }
            };
        }
    }
}