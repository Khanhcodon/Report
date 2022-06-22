using System.Web.Mvc;
using Bkav.eGovCloud.Business;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;

namespace Bkav.eGovCloud.Controllers
{
    //[RequireHttps]
    public class ErrorController : Controller
    {
        private readonly UserBll _userService;
        private readonly LogBll _logService;

        public ErrorController(UserBll userService, LogBll logService)
        {
            _userService = userService;
            _logService = logService;
        }

        public ActionResult AccessDenied(string pageUrl)
        {
            var user = _userService.CurrentUser;
            _logService.Information(string.Format("Người dùng '{0}' bị từ chối truy cập trang {1}", user.UsernameEmailDomain, pageUrl));
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}