using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using Transfer8Pro.Utils;

namespace Transfer8Pro.CommunalWebAPI.Infrastructure.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            //base.OnException(actionExecutedContext);
            //如果截获异常为我们自定义，可以处理的异常则通过我们自己的规则处理
            //if (actionExecutedContext.Exception is DemoException)
            //{
            //    //TODO:记录日志
            //    actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(
            //            HttpStatusCode.BadRequest, new { Message = actionExecutedContext.Exception.Message });
            //}
            //else
            //{
            //    //如果截获异常是我没无法预料的异常，则将通用的返回信息返回给用户，避免泄露过多信息，也便于用户处理

            //    //TODO:记录日志
            //    actionExecutedContext.Response =
            //            actionExecutedContext.Request.CreateResponse(HttpStatusCode.InternalServerError,
            //                new { Message = "服务器被外星人拐跑了！" });
            //}

            LogUtil.WriteLog(actionExecutedContext.Exception);
        }
    }
}