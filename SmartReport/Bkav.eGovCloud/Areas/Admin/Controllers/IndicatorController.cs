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
    public class IndicatorController : CustomController
    {
        private readonly AdminGeneralSettings _generalSettings;
        private readonly IndicatorCatalogBll _indicatorCatalogService;
        private readonly ResourceBll _resourceService;
        private readonly CodeBll _codeService;
        private readonly DocTypeBll _docTypeService;
        private readonly CategoryBll _categoryService;
        private readonly InCatalogBll _inCatalogService;
        private readonly InCatalogValueBll _inCatalogValueService;
        private readonly IndicatorBll _indicatorSevice;

        public IndicatorController(IndicatorCatalogBll indicatorCatalogService,
            ResourceBll resourceService,
            AdminGeneralSettings generalSettings,
            CategoryBll categoryService,
            InCatalogBll inCatalogService, 
            InCatalogValueBll inCatalogValueService,
            IndicatorBll indicatorSevice)
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
            var model = new IndicatorModel { Indicators = _indicatorSevice.GetsAs(c =>
            new IndicatorModel { IndicatorId = c.IndicatorId, IndicatorName = c.IndicatorName,
                IndicatorDesctiption = c.IndicatorDesctiption, IsActivated = c.IsActivated }) };
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Index(IndicatorModel model)
        {
            model.Indicators = _indicatorSevice.GetsAs(c =>
            new IndicatorModel
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
            foreach(var id in model)
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

    }
}