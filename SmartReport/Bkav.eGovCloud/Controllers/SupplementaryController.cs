#region

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Transactions;
using System.Web.Mvc;
using Bkav.eGovCloud.Business;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Business.Utils;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Core.Workflow;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Entities.Enum;
using Bkav.eGovCloud.Models;
using Bkav.eGovCloud.Web.Framework;
using FeeType = Bkav.eGovCloud.Entities.FeeType;

#endregion

namespace Bkav.eGovCloud.Controllers
{
    //[RequireHttps]
    public class SupplementaryController : CustomerBaseController
    {
        #region Readonly & Static Fields

        private readonly CommentBll _commentService;
        private readonly DocumentCopyBll _docCopyService;
        private readonly DocumentBll _documentService;
        private readonly FeeBll _feeService;
        private readonly PaperBll _paperService;
        private readonly SupplementaryBll _supplementaryService;
        private readonly UserBll _userService;
        private readonly WorktimeHelper _workTimeHelper;
        private readonly DocumentPermissionHelper _documentPermissionHelper;
        private readonly TemplateBll _templateService;
        private readonly DailyProcessBll _dailyProcessService;
        private readonly TemplateHelper _templateHelper;
        private readonly ResourceBll _resourceService;
        private readonly SendSmsHelper _smsService;
        private readonly SendEmailHelper _mailService;

        #endregion

        #region C'tors

        public SupplementaryController(
            SupplementaryBll supplementaryService,
            PaperBll paperService,
            FeeBll feeService,
            DocumentCopyBll docCopyService,
            WorktimeHelper worktimeHelper,
            CommentBll commentService,
            DocumentPermissionHelper documentPermissionHelper,
            DailyProcessBll dailyProcessService,
            DocumentBll documentService,
            UserBll userService,
            TemplateBll templateService,
            ResourceBll resourceService,
            TemplateHelper templateHelper,
            SendSmsHelper smsService,
            SendEmailHelper mailService)
        {
            _supplementaryService = supplementaryService;
            _paperService = paperService;
            _feeService = feeService;
            _docCopyService = docCopyService;
            _workTimeHelper = worktimeHelper;
            _commentService = commentService;
            _documentPermissionHelper = documentPermissionHelper;
            _dailyProcessService = dailyProcessService;
            _documentService = documentService;
            _userService = userService;
            _templateService = templateService;
            _resourceService = resourceService;
            _templateHelper = templateHelper;
            _smsService = smsService;
            _mailService = mailService;
        }

        #endregion

        #region Instance Methods

        #region Yêu cầu bổ sung

