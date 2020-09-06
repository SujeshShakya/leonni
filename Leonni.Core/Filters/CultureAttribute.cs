using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using Leonni.Core.Controllers;
using Leonni.Interfaces;
using Leonni.Models;
using Leonni.Models.ModelMappers;
using Leonni.Models.ViewModels;


namespace Leonni.Core.Filters
{
    public class CultureAttribute : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var controller = (filterContext.Controller as LeonniApplicationController);
            if (controller != null)
            {
                var viewModel = (filterContext.Controller.ViewData.Model as BaseViewModel);
                if (viewModel != null)
                {
                    viewModel.SiteLanguages =  LanguageMap.Map(controller.SiteLanguages);
                    viewModel.CurrentLanguage =  LanguageMap.Map(controller.CurrentLanguage);
                }
            }
            else
            {
                throw new Exception("Always derieve controller from LeonniApplicationController");
            }

        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var repo = DependencyResolver.Current.GetService<ILanguageRepository>();
            var controller = (filterContext.Controller as LeonniApplicationController);
            if (controller != null)
            {
                var currentCulture = Thread.CurrentThread.CurrentCulture.ToString();

                List<Language> lst = new List<Language>();

                //Language objLanguage = new Language { LanguageCode = "en-US", Id = 1, LanguageName = "English",LanguageDirection="LTR" };
                //lst.Add(objLanguage);
                controller.SiteLanguages = repo.GetList().ToList();
                //expectation: A neutral and Regional dialect of same language won't be supported at once
                //for example: if en is supported then en-US or en-GB etc won't be supported. May require change if that is the case.
                controller.CurrentLanguage = controller.SiteLanguages.Where(x => currentCulture.StartsWith(x.LanguageCode, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            }
            else
            {
                throw new Exception("Always derieve controller from LeonniApplicationController");
            }
        }

    }
}
