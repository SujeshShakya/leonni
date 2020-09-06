using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Leonni.Models.ViewModels
{
    public class AdvertisementModel : BaseViewModel
    {
        public int CategoryId { get; set; }
        public string CountryName { get; set; }
        public int CountryId { get; set; }
        public int ProvinceId { get; set; }
        public int DisciplineId { get; set; }
        public long Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Sex { get; set; }
        public int SectionId { get; set; }
        public string BeginningDay { get; set; }
        public System.DateTime DateCreated { get; set; }
        public System.DateTime DateModified { get; set; }
        public int UserProfileId { get; set; }
        public string EngineeredTo { get; set; }

    }
}
