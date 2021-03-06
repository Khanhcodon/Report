using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Transactions;
using System.Web.Mvc;
using Bkav.eGovCloud.Business;
using Bkav.eGovCloud.Business.Caching;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Business.Utils;
using Bkav.eGovCloud.Core.Document;
using Bkav.eGovCloud.Core.History;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Models;
using Bkav.eGovCloud.Web.Framework;
using Newtonsoft.Json;
using System.Configuration;
using System.Dynamic;
using Renci.SshNet;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Core.Logging;
using Bkav.eGovCloud.Entities.Customer.Settings;

namespace Bkav.eGovCloud.Controllers
{
    /// <summary>
    /// Lớp xử lý lưu sổ phát hành, liên thông
    /// </summary>
    public class PublishController : CustomerBaseController
    {
        private readonly CodeBll _codeService;
        private readonly UserBll _userService;
        private readonly AddressBll _addressService;
        private readonly DocumentPublishBll _documentPublishService;
        private readonly DocumentPublishPlusBll _documentPublishPlusService;
        private readonly DocumentsCache _documentCache;
        private readonly CommentBll _commentService;
        private readonly DocumentCopyBll _docCopyService;
        private readonly AttachmentBll _attachmentService;
        private readonly EdocBll _edocService;
        private readonly AnticipateBll _anticipateService;
        private readonly DocumentBll _documentService;
        private readonly DocTypeBll _doctypeService;
        private readonly DocTypeFormBll _doctypeFormService;
        private readonly FormBll _formService;
        private readonly DepartmentBll _departmentService;
        private AdminGeneralSettings _generalSettings;
        private ReportConfigSettings _reportConfigSettings;

        private readonly DocumentHelper _documentHelper;
        private readonly SendEmailHelper _mailHelper;
        private readonly DocumentPermissionHelper _documentPermissionHelper;

        public PublishController(CodeBll codeService, UserBll userService, AddressBll addressService, DocumentPublishBll documentPublishService, DocumentPublishPlusBll documentPublishPlusService,
                                DocumentHelper documentHelper, DocumentsCache documentCache, CommentBll commentService, DocumentCopyBll docCopyService,
                                DocumentPermissionHelper documentPermissionHelper, AttachmentBll attachmentService, EdocBll edocService,
                                AnticipateBll anticipateService, DocumentBll documentService, SendEmailHelper mailHelper
            , DocTypeBll doctypeService, DocTypeFormBll doctypeFormService, FormBll formService, DepartmentBll departmentService, AdminGeneralSettings generalSettings, ReportConfigSettings reportConfigSettings)
        {
            _codeService = codeService;
            _userService = userService;
            _addressService = addressService;
            _documentPublishService = documentPublishService;
            _documentPublishPlusService = documentPublishPlusService;
            _documentHelper = documentHelper;
            _documentCache = documentCache;
            _commentService = commentService;
            _docCopyService = docCopyService;
            _documentPermissionHelper = documentPermissionHelper;
            _attachmentService = attachmentService;
            _edocService = edocService;
            _anticipateService = anticipateService;
            _documentService = documentService;
            _mailHelper = mailHelper;
            _doctypeService = doctypeService;
            _formService = formService;
            _doctypeFormService = doctypeFormService;
            _departmentService = departmentService;
            _generalSettings = generalSettings;
            _reportConfigSettings = reportConfigSettings;
        }

        public string TestLgsp()
        {
            List<object> excelData = null;
            using (Stream str = System.IO.File.Open(@"C:\Users\tienbv\Downloads\Test1.xlsx", FileMode.Open))
            {
                var xlsxParser = new XlsxToJson(str);
                excelData = xlsxParser.ConvertXlsxToJson(1, 2, 1, 1);
            }

            if (excelData == null) return "Fail";

            var result = new Dictionary<string, dynamic>();
            result.Add("data", new LgspDataReport()
            {
                don_vi = "Test",
                dia_chi_noi_gui = "Test",
                ky_bao_cao = "Test",
                linh_vuc = "Test",
                loai_bao_cao = "Test",
                loai_ky_bao_cao = 1,
                ma_don_vi = "Test",
                ma_linh_vuc = "Test",
                ten_bao_cao = "Test",
                version = 1
            });

            var rows = new Dictionary<string, dynamic>();
            var idx = 1;
            foreach (var row in excelData)
            {
                rows.Add("row" + idx, row);
                idx++;
            }

            result.Add("row", rows);

            Post("BIReport", result, Guid.Empty);

            return "Ok";
        }

        #region Phát hành báo cáo VP chính phủ
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public JsonResult TransferPublishGov(int documentCopyId, string doc, string files, List<int> removeAttachmentIds, string modifiedFiles, string publishinfo,
            List<int> usersConsult, string userHasReceiveDocuments, string searchAddr, string targetForComments)
        {
            /*
			 Quy trình cập nhật khi Lưu sổ phát hành

				=> Kiểm tra số được cấp đã sử dụng hay chưa => Kiểm tra người duyệt => Confirm số được cấp
				=> Cập nhật Document về dữ liệu mới nhất => Phát hành văn bản => Tạo văn bản đến, thông báo => Gửi Mail (Schedule) => Gửi Thông báo (Schedule)
				=> Cập nhật Cache
			 */

            DocumentCopy documentCopy = null;
            var userSendId = GetUserCreatedId();
            var codeId = 0;

            try
            {
                var model = Json2.ParseAsJs<DocumentModel>(doc);
                var publish = Json2.ParseAsJs<PublishPlusInfoModel>(publishinfo);

                if (string.IsNullOrEmpty(publish.Code))
                {
                    return Json(new { error = "Bạn chưa cấp số. Vui lòng thử lại." });
                }
                // tạm thời chưa check
                //if (string.IsNullOrEmpty(publish.Approvers))
                //{
                //    return Json(new { error = "Bạn chưa chọn người duyệt. Vui lòng thử lại." });
                //}

                using (var trans = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (publish.CodeId == 0 && !string.IsNullOrEmpty(publish.Code))
                    {
                        publish.IsCustomCode = true;
                    }
                    codeId = publish.CodeId;

                    var docCode = ConfirmDocCodeGov(publish, codeId, model.DocumentId, publish.IsCustomCode);
                    publish.Code = docCode;

                    var commentTransfers = Json2.ParseAs<List<CommentTransfer>>(targetForComments);
                    var processInfo = _documentHelper.GetCommentTransferText(commentTransfers);
                    // là phát hành ngoài cơ quan
                    model.IsTransferPublish = true;

                    model = BuildDocumentModelPublishGov(model, publish, commentTransfers, processInfo, usersConsult);

                    var newFiles = Json2.ParseAs<IDictionary<string, IDictionary<string, string>>>(files);
                    var modifiedAttachment = Json2.ParseAs<IDictionary<int, string>>(modifiedFiles);

                    documentCopy = documentCopyId == 0
                                        ? _documentHelper.CreateDocumentDefault(model, userSendId, newFiles)
                                        : _documentHelper.UpdateDocumentDefault(model, newFiles, removeAttachmentIds, modifiedAttachment, userSendId, hasChangeAttachment: true);

                    if (documentCopy == null)
                    {
                        return Json(new { error = "Lỗi trong quá trình phát hành." });
                    }

                    List<string> listMailReceive;
                    PublishGov(documentCopy, publish, searchAddr, userSendId, out listMailReceive);

                    var receivedVbDens = Json2.ParseAs<Dictionary<int, string>>(userHasReceiveDocuments);
                    SentThongBaoAndVbDen(documentCopy, userSendId, receivedVbDens, DateTime.Now);

                    #region Lưu sổ văn bản

                    //if (model.StoreId.HasValue)
                    //{
                    //    SaveStoreDoc(model.StoreId.Value, documentCopy.Document);
                    //}

                    #endregion

                    #region Xử lý cache

                    // update code đã sử dụng
                    _codeService.AddUsedCache(documentCopy.DocumentId, publish.Code, "", model.CategoryBusinessId, model.Organization, model.StoreId);

                    if (documentCopyId != 0)
                    {
                        _documentCache.RemoveAll(documentCopy.DocumentCopyId);
                    }

                    #endregion

                    #region Gửi mail phát hành

                    listMailReceive.AddRange(commentTransfers.Where(m => m.Type.Equals("4") && m.Label.IsEmailAddress()).Select(ad => ad.Label));
                    if (listMailReceive.Any())
                    {
                        SendPublishMails(listMailReceive, documentCopy.Document, documentCopy.DocumentCopyId);
                    }

                    #endregion

                    #region Gửi ý kiến phát hành

                    var comment = new StringBuilder();
                    comment.AppendLine("Phát hành văn bản: " + model.Comments.Content);
                    comment.AppendLine("Số ký hiệu: " + publish.Code);
                    comment.AppendLine(processInfo);
                    _commentService.SendComment(documentCopy.DocumentCopyId, userSendId, comment.ToString(), DateTime.Now, "");

                    #endregion

                    #region Gửi notify thông báo
                    if (usersConsult == null)
                    {
                        usersConsult = new List<int>();
                    }

                    usersConsult.AddRange(documentCopy.UserThamGias());
                    _documentHelper.PushNotifyMessage(usersConsult.Distinct(), documentCopy, model.Compendium, DateTime.Now, isCreatingDocument: documentCopyId == 0);

                    #endregion

                    trans.Complete();
                }
            }
            catch (Exception ex)
            {
                if (documentCopy != null)
                {
                    _codeService.RemoveUsedCache(documentCopy.DocumentId);
                }
                _codeService.DecreaseDocNumber(codeId);

                LogException(ex);
                return Json(new { error = "Lỗi trong quá trình phát hành: " + ex.Message });
            }
            return Json(new { success = true });
        }
        #endregion


