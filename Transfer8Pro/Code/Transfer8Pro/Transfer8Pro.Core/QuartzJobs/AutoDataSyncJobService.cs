using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Quartz;
using Transfer8Pro.Core.Infrastructure;
using Transfer8Pro.Core.Service;
using Transfer8Pro.Entity;
using Transfer8Pro.Entity.OB;
using Transfer8Pro.Utils;

namespace Transfer8Pro.Core.QuartzJobs
{
    /// <summary>
    /// 自动数据同步作业服务
    /// </summary>
    public class AutoDataSyncJobService : JobService<AutoDataSyncJob>
    {
        public override string JobKey => "AutoDataSyncServiceJob";

        protected override string JobName => "自动数据上传作业";

        protected override string GroupName => "自动数据上传作业组";

        protected override CancellationToken CancelToken { get; }

        public AutoDataSyncJobService(CancellationToken ct)
        {
            CancelToken = ct;
        }

        protected override ITrigger GetTrigger()
        {
            try
            {
                ITrigger trigger = null;

                SystemConfigEntity systemConfig = new SystemConfigService().FindSystemConfig((int)SystemConfigs.ConfigSynStatus);
                string cronExp = "0 0/5 * * * ? *";

                if (systemConfig != null && !string.IsNullOrEmpty(systemConfig.Cron))
                {
                    cronExp = systemConfig.Cron;
                }

                if (QuartzHelper.ValidExpression(cronExp))
                {
                    trigger = TriggerBuilder.Create().WithIdentity(JobName, "自动数据上传作业触发器")
                              .WithCronSchedule(cronExp).Build();
                }
                else
                {
                    throw new Exception($"AutoDataSyncJobService.GetTrigger()，配置AutoDataSyncCronExpression的Cron表达式[{cronExp}]语法错误");
                }
                return trigger;
            }
            catch (Exception ex)
            {
                throw new Exception($"AutoDataSyncJobService.GetTrigger()异常，异常信息[{ex.Message}]");
            }
        }
    }

    /// <summary>
    /// 自动数据同步作业
    /// </summary>
    [DisallowConcurrentExecution]
    public class AutoDataSyncJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                CancellationToken ct = (CancellationToken)context.JobDetail.JobDataMap["CanellationTokenParam"];
                ct.ThrowIfCancellationRequested();

                #region 删除历史日志和数据备份文件
                Task.Factory.StartNew(() =>
                {
                    try
                    {
                        DateTime delBaseTime = DateTime.Now.AddMonths(-1);

                        //1删除日志数据(保留近1个月数据)
                        DeleteAllByFloder(AppPath.LogFolder, delBaseTime);
                        //2删除数据备份文件(保留近1个月数据)
                        DeleteAllByFloder(Path.Combine(AppPath.DataFolder, "UploadFileBackup"), delBaseTime);
                        DeleteAllByFloder(Path.Combine(AppPath.DataFolder, "NormalDataFile"), delBaseTime);
                        DeleteAllByFloder(Path.Combine(AppPath.DataFolder, "CompressDataFile"), delBaseTime);
                    }
                    catch (Exception ex)
                    {
                        LogUtil.WriteLog($"删除历史数据异常,异常信息[{ex.Message}][{ex.StackTrace}]");
                    }
                });
                #endregion

                string openbookWebApi = Common.DecryptConfigKey("OpenBookWebAPI");
                if (string.IsNullOrEmpty(openbookWebApi))
                {
                    LogUtil.WriteLog("系统未配置开卷WepApi接口地址");
                    return;
                }

                SystemConfigEntity systemConfigSyn = new SystemConfigService().FindSystemConfig((int)SystemConfigs.ConfigSynStatus);
                if (systemConfigSyn == null)
                {
                    LogUtil.WriteLog("系统未找到配置同步状态记录");
                    return;
                }

                if (systemConfigSyn.Status == 0)
                {
                    return;
                }

                //随机暂停秒数
                Thread.Sleep(new Random().Next(15) * 500);

                FtpService ftpBll = new FtpService();
                TaskService taskBll = new TaskService();
                SystemConfigService systemConfigBll = new SystemConfigService();
                FtpUploadLogService ftpUploadBll = new FtpUploadLogService();
                TaskLogService taskLogBll = new TaskLogService();


