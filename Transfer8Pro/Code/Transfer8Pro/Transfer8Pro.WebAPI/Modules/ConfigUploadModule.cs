using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Transfer8Pro.Core;
using Transfer8Pro.Core.Service.OB;
using Transfer8Pro.Entity;
using Transfer8Pro.Entity.OB;
using Transfer8Pro.Utils;

namespace Transfer8Pro.WebAPI.Modules
{
    public class ConfigUploadModule : BaseModule
    {
        public ConfigUploadModule() : base("configupload")
        {
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
                if (!base.IsExistsFtpUserName(fname,out ftpEncryptKey))
                {
                    return ErrorResult($"参数k值[{encryptKey}]系统中不存在");
                }
               
                string decryptList = Common.DecryptData(encryptValue, ftpEncryptKey);
                List<SystemConfigEntity> list = JsonObj.FromJson(decryptList, typeof(List<SystemConfigEntity>)) as List<SystemConfigEntity>;

                bool isSuccess = new ClientUploadService().SaveSysConfig(list);
                if (isSuccess)
                {
                    return OkResult();
                }
                else
                {
                    return ErrorResult("保存失败");
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

                string decryptValue = Common.DecryptData(encryptValue, ftpEncryptKey);
                List<FtpConfigEntity> list = JsonObj.FromJson(decryptValue,typeof(List<FtpConfigEntity>)) as List<FtpConfigEntity>;
                bool isSuccess = new ClientUploadService().SaveFtpConfig(list);

                if (isSuccess)
                {
                    return OkResult();
                }
                else
                {
                    return ErrorResult("保存失败");
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

                string decryptValue = Common.DecryptData(encryptValue, ftpEncryptKey);
                List<TaskEntity> list = JsonObj.FromJson(decryptValue, typeof(List<TaskEntity>)) as List<TaskEntity>;
                bool isSuccess = new ClientUploadService().SaveTask(list);
                if (isSuccess)
                {
                    return OkResult();
                }
                else
                {
                    return ErrorResult("保存失败");
                }
            };

            Post["/tasklogdetail"] = _ =>
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

                string decryptValue = Common.DecryptData(encryptValue, ftpEncryptKey);
                List<TaskLogDetailEntity> list = JsonObj.FromJson(decryptValue, typeof(List<TaskLogDetailEntity>)) as List<TaskLogDetailEntity>;
                bool isSuccess = new ClientUploadService().SaveTaskLogDetail(list);
                if (isSuccess)
                {
                    return OkResult();
                }
                else
                {
                    return ErrorResult("保存失败");
                }
            };

            Post["/ftpuploadlog"] = _ =>
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

                string decryptValue = Common.DecryptData(encryptValue, ftpEncryptKey);
                List<FtpUploadLogEntity> list = JsonObj.FromJson(decryptValue, typeof(List<FtpUploadLogEntity>)) as List<FtpUploadLogEntity>;
                bool isSuccess = new ClientUploadService().SaveFtpUploadLog(list);
                if (isSuccess)
                {
                    return OkResult();
                }
                else
                {
                    return ErrorResult("保存失败");
                }
            };

            //检测开卷端设置重新执行一次的任务
            Post["/manualtask"] = _ =>
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

                ManualTaskEntity prmsEntity = new ManualTaskEntity { FtpUserName = fname };
                List<ManualTaskEntity> taskList = new ManualTaskService().GetWaitingManualTaskList(prmsEntity);
                string encryptData = Common.EncryptData(JsonObj.ToJson(taskList), ftpEncryptKey);
                return OkResult("",encryptData);
            };

            //客户端更新执行手动任务结果
            Post["/updatemanualtask"] = _ =>
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

                string decryptList = Common.DecryptData(encryptValue, ftpEncryptKey);
                List<ManualTaskEntity> list = JsonObj.FromJson(decryptList, typeof(List<ManualTaskEntity>)) as List<ManualTaskEntity>;
                if (list != null && list.Count > 0)
                {
                    bool isSuccess = new ManualTaskService().UpdateStatus(list);
                    if (isSuccess)
                    {
                        return OkResult();
                    }
                    else
                    {
                        return ErrorResult("更新任务状态异常");
                    }
                }
                else
                {
                    return ErrorResult($"参数v值[{encryptValue}]数据为空");
                }               
            };
        }
    }
}