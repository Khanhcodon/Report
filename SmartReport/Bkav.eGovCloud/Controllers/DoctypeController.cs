using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Web.Framework;
using Newtonsoft.Json;

namespace Bkav.eGovCloud.Controllers
{
    public class DoctypeController : CustomerBaseController
    {
        private readonly DocTypeBll _doctypeService;
        private readonly PaperBll _paperService;
        private readonly FeeBll _feeService;
        private readonly UserBll _userService;

        public DoctypeController(DocTypeBll doctypeService, PaperBll paperService, FeeBll feeService, UserBll userService)
        {
            _doctypeService = doctypeService;
            _paperService = paperService;
            _feeService = feeService;
            _userService = userService;
        }

        public JsonResult GetDocType(Guid id)
        {
            var doctype = _doctypeService.GetFromCache(id);

            var result = doctype == null ? null : new
                {
                    CategoryBusinessId = doctype.CategoryBusinessId,
                    DocTypeId = doctype.DocTypeId,
                    DocTypeName = doctype.DocTypeName
                };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// </summary>
        /// <returns> </returns>
        public JsonResult GetDocTypes()
        {
            var userId = _userService.CurrentUser.UserId;
            var doctypes = _doctypeService.GetsByUserId(userId);
            return Json(doctypes.Select(dt => new
            {
                dt.DocTypeId,
                dt.DocTypeName,
                dt.DocFieldName,
                dt.DocFieldId,
                dt.CategoryBusinessId
            }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPaperAndFees(Guid doctypeId, int type)
        {
            var papers = _paperService.Gets(doctypeId, (PaperType)type);
            var fees = _feeService.Gets(doctypeId, (FeeType)type);

            return Json(new { papers = papers, fees = fees }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdatePaperAndFees(Guid doctypeId, int type, string papers, string fees)
        {
            var doctypePapers = JsonConvert.DeserializeObject<IEnumerable<Paper>>(papers);
            var doctypeFees = JsonConvert.DeserializeObject<IEnumerable<Fee>>(fees);

            _doctypeService.UpdatePapers(doctypePapers, doctypeId, (PaperType)type);
            _doctypeService.UpdateFees(doctypeFees, doctypeId, (FeeType)type);

            return Json(new { papers = papers, fees = fees }, JsonRequestBehavior.AllowGet);
        }
    }
}
