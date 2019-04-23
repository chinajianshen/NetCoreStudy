using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Transfer8Pro.Entity
{
    /// <summary>
    /// Quartz作业实体
    /// </summary>
    [Serializable]
    public class QuartzJobEntity
    {
        public string JobKey { get; set; }

        public TaskStatus TaskStatus { get; set; }    
        
        public string Cron { get; set; }

        /// <summary>
        /// 唯一标识码
        /// </summary>
        public string UniqueCode { get; set; }        
    }
}
