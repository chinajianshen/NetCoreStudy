using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JianShen.Bee.RightTest.PermissionMiddleware
{
    public static class PermissionMiddlewareExtensions
    {
        public static IApplicationBuilder UsePermission(this IApplicationBuilder builder, PermissionMiddlewareOption option)
        {
            return builder.UseMiddleware<PermissionMiddleware>(option);
        }
    }
}
