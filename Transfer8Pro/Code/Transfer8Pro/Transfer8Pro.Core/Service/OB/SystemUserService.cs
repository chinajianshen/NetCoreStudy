using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Transfer8Pro.DAO.OB;
using Transfer8Pro.Entity;
using Transfer8Pro.Entity.OB;

namespace Transfer8Pro.Core.Service.OB
{
    public class SystemUserService
    {
        private SystemUserDAO dao;
        public SystemUserService()
        {
            dao = new SystemUserDAO();
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="taskEntity"></param>       
        /// <returns></returns>
        public ParamtersForDBPageEntity<SystemUserViewEntity> GetList(SystemUserViewEntity userEntity)
        {
            return dao.GetList(userEntity);
        }

        /// <summary>
        /// 新加或更新系统用户表
        /// </summary>
        /// <param name="userTable">开卷Web服务中获取的所有员工信息表</param>
        /// <returns></returns>
        public bool InsertOrUpdate(DataTable userTable)
        {
            return dao.InsertOrUpdate(userTable);
        }

        /// <summary>
        /// 更改用户角色
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public bool InsertOrUpdateRole(string userID, int roleID)
        {
            return dao.InsertOrUpdateRole(userID, roleID);
        }

        /// <summary>
        /// 用户登录  
        /// 返回第一个参数 状态 0系统异常 1允许登录 2用户未配置权限
        /// 返回第二个参数 用户实体
        /// 返回第三个参数 权限功能菜单列表
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public Tuple<int, SystemUserViewEntity, List<SystemFunctionEntity>> UserLogin(string userID)
        {
            return dao.UserLogin(userID);
        }
    }
}
