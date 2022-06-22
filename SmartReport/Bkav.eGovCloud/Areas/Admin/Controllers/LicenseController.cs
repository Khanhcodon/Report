using System.Security.Cryptography;
using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Domain.License;
using Bkav.eGovCloud.Core.License;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovCloud.Entities;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    public class LicenseController : CustomController
    {
        private readonly ResourceBll _resourceService;
        private readonly UserBll _userService;
        private LicenseSettings _licenseSettings;

        public LicenseController(
            ResourceBll resourceService,
            UserBll userBll)
            : base()
        {
            _resourceService = resourceService;
            _userService = userBll;
            _licenseSettings = LicenseSettings.Current;
        }

        public ActionResult Index()
        {
            var license = LicenseSettings.Current;
            if (license == null)
            {
                return View("Activate");
            }

            var licenseInfo = license.Infomation;
            if (licenseInfo == null)
            {
                return View("Activate");
            }

            var model = new LicenseModel
            {
                CustomerName = licenseInfo.CustomerName,
                Email = licenseInfo.Email,
                Phone = licenseInfo.Phone,
                ToDate = licenseInfo.ToDate,
                TotalUser = licenseInfo.TotalUser
            };

            return View(model);
        }

        #region Sử dụng service license của eO

        //[HttpPost]
        //public ActionResult RegisterLicenseEo(RegisterLicenseModel model)
        //{
        //    var service = new eGovLicense.LicenseClient();

        //    var nummberOfUsers = _userService.GetTotalUser();

        //    string licenseKey;
        //    var token = LicenseHelper.GetLicenseToken(model.Key, model.Email, out licenseKey);

        //    var checkLisenseRequest = new eGovLicense.CheckLisenseRequestMessage()
        //    {
        //        PublicKey = licenseKey,
        //        Token = token,
        //        LicenseCode = model.Key,
        //        Email = model.Email,
        //        CurrentNumberOfUsers = nummberOfUsers
        //    };

        //    var response = service.CheckLicense(checkLisenseRequest);
        //    if (response.Status == false)
        //    {
        //        ErrorNotification(response.Message);
        //        return View("Register", model);
        //    }

        //    _licenseSettings.Save(response.Token);
                        
        //    return RedirectToAction("Index");
        //}

        #endregion

        #region License eGov

        //[HttpPost]
        //public ActionResult Register(RegisterLicenseModel model)
        //{
        //    var motherBoardSerial = WmiHelper.GetMotherBoardSerial();
        //    var cpuProcessorId = WmiHelper.GetCpuProcessorId();
        //    var diskDriveSerial = WmiHelper.GetDiskDriveSerial();


        //    var service = new LicenseServiceClient();
        //    var license = service.Register(model.CustomerName, model.Phone, model.Email, motherBoardSerial, cpuProcessorId,
        //        diskDriveSerial, model.Key);

        //    if (string.IsNullOrWhiteSpace(license))
        //    {
        //        ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.License.Create.Error"));
        //        return View(model);
        //    }

        //    _licenseSettings.Save(license);

        //    return RedirectToAction("Index");
        //}

        public ActionResult Activate()
        {
            var licenseInfo = _licenseSettings.Infomation;
            var customerName = "";
            var key = "";
            if (licenseInfo != null)
            {
                customerName = licenseInfo.Email;
                key = licenseInfo.LicenseCode;
            }

            ViewBag.CustomerName = customerName;
            ViewBag.Key = key;
            return View();
        }

        [HttpPost]
        public ActionResult Activate(string name, string key)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(key))
            {
                ErrorNotification("Chưa nhập đầy đủ thông tin.");
                CreateActivityLog(ActivityLogType.Admin, "Chưa nhập đầy đủ thông tin.");
                ViewBag.CustomerName = name;
                ViewBag.Key = key;
                return View();
            }

            var service = new eGovLicense.LicenseClient();
            var response = service.CheckLicense(new eGovLicense.CheckLisenseRequestMessage()
            {
                Email = name,
                LicenseCode = key
            });

            if (response.Status == false)
            {
                ErrorNotification(response.Message);

                ViewBag.CustomerName = name;
                ViewBag.Key = key;
                return View();
            }

            var token = response.Token;
            _licenseSettings.Save(token);

            return RedirectToAction("Index");
        }

        #endregion
    }
}