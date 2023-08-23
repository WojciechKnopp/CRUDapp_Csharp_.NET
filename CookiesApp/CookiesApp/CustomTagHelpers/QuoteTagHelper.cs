using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CookiesApp.CustomTagHelpers;

[HtmlTargetElement("quote")]
public class QuoteTagHelper : TagHelper
{
    [HtmlAttributeName("author")]
    public string Author { get; set; }
    
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "span";
        output.Attributes.SetAttribute("class", "d-flex flex-column align-items-center");
        output.Content.AppendHtml("<text style=\"font-style: italic; opacity: 0.7; font-size: 0.8em\">" +
                                  $"\"{output.GetChildContentAsync().Result.GetContent()}\"</text>");
        output.Content.AppendHtml($"<text style=\"opacity: 0.9; font-size: 0.8em\">{Author}</text>");
        output.TagMode = TagMode.StartTagAndEndTag;
    }
}