        #region Lưu sổ phát hành

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public JsonResult TransferPublish(int documentCopyId, string doc, string files, List<int> removeAttachmentIds, string modifiedFiles, string publishinfo,
            List<int> usersConsult, string userHasReceiveDocuments, string searchAddr, string targetForComments)
        {
            /*
			 Quy trình cập nhật khi Lưu sổ phát hành

				=> Kiểm tra số được cấp đã sử dụng hay chưa => Kiểm tra người duyệt => Confirm số được cấp
				=> Cập nhật Document về dữ liệu mới nhất => Phát hành văn bản => Tạo văn bản đến, thông báo => Gửi Mail (Schedule) => Gửi Thông báo (Schedule)
				=> Cập nhật Cache
			 */

            DocumentCopy documentCopy = null;
            var userSendId = GetUserCreatedId();
            var codeId = 0;

            try
            {
                var model = Json2.ParseAsJs<DocumentModel>(doc);
                var publish = Json2.ParseAsJs<PublishInfoModel>(publishinfo);

                if (string.IsNullOrEmpty(publish.Code))
                {
                    return Json(new { error = "Bạn chưa cấp số. Vui lòng thử lại." });
                }

                if (string.IsNullOrEmpty(publish.Approvers))
                {
                    return Json(new { error = "Bạn chưa chọn người duyệt. Vui lòng thử lại." });
                }

                using (var trans = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (publish.CodeId == 0 && !string.IsNullOrEmpty(publish.Code))
                    {
                        publish.IsCustomCode = true;
                    }
                    codeId = publish.CodeId;

                    var docCode = ConfirmDocCode(publish, codeId, model.DocumentId, publish.IsCustomCode);
                    publish.Code = docCode;

                    var commentTransfers = Json2.ParseAs<List<CommentTransfer>>(targetForComments);
                    var processInfo = _documentHelper.GetCommentTransferText(commentTransfers);
                    // là phát hành ngoài cơ quan
                    model.IsTransferPublish = true;

                    model = BuildDocumentModelPublish(model, publish, commentTransfers, processInfo, usersConsult);

                    var newFiles = Json2.ParseAs<IDictionary<string, IDictionary<string, string>>>(files);
                    var modifiedAttachment = Json2.ParseAs<IDictionary<int, string>>(modifiedFiles);

                    documentCopy = documentCopyId == 0
                                        ? _documentHelper.CreateDocumentDefault(model, userSendId, newFiles)
                                        : _documentHelper.UpdateDocumentDefault(model, newFiles, removeAttachmentIds, modifiedAttachment, userSendId, hasChangeAttachment: true);

                    if (documentCopy == null)
                    {
                        return Json(new { error = "Lỗi trong quá trình phát hành." });
                    }

                    List<string> listMailReceive;
                    Publish(documentCopy, publish, searchAddr, userSendId, out listMailReceive);

                    var receivedVbDens = Json2.ParseAs<Dictionary<int, string>>(userHasReceiveDocuments);
                    SentThongBaoAndVbDen(documentCopy, userSendId, receivedVbDens, DateTime.Now);

                    #region Lưu sổ văn bản

                    if (model.StoreId.HasValue)
                    {
                        SaveStoreDoc(model.StoreId.Value, documentCopy.Document);
                    }

                    #endregion

                    #region Xử lý cache

                    // update code đã sử dụng
                    _codeService.AddUsedCache(documentCopy.DocumentId, publish.Code, "", model.CategoryBusinessId, model.Organization, model.StoreId);

                    if (documentCopyId != 0)
                    {
                        _documentCache.RemoveAll(documentCopy.DocumentCopyId);
                    }

                    #endregion

                    #region Gửi mail phát hành

                    listMailReceive.AddRange(commentTransfers.Where(m => m.Type.Equals("4") && m.Label.IsEmailAddress()).Select(ad => ad.Label));
                    if (listMailReceive.Any())
                    {
                        SendPublishMails(listMailReceive, documentCopy.Document, documentCopy.DocumentCopyId);
                    }

                    #endregion

                    #region Gửi ý kiến phát hành

                    var comment = new StringBuilder();
                    comment.AppendLine("Phát hành văn bản: " + model.Comments.Content ?? string.Empty);
                    comment.AppendLine("Số ký hiệu: " + publish.Code);
                    comment.AppendLine(processInfo);
                    _commentService.SendComment(documentCopy.DocumentCopyId, userSendId, comment.ToString(), DateTime.Now, "");

                    #endregion

                    #region Gửi notify thông báo
                    if (usersConsult == null)
                    {
                        usersConsult = new List<int>();
                    }

                    usersConsult.AddRange(documentCopy.UserThamGias());
                    _documentHelper.PushNotifyMessage(usersConsult.Distinct(), documentCopy, model.Compendium, DateTime.Now, isCreatingDocument: documentCopyId == 0);

                    #endregion

                    trans.Complete();
                }
            }
            catch (Exception ex)
            {
                if (documentCopy != null)
                {
                    _codeService.RemoveUsedCache(documentCopy.DocumentId);
                }
                _codeService.DecreaseDocNumber(codeId);

                LogException(ex);
                return Json(new { error = "Lỗi trong quá trình phát hành: " + ex.Message });
            }
            return Json(new { success = true });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult TransferPublishTheoLo(List<int> documentCopyIds, string publishinfo, List<int> usersConsult,
                    string userHasReceiveDocuments, string searchAddr)
        {
            try
            {
                if (documentCopyIds == null || !documentCopyIds.Any())
                {
                    return Json(new { error = "Chưa có văn bản." });
                }

                var publish = Json2.ParseAsJs<PublishInfoModel>(publishinfo);

                var documentCopys = _docCopyService.Gets(documentCopyIds);

                #region Check điều kiện

                if (documentCopyIds.Count != documentCopys.Count())
                {
                    return Json(new { error = "Có văn bản không tồn tại.Vui lòng thử lại." });
                }

                var user = _userService.CurrentUser;

                //danh sách chứa các giá trị phục vụ cho việc bàn giao văn bản theo lô
                var objectTheoLoList = new Dictionary<int, DocumentCopy>();

                foreach (var documentCopy in documentCopys)
                {
                    int userSendId;
                    if (!_documentPermissionHelper.CheckForUyQuyenVaXuLyVanBan(documentCopy, user.UserId, out userSendId))
                    {
                        LogException("Không có quyền xử lý văn bản");
                        return Json(new { error = "Không có quyền xử lý văn bản" });
                    }

                    if (_documentPermissionHelper.Check(documentCopy, userSendId, DocumentPermissions.PhatHanh) != DocumentPermissions.PhatHanh)
                    {
                        LogException("Không có quyền phát hành văn bản.");
                        return Json(new { error = "Không có quyền phát hành văn bản này." });
                    }

                    objectTheoLoList.Add(userSendId, documentCopy);
                }

                #endregion

                using (var trans = new TransactionScope(TransactionScopeOption.Required))
                {
                    var dateCreated = DateTime.Now;

                    //todo: Chỗ này gán DocCode = null để đánh lại DocCode cho các  văn bản tiếp theo
                    publish.IsCustomCode = false;
                    publish.Code = "";

                    foreach (var item in objectTheoLoList)
                    {
                        var documentCopy = item.Value;
                        var model = documentCopy.Document.ToModel();
                        var userSendId = item.Key;

                        var docCode = ConfirmDocCode(publish, publish.CodeId, model.DocumentId, publish.IsCustomCode);
                        publish.Code = docCode;

                        model.DocumentCopyModel = new DocumentCopyModel()
                        {
                            UserThongBao = DocumentCopy.UserCompareString(usersConsult)
                        };

                        _documentHelper.UpdateDocumentDefault(model, null, null, null, userSendId);

                        List<string> listMailReceive;
                        Publish(documentCopy, publish, searchAddr, userSendId, out listMailReceive);

                        #region Lưu sổ văn bản

                        if (model.StoreId.HasValue)
                        {
                            SaveStoreDoc(model.StoreId.Value, documentCopy.Document);
                        }

                        #endregion

                        var receivedVbDens = Json2.ParseAs<Dictionary<int, string>>(userHasReceiveDocuments);
                        SentThongBaoAndVbDen(documentCopy, userSendId, receivedVbDens, DateTime.Now);
                    }

                    _documentCache.RemoveAll(documentCopyIds);

                    trans.Complete();
                }
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                LogException(ex);
                return Json(new { error = "Lỗi trong quá trình phát hành theo lô." });
            }
        }

        [HttpPost]
        public JsonResult RePublish(int documentCopyId, string addressRepublish,
            string files, List<int> removeAttachmentIds, string modifiedFiles,
            List<int> usersConsult, string userHasReceiveDocuments,
            string searchAddr, string targetForComments)
        {
            // Cho phép thay đổi tệp đính kèm: trường hợp văn thư gửi thiếu file hoặc gửi sai file.
            // Bổ sung vào quyền Phát hành, ko tạo quyền mới.            
            // Cho phép gửi lại cơ quan cũ

            var currentUser = _userService.CurrentUser.ToUser();
            if (currentUser == null)
            {
                return Json(new { error = true, message = "Bạn chưa đăng nhập hệ thống" }, JsonRequestBehavior.AllowGet);
            }

            var docCopy = _docCopyService.Get(documentCopyId);

            if (!_documentPermissionHelper.CheckForQuyenXuly(docCopy, currentUser.UserId))
            {
                return Json(new { error = true, message = "Bạn không có quyền thực hiện phát hành tiếp." }, JsonRequestBehavior.AllowGet);
            }

            var document = docCopy.Document;
            if (!document.IsTransferPublish.HasValue || document.IsTransferPublish.Value == false)
            {
                return Json(new { error = true, message = "Bạn không có quyền thực hiện phát hành tiếp." }, JsonRequestBehavior.AllowGet);
            }

            using (var trans = new TransactionScope(TransactionScopeOption.Required))
            {
                var commentTransfers = Json2.ParseAs<List<CommentTransfer>>(targetForComments);
                var processInfo = _documentHelper.GetCommentTransferText(commentTransfers);
                document.ProcessInfo = processInfo.ToString();

                var newFiles = Json2.ParseAs<IDictionary<string, IDictionary<string, string>>>(files);
                var modifiedAttachment = Json2.ParseAs<IDictionary<int, string>>(modifiedFiles);

                docCopy = UpdateDocumentRePublish(docCopy, newFiles, modifiedAttachment, removeAttachmentIds, usersConsult, currentUser);

                var addressDictionary = Json2.ParseAsJs<Dictionary<int, DateTime?>>(addressRepublish);
                RePublish(docCopy, addressDictionary, currentUser.UserId);

                var receivedVbDens = Json2.ParseAs<Dictionary<int, string>>(userHasReceiveDocuments);
                SentThongBaoAndVbDen(docCopy, _userService.CurrentUser.UserId, receivedVbDens, DateTime.Now);

                _documentCache.RemoveAll(docCopy.DocumentCopyId);

                trans.Complete();
            }

            return Json(new { error = false, message = "Đã phát hành tiếp thành công." }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Lưu sổ phát hành nội bộ

        [HttpPost]
        public JsonResult TransferPrivatePublish(int documentCopyId, string doc, string files, List<int> removeAttachmentIds, string modifiedFiles,
                        string publishinfo, List<int> usersConsult, string userHasReceiveDocuments, string targetForComments)
        {
            var model = Json2.ParseAsJs<DocumentModel>(doc);
            var userSendId = GetUserCreatedId();

            var publish = Json2.ParseAsJs<PublishInfoModel>(publishinfo);
            if (string.IsNullOrEmpty(publish.Code))
            {
                return Json(new { error = "Bạn chưa cấp số. Vui lòng thử lại." });
            }

            var codeId = publish.CodeId;
            DocumentCopy documentCopy = null;
            try
            {
                using (var trans = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (publish.CodeId == 0 && !string.IsNullOrEmpty(publish.Code))
                    {
                        publish.IsCustomCode = true;
                    }

                    var docCode = ConfirmDocCode(publish, publish.CodeId, model.DocumentId, publish.IsCustomCode);
                    publish.Code = docCode;

                    var commentTransfers = Json2.ParseAs<List<CommentTransfer>>(targetForComments);
                    var processInfo = _documentHelper.GetCommentTransferText(commentTransfers);
                    // Là phát hành nội bộ
                    model.IsTransferPublish = false;

                    model = BuildDocumentModelPublish(model, publish, commentTransfers, processInfo, usersConsult);

                    var newFiles = Json2.ParseAs<IDictionary<string, IDictionary<string, string>>>(files);
                    var modifiedAttachment = Json2.ParseAs<IDictionary<int, string>>(modifiedFiles);
                    documentCopy = documentCopyId == 0
                                        ? _documentHelper.CreateDocumentDefault(model, userSendId, newFiles)
                                        : _documentHelper.UpdateDocumentDefault(model, newFiles, removeAttachmentIds, modifiedAttachment, userSendId, hasChangeAttachment: true);

                    if (documentCopy == null)
                    {
                        return Json(new { error = "Phát hành thất bại. Vui lòng thử lại." });
                    }

                    var receivedVbDens = Json2.ParseAs<Dictionary<int, string>>(userHasReceiveDocuments);
                    SentThongBaoAndVbDen(documentCopy, userSendId, receivedVbDens, DateTime.Now);

                    #region Lưu sổ văn bản

                    if (model.StoreId.HasValue)
                    {
                        SaveStoreDoc(model.StoreId.Value, documentCopy.Document);
                    }

                    #endregion

                    #region Gửi ý kiến phát hành

                    var contentAuthorize = userSendId != GetUserCreatedId() ? string.Format("Xử lý ủy quyền:{0} ({1})", User.GetFullName(), User.GetUserName()) : string.Empty;

                    var comment = new StringBuilder();
                    comment.AppendLine("Phát hành nội bộ văn bản: " + model.Comments.Content ?? string.Empty);
                    comment.AppendLine("Số ký hiệu: " + publish.Code);
                    comment.AppendLine(processInfo);
                    _commentService.SendComment(documentCopy.DocumentCopyId, userSendId, comment.ToString(), DateTime.Now, contentAuthorize);

                    #endregion

                    #region Gửi notify thông báo

                    if (usersConsult != null)
                    {
                        _documentHelper.PushNotifyMessage(usersConsult, documentCopy, model.Compendium, DateTime.Now, isCreatingDocument: documentCopyId == 0);
                    }

                    #endregion

                    _documentCache.RemoveAll(documentCopy.DocumentCopyId);
                    trans.Complete();
                }
            }
            catch (Exception ex)
            {
                if (documentCopy != null)
                {
                    _codeService.RemoveUsedCache(documentCopy.DocumentId);
                }
                _codeService.DecreaseDocNumber(codeId);

                LogException(ex);
                return Json(new { error = "Phát hành thất bại: " + ex.Message });
            }
            return Json(new { success = "Thao tác thực hiện thành công." });
        }

        [HttpPost]
        public JsonResult TransferPrivatePublishTheoLo(List<int> documentCopyIds, string publishinfo, List<int> usersConsult,
                        string userHasReceiveDocuments)
        {
            #region Check đầu vào

            if (documentCopyIds == null || !documentCopyIds.Any())
            {
                return Json(new { error = "Chưa có văn bản." });
            }

            var documentCopys = _docCopyService.Gets(documentCopyIds);
            if (documentCopyIds.Count() != documentCopys.Count())
            {
                return Json(new { error = "Có văn bản không tồn tại.Vui lòng thử lại." });
            }

            var user = _userService.CurrentUser;

            var objectTheoLoList = new Dictionary<int, DocumentCopy>();
            foreach (var documentCopy in documentCopys)
            {
                int userSendId;
                //Check quyền xử lý văn bản
                if (!_documentPermissionHelper.CheckForUyQuyenVaXuLyVanBan(documentCopy, user.UserId, out userSendId))
                {
                    LogException("Không có quyền xử lý văn bản");
                    return Json(new { error = "Không có quyền xử lý văn bản" });
                }

                //Check quyền bàn giao văn bản
                if (_documentPermissionHelper.Check(documentCopy, userSendId, DocumentPermissions.LuuSo) != DocumentPermissions.LuuSo)
                {
                    LogException("Không có quyền phát hành văn bản.");
                    return Json(new { error = "Không có quyền phát hành văn bản này." });
                }

                objectTheoLoList.Add(userSendId, documentCopy);
            }

            #endregion

            try
            {
                using (var trans = new TransactionScope(TransactionScopeOption.Required))
                {
                    var publish = Json2.ParseAsJs<PublishInfoModel>(publishinfo);
                    publish.Code = null;
                    publish.IsCustomCode = true;

                    foreach (var item in objectTheoLoList)
                    {
                        var documentCopy = item.Value;
                        var userSendId = item.Key;
                        var model = documentCopy.Document.ToModel();

                        var docCode = ConfirmDocCode(publish, publish.CodeId, model.DocumentId, publish.IsCustomCode);
                        publish.Code = docCode;

                        model.DocumentCopyModel = new DocumentCopyModel()
                        {
                            UserThongBao = DocumentCopy.UserCompareString(usersConsult)
                        };
                        _documentHelper.UpdateDocumentDefault(model, null, null, null, userSendId);

                        var receivedVbDens = Json2.ParseAs<Dictionary<int, string>>(userHasReceiveDocuments);
                        SentThongBaoAndVbDen(documentCopy, userSendId, receivedVbDens, DateTime.Now);

                        #region Lưu sổ văn bản

                        if (model.StoreId.HasValue)
                        {
                            SaveStoreDoc(model.StoreId.Value, documentCopy.Document);
                        }

                        #endregion

                        #region Gửi ý kiến phát hành

                        var contentAuthorize = userSendId != GetUserCreatedId()
                                                    ? string.Format("Xử lý ủy quyền:{0} ({1})", User.GetFullName(), User.GetUserName())
                                                    : string.Empty;
                        var comment = new StringBuilder();
                        comment.AppendLine("Phát hành văn bản: " + model.Comments.Content ?? string.Empty);
                        comment.AppendLine("Số ký hiệu: " + publish.Code);
                        _commentService.SendComment(documentCopy.DocumentCopyId, userSendId, comment.ToString(), DateTime.Now, contentAuthorize);

                        #endregion
                    }

                    // Xóa cache
                    _documentCache.RemoveAll(documentCopyIds);

                    trans.Complete();
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
                return Json(new { error = "Phát hành thất bại." });
            }

            return Json(new { success = "Phát hành thành công." });
        }

        #endregion

        #region Liên thông

        public JsonResult TransferLienThong(int documentCopyId, string doc, string files, List<int> removeAttachmentIds, string modifiedFiles, List<int> addressIds, string comment, DateTime? dateAppointed)
        {
            var documentCopy = _docCopyService.Get(documentCopyId);
            if (documentCopy == null)
            {
                return Json(new { error = "Văn bản không tồn tại." });
            }

            int userSendId;
            if (!_documentPermissionHelper.CheckForUyQuyenVaXuLyVanBan(documentCopy, GetUserCreatedId(), out userSendId)
                || _documentPermissionHelper.Check(documentCopy, userSendId, DocumentPermissions.DungXuLy) != DocumentPermissions.DungXuLy)
            {
               // return Json(new { error = "Không có quyền xử lý văn bản này." });
            }

            var userSend = _userService.GetFromCache(userSendId);
            var model = Json2.ParseAsJs<DocumentModel>(doc);

            try
            {
                using (var trans = new TransactionScope(TransactionScopeOption.Required))
                {
                    var newFiles = Json2.ParseAs<IDictionary<string, IDictionary<string, string>>>(files);
                    var modifiedAttachment = Json2.ParseAs<IDictionary<int, string>>(modifiedFiles);

                    model = BuildDocumentModelLienThong(model);
                    documentCopy = _documentHelper.UpdateDocumentDefault(model, newFiles, removeAttachmentIds, modifiedAttachment, userSendId, hasChangeAttachment: true);

                    #region Cập nhật bảng doc_publish

                    var addresses = _addressService.Gets(addressIds).Where(a => a.EdocId != null && a.EdocId == "IOC");
                    if (addresses != null && addresses.Any())
                    {
                        SentIOC(documentCopy.DocumentId, documentCopy.Document);
                        return Json(new { success = true });
                    }

                    var addresseVPCP = _addressService.Gets(addressIds).Where(a => a.EdocId != null && a.EdocId == "VPCP");

                    if (addresseVPCP != null && addresseVPCP.Any())
                    {
                        SendToVPCP(documentCopy.DocumentId, documentCopy.Document);
                        return Json(new { success = true });
                    }

                    Stream fileAttach = null;
                    var attachments = _attachmentService.Gets(documentCopy.DocumentId);
                    if (attachments != null && attachments.Any())
                    {
                        attachments = attachments.Where(a => a.IsDeleted == false);
                        if (attachments != null && attachments.Any())
                        {
                            var att = attachments.LastOrDefault();
                            var fileName = "";
                            fileAttach = _attachmentService.DownloadAttachment(out fileName, att.AttachmentId, null, null, _userService.CurrentUser.UserId);
                        }
                    }
                   
                    #endregion

                    SendToLgsp(documentCopy.DocumentId, documentCopy.Document, fileAttach);

                    // SaveChange
                    _docCopyService.Update(documentCopy);

                    _documentCache.RemoveAll(documentCopy.DocumentCopyId);

                    trans.Complete();
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
                return Json(new { error = "Lỗi trong quá trình liên thông hồ sơ, văn bản." });
            }
            return Json(new { success = true });
        }

        public void SendToLgsp(Guid documentId, Document document, Stream dataFile = null, bool isJSONDaTa = false)
        {
            StaticLog.Log(new List<string>() { "vao ham gui dl" });
                var fileName = "";
            var doctypeForm = document.DocTypeId.HasValue ? _doctypeFormService.Get(document.DocTypeId.Value, true) != null ? _doctypeFormService.Get(document.DocTypeId.Value, true) : _doctypeFormService.Get(document.DocTypeId.Value, false) :  null;

            if (document.CategoryBusinessId == 64)
            {
                var data = document.CompilationData;

                var docs = JsonConvert.DeserializeObject<List<object>>(data);
                var dataSend = new DataModel();
                dataSend.data = docs;
                dataSend.type_name = "eform_model";
                PostModel(dataSend);
                return;
            }

            // 20191219 VuHQ START REQ-2
            List<string> values;
            string header = null;
            Dictionary<string, object> delete_columns = new Dictionary<string, object>();

            var organizekey = "000.00.99.H49";
            //var department = _departmentService.GetReadOnlys().Where(d => d.DepartmentName == document.InOutPlace.Trim() && d.IsActivated == true);
            //if (department != null && department.Any())
            //{
            //    organizekey = department.FirstOrDefault().Emails;
            //}
            //else {
                organizekey = _departmentService.GetReadOnlys().Where(d => d.Emails == document.OrganizationCode.Trim() && d.IsActivated == true).FirstOrDefault().Emails;
            //}
            // 20191219 VuHQ END REQ-2

            if (doctypeForm != null)
            {
                var form = _formService.Get(doctypeForm.FormId);
                if (form != null)
                {
                    // 20191217 VuHQ REQ-2 START
                    // var formName = form.EmbryonicPath.Replace(".xlsx", "");
                    fileName = XlsxToJson.ConvertToAscii(form.TableName);
                    dynamic defineConfigJson = JsonConvert.DeserializeObject(form.DefineConfigJson);
                    dynamic defineFieldJson = JsonConvert.DeserializeObject(form.DefineFieldJson);

                    var defineConfigJsonData = defineConfigJson.data;
                    var defineConfigJsonDataCount = defineConfigJson.data.Count;
                    var defineFieldJsonDataCount = defineFieldJson.data.Count;

                    var documentNote = JsonConvert.DeserializeObject<List<object>>(document.Note);

                    for (var i = 0; i < defineConfigJsonDataCount; i++)
                    {
                        if (defineConfigJsonData[i][defineFieldJsonDataCount].ToString().Equals("Catalog") 
                            &&  defineConfigJsonData[i][defineFieldJsonDataCount + 6].ToString().Equals("True"))
                        {
                            header = string.Empty;
                            values = new List<string>();
                            for (var j = 0; j < documentNote.Count; j++ )
                            {
                                var item = JsonConvert.DeserializeObject<Dictionary<string, string>>(documentNote.ElementAt(j).ToString());
                                values.Add(item.ElementAt(j).Value);
                                if (string.IsNullOrEmpty(header))
                                    header = item.ElementAt(j).Key;
                            }
                            if (!string.IsNullOrEmpty(header))
                                delete_columns.Add(header, values);
                        }
                    }

                    delete_columns.Add("organizekey", organizekey);
                    delete_columns.Add("timekey", !document.DatePublished.HasValue ? DateTime.Now.ToString("yyyyMMdd") : document.DatePublished.Value.ToString("yyyyMMdd"));
                    // 20191217 VuHQ REQ-2 END
                }
            }
            List<object> excelData = null;
            if (!isJSONDaTa)
            {
                var docContents = _docCopyService.GetDocumentContents(documentId).Where(c => !string.IsNullOrEmpty(c.ContentUrl));
                if (!docContents.Any()) return;

                var docContent = docContents.First();
                var startData = 3;
                var countHeader = 1;

                var path = CommonHelper.MapPath("~/" + docContent.ContentUrl);
                StaticLog.Log(new List<string>() { docContent.ContentUrl });
                
                if (dataFile == null)
                {
                    using (Stream str = System.IO.File.Open(path, FileMode.Open))
                    {
                        var xlsxParser = new XlsxToJson(str);
                        excelData = xlsxParser.ConvertXlsxToJson(1, startData, 1, countHeader);
                    }
                }
                else
                {
                    var xlsxParser = new XlsxToJson(dataFile);
                    excelData = xlsxParser.ConvertXlsxToJson(1, startData, 1, countHeader);
                }
            }
            else
            {
                excelData = JsonConvert.DeserializeObject<List<object>>(document.Note);
            }

            if (excelData == null) return;
            var weekly = document.DatePublished.Value.WeekOfYear() >= 10 ? document.DatePublished.Value.WeekOfYear().ToString() : "0" + document.DatePublished.Value.WeekOfYear().ToString();
            var result = new LgspDataReportModel()
            {
                type = fileName,

                data = new List<LgspDataReportModelArray>()
               {
                  new LgspDataReportModelArray()
                  {
                       organizekey = organizekey,
                       datekey = document.DatePublished.HasValue? document.DatePublished.Value.ToString("yyyyMMdd") : "0",
                       yearkey = document.DatePublished.HasValue? document.DatePublished.Value.ToString("yyyy") : "0",
                       ninekey = DateTime.Now.ToString("yyyy")+ "09",
                       halfkey = document.DatePublished.HasValue? document.DatePublished.Value.ToString("yyyy") + GetHalf(document.DatePublished.Value)  : "0",
                       quarterkey = document.DatePublished.HasValue? document.DatePublished.Value.ToString("yyyy") + GetQuarter(document.DatePublished.Value) : "0",
                       monthkey = document.DatePublished.HasValue? document.DatePublished.Value.ToString("yyyyMM") : "0",
                       weekkey = document.DatePublished.HasValue? (( document.DatePublished.Value.ToString("yyyy") + document.DatePublished.Value.WeekOfYear()).Length == 6
                            ? document.DatePublished.Value.ToString("yyyy") + document.DatePublished.Value.WeekOfYear() :  document.DatePublished.Value.ToString("yyyy") +"0"+ document.DatePublished.Value.WeekOfYear()): "0",
                       minutekey = DateTime.Now.ToString("yyyyMMddHHmm"),
                       status =  1,
                       timekey = DateTime.Now.ToString("yyyyMMdd"),
                       array = new LgspDataReportArray()
                       {
                           rows = excelData
                       }
                  }
               }
            };
            try
            {
                dynamic expando = new ExpandoObject();
                var catalogs = JsonConvert.DeserializeObject<Dictionary<string, string>>(document.Address);
                result = new LgspDataReportModel()
                {
                    type = fileName,

                    data = new List<LgspDataReportModelArray>()
                    {
                        new LgspDataReportModelArray()
                        {
                            muavu = catalogs.ContainsKey("muavu") ? catalogs["muavu"] : "",
                            loaicay = catalogs.ContainsKey("loaicay") ? catalogs["loaicay"] : "",
                            caphoc = catalogs.ContainsKey("caphoc") ? catalogs["caphoc"] : "",
                            namhoc = catalogs.ContainsKey("namhoc") ? catalogs["namhoc"] : "",
                            hocky = catalogs.ContainsKey("hocky") ? catalogs["hocky"] : "",
                            khoi = catalogs.ContainsKey("khoi") ? catalogs["khoi"] : "",
                            monhoc = catalogs.ContainsKey("monhoc") ? catalogs["monhoc"] : "",
                            organizekey = organizekey,
                            datekey = document.DatePublished.HasValue ? document.DatePublished.Value.ToString("yyyyMMdd") : "0",
                            yearkey = document.DatePublished.HasValue ? document.DatePublished.Value.ToString("yyyy") : "0",
                            ninekey = document.DatePublished.HasValue ? document.DatePublished.Value.ToString("yyyy")+"09" : "0",
                            halfkey = document.DatePublished.HasValue ? document.DatePublished.Value.ToString("yyyy") + GetHalf(document.DatePublished.Value) : "0",
                            quarterkey = document.DatePublished.HasValue ? document.DatePublished.Value.ToString("yyyy") + GetQuarter(document.DatePublished.Value) : "0",
                            monthkey = document.DatePublished.HasValue ? document.DatePublished.Value.ToString("yyyyMM") : "0",
                            weekkey = document.DatePublished.HasValue ?  (( document.DatePublished.Value.ToString("yyyy") + document.DatePublished.Value.WeekOfYear()).Length == 6 
                            ? document.DatePublished.Value.ToString("yyyy") + document.DatePublished.Value.WeekOfYear() :  document.DatePublished.Value.ToString("yyyy") +"0"+ document.DatePublished.Value.WeekOfYear()): "0",
                            minutekey = DateTime.Now.ToString("yyyyMMddHHmm"),
                            status = 1,
                            timekey = DateTime.Now.ToString("yyyyMMdd"),
                            array = new LgspDataReportArray()
                            {
                                rows = excelData
                            }
                        }
                    },
                    // 20191217 VuHQ START REQ-2
                    delete_columns = delete_columns
                    // 20191217 VuHQ END REQ-2
                };
                
                CreateActivityLog(ActivityLogType.Admin, Newtonsoft.Json.JsonConvert.SerializeObject(result));

                Post("BIReport", result, document.DocTypeId.Value);
            }
            catch (Exception e)
            {

                CreateActivityLog(ActivityLogType.Admin, Newtonsoft.Json.JsonConvert.SerializeObject(result));
                Post("BIReport", result, document.DocTypeId.Value);
            }
        }

        public void SendToVPCP(Guid documentId, Document document, Stream dataFile = null, bool isJSONDaTa = false)
        {
            StaticLog.Log(new List<string>() { "vao ham gui dl" });
            var fileName = "";
            var doctypeForm = document.DocTypeId.HasValue ? _doctypeFormService.Get(document.DocTypeId.Value, true) : null;

            var organizekey = "000.00.99.H49";
            var department = _departmentService.GetReadOnlys().Where(d => d.DepartmentName == document.InOutPlace.Trim() && d.IsActivated == true);
            if (department != null && department.Any())
            {
                organizekey = department.FirstOrDefault().Emails;
            }

            StaticLog.Log(new List<string>() { organizekey });
            StaticLog.Log(new List<string>() { doctypeForm.FormId.ToString() });
            StaticLog.Log(new List<string>() { doctypeForm.DocTypeId.ToString() });

            // 20191219 VuHQ END REQ-2

            if (doctypeForm != null)
            {
                var form = _formService.Get(doctypeForm.FormId);
                var doctype = _doctypeService.Get(doctypeForm.DocTypeId);
                if (form != null && doctype != null)
                {
                    try
                    {
                        StaticLog.Log(new List<string>() { "vao ham ktra dk" });

                        var CodeForm = doctype.CompendiumDefault;
                        var documentNote = JsonConvert.DeserializeObject<List<object>>(document.Note);

                        var data = new DataLienThongVPCP();
                        data.Header = new HeaderVPCP()
                        {
                            Code = CodeForm,
                            Org = organizekey,
                            Period = document.TimeKey
                        };
                        data.Data = documentNote;

                        LienThongVPCP("LT", data);
                    }
                    catch (Exception ex)
                    {
                        StaticLog.Log(new List<string>() { ex.Message});

                    }
                    // 20191217 VuHQ REQ-2 START
                    // var formName = form.EmbryonicPath.Replace(".xlsx", "");
                    //fileName = XlsxToJson.ConvertToAscii(form.TableName);

                }
            }
        }

        private static dynamic DictionaryToObject(IDictionary<String, Object> dictionary)
        {
            var expandoObj = new ExpandoObject();
            var expandoObjCollection = (ICollection<KeyValuePair<String, Object>>)expandoObj;

            foreach (var keyValuePair in dictionary)
            {
                expandoObjCollection.Add(keyValuePair);
            }
            dynamic eoDynamic = expandoObj;
            return eoDynamic;
        }

        private bool PostModel(dynamic data)
        {
            var client = new HttpClient();

            var appSettings = ConfigurationManager.AppSettings;
            var url = appSettings["createModel"] ?? "";
            
            StaticLog.Log(new List<string>() { JsonConvert.SerializeObject(data) });
            StaticLog.Log(new List<string>() { url });

            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = TimeSpan.FromSeconds(30);
            if (data != null)
            {
                var content = new ObjectContent(data.GetType(), data, new JsonMediaTypeFormatter());
                var responseMessage = client.PostAsync("", content).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    //var result = JsonConvert.DeserializeObject<bool>(responseMessage.Content.ReadAsStringAsync().Result);
                    //return result;
                }
            }

            return false;
        }

        private bool Post(string action, dynamic data, Guid doctypeId)
        {
            var client = new HttpClient();

            var appSettings = ConfigurationManager.AppSettings;
            var doctypeIds = appSettings["doctypeDWH"] ?? "";
            var url = "";
            if (!string.IsNullOrEmpty(doctypeIds))
            {
                var dts = doctypeIds.Split(new char[] { ',' });
                if (dts.Any() && doctypeIds.Contains(doctypeId.ToString()))
                {
                    url = appSettings["dashboardUrlDWH"] ?? "";
                }
            }

            if (string.IsNullOrEmpty(url))
            {
                url = appSettings["dashboardUrl"];
            }
            StaticLog.Log(new List<string>() { JsonConvert.SerializeObject(data) });
            StaticLog.Log(new List<string>() { url });

            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = TimeSpan.FromSeconds(30);
            if (data != null)
            {
                var content = new ObjectContent(data.GetType(), data, new JsonMediaTypeFormatter());
                var responseMessage = client.PostAsync("", content).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    //var result = JsonConvert.DeserializeObject<bool>(responseMessage.Content.ReadAsStringAsync().Result);
                    //return result;
                }
            }
            else
            {
                var responseMessage = client.PostAsync(action, null).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    //var result = JsonConvert.DeserializeObject<bool>(responseMessage.Content.ReadAsStringAsync().Result);
                    //return result;
                }
            }

            return false;
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "PublishResendLienThong")]
        public JsonResult ResendLienThong(int publishId)
        {
            var publish = _documentPublishService.Get(publishId);
            if (publish == null || !publish.IsPending || !publish.AddressId.HasValue)
            {
                return Json(new { error = true, message = "Yêu cầu không tồn tại, vui lòng tải lại trang và thử lại." }, JsonRequestBehavior.AllowGet);
            }

            var documentCopy = _docCopyService.Get(publish.DocumentCopyId);

            try
            {
                var sent = _edocService.ReSendLienThong(documentCopy, publish);
                if (!sent)
                {
                    return Json(new { error = true, message = "Gửi lại thất bại." }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(new { error = true, message = "Có lỗi xảy ra khi gửi lại, vui lòng tải lại trang và thử lại." }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Lấy lại văn bản gửi liên thông
        /// </summary>
        /// <param name="publishId"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult RecalledLienThong(int publishId, string comment)
        {
            var publish = _documentPublishService.Get(publishId);
            if (publish == null || !publish.AddressId.HasValue)
            {
                return Json(new { error = true, message = "Yêu cầu không tồn tại, vui lòng tải lại trang và thử lại." }, JsonRequestBehavior.AllowGet);
            }

            var result = 2;
            try
            {
                if (publish.IsPending)
                {
                    publish.IsPending = false;
                    publish.HasLienThong = false;
                    publish.Note = string.Format("{0} Đã thu hồi, lý do: ", DateTime.Now.ToString("g"), comment);
                    result = 15;
                }
                else
                {
                    publish.Status = 13;
                    publish.DateSent = DateTime.Now;
                    publish.Note = comment;
                    result = 13;
                }

                _documentPublishService.SaveChanges();
            }
            catch
            {
                return Json(new { error = true, message = "Có lỗi xảy ra khi gửi yêu cầu thu hồi, vui lòng tải lại trang và thử lại." }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = true, status = result }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Dự kiến phát hành

        [HttpPost]
        public JsonResult PublishmentPlan(int documentCopyId, string destination)
        {
            if (string.IsNullOrWhiteSpace(destination))
            {
                return Json(new { error = "Nội dung dự kiến phát hành lỗi." });
            }

            var userId = _userService.CurrentUser.UserId;
            var anticipatePublish = new Anticipate
            {
                DocumentCopyId = 0,
                DocumentId = Guid.Empty,
                Destination = destination,
                UserId = userId,
                AnticipateType = (int)AnticipateType.PhatHanh
            };

            var documentCopy = _docCopyService.Get(documentCopyId);
            if (documentCopy != null)
            {
                anticipatePublish.DocumentCopyId = documentCopy.DocumentCopyId;
                anticipatePublish.DocumentId = documentCopy.DocumentId;
            }

            var id = _anticipateService.Create(anticipatePublish, true);

            return Json(new { success = "Tạo mới dự kiến chuyển thành công.", id = id });
        }

        #endregion

        #region Private

        private string ConfirmDocCode(PublishInfoModel publish, int codeId, Guid documentId, bool isCustomCode)
        {
            var docCode = publish.Code;
            var code = _codeService.GetFromCache(codeId);

            docCode = _codeService.ConfirmCode(code, DateTime.Now, documentId, docCode);

            if (string.IsNullOrEmpty(docCode))
            {
                throw new Exception("Số ký hiệu không hợp lệ. Vui lòng thử lại.");
            }

            if (DocCodeIsUsed(docCode, publish.Organization, publish.StoreId, documentId, categoryBussiness: CategoryBusinessTypes.VbDi))
            {
                throw new Exception(string.Format("Mã {0} đã được cấp, vui lòng nhập mã khác.", publish.Code));
            }

            return docCode;
        }
        private string ConfirmDocCodeGov(PublishPlusInfoModel publish, int codeId, Guid documentId, bool isCustomCode)
        {
            var docCode = publish.Code;
            var code = _codeService.GetFromCache(codeId);

            docCode = _codeService.ConfirmCode(code, DateTime.Now, documentId, docCode);

            if (string.IsNullOrEmpty(docCode))
            {
                throw new Exception("Số ký hiệu không hợp lệ. Vui lòng thử lại.");
            }

            if (DocCodeIsUsed(docCode, publish.Organization, publish.StoreId, documentId, categoryBussiness: CategoryBusinessTypes.VbDi))
            {
                throw new Exception(string.Format("Mã {0} đã được cấp, vui lòng nhập mã khác.", publish.Code));
            }

            return docCode;
        }
        private DocumentModel BuildDocumentModelPublish(DocumentModel model, PublishInfoModel publish, List<CommentTransfer> commentTransfers, string processInfo, List<int> userConsult)
        {
            var dateNow = DateTime.Now;
            model.DocCode = publish.Code;

            var approve = _userService.GetFromCache(System.Convert.ToInt32(publish.Approvers));
            if (approve == null)
            {
                throw new Exception("Không tìm thấy người ký duyệt");
            }
            else
            {
                model.UserSuccessId = approve.UserId;
                model.UserSuccessName = approve.FullName;
                model.SuccessNote = approve.FullName;
            }

            // Cập nhật thông tin phát hành
            model.Organization = publish.Organization;
            model.OrganizationCode = publish.OrganizationCode;

            model.StoreId = publish.StoreId;
            model.SecurityId = publish.SecurityId;
            model.TotalPage = publish.TotalPage;
            model.DatePublished = publish.DatePublished;
            model.Note = publish.InPlace;
            model.DateSuccess = dateNow;
            model.DateOfIssueCode = dateNow;
            model.StoreId = publish.StoreId;

            model.ProcessInfo = processInfo;

            // Kết thúc văn bản khi phát hành
            model.Status = (byte)DocumentStatus.KetThuc;

            model.DateFinished = dateNow;
            model.DateCreated = dateNow;

            model.DocumentCopyModel = new DocumentCopyModel()
            {
                Status = (byte)DocumentStatus.KetThuc,
                DateFinished = dateNow,
                UserThongBao = DocumentCopy.UserCompareString(userConsult),
                DocumentUsers = DocumentCopy.UserCompareString(userConsult)
            };

            foreach (var r in model.RelationModels)
            {
                r.IsAddNext = true;
            }

            return model;
        }
        private DocumentModel BuildDocumentModelPublishGov(DocumentModel model, PublishPlusInfoModel publish, List<CommentTransfer> commentTransfers, string processInfo, List<int> userConsult)
        {
            var dateNow = DateTime.Now;
            model.DocCode = publish.Code;

            var approve = string.IsNullOrWhiteSpace(publish.Approvers) ? null : _userService.GetFromCache(System.Convert.ToInt32(publish.Approvers));
            if (approve == null)
            {
                //throw new Exception("Không tìm thấy người ký duyệt");
            }
            else
            {
                model.UserSuccessId = approve.UserId;
                model.UserSuccessName = approve.FullName;
                model.SuccessNote = approve.FullName;
            }

            // Cập nhật thông tin phát hành
            model.Organization = publish.Organization;
            model.OrganizationCode = publish.OrganizationCode;

            model.StoreId = publish.StoreId;
            model.SecurityId = publish.SecurityId;
            model.TotalPage = publish.TotalPage;
            // model.DatePublished = publish.DatePublished;
            // model.Note = publish.InPlace;
            model.DateSuccess = dateNow;
            model.DateOfIssueCode = dateNow;
            model.StoreId = publish.StoreId;

            // model.ProcessInfo = processInfo;

            // Kết thúc văn bản khi phát hành
            model.Status = (byte)DocumentStatus.KetThuc;

            model.DateFinished = dateNow;
            model.DateCreated = dateNow;

            model.DocumentCopyModel = new DocumentCopyModel()
            {
                Status = (byte)DocumentStatus.KetThuc,
                DateFinished = dateNow,
                UserThongBao = DocumentCopy.UserCompareString(userConsult),
                DocumentUsers = DocumentCopy.UserCompareString(userConsult)
            };

            foreach (var r in model.RelationModels)
            {
                r.IsAddNext = true;
            }

            return model;
        }
        private void RePublish(DocumentCopy docCopy, Dictionary<int, DateTime?> addressDictionary, int userId)
        {
            var addressIds = addressDictionary.Select(ad => ad.Key).ToList();
            var addresses = _addressService.Gets(addressIds).ToList();
            var publishInfo = new PublishInfoModel()
            {
                Address = addressIds,
                DatePublished = DateTime.Now,
                DateResponeAddress = addressDictionary
            };

            List<string> listMailReceive;
            Publish(docCopy, publishInfo, "", userId, out listMailReceive);

            #region Gửi mail phát hành

            if (listMailReceive.Any())
            {
                SendPublishMails(listMailReceive, docCopy.Document, docCopy.DocumentCopyId);
            }

            #endregion
        }

        private void Publish(DocumentCopy documentCopy, PublishInfoModel publish, string searchAddr, int userSendId, out List<string> listMailReceive)
        {
            var document = documentCopy.Document;
            var dateNow = DateTime.Now;
            var hasAttachments = document.Attachments.Any();
            listMailReceive = new List<string>();

            var userSend = _userService.CurrentUser;

            var addressIds = publish.Address;
            var addressDictionary = publish.DateResponeAddress;

            var receiveMails = new List<string>();

            if (addressIds.Any())
            {
                var addresses = _addressService.Gets(addressIds);
                foreach (var address in addresses)
                {
                    var dateResponse = publish.DateResponse;
                    if (addressDictionary != null)
                    {
                        var addressDic = addressDictionary.First(ad => ad.Key == address.AddressId);
                        dateResponse = addressDic.Value ?? dateResponse;
                    }

                    var hasLienThong = !string.IsNullOrEmpty(address.EdocId) && hasAttachments;
                    var docPublish = new DocPublish()
                    {
                        DocumentCopyId = documentCopy.DocumentCopyId,
                        DocumentId = document.DocumentId,
                        DoctypeId = document.DocTypeId.Value,
                        DocCode = document.DocCode,
                        DatePublished = publish.DatePublished,
                        IsHsmc = false,
                        UserPublishId = userSendId,
                        UserPublishName = userSend.FullName,
                        AddressName = address.Name,
                        HasLienThong = hasLienThong,
                        IsPending = hasLienThong ? true : false,
                        AddressId = address.AddressId,
                        HasRequireResponse = dateResponse.HasValue,
                        AddressCode = address.EdocId,
                        DateAppointed = dateResponse
                    };
                    _documentPublishService.Create(docPublish);

                    if (address.IsPublishEmail && !string.IsNullOrWhiteSpace(address.Email) && address.Email.IsEmailAddress())
                    {
                        listMailReceive.Add(address.Email);
                    }
                }
            }
            else
            {
                var docPublish = new DocPublish()
                {
                    DocumentCopyId = documentCopy.DocumentCopyId,
                    DocumentId = document.DocumentId,
                    DoctypeId = document.DocTypeId.Value,
                    DocCode = document.DocCode,
                    DatePublished = publish.DatePublished,
                    IsHsmc = false,
                    UserPublishId = userSendId,
                    UserPublishName = userSend.FullName,
                    AddressName = searchAddr,
                    AddressCode = "",
                    HasLienThong = false,
                    IsPending = false,
                    HasRequireResponse = false
                };

                _documentPublishService.Create(docPublish);
            }

            CreateActivityLog(ActivityLogType.PhatHanh, string.Format("{0} phát hành văn bản: {1}", User.GetUserNameWithDomain(), documentCopy.Document.Compendium));
        }

        private void PublishGov(DocumentCopy documentCopy, PublishPlusInfoModel publish, string searchAddr, int userSendId, out List<string> listMailReceive)
        {
            var document = documentCopy.Document;
            var dateNow = DateTime.Now;
            var hasAttachments = document.Attachments.Any();
            listMailReceive = new List<string>();

            var userSend = _userService.CurrentUser;

            var addressIds = publish.Address;
            var addressDictionary = publish.DateResponeAddress;

            var receiveMails = new List<string>();

            if (addressIds.Any())
            {
                var addresses = _addressService.Gets(addressIds);
                foreach (var address in addresses)
                {
                    var dateResponse = publish.DateResponse;
                    if (addressDictionary != null)
                    {
                        var addressDic = addressDictionary.First(ad => ad.Key == address.AddressId);
                        dateResponse = addressDic.Value ?? dateResponse;
                    }
                    var dataSent = _docCopyService.GetDataReportFromDocumentCopyId(documentCopy.DocumentCopyId, userSendId, _reportConfigSettings.OzganizeKey);
                    var hasLienThong = !string.IsNullOrEmpty(address.EdocId) && hasAttachments;
                    var docPublish = new DocPublishPlus
                    {
                        DocumentCopyId = documentCopy.DocumentCopyId,
                        DocumentId = document.DocumentId,
                        DoctypeId = document.DocTypeId ?? Guid.Empty,
                        DocCode = document.DocCode,
                        DatePublished = publish.DatePublished,
                        IsHsmc = false,
                        UserPublishId = userSendId,
                        UserPublishName = userSend.FullName,
                        AddressName = address.Name,
                        HasLienThong = hasLienThong,
                        IsPending = true,
                        AddressId = address.AddressId,
                        HasRequireResponse = dateResponse.HasValue,
                        AddressCode = address.EdocId,
                        DateAppointed = dateResponse,
                        Status = 2,
                        DataSend = JsonConvert.SerializeObject(dataSent)
                    };
                    if (_reportConfigSettings.SendDirectly)
                    {
                        dataSent.access_token = _reportConfigSettings.TokenService;
                        LienThongVPCPLive("sndData", dataSent);
                    }

                    _documentPublishPlusService.Create(docPublish);

                    if (address.IsPublishEmail && !string.IsNullOrWhiteSpace(address.Email) && address.Email.IsEmailAddress())
                    {
                        listMailReceive.Add(address.Email);
                    }
                }
            }
            else
            {
                var docPublish = new DocPublishPlus
                {
                    DocumentCopyId = documentCopy.DocumentCopyId,
                    DocumentId = document.DocumentId,
                    DoctypeId = document.DocTypeId ?? Guid.Empty,
                    DocCode = document.DocCode,
                    DatePublished = publish.DatePublished,
                    IsHsmc = false,
                    UserPublishId = userSendId,
                    UserPublishName = userSend.FullName,
                    AddressName = searchAddr,
                    AddressCode = "",
                    HasLienThong = false,
                    IsPending = true,
                    HasRequireResponse = false,
                    Status = 2
                };

                _documentPublishPlusService.Create(docPublish);
            }

            CreateActivityLog(ActivityLogType.PhatHanh, string.Format("{0} phát hành văn bản: {1}", User.GetUserNameWithDomain(), documentCopy.Document.Compendium));
        }

        public JsonResult APIPublishVPCP(int documentCopyId)
        {
            var docCopy = _docCopyService.Get(documentCopyId);
            if (docCopy == null)
            {
                return Json(new { error = true, message = "Không tìm thấy báo cáo" });
            }

            var sendVPCP = PublishVPCP(docCopy, docCopy.UserCurrentId);

            return Json(new { data = sendVPCP } );
        }
        private string PublishVPCP(DocumentCopy documentCopy, int userSendId)
        {
            var document = documentCopy.Document;
            var result = "";
            var dataSent = _docCopyService.GetDataReportFromDocumentCopyId(documentCopy.DocumentCopyId, userSendId);
            var userSend = _userService.Get(documentCopy.UserCurrentId);
            var docPublish = new DocPublishPlus
            {
                DocumentCopyId = documentCopy.DocumentCopyId,
                DocumentId = document.DocumentId,
                DoctypeId = document.DocTypeId ?? Guid.Empty,
                DocCode = document.DocCode,
                DatePublished = DateTime.Now,
                IsHsmc = false,
                UserPublishId = userSendId,
                UserPublishName = userSend.FullName,
                AddressName = "VPCP",
                HasLienThong = true,
                IsPending = true,
                AddressId = 1,
                HasRequireResponse = false,
                AddressCode = "000.00.00.G22",
                DateAppointed = DateTime.Now,
                Status = 2,
                DataSend = JsonConvert.SerializeObject(dataSent)
            };
            if (_reportConfigSettings.SendDirectly)
            {
                dataSent.access_token = _reportConfigSettings.TokenService;
                result = LienThongVPCPLive("sndData", dataSent);
            }

            _documentPublishPlusService.Create(docPublish);

            CreateActivityLog(ActivityLogType.PhatHanh, string.Format("{0} phát hành văn bản: {1}", User.GetUserNameWithDomain(), documentCopy.Document.Compendium));
            return result;
        }

        private void SendPublishMails(List<string> listMailReceive, Document document, int documentCopyId)
        {
            if (listMailReceive == null || !listMailReceive.Any()) return;

            var attachments = document.Attachments;
            var attachNames = attachments.Select(a => a.AttachmentName).ToList();
            var attachmentIds = attachments.Select(a => a.AttachmentId).ToList();

            var sendMail = _mailHelper.SendTranferPublishDocument(document, listMailReceive, attachmentIds, documentCopyId, attachNames);
        }

        private bool DocCodeIsUsed(string docCode, string organization = null,
                                        int? storeId = null, Guid? documentId = null,
                                        CategoryBusinessTypes? categoryBussiness = null,
                                        string inOutPlace = "")
        {
            if (string.IsNullOrEmpty(docCode))
            {
                return false;
            }

            return _codeService.CodeIsUsed(docCode, true, storeId ?? 0, categoryBussiness, organization, documentId, inOutPlace);
        }

        private void SentThongBaoAndVbDen(DocumentCopy documentCopy, int userSendId,
                        Dictionary<int, string> userHasReceiveDocuments, DateTime dateCreated)
        {
            var userReceiveMessage = new List<UserReceives>();
            var userReceivedIds = new List<int>();
            var document = documentCopy.Document;
            var compendium = document.Compendium;

            var hasVBden = userHasReceiveDocuments != null && userHasReceiveDocuments.Any();
            if (!hasVBden) return;

            // Add Văn bản liên quan gốc vào văn bản mới
            var relations = _documentService.GetDocRelations(d => d.DocumentId == document.DocumentId);

            foreach (var userReceive in userHasReceiveDocuments)
            {
                var userReceiveId = userReceive.Key;
                document.InOutPlace = userReceive.Value;

                var newDocumentCopy = CreateCommingDocument(userSendId, userReceiveId, document, relations);

                // Gửi notify
                _documentHelper.PushNotifyMessage(new List<int>() { userReceiveId }, newDocumentCopy, compendium, dateCreated);
                userReceivedIds.Add(userReceiveId);
            }

            _docCopyService.UpdateRelationUserJoineds(relations.ToList(), userReceivedIds, hasSaveChange: true);

            try
            {
                _documentHelper.SendDocumentMail(documentCopy.Document, userSendId, userReceivedIds);
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        private DocumentCopy CreateCommingDocument(int userSendId, int userReceiveId, Document document, IEnumerable<DocRelation> relations)
        {
            // TienBV: thay đổi nghiệp vụ copy attachment khi gửi phát hành nội bộ, liên thông, đồng xử lý, ...
            // - Các attachment sẽ sử dụng chung phiên bản với bản chính thông qua ParentId ở dưới.
            // - Khi hướng nào có chỉnh sửa thì sẽ lưu phiên bản riêng cho hướng đó.
            // var attachments = _attachmentService.CopyAttachment(document.Attachments, userSendId);
            var newDocId = Guid.NewGuid();
            var userCreate = _userService.GetFromCache(userSendId);

            var attachments = document.Attachments.Where(a => !a.IsDeleted)
                .Select(a =>
                {
                    var ad = a.AttachmentDetails.Last();
                    var newAtt = new Attachment()
                    {
                        DocumentId = newDocId,
                        AttachmentName = a.AttachmentName,
                        Size = a.Size,
                        VersionAttachment = 1
                    };

                    newAtt.AttachmentDetails.Add(new AttachmentDetail()
                    {
                        FileName = ad.FileName,
                        CreatedByUserId = userSendId,
                        CreatedByUserName = userCreate.Username,
                        CreatedOnDate = ad.CreatedOnDate,
                        FileLocationId = ad.FileLocationId,
                        IdentityFolder = ad.IdentityFolder,
                        Size = (int)ad.Size,
                        VersionAttachmentDetail = 1,
                        IsLink = ad.IsLink,
                        AttachLink = ad.AttachLink
                    });

                    return newAtt;
                }).ToList();

            var newDocument = new Document()
            {
                CategoryBusinessId = (int)CategoryBusinessTypes.VbDen,
                CategoryId = document.CategoryId,
                CategoryName = document.CategoryName,
                DocumentId = newDocId,
                DocCode = document.DocCode,
                Compendium = document.Compendium,
                Original = 0,
                Organization = document.Organization,
                OrganizationCode = "",
                InOutPlace = document.InOutPlace,
                TotalPage = 1,
                UserCreatedId = userSendId,
                UserCreatedName = userCreate == null ? "" : userCreate.FullName,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                DateArrived = DateTime.Now,
                DatePublished = null,
                Attachments = attachments,
                LienThongStatus = ""
            };

            // Lưu ý không add người gửi vào danh sách documentuser của văn bản đến
            var newDocumentCopy = _documentService.CreateComingDocument(newDocument, userReceiveId, new List<int>() { userReceiveId }, null);

            var createdComment = string.Format("Văn bản đến lúc: {0}.", DateTime.Now.ToString("HH:mm dd/MM/yyyy"));
            _commentService.SendCommentCommon(newDocumentCopy, 0, DateTime.Now, createdComment, CommentType.Common);

            // Add Văn bản liên quan gốc vào văn bản mới
            if (relations == null || !relations.Any())
            {
                return newDocumentCopy;
            }

            foreach (var relation in relations)
            {
                var newRelation = new DocRelation()
                {
                    DocumentCopyId = newDocumentCopy.DocumentCopyId,
                    DocumentId = newDocumentCopy.DocumentId,
                    RelationId = relation.RelationId,
                    RelationCopyId = relation.RelationCopyId,
                    DocCode = relation.DocCode,
                    InOutCode = relation.InOutCode,
                    CategoryName = relation.CategoryName,
                    CitizenName = relation.CitizenName,
                    Compendium = relation.Compendium,
                    DateArrived = relation.DateArrived,
                    RelationType = (int)RelationTypes.LienQuanThongThuong
                };

                _documentService.CreateDocRelations(newRelation, isSaveChanges: true);
            }

            return newDocumentCopy;
        }

        private DocumentCopy UpdateDocumentRePublish(DocumentCopy docCopy, IDictionary<string, IDictionary<string, string>> newFiles,
                                IDictionary<int, string> modifiedAttachment, List<int> removeAttachmentIds, List<int> usersConsult, User currentUser)
        {
            var document = docCopy.Document;
            _documentService.UpdateAttachments(document, newFiles, removeAttachmentIds, modifiedAttachment, currentUser);

            docCopy.Status = (byte)DocumentStatus.KetThuc;
            document.Status = (byte)DocumentStatus.KetThuc;

            _docCopyService.UpdateUserThongBao(docCopy, usersConsult);

            _docCopyService.Update(docCopy);

            return docCopy;
        }

        private DocumentModel BuildDocumentModelLienThong(DocumentModel model)
        {
            model.DocumentCopyType = (int)DocumentCopyTypes.ChoKetQuaDungXuLy;
            model.DocumentCopyModel.DocumentCopyType = (int)DocumentCopyTypes.ChoKetQuaDungXuLy;
            model.Status = (int)DocumentStatus.KetThuc;
            model.DocumentCopyStatus = (int)DocumentStatus.KetThuc;
            model.DateRequireSupplementary = DateTime.Now;
            //model.IsGettingOut = true;
            //model.IsTransferPublish = true;

            return model;
        }

        private void SaveStoreDoc(int storeId, Document document)
        {
            document.StoreDocs.Add(new StoreDoc()
            {
                StoreId = storeId
            });

            _documentService.SaveChanges();
        }

        #endregion

        public int GetHalf(DateTime date)
        {
            return (date.Month + 5) / 6;
        }
        public int GetQuarter(DateTime date)
        {
            return (date.Month + 2) / 3;
        }

        public void SentIOC(Guid documentId, Document document)
        {
            var docContents = _docCopyService.GetDocumentContents(documentId).Where(c => !string.IsNullOrEmpty(c.ContentUrl));
            if (!docContents.Any()) return;
            var docContent = docContents.First();

            var path = CommonHelper.MapPath("~/" + docContent.ContentUrl);
            using (Stream str = System.IO.File.Open(path, FileMode.Open))
            {
                var tempFileCSV = "~/Temp/" + Guid.NewGuid() + ".csv";
                var pathCSV = CommonHelper.MapPath(tempFileCSV);
                var xlsxParser = new XlsxToJson(str);
                xlsxParser.SaveCSV(pathCSV);

                FileStream stream = System.IO.File.OpenRead(pathCSV);
                byte[] fileBytes = new byte[stream.Length];

                stream.Read(fileBytes, 0, fileBytes.Length);
                stream.Close();

                FileUploadSFTP(fileBytes);
            }
        }

        public void FileUploadSFTP(byte[] csvFile)
        {
            var host = "sftp-quangninh.worldsensing.com";
            var port = 22;
            var username = "quangninhioc";
            var password = "={JrF]y0?Cn9";

            var connectionInfo = new ConnectionInfo(host, "sftp", new PasswordAuthenticationMethod(username, password));
            var methods = new List<AuthenticationMethod>
            {
                new PasswordAuthenticationMethod(username, password)
            };

            var connect = new ConnectionInfo(host, port, username, methods.ToArray());
            using (var client = new SftpClient(connect))
            {
                client.Connect();
                if (client.IsConnected)
                {

                    using (var ms = new MemoryStream(csvFile))
                    {
                        var a = client.WorkingDirectory;
                        client.BufferSize = (uint)ms.Length; // bypass Payload error large files
                        client.UploadFile(ms, "/uploads/prod/smart_connector/templates/schools.csv", true);
                    }
                }
                else
                {
                }
            }
        }

        public bool LienThongVPCP(string action, dynamic data)
        {
            var client = new HttpClient();

            var appSettings = ConfigurationManager.AppSettings;
            var url = appSettings["urlVPCP"] ?? "";

            StaticLog.Log(new List<string>() { JsonConvert.SerializeObject(data) });
            StaticLog.Log(new List<string>() { "Gửi liên thông văn phòng chính phủ" });

            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = TimeSpan.FromSeconds(30);
            if (data != null)
            {
                var content = new ObjectContent(data.GetType(), data, new JsonMediaTypeFormatter());
                var responseMessage = client.PostAsync("", content).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    StaticLog.Log(new List<string>() { responseMessage.Content.ReadAsStringAsync().Result }); 
                    //return result;
                }
            }
            else
            {
                var responseMessage = client.PostAsync(action, null).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    //var result = JsonConvert.DeserializeObject<bool>(responseMessage.Content.ReadAsStringAsync().Result);
                    //return result;
                }
            }

            return false;
        }

        public string LienThongVPCPLive(string action, dynamic data)
        {
            var client = new HttpClient();
            var url = _reportConfigSettings.UrlService;

            //StaticLog.LogLienThong(new List<string>() { JsonConvert.SerializeObject(data) });

            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = TimeSpan.FromSeconds(30);
            if (data != null)
            {
                var content = new ObjectContent(data.GetType(), data, new JsonMediaTypeFormatter());
                var responseMessage = client.PostAsync("", content).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    StaticLog.Log(new List<string>() { responseMessage.Content.ReadAsStringAsync().Result });
                    return responseMessage.Content.ReadAsStringAsync().Result;
                }
            }
            else
            {
                var responseMessage = client.PostAsync(action, null).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    //var result = JsonConvert.DeserializeObject<bool>(responseMessage.Content.ReadAsStringAsync().Result);
                    //return result;
                }
            }

            return "";
        }
    }

    public class LgspDataReport
    {
        public string ma_don_vi { get; set; }

        public string don_vi { get; set; }

        public string dia_chi_noi_gui { get; set; }

        public string ma_linh_vuc { get; set; }

        public string linh_vuc { get; set; }

        public string loai_bao_cao { get; set; }

        public string ten_bao_cao { get; set; }

        public int loai_ky_bao_cao { get; set; }

        public string ky_bao_cao { get; set; }

        public int version { get; set; }
    }

    public class LgspDataReportModel
    {
        public LgspDataReportModel()
        {
        }
        public string type { get; set; }

        public List<LgspDataReportModelArray> data { get; set; }

        // 20191217 VuHQ
        public Dictionary<string, object> delete_columns { get; set; }
    }

    public class LgspDataReportModelArray
    {
        public string monhoc { get; set; }
        public string hocky { get; set; }
        public string khoi { get; set; }
        public string caphoc { get; set; }
        public string muavu { get; set; }
        public string loaicay { get; set; }
        public string namhoc { get; set; }
        public string organizekey { get; set; }
        public string datekey { get; set; }
        public string weekkey { get; set; }
        public string monthkey { get; set; }
        public string quarterkey { get; set; }
        public string halfkey { get; set; }
        public string yearkey { get; set; }
        public string ninekey { get; set; }
        public string minutekey { get; set; }
        public int status { get; set; }

        //20200102 VuHQ them timekey dang datetime
        public string timekey { get; set; }

        public LgspDataReportArray array { get; set; }
    }
    public class LgspDataReportArray
    {
        public List<object> rows { get; set; }
    }

    public class HeaderVPCP
    {
        public string Code { get; set; }
        public string Org { get; set; }
        public string Period { get; set; }
    }
    
    public class DataLienThongVPCP
    {
        public HeaderVPCP Header { get; set; }
        public List<object> Data { get; set; }
    }

    public class DataModel
    {
        public string type_name { get; set; }
        public List<object> data { get; set; }
    }
}