#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bkav.eGovCloud.Business;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Document;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Models;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovCloud.Business.Utils;

#endregion

namespace Bkav.eGovCloud.Controllers
{
    //[RequireHttps]
    public class ReturnController : CustomerBaseController
    {
        #region Readonly & Static Fields

        private readonly ApproverBll _approverService;
        private readonly AttachmentBll _attachmentService;
        private readonly DocumentCopyBll _docCopyService;
        private readonly DocumentPermissionHelper _documentPermissionHelper;
        private readonly DocumentBll _documentService;
        private readonly PaperBll _paperService;
        private readonly FeeBll _feeService;
        private readonly TemplateBll _templateService;
        private readonly UserBll _userService;
        private readonly SendEmailHelper _mailHelper;

        #endregion

        #region C'tors

        public ReturnController(
            DocumentCopyBll docCopyService,
            PaperBll paperService,
            ApproverBll approverService,
            DocumentPermissionHelper documentPermissionHelper,
            DocumentBll documentService,
            FeeBll feeService,
            AttachmentBll attachmentService,
            SendEmailHelper mailHelper,
            TemplateBll templateService,
            UserBll userService)
        {
            _docCopyService = docCopyService;
            _paperService = paperService;
            _approverService = approverService;
            _documentService = documentService;
            _documentPermissionHelper = documentPermissionHelper;
            _feeService = feeService;
            _templateService = templateService;
            _userService = userService;
            _mailHelper = mailHelper;
            _attachmentService = attachmentService;
        }

        #endregion

        #region Instance Methods

        //
        // GET: /Return/
        public ActionResult Index(int docCopyId)
        {
            var docCopy = _docCopyService.Get(docCopyId);
            if (docCopy == null)
            {
                return PartialView("Index", new DocumentModel());
            }

            // CuongNT@bkav.com - 210613: Ủy quyền xử lý
            int userSendId;
            if (!_documentPermissionHelper.CheckForUyQuyenVaXuLyVanBan(docCopy, CurrentUserId(), out userSendId))
            {
                return PartialView("Index", new DocumentModel());
            }

            ViewBag.PaperReturns = _paperService.Gets(docCopy.Document.DocTypeId.Value, PaperType.TraCongDan);
            ViewBag.Approvers = _approverService.Gets(docCopyId);
            ViewBag.DocumentPermissions = _documentPermissionHelper.CheckAll(docCopy.Document, docCopy, userSendId);
            ViewBag.DocumentCopyId = docCopyId;

            return PartialView("Index", docCopy.Document.ToModel());
        }

        public JsonResult GetReturnResult(int documentCopyId)
        {
            var documentCopy = _docCopyService.Get(documentCopyId);

            if (documentCopy == null)
            {
                LogException("Trả kết quả: văn bản không tồn tại theo documentCopyId = " + documentCopyId);
                return Json(new { error = true }, JsonRequestBehavior.AllowGet);
            }

            var userSendId = CurrentUserId();
            if (!_documentPermissionHelper.CheckForQuyenXuly(documentCopy, userSendId) ||
               _documentPermissionHelper.Check(documentCopy, userSendId, DocumentPermissions.TraKetQua) != DocumentPermissions.TraKetQua)
            {
                LogException("Trả kết quả: Không có quyền trả kết quả");
                return Json(new { error = true }, JsonRequestBehavior.AllowGet);
            }

            var result = new
            {
                Papers = _paperService.Gets(documentCopy.Document.DocTypeId.Value, PaperType.TraCongDan),
                Fee = _feeService.Gets(documentCopy.Document.DocTypeId.Value, FeeType.TraCongDan),

                // Sửa lại, mặc định cho phép kết thúc khi trả kết quả
                hasFinish = true, // _documentPermissionHelper.Check(documentCopy, userSendId, DocumentPermissions.KetThucXuLy) == DocumentPermissions.KetThucXuLy,
                IsSuccess = documentCopy.Document.IsSuccess,
                PrintTemplates = _templateService.Gets(Entities.Enum.DocumentProcessType.TraKetQua)
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateReturn(int documentCopyId, string note, bool isFinish, string fees, string papers)
        {
            var documentCopy = _docCopyService.Get(documentCopyId);

            if (documentCopy == null)
            {
                LogException("Trả kết quả: văn bản không tồn tại theo documentCopyId = " + documentCopyId);
                return Json(new { error = true }, JsonRequestBehavior.AllowGet);
            }

            var userSendId = CurrentUserId();
            if (!_documentPermissionHelper.CheckForQuyenXuly(documentCopy, userSendId) ||
               _documentPermissionHelper.Check(documentCopy, userSendId, DocumentPermissions.TraKetQua) != DocumentPermissions.TraKetQua)
            {
                LogException("Trả kết quả: Không có quyền trả kết quả");
                return Json(new { error = true }, JsonRequestBehavior.AllowGet);
            }
            var document = documentCopy.Document;
            try
            {
                var docPapers = Json2.ParseAs<List<DocPaper>>(papers);
                var docFees = Json2.ParseAs<List<DocFee>>(fees);
                document.DocPapers = docPapers;
                document.DocFees = docFees;

                _documentService.UpdateForReturning(document, documentCopy, userSendId, note);

                // Nếu trả kết quả và kết thúc xử lý luôn
                if (isFinish)
                {
                    var dateCreated = DateTime.Now;
                    //HopCV :170915
                    //Sửa nội dung khi kết thúc văn bản
                    var contentFinish = string.Format("{0} kết thúc văn bản vào lúc :{1}", User.GetFullName(), dateCreated.ToString("dd/MM/yyyy HH:mm:ss"));
                    _docCopyService.Finish(documentCopy, dateCreated, userSendId, contentFinish);
                }

                if (string.IsNullOrEmpty(document.Email))
                {
                    return Json(new { isFinish = isFinish, papers = docPapers, fees = docFees }, JsonRequestBehavior.AllowGet);
                }
                // Gửi mail trả kết quả
                var attachments = _attachmentService.Gets(document.DocumentId);
                if (attachments == null)
                {
                    attachments = new List<Attachment>();
                }
                var userReveices = new List<string>()
                {
                    document.Email
                };
                _mailHelper.SendReturnResult(document, userReveices, attachments.Select(a => a.AttachmentId).ToList(), documentCopy.DocumentCopyId, attachments.Select(a => a.AttachmentName).ToList());

                return Json(new { isFinish = isFinish, papers = docPapers, fees = docFees }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        private int CurrentUserId()
        {
            return _userService.CurrentUser.UserId;
        }

        #endregion
    }
}