using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Web;
using System.Web.Mvc;
using Bkav.eGovCloud.Admin.Models;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Helper;
using Bkav.eGovCloud.Web.Framework;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using System.Data;
using System.Xml;
using Bkav.eGovCloud.Business.Utils;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Configuration;
using ClosedXML.Excel;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using DocumentFormat.OpenXml.Spreadsheet;
using System.IO;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    public class DisaggregationController : CustomController
    {
        private readonly AdminGeneralSettings _generalSettings;
        private readonly IndicatorCatalogBll _indicatorCatalogService;
        private readonly ResourceBll _resourceService;
        private readonly CodeBll _codeService;
        private readonly DocTypeBll _docTypeService;
        private readonly CategoryBll _categoryService;
        private readonly InCatalogBll _inCatalogService;
        private readonly InCatalogValueBll _inCatalogValueService;
        private readonly DisaggregationBll _indicatorSevice;

        public DisaggregationController(IndicatorCatalogBll indicatorCatalogService,
            ResourceBll resourceService,
            AdminGeneralSettings generalSettings,
            CategoryBll categoryService,
            InCatalogBll inCatalogService,
            InCatalogValueBll inCatalogValueService,
            DisaggregationBll indicatorSevice)
            : base()
        {
            _indicatorCatalogService = indicatorCatalogService;
            _resourceService = resourceService;
            _generalSettings = generalSettings;
            _categoryService = categoryService;
            _inCatalogService = inCatalogService;
            _inCatalogValueService = inCatalogValueService;
            _indicatorSevice = indicatorSevice;
        }

        [HttpGet]
        public ActionResult Index()
        {
            ModelState.Clear();
            var model = new DisaggregationModel
            {
                Indicators = _indicatorSevice.GetsAs(c =>
new DisaggregationModel
{
IndicatorId = c.IndicatorId,
IndicatorName = c.IndicatorName,
IndicatorDesctiption = c.IndicatorDesctiption,
IsActivated = c.IsActivated
})
            };
            if (TempData["Create"] != null && TempData["Update"] != null)
            {
                ViewBag.Creates = TempData["Create"];
                ViewBag.Updates = TempData["Update"];
            }
            else
            {
                ViewBag.Creates = "L";
                ViewBag.Updates = "L";
            }


            if (TempData["Success"] != null)
            {
                ViewBag.Success = TempData["Success"];

            }
            else if (TempData["Error"] != null)
            {
                ViewBag.Error = TempData["Error"];
            }

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Index(DisaggregationModel model)
        {
            model.Indicators = _indicatorSevice.GetsAs(c =>
            new DisaggregationModel
            {
                IndicatorId = c.IndicatorId,
                IndicatorName = c.IndicatorName,
                IndicatorDesctiption = c.IndicatorDesctiption,
                IsActivated = c.IsActivated
            });

            if (!ModelState.IsValid) return View(model);
            try
            {
                //create
                if (model.IndicatorId == default(Guid))
                {
                    var catalog = model.ToEntity();
                    _indicatorSevice.Create(catalog);
                    TempData["Create"] = "TC";
                    TempData["Update"] = "L";
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.IndicatorCatalog.Created"));
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.IndicatorCatalog.Created"));
                }
                else
                {
                    var catalog = _indicatorSevice.Get(model.IndicatorId);
                    if (catalog == null)
                        return RedirectToAction("Index");
                    catalog = model.ToEntity(catalog);
                    _indicatorSevice.Update(catalog);
                    TempData["Create"] = "L";
                    TempData["Update"] = "TC";
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.IndicatorCatalog.Updated"));
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.IndicatorCatalog.Updated"));
                }
                return RedirectToAction("Index", "Disaggregation");
            }
            catch (EgovException ex)
            {
                LogException(ex);
                CreateActivityLog(ActivityLogType.Admin, ex.Message);
                ErrorNotification(ex.Message);
            }
            return View(model);
        }

        public JsonResult Edit(Guid id)
        {
            var indicator = _indicatorSevice.Get(id);
            if (indicator == null)
            {
                return null;
            }
            var model = indicator.ToModel();

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            var indicator = _indicatorSevice.Get(id);
            if (indicator == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ActionLevel.NotExist"));
                ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ActionLevel.NotExist"));
                return RedirectToAction("Index");
            }

            try
            {
                _indicatorSevice.Delete(indicator);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ActionLevel.Deleted.Success"));
                SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ActionLevel.Deleted.Success"));
            }
            catch (EgovException ex)
            {
                LogException(ex);
                CreateActivityLog(ActivityLogType.Admin, ex.Message);
                ErrorNotification(ex.Message);
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteMul(List<Guid> model)
        {
            foreach (var id in model)
            {
                var indicator = _indicatorSevice.Get(id);
                if (indicator == null)
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ActionLevel.NotExist"));
                    ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ActionLevel.NotExist"));
                    return RedirectToAction("Index");
                }

                try
                {
                    _indicatorSevice.Delete(indicator);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ActionLevel.Deleted.Success"));
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ActionLevel.Deleted.Success"));
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    CreateActivityLog(ActivityLogType.Admin, ex.Message);
                    ErrorNotification(ex.Message);
                }
                catch (Exception ex)
                {
                    LogException(ex);
                }
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult ExportToExcel(HttpPostedFileBase excelfile)
        {
            var i = 0;
            List<string> dataList = new List<string>();
            if (excelfile != null || excelfile.ContentLength == 0 && excelfile.FileName.EndsWith("xls")
                || excelfile.FileName.EndsWith("xlsx"))
            {
                if (excelfile.ContentType == "application/vnd.ms-excel" ||
                    excelfile.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    string filename = excelfile.FileName;
                    string targetpath = Server.MapPath("~/TempFile/" + filename);
                    excelfile.SaveAs(targetpath);
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    var package = new ExcelPackage(new System.IO.FileInfo(targetpath));
                    int startColumn = 1;
                    int startRow = 2;

                    ExcelWorksheet wordSheet = package.Workbook.Worksheets[0];
                    object data = null;
                    var disgg = new DisaggregationModel();
                    do
                    {
                        data = wordSheet.Cells[startRow + i, startColumn].Value;
                        object ClassName = wordSheet.Cells[startRow + i, startColumn + 1].Value;
                        if (data != null && ClassName != null)
                        {
                            // read data
                            disgg.IndicatorName = data.ToString();
                            disgg.IndicatorDesctiption = ClassName.ToString();
                            disgg.IsActivated = true;
                            var modelEnd = disgg.ToEntity();
                            _indicatorSevice.Create(modelEnd);
                            i += 1;
                        }
                    }
                    while (data != null);

                    TempData["Success"] = "SS";
                }
                else
                {
                    TempData["Error"] = "ER";
                }
            }

            return RedirectToAction("Index", "Disaggregation");
        }

    }
}