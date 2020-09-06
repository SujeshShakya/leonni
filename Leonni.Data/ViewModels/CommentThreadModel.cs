using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Leonni.Models.ViewModels
{
    public class CommentThreadModel : BaseViewModel
    {
        public System.Guid ID { get; set; }
        public string URL { get; set; }
        public Nullable<int> SectionId { get; set; }
        public Nullable<long> EntityID { get; set; }


        // Navigation properties

        //public virtual ICollection<Comment> Comments { get; set; }
    
    }
}
