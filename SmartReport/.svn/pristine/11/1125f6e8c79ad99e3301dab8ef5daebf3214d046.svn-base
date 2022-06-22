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
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    public class BackupRestoreManagerController : CustomController
    {
        private readonly ResourceBll _resourceService;
        private readonly BackupRestoreManagerBll _backupRestoreManagerService;
        private readonly BackupRestoreHistoryBll _backupRestoreHistoryService;
        private readonly AdminGeneralSettings _generalSettings;
        private readonly FileUploadSettings _fileSettings;

        private const string DEFAULT_SORT_BY = "DateCreated";
        private const string FORMAT_DATE_STRING = "yyyyMMddHHmmss";

        public BackupRestoreManagerController(
            ResourceBll resourceService,
            AdminGeneralSettings generalSettings,
            BackupRestoreManagerBll backupRestoreManagerService,
            BackupRestoreHistoryBll backupRestoreHistoryService,
            FileUploadSettings fileSettings)
            : base()
        {
            _resourceService = resourceService;
            _backupRestoreManagerService = backupRestoreManagerService;
            _generalSettings = generalSettings;
            _backupRestoreHistoryService = backupRestoreHistoryService;
            _fileSettings = fileSettings;
        }

        public ActionResult Index()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.BackupRestoreConfig.NotPermission"));
            //    return RedirectToAction("Index", "Welcome");
            //}

            var sortAndPage = new SortAndPagingModel
            {
                PageSize = _generalSettings.DefaultPageSize,
                CurrentPage = 1,
                IsSortDescending = true,
                SortBy = DEFAULT_SORT_BY
            };

            var search = new BackupRestoreManagerSearchModel();
            var httpCookie = Request.Cookies[CookieName.SearchBackupRestoreManager];
            var isInvalidCookie = false;
            if (httpCookie != null)
            {
                try
                {
                    var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                    search = Json2.ParseAs<BackupRestoreManagerSearchModel>(data["Search"].ToString());
                    sortAndPage = Json2.ParseAs<SortAndPagingModel>(data["SortAndPaging"].ToString());
                }
                catch (Exception)
                {
                    isInvalidCookie = true;
                }
            }

            int totalRecords;
            var model = _backupRestoreManagerService.GetsAs(out totalRecords,
                                               l => l,
                                               pageSize: sortAndPage.PageSize,
                                               sortBy: sortAndPage.SortBy,
                                               isDescending: sortAndPage.IsSortDescending,
                                               currentPage: sortAndPage.CurrentPage,
                                               isDatabase: search.IsDatabaseFile,
                                               from: string.IsNullOrEmpty(search.FromDate)
                                                       ? (DateTime?)null
                                                       : DateTime.Parse(search.FromDate,
                                                           System.Globalization.CultureInfo.
                                                                   GetCultureInfo("vi-VN").
                                                                   DateTimeFormat),
                                               to: string.IsNullOrEmpty(search.ToDate)
                                                       ? (DateTime?)null
                                                       : DateTime.Parse(search.ToDate,
                                                           System.Globalization.CultureInfo.
                                                                   GetCultureInfo("vi-VN").
                                                                   DateTimeFormat).AddDays(1).AddSeconds(-1)
                                               ).ToListModel();

            sortAndPage.TotalRecordCount = totalRecords;
            ViewBag.SortAndPage = sortAndPage;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            ViewBag.Search = search;

            if (isInvalidCookie)
            {
                CreateCookieSearch(search, sortAndPage);
            }

            return View(model);
        }

        private void CreateCookieSearch(BackupRestoreManagerSearchModel search, SortAndPagingModel sortpage)
        {
            var data = new Dictionary<string, object> { { "Search", search }, { "SortAndPaging", sortpage } };
            var cookie = Request.Cookies[CookieName.SearchBackupRestoreManager];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(1);
                cookie.Value = data.StringifyJs();
                cookie.Path = "/admin";
            }
            else
            {
                cookie = new HttpCookie(CookieName.SearchBackupRestoreManager, data.StringifyJs()) { Expires = DateTime.Now.AddDays(1) };
                cookie.Path = "/admin";
            }
            Response.Cookies.Add(cookie);
        }

        public ActionResult Search(BackupRestoreManagerSearchModel search, int pageSize)
        {
            IEnumerable<BackupRestoreManagerModel> model = null;
            SortAndPagingModel sortAndPage = null;
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    int totalRecords;
                    model = _backupRestoreManagerService.GetsAs(out totalRecords,
                                                   l => l,
                                                   pageSize: pageSize, sortBy: DEFAULT_SORT_BY,
                                                   isDescending: true,
                                                   isDatabase: search.IsDatabaseFile,
                                                   from: string.IsNullOrEmpty(search.FromDate)
                                                               ? (DateTime?)null
                                                               : DateTime.Parse(search.FromDate,
                                                                               System.Globalization.CultureInfo.
                                                                                   GetCultureInfo("vi-VN").
                                                                                   DateTimeFormat),
                                                   to: string.IsNullOrEmpty(search.ToDate)
                                                           ? (DateTime?)null
                                                           : DateTime.Parse(search.ToDate,
                                                                           System.Globalization.CultureInfo.
                                                                               GetCultureInfo("vi-VN").
                                                                               DateTimeFormat).AddDays(1).AddSeconds(-1))
                                                                               .ToListModel();
                    sortAndPage = new SortAndPagingModel
                    {
                        PageSize = pageSize,
                        CurrentPage = 1,
                        IsSortDescending = true,
                        SortBy = DEFAULT_SORT_BY,
                        TotalRecordCount = totalRecords
                    };
                }
                else
                {
                    sortAndPage = new SortAndPagingModel
                    {
                        PageSize = pageSize,
                        CurrentPage = 1,
                        IsSortDescending = false,
                        SortBy = DEFAULT_SORT_BY,
                        TotalRecordCount = 0
                    };
                }
            }

            ViewBag.SortAndPage = sortAndPage;
            ViewBag.Search = search;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            CreateCookieSearch(search, sortAndPage);
            return PartialView("_PartialList", model);
        }

        public ActionResult SortAndPaging(BackupRestoreManagerSearchModel search, string sortBy,
            bool isSortDesc, int page, int pageSize)
        {
            IEnumerable<BackupRestoreManagerModel> model = null;
            SortAndPagingModel sortAndPage = null;

            if (Request.IsAjaxRequest())
            {
                int totalRecords;
                model = _backupRestoreManagerService.GetsAs(out totalRecords,
                                               l => l, pageSize: pageSize, sortBy: sortBy,
                                               isDescending: isSortDesc,
                                               isDatabase: search.IsDatabaseFile,
                                               from: string.IsNullOrEmpty(search.FromDate)
                                                       ? (DateTime?)null
                                                       : DateTime.Parse(search.FromDate,
                                                           System.Globalization.CultureInfo.
                                                                   GetCultureInfo("vi-VN").
                                                                   DateTimeFormat),
                                               to: string.IsNullOrEmpty(search.ToDate)
                                                       ? (DateTime?)null
                                                       : DateTime.Parse(search.ToDate,
                                                           System.Globalization.CultureInfo.
                                                                   GetCultureInfo("vi-VN").
                                                                   DateTimeFormat).AddDays(1).AddSeconds(-1),
                                               currentPage: page).ToListModel();
                sortAndPage = new SortAndPagingModel
                {
                    PageSize = pageSize,
                    CurrentPage = page,
                    IsSortDescending = isSortDesc,
                    SortBy = sortBy,
                    TotalRecordCount = totalRecords
                };
            }

            ViewBag.SortAndPage = sortAndPage;
            ViewBag.Search = search;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            CreateCookieSearch(search, sortAndPage);
            return PartialView("_PartialList", model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.BackupRestoreConfig.NotPermissionCreate"));
            //    return RedirectToAction("Index");
            //}

            return View(new BackupRestoreManagerModel());
        }

        [HttpPost]
        public ActionResult Create(BackupRestoreManagerModel model, IEnumerable<HttpPostedFileBase> fileUpload)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.BackupRestoreConfig.NotPermissionCreate"));
            //    return RedirectToAction("Index");
            //}

            if (fileUpload == null || !fileUpload.Any())
            {
                ModelState.AddModelError("fileUpLoadError",
                    _resourceService.GetResource("Customer.BackupRestoreManager.FileUploadIsNotExist"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.BackupRestoreManager.FileUploadIsNotExist"));
                return View(model);
            }

            var dateCreated = DateTime.Now;
            if (ModelState.IsValid)
            {
                try
                {
                    #region

                    var filePaths = new List<string>();
                    var tempPath = Server.MapPath("~/Temp/");
                    var rootPath = Path.Combine(tempPath, dateCreated.ToString(FORMAT_DATE_STRING));

                    if (!System.IO.Directory.Exists(rootPath))
                    {
                        System.IO.Directory.CreateDirectory(rootPath);
                    }

                    foreach (var item in fileUpload)
                    {
                        var tmpPath = Path.Combine(rootPath, item.FileName);
                        filePaths.Add(tmpPath);
                        item.SaveAs(tmpPath);
                    }

                    var entity = model.ToEntity();
                    var fileNameAlias = string.Format("{0}_{1}.zip", model.Alias, dateCreated.ToString(FORMAT_DATE_STRING));
                    var outZipName = Path.Combine(rootPath, fileNameAlias);
                    FileHelper.CreateZipFile(filePaths, rootPath, fileNameAlias, true, true, password: model.ZipPassword);

                    using (var stream = System.IO.File.OpenRead(outZipName))
                    {
                        _backupRestoreManagerService.Create(stream, entity.Domain,
                            fileNameAlias, model.ZipPassword, model.Description,
                            model.Alias, model.IsDatabaseFile, dateCreated, User.GetUserNameWithDomain());
                    }

                    if (System.IO.Directory.Exists(rootPath))
                    {
                        System.IO.Directory.Delete(rootPath, true);
                    }

                    #endregion

                    string desciption = string.Format(
                        _resourceService.GetResource("Customer.BackupRestoreManager.LogCreateSuccess"),
                        User.GetUserNameWithDomain());
                    _backupRestoreHistoryService.Create(desciption, dateCreated, true, true, User.GetUserNameWithDomain());

                    SuccessNotification(_resourceService.GetResource("Customer.BackupRestoreManager.Created"));
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.BackupRestoreManager.Created"));
                    return RedirectToAction("Index");
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    string desciption = string.Format(_resourceService.GetResource("Customer.BackupRestoreManager.LogCreateError"), User.GetUserNameWithDomain());
                    _backupRestoreHistoryService.Create(desciption, dateCreated, true, false, User.GetUserNameWithDomain());

                    ErrorNotification(_resourceService.GetResource("Customer.BackupRestoreManager.ErrorCreated"));
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.BackupRestoreManager.ErrorCreated"));
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.BackupRestoreConfig.NotPermissionUpdate"));
            //    return RedirectToAction("Index");
            //}

            var model = _backupRestoreManagerService.Get(id);
            if (model == null)
            {
                ErrorNotification(_resourceService.GetResource("Customer.BackupRestoreManager.NotExist"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.BackupRestoreManager.NotExist"));
                return RedirectToAction("Index");
            }

            return View(model.ToModel());
        }

        [HttpPost]
        public ActionResult Edit(BackupRestoreManagerModel model, IEnumerable<HttpPostedFileBase> fileUpload)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.BackupRestoreConfig.NotPermissionUpdate"));
            //    return RedirectToAction("Index");
            //}

            if (ModelState.IsValid)
            {
                var entity = _backupRestoreManagerService.Get(model.BackupRestoreManagerId);
                if (entity == null)
                {
                    ErrorNotification(_resourceService.GetResource("Customer.BackupRestoreManager.NotExist"));
                    return RedirectToAction("Index");
                }

                var dateCreated = DateTime.Now;

                try
                {
                    #region

                    Stream stream = null;
                    if (fileUpload != null && fileUpload.Any())
                    {
                        var filePaths = new List<string>();
                        var rootPath = Server.MapPath("~/Temp/");
                        foreach (var item in fileUpload)
                        {
                            if (item != null && item.ContentLength > 0)
                            {
                                var tmpPath = Path.Combine(rootPath, item.FileName);
                                filePaths.Add(tmpPath);
                                item.SaveAs(tmpPath);
                            }
                        }

                        if (filePaths != null && filePaths.Any())
                        {
                            var fileNameAlias = string.Format("{0}_{1}.zip", model.Alias, dateCreated.ToString(FORMAT_DATE_STRING));
                            FileHelper.CreateZipFile(filePaths, rootPath, fileNameAlias,
                                deleteSource: true, password: model.ZipPassword);

                            var outZipName = Path.Combine(rootPath, fileNameAlias);
                            stream = System.IO.File.OpenRead(outZipName);
                            model.FileNameAlias = fileNameAlias;
                        }
                    }

                    _backupRestoreManagerService.Update(model.ToEntity(entity), stream, hasDeleteOldFile: true);

                    #endregion

                    string desciption = string.Format(
                        _resourceService.GetResource("Customer.BackupRestoreManager.LogEditSuccess"),
                        User.GetUserNameWithDomain());
                    _backupRestoreHistoryService.Create(desciption, dateCreated, true, true, User.GetUserNameWithDomain());

                    SuccessNotification(_resourceService.GetResource("Customer.BackupRestoreManager.Updated.Success"));
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.BackupRestoreManager.Updated.Success"));
                    return RedirectToAction("Index");
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    string desciption = string.Format(
                        _resourceService.GetResource("Customer.BackupRestoreManager.LogEditError"),
                        User.GetUserNameWithDomain());
                    _backupRestoreHistoryService.Create(desciption, dateCreated, true, false, User.GetUserNameWithDomain());
                    ErrorNotification(_resourceService.GetResource("Customer.BackupRestoreManager.Updated.Error"));
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.BackupRestoreManager.Updated.Error"));
                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(List<int> ids)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.BackupRestoreConfig.NotPermissionDelete"));
            //    return RedirectToAction("Index");
            //}

            if (ids == null || !ids.Any())
            {
                return RedirectToAction("Index");
            }

            var models = _backupRestoreManagerService.Gets(p => ids.Contains(p.BackupRestoreManagerId));
            if (models == null || !models.Any())
            {
                ErrorNotification(_resourceService.GetResource("Customer.BackupRestoreManager.NotExist"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.BackupRestoreManager.NotExist"));
                return RedirectToAction("Index");
            }

            try
            {
                _backupRestoreManagerService.Delete(models);
                SuccessNotification(_resourceService.GetResource("Customer.BackupRestoreManager.Deleted.Success"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.BackupRestoreManager.Deleted.Success"));
            }
            catch (EgovException ex)
            {
                LogException(ex);
                ErrorNotification(_resourceService.GetResource("Customer.BackupRestoreManager.Deleted.Error"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.BackupRestoreManager.Deleted.Error"));
            }

            return RedirectToAction("Index");
        }

        public FileResult Download(int id)
        {
            var model = _backupRestoreManagerService.Get(id);
            if (model == null)
                throw new Exception("Not exist.");

            var stream = _backupRestoreManagerService.Download(model);
            if (stream == null && model.Size > 0)
                throw new Exception("Files was deleted.");

            return File(stream, System.Net.Mime.MediaTypeNames.Application.Octet, model.FileNameAlias);
        }
    }
}
