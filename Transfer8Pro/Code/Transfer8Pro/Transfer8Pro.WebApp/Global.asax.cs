using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Transfer8Pro.Core.Service.OB;
using Transfer8Pro.Entity;
using Transfer8Pro.Utils;
using Transfer8Pro.WebApp.Infrastructure.Filters;

namespace Transfer8Pro.WebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            GlobalFilters.Filters.Add(new JsonResultFilter());
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RtxNotifyMessage();
            RtxNotifyTaskErrorMessage();
#if DEBUG
            BundleTable.EnableOptimizations = false;
#else
             BundleTable.EnableOptimizations = true;
#endif
        }
        /// <summary>
        /// 全局错误处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Error(object sender, EventArgs e)
        {
            //忽略不是ajax请求js脚本错误和css错误
            //判断是否是ajax请求 Request.Headers["X-Requested-With"] == "XMLHttpRequest"

            if (Request.Url.ToString().ToLower().Contains("/login/.com/css"))
            {
                Server.ClearError();
                return;
            }

            if (Request.Url.ToString().ToLower().Contains("/content/") && Request != null && (new HttpRequestWrapper(Request)).IsAjaxRequest() == false)
            {
                Server.ClearError();
                return;
            }


            Exception exception = null;
            try
            {
                exception = Server.GetLastError();
                if (exception != null)
                {
                    if (Request != null && (new HttpRequestWrapper(Request)).IsAjaxRequest())
                    {
                        Response.Clear();
                        Response.ContentType = "application/json; charset=utf-8";
                        Response.Write(JsonObj.ToJson(
                        new ResponseModel
                        {
                            Status = -1,
                            Msg = exception.Message
                        }));
                        Response.Flush();
                        Server.ClearError();
                    }
                    else
                    {
                        Response.Clear();
                        HttpException httpException = exception as HttpException;
                        RouteData routeData = new RouteData();
                        routeData.Values.Add("controller", "Error");

                        if (httpException != null)
                        {
                            switch (httpException.GetHttpCode())
                            {
                                case 404:
                                    routeData.Values.Add("action", "HttpError404");
                                    break;
                                case 500:
                                    routeData.Values.Add("action", "HttpError500");
                                    break;
                                default:
                                    routeData.Values.Add("action", "General");
                                    break;
                            }
                        }
                        else
                        {
                            routeData.Values.Add("action", "Index");
                        }

                        routeData.Values.Add("error", exception.Message);
                        Server.ClearError();
                        IController errorController = new Controllers.ErrorController();
                        errorController.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtil.WriteLog(ex);
            }
            finally
            {
                if (exception != null)
                {
                    LogUtil.WriteLog(exception);
                }
            }

        }


        private static void RtxNotifyMessage()
        {
            new Thread(new ThreadStart(() =>
            {
                HeartbeatInfoService heartbeatService = new HeartbeatInfoService();
                while (true)
                {
                    //晚上23点-到第2天9点之前不检测心跳
                    DateTime nowTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    if (nowTime.Hour >= 9 && nowTime.Hour < 23)
                    {
                        heartbeatService.RtxNotifyDiedHeartbeat();
                    }                   

                    //每30分钟执行一次
                    for (int second = 0; second < 60 * 30; second++)
                    {
                        Thread.Sleep(1000);
                    }
                }
            })).Start();
        }

        private static void RtxNotifyTaskErrorMessage()
        {
            new Thread(new ThreadStart(() =>
            {
                HeartbeatInfoService heartbeatService = new HeartbeatInfoService();
                while (true)
                {
                    //晚上23点-到第2天9点之前不检测心跳
                    DateTime nowTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    if (nowTime.Hour >= 9 && nowTime.Hour < 23)
                    {
                        heartbeatService.RtxNotifyTaskError();
                    }

                    //每10分钟执行一次
                    for (int second = 0; second < 60 * 10; second++)
                    {
                        Thread.Sleep(1000);
                    }
                }
            })).Start();
        }

    }
}
