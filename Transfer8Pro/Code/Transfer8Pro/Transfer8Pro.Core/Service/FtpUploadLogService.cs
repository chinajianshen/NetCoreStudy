using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Transfer8Pro.Entity;
using Transfer8Pro.DAO;

namespace Transfer8Pro.Core.Service
{
    public class FtpUploadLogService
    {
        private FtpUploadLogDAO dao = null;

        public FtpUploadLogService()
        {
            dao = new FtpUploadLogDAO();
        }

        public ParamtersForDBPageEntity<FtpUploadLogEntity> GetFtpUploadList(FtpUploadLogEntity ftpUploadLogEntity, int pageIndex, int pageSize)
        {
            return dao.GetFtpUploadList(ftpUploadLogEntity, pageIndex, pageSize);
        }

        /// <summary>
        /// 根据文件名查找上传文件数据
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public FtpUploadLogEntity Find(string fileName)
        {
            return dao.Find(fileName);
        }

        /// <summary>
        /// 根据ID查找上传文件数据
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public FtpUploadLogEntity Find(int ftpUploadID)
        {
            return dao.Find(ftpUploadID);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="uploadLogEntity"></param>
        /// <returns></returns>
        public bool Insert(FtpUploadLogEntity uploadLogEntity)
        {
            return dao.Insert(uploadLogEntity);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="uploadLogEntity"></param>
        /// <returns></returns>
        public bool Insert(FtpUploadLogEntity uploadLogEntity, out int ftpUploadID)
        {
            return dao.Insert(uploadLogEntity, out ftpUploadID);
        }


        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="uploadLogEntity"></param>
        /// <returns></returns>
        public bool Update(FtpUploadLogEntity uploadLogEntity)
        {
            return dao.Update(uploadLogEntity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ftpUploadID"></param>
        /// <param name="uploadEndTime"></param>
        /// <param name="ftpUploadStatus"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public bool UpdateFtpStatus(int ftpUploadID, DateTime uploadEndTime, FtpUploadStatus ftpUploadStatus, string remark)
        {
            return dao.UpdateFtpStatus(ftpUploadID, uploadEndTime, ftpUploadStatus, remark);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ftpUploadID"></param>    
        /// <param name="ftpUploadStatus"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public bool UpdateFtpStatus(int ftpUploadID, FtpUploadStatus ftpUploadStatus, string remark)
        {
            return dao.UpdateFtpStatus(ftpUploadID, ftpUploadStatus, remark);
        }

        /// 删除
        /// </summary>
        /// <param name="ftpUploadID"></param>
        /// <returns></returns>
        public bool Delete(int ftpUploadID)
        {
            return dao.Delete(ftpUploadID); 
        }

        /// <summary>
        /// 获取待上传列表
        /// </summary>
        /// <returns></returns>
        public List<FtpUploadLogEntity> GetWaitUploadLogList()
        {
            return dao.GetWaitUploadLogList();
        }
        /// <summary>
        /// 更新FTP上传状态
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool UpdateUploadStataus(List<FtpUploadLogEntity> list)
        {
            return dao.UpdateUploadStataus(list);
        }
    }
}
