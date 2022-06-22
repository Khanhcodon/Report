using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Controllers
{
    public class TestController : Controller
    {
        private readonly DocTypeBll _doctypeService;
        private readonly DocFieldBll _docfieldService;
        private readonly DocumentCopyBll _documentCopyService;

        public TestController(DocTypeBll doctypeService, DocumentCopyBll documentCopyService, DocFieldBll docfieldService)
        {
            _doctypeService = doctypeService;
            _documentCopyService = documentCopyService;
            _docfieldService = docfieldService;
        }

        public ActionResult Index()
        {
            GetDoctypes();
            return View();
        }

        private void GetDoctypes()
        {
            _doctypeService.GetRepository().Gets(true, dt => dt.IsActivated);
        }

        public ActionResult TestDocument()
        {
            var doc = _documentCopyService.Get(1064);
            var compendium = doc.Document.Compendium;
            var citizenName = doc.Document.CitizenName;
            var currentUser = doc.UserCurrentName;

            return View("Index");
        }
    }
}