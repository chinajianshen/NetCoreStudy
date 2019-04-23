using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Transfer8Pro.Entity.OB;
using Dapper;

namespace Transfer8Pro.DAO.OB
{
    public class SystemFunctionDAO : DAOBase<SqlConnection>
    {
        public SystemFunctionDAO()
        {
            base._default_connect_str = base.GetSqlServerConnectString();
        }

        /// <summary>
        /// 获取所有记录
        /// </summary>
        /// <returns></returns>
        public List<SystemFunctionEntity> GetList()
        {
            string sql = "SELECT * FROM T8_SystemFunctions ORDER BY FunctionID,FunctionOrder";

            return base.ExecuteFor(conn =>
            {
                return conn.Query<SystemFunctionEntity>(sql).ToList();
            });
        }


    }
}
