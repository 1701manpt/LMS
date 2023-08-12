using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using System.Text.RegularExpressions;

namespace LMS.TagHelpers
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

            breadcrumb.Append("<nav style=\"--bs-breadcrumb-divider: url(&#34;data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='8' height='8'%3E%3Cpath d='M2.5 0L1 1.5 3.5 4 1 6.5 2.5 8l4-4-4-4z' fill='currentColor'/%3E%3C/svg%3E&#34;);\" aria-label=\"breadcrumb\"><ol class=\"breadcrumb\">");

            for (int i = 0; i < breadcrumbItems.Count; i++)
            {
                var breadcrumbItem = breadcrumbItems[i];

                if (i == 0)
                {
                    breadcrumb.AppendFormat("<li class=\"breadcrumb-item\"><a class=\"text-decoration-none\" href=\"/{0}\">{1}</a></li>", breadcrumbItem.ToLower(), AddSpacesToCamelCase(breadcrumbItem));
                }
                else
                {
                    breadcrumb.AppendFormat("<li class=\"breadcrumb-item\">{0}</li>", breadcrumbItem);
                }
            }

            breadcrumb.Append("</ol></nav><hr />");

            return new HtmlString(breadcrumb.ToString());
        }

        private static string AddSpacesToCamelCase(string input)
        {
            return Regex.Replace(input, "(?<=[a-z])([A-Z])", " $1", RegexOptions.Compiled).Trim();
        }
    }
}
