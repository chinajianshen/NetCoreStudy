using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Quartz;
using Transfer8Pro.Core.Infrastructure;
using Transfer8Pro.Core.Service;
using Transfer8Pro.Entity;
using Transfer8Pro.Utils;

namespace Transfer8Pro.Core.QuartzJobs
{
    /// <summary>
    /// 自动数据导出服务
    /// </summary>
    public class AutoDataExportJobService : JobService<AutoDataExportJob>
    {
        protected override string JobName => "自动添加传8任务作业";

        protected override string GroupName => "自动添加传8任务作业组";

        protected override CancellationToken CancelToken { get;}      

        public override string JobKey
        {
            get
            {
                return "T8_AutoDataExportJob";
            }
        }

        public AutoDataExportJobService(CancellationToken ct)
        {
            CancelToken = ct;
        }

        protected override ITrigger GetTrigger()
        {
            try
            {
                ITrigger trigger = null;              
                string cronExp = "0/30 * * * * ? *"; //如果配置没有配置，自动数据作业则默认30秒自动执行一次
                SystemConfigEntity systemConfig = new SystemConfigService().FindSystemConfig((int)SystemConfigs.DataExportService);
                if (systemConfig != null && !string.IsNullOrEmpty(systemConfig.Cron))
                {
                    cronExp = systemConfig.Cron;
                }

                if (QuartzHelper.ValidExpression(cronExp))
                {
                    trigger = TriggerBuilder.Create().WithIdentity(JobName, "自动传8任务作业触发器")
                              .WithCronSchedule(cronExp).Build();
                }
                else
                {
                    throw new Exception($"执行自动作业处理服务AutoAddJobService.GetTrigger()，配置AutoJobCronExpression的Cron表达式[{cronExp}]语法错误");
                }
                return trigger;
            }
            catch (Exception ex)
            {
                throw new Exception($"自动作业处理服务AutoAddJobService.GetTrigger()异常，异常信息[{ex.Message}]");
            }
        }
    }
}
