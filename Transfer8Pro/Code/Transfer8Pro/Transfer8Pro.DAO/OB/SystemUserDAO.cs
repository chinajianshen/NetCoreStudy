using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using Transfer8Pro.Entity;
using Transfer8Pro.Entity.OB;
using Transfer8Pro.Utils;

namespace Transfer8Pro.DAO.OB
{
    public class SystemUserDAO : DAOBase<SqlConnection>
    {
        public SystemUserDAO()
        {
            base._default_connect_str = base.GetSqlServerConnectString();
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="taskEntity"></param>       
        /// <returns></returns>
        public ParamtersForDBPageEntity<SystemUserViewEntity> GetList(SystemUserViewEntity userEntity)
        {
            DynamicParameters prms = new DynamicParameters();
            prms.Add("@TableName", "T8_V_SysUserList");
            prms.Add("@Fields", "*");
            prms.Add("@OrderField", "CreateTime DESC");
            prms.Add("@pageSize", userEntity.PageSize);
            prms.Add("@pageIndex", userEntity.PageIndex);
            prms.Add("@Records", "", DbType.Int32, ParameterDirection.Output);

            StringBuilder sbWhere = new StringBuilder(" 1=1 ");
            if (userEntity != null)
            {
                if (!string.IsNullOrEmpty(userEntity.UserName))
                {
                    sbWhere.Append($" AND UserName like '%{userEntity.UserName}%'");
                }

                if (!string.IsNullOrEmpty(userEntity.UserLoginName))
                {
                    sbWhere.Append($" AND UserLoginName like '%{userEntity.UserLoginName}%'");
                }
            }

            prms.Add("@sqlWhere", sbWhere.ToString());

            return base.ExecuteFor(conn =>
            {
                List<SystemUserViewEntity> list = conn.Query<SystemUserViewEntity>("sp_pager05", prms, null, true, null, CommandType.StoredProcedure).ToList();
                return new ParamtersForDBPageEntity<SystemUserViewEntity>()
                {
                    PageIndex = userEntity.PageIndex,
                    PageSize = userEntity.PageSize,
                    Total = prms.Get<int>("@Records"),
                    DataList = list
                };
            });
        }

        /// <summary>
        /// 新加或更新系统用户表
        /// </summary>
        /// <param name="userTable">开卷Web服务中获取的所有员工信息表</param>
        /// <returns></returns>
        public bool InsertOrUpdate(DataTable userTable)
        {
            if (userTable == null || userTable.Rows.Count == 0)
            {
                return false;
            }

            DataTable customtable = new DataTable();
            customtable.Columns.Add(new DataColumn("UserID", typeof(string)));
            customtable.Columns.Add(new DataColumn("UserLoginName", typeof(string)));
            customtable.Columns.Add(new DataColumn("UserName", typeof(string)));                    
            customtable.Columns.Add(new DataColumn("UserSex", typeof(int)));
            customtable.Columns.Add(new DataColumn("UserTel", typeof(string)));
            customtable.Columns.Add(new DataColumn("UserMobile", typeof(string)));
            customtable.Columns.Add(new DataColumn("UserMail", typeof(string)));
            customtable.Columns.Add(new DataColumn("UserDeptID", typeof(int)));           

            try
            {
                DataRow row = null;
                foreach (DataRow dr in userTable.Rows)
                {
                    row = customtable.NewRow();
                    row["UserID"] = dr["User_ID"].ToString();
                    row["UserLoginName"] = dr["User_LoginName"].ToString();
                    row["UserName"] = dr["User_Name"].ToString();
                    row["UserDeptID"] = dr["User_DeptID"].ToString();
                    customtable.Rows.Add(row);
                }

                SqlParameter[] prms =
                {
                    new SqlParameter("@SystemUserTable",SqlDbType.Structured),
                    new SqlParameter("@ReturnValue","")
                };
                prms[0].Value = customtable;
                prms[1].Direction = ParameterDirection.ReturnValue;


                SQlHelper.ExecuteNonQuery(base.GetSqlServerConnectString(), CommandType.StoredProcedure, "T8_Proc_InsertOrUpdateSystemUser", prms);
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
        /// 更改用户角色 
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public bool InsertOrUpdateRole(string userID,int roleID)
        {
            string updateSQL = "UPDATE T8_System_User_Role_Rel SET RoleID=@RoleID  WHERE UserID=@UserID";
            string insertSQL = "INSERT INTO T8_System_User_Role_Rel(UserID,RoleID)VALUES(@UserID,@RoleID)";
            string existSQL = "SELECT COUNT(1) AS Cnt FROM T8_System_User_Role_Rel WHERE UserID=@UserID";

            return base.ExecuteFor(conn =>
            {
                var prms = new
                {
                    RoleID=roleID,
                    UserID = userID
                };
                DataTable resultTable = conn.QueryDT(existSQL, prms);
                if (resultTable.Rows[0]["Cnt"].ToString().ToInt() > 0)
                {
                    return conn.Execute(updateSQL, prms) > 0;
                }
                else
                {
                    return conn.Execute(insertSQL, prms) > 0;
                }
            });
        }      

        /// <summary>
        /// 用户登录  
        /// 返回第一个参数 状态 0系统异常 1允许登录 2用户未配置权限
        /// 返回第二个参数 用户实体
        /// 返回第三个参数 权限功能菜单列表
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public Tuple<int, SystemUserViewEntity,List<SystemFunctionEntity>> UserLogin(string userID)
        {
            //string queryUserSQL = "SELECT * FROM T8_V_SysUserList WHERE UserID=@UserID";
            string quertFunSQL = "SELECT * FROM T8_V_SysUserFunList WHERE UserID=@UserID";
            string updateUserLoginSQL = "UPDATE T8_SystemUsers SET LastLoginTime=GetDate() WHERE UserID=@UserID";

            return base.ExecuteFor(conn =>
            {
                var prms = new
                {
                    UserID = userID
                };

                SystemUserViewEntity systemUserView = conn.Query<SystemUserViewEntity>(quertFunSQL, prms).FirstOrDefault();
                if (systemUserView == null || systemUserView.RoleID == 0)
                {
                    return new Tuple<int, SystemUserViewEntity, List<SystemFunctionEntity>>(2, null, null);
                }

                List<SystemFunctionEntity> funlist = conn.Query<SystemFunctionEntity>(quertFunSQL, prms).ToList();
                List<SystemFunctionEntity> groupFunList = funlist.FindAll(item => item.FunctionLevel == 1).OrderBy(item => item.FunctionOrder).ToList();
                foreach (SystemFunctionEntity groupFun in groupFunList)
                {
                    groupFun.ChildFunList = funlist.FindAll(item => item.FunctionParentID == groupFun.FunctionID).OrderBy(item => item.FunctionOrder).ToList();
                }
                conn.Execute(updateUserLoginSQL, prms);

                return new Tuple<int, SystemUserViewEntity, List<SystemFunctionEntity>>(1, systemUserView, groupFunList);
            });
        }
       
    }
}
