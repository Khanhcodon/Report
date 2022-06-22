using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class BackupRestoreFileConfigController : CustomController
    {
        private readonly ResourceBll _resourceService;
        private readonly BackupRestoreFileConfigBll _backupRestoreFileConfigService;
        private readonly ShareFolderBll _shareFolderService;
        private const string FORMAT_DATE_STRING = "yyyyMMddHHmmss";

        public BackupRestoreFileConfigController(
            ResourceBll resourceService,
            BackupRestoreFileConfigBll backupRestoreFileConfigService,
            ShareFolderBll shareFolderService)
            : base()
        {
            _resourceService = resourceService;
            _backupRestoreFileConfigService = backupRestoreFileConfigService;
            _shareFolderService = shareFolderService;
        }

        public ActionResult Index()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.BackupRestoreFileConfig.NotPermission"));
            //    return RedirectToAction("Index", "Welcome");
            //}

            var model = _backupRestoreFileConfigService.GetsReadOnly().ToListModel();
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.BackupRestoreFileConfig.NotPermissionCreate"));
            //    return RedirectToAction("Index");
            //}

            BindData();
            return View(new BackupRestoreFileConfigModel());
        }

        [HttpPost]
        public ActionResult Create(BackupRestoreFileConfigModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.BackupRestoreFileConfig.NotPermissionCreate"));
            //    return RedirectToAction("Index");
            //}

            if (ModelState.IsValid)
            {
                try
                {
                    var entity = model.ToEntity();
                    entity.UserName = entity.UserName.Base64Encode();
                    entity.Password = entity.Password.Base64Encode();

                    _backupRestoreFileConfigService.Create(model.ToEntity());
                    SuccessNotification(_resourceService.GetResource("Customer.BackupRestoreFileConfig.Created"));
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.BackupRestoreFileConfig.Created"));
                    return RedirectToAction("Index");
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(_resourceService.GetResource("Customer.BackupRestoreFileConfig.ErrorCreated"));
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
            //    ErrorNotification(_resourceService.GetResource("Customer.BackupRestoreFileConfig.NotPermissionUpdate"));
            //    return RedirectToAction("Index");
            //}

            var entity = _backupRestoreFileConfigService.Get(id);
            if (entity == null)
            {
                ErrorNotification(_resourceService.GetResource("Customer.BackupRestoreFileConfig.NotExist"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.BackupRestoreFileConfig.NotExist"));
                return RedirectToAction("Index");
            }

            var model = entity.ToModel();
            model.UserName = model.UserName.Base64Decode();
            model.Password = model.Password.Base64Decode();

            BindData();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(BackupRestoreFileConfigModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.BackupRestoreFileConfig.NotPermissionUpdate"));
            //    return RedirectToAction("Index");
            //}

            if (ModelState.IsValid)
            {
                var entity = _backupRestoreFileConfigService.Get(model.BackupRestoreFileConfigId);
                if (entity == null)
                {
                    ErrorNotification(_resourceService.GetResource("Customer.BackupRestoreFileConfig.NotExist"));
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.BackupRestoreFileConfig.NotExist"));
                    return RedirectToAction("Index");
                }

                try
                {
                    entity = model.ToEntity(entity);
                    entity.UserName = entity.UserName.Base64Encode();
                    entity.Password = entity.Password.Base64Encode();

                    _backupRestoreFileConfigService.Update(entity);
                    SuccessNotification(_resourceService.GetResource("Customer.BackupRestoreFileConfig.UpdateSuccessed"));
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.BackupRestoreFileConfig.UpdateSuccessed"));
                    return RedirectToAction("Index");
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(_resourceService.GetResource("Customer.BackupRestoreFileConfig.UpdateError"));
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.BackupRestoreFileConfig.UpdateError"));
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
            //    ErrorNotification(_resourceService.GetResource("Customer.BackupRestoreFileConfig.NotPermissionDelete"));
            //    return RedirectToAction("Index");
            //}

            var entity = _backupRestoreFileConfigService.Get(id);
            if (entity == null)
            {
                ErrorNotification(_resourceService.GetResource("Customer.BackupRestoreFileConfig.NotExist"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.BackupRestoreFileConfig.NotExist"));
                return RedirectToAction("Index");
            }

            try
            {
                _backupRestoreFileConfigService.Delete(entity);
                SuccessNotification(_resourceService.GetResource("Customer.BackupRestoreFileConfig.DeleteSuccessed"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.BackupRestoreFileConfig.DeleteSuccessed"));
            }
            catch (EgovException ex)
            {
                LogException(ex);
                ErrorNotification(_resourceService.GetResource("Customer.BackupRestoreFileConfig.DeleteError"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.BackupRestoreFileConfig.DeleteError"));
            }

            return RedirectToAction("Index");
        }

        public ActionResult Detail(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.BackupRestoreFileConfig.NotPermissionDelete"));
            //    return RedirectToAction("Index");
            //}

            var entity = _backupRestoreFileConfigService.Get(id);
            if (entity == null)
            {
                ErrorNotification(_resourceService.GetResource("Customer.BackupRestoreFileConfig.NotExist"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.BackupRestoreFileConfig.NotExist"));
                return RedirectToAction("Index");
            }

            var model = entity.ToModel();
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
            //    ErrorNotification(_resourceService.GetResource("Customer.BackupRestoreFileConfig.NotPermissionBackup"));
            //    return RedirectToAction("Index");
            //}

            var entity = _backupRestoreFileConfigService.Get(id);
            if (entity == null)
            {
                ErrorNotification(_resourceService.GetResource("Customer.BackupRestoreFileConfig.NotExist"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.BackupRestoreFileConfig.NotExist"));
                return RedirectToAction("Index");
            }

            try
            {
                _backupRestoreFileConfigService.Backup(entity, User.GetUserNameWithDomain());
                SuccessNotification(_resourceService.GetResource("Customer.BackupRestoreFileConfig.BackupSuccessed"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.BackupRestoreFileConfig.BackupSuccessed"));
            }
            catch (Exception ex)
            {
                LogException(ex);
                _backupRestoreFileConfigService.CreateHistoryBackupError(entity, User.GetUserNameWithDomain());
                ErrorNotification(ex.Message);
            }

            return RedirectToAction("Detail", new { id = id });
        }

        [HttpPost]
        public ActionResult Restore(int id, string unzipPassword, HttpPostedFileBase fileRestore)
        {
            if (fileRestore == null || fileRestore.ContentLength <= 0)
            {
                ErrorNotification("File retore is null or empty");
                CreateActivityLog(ActivityLogType.Admin, "File retore is null or empty");
                return RedirectToAction("Restore", new { id = id });
            }

            if (!fileRestore.FileName.EndsWith(".zip", StringComparison.InvariantCultureIgnoreCase))
            {
                ErrorNotification(_resourceService.GetResource("Customer.BackupRestoreFileConfig.FileNotSuport"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.BackupRestoreFileConfig.FileNotSuport"));
                return RedirectToAction("Detail", new { id = id });
            }

            var entity = _backupRestoreFileConfigService.Get(id);
            if (entity == null)
            {
                ErrorNotification(_resourceService.GetResource("Customer.BackupRestoreFileConfig.NotExist"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.BackupRestoreFileConfig.NotExist"));
                return RedirectToAction("Index");
            }

            try
            {
                var tempPath = Server.MapPath("~/Temp/");
                var dateCreate = DateTime.Now;
                var rootPath = Path.Combine(tempPath, dateCreate.ToString(FORMAT_DATE_STRING));

                if (!System.IO.Directory.Exists(rootPath))
                {
                    System.IO.Directory.CreateDirectory(rootPath);
                }

                var filePath = Path.Combine(rootPath, fileRestore.FileName);
                fileRestore.SaveAs(filePath);
                _backupRestoreFileConfigService.Restore(entity, filePath, fileRestore.FileName,
                    unzipPassword, User.GetUserNameWithDomain());

                if (System.IO.Directory.Exists(rootPath))
                {
                    System.IO.Directory.Delete(rootPath, true);
                }

                SuccessNotification(_resourceService.GetResource("Customer.BackupRestoreFileConfig.RestoreSuccessed"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.BackupRestoreFileConfig.RestoreSuccessed"));
            }
            catch (Exception ex)
            {
                LogException(ex);
                _backupRestoreFileConfigService.CreateHistoryRestoreError(entity, fileRestore.FileName, User.GetUserNameWithDomain());
                ErrorNotification(ex.Message);
                CreateActivityLog(ActivityLogType.Admin, ex.Message);
            }

            return RedirectToAction("Detail", new { id = id });
        }

        private void BindData()
        {
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
