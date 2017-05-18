using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitePessoal
{
    /// <summary>
    /// Taghelper to conditionally add the class based on item value.
    /// </summary>
    [HtmlTargetElement("a", Attributes = DropDownBsSelectedValueAttributeName + "," + DropDownBsItemValueAttributeName + "," + DropDownBsCssAttributeName)]
    public class DropDownBsSelectedTagHelper : TagHelper
    {
        private const string DropDownBsSelectedValueAttributeName = "bs-dropdown-selected-value";
        private const string DropDownBsItemValueAttributeName = "bs-dropdown-item-value";
        private const string DropDownBsCssAttributeName = "bs-dropdown-css";
        [HtmlAttributeName(DropDownBsSelectedValueAttributeName)]
        public string SelectedValue { get; set; }

        [HtmlAttributeName(DropDownBsItemValueAttributeName)]
        public string ItemValue { get; set; }

        [HtmlAttributeName(DropDownBsCssAttributeName)]
        public string Css { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string classValue = string.Empty;
            if (output.Attributes.ContainsName("class"))
                classValue = output.Attributes["class"].Value as string;
            
            if (SelectedValue == ItemValue)
                classValue += Css;

            if(!string.IsNullOrEmpty( classValue))
                output.Attributes.SetAttribute("class", classValue);
            base.Process(context, output);
        }
    }


    /// <summary>
    /// Taghelper to list description in a span
    /// </summary>
    /// <remarks>
    /// Requires plugin's initialization  
    /// </remarks>
    [HtmlTargetElement("span", Attributes = SpanDescriptionAttributeName)]
    public class SpanDescriptionTagHelper : TagHelper
    {
        private const string SpanDescriptionAttributeName = "span-description-array";
        /// <summary>
        /// Comma separated list of strings
        /// </summary>
        [HtmlAttributeName(SpanDescriptionAttributeName)]
        public string SpanDescription { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string classValue = string.Empty;
            if (output.Attributes.ContainsName("data-description"))
            {
                var desc = ((Microsoft.AspNetCore.Html.HtmlString)output.Attributes["data-description"].Value);
                if (desc.Value.Contains(","))
                {
                    var arrayDescription = desc.Value.Split(',');//SpanDescription.Split(',');
                    StringBuilder sb = new StringBuilder();
                    foreach (var description in arrayDescription)
                    {
                        sb.Append($"<span><strong>{description}</strong></span>");
                    }
                    output.Content.SetHtmlContent(sb.ToString());
                    //if (output.Attributes.ContainsName("class"))
                    //    output.Attributes.SetAttribute("class", output.Attributes["class"].Value as string);
                }
            }
            base.Process(context, output);
        }
    }

}
