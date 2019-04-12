using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenBook.WebCoreApp.Infrastructure.CustomTagHelpComponents
{
    public class AddressStyleTagHelperComponent:TagHelperComponent
    {
        private readonly string _style = @"<link rel=""stylesheet"" href=""/css/address.css"" />";

        public override int Order => 1;

        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            //return base.ProcessAsync(context, output);
            if (string.Equals(context.TagName,"head", StringComparison.OrdinalIgnoreCase))
            {
                output.PostContent.AppendHtml(_style);
            }
            return Task.CompletedTask;
        }
    }
}
