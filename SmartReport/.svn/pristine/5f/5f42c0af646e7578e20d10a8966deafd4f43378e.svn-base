using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Business.Customer.Ad_Report;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer.Ad_Report;
using Bkav.eGovCloud.Web.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]

    public class TargetsController : CustomController
    {
        private readonly ResourceBll _resourceService;
        private readonly InCatalogValueBll _inCatalogValueService;
        private readonly Ad_TagetsBll _TagetsService;

        private const string DEFAULT_SORT_BY = "CategoryName";
        public TargetsController(ResourceBll resourceService, InCatalogValueBll inCatalogValueService, Ad_TagetsBll TagetsService) : base()
        {
            _resourceService = resourceService;
            _inCatalogValueService = inCatalogValueService;
            _TagetsService = TagetsService;
        }
        [HttpGet]
        public ActionResult Index()
        {
           
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Ad_TargetsModel model)
        {
            model.Ad_TargetsModels = _TagetsService.GetsAs(c =>
            new Ad_TargetsModel
            {
                Id = c.Id,
                ValueId = c.ValueId,
                DepartmentId = c.DepartmentId,
                Upper = c.Upper,
                Lower = c.Lower
               
            });

            if (!ModelState.IsValid) return View(model);
            try
            {
                //create
                if (model.Id == default(int))
                {
                    var catalog = model.ToEntity();
                    _TagetsService.Create(catalog);
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.IndicatorCatalog.Created"));
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.IndicatorCatalog.Created"));
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
        public JsonResult GetCatalog()
        {
            var indicatorValues = _inCatalogValueService.Gets().Select(d => new {
                id = d.InCatalogValueId.ToString(),
                name = d.InCatalogValueName,
                parent = d.ParentId.HasValue ? d.ParentId.Value.ToString() : "#",
                order = d.Order,
                level = d.Level,
                catalog = d.InCatalogId.ToString(),
                text = d.InCatalogValueCode + " " + d.InCatalogValueName,
                code = d.InCatalogValueCode,
                //unit = d.UnitName
            });
            return Json(indicatorValues, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetIndicator(int departmentId)
        {
            var indicatorDepart = _TagetsService.Gets(d => d.DepartmentId == departmentId);
            var indicatorDepartIds = indicatorDepart.Select(i => i.ValueId);
            if (true)
            {

            }
            var indicatorValues = _inCatalogValueService.Gets().Select(d => {
                var checkedValue = indicatorDepartIds.Contains(d.InCatalogValueId);
                return new
                {
                    id = d.InCatalogValueId.ToString(),
                    name = d.InCatalogValueName,
                    parent = d.ParentId.HasValue ? d.ParentId.Value.ToString() : "#",
                    order = d.Order,
                    level = d.Level,
                    state = new
                    {
                        selected = checkedValue
                    },
                    catalog = d.InCatalogId.ToString(),
                    text = d.InCatalogValueCode + " " + d.InCatalogValueName,
                    code = d.InCatalogValueName,
                    indicatorDepartCheck = checkedValue
                };
            });
            return Json(indicatorValues, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetIndicatorsss()
        {
            var indicatorTagets = _TagetsService.Gets(true);
            var indicatorDepartIds = indicatorTagets.Select(i => i.ValueId);
            var indicatorValues = _inCatalogValueService.Gets().Select(d => {
                return new
                {
                    id = d.InCatalogValueId.ToString(),
                    name = d.InCatalogValueName,
                    parent = d.ParentId.HasValue ? d.ParentId.Value.ToString() : "#",
                    order = d.Order,
                    level = d.Level,
                    catalog = d.InCatalogId.ToString(),
                    text = d.InCatalogValueCode + " " + d.InCatalogValueName,
                    code = d.InCatalogValueCode,
                };
            });
            return Json(indicatorValues, JsonRequestBehavior.AllowGet);
        }


        public JsonResult SaveIndicator(string dataIds, int departmentId)
        {
            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dataIds);
            var indicatorDepartOld = _TagetsService.Gets(i => i.DepartmentId == departmentId);
            var idIndicatorDepts = indicatorDepartOld.Select(i => i.ValueId.ToString());

            var indicatorRemoves = new List<string>();
            if (idIndicatorDepts != null && idIndicatorDepts.Any())
            {
                indicatorRemoves = idIndicatorDepts.Except(data).ToList();
            }
            var indicatorAdds = new List<string>();
            if (data != null && data.Any())
            {
                indicatorAdds = data.Except(idIndicatorDepts).ToList();
            }

            if (indicatorRemoves != null && indicatorRemoves.Any())
            {
                var inRemoves = indicatorRemoves.Select(i => Guid.Parse(i));
                var idDepartRemoves = _TagetsService.Gets(false, id => inRemoves.Contains(id.ValueId) && id.DepartmentId == departmentId);
                _TagetsService.Detele(idDepartRemoves);
            }

            if (indicatorAdds != null && indicatorAdds.Any())
            {
                var listIndicator = new List<Ad_targets>();
                foreach (var indicatorAdd in indicatorAdds)
                {
                    var indicatorDepart = new Ad_targets();
 
                    indicatorDepart.ValueId = Guid.Parse(indicatorAdd);
                    indicatorDepart.DepartmentId = departmentId;
                    listIndicator.Add(indicatorDepart);
                }

                _TagetsService.Create(listIndicator);
            }

            return Json(new { error = false });
        }
    }
}