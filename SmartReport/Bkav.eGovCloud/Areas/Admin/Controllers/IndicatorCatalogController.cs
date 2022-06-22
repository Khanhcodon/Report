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

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class IndicatorCatalogController : CustomController
    {
        private readonly AdminGeneralSettings _generalSettings;
        private readonly IndicatorCatalogBll _indicatorCatalogService;
        private readonly ResourceBll _resourceService;
        private readonly CodeBll _codeService;
        private readonly DocTypeBll _docTypeService;
        private readonly CategoryBll _categoryService;
        private readonly InCatalogBll _inCatalogService;
        private readonly InCatalogValueBll _inCatalogValueService;

        private const string DEFAULT_SORT_BY = "CategoryName";

        public IndicatorCatalogController(IndicatorCatalogBll indicatorCatalogService,
            ResourceBll resourceService,
            AdminGeneralSettings generalSettings,
            CategoryBll categoryService,
            InCatalogBll inCatalogService, InCatalogValueBll inCatalogValueService)
            : base()
        {
            _indicatorCatalogService = indicatorCatalogService;
            _resourceService = resourceService;
            _generalSettings = generalSettings;
            _categoryService = categoryService;
            _inCatalogService = inCatalogService;
            _inCatalogValueService = inCatalogValueService;
        }
        [HttpGet]
        public ActionResult Index()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                ErrorNotification(_resourceService.GetResource("Customer.IndicatorCatalog.NotPermission"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.IndicatorCatalog.NotPermission"));
                return RedirectToAction("Index");
            }
            ModelState.Clear();
            var model = new InCatalogModel { InCatalogs = _inCatalogService.GetsAs(c => new InCatalogModel { InCatalogId = c.InCatalogId, InCatalogName = c.InCatalogName , InCatalogKey = c.InCatalogKey}) };
            return View(model);
        }

        private void GetCatalog()
        {
            var category = _inCatalogService.GetsAs(c => new InCatalogModel { InCatalogId = c.InCatalogId, InCatalogName = c.InCatalogName });
            var tmp = category.Select(c => new SelectListItem {Value = c.InCatalogId.ToString(), Text = c.InCatalogName })
                .ToList();
            tmp.Insert(0, new SelectListItem() {Value = "", Text = ""});
            ViewBag.Category = tmp;
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Index(InCatalogModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                if (model.InCatalogId == default(Guid))
                {
                    ErrorNotification(_resourceService.GetResource("Customer.IndicatorCatalog.NotPermissionCreate"));
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.IndicatorCatalog.NotPermissionCreate"));
                }
                else
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.IndicatorCatalog.NotPermissionUpdate"));
                    ErrorNotification(_resourceService.GetResource("Customer.IndicatorCatalog.NotPermissionUpdate"));
                }

                return RedirectToAction("Index");
            }

            model.InCatalogs = _inCatalogService.GetsAs(c => new InCatalogModel
                {InCatalogId = c.InCatalogId, InCatalogName = c.InCatalogName, InCatalogKey = c.InCatalogKey});
            if (!ModelState.IsValid) return View(model);
            try
            {
                //create
                if (model.InCatalogId == default(Guid))
                {
                    var catalog = model.ToEntity();
                    _inCatalogService.Create(catalog);
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.IndicatorCatalog.Created"));
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.IndicatorCatalog.Created"));
                }
                else
                {
                    var catalog = _inCatalogService.Get(model.InCatalogId);
                    if (catalog == null)
                        return RedirectToAction("Index");
                    catalog = model.ToEntity(catalog);
                    _inCatalogService.Update(catalog);
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
        [HttpPost]
        public ActionResult Delete(Guid catalogId)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.IndicatorCatalog.NotPermissionDelete"));
                ErrorNotification(_resourceService.GetResource("Customer.IndicatorCatalog.NotPermissionDelete"));
                return RedirectToAction("Index");
            }
            var catalog = _inCatalogService.Get(catalogId);
            if (catalog == null) return RedirectToAction("Index");
            try
            {
                _inCatalogService.Delete(catalog);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.IndicatorCatalog.Deleted"));
                SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.IndicatorCatalog.Deleted"));
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
                var arr = _inCatalogValueService.GetsChildByParent(parentId);
                var indicatorCatalogs = arr as List<InCatalogValue> ?? arr.ToList();
                return Json(new { success = true, data = indicatorCatalogs }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
