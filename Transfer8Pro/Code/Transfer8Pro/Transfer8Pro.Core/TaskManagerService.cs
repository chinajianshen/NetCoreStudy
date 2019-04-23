using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Transfer8Pro.Core
{
    /// <summary>
    /// 任务管理服务
    /// </summary>
    public class TaskManagerService
    {      
        public static void Start()
        {           
            QuartzBase.StartScheduler();
        }

        public static void Stop()
        {
            QuartzBase.StopSchedule();           
        }

        public static void ReStart()
        {
            QuartzBase.StopSchedule();
            QuartzBase.ClearScheduleData();
            QuartzBase.StartScheduler();
        }
    }
}
