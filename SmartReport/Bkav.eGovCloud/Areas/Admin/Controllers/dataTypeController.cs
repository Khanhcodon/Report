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
using OfficeOpenXml;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{


    [EgovAuthorize]
    public class dataTypeController : CustomController
    {
        private readonly AdminGeneralSettings _generalSettings;
        private readonly IndicatorCatalogBll _indicatorCatalogService;
        private readonly ResourceBll _resourceService;
        private readonly CodeBll _codeService;
        private readonly DocTypeBll _docTypeService;
        private readonly CategoryBll _categoryService;
        private readonly InCatalogBll _inCatalogService;
        private readonly InCatalogValueBll _inCatalogValueService;
        private readonly dataTypeBll _dataTypeSevice;

        public dataTypeController(IndicatorCatalogBll indicatorCatalogService,
            ResourceBll resourceService,
            AdminGeneralSettings generalSettings,
            CategoryBll categoryService,
            InCatalogBll inCatalogService,
            InCatalogValueBll inCatalogValueService,
            dataTypeBll indicatorSevice)
            : base()
        {
            _indicatorCatalogService = indicatorCatalogService;
            _resourceService = resourceService;
            _generalSettings = generalSettings;
            _categoryService = categoryService;
            _inCatalogService = inCatalogService;
            _inCatalogValueService = inCatalogValueService;
            _dataTypeSevice = indicatorSevice;
        }

        [HttpGet]
        public ActionResult Index()
        {
            ModelState.Clear();
            var model = new dataTypeModel
            {
                Indicators = _dataTypeSevice.GetsAs(c =>new dataTypeModel
                    {
                    dataTypeId = c.dataTypeId,
                    dataTypeName = c.dataTypeName,
                    dataTypeDescription = c.dataTypeDescription,
                    nameID= c.nameID,
                    distribute = c.distribute,
                    IsActivated = c.IsActivated
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

        public JsonResult GetDataTypes()
        {
            var dataTypes = _dataTypeSevice.GetsSelects();
            return Json(new { data = dataTypes }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Index(dataTypeModel model)
        {
            model.Indicators = _dataTypeSevice.GetsAs(c =>
            new dataTypeModel
            {
                dataTypeId = c.dataTypeId,
                nameID = c.nameID,
                dataTypeName = c.dataTypeName,
                distribute = c.distribute,
                dataTypeDescription = c.dataTypeDescription, 
                IsActivated = c.IsActivated
            });

            if (!ModelState.IsValid) return View(model);
            try
            {
                //create
                if(model.dataTypeId == default(Guid))
                {
                    var catalog = model.ToEntity();
                    _dataTypeSevice.Create(catalog);
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.IndicatorCatalog.Created"));
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.IndicatorCatalog.Created"));
                }
                else
                {
                    var catalog = _dataTypeSevice.Get(model.dataTypeId);
                    if (catalog == null)
                        return RedirectToAction("Index");
                    catalog = model.ToEntity(catalog);
                    _dataTypeSevice.Update(catalog);
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

        public JsonResult Edit(Guid idzz)
        {
            var indicator = _dataTypeSevice.Get(idzz);
            if (indicator == null)
            {
                return null;
            }
            var model = indicator.ToModel();

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(Guid idzz)
        {
            var listError = new KTSuccess();
            var indicator = _dataTypeSevice.Get(idzz);
            var catalogValue = _inCatalogValueService.Gets(d => d.Type == idzz);
            if (catalogValue.Count() > 0)
            {
                var listErrors = new KTSuccess();
                listErrors.isActive = false;
                return Json(listErrors, JsonRequestBehavior.AllowGet);
            }
            else
            {    
                if (indicator == null)
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ActionLevel.NotExist"));
                    ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ActionLevel.NotExist"));
                    return RedirectToAction("Index");
                }

                try
                {
                    _dataTypeSevice.Delete(indicator);
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
            return Json(listError, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteMul(List<Guid> model)
        {
            var listError = new KTSuccess();
            foreach (var id in model)
            {      
                var indicator = _dataTypeSevice.Get(id);
                var catalogValue = _inCatalogValueService.Gets(d => d.Type == id);
                if (catalogValue.Count() > 0)
                {
                    var listErrors = new KTSuccess();
                    listErrors.isActive = false;
                    return Json(listErrors, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    if (indicator == null)
                    {
                        CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ActionLevel.NotExist"));
                        ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ActionLevel.NotExist"));
                        return RedirectToAction("Index");
                    }

                    try
                    {
                        _dataTypeSevice.Delete(indicator);
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
                    object dataNameId = null;
                    var dataType = new dataTypeModel();
                    do
                    {
                        dataNameId = wordSheet.Cells[startRow + i, startColumn].Value;
                        object dataTypeName_= wordSheet.Cells[startRow + i, startColumn + 1].Value;
                        object dataTypeDes_ = wordSheet.Cells[startRow + i, startColumn + 2].Value;
                        object distribute_ = wordSheet.Cells[startRow + i, startColumn + 3].Value;
                        if (dataNameId != null && dataTypeName_ != null && dataTypeDes_ != null && distribute_ != null)
                        {
                            // read data
                            dataType.nameID = dataNameId.ToString();
                            dataType.dataTypeName = dataTypeName_.ToString();
                            dataType.IsActivated = true;
                            dataType.dataTypeDescription = dataTypeDes_.ToString();
                            dataType.distribute = distribute_.ToString();
                            var modelEnd = dataType.ToEntity();
                            _dataTypeSevice.Create(modelEnd);
                            i += 1;
                        }
                    }
                    while (dataNameId != null);

                    TempData["Success"] = "SS";
                }
                else
                {
                    TempData["Error"] = "ER";
                }
            }

            return RedirectToAction("Index", "DataType");
        }

    }
}