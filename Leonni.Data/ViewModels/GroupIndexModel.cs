using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Leonni.Models.ViewModels
{
    public class GroupIndexModel :BaseViewModel
    {
        public int CategoryId { get; set; }
        public string CountryName { get; set; }
        public int CountryId { get; set; }
        //public List<DisciplineModel> Disciplines { get; set; }       

        public int ProvinceId { get; set; }
        public int DisciplineId { get; set; }
        public int SortId { get; set; }

        public Nullable<long> AdvertisingBanner { get; set; }
        public string VideoUrl { get; set; }
        public List<FrontEntityFile> BannerList { get; set; }
    }
}
