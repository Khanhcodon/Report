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

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class IndicatorDepartmentController : CustomController
    {
        private readonly AdminGeneralSettings _generalSettings;
        private readonly IndicatorCatalogBll _indicatorCatalogService;
        private readonly ResourceBll _resourceService;
        //private readonly CodeBll _codeService;
        //private readonly DocTypeBll _docTypeService;
        private readonly CategoryBll _categoryService;
        private readonly InCatalogBll _inCatalogService;
        private readonly InCatalogValueBll _inCatalogValueService;
        private readonly IndicatorValueDepartmentBll _indicatorDepartmentService;
        private readonly UnitBll _unitService;

        private const string DEFAULT_SORT_BY = "CategoryName";

        public IndicatorDepartmentController(IndicatorCatalogBll indicatorCatalogService,
            ResourceBll resourceService,
            AdminGeneralSettings generalSettings,
            CategoryBll categoryService,
            InCatalogBll inCatalogService, 
            InCatalogValueBll inCatalogValueService, 
            IndicatorValueDepartmentBll indicatorDepartmentService,
            UnitBll unitService
            )
            : base()
        {
            _indicatorCatalogService = indicatorCatalogService;
            _resourceService = resourceService;
            _generalSettings = generalSettings;
            _categoryService = categoryService;
            _inCatalogService = inCatalogService;
            _inCatalogValueService = inCatalogValueService;
            _indicatorDepartmentService = indicatorDepartmentService;
            _unitService = unitService;
        }

        public ActionResult Index()
        {
            return View();
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
                unit = d.UnitName
            });
            return Json(indicatorValues, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetIndicator(int departmentId)
        {
            var indicatorDepart = _indicatorDepartmentService.Gets(d=>d.DepartmentId == departmentId);
            var indicatorDepartIds = indicatorDepart.Select(i => i.IndicatorValueId);
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
                    state =  new {
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

        public JsonResult SaveIndicator(string dataIds, int departmentId)
        {
            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dataIds);
            var indicatorDepartOld = _indicatorDepartmentService.Gets( i=>i.DepartmentId == departmentId);
            var idIndicatorDepts = indicatorDepartOld.Select(i=> i.IndicatorValueId.ToString());
            
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
