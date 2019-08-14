using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using JianShen.Bee.RightTest.PermissionMiddleware;
using JianShen.Bee.RightTest.InterfaceMultipleRealize;
using Abp.Runtime.Session;

namespace JianShen.Bee.RightTest
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

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<IFoobar, Foo>();
            services.AddSingleton<IFoobar, Bar>();
            //FoobarSelector作为最后注册的服务，按照“后来居上”的原则，如果我们利用DI容器获取针对IFoobar接口的服务实例，返回的将会是一个FoobarSelector对象
            services.AddSingleton<IFoobar, FooBarSelector>();

            services.AddSingleton<IFoobarProvider, FoobarProvider>();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //添加认证Cookie信息
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(options =>
{
    options.LoginPath = new PathString("/login");
    options.AccessDeniedPath = new PathString("/denied");
    options.ExpireTimeSpan = TimeSpan.FromSeconds(120);
});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //需要Nuget下载Microsoft.VisualStudio.Web.BrowserLink.dll
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            //验证中间件
            app.UseAuthentication();

            //添加权限中间件, 一定要放在app.UseAuthentication后
            app.UsePermission(new PermissionMiddlewareOption()
            {
                LoginAction = @"/login",
                NoPermissionAction = @"/denied",
                //这个集合从数据库中查出所有用户的全部权限
                UserPerssions = new List<UserPermission>()
                 {
                    new UserPermission {  Url="/", UserName="ad"},
                    new UserPermission {  Url="/home/permissionadd", UserName="ad"},
                    new UserPermission {  Url="/", UserName="sy"},
                    new UserPermission {  Url="/home/contact", UserName="sy"},
                    new UserPermission {  Url="/home/about", UserName="sy"}
                 }
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