        /// <summary>
        /// Trả về dữ liệu cho form tiếp nhận bổ sung
        /// </summary>
        /// <param name="supplementaryId"></param>
        /// <returns></returns>
        public JsonResult GetDetails(int supplementaryId)
        {
            var supp = _supplementaryService.Get(supplementaryId, true);

            if (supp == null)
            {
                LogException("Yêu cầu bổ sung không tồn tại " + supplementaryId);
                return Json(new { error = true }, JsonRequestBehavior.AllowGet);
            }

            var docCopy = _docCopyService.Get(supp.DocumentCopyId.Value);
            var doc = docCopy.Document;
            var doctypeId = doc.DocTypeId.Value;

            var model = supp.ToModel();

            var papers = Json2.ParseAs<IEnumerable<Paper>>(supp.Papers);
            var fees = Json2.ParseAs<IEnumerable<Fee>>(supp.Fees);
            
            IEnumerable<Template> templates = null;
            var hasReceiveSupplementaryPermission = EnumHelper<NodePermissions>.ContainFlags((NodePermissions)docCopy.NodeCurrentPermission, NodePermissions.QuyenTiepNhanBoSung);
            if (hasReceiveSupplementaryPermission)
            {
                model.NewDateAppointed = GetDateAppointSupplementary(doc.DateCreated, model.DateSend,
                    doc.DateAppointed.Value, (SupplementType)model.SupplementType, doc.ExpireProcess ?? 1, supp.OffsetDay).ToString("hh:mm dd/MM/yyyy");
            }

            var isNotReceived = !supp.UserReceivedId.HasValue;

            templates = _templateService.Gets(DocumentProcessType.TiepNhanBoSung);

            return Json(new
            {
                Supplementary = model,
                HasReceiveSupplementary = hasReceiveSupplementaryPermission,
                Papers = papers,
                Fee = fees,
                PrintTemplates = templates
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Trả về dữ liệu cho form yêu cầu bổ sung
        /// </summary>
        /// <param name="docCopyId"></param>
        /// <returns></returns>
        public JsonResult CreateSupplementary(int docCopyId)
        {
            var docCopy = _docCopyService.Get(docCopyId);
            int userSendId;
            if (docCopy == null ||
                !_documentPermissionHelper.CheckForUyQuyenVaXuLyVanBan(docCopy, CurrentUserId(), out userSendId) ||
                !docCopy.IsHuongXuLyChinh()) // Tạm thời đang để chỉ hướng xử lý chính mới có quyền gửi yêu cầu bổ sung (xem hàm check quyền yêu cầu bổ sung trong DocumentPermissionHelper)
            {
                LogException("Yêu cầu bổ sung không có quyền xử lý.");
                return Json(new { error = "Không có quyền yêu cầu bổ sung hồ sơ!" }, JsonRequestBehavior.AllowGet);
            }

            var doc = docCopy.Document;
            var doctypeId = doc.DocTypeId.Value;

            var papers = _paperService.Gets(doctypeId, PaperType.ThuongBosung);
            var fees = _feeService.Gets(doctypeId, FeeType.ThuongBosung);
            var templates = _supplementaryService.GetRequireds(doctypeId, userSendId);

            return Json(new
            {
                HasReceiveSupplementary = false,
                Papers = papers,
                Fee = fees,
                RequireTemplates = templates
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gửi yêu cầu bổ sung
        /// </summary>
        /// <param name="supplementaryId"></param>
        /// <param name="docCopyId"></param>
        /// <param name="comment"></param>
        /// <param name="detailId"></param>
        /// <param name="supplementType"></param>
        /// <param name="offsetDay"></param>
        /// <param name="papers"></param>
        /// <param name="fees"></param>
        /// <returns></returns>
        [HttpPost]
        // [ValidateAntiForgeryToken(Salt = "SupplementarySendRequire")]
		[ValidateAntiForgeryToken]
		public JsonResult SendRequire(int supplementaryId, int docCopyId, string comment, int detailId, int supplementType, int offsetDay, string papers, string fees)
        {
            var docCopy = _docCopyService.Get(docCopyId);

            int userSendId;
            if (docCopy == null ||
                !_documentPermissionHelper.CheckForUyQuyenVaXuLyVanBan(docCopy, CurrentUserId(), out userSendId) ||
                !docCopy.IsHuongXuLyChinh()) // Tạm thời đang để chỉ hướng xử lý chính mới có quyền gửi yêu cầu bổ sung (xem hàm check quyền yêu cầu bổ sung trong DocumentPermissionHelper)
            {
                LogException("Yêu cầu bổ sung không có quyền xử lý.");
                return Json(new { error = "Không có quyền yêu cầu bổ sung hồ sơ!" }, JsonRequestBehavior.AllowGet);
            }

            if (string.IsNullOrEmpty(comment))
            {
                var isRemoved = false;
                // Hủy yêu cầu bổ sung
                if (detailId > 0)
                {
                    isRemoved = _supplementaryService.CancelRequire(detailId);
                }
                return Json(new { success = true, isRemoved = isRemoved }, JsonRequestBehavior.AllowGet);
            }

            var supplementary = _supplementaryService.GetNotReceived(docCopy.DocumentId);
            var docPapers = Json2.ParseAs<List<DocPaper>>(papers);
            var docFees = Json2.ParseAs<List<DocFee>>(fees);
            if (supplementary == null)
            {
                supplementary = new Supplementary()
                {
                    DocumentCopyId = docCopyId,
                    DocumentCopyIds = string.Format(";{0};", docCopyId),
                    DateSend = DateTime.Now,
                    DocumentId = docCopy.Document.DocumentId,
                    OffsetDay = offsetDay,
                    SupplementType = supplementType,
                    UserSendId = _userService.CurrentUser.UserId,
                    UserSendName = _userService.CurrentUser.Username,
                    OldDateAppointed = docCopy.Document.DateAppointed,
                    CommentSend = comment
                };
                _supplementaryService.Create(supplementary);
            }

            supplementary.OffsetDay = offsetDay;
            supplementary.SupplementType = supplementType;
            supplementary.Papers = papers;
            supplementary.Fees = fees;
            _supplementaryService.Update(supplementary, docPapers, docFees, detailId, comment, docCopy.DocTypeId, _userService.CurrentUser.ToUser());

            return Json(new { success = true, supplementary = supplementary }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///   <para> Tiếp nhận bổ sung </para>
        /// </summary>
        /// <param name="model"> Supplementary model (json) </param>
        /// <param name="papers"> Danh sách các giấy tờ thêm </param>
        /// <param name="fees"> Danh sách các lệ phí thêm </param>
        /// <returns> </returns>
        [HttpPost]
        public JsonResult Receive(int suppId, string comment, bool isSuccess, string papers, string fees)
        {
            var dateCreated = DateTime.Now;
            var supplementary = _supplementaryService.Get(suppId);

            // CuongNT@bkav.com - 210613: Ủy quyền xử lý.
            int userSendId;
            if (!_documentPermissionHelper.CheckForUyQuyenVaXuLyVanBan(supplementary.DocumentCopyId.Value, CurrentUserId(), out userSendId))
            {
                return Json(new { error = "Yêu cầu này đã được cập nhật kết quả" }, JsonRequestBehavior.AllowGet);
            }

            if (supplementary.UserReceivedId.HasValue && supplementary.UserReceivedId.Value != userSendId)
            {
                return Json(new { error = "Yêu cầu này đã được cập nhật kết quả bởi" + supplementary.UserReceiveName }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                using (var trans = new TransactionScope())
                {
                    var doc = _documentService.Get(supplementary.DocumentId);
                    var expireProcess = doc.ExpireProcess ?? _workTimeHelper.GetWorkdays(doc.DateCreated, doc.DateAppointed.Value);
                    var newDateAppointed = GetDateAppointSupplementary(doc.DateCreated, supplementary.DateSend,
                                                        doc.DateAppointed.Value, (SupplementType)supplementary.SupplementType,
                                                        expireProcess, supplementary.OffsetDay);

                    supplementary.UserReceivedId = userSendId;
                    supplementary.UserReceiveName = User.GetUserName();
                    supplementary.DateReceived = dateCreated;
                    supplementary.CommentReceived = comment;
                    supplementary.DateBeginProcess = dateCreated;
                    supplementary.IsSuccess = isSuccess;
                    supplementary.OldDateAppointed = doc.DateAppointed;
                    supplementary.NewDateAppointed = newDateAppointed;
                    supplementary.IsReceived = true;

                    var docPapers = Json2.ParseAs<List<DocPaper>>(papers);
                    var docFees = Json2.ParseAs<List<DocFee>>(fees);

                    _supplementaryService.Receive(supplementary, docPapers, docFees, dateCreated, userSendId);

                    #region Cập nhật document

                    doc.IsSupplemented = null; // supplementary.IsSuccess;
                    doc.Status = (int)DocumentStatus.DangXuLy;
                    doc.DateRequireSupplementary = null;
                    doc.DateAppointed = newDateAppointed;

                    var documentCopy = _docCopyService.Get(supplementary.DocumentCopyId.Value);
                    documentCopy.Status = (int)DocumentStatus.DangXuLy;

                    _documentService.Update(doc);

                    #endregion

                    var userSend = _userService.CurrentUser.ToUser();
                    try
                    {
                        _smsService.SendCompleteSupplementary(documentCopy.Document, supplementary, userSend, documentCopy.DocumentCopyId);
                    }
                    catch (Exception ex)
                    {
                        LogException(ex);
                    }

                    try
                    {
                        _mailService.SendCompleteSupplementary(documentCopy.Document, supplementary, userSend, documentCopy.DocumentCopyId);
                    }
                    catch (Exception ex)
                    {
                        LogException(ex);
                    }

                    trans.Complete();

                    return Json(new { supplementary = supplementary }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
                return Json(new { error = "Xảy ra lỗi khi thêm tiếp nhận bổ sung" }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Hủy yêu cầu bổ sung

        [HttpPost]
        public JsonResult CancelReceive(int suppId)
        {
            var dateCreated = DateTime.Now;
            var supplementary = _supplementaryService.Get(suppId);

            // CuongNT@bkav.com - 210613: Ủy quyền xử lý.
            int userSendId;
            if (!_documentPermissionHelper.CheckForUyQuyenVaXuLyVanBan(supplementary.DocumentCopyId.Value, CurrentUserId(), out userSendId))
            {
                return Json(new { error = "YBạn không có quyền hủy yêu cầu" }, JsonRequestBehavior.AllowGet);
            }

            if (supplementary.UserReceivedId.HasValue && supplementary.UserReceivedId.Value != userSendId)
            {
                return Json(new { error = "Bạn không có quyền hủy yêu cầu" }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                using (var trans = new TransactionScope())
                {
                    _supplementaryService.Cancel(supplementary);
                    trans.Complete();

                    return Json(new { success = true, supplementary = supplementary }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(new { error = "Xảy ra lỗi khi thêm tiếp nhận bổ sung" }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Tiếp tục xử lý

        /// <summary>
        /// Tiếp tục xử lý
        /// </summary>
        /// <param name="documentCopyId">Id document copy</param>
        /// <param name="comment">Nội dung cập nhật</param>
        /// <param name="dateAppointed">Ngày hẹn trả mới</param>
        /// <returns></returns>
        public JsonResult ContinueProcess(int documentCopyId, string comment, DateTime dateAppointed)
        {
            // CuongNT@bkav.com - 210613: Ủy quyền xử lý.
            int userSendId;
            if (!_documentPermissionHelper.CheckForUyQuyenVaXuLyVanBan(documentCopyId, CurrentUserId(), out userSendId))
            {
                return Json(new { error = "Yêu cầu này đã được cập nhật kết quả" }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                comment = User.GetUserName() + ": " + comment;
                _docCopyService.ContinueProcess(documentCopyId, comment, dateAppointed);

                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region In biên nhận bổ sung

        //public string GetPrintContent(int templateId, int documentCopyId)
        //{
        //    var result = string.Empty;
        //    var documentCopy = _docCopyService.Get(documentCopyId);
        //    var template = _templateService.Get(templateId);
        //    result = _templateHelper.ParseContentNew(template, CurrentUserId(), documentCopy.Document.DocumentId, null, null, null);
        //    return result;
        //}

        #endregion

        /// <summary>
        ///   <para> Tính lại ngày hẹn trả khi tiếp nhận bổ sung </para>
        ///   <para> (Tienbv@bkav.com - 270213) </para>
        /// </summary>
        /// <param name="type"> Cách tính lại ngày hẹn trả </param>
        /// <param name="docCopyId"> </param>
        /// <param name="range"> </param>
        /// <returns> </returns>
        public JsonResult GetDateAppointed(int type, int docCopyId, int range)
        {
            try
            {
                var docCopy = _docCopyService.Get(docCopyId);
                var dateCreated = docCopy.Document.DateCreated;
                var dateAppointed = docCopy.Document.DateAppointed;
                if (dateAppointed == null)
                {
                    return Json(new { error = "Không có ngày hẹn trả" }, JsonRequestBehavior.AllowGet);
                }
                var supp = _supplementaryService.GetByDocCopy(docCopyId);
                if (supp == null)
                {
                    return Json(new { error = "Hồ sơ không tồn tại" }, JsonRequestBehavior.AllowGet);
                }
                var suppCreated = Convert.ToDateTime(supp.DateSend); // nếu đã tồn tại supp thì dateSend != null

                var doc = docCopy.Document;
                var expireProcess = doc.ExpireProcess ?? _workTimeHelper.GetWorkdays(doc.DateCreated, doc.DateAppointed.Value);
                var result = GetDateAppointSupplementary(dateCreated, suppCreated, Convert.ToDateTime(dateAppointed), (SupplementType)type,
                                        expireProcess, range);

                return Json(result.ToString("hh:mm dd/MM/yyyy"), JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { error = "Lỗi" }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Trả về ngày hẹn trả mới tương ứng với loại yêu cầu bổ sung.
        /// <para>(Tienbv@bkav.com 090313)</para>
        /// </summary>
        /// <param name="docDateCreated">Ngày tiếp nhận hs</param>
        /// <param name="suppDateCreated">Ngày yêu cầu tiếp nhận bổ sung</param>
        /// <param name="docDateAppoint">Ngày hẹn trả cũ</param>
        /// <param name="suppType">Loại bổ sung</param>
        /// <param name="docExprire">Thời hạn xử lý</param>
        /// <param name="dayOffset">Số ngày cộng thêm</param>
        /// <returns>Ngày hẹn trả mới.</returns>
        private DateTime GetDateAppointSupplementary(DateTime docDateCreated, DateTime suppDateCreated, DateTime docDateAppoint,
            SupplementType suppType, int docExprire, int dayOffset = 0)
        {
            var startTime = DateTime.Now;
            int workDays;
            switch (suppType)
            {
                case SupplementType.FixedDays:
                    workDays = dayOffset;
                    break;
                case SupplementType.Continue:
                    if (suppDateCreated > docDateAppoint)
                    {
                        workDays = 1;
                        startTime = docDateAppoint.AddDays(-1);
                    }
                    else
                    {
                        workDays = _workTimeHelper.GetWorkdays(suppDateCreated, docDateAppoint);
                    }
                    break;
                case SupplementType.Reset:
                    workDays = docExprire;
                    break;
                default:
                    workDays = 1;
                    break;
            }

            return _workTimeHelper.GetDateAppoint(startTime, workDays).Value;
        }

        private int CurrentUserId()
        {
            return _userService.CurrentUser.UserId;
        }
        #endregion
    }
}