                FtpConfigEntity ftpConfig = ftpBll.GetFirstFtpInfo();
                if (ftpConfig == null || string.IsNullOrEmpty(ftpConfig.UserName))
                {
                    LogUtil.WriteLog("系统未配置FTP");
                    return;
                }

                HttpHelper httpHelper = new HttpHelper();

                string url = $"{openbookWebApi.Trim(new char[] { '/', '\\' })}";
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("signature", ApiHelper.GenerateApiSignature());
                string ftpUserName = HttpHelper.UrlEncodeUnicode(RijndaelCrypt.Encrypt(ftpConfig.UserName));

                #region 1配置上传 (FTP配置 系统配置) 
                List<SystemConfigEntity> sysconfigList = systemConfigBll.GetWaitUploadList();
                if (sysconfigList.Count > 0)
                {
                    foreach (var item in sysconfigList)
                    {
                        item.FtpUserName = ftpConfig.UserName;
                    }
                    string apiaddress = $"{url}/configupload/sysconfig";
                    string list = HttpHelper.UrlEncodeUnicode(Common.EncryptData(JsonObj.ToJson(sysconfigList)));
                    dynamic result = JsonObj<dynamic>.FromJson(httpHelper.Post(apiaddress, $"k={ftpUserName}&v={list}", url, Encoding.UTF8, dic));
                    if (result.Status == 1)
                    {
                        List<SystemConfigEntity> uploadConfigList = sysconfigList.Select(item => new SystemConfigEntity { SysConfigID = item.SysConfigID, UploadStatus = 1 }).ToList();
                        systemConfigBll.UpdateUploadStataus(uploadConfigList);
                    }
                    else
                    {
                        LogUtil.WriteLog($"向服务器同步系统配置数据异常，返回错误信息{result.Msg}");
                        List<SystemConfigEntity> uploadConfigList = sysconfigList.Select(item => new SystemConfigEntity { SysConfigID = item.SysConfigID, UploadStatus = 2 }).ToList();
                        systemConfigBll.UpdateUploadStataus(uploadConfigList);
                    }
                }


                List<FtpConfigEntity> ftpConfigUploadList = ftpBll.GetWaitUpload();
                if (ftpConfigUploadList.Count > 0)
                {
                    string apiaddress = $"{url}/configupload/ftpconfig";
                    string ftp = HttpHelper.UrlEncodeUnicode(Common.EncryptData(JsonObj.ToJson(ftpConfigUploadList)));
                    dynamic result = JsonObj<dynamic>.FromJson(httpHelper.Post(apiaddress, $"k={ftpUserName}&v={ftp}", url, Encoding.UTF8, dic));
                    if (result.Status == 1)
                    {
                        List<FtpConfigEntity> ftpList = ftpConfigUploadList.Select(item => new FtpConfigEntity { FtpID = item.FtpID, UploadStatus = 1 }).ToList();
                        ftpBll.UpdateUploadStataus(ftpList);
                    }
                    else
                    {
                        LogUtil.WriteLog($"向服务器同步FTP配置数据异常，返回错误信息{result.Msg}");
                        List<FtpConfigEntity> ftpList = ftpConfigUploadList.Select(item => new FtpConfigEntity { FtpID = item.FtpID, UploadStatus = 2 }).ToList();
                        ftpBll.UpdateUploadStataus(ftpList);
                    }
                }
                #endregion

