using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Leonni.Models.ViewModels
{
   public class PageContentIndexView:BaseViewModel
    {
       public List<PageContentModel> PageContents { get; set; }
       public long CurrentPage { get; set; }
       public long TotalPages { get; set; }
       public long TotalItems { get; set; }
       public long ItemsPerPage { get; set; }
    }
}
