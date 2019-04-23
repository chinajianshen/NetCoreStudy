using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Transfer8Pro.DAO.OB;
using Transfer8Pro.Entity.OB;

namespace Transfer8Pro.Core.Service.OB
{
   public class SystemFunctionService
    {
        private readonly SystemFunctionDAO dao;
        public SystemFunctionService()
        {
            dao = new SystemFunctionDAO();
        }

        /// 获取所有记录
        /// </summary>
        /// <returns></returns>
        public List<SystemFunctionEntity> GetList()
        {
            return dao.GetList();
        }
        }
}
