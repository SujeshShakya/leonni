using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Leonni.Models.ViewModels
{
    public class GroupContentModel : BaseViewModel
    {

        public List<PublicationModel> Publications { get; set; }
        public List<ActivityModel> Activities { get; set; }
        public List<GalleryModel> Galleries { get; set; }
    }
}
