using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Transfer8Pro.Core.Infrastructure;
using Transfer8Pro.DAO.OB;
using Transfer8Pro.Entity;

namespace Transfer8Pro.Core.Service.OB
{
    public class ClientUploadService
    {
        private readonly ClientUploadDAO dao = null;
        public ClientUploadService()
        {
            dao = new ClientUploadDAO();
        }

        /// <summary>
        /// 保存客户系统配置
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool SaveSysConfig(List<SystemConfigEntity> list)
        {
            return dao.SaveSysConfig(list);
        }

        /// <summary>
        /// 保存FTP配置
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool SaveFtpConfig(List<FtpConfigEntity> list)
        {
            return dao.SaveFtpConfig(list);
        }


        /// <summary>
        /// 保存任务
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool SaveTask(List<TaskEntity> list)
        {
            return dao.SaveTask(list);
        }

        /// <summary>
        /// 保存任务日志明细
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool SaveTaskLogDetail(List<TaskLogDetailEntity> list)
        {
            return dao.SaveTaskLogDetail(list);
        }

        /// <summary>
        /// 保存FTP上传日志
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool SaveFtpUploadLog(List<FtpUploadLogEntity> list)
        {
            return dao.SaveFtpUploadLog(list);
        }

        /// <summary>
        /// 获取系统配置列表
        /// </summary>
        /// <param name="taskEntity"></param>      
        /// <returns></returns>
        public ParamtersForDBPageEntity<SystemConfigViewEntity> GetSystemConfigList(SystemConfigViewEntity taskEntity)
        {
            return dao.GetSystemConfigList(taskEntity);
        }

        /// <summary>
        /// 获取FTP配置列表
        /// </summary>
        /// <param name="taskEntity"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSizent"></param>
        /// <returns></returns>
        public ParamtersForDBPageEntity<FtpConfigViewEntity> GetFtpConfigList(FtpConfigViewEntity taskEntity)
        {
            ParamtersForDBPageEntity<FtpConfigViewEntity> ftpConfig = dao.GetFtpConfigList(taskEntity);
            if (ftpConfig != null)
            {
                 foreach (FtpConfigViewEntity item in ftpConfig.DataList)
                {
                    item.UserPassword = Common.DecryptData(item.UserPassword, item.FtpEncryptKey);
                }
            }

            return ftpConfig;
        }

        /// <summary>
        /// 获取任务配置列表
        /// </summary>
        /// <param name="taskEntity"></param>      
        /// <returns></returns>
        public ParamtersForDBPageEntity<TaskViewEntity> GetTaskList(TaskViewEntity taskEntity)
        {
            return dao.GetTaskList(taskEntity);
        }

        /// <summary>
        /// 获取任务
        /// </summary>
        /// <param name="taskEntity"></param>
        /// <returns></returns>
        public TaskViewEntity GetTask(TaskViewEntity taskEntity)
        {
            TaskViewEntity task = dao.GetTask(taskEntity);
            if (task != null)
            {
                if (!string.IsNullOrEmpty(task.DBConnectString_Hashed))
                {
                    task.DBConnectString_Hashed = Common.DecryptData(task.DBConnectString_Hashed, task.FtpEncryptKey);
                }

                if (!string.IsNullOrEmpty(task.Cron))
                {
                    task.CronDesc = QuartzHelper.GenerateCronDesc(task.Cron);                  
                }
            }
            return task;
        }     

        /// <summary>
        /// 获取任务日志列表
        /// </summary>
        /// <param name="taskEntity"></param>      
        /// <returns></returns>
        public ParamtersForDBPageEntity<TaskLogDetailViewEntity> GetTaskLogDetailList(TaskLogDetailViewEntity taskEntity)
        {
            return dao.GetTaskLogDetailList(taskEntity);
        }

        /// <summary>
        /// 获取FTP任务日志列表
        /// </summary>
        /// <param name="taskEntity"></param>      
        /// <returns></returns>
        public ParamtersForDBPageEntity<FtpUploadLogViewEntity> GetFtpUploadLogList(FtpUploadLogViewEntity taskEntity)
        {
            return dao.GetFtpUploadLogList(taskEntity);
        }

        /// <summary>
        /// 下载客户系统配置列表
        /// </summary>
        /// <param name="taskEntity"></param>      
        /// <returns></returns>
        public List<SystemConfigEntity> DownloadSystemConfigList(SystemConfigEntity systemConfigEntity)
        {
            return dao.DownloadSystemConfigList(systemConfigEntity);
        }

        /// <summary>
        /// 下载客户FTP配置信息
        /// </summary>
        /// <param name="ftpConfigViewEntity"></param>
        /// <returns></returns>
        public List<FtpConfigEntity> DownloadFtpConfigList(FtpConfigViewEntity ftpConfigViewEntity)
        {
            return dao.DownloadFtpConfigList(ftpConfigViewEntity);
        }

        /// <summary>
        ///  下载客户任务配置信息
        /// </summary>
        /// <param name="taskEntity"></param>
        /// <returns></returns>
        public List<TaskEntity> DownloadTaskList(TaskEntity taskEntity)
        {
            return dao.DownloadTaskList(taskEntity);
        }

        /// <summary>
        /// 检测客户是否上传配置数据
        /// </summary>
        /// <param name="ftpUserName"></param>
        /// <returns></returns>
        public bool CheckUploadConfig(string ftpUserName)
        {
            return dao.CheckUploadConfig(ftpUserName);
        }
    }
}
