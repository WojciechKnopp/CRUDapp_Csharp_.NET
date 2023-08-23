using System.Collections;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CookiesApp.CustomTagHelpers;

[HtmlTargetElement("list")]
public class ListTagHelper : TagHelper
{
    [HtmlAttributeName("string-list")]
    public string list { get; set; }
    
    [HtmlAttributeName("items")]
    public IEnumerable Items { get; set; }
    
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "span";
        output.Content.AppendHtml($"<text>{output.GetChildContentAsync().Result.GetContent()}</text>");
        
        if(Items != null)
        {
            output.Content.AppendHtml("<ul>");
            foreach (var item in Items)
            {
                output.Content.AppendHtml($"<li>{item}</li>");
            }
            output.Content.AppendHtml("</ul>");
        }
        else
        {
            output.Content.AppendHtml("<ul>");
            foreach (var item in list.Split(","))
            {
                var i = item.Trim();
                output.Content.AppendHtml($"<li>{i}</li>");
            }
            output.Content.AppendHtml("</ul>");
        }
        
        output.TagMode = TagMode.StartTagAndEndTag;
    }
}