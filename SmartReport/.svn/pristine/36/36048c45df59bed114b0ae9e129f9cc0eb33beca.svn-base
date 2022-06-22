using Bkav.eGovCloud.Admin.Models;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Web.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class SurveyCatalogController : CustomController
    {
        private readonly SurveyCatalogBll _surveyCatalogService;
        private readonly ResourceBll _resourceService;
        private readonly SurveyCatalogValueBll _surveyCatalogValueService;
        private readonly AdminGeneralSettings _generalSettings;
        private const string DEFAULT_SORT_BY = "CatalogName";

        public SurveyCatalogController(
            SurveyCatalogBll surveyCatalogBll,
            ResourceBll resourceBll,
            SurveyCatalogValueBll surveyCatalogValueBll,
            AdminGeneralSettings generalSettings)
            : base()
        {
            _surveyCatalogService = surveyCatalogBll;
            _resourceService = resourceBll;
            _surveyCatalogValueService = surveyCatalogValueBll;
            _generalSettings = generalSettings;
        }
        [HttpPost]
        public JsonResult GetAllData()
        {
            try
            {
                var data = _surveyCatalogService.Gets(null);
                return Json(new { success = true, data }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetSurveyCatalogValues(Guid catalogId)
        {
            var surveyCatalogValues = _surveyCatalogValueService.Gets(catalogId);
            return Json(surveyCatalogValues, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Index()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                ErrorNotification(_resourceService.GetResource("Customer.SurveyCatalog.NotPermission"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.SurveyCatalog.NotPermission"));
                return RedirectToAction("Index");
            }
            ModelState.Clear();
            var model = new SurveyCatalogModel { SurveyCatalogs = _surveyCatalogService.GetsAs(c => new SurveyCatalogModel { CatalogId = c.CatalogId, CatalogName = c.CatalogName, CatalogKey = c.CatalogKey }) };
            return View(model);
        }

        private void GetCatalog()
        {
            var category = _surveyCatalogService.GetsAs(c => new SurveyCatalogModel { CatalogId = c.CatalogId, CatalogName = c.CatalogName });
            var tmp = category.Select(c => new SelectListItem {Value = c.CatalogId.ToString(), Text = c.CatalogName })
                .ToList();
            tmp.Insert(0, new SelectListItem() {Value = "", Text = ""});
            ViewBag.Category = tmp;
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Index(SurveyCatalogModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                if (model.CatalogId == default(Guid))
                {
                    ErrorNotification(_resourceService.GetResource("Customer.SurveyCatalog.NotPermissionCreate"));
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.SurveyCatalog.NotPermissionCreate"));
                }
                else
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.SurveyCatalog.NotPermissionUpdate"));
                    ErrorNotification(_resourceService.GetResource("Customer.SurveyCatalog.NotPermissionUpdate"));
                }

                return RedirectToAction("Index");
            }

            model.SurveyCatalogs = _surveyCatalogService.GetsAs(c => new SurveyCatalogModel
            { CatalogId = c.CatalogId, CatalogName = c.CatalogName, CatalogKey = c.CatalogKey});
            if (!ModelState.IsValid) return View(model);
            try
            {
                //create
                if (model.CatalogId == default(Guid))
                {
                    var catalog = model.ToEntity();
                    _surveyCatalogService.Create(catalog);
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.SurveyCatalog.Created"));
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.SurveyCatalog.Created"));
                }
                else
                {
                    var catalog = _surveyCatalogService.Get(model.CatalogId);
                    if (catalog == null)
                        return RedirectToAction("Index");
                    catalog = model.ToEntity(catalog);
                    _surveyCatalogService.Update(catalog);
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.SurveyCatalog.Updated"));
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.SurveyCatalog.Updated"));

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
        [HttpPost]
        public ActionResult Delete(Guid catalogId)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.SurveyCatalog.NotPermissionDelete"));
                ErrorNotification(_resourceService.GetResource("Customer.SurveyCatalog.NotPermissionDelete"));
                return RedirectToAction("Index");
            }
            var catalog = _surveyCatalogService.Get(catalogId);
            if (catalog == null) return RedirectToAction("Index");
            try
            {
                _surveyCatalogService.Delete(catalog);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.SurveyCatalog.Deleted"));
                SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.SurveyCatalog.Deleted"));
            }
            catch (EgovException ex)
            {
                LogException(ex);
                CreateActivityLog(ActivityLogType.Admin, ex.Message);
                ErrorNotification(ex.Message);
            }
            return RedirectToAction("Index");
        }
        
       
        public JsonResult GetCatalogByCategoryChild(Guid parentId)
        {
            try
            {
                var arr = _surveyCatalogValueService.GetsChildByParent(parentId);
                var surveyCatalogs = arr as List<SurveyCatalogValue> ?? arr.ToList();
                return Json(new { success = true, data = surveyCatalogs }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}