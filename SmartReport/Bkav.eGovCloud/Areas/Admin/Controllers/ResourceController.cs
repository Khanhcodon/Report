using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Bkav.eGovCloud.Admin.Models;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class ResourceController : CustomController//BaseController
    {
        private readonly ResourceBll _resourceService;
        private readonly AdminGeneralSettings _generalSettings;
        private readonly FileUploadSettings _fileUploadSettings;
        private const string DEFAULT_SORT_BY = "ResourceKey";

        public ResourceController(
            ResourceBll resourceService,
            AdminGeneralSettings generalSettings,
            FileUploadSettings fileUploadSettings)
            : base()
        {
            _resourceService = resourceService;
            _generalSettings = generalSettings;
            _fileUploadSettings = fileUploadSettings;
        }

        public ActionResult Index()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Resource.NotPermission"));
                ErrorNotification(_resourceService.GetResource("Customer.Resource.NotPermission"));
                return RedirectToAction("Index", "Welcome");
            }

            var search = string.Empty;
            var sortAndPage = new SortAndPagingModel
            {
                PageSize = _generalSettings.DefaultPageSize,
                CurrentPage = 1,
                IsSortDescending = false,
                SortBy = DEFAULT_SORT_BY
            };
            var httpCookie = Request.Cookies[CookieName.SearchResource];
            var isInvalidCookie = false;
            if (httpCookie != null)
            {
                try
                {
                    var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                    search = data["Search"] == null ? string.Empty : data["Search"].ToString();
                    sortAndPage = Json2.ParseAs<SortAndPagingModel>(data["SortAndPaging"].ToString());
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    isInvalidCookie = true;
                }
            }
            int totalRecords;
            var model = _resourceService.GetAllResources(out totalRecords, pageSize: sortAndPage.PageSize,
                                                            sortBy: sortAndPage.SortBy, isDescending: sortAndPage.IsSortDescending,
                                                            resourceKey: search, currentPage: sortAndPage.CurrentPage).ToListModel();
            sortAndPage.TotalRecordCount = totalRecords;

            if (isInvalidCookie)
            {
                CreateCookieSearch(search, sortAndPage);
            }

            ViewBag.SortAndPage = sortAndPage;
            ViewBag.ResourceKey = search;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            return View(model);
        }

        private void CreateCookieSearch(string resourceKey, SortAndPagingModel sortpage)
        {
            var data = new Dictionary<string, object> { { "Search", resourceKey }, { "SortAndPaging", sortpage } };
            var cookie = Request.Cookies[CookieName.SearchResource];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(1);
                cookie.Value = data.StringifyJs();
            }
            else
            {
                cookie = new HttpCookie(CookieName.SearchResource, data.StringifyJs()) { Expires = DateTime.Now.AddDays(1) };
            }
            cookie.Path = "/admin";
            Response.Cookies.Add(cookie);
        }

        public ActionResult Search(string resourceKey, int pageSize)
        {
            IEnumerable<ResourceModel> model = null;
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    if (!string.IsNullOrWhiteSpace(resourceKey))
                    {
                        resourceKey = resourceKey.Trim();
                    }
                    int totalRecords;
                    model = _resourceService.GetAllResources(out totalRecords, pageSize: pageSize,
                                                                sortBy: DEFAULT_SORT_BY, isDescending: false,
                                                                resourceKey: resourceKey).ToListModel();
                    var sortAndPage = new SortAndPagingModel
                    {
                        PageSize = pageSize,
                        CurrentPage = 1,
                        IsSortDescending = false,
                        SortBy = DEFAULT_SORT_BY,
                        TotalRecordCount = totalRecords
                    };
                    CreateCookieSearch(resourceKey, sortAndPage);
                    ViewBag.SortAndPage = sortAndPage;
                    ViewBag.ResourceKey = resourceKey;
                    ViewBag.ListPageSize = _generalSettings.ListPageSize;
                }
            }

            return PartialView("_PartialList", model);
        }

        public ActionResult SortAndPaging(
            string resourceKey,
            string sortBy,
            bool isSortDesc,
            int page,
            int pageSize)
        {
            IEnumerable<ResourceModel> model = null;
            if (Request.IsAjaxRequest())
            {
                if (!string.IsNullOrWhiteSpace(resourceKey))
                {
                    resourceKey = resourceKey.Trim();
                }
                int totalRecords;
                model = _resourceService.GetAllResources(out totalRecords, pageSize: pageSize,
                                                            sortBy: sortBy, isDescending: isSortDesc,
                                                            resourceKey: resourceKey, currentPage: page).ToListModel();
                var sortAndPage = new SortAndPagingModel
                {
                    PageSize = pageSize,
                    CurrentPage = page,
                    IsSortDescending = isSortDesc,
                    SortBy = sortBy,
                    TotalRecordCount = totalRecords
                };
                CreateCookieSearch(resourceKey, sortAndPage);
                ViewBag.SortAndPage = sortAndPage;
                ViewBag.ResourceKey = resourceKey;
                ViewBag.ListPageSize = _generalSettings.ListPageSize;
            }

            return PartialView("_PartialList", model);
        }

        public ActionResult Create()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Resource.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.Resource.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            return View(new ResourceModel());
        }

        [HttpPost]
        [ValidateInput(false)]
        //[ValidateAntiForgeryToken(Salt = "ResourceCreate")]
        public ActionResult Create(ResourceModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Resource.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.Resource.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _resourceService.Create(model.ToEntity());
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                    return View(model);
                }
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.Resource.Created"));
                SuccessNotification(_resourceService.GetResource("Common.Resource.Created"));
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
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Resource.NotPermissionUpdate"));
                ErrorNotification(_resourceService.GetResource("Customer.Resource.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            var resource = _resourceService.GetById(id);
            if (resource == null)
                return RedirectToAction("Index");
            var model = resource.ToModel();

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        //[ValidateAntiForgeryToken(Salt = "ResourceEdit")]
        public ActionResult Edit(ResourceModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Resource.NotPermissionUpdate"));
                ErrorNotification(_resourceService.GetResource("Customer.Resource.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var resource = _resourceService.GetById(model.ResourceId);
                if (resource == null)
                    return RedirectToAction("Index");
                var oldResourceKey = resource.ResourceKey;
                resource = model.ToEntity(resource);
                try
                {
                    _resourceService.Update(resource, oldResourceKey);
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                    return View(model);
                }
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.Resource.Updated"));
                SuccessNotification(_resourceService.GetResource("Common.Resource.Updated"));
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "ResourceDelete")]
        public ActionResult Delete(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Resource.NotPermissionDelete"));
                ErrorNotification(_resourceService.GetResource("Customer.Resource.NotPermissionDelete"));
                return RedirectToAction("Index");
            }

            var resource = _resourceService.GetById(id);
            if (resource == null)
                return RedirectToAction("Index");

            _resourceService.Delete(resource);
            CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.Resource.Deleted"));
            SuccessNotification(_resourceService.GetResource("Common.Resource.Deleted"));
            return RedirectToAction("Index");
        }

        public ActionResult Export()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Resource.NotPermissionExport"));
                ErrorNotification(_resourceService.GetResource("Customer.Resource.NotPermissionExport"));
                return RedirectToAction("Index");
            }

            try
            {
                var json = _resourceService.ExportResourcesToJson();
                var bytes = Encoding.UTF8.GetBytes(json);
                var stream = new MemoryStream(bytes);
                return File(stream, System.Net.Mime.MediaTypeNames.Text.Plain, "Resources.json");
            }
            catch (Exception ex)
            {
                LogException(ex);
                ErrorNotification(ex.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "ResourceImport")]
        public ActionResult Import(HttpPostedFileBase fileImport)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Resource.NotPermissionImport"));
                ErrorNotification(_resourceService.GetResource("Customer.Resource.NotPermissionImport"));
                return RedirectToAction("Index");
            }

            if (!_fileUploadSettings.FileUploadAllowedExtensions
                .Contains(fileImport.FileName.Substring(fileImport.FileName.LastIndexOf('.'))))
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Setting.FileUpload.FileNotAllow"));
                ErrorNotification(_resourceService.GetResource("Setting.FileUpload.FileNotAllow"));
            }
            else if (_fileUploadSettings.FileUploadMaximumSizeBytes < fileImport.ContentLength)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Setting.FileUpload.FileSizeGreaterThanMaximumSize"));
                ErrorNotification(_resourceService.GetResource("Setting.FileUpload.FileSizeGreaterThanMaximumSize"));
            }
            else
            {
                try
                {
                    _resourceService.ImportResourceFromJson(fileImport.InputStream);
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                }
            }

            return RedirectToAction("Index");
        }
    }
}
