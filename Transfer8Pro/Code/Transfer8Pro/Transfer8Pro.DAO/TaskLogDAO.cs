using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using Transfer8Pro.Entity;
using Transfer8Pro.Utils;

namespace Transfer8Pro.DAO
{
    /// <summary>
    /// 任务日志
    /// </summary>
    public class TaskLogDAO : DAOBase<SQLiteConnection>
    {
        public TaskLogDAO()
        {
            base._default_connect_str = base.GetSqliteConnectString();
        }

        public ParamtersForDBPageEntity<TaskLogViewEntity> GetTaskLogList(TaskLogViewEntity taskLogEntity, int pageIndex, int pageSize)
        {
            string sqlFormatter = "SELECT {2} FROM T8_V_TaskLog WHERE {3} Order By TaskLogID desc Limit {0} offset {0}*{1}";
            string sqlTotalFormatter = "SELECT COUNT(*) AS Cnt FROM T8_V_TaskLog WHERE {0} Order By TaskLogID desc";
            return base.ExecuteFor(conn =>
            {
                List<TaskLogViewEntity> list = null;
                int total = -1;

                DynamicParameters prms = new DynamicParameters();
                string sqlWhere = " 1=1 ";

                if (taskLogEntity != null)
                {
                    if (!string.IsNullOrEmpty(taskLogEntity.TaskID))
                    {
                        sqlWhere += " AND TaskID=@TaskID";
                        prms.Add("@TaskID", taskLogEntity.TaskID);
                    }

                    if (taskLogEntity.CycleType.ToString() != "0")
                    {
                        sqlWhere += " AND CycleType=@CycleType";
                        prms.Add("@CycleType", taskLogEntity.CycleType);
                    }

                    if (taskLogEntity.DataType.ToString() != "0")
                    {
                        sqlWhere += " AND DataType=@DataType";
                        prms.Add("@DataType", taskLogEntity.DataType);
                    }

                    if (taskLogEntity.TaskExecutedStatus.ToString() != "0")
                    {
                        sqlWhere += " AND TaskExecutedStatus=@TaskExecutedStatus";
                        prms.Add("@TaskExecutedStatus", taskLogEntity.TaskExecutedStatus);
                    }
                }

                string sql = string.Format(sqlFormatter, pageSize, pageIndex - 1, "*", sqlWhere);
                string totalSql = string.Format(sqlTotalFormatter, sqlWhere);
                list = conn.Query<TaskLogViewEntity>(sql, prms).ToList();

                if (pageIndex == 1)
                {
                    DataTable totalTable = conn.QueryDT(totalSql, prms);
                    total = totalTable.Rows[0]["Cnt"].ToString().ToInt();
                }

                return new ParamtersForDBPageEntity<TaskLogViewEntity>()
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    Total = total,
                    DataList = list
                };
            });
        }

