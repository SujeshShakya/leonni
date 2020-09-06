using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Leonni.Models.ViewModels
{
    public class FrontEntityFileModel
    {
        public long Id { get; set; }
        public long SectionId { get; set; }
        public long EntityId { get; set; }
        public long FileId { get; set; }
    }
}
