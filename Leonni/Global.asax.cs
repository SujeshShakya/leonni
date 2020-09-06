using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using FluentFilters;
using Leonni.Core;
using Leonni.Core.Filters;
using FluentFilters.Criteria;
using Leonni.Core.Routing;

namespace Leonni
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {

        public static void RegisterGlobalFilters(GlobalFilterCollection globalfilters, FluentFilterCollection fluentFilters)
        {
            globalfilters.Add(new HandleErrorAttribute(), 1);
            //globalfilters.Add(new CompressFilter(), 2);
            //fluentFilters.Add<SecurityAttribute>(x => x.Exclude(new ControllerFilterCriteria("Security")), 4);
            fluentFilters.Add<CultureAttribute>(x => x.Exclude(new AreaFilterCriteria("SysAdmin")), 5);
            fluentFilters.Add<UsernameFilterAttribute>();
            fluentFilters.Add<DropdownFilterAttribute>();
            //fluentFilters.Add<UserRoleFilterAttribute>(x=>x.Exclude(new AreaFilterCriteria("SysAdmin")));

            //fluentFilters.Add<AdminRolePermissionFilterAttribute>(x =>
            //{
            //    x.Require(new AreaFilterCriteria("SysAdmin"));
            //    x.Exclude(new ControllerFilterCriteria("AdminAccount")).And(new ActionFilterCriteria("LogOn")).Or(new ActionFilterCriteria("LogOff"));
            //});
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var localizingRoute = new AutoLocalizingRoute(
           "{language}/{controller}/{action}/{id}",
           new { area = "usersection", controller = "Front", action = "Index", id = UrlParameter.Optional },
           new { language = @"^[A-Za-z]{2}(-[A-Za-z]{2})?$" }
       );

            routes.Add("LocalizingRoute", localizingRoute);

            routes.MapRoute(
              "Default", // Route name
              "{controller}/{action}/{id}", // URL with parameters
              new { area = "usersection", controller = "Front", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            FilterProviders.Providers.Add(FluentFiltersBuider.Filters);
            RegisterGlobalFilters(GlobalFilters.Filters, FluentFiltersBuider.Filters); // Register filters
            RegisterRoutes(RouteTable.Routes);

            //BundleTable.Bundles.RegisterTemplateBundles();
        }
    }
}