        /// <summary>
        /// 获取日志明细
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public ParamtersForDBPageEntity<TaskLogDetailViewEntity> GetTaskDetailLogList(TaskLogDetailViewEntity taskLogEntity, int pageIndex, int pageSize)
        {
            string sqlFormatter = "SELECT {2} FROM T8_V_TaskDetailLog WHERE {3} Order By CreateTime desc Limit {0} offset {0}*{1}";
            string sqlTotalFormatter = "SELECT COUNT(*) AS Cnt FROM T8_V_TaskDetailLog WHERE {0} Order By CreateTime desc";
            return base.ExecuteFor(conn =>
            {
                List<TaskLogDetailViewEntity> list = null;
                int total = -1;

                DynamicParameters prms = new DynamicParameters();
                string sqlWhere = " 1=1 ";

                if (taskLogEntity != null)
                {
                    if (!string.IsNullOrEmpty(taskLogEntity.TaskID))
                    {
                        sqlWhere += " AND TaskID=@TaskID";
                        prms.Add("@TaskID", taskLogEntity.TaskID);
                    }
                }

                string sql = string.Format(sqlFormatter, pageSize, pageIndex - 1, "*", sqlWhere);
                string totalSql = string.Format(sqlTotalFormatter, sqlWhere);
                list = conn.Query<TaskLogDetailViewEntity>(sql, prms).ToList();

                if (pageIndex == 1)
                {
                    DataTable totalTable = conn.QueryDT(totalSql, prms);
                    total = totalTable.Rows[0]["Cnt"].ToString().ToInt();
                }

                return new ParamtersForDBPageEntity<TaskLogDetailViewEntity>()
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    Total = total,
                    DataList = list
                };
            });
        }

        /// <summary>
        /// 新建或修改任务日志
        /// </summary>
        /// <param name="taskLogEntity"></param>
        /// <returns></returns>
        public bool InsertOrUpdateLog(TaskLogEntity taskLogEntity,out int taskLogDetailID)
        {
            taskLogDetailID = 0;
            string insertSql = @"INSERT INTO T8_TaskLog(TaskID,DataType,CycleType,ModifyTime,CreateTime) 
 VALUES(@TaskID,@DataType,@CycleType,@ModifyTime,@CreateTime)";
            string updatetSql = @"UPDATE T8_TaskLog SET TaskID=@TaskID,DataType=@DataType,CycleType=@CycleType,ModifyTime=@ModifyTime,CreateTime=@CreateTime
 WHERE TaskLogID=@TaskLogID";
            string existSql = "SELECT * FROM T8_TaskLog WHERE  TaskID=@TaskID";

            string insertDetailSql = @"INSERT INTO T8_TaskLogDetail(TaskLogDetailID,TaskID,StartTime,EndTime,TaskExecutedStatus,CreateTime,ErrorContent,FileName)
 VALUES(NULL,@TaskID,@StartTime,@EndTime,@TaskExecutedStatus,@CreateTime,@ErrorContent,@FileName);
 select last_insert_rowid() TaskLogDetailID;";

             taskLogDetailID = base.ExecuteForWithTrans(trans =>
            {
                TaskLogEntity orgLogEntity = trans.Connection.Query<TaskLogEntity>(existSql, new { TaskID= taskLogEntity.TaskID },trans).FirstOrDefault();
                if (orgLogEntity != null) //修改
                {
                    var prms = new
                    {
                        TaskID = orgLogEntity.TaskID,
                        DataType = taskLogEntity.DataType,
                        CycleType = taskLogEntity.CycleType,                       
                        ModifyTime = taskLogEntity.ModifyTime != DateTime.MinValue ? taskLogEntity.ModifyTime.ToString("s") : null,
                        CreateTime = orgLogEntity.CreateTime != DateTime.MinValue ?  orgLogEntity.CreateTime.ToString("s") :null,
                        TaskLogID = orgLogEntity.TaskLogID
                    };
                    trans.Connection.Execute(updatetSql, prms,trans);
                }
                else //新加
                {
                    var prms = new
                    {
                        TaskID = taskLogEntity.TaskID,
                        DataType = taskLogEntity.DataType,
                        CycleType = taskLogEntity.CycleType,                     
                        ModifyTime = taskLogEntity.ModifyTime != DateTime.MinValue ? taskLogEntity.ModifyTime.ToString("s") : null,
                        CreateTime = taskLogEntity.CreateTime != DateTime.MinValue ? taskLogEntity.CreateTime.ToString("s") : DateTime.Now.ToString("s")
                    };
                    trans.Connection.Execute(insertSql, prms,trans);
                }

                //添加日志明细
                var prmsDetail = new
                {
                    TaskID = taskLogEntity.TaskID,
                    StartTime = DateTime.Now.ToString("s"),
                    EndTime = taskLogEntity.ModifyTime != DateTime.MinValue ? taskLogEntity.ModifyTime.ToString("s") : null,
                    TaskExecutedStatus = TaskExecutedStatus.执行中,
                    CreateTime = DateTime.Now.ToString("s"),
                    ErrorContent = "",
                    FileName = taskLogEntity.FileName
                };

                TaskLogDetailEntity taskLogDetailEntity = trans.Connection.Query<TaskLogDetailEntity>(insertDetailSql, prmsDetail,trans).FirstOrDefault();
                if (taskLogDetailEntity == null && taskLogDetailEntity.TaskLogDetailID == 0)
                {
                    return 0;
                }

                return taskLogDetailEntity.TaskLogDetailID;               
            },result => {
                return result > 0;
            });
            return taskLogDetailID > 0;
        }

        /// <summary>
        /// 更新成功日志信息
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="taskLogDetailID"></param>
        /// <param name="taskExecutedStatus"></param>    
        /// <returns></returns>
        public bool UpdateSuccessLog(string taskID, int taskLogDetailID,string fileName)
        {
            string updateLogSql = "UPDATE T8_TaskLog SET ModifyTime=@ModifyTime WHERE TaskID=@TaskID";
            string updateDetailSql = "UPDATE T8_TaskLogDetail SET EndTime=@EndTime,TaskExecutedStatus=@TaskExecutedStatus,FileName=@FileName,UploadStatus=0 WHERE TaskLogDetailID=@TaskLogDetailID";

            return base.ExecuteForWithTrans(conn =>
            {
                var prms = new
                {
                    TaskID = taskID,
                    ModifyTime = DateTime.Now.ToString("s"),
                    TaskExecutedStatus = TaskExecutedStatus.成功,
                    TaskLogDetailID = taskLogDetailID,
                    EndTime = DateTime.Now.ToString("s"),
                    FileName=fileName
                };

                conn.Connection.Execute(updateLogSql,prms);
                conn.Connection.Execute(updateDetailSql,prms);
                return true;
            });
        }       

        /// <summary>
        /// 更新成功日志信息
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="taskLogDetailID"></param>
        /// <param name="taskExecutedStatus"></param>    
        /// <returns></returns>
        public bool UpdateErrorLog(string taskID, int taskLogDetailID,string errorContent)
        {
            string updateLogSql = "UPDATE T8_TaskLog SET ModifyTime=@ModifyTime WHERE TaskID=@TaskID";
            string updateDetailSql = "UPDATE T8_TaskLogDetail SET ErrorContent=@ErrorContent,TaskExecutedStatus=@TaskExecutedStatus,UploadStatus=0 WHERE TaskLogDetailID=@TaskLogDetailID";

            return base.ExecuteForWithTrans(conn =>
            {
                var prms = new
                {
                    TaskID = taskID,
                    ModifyTime = DateTime.Now.ToString("s"),
                    TaskExecutedStatus = TaskExecutedStatus.失败,
                    TaskLogDetailID = taskLogDetailID,
                    ErrorContent = errorContent
                };

                conn.Connection.Execute(updateLogSql, prms);
                conn.Connection.Execute(updateDetailSql, prms);
                return true;
            });
        }      

        /// <summary>
        /// 获取待上传日志明细列表
        /// </summary>
        /// <returns></returns>
        public List<TaskLogDetailEntity> GetWaitUploadLogList()
        {
            string sql = "Select * From T8_TaskLogDetail Where  UploadStatus!=1";
            return base.ExecuteFor(conn =>
            {
                return conn.Query<TaskLogDetailEntity>(sql).ToList();
            });
        }

        /// <summary>
        /// 更新上传状态
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool UpdateUploadStataus(List<TaskLogDetailEntity> list)
        {
            string sql = "Update T8_TaskLogDetail Set UploadStatus=@UploadStatus Where TaskLogDetailID=@TaskLogDetailID";
            return base.ExecuteForWithTrans(trans =>
            {
                return trans.Connection.Execute(sql, list) > 0;
            });
        }
    }
}
