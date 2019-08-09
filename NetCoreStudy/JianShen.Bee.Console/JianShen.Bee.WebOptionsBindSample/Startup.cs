using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using JianShen.Bee.WebOptionsBindSample.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JianShen.Bee.WebOptionsBindSample
{
    public class Startup
    {
        //由于我们要用到Configuration，所以我们要用到依赖注入，把Configuration加进来
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {               
            services.AddMvc();//依赖注册Mvc

            services.AddRouting();//路由依赖注册

            //注册班级信息到IOptions
            services.Configure<Class>(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,IConfiguration configuration,IApplicationLifetime applicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //不常用
            app.Map("/task", taskApp =>
            {
                taskApp.Run(async context =>
                {
                    await context.Response.WriteAsync("this is a Task \r\n");
                });
            });

            app.UseMvcWithDefaultRoute();// 使用MVC并使用默认路由

            #region 路由的使用 （前提 ConfigureServices先注册 services.AddRouting()）
            //第一种方式 
            app.UseRouter(builder => builder.MapGet("actionfirst", async context =>
            {
                await context.Response.WriteAsync("this is first action");
            }));

            //第二种方式
            RequestDelegate handler = context => context.Response.WriteAsync("this is second action");
            var route = new Route(new RouteHandler(handler), "actionsecond", app.ApplicationServices.GetRequiredService<IInlineConstraintResolver>());
            app.UseRouter(route);
            #endregion

            #region 中间件使用 Use方法
            //第一种 使用HttpContext
            app.Use(async (context,next) => {
                await context.Response.WriteAsync("1:befoe start...\r\n");
                await next.Invoke();
                await context.Response.WriteAsync("1:befoe end...\r\n");
            });

            //第二种 接受一个委托,next是我们接收到的一个请求委托，我们返回的也是请求委托
            app.Use(next =>
            {
                return (context) =>
                {
                    context.Response.WriteAsync("2:in the middle of start...\r\n");
                    next(context);
                    return context.Response.WriteAsync("2:in the middle of end...\r\n");
                };
            });
            #endregion

          

            #region 注释掉，防止MVC管道被占用
            app.Run(async (context) =>
            {
                //var myClass = new Class();
                ////将appsetting.json配置文件内容绑定到Class类中
                //Configuration.Bind(myClass);

                //await context.Response.WriteAsync($"ClassNo:{myClass.ClassNo}\r\n");
                //await context.Response.WriteAsync($"ClassDesc:{myClass.ClassDesc}\r\n");
                //await context.Response.WriteAsync($"{myClass.Students.Count}Students\r\n");

                //await context.Response.WriteAsync($"ApplicationName={env.ApplicationName}\r\n");
                //await context.Response.WriteAsync($"ContentRootFileProvider={env.ContentRootFileProvider}");
                //await context.Response.WriteAsync($"ContentRootPath={env.ContentRootPath}\r\n");
                //await context.Response.WriteAsync($"EnvironmentName={env.EnvironmentName}\r\n");
                //await context.Response.WriteAsync($"WebRootPath={env.WebRootPath}\r\n");
                //await context.Response.WriteAsync($"WebRootFileProvider={env.WebRootFileProvider}\r\n");

                //await context.Response.WriteAsync(configuration["ConnectionStrings:DefaultConnection"]);             

                ////ApplicationLifetime可以在应用开始、结束中、结束后的时候执行委托的事件
                //applicationLifetime.ApplicationStarted.Register(() =>
                //{
                //    Console.WriteLine("Started");
                //});

                //applicationLifetime.ApplicationStopping.Register(() => {
                //    Console.WriteLine("Stopping");
                //});

                //applicationLifetime.ApplicationStopped.Register(() =>
                //{
                //    Thread.Sleep(2000);
                //    Console.WriteLine("Stopped");
                //});

                await context.Response.WriteAsync("3:start \r\n");

            });
            #endregion
        }
    }
}
