using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OpenBook.WebCoreApp.Infrastructure.CustomFilters;

namespace OpenBook.WebCoreApp.Controllers
{
    [AddHeader("Author", "Steve Smith @ardalis")]
    public class NamespaceRoutingController : Controller
    {
        public string Index()
        {
            return "This demonstrates namespace routing.";
        }

        [ShortCircuitingResourceFilter]
        public string Resource()
        {
            return "123";
        }
    }
}