using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Transfer8Pro.Entity.OB
{
    [Serializable]
    public class HeartbeatInfoEntity: ParamtersForDBPageEntity
    {
        public int FtpID { get; set; }

        /// <summary>
        /// 1日采集系统  2T8系统
        /// </summary>
        public int SystemType { get; set; }

        /// <summary>
        /// 最近数据导出心跳时间
        /// </summary>
        public DateTime? RecentDataHeartbeatTime { get; set; }

        /// <summary>
        /// 最近FTP上传心跳时间
        /// </summary>
        public DateTime RecentFtpHeartbeatTime { get; set; }

        public DateTime? ModifyTime { get; set; }

        public DateTime CreateTime { get; set; }       
    }

    [Serializable]
    public class FtpHeartbeatViewEntity : HeartbeatInfoEntity
    {
        public int FtpID { get; set; }

        /// <summary>
        /// 1日采集系统  2T8系统
        /// </summary>
        public int SystemType { get; set; }

        /// <summary>
        /// 最近数据导出心跳时间
        /// </summary>
        public DateTime? RecentDataHeartbeatTime { get; set; }

        /// <summary>
        /// 最近FTP上传心跳时间
        /// </summary>
        public DateTime RecentFtpHeartbeatTime { get; set; }

        public DateTime? ModifyTime { get; set; }

        public DateTime CreateTime { get; set; }      

        public string FtpUserName { get; set; }

        public string FtpFolderName { get; set; }

        public string FtpEncryptKey { get; set; }      

        /// <summary>
        /// 数据导出心跳状态 1正常 2中断
        /// </summary>
        public int RecentDataStatus { get; set; }      

        /// <summary>
        /// Ftp心跳状态 1正常 2中断
        /// </summary>
        public int RecentFtpStatus { get; set; }

        /// <summary>
        /// 心跳状态 1正常（数据导出和FTP都正常才正常） 2(数据导出和FTP一个中断则都是中断)
        /// </summary>
        public int HeartbeatStatus { get; set; }

        /// <summary>
        /// 书店名称
        /// </summary>
        public string SupName { get; set;  }
    }

    [Serializable]
    public class NotifyInfoEntity
    {
       public int NotifyID { get; set; }

        public int FtpID { get; set; }

        /// <summary>
        /// 通知状态 1已通知 2通知失败
        /// </summary>
        public int NotifyStatus { get; set; }

        public DateTime NotifyTime { get; set; }
    }

    [Serializable]
    public class NotifyLogInfoEntity
    {
        public int NotifyLogID { get; set; }

        public int FtpID { get; set; }
     
        public string NotifyMessage { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
