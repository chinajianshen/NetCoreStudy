using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Transfer8Pro.Entity.OB;
using Dapper;
using Transfer8Pro.Entity;
using System.Data;

namespace Transfer8Pro.DAO.OB
{
    public class HeartbeatInfoDAO : DAOBase<SqlConnection>
    {
        public HeartbeatInfoDAO()
        {
            base._default_connect_str = base.GetSqlServerConnectString();
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="taskEntity"></param>       
        /// <returns></returns>
        public ParamtersForDBPageEntity<FtpHeartbeatViewEntity> GetList(FtpHeartbeatViewEntity taskEntity)
        {
            DynamicParameters prms = new DynamicParameters();
            prms.Add("@TableName", "T8_V_FtpHeartbeatList");
            prms.Add("@Fields", "*");
            prms.Add("@OrderField", "ModifyTime DESC,CreateTime DESC");
            prms.Add("@pageSize", taskEntity.PageSize);
            prms.Add("@pageIndex", taskEntity.PageIndex);
            prms.Add("@Records", "", DbType.Int32, ParameterDirection.Output);

            StringBuilder sbWhere = new StringBuilder(" 1=1 ");
            if (taskEntity != null)
            {
                if (!string.IsNullOrEmpty(taskEntity.FtpUserName))
                {
                    sbWhere.Append($" AND FtpUserName like '%{taskEntity.FtpUserName}%'");
                }
                if (taskEntity.SystemType > 0)
                {
                    sbWhere.Append($" AND SystemType={taskEntity.SystemType}");
                }


                if (taskEntity.HeartbeatStatus == 1)
                {
                    sbWhere.Append($" AND (RecentDataStatus=1 AND RecentFtpStatus=1)");
                }
                else if (taskEntity.HeartbeatStatus == 2)
                {
                    sbWhere.Append($" AND (RecentDataStatus=2 OR RecentFtpStatus=2)");
                }

            }
            prms.Add("@sqlWhere", sbWhere.ToString());

            return base.ExecuteFor(conn =>
            {
                List<FtpHeartbeatViewEntity> list = conn.Query<FtpHeartbeatViewEntity>("sp_pager05", prms, null, true, null, CommandType.StoredProcedure).ToList();
                return new ParamtersForDBPageEntity<FtpHeartbeatViewEntity>()
                {
                    PageIndex = taskEntity.PageIndex,
                    PageSize = taskEntity.PageSize,
                    Total = prms.Get<int>("@Records"),
                    DataList = list
                };
            });

        }

        /// <summary>
        /// 获取心跳停止的所有记录
        /// </summary>
        /// <returns></returns>
        public List<FtpHeartbeatViewEntity> GetDiedHeartbeatList()
        {
            //            string delSQL = "DELETE FROM T8_NotifyInfo WHERE CONVERT(VARCHAR(10),NotifyTime,23) != CONVERT(VARCHAR(10),GETDATE(),23)";
            //            string sql = @"SELECT a.* 
            //FROM dbo.T8_V_FtpHeartbeatList a
            //LEFT JOIN dbo.T8_NotifyInfo b ON b.FtpID = a.FtpID
            //WHERE (RecentDataStatus=2 OR RecentFtpStatus=2) AND ISNULL(b.NotifyStatus,0)  != 1";

            return base.ExecuteFor(conn =>
            {
                //conn.Execute(delSQL);
                return conn.Query<FtpHeartbeatViewEntity>("T8_Proc_GetDiedHeartbeatList", null, null, false, null, CommandType.StoredProcedure).ToList();
            });
        }

        /// <summary>
        /// 保存通知记录
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool UpdateHeartbeatStatus(List<NotifyInfoEntity> notifyList, List<NotifyLogInfoEntity> logList)
        {
            string sql = "INSERT INTO T8_NotifyInfo(FtpID,NotifyStatus) VALUES(@FtpID,@NotifyStatus)";
            string sql2 = "INSERT INTO T8_NotifyLogInfo(FtpID,NotifyMessage)VALUES(@FtpID,@NotifyMessage)";
            return base.ExecuteForWithTrans(trans =>
            {
                trans.Connection.Execute(sql, notifyList, trans);
                trans.Connection.Execute(sql2, logList, trans);
                return true;
            });
        }

        /// <summary>
        /// 添加或修改 
        /// </summary>
        /// <param name="heartbeatInfo"></param>
        /// <returns></returns>
        public bool InsertOrUpdate(HeartbeatInfoEntity heartbeatInfo)
        {
            string updateSQL = "UPDATE T8_HeartbeatInfo SET SystemType=@SystemType,RecentDataHeartbeatTime=@RecentDataHeartbeatTime,RecentFtpHeartbeatTime=@RecentFtpHeartbeatTime,ModifyTime=@ModifyTime WHERE FtpID=@FtpID";
            string insertSQL = @"INSERT INTO T8_HeartbeatInfo(FtpID,SystemType,RecentDataHeartbeatTime,RecentFtpHeartbeatTime,ModifyTime)VALUES(
@FtpID,@SystemType,@RecentDataHeartbeatTime,@RecentFtpHeartbeatTime,@ModifyTime)";
            string selectSQL = "SELECT * FROM T8_HeartbeatInfo WHERE FtpID=@FtpID";

            return base.ExecuteFor(conn =>
            {
                DynamicParameters prms = new DynamicParameters();
                prms.Add("@FtpID", heartbeatInfo.FtpID);
                prms.Add("@SystemType", heartbeatInfo.SystemType);
                prms.Add("@RecentFtpHeartbeatTime", heartbeatInfo.RecentFtpHeartbeatTime);
                prms.Add("@ModifyTime", DateTime.Now);

                HeartbeatInfoEntity orgHeartbeatInfo = conn.Query<HeartbeatInfoEntity>(selectSQL, prms).FirstOrDefault();
                if (orgHeartbeatInfo == null)
                {
                    //添加   
                    prms.Add("@RecentDataHeartbeatTime", heartbeatInfo.RecentDataHeartbeatTime);

                    return conn.Execute(insertSQL, prms) > 0;
                }
                else
                {
                    //修改
                    if (!heartbeatInfo.RecentDataHeartbeatTime.HasValue)
                    {
                        prms.Add("@RecentDataHeartbeatTime", orgHeartbeatInfo.RecentDataHeartbeatTime);
                    }
                    else
                    {
                        prms.Add("@RecentDataHeartbeatTime", heartbeatInfo.RecentDataHeartbeatTime);
                    }

                    prms.Add("@ModifyTime", DateTime.Now);
                    return conn.Execute(updateSQL, prms) > 0;
                }
            });
        }

