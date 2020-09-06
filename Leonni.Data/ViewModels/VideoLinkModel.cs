using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Leonni.Models.ViewModels
{
    public class VideoLinkModel : BaseViewModel
    {
        public long Id { get; set; }
        //[Required]
        public string VideoLinkUrl { get; set; }
        public Nullable<long> SectionId { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
    }
}
