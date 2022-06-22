using System.Linq;
using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovCloud.Entities;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class ShareFolderController : CustomController
    {
        private readonly AdminGeneralSettings _generalSettings;
        private readonly ShareFolderBll _shareFolderService;
        private readonly BackupRestoreConfigBll _backupRestoreConfigService;
        private readonly BackupRestoreHistoryBll _backupRestoreHistoryService;
        private readonly ResourceBll _resourceService;

        public ShareFolderController(ShareFolderBll shareFolderService,
            ResourceBll resourceService,
            AdminGeneralSettings generalSettings,
            BackupRestoreConfigBll backupRestoreConfigService,
            BackupRestoreHistoryBll backupRestoreHistoryService)
            : base()
        {
            _shareFolderService = shareFolderService;
            _resourceService = resourceService;
            _generalSettings = generalSettings;
            _backupRestoreConfigService = backupRestoreConfigService;
            _backupRestoreHistoryService = backupRestoreHistoryService;
        }

        public ActionResult Index()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.ShareFolder.NotPermission"));
            //    return RedirectToAction("Index", "Welcome");
            //}

            var model = _shareFolderService.GetsReadOnly().ToListModel();
            return View(model);
        }

        public ActionResult Create()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.ShareFolder.NotPermissionCreate"));
            //    return RedirectToAction("Index");
            //}

            return View(new ShareFolderModel());
        }

        [HttpPost]
        public ActionResult Create(ShareFolderModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.ShareFolder.NotPermissionCreate"));
            //    return RedirectToAction("Index");
            //}

            if (ModelState.IsValid)
            {
                var shareFolder = model.ToEntity();
                shareFolder.UserName = shareFolder.UserName.Base64Encode();
                shareFolder.Password = shareFolder.Password.Base64Encode();

                try
                {
                    _shareFolderService.Create(shareFolder);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.ShareFolder.Created"));
                    SuccessNotification(_resourceService.GetResource("Admin.ShareFolder.Created"));
                    return RedirectToAction("Index");
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                }
            }

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.ShareFolder.NotPermissionUpdate"));
            //    return RedirectToAction("Index");
            //}

            var shareFolder = _shareFolderService.Get(id);
            if (shareFolder == null)
            {
                return RedirectToAction("Index");
            }

            var model = shareFolder.ToModel();
            model.UserName = model.UserName.Base64Decode();
            model.Password = model.Password.Base64Decode();

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ShareFolderModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.ShareFolder.NotPermissionUpdate"));
            //    return RedirectToAction("Index");
            //}

            if (ModelState.IsValid)
            {
                var shareFolder = _shareFolderService.Get(model.ShareFolderId);
                if (shareFolder == null)
                {
                    return RedirectToAction("Index");
                }

                try
                {
                    shareFolder = model.ToEntity(shareFolder);
                    shareFolder.UserName = shareFolder.UserName.Base64Encode();
                    shareFolder.Password = shareFolder.Password.Base64Encode();

                    _shareFolderService.Update(shareFolder);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.ShareFolder.Updated"));
                    SuccessNotification(_resourceService.GetResource("Admin.ShareFolder.Updated"));
                    return RedirectToAction("Index");
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
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
            //    ErrorNotification(_resourceService.GetResource("Customer.ShareFolder.NotPermissionDelete"));
            //    return RedirectToAction("Index");
            //}

            var shareFolder = _shareFolderService.Get(id);
            if (shareFolder == null)
                return RedirectToAction("Index");

            var backupRestoreConfigs = _backupRestoreConfigService.Gets(p => p.ShareFolderId == id);
            if (backupRestoreConfigs != null && backupRestoreConfigs.Any())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.ShareFolder.ShareFolderIsUsing"));
                ErrorNotification(_resourceService.GetResource("Customer.ShareFolder.ShareFolderIsUsing"));
                return RedirectToAction("Index");
            }

            try
            {
                _shareFolderService.Delete(shareFolder);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.ShareFolder.Deleted"));
                SuccessNotification(_resourceService.GetResource("Admin.ShareFolder.Deleted"));
            }
            catch (EgovException ex)
            {
                LogException(ex);
                ErrorNotification(ex.Message);
            }

            return RedirectToAction("Index");
        }
    }
}
