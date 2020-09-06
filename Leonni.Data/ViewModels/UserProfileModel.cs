using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Leonni.Models.ViewModels
{
    public class UserProfileModel : BaseViewModel
    {
        public long Id { get; set; }
        public System.Guid UserId { get; set; }
        
        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string SurName { get; set; }
        
        [Required]
        public int CategoryId { get; set; }
        
        [Required]
        public int DisciplineId { get; set; }

        //[Required]
        public string Summary { get; set; }

        //[Required]
        public string Description { get; set; }
        public string Occupation { get; set; }
        public string Company { get; set; }
        public string Sex { get; set; }
        
        [Required]
        public DateTime BirthDate { get; set; }

        public string Date { get; set; }
        
        public string Phone { get; set; }
        public string WebLink { get; set; }
        
        [Required(ErrorMessage="Choose Country")]
        public string CountryName { get; set; }
        
        //[Required(ErrorMessage="Choose State")]
        public int StateId { get; set; }
        public string Locality { get; set; }

        [Required]
        public string Address { get; set; }
        public string ZipCode { get; set; }
        //[Display(Name="Profile pic")]
        //[Required(ErrorMessage="Choose a picture")]
        public HttpPostedFileBase ProfilePic { get; set; }

        public bool IsNew { get; set; }

        public ProfileSearchModel ProfileSearch { get; set; }

        public CategoryModel Category { get; set; }

        public DisciplineModel Discipline { get; set; }

        public int TotalListCount { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public long? FileId { get; set; }

        public bool IsLeonniTeam { get; set; }

        public int StatusId { get; set; }

        public ProvinceModel Province { get; set; }

        public string Email { get; set; }
        public int Age { get; set; }

        public int ViewCount { get; set; }
        public int LikeCount { get; set; }
        public int CommentCount { get; set; }
    }
}
