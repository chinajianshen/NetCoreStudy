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
    public abstract class ServiceFilterAttribute:Attribute
    {
        public abstract bool Match(HttpContext httpContext);
    }

    public abstract class ServiceSelector<T> where T : class
    {
        private static ConcurrentDictionary<Type, ServiceFilterAttribute> _filters = new ConcurrentDictionary<Type, ServiceFilterAttribute>();

        private readonly IHttpContextAccessor _httpContextAccessor;

        protected ServiceSelector(IHttpContextAccessor httpContextAccessor) => _httpContextAccessor = httpContextAccessor;

        protected T GetService()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            return httpContext.RequestServices.GetServices<T>()
                .FirstOrDefault(it => it != this && GetFilter(it)?.Match(httpContext) == true);


            ServiceFilterAttribute GetFilter(object service)
            {
                var type = service.GetType();
                return _filters.GetOrAdd(type, _ => type.GetCustomAttribute<ServiceFilterAttribute>());
            }            
        }
    }
}


