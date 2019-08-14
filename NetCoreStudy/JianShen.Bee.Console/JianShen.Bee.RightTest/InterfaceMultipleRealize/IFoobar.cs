using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace JianShen.Bee.RightTest.InterfaceMultipleRealize
{
    /// <summary>
    /// 引用地址：https://www.cnblogs.com/artech/p/upgrade-degree.html
    /// </summary>
    public interface IFoobar
    {
        Task InvokeAsync(HttpContext httpContext);
    }

    public class InvocationSourceAttribute : ServiceFilterAttribute
    {
        public string Source { get; }

        public InvocationSourceAttribute(string source) => Source = source;

        public override bool Match(HttpContext httpContext)
        {
            return httpContext.GetInvocationSource() == Source;
        }
    }

    [InvocationSource("MiniApp")]
    public class Bar : IFoobar
    {
        public Task InvokeAsync(HttpContext httpContext) => httpContext.Response.WriteAsync("Process for MiniApp");
    }

    [InvocationSource("App")]
    public class Foo : IFoobar
    {
        public Task InvokeAsync(HttpContext httpContext)
        {
            return httpContext.Response.WriteAsync("Process for App");
        }
    }

    public class FooBarSelector : ServiceSelector<IFoobar>, IFoobar
    {
        //private static ConcurrentDictionary<Type, string> _sources = new ConcurrentDictionary<Type, string>();

        public FooBarSelector(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {

        }

        public Task InvokeAsync(HttpContext httpContext)
        {
            return GetService()?.InvokeAsync(httpContext);
        }

        //public Task InvokeAsync(HttpContext httpContext)
        //{
        //    return httpContext.RequestServices.GetServices<IFoobar>()
        //        .FirstOrDefault(it => it != this && GetInvocationSource(it) == httpContext.GetInvocationSource())?.InvokeAsync(httpContext);
        //}

        //string GetInvocationSource(object service)
        //{
        //    var type = service.GetType();
        //    return _sources.GetOrAdd(type, _ => type.GetCustomAttribute<InvocationSourceAttribute>()?.Source);
        //}
    }
}
