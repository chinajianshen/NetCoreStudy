using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Transfer8Pro.CommunalWebAPI.Infrastructure.Filters
{   
    public class CustomModelValidActionFilter: ActionFilterAttribute
    {
        //https://www.cnblogs.com/lijingran/p/6420397.html
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            //base.OnActionExecuting(actionContext);
            if (!actionContext.ModelState.IsValid)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, actionContext.ModelState);
            }
        }   
    }

    public class CustomEncryptActionFilter: ActionFilterAttribute
    {
        public override async Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            //return base.OnActionExecutedAsync(actionExecutedContext, cancellationToken);
            var key = 10;
           
            var responseBody = await actionExecutedContext.Response.Content.ReadAsByteArrayAsync();

            actionExecutedContext.Response.Content = new ByteArrayContent(responseBody); //将结果赋值给Response的Content

            for (int i = 0; i < responseBody.Length; i++)
            {
                responseBody[i] = (byte)(responseBody[i] ^ key); //对每一个Byte做异或运算
            }

            actionExecutedContext.Response.Content = new ByteArrayContent(responseBody); //将结果赋值给Response的Content

            actionExecutedContext.Response.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("Encrypt/Bytes"); //并修改Content-Type
        }
    }
}