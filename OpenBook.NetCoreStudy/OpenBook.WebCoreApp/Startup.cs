using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenBook.WebCoreApp.Infrastructure.CustomApplicationModel;
using OpenBook.WebCoreApp.Infrastructure.CustomFilters;
using OpenBook.WebCoreApp.Infrastructure.CustomTagHelpComponents;

namespace OpenBook.WebCoreApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc(options => {
                options.Conventions.Add(new ApplicationDescription("My Application Description"));
                //options.Conventions.Add(new NamespaceRoutingConvention());
                options.Filters.Add(new AddHeaderAttribute("services.AddMvc", "Header Added"));
                options.Filters.Add(typeof(SampleActionFilter));
                options.Filters.Add(typeof(SampleAsyncActionFilter));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //services.AddTransient<ITagHelperComponent, AddressStyleTagHelperComponent>();
            //services.AddTransient<ITagHelperComponent, AddressScriptTagHelperComponent>();
            services.AddTransient<ITagHelperComponent, AddressTagHelperComponent>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                //app.UseExceptionHandler(errop =>
                //{
                //    errop.Run(async context =>
                //    {
                //        context.Response.StatusCode = 500;
                //        context.Response.ContentType = "text/html";
                //        await context.Response.WriteAsync("<html lang=\"en\"><body>\r\n");
                //        await context.Response.WriteAsync("ERROR!<br><br>\r\n");


                //        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

                //        // Use exceptionHandlerPathFeature to process the exception (for example, 
                //        // logging), but do NOT expose sensitive error information directly to 
                //        // the client.

                //        if (exceptionHandlerPathFeature?.Error is FileNotFoundException)
                //        {
                //            await context.Response.WriteAsync("File error thrown!<br><br>\r\n");
                //        }

                //        await context.Response.WriteAsync("<a href=\"/\">Home</a><br>\r\n");
                //        await context.Response.WriteAsync("</body></html>\r\n");
                //        await context.Response.WriteAsync(new string(' ', 512)); // IE padding
                //    });
                //});


                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            //app.UseResponseCompression();

            //app.Use(async (context, next) =>
            //{
            //    // Do work that doesn't write to the Response.
            //    await next.Invoke();
            //    // Do logging or other work that doesn't write to the Response.
            //});

            //委托终止了管道
            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("");
            //});

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=TagForm}/{id?}");
            });
        }
    }
}
