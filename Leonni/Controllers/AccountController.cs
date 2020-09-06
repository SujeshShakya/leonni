using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Leonni.Models;
using Leonni.Core.Controllers;
using Leonni.Core;



namespace Leonni.Controllers
{

    public class AccountController : LeonniApplicationController
    {
        private readonly string userRegisterKey = "(#$&%@(#$JV)*(09)_(_)9klsfkljagl49ig;l*JAMHHG15∙∙∙∙ksa8ij5l9954aglk;dfhsgalkjh0N#%^&BSVB%sdfsfhsffmdb.kalagsdfsdf";
        private string RegistrationConfirmationLink(string email)
        {
            var currentLang = CurrentLanguage.LanguageCode == null ? "en-us" : CurrentLanguage.LanguageCode;
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + '/';

            string token = Encryptor.EncryptString(email, userRegisterKey);
            token = token.Replace("/", "-");


            string link = baseUrl + currentLang + "/account/confirm/" + HttpUtility.UrlEncode(token);

            return link;

        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return ContextDependentView();
        }

        //
        // POST: /Account/JsonLogin

        [AllowAnonymous]
        [HttpPost]
        public JsonResult JsonLogin(Leonni.Models.ViewModels.BaseViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                //if (Roles.IsUserInRole(model.UserName, "Moderate"))
                //{
                if (Membership.ValidateUser(model.CurrentUser.UserName, model.CurrentUser.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.CurrentUser.UserName, model.CurrentUser.RememberMe);
                    return Json(new { status = "success", redirect = returnUrl });
                }
                else
                {
                    ModelState.AddModelError("", "The Username / Password provided is incorrect. Please try again.--- Forgot your data access? <br/> No Problem. <br/> Just enter the email address and click on 'RecoverPassword' and an email will be sent to that address. ");
                }
            }
            //}
            //else
            //{
            //    ModelState.AddModelError("", "UnAuthorized User");
            //}

            // If we got this far, something failed
            return Json(new { status = "error", errors = GetErrorsFromModelState() });
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(Leonni.Models.ViewModels.LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                //if (Roles.IsUserInRole(model.UserName, "Moderate"))
                //{
                if (System.Web.Security.Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                //    else
                //    {
                //        ModelState.AddModelError("", "The user name or password provided is incorrect.");
                //    }
                //}
                else
                {
                    ModelState.AddModelError("", "UnAuthorized User");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Front");
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return ContextDependentView();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult JsonRegister(string username, string password)
        {
            if (ModelState.IsValid)
            {
                var objUser = Membership.GetUser(username);
                if (objUser != null)
                {
                    ModelState.AddModelError("", "The email address is already registered in the system. Please try again.");
                    return Json(new { errors = GetErrorsFromModelState(), status = "error" });
                }
                else
                {
                    // Attempt to register the user
                    MembershipCreateStatus createStatus;
                    System.Web.Security.Membership.CreateUser(username, password, username, passwordQuestion: null, passwordAnswer: null, isApproved: false, providerUserKey: null, status: out createStatus);

                    if (createStatus == MembershipCreateStatus.Success)
                    {
                        //FormsAuthentication.SetAuthCookie(username, createPersistentCookie: false);

                        string link = this.RegistrationConfirmationLink(username);
                        EmailSender.RegistrationConfirmationEmail(email: username, activationLink: link);

                        RoleProvider objRoleProvider = Roles.Providers["DefaultRoleProvider"];

                        string[] arrUsername = new string[1] { username };
                        string[] arrRole = new string[1] { "Leonni User" };

                        objRoleProvider.AddUsersToRoles(arrUsername, arrRole);
                        return Json(new { success = true });
                        //@sujesh
                        //Do not login the user here, just display a message to check his email and click on that link.
                    }
                    else
                    {
                        ModelState.AddModelError("", ErrorCodeToString(createStatus));
                    }
                    return Json(new { errors = GetErrorsFromModelState(), status = "success" });
                }
            }

            // If we got this far, something failed
            return Json(new { errors = GetErrorsFromModelState(), status = "error" });
        }

        [AllowAnonymous]
        public ActionResult Confirm(string id)
        {
            id = HttpUtility.UrlDecode(id).Replace(' ', '+').Replace('-', '/');
            string email = Encryptor.DecryptString(id, userRegisterKey).Trim();

            var user = Membership.GetUser(email);
            if (user != null)
            {
                user.IsApproved = true;
                Membership.UpdateUser(user);

                TempData["messagetype"] = "user_account_activated";
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, email,
                              DateTime.Now,
                              DateTime.Now.AddDays(AppSettings.FormsAuthTimeOut), false, "",
                              FormsAuthentication.FormsCookiePath);

                // Encrypt the ticket.
                string encTicket = FormsAuthentication.Encrypt(ticket);

                // Create the cookie.
                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
                return RedirectToAction("Front", "ControlPanel");
            }

            return View("UserRegistrationSuccess");
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.Email, model.Password, model.Email, passwordQuestion: null, passwordAnswer: null, isApproved: false, providerUserKey: null, status: out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    RoleProvider objRoleProvider = Roles.Providers["DefaultRoleProvider"];

                    //string[] arrUsername = new string[1] { username };
                    string[] arrRole = new string[1] { "Leonni User" };

                    //objRoleProvider.AddUsersToRoles(arrUsername, arrRole);

                    EmailSender.RegistrationConfirmationEmail(model.Email, RegistrationConfirmationLink(model.Email));

                    TempData["messagetype"] = "user_confirmation_mail_sent";

                    return RedirectToAction("message", "home");
                    //@sujesh, change this redirect code to sth appropriate
                    //just display a message to check his email and click on that link.

                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = System.Web.Security.Membership.GetUser(User.Identity.Name, userIsOnline: true);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        public ActionResult RecoverPassword(string email)
        {
            if (ModelState.IsValid)
            {
                var objUser = Membership.GetUser(email);
                if (objUser != null)
                {
                    string newPassword = objUser.ResetPassword();
                    EmailSender.RecoverPassword(email, email, newPassword);
                    return Json(new { successMessage = "The new password has been sent in your mail account. Please check and login with the new password", status = "success" });
                }
                else
                {

                    return Json(new { errors = GetErrorsFromModelState(), status = "error" });
                }
            }

            // If we got this far, something failed
            return Json(new { errors = GetErrorsFromModelState(), status = "error" });
        }


        private ActionResult ContextDependentView()
        {
            string actionName = ControllerContext.RouteData.GetRequiredString("action");
            if (Request.QueryString["content"] != null)
            {
                ViewBag.FormAction = "Json" + actionName;
                return PartialView();
            }
            else
            {
                ViewBag.FormAction = actionName;
                return View();
            }
        }

        private IEnumerable<string> GetErrorsFromModelState()
        {
            return ModelState.SelectMany(x => x.Value.Errors.Select(error => error.ErrorMessage));
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
