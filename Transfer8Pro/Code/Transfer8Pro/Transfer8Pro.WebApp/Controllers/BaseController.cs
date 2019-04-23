using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Transfer8Pro.Entity;
using Transfer8Pro.Entity.OB;
using Transfer8Pro.WebApp.Infrastructure;
using Transfer8Pro.WebApp.Infrastructure.Filters;
using Transfer8Pro.WebApp.Infrastructure.Results;
using Transfer8Pro.WebApp.Models;

namespace Transfer8Pro.WebApp.Controllers
{
    [ActionAuthFilter]
    public class BaseController : Controller
    {
        /// <summary>
        /// 当前用户Session信息
        /// </summary>
        public UserSessionEntity CurrSystemUser
        {
            get
            {
                UserSessionEntity userSession = Session[G.CurrUserKey] as UserSessionEntity;
                if (userSession != null)
                {
                    return userSession;                   
                }
                return null;
            }
        }     

        public JsonNetResult OkResult()
        {
            return new JsonNetResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                ContentEncoding = Encoding.UTF8,
                Data = new ResponseModel { Status = 1 }
            };
        }

        public JsonNetResult OkResult(string msg)
        {
            return new JsonNetResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                ContentEncoding = Encoding.UTF8,
                Data = new ResponseModel { Status = 1, Msg = msg }
            };
        }

        public JsonNetResult OkResult(object data)
        {
            return new JsonNetResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                ContentEncoding = Encoding.UTF8,
                Data = new ResponseModel { Status = 1, Data = data }
            };
        }

        public JsonNetResult OkResult(string msg, object data)
        {           
            return new JsonNetResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                ContentEncoding = Encoding.UTF8,
                Data = new ResponseModel { Status = 1, Msg = msg, Data = data }
            };
        }

        public JsonNetResult ErrorResult()
        {         
            return new JsonNetResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                ContentEncoding = Encoding.UTF8,
                Data = new ResponseModel { Status = -1 }
            };
        }

        public JsonNetResult ErrorResult(string msg)
        {          
            return new JsonNetResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                ContentEncoding = Encoding.UTF8,
                Data = new ResponseModel { Status = -1, Msg = msg }
            };
        }

        public JsonNetResult ErrorResult(object data)
        {         
            return new JsonNetResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                ContentEncoding = Encoding.UTF8,
                Data = new ResponseModel { Status = -1, Data = data }
            };
        }

        public JsonNetResult ErrorResult(string msg, object data)
        {         
            return new JsonNetResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                ContentEncoding = Encoding.UTF8,
                Data = new ResponseModel { Status = -1, Msg = msg, Data = data }
            };
        }
    }
}