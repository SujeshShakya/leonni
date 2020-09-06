using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Leonni.Models.ViewModels
{
    public class UserIndexModel : BaseViewModel
    {
        public Nullable<long> AdvertisingBanner { get; set; }
        public string VideoUrl { get; set; }
        public List<FrontEntityFile> BannerList { get; set; }
    }
}
