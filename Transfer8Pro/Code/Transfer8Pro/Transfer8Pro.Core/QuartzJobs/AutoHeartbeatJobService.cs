using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Transfer8Pro.Entity;
using Transfer8Pro.Core.Service;
using Transfer8Pro.Utils;
using Transfer8Pro.Core.Infrastructure;
using System.IO;

namespace Transfer8Pro.Core.QuartzJobs
{
    /// <summary>
    /// 心跳服务
    /// </summary>
    public class AutoHeartbeatJobService : JobService<AutoHeartbeatJob>
    {
        public override string JobKey => "AutoHeartbeatServiceJob";

        protected override string JobName => "自动心跳作业";

        protected override string GroupName => "自动心跳作业组";

        protected override CancellationToken CancelToken
        {
            get;
        }

        public AutoHeartbeatJobService(CancellationToken ct)
        {
            CancelToken = ct;
        }

        protected override ITrigger GetTrigger()
        {
            try
            {
                ITrigger trigger = null;
                string cronExp = ConfigHelper.GetConfig("AutoHeartbeatCron", "0 0/5 * * * ? *"); //如果配置没有配置，自动数据作业则默认1分自动执行一次               


                if (QuartzHelper.ValidExpression(cronExp))
                {
                    trigger = TriggerBuilder.Create().WithIdentity(JobName, "自动心跳任务作业触发器")
                              .WithCronSchedule(cronExp).Build();
                }
                else
                {
                    throw new Exception($"执行自动心跳处理服务AutoHeartbeatJobService.GetTrigger()，配置AutoJobCronExpression的Cron表达式[{cronExp}]语法错误");
                }
                return trigger;
            }
            catch (Exception ex)
            {
                throw new Exception($"自动作业处理服务AutoHeartbeatJobService.GetTrigger()异常，异常信息[{ex.Message}]");
            }
        }
    }

    /// <summary>
    /// 自动数据导出心跳
    /// </summary>
    [DisallowConcurrentExecution]
    public class AutoHeartbeatJob : IJob
    {
        private static SystemConfigService systemConfigBll = null;
        static AutoHeartbeatJob()
        {
            systemConfigBll = new SystemConfigService();
        }
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                CancellationToken ct = (CancellationToken)context.JobDetail.JobDataMap["CanellationTokenParam"];
                ct.ThrowIfCancellationRequested();

                //作业名称
                string jobKeyParam = context.JobDetail.JobDataMap["JobKeyParam"].ToString();

                string dataJobKey = "DataExportHeartbeat30";
                string ftpJobKey = "UploadHeartbeat30";

                //获取心跳服务配置
                SystemConfigEntity systemConfig = systemConfigBll.FindSystemConfig((int)SystemConfigs.HeartbeatService);
                List<QuartzJobEntity> list = QuartzBase.GetQuartzJobList();
                QuartzJobEntity quartzJobEntity = null;

