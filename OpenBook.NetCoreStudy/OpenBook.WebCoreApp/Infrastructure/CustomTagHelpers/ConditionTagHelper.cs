using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenBook.WebCoreApp.Infrastructure.CustomTagHelpers
{
    public class ConditionTagHelper:TagHelper
    {
        public bool Condition { get; set; }       

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (!Condition)
            {
                output.SuppressOutput();
            }

            
         
            //return base.ProcessAsync(context, output);
        }
    }
}
