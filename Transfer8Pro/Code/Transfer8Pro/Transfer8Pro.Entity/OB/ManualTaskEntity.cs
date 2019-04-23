using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Transfer8Pro.Entity.OB
{
    /// <summary>
    /// 手动执行任务实体
    /// </summary>
    [Serializable]
    public class ManualTaskEntity:ParamtersForDBPageEntity
    {      
        public int ManualTaskID { get; set; }

        public string FtpUserName { get; set; }

        public string TaskID { get; set; }

        public ManualTaskStatus ManualTaskStatus { get; set; }

        public string CreatePerson { get; set; }

        public string Remark { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime? CompletionTime { get; set; }
    }

    [Serializable]
    public class ManualTaskViewEntity: ManualTaskEntity
    {
        public string FtpEncryptKey { get; set; }

        public DataTypes DataType { get; set; }

        public CycleTypes CycleType { get; set; }

        public PosTypes PosTypes { get; set; }

        public string TaskName { get; set; }

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

        public string PosTypeName
        {
            get
            {
                return PosTypes.ToString();
            }
        }

        public string ManualTaskStatusName
        {
            get
            {
                return ManualTaskStatus.ToString();
            }
        }

        /// <summary>
        /// 创建到本次查询间隔分钟数
        /// </summary>
        public int IntervalMinute { get; set; }
    }
}
