using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Transfer8Pro.Core.Infrastructure;
using Transfer8Pro.Core.QuartzJobs;
using Transfer8Pro.Core.Service;
using Transfer8Pro.Entity;
using Transfer8Pro.Utils;
using System.Threading;
using Quartz.Impl.Triggers;
using Quartz.Impl.Matchers;

namespace Transfer8Pro.Core
{
    /// <summary>
    /// Quartz基类
    /// </summary>
    public class QuartzBase
    {
        private static IScheduler scheduler;
        private static CancellationTokenSource cts = null;

        private static object lockObj = new object();
        private static object lockObj2 = new object();

        #region 当前任务队列操作
        private static List<TaskEntity> _currentTaskList = new List<TaskEntity>();
        /// <summary>
        /// 获取当前任务队列
        /// </summary>
        /// <returns></returns>
        public static List<TaskEntity> GetCurrentTaskList()
        {
            lock (lockObj)
            {
                List<TaskEntity> list = DeepCopyUtil.DeepCopyByBinary<TaskEntity>(_currentTaskList);
                return list;
            }
        }

        /// <summary>
        /// 添加任务队列
        /// </summary>
        /// <param name="taskEntity"></param>
        public static void AddTask(TaskEntity taskEntity)
        {
            lock (lockObj)
            {
                _currentTaskList.Add(taskEntity);
            }
        }

        /// <summary>
        /// 更新任务队列
        /// </summary>
        /// <param name="taskEntity"></param>
        public static void UpdateTask(TaskEntity taskEntity)
        {
            lock (lockObj)
            {
                int index = _currentTaskList.FindIndex(item => item.TaskID == taskEntity.TaskID);
                if (index >= 0)
                {
                    _currentTaskList[index] = taskEntity;
                }
            }
        }

        /// <summary>
        /// 删除任务队列
        /// </summary>
        /// <param name="taskID"></param>
        public static void DeleteTask(string taskID)
        {
            lock (lockObj)
            {
                TaskEntity task = _currentTaskList.Find(item => item.TaskID == taskID);
                if (task != null)
                {
                    _currentTaskList.Remove(task);
                }
            }
        }

        #endregion

        #region QuartzBase队列操作
        private static List<QuartzJobEntity> _QuartzJobList = new List<QuartzJobEntity>();
        /// <summary>
        /// 获取当前任务队列
        /// </summary>
        /// <returns></returns>
        public static List<QuartzJobEntity> GetQuartzJobList()
        {
            lock (lockObj2)
            {
                List<QuartzJobEntity> list = DeepCopyUtil.DeepCopyByBinary<QuartzJobEntity>(_QuartzJobList);
                return list;
            }
        }

        /// <summary>
        /// 添加任务队列
        /// </summary>
        /// <param name="entity"></param>
        public static void AddQuartzJob(QuartzJobEntity entity)
        {
            lock (lockObj2)
            {
                _QuartzJobList.Add(entity);
            }
        }

        /// <summary>
        /// 更新任务队列
        /// </summary>
        /// <param name="entity"></param>
        public static void UpdateQuartzJob(QuartzJobEntity entity)
        {
            lock (lockObj2)
            {
                int index = _QuartzJobList.FindIndex(item => item.JobKey == entity.JobKey);
                if (index >= 0)
                {
                    _QuartzJobList[index] = entity;
                }
            }
        }

        /// <summary>
        /// 删除任务队列
        /// </summary>
        /// <param name="taskID"></param>
        public static void DeleteQuartzJob(string jobKey)
        {
            lock (lockObj2)
            {
                QuartzJobEntity task = _QuartzJobList.Find(item => item.JobKey == jobKey);
                if (task != null)
                {
                    _QuartzJobList.Remove(task);
                }
            }
        }
        #endregion


        private static bool _EnabledDataExportJob = true;//开启数据导出作业状态 true开启 false暂停
        /// <summary>
        ///  设置数据导出作业状态 true开启 false暂停
        ///  当客户停止数据导出服务 暂停当前运行的所有导出作业  
        ///  当客户开启数据导出服务 重新运行所有暂停作业
        /// </summary>
        /// <param name="status"></param>
        public static bool EnabledDataExportJob
        {
            get
            {
                return _EnabledDataExportJob;
            }

            set
            {
                lock (lockObj)
                {
                    _EnabledDataExportJob = value;
                }
            }
        }

