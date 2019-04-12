using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OpenBook.WebCoreApp.Infrastructure.CustomTagHelpComponents
{
    public class AddressScriptTagHelperComponent:TagHelperComponent
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public AddressScriptTagHelperComponent(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }      

        public override int Order => 2;

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            //return base.ProcessAsync(context, output);
            if (string.Equals(context.TagName,"body", StringComparison.OrdinalIgnoreCase))
            {
                string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "templates\\AddressToolTipScript.html");
                var script = await File.ReadAllTextAsync(filePath);
                output.PostContent.AppendHtml(script);
            }
        }
    }
}
