using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Transfer8Pro.CommunalWebAPI.Infrastructure.Filters;

namespace Transfer8Pro.CommunalWebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务
            //注册全局权限筛选器
            //config.Filters.Add(new ActionAuthFilter());

            //注册全局异常处理
            config.Filters.Add(new ExceptionFilter());

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
