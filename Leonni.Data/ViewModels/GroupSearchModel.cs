using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Leonni.Models.ViewModels
{
    public class GroupSearchModel : BaseViewModel
    {
        //public GroupModel GroupModel { get; set; }

        public string Title { get; set; }
        public string CountryName { get; set; }
        public int DisciplineId { get; set; }
        public int StateId { get; set; }
        public string SortBy { get; set; }
    }
}
