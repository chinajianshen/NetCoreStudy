using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Transfer8Pro.Entity;
using Dapper;
using Transfer8Pro.Utils;
using System.Data;
using Transfer8Pro.Entity.OB;

namespace Transfer8Pro.DAO.OB
{
    /// <summary>
    ///  传8客户端数据配置上传
    /// </summary>
    public class ClientUploadDAO : DAOBase<SqlConnection>
    {
        public ClientUploadDAO()
        {
            base._default_connect_str = base.GetSqlServerConnectString();
        }

        #region 保存客户上传数据

        /// <summary>
        /// 保存客户系统配置
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool SaveSysConfig(List<SystemConfigEntity> list)
        {
            string insertSql = @"Insert Into T8_Upload_SystemConfig(FtpUserName,SysConfigID,SysConfigName,Status,Cron,ExSetting01,ExSetting02) Values (@FtpUserName,@SysConfigID,@SysConfigName,@Status,@Cron,@ExSetting01,@ExSetting02)";
            string delSql = "DELETE FROM T8_Upload_SystemConfig WHERE FtpUserName=@FtpUserName AND SysConfigID=@SysConfigID";
            return base.ExecuteForWithTrans(trans =>
            {
                trans.Connection.Execute(delSql, list, trans);
                trans.Connection.Execute(insertSql, list, trans);
                return true;
            });
        }

        /// <summary>
        /// 保存FTP配置
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool SaveFtpConfig(List<FtpConfigEntity> list)
        {
            string insertSql = "Insert Into T8_Upload_FtpConfig(UserName,UserPassword,ServerAddress,ExportFileDirectory,ServerDirectory,FtpType) Values (@UserName,@UserPassword,@ServerAddress,@ExportFileDirectory,@ServerDirectory,@FtpType)";
            string delSql = "Delete From T8_Upload_FtpConfig Where UserName=@UserName AND FtpType=@FtpType";
            return base.ExecuteForWithTrans(trans =>
            {
                trans.Connection.Execute(delSql,list, trans);
                trans.Connection.Execute(insertSql, list, trans);
                return true;
            });
        }


