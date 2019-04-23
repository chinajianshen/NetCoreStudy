using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Transfer8Pro.Entity;

namespace Transfer8Pro.ForeignWebAPI.Modules
{
    public class BaseModule : NancyModule
    {
        public BaseModule()
        {

        }

        public BaseModule(string part):base(part)
        {

        }

        public Response OkResult()
        {
            //return new JsonResponse(new ResponseModel { Status = 1 }, new JsonSerializer());
            return Response.AsJson(new ResponseModel { Status = 1 });
        }

        public Response OkResult(string msg)
        {
            //return new JsonResponse(new ResponseModel { Status = 1, Msg = msg }, new JsonSerializer());
            return Response.AsJson(new ResponseModel { Status = 1, Msg = msg });
        }

        public Response OkResult(object data)
        {
            //return new JsonResponse(new ResponseModel { Status = 1, Data = data }, new JsonSerializer());
            return Response.AsJson(new ResponseModel { Status = 1, Data = data });
        }

        public Response OkResult(string msg, object data)
        {

            //return new JsonResponse(new ResponseModel { Status = 1, Msg = msg, Data = data }, new JsonSerializer());
            return Response.AsJson(new ResponseModel { Status = 1, Msg = msg, Data = data });
        }

        public Response ErrorResult()
        {
            //return new JsonResponse(new ResponseModel { Status = -1 }, new DefaultJsonSerializer { RetainCasing = true });
            return Response.AsJson(new ResponseModel { Status = -1 });
        }

        public Response ErrorResult(string msg)
        {
            //return new JsonResponse(new ResponseModel { Status = -1, Msg = msg }, new DefaultJsonSerializer { RetainCasing = true });
            return Response.AsJson(new ResponseModel { Status = -1, Msg = msg });
        }

        public Response ErrorResult(object data)
        {
            //return new JsonResponse(new ResponseModel { Status = -1, Data = data }, new DefaultJsonSerializer { RetainCasing = true });
            return Response.AsJson(new ResponseModel { Status = -1, Data = data });
        }

        public Response ErrorResult(string msg, object data)
        {
            //return new JsonResponse(new ResponseModel { Status = -1, Msg = msg, Data = data }, new DefaultJsonSerializer { RetainCasing = true });
            return Response.AsJson(new ResponseModel { Status = -1, Msg = msg, Data = data });
        }

        public Response CustomerResult(int status, string msg = null, object data = null)
        {
            //return new JsonResponse(new ResponseModel { Status = status, Msg = msg, Data = data }, new DefaultJsonSerializer { RetainCasing = true });
            return Response.AsJson(new ResponseModel { Status = status, Msg = msg, Data = data });
        }
    }
}