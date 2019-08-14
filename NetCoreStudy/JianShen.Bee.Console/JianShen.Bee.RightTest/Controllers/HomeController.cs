using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JianShen.Bee.RightTest.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using JianShen.Bee.RightTest.InterfaceMultipleRealize;

namespace JianShen.Bee.RightTest.Controllers
{
    //[Authorize(Roles ="admin,system")]
    public class HomeController : BaseController
    {
        private readonly IFoobar _foobar;
        private readonly IFoobarProvider _foobarProvider;

        public HomeController(IFoobar foobar, IFoobarProvider foobarProvider)
        {
            _foobar = foobar;
            _foobarProvider = foobarProvider;
        }  

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PermissionAdd()
        {
            return View();
        }

        //[HttpPost("addpermission")]
        //public IActionResult AddPermission(string url, string userName)
        //{
        //    //添加权限
        //    PermissionMiddleware._userPermissions.Add(new UserPermission { Url = url, UserName = userName });
        //    return Content("添加成功");
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize(Roles ="admin")]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            return View();
        }

        [Authorize(Roles ="system")]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
            return View();
        }

        [AllowAnonymous]
        [HttpGet("login")]
        public IActionResult Login(string returnUrl = null)
        {
            TempData["returnUrl"] = returnUrl;
            return View();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(string userName,string password,string returnUrl = null)
        {
            var list = new List<dynamic>
            {
                new { UserName ="ad",Name="张三", Password="123456", Role="admin"},
                new { UserName ="sy",Name="李四", Password="123456", Role="system"}
            };

            var user = list.SingleOrDefault(s => s.UserName == userName && s.Password == password);
            if (user != null)
            {
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Sid,userName));
                identity.AddClaim(new Claim(ClaimTypes.Name, user.Name));
                identity.AddClaim(new Claim(ClaimTypes.Role, user.Role));
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                if (returnUrl == null)
                {
                    returnUrl = TempData["returnUrl"]?.ToString();
                }

                if (returnUrl != null)
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
            }
            else
            {
                const string badUserNameOrPasswordMessage = "用户名或密码错误！";
                return BadRequest(badUserNameOrPasswordMessage);
            }
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
     
        [HttpGet("denied")]
        public IActionResult Denied()
        {
            return View();
        }


        public Task GetApp(string source)
        {
            HttpContext.SetInvocationSource(source ?? "App");
            return _foobarProvider.GetService()?.InvokeAsync(HttpContext) ?? Task.CompletedTask;         
        }

        public Task GetApp2(string source)
        {
            HttpContext.SetInvocationSource(source ?? "App");         
            return _foobar.InvokeAsync(HttpContext) ?? Task.CompletedTask;
        }
    }
}
