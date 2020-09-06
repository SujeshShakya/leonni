using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Leonni.Models.ViewModels
{
    public class CommentModel : BaseViewModel
    {
        public long ID { get; set; }
        public string Date { get; set; }
        public string Body { get; set; }
        public System.Guid ThreadID { get; set; }
        public System.Guid UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public Nullable<long> ProfileId { get; set; }
        public Nullable<long> FileId { get; set; }
        public bool ShowLike { get; set; }
        public int LikeCount { get; set; }
        public bool ShowDelete { get; set; }
        public string CurrentUserRole { get; set; }
        // Navigation properties

        //public List<CommentThread> CommentThread { get; set; }
    }
}
