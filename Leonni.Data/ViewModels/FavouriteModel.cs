using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Leonni.Models.ViewModels
{
   public class FavouriteModel : BaseViewModel
    {
        // Primitive properties

        public long Id { get; set; }
        public int SectionId { get; set; }
        public long EntityId { get; set; }
        public long UserProfileId { get; set; }

        // Navigation properties

        public SectionModel Section { get; set; }
    }
}
