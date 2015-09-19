using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace BookStore.App_Code
{
    public static class ChangeDataHtmlHelpers
    {
        /// <summary>
        /// Cut any string with the maxlength definite.
        /// </summary>
        /// <returns>New string with maxlength or string.</returns>
        public static MvcHtmlString TrimFrontIfLongerThan(this HtmlHelper htmlHelper, string value, int maxLength)
        {
            if (value.Length > maxLength)
            {
                return  MvcHtmlString.Create(value.Substring(0,(maxLength - 3)) + "...");
            }

            return MvcHtmlString.Create(value);

        }
    }
}