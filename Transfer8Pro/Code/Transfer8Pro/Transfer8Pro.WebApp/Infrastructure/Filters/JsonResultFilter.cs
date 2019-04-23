using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Transfer8Pro.WebApp.Infrastructure.Results;

namespace Transfer8Pro.WebApp.Infrastructure.Filters
{
    public class JsonResultFilter: ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {        
            if (filterContext.Result is JsonResult && !(filterContext.Result is JsonNetResult))
            {
                var jsonResult = (JsonResult)filterContext.Result;

                filterContext.Result = new JsonNetResult
                {
                    ContentEncoding = jsonResult.ContentEncoding,
                    ContentType = jsonResult.ContentType,
                    Data = jsonResult.Data,
                    JsonRequestBehavior = jsonResult.JsonRequestBehavior
                };
            }
            else
            {
                base.OnActionExecuted(filterContext);
            }           
        }
    }
}