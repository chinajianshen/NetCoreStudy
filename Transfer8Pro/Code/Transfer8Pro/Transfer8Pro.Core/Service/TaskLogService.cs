using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Transfer8Pro.DAO;
using Transfer8Pro.Entity;

namespace Transfer8Pro.Core.Service
{
   public class TaskLogService
    {
        private TaskLogDAO dao = null;
        public TaskLogService()
        {
            dao = new TaskLogDAO();
        }

        public ParamtersForDBPageEntity<TaskLogViewEntity> GetTaskLogList(TaskLogViewEntity taskLogEntity, int pageIndex, int pageSize)
        {
            return dao.GetTaskLogList(taskLogEntity, pageIndex, pageSize);
        }

        /// <summary>
        /// 获取日志明细
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public ParamtersForDBPageEntity<TaskLogDetailViewEntity> GetTaskDetailLogList(TaskLogDetailViewEntity taskLogEntity, int pageIndex, int pageSize)
        {
            return dao.GetTaskDetailLogList(taskLogEntity, pageIndex, pageSize);
        }

        /// <summary>
        /// 新建或修改任务日志
        /// </summary>
        /// <param name="taskLogEntity"></param>
        /// <returns></returns>
        public bool InsertOrUpdateLog(TaskLogEntity taskLogEntity, out int taskLogDetailID)
        {
            return dao.InsertOrUpdateLog(taskLogEntity, out taskLogDetailID);
        }

        /// <summary>
        /// 更新成功日志信息
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="taskLogDetailID"></param>
        /// <param name="taskExecutedStatus"></param>    
        /// <returns></returns>
        public bool UpdateSuccessLog(string taskID, int taskLogDetailID, string fileName)
        {
            return dao.UpdateSuccessLog(taskID, taskLogDetailID,fileName);
        }

        /// <summary>
        /// 更新成功日志信息
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="taskLogDetailID"></param>
        /// <param name="taskExecutedStatus"></param>    
        /// <returns></returns>
        public bool UpdateErrorLog(string taskID, int taskLogDetailID, string errorContent)
        {
            return dao.UpdateErrorLog(taskID, taskLogDetailID, errorContent);
        }

        /// <summary>
        /// 获取待上传日志明细列表
        /// </summary>
        /// <returns></returns>
        public List<TaskLogDetailEntity> GetWaitUploadLogList()
        {
            return dao.GetWaitUploadLogList();
        }

        /// <summary>
        /// 更新上传状态
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool UpdateUploadStataus(List<TaskLogDetailEntity> list)
        {
            return dao.UpdateUploadStataus(list);
        }
        }
}
