using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Web.Framework;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class UnitController : CustomController
    {

        private readonly ResourceBll _resourceService;
        private readonly UnitBll _unitService;
        private readonly InCatalogBll _inCatalogService;
        private readonly InCatalogValueBll _inCatalogValueService;

        public UnitController(UnitBll unitService, 
            ResourceBll resourceService,
            InCatalogValueBll incataLogValueService,
            InCatalogBll incataLogService) : base()
        {
            _unitService = unitService;
            _resourceService = resourceService;
            _inCatalogValueService = incataLogValueService;
            _inCatalogService = incataLogService;
        }
        [HttpGet]
        public ActionResult Index()
        {
            ModelState.Clear();
            var model = new Ad_UnitModel
            {
                Ad_UnitModels = _unitService.GetsAs(c =>
                new Ad_UnitModel
                {
                    IdUnit = c.IdUnit,
                    Unit = c.Unit,
                    Exchange=c.Exchange,
                    OriginalUnit=c.OriginalUnit,
                    Description = c.Description,
                    Use = c.Use
                })
            };

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
        public ActionResult Index(Ad_UnitModel model)
        {
            model.Ad_UnitModels = _unitService.GetsAs(c =>
            new Ad_UnitModel
            {
                IdUnit = c.IdUnit,
                Unit = c.Unit,
                Exchange=c.Exchange,
                OriginalUnit=c.OriginalUnit,
                Description = c.Description,
                Use = c.Use
            });

            if (!ModelState.IsValid) return View(model);
            try
            {
                //create
                if (model.IdUnit == default(Guid))
                {
                    var catalog = model.ToEntity();
                    _unitService.Create(catalog);
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.IndicatorCatalog.Created"));
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.IndicatorCatalog.Created"));
                }
                else
                {
                    var catalog = _unitService.Get(model.IdUnit);
                    if (catalog == null)
                        return RedirectToAction("Index");
                    catalog = model.ToEntity(catalog);
                    _unitService.Update(catalog);
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.IndicatorCatalog.Updated"));
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.IndicatorCatalog.Updated"));

                }
                return RedirectToAction("Index");
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
            var ad_Unit = _unitService.Get(id);
            if (ad_Unit == null)
            {
                return null;
            }
            var model = ad_Unit.ToModel();

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Remove(Guid id)
        {
            var ad_Unit = _unitService.Get(id);
            var catalogValue = _inCatalogValueService.Gets(d => d.Unit == id);
            if(catalogValue.Count() > 0)
            {
                var listError = new KTSuccess();
                listError.isActive = false;
                return Json(listError, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var listError = new KTSuccess();
                if (ad_Unit == null)
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ActionLevel.NotExist"));
                    ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ActionLevel.NotExist"));
                    return Json(null, JsonRequestBehavior.AllowGet);
                }
                try
                {
                    _unitService.Remove(ad_Unit);     
                    listError.isActive = true;
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
                return Json(listError, JsonRequestBehavior.AllowGet);
            }   
            
        }

        public JsonResult DeleteMul(List<Guid> model)
        {
            var listError = new KTSuccess();
            foreach (var id in model)
            {
                var ad_Unit = _unitService.Get(id);
                var catalogValue = _inCatalogValueService.Gets(d => d.Unit == id);
                if (catalogValue.Count() > 0)
                {
                    var listErrors = new KTSuccess();
                    listErrors.isActive = false;
                    return Json(listErrors, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    
                    if (ad_Unit == null)
                    {
                        CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ActionLevel.NotExist"));
                        ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ActionLevel.NotExist"));
                        return Json(null, JsonRequestBehavior.AllowGet);
                    }
                    try
                    {
                        _unitService.Remove(ad_Unit);
                        listError.isActive = true;
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
            }
            return Json(listError, JsonRequestBehavior.AllowGet);
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
                    var unit = new Ad_UnitModel();
                    do
                    {
                        data = wordSheet.Cells[startRow + i, startColumn].Value;
                        object ExChangeUnit = wordSheet.Cells[startRow + i, startColumn + 1].Value;
                        object OriginalUnit_ = wordSheet.Cells[startRow + i, startColumn + 2].Value;
                        object Description_ = wordSheet.Cells[startRow + i, startColumn + 3].Value;
                        if (data != null && Description_ != null && ExChangeUnit != null && OriginalUnit_ != null)
                        {
                            // read data
                            unit.Unit = data.ToString();
                            unit.Exchange = ExChangeUnit.ToString();
                            unit.Use = true;
                            unit.OriginalUnit = OriginalUnit_.ToString();
                            unit.Description = Description_.ToString();
                            var modelEnd = unit.ToEntity();
                            _unitService.Create(modelEnd);
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

            return RedirectToAction("Index", "Unit");
        }
    }
    public class KTSuccess
    {
        public bool isActive { get; set; }
    }
}