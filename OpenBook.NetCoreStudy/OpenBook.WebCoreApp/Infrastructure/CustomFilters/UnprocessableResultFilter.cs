using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenBook.WebCoreApp.Infrastructure.CustomFilters
{
    /// <summary>
    /// 除非应用 IExceptionFilter 或 IAuthorizationFilter 并使响应短路，否则会将筛选器应用于操作结果。
    /// </summary>
    public class UnprocessableResultFilter : Attribute, IAlwaysRunResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
           
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is StatusCodeResult statusCodeResult && statusCodeResult.StatusCode == 415)
            {
                context.Result = new ObjectResult("Can't process this!")
                {
                    StatusCode = 422
                };
            }
        }
    }
}
