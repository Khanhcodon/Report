using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Models;
using Bkav.eGovCloud.Web.Framework;
using System.Linq;
using System.Web.Mvc;

namespace Bkav.eGovCloud.Controllers
{
    //[RequireHttps]
    [EgovAuthorize]
    public class DocumentDraftController : CustomerBaseController
    {
        private readonly DocTypeBll _docTypeService;

        public DocumentDraftController(DocTypeBll docTypeService)
        {
            _docTypeService = docTypeService;
        }

        public ActionResult Index()
        {
            ViewBag.ListDocTypes = _docTypeService.Gets().OrderBy(dt => dt.DocTypeName);
            var model = new DocumentDraftModel();
            return View(model);
        }
    }
}