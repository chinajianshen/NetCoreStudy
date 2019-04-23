using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace OpenBook.WebCoreApp.Infrastructure.CustomFilters
{
    public class LogConstantFilter : IActionFilter
    {
        private readonly string _value;
        private readonly ILogger<LogConstantFilter> _logger;

        public LogConstantFilter(string value,ILogger<LogConstantFilter> logger)
        {
            _value = value;
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation(_value);
        }
    }
}
