using System.Web.Mvc;
using Leonni.Models;
using System.Collections.Generic;
using Leonni.Models.ViewModels;
using System.Security.Principal;
using System.Web.Security;
using System;

namespace Leonni.Core.Controllers
{
    public abstract class LeonniApplicationController : Controller
    {
        // will be set by Username filter (Enmask.Core.Filters.UserNameFilter).
        public LoginModel CurrentUser { get; set; }
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string CurrentRole { get; set; }
        public bool IsLeonniTeam { get; set; }

        //// will be set by Culture filter (Enmask.Core.Filters.CultureAttribute).
        public List<Language> SiteLanguages { get; set; }
        public Language CurrentLanguage { get; set; }

        //set by Dropdownfilterattribute
        public SelectList Countries { get; set; }
        public SelectList Provinces { get; set; }
        public SelectList Categories { get; set; }
        public SelectList Years { get; set; }
        public SelectList Months { get; set; }
        public SelectList Sorts { get; set; }
        public SelectList Disciplines { get; set; }
        public SelectList Sexs { get; set; }
        
    }
}
