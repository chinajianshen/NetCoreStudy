using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace OpenBook.WebCoreApp.Infrastructure.CustomFilters
{
    public class ShortCircuitingResourceFilterAttribute : Attribute, IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
          
            if (context.HttpContext.Request.Path.ToString().Contains("NamespaceRouting") && context.HttpContext.Request.Query.ContainsKey("a1"))
            {
                context.Result = new ContentResult()
                {
                     StatusCode=404,
                    Content = "Resource unavailable - header should not be set"
                };
            }
          
        }
    }
}
