using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Transfer8Pro.DAO.OB;
using Transfer8Pro.Entity;
using Transfer8Pro.Entity.OB;

namespace Transfer8Pro.Core.Service.OB
{
    public class SystemRoleService
    {
        private readonly SystemRoleDAO dao;
        public SystemRoleService()
        {
            dao = new SystemRoleDAO();
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="taskEntity"></param>       
        /// <returns></returns>
        public ParamtersForDBPageEntity<SystemRoleEntity> GetList(SystemRoleEntity prmsEntity)
        {
            return dao.GetList(prmsEntity);
        }

        /// <summary>
        /// 获取所有记录
        /// </summary>
        /// <returns></returns>
        public List<SystemRoleEntity> GetList()
        {
            return dao.GetList();
        }

        /// <summary>
        /// 变更角色和功能菜单关联
        /// </summary>
        /// <param name="roleRelFunList"></param>
        /// <returns></returns>
        public bool ChangeRole_Fun_Rel(List<SystemRoleRelFunEntity> roleRelFunList)
        {
            return dao.ChangeRole_Fun_Rel(roleRelFunList);
        }

        /// <summary>
        /// 根据该角色下已设置所有菜单
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public List<SystemRoleRelFunEntity> GetRoleRelFunList(int roleID)
        {
            return dao.GetRoleRelFunList(roleID);
        }
    }
}
