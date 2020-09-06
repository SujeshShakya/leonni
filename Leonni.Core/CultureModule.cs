using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Leonni.Models;
using System.Text.RegularExpressions;
using System.Web.Routing;
using System.Threading;
using System.Globalization;
using System.Web.Mvc;
using Leonni.Interfaces;

namespace Leonni.Core
{
    public class CultureModule : IHttpModule
    {
        public const string DEFAULT_LANGUAGE = "en-US";
        public void Dispose()
        {

        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(OnBeginRequest);
        }


        public void OnBeginRequest(Object s, EventArgs e)
        {
            var repo = DependencyResolver.Current.GetService<ILanguageRepository>();
            HttpApplication app = s as HttpApplication;
            //string url = app.Request.Url.AbsolutePath.ToString();
            var currentContext = new HttpContextWrapper(HttpContext.Current);
            if (currentContext.Request.Url.ToString().ToLower().Contains(".axd"))
                return;

            if (currentContext.Request.Url.ToString().ToLower().Contains("/scripts/"))
                return;

            if (currentContext.Request.Url.ToString().ToLower().Contains("/content/"))
                return;

            var routeData = RouteTable.Routes.GetRouteData(currentContext);
            
            if (routeData != null)
            {
                var area = (routeData.Values["area"] ?? "").ToString();
                var action = (routeData.Values["action"] ?? "").ToString();
                var controller = (routeData.Values["controller"] ?? "").ToString();
                if (string.Compare(action, "ChangeLanguage", true) == 0)
                {
                    var redirectUrl = currentContext.Request.UrlReferrer.ToString();
                    var cookie = new HttpCookie("lang_Leonni", (routeData.Values["id"] ?? "en-US").ToString());
                    currentContext.Response.SetCookie(cookie);
                    currentContext.Response.Redirect(redirectUrl, true);
                    return;
                }
                else if (string.Compare(controller, "CaptchaFont", true) == 0)
                {
                    return;
                }
                else if (string.Compare(area, "SysAdmin", true) == 0)
                {
                    return;
                }
            }
            //try get language from cookie
            var languageCode = (app.Request.Cookies["lang_Leonni"] ?? new HttpCookie("lang_Leonni", "")).Value;
            if (!string.IsNullOrWhiteSpace(languageCode))
            {
                //validate that user has not changed the cookie.
                var dbLang = repo.GetSingle(x=>x.LanguageCode==languageCode);
                if (dbLang == null)
                {
                    languageCode = string.Empty;
                }
            }

            if (string.IsNullOrWhiteSpace(languageCode))
            {
                //If not cookie language is set, try getting language from the list of Browser languages.
                var userLanguages = app.Request.UserLanguages;
                if (userLanguages != null)
                {
                    foreach (var userLanguage in userLanguages)
                    {
                        Regex reg1 = new Regex(@";.+");//support language strings like en-us;q=0.7
                        var uLanguage = reg1.Replace(userLanguage, "");
                        if (string.Compare(uLanguage, DEFAULT_LANGUAGE, true) == 0)
                        {
                            languageCode = DEFAULT_LANGUAGE;
                            break;
                        }

                        var dbLang = repo.GetSingle(x => x.LanguageCode == uLanguage);
                            //Language.FindSingleBy.LanguageCode(uLanguage);
                        if (dbLang != null)
                        {
                            languageCode = dbLang.LanguageCode;
                            break;
                        }
                    }
                }
            }

            //if both cookie language is not found and browser languages are not supported by the site, set language to default language
            if (string.IsNullOrWhiteSpace(languageCode))
            {
                languageCode = DEFAULT_LANGUAGE;
            }

            if (routeData != null && routeData.Values.Count > 0)
            {
                if (string.Compare((routeData.Values["area"] ?? "").ToString(), "SysAdmin", true) != 0)
                {
                    string routeLanguageCode = (routeData.Values["language"] ?? "").ToString();
                    if (string.Compare(routeLanguageCode, languageCode, true) != 0)
                    {
                        routeData.Values["language"] = languageCode;
                        
                        //redirect only if the request is a GET request, so as not to disturb forms.
                        //May require change if it is absolutely necessary to change language in URL, for other methods than GET.
                        if (string.Compare(HttpContext.Current.Request.HttpMethod, "GET", true) == 0)
                        {
                            RedirectToUrlWithAppropriateLanguage(currentContext, routeData);
                        }
                        Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(languageCode);
                        //No further processing is required.
                        return;
                    }
                    languageCode = (string)routeData.Values["language"];
                }
            }

            try
            {
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(languageCode);
                Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(languageCode); //CultureInfo.CreateSpecificCulture(languageCode);
            }
            catch 
            {
                var cookie = new HttpCookie("lang_Leonni", "en-US");
                app.Response.Cookies.Add(cookie);
                routeData.Values["language"] = languageCode;
                RedirectToUrlWithAppropriateLanguage(currentContext, routeData);
            }
        }

        private void RedirectToUrlWithAppropriateLanguage(HttpContextWrapper currentContext, RouteData routeData)
        {
            if (string.Compare((routeData.Values["controller"] ?? "").ToString(), "home", true) == 0 && string.Compare((routeData.Values["action"] ?? "").ToString(), "index", true) == 0)
            {
                routeData.Values["controller"] = "";
                routeData.Values["action"] = "";
            }
            routeData.Values["area"] = "usersection";
            HttpContext.Current.Response.RedirectToRoute(routeData.Values);
        }
    }
}
