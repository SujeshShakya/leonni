using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Leonni.Models.ViewModels
{
    public class GroupModel : BaseViewModel
    {
        public long Id { get; set; }
        public bool IsVisible { get; set; }
        public bool IsCloseIncome { get; set; }
        public bool AllowContents { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public int DisciplineId { get; set; }
        public string Summary { get; set; }
        public string Text { get; set; }
        
        [Required(ErrorMessage = "Choose Country")]
        public string CountryName { get; set; }
        [Required(ErrorMessage = "Choose State")]
        public int ProvinceId { get; set; }
        
        //[Display(Name = "Choose a Picture")]
        //[Required(ErrorMessage = "Choose a picture")]
        public HttpPostedFileBase GroupPic { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<long> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public long CreatedBy { get; set; }

        public bool IsNew { get; set; }
        public string DisciplineName { get; set; }
        public Nullable<long> FileId { get; set; }

       
        public int SortId { get; set; }

        public Nullable<long> AdvertisingBanner { get; set; }
        public string VideoUrl { get; set; }
        public List<FrontEntityFile> BannerList { get; set; }

        public int Status { get; set; }

        public UserProfileModel UserProfile { get; set; }

        public bool isFavourite { get; set; }
        
    }
}
