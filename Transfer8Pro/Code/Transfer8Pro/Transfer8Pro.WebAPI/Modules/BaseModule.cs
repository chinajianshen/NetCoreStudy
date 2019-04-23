using Nancy;
using Nancy.Json;
using Nancy.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Transfer8Pro.Core.Service.OB;
using Transfer8Pro.Entity;
using Transfer8Pro.Entity.OB;
using Transfer8Pro.WebAPI.Serializer;

namespace Transfer8Pro.WebAPI.Modules
{
    public class BaseModule: NancyModule
    {
        public BaseModule()
        {
            JsonSettings.MaxJsonLength = Int32.MaxValue;
        }

        public BaseModule(string part) : base(part)
        {
            JsonSettings.MaxJsonLength = Int32.MaxValue;

        }

        public Response OkResult()
        {
            return new JsonResponse(new ResponseModel { Status = 1 }, new JsonSerializer());
        }

        public Response OkResult(string msg)
        {
            return new JsonResponse(new ResponseModel { Status = 1, Msg = msg }, new JsonSerializer());
        }

        public Response OkResult(object data)
        {
            return new JsonResponse(new ResponseModel { Status = 1, Data = data }, new JsonSerializer());
        }

        public Response OkResult(string msg, object data)
        {

            return new JsonResponse(new ResponseModel { Status = 1, Msg = msg, Data = data }, new JsonSerializer());
        }

        public Response ErrorResult()
        {
            return new JsonResponse(new ResponseModel { Status = -1 }, new DefaultJsonSerializer { RetainCasing = true });
        }

        public Response ErrorResult(string msg)
        {
            return new JsonResponse(new ResponseModel { Status = -1, Msg = msg }, new DefaultJsonSerializer { RetainCasing = true });
        }

        public Response ErrorResult(object data)
        {
            return new JsonResponse(new ResponseModel { Status = -1, Data = data }, new DefaultJsonSerializer { RetainCasing = true });
        }

        public Response ErrorResult(string msg, object data)
        {
            return new JsonResponse(new ResponseModel { Status = -1, Msg = msg, Data = data }, new DefaultJsonSerializer { RetainCasing = true });
        }

        public Response CustomerResult(int status, string msg = null, object data = null)
        {
            return new JsonResponse(new ResponseModel { Status = status, Msg = msg, Data = data }, new DefaultJsonSerializer { RetainCasing = true });
        }

        /// <summary>
        /// 系统中是否存在FTP账号
        /// </summary>
        /// <param name="ftpUserName"></param>
        /// <returns></returns>
        protected bool IsExistsFtpUserName(string ftpUserName,out string encryKey)
        {
            encryKey = "";
            FtpInfoEntity ftpInfo = new FtpInfoService().Find(ftpUserName);
            if (ftpInfo != null && !string.IsNullOrEmpty(ftpInfo.FtpEncryptKey))
            {
                encryKey = ftpInfo.FtpEncryptKey;
                return true;
            }
            return false;
        }
    }
}