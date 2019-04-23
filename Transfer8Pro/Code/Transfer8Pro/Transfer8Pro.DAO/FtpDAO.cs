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
    public class FtpDAO : DAOBase<SQLiteConnection>
    {
        public FtpDAO()
        {
            //string localDbConn = ConfigHelper.GetDBConnectStringConfig("LocalDBConnectStr");    
            base._default_connect_str = base.GetSqliteConnectString();
        }

        /// <summary>
        /// 新建或更新 
        /// </summary>
        /// <param name="ftpConfigEntity"></param>
        /// <returns></returns>
        public bool InsertOrUpdate(FtpConfigEntity ftpConfigEntity)
        {
            var sql = @"INSERT INTO T8_FtpConfig (ServerAddress,UserName,UserPassword,ExportFileDirectory,CreateTime,ServerDirectory,FtpType) 
                       VALUES(@ServerAddress,@UserName,@UserPassword,@ExportFileDirectory,@CreateTime,@ServerDirectory,@FtpType)";
            var existSql = "SELECT COUNT(*) AS Cnt FROM T8_FtpConfig Where FtpID=@FtpID";
            var updateSql = @"UPDATE T8_FtpConfig SET ServerAddress=@ServerAddress,UserName=@UserName,UserPassword=@UserPassword,ExportFileDirectory=@ExportFileDirectory,ServerDirectory=@ServerDirectory,FtpType=@FtpType,UploadStatus=0 Where FtpID=@FtpID";

            return base.ExecuteFor((conn) =>
            {
                var prms = new
                {
                    FtpID = ftpConfigEntity.FtpID,
                    ServerAddress = ftpConfigEntity.ServerAddress,
                    UserName = ftpConfigEntity.UserName,
                    UserPassword = ftpConfigEntity.UserPassword,
                    ExportFileDirectory = ftpConfigEntity.ExportFileDirectory,
                    CreateTime = DateTime.Now.ToString("s"),
                    ServerDirectory = ftpConfigEntity.ServerDirectory,
                    FtpType = ftpConfigEntity.FtpType
                };

                DataTable table = conn.QueryDT(existSql, prms);
                if (table != null && table.Rows[0]["Cnt"].ToString().ToInt() > 0)
                {
                    return conn.Execute(updateSql, prms) > 0;
                }
                else
                {
                    return conn.Execute(sql, prms) > 0;
                }              
            });
        }

        /// <summary>
        /// 新建或更新 
        /// </summary>
        /// <param name="ftpConfigEntity"></param>
        /// <returns></returns>
        public bool InsertOrUpdate(List<FtpConfigEntity> ftpConfigList)
        {           
            var sql = @"INSERT INTO T8_FtpConfig (ServerAddress,UserName,UserPassword,ExportFileDirectory,CreateTime,ServerDirectory,FtpType) 
                       VALUES(@ServerAddress,@UserName,@UserPassword,@ExportFileDirectory,@CreateTime,@ServerDirectory,@FtpType)";
            var existSql = "SELECT COUNT(*) AS Cnt FROM T8_FtpConfig Where FtpID=@FtpID";
            var updateSql = @"UPDATE T8_FtpConfig SET ServerAddress=@ServerAddress,UserName=@UserName,UserPassword=@UserPassword,ExportFileDirectory=@ExportFileDirectory,ServerDirectory=@ServerDirectory,FtpType=@FtpType,UploadStatus=0 Where FtpID=@FtpID";
            var delSql = "Delete From T8_FtpConfig";

            return base.ExecuteForWithTrans(trans =>
            {
                if (ftpConfigList.Count == 1)
                {
                    trans.Connection.Execute(delSql, null, trans);
                }

                foreach (FtpConfigEntity ftpConfig in ftpConfigList)
                {
                    var prms = new
                    {
                        FtpID = ftpConfig.FtpID,
                        ServerAddress = ftpConfig.ServerAddress,
                        UserName = ftpConfig.UserName,
                        UserPassword = ftpConfig.UserPassword,
                        ExportFileDirectory = ftpConfig.ExportFileDirectory,
                        CreateTime = DateTime.Now.ToString("s"),
                        ServerDirectory = ftpConfig.ServerDirectory,
                        FtpType = ftpConfig.FtpType
                    };

                    DataTable table = trans.Connection.QueryDT(existSql, prms, trans);
                    if (table != null && table.Rows[0]["Cnt"].ToString().ToInt() > 0)
                    {
                        trans.Connection.Execute(updateSql, prms, trans);
                    }
                    else
                    {
                        trans.Connection.Execute(sql, prms, trans);
                    }                  
                }
                return true;
            });
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="ftpConfigEntity"></param>
        /// <returns></returns>
        public bool Update(FtpConfigEntity ftpConfigEntity)
        {
            var sql = @"UPDATE T8_FtpConfig SET ServerAddress=@ServerAddress,UserName=@UserName,UserPassword=@UserPassword,ExportFileDirectory=@ExportFileDirectory,UploadStatus=0 WHERE FtpID=@FtpID";
            return base.ExecuteFor((conn) =>
            {
                var prms = new
                {
                    FtpID = ftpConfigEntity.FtpID,
                    ServerAddress = ftpConfigEntity.ServerAddress,
                    UserName = ftpConfigEntity.UserName,
                    UserPassword = ftpConfigEntity.UserPassword,
                    ExportFileDirectory = ftpConfigEntity.ExportFileDirectory,
                    CreateTime = DateTime.Now.ToString("s")
                };
                return conn.Execute(sql, prms) > 0;
            });
        }

        /// <summary>
        /// 删除 
        /// </summary>
        /// <param name="ftpID"></param>
        /// <returns></returns>
        public bool Delete(int ftpID)
        {
            var sql = @"DELETE FROM T8_FtpConfig WHERE FtpID=@FtpID";
            return base.ExecuteFor(conn =>
            {
                var prms = new
                {
                    FtpID = ftpID
                };
                return conn.Execute(sql, prms) > 0;
            });
        }

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <param name="ftpID"></param>
        /// <returns></returns>
        public FtpConfigEntity Find(int ftpID)
        {
            string sql = "SELECT * FROM T8_FtpConfig WHERE FtpID=@FtpID";
            return base.ExecuteFor(conn =>
            {
                var prms = new { FtpID = ftpID };
                return conn.Query<FtpConfigEntity>(sql, prms).FirstOrDefault();
            });
        }

        /// <summary>
        /// 获取主FTP数据
        /// </summary>      
        /// <returns></returns>
        public FtpConfigEntity GetFirstFtpInfo()
        {
            string sql = "SELECT * FROM T8_FtpConfig Where FtpType=1";
            return base.ExecuteFor(conn =>
            {

                return conn.Query<FtpConfigEntity>(sql).FirstOrDefault();
            });
        }

        /// <summary>
        /// 获取备用FTP数据
        /// </summary>
        /// <returns></returns>
        public FtpConfigEntity GetSecondFtpInfo()
        {
            string sql = "SELECT * FROM T8_FtpConfig Where FtpType=2";
            return base.ExecuteFor(conn =>
            {

                return conn.Query<FtpConfigEntity>(sql).FirstOrDefault();
            });
        }

        /// <summary>
        /// 获取待上传列表
        /// </summary>
        /// <returns></returns>
        public List<FtpConfigEntity> GetWaitUpload()
        {
            string sql = "SELECT * FROM T8_FtpConfig WHERE UploadStatus!=1";
            return base.ExecuteFor(conn =>
            {
                return conn.Query<FtpConfigEntity>(sql).ToList();
            });
        }

        /// <summary>
        /// 更新FTP配置上传状态
        /// </summary>
        /// <param name="ftpConfigEntity"></param>
        /// <returns></returns>
        public bool UpdateUploadStataus(List<FtpConfigEntity> ftpConfigList)
        {
            string sql = "Update T8_FtpConfig Set UploadStatus=@UploadStatus Where FtpID=@FtpID";
            return base.ExecuteForWithTrans(trans =>
            {
                return trans.Connection.Execute(sql, ftpConfigList,trans) > 0;
            });
        }
    }
}
