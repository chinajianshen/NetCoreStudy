using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OpenBook.WebCoreApp.Infrastructure.CustomFeatureProviders;

namespace OpenBook.WebCoreApp.Controllers
{
    [GenericControllerNameConvention]
    public class GenericController<T> : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}