        /// <summary>
        /// 保存任务
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool SaveTask(List<TaskEntity> list)
        {
            DataTable table = new DataTable();
            table.Columns.Add("FtpUserName", typeof(string));
            table.Columns.Add("TaskID", typeof(string));
            table.Columns.Add("TaskName", typeof(string));
            table.Columns.Add("Cron", typeof(string));
            table.Columns.Add("DataHandler", typeof(string));
            table.Columns.Add("DBConnectString_Hashed", typeof(string));
            table.Columns.Add("SQL", typeof(string));
            table.Columns.Add("DataType", typeof(int));
            table.Columns.Add("IsDelete", typeof(bool));
            table.Columns.Add("Enabled", typeof(int));
            table.Columns.Add("CycleType", typeof(int));
            table.Columns.Add("TaskStatus", typeof(int));
            table.Columns.Add("PosTypes", typeof(int));
            table.Columns.Add("CreateTime", typeof(DateTime));
            table.Columns.Add("ModifyTime", typeof(DateTime));
            table.Columns.Add("RecentRunTime", typeof(DateTime));
            table.Columns.Add("NextFireTime", typeof(DateTime));
            table.Columns.Add("Remark", typeof(string));


            try
            {
                DataRow row = null;
                foreach(var item in list)
                {
                    row = table.NewRow();
                    row["FtpUserName"] = item.FtpUserName;
                    row["TaskID"] = item.TaskID;
                    row["TaskName"] = item.TaskName;
                    row["Cron"] = item.Cron;
                    row["DataHandler"] = item.DataHandler;
                    row["DBConnectString_Hashed"] = item.DBConnectString_Hashed;
                    row["SQL"] = item.SQL;
                    row["DataType"] = item.DataType;
                    row["IsDelete"] = item.IsDelete;
                    row["Enabled"] = item.Enabled;
                    row["CycleType"] = item.CycleType;
                    row["TaskStatus"] = item.TaskStatus;
                    row["PosTypes"] = item.PosTypes;
                    DateTime stTime;
                    if (item.CreateTime != DateTime.MinValue && DateTime.TryParse(item.CreateTime.ToString(), out stTime))
                    {
                        row["CreateTime"] = item.CreateTime;
                    }
                    if (item.ModifyTime != DateTime.MinValue && DateTime.TryParse(item.ModifyTime.ToString(), out stTime))
                    {
                        row["ModifyTime"] = item.ModifyTime;
                    }
                    if (item.RecentRunTime != DateTime.MinValue && DateTime.TryParse(item.RecentRunTime.ToString(), out stTime))
                    {
                        row["RecentRunTime"] = item.RecentRunTime;
                    }
                    if (item.NextFireTime != DateTime.MinValue && DateTime.TryParse(item.NextFireTime.ToString(), out stTime))
                    {
                        row["NextFireTime"] = item.NextFireTime;
                    }                   
                    row["Remark"] = item.Remark;

                    table.Rows.Add(row);
                }


             SqlParameter[] prms =
             {
                    new SqlParameter("@TaskTable",SqlDbType.Structured),
                    new SqlParameter("@ReturnValue","")
                };
                prms[0].Value = table;
                prms[1].Direction = ParameterDirection.ReturnValue;

                SQlHelper.ExecuteNonQuery(base.GetSqlServerConnectString(), CommandType.StoredProcedure, "T8_Proc_Upload_InsertOrUpdateTask", prms);
                int returnValue = prms[1].Value.ToString().ToInt();
                return returnValue == 1 ? true : false;
            }
            catch (Exception ex)
            {
                LogUtil.WriteLog(ex);
                return false;
            }
        }

        /// <summary>
        /// 保存任务日志明细
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool SaveTaskLogDetail(List<TaskLogDetailEntity> list)
        {
            DataTable table = new DataTable();
            table.Columns.Add("FtpUserName", typeof(string));
            table.Columns.Add("TaskLogDetailID", typeof(int));
            table.Columns.Add("TaskID", typeof(string));
            table.Columns.Add("FileName", typeof(string));
            table.Columns.Add("StartTime", typeof(DateTime));
            table.Columns.Add("EndTime", typeof(DateTime));
            table.Columns.Add("TaskExecutedStatus", typeof(int));
            table.Columns.Add("CreateTime", typeof(DateTime));
            table.Columns.Add("ErrorContent", typeof(string));
            table.Columns.Add("UploadStatus", typeof(int));


            try
            {
                DataRow row = null;
                DateTime stTime;
                DateTime edTime;
                DateTime ctTime;
                foreach (var item in list)
                {
                    row = table.NewRow();
                    row["FtpUserName"] = item.FtpUserName;
                    row["TaskLogDetailID"] = item.TaskLogDetailID;
                    row["TaskID"] = item.TaskID;
                    row["FileName"] = item.FileName;
                    if (item.StartTime != DateTime.MinValue && DateTime.TryParse(item.StartTime.ToString(),out stTime))
                    {
                        row["StartTime"] = stTime;
                    }

                    if (item.EndTime != DateTime.MinValue && DateTime.TryParse(item.EndTime.ToString(), out edTime))
                    {
                        row["EndTime"] = edTime;
                    }
                  
                    row["TaskExecutedStatus"] = item.TaskExecutedStatus;

                    if (item.CreateTime != DateTime.MinValue && DateTime.TryParse(item.CreateTime.ToString(),out ctTime))
                    {
                        row["CreateTime"] = ctTime;
                    }
                  
                    row["ErrorContent"] = item.ErrorContent;
                    row["UploadStatus"] = item.UploadStatus;

                    table.Rows.Add(row);
                }


                SqlParameter[] prms =
                {
                    new SqlParameter("@TaskLogDetailTable",SqlDbType.Structured),
                    new SqlParameter("@ReturnValue","")
                };
                prms[0].Value = table;
                prms[1].Direction = ParameterDirection.ReturnValue;

                SQlHelper.ExecuteNonQuery(base.GetSqlServerConnectString(), CommandType.StoredProcedure, "T8_Proc_Upload_InsertOrUpdateTaskLogDetail", prms);
                int returnValue = prms[1].Value.ToString().ToInt();
                return returnValue == 1 ? true : false;
            }
            catch (Exception ex)
            {
                LogUtil.WriteLog(ex);
                return false;
            }
        }

