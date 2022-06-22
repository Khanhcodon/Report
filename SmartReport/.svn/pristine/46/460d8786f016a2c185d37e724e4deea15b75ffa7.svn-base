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
using Bkav.eGovCloud.Core.FileSystem;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    public class BackupRestoreHistoryController : CustomController
    {
        private readonly ResourceBll _resourceService;
        private readonly BackupRestoreHistoryBll _backupRestoreHistoryService;
        private readonly AdminGeneralSettings _generalSettings;
        private readonly BackupRestoreConfigBll _backupRestoreConfigService;
        private readonly BackupRestoreFileConfigBll _backupRestoreFileConfigService;
        private const string DEFAULT_SORT_BY = "DateCreated";
        private const string TEMPLALTE_PATH = "~/Content/HistoryBackupConfig";
        private const string FILE_NAME = "config.txt";

        public BackupRestoreHistoryController(
            ResourceBll resourceService,
            BackupRestoreHistoryBll backupRestoreHistoryService,
            AdminGeneralSettings generalSettings,
            BackupRestoreConfigBll backupRestoreConfigService,
            BackupRestoreFileConfigBll backupRestoreFileConfigService
            )
            : base()
        {
            _resourceService = resourceService;
            _backupRestoreHistoryService = backupRestoreHistoryService;
            _generalSettings = generalSettings;
            _backupRestoreConfigService = backupRestoreConfigService;
            _backupRestoreFileConfigService = backupRestoreFileConfigService;
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

            var search = new BackupRestoreHistorySearchModel();
            var httpCookie = Request.Cookies[CookieName.SearchBackupRestoreHistory];
            var isInvalidCookie = false;
            if (httpCookie != null)
            {
                try
                {
                    var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                    search = Json2.ParseAs<BackupRestoreHistorySearchModel>(data["Search"].ToString());
                    sortAndPage = Json2.ParseAs<SortAndPagingModel>(data["SortAndPaging"].ToString());
                }
                catch (Exception)
                {
                    isInvalidCookie = true;
                }
            }

            int totalRecords;
            var model = _backupRestoreHistoryService.GetsAs(out totalRecords,
                                                       l => l,
                                                       pageSize: sortAndPage.PageSize,
                                                       sortBy: sortAndPage.SortBy,
                                                       currentPage: sortAndPage.CurrentPage,
                                                       isDescending: true,
                                                       from: string.IsNullOrEmpty(search.FromDate)
                                                                   ? (DateTime?)null
                                                                   : DateTime.Parse(search.FromDate),
                                                       to: string.IsNullOrEmpty(search.ToDate)
                                                               ? (DateTime?)null
                                                               : DateTime.Parse(search.ToDate).AddDays(1).AddSeconds(-1),
                                                       search: search.Domain,
                                                       type: search.Type);
            sortAndPage.TotalRecordCount = totalRecords;
            if (isInvalidCookie)
            {
                CreateCookieSearch(search, sortAndPage);
            }

            ViewBag.SortAndPage = sortAndPage;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            ViewBag.Historys = model.ToListModel();
            ViewBag.Search = search;
            CheckExistFile();
            return View(search);
        }

        private void CreateCookieSearch(BackupRestoreHistorySearchModel search, SortAndPagingModel sortpage)
        {
            var data = new Dictionary<string, object> { { "Search", search }, { "SortAndPaging", sortpage } };
            var cookie = Request.Cookies[CookieName.SearchBackupRestoreHistory];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(1);
                cookie.Value = data.StringifyJs();
                cookie.Path = "/admin";
            }
            else
            {
                cookie = new HttpCookie(CookieName.SearchBackupRestoreHistory, data.StringifyJs())
                {
                    Expires = DateTime.Now.AddDays(1),
                    Path = "/admin"
                };
            }
            Response.Cookies.Add(cookie);
        }

        public ActionResult Search(BackupRestoreHistorySearchModel search, int pageSize)
        {
            IEnumerable<BackupRestoreHistoryModel> model = null;
            SortAndPagingModel sortAndPage = null;
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    int totalRecords;

                    if (search == null)
                    {
                        model = _backupRestoreHistoryService.GetsAs(out totalRecords, l => l, sortBy: DEFAULT_SORT_BY).ToListModel();
                    }
                    else
                    {
                        model = _backupRestoreHistoryService.GetsAs(out totalRecords,
                                                       l => l,
                                                       pageSize: pageSize,
                                                       sortBy: DEFAULT_SORT_BY,
                                                       isDescending: true,
                                                       from: string.IsNullOrEmpty(search.FromDate)
                                                                   ? (DateTime?)null
                                                                   : DateTime.Parse(search.FromDate),
                                                       to: string.IsNullOrEmpty(search.ToDate)
                                                               ? (DateTime?)null
                                                               : DateTime.Parse(search.ToDate).AddDays(1).AddSeconds(-1),
                                                       search: search.Domain,
                                                       type: search.Type).ToListModel();
                    }
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
            ViewBag.Historys = model;
            ViewBag.Search = search;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            CreateCookieSearch(search, sortAndPage);
            return PartialView("_PartialList");
        }

        public ActionResult SortAndPaging(BackupRestoreHistorySearchModel search, string sortBy,
            bool isSortDesc, int page, int pageSize)
        {
            IEnumerable<BackupRestoreHistoryModel> model = null;
            SortAndPagingModel sortAndPage = null;

            if (Request.IsAjaxRequest())
            {
                int totalRecords;
                if (search == null)
                {
                    model = _backupRestoreHistoryService.GetsAs(out totalRecords, l => l, sortBy: DEFAULT_SORT_BY).ToListModel();
                }
                else
                {
                    model = _backupRestoreHistoryService.GetsAs(out totalRecords,
                                                   l => l,
                                                   pageSize: pageSize,
                                                   sortBy: sortBy,
                                                   isDescending: isSortDesc,
                                                   from: string.IsNullOrEmpty(search.FromDate)
                                                           ? (DateTime?)null
                                                           : DateTime.Parse(search.FromDate),
                                                   to: string.IsNullOrEmpty(search.ToDate)
                                                           ? (DateTime?)null
                                                           : DateTime.Parse(search.ToDate).AddDays(1).AddSeconds(-1),
                                                   currentPage: page,
                                                   search: search.Domain,
                                                   type: search.Type).ToListModel();
                }

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
            ViewBag.Historys = model;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            CreateCookieSearch(search, sortAndPage);
            return PartialView("_PartialList");
        }

        [HttpPost]
        public ActionResult Delete(List<int> ids)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.BackupRestoreConfig.NotPermission"));
            //    return RedirectToAction("Index", "Welcome");
            //}

            if (ids == null || !ids.Any())
            {
                return RedirectToAction("Index");
            }

            var models = _backupRestoreHistoryService.Gets(p => ids.Contains(p.BackupRestoreHistoryId));
            if (models == null || !models.Any())
            {
                ErrorNotification(_resourceService.GetResource("Customer.BackupRestoreHistory.NotExist"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.BackupRestoreHistory.NotExist"));
                return RedirectToAction("Index");
            }

            try
            {
                _backupRestoreHistoryService.Delete(models);
                SuccessNotification(_resourceService.GetResource("Customer.BackupRestoreHistory.Deleted"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.BackupRestoreHistory.Deleted"));
            }
            catch (EgovException ex)
            {
                ErrorNotification(ex.Message);
                CreateActivityLog(ActivityLogType.Admin, ex.Message);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Detail(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.BackupRestoreConfig.NotPermission"));
            //    return RedirectToAction("Index", "Welcome");
            //}

            var model = _backupRestoreHistoryService.Get(id);
            if (model == null)
            {
                ErrorNotification(_resourceService.GetResource("Customer.BackupRestoreHistory.NotExist"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.BackupRestoreHistory.NotExist"));
                return RedirectToAction("Index");
            }

            return View(model.ToModel());
        }

        public ActionResult Create()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.BackupRestoreConfig.NotPermission"));
            //    return RedirectToAction("Index", "Welcome");
            //}
            return View(new BackupRestoreHistoryModel());
        }

        [HttpPost]
        public ActionResult Create(BackupRestoreHistoryModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.BackupRestoreConfig.NotPermission"));
            //    return RedirectToAction("Index", "Welcome");
            //}

            if (ModelState.IsValid)
            {
                try
                {
                    model.Account = User.GetUserNameWithDomain();
                    _backupRestoreHistoryService.Create(model.ToEntity());
                    SuccessNotification(_resourceService.GetResource("Customer.BackupRestoreHistory.Created.Success"));
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.BackupRestoreHistory.Created.Success"));
                    return RedirectToAction("Create");
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    ErrorNotification(_resourceService.GetResource("Customer.BackupRestoreHistory.Created.Error"));
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.BackupRestoreHistory.Created.Error"));
                }
            }
            return View(model);
        }

        [HttpPost]
        public JsonResult Upload(HttpPostedFileBase file)
        {
            if (file == null || file.ContentLength == 0)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.BackupRestoreHistory.NotExistFile"));
                return Json(new { error = _resourceService.GetResource("Admin.BackupRestoreHistory.NotExistFile") });
                
            }

            var extension = Path.GetExtension(file.FileName);
            if (!string.Equals(".rpt", extension, StringComparison.InvariantCultureIgnoreCase))
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.BackupRestoreHistory.OnlySupportRpt"));
                return Json(new { error = _resourceService.GetResource("Admin.BackupRestoreHistory.OnlySupportRpt") });
            }

            try
            {
                if (!FileManager.Default.Exist(FILE_NAME, Server.MapPath(TEMPLALTE_PATH)))
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.Customer.BackupRestoreHistory.NotExistFileConfig"));
                    throw new Exception(_resourceService.GetResource("Admin.Customer.BackupRestoreHistory.NotExistFileConfig"));
                }

                var content = FileManager.Default.ReadString(FILE_NAME, Server.MapPath(TEMPLALTE_PATH));
                HistoryFile oldConfig = null;

                if (!string.IsNullOrEmpty(content))
                {
                    oldConfig = Json2.ParseAs<HistoryFile>(content);
                }

                var newConfig = _backupRestoreHistoryService.Upload(file.InputStream, file.FileName, oldConfig);
                FileManager.Default.Update(FILE_NAME, newConfig.StringifyJs(), Server.MapPath(TEMPLALTE_PATH));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.BackupRestoreHistory.UpdatedFile.Success"));
                return Json(new { success = _resourceService.GetResource("Admin.BackupRestoreHistory.UpdatedFile.Success") });
            }
            catch (Exception ex)
            {
                LogException(ex);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.BackupRestoreHistory.UpdatedFile.Error"));
                return Json(new { error = _resourceService.GetResource("Admin.BackupRestoreHistory.UpdatedFile.Error") });
            }
        }

        public FileResult Download()
        {
            if (!FileManager.Default.Exist(FILE_NAME, Server.MapPath(TEMPLALTE_PATH)))
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.Customer.BackupRestoreHistory.NotExistFileConfig"));
                throw new Exception(_resourceService.GetResource("Admin.Customer.BackupRestoreHistory.NotExistFileConfig"));
            }

            var content = FileManager.Default.ReadString(FILE_NAME, Server.MapPath(TEMPLALTE_PATH));
            if (string.IsNullOrEmpty(content))
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.Customer.BackupRestoreHistory.NotConfig"));
                throw new Exception(_resourceService.GetResource("Admin.Customer.BackupRestoreHistory.NotConfig"));
            }

            var config = Json2.ParseAs<HistoryFile>(content);
            var stream = _backupRestoreHistoryService.Download(config);
            return File(stream, System.Net.Mime.MediaTypeNames.Application.Octet, config.RealFileName);
        }

        public void ExportToFile(BackupRestoreHistorySearchModel search, string selectedIds, string exportKey = "EXCELL")
        {
            if (!FileManager.Default.Exist(FILE_NAME, Server.MapPath(TEMPLALTE_PATH)))
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.Customer.BackupRestoreHistory.NotExistFileConfig"));
                throw new Exception(_resourceService.GetResource("Admin.Customer.BackupRestoreHistory.NotExistFileConfig"));
            }

            var content = FileManager.Default.ReadString(FILE_NAME, Server.MapPath(TEMPLALTE_PATH));
            if (string.IsNullOrEmpty(content))
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.Customer.BackupRestoreHistory.NotConfig"));
                throw new Exception(_resourceService.GetResource("Admin.Customer.BackupRestoreHistory.NotConfig"));
            }

            var config = Json2.ParseAs<HistoryFile>(content);
            List<int> selected = null;
            if (!string.IsNullOrEmpty(selectedIds))
            {
                try { selected = Json2.ParseAs<List<int>>(selectedIds); }
                catch { selected = new List<int>(); }
            }

            IEnumerable<BackupRestoreHistory> historys;
            if (selected != null && selected.Any())
            {
                historys = _backupRestoreHistoryService.GetsReadOnly(p => selected.Contains(p.BackupRestoreHistoryId));
            }
            else
            {
                historys = _backupRestoreHistoryService.GetsAs(
                                                           l => l,
                                                           from: string.IsNullOrEmpty(search.FromDate)
                                                                       ? (DateTime?)null
                                                                       : DateTime.Parse(search.FromDate),
                                                           to: string.IsNullOrEmpty(search.ToDate)
                                                                   ? (DateTime?)null
                                                                   : DateTime.Parse(search.ToDate).AddDays(1).AddSeconds(-1),
                                                           search: search.Domain,
                                                           type: search.Type);
            }

            try
            {
                ExportToFile(historys, config, exportKey);
            }
            catch (Exception ex)
            {
                LogException(ex);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.Customer.BackupRestoreHistory.ExportFile.Error"));
                throw new Exception(_resourceService.GetResource("Admin.Customer.BackupRestoreHistory.ExportFile.Error"));
            }
        }

        private void ExportToFile(IEnumerable<BackupRestoreHistory> historys, HistoryFile config, string exportKey = "EXCELL")
        {
            using (var rd = new CrystalDecisions.CrystalReports.Engine.ReportDocument())
            {
                var strPath = LoadCrystalFile(config);
                var name = "HistoryFile_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                rd.Load(strPath);
                rd.SetDataSource(historys);

                if (string.Equals(exportKey, "EXCELL", StringComparison.InvariantCultureIgnoreCase))
                {
                    rd.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel,
                        System.Web.HttpContext.Current.Response, true, name);
                }
                else if (string.Equals(exportKey, "WORD", StringComparison.InvariantCultureIgnoreCase))
                {
                    rd.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.WordForWindows,
                       System.Web.HttpContext.Current.Response, true, name);
                }
                else if (string.Equals(exportKey, "PDF", StringComparison.InvariantCultureIgnoreCase))
                {
                    rd.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat,
                       System.Web.HttpContext.Current.Response, true, name);
                }
            }
        }

        private string LoadCrystalFile(HistoryFile file)
        {
            var stream = _backupRestoreHistoryService.Download(file);
            var tempPath = Bkav.eGovCloud.Core.FileSystem.ResourceLocation.Default.FileUploadTemp;
            var temp = Bkav.eGovCloud.Core.FileSystem.FileManager.Default.Create(stream, tempPath, null, ".rpt");
            return temp.FullName;
        }

        private void CheckExistFile()
        {
            var exist = false;
            if (FileManager.Default.Exist(FILE_NAME, Server.MapPath(TEMPLALTE_PATH)))
            {
                var content = FileManager.Default.ReadString(FILE_NAME, Server.MapPath(TEMPLALTE_PATH));
                if (!string.IsNullOrEmpty(content))
                {
                    try
                    {
                        var config = Json2.ParseAs<HistoryFile>(content);
                        exist = true;
                    }
                    catch { }
                }
            }

            ViewBag.ExistFile = exist;
        }
    }
}
