using System;
using System.Linq;
using System.Web.Mvc;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Web.Framework;
using System.Collections.Generic;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class CategoryBusinessController : CustomController
    {
        //private const string TEMPLATE_PATH = "~/Content/TemplateUICategoryBusiness";
        private readonly ResourceBll _resourceService;
        private readonly InterfaceConfigBll _interfaceConfigService;

        public CategoryBusinessController(ResourceBll resourceService, InterfaceConfigBll interfaceConfigService)
            : base()
        {
            _resourceService = resourceService;
            _interfaceConfigService = interfaceConfigService;
        }

        public ActionResult Index()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.CategoryBusiness.NotPermission"));
                ErrorNotification(_resourceService.GetResource("Customer.CategoryBusiness.NotPermission"));
                return RedirectToAction("Index", "Welcome");
            }

            //var listCategoryBusiness = EnumHelper<CategoryBusinessTypes>.GetNameAndDescription();
            var listCategoryBusiness = new Dictionary<string, string>();
            listCategoryBusiness.Add("1", "Báo cáo thuyết minh");
            listCategoryBusiness.Add("2", "Báo cáo Danh sách");
            listCategoryBusiness.Add("4", "Báo cáo Báo cáo số liệu");
            //#if !HoSoMotCuaEdition || XuLyVanBanEdition
            //            listCategoryBusiness.Remove(CategoryBusinessTypes.Hsmc.ToString());
            //#endif
            return View(listCategoryBusiness);
        }

        public ActionResult ConfigTemplate(string id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.CategoryBusiness.NotPermissionConfigTemplate"));
                ErrorNotification(_resourceService.GetResource("Customer.CategoryBusiness.NotPermissionConfigTemplate"));
                return RedirectToAction("Index");
            }

            CategoryBusinessTypes categoryBusiness;
            try
            {
                categoryBusiness = (CategoryBusinessTypes)Enum.Parse(typeof(CategoryBusinessTypes), id, true);
            }
            catch (Exception ex)
            {
                LogException(ex);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.CategoryBusiness.NotFindId"));
                ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.CategoryBusiness.NotFindId") + id);
                return RedirectToAction("Index", "Error");
            }

            var categoryBusinessId = (int)categoryBusiness;
            var allCfgs = _interfaceConfigService.GetsAs(p => new
            {
                p.InterfaceConfigId,
                p.InterfaceConfigName,
                p.Template,
                p.CategoryBusinessId
            }, p => !p.CategoryBusinessId.HasValue || p.CategoryBusinessId == categoryBusinessId);
            var exist = allCfgs.FirstOrDefault(p => p.CategoryBusinessId == categoryBusinessId);

            ViewBag.Template = exist == null ? string.Empty : exist.Template;
            ViewBag.SelectInterfaceId = exist == null ? 0 : exist.InterfaceConfigId;
            ViewBag.AllTemplates = allCfgs.Select(p => new { p.InterfaceConfigId, p.InterfaceConfigName }).Stringify();
            ViewBag.CategoryBusiness = categoryBusiness.ToString();

            //FileManager.Default.Exist(categoryBusiness.ToString(), Server.MapPath(TEMPLATE_PATH))
            //? FileManager.Default.ReadString(categoryBusiness.ToString(), Server.MapPath(TEMPLATE_PATH))
            //: string.Empty;

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult ConfigTemplate(string id, string template, int interfaceConfigId)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.CategoryBusiness.NotPermissionConfigTemplate"));
                return Json(new { error = _resourceService.GetResource("Customer.CategoryBusiness.NotPermissionConfigTemplate") });
            }

            if (string.IsNullOrEmpty(template))
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.CategoryBusiness.NullTemplate"));
                return Json(new { error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.CategoryBusiness.NullTemplate") });
            }

            CategoryBusinessTypes categoryBusiness;
            try
            {
                categoryBusiness = (CategoryBusinessTypes)Enum.Parse(typeof(CategoryBusinessTypes), id, true);
            }
            catch (Exception ex)
            {
                LogException(ex);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.CategoryBusiness.NotFindId"));
                return Json(new { error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.CategoryBusiness.NotFindId") + id });
            }

            var cfg = _interfaceConfigService.Get(interfaceConfigId);
            if (cfg == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.CategoryBusiness.InterfaceConfig.NotExist"));
                return Json(new { error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.CategoryBusiness.InterfaceConfig.NotExist") });
            }

            _interfaceConfigService.UpdateTempCategoryBussiness(cfg, template, categoryBusiness);

            //   FileManager.Default.Update(categoryBusiness.ToString(), template, Server.MapPath(TEMPLATE_PATH));
            CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.CategoryBusiness.UpdateTemplate.Success"));
            return Json(new { success = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.CategoryBusiness.UpdateTemplate.Success") });
        }
    }
}