        /// <summary>
        /// 保存FTP上传日志
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool SaveFtpUploadLog(List<FtpUploadLogEntity> list)
        {
            DataTable table = new DataTable();
            table.Columns.Add("FtpUserName", typeof(string));
            table.Columns.Add("FtpUploadID", typeof(int));
            table.Columns.Add("FileFullPath", typeof(string));
            table.Columns.Add("FileName", typeof(string));
            table.Columns.Add("UploadStartTime", typeof(DateTime));
            table.Columns.Add("UploadEndTime", typeof(DateTime));
            table.Columns.Add("CycleType", typeof(int));
            table.Columns.Add("DataType", typeof(int));
            table.Columns.Add("FtpUploadStatus", typeof(int));
            table.Columns.Add("Remark", typeof(string));

            try
            {
                DataRow row = null;
                DateTime stTime;
                DateTime edTime;
                foreach (var item in list)
                {
                    row = table.NewRow();
                    row["FtpUserName"] = item.FtpUserName;
                    row["FtpUploadID"] = item.FtpUploadID;
                    row["FileFullPath"] = item.FileFullPath;
                    row["FileName"] = item.FileName;
                    if (item.UploadStartTime != DateTime.MinValue && DateTime.TryParse(item.UploadStartTime.ToString(),out stTime))
                    {
                        row["UploadStartTime"] = stTime;
                    }

                    if (item.UploadEndTime != DateTime.MinValue && DateTime.TryParse(item.UploadEndTime.ToString(),out edTime))
                    {
                        row["UploadEndTime"] = edTime;
                    }                   
                  
                    row["CycleType"] = item.CycleType;
                    row["DataType"] = item.DataType;
                    row["FtpUploadStatus"] = item.FtpUploadStatus;
                    row["Remark"] = item.Remark;
                    table.Rows.Add(row);
                }

               SqlParameter[] prms =
               {
                    new SqlParameter("@FtpUploadLogTable",SqlDbType.Structured),
                    new SqlParameter("@ReturnValue","")
                };
                prms[0].Value = table;
                prms[1].Direction = ParameterDirection.ReturnValue;

                SQlHelper.ExecuteNonQuery(base.GetSqlServerConnectString(), CommandType.StoredProcedure, "T8_Proc_Upload_InsertOrUpdateFtpUploadLog", prms);
                int returnValue = prms[1].Value.ToString().ToInt();
                return returnValue == 1 ? true : false;
            }
            catch (Exception ex)
            {
                LogUtil.WriteLog(ex);
                return false;
            }
        }

        #endregion


