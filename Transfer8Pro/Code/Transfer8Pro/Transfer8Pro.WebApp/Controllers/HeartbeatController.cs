using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Transfer8Pro.Entity.OB;
using Transfer8Pro.Core.Service.OB;
using Transfer8Pro.Entity;

namespace Transfer8Pro.WebApp.Controllers
{
    public class HeartbeatController : BaseController
    {
        private HeartbeatInfoService heartbeatBll = null;
        public HeartbeatController()
        {
            heartbeatBll = new HeartbeatInfoService();
        }

        // GET: Heartbeat
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            return View();
        }

        [HttpPost]
        public ActionResult List(FtpHeartbeatViewEntity prms)
        {
           var pageObj =  heartbeatBll.GetList(prms);
            var res = JsonObj.ToJson(new { rows = pageObj.DataList, total = pageObj.Total });
            return Content(res);
        }
    }
}