                #region 2任务上传
                List<TaskEntity> taskList = taskBll.GetWaitUploadList();
                if (taskList.Count > 0)
                {
                    foreach (var item in taskList)
                    {
                        item.FtpUserName = ftpConfig.UserName;
                    }

                    string apiaddress = $"{url}/configupload/taskconfig";
                    string ftp = HttpHelper.UrlEncodeUnicode(Common.EncryptData(JsonObj.ToJson(taskList)));
                    dynamic result = JsonObj<dynamic>.FromJson(httpHelper.Post(apiaddress, $"k={ftpUserName}&v={ftp}", url, Encoding.UTF8, dic));
                    if (result.Status == 1)
                    {
                        List<TaskEntity> uploadTaskList = taskList.Select(item => new TaskEntity { TaskID = item.TaskID, UploadStatus = 1 }).ToList();
                        taskBll.UpdateUploadStataus(uploadTaskList);
                    }
                    else
                    {
                        LogUtil.WriteLog($"向服务器同步任务数据异常，返回错误信息{result.Msg}");
                        List<TaskEntity> uploadTaskList = taskList.Select(item => new TaskEntity { TaskID = item.TaskID, UploadStatus = 2 }).ToList();
                        taskBll.UpdateUploadStataus(uploadTaskList);
                    }
                }
                #endregion

                #region 3日志上传 (任务日志 FTP上传日志)
                List<TaskLogDetailEntity> taskLogList = taskLogBll.GetWaitUploadLogList();
                if (taskLogList.Count > 0)
                {
                    foreach (var item in taskLogList)
                    {
                        item.FtpUserName = ftpConfig.UserName;
                    }
                    string apiaddress = $"{url}/configupload/tasklogdetail";
                    string list = HttpHelper.UrlEncodeUnicode(Common.EncryptData(JsonObj.ToJson(taskLogList)));
                    dynamic result = JsonObj<dynamic>.FromJson(httpHelper.Post(apiaddress, $"k={ftpUserName}&v={list}", url, Encoding.UTF8, dic));
                    if (result.Status == 1)
                    {
                        List<TaskLogDetailEntity> uploadTaskLogList = taskLogList.Select(item => new TaskLogDetailEntity { TaskLogDetailID = item.TaskLogDetailID, UploadStatus = 1 }).ToList();
                        taskLogBll.UpdateUploadStataus(uploadTaskLogList);
                    }
                    else
                    {
                        LogUtil.WriteLog($"向服务器同步任务日志数据异常，返回错误信息{result.Msg}");
                        List<TaskLogDetailEntity> uploadTaskLogList = taskLogList.Select(item => new TaskLogDetailEntity { TaskLogDetailID = item.TaskLogDetailID, UploadStatus = 2 }).ToList();
                        taskLogBll.UpdateUploadStataus(uploadTaskLogList);
                    }
                }

                List<FtpUploadLogEntity> ftpLogList = ftpUploadBll.GetWaitUploadLogList();
                if (ftpLogList.Count > 0)
                {
                    foreach (var item in ftpLogList)
                    {
                        item.FtpUserName = ftpConfig.UserName;
                    }
                    string apiaddress = $"{url}/configupload/ftpuploadlog";
                    string list = HttpHelper.UrlEncodeUnicode(Common.EncryptData(JsonObj.ToJson(ftpLogList)));
                    dynamic result = JsonObj<dynamic>.FromJson(httpHelper.Post(apiaddress, $"k={ftpUserName}&v={list}", url, Encoding.UTF8, dic));
                    if (result.Status == 1)
                    {
                        List<FtpUploadLogEntity> ftpUploadList = ftpLogList.Select(item => new FtpUploadLogEntity { FtpUploadID = item.FtpUploadID, UploadStatus = 1 }).ToList();
                        ftpUploadBll.UpdateUploadStataus(ftpUploadList);
                    }
                    else
                    {
                        LogUtil.WriteLog($"向服务器同步任务日志数据异常，返回错误信息{result.Msg}");
                        List<FtpUploadLogEntity> ftpUploadList = ftpLogList.Select(item => new FtpUploadLogEntity { FtpUploadID = item.FtpUploadID, UploadStatus = 2 }).ToList();
                        ftpUploadBll.UpdateUploadStataus(ftpUploadList);
                    }
                }
                #endregion

                #region 4重新执行一次开卷端设置的任务
                ct.ThrowIfCancellationRequested();
                string manualTaskAddress = $"{url}/configupload/manualtask";
                string callbackManualTaskAddress = $"{url}/configupload/updatemanualtask";

                List<ManualTaskEntity> manualTaskList = null; ;

