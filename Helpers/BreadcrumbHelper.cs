using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LMS.Helpers
{
    public static class BreadcrumbHelper
    {
        public static IHtmlContent Breadcrumb(this IHtmlHelper htmlHelper)
        {
            var breadcrumb = new StringBuilder();
            var routeData = htmlHelper.ViewContext.RouteData;
            var controller = routeData.Values["controller"]?.ToString();
            var action = routeData.Values["action"]?.ToString();
            var parameters = htmlHelper.ViewContext.HttpContext.Request.Query;
            var fragments = htmlHelper.ViewContext.HttpContext.Request.Query["_fragment"];
            var currentUrl = htmlHelper.ViewContext.HttpContext.Request.Path;

            var breadcrumbItems = new List<string>();

            if (!string.IsNullOrEmpty(controller))
            {
                breadcrumbItems.Add(controller);
            }

            if (!string.IsNullOrEmpty(action))
            {
                breadcrumbItems.Add(action);
            }

            foreach (var param in parameters)
            {
                breadcrumbItems.Add($"{param.Key}: {param.Value}");
            }

            if (!string.IsNullOrEmpty(fragments))
            {
                breadcrumbItems.Add($"Fragment: {fragments}");
            }

            breadcrumb.Append("<nav aria-label=\"breadcrumb\"><ol class=\"breadcrumb\">");

            for (int i = 0; i < breadcrumbItems.Count; i++)
            {
                var breadcrumbItem = breadcrumbItems[i];

                if (i == breadcrumbItems.Count - 1)
                {
                    breadcrumb.AppendFormat("<li class=\"breadcrumb-item active\">{0}</li>", breadcrumbItem);
                }
                else
                {
                    breadcrumb.AppendFormat("<li class=\"breadcrumb-item\"><a href=\"{0}\">{0}</a></li>", GetBreadcrumbLink(breadcrumbItems.Take(i + 1)));
                }
            }

            breadcrumb.Append("</ol></nav>");

            return new HtmlString(breadcrumb.ToString());
        }

        private static string GetBreadcrumbLink(IEnumerable<string> items)
        {
            return "/" + string.Join("/", items);
        }
    }
}
