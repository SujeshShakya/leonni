using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Leonni.Models.ViewModels
{
    public class HomeIndexModel:BaseViewModel
    {
        public int SortId { get; set; }
        public int CategoryId { get; set; }
        public string CountryName { get; set; }
        public int YearId { get; set; }
        public int MonthId { get; set; }
        public int CountryId { get; set; }
        public int ProvinceId { get; set; }
        public int DisciplineId { get; set; }
        public Nullable<long> AdvertisingBanner { get; set; }
        public string VideoUrl { get; set; }
        public List<FrontEntityFile> BannerList { get; set; }
    }
}
