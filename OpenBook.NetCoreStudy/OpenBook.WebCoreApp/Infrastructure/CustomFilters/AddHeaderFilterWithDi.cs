using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace OpenBook.WebCoreApp.Infrastructure.CustomFilters
{
    public class AddHeaderFilterWithDi : IResultFilter
    {
        private readonly ILogger _logger;

        public AddHeaderFilterWithDi(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<AddHeaderFilterWithDi>();
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
           
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            var headerName = "OnResultExecuting";
            context.HttpContext.Response.Headers.Add(headerName, new string[] { "ResultExecutingSuccessfully" });
            _logger.LogInformation($"Header added: {headerName}");

           
        }
    }
}
