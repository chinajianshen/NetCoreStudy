using Nancy;
using Nancy.Bootstrapper;
using Nancy.Responses;
using Nancy.TinyIoc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Web;
using Transfer8Pro.Core.Infrastructure;
using Transfer8Pro.Entity;
using Transfer8Pro.Utils;

namespace Transfer8Pro.ForeignWebAPI
{
    public class BootStrapper : DefaultNancyBootstrapper
    {
        private static List<string> WhiteList { get; set; }

        static BootStrapper()
        {
            var pathConf = ConfigurationManager.AppSettings["WhitePath"];

            if (!string.IsNullOrEmpty(pathConf))
            {
                WhiteList = pathConf.Split(new[] { ',' }).ToList();
            }
            else
            {
                WhiteList = new List<string>();
            }
        }

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            //校验Signature
            pipelines.BeforeRequest += ValidateSignature;

            //全局GZIP压缩
            pipelines.AfterRequest.AddItemToEndOfPipeline(AddGZip);

            //异常处理
            pipelines.OnError += ErrorHandler;


            base.ApplicationStartup(container, pipelines);
        }

        #region 私有方法
        private bool PathValidate(string path)
        {
            return WhiteList.Exists(p => p.Equals(path.Trim('/'), StringComparison.OrdinalIgnoreCase));
        }
        private Response ValidateSignature(NancyContext ctx)
        {
            if (PathValidate(ctx.Request.Path))
            {
                return null;
            }

            #region
            //bool valid = false;
            //string msg = string.Empty;
          
            //try
            //{
            //    string appkey = ctx.Request.Headers["ox_appkey"].FirstOrDefault();
            //    string configAppKey = System.Configuration.ConfigurationManager.AppSettings["AppKey"];

            //    if (!string.IsNullOrEmpty(appkey) && !string.IsNullOrEmpty(configAppKey) && appkey == configAppKey)
            //    {
            //        string token = ctx.Request.Headers["ox_token"].FirstOrDefault();
            //        string tick = ctx.Request.Headers["ox_tick"].FirstOrDefault();
            //        string method = ctx.Request.Method.ToLower();
            //        if (!string.IsNullOrEmpty(token))
            //        {
            //            long tickl = long.Parse(tick);
            //            if ((Math.Abs(DateTime.Now.Ticks - tickl) / 10000) <= 20 * 60 * 1000)
            //            {
            //                string data = method == "get" ? ctx.Request.Query.data : ctx.Request.Form.data;
            //                string token1 = GetSign(data, tick, appkey, configAppKey);
            //                if (token == token1)
            //                {
            //                    //SparrowIdentity identity = new SparrowIdentity();
            //                    //identity.SessionID = si.SessionID;
            //                    //ctx.CurrentUser = identity;
            //                    valid = true;
            //                }
            //                else
            //                {
            //                    msg = "调用签名错误！";
            //                }
            //            }
            //            else
            //            {
            //                msg = string.Format("您的请求已经失效，请求有效期为20分钟，请确认您本机的时间是否正确！（服务器时间:{0}）", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            //            }
            //        }
            //        else
            //        {
            //            msg = "缺少会话签名";
            //        }
            //    }
            //    else
            //    {
            //        msg = "非法AppKey";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    msg = ex.Message;
            //    LogUtil.WriteLog(ex);
            //}         

            //if (!valid)
            //{
            //    //var res = new Response();
            //    //res.ContentType = "application/json; charset=utf-8";
            //    //res.Contents = s =>
            //    //{
            //    //    byte[] bs = Encoding.UTF8.GetBytes(string.Format("{{\"Status\":0,\"Msg\":\"{0}\"}}", msg));
            //    //    s.Write(bs, 0, bs.Length);
            //    //};
            //    //return res;
            //    return new JsonResponse(new ResponseModel { Status = -1, Msg = msg }, new DefaultJsonSerializer { RetainCasing = true });
            //}
            #endregion

            return null;
        }
        private void AddGZip(NancyContext context)
        {
            var jsonData = new MemoryStream();

            context.Response.Contents.Invoke(jsonData);
            jsonData.Position = 0;
            context.Response.Headers["Content-Encoding"] = "gzip";
            context.Response.Contents = s =>
            {
                var gzip = new GZipStream(s, CompressionMode.Compress, true);
                jsonData.CopyTo(gzip);
                gzip.Close();
            };
        }
        private Response ErrorHandler(NancyContext ctx, Exception ex)
        {
            LogUtil.WriteLog(ex);

            throw new NotImplementedException();
            //return new JsonResponse(new ResponseModel { Status = -1, Msg = "系统异常，请稍后再试" }, new DefaultJsonSerializer());
        }

        static string GetSign(string data, string tick, string appkey, string AppScrect)
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

        static string fun_MD5(string str)
        {
            byte[] b = System.Text.Encoding.UTF8.GetBytes(str);
            b = new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(b);
            string ret = "";
            for (int i = 0; i < b.Length; i++)
                ret += b[i].ToString("x").PadLeft(2, '0');
            return ret;
        }
        #endregion
    }
}