        /// <summary>
        /// 开始任务调度
        /// </summary>
        public static void StartScheduler()
        {
            try
            {
                cts = new CancellationTokenSource();

                if (scheduler != null)
                {
                    scheduler = null;
                }

                InitScheduler();

                if (!scheduler.IsStarted)
                {
                    //添加全局监听
                    scheduler.ListenerManager.AddTriggerListener(new TaskTriggerListener(), GroupMatcher<TriggerKey>.AnyGroup());
                    scheduler.Start();

                    //自动数据导出作业
                    JobService<AutoDataExportJob> autoService = new AutoDataExportJobService(cts.Token);
                    autoService.AddJobToSchedule(scheduler);
                    LogUtil.WriteLog("自动数据导出作业已启动");

                    //自动FTP上传作业任
                    JobService<AutoUploadFtpJob> autoFtpService = new AutoUploadFtpJobService(cts.Token);
                    autoFtpService.AddJobToSchedule(scheduler);
                    LogUtil.WriteLog("自动FTP上传作业已启动");

                    //自动心跳作业
                    JobService<AutoHeartbeatJob> autoHeartService = new AutoHeartbeatJobService(cts.Token);
                    autoHeartService.AddJobToSchedule(scheduler);
                    LogUtil.WriteLog("自动心跳作业已启动");

                    //自动配置数据同步作业
                    JobService<AutoDataSyncJob> autoDataSyncService = new AutoDataSyncJobService(cts.Token);
                    autoDataSyncService.AddJobToSchedule(scheduler);
                    LogUtil.WriteLog("自动配置数据同步作业已启动");

                }
            }
            catch (Exception ex)
            {
                LogUtil.WriteLog($"Quartz任务调度启动失败,异常信息[{ex.Message}][{ex.StackTrace}]");
            }
        }

        /// <summary>
        /// 清除数据 重启服务调用
        /// </summary>
        public static void ClearScheduleData()
        {
            try
            {
                lock (lockObj)
                {
                    if (_currentTaskList != null)
                    {
                        _currentTaskList.Clear();
                    }                  
                }

                lock (lockObj2)
                {
                    if (_QuartzJobList != null)
                    {
                        _QuartzJobList.Clear();
                    }                   
                }
            }
            catch(Exception ex)
            {
                LogUtil.WriteLog($"Quartz清除数据错误，异常信息[{ex.Message}][{ex.StackTrace}]");
            }         
        }


        /// <summary>
        /// 停止任务调度
        /// </summary>
        public static void StopSchedule()
        {
            try
            {
                //判断调度是否已经关闭
                if (!scheduler.IsShutdown)
                {
                    cts.Cancel();
                    //等待任务运行完成
                    scheduler.Shutdown(false);

                    LogUtil.WriteLog("Quartz任务调度停止！");
                }
            }
            catch (Exception ex)
            {
                LogUtil.WriteLog($"Quartz任务调度停止失败,异常信息[{ex.Message}][{ex.StackTrace}]");
            }
        }

        /// <summary>
        /// 作业调度是否运行状态
        /// </summary>
        /// <returns></returns>
        public static bool ScheduleRunning()
        {
            if (scheduler == null)
            {
                return false;
            }

            if (scheduler.IsShutdown)
            {
                return false;
            }

            if (!scheduler.IsStarted)
            {
                return false;
            }
            return true;
        }


