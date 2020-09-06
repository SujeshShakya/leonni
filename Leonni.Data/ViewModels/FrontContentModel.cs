using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Leonni.Models.ViewModels
{
    public class FrontContentModel : BaseViewModel
    {
        public long Id { get; set; }
        public long SectionId { get; set; }
        public long ContentId { get; set; }
        public System.DateTime CreateDate { get; set; }

        public List<UserProfileModel> UserProfiles { get; set; }
        public List<GroupModel> Groups { get; set; }
    }

}
