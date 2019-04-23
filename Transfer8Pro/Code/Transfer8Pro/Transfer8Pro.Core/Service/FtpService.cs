using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Transfer8Pro.DAO;
using Transfer8Pro.Entity;

namespace Transfer8Pro.Core.Service
{
    public class FtpService
    {
        FtpDAO dao = null;
        public FtpService()
        {
            dao = new FtpDAO();
        }

        /// <summary>
        /// 新建或更新
        /// </summary>
        /// <param name="ftpConfigEntity"></param>
        /// <returns></returns>
        public bool InsertOrUpdate(FtpConfigEntity ftpConfigEntity)
        {           
            if (ftpConfigEntity != null)
            {
                ftpConfigEntity.UserPassword = Common.EncryptData(ftpConfigEntity.UserPassword);
            }
            return dao.InsertOrUpdate(ftpConfigEntity);
        }

        /// <summary>
        /// 新建或更新 
        /// </summary>
        /// <param name="ftpConfigEntity"></param>
        /// <returns></returns>
        public bool InsertOrUpdate(List<FtpConfigEntity> ftpConfigList)
        {
            if (ftpConfigList != null && ftpConfigList.Count > 0)
            {
                foreach (var item in ftpConfigList)
                {
                    item.UserPassword = Common.EncryptData(item.UserPassword);
                }
            }
            return dao.InsertOrUpdate(ftpConfigList);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="ftpConfigEntity"></param>
        /// <returns></returns>
        public bool Update(FtpConfigEntity ftpConfigEntity)
        {
            return dao.Update(ftpConfigEntity);
        }

        /// <summary>
        /// 删除 
        /// </summary>
        /// <param name="ftpID"></param>
        /// <returns></returns>
        public bool Delete(int ftpID)
        {
            return dao.Delete(ftpID);
        }

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <param name="ftpID"></param>
        /// <returns></returns>
        public FtpConfigEntity Find(int ftpID)
        {
            FtpConfigEntity ftpConfigEntity = dao.Find(ftpID);
            if (ftpConfigEntity != null)
            {
                ftpConfigEntity.UserPassword = Common.DecryptData(ftpConfigEntity.UserPassword);
            }
            return ftpConfigEntity;
        }

        /// <summary>
        /// 获取主FTP数据
        /// </summary>    
        /// <returns></returns>
        public FtpConfigEntity GetFirstFtpInfo()
        {
            FtpConfigEntity ftpConfigEntity = dao.GetFirstFtpInfo();
            if (ftpConfigEntity != null)
            {
                ftpConfigEntity.UserPassword = Common.DecryptData(ftpConfigEntity.UserPassword);
            }
            return ftpConfigEntity;
        }

        /// <summary>
        /// 获取备用FTP数据
        /// </summary>
        /// <returns></returns>
        public FtpConfigEntity GetSecondFtpInfo()
        {
            FtpConfigEntity ftpConfigEntity = dao.GetSecondFtpInfo();
            if (ftpConfigEntity != null)
            {
                ftpConfigEntity.UserPassword = Common.DecryptData(ftpConfigEntity.UserPassword);
            }
            return ftpConfigEntity;
        }

        /// <summary>
        /// 获取待上传列表
        /// </summary>
        /// <returns></returns>
        public List<FtpConfigEntity> GetWaitUpload()
        {
            return dao.GetWaitUpload();
        }

        /// 更新FTP配置上传状态
        /// </summary>
        /// <param name="ftpConfigEntity"></param>
        /// <returns></returns>
        public bool UpdateUploadStataus(List<FtpConfigEntity> ftpConfigList)
        {
            return dao.UpdateUploadStataus(ftpConfigList);
        }
    }
}
