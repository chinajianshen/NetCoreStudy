using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Transfer8Pro.Entity
{
    /// <summary>
    /// Ftp类
    /// </summary>   
    [Serializable]
    public class FtpConfigEntity: ParamtersForDBPageEntity
    {
        public int FtpID { get; set; }
        /// <summary>
        /// FTP服务器地址 
        /// </summary>
        public string ServerAddress { get; set; }

        public string UserName { get; set; }

        public string UserPassword { get; set; }

        /// <summary>
        /// 导出文件目录
        /// </summary>
        public string ExportFileDirectory { get; set; }

        /// <summary>
        /// FTP服务器文件目录
        /// </summary>
        public string ServerDirectory { get; set; }

        /// <summary>
        /// 类型 1主FTP 2备用FTP
        /// </summary>
        public int FtpType { get; set; }

        /// <summary>
        /// 上传状态 0未上传 1已上传 2上传异常
        /// </summary>
        public int UploadStatus { get; set; }
      
        public FtpConfigEntity Clone()
        {
            return this.MemberwiseClone() as FtpConfigEntity;
        }
    }

    [Serializable]
    public class FtpConfigViewEntity : FtpConfigEntity
    {
        public DateTime UploadTime { get; set; }

        public string FtpUserName { get; set; }

        public string FtpEncryptKey { get; set; }
    }
}
