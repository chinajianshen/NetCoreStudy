using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OpenBook.WebCoreApp.Models;
using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
using OpenBook.WebCoreApp.Infrastructure.CustomTagHelpComponents;
using Microsoft.AspNetCore.Hosting;
using OpenBook.WebCoreApp.Infrastructure.CustomApplicationModel;
using OpenBook.WebCoreApp.Infrastructure.CustomFilters;
using System.Globalization;
using OpenBook.WebCoreApp.Infrastructure.CustomMiddleware;
using OpenBook.WebCoreApp.Infrastructure.CustomFeatureProviders;

namespace OpenBook.WebCoreApp.Controllers
{
    //[AddHeader("Author", "Steve Smith @ardalis")]
    //[ControllerDescription("Controller Description")]
    //[TypeFilter(typeof(CustomExceptionFilterAttribute))]
   
    public class HomeController : Controller
    {
        private readonly ITagHelperComponentManager _tagHelperComponentManager;
        private readonly IHostingEnvironment _hostingEnvironment;
        public HomeController(ITagHelperComponentManager tagHelperComponentManager, IHostingEnvironment hostingEnvironment)
        {           
            _tagHelperComponentManager = tagHelperComponentManager;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            //var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            //var path = exceptionHandlerPathFeature?.Path;
            //var error = exceptionHandlerPathFeature?.Error;
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //[ActionDescription("Action Description")]
        //[TypeFilter(typeof(LogConstantFilter), Arguments =new object[] { "这是一个TypeFilter注册的筛选器" })] //不用注册，直接用TypeFilter调用 
        //[SampleActionFilter]
        //[TypeFilter(typeof(AddHeaderFilterWithDi))]
        [UnprocessableResultFilter]
        public IActionResult TagForm()
        {
            //string desc = ControllerContext.ActionDescriptor.Properties["description"].ToString();
            //_tagHelperComponentManager.Components.Add(new AddressScriptTagHelperComponent(_hostingEnvironment));
            //_tagHelperComponentManager.Components.Add(new AddressStyleTagHelperComponent());


            return View();           
        }


        public string GetById([MustBeInRouteParameterModelConvention]int id)
        {
            return $"Bound to id: {id}";
        }

        //[CustomActionName("MyAction")]
        public string GetById2(int id)
        {
            return $"Bound to id: {id}";
        }

        [MiddlewareFilter(typeof(LocalizationPipelineMiddleware))]
        public IActionResult CultureFromRouteData()
        {
            return Content($"CurrentCulture:{CultureInfo.CurrentCulture.Name},CurrentUICulture:{CultureInfo.CurrentUICulture.Name}");
        }

    }
}
