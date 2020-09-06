using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Leonni.Models.ViewModels
{
    public class ProjectModel : BaseViewModel
    {
        public long Id { get; set; }
        [Required]
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public int DisciplineId { get; set; }
        public string CountryName { get; set; }
        public int StateId { get; set; }
        public string Cost { get; set; }
        public string Inversion { get; set; }
        public string Resume { get; set; }
        public string Description { get; set; }
        public string YoutubeLink { get; set; }
        public string Link { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<bool> IsDisabled { get; set; }
        public Nullable<long> DeletedBy { get; set; }
        public Nullable<long> DisabledBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public bool IsNew { get; set; }
        public CategoryModel Category { get; set; }

        public DisciplineModel Discipline { get; set; }

        public UploadModel ProjectUploads { get; set; }

        public ProvinceModel Province { get; set; }

        public DateTime? ModifiedDate { get; set; }


    }

}
