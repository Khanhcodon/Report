using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    //[RequireHttps]
    public class BackupRestoreConfigController : CustomController
    {
        private readonly ResourceBll _resourceService;
        private readonly BackupRestoreConfigBll _backupRestoreConfigService;
        private readonly BackupRestoreHistoryBll _backupRestoreHistoryService;
        private readonly ShareFolderBll _shareFolderService;
        private readonly AdminGeneralSettings _generalSettings;
        private const string DEFAULT_SORT_BY = "Domain";

        public BackupRestoreConfigController(
            ResourceBll resourceService,
            BackupRestoreConfigBll backupRestoreConfigService,
            BackupRestoreHistoryBll backupRestoreHistoryService,
            ShareFolderBll shareFolderService,
            AdminGeneralSettings generalSettings)
            : base()
        {
            _resourceService = resourceService;
            _backupRestoreConfigService = backupRestoreConfigService;
            _backupRestoreHistoryService = backupRestoreHistoryService;
            _shareFolderService = shareFolderService;
            _generalSettings = generalSettings;
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

            int totalRecords;
            var model = _backupRestoreConfigService.GetsAs(out totalRecords,
                t => t, sortBy: DEFAULT_SORT_BY, isDescending: true).ToListModel();
            ViewBag.SortAndPage = new SortAndPagingModel
            {
                PageSize = _generalSettings.DefaultPageSize,
                CurrentPage = 1,
                IsSortDescending = false,
                SortBy = DEFAULT_SORT_BY,
                TotalRecordCount = totalRecords
            };
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            ViewBag.Search = string.Empty;

            return View(model);
        }

        public ActionResult SortAndPaging(string search, string sortBy, bool isSortDesc, int page, int pageSize)
        {
            IEnumerable<BackupRestoreConfigModel> model = null;
            if (Request.IsAjaxRequest())
            {
                int totalRecords;
                Expression<Func<BackupRestoreConfig, bool>> spec = null;
                if (!string.IsNullOrEmpty(search))
                {
                    spec = p => p.Domain.Contains(search);
                }
                model = _backupRestoreConfigService.GetsAs(out totalRecords,
                                               t => t,
                                               spec: spec,
                                                pageSize: pageSize,
                                                sortBy: sortBy,
                                                isDescending: isSortDesc,
                                                currentPage: page).ToListModel();
                ViewBag.SortAndPage = new SortAndPagingModel
                {
                    PageSize = pageSize,
                    CurrentPage = page,
                    IsSortDescending = isSortDesc,
                    SortBy = sortBy,
                    TotalRecordCount = totalRecords
                };

                ViewBag.Search = search;
                ViewBag.ListPageSize = _generalSettings.ListPageSize;
            }

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

            BindData();
            return View(new BackupRestoreConfigModel());
        }

        [HttpPost]
        public ActionResult Create(BackupRestoreConfigModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.BackupRestoreConfig.NotPermissionCreate"));
            //    return RedirectToAction("Index");
            //}

            if (ModelState.IsValid)
            {
                try
                {
                    var entity = model.ToEntity();
                    entity.UserName = entity.UserName.Base64Encode();
                    entity.Password = entity.Password.Base64Encode();

                    _backupRestoreConfigService.Create(entity);
                    SuccessNotification(_resourceService.GetResource("Customer.BackupRestoreConfig.Created"));
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.BackupRestoreConfig.Created"));
                    return RedirectToAction("Index");
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                }
            }

            BindData();
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

            var backupRestoreConfig = _backupRestoreConfigService.Get(id);
            if (backupRestoreConfig == null)
            {
                ErrorNotification(_resourceService.GetResource("Customer.BackupRestoreConfig.NotExist"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.BackupRestoreConfig.NotExist"));
                return RedirectToAction("Index");
            }

            var model = backupRestoreConfig.ToModel();
            model.UserName = model.UserName.Base64Decode();
            model.Password = model.Password.Base64Decode();

            BindData();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(BackupRestoreConfigModel model)
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
                var backupRestoreConfig = _backupRestoreConfigService.Get(model.BackupRestoreConfigId);
                if (backupRestoreConfig == null)
                {
                    ErrorNotification(_resourceService.GetResource("Customer.BackupRestoreConfig.NotExist"));
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.BackupRestoreConfig.NotExist"));
                    return RedirectToAction("Index");
                }

                try
                {
                    backupRestoreConfig = model.ToEntity(backupRestoreConfig);
                    backupRestoreConfig.UserName = backupRestoreConfig.UserName.Base64Encode();
                    backupRestoreConfig.Password = backupRestoreConfig.Password.Base64Encode();

                    _backupRestoreConfigService.Update(backupRestoreConfig);
                    SuccessNotification(_resourceService.GetResource("Customer.BackupRestoreConfig.Updated"));
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.BackupRestoreConfig.Updated"));
                    return RedirectToAction("Index");
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                }
            }

            BindData();
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.BackupRestoreConfig.NotPermissionDelete"));
            //    return RedirectToAction("Index");
            //}

            var backupRestoreConfig = _backupRestoreConfigService.Get(id);
            if (backupRestoreConfig == null)
            {
                ErrorNotification(_resourceService.GetResource("Customer.BackupRestoreConfig.NotExist"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.BackupRestoreConfig.NotExist"));
                return RedirectToAction("Index");
            }

            try
            {
                _backupRestoreConfigService.Delete(backupRestoreConfig);
                SuccessNotification(_resourceService.GetResource("Customer.BackupRestoreConfig.Deleted"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.BackupRestoreConfig.Deleted"));
            }
            catch (EgovException ex)
            {
                LogException(ex);
                ErrorNotification(ex.Message);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Detail(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.BackupRestoreConfig.NotPermissionUpdate"));
            //    return RedirectToAction("Index");
            //}

            var backupRestoreConfig = _backupRestoreConfigService.Get(id);
            if (backupRestoreConfig == null)
            {
                ErrorNotification(_resourceService.GetResource("Customer.BackupRestoreConfig.NotExist"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.BackupRestoreConfig.NotExist"));
                return RedirectToAction("Index");
            }

            var model = backupRestoreConfig.ToModel();
            BindData();
            return View(model);
        }

        [HttpPost]
        public ActionResult Backup(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.BackupRestoreConfig.NotPermissionRunBackup"));
            //    return RedirectToAction("Index");
            //}

            var model = _backupRestoreConfigService.Get(id);
            if (model == null)
            {
                ErrorNotification(_resourceService.GetResource("Customer.BackupRestoreConfig.NotExist"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.BackupRestoreConfig.NotExist"));
                return RedirectToAction("Index");
            }

            try
            {
                _backupRestoreConfigService.Backup(model, User.GetUserNameWithDomain());
                SuccessNotification(_resourceService.GetResource("Customer.BackupRestoreConfig.Backup.Success"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.BackupRestoreConfig.Backup.Success"));
            }
            catch (Exception ex)
            {
                LogException(ex);
                _backupRestoreConfigService.CrateHitoryBackupError(model, DateTime.Now, User.GetUserNameWithDomain());
                ErrorNotification(_resourceService.GetResource("Customer.BackupRestoreConfig.Backup.Error"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.BackupRestoreConfig.Backup.Success"));
            }

            return RedirectToAction("Detail", new { id = id });
        }

        [HttpGet]
        public ActionResult Config(int id, int type)
        {
            if (type == (int)Entities.DatabaseType.SqlServer)
            {
                return RedirectToAction("ConfigInSqlServer", new { id = id, type = type });
            }
            else if (type == (int)Entities.DatabaseType.MySql)
            {
                return RedirectToAction("ConfigInMySql", new { id = id, type = type });
            }
            else if (type == (int)Entities.DatabaseType.Oracle)
            {
                //  return RedirectToAction("ConfigInOracle", new { id = id, type = type });
                return View();
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult ConfigInSqlServer(int id, int type)
        {
            if (id <= 0)
            {
                return View(new ConfigInSqlServerModel());
            }

            var backupRestoreConfig = _backupRestoreConfigService.Get(id);
            if (backupRestoreConfig == null
                || backupRestoreConfig.DatabaseType != type
                || string.IsNullOrWhiteSpace(backupRestoreConfig.Config))
            {
                return View(new ConfigInSqlServerModel());
            }

            var model = Json2.ParseAs<ConfigInSqlServerModel>(backupRestoreConfig.Config);
            return View(model);
        }

        [HttpGet]
        public ActionResult ConfigInMySql(int id, int type)
        {
            if (id <= 0)
            {
                return View(new ConfigInMySqlModel());
            }

            var backupRestoreConfig = _backupRestoreConfigService.Get(id);
            if (backupRestoreConfig == null
                || backupRestoreConfig.DatabaseType != type
                || string.IsNullOrWhiteSpace(backupRestoreConfig.Config))
            {
                return View(new ConfigInMySqlModel());
            }

            var model = Json2.ParseAs<ConfigInMySqlModel>(backupRestoreConfig.Config);
            return View(model);
        }

        [HttpGet]
        public ActionResult ConfigInOracle(int id, int type)
        {
            if (id <= 0)
            {
                return View(new ConfigInOracleModel());
            }

            var backupRestoreConfig = _backupRestoreConfigService.Get(id);
            if (backupRestoreConfig == null
                || backupRestoreConfig.DatabaseType != type
                || string.IsNullOrWhiteSpace(backupRestoreConfig.Config))
            {
                return View(new ConfigInOracleModel());
            }

            var model = Json2.ParseAs<ConfigInOracleModel>(backupRestoreConfig.Config);
            return View(model);
        }

        [HttpGet]
        public ActionResult Restore(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.BackupRestoreConfig.NotPermissionUpdate"));
            //    return RedirectToAction("Index");
            //}

            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            var backupRestoreConfig = _backupRestoreConfigService.Get(id);
            if (backupRestoreConfig == null)
            {
                ErrorNotification(_resourceService.GetResource("Customer.BackupRestoreConfig.NotExist"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.BackupRestoreConfig.NotExist"));
                return RedirectToAction("Index");
            }
            var model = backupRestoreConfig.ToModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Restore(int BackupRestoreConfigId, HttpPostedFileBase fileBackup)
        {
            if (fileBackup == null || fileBackup.ContentLength < 0)
            {
                ErrorNotification("File retore is null or empty");
                return RedirectToAction("Restore", new { id = BackupRestoreConfigId });
            }

            var backupRestoreConfig = _backupRestoreConfigService.Get(BackupRestoreConfigId);
            if (backupRestoreConfig == null)
            {
                ErrorNotification(_resourceService.GetResource("Customer.BackupRestoreConfig.NotExist"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.BackupRestoreConfig.NotExist"));
                return RedirectToAction("Index");
            }

            try
            {
                var stream = fileBackup.InputStream;
                _backupRestoreConfigService.Restore(backupRestoreConfig, stream, fileBackup.FileName, User.GetUserNameWithDomain());
                SuccessNotification(_resourceService.GetResource("Customer.BackupRestoreConfig.RestoreSuccess"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.BackupRestoreConfig.RestoreSuccess"));
            }
            catch (Exception ex)
            {
                LogException(ex);
                _backupRestoreConfigService.CrateHitoryRestoreError(backupRestoreConfig, fileBackup.FileName, User.GetUserNameWithDomain());
                ErrorNotification(ex.Message);
            }

            return RedirectToAction("Restore", new { id = BackupRestoreConfigId });
        }

        private void BindData()
        {
            ViewBag.DatabaseTypeList = _resourceService.EnumToSelectList<DatabaseType>();

            var folderShareList = new List<SelectListItem>();
            var shareFolders = _shareFolderService.GetsReadOnly();

            if (shareFolders != null && shareFolders.Any())
            {
                foreach (var item in shareFolders)
                {
                    folderShareList.Add(new SelectListItem()
                    {
                        Text = item.Directory,
                        Value = item.ShareFolderId.ToString()
                    });
                }
            }

            ViewBag.FolderShareList = folderShareList;
        }
    }
}
