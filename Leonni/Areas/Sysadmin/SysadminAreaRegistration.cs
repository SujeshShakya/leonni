using System.Web.Mvc;

namespace Leonni.Areas.SysAdmin
{
    public class SysAdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "SysAdmin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {

            context.MapRoute
               (
               "SysAdmin_default",
               "SysAdmin/{controller}/{action}/{id}",
               new { area = "SysAdmin", Controller = "Admin", action = "Login", id = UrlParameter.Optional }
           );
        }
    }
}
