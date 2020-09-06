using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Leonni.Models.ViewModels
{
    public class CategoryModel: BaseViewModel
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public string CategoryName { get; set; }
    }
}
