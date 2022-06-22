using System;
using System.Web.Mvc;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Mailers;
using Bkav.eGovCloud.Web.Framework;
using Mvc.Mailer;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class DiagnoseController : CustomController
    {
        private readonly IUserMailer _userMailer;
        private readonly ResourceBll _resourceService;

        public DiagnoseController(IUserMailer userMailer,
                                ResourceBll resourceService)
            : base()
        {
            _userMailer = userMailer;
            _resourceService = resourceService;
        }

        public ActionResult Index()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Diagnose.NotPermission"));
                ErrorNotification(_resourceService.GetResource("Customer.Diagnose.NotPermission"));
                return RedirectToAction("Index", "Welcome");
            }

            return View();
        }

        public ActionResult Cookie()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Diagnose.NotPermission"));
                ErrorNotification(_resourceService.GetResource("Customer.Diagnose.NotPermission"));
                return RedirectToAction("Index");
            }

            var principal = User as CustomerPrincipal;
            if (principal == null)
            {
                return null;
            }

            var identity = principal.Identity as CustomerIdentity;
            return identity == null ? null : View(identity.CookieData);
        }

        public ActionResult EmailSettings()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Diagnose.NotPermission"));
                ErrorNotification(_resourceService.GetResource("Customer.Diagnose.NotPermission"));
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "DiagnoseEmailSettings")]
        public JsonResult EmailSettings(string email)
        {
            bool success = false;

            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Diagnose.NotPermission"));
                ErrorNotification(_resourceService.GetResource("Customer.Diagnose.NotPermission"));
                return Json(new { success });
            }

            var mailMessage = _userMailer.Test();
            mailMessage.To.Add(email);
            try
            {
                mailMessage.Send(_userMailer.SmtpClient);
                success = true;
            }
            catch (Exception ex)
            {
                LogException(ex);
                success = false;
            }

            return Json(new { success });
        }
    }
}
