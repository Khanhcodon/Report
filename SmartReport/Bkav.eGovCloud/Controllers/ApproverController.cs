using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bkav.eGovCloud.Business;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Document;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Models;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class ApproverController : CustomerBaseController
    {
        private readonly ApproverBll _approverService;
        private readonly DocumentCopyBll _docCopyService;
        private readonly DocumentPermissionHelper _documentPermissionHelper;
        private readonly DocumentBll _documentService;
        private readonly ResourceBll _resourceService;
        private readonly UserBll _userService;

        public ApproverController(
            ApproverBll approverService,
            DocumentCopyBll docCopyService,
            DocumentPermissionHelper documentPermissionHelper,
            DocumentBll documentService,
            ResourceBll resourceService, UserBll userService)
        {
            _approverService = approverService;
            _docCopyService = docCopyService;
            _documentPermissionHelper = documentPermissionHelper;
            _documentService = documentService;
            _resourceService = resourceService;
            _userService = userService;
        }

        public JsonResult Index(int docCopyId)
        {
            // CuongNT@bkav.com - 210613: Ủy quyền xử lý.
            int userSendId;
            var docCopy = _docCopyService.Get(docCopyId);
            if (!_documentPermissionHelper.CheckForUyQuyenVaXuLyVanBan(docCopy, CurrentUserId(), out userSendId))
            {
                LogException("Kí duyệt văn bản không có quyền xử lý.");
                return Json(new { error = "Không có quyền phê duyệt hồ sơ!" }, JsonRequestBehavior.AllowGet);
            }

            var approvers = _approverService.Gets(docCopyId).ToList();

            // Lấy danh sách kí duyệt của cán bộ khác
            ViewBag.SignedCollection = approvers.Where(a => a.UserSendId != userSendId).ToListModel();

            // Nội dung kí duyệt của người đăng nhập
            var model = approvers.SingleOrDefault(a => a.UserSendId == userSendId).ToModel() ?? new ApproverModel
            {
                DocumentCopyId = docCopyId,
                DocumentId = docCopy.DocumentId,
                IsDraft = true
            };

            // Cho phép sửa lại ý kiến đã ký
            if (docCopy.UserCurrentId == CurrentUserId())
            {
                model.IsDraft = true;
            }

            model.IsDocSuccess = docCopy.Document.IsSuccess != null && docCopy.Document.IsSuccess.Value;

            return Json(new { isDraft = model.IsDraft, data = RenderRazorViewToString("_Index", model) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
		// [ValidateAntiForgeryToken(Salt = "ApproverUpdateSign")]
		[ValidateAntiForgeryToken]

		public JsonResult UpdateSign(string model)
        {
            try
            {
                var dateProcess = DateTime.Now;
                var approverModel = Json2.ParseAs<ApproverModel>(model);
                int userSendId;
                var docCopy = _docCopyService.Get(approverModel.DocumentCopyId);
                if (!_documentPermissionHelper.CheckForUyQuyenVaXuLyVanBan(docCopy, CurrentUserId(), out userSendId))
                {
                    LogException("Kí duyệt văn bản không có quyền xử lý.");
                    return Json(new { error = "Không có quyền phê duyệt hồ sơ!" }, JsonRequestBehavior.AllowGet);
                }

                approverModel.UserSendId = userSendId;
                if (approverModel.ApproverId == 0)
                {
                    var approver = approverModel.ToEntity();
                    approver.DateCreated = dateProcess;
                    _approverService.Create(approver);
                }
                else
                {
                    var approver = _approverService.Get(approverModel.ApproverId);
                    approver = approverModel.ToEntity(approver);
                    _approverService.Update(approver);
                }

                // Cập nhật kết quả xử lý cuối nếu node hiện tại có quyền cập nhật kết quả cuối
                var documentPermissions = _documentPermissionHelper.CheckAll(docCopy.Document, docCopy, userSendId);
                var checkQuyenCapNhatKetQuaXuLyCuoi = EnumHelper<DocumentPermissions>.ContainFlags(documentPermissions, DocumentPermissions.CapNhatKetQuaXuLyCuoi);
                if (checkQuyenCapNhatKetQuaXuLyCuoi)
                {
                    _documentService.UpdateForSigning(approverModel.DocumentId, userSendId, approverModel.IsSuccess, approverModel.Content, dateProcess);
                }
                return Json(new { success = "Phê duyệt thành công." });
            }
            catch
            {
                return Json(new { error = "Lỗi sảy ra khi thực hiện phê duyệt hồ sơ. Vui lòng thử lại!" });
            }
        }

        public JsonResult DeleteApprover(int appId)
        {
            try
            {
                var app = _approverService.Get(appId);
                if (app == null)
                {
                    return Json(new { error = _resourceService.GetResource("Document.DeleteApprover.Error") }, JsonRequestBehavior.AllowGet);
                }
                var doc = _documentService.Get(app.DocumentId);
                _approverService.Delete(app);
                var approverInDoc = _approverService.Gets(doc.DocumentId);
                if (approverInDoc.Any())
                {
                    var newApprover = approverInDoc.FirstOrDefault();
                    doc.IsSuccess = newApprover.IsSuccess;
                    doc.UserSuccessId = newApprover.UserSendId;
                    doc.UserSuccessName = newApprover.FullName;
                    doc.SuccessNote = newApprover.FullName;
                }
                else
                {
                    doc.IsSuccess = null;
                    doc.UserSuccessId = null;
                    doc.UserSuccessName = "";
                    doc.SuccessNote = null;
                }
                _documentService.Update(doc);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { error = _resourceService.GetResource("Document.DeleteApprover.Error") }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        // [ValidateAntiForgeryToken(Salt = "ApproverSend")]
		[ValidateAntiForgeryToken]
		public JsonResult Send(string docCopyIds, bool isSuccess)
        {
            try
            {
                int userSendId;
                var docCopies = _docCopyService.Gets(Json2.ParseAs<List<int>>(docCopyIds), true);
                foreach (var doc in docCopies)
                {
                    if (!_documentPermissionHelper.CheckForUyQuyenVaXuLyVanBan(doc, CurrentUserId(), out userSendId))
                    {
                        LogException("Kí duyệt văn bản không có quyền xử lý.");
                        return Json(new { error = "Không có quyền phê duyệt hồ sơ!" }, JsonRequestBehavior.AllowGet);
                    }
                    var approver = _approverService.Get(doc.DocumentCopyId, CurrentUserId()) ??
                               new Approver { Content = string.Empty };
                    approver.UserName = User.GetUserName();
                    approver.DateCreated = DateTime.Now;
                    approver.DocumentCopyId = doc.DocumentCopyId;
                    approver.IsSuccess = isSuccess;
                    approver.UserSendId = userSendId;
                    approver.FullName = User.GetFullName();
                    approver.DocumentId = doc.DocumentId;

                    if (approver.ApproverId == 0)
                    {
                        _approverService.Create(approver);
                    }
                    else
                    {
                        _approverService.Update(approver);
                    }

                    // Cập nhật kết quả xử lý cuối nếu node hiện tại có quyền cập nhật kết quả cuối
                    var documentPermissions = _documentPermissionHelper.CheckAll(doc.Document, doc, userSendId);
                    var checkQuyenCapNhatKetQuaXuLyCuoi = EnumHelper<DocumentPermissions>.ContainFlags(documentPermissions, DocumentPermissions.CapNhatKetQuaXuLyCuoi);
                    if (checkQuyenCapNhatKetQuaXuLyCuoi)
                    {
                        _documentService.UpdateForSigning(approver.DocumentId, userSendId, approver.IsSuccess, approver.Content, DateTime.Now);
                    }
                    CreateActivityLog(ActivityLogType.KyDuyet, string.Format("{0} hủy văn bản: {1}", User.GetUserNameWithDomain(), doc.Document.Compendium));
                }

                return Json(new { success = "Phê duyệt thành công." });
            }
            catch
            {
                return Json(new { error = "Lỗi sảy ra khi thực hiện phê duyệt hồ sơ. Vui lòng thử lại!" });
            }
        }
        
        private int CurrentUserId()
        {
            return _userService.CurrentUser.UserId;
        }
    }
}