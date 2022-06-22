using System;
using System.Web.Mvc;
using System.Web.Security;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class WelcomeController : CustomController
    {
        public ActionResult Index()
        {
            if (!HasPermission())
            {
                Response.Redirect("/");
            }

            if (IsSuperAdmin)
            {
                return RedirectToAction("Index", "Domain");
            }

            return RedirectToAction("General", "Setting");
        }

        public void LogOut()
        {
            var myCookies = Request.Cookies.AllKeys;
            foreach (string cookie in myCookies)
            {
                Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
            }
            FormsAuthentication.SignOut();
        }

    }
}
