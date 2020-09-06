using System.Collections.Generic;

namespace Leonni.Models.ViewModels
{
    public class LanguageView : BaseViewModel
    {
        public List<LanguageModel> Languages { get; set; }
        public long CurrentPage { get; set; }
        public long TotalPages { get; set; }
        public long TotalItems { get; set; }
        public long ItemsPerPage { get; set; }
    }

    public class LanguageModel : BaseViewModel
    {
        public int Id { get; set; }
        public string LanguageName { get; set; }
        public string LanguageCode { get; set; }
        public string LanguageDirection { get; set; }
        public string FileType { get; set; }
    }
}
