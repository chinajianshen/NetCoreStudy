using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Transfer8Pro.Entity
{
    /// <summary>
    /// FTP上传记录
    /// </summary>
    [Serializable]
    public class FtpUploadLogEntity:ParamtersForDBPageEntity
    {
        public int FtpUploadID { get; set; }      

        public string FileFullPath { get; set; }

        public string FileName { get; set; }

        public DateTime UploadStartTime { get; set; }

        public DateTime UploadEndTime { get; set; }

        /// <summary>
        /// 耗时
        /// </summary>
        public string ElapsedTime
        {
            get
            {
                if (UploadStartTime != DateTime.MinValue && UploadEndTime != DateTime.MinValue)
                {
                    TimeSpan ts = UploadEndTime - UploadStartTime;
                    string result = $"{(ts.TotalSeconds / 60 < 1 ? "0分" : ((int)(ts.TotalSeconds / 60)).ToString() + "分")}{ts.TotalSeconds % 60 }秒";
                    return result;
                }
                else
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// 上传状态
        /// </summary>
        public FtpUploadStatus FtpUploadStatus { get; set; }

        public DataTypes DataType { get; set; }

        public CycleTypes CycleType { get; set; }

        public DateTime CreateTime { get; set; }

        public string Remark { get; set; }

        /// <summary>
        /// 上传状态 0未上传 1已上传 2上传异常
        /// </summary>
        public int UploadStatus { get; set; }

        public string FtpUserName { get; set; }
    }

    [Serializable]
    public class FtpUploadLogViewEntity: FtpUploadLogEntity
    {
        public DateTime UploadTime { get; set; }

        public string DataTypeName
        {
            get
            {
                return DataType.ToString();
            }
        }

        public string CycleTypeName
        {
            get
            {
                return CycleType.ToString();
            }
        }

        public string FtpUploadStatusName
        {
            get
            {
                return FtpUploadStatus.ToString();
            }
        }
    }
}
