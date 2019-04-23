using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using Transfer8Pro.Entity;
using Dapper;
using System.Data;
using Transfer8Pro.Utils;

namespace Transfer8Pro.DAO
{
    /// <summary>
    /// FTP上传日志
    /// </summary>
    public class FtpUploadLogDAO : DAOBase<SQLiteConnection>
    {
        public FtpUploadLogDAO()
        {
            base._default_connect_str = base.GetSqliteConnectString();
        }

        public ParamtersForDBPageEntity<FtpUploadLogEntity> GetFtpUploadList(FtpUploadLogEntity ftpUploadLogEntity, int pageIndex, int pageSize)
        {
            string sqlFormatter = "SELECT {2} FROM T8_FtpUploadLog WHERE {3} Order By CreateTime desc Limit {0} offset {0}*{1}";
            string sqlTotalFormatter = "SELECT COUNT(*) AS Cnt FROM T8_FtpUploadLog WHERE {0} Order By CreateTime desc";
            return base.ExecuteFor(conn =>
            {
                List<FtpUploadLogEntity> list = null;
                int total = -1;

                DynamicParameters prms = new DynamicParameters();
                string sqlWhere = " 1=1 ";

                if (ftpUploadLogEntity != null)
                {                   

                    if (!string.IsNullOrEmpty(ftpUploadLogEntity.FileName))
                    {
                        sqlWhere += $" AND FileName LIKE '%{ftpUploadLogEntity.FileName}%'";
                    }

                    if (ftpUploadLogEntity.FtpUploadStatus.ToString() != "0")
                    {
                        sqlWhere += " AND FtpUploadStatus=@FtpUploadStatus";
                        prms.Add("@FtpUploadStatus", ftpUploadLogEntity.FtpUploadStatus);
                    }

                    if (ftpUploadLogEntity.CycleType.ToString() != "0")
                    {
                        sqlWhere += " AND CycleType=@CycleType";
                        prms.Add("@CycleType", ftpUploadLogEntity.CycleType);
                    }

                    if (ftpUploadLogEntity.DataType.ToString() != "0")
                    {
                        sqlWhere += " AND DataType=@DataType";
                        prms.Add("@DataType", ftpUploadLogEntity.DataType);
                    }

                    if (ftpUploadLogEntity.UploadStartTime != DateTime.MinValue)
                    {
                        sqlWhere += " AND UploadStartTime>=@UploadStartTime AND UploadEndTime<=@UploadEndTime";
                        prms.Add("@UploadStartTime", ftpUploadLogEntity.UploadStartTime.ToString("s"));
                        prms.Add("@UploadEndTime", ftpUploadLogEntity.UploadEndTime.ToString("s"));
                    }
                }

                string sql = string.Format(sqlFormatter, pageSize, pageIndex - 1, "*", sqlWhere);
                string totalSql = string.Format(sqlTotalFormatter, sqlWhere);
                list = conn.Query<FtpUploadLogEntity>(sql, prms).ToList();

                if (pageIndex == 1)
                {
                    DataTable totalTable = conn.QueryDT(totalSql, prms);
                    total = totalTable.Rows[0]["Cnt"].ToString().ToInt();
                }

                return new ParamtersForDBPageEntity<FtpUploadLogEntity>()
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    Total = total,
                    DataList = list
                };
            });
        }

        /// <summary>
        /// 根据文件名查找上传文件数据
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public FtpUploadLogEntity Find(string fileName)
        {
            string sql = "SELECT * FROM T8_FtpUploadLog WHERE FileName=@FileName  ORDER BY CreateTime DESC";
            return base.ExecuteFor(conn =>
            {
                var prms = new
                {
                    FileName = fileName
                };

                return conn.Query<FtpUploadLogEntity>(sql, prms).FirstOrDefault();
            });
        }

