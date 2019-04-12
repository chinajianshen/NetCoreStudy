using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace OpenBook.WebCoreApp.Infrastructure.CustomTagHelpComponents
{
    public class AddressTagHelperComponent:TagHelperComponent
    {
        private readonly string _printableButton =
        "<button type='button' class='btn btn-info' onclick=\"window.open("+
        "'https://binged.it/2AXRRYw')\">" +
        "<span class='glyphicon glyphicon-road' aria-hidden='true'></span>" +
        "</button>";

        public override int Order => 3;

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
           if (string.Equals(context.TagName,"address2",StringComparison.OrdinalIgnoreCase) && output.Attributes.ContainsName("printable"))
            {
                var content = await output.GetChildContentAsync();
                output.Content.SetHtmlContent($"<div>{content.GetContent()}</div>{_printableButton}");
            }
        }
    }

    [HtmlTargetElement("address2")]
    [EditorBrowsable( EditorBrowsableState.Never)]
    public class AddressTagHelperComponentTaggHelper : TagHelperComponentTagHelper
    {
        public AddressTagHelperComponentTaggHelper(ITagHelperComponentManager componentManager,ILoggerFactory loggerFactory) : base(componentManager,loggerFactory)
        {

        }
    }
}
