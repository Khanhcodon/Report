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
    public class CategoryDisaggregationsController : CustomController
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
        private readonly CategoryDisaggregationsBll _categoryDisaggregations;



        public CategoryDisaggregationsController(IndicatorCatalogBll indicatorCatalogService,
            ResourceBll resourceService,
            AdminGeneralSettings generalSettings,
            CategoryBll categoryService,
            InCatalogBll inCatalogService,
            InCatalogValueBll inCatalogValueService,
            DisaggregationBll indicatorSevice,
            CategoryDisaggregationsBll categoryDisaggregationSevice)
            : base()
        {
            _indicatorCatalogService = indicatorCatalogService;
            _resourceService = resourceService;
            _generalSettings = generalSettings;
            _categoryService = categoryService;
            _inCatalogService = inCatalogService;
            _inCatalogValueService = inCatalogValueService;
            _indicatorSevice = indicatorSevice;
            _categoryDisaggregations = categoryDisaggregationSevice;
        }

        [HttpGet]
        public ActionResult Index()
        {
            ModelState.Clear();
            var model = new CategoryDisaggreationModel { CategoryDisagreationModels = _categoryDisaggregations.GetsAs(c =>
            new CategoryDisaggreationModel
            { CategoryDisaggregationId = c.CategoryDisaggregationId,
                IndicatorId = c.IndicatorId,
                CategoryDisaggregationName = c.CategoryDisaggregationName,
                CategoryDisaggregationCode = c.CategoryDisaggregationCode,
                IsActivated = c.IsActivated,
                OrderType = c.OrderType}) };


            var indicators = _indicatorSevice.Gets(c => c.IsActivated == true);
            var indimodel = new List<DisaggregationModel>();
            foreach(var item in indicators)
            {
                indimodel.Add(new DisaggregationModel
                {
                    IndicatorId = item.IndicatorId,
                    IndicatorName = item.IndicatorName,
                    IndicatorDesctiption = item.IndicatorDesctiption,
                    IsActivated = item.IsActivated
                });
            }
            ViewBag.ListLeft = indimodel;
            var Listindicators = new List<SelectListItem>();
            Listindicators.Add(new SelectListItem()
            {
                Selected = true,
                Text = "Chọn loại danh mục phân tổ",
                Value = "00000000-0000-0000-0000-000000000000"
            });
            foreach (var categoryDisList in indicators)
            {
                Listindicators.Add(new SelectListItem()
                {
                    Selected = false,
                    Text = categoryDisList.IndicatorName,
                    Value = categoryDisList.IndicatorId.ToString(),
                });
            }
            ViewBag.ListIndis = Listindicators;

            if (TempData["Create"] != null && TempData["Update"] != null)
            {
                ViewBag.Creates = TempData["Create"];
                ViewBag.Updates = TempData["Update"];
            }
            else if (TempData["Create"] == null && TempData["Update"] == null && TempData["NotId"] == null)
            {
                ViewBag.Creates = "L";
                ViewBag.Updates = "L";
            }
            else
            {
                ViewBag.NotId = TempData["NotId"];
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
        public ActionResult Index(CategoryDisaggreationModel model)
        {
            if(model.IndicatorId != default(Guid))
            {
                model.CategoryDisagreationModels = _categoryDisaggregations.GetsAs(c =>
                new CategoryDisaggreationModel
                {
                    CategoryDisaggregationId = c.CategoryDisaggregationId,
                    IndicatorId = c.IndicatorId,
                    CategoryDisaggregationName = c.CategoryDisaggregationName,
                    CategoryDisaggregationCode = c.CategoryDisaggregationCode,
                    IsActivated = c.IsActivated,
                    OrderType = c.OrderType
                });
            }
            if (!ModelState.IsValid) return View(model);
            try
            {
                //create
                if (model.CategoryDisaggregationId == default(Guid) && model.IndicatorId != default(Guid))
                {
                    var catalog = model.ToEntity();
                    _categoryDisaggregations.Create(catalog);
                    TempData["Create"] = "TC";
                    TempData["Update"] = "L";

                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.IndicatorCatalog.Created"));
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.IndicatorCatalog.Created"));
                }
                else if(model.CategoryDisaggregationId != default(Guid) && model.IndicatorId != default(Guid))
                {
                    var catalog = _categoryDisaggregations.Get(model.CategoryDisaggregationId);
                    if (catalog == null)
                        return RedirectToAction("Index");

                    catalog.CategoryDisaggregationId = model.CategoryDisaggregationId;
                    catalog.IndicatorId = model.IndicatorId;
                    catalog.CategoryDisaggregationName = model.CategoryDisaggregationName;
                    catalog.CategoryDisaggregationCode = model.CategoryDisaggregationCode;
                    catalog.IsActivated = model.IsActivated;
                    _categoryDisaggregations.Update(catalog);

                    TempData["Create"] = "L";
                    TempData["Update"] = "TC";

                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.IndicatorCatalog.Updated"));
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.IndicatorCatalog.Updated"));

                }else
                {
                    TempData["NotId"] = "NotId";
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
            var categoryDis = _categoryDisaggregations.Get(id);
            if (categoryDis == null)
            {
                return null;
            }
            var model = categoryDis.ToModel();
            //var modelCas = new CategoryDisaggregations();
            //modelCas.CategoryDisaggregationId = categoryDis.CategoryDisaggregationId;
            //modelCas.IndicatorId = categoryDis.IndicatorId;
            //modelCas.CategoryDisaggregationName = categoryDis.CategoryDisaggregationName;
            //modelCas.CategoryDisaggregationCode = categoryDis.CategoryDisaggregationCode;
            //modelCas.IsActivated = categoryDis.IsActivated;
            //modelCas.OrderType = categoryDis.OrderType;

            return Json(model, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            var indicator = _categoryDisaggregations.Get(id);
            if (indicator == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ActionLevel.NotExist"));
                ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ActionLevel.NotExist"));
                return RedirectToAction("Index");
            }

            try
            {
                _categoryDisaggregations.Delete(indicator);
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
                var indicator = _categoryDisaggregations.Get(id);
                if (indicator == null)
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ActionLevel.NotExist"));
                    ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ActionLevel.NotExist"));
                    return RedirectToAction("Index");
                }

                try
                {
                    _categoryDisaggregations.Delete(indicator);
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

        /// <summary>
        /// GetIdBy
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetIdBy(Guid id)
        {
            var onlyId = _categoryDisaggregations.Gets(x => x.IndicatorId == id).OrderByDescending(x => x.OrderType);

            return Json(onlyId, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// GetIdBy
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult Getall(Guid id)
        {
            var onlyId = _categoryDisaggregations.Gets();

            return Json(onlyId, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDisaggregations()
        {
            var dataTypes = _categoryDisaggregations.GetsSelects();
            return Json(new { data = dataTypes }, JsonRequestBehavior.AllowGet);
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
                    object cateDisaggId = null;
                    var cateDisagg = new CategoryDisaggreationModel();
                    do
                    {
                        cateDisaggId = wordSheet.Cells[startRow + i, startColumn].Value;
                        object cateDisaggName_ = wordSheet.Cells[startRow + i, startColumn + 1].Value;
                        object cateDisaggCode_ = wordSheet.Cells[startRow + i, startColumn + 2].Value;
                        if (cateDisaggId != null && cateDisaggName_ != null && cateDisaggCode_ != null)
                        {
                            // read data
                            var ConvertNameToID = cateDisaggId.ToString(); // chi tieu phan to cha
                            var IndicatorId_ = _indicatorSevice.Gets(d => d.IndicatorName == ConvertNameToID).FirstOrDefault();
                            cateDisagg.IndicatorId = IndicatorId_.IndicatorId;
                            cateDisagg.CategoryDisaggregationName = cateDisaggName_.ToString();
                            cateDisagg.IsActivated = true;
                            cateDisagg.CategoryDisaggregationCode = cateDisaggCode_.ToString();
                            var catalog = cateDisagg.ToEntity();
                            _categoryDisaggregations.Create(catalog);
                            i += 1;
                        }
                    }
                    while (cateDisaggId != null);

                    TempData["Success"] = "SS";
                }
                else
                {
                    TempData["Error"] = "ER";
                }
            }

            return RedirectToAction("Index", "CategoryDisaggregations");
        }
    }
}