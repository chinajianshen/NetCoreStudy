using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Transfer8Pro.Entity;
using Transfer8Pro.Utils;

namespace Transfer8Pro.CommunalWebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static List<string> WhiteList { get; set; }

        static WebApiApplication()
        {
            var pathConf = ConfigHelper.GetConfig("WhitePath");

            if (!string.IsNullOrEmpty(pathConf))
            {
                WhiteList = pathConf.Split(new[] { ',' }).Select(item => item.ToLower()).ToList();
            }
            else
            {
                WhiteList = new List<string>();
            }
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_BeginRequest()
        {
            if (PathValidate(Request.Path))
            {
                return;
            }

            #region
            bool valid = false;
            string msg = string.Empty;
            try
            {
                string appkey = Request.Headers["t8_appkey"];
                string configAppKey = ConfigHelper.GetConfig("AppSecrectKey");                

                if (!string.IsNullOrEmpty(appkey) && !string.IsNullOrEmpty(configAppKey) && configAppKey == appkey)
                {
                    string token = Request.Headers["t8_token"].ToString();
                    string tick = Request.Headers["t8_tick"].ToString();
                    string method = Request.HttpMethod.ToLower();
                    if (!string.IsNullOrEmpty(token))
                    {
                        long tickl = long.Parse(tick);
                        if ((Math.Abs(DateTime.Now.Ticks - tickl) / 10000) <= 20 * 60 * 1000)
                        {
                            string data = method == "get" ? Request.QueryString.ToString() : Request.Form.ToString();
                            string token1 = GetSign(data, tick, appkey, configAppKey);
                            if (token == token1)
                            {
                                //SparrowIdentity identity = new SparrowIdentity();
                                //identity.SessionID = si.SessionID;
                                //ctx.CurrentUser = identity;
                                valid = true;
                            }
                            else
                            {
                                msg = "调用签名错误！";
                            }
                        }
                        else
                        {
                            msg = string.Format("您的请求已经失效，请求有效期为20分钟，请确认您本机的时间是否正确！（服务器时间:{0}）", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        }
                    }
                    else
                    {
                        msg = "缺少会话签名";
                    }
                }
                else
                {
                    msg = "非法AppKey";
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                LogUtil.WriteLog(ex);
            }

            if (!valid)
            {
                ErrorResponse(msg);
            }
            #endregion
        }

        private bool PathValidate(string path)
        {
            return WhiteList.Exists(p => p.ToLower().Contains(path.Trim('/')));
        }

        private static string fun_MD5(string str)
        {
            byte[] b = System.Text.Encoding.UTF8.GetBytes(str);
            b = new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(b);
            string ret = "";
            for (int i = 0; i < b.Length; i++)
                ret += b[i].ToString("x").PadLeft(2, '0');
            return ret;
        }

        private static string GetSign(string data, string tick, string appkey, string AppScrect)
        {
            try
            {
                string text1 = fun_MD5(string.IsNullOrEmpty(data) ? "" : System.Web.HttpUtility.UrlDecode(data, Encoding.UTF8));
                string text2 = fun_MD5(string.Concat(new object[] {
                AppScrect,
                "&",text1,"&",tick
            }));
                return text2;
            }
            catch
            {

                return string.Empty;
            }

        }
              

        public static void ErrorResponse(string msg)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";           
            HttpContext.Current.Response.Write(JsonObj<ResponseModel>.ToJson(new ResponseModel { Status = -1, Msg = msg }));
            HttpContext.Current.Response.Flush();
        }
    }
}
