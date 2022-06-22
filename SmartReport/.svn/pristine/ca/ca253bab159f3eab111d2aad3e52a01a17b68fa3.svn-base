using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Controllers
{
    //[RequireHttps]
    public class StorePrivateController : AsyncController
	{
		private readonly StorePrivateBll _storePrivateService;
        private readonly UserBll _userService;

        public StorePrivateController(StorePrivateBll storePrivateService, UserBll userService)
        {
            _storePrivateService = storePrivateService;
            _userService = userService;
        }

        public JsonResult Gets()
        {
            var userCurrentId = CurrentUserId();

            var storeShare = _storePrivateService.GetsStoreShared(userCurrentId, parentId: 0).Select(s => new
                            {
                                id = s.StorePrivateId,
                                storePrivateId = s.StorePrivateId,
                                name = s.StorePrivateName,
                                descStorePrivate = s.Description,
                                parentId = s.ParentId,
                                level = s.Level,
                                status = s.Status,
                                userCreated = s.CreatedByUserId,
                                isStoreShared = s.HasShared,
                                isPrivate = !s.HasShared,
                                userIdJoined = s.UserIdJoined,
                                deptIdJoined = s.DeptIdJoined
                            });

            var storePrivate = _storePrivateService.GetsStorePrivate(userCurrentId, parentId: 0).Select(s => new
                {
                    id = s.StorePrivateId,
                    storePrivateId = s.StorePrivateId,
                    name = s.StorePrivateName,
                    descStorePrivate = s.Description,
                    parentId = s.ParentId,
                    level = s.Level,
                    status = s.Status,
                    isPrivate = true,
                    userCreated = s.CreatedByUserId,
                    userIdJoined = s.UserIdJoined,
                    deptIdJoined = s.DeptIdJoined,
                    isStoreShared = s.HasShared
                });

            return Json(new { storePrivate, storeShare }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AnycStoreShare()
        {
            var userCurrentId = CurrentUserId();

            var storeShare = _storePrivateService.GetsStoreShared(userCurrentId, 0)
                                .Select(s => new
                                {
                                    id = s.StorePrivateId,
                                    storePrivateId = s.StorePrivateId,
                                    name = s.StorePrivateName,
                                    descStorePrivate = s.Description,
                                    parentId = s.ParentId,
                                    level = s.Level,
                                    status = s.Status,
                                    isStoreShared = true,
                                    userIdJoined = s.UserIdJoined
                                });

            return Json(storeShare, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStoreActive()
        {
            var storePrivate = _storePrivateService.GetsStorePrivate(CurrentUserId(), 0, true)
                                    .Select(s => new { s.StorePrivateId, s.StorePrivateName, s.ParentId, s.Level, s.Status });

            var storeShare = _storePrivateService.GetsStoreShared(CurrentUserId(), 0, true)
                                    .Select(s => new { s.StorePrivateId, s.StorePrivateName, s.ParentId, s.Level, s.Status });

            return Json(new { storePrivate, storeShare }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUserJoined(int id)
        {
            var storePrivate = _storePrivateService.Get(id, CurrentUserId());
            if (storePrivate == null)
            {
                return Json(new { error = true, message = "Không tìm thấy hồ sơ cá nhân" }, JsonRequestBehavior.AllowGet);
            }
            return
                Json(storePrivate.StorePrivateUsers.Select(s => new
                {
                    id = s.UserId
                }));
        }

        [HttpPost]
        // [ValidateAntiForgeryToken(Salt = "StorePrivateCreate")]
		[ValidateAntiForgeryToken]
		public JsonResult Create(string storePrivateName, string descStorePrivate, int? parentId, List<int> userIdJoined, List<int> deptIdJoined)
        {
            if (string.IsNullOrWhiteSpace(storePrivateName))
            {
                return Json(new { validateMessage = "Bạn phải nhập tên hồ sơ" });
            }
            var result = new List<dynamic>();

            try
            {
                var storeNames = storePrivateName.Split(';');
                foreach (var name in storeNames)
                {
                    if (string.IsNullOrEmpty(name))
                    {
                        continue;
                    }

                    var storePrivate = new StorePrivate
                    {
                        StorePrivateName = name,
                        Description = descStorePrivate,
                        ParentId = parentId,
                        CreatedByUserId = CurrentUserId(),
                        CreatedOnDate = DateTime.Now,
                        Status = (byte)StorePrivateStatus.IsActive
                    };

                    _storePrivateService.Create(storePrivate, userIdJoined, deptIdJoined);
                    result.Add(new
                    {
                        storePrivateId = storePrivate.StorePrivateId,
                        id = storePrivate.StorePrivateId,
                        name = name,
                        userIdJoined = userIdJoined,
                        deptIdJoined = deptIdJoined,
                        isStoreShared = (userIdJoined != null && userIdJoined.Any()) || (deptIdJoined != null && deptIdJoined.Any())
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new { error = true, message = ex.Message });
            }

            return Json(new
            {
                success = true,
                data = result,
                id = 0
            });
        }

        [HttpPost]
        // [ValidateAntiForgeryToken(Salt = "StorePrivateUpdate")]
		[ValidateAntiForgeryToken]
		public JsonResult Update(int id, string storePrivateName, string descStorePrivate, List<int> userIdJoined, List<int> deptIdJoined)
        {
            if (string.IsNullOrWhiteSpace(storePrivateName))
            {
                return Json(new { validateMessage = "Bạn phải nhập tên hồ sơ" });
            }
            var storePrivate = _storePrivateService.Get(id, CurrentUserId());
            if (storePrivate == null)
            {
                return Json(new { error = true, message = "Không tìm thấy hồ sơ cá nhân" });
            }
            var oldName = storePrivate.StorePrivateName;
            storePrivate.StorePrivateName = storePrivateName;
            storePrivate.Description = descStorePrivate;
            try
            {
                _storePrivateService.Update(storePrivate, userIdJoined, deptIdJoined, oldName);
                return Json(new { success = true });
            }
            catch (EgovException ex)
            {
                return Json(new { error = true, message = ex.Message });
            }
        }

        [HttpPost]
        // [ValidateAntiForgeryToken(Salt = "StorePrivateOpen")]
		[ValidateAntiForgeryToken]
		public JsonResult Open(int id)
        {
            try
            {
                _storePrivateService.Open(id, CurrentUserId());
                return Json(new { success = true });
            }
            catch (EgovException ex)
            {
                return Json(new { error = true, message = ex.Message });
            }
        }

        [HttpPost]
        // [ValidateAntiForgeryToken(Salt = "StorePrivateClose")]
		[ValidateAntiForgeryToken]
		public JsonResult Close(int id)
        {
            try
            {
                _storePrivateService.Close(id, CurrentUserId());
                return Json(new { success = true });
            }
            catch (EgovException ex)
            {
                return Json(new { error = true, message = ex.Message });
            }
        }

        [HttpPost]
        // [ValidateAntiForgeryToken(Salt = "StorePrivateDelete")]
		[ValidateAntiForgeryToken]
		public JsonResult Delete(int id)
        {
            try
            {
                _storePrivateService.Delete(id, CurrentUserId());
                return Json(new { success = true });
            }
            catch (EgovException ex)
            {
                return Json(new { error = true, message = ex.Message });
            }
        }

        public JsonResult Get(int id)
        {
            var storePrivate = _storePrivateService.Get(id, CurrentUserId());
            if (storePrivate == null)
            {
                return Json(new { error = true, message = "Không tìm thấy hồ sơ cá nhân" }, JsonRequestBehavior.AllowGet);
            }
            return
                Json(
                    new
                    {
                        storePrivateName = storePrivate.StorePrivateName,
                        descStorePrivate = storePrivate.Description,
                        userIdJoined = storePrivate.StorePrivateUsers.Where(s => s.UserId != 0 || s.UserId.HasValue).Select(s => s.UserId),
                        deptIdJoined = storePrivate.StorePrivateUsers.Where(s => s.UserId == 0 || !s.UserId.HasValue).Select(s => s.DepartmentId.Value)
                    }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetDocuments(int id)
        {
            var list = _storePrivateService.GetDocumentsByStorePrivateId(id, CurrentUserId());
            var documents = list == null ? "[]" : list.StringifyJs();
            return Json(new { documents });
        }

        [HttpPost]
		[ValidateAntiForgeryToken]
		// [ValidateAntiForgeryToken(Salt = "StorePrivateAddAttachment")]
        public JsonResult AddAttachment(int id, string desc, IEnumerable<HttpPostedFileBase> file)
        {
            try
            {
                foreach (var f in file)
                {
                    _storePrivateService.AddAttachment(id, desc, f.FileName, f.InputStream);
                }
            }
            catch (Exception ex)
            {
                return Json(new { error = ex });
            }
            return Json(new { success = true });
        }

        public JsonResult GetAttachment(int id)
        {
            var attachment = _storePrivateService.GetAttachment(id);
            if (attachment == null)
            {
                return Json(new { error = "Tài liệu không tồn tại" }, JsonRequestBehavior.AllowGet);
            }
            if (_storePrivateService.CheckPermissionStorePrivate(attachment.StorePrivateId, CurrentUserId()) == null)
            {
                return Json(new { error = "Bạn không có quyền xem tài liệu này" }, JsonRequestBehavior.AllowGet);
            }
            return
                Json(
                    new
                    {
                        attachment.AttachmentName,
                        attachment.CreatedByUserName,
                        CreatedOnDate = attachment.CreatedOnDate.ToString("d"),
                        attachment.Description,
                        attachment.SizeString
                    }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "StorePrivateRemoveAttachment")]
        public JsonResult RemoveAttachment(int id)
        {
            var attachment = _storePrivateService.GetAttachment(id);
            if (attachment == null)
            {
                return Json(new { error = "Tài liệu không tồn tại" }, JsonRequestBehavior.AllowGet);
            }
            if (_storePrivateService.CheckPermissionStorePrivate(attachment.StorePrivateId, CurrentUserId()) == null)
            {
                return Json(new { error = "Bạn không có quyền xem tài liệu này" }, JsonRequestBehavior.AllowGet);
            }
            _storePrivateService.RemoveAttachment(attachment);
            return Json(new { success = true });
        }

        public JsonResult RemoveDocuments(int storeId, List<int> documentCopyIds)
        {
            if (_storePrivateService.CheckPermissionStorePrivate(storeId, CurrentUserId()) == null)
            {
                return Json(new { error = "Bạn không có quyền xem hồ sơ này" }, JsonRequestBehavior.AllowGet);
            }

            var storeDoc = _storePrivateService.GetDocuments(storeId, documentCopyIds);
            if (!storeDoc.Any())
            {
                return Json(new { error = "Văn bản không tồn tại" }, JsonRequestBehavior.AllowGet);
            }

            _storePrivateService.RemoveDocuments(storeDoc);

            return Json(new { success = true });
        }

        public void DownloadAttachmentAsync(int id)
        {
            var userId = CurrentUserId();
            AsyncManager.OutstandingOperations.Increment();
            Task.Factory.StartNew(() =>
            {
                string fileName;
                var file = _storePrivateService.DownloadAttachment(out fileName, id, userId);
                return new { file, fileName };
            }).ContinueWith(t =>
            {
                if (!t.IsFaulted)
                {
                    AsyncManager.Parameters["file"] = t.Result.file;
                    AsyncManager.Parameters["fileName"] = t.Result.fileName;
                }
                else if (t.Exception != null)
                {
                    AsyncManager.Parameters["error"] = t.Exception;
                }
                AsyncManager.OutstandingOperations.Decrement();
            });
        }

        public ActionResult DownloadAttachmentCompleted(Stream file, string fileName, AggregateException error)
        {
            if (error != null)
            {
                if (error.InnerException is EgovException)
                {
                    return Json(new { error = error.InnerException.Message }, JsonRequestBehavior.AllowGet);
                }
                throw error.Flatten();
            }

            return File(file, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        public void DownloadAttachmentBase64Async(int id)
        {
            AsyncManager.OutstandingOperations.Increment();
            var userId = CurrentUserId();
            Task.Factory.StartNew(() =>
            {
                string fileName;
                var file = _storePrivateService.DownloadAttachment(out fileName, id, userId);
                return new { file, fileName };
            }).ContinueWith(t =>
            {
                if (!t.IsFaulted)
                {
                    AsyncManager.Parameters["id"] = id;
                    AsyncManager.Parameters["file"] = t.Result.file;
                    AsyncManager.Parameters["fileName"] = t.Result.fileName;
                }
                else if (t.Exception != null)
                {
                    AsyncManager.Parameters["error"] = t.Exception;
                }
                AsyncManager.OutstandingOperations.Decrement();
            });
        }

        public string DownloadAttachmentBase64Completed(int id, Stream file, string fileName, AggregateException error)
        {
            if (error != null)
            {
                if (error.InnerException is EgovException)
                {
                    return (new { error = error.InnerException.Message }).StringifyJs();
                }
                throw error.Flatten();
            }
            var ms = new MemoryStream();
            using (file)
            {
                var buffer = new byte[4096];
                while (true)
                {
                    var count = file.Read(buffer, 0, 4096);
                    if (count != 0)
                        ms.Write(buffer, 0, count);
                    else
                        break;
                }
            }
            return (new { fileName, content = Convert.ToBase64String(ms.ToArray()) }).StringifyJs();
        }

        private int CurrentUserId()
        {
            return _userService.CurrentUser.UserId;
        }

    }
}