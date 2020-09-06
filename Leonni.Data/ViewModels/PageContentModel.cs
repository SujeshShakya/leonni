using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;

namespace Leonni.Models.ViewModels
{
    public class PageContentModel : BaseViewModel
    {
        public int PageId { get; set; }
        public int LanguageId { get; set; }
        public string PageUrl { get; set; }
        public string PageName { get; set; }
        public string Content { get; set; }
        public string HyperLinkText { get; set; }
        public string SortKey { get; set; }
        public string FileType { get; set; }

        public LanguageModel Language { get; set; }
    }

}
