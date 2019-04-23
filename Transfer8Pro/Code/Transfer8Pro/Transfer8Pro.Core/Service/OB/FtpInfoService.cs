using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Transfer8Pro.DAO.OB;
using Transfer8Pro.Entity;
using Transfer8Pro.Entity.OB;

namespace Transfer8Pro.Core.Service.OB
{
    public class FtpInfoService
    {
        private FtpInfoDAO dao;
        public FtpInfoService()
        {
            dao = new FtpInfoDAO();
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
            return dao.GetList(taskEntity);
        }

        /// <summary>
        /// 查找一条FTP数据
        /// </summary>
        /// <param name="ftpUserName"></param>
        /// <returns></returns>
        public FtpInfoEntity Find(string ftpUserName)
        {
            return dao.Find(ftpUserName);
        }

        /// <summary>
        /// 查找一条FTP数据
        /// </summary>
        /// <param name="ftpID"></param>
        /// <returns></returns>
        public FtpInfoEntity Find(int ftpID)
        {
            return dao.Find(ftpID);
        }

        /// <summary>
        /// 添加 0失败 1成功 2FtpUserName已存在
        /// </summary>
        /// <param name="ftpInfo"></param>
        /// <returns></returns>
        public int Insert(FtpInfoEntity ftpInfo)
        {
            return dao.Insert(ftpInfo);
        }

        /// <summary>
        /// 修改 0失败 1成功 2FtpUserName已存在
        /// </summary>
        /// <param name="ftpInfo"></param>
        /// <returns></returns>
        public int Update(FtpInfoEntity ftpInfo)
        {
            return dao.Update(ftpInfo);
        }

        /// <summary>
        ///  删除一条记录
        /// </summary>
        /// <param name="ftpID"></param>
        /// <returns></returns>
        public bool Delete(int ftpID)
        {
            return dao.Delete(ftpID);
        }
    }
}
