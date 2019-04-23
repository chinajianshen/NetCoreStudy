using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Transfer8Pro.Entity.OB;
using Dapper;
using Transfer8Pro.Utils;
using Transfer8Pro.Entity;

namespace Transfer8Pro.DAO.OB
{
    public class ManualTaskDAO : DAOBase<SqlConnection>
    {
        public ManualTaskDAO()
        {
            base._default_connect_str = base.GetSqlServerConnectString();
        }

        /// <summary>
        /// 新建 1成功 0失败 2存在未执行任务
        /// </summary>
        /// <param name="manualTaskEntity"></param>
        /// <returns></returns>
        public int Insert(ManualTaskEntity manualTaskEntity)
        {
            string sql = @"INSERT INTO T8_ManualTaskInfo(FtpUserName,TaskID,ManualTaskStatus,CreatePerson,Remark,CompletionTime)
VALUES(@FtpUserName,@TaskID,@ManualTaskStatus,@CreatePerson,@Remark,@CompletionTime)";
            string existSql = "SELECT COUNT(*) AS Cnt FROM T8_ManualTaskInfo WHERE ManualTaskStatus=0 AND TaskID=@TaskID AND FtpUserName=@FtpUserName";

            return base.ExecuteFor(conn =>
            {
                var prms = new
                {
                    FtpUserName=manualTaskEntity.FtpUserName,
                    TaskID=manualTaskEntity.TaskID,
                    ManualTaskStatus=manualTaskEntity.ManualTaskStatus,
                    CreatePerson=manualTaskEntity.CreatePerson,
                    Remark=manualTaskEntity.Remark,
                    CompletionTime = manualTaskEntity.CompletionTime
                };

                DataTable table = conn.QueryDT(existSql, prms);
                if (table.Rows[0]["Cnt"].ToString().ToInt() > 0)
                {
                    return 2;
                }

                conn.Execute(sql, prms);
                return 1;
            });
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="manualTaskID"></param>
        /// <returns></returns>
        public bool Delete(int manualTaskID)
        {
            string sql = "DELETE FROM T8_ManualTaskInfo WHERE ManualTaskID=@ManualTaskID";
            return base.ExecuteFor(conn =>
            {
                return conn.Execute(sql, new { ManualTaskID = manualTaskID }) > 0;
            });
        }

        /// <summary>
        /// 更新执行中的手动任务状态
        /// </summary>
        /// <param name="manualTasks"></param>
        /// <returns></returns>
        public bool UpdateStatus(List<ManualTaskEntity> manualTasks)
        {
            string sql = "UPDATE T8_ManualTaskInfo SET ManualTaskStatus=@ManualTaskStatus,CompletionTime=getDate() WHERE ManualTaskID=@ManualTaskID";

            return base.ExecuteForWithTrans(trans =>
            {
                return trans.Connection.Execute(sql, manualTasks, trans) >0;
            });
        }

        /// <summary>
        /// 获取等待执行手动任务列表
        /// </summary>
        /// <returns></returns>
        public List<ManualTaskEntity> GetWaitingManualTaskList(ManualTaskEntity manualTask)
        {
            string querySql = "SELECT ManualTaskID,TaskID,ManualTaskStatus FROM T8_ManualTaskInfo WHERE ManualTaskStatus=0 AND FtpUserName=@FtpUserName";
            string updateSql = "UPDATE T8_ManualTaskInfo SET ManualTaskStatus=@ManualTaskStatus WHERE ManualTaskID IN @ManualTaskID";

            return base.ExecuteFor(conn =>
            {
                List<ManualTaskEntity> list = conn.Query<ManualTaskEntity>(querySql, manualTask).ToList();

                if (list.Count > 0)
                {
                    conn.Execute(updateSql, new { ManualTaskStatus= ManualTaskStatus.执行中, ManualTaskID = list.Select(item => item.ManualTaskID).ToArray() });
                }
                return list;
            });
        }

        /// <summary>
        /// 获取手动任务列表
        /// </summary>
        /// <param name="taskEntity"></param>      
        /// <returns></returns>
        public ParamtersForDBPageEntity<ManualTaskViewEntity> GeManualTaskList(ManualTaskEntity taskEntity)
        {
            DynamicParameters prms = new DynamicParameters();
            prms.Add("@TableName", "T8_V_ManualTaskList");
            prms.Add("@Fields", "*");
            prms.Add("@OrderField", "ManualTaskID DESC");
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
            }
            prms.Add("@sqlWhere", sbWhere.ToString());

            return base.ExecuteFor(conn =>
            {
                List<ManualTaskViewEntity> list = conn.Query<ManualTaskViewEntity>("sp_pager05", prms, null, true, null, CommandType.StoredProcedure).ToList();
                return new ParamtersForDBPageEntity<ManualTaskViewEntity>()
                {
                    PageIndex = taskEntity.PageIndex,
                    PageSize = taskEntity.PageSize,
                    Total = prms.Get<int>("@Records"),
                    DataList = list
                };
            });

        }
    }
}
