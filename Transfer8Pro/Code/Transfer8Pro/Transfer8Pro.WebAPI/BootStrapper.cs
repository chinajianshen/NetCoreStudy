using Nancy;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.Responses;
using Nancy.TinyIoc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Transfer8Pro.Core.Infrastructure;
using Transfer8Pro.Entity;
using Transfer8Pro.Utils;

namespace Transfer8Pro.WebAPI
{
    public class BootStrapper : DefaultNancyBootstrapper
    {
        private static List<string> WhiteList { get; set; }

        static BootStrapper()
        {
            var pathConf = ConfigurationManager.AppSettings["whitePath"];

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

        protected override void ConfigureConventions(NancyConventions conventions)
        {
            conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("ShareFiles", @"ShareFiles"));

            base.ConfigureConventions(conventions);
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
          
            var res = ctx.Request.Headers["signature"].ToList();
            if (res.Count == 0)
            {
                return new JsonResponse<ResponseModel>(new ResponseModel("signature非法"), new DefaultJsonSerializer { RetainCasing = true });
            }

            var signature = res[0];

            string msg = string.Empty;
            if (!ApiHelper.ResolveApiSignature(signature,out msg))
            {
                return new JsonResponse<ResponseModel>(new ResponseModel(msg), new DefaultJsonSerializer { RetainCasing = true });
            }

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

            return new JsonResponse(new ResponseModel { Status = -1, Msg = "系统异常，请稍后再试" }, new DefaultJsonSerializer { RetainCasing = true });
        }
        #endregion
    }
}