        /// <summary>
        /// 获取系统配置列表
        /// </summary>
        /// <param name="taskEntity"></param>      
        /// <returns></returns>
        public ParamtersForDBPageEntity<SystemConfigViewEntity> GetSystemConfigList(SystemConfigViewEntity taskEntity)
        {
            DynamicParameters prms = new DynamicParameters();
            prms.Add("@TableName", "T8_V_Upload_SystemConfig");
            prms.Add("@Fields", "*");
            prms.Add("@OrderField", "ID DESC");
            prms.Add("@pageSize", taskEntity.PageSize);
            prms.Add("@pageIndex", taskEntity.PageIndex);
            prms.Add("@Records", "", DbType.Int32, ParameterDirection.Output);

            StringBuilder sbWhere = new StringBuilder(" 1=1 ");
            if (taskEntity != null)
            {
                if (!string.IsNullOrEmpty(taskEntity.FtpUserName))
                {
                    sbWhere.Append($" AND FtpUserName like '%{taskEntity.FtpUserName.Trim()}%'");
                }
            }
            prms.Add("@sqlWhere", sbWhere.ToString());

            return base.ExecuteFor(conn =>
            {
                List<SystemConfigViewEntity> list = conn.Query<SystemConfigViewEntity>("sp_pager05", prms, null, true, null, CommandType.StoredProcedure).ToList();
                return new ParamtersForDBPageEntity<SystemConfigViewEntity>()
                {
                    PageIndex = taskEntity.PageIndex,
                    PageSize = taskEntity.PageSize,
                    Total = prms.Get<int>("@Records"),
                    DataList = list
                };
            });

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
            DynamicParameters prms = new DynamicParameters();
            prms.Add("@TableName", "T8_V_Upload_FtpConfig");
            prms.Add("@Fields", "*");
            prms.Add("@OrderField", "ID DESC");
            prms.Add("@pageSize", taskEntity.PageSize);
            prms.Add("@pageIndex", taskEntity.PageIndex);
            prms.Add("@Records", "", DbType.Int32, ParameterDirection.Output);

            StringBuilder sbWhere = new StringBuilder(" 1=1 ");
            if (taskEntity != null)
            {               
                if (!string.IsNullOrEmpty(taskEntity.FtpUserName))
                {
                    sbWhere.Append($" AND UserName like '%{taskEntity.FtpUserName.Trim()}%'");
                }
            }
            prms.Add("@sqlWhere", sbWhere.ToString());

            return base.ExecuteFor(conn =>
            {
                List<FtpConfigViewEntity> list = conn.Query<FtpConfigViewEntity>("sp_pager05", prms, null, true, null, CommandType.StoredProcedure).ToList();
                return new ParamtersForDBPageEntity<FtpConfigViewEntity>()
                {
                    PageIndex = taskEntity.PageIndex,
                    PageSize = taskEntity.PageSize,
                    Total = prms.Get<int>("@Records"),
                    DataList = list
                };
            });

        }

        /// <summary>
        /// 获取任务配置列表
        /// </summary>
        /// <param name="taskEntity"></param>      
        /// <returns></returns>
        public ParamtersForDBPageEntity<TaskViewEntity> GetTaskList(TaskViewEntity taskEntity)
        {
            DynamicParameters prms = new DynamicParameters();
            prms.Add("@TableName", "T8_V_Upload_Task");
            prms.Add("@Fields", "*");
            prms.Add("@OrderField", "ID DESC");
            prms.Add("@pageSize", taskEntity.PageSize);
            prms.Add("@pageIndex", taskEntity.PageIndex);
            prms.Add("@Records", "", DbType.Int32, ParameterDirection.Output);

            StringBuilder sbWhere = new StringBuilder(" 1=1 ");
            if (taskEntity != null)
            {
                if (!string.IsNullOrEmpty(taskEntity.FtpUserName))
                {
                    sbWhere.Append($" AND FtpUserName like '%{taskEntity.FtpUserName.Trim()}%'");
                }
            }
            prms.Add("@sqlWhere", sbWhere.ToString());

            return base.ExecuteFor(conn =>
            {
                List<TaskViewEntity> list = conn.Query<TaskViewEntity>("sp_pager05", prms, null, true, null, CommandType.StoredProcedure).ToList();
                return new ParamtersForDBPageEntity<TaskViewEntity>()
                {
                    PageIndex = taskEntity.PageIndex,
                    PageSize = taskEntity.PageSize,
                    Total = prms.Get<int>("@Records"),
                    DataList = list
                };
            });

        }

        /// <summary>
        /// 获取任务
        /// </summary>
        /// <param name="taskEntity"></param>
        /// <returns></returns>
        public TaskViewEntity GetTask(TaskViewEntity taskEntity)
        {
            string sql = "Select * From T8_V_Upload_Task Where TaskID=@TaskID AND FtpUserName=@FtpUserName";
            return base.ExecuteFor(conn =>
            {
                return conn.Query<TaskViewEntity>(sql, new { TaskID = taskEntity.TaskID, FtpUserName = taskEntity.FtpUserName.Trim() }).FirstOrDefault();
            });
        }

        /// <summary>
        /// 获取任务日志列表
        /// </summary>
        /// <param name="taskEntity"></param>      
        /// <returns></returns>
        public ParamtersForDBPageEntity<TaskLogDetailViewEntity> GetTaskLogDetailList(TaskLogDetailViewEntity taskEntity)
        {
            DynamicParameters prms = new DynamicParameters();
            prms.Add("@TableName", "T8_V_Upload_TaskLogDetail");
            prms.Add("@Fields", "*");
            prms.Add("@OrderField", "ID DESC");
            prms.Add("@pageSize", taskEntity.PageSize);
            prms.Add("@pageIndex", taskEntity.PageIndex);
            prms.Add("@Records", "", DbType.Int32, ParameterDirection.Output);

            StringBuilder sbWhere = new StringBuilder(" 1=1 ");
            if (taskEntity != null)
            {
                if (!string.IsNullOrEmpty(taskEntity.FtpUserName))
                {
                    sbWhere.Append($" AND FtpUserName like '%{taskEntity.FtpUserName.Trim()}%'");
                }

                if ((int)taskEntity.TaskExecutedStatus > 0)
                {
                    sbWhere.Append($" AND TaskExecutedStatus={(int)taskEntity.TaskExecutedStatus}");
                }
            }
            prms.Add("@sqlWhere", sbWhere.ToString());

            return base.ExecuteFor(conn =>
            {
                List<TaskLogDetailViewEntity> list = conn.Query<TaskLogDetailViewEntity>("sp_pager05", prms, null, true, null, CommandType.StoredProcedure).ToList();
                return new ParamtersForDBPageEntity<TaskLogDetailViewEntity>()
                {
                    PageIndex = taskEntity.PageIndex,
                    PageSize = taskEntity.PageSize,
                    Total = prms.Get<int>("@Records"),
                    DataList = list
                };
            });

        }

        /// <summary>
        /// 获取FTP任务日志列表
        /// </summary>
        /// <param name="taskEntity"></param>      
        /// <returns></returns>
        public ParamtersForDBPageEntity<FtpUploadLogViewEntity> GetFtpUploadLogList(FtpUploadLogViewEntity taskEntity)
        {
            DynamicParameters prms = new DynamicParameters();
            prms.Add("@TableName", "T8_V_Upload_FtpUploadLog");
            prms.Add("@Fields", "*");
            prms.Add("@OrderField", "ID DESC");
            prms.Add("@pageSize", taskEntity.PageSize);
            prms.Add("@pageIndex", taskEntity.PageIndex);
            prms.Add("@Records", "", DbType.Int32, ParameterDirection.Output);

            StringBuilder sbWhere = new StringBuilder(" 1=1 ");
            if (taskEntity != null)
            {
                if (!string.IsNullOrEmpty(taskEntity.FtpUserName))
                {
                    sbWhere.Append($" AND FtpUserName like '%{taskEntity.FtpUserName.Trim()}%'");
                }

                if ((int)taskEntity.FtpUploadStatus > 0)
                {
                    sbWhere.Append($" AND FtpUploadStatus={(int)taskEntity.FtpUploadStatus}");
                }
            }
            prms.Add("@sqlWhere", sbWhere.ToString());

            return base.ExecuteFor(conn =>
            {
                List<FtpUploadLogViewEntity> list = conn.Query<FtpUploadLogViewEntity>("sp_pager05", prms, null, true, null, CommandType.StoredProcedure).ToList();
                return new ParamtersForDBPageEntity<FtpUploadLogViewEntity>()
                {
                    PageIndex = taskEntity.PageIndex,
                    PageSize = taskEntity.PageSize,
                    Total = prms.Get<int>("@Records"),
                    DataList = list
                };
            });

        }

