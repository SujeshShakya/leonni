using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Leonni.Models.ViewModels
{
    public class TranslationIndexView : BaseViewModel
    {
        public int LanguageId { get; set; }
        public string Classification { get; set; }
        public List<LanguageModel> Languages { get; set; }
        public List<string> TranslationClassifications { get; set; }
        public string EmailType { get; set; }

        private List<string> _emailTypes = new List<string> { "Forgot password mail", "Registration Confirmation mail","User create mail"};
        public List<string> EmailTypes { get { return _emailTypes; } }

    }
}
