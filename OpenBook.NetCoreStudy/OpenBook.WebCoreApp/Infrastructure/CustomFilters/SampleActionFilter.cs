using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace OpenBook.WebCoreApp.Infrastructure.CustomFilters
{
    // 如果在一个类中同时实现了这两种接口，则仅调用异步方法  异步优先
    public class SampleActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
          
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            context.HttpContext.Response.Headers.Add("SampleActionFilter", new string[] { "Header Added" });
        }
    }

    public class SampleAsyncActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            context.HttpContext.Response.Headers.Add("SampleAsyncActionFilter", new string[] { "Header Added" });
            var resultContext = await next();
        }
    }

    public class Sample2ActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }
    }
}
