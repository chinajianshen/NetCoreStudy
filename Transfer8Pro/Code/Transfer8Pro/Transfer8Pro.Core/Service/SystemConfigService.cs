using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Transfer8Pro.DAO;
using Transfer8Pro.Entity;

namespace Transfer8Pro.Core.Service
{
    public class SystemConfigService
    {
        private SystemConfigDAO dao;
        public SystemConfigService()
        {
            dao = new SystemConfigDAO();
        }

        /// <summary>
        /// 查询数据库所有配置
        /// </summary>
        /// <returns></returns>
        public List<SystemConfigEntity> GetSystemConfigList()
        {
            return dao.GetSystemConfigList();
        }

        /// <summary>
        /// 获取一条配置
        /// </summary>
        /// <param name="sysConfigID"></param>
        /// <returns></returns>
        public SystemConfigEntity FindSystemConfig(int sysConfigID)
        {
            return dao.FindSystemConfig(sysConfigID);
        }

        /// <summary>
        /// 更新配置状态
        /// </summary>
        /// <param name="sysConfigID"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool UpdateConfigStatus(int sysConfigID, int status)
        {
            return dao.UpdateConfigStatus(sysConfigID, status);
        }

        /// <summary>
        /// 更新系统配置
        /// </summary>
        /// <param name="systemConfig"></param>
        /// <returns></returns>
        public bool Update(SystemConfigEntity systemConfig)
        {
            return dao.Update(systemConfig);
        }

        /// <summary>
        /// 更新系统版本
        /// </summary>
        /// <param name="sysConfigID"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public bool UpdateConfigVersion(int sysConfigID, string version)
        {
            return dao.UpdateConfigVersion(sysConfigID, version);
        }

        /// <summary>
        /// 忽略系统版本 
        /// 忽略该版本，则以后只要该版本系统不会再更新版本
        /// </summary>
        /// <param name="sysConfigID"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public bool IgnoreConfigVersion(int sysConfigID, string version)
        {
            return dao.IgnoreConfigVersion(sysConfigID, version);
        }

        /// <summary>
        /// 更新系统配置
        /// </summary>
        /// <param name="systemConfig"></param>
        /// <returns></returns>
        public bool Insert(SystemConfigEntity systemConfig)
        {
            return dao.Insert(systemConfig);
        }

        public bool UpdateList(List<SystemConfigEntity> systemConfigList)
        {
            return dao.UpdateList(systemConfigList);
        }

        /// <summary>
        /// 获取待上传列表
        /// </summary>
        /// <returns></returns>
        public List<SystemConfigEntity> GetWaitUploadList()
        {
            return dao.GetWaitUploadList();
        }

        /// <summary>
        /// 更新配置上传状态
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool UpdateUploadStataus(List<SystemConfigEntity> list)
        {
            return dao.UpdateUploadStataus(list);
        }
        }
}
