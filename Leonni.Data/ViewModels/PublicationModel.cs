using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Leonni.Models.ViewModels
{
    public class PublicationModel : BaseViewModel
    {
        // Primitive properties

        public long Id { get; set; }
        public long GroupId { get; set; }
        public long ProjectId { get; set; }
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public int DisciplineId { get; set; }
        public string Resume { get; set; }
        public string Description { get; set; }
        public Nullable<long> PhotoId { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<bool> IsDisabled { get; set; }
        public Nullable<long> DeletedBy { get; set; }
        public Nullable<long> DisabledBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public long CreatedBy { get; set; }

        public SelectList Groups { get; set; }
        public SelectList Projects { get; set; }

        public CategoryModel Category { get; set; }

        public DisciplineModel Discipline { get; set; }
        public UserProfileModel UserProfile { get; set; }

        // Navigation properties

        public ProjectModel Project { get; set; }

        public bool IsNew { get; set; }

        public int Status { get; set; }
    }
}
