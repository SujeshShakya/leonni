using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Leonni.Models.ViewModels
{
    public class TranslationImportModel : BaseViewModel
    {
        public string Content { get; set; }
    }

    public class EmailImportModel : BaseViewModel
    {
        public string EmailType { get; set; }
        public string Content { get; set; }
    }
}
