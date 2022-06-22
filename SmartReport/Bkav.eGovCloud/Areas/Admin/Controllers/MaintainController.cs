using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Core.FileSystem;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Caching;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    public class MaintainController : CustomController
    {
        private MemoryCacheManager _cache;

        public MaintainController(MemoryCacheManager cache)
        {
            _cache = cache;
        }

        public ActionResult Index()
        {
            #region Kiểm tra dung lượng trống của thư mục cài đặt eGov

            var applicationPath = Server.MapPath("~");
            var pathRoot = Path.GetPathRoot(applicationPath);
            var driveInfo = new DriveInfo(pathRoot);
            var totalSize = driveInfo.TotalSize;
            var freeSpace = driveInfo.TotalFreeSpace;

            #endregion

            #region Temp File

            var temfileSize = DirectoryUtil.GetDirectorySize(ResourceLocation.Default.FileUploadTemp);
            temfileSize += DirectoryUtil.GetDirectorySize(ResourceLocation.Default.FileTemp);

            #endregion

            #region Cache

            var cacheSize = _cache.GetSizeOfMemories();

            #endregion

            var model = new MaintainModel()
            {
                DiskTotalSize = StringExtension.ReadFileSize(totalSize),
                DiskTotalFreeSpace = StringExtension.ReadFileSize(freeSpace),
                TempSize = StringExtension.ReadFileSize(temfileSize),
                CacheSize = cacheSize.StringifyJs()
            };

            return View(model);
        }

        public string DeleteAttachmentError()
        {
            var attachmentService = DependencyResolver.Current.GetService<AttachmentBll>();
            attachmentService.DeleteAttachmentError();

            return "Ok";
        }

        public string DeleteAttachmentErrorByDate(int year, int month)
        {
            var attachmentService = DependencyResolver.Current.GetService<AttachmentBll>();
            attachmentService.DeleteAttachmentError(year, month);

            return "Ok";
        }

        [HttpPost]
        public ActionResult DeleteTempFolder()
        {
            try
            {
                DirectoryUtil.EmptyFolder(ResourceLocation.Default.FileUploadTemp);
                DirectoryUtil.EmptyFolder(ResourceLocation.Default.FileTemp);
            }
            catch
            {
                CreateActivityLog(ActivityLogType.Admin, "Không thể làm trống thư mục tạm, vui lòng phân quyền và thử lại.");
                ErrorNotification("Không thể làm trống thư mục tạm, vui lòng phân quyền và thử lại.");
            }

            return RedirectToAction("Index");
        }
    }
}