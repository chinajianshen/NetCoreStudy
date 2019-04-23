using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Transfer8Pro.WebApp.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index(string error)
        {
            var errorTitle = "网站内部错误，请稍后再尝试";
            ViewData["Title"] = errorTitle;
            ViewData["Description"] = error;
            //return Content(errorTitle, null, System.Text.Encoding.GetEncoding("GB2312"));
            return View();
        }

        public ActionResult HttpError404(string error)
        {
            var errorTitle = "HTTP 404 - 无法找到文件";
            ViewData["Title"] = errorTitle;
            ViewData["Description"] = error;
            //return Content(errorTitle, null, System.Text.Encoding.GetEncoding("GB2312"));  
            return View();
        }

        public ActionResult HttpError500(string error)
        {
            var errorTitle = "HTTP 500 - 内部服务器错误";
            ViewData["Title"] = errorTitle;
            ViewData["Description"] = error;
            //return Content(errorTitle, null, System.Text.Encoding.GetEncoding("GB2312"));
            return View();
        }

        public ActionResult General(string error)
        {
            var errorTitle = "HTTP 发生错误";
            ViewData["Title"] = errorTitle;
            ViewData["Description"] = error;
            //return Content(errorTitle, null, System.Text.Encoding.GetEncoding("GB2312"));
            return View();
        }

    }
}