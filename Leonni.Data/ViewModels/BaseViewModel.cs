using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;

namespace Leonni.Models.ViewModels
{
    public class BaseViewModel
    {
        ////set by UserNameFilter
        public bool IsAuthenticated { get; set; }
        public LoginModel CurrentUser { get; set; }
        public string CurrentRole { get; set; }
        public bool IsLeonniTeam { get; set; }

        ////set by CultureAttribute
        public List<LanguageModel> SiteLanguages { get; set; }
        public LanguageModel CurrentLanguage { get; set; }

        //set by Dropdownfilterattribute
        public SelectList Countries { get; set; }
        public SelectList Provinces { get; set; }
        public SelectList Categories { get; set; }
        public SelectList Years { get; set; }
        public SelectList Months { get; set; }
        public SelectList Sorts { get; set; }
        public SelectList Disciplines { get; set; }
        public SelectList Sexs { get; set; }

        public static DefaultBaseViewModel Default { get { return new DefaultBaseViewModel(); } }

        public LoginModel LoginModel { get; set; }
    }

    public class DefaultBaseViewModel : BaseViewModel
    {
    }


}