                if (systemConfig.Status == 1)
                {
                    #region 添加
                    //添加数据导出心跳作业
                    quartzJobEntity = list.Find(item => item.JobKey == dataJobKey);
                    if (quartzJobEntity == null)
                    {
                        quartzJobEntity = new QuartzJobEntity();
                        quartzJobEntity.JobKey = dataJobKey;
                        quartzJobEntity.Cron = systemConfig.Cron;
                        quartzJobEntity.TaskStatus = TaskStatus.RUN;
                        quartzJobEntity.UniqueCode = systemConfig.UnqiueID();
                        QuartzBase.ScheduleJob<AutoDataExportHeartbeatJob>(quartzJobEntity);
                        QuartzBase.AddQuartzJob(quartzJobEntity);
                    }

                    //添加FTP上传心跳作业
                    quartzJobEntity = list.Find(item => item.JobKey == ftpJobKey);
                    if (quartzJobEntity == null)
                    {
                        quartzJobEntity = new QuartzJobEntity();
                        quartzJobEntity.JobKey = ftpJobKey;
                        quartzJobEntity.Cron = systemConfig.Cron;
                        quartzJobEntity.TaskStatus = TaskStatus.RUN;
                        quartzJobEntity.UniqueCode = systemConfig.UnqiueID();
                        QuartzBase.ScheduleJob<AutoFtpUploadHeartbeatJob>(quartzJobEntity);
                        QuartzBase.AddQuartzJob(quartzJobEntity);
                    }
                    #endregion

                    #region 内容修改
                    list = QuartzBase.GetQuartzJobList();
                    foreach (QuartzJobEntity item in list)
                    {
                        if (item.JobKey == dataJobKey && item.UniqueCode != systemConfig.UnqiueID())
                        {
                            item.TaskStatus = TaskStatus.RUN;
                            item.Cron = systemConfig.Cron;
                            item.UniqueCode = systemConfig.UnqiueID();
                            QuartzBase.ScheduleJob<AutoDataExportHeartbeatJob>(item, true);
                            QuartzBase.UpdateQuartzJob(item);
                        }

                        if (item.JobKey == ftpJobKey && item.UniqueCode != systemConfig.UnqiueID())
                        {
                            item.TaskStatus = TaskStatus.RUN;
                            item.Cron = systemConfig.Cron;
                            item.UniqueCode = systemConfig.UnqiueID();
                            QuartzBase.ScheduleJob<AutoFtpUploadHeartbeatJob>(item, true);
                            QuartzBase.UpdateQuartzJob(item);
                        }
                    }
                    #endregion

                }
                else
                {
                    foreach (QuartzJobEntity item in list)
                    {
                        if (item.JobKey == dataJobKey && item.TaskStatus != TaskStatus.STOP)
                        {
                            item.TaskStatus = TaskStatus.STOP;
                            QuartzBase.PauseJob(item.JobKey);
                            QuartzBase.UpdateQuartzJob(item);
                        }

                        if (item.JobKey == ftpJobKey && item.TaskStatus != TaskStatus.STOP)
                        {
                            item.TaskStatus = TaskStatus.STOP;
                            QuartzBase.PauseJob(item.JobKey);
                            QuartzBase.UpdateQuartzJob(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtil.WriteLog($"AutoHeartbeatJob自动心跳作业异常,异常信息[{ex.Message}][{ex.StackTrace}]");
            }
        }
    }

    /// <summary>
    /// 自动数据导出心跳作业
    /// </summary>

    [DisallowConcurrentExecution]
    public class AutoDataExportHeartbeatJob : IJob
    {
        private static FtpService ftpBll = null;
        private static SystemConfigService systemConfigBll = null;

        static AutoDataExportHeartbeatJob()
        {
            ftpBll = new FtpService();
            systemConfigBll = new SystemConfigService();
        }
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                //数据服务停止，则心跳不输出
                var dataService = systemConfigBll.FindSystemConfig((int)SystemConfigs.DataExportService);
                if (dataService == null || dataService.Status == 0)
                {
                    return;
                }

                FtpConfigEntity ftpConfig = ftpBll.GetFirstFtpInfo();
                if (ftpConfig == null || string.IsNullOrEmpty(ftpConfig.ExportFileDirectory))
                {
                    LogUtil.WriteLog("系统未配置FTP导出文件目录 ");
                    return;
                }

                string heartbeatfile = Path.Combine(ftpConfig.ExportFileDirectory, "DataExportHeartbeat.stamp");

                //15秒文件还是被占用，放弃本次写入时间戳
                for (int i = 0; i < 30; i++)
                {
                    //判断文件文件是否被占用
                    if (!FileHelper.CheckFileInUse(heartbeatfile))
                    {
                        try
                        {
                            string content = Common.EncryptData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            byte[] bytes = Encoding.UTF8.GetBytes(content);
                            using (FileStream fs = new FileStream(heartbeatfile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None))
                            {
                                fs.Write(bytes, 0, bytes.Length);
                                fs.Flush();
                            }
                            //LogUtil.WriteLog($"数据导出作业心跳已写入：{content}");
                            break;
                        }
                        catch (Exception ex)
                        {
                            LogUtil.WriteLog($"向文件[{heartbeatfile}]写入时间戳异常,异常信息[{ex.Message}][{ex.StackTrace}]");
                        }
                    }
                    else
                    {
                        Thread.Sleep(500);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtil.WriteLog($"AutoDataExportHeartbeatJob自动数据导出心跳作业异常,异常信息[{ex.Message}][{ex.StackTrace}]");
            }
        }
    }

    /// <summary>
    /// 自动FTP上传心跳作业
    /// </summary>
    [DisallowConcurrentExecution]
    public class AutoFtpUploadHeartbeatJob : IJob
    {

        private static HttpHelper httpHelper = null;
        private static SystemConfigService systemConfigBll = null;

        //private static SystemConfigEntity systemConfig = null;
        private static int openbookSysType = 0;
        //private static SystemConfigEntity systemConfigHeart = null;
        string openbookWebApi = Common.DecryptConfigKey("OpenBookWebAPI");

        static AutoFtpUploadHeartbeatJob()
        {           
            httpHelper = new HttpHelper();
            systemConfigBll = new SystemConfigService();           
            //systemConfigHeart = systemConfigBll.FindSystemConfig((int)SystemConfigs.HeartbeatService);
            openbookSysType=Common.DecryptConfigKey("OpenBookSystemType").ToInt();
        }

        public void Execute(IJobExecutionContext context)
        {
            try
            {
                SystemConfigEntity ftpServiceEntity = systemConfigBll.FindSystemConfig((int)SystemConfigs.FtpUpoladService);
                if (ftpServiceEntity == null || ftpServiceEntity.Status == 0)
                {
                    return;
                }

                FtpConfigEntity ftpConfig = new FtpService().GetFirstFtpInfo();             
               
                if (ftpConfig == null)
                {
                    LogUtil.WriteLog("未配置FTP信息");
                    return;
                }

                if (string.IsNullOrEmpty(ftpConfig.ExportFileDirectory))
                {
                    LogUtil.WriteLog("FTP导出文件目录未配置");
                    return;
                }                
             

                if (openbookSysType == 0)
                {
                    LogUtil.WriteLog("系统未配置系统类型");
                    return;
                }

                if (string.IsNullOrEmpty(openbookWebApi))
                {
                    LogUtil.WriteLog("系统未配置开卷接口地址");
                    return;
                }


                Thread.Sleep(1000);
                string heartbeatfile = Path.Combine(ftpConfig.ExportFileDirectory, "DataExportHeartbeat.stamp");

                string encryptstr;
                DateTime dataExportTimestamp;
                if (GetDataExportTime(heartbeatfile, out dataExportTimestamp))
                {
                    //拿到数据导出心跳时间戳
                    encryptstr = Common.EncryptData(dataExportTimestamp.ToString() + "_" + DateTime.Now.ToString());
                }
                else
                {
                    //未拿到数据导出心跳时间戳
                    encryptstr = Common.EncryptData(DateTime.MinValue.ToString() + "_" + DateTime.Now.ToString());
                }

                //把心跳时间戳上传到开卷接口
                string url = $"{openbookWebApi.Trim(new char[] { '/', '\\' })}/heartbeat/poll";
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("signature", ApiHelper.GenerateApiSignature());

                encryptstr = HttpHelper.UrlEncodeUnicode(encryptstr);
                string ftpUserName = HttpHelper.UrlEncodeUnicode(RijndaelCrypt.Encrypt(ftpConfig.UserName));
                string systemtype = HttpHelper.UrlEncodeUnicode(RijndaelCrypt.Encrypt(openbookSysType.ToString()));

                //随机暂停秒数
                Thread.Sleep(new Random().Next(15) * 500);
                dynamic result = JsonObj<dynamic>.FromJson(httpHelper.Post(url, $"fname={ftpUserName}&encryptstr={encryptstr}&systemtype={systemtype}", url, Encoding.UTF8, dic));
                if (result.Status != 1)
                {
                    LogUtil.WriteLog($"向服务器同步心跳数据异常，返回错误信息{result.Msg}");
                }

            }
            catch (Exception ex)
            {
                LogUtil.WriteLog($"AutoFtpUploadHeartbeatJob自动Ftp上传心跳作业异常,异常信息[{ex.Message}][{ex.StackTrace}]");
            }
        }

        /// <summary>
        /// 获取数据导出时间戳
        /// </summary>
        /// <param name="heartbeatfile"></param>
        /// <param name="dataExportTime"></param>
        /// <returns></returns>
        private bool GetDataExportTime(string heartbeatfile, out DateTime dataExportTime)
        {
            dataExportTime = DateTime.MinValue;
            bool result = false;
            try
            {
                string content = "";
                //15秒文件还是被占用，放弃本次写入时间戳
                for (int i = 0; i < 30; i++)
                {
                    //判断文件文件是否被占用
                    if (!FileHelper.CheckFileInUse(heartbeatfile))
                    {
                        try
                        {
                            using (FileStream fs = new FileStream(heartbeatfile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None))
                            {
                                int fslen = (int)fs.Length;
                                byte[] bytes = new byte[fslen];
                                fs.Read(bytes, 0, bytes.Length);

                                content = Encoding.UTF8.GetString(bytes);
                            }

                            content = !string.IsNullOrEmpty(content) ? Common.DecryptData(content) : "";
                            //LogUtil.WriteLog($"拿到数据导出心跳时间:{content}");

                            try
                            {
                                File.Delete(heartbeatfile);
                            }
                            catch (Exception ex)
                            {
                                LogUtil.WriteLog($"已拿到数据导出心跳时间，但删除心跳文件时异常（并不影响程序流程），异常信息[{ex.Message}][{ex.StackTrace}]");
                            }
                            break;
                        }
                        catch (Exception ex)
                        {
                            LogUtil.WriteLog($"向文件[{heartbeatfile}]写入时间戳异常,异常信息[{ex.Message}][{ex.StackTrace}]");
                        }
                    }
                    else
                    {
                        Thread.Sleep(500);
                    }
                }


                if (DateTime.TryParse(content, out dataExportTime))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
    }

}
