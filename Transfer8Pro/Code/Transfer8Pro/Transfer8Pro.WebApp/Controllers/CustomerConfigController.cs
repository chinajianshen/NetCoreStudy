using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Transfer8Pro.Core.Service.OB;
using Transfer8Pro.Entity;
using Transfer8Pro.Entity.OB;

namespace Transfer8Pro.WebApp.Controllers
{
    public class CustomerConfigController : BaseController
    {
        private readonly ClientUploadService clientUploadBll;
        private readonly ManualTaskService manualTaskBll;
        public CustomerConfigController()
        {
            clientUploadBll = new ClientUploadService();
            manualTaskBll = new ManualTaskService();
        }


        public ActionResult SystemConfig()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SystemConfigList(SystemConfigViewEntity prms)
        {
            var pageObj = clientUploadBll.GetSystemConfigList(prms);
            var res = JsonObj.ToJson(new { rows = pageObj.DataList, total = pageObj.Total });
            return Content(res);
        }


        public ActionResult FtpConfig()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FtpConfigList(FtpConfigViewEntity prms)
        {
            var pageObj = clientUploadBll.GetFtpConfigList(prms);
            var res = JsonObj.ToJson(new { rows = pageObj.DataList, total = pageObj.Total });
            return Content(res);
        }

        public ActionResult Task()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TaskList(TaskViewEntity prms)
        {
            var pageObj = clientUploadBll.GetTaskList(prms);
            var res = JsonObj.ToJson(new { rows = pageObj.DataList, total = pageObj.Total });
            return Content(res);
        }
                
        public ActionResult TaskShow(string taskID,string ftpUserName)
        {
            TaskViewEntity task = clientUploadBll.GetTask(new TaskViewEntity { TaskID = taskID, FtpUserName = ftpUserName });
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        public ActionResult TaskDetail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TaskDetailList(TaskLogDetailViewEntity prms)
        {
            var pageObj = clientUploadBll.GetTaskLogDetailList(prms);
            var res = JsonObj.ToJson(new { rows = pageObj.DataList, total = pageObj.Total });
            return Content(res);
        }

        public ActionResult FtpDetail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FtpDetailList(FtpUploadLogViewEntity prms)
        {
            var pageObj = clientUploadBll.GetFtpUploadLogList(prms);
            var res = JsonObj.ToJson(new { rows = pageObj.DataList, total = pageObj.Total });
            return Content(res);
        }

        public ActionResult ManualTask()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ManualTaskList(ManualTaskEntity prms)
        {
            var pageObj = manualTaskBll.GeManualTaskList(prms);
            var res = JsonObj.ToJson(new { rows = pageObj.DataList, total = pageObj.Total });
            return Content(res);
        }

        /// <summary>
        /// 添加手动任务
        /// </summary>
        /// <param name="ftpUserName"></param>
        /// <param name="taskID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult InsertManualTask(string ftpUserName,string taskID)
        {
            if (string.IsNullOrEmpty(ftpUserName) || string.IsNullOrEmpty(taskID))
            {
                return ErrorResult("参数值不能为空");
            }

            TaskViewEntity prms = new TaskViewEntity() { FtpUserName = ftpUserName,TaskID = taskID };
            TaskViewEntity task = clientUploadBll.GetTask(prms);
            if (task == null)
            {
                return ErrorResult("系统中不存在该任务");
            }

            ManualTaskEntity prmsManualTask = new ManualTaskEntity();
            prmsManualTask.FtpUserName = task.FtpUserName;
            prmsManualTask.TaskID = task.TaskID;
            prmsManualTask.ManualTaskStatus = ManualTaskStatus.已创建;
            prmsManualTask.CreatePerson = base.CurrSystemUser.SystemUser.UserID;
            int status = manualTaskBll.Insert(prmsManualTask);
            if (status == 1)
            {
                return OkResult("保存成功");
            }
            else  if (status == 2)
            {
                return ErrorResult("该任务已设置重新执行，客户端程序未调用，请耐心等待");
            }
            else
            {
                return ErrorResult("保存失败");
            }            
        }

        [HttpPost]
        public ActionResult DelManualTask(int pid)
        {
            bool isSuccess = manualTaskBll.Delete(pid);
            if (isSuccess)
            {
                return OkResult("删除成功");
            }
            else
            {
                return ErrorResult("删除失败");
            }
        }
    }
}