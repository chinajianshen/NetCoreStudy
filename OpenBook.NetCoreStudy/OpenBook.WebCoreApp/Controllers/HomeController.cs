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

namespace OpenBook.WebCoreApp.Controllers
{
    [AddHeader("Author", "Steve Smith @ardalis")]
    [ControllerDescription("Controller Description")]
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

        [ActionDescription("Action Description")]
        public IActionResult TagForm()
        {
            string desc = ControllerContext.ActionDescriptor.Properties["description"].ToString();
            //_tagHelperComponentManager.Components.Add(new AddressScriptTagHelperComponent(_hostingEnvironment));
            //_tagHelperComponentManager.Components.Add(new AddressStyleTagHelperComponent());
            return View();
        }


        public string GetById([MustBeInRouteParameterModelConvention]int id)
        {
            return $"Bound to id: {id}";
        }

        [CustomActionName("MyAction")]
        public string GetById2(int id)
        {
            return $"Bound to id: {id}";
        }

    }
}
