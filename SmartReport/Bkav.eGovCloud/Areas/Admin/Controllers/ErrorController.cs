using System.Web.Mvc;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    //[RequireHttps]
    public class ErrorController : Controller
    {
        //
        // GET: /Admin/Error/

        public ActionResult Index()
        {
            return View();
        }

    }
}
