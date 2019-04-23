using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization.Routing;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace OpenBook.WebCoreApp.Infrastructure.CustomMiddleware
{
    /// <summary>
    /// 在筛选器管道中使用中间件
    ///
    /// </summary>
    public class LocalizationPipelineMiddleware
    {
        public void Configure(IApplicationBuilder applicationBuilder)
        {
            var supportedCultures = new[]
            {
                new CultureInfo("en-US"),
                new CultureInfo("fr")
            };

            var options = new RequestLocalizationOptions
            {
                 DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(culture:"en-US",uiCulture:"en-US"),
                 SupportedCultures = supportedCultures,
                 SupportedUICultures = supportedCultures
            };
            options.RequestCultureProviders = new[] { new RouteDataRequestCultureProvider() { Options = options } };
            applicationBuilder.UseRequestLocalization(options);
        }
    }
}
