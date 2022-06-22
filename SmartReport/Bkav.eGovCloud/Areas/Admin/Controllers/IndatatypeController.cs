using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Business.Customer.Ad_Report;
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

    public class IndatatypeController : CustomController
    {

        private readonly dataTypeBll _dataTypeService;
        private readonly IndatatypeBll _indatatypeService;

        private const string DEFAULT_SORT_BY = "CategoryName";
        public IndatatypeController(dataTypeBll dataTypeService, IndatatypeBll indatatypeService) : base()
        {
            _dataTypeService = dataTypeService;
            _indatatypeService = indatatypeService;
        }
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetCatalog()
        {
            var dataType = _dataTypeService.GetsSelects().Select(d => new {
                id = d.dataTypeId.ToString(),
                name = d.dataTypeName,
                di = d.distribute,
                text = d.nameID + " " + d.dataTypeName,
                code = d.dataTypeName,
                des= d.dataTypeDescription,
                ac = d.IsActivated
            });
            return Json(dataType, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetIndicator(int departmentId)
        {
            var indicatorDepart = _indatatypeService.Gets(d => d.DepartmentId == departmentId);
            var indicatorDepartIds = indicatorDepart.Select(i => i.ValueId);
            if (true)
            {

            }
            var dataType = _dataTypeService.GetsSelects().Select(d =>
            {
                var checkedValue = indicatorDepartIds.Contains(d.dataTypeId);
                return new
                {
                    id = d.dataTypeId.ToString(),
                    name = d.dataTypeName,
                    di = d.distribute,
                    state = new
                    {
                        selected = checkedValue
                    },
                    text = d.nameID + " " + d.dataTypeName,
                    code = d.dataTypeName,
                    des = d.dataTypeDescription,
                    ac = d.IsActivated,

                    indicatorDepartCheck = checkedValue
                };
            });
            return Json(dataType, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveIndicator(string dataIds, int departmentId)
        {
            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dataIds);
            var indicatorDepartOld = _indatatypeService.Gets(i => i.DepartmentId == departmentId);
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
                var idDepartRemoves = _indatatypeService.Gets(false, id => inRemoves.Contains(id.ValueId) && id.DepartmentId == departmentId);
                _indatatypeService.Detele(idDepartRemoves);
            }

            if (indicatorAdds != null && indicatorAdds.Any())
            {
                var listIndicator = new List<Dim_indicatordatatype>();
                foreach (var indicatorAdd in indicatorAdds)
                {
                    var indicatorDepart = new Dim_indicatordatatype();
                    indicatorDepart.Id = Guid.NewGuid();
                    indicatorDepart.ValueId = Guid.Parse(indicatorAdd);
                    indicatorDepart.DepartmentId = departmentId;
                    listIndicator.Add(indicatorDepart);
                }

                _indatatypeService.Create(listIndicator);
            }

            return Json(new { error = false });
        }
    }
}