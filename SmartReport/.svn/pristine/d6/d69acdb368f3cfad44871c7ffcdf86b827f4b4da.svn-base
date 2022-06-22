using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bkav.eGovCloud.Admin.Models;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
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
    public class InterfaceConfigController : CustomController
    {
        private readonly AdminGeneralSettings _generalSettings;
        private readonly ResourceBll _resourceService;
        private readonly InterfaceConfigBll _interfaceConfigService;
        private readonly WorkflowBll _workflowService;

        //private const string TEMPLALTE_PATH = "~/Content/TemplateUICategoryBusiness";
        private const string DEFAULT_SORT_BY = "InterfaceConfigName";

        public InterfaceConfigController(
            AdminGeneralSettings generalSettings,
            ResourceBll resourceService,
            InterfaceConfigBll interfaceConfigService,
            WorkflowBll workflowService)
            : base()
        {
            _generalSettings = generalSettings;
            _resourceService = resourceService;
            _interfaceConfigService = interfaceConfigService;
            _workflowService = workflowService;
        }

        public ActionResult Index()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Template.NotPermission"));
            //    return RedirectToAction("Index", "Welcome");
            //}

            var search = new InterfaceConfigSearchModel();
            var sortAndPage = new SortAndPagingModel
            {
                PageSize = _generalSettings.DefaultPageSize,
                CurrentPage = 1,
                IsSortDescending = true,
                SortBy = DEFAULT_SORT_BY
            };

            var httpCookie = Request.Cookies[CookieName.SearchInterfaceConfig];
            var isInvalidCookie = false;
            if (httpCookie != null)
            {
                try
                {
                    var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                    search = Json2.ParseAs<InterfaceConfigSearchModel>(data["Search"].ToString());
                    sortAndPage = Json2.ParseAs<SortAndPagingModel>(data["SortAndPaging"].ToString());
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    isInvalidCookie = true;
                }
            }

            int totalRecords;

            var model = _interfaceConfigService.GetsAs(out totalRecords,
                p => new InterfaceConfigModel
                {
                    InterfaceConfigId = p.InterfaceConfigId,
                    InterfaceConfigName = p.InterfaceConfigName,
                    Description = p.Description
                },
                   pageSize: sortAndPage.PageSize,
                   sortBy: sortAndPage.SortBy,
                   isDescending: sortAndPage.IsSortDescending,
                   currentPage: sortAndPage.CurrentPage,
                   findText: search.FindText);

            sortAndPage.TotalRecordCount = totalRecords;
            if (isInvalidCookie)
            {
                CreateCookieSearch(search, sortAndPage);
            }

            ViewBag.SortAndPage = sortAndPage;
            ViewBag.KeySearch = search;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;

            return View(model);
        }

        public ActionResult Create()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Template.NotPermissionCreate"));
            //    return RedirectToAction("Index");
            //}

            var model = new InterfaceConfigModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(InterfaceConfigModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Template.NotPermissionCreate"));
            //    return RedirectToAction("Index");
            //}

            if (ModelState.IsValid)
            {
                try
                {
                    var entity = model.ToEntity();
                    if (model.HasCreatePacket)
                    {
                        var names = model.InterfaceConfigName.Split(';').Distinct();
                        var list = new List<InterfaceConfig>();
                        foreach (var name in names)
                        {
                            var item = entity.Clone();
                            item.InterfaceConfigName = name;
                            list.Add(item);
                        }
                        _interfaceConfigService.Create(list);
                    }
                    else
                    {
                        _interfaceConfigService.Create(entity);
                    }

                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.InterfaceConfig.Create.Successed"));
                    SuccessNotification(_resourceService.GetResource("Customer.InterfaceConfig.Create.Successed"));
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.InterfaceConfig.Create.Erorr"));
                    SuccessNotification(_resourceService.GetResource("Customer.InterfaceConfig.Create.Erorr"));
                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Template.NotPermissionDelete"));
            //    return RedirectToAction("Index");
            //}

            var cfg = _interfaceConfigService.Get(id);
            if (cfg == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.InterfaceConfig.NotExist"));
                ErrorNotification(_resourceService.GetResource("Customer.InterfaceConfig.NotExist"));
                return RedirectToAction("Index");
            }

            if (cfg.CategoryBusinessId.HasValue)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.InterfaceConfig.CategoryBusiness.Activated"));
                ErrorNotification(_resourceService.GetResource("Customer.InterfaceConfig.CategoryBusiness.Activated"));
                return RedirectToAction("Index");
            }

            var workflows = _workflowService.Gets();
            if (workflows != null && workflows.Any())
            {
                var exists = workflows.Where(p => p.InterfaceConfigId == id);
                if (exists != null && exists.Any())
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.InterfaceConfig.Delete.IsUser"));
                    ErrorNotification(_resourceService.GetResource("Customer.InterfaceConfig.Delete.IsUser"));
                    return RedirectToAction("Index");
                }

                foreach (var item in workflows)
                {
                    try
                    {
                        var jsonInObject = item.JsonInObject;
                        if (jsonInObject == null
                            || (jsonInObject.Nodes == null || !jsonInObject.Nodes.Any()))
                        {
                            continue;
                        }

                        foreach (var node in jsonInObject.Nodes)
                        {
                            if (node.TemplateId.HasValue && node.TemplateId.Value == id)
                            {
                                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.InterfaceConfig.Delete.IsUser"));
                                ErrorNotification(_resourceService.GetResource("Customer.InterfaceConfig.Delete.IsUser"));
                                return RedirectToAction("Index");
                            }
                        }

                    }
                    catch { }
                }
            }

            try
            {
                _interfaceConfigService.Delete(cfg);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.InterfaceConfig.Delete.Successed"));
                SuccessNotification(_resourceService.GetResource("Customer.InterfaceConfig.Delete.Successed"));
            }
            catch (Exception ex)
            {
                LogException(ex);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.InterfaceConfig.Delete.Error"));
                ErrorNotification(_resourceService.GetResource("Customer.InterfaceConfig.Delete.Error"));
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Template.NotPermissionUpdate"));
            //    return RedirectToAction("Index");
            //}

            var cfg = _interfaceConfigService.Get(id);
            if (cfg == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.InterfaceConfig.NotExist"));
                ErrorNotification(_resourceService.GetResource("Customer.InterfaceConfig.NotExist"));
                return RedirectToAction("Index");
            }

            return View(cfg.ToModel());
        }

        [HttpPost]
        public ActionResult Edit(InterfaceConfigModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Template.NotPermissionUpdate"));
            //    return RedirectToAction("`Index");
            //}

            if (ModelState.IsValid)
            {
                var cfg = _interfaceConfigService.Get(model.InterfaceConfigId);
                if (cfg == null)
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.InterfaceConfig.NotExist"));
                    ErrorNotification(_resourceService.GetResource("Customer.InterfaceConfig.NotExist"));
                    return RedirectToAction("Index");
                }

                try
                {
                    var oldTempCfg = cfg.Template;
                    var oldCategoryBussinessId = cfg.CategoryBusinessId;
                    cfg = model.ToEntity(cfg);
                    cfg.Template = oldTempCfg;
                    cfg.CategoryBusinessId = oldCategoryBussinessId;

                    _interfaceConfigService.Update(cfg);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.InterfaceConfig.Updated.Success"));
                    SuccessNotification(_resourceService.GetResource("Customer.InterfaceConfig.Updated.Success"));

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.InterfaceConfig.Updated.Error"));
                    ErrorNotification(_resourceService.GetResource("Customer.InterfaceConfig.Updated.Error"));
                }
            }

            return View(model);
        }

        public ActionResult Search(InterfaceConfigSearchModel search, int pageSize)
        {
            IEnumerable<InterfaceConfigModel> model = null;
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    int totalRecords;
                    var sortAndPage = new SortAndPagingModel
                    {
                        PageSize = pageSize,
                        CurrentPage = 1,
                        IsSortDescending = true,
                        SortBy = DEFAULT_SORT_BY
                    };

                    model = _interfaceConfigService.GetsAs(
                        out totalRecords,
                          p => new InterfaceConfigModel
                          {
                              InterfaceConfigId = p.InterfaceConfigId,
                              InterfaceConfigName = p.InterfaceConfigName,
                              Description = p.Description
                          },
                            pageSize: sortAndPage.PageSize,
                            sortBy: sortAndPage.SortBy,
                            isDescending: sortAndPage.IsSortDescending,
                            currentPage: sortAndPage.CurrentPage,
                            findText: search.FindText);

                    sortAndPage.TotalRecordCount = totalRecords;
                    CreateCookieSearch(search, sortAndPage);
                    ViewBag.SortAndPage = sortAndPage;
                    ViewBag.KeySearch = search;
                    ViewBag.ListPageSize = _generalSettings.ListPageSize;
                }
            }
            return PartialView("_PartialList", model);
        }

        public ActionResult SortAndPaging(
            InterfaceConfigSearchModel search,
            string sortBy, bool isSortDesc,
            int page, int pageSize)
        {
            IEnumerable<InterfaceConfigModel> model = null;
            if (Request.IsAjaxRequest())
            {
                int totalRecords;
                var sortAndPage = new SortAndPagingModel
                {
                    PageSize = pageSize,
                    CurrentPage = page,
                    IsSortDescending = isSortDesc,
                    SortBy = sortBy
                };

                model = _interfaceConfigService.GetsAs(
                         out totalRecords,
                           p => new InterfaceConfigModel
                           {
                               InterfaceConfigId = p.InterfaceConfigId,
                               InterfaceConfigName = p.InterfaceConfigName,
                               Description = p.Description
                           },
                             pageSize: sortAndPage.PageSize,
                             sortBy: sortAndPage.SortBy,
                             isDescending: sortAndPage.IsSortDescending,
                             currentPage: sortAndPage.CurrentPage,
                             findText: search.FindText);

                sortAndPage.TotalRecordCount = totalRecords;

                CreateCookieSearch(search, sortAndPage);
                ViewBag.SortAndPage = sortAndPage;
                ViewBag.KeySearch = search;
                ViewBag.ListPageSize = _generalSettings.ListPageSize;
            }

            return PartialView("_PartialList", model);
        }

        public FileResult ExportJsonToFile(int id)
        {
            var cfg = _interfaceConfigService.Get(id);
            if (cfg == null)
            {
                throw new Exception("InterfaceConfig is not eixst.");
            }

            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(cfg.Template);
            writer.Flush();
            stream.Position = 0;
            var name = string.Format("{0}.json", cfg.InterfaceConfigName.StripVietnameseChars());
            return File(stream, System.Net.Mime.MediaTypeNames.Application.Octet, name);
        }

        public JsonResult UpdateJson(int id, HttpPostedFileBase files)
        {
            var cfg = _interfaceConfigService.Get(id);
            if (cfg == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.InterfaceConfig.NotExist"));
                return Json(new { error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.InterfaceConfig.NotExist") });
            }

            if (files == null || files.ContentLength == 0)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.InterfaceConfig.NotFile"));
                return Json(new { error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.InterfaceConfig.NotFile") });
            }

            var extension = System.IO.Path.GetExtension(files.FileName);
            var extSupport = new[] { @".json", @".txt" };
            if (!extSupport.Contains(extension.ToLower()))
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.InterfaceConfig.Extension"));
                return Json(new { error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.InterfaceConfig.Extension") });
            }

            var reader = new StreamReader(files.InputStream);
            var json = reader.ReadToEnd();

            try
            {
                cfg.Template = json;
                _interfaceConfigService.Update(cfg);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.InterfaceConfig.UpdateTemplate.Success"));
                return Json(new { success = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.InterfaceConfig.UpdateTemplate.Success") });
            }
            catch
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.InterfaceConfig.UpdateTemplate.Error"));
                return Json(new { error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.InterfaceConfig.UpdateTemplate.Error") });
            }
        }

        public ActionResult ConfigTemplateWorkflow(int id)
        {
            var cfg = _interfaceConfigService.Get(id);
            if (cfg == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.InterfaceConfig.NotExist"));
                ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.InterfaceConfig.NotExist"));
                return RedirectToAction("Index");
            }

            var enums = Enum.GetNames(typeof(CategoryBusinessTypes));
            ViewBag.TemplateCategoryBusiness = enums.StringifyJs();
            ViewBag.InterfaceConfigId = id;
            ViewBag.Template = cfg.Template;
            ViewBag.OtherInterfaceConfigs = _interfaceConfigService.GetsAs(w => new
            {
                w.InterfaceConfigId,
                w.InterfaceConfigName
            }, w => w.InterfaceConfigId != id).StringifyJs();

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult ConfigTemplateWorkflow(int id, string template)
        {
            var cfg = _interfaceConfigService.Get(id);
            if (cfg == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.InterfaceConfig.NotExist"));
                return Json(new { error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.InterfaceConfig.NotExist") });
            }

            try
            {
                cfg.Template = template;
                _interfaceConfigService.Update(cfg);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                LogException(ex);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.InterfaceConfig.UpdateTemplate.Error"));
                return Json(new { error = _resourceService.GetResource("Admin.InterfaceConfig.UpdateTemplate.Error") });
            }
        }

        public JsonResult GetWorkflowTemplate(int id)
        {
            var template = string.Empty;
            var cfg = _interfaceConfigService.Get(id);
            if (cfg != null)
            {
                template = cfg.Template;
            }
            return Json(template, JsonRequestBehavior.AllowGet);
        }

        #region

        private void CreateCookieSearch(InterfaceConfigSearchModel search, SortAndPagingModel sortpage)
        {
            var data = new Dictionary<string, object> { { "Search", search }, { "SortAndPaging", sortpage } };
            var cookie = Request.Cookies[CookieName.SearchInterfaceConfig];
            if (cookie == null)
            {
                cookie = new HttpCookie(CookieName.SearchInterfaceConfig);
            }

            cookie.Expires = DateTime.Now.AddDays(1);
            cookie.Value = data.StringifyJs();
            cookie.Path = "/admin";

            Response.Cookies.Add(cookie);
        }

        #endregion
    }
}