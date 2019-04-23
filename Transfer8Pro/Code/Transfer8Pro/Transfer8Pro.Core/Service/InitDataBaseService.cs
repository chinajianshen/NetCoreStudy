using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Transfer8Pro.DAO;
using Transfer8Pro.Entity;

namespace Transfer8Pro.Core.Service
{
    public class InitDataBaseService
    {
        InitDataBaseDAO dao = null;
        public InitDataBaseService()
        {
            dao = new InitDataBaseDAO();
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <returns></returns>
        public bool InitDbData()
        {
            return dao.InitDbData();
        }

        /// <summary>
        ///  保存从开卷接口下载的配置数据
        /// </summary>
        /// <param name="sysConfigList"></param>
        /// <param name="ftpConfigList"></param>
        /// <param name="taskList"></param>
        /// <returns></returns>
        public bool SaveDownloadConfig(List<SystemConfigEntity> sysConfigList, List<FtpConfigEntity> ftpConfigList, List<TaskEntity> taskList)
        {
            return dao.SaveDownloadConfig(sysConfigList, ftpConfigList, taskList);
        }
    }
}
