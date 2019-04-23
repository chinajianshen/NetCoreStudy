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
    public class FtpController : BaseController
    {
        private FtpInfoService ftpInfoBll = null;

        public FtpController()
        {
            ftpInfoBll = new FtpInfoService();
        }

        public ActionResult List()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult List(FtpInfoEntity prms)
        {
            var pageObj = ftpInfoBll.GetList(prms);
            var res = JsonObj.ToJson(new { rows = pageObj.DataList, total = pageObj.Total });
            return Content(res);
        }

        public ActionResult Edit(int? pid)
        {
            FtpInfoEntity ftpInfoEntity = new FtpInfoEntity();
            if (pid.HasValue)
            {
                ftpInfoEntity = ftpInfoBll.Find(pid.Value);
                if (ftpInfoEntity == null)
                {
                   throw new Exception($"{pid}记录系统中不存在");
                }
            }
            else
            {
                ftpInfoEntity.FtpEncryptKey = Guid.NewGuid().ToString("N").ToUpper();
            }
            return View(ftpInfoEntity);
        }

        [HttpPost]
        public ActionResult Edit(FtpInfoEntity ftpInfoEntity)
        {
            int exeresult = 0;
            if (ftpInfoEntity.FtpID > 0)
            {
                exeresult = ftpInfoBll.Update(ftpInfoEntity);
            }
            else
            {
                exeresult = ftpInfoBll.Insert(ftpInfoEntity);
            }

            if (exeresult == 1)
            {
                return OkResult("保存成功");
            }
            else if (exeresult == 2)
            {
                return ErrorResult("Ftp账号已存在");
            }
            else
            {
                return ErrorResult("保存失败");
            }
        }


        [HttpPost]
        public ActionResult Delete(int pid)
        {
            bool isSuccess = ftpInfoBll.Delete(pid);
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