using System;
using System.Collections.Generic;
using System.Linq;
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
using Bkav.eGovCloud.Web.Framework;
using OfficeOpenXml;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    public class Ad_LocalityController : CustomController
    {
        private readonly AdminGeneralSettings _generalSettings;
        private readonly IndicatorCatalogBll _indicatorCatalogService;
        private readonly ResourceBll _resourceService;
        private readonly CategoryBll _categoryService;
        private readonly InCatalogBll _inCatalogService;
        private readonly InCatalogValueBll _inCatalogValueService;
        private readonly Ad_LocalityBll _ad_localitySevice;

        public Ad_LocalityController(IndicatorCatalogBll indicatorCatalogService,
            ResourceBll resourceService,
            AdminGeneralSettings generalSettings,
            CategoryBll categoryService,
            InCatalogBll inCatalogService,
            InCatalogValueBll inCatalogValueService,
            Ad_LocalityBll ad_localitySevice)
            : base()
        {
            _indicatorCatalogService = indicatorCatalogService;
            _resourceService = resourceService;
            _generalSettings = generalSettings;
            _categoryService = categoryService;
            _inCatalogService = inCatalogService;
            _inCatalogValueService = inCatalogValueService;
            _ad_localitySevice = ad_localitySevice;
        }

        [HttpGet]
        public ActionResult Index()
        {
            ModelState.Clear();
            var model = new Ad_LocalityModel
            {
                Ad_Localitys = _ad_localitySevice.GetsAs(c =>
                new Ad_LocalityModel
                {
                LocalityId = c.LocalityId,
                LocalityName = c.LocalityName,
                Id = c.Id ,
                ParentId = c.ParentId,
                Type = c.Type,
                Description = c.Description,
                IsActive = c.IsActive
                })
            };

            foreach(var item in model.Ad_Localitys)
            {
                var parentIdOnly = item.ParentId;
                if (parentIdOnly != null)
                {
                    var listOnlyAdLocality = _ad_localitySevice.GetParent(parentIdOnly);
                    item.ParentName = listOnlyAdLocality.LocalityName;
                }
            }
                     
            var listParents = _ad_localitySevice.GetsAll(c => c.IsActive == true);

            var listOption = new List<SelectListItem>();
            listOption.Add(new SelectListItem()
            {
                Selected = true,
                Value = "",
                Text = "Lựa chọn địa bàn cha"
            });
            foreach (var item in listParents)
            {
                listOption.Add(new SelectListItem()
                {
                    Selected = false,
                    Value = item.LocalityId.ToString(),
                    Text = item.LocalityName
                });
            }
             
            ViewBag.ListOption = listOption;

            return View(model);
        }

        public JsonResult GetLocalities()
        {
            var localities = _ad_localitySevice.GetsAll();
            return Json(new { data = localities }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Ad_LocalityModel model)
        {
            model.Ad_Localitys = _ad_localitySevice.GetsAs(c =>
            new Ad_LocalityModel
            {
                LocalityId = c.LocalityId,
                LocalityName = c.LocalityName,
                Id = c.Id,
                ParentId = c.ParentId,
                Type = c.Type,
                Description = c.Description,
                IsActive = c.IsActive
            });

            if (!ModelState.IsValid) return View(model);
            try
            {
                //create
                if (model.LocalityId == default(Guid))
                {
                    var catalog = model.ToEntity(); 
                    _ad_localitySevice.Create(catalog);
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.IndicatorCatalog.Created"));
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.IndicatorCatalog.Created"));
                }
                else
                {
                    var catalog = _ad_localitySevice.Get(model.LocalityId);
                    if (catalog == null)
                        return RedirectToAction("Index");
                    catalog = model.ToEntity(catalog);
                    _ad_localitySevice.Update(catalog);
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
            var locality = _ad_localitySevice.Get(id);
            if (locality == null)
            {
                return null;
            }
            var model = locality.ToModel();

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            var locality = _ad_localitySevice.Get(id);
            if (locality == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ActionLevel.NotExist"));
                ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ActionLevel.NotExist"));
                return RedirectToAction("Index");
            }

            try
            {
                _ad_localitySevice.Delete(locality);
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
                var locality = _ad_localitySevice.Get(id);
                if (locality == null)
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ActionLevel.NotExist"));
                    ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.ActionLevel.NotExist"));
                    return RedirectToAction("Index");
                }

                try
                {
                    _ad_localitySevice.Delete(locality);
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
                    var ad_local = new Ad_LocalityModel();
                    do
                    {
                        data = wordSheet.Cells[startRow + i, startColumn].Value;
                        object ClassName = wordSheet.Cells[startRow + i, startColumn + 1].Value;
                        object Description_ = wordSheet.Cells[startRow + i, startColumn + 2].Value;
                        object Parent = wordSheet.Cells[startRow + i, startColumn + 3].Value;
                        object typeLocality = wordSheet.Cells[startRow + i, startColumn + 4].Value;
                        if (data != null && ClassName != null)
                        {
                            // read data
                            ad_local.LocalityName = data.ToString();
                            ad_local.Id = ClassName.ToString();
                            ad_local.Description = Description_.ToString();          
                            if(Parent != null)
                            {
                                var parentName = Parent.ToString();
                                var parentId = _ad_localitySevice.Gets(d => d.LocalityName == parentName).FirstOrDefault() ;
                                ad_local.ParentId = parentId.LocalityId;
                            }
                            ad_local.IsActive = true;
                            if(typeLocality.ToString() == "Xã/Phường/Thị trấn")
                            {
                                ad_local.Type = 3;
                            }else if(typeLocality.ToString() == "Quận/Huyện/Thị xã")
                            {
                                ad_local.Type = 2;
                            }else if(typeLocality.ToString() == "Tỉnh/Thành phố")
                            {
                                ad_local.Type = 1;
                            }
                            var modelAd_local = ad_local.ToEntity();
                            _ad_localitySevice.Create(modelAd_local);
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

            return RedirectToAction("Index", "Ad_Locality");
        }

    }
}