        #region 下载客户配置信息
        /// <summary>
        /// 检测客户是否上传配置数据
        /// </summary>
        /// <param name="ftpUserName"></param>
        /// <returns></returns>
        public bool CheckUploadConfig(string ftpUserName)
        {
            string sql1 = "Select Count(*) as Cnt From T8_V_Upload_SystemConfig Where FtpUserName=@FtpUserName";
            string sql2 = "Select Count(*) as Cnt From T8_V_Upload_FtpConfig Where FtpUserName=@FtpUserName";
            string sql3 = "SELECT Count(*) as Cnt FROM T8_V_Upload_Task WHERE FtpUserName=@FtpUserName";

            return base.ExecuteFor(conn =>
            {
                var prms = new
                {
                    FtpUserName = ftpUserName
                };

                DataTable table1 = conn.QueryDT(sql1, prms);
                if (table1 != null && table1.Rows.Count>0 && table1.Rows[0]["Cnt"].ToString().ToInt() > 0)
                {
                    return true;
                }

                DataTable table2 = conn.QueryDT(sql2, prms);
                if (table2 != null && table2.Rows.Count > 0 && table2.Rows[0]["Cnt"].ToString().ToInt() > 0)
                {
                    return true;
                }

                DataTable table3 = conn.QueryDT(sql3, prms);
                if (table3 != null && table3.Rows.Count > 0 && table3.Rows[0]["Cnt"].ToString().ToInt() > 0)
                {
                    return true;
                }

                return false;
            });
        }

        /// <summary>
        /// 下载客户系统配置列表
        /// </summary>
        /// <param name="taskEntity"></param>      
        /// <returns></returns>
        public List<SystemConfigEntity> DownloadSystemConfigList(SystemConfigEntity systemConfigEntity)
        {
            string sql = "Select * From T8_V_Upload_SystemConfig Where FtpUserName=@FtpUserName";                        

            return base.ExecuteFor(conn =>
            {
                var prms = new
                {
                    FtpUserName = systemConfigEntity.FtpUserName
                };
                return conn.Query<SystemConfigEntity>(sql, prms).ToList();
            });
        }

        /// <summary>
        /// 下载客户FTP配置信息
        /// </summary>
        /// <param name="ftpConfigViewEntity"></param>
        /// <returns></returns>
        public List<FtpConfigEntity> DownloadFtpConfigList(FtpConfigViewEntity ftpConfigViewEntity)
        {
            string sql = "Select * From T8_V_Upload_FtpConfig Where FtpUserName=@FtpUserName";

            return base.ExecuteFor(conn =>
            {
                var prms = new
                {
                    FtpUserName = ftpConfigViewEntity.FtpUserName
                };
                return conn.Query<FtpConfigEntity>(sql, prms).ToList();
            });
        }

        /// <summary>
        ///  下载客户任务配置信息
        /// </summary>
        /// <param name="taskEntity"></param>
        /// <returns></returns>
        public List<TaskEntity> DownloadTaskList(TaskEntity taskEntity)
        {
            string sql = "SELECT * FROM T8_V_Upload_Task WHERE FtpUserName=@FtpUserName";
            return base.ExecuteFor(conn =>
            {
                var prms = new
                {
                    FtpUserName = taskEntity.FtpUserName
                };
                return conn.Query<TaskEntity>(sql, prms).ToList();
            });
        }
        #endregion
    }
}