        /// <summary>
        /// 获取任务异常记录
        /// </summary>
        /// <returns></returns>
        public List<TaskLogDetailViewEntity> GetTaskErrorList()
        {
            string sql = @"SELECT a.SupName,a.FtpUserName,a.ID as TaskLogDetailID,a.TaskName,a.DataType,a.CycleType,a.TaskExecutedStatus,a.ErrorContent,a.StartTime
FROM T8_V_Upload_TaskLogDetail a
LEFT JOIN dbo.T8_NotifyTaskError b ON b.TaskLogDetailID = a.ID
WHERE TaskExecutedStatus = 2 AND ISNULL(b.NotifyStatus,0) != 1";
            string insertSql = "Insert Into T8_NotifyTaskError(TaskLogDetailID)Values(@TaskLogDetailID)";
            string delSql = "Delete From T8_NotifyTaskError Where TaskLogDetailID=@TaskLogDetailID";

            return base.ExecuteForWithTrans(trans =>
            {
                List<TaskLogDetailViewEntity> errorTaskIDs = trans.Connection.Query<TaskLogDetailViewEntity>(sql, null, trans).ToList();

                if (errorTaskIDs.Count > 0)
                {
                    trans.Connection.Execute(delSql, errorTaskIDs, trans);
                    trans.Connection.Execute(insertSql, errorTaskIDs, trans);
                }
                return errorTaskIDs;
            }, t => true);
        }

        /// <summary>
        /// 更新任务异常记录
        /// </summary>
        /// <param name="taskErrorList"></param>
        /// <returns></returns>
        public bool UpdateTaskErrorList(List<KVEntity<int,int>> taskErrorList)
        {
            string sql = "UPDATE T8_NotifyTaskError SET NotifyMessage=@K,NotifyStatus=@T2 WHERE TaskLogDetailID=@T1";
            return base.ExecuteForWithTrans(trans =>
            {
                trans.Connection.Execute(sql, taskErrorList,trans);
                return true;
            });
        }
    }
}
