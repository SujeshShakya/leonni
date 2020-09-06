using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Leonni.Core.Extensions
{
    public static class ModelStateExtensions
    {
        public static string ToHtmlString(this ModelStateDictionary ModelState)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<ul>");
            foreach (var obj in ModelState.Values)
            {
                foreach (var error in obj.Errors)
                {
                    if (!string.IsNullOrEmpty(error.ErrorMessage))
                    {
                        sb.AppendFormat("<li>{0}</li>", error.ErrorMessage);
                        sb.AppendLine();
                    }
                }
            }
            sb.AppendLine("</ul>");
            return sb.ToString();
        }
    }
}
