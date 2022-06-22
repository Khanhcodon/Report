using System.Linq;
using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovCloud.Entities;
using System.Web;
using System.IO;
namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class InfomationController : CustomController//BaseController
    {
        private readonly InfomationBll _infomationService;
        private readonly ResourceBll _resourceService;

        public InfomationController(
            InfomationBll infomationService,
            ResourceBll resourceService)
            : base()
        {
            _infomationService = infomationService;
            _resourceService = resourceService;
        }

        public ActionResult Index()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Infomation.NotPermission"));
                ErrorNotification(_resourceService.GetResource("Customer.Infomation.NotPermission"));
                return RedirectToAction("Index", "Welcome");
            }

            return RedirectToAction("Edit");
            //var model = _infomationService.Gets().ToListModel();
            //return View(model);
        }
        [HttpPost]
        public JsonResult Upload()
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i]; //Uploaded file
                                                            //Use the following properties to get file's name, size and MIMEType
                int fileSize = file.ContentLength;
                string fileName = file.FileName;
                string mimeType = file.ContentType;
                System.IO.Stream fileContent = file.InputStream;
                //To save file, use SaveAs method
                file.SaveAs(Server.MapPath("~/Content/Images/home/") + fileName); //File will be saved in application root
            }
            return Json("Uploaded " + Request.Files.Count + " files");
        }
        public ActionResult Create()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Infomation.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.Infomation.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            var infomation = _infomationService.Gets();
            if (infomation.Any())
            {
                return RedirectToAction("Index");
            }
            return View(new InfomationModel());
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "InfomationCreate")]
        public ActionResult Create(InfomationModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Infomation.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.Infomation.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                if (_infomationService.Gets().Any())
                {
                    return RedirectToAction("Index");
                }
                var infomation = model.ToEntity();
                try
                {
                    _infomationService.Create(infomation);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Infomation.Created"));
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Infomation.Created"));
                    return RedirectToAction("Index");
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                    return View(model);
                }
            }
            return View(model);
        }

        public ActionResult Edit()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Infomation.NotPermissionUpdate"));
                ErrorNotification(_resourceService.GetResource("Customer.Infomation.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            var infomation = _infomationService.First();
            ViewBag.NameFile = infomation.ImagePath;
            return View(infomation.ToModel());
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "InfomationEdit")]
        public ActionResult Edit(InfomationModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Infomation.NotPermissionUpdate"));
                ErrorNotification(_resourceService.GetResource("Customer.Infomation.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var infomation = _infomationService.Get(model.InfomationId);
                    if (infomation == null)
                    {
                        _infomationService.Create(model.ToEntity());
                        CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Infomation.Create"));
                        SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Infomation.Create"));
                        return View(model);
                    }
                    infomation = model.ToEntity(infomation);
                    _infomationService.Update(infomation);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Infomation.Updated"));
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Infomation.Updated"));
                    return RedirectToAction("Index");
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                    return View(model);
                }
            }
            return View(model);
        }
    }
}
