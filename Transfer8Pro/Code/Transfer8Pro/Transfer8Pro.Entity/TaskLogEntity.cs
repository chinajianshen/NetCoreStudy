using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Transfer8Pro.Entity
{
    /// <summary>
    /// 任务日志实体
    /// </summary>
    [Serializable]
    public class TaskLogEntity
    {
        public int TaskLogID { get; set; }

        public string TaskID { get; set; }    

        public DataTypes DataType { get; set; }

        public CycleTypes CycleType { get; set; }       

        public DateTime ModifyTime { get; set; }

        public DateTime CreateTime { get; set; }   
        
        public string FileName { get; set; }      
    }

    /// <summary>
    /// 任务日志明细实体
    /// </summary>
    [Serializable]
    public class TaskLogDetailEntity:ParamtersForDBPageEntity
    {
        public int TaskLogDetailID { get; set; }

        public string TaskID { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        /// <summary>
        /// 耗时
        /// </summary>       
        public string ElapsedTime
        {
            get
            {
                if (StartTime != DateTime.MinValue && EndTime != DateTime.MinValue)
                {
                    TimeSpan ts = EndTime - StartTime;
                    string result = $"{(ts.TotalSeconds / 60 < 1 ? "0分" : ((int)(ts.TotalSeconds / 60)).ToString() + "分")}{ts.TotalSeconds % 60 }秒";
                    return result;
                }
                else
                {
                    return "";
                }
            }
        }

        public string FileName { get; set; }

        public TaskExecutedStatus TaskExecutedStatus { get; set; }

        public DateTime CreateTime { get; set; }

        public string ErrorContent { get; set; }

        /// <summary>
        /// 上传状态 0未上传 1已上传 2上传异常
        /// </summary>
        public int UploadStatus { get; set; }

        public string FtpUserName { get; set; }
    }

    [Serializable]
    public class TaskLogViewEntity: TaskLogEntity
    {
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }      

        public TaskExecutedStatus TaskExecutedStatus { get; set; }

        /// <summary>
        /// 耗时
        /// </summary>
        public string ElapsedTime
        {
            get
            {
                if (StartTime != DateTime.MinValue && EndTime != DateTime.MinValue)
                {
                    TimeSpan ts = EndTime - StartTime;
                    string result = $"{(ts.TotalSeconds / 60 < 1 ? "0分" : ((int)(ts.TotalSeconds / 60)).ToString() + "分")}{ts.TotalSeconds % 60 }秒";
                    return result;
                }
                else
                {
                    return "";
                }
            }
        }

        public string TaskName { get; set; }

        /// <summary>
        /// 上传状态 0未上传 1已上传 2上传异常
        /// </summary>
        public int UploadStatus { get; set; }
    }

    [Serializable]
    public class TaskLogDetailViewEntity : TaskLogDetailEntity
    {
        public DataTypes DataType { get; set; }

        public CycleTypes CycleType { get; set; }

        public string TaskName { get; set; }

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

        public string TaskExecutedStatusName
        {
            get
            {
                return TaskExecutedStatus.ToString();
            }
        }

        public string SupName { get; set; }
    }
}