                try
                {
                    string encryptFtpUserName = HttpHelper.UrlEncodeUnicode(Common.EncryptData(ftpConfig.UserName));
                    dynamic manualTaskResult = JsonObj<dynamic>.FromJson(httpHelper.Post(manualTaskAddress, $"k={ftpUserName}&v={encryptFtpUserName}", url, Encoding.UTF8, dic));
                    if (manualTaskResult.Status == 1)
                    {
                        string encryptData = (string)manualTaskResult.Data;
                        string decryptData = Common.DecryptData(encryptData);
                        manualTaskList = JsonObj<ManualTaskEntity>.FromJson3(decryptData);

                        if (manualTaskList != null && manualTaskList.Count > 0)
                        {
                            List<string> taskIdList = manualTaskList.Select(item => item.TaskID).ToList();
                            List<TaskEntity> executeTaskList = taskBll.GetTaskList(taskIdList);

                            //构造数据文件产品并执行
                            DbFileProductDirector director = new DbFileProductDirector();
                            ADbFileProductBuilder productBuilder = Common.GetDbFileProductBuilder();
                            director.ConstructProduct(productBuilder);
                            DbFileProduct product = productBuilder.GetDbFileProduct();
                            foreach (TaskEntity item in executeTaskList)
                            {
                                product.Execute(item, ct);
                                ManualTaskEntity manualTask = manualTaskList.Find(q => q.TaskID == item.TaskID);
                                if (manualTask != null)
                                {
                                    manualTask.ManualTaskStatus = ManualTaskStatus.已执行;
                                }
                            }

                            //执行完成，回调服务端接口，标记任务状态 
                            string list = HttpHelper.UrlEncodeUnicode(Common.EncryptData(JsonObj.ToJson(manualTaskList)));
                            dynamic callbackManualTaskResult = JsonObj<dynamic>.FromJson(httpHelper.Post(callbackManualTaskAddress, $"k={ftpUserName}&v={list}", url, Encoding.UTF8, dic));
                            if (callbackManualTaskResult.Status != 1)
                            {
                                LogUtil.WriteLog($"向服务器标记手动执行任务状态异常，返回错误信息{callbackManualTaskResult.Msg}");
                            }
                        }
                    }
                    else
                    {
                        LogUtil.WriteLog($"获取手动执行任务接口数据异常,返回错误信息{manualTaskResult.Msg}");
                    }
                }
                catch (Exception ex)
                {
                    LogUtil.WriteLog($"重新执行一次开卷端设置的任务异常，异常信息:[{ex.Message}][{ex.StackTrace}]");
                    if (manualTaskList != null && manualTaskList.Count > 0)
                    {
                        foreach (ManualTaskEntity item in manualTaskList)
                        {
                            if (item.ManualTaskStatus == ManualTaskStatus.已创建)
                            {
                                item.ManualTaskStatus = ManualTaskStatus.执行失败;
                            }
                        }
                        //执行完成，回调服务端接口，标记任务状态 
                        string list = HttpHelper.UrlEncodeUnicode(Common.EncryptData(JsonObj.ToJson(manualTaskList)));
                        dynamic callbackManualTaskResult = JsonObj<dynamic>.FromJson(httpHelper.Post(callbackManualTaskAddress, $"k={ftpUserName}&v={list}", url, Encoding.UTF8, dic));
                        if (callbackManualTaskResult.Status != 1)
                        {
                            LogUtil.WriteLog($"重新执行一次开卷端设置的任务出现异常后，向服务器标记手动执行任务状态异常，返回错误信息{callbackManualTaskResult.Msg}");
                        }
                    }
                }
                #endregion

            }
            catch (Exception ex)
            {
                LogUtil.WriteLog($"AutoDataSyncJob自动数据同步作业异常,异常信息[{ex.Message}][{ex.StackTrace}]");
            }
        }

        private void DeleteAllByFloder(string loaderPath, DateTime delBaseTime)
        {
            if (!Directory.Exists(loaderPath))
            {
                return;
            }

            string[] filePaths = Directory.GetFileSystemEntries(loaderPath);
            foreach (string path in filePaths)
            {
                if (Directory.Exists(path))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(path);
                    if (directoryInfo.CreationTime <= delBaseTime)
                    {
                        Directory.Delete(path, true);
                    }
                }
                else if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
        }
    }
}
