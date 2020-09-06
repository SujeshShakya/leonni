using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web.Configuration;

namespace Leonni.Core
{
    public static class AppSettings
    {
        public static string UploadFlagImage { get { return ConfigurationManager.AppSettings["UploadFlagImage"] ?? ""; } }

        public static bool RefreshTranslationCache { get { return bool.Parse(ConfigurationManager.AppSettings["RefreshTranslationCache"] ?? "false"); } }

        public static int FormsAuthTimeOut
        {
            get
            {
                // Get the Web application configuration.
                Configuration configuration = WebConfigurationManager.OpenWebConfiguration("/");

                // Get the external Authentication section.
                AuthenticationSection authenticationSection = (AuthenticationSection)configuration.GetSection("system.web/authentication");

                // Get the external Forms section .
                FormsAuthenticationConfiguration formsAuthentication = authenticationSection.Forms;

                return Convert.ToInt32(Math.Round(formsAuthentication.Timeout.TotalMinutes, 0));
            }
        }

        public static string PaypalUrl
        {
            get
            {
                return (ConfigurationManager.AppSettings["PaypalUrl"] ?? "");
            }
        }
        public static string PaypalReturnUrl 
        {
            get
            {
                return (ConfigurationManager.AppSettings["PaypalReturnUrl"] ?? "");
            }
        }
        public static string PaypalCancelReturnUrl
        {
            get
            {
                return (ConfigurationManager.AppSettings["PaypalCancelReturnUrl"] ?? "");
            }
        }
        public static string PaypalBusinessEmail
        {
            get
            {
                return (ConfigurationManager.AppSettings["PaypalBusinessEmail"] ?? "");
            }
        }
        public static string PaypalHeaderImageUrl
        {
            get
            {
                return (ConfigurationManager.AppSettings["PaypalHeaderImageUrl"] ?? "");
            }
        }

        public static string SiteUrlBase
        {
            get
            {
                return (ConfigurationManager.AppSettings["SiteUrlBase"] ?? "");
            }
        }
    }
}
