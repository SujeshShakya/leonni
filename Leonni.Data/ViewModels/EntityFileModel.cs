using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Leonni.Models.ViewModels
{
    public class EntityFileModel : BaseViewModel
    {
        public long Id { get; set; }
        public Nullable<int> SectionId { get; set; }
        public Nullable<long> EntityId { get; set; }
        public Nullable<long> FileId { get; set; }

        public SectionModel Section { get; set; }
    }
}
