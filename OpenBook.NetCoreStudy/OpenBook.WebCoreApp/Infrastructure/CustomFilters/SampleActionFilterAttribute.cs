using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace OpenBook.WebCoreApp.Infrastructure.CustomFilters
{
    /// <summary>
    /// 在属性上实现 IFilterFactory
    /// 1不需要任何参数。
    /// 2具备需要由 DI 填充的构造函数依赖项。
    /// </summary>
    public class SampleActionFilterAttribute: TypeFilterAttribute
    {
        public SampleActionFilterAttribute() : base(typeof(SampleActionFilterImpl)) { }

        private class SampleActionFilterImpl : IActionFilter
        {
            private readonly ILogger _logger;

            public SampleActionFilterImpl(ILoggerFactory loggerFactory)
            {
                _logger = loggerFactory.CreateLogger<SampleActionFilterAttribute>();
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {
                _logger.LogInformation("Business action starting...");
            }

            public void OnActionExecuting(ActionExecutingContext context)
            {
                _logger.LogInformation("Business action completed.");
            }
        }
    }
}
