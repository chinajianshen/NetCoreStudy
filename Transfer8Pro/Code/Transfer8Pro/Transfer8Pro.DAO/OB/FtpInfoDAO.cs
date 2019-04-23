using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Transfer8Pro.Entity.OB;
using Dapper;
using System.Data;
using Transfer8Pro.Utils;
using Transfer8Pro.Entity;

namespace Transfer8Pro.DAO.OB
{
    public class FtpInfoDAO : DAOBase<SqlConnection>
    {
        public FtpInfoDAO()
        {
            base._default_connect_str = base.GetSqlServerConnectString();
        }


        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="taskEntity"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSizent"></param>
        /// <returns></returns>
        public ParamtersForDBPageEntity<FtpInfoEntity> GetList(FtpInfoEntity taskEntity)
        {
            DynamicParameters prms = new DynamicParameters();
            prms.Add("@TableName", "T8_V_FtpInfoList");
            prms.Add("@Fields", "*");
            prms.Add("@OrderField", "FtpID DESC");
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

                if (!string.IsNullOrEmpty(taskEntity.FtpEncryptKey))
                {
                    sbWhere.Append($" AND FtpEncryptKey='{taskEntity.FtpEncryptKey}'");
                }
              
            }
            prms.Add("@sqlWhere", sbWhere.ToString());

            return base.ExecuteFor(conn =>
            {
                List<FtpInfoEntity> list = conn.Query<FtpInfoEntity>("sp_pager05", prms, null, true, null, CommandType.StoredProcedure).ToList();
                return new ParamtersForDBPageEntity<FtpInfoEntity>()
                {
                    PageIndex = taskEntity.PageIndex,
                    PageSize = taskEntity.PageSize,
                    Total = prms.Get<int>("@Records"),
                    DataList = list
                };
            });

        }

        /// <summary>
        /// 查找一条FTP数据
        /// </summary>
        /// <param name="ftpUserName"></param>
        /// <returns></returns>
        public FtpInfoEntity Find(string ftpUserName)
        {
            string sql = "SELECT * FROM dbo.T8_V_FtpInfoList WHERE FtpUserName=@FtpUserName";
            return base.ExecuteFor(conn =>
            {
                var prms = new
                {
                    FtpUserName = ftpUserName
                };
                return conn.Query<FtpInfoEntity>(sql, prms).FirstOrDefault();
            });
        }

        /// <summary>
        /// 查找一条FTP数据
        /// </summary>
        /// <param name="ftpID"></param>
        /// <returns></returns>
        public FtpInfoEntity Find(int ftpID)
        {
            string sql = "SELECT * FROM dbo.T8_V_FtpInfoList WHERE FtpID=@FtpID";
            return base.ExecuteFor(conn =>
            {
                var prms = new
                {
                    FtpID = ftpID
                };
                return conn.Query<FtpInfoEntity>(sql, prms).FirstOrDefault();
            });
        }

        /// <summary>
        /// 添加 0失败 1成功 2FtpUserName已存在
        /// </summary>
        /// <param name="ftpInfo"></param>
        /// <returns></returns>
        public int Insert(FtpInfoEntity ftpInfo)
        {
            string insertSQL = "INSERT INTO T8_FtpInfo(FtpUserName,FtpPassword,FtpFolderName,FtpEncryptKey) VALUES(@FtpUserName,@FtpPassword,@FtpFolderName,@FtpEncryptKey)";
            string existSQL = "SELECT COUNT(*) AS Cnt FROM T8_FtpInfo WHERE FtpUserName=@FtpUserName";
            return base.ExecuteFor<int>(conn =>
            {
                var prms = new
                {
                    FtpID = ftpInfo.FtpID,
                    FtpUserName = ftpInfo.FtpUserName,
                    FtpPassword = ftpInfo.FtpPassword,
                    FtpFolderName = ftpInfo.FtpFolderName,
                    FtpEncryptKey = ftpInfo.FtpEncryptKey
                };

                DataTable table = conn.QueryDT(existSQL, prms);
                if (table == null)
                {
                    return 0;
                }

                if (table.Rows[0]["Cnt"].ToString().ToInt() > 0)
                {
                    return 2;
                }

                int result = conn.Execute(insertSQL, prms);
                return result > 0 ? 1 : 0;
            });
        }

        /// <summary>
        /// 修改 0失败 1成功 2FtpUserName已存在
        /// </summary>
        /// <param name="ftpInfo"></param>
        /// <returns></returns>
        public int Update(FtpInfoEntity ftpInfo)
        {
            string sql = "UPDATE T8_FtpInfo SET FtpUserName=@FtpUserName,FtpPassword=@FtpPassword,FtpFolderName=@FtpFolderName,FtpEncryptKey=@FtpEncryptKey WHERE FtpID=@FtpID";
            string existSQL = "SELECT COUNT(*) AS Cnt FROM T8_FtpInfo WHERE FtpID!=@FtpID AND FtpUserName=@FtpUserName";

            return base.ExecuteFor(conn =>
            {
                var prms = new
                {
                    FtpID = ftpInfo.FtpID,
                    FtpUserName = ftpInfo.FtpUserName,
                    FtpPassword = ftpInfo.FtpPassword,
                    FtpFolderName = ftpInfo.FtpFolderName,
                    FtpEncryptKey = ftpInfo.FtpEncryptKey
                };

                DataTable table = conn.QueryDT(existSQL, prms);
                if (table.Rows[0]["Cnt"].ToString().ToInt()>0)
                {
                    return 2;
                }
                return conn.Execute(sql, prms) > 0 ? 1:0;
            });
        }

        /// <summary>
        ///  删除一条记录
        /// </summary>
        /// <param name="ftpID"></param>
        /// <returns></returns>
        public bool Delete(int ftpID)
        {
            string sql = "DELETE FROM T8_FtpInfo WHERE FtpID=@FtpID";
            return base.ExecuteFor(conn =>
            {
                var prms = new
                {
                    FtpID = ftpID
                };
                return conn.Execute(sql, prms)>0;
            });
        }
    }
}
