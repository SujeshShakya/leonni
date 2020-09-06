using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Leonni.Models.ViewModels
{
    public class GalleryModel : BaseViewModel
    {
        public long Id { get; set; }
        public long GroupId { get; set; }
        public long ProjectId { get; set; }
        public string Title { get; set; }
        public int DisciplineId { get; set; }
        public string Resume { get; set; }
        public string Description { get; set; }

        // Navigation properties

        public virtual DisciplineModel Discipline { get; set; }
        public virtual ProjectModel Project { get; set; }
        public virtual GroupModel Group { get; set; }

        public UploadModel GalleryUploads { get; set; }
    }

}
