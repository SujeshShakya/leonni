using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Leonni.Models.ViewModels
{
    public class LikeModel : BaseViewModel
    {
        public long Id { get; set; }
        public Nullable<long> EntityId { get; set; }
        public string EntityName { get; set; }
        public Nullable<long> ProfileId { get; set; }
    }
}
