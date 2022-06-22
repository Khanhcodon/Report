using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovCloud.Entities;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class PrinterController : CustomController// BaseController
    {
        private readonly ResourceBll _resourceService;
        private readonly AdminGeneralSettings _generalSettings;
        private readonly PrinterBll _printerService;
        private readonly UserBll _userService;
        private readonly PositionBll _positionService;
        private readonly DepartmentBll _departmentService;


        public PrinterController(
            ResourceBll resourceService,
            AdminGeneralSettings generalSettings,
            PrinterBll printerService,
            UserBll userService,
            PositionBll positionService,
            DepartmentBll departmentService)
            : base()
        {
            _resourceService = resourceService;
            _generalSettings = generalSettings;
            _printerService = printerService;
            _userService = userService;
            _positionService = positionService;
            _departmentService = departmentService;
        }

        public ActionResult Index()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Printer.NotPermission"));
                ErrorNotification(_resourceService.GetResource("Customer.Printer.NotPermission"));
                return RedirectToAction("Index", "Welcome");
            }

            var printers = _printerService.Gets();
            var model = printers.ToListModel();
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Printer.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.Printer.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            var model = new PrinterModel();
            ViewBag.AllPrinter = GetPrinters();
            ViewBag.IsCreate = true;
            GetDefaultInfo();
            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "PrinterCreate")]
        public ActionResult Create(PrinterModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Printer.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.Printer.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            ViewBag.AllPrinter = GetPrinters();
            ViewBag.IsCreate = true;
            if (ModelState.IsValid)
            {
                try
                {

                    var check = CheckConnect(model.PrinterName);
                    if (!check)
                    {
                        CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Printer.ConnectionError"));
                        ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Printer.ConnectionError"));
                        return View(model);
                    }
                    var printer = model.ToEntity();
                    _printerService.Create(printer);
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Printer.Created"));
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Printer.Created"));
                    return RedirectToAction("Index");
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                    return View(model);
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Printer.NotPermissionUpdate"));
                ErrorNotification(_resourceService.GetResource("Customer.Printer.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            ViewBag.AllPrinter = GetPrinters();
            ViewBag.IsCreate = false;
            var printer = _printerService.Get(id);
            if (printer == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Printer.NotFoundObject"));
                ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Printer.NotFoundObject"));
                return RedirectToAction("Index");
            }
            GetDefaultInfo();
            var model = printer.ToModel();
            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "PrinterEdit")]
        public ActionResult Edit(PrinterModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Printer.NotPermissionUpdate"));
                ErrorNotification(_resourceService.GetResource("Customer.Printer.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            ViewBag.AllPrinter = GetPrinters();
            ViewBag.IsCreate = false;
            if (ModelState.IsValid)
            {
                try
                {
                    var printer = _printerService.Get(model.PrinterId);
                    if (printer == null)
                    {
                        CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Printer.NotFoundObject"));
                        ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Printer.NotFoundObject"));
                        return RedirectToAction("Index");
                    }
                    var check = CheckConnect(model.PrinterName);
                    if (!check)
                    {
                        CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Printer.ConnectionError"));
                        ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Printer.ConnectionError"));
                        return View(model);
                    }
                    printer = model.ToEntity(printer);
                    _printerService.Update(printer);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Printer.Updated"));
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Printer.Updated"));
                    return RedirectToAction("Index");
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                    return View(model);
                }
            }
            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "PrinterDelete")]
        public ActionResult Delete(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Printer.NotPermissionDelete"));
                ErrorNotification(_resourceService.GetResource("Customer.Printer.NotPermissionDelete"));
                return RedirectToAction("Index");
            }

            var printer = _printerService.Get(id);
            if (printer == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Printer.DeletedError"));
                ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Printer.DeletedError"));
                return RedirectToAction("Index");
            }
            _printerService.Delete(printer);
            CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Printer.DeletedSuccess"));
            SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Printer.DeletedSuccess"));
            return RedirectToAction("Index");
        }

        //public JsonResult ChangeShared(int id, bool shared)
        //{
        //    var printer = _printerService.Get(id);
        //    if (printer == null)
        //    {
        //        return Json(new { success = false, message = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Printer.NotFoundObject") }, JsonRequestBehavior.AllowGet);
        //    }
        //    printer.IsShared = shared;
        //    _printerService.Update(printer);
        //    return Json(new { success = true, message = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Printer.Updated") }, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult ChangeActivities(int id, bool activities)
        {
            var printer = _printerService.Get(id);
            if (printer == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Printer.NotFoundObject"));
                return Json(new { success = false, message = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Printer.NotFoundObject") });
            }
            printer.IsActivated = activities;
            _printerService.Update(printer);
            CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Printer.Updated"));
            return Json(new { success = true, message = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Printer.Updated") });
        }

        public JsonResult TestConnection(string printerName)
        {
            bool success = false;
            if (string.IsNullOrEmpty(printerName))
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Printer.PrinterNameIsNull"));
                return Json(new
                                {
                                    success = success,
                                    message = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Printer.PrinterNameIsNull")
                                }, JsonRequestBehavior.AllowGet);
            }
            success = CheckConnect(printerName);
            var message = success
                           ? _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Printer.ConnectionSuccess")
                           : _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Printer.ConnectionError");
            CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Printer.ConnectionError"));
            return Json(new
                            {
                                success = success,
                                message = message
                            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Kiểm tra kết nối đến máy in
        /// </summary>
        /// <param name="printerName">Tên máy in</param>
        /// <returns></returns>
        private bool CheckConnect(string printerName)
        {
            System.Drawing.Printing.PrintDocument pd = new System.Drawing.Printing.PrintDocument();
            pd.PrinterSettings.PrinterName = string.Format(@"{0}", printerName.Replace(@"/", @"\"));
            return pd.PrinterSettings.IsValid;
        }

        /// <summary>
        /// Lấy dnah sách máy in ở máy
        /// </summary>
        /// <returns></returns>
        private List<SelectListItem> GetAllprinerFromComputer()
        {
            var printers = System.Drawing.Printing.PrinterSettings.InstalledPrinters;
            List<SelectListItem> selectListItem = new List<SelectListItem>();
            foreach (string item in printers)
            {
                selectListItem.Add(new SelectListItem
                {
                    Value = item,
                    Text = item
                });
            }
            return selectListItem;
        }

        /// <summary>
        /// Lấy danh sách những máy in kết nối với server
        /// </summary>
        /// <returns></returns>
        private List<SelectListItem> GetPrinters()
        {
            var printerQuery = new ManagementObjectSearcher("SELECT * from Win32_Printer");
            var printers = printerQuery.Get();
            var results = new List<SelectListItem>();
            if (printers != null && printers.Count > 0)
            {
                foreach (var printer in printers)
                {
                    var name = printer.GetPropertyValue("Name");
                    //var status = printer.GetPropertyValue("Status");
                    //var isDefault = printer.GetPropertyValue("Default");
                    //var isNetworkPrinter = printer.GetPropertyValue("Network");
                    results.Add(new SelectListItem
                    {
                        Value = name.ToString(),
                        Text = name.ToString()
                    });
                }
            }
            return results;
        }

        /// <summary>
        /// Lấy ra danh sách người dùng, phòng ban, vị trí cho vào viewbag nhằm autocomplete
        /// </summary>
        private void GetDefaultInfo()
        {
            var allUsers = _userService.GetAllCached(true).OrderBy(u => u.Username);
            ViewBag.AllUsers = allUsers != null ? allUsers.Select(
                                u =>
                                new
                                {
                                    value = u.UserId,
                                    label = u.Username + " - " + u.FullName,
                                }).StringifyJs() : "[]";

            var allDepartments = _departmentService.GetCacheAllDepartments(true);
            ViewBag.AllDepartments = allDepartments != null ? allDepartments
                        .Select(d =>
                            new
                            {
                                label = d.DepartmentPath,
                                value = d.DepartmentId,
                                departmentName = d.DepartmentName,
                                parentId = d.ParentId
                            }
                        )
                        .OrderBy(d => d.label).StringifyJs() : "[]";

            var allPositions = _positionService.GetCacheAllPosition();
            ViewBag.AllPositions = allPositions != null ? allPositions
                        .Select(d =>
                            new
                            {
                                label = d.PositionName,
                                value = d.PositionId,
                            }
                        )
                        .OrderBy(d => d.label).StringifyJs() : "[]";
        }
    }
}