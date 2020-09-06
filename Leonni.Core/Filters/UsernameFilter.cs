using System;
using System.Web.Mvc;
using System.Web.Security;
using Leonni.Core.Controllers;
using Leonni.Models.ViewModels;
using Leonni.Interfaces;

namespace Leonni.Core.Filters
{
    public class UsernameFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controller = (filterContext.Controller as LeonniApplicationController);

            if (controller != null)
            {
                if (filterContext.HttpContext != null && filterContext.HttpContext.User != null && filterContext.HttpContext.User.Identity != null)
                {

                    if (filterContext.HttpContext.User.Identity.IsAuthenticated)
                    {

                        MembershipUser objUser = Membership.GetUser(filterContext.HttpContext.User.Identity.Name);
                        RoleProvider objRoleProvider = Roles.Providers["DefaultRoleProvider"];
                        var repo = DependencyResolver.Current.GetService<IUserProfileRepository>();

                        controller.CurrentUser = new LoginModel();

                        if (objUser != null)
                        {
                            controller.CurrentUser.UserId = (Guid)objUser.ProviderUserKey;
                            controller.CurrentUser.UserName = objUser.UserName;
                            var objUserProfile = repo.GetSingle(x => x.UserId == (Guid)objUser.ProviderUserKey);
                            if (objUserProfile != null)
                            {
                                controller.IsLeonniTeam = repo.GetSingle(x => x.UserId == (Guid)objUser.ProviderUserKey).IsLeonniTeam;
                            }
                            else
                            {
                                controller.IsLeonniTeam = false;
                            }
                            //objRoleProvider.
                            controller.CurrentRole = objRoleProvider.GetRolesForUser(filterContext.HttpContext.User.Identity.Name)[0];
                        }

                    }
                }
            }
            else
            {
                throw new Exception("Always derive controller from LeonniApplicationController");
            }

            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var controller = (filterContext.Controller as LeonniApplicationController);

            if (controller != null)
            {
                var viewModel = (filterContext.Controller.ViewData.Model as BaseViewModel);
                if (viewModel != null)
                {
                    LoginModel objUser = controller.CurrentUser; //User.FindSingleBy.UserName(filterContext.HttpContext.User.Identity.Name);

                    if (objUser != null)
                    {
                        viewModel.CurrentUser = objUser;
                        viewModel.CurrentRole = controller.CurrentRole;
                        viewModel.IsLeonniTeam = controller.IsLeonniTeam;
                    }
                }
            }
            else
            {
                throw new Exception("Always derive controller from LeonniApplicationController");
            }

            base.OnActionExecuted(filterContext);
        }

    }


}
