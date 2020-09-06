using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Leonni.Models;
using Leonni.Core.Controllers;
using System.Web;
using System.Text;
using Leonni.Models.ViewModels;
using Leonni.Models.ModelMappers;
using System.Threading;
using System;
using Leonni.Interfaces;
using System.Web.Mvc.Html;
using System.Web.Security;

namespace Leonni.Core.Extensions
{
    public static class HtmlHelperExtensions
    {

        public static MvcHtmlString RenderGlobalMessage(this HtmlHelper Html)
        {
            StringBuilder htmlBuilder = new StringBuilder();

            //var validationSummary = (Html.ValidationSummary(false, "Please Correct:") ?? new MvcHtmlString("")).ToString();
            var validationSummary = new MvcHtmlString("").ToString();
            var errorMessage = (Html.ViewData["ErrorMessage"] ?? Html.ViewContext.TempData["ErrorMessage"] ?? "").ToString();
            var successMessage = (Html.ViewData["SuccessMessage"] ?? Html.ViewContext.TempData["SuccessMessage"] ?? "").ToString();

            if (!string.IsNullOrWhiteSpace(errorMessage) || !string.IsNullOrWhiteSpace(validationSummary))
            {
                htmlBuilder.Append("<div class='global-error-message'>");
                htmlBuilder.Append("<div class='message-closer'><span>×</span></div>");
                htmlBuilder.Append("Following Error(s) has occured while processing your request:");
                if (!string.IsNullOrWhiteSpace(errorMessage))
                {
                    htmlBuilder.Append("<div>");
                    htmlBuilder.Append(errorMessage);
                    htmlBuilder.Append("</div>");
                }
                if (!string.IsNullOrWhiteSpace(validationSummary))
                {
                    htmlBuilder.Append("<div>");
                    htmlBuilder.Append(validationSummary);
                    htmlBuilder.Append("</div>");
                }
                htmlBuilder.Append("</div>");
            }
            else if (!string.IsNullOrWhiteSpace(successMessage))
            {
                htmlBuilder.Append("<div class='global-success-message'>");
                htmlBuilder.Append("<div class='message-closer'><span>×</span></div>");
                htmlBuilder.Append("<div>");
                htmlBuilder.Append(successMessage);
                htmlBuilder.Append("</div>");
                htmlBuilder.Append("</div>");
            }
            return new MvcHtmlString(htmlBuilder.ToString());
        }

