using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using Dapper;
using Transfer8Pro.Entity;

namespace Transfer8Pro.DAO
{
    /// <summary>
    /// 初始化数据操作
    /// </summary>
    public class InitDataBaseDAO : DAOBase<SQLiteConnection>
    {
        public InitDataBaseDAO()
        {
            base._default_connect_str = base.GetSqliteConnectString();
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <returns></returns>
        public bool InitDbData()
        {
            string delTable1 = "DELETE FROM T8_FtpConfig";
            string delTable2 = "DELETE FROM T8_FtpUploadLog";
            string delTable3 = "DELETE FROM T8_Task";
            string delTable4 = "DELETE FROM T8_TaskLog";
            string delTable5 = "DELETE FROM T8_TaskLogDetail";
            string initSysConfigTable = @"UPDATE T8_SystemConfig SET Cron='0 0/5 * * * ? * ',Status=1,UploadStatus=0 WHERE SysConfigID=10;
 UPDATE T8_SystemConfig SET Cron='0 0/5 * * * ? * ',Status=1,UploadStatus=0 WHERE SysConfigID=20;
 UPDATE T8_SystemConfig SET Cron='0 0/2 * * * ? * ',Status=1,ExSetting01='',UploadStatus=0 WHERE SysConfigID=30;
 UPDATE T8_SystemConfig SET Cron='0 0/10 * * * ? * ', Status=1,UploadStatus=0 WHERE SysConfigID=40;
 UPDATE T8_SystemConfig SET Status=1,UploadStatus=0 WHERE SysConfigID=50;
 UPDATE T8_SystemConfig SET Status=1,ExSetting01='',UploadStatus=0 WHERE SysConfigID=60;";

            return base.ExecuteForWithTrans(trans =>
            {
                trans.Connection.Execute(delTable1, null, trans);
                trans.Connection.Execute(delTable2, null, trans);
                trans.Connection.Execute(delTable3, null, trans);
                trans.Connection.Execute(delTable4, null, trans);
                trans.Connection.Execute(delTable5, null, trans);
                trans.Connection.Execute(initSysConfigTable, null, trans);
                return true;
            });
        }

        /// <summary>
        ///  保存从开卷接口下载的配置数据
        /// </summary>
        /// <param name="sysConfigList"></param>
        /// <param name="ftpConfigList"></param>
        /// <param name="taskList"></param>
        /// <returns></returns>
        public bool SaveDownloadConfig(List<SystemConfigEntity> sysConfigList, List<FtpConfigEntity> ftpConfigList, List<TaskEntity> taskList)
        {
            string delTable1 = "DELETE FROM T8_FtpConfig";
            string delTable2 = "DELETE FROM T8_FtpUploadLog";
            string delTable3 = "DELETE FROM T8_Task";
            string delTable4 = "DELETE FROM T8_TaskLog";
            string delTable5 = "DELETE FROM T8_TaskLogDetail";
            string delTable6 = "DELETE FROM T8_SystemConfig";

            string insertSQL1 = @"INSERT INTO T8_SystemConfig(SysConfigID,SysConfigName,Status,Cron,ExSetting01,ExSetting02,UploadStatus)
 VALUES(@SysConfigID,@SysConfigName,@Status,@Cron,@ExSetting01,@ExSetting02,1)";
            string insertSQL2 = @"INSERT INTO T8_FtpConfig (ServerAddress,UserName,UserPassword,ExportFileDirectory,CreateTime,ServerDirectory,FtpType,UploadStatus) 
 VALUES(@ServerAddress,@UserName,@UserPassword,@ExportFileDirectory,@CreateTime,@ServerDirectory,@FtpType,@UploadStatus)";
            string insertSQL3 = @"INSERT INTO T8_Task (TaskID,TaskName,Cron,DataHandler,DBConnectString_Hashed,SQL,DataType,IsDelete,TaskStatus,CreateTime,Remark,Enabled,CycleType,PosTypes,UploadStatus) 
 VALUES(@TaskID,@TaskName,@Cron,@DataHandler,@DBConnectString_Hashed,@SQL,@DataType,@IsDelete,@TaskStatus,@CreateTime,@Remark,@Enabled,@CycleType,@PosTypes,@UploadStatus)";

            return base.ExecuteForWithTrans(trans =>
            {
                trans.Connection.Execute(delTable1, null, trans);
                trans.Connection.Execute(delTable2, null, trans);
                trans.Connection.Execute(delTable3, null, trans);
                trans.Connection.Execute(delTable4, null, trans);
                trans.Connection.Execute(delTable5, null, trans);
                trans.Connection.Execute(delTable6, null, trans);

                #region
                if (sysConfigList != null && sysConfigList.Count > 0)
                {
                    trans.Connection.Execute(insertSQL1, sysConfigList, trans);
                }
                #endregion

                #region
                if (ftpConfigList != null && ftpConfigList.Count > 0)
                {
                    foreach (FtpConfigEntity item in ftpConfigList)
                    {
                        var prms = new
                        {
                            ServerAddress = item.ServerAddress,
                            UserName=item.UserName,
                            UserPassword=item.UserPassword,
                            ExportFileDirectory=item.ExportFileDirectory,
                            CreateTime=DateTime.Now.ToString("s"),
                            ServerDirectory=item.ServerDirectory,
                            FtpType=item.FtpType,
                            UploadStatus=1
                        };
                        trans.Connection.Execute(insertSQL2, prms, trans);
                    }
                }
                #endregion

                #region
                if (taskList != null && taskList.Count > 0)
                {
                    foreach (TaskEntity item in taskList)
                    {
                        var prms = new
                        {
                            TaskID = item.TaskID,
                            TaskName = item.TaskName,
                            Cron = item.Cron,
                            DataHandler = item.DataHandler,
                            DBConnectString_Hashed = item.DBConnectString_Hashed,
                            SQL = item.SQL,
                            DataType = item.DataType,
                            IsDelete = item.IsDelete,
                            TaskStatus = item.TaskStatus,
                            CreateTime = DateTime.Now.ToString("s"),                         
                            Remark = item.Remark,
                            Enabled = item.Enabled,
                            CycleType = item.CycleType,
                            PosTypes = item.PosTypes,
                            UploadStatus = 1
                        };
                        trans.Connection.Execute(insertSQL3, prms, trans);
                    }
                }
                #endregion

                return true;
            });
        }
    }
}
