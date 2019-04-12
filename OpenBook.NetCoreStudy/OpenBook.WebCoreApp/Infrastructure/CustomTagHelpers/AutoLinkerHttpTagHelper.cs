using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OpenBook.WebCoreApp.Infrastructure.CustomTagHelpers
{
    [HtmlTargetElement("p")]
    public class AutoLinkerHttpTagHelper:TagHelper
    {
        public override int Order
        {
            get { return int.MaxValue; }
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            //同一标记下，多次运行，回覆盖上次缓存内容，解决方法
            //var childContent = await output.GetChildContentAsync();
            var childContent = output.Content.IsModified ? output.Content.GetContent() : (await output.GetChildContentAsync()).GetContent();
            output.Content.SetHtmlContent(Regex.Replace(childContent, @"\b(?:https ?://)(\S+)\b", "<a target=\"_blank\" href=\"$0\">$0</a>"));
        }
    }

    [HtmlTargetElement("p")]
    public class AutoLinkerWwwTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var childContent = output.Content.IsModified ? output.Content.GetContent() :(await output.GetChildContentAsync()).GetContent();         
            output.Content.SetHtmlContent(Regex.Replace(
                childContent,
                 @"\b(www\.)(\S+)\b",
                 "<a target=\"_blank\" href=\"http://$0\">$0</a>"));  // www version
        }
    }
}
