using System.Web.Mvc;

namespace Bkav.eGovCloud.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Welcome", action = "Index", id = UrlParameter.Optional },
                new[] { "Bkav.eGovCloud.Areas.Admin.Controllers" }
            );
        }
    }
}