        public static MvcHtmlString LanguageDropDown(this HtmlHelper Html)
        {
            var repo = DependencyResolver.Current.GetService<ILanguageRepository>();
            StringBuilder htmlBuilder = new StringBuilder();
            if (Html.ViewData != null)
            {
                var viewModel = Html.ViewData.Model as BaseViewModel;
                if (Html.ViewData.Model is HandleErrorInfo)
                {
                    var currentCulture = Thread.CurrentThread.CurrentCulture.ToString();
                    viewModel = new BaseViewModel();
                    try
                    {
                        viewModel.SiteLanguages = LanguageMap.Map(repo.GetList().ToList());
                    }
                    catch
                    {
                        viewModel.SiteLanguages = LanguageMap.Map(new List<Language>() { new Language { LanguageCode = "en-US", Id = 1 } });
                    }

                    try
                    {
                        viewModel.CurrentLanguage = viewModel.SiteLanguages.Where(x => currentCulture.StartsWith(x.LanguageCode, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                    }
                    catch
                    {
                        viewModel.CurrentLanguage = LanguageMap.Map(new Language { LanguageCode = "en-US", Id = 1 });
                    }

                }

                if (viewModel != null)
                {
                    if (viewModel.SiteLanguages != null)
                    {
                        viewModel.CurrentLanguage = viewModel.CurrentLanguage ?? LanguageMap.Map(new Language { LanguageCode = "en-US", Id = 1 });
                        UrlHelper Url = new UrlHelper(Html.ViewContext.RequestContext);
                        SelectList list = new SelectList(viewModel.SiteLanguages, "LanguageCode", "LanguageName", viewModel.CurrentLanguage.LanguageCode);

                        if (list != null)
                        {
                            htmlBuilder.Append("<div class='dropdown-list-widget'>");
                            htmlBuilder.Append("<ul class='dropdown-list language-list' data-role='dropdown-list'>");
                            foreach (SelectListItem item in list)
                            {
                                if (item.Selected)
                                {
                                    htmlBuilder.AppendFormat("<li data-role='dropdown-list-item' data-drop-down-item-value='{0}' class='dropdown-list-item selected'><a href='{2}'><img src=\"{1}\" alt=\"left\" />{3}</a></li>", item.Value, Url.Content("~/content/images/flags/" + item.Value + ".png"), Url.Action("ChangeLanguage", new { id = item.Value }), item.Text);
                                }
                                else
                                {
                                    htmlBuilder.AppendFormat("<li data-role='dropdown-list-item' data-drop-down-item-value='{0}' class='dropdown-list-item'><a href='{2}'><img src=\"{1}\" alt=\"left\" />{3}</a></li>", item.Value, Url.Content("~/content/images/flags/" + item.Value + ".png"), Url.Action("ChangeLanguage", new { id = item.Value }), item.Text);
                                }
                            }
                            htmlBuilder.Append("</ul>");
                            htmlBuilder.Append("</div>");
                        }
                        //end if
                    }
                    //end if
                }
                //end if
            }
            //end if
            return new MvcHtmlString(htmlBuilder.ToString());
        }
        //end function

        public static MvcHtmlString UsersAdminOptions(this HtmlHelper Html, long id, int SectionId)
        {
            var repoUserProfile = DependencyResolver.Current.GetService<IUserProfileRepository>();

            var objProfile = repoUserProfile.GetSingle(x => x.Id == id);  ///to check user is in the leonni team or not
            StringBuilder htmlBuilder = new StringBuilder();

            var repoFrontContent = DependencyResolver.Current.GetService<IFrontContentRepository>();
            var objFrontContent = repoFrontContent.GetSingle(x => x.ContentId == id && x.SectionId == SectionId);  ///To check content is public or not
            ///
            if (objProfile.IsLeonniTeam == false && objFrontContent == null)  ///if user is not in  the team and not public
            {
                htmlBuilder.Append("<div class='adminOptions'><ul><li id='view" + id + "' onclick='View(" + id + "," + SectionId + ")'>View</li><li id='Public" + id + "' onclick='AddRemovePublic(" + id + "," + SectionId + ")'>Make Public</li><li onclick='DeleteContent(" + id + ",this," + SectionId + ")'>Delete</li><li id='Team" + id + "' onclick='AddRemoveFromTeam(" + id + ")'>Add to Team</li><li onclick='DisableContent(" + id + ",this)'>Disable</li></ul></div>");
            }
            else if (objProfile.IsLeonniTeam == true && objFrontContent == null) ///if user is  in  the team and not public
            {
                htmlBuilder.Append("<div class='adminOptions'><ul><li id='view" + id + "' onclick='View(" + id + "," + SectionId + ")'>View</li><li id='Public" + id + "' onclick='AddRemovePublic(" + id + "," + SectionId + ")'>Make Public</li><li onclick='DeleteContent(" + id + ",this," + SectionId + ")'>Delete</li><li id='Team" + id + "' onclick='AddRemoveFromTeam(" + id + ")'>Remove From Team</li><li onclick='DisableContent(" + id + ",this)'>Disable</li></ul></div>");
            }
            else if (objProfile.IsLeonniTeam == true && objFrontContent != null) ///if user is  in  the team and is public
            {
                htmlBuilder.Append("<div class='adminOptions'><ul><li id='view" + id + "' onclick='View(" + id + "," + SectionId + ")'>View</li><li id='Public" + id + "' onclick='AddRemovePublic(" + id + "," + SectionId + ")'>Remove Public</li><li onclick='DeleteContent(" + id + ",this," + SectionId + ")'>Delete</li><li id='Team" + id + "' onclick='AddRemoveFromTeam(" + id + ")'>Remove From Team</li><li onclick='DisableContent(" + id + ",this)'>Disable</li></ul></div>");
            }
            else if (objProfile.IsLeonniTeam == false && objFrontContent != null) ///if user is  in not the team and is public
            {
                htmlBuilder.Append("<div class='adminOptions'><ul><li id='view" + id + "' onclick='View(" + id + "," + SectionId + ")'>View</li><li id='Public" + id + "' onclick='AddRemovePublic(" + id + "," + SectionId + ")'>Remove Public</li><li onclick='DeleteContent(" + id + ",this," + SectionId + ")'>Delete</li><li id='Team" + id + "' onclick='AddRemoveFromTeam(" + id + ")'>Add To Team</li><li onclick='DisableContent(" + id + ",this)'>Disable</li></ul></div>");
            }
            return new MvcHtmlString(htmlBuilder.ToString());

        }

        public static MvcHtmlString AdminOptions(this HtmlHelper Html, long id, int SectionId)
        {
            var repoUserProfile = DependencyResolver.Current.GetService<IUserProfileRepository>();
            StringBuilder htmlBuilder = new StringBuilder();

            var repoFrontContent = DependencyResolver.Current.GetService<IFrontContentRepository>();
            var objFrontContent = repoFrontContent.GetSingle(x => x.ContentId == id && x.SectionId == SectionId);  ///To check content is public or not

            if (objFrontContent == null)  ///if user is not in  the team and not public
            {
                htmlBuilder.Append("<div class='adminOptions'><ul><li id='view" + id + "' onclick='ViewGroup(" + id + "," + SectionId + ")'>View</li><li id='Public" + id + "' onclick='AddRemovePublic(" + id + ")'>Make Public</li><li onclick='RemoveContent(" + id + ",this)'>Remove</li><li id='disable" + id + "' onclick='EnableDisable(" + id + ")'>Disable</li><li onclick='Highlight(" + id + ",this)'>Highlight</li></ul></div>");
            }
            else
            {
                htmlBuilder.Append("<div class='adminOptions'><ul><li id='view" + id + "' onclick='ViewGroup(" + id + "," + SectionId + ")'>View</li><li id='Public" + id + "' onclick='AddRemovePublic(" + id + ")'>Remove Public</li><li onclick='DeleteContent(" + id + ",this)'>Delete</li><li id='disable" + id + "' onclick='EnableDisable(" + id + ")'>Disable</li><li onclick='Highlight(" + id + ",this)'>Highlight</li></ul></div>");
            }

            return new MvcHtmlString(htmlBuilder.ToString());

        }

        public static MvcHtmlString LeonniUserOptions(this HtmlHelper Html, long id, int SectionId)
        {
            StringBuilder htmlBuilder = new StringBuilder();

            htmlBuilder.Append("<div class='adminOptions'><ul><li onclick='DeleteContent(" + id + ",this," + SectionId + ")'>Delete</li><li id='view" + id + "' onclick='View(" + id + "," + SectionId + ")'>View</li><li onclick='DisableContent(" + id + ",this,"+ SectionId +")'>Disable</li></ul></div>");


            return new MvcHtmlString(htmlBuilder.ToString());

        }
        public static MvcHtmlString MyAccountAndLogOffPanel(this HtmlHelper Html)
        {
            StringBuilder htmlBuilder = new StringBuilder();
            if (Html.ViewData != null)
            {
                var viewModel = Html.ViewData.Model as BaseViewModel;
                if (Html.ViewData.Model is HandleErrorInfo)
                {
                    var currentCulture = Thread.CurrentThread.CurrentCulture.ToString();
                    viewModel = new BaseViewModel();
                    var controller = (Html.ViewContext.Controller as LeonniApplicationController);
                    if (controller != null)
                    {
                        // viewModel.CurrentUser = controller.CurrentUser;
                    }
                    else
                    {
                        //viewModel.CurrentUser = new MembershipUser { UserName = "Annonymous" };
                    }
                }

                if (viewModel != null)
                {
                    if (viewModel.IsAuthenticated)
                    {
                        htmlBuilder.AppendLine("<div class=\"logoff-container\">");
                        //@nrip- http://projects.tylersoftware.com/issues/111, old code: htmlBuilder.AppendLine(Html.Translate("layout", "welcome") + "<strong>" + viewModel.CurrentUser.UserName + "</strong>&nbsp;[");
                        htmlBuilder.AppendLine("<strong>" + viewModel.CurrentUser.UserName + "</strong>&nbsp;[");
                        htmlBuilder.AppendLine(Html.ActionLink(@Html.Translate("layout", "myaccount").ToString(), "CaptchaSoln", "CaptchaSoln").ToString());
                        htmlBuilder.AppendLine("]&nbsp;" + Html.ActionLink(@Html.Translate("layout", "logoff").ToString(), "LogOff", "Account") + "</div>");
                    }
                }
                //end if
            }
            //end if
            return new MvcHtmlString(htmlBuilder.ToString());
        }
        //end function

        public static MvcHtmlString Translate(this HtmlHelper htmlHelper, string groupName, string key)
        {
            var repo = DependencyResolver.Current.GetService<ITranslationRepository>();


            Language objLanguage = (htmlHelper.ViewContext.Controller as LeonniApplicationController).CurrentLanguage ?? new Language { LanguageCode = "en-US", Id = 1, LanguageName = "English" };
            List<Translation> lstTranslations = HttpRuntime.Cache[groupName + objLanguage.LanguageCode] as List<Translation>;

            if (lstTranslations == null || AppSettings.RefreshTranslationCache)
            {

                lstTranslations = repo.GetList(x => x.GroupName == groupName && x.LanguageId == objLanguage.Id).ToList();
                //Translation.FindAllBy.GroupName.And.LanguageId(groupName, objLanguage.Id);
                HttpRuntime.Cache.Insert(groupName + objLanguage.LanguageCode, lstTranslations);
            }

            string translatedValue = lstTranslations.Where(x => x.Key == key).DefaultIfEmpty(new Translation { Value = key }).SingleOrDefault().Value;
            if (string.IsNullOrEmpty(translatedValue))
            {
                translatedValue = key;
            }

            if (!string.IsNullOrEmpty(translatedValue))
            {
                string applicationPath = "/";
                if (HttpContext.Current != null)
                {
                    applicationPath = HttpContext.Current.Request.ApplicationPath;
                    if (applicationPath != "/")
                    {
                        applicationPath += "/";
                    }
                }
                translatedValue = translatedValue.Replace("~/", applicationPath);
            }
            return MvcHtmlString.Create(translatedValue);
        }


    }
    //end class
}