        /// <summary>
        /// 初始化任务调度对象
        /// </summary>
        private static void InitScheduler()
        {
            try
            {
                lock (lockObj)
                {
                    if (scheduler == null)
                    {
                        ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
                        scheduler = schedulerFactory.GetScheduler();

                        LogUtil.WriteLog("任务调度初始化成功！");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 暂停任务
        /// </summary>
        /// <param name="jobKey"></param>
        public static void PauseJob(string jobKey)
        {
            try
            {
                JobKey jk = new JobKey(jobKey);
                if (scheduler.CheckExists(jk))
                {
                    scheduler.PauseJob(jk);
                    LogUtil.WriteLog($"任务[{jobKey}]已经暂停");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"任务[{jobKey}]暂停出现错误，错误信息[{ex.Message}]");
            }
        }

        /// <summary>
        /// 恢复运行暂停任务
        /// </summary>
        /// <param name="jobKey"></param>
        public static void ResumeJob(string jobKey)
        {
            try
            {
                JobKey jk = new JobKey(jobKey);
                if (scheduler.CheckExists(jk))
                {
                    scheduler.ResumeJob(jk);
                    LogUtil.WriteLog($"任务[{jobKey}恢复运行]");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"任务[{jobKey}]恢复运行出现错误，错误信息[{ex.Message}]");
            }
        }

        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="jobKey"></param>
        public static void DeleteJob(string jobKey)
        {
            try
            {
                JobKey jk = new JobKey(jobKey);
                if (scheduler.CheckExists(jk))
                {
                    scheduler.DeleteJob(jk);
                    LogUtil.WriteLog($"任务[{jobKey}]删除");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"任务[{jobKey}]删除出现错误，错误信息[{ex.Message}]");
            }
        }

        /// <summary>
        /// 判断作业是否存在
        /// </summary>
        /// <param name="jobKey"></param>
        /// <returns></returns>
        public static bool CheckExistJob(string jobKey)
        {
            JobKey jk = new JobKey(jobKey);
            return scheduler.CheckExists(jk);
        }

        /// <summary>
        /// 调度作业
        /// </summary>
        /// <param name="task"></param>      
        /// <param name="isDeleteOldTask"></param>
        public static void ScheduleJob<T>(TaskEntity task, bool isDeleteOldTask = false) where T : IJob
        {
            try
            {
                if (isDeleteOldTask)
                {
                    DeleteJob(task.TaskID);
                }

                //验证Cron表达式
                if (QuartzHelper.ValidExpression(task.Cron))
                {
                    IJobDetail job = new JobDetailImpl(task.TaskID, typeof(T));
                    //添加任务参数
                    job.JobDataMap.Add("TaskParam", task);
                    job.JobDataMap.Add("CanellationTokenParam", cts.Token);

                    CronTriggerImpl trigger = new CronTriggerImpl();
                    trigger.CronExpressionString = task.Cron;
                    trigger.Name = task.TaskID;
                    trigger.Description = task.TaskName;
                    scheduler.ScheduleJob(job, trigger);

                    if (task.TaskStatus == TaskStatus.STOP)
                    {
                        JobKey jk = new JobKey(task.TaskID);
                        scheduler.PauseJob(jk);
                    }
                    else
                    {
                        List<DateTime> list = QuartzHelper.GetNextFireTime(task.Cron, 5);
                        string msg = string.Join("\r\n", list.Select(item => item.ToString("yyyy-MM-dd HH:mm:ss")));

                        LogUtil.WriteLog($"任务[{task.TaskID}]启动成功,未来5次运行时间如下:\r\n{msg}");
                    }
                }
                else
                {
                    throw new Exception($"任务[{task.TaskID}]，Cron表达式[{task.Cron}]不正确，无法启动任务");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"ScheduleJob()方法出现异常，异常信息[{ex.Message}]");
            }
        }

        /// <summary>
        /// 调度作业
        /// </summary>
        /// <param name="task"></param>      
        /// <param name="isDeleteOldTask"></param>
        public static void ScheduleJob<T>(QuartzJobEntity task, bool isDeleteOldTask = false) where T : IJob
        {
            try
            {
                if (isDeleteOldTask)
                {
                    DeleteJob(task.JobKey);
                }

                //验证Cron表达式
                if (QuartzHelper.ValidExpression(task.Cron))
                {
                    IJobDetail job = new JobDetailImpl(task.JobKey, typeof(T));
                    //添加任务参数
                    job.JobDataMap.Add("TaskParam", task);
                    job.JobDataMap.Add("CanellationTokenParam", cts.Token);

                    CronTriggerImpl trigger = new CronTriggerImpl();
                    trigger.CronExpressionString = task.Cron;
                    trigger.Name = task.JobKey;
                    trigger.Description = task.JobKey;
                    scheduler.ScheduleJob(job, trigger);

                    if (task.TaskStatus == TaskStatus.STOP)
                    {
                        JobKey jk = new JobKey(task.JobKey);
                        scheduler.PauseJob(jk);
                    }
                    else
                    {
                        List<DateTime> list = QuartzHelper.GetNextFireTime(task.Cron, 5);
                        string msg = string.Join("\r\n", list.Select(item => item.ToString("yyyy-MM-dd HH:mm:ss")));

                        LogUtil.WriteLog($"任务[{task.JobKey}]启动成功,未来5次运行时间如下:\r\n{msg}");
                    }
                }
                else
                {
                    throw new Exception($"任务[{task.JobKey}]，Cron表达式[{task.Cron}]不正确，无法启动任务");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"ScheduleJob()方法出现异常，异常信息[{ex.Message}]");
            }
        }
    }
}