        /// <summary>
        /// 根据ID查找上传文件数据
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public FtpUploadLogEntity Find(int ftpUploadID)
        {
            string sql = "SELECT * FROM T8_FtpUploadLog WHERE FtpUploadID=@FtpUploadID ORDER BY CreateTime DESC";
            return base.ExecuteFor(conn =>
            {
                var prms = new
                {
                    FtpUploadID = ftpUploadID
                };

                return conn.Query<FtpUploadLogEntity>(sql, prms).FirstOrDefault();
            });
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="uploadLogEntity"></param>
        /// <returns></returns>
        public bool Insert(FtpUploadLogEntity uploadLogEntity)
        {
            string sql = @"INSERT INTO T8_FtpUploadLog(FileFullPath,FileName,UploadStartTime,UploadEndTime,FtpUploadStatus,CreateTime,Remark,CycleType,DataType) 
 VALUES(@FileFullPath,@FileName,@UploadStartTime,@UploadEndTime,@FtpUploadStatus,@CreateTime,@Remark,@CycleType,@DataType);";

            return base.ExecuteFor(conn =>
            {
                var prms = new
                {                  
                    FileFullPath = uploadLogEntity.FileFullPath,
                    FileName = uploadLogEntity.FileName,
                    UploadStartTime = uploadLogEntity.UploadStartTime != DateTime.MinValue ? uploadLogEntity.UploadStartTime.ToString("s") : null,
                    UploadEndTime = uploadLogEntity.UploadEndTime != DateTime.MinValue ? uploadLogEntity.UploadEndTime.ToString("s") : null,
                    FtpUploadStatus = uploadLogEntity.FtpUploadStatus,
                    CreateTime = uploadLogEntity.CreateTime != DateTime.MinValue ? uploadLogEntity.CreateTime.ToString("s") : null,
                    Remark = uploadLogEntity.Remark,
                    CycleType = uploadLogEntity.CycleType,
                    DataType = uploadLogEntity.DataType
                };
                return conn.Execute(sql, prms) > 0;
            });
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="uploadLogEntity"></param>
        /// <returns></returns>
        public bool Insert(FtpUploadLogEntity uploadLogEntity, out int ftpUploadID)
        {
            ftpUploadID = 0;
            string sql = @"INSERT INTO T8_FtpUploadLog(FtpUploadID,FileFullPath,FileName,UploadStartTime,UploadEndTime,FtpUploadStatus,CreateTime,Remark,CycleType,DataType) 
 VALUES(NULL,@FileFullPath,@FileName,@UploadStartTime,@UploadEndTime,@FtpUploadStatus,@CreateTime,@Remark,@CycleType,@DataType);
 select last_insert_rowid() FtpUploadID;";

             ftpUploadID = base.ExecuteFor(conn =>
            {
                var prms = new
                {                  
                    FileFullPath = uploadLogEntity.FileFullPath,
                    FileName = uploadLogEntity.FileName,
                    UploadStartTime = uploadLogEntity.UploadStartTime != DateTime.MinValue ? uploadLogEntity.UploadStartTime.ToString("s") : null,
                    UploadEndTime = uploadLogEntity.UploadEndTime != DateTime.MinValue ? uploadLogEntity.UploadEndTime.ToString("s") : null,
                    FtpUploadStatus = uploadLogEntity.FtpUploadStatus,
                    CreateTime = uploadLogEntity.CreateTime != DateTime.MinValue ? uploadLogEntity.CreateTime.ToString("s") : null,
                    Remark = uploadLogEntity.Remark,
                    CycleType = uploadLogEntity.CycleType,
                    DataType = uploadLogEntity.DataType
                };
                FtpUploadLogEntity ftpUploadLog = conn.Query<FtpUploadLogEntity>(sql, prms).FirstOrDefault();
                if (ftpUploadLog == null || ftpUploadLog.FtpUploadID == 0)
                {
                    return 0;
                }
                return ftpUploadLog.FtpUploadID;               
            });
            return ftpUploadID > 0 ? true : false;
        }


        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="uploadLogEntity"></param>
        /// <returns></returns>
        public bool Update(FtpUploadLogEntity uploadLogEntity)
        {
            string sql = @"UPDATE T8_FtpUploadLog SET FileFullPath=@FileFullPath,FileName=@FileName,UploadStartTime=@UploadStartTime,UploadEndTime=@UploadEndTime,FtpUploadStatus=@FtpUploadStatus,CreateTime=@CreateTime,
 Remark=@Remark,CycleType=@CycleType,DataType=@DataType,UploadStatus=0
 WHERE FtpUploadID=@FtpUploadID";

            return base.ExecuteFor(conn =>
            {
                var prms = new
                {                  
                    FileFullPath = uploadLogEntity.FileFullPath,
                    FileName = uploadLogEntity.FileName,
                    UploadStartTime = uploadLogEntity.UploadStartTime != DateTime.MinValue ? uploadLogEntity.UploadStartTime.ToString("s") : null,
                    UploadEndTime = uploadLogEntity.UploadEndTime != DateTime.MinValue ? uploadLogEntity.UploadEndTime.ToString("s") : null,
                    FtpUploadStatus = uploadLogEntity.FtpUploadStatus,
                    CreateTime = uploadLogEntity.CreateTime != DateTime.MinValue ? uploadLogEntity.CreateTime.ToString("s") : null,
                    Remark = uploadLogEntity.Remark,
                    FtpUploadID = uploadLogEntity.FtpUploadID,
                    CycleType = uploadLogEntity.CycleType,
                    DataType = uploadLogEntity.DataType
                };
                return conn.Execute(sql, prms) > 0;
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ftpUploadID"></param>
        /// <param name="uploadEndTime"></param>
        /// <param name="ftpUploadStatus"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public bool UpdateFtpStatus(int ftpUploadID, DateTime uploadEndTime, FtpUploadStatus ftpUploadStatus, string remark)
        {
            string sql = "UPDATE T8_FtpUploadLog SET UploadEndTime=@UploadEndTime,FtpUploadStatus=@FtpUploadStatus,Remark=@Remark,UploadEndTime=0,UploadStatus=0 WHERE FtpUploadID=@FtpUploadID";
            return base.ExecuteFor(conn =>
            {
                var prms = new
                {
                    UploadEndTime = uploadEndTime != DateTime.MinValue ? uploadEndTime.ToString("s") : null,
                    FtpUploadStatus = ftpUploadStatus,
                    Remark = remark,
                    FtpUploadID = ftpUploadID
                };
                return conn.Execute(sql, prms) > 0;
            });

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ftpUploadID"></param>    
        /// <param name="ftpUploadStatus"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public bool UpdateFtpStatus(int ftpUploadID,FtpUploadStatus ftpUploadStatus, string remark)
        {
            string sql = "UPDATE T8_FtpUploadLog SET FtpUploadStatus=@FtpUploadStatus,Remark=@Remark,UploadStatus=0 WHERE FtpUploadID=@FtpUploadID";
            return base.ExecuteFor(conn =>
            {
                var prms = new
                {                
                    FtpUploadStatus = ftpUploadStatus,
                    Remark = remark,
                    FtpUploadID = ftpUploadID
                };
                return conn.Execute(sql, prms) > 0;
            });

        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ftpUploadID"></param>
        /// <returns></returns>
        public bool Delete(int ftpUploadID)
        {
            string sql = "DELETE FROM T8_FtpUploadLog WHERE FtpUploadID=@FtpUploadID";
            return base.ExecuteFor(conn =>
            {
                var prms = new
                {
                    FtpUploadID = ftpUploadID
                };
                return conn.Execute(sql, prms) > 0;
            });
        }

        /// <summary>
        /// 获取待上传列表
        /// </summary>
        /// <returns></returns>
        public List<FtpUploadLogEntity> GetWaitUploadLogList()
        {
            string sql = "SELECT * FROM T8_FtpUploadLog WHERE UploadStatus!=1";
            return base.ExecuteFor(conn =>
            {
                return conn.Query<FtpUploadLogEntity>(sql).ToList();
            });
        }
        /// <summary>
        /// 更新FTP上传状态
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool UpdateUploadStataus(List<FtpUploadLogEntity> list)
        {
            string sql = "Update T8_FtpUploadLog Set UploadStatus=@UploadStatus Where FtpUploadID=@FtpUploadID";
            return base.ExecuteForWithTrans(trans =>
            {
                return trans.Connection.Execute(sql, list, trans) > 0;
            });
        }
    }
}
