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
   public class SystemRoleDAO : DAOBase<SqlConnection>
    {
        public SystemRoleDAO()
        {
            base._default_connect_str = base.GetSqlServerConnectString();
        }
        /// <summary>
        /// 获取所有记录
        /// </summary>
        /// <returns></returns>
        public List<SystemRoleEntity> GetList()
        {
            string sql = "SELECT * FROM T8_SystemRoles";
            return base.ExecuteFor(conn =>
            {              
                return conn.Query<SystemRoleEntity>(sql).ToList();
            });
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="taskEntity"></param>       
        /// <returns></returns>
        public ParamtersForDBPageEntity<SystemRoleEntity> GetList(SystemRoleEntity prmsEntity)
        {
            DynamicParameters prms = new DynamicParameters();
            prms.Add("@TableName", "T8_SystemRoles");
            prms.Add("@Fields", "*");
            prms.Add("@OrderField", "RoleID ASC");
            prms.Add("@pageSize", prmsEntity.PageSize);
            prms.Add("@pageIndex", prmsEntity.PageIndex);
            prms.Add("@Records", "", DbType.Int32, ParameterDirection.Output);

            StringBuilder sbWhere = new StringBuilder(" 1=1 ");
            if (prmsEntity != null)
            {
                if (!string.IsNullOrEmpty(prmsEntity.RoleName))
                {
                    sbWhere.Append($" AND RoleName like '%{prmsEntity.RoleName}%'");
                }
            }

            prms.Add("@sqlWhere", sbWhere.ToString());

            return base.ExecuteFor(conn =>
            {
                List<SystemRoleEntity> list = conn.Query<SystemRoleEntity>("sp_pager05", prms, null, true, null, CommandType.StoredProcedure).ToList();
                return new ParamtersForDBPageEntity<SystemRoleEntity>()
                {
                    PageIndex = prmsEntity.PageIndex,
                    PageSize = prmsEntity.PageSize,
                    Total = prms.Get<int>("@Records"),
                    DataList = list
                };
            });
        }

        /// <summary>
        /// 变更角色和功能菜单关联
        /// </summary>
        /// <param name="roleRelFunList"></param>
        /// <returns></returns>
        public bool ChangeRole_Fun_Rel(List<SystemRoleRelFunEntity> roleRelFunList)
        {        
            string delSQL = "DELETE FROM T8_System_Role_Fun_Rel WHERE RoleID=@RoleID";
            string addSQL = "INSERT INTO T8_System_Role_Fun_Rel(RoleID,FunctionID) VALUES(@RoleID,@FunctionID)";          

            return base.ExecuteForWithTrans(trans =>
            {               
                trans.Connection.Execute(delSQL, new { RoleID  = roleRelFunList.First().RoleID},trans);              
                trans.Connection.Execute(addSQL, roleRelFunList,trans);
                return true;
              
            });
        }

        /// <summary>
        /// 根据该角色下已设置所有菜单
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public List<SystemRoleRelFunEntity> GetRoleRelFunList(int roleID)
        {
            string sql = "SELECT * FROM T8_System_Role_Fun_Rel WHERE RoleID=@RoleID";
            return base.ExecuteFor(conn =>
            {
                return conn.Query<SystemRoleRelFunEntity>(sql, new { RoleID = roleID }).ToList();
            });
        }

    }
}
