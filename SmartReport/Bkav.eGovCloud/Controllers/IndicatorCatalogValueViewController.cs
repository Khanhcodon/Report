using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Web;
using System.Web.Mvc;
using Bkav.eGovCloud.Admin.Models;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Helper;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class IndicatorCatalogValueViewController : CustomerBaseController
    {
        private readonly AdminGeneralSettings _generalSettings;
        private readonly IndicatorCatalogBll _indicatorCatalogService;
        private readonly ResourceBll _resourceService;
        private readonly CategoryBll _categoryService;
        private readonly InCatalogBll _inCatalogService;
        private readonly InCatalogValueBll _inCatalogValueService;
        private readonly IndicatorValueDepartmentBll _indicatorDepartmentService;
        private readonly DepartmentBll _departmentService;
        private readonly LocalityDepartmentBll _localityDepartmentService;
        private readonly Ad_LocalityBll _ad_LocalityService;

        private const string DEFAULT_SORT_BY = "CategoryName";

        public IndicatorCatalogValueViewController(IndicatorCatalogBll indicatorCatalogService,
            ResourceBll resourceService,
            AdminGeneralSettings generalSettings, Ad_LocalityBll ad_LocalityService,
            CategoryBll categoryService, LocalityDepartmentBll localityDepartmentService,
            InCatalogBll inCatalogService, InCatalogValueBll inCatalogValueService, IndicatorValueDepartmentBll indicatorDepartmentService, DepartmentBll departmentService)
            : base()
        {
            _indicatorCatalogService = indicatorCatalogService;
            _resourceService = resourceService;
            _generalSettings = generalSettings;
            _categoryService = categoryService;
            _inCatalogService = inCatalogService;
            _inCatalogValueService = inCatalogValueService;
            _indicatorDepartmentService = indicatorDepartmentService;
            _departmentService = departmentService;
            _localityDepartmentService = localityDepartmentService;
            _ad_LocalityService = ad_LocalityService;
        }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LocalityIndicator()
        {
            var department = _departmentService.GetCacheAllUserDepartmentJobTitlesPosition().Where(p => p.UserId == User.GetUserId()).OrderBy(p => p.IsPrimary).FirstOrDefault();
            var localityDepartment = _localityDepartmentService.GetDepartmentIds(s => s.DepartmentId == department.DepartmentId);
            var localityIds = localityDepartment.Select(s => s.LocalityId);
            var locality = _ad_LocalityService.GetsAll(s=> localityIds.Contains(s.LocalityId));
            var model = locality;
            return View(locality);
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
                code = d.InCatalogValueName
            });
            return Json(indicatorValues, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLocality()
        {
            var department = _departmentService.GetCacheAllUserDepartmentJobTitlesPosition().Where(p => p.UserId == User.GetUserId()).OrderBy(p => p.IsPrimary).FirstOrDefault();

            var localityDepartment = _localityDepartmentService.GetDepartmentIds(s => s.DepartmentId == department.DepartmentId);
            var localityIds = localityDepartment.Select(s => s.LocalityId);
            var locality = _ad_LocalityService.GetsAll(s => localityIds.Contains(s.LocalityId)).Select(s => {
                var checkedValue = localityIds.Contains(s.LocalityId);
                return new
                {
                    id = s.LocalityId.ToString(),
                    text = s.LocalityName,
                    parent = s.ParentId.HasValue ? s.ParentId.ToString() : "#",
                    state = new
                    {
                        selected = checkedValue
                    },
                    code = s.Id,
                    localityDepartCheck = checkedValue
                };
            });
            return Json(locality, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetIndicator(int departmentId)
        {
            var department = _departmentService.GetCacheAllUserDepartmentJobTitlesPosition().Where(p => p.UserId == User.GetUserId()).OrderBy(p => p.IsPrimary).FirstOrDefault();
            var indicatorDepart = _indicatorDepartmentService.Gets(d => d.DepartmentId == department.DepartmentId);
            var indicatorDepartIds = indicatorDepart.Select(i => i.IndicatorValueId);
            
            var indicatorValues = _inCatalogValueService.Gets().Select(d => {
                var checkedValue = indicatorDepartIds.Contains(d.InCatalogValueId);
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
                    indicatorDepartCheck = checkedValue, 
                    unit = d.UnitName
                };
            });
            return Json(indicatorValues, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveIndicator(string dataIds, int departmentId)
        {
            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dataIds);
            var indicatorDepartOld = _indicatorDepartmentService.Gets(i => i.DepartmentId == departmentId);
            var idIndicatorDepts = indicatorDepartOld.Select(i => i.IndicatorValueId.ToString());

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
                var idDepartRemoves = _indicatorDepartmentService.Gets(false, id => inRemoves.Contains(id.IndicatorValueId) && id.DepartmentId == departmentId);
                _indicatorDepartmentService.Detele(idDepartRemoves);
            }

            if (indicatorAdds != null && indicatorAdds.Any())
            {
                var listIndicator = new List<IndicatorValueDepartment>();
                foreach (var indicatorAdd in indicatorAdds)
                {
                    var indicatorDepart = new IndicatorValueDepartment();
                    indicatorDepart.IndicatorValueId = Guid.Parse(indicatorAdd);
                    indicatorDepart.DepartmentId = departmentId;
                    listIndicator.Add(indicatorDepart);
                }

                _indicatorDepartmentService.Create(listIndicator);
            }

            return Json(new { error = false });
        }
    }
}
