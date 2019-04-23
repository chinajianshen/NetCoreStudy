using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Transfer8Pro.Entity;
using Transfer8Pro.WebApp.Infrastructure.Results;

namespace Transfer8Pro.WebApp.Infrastructure.Filters
{
    [AttributeUsage( AttributeTargets.Class | AttributeTargets.Method,AllowMultiple = true)]
    public class ActionAuthFilter: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session[G.CurrUserKey] == null)
            {
                if (filterContext.HttpContext.Request.HttpMethod.ToUpper() == "GET")
                {
                    filterContext.Result = new RedirectResult("/Auth/Index");
                }
                else
                {
                    filterContext.Result = new ContentResult();
                    filterContext.HttpContext.Response.Clear();
                    filterContext.HttpContext.Response.Write(JsonObj.ToJson(new ResponseModel { Status =-1,Msg= "登录凭证已过期，请重新登录！" }));
                }
                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}