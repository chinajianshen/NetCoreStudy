using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenBook.WebCoreApp.Infrastructure.CustomTagHelpers
{
    [HtmlTargetElement("email")]
    public class EmailTagHelper: TagHelper
    {
        private const string EmailDomain = "qq.com";

        public string MailTo { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //base.Process(context, output);
            output.TagName = "a";

            //var address = MailTo + "@" + EmailDomain;
            //output.Attributes.Add("href", "mailto:" + address);
            //output.Content.SetContent(address);

            //获取不到值 只能异步获取
            var content = output.Content.GetContent();
            var target = content + "@" + EmailDomain;
            output.Attributes.SetAttribute("href", "mailto:" + target);
            output.Content.SetContent(target);
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            //return base.ProcessAsync(context, output);
            output.TagName = "a";
            var content = await output.GetChildContentAsync();
            var target = content.GetContent() + "@" + EmailDomain;
            output.Attributes.SetAttribute("href", "mailto:" + target);
            output.Content.SetContent(target);
        }
    }
}
