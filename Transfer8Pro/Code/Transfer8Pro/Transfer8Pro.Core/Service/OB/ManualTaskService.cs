using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Transfer8Pro.DAO.OB;
using Transfer8Pro.Entity;
using Transfer8Pro.Entity.OB;

namespace Transfer8Pro.Core.Service.OB
{
    public class ManualTaskService
    {
        private readonly ManualTaskDAO dao;

        public ManualTaskService()
        {
            dao = new ManualTaskDAO();
        }

        /// <summary>
        /// 新建 1成功 0失败 2存在未执行任务
        /// </summary>
        /// <param name="manualTaskEntity"></param>
        /// <returns></returns>
        public int Insert(ManualTaskEntity manualTaskEntity)
        {
            return dao.Insert(manualTaskEntity);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="manualTaskID"></param>
        /// <returns></returns>
        public bool Delete(int manualTaskID)
        {
            return dao.Delete(manualTaskID);
        }

        /// <summary>
        /// 更新执行中的手动任务状态
        /// </summary>
        /// <param name="manualTasks"></param>
        /// <returns></returns>
        public bool UpdateStatus(List<ManualTaskEntity> manualTasks)
        {
            return dao.UpdateStatus(manualTasks);
        }

        /// <summary>
        /// 获取等待执行手动任务列表
        /// </summary>
        /// <returns></returns>
        public List<ManualTaskEntity> GetWaitingManualTaskList(ManualTaskEntity manualTask)
        {
            return dao.GetWaitingManualTaskList(manualTask);
        }

        /// <summary>
        /// 获取手动任务列表
        /// </summary>
        /// <param name="taskEntity"></param>      
        /// <returns></returns>
        public ParamtersForDBPageEntity<ManualTaskViewEntity> GeManualTaskList(ManualTaskEntity taskEntity)
        {
            return dao.GeManualTaskList(taskEntity);
        }
    }
}
