using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Controllers
{
    public class OverallController : CustomerBaseController
    {
        private readonly UserBll _userService;
        private readonly DocumentCopyBll _docCopyService;
        private readonly DepartmentBll _departmentService;

        public OverallController(UserBll userService, 
                                DocumentCopyBll documentCopyService, 
                                DepartmentBll departmentService)
        {
            _userService = userService;
            _docCopyService = documentCopyService;
            _departmentService = departmentService;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}
