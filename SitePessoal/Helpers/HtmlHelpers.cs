using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SitePessoal.Helpers
{
    public static class HtmlHelpers
    {

        public static string ConditionalClass(this HtmlHelper html, bool condition, string classToApply)
        {
            if (condition)
                return classToApply;
            return "";
        }

    }
}
