using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Transfer8Pro.DAO.OB;
using Transfer8Pro.Entity;
using Transfer8Pro.Entity.OB;
using RTXNotifyLib;
using Transfer8Pro.Utils;

namespace Transfer8Pro.Core.Service.OB
{
    public class HeartbeatInfoService
    {
        private HeartbeatInfoDAO dao;
        public HeartbeatInfoService()
        {
            dao = new HeartbeatInfoDAO();
        }

        /// 获取列表
        /// </summary>
        /// <param name="taskEntity"></param>       
        /// <returns></returns>
        public ParamtersForDBPageEntity<FtpHeartbeatViewEntity> GetList(FtpHeartbeatViewEntity taskEntity)
        {
            return dao.GetList(taskEntity);
        }

        /// <summary>
        /// 添加或修改 
        /// </summary>
        /// <param name="heartbeatInfo"></param>
        /// <returns></returns>
        public bool InsertOrUpdate(HeartbeatInfoEntity heartbeatInfo)
        {
            return dao.InsertOrUpdate(heartbeatInfo);
        }

        /// <summary>
        /// RTX通知停止心跳的客户
        /// 数据导出心跳或FTP上传心跳停止30分会通通知
        /// </summary>
        public void RtxNotifyDiedHeartbeat()
        {
            try
            {
                string notifyUsers = ConfigHelper.GetConfig("RtxNotifyUsers");
                if (string.IsNullOrEmpty(notifyUsers))
                {
                    LogUtil.WriteLog("未配置RTX通知接收人");
                    return;
                }

                List<FtpHeartbeatViewEntity> diedHeartbeatList = dao.GetDiedHeartbeatList();
                if (diedHeartbeatList.Count == 0)
                {
                    return;
                }


                List<NotifyInfoEntity> notifyList = new List<NotifyInfoEntity>();
                List<NotifyLogInfoEntity> logList = new List<NotifyLogInfoEntity>();
                StringBuilder sbRtxMessage = new StringBuilder();
                sbRtxMessage.Append($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm")}分，系统检测到{diedHeartbeatList.Count}家书店采集系统心跳异常：");
                sbRtxMessage.AppendLine();

                foreach (FtpHeartbeatViewEntity item in diedHeartbeatList)
                {
                    string heartTypeMsg = string.Empty;
                    if (item.RecentDataStatus == 2)
                    {
                        heartTypeMsg += "数据导出";
                    }

                    if (item.RecentFtpStatus == 2)
                    {
                        if (heartTypeMsg != "")
                        {
                            heartTypeMsg += "和";
                        }
                        heartTypeMsg += "FTP上传";
                    }
                
                    string message = $"客户[{item.SupName}({item.FtpUserName})]，使用的开卷{(item.SystemType == 1 ? "日采集系统" : "传8系统")}，[{heartTypeMsg}]心跳异常";
                    sbRtxMessage.Append($"{diedHeartbeatList.IndexOf(item) + 1}、{message}");
                    sbRtxMessage.AppendLine();

                    notifyList.Add(new NotifyInfoEntity { FtpID = item.FtpID, NotifyStatus = 1 });
                    logList.Add(new NotifyLogInfoEntity { FtpID = item.FtpID, NotifyMessage = message});
                }            

                if (!RTXMsg.SendIMts(notifyUsers, null, null, sbRtxMessage.ToString(), "传8/日采集系统心跳异常"))
                {
                    foreach (var item in notifyList)
                    {
                        item.NotifyStatus = 2;
                    }

                    foreach (var item in logList)
                    {
                        item.NotifyMessage = "RTX通知失败";
                    }
                }


                //保存通知日志
                dao.UpdateHeartbeatStatus(notifyList, logList);
            }
            catch (Exception ex)
            {
                LogUtil.WriteLog(ex);
            }

        }

        /// <summary>
        /// RTX通知任务执行异常的客户
        /// </summary>
        public void RtxNotifyTaskError()
        {
            try
            {
                string notifyUsers = ConfigHelper.GetConfig("RtxNotifyUsers");
                if (string.IsNullOrEmpty(notifyUsers))
                {
                    LogUtil.WriteLog("未配置RTX通知接收人");
                    return;
                }

                List<TaskLogDetailViewEntity> taskErrorList = dao.GetTaskErrorList();
                if (taskErrorList.Count == 0)
                {
                    return;
                }

                StringBuilder sbRtxMessage = new StringBuilder();
                sbRtxMessage.AppendLine();
                sbRtxMessage.Append($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm")}分，系统检测到{taskErrorList.Count}条任务出现异常：");
                sbRtxMessage.AppendLine();
                List<KVEntity<int,int>> saveNotifyMessages = new List<KVEntity<int,int>>();
                foreach (var item in taskErrorList)
                {
                    string errorDesc = "[未查询到数据]";
                    if (!item.ErrorContent.Contains("本次查询总条数为0"))
                    {
                        errorDesc = "[出现异常错误]";
                    }

                    string message = $"客户[{item.SupName}({item.FtpUserName})]，任务[{item.TaskName}]在{item.StartTime.ToString("yyyy-MM-dd HH:mm:ss")}生成本期数据文件时{errorDesc}";
                    sbRtxMessage.Append($"{taskErrorList.IndexOf(item) + 1}、{message}");
                    sbRtxMessage.AppendLine();

                    saveNotifyMessages.Add(new KVEntity<int,int> { T1 = item.TaskLogDetailID, K = message, T2=1 });                   
                }

                if (!RTXMsg.SendIMts(notifyUsers, null, null, sbRtxMessage.ToString(), "传8/日采集系统任务异常"))
                {
                    foreach (var msgitem in saveNotifyMessages)
                    {
                        msgitem.T2 = 2;
                    }
                }

                //保存
                dao.UpdateTaskErrorList(saveNotifyMessages);
            }
            catch (Exception ex)
            {
                LogUtil.WriteLog(ex);
            }        
        }
    }
}
