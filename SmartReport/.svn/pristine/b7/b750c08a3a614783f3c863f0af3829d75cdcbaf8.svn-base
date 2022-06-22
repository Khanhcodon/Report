using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovCloud.Entities;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class BusinessTypeController : CustomController
    {
        private readonly BusinessTypeBll _businessTypeService;
        private readonly ResourceBll _resourceService;

        public BusinessTypeController(BusinessTypeBll businessTypeService,
                                     ResourceBll resourceService)
            : base()
        {
            _businessTypeService = businessTypeService;
            _resourceService = resourceService;
        }

        public ActionResult Index()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                ErrorNotification(_resourceService.GetResource("Customer.BusinessType.NotPermission"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.BusinessType.NotPermission"));
                return RedirectToAction("Index", "Welcome");
            }

            var model = _businessTypeService.Gets().ToListModel();
            return View(model);
        }

        public ActionResult Create()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                ErrorNotification(_resourceService.GetResource("Customer.BusinessType.NotPermissionCreate"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.BusinessType.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            return View(new BusinessTypeModel());
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "BusinessTypeCreate")]
        public ActionResult Create(BusinessTypeModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                ErrorNotification(_resourceService.GetResource("Customer.BusinessType.NotPermissionCreate"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.BusinessType.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var businessType = model.ToEntity();
                try
                {
                    _businessTypeService.Create(businessType);
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.BusinessType.Created"));
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.BusinessType.Created"));
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                    CreateActivityLog(ActivityLogType.Admin, ex.Message);
                    return View(model);
                }
                return RedirectToAction("Create");
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                ErrorNotification(_resourceService.GetResource("Customer.BusinessType.NotPermissionUpdate"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.BusinessType.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            var businessType = _businessTypeService.Get(id);
            if (businessType == null)
            {
                return RedirectToAction("Index");
            }
            var model = businessType.ToModel();
            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "BusinessTypeEdit")]
        public ActionResult Edit(BusinessTypeModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                ErrorNotification(_resourceService.GetResource("Customer.BusinessType.NotPermissionUpdate"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.BusinessType.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var businessType = _businessTypeService.Get(model.BusinessTypeId);
                if (businessType == null)
                {
                    return RedirectToAction("Index");
                }
                var oldBusinessTypeName = businessType.BusinessTypeName;
                try
                {
                    _businessTypeService.Update(model.ToEntity(businessType), oldBusinessTypeName);
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                    return View(model);
                }
                SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.BusinessType.Updated"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.BusinessType.Updated"));
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "BusinessTypeDelete")]
        public ActionResult Delete(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                ErrorNotification(_resourceService.GetResource("Customer.BusinessType.NotPermissionDelete"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.BusinessType.NotPermissionDelete"));
                return RedirectToAction("Index");
            }

            var businessType = _businessTypeService.Get(id);
            if (businessType != null)
            {
                try
                {
                    _businessTypeService.Delete(businessType);
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.BusinessType.Deleted"));
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.BusinessType.Deleted"));
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                    CreateActivityLog(ActivityLogType.Admin, ex.Message);
                }
            }
            return RedirectToAction("Index");
        }
    }
}
