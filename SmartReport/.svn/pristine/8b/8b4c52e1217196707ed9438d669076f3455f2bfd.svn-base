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
using CrystalDecisions.Shared.Json;
using DocumentFormat.OpenXml.Office2013.PowerPoint;
using DocumentFormat.OpenXml.Office2013.PowerPoint.Roaming;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    public class LocalityDepartmentController : CustomController
    {
        private readonly AdminGeneralSettings _generalSettings;
        private readonly LocalityBll _localityService;
        private readonly Ad_LocalityBll _ad_LocalityService;
        private readonly ResourceBll _resourceService;
        private readonly DepartmentBll _departmentService;
        private readonly InCatalogValueBll _inCatalogValueService;
        private readonly LocalityDepartmentBll _localityDepartmentService;

        private const string DEFAULT_SORT_BY = "CategoryName";

        public LocalityDepartmentController(LocalityBll localityService,
            ResourceBll resourceService,
            AdminGeneralSettings generalSettings,
            DepartmentBll departmentService,
            InCatalogValueBll inCatalogValueService,
            LocalityDepartmentBll localityDepartmentService,
            Ad_LocalityBll ad_LocalitySerive)
            : base()
        {
            _localityService = localityService;
            _resourceService = resourceService;
            _generalSettings = generalSettings;
            _departmentService = departmentService;
            _inCatalogValueService = inCatalogValueService;
            _localityDepartmentService = localityDepartmentService;
            _ad_LocalityService = ad_LocalitySerive;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetAllDepartment()
        {
            var departments = _departmentService.Gets().Select(s => {
                return new
                {
                    id = s.DepartmentId.ToString(),
                    text = s.DepartmentName,
                    parent = s.ParentId.HasValue ? s.ParentId.Value.ToString() : "#",
                    level = s.Level,
                    departmentIdExt = s.DepartmentIdExt.ToString()
                };
            });
            return Json(departments, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLocality(int departmentId)
        {
            var localityDepartment = _localityDepartmentService.GetDepartmentIds(s => s.DepartmentId == departmentId);
            var localityIds = localityDepartment.Select(s => s.LocalityId);
            var locality = _ad_LocalityService.Gets("", false).Select(s => {
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
                    localityDepartCheck = checkedValue,
                };
            });
            return Json(locality, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult saveLocality(int departmentId, string dataIds)
        {
            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dataIds);
            var localityDepartOld = _localityDepartmentService.GetDepartmentIds(s => s.DepartmentId == departmentId);
            var localityIds = localityDepartOld.Select(i => i.LocalityId.ToString());

            var localityRemoves = new List<string>();
            if (localityIds != null && localityIds.Any())
            {
                localityRemoves = localityIds.Except(data).ToList();
            }
            var localityAdds = new List<string>();
            if (data != null && data.Any())
            {
                localityAdds = data.Except(localityIds).ToList();
            }

            if (localityRemoves != null && localityRemoves.Any())
            {
                var inRemoves = localityRemoves.Select(i => Guid.Parse(i));
                var idDepartRemoves = _localityDepartmentService.Gets(false, id => inRemoves.Contains(id.LocalityId) && id.DepartmentId == departmentId);
                _localityDepartmentService.Delete(idDepartRemoves);
            }

            if (localityAdds != null && localityAdds.Any())
            {
                var listLocalityDepart = new List<LocalityDepartment>();
                foreach (var localityAdd in localityAdds)
                {
                    var localityDepart = new LocalityDepartment();
                    localityDepart.LocalityDepartmentId = Guid.NewGuid();
                    localityDepart.LocalityId = Guid.Parse(localityAdd);
                    localityDepart.DepartmentId = departmentId;
                    listLocalityDepart.Add(localityDepart);
                }

                _localityDepartmentService.Create(listLocalityDepart);
            }


            return Json(new { error = false });

        }
    }
}
