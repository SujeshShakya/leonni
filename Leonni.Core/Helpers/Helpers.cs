using System.Globalization;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using System.Web;
using System.Configuration;


namespace Leonni.Core.Helpers
{
    public static class Helpers
    {
        public static void PaginationElement(this HtmlHelper htmlHelper, int currentPage, int totalpages, out int start, out int end)
        {
            start = currentPage - 2;
            end = start + 5;

            if (start < 1)
            {
                start = 1;
                end = start + 4;
            }

            if (end > totalpages)
            {
                end = (int)totalpages;
                start = end - 4;
                end += 1;
            }

        }

        //public static MvcHtmlString AlternativeRow(this HtmlHelper htmlHelper, int i)
        //{
        //    string classname = string.Empty;
        //    if (i % 2 == 0)
        //    {
        //        classname = "column even";
        //    }
        //    else
        //    {
        //        classname = "column odd";
        //    }
        //    return MvcHtmlString.Create(classname);
        //}

 

    }

    public static class CaseChange
    {
        public static string TitleCase(string str)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str);
        }
    }

    
}
