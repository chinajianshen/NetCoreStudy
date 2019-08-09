using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JianShen.Bee.WebOptionsBindSample.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace JianShen.Bee.WebOptionsBindSample.Controllers
{
    //通过依赖注入的方式在HomeController中注入myClass。只不过这次我们不能用bing的方法，而是用IOption<T> 泛型方法将班级注入进去
    public class HomeController : Controller
    {
        private readonly Class _myClass;

        public HomeController(IOptions<Class> classAccesser)
        {
            _myClass = classAccesser.Value;
        }

        public IActionResult Index()
        {
            //HttpContext.RequestServices.GetService()
            return View(_myClass);
        }

        //我们也可以把注入直接取出来，通过依赖注入框架直接在视图中显示出来 @inject
        public IActionResult Index2()
        {
            return View();
        }
    }
}