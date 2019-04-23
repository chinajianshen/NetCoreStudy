using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Transfer8Pro.Core.Service.OB;
using Transfer8Pro.Entity;
using Transfer8Pro.Entity.OB;
using Transfer8Pro.Utils;
using Transfer8Pro.WebApp.Infrastructure;
using Transfer8Pro.WebApp.Infrastructure.Results;
using Transfer8Pro.WebApp.Infrastructure.WebServices;
using Transfer8Pro.WebApp.Models;

namespace Transfer8Pro.WebApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly SystemUserService userBll;

        public AuthController()
        {
            userBll = new SystemUserService();
        }


        // GET: Auth
        public ActionResult Index()
        {          
            return View();
        }

        [HttpPost]
        public ActionResult Login(string userName, string passWord, string validateCode)
        {          
            if (string.IsNullOrEmpty(validateCode))
            {
                return ErrorResult("请输入验证码");
            }

            if (validateCode.ToInt() != DateTime.Now.Day)
            {
                return ErrorResult("验证码错误");
            }

            string userID;            
            if (new PassportServiceProxy().TryLogin(userName, passWord, ConfigHelper.GetConfig("SystemFlag", "T8System"), out userID))
            {
                Tuple<int, SystemUserViewEntity, List<SystemFunctionEntity>> result = userBll.UserLogin(userID);
                if (result == null)
                {
                    return ErrorResult("登录异常");
                }

                if (result.Item1 == 1)
                {
                    UserSessionEntity userSessionEntity = new UserSessionEntity();
                    userSessionEntity.SystemUser = result.Item2;
                    userSessionEntity.SystemFunList = result.Item3;
                    Session[G.CurrUserKey] = userSessionEntity;
                    return OkResult();
                }
                else if (result.Item1 == 2)
                {
                    return ErrorResult("用户未授权，无法登录系统");
                }
                else
                {
                    return ErrorResult("登录异常");
                }
            }
            else
            {

                return ErrorResult("用户名或密码错误");
            }
        }


        [HttpPost]
        public ActionResult Logout()
        {
            Session.Clear();
            return OkResult();
        }

        private JsonNetResult ErrorResult(string msg)
        {
            return new JsonNetResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                ContentEncoding = Encoding.UTF8,
                Data = new ResponseModel { Status = -1, Msg = msg }
            };
        }

        private JsonNetResult OkResult()
        {
            return new JsonNetResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                ContentEncoding = Encoding.UTF8,
                Data = new ResponseModel { Status = 1 }
            };
        }
    }
}