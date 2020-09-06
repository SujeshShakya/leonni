using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Leonni.Models.ViewModels
{
    public class ProfileSearchModel : BaseViewModel
    {
        public long Id { get; set; }
        public System.Guid UserId { get; set; }

        public string FirstName { get; set; }
        public string SurName { get; set; }
        public int CategoryId { get; set; }
        public int DisciplineId { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string Occupation { get; set; }
        public string Company { get; set; }
        public string Sex { get; set; }
        public string Phone { get; set; }
        public string WebLink { get; set; }
        public string CountryName { get; set; }
        public int StateId { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }

        public string SortBy { get; set; }

        public int YearId { get; set; }
        public int MonthId { get; set; }


    }
}
