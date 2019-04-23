using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Transfer8Pro.ForeignWebAPI.Modules
{
    public class HomeModule:BaseModule
    {
       public HomeModule()
        {
            Get("/", _ =>
            {
                return Response.AsRedirect("/swagger-ui");
            });

            Get("/swagger-ui", _ =>
            {
                var url = $"{Request.Url.BasePath}/api-docs";
                return View["doc", url];
            });
        }
    }
}