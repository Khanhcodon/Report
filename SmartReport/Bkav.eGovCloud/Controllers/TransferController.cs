#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Web.Mvc;
using System.Threading.Tasks;

using Bkav.eGovCloud.Business;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Business.Utils;
using Bkav.eGovCloud.Core.Document;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.FileSystem;
using Bkav.eGovCloud.Core.History;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Core.Workflow;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Entities.Enum;
using Bkav.eGovCloud.Models;
using Bkav.eGovCloud.Search;
using Bkav.eGovCloud.Web.Framework;
using Microsoft.AspNet.SignalR;
using Microsoft.JScript;
using SolrNet.Utils;
using Action = Bkav.eGovCloud.Core.Workflow.Action;
using Bkav.eGovCloud.Business.Caching;
using Newtonsoft.Json;
using System.Dynamic;
using Bkav.eGovCloud.Entities.Customer.Settings;
using System.Text.RegularExpressions;

#endregion

namespace Bkav.eGovCloud.Controllers
{
    public class TransferController : CustomerBaseController
    {
#pragma warning disable 618 

        #region Readonly & Static Fields

        private readonly TemplateHelper _templateHelper;
        private readonly CodeBll _codeService;
        private readonly CommentBll _commentService;
        private readonly DepartmentBll _departmentService;
        private readonly DocTypeBll _docTypeService;
        private readonly DocumentCopyBll _docCopyService;
        private readonly DocumentPermissionHelper _documentPermissionHelper;
        private readonly DocumentBll _documentService;
        private readonly PositionBll _positionService;
        private readonly SupplementaryBll _supplementaryService;
        private readonly UserBll _userService;
        private readonly WorkflowHelper _workflowHelper;
        private readonly WorkflowBll _workflowService;
        private readonly AttachmentBll _attachmentService;
        private readonly ExtensionTimeBll _extensionTimeService;
        private readonly StorePrivateBll _storePrivateService;
        private readonly DailyProcessBll _dailyProcessService;
        private readonly StoreBll _storeService;
        private readonly AddressBll _addressService;
        private readonly JobTitlesBll _jobTitlesService;
        private readonly KeyWordBll _keyWordService;
        private readonly IncreaseBll _increaseService;
        private readonly DocumentPublishBll _documentPublishService;
        private readonly DocumentPublishPlusBll _documentPublishPlusService;
        private readonly WorktimeHelper _workTimeHelper;
        private readonly AnticipateBll _anticipateService;
        private readonly EgovSearch _searchService;
        private readonly SendSmsHelper _smsHelper;
        private readonly SmsSettings _smsSettings;
        private readonly UserActivityLogBll _userActivityLogService;
        private readonly NotificationBll _notificationService;
        private readonly MobileDeviceBll _mobileDeviceService;
        private readonly DocFinishBll _docFinishService;
        private readonly NotificationSettings _notificationSettings;
        private ReportConfigSettings _reportConfigSettings;

        private readonly FormBll _formService;
        private readonly DocTypeFormBll _doctypeFormService;
        private readonly EdocBll _edocService;

        private readonly IHubContext _hubContext;
        private readonly UserConnectionBll _userConnectionService;
        private readonly ResourceBll _resourceService;
        private readonly AdminGeneralSettings _generalSettings;
        private readonly SendEmailHelper _mailHelper;
        private readonly DocumentContentBll _documentContentService;
        private readonly CategoryBll _categoryService;
        private readonly Helper.UserSetting _helperUserSetting;
        private readonly Helper.NotificationHelper _notificationHelper;
        private readonly CitizenBll _citizenService;
        private readonly StoreDocBll _storeDocService;
        private readonly DocumentHelper _documentHelper;

        private readonly DocumentsCache _documentCache;

        #endregion

        #region C'tors

        public TransferController(
            TemplateHelper templateHelper,
            UserBll userService,
            WorkflowBll workflowService,
            DepartmentBll departmentService,
            PositionBll positionService,
            CommentBll commentService,
            DocTypeBll docTypeService,
            DocumentBll documentService,
            DocumentCopyBll documentCopyService,
            SupplementaryBll supplementaryService,
            DocumentPermissionHelper documentPermissionHelper,
            WorkflowHelper workflowHelper,
            CodeBll codeService,
            AttachmentBll attachmentService,
            ExtensionTimeBll extensionTimeService,
            StorePrivateBll storePrivateService,
            DailyProcessBll dailyProcessService,
            StoreBll storeService,
            AddressBll addressService,
            JobTitlesBll jobTitlesService,
            KeyWordBll keyWordService,
            IncreaseBll increaseService,
            DocumentPublishBll documentPublishService,
            DocumentPublishPlusBll documentPublishPlusService,
            WorktimeHelper workTimeHelper,
            AnticipateBll anticipateService,
            EgovSearch searchService,
            SendSmsHelper smsHelper,
            SmsSettings smsSettings,
            UserActivityLogBll userActivityLogService,
            DocFinishBll docFinishService,
            UserConnectionBll userConnectionService,
            EdocBll edocService,
            ResourceBll resourceService,
            Helper.UserSetting helperUserSetting,
            Helper.NotificationHelper notificationHelper,
            AdminGeneralSettings generalSettings,
            NotificationSettings notificationSettings,
            SendEmailHelper mailHelper,
            NotificationBll notificationService,
            MobileDeviceBll mobileDeviceService,
            DocumentContentBll documentContentService,
            CategoryBll categoryService, DocumentsCache documentCache,
            StoreDocBll storeDocService, DocumentHelper documentHelper,
            CitizenBll citizenService,
            FormBll formService,
             DocTypeFormBll doctypeFormService,
             ReportConfigSettings reportConfigSettings)
        {
            _templateHelper = templateHelper;
            _edocService = edocService;
            _userService = userService;
            _workflowService = workflowService;
            _departmentService = departmentService;
            _positionService = positionService;
            _commentService = commentService;
            _docTypeService = docTypeService;
            _documentService = documentService;
            _docCopyService = documentCopyService;
            _supplementaryService = supplementaryService;
            _documentPermissionHelper = documentPermissionHelper;
            _workflowHelper = workflowHelper;
            _codeService = codeService;
            _attachmentService = attachmentService;
            _extensionTimeService = extensionTimeService;
            _storePrivateService = storePrivateService;
            _dailyProcessService = dailyProcessService;
            _storeService = storeService;
            _addressService = addressService;
            _jobTitlesService = jobTitlesService;
            _keyWordService = keyWordService;
            _increaseService = increaseService;
            _documentPublishService = documentPublishService;
            _documentPublishPlusService = documentPublishPlusService;
            _workTimeHelper = workTimeHelper;
            _anticipateService = anticipateService;
            _searchService = searchService;
            _smsHelper = smsHelper;
            _smsSettings = smsSettings;
            _userActivityLogService = userActivityLogService;
            _docFinishService = docFinishService;
            _userConnectionService = userConnectionService;
            _resourceService = resourceService;
            _generalSettings = generalSettings;
            _notificationSettings = notificationSettings;
            _helperUserSetting = helperUserSetting;
            _notificationHelper = notificationHelper;
            _mailHelper = mailHelper;
            _documentContentService = documentContentService;
            _categoryService = categoryService;
            _citizenService = citizenService;
            _notificationService = notificationService;
            _mobileDeviceService = mobileDeviceService;
            _storeDocService = storeDocService;

            _documentCache = documentCache;
            _documentHelper = documentHelper;
            _formService = formService;
            _doctypeFormService = doctypeFormService;
            _reportConfigSettings = reportConfigSettings;

            _hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationHubs>();
        }

        #endregion

        #region Cập nhật hồ sơ, lưu văn bản dự thảo

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public JsonResult SaveDoc(string files, string doc, List<int> removeAttachmentIds, string modifiedFiles)
        {
            try
            {
                var model = Json2.ParseAs<DocumentModel>(doc);
                var documentCopy = UpdateDocumentTransfer(model, files, removeAttachmentIds, modifiedFiles, transferData: null, isCreatingDocument: false, isPhanLoai: false, isSaveDocDraft: true);

                _documentCache.RemoveAll(documentCopy.DocumentCopyId);
            }
            catch (Exception ex)
            {
                LogException(ex);
                return Json(new { error = "Có lỗi xảy ra trong quá trình lưu văn bản. Vui lòng thử lại." });
            }
            return Json(new { success = "Lưu văn bản thành công." });
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public JsonResult SaveDocDraft(string files, string doc)
        {
            try
            {
                var model = Json2.ParseAsJs<DocumentModel>(doc);
                var tempFiles = Json2.ParseAsJs<IDictionary<string, IDictionary<string, string>>>(files);
                var userSendId = CurrentUserId();

                model.Status = (byte)DocumentStatus.DuThao;
                foreach (var r in model.RelationModels)
                {
                    r.IsAddNext = true;
                }

                var newDocumentCopy = _documentHelper.CreateDocumentDefault(model, userSendId, tempFiles);

                return Json(new { success = "Lưu dự thảo thành công.", documentCopyId = newDocumentCopy.DocumentCopyId });
            }
            catch (Exception ex)
            {
                LogException(ex);
                return Json(new { error = "Có lỗi xảy ra khi tạo mới văn bản. Vui lòng thử lại." });
            }
        }

        #endregion

        #region
        [ValidateInput(false)]
        public JsonResult PublishAndFinish(string doc, string files, List<int> removeAttachmentIds, string modifiedFiles,
                                   string destination, int? storePrivateId, string destinationPlan, string jsonFile = "")
        {
            var codeId = 0;
            var isCreatingDocument = false;
            var isDangPhanLoai = false;
            var documentIdTransfering = Guid.Empty;
            var dateNow = DateTime.Now;
            var originDocumentCopyId = 0;
            try
            {
                #region Kiểm tra điều kiện đầu vào

                DestinationModel destinationModel;
                try
                {
                    destinationModel = Json2.ParseAs<DestinationModel>(destination);
                }
                catch (Exception)
                {
                    return Json(new { error = "Thông tin chuyển văn bản không đúng. Vui lòng thử lại." });
                }

                var model = Json2.ParseAs<DocumentModel>(doc);
                isCreatingDocument = model.TransferTypeInEnum == TransferTypes.TaoMoi || model.TransferTypeInEnum == TransferTypes.TraLoi || model.TransferTypeInEnum == TransferTypes.ThuHoi;
                isDangPhanLoai = model.TransferTypeInEnum == TransferTypes.PhanLoai;
                codeId = model.CodeId;

                var categoryBussiness = (CategoryBusinessTypes)model.CategoryBusinessId;
                var isSuccess = model.IsSuccess;

                #endregion

                var comment = model.Comments.Content ?? string.Empty;
                var transferData = ParseTransferData(model.DocumentCopyId, model.DocumentCopyType, destinationModel, comment, isCreatingDocument, isDangPhanLoai);
                originDocumentCopyId = model.DocumentCopyId;

                #region Update Phân loại văn bản
                var hasUpdateDateAppointed = false;

                if (isDangPhanLoai)
                {
                    model = UpdateDocumentWhenChangeDoctype(model, destinationModel, transferData, out hasUpdateDateAppointed);

                    isCreatingDocument = isCreatingDocument || model.HasChangeInoutCode;
                }

                #endregion

                #region Update dữ liệu văn bản đến

                if (categoryBussiness == CategoryBusinessTypes.VbDen)
                {
                    model = UpdateModelForVbDen(model, isCreatingDocument, isDangPhanLoai);
                }

                #endregion

                #region Update Dữ liệu văn bản đi

                if (categoryBussiness == CategoryBusinessTypes.VbDi)
                {
                    model = UpdateModelForVbDi(model);
                }

                #endregion

                #region Update dữ liệu HSMC

                if (categoryBussiness == CategoryBusinessTypes.Hsmc)
                {
                    model = UpdateModelForHsmc(model, transferData);
                }


                #endregion

                using (var trans = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    #region Xác định văn bản gốc: cập nhật hoặc tạo mới văn bản đang thao tác

                    // 2 la chua duyet
                    // 4 da duyet

                    if (transferData.NodeSend.ReturnResult)
                    {
                        model.StatusReport = 4;
                    }
                    else
                    {
                        model.StatusReport = 2;
                    }

                    var documentCopy = UpdateDocumentTransfer(model, files, removeAttachmentIds, modifiedFiles, transferData, isCreatingDocument, isDangPhanLoai, hasUpdateDateAppointed: hasUpdateDateAppointed);

                    var document = documentCopy.Document;

                    #endregion

                    #region Do Transfer

                    var transferResult = new Dictionary<int, DocumentCopy>();

                    // gán tạm diff vào documentCopy để sử dụng, không muốn thay đổi tham số của các function cũ
                    documentCopy.Diff = model.Comments.Diff == null ? string.Empty : JsonConvert.SerializeObject(model.Comments.Diff);

                    if (transferData.UserXlcs.Count == 1)
                    {
                        transferResult = transferResult.Concat(TransferToXlc(documentCopy, transferData)).Where(x => !transferResult.ContainsKey(x.Key)).ToDictionary(x => x.Key, x => x.Value);
                    }
                    else
                    {
                        transferResult = transferResult.Concat(TransferToXlcs(documentCopy, model, transferData).Where(x => !transferResult.ContainsKey(x.Key))).ToDictionary(x => x.Key, x => x.Value);
                    }

                    if (transferData.UserDxls.Any() && model.CategoryBusinessId != 32)
                    {
                        transferResult = transferResult.Concat(TransferToDxls(documentCopy, transferData)).Where(x => !transferResult.ContainsKey(x.Key)).ToDictionary(x => x.Key, x => x.Value);
                    }

                    #endregion Do Transfer

                    #region Xử lý cache

                    // update code đã sử dụng
                    _codeService.AddUsedCache(document.DocumentId, document.DocCode, document.InOutCode, document.CategoryBusinessId, document.Organization, document.StoreId);

                    ResetCache(documentCopy.DocumentCopyId);

                    CacheCategoryAndStore(document.DocTypeId, document.CategoryId, document.StoreId);

                    #endregion

                    #region Phát hành báo cáo
                    if (transferData.NodeSend.ReturnResult)
                    {
                        var clPublish = new PublishController(_codeService, _userService, _addressService, _documentPublishService, _documentPublishPlusService, _documentHelper, _documentCache, _commentService, _docCopyService, _documentPermissionHelper, _attachmentService,
                            _edocService, _anticipateService, _documentService, _mailHelper, _docTypeService, _doctypeFormService, _formService, _departmentService, _generalSettings, _reportConfigSettings);
                        if (document.CategoryBusinessId == 4)
                            clPublish.SendToLgsp(document.DocumentId, document, isJSONDaTa: true);
                    }
                    #endregion

                    trans.Complete();
                }
            }
            catch (Exception ex)
            {
                if (isCreatingDocument || isDangPhanLoai)
                {
                    //Xử lý rollback
                    _codeService.RemoveUsedCache(documentIdTransfering);
                    _codeService.DecreaseDocNumber(codeId);
                }

                LogException(ex);
                return Json(new { error = "Có lỗi trong quá trình chuyển văn bản: " + ex.Message });
            }
            return Json(new { success = "Đã phát hành." });
        }
        #endregion

            #region Transfer

        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public JsonResult TransferDocument(string doc, string files, List<int> removeAttachmentIds, string modifiedFiles,
                                   string destination, int? storePrivateId, string destinationPlan, string jsonFile = "")
        {
            var codeId = 0;
            var isCreatingDocument = false;
            var isDangPhanLoai = false;
            var documentIdTransfering = Guid.Empty;
            var dateNow = DateTime.Now;
            var originDocumentCopyId = 0;
            try
            {
                #region Kiểm tra điều kiện đầu vào

                DestinationModel destinationModel;
                try
                {
                    destinationModel = Json2.ParseAs<DestinationModel>(destination);
                }
                catch (Exception)
                {
                    return Json(new { error = "Thông tin chuyển văn bản không đúng. Vui lòng thử lại." });
                }

                var model = Json2.ParseAs<DocumentModel>(doc);
                var doctext = model.Note;
                Regex regex = new Regex(@"\$\$([A-Za-z0-9\-]+)\$\$");
                Match match = regex.Match(doctext);
                while (match.Success)
                {
                    doctext = doctext.Replace(match.ToString(), "");
                    match = match.NextMatch();
                }
                model.Note = doctext;
                isCreatingDocument = model.TransferTypeInEnum == TransferTypes.TaoMoi || model.TransferTypeInEnum == TransferTypes.TraLoi || model.TransferTypeInEnum == TransferTypes.ThuHoi;
                isDangPhanLoai = model.TransferTypeInEnum == TransferTypes.PhanLoai;
                codeId = model.CodeId;

                var categoryBussiness = (CategoryBusinessTypes)model.CategoryBusinessId;
                var isSuccess = model.IsSuccess;

                #endregion

                var comment = model.Comments.Content ?? string.Empty;
                var transferData = ParseTransferData(model.DocumentCopyId, model.DocumentCopyType, destinationModel, comment, isCreatingDocument, isDangPhanLoai);
                originDocumentCopyId = model.DocumentCopyId;

                #region Update Phân loại văn bản
                var hasUpdateDateAppointed = false;

                if (isDangPhanLoai)
                {
                    model = UpdateDocumentWhenChangeDoctype(model, destinationModel, transferData, out hasUpdateDateAppointed);

                    isCreatingDocument = isCreatingDocument || model.HasChangeInoutCode;
                }

                #endregion

                #region Update dữ liệu văn bản đến

                if (categoryBussiness == CategoryBusinessTypes.VbDen)
                {
                    model = UpdateModelForVbDen(model, isCreatingDocument, isDangPhanLoai);
                }

                #endregion

                #region Update Dữ liệu văn bản đi

                if (categoryBussiness == CategoryBusinessTypes.VbDi)
                {
                    model = UpdateModelForVbDi(model);
                }

                #endregion

                #region Update dữ liệu HSMC

                if (categoryBussiness == CategoryBusinessTypes.Hsmc)
                {
                    model = UpdateModelForHsmc(model, transferData);
                }


                #endregion

                using (var trans = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    #region Xác định văn bản gốc: cập nhật hoặc tạo mới văn bản đang thao tác

                    // 2 la chua duyet
                    // 4 da duyet

                    if (transferData.NodeSend.ReturnResult)
                    {
                        model.StatusReport = 4;
                    }
                    else
                    {
                        model.StatusReport = 2;
                    }

                    var documentCopy = UpdateDocumentTransfer(model, files, removeAttachmentIds, modifiedFiles, transferData, isCreatingDocument, isDangPhanLoai, hasUpdateDateAppointed: hasUpdateDateAppointed);
                  
                    var document = documentCopy.Document;

                    #endregion

                    #region Do Transfer

                    var transferResult = new Dictionary<int, DocumentCopy>();

                    // gán tạm diff vào documentCopy để sử dụng, không muốn thay đổi tham số của các function cũ
                    documentCopy.Diff = model.Comments.Diff == null ? string.Empty : JsonConvert.SerializeObject(model.Comments.Diff);

                    if (transferData.UserXlcs.Count == 1)
                    {
                        transferResult = transferResult.Concat(TransferToXlc(documentCopy, transferData)).Where(x => !transferResult.ContainsKey(x.Key)).ToDictionary(x => x.Key, x => x.Value);
                    }
                    else
                    {
                        transferResult = transferResult.Concat(TransferToXlcs(documentCopy, model, transferData).Where(x => !transferResult.ContainsKey(x.Key))).ToDictionary(x => x.Key, x => x.Value);
                    }

                    if (transferData.UserDxls.Any() && model.CategoryBusinessId != 32)
                    {
                        transferResult = transferResult.Concat(TransferToDxls(documentCopy, transferData)).Where(x => !transferResult.ContainsKey(x.Key)).ToDictionary(x => x.Key, x => x.Value);
                    }

                    #endregion Do Transfer

                    #region Xử lý văn bản liên quan

                    _docCopyService.UpdateRelationUserJoineds(model.RelationModels.Select(r => r.RelationCopyId), transferResult.Keys.ToList());

                    #endregion

                    #region Xử lý văn bản trả lời

                    if (model.TransferTypeInEnum == TransferTypes.TraLoi)
                    {
                        // Nếu là đang tạo mới, và có văn bản liên quan dạng trả lời thì kết thúc văn bản liên quan này
                        // Chỉ được phép có 1 văn bản liên quan dạng trả lời
                        var documentRelationReply = model.RelationModels.Single(t => t.RelationType == (int)RelationTypes.LienQuanTraLoi);

                        var relationCopy = _docCopyService.Get(documentRelationReply.RelationCopyId);
                        if (relationCopy != null)
                        {
                            _docCopyService.AddDocRelations(relationCopy.DocumentCopyId, documentCopy.DocumentCopyId,
                                    relationCopy.Document.DocumentId, documentCopy.Document, RelationTypes.LienQuanTraLoi);

                            if (_generalSettings.FinishOriginalDocumentWhenAnswer && relationCopy.Document.CategoryBusinessIdInEnum == CategoryBusinessTypes.VbDen)
                            {
                                var contentFinish = string.Format("Kết thúc văn bản sau khi đã trả lời bằng văn bản: {0}", model.Compendium.StripDelimiters());
                                _docCopyService.Finish(relationCopy, transferData.DateSend, transferData.UserSend.UserId, contentFinish);
                                _docCopyService.ClearCache(relationCopy.DocumentCopyId);
                            }
                        }
                    }

                    if (!_generalSettings.FinishOriginalDocumentWhenAnswer &&
                            (transferData.NodeReceive.IsSaveRecordAndRelease || transferData.NodeReceive.IsSaveRecordAndInternalRelease))
                    {
                        // Kết thúc văn bản đến khi văn bản đi trả lời chuyển đến node phát hành.

                        var documentRelationReply = model.RelationModels.SingleOrDefault(t => t.RelationType == (int)RelationTypes.LienQuanTraLoi);
                        if (documentRelationReply != null)
                        {
                            var originalDocumentCopy = _docCopyService.Get(documentRelationReply.RelationCopyId);
                            if (originalDocumentCopy != null)
                            {
                                var contentFinish = string.Format("Kết thúc văn bản sau khi đã trả lời bằng văn bản: {0}", model.Compendium);
                                _docCopyService.Finish(originalDocumentCopy, transferData.DateSend, transferData.UserSend.UserId, contentFinish);
                                _docCopyService.ClearCache(originalDocumentCopy.DocumentCopyId);
                            }
                        }
                    }

                    #endregion

                    #region Xử lý văn bản thu hồi

                    if (model.TransferTypeInEnum == TransferTypes.ThuHoi)
                    {
                        var documentRelationReply = model.RelationModels.Single(t => t.RelationType == (int)RelationTypes.LienQuanThuHoi);

                        var relationCopy = _docCopyService.Get(documentRelationReply.RelationCopyId);
                        if (relationCopy != null)
                        {
                            _docCopyService.AddDocRelations(relationCopy.DocumentCopyId, documentCopy.DocumentCopyId,
                                    relationCopy.Document.DocumentId, documentCopy.Document, RelationTypes.LienQuanThuHoi);
                        }
                    }

                    #endregion

                    #region Xử lý đánh lại số đến văn bản gốc

                    if (model.HasChangeInoutCode && originDocumentCopyId != 0)
                    {
                        // Kết thúc văn bản gốc khi đánh lại số đến
                        var originDocumentCopy = _docCopyService.Get(originDocumentCopyId);
                        if (originDocumentCopy != null)
                        {
                            var changeComment = "Đánh lại số đến.";
                            _docCopyService.Finish(originDocumentCopy, transferData.DateSend, transferData.UserSend.UserId, changeComment);
                        }
                    }

                    #endregion

                    #region dự kiến phát hành

                    if (destinationModel.PublishPlanId > 0)
                    {
                        _anticipateService.Update(destinationModel.PublishPlanId, documentCopy.DocumentId, transferData.UserSend.UserId);
                    }

                    #endregion

#if HoSoMotCuaEdition

                    #region Xử lý ký duyệt

                    if (isSuccess != null)
                    {
                        _documentService.UpdateForSigning(documentCopy, transferData.UserSend, isSuccess.Value, "", DateTime.Now);
                    }

                    #endregion

#endif

                    #region Xử lý cache

                    // update code đã sử dụng
                    _codeService.AddUsedCache(document.DocumentId, document.DocCode, document.InOutCode, document.CategoryBusinessId, document.Organization, document.StoreId);

                    ResetCache(documentCopy.DocumentCopyId);

                    CacheCategoryAndStore(document.DocTypeId, document.CategoryId, document.StoreId);

                    #endregion

                    #region Lưu sổ văn bản

                    if (model.StoreId.HasValue && isCreatingDocument)
                    {
                        /* 
						 * Các trường hợp xảy ra lưu sổ:
						 * - Tạo với văn bản đến.
						 * - Phân loại, đánh lại số đến văn bản đến.
						 * - Cấp số trước với văn bản đi.
						 * - Trường hợp phát hành xử lý ở controller phát hành.
						*/

                        var hasExist = isCreatingDocument ? false : _storeDocService.Exist(st => st.StoreId == model.StoreId.Value && st.DocumentId == documentIdTransfering);
                        if (!hasExist)
                        {
                            _storeDocService.Create(new StoreDoc
                            {
                                StoreId = model.StoreId.Value,
                                DocumentId = document.DocumentId
                            });
                        }
                    }

                    #endregion

                    #region Lưu sổ cá nhân

                    if (storePrivateId.HasValue)
                    {
                        var storePrivate = _storePrivateService.CheckPermissionStorePrivate(storePrivateId.Value, CurrentUserId());
                        if (storePrivate != null && storePrivate.Status == (byte)StorePrivateStatus.IsActive)
                        {
                            _storePrivateService.AddDocumentToStore(storePrivate, documentCopy.DocumentCopyId, documentCopy.DocumentId, true);
                        }
                    }

                    #endregion

                    #region Gửi Mail, Sms, Notification

                    #region Gửi mail Trạng thái trả kết quả
                    //là văn phòng 1 cửa                  
                    if (transferData.NodeReceive.ReturnResult && document.IsSuccess.HasValue)
                    {
                        try
                        {
                            // Tự động gửi mail thông báo
                            _mailHelper.SendReturnResultMail(document, transferData.UserSend, documentCopy.DocumentCopyId);                       
                        }
                        catch (Exception ex)
                        {
                            LogException(ex);
                        }

                        try
                        {
                            _smsHelper.SendReturnResultSms(document, transferData.UserSend, documentCopy.DocumentCopyId);
                        }
                        catch (Exception ex)
                        {
                            LogException(ex);
                        }
                    }
                    else
                    {
                        //lay thong tin email va phone cua user dua vao document                 
                        var userRecive = documentCopy.UserCurrentId;
                        var userId_ = _userService.Get(userRecive);

                        var documentEmail_ = userId_.Email;
                       
                        var documentPhone_ = userId_.Phone;
                        
                        document.Email = documentEmail_;
                        document.Phone = documentPhone_;

                        if (documentEmail_ != null && documentPhone_ != null)
                        {
                            try
                            {
                                _mailHelper.SendTranferDocumentMail_(document, transferData.UserSend, documentCopy.DocumentCopyId);
                                //_mailHelper.SendTranferDocumentMail(document, transferData.UserSend, new List<string>() { document.Email }, documentCopy.DocumentCopyId);
                            }
                            catch (Exception ex)
                            {
                                LogException(ex);
                            }

                            try
                            {
                                _smsHelper.SendTranferDocumentSms(document, transferData.UserSend, documentCopy.DocumentCopyId);

                            }
                            catch (Exception ex)
                            {
                                LogException(ex);
                            }
                        }                             
                    }

                    #endregion

                    SaveNotificationToQueue(transferResult, model.Compendium, transferData.DateSend, isCreatingDocument);

                    #endregion

                    #region Phát hành báo cáo
                    if (transferData.NodeSend.ReturnResult)
                    {
                        var clPublish = new PublishController(_codeService, _userService, _addressService, _documentPublishService,_documentPublishPlusService, _documentHelper, _documentCache, _commentService, _docCopyService, _documentPermissionHelper, _attachmentService,
                            _edocService, _anticipateService, _documentService, _mailHelper, _docTypeService, _doctypeFormService, _formService, _departmentService, _generalSettings, _reportConfigSettings);
                        if (document.CategoryBusinessId == 4 || document.CategoryBusinessId == 64)
                            clPublish.SendToLgsp(document.DocumentId, document, isJSONDaTa: true);
                    }
                    #endregion

                    #region Giao kế hoạch
                    if (transferData.NodeSend.Booking)
                    {
                        var departcurents = _userService.CurrentUser.UserDepartmentJobTitless;
                        if (departcurents != null && departcurents.Any())
                        {
                            var userDepartPositions = _departmentService.GetCacheAllUserDepartmentJobTitlesPosition();
                            var departmentChildrens = _departmentService.GetChildrens(departcurents.OrderByDescending(d => d.IsPrimary).FirstOrDefault().DepartmentId);

                            if (departmentChildrens != null && departmentChildrens.Any())
                            {
                                var departChildrenIds = departmentChildrens.Select(d => d.DepartmentId);

                                var users = userDepartPositions.Where(ud => departChildrenIds.Contains(ud.DepartmentId) && ud.HasReceiveDocument);
                                if (users != null && users.Any())
                                {
                                    Announcement(documentCopy.DocumentCopyId, users.Select(u => u.UserId).ToList(), "");
                                }
                            }
                        }
                    }
                    #endregion

                    trans.Complete();
                }
            }
            catch (Exception ex)
            {
                if (isCreatingDocument || isDangPhanLoai)
                {
                    //Xử lý rollback
                    _codeService.RemoveUsedCache(documentIdTransfering);
                    _codeService.DecreaseDocNumber(codeId);
                }

                LogException(ex);
                return Json(new { error = "Có lỗi trong quá trình chuyển văn bản: " + ex.Message });
            }

            return Json(new { success = "Chuyển văn bản thành công." });
        }

        [ValidateInput(false)]
        public JsonResult SurveyReleased(string doc, string files, List<int> removeAttachmentIds, string modifiedFiles,
            string destination, int? storePrivateId, string destinationPlan, string jsonFile = "")
        {
            var codeId = 0;
            var isCreatingDocument = false;
            var isDangPhanLoai = false;
            var documentIdTransfering = Guid.Empty;
            var dateNow = DateTime.Now;
            var originDocumentCopyId = 0;
            try
            {
                #region Kiểm tra điều kiện đầu vào

                DestinationModel destinationModel;
                try
                {
                    destinationModel = Json2.ParseAs<DestinationModel>(destination);
                }
                catch (Exception)
                {
                    return Json(new { error = "Thông tin chuyển văn bản không đúng. Vui lòng thử lại." });
                }

                var model = Json2.ParseAs<DocumentModel>(doc);
                isCreatingDocument = model.TransferTypeInEnum == TransferTypes.TaoMoi || model.TransferTypeInEnum == TransferTypes.TraLoi || model.TransferTypeInEnum == TransferTypes.ThuHoi;
                isDangPhanLoai = model.TransferTypeInEnum == TransferTypes.PhanLoai;
                codeId = model.CodeId;

                var categoryBussiness = (CategoryBusinessTypes)model.CategoryBusinessId;
                var isSuccess = model.IsSuccess;

                #endregion

                var comment = model.Comments.Content ?? string.Empty;
                var transferData = ParseTransferDataSurvey(model.DocumentCopyId, model.DocumentCopyType, destinationModel, comment, isCreatingDocument, isDangPhanLoai);
                originDocumentCopyId = model.DocumentCopyId;

                #region Update Phân loại văn bản
                var hasUpdateDateAppointed = false;

                if (isDangPhanLoai)
                {
                    model = UpdateDocumentWhenChangeDoctype(model, destinationModel, transferData, out hasUpdateDateAppointed);

                    isCreatingDocument = isCreatingDocument || model.HasChangeInoutCode;
                }

                #endregion
                using (var trans = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    #region Xác định văn bản gốc: cập nhật hoặc tạo mới văn bản đang thao tác

                    // 2 la chua duyet
                    // 4 da duyet

                    //if (transferData.NodeSend.ReturnResult)
                    //{
                    model.StatusReport = 4;
                    //}
                    //else
                    //{
                    //    model.StatusReport = 2;
                    //}

                    var documentCopy = UpdateDocumentTransfer(model, files, removeAttachmentIds, modifiedFiles, transferData, isCreatingDocument, isDangPhanLoai, hasUpdateDateAppointed: hasUpdateDateAppointed);

                    var document = documentCopy.Document;

                    #endregion

                    #region Do Transfer

                    var transferResult = new Dictionary<int, DocumentCopy>();

                    // gán tạm diff vào documentCopy để sử dụng, không muốn thay đổi tham số của các function cũ
                    documentCopy.Diff = model.Comments.Diff == null ? string.Empty : JsonConvert.SerializeObject(model.Comments.Diff);

                    //if (transferData.UserXlcs.Count == 1)
                    //{
                    //    transferResult = transferResult.Concat(TransferToXlc(documentCopy, transferData)).Where(x => !transferResult.ContainsKey(x.Key)).ToDictionary(x => x.Key, x => x.Value);
                    //}
                    //else
                    //{
                    //    transferResult = transferResult.Concat(TransferToXlcs(documentCopy, model, transferData).Where(x => !transferResult.ContainsKey(x.Key))).ToDictionary(x => x.Key, x => x.Value);
                    //}

                    if (transferData.UserDxls.Any() && model.CategoryBusinessId == 32)
                    {
                        transferResult = transferResult.Concat(TransferSurveyToDxls(documentCopy, transferData)).Where(x => !transferResult.ContainsKey(x.Key)).ToDictionary(x => x.Key, x => x.Value);
                    }
                    #endregion Do Transfer
                    #region Xử lý văn bản liên quan

                    _docCopyService.UpdateRelationUserJoineds(model.RelationModels.Select(r => r.RelationCopyId), transferResult.Keys.ToList());

                    #endregion
                    #region Xử lý cache

                    ResetCache(documentCopy.DocumentCopyId);

                    #endregion
                    trans.Complete();
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
                return Json(new { error = "Có lỗi trong quá trình phát hành khảo sát: " + ex.Message });
            }
            return Json(new { success = "Phát hành khảo sát thành công." });
        }
        [ValidateInput(false)]
        public JsonResult SurveyComplete(int docId, string data)
        {
            try
            {
                DocumentCopy documentCopy;
                try
                {
                    documentCopy = _docCopyService.Get(docId);
                    if(documentCopy == null)
                        return Json(new { error = "Phiếu khảo sát không tồn tại. Vui lòng thử lại." });
                }
                catch 
                {
                    return Json(new { error = "Phiếu khảo sát không tồn tại. Vui lòng thử lại." });
                }

                using (var trans = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    documentCopy.Note = data;
                    documentCopy.Status = (int) DocumentStatus.KetThuc;
                    _docCopyService.Update(documentCopy);
                    var docCopyParent = _docCopyService.Get(documentCopy.ParentId ?? 0);
                    docCopyParent = docCopyParent ?? documentCopy;
                    _commentService.SendSurvey(docCopyParent, documentCopy.UserCurrentId,0,"",new List<CommentTransfer>(), DateTime.Now, CommentType.CompletedSurvey);
                    #region Xử lý cache

                    ResetCache(documentCopy.DocumentCopyId);

                    #endregion
                    trans.Complete();
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
                return Json(new { error = "Có lỗi trong quá trình hoàn thành khảo sát: " + ex.Message });
            }
            return Json(new { success = "Hoàn thành phiếu khảo sát." });
        }
        [ValidateInput(false)]
        public JsonResult SurveySaveReport(int docId, string data)
        {
            try
            {
                var listId = new List<int>();
                DocumentCopy documentCopy;
                try
                {
                    documentCopy = _docCopyService.Get(docId);
                    if (documentCopy == null)
                        return Json(new { error = "Phiếu khảo sát không tồn tại. Vui lòng thử lại." });
                }
                catch
                {
                    return Json(new { error = "Phiếu khảo sát không tồn tại. Vui lòng thử lại." });
                }

                using (var trans = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    if (documentCopy.ParentId.HasValue)
                    {
                        var docCopyUnits = _docCopyService.Gets(c=>c.ParentId == documentCopy.ParentId);
                        foreach (var item in docCopyUnits)
                        {
                            item.ProcessInfoPlus = data;
                            listId.Add(item.DocumentCopyId);
                            _docCopyService.Update(item);
                        }

                        var doc = _docCopyService.Get(documentCopy.ParentId ?? 0);
                        if (doc != null)
                        {
                            listId.Add(doc.DocumentCopyId);
                            doc.ProcessInfoPlus = data;
                            _docCopyService.Update(doc);
                        }
                    }
                    else
                    {
                        documentCopy.ProcessInfoPlus = data;
                        _docCopyService.Update(documentCopy);
                        var docCopyUnits = _docCopyService.Gets(c => c.ParentId == documentCopy.DocumentCopyId);
                        foreach (var item in docCopyUnits)
                        {
                            item.ProcessInfoPlus = data;
                            listId.Add(item.DocumentCopyId);
                            _docCopyService.Update(item);
                        }
                        listId.Add(documentCopy.DocumentCopyId);
                    }
                    #region Xử lý cache

                    //ResetCache(documentCopy.DocumentCopyId);
                    _documentCache.RemoveAll(listId);

                    #endregion
                    trans.Complete();
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
                return Json(new { error = "Có lỗi trong quá trình hoàn thành khảo sát: " + ex.Message });
            }
            return Json(new { success = "Chỉnh sửa cấu hình báo cáo thành công!" });
        }
        
        private void CacheCategoryAndStore(Guid? doctypeId, int? categoryId, int? storeId)
        {
            // Lưu sổ văn bản và hình thức văn bản gần nhất đã sử dụng theo loại văn bản.
            var userSettings = _helperUserSetting.GetUserCurrentSetting(true);
            userSettings.CategoryId = categoryId;
            if (storeId.HasValue && doctypeId.HasValue)
            {
                userSettings.StoreIds[doctypeId.Value] = storeId.Value;
            }

            _userService.UpdateUserSetting(userSettings.StringifyJs());
        }

        [ValidateAntiForgeryToken]
        public JsonResult TransferAnnouncement(int documentCopyId, List<int> ccUsers, string targetForComments)
        {
            int userSendId;

            ccUsers = ccUsers.Distinct().ToList();
            if (ccUsers == null || !ccUsers.Any())
            {
                return Json(new { error = "Chưa chọn cán bộ thông báo, vui lòng chọn cán bộ và gửi lại." });
            }
            var documentCopy = _docCopyService.Get(documentCopyId);
            if (documentCopy == null)
            {
                return Json(new { error = "Văn bản/hồ sơ không tồn tại." });
            }

            if (!_documentPermissionHelper.CheckForUyQuyenVaXuLyVanBan(documentCopy, CurrentUserId(), out userSendId) ||
                _documentPermissionHelper.Check(documentCopy, userSendId, DocumentPermissions.ThongBao) != DocumentPermissions.ThongBao)
            {
                userSendId = CurrentUserId();
                if (!_documentPermissionHelper.CheckForQuyenXem(documentCopy, userSendId))
                {
                    return Json(new { error = "Không có quyền gửi thông báo." });
                }
            }

            try
            {
                var commentTransfers = Json2.ParseAs<List<CommentTransfer>>(targetForComments);
                using (var trans = new TransactionScope(TransactionScopeOption.Required))
                {
                    var dateCreated = DateTime.Now;
                    _docCopyService.UpdateUserThongBao(documentCopy, ccUsers, hasSaveChange: true);
                    _commentService.SendTransfer(documentCopy, userSendId, 0, "", commentTransfers, dateCreated, GetAuthorizeComment(userSendId));

                    #region Notification

                    PushNotifyMessage(ccUsers, documentCopy, dateCreated);

                    #endregion

                    #region Xử lý cache

                    ResetCache(documentCopy.DocumentCopyId);

                    #endregion

                    trans.Complete();
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
                return Json(new { error = "Có lỗi xảy ra trong quá trình gửi thông báo. Vui lòng thử lại." });
            }

            return Json(new { success = "Đã gửi văn bản thông báo." });
        }

        private bool Announcement(int documentCopyId, List<int> ccUsers, string targetForComments)
        {
            int userSendId;
            ccUsers = ccUsers.Distinct().ToList();
            var documentCopy = _docCopyService.Get(documentCopyId);
            if (documentCopy == null)
            {
                return false;
            }
            _documentPermissionHelper.CheckForUyQuyenVaXuLyVanBan(documentCopy, CurrentUserId(), out userSendId);

            try
            {
                //var commentTransfers = Json2.ParseAs<List<CommentTransfer>>(targetForComments);
                using (var trans = new TransactionScope(TransactionScopeOption.Required))
                {
                    var dateCreated = DateTime.Now;
                    _docCopyService.UpdateUserThongBao(documentCopy, ccUsers, hasSaveChange: true);
                    //_commentService.SendTransfer(documentCopy, userSendId, 0, "", commentTransfers, dateCreated, GetAuthorizeComment(userSendId));

                    #region Notification

                    PushNotifyMessage(ccUsers, documentCopy, dateCreated);

                    #endregion

                    #region Xử lý cache

                    ResetCache(documentCopy.DocumentCopyId);

                    #endregion

                    trans.Complete();
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
                return false;
            }

            return true;
        }

        [ValidateAntiForgeryToken]
        public JsonResult TransferConsult(string doc, string files, List<int> removeAttachmentIds,
                                                  string modifiedFiles, List<int> usersConsult, string contentRequest, string targetForComments)
        {
            var model = Json2.ParseAsJs<DocumentModel>(doc);
            var categoryBussiness = (CategoryBusinessTypes)model.CategoryBusinessId;
            var isCreatingDocument = false;
            var isChangeDoctyping = false;
            var isTransfering = false;

            var docCopy = _docCopyService.Get(model.DocumentCopyId);
            var destination = new DestinationModel()
            {
                CurrentNodeId = docCopy.NodeCurrentId ?? 0,
                NextNodeId = docCopy.NodeCurrentId ?? 0,
                TargetComment = targetForComments,
                WorkflowId = docCopy.WorkflowId,
                UserIdXlc = CurrentUserId()
            };

            var transferData = ParseTransferData(model.DocumentCopyId, model.DocumentCopyType, destination, contentRequest, isCreatingDocument, isChangeDoctyping);

            #region Update dữ liệu văn bản đến

            if (categoryBussiness == CategoryBusinessTypes.VbDen)
            {
                model = UpdateModelForVbDen(model, isCreatingDocument, isChangeDoctyping);
            }

            #endregion

            #region Update Dữ liệu văn bản đi

            if (categoryBussiness == CategoryBusinessTypes.VbDi)
            {
                model = UpdateModelForVbDi(model);
            }

            #endregion

            var transferResult = new Dictionary<int, DocumentCopy>();
            using (var trans = new TransactionScope(TransactionScopeOption.Required))
            {
                // xác định văn bản gốc: cập nhật hoặc tạo mới văn bản đang thao tác
                var documentCopy = UpdateDocumentTransfer(model, files, removeAttachmentIds, modifiedFiles, null, isCreatingDocument, isChangeDoctyping);

                var comment = _commentService.SendTransfer(documentCopy, transferData.UserSend.UserId, null, contentRequest,
                                                        transferData.CommentTransfer, transferData.DateSend);

                foreach (var userReceiveId in usersConsult)
                {
                    var newDocumentCopy = _docCopyService.CreateSpecial(documentCopy, transferData.NodeSend, transferData.UserSend.UserId, userReceiveId,
                                                transferData.DateSend, DocumentCopyTypes.XinYKien, DocumentStatus.DangXuLy, new List<DocRelation>(),
                                                isTransfering, contentRequest, transferData.UserSend.FullName);

                    _commentService.SendTransfer2(newDocumentCopy, userReceiveId, contentRequest, transferData.CommentTransfer, transferData.DateSend,
                                        transferData.AuthorizeComment, comment.CommentId);

                    transferResult.Add(userReceiveId, newDocumentCopy);
                }

                ResetCache(documentCopy.DocumentCopyId);

                SaveNotificationToQueue(transferResult, model.Compendium, transferData.DateSend, isCreatingDocument);

                trans.Complete();
            }

            return Json(new { success = "Đã gửi xin ý kiến." });
        }

        [ValidateAntiForgeryToken]
        public JsonResult TransferMultiple(List<int> documentCopyIds, string destination, string modifyAttachments, string removeAttachmentIds,
                                string files, string comments)
        {
            var documentIdTransfering = Guid.Empty;
            var dateNow = DateTime.Now;
            var userSend = _userService.CurrentUser;

            #region Kiểm tra điều kiện đầu vào

            DestinationModel destinationModel;
            try
            {
                destinationModel = Json2.ParseAs<DestinationModel>(destination);
            }
            catch
            {
                return Json(new { error = "Thông tin chuyển văn bản không đúng. Vui lòng thử lại." });
            }

            Dictionary<int, string> transferComments = null;
            try
            {
                transferComments = Json2.ParseAs<Dictionary<int, string>>(comments);
            }
            catch
            {
                return Json(new { error = "Thông tin chuyển văn bản không đúng. Vui lòng thử lại." });
            }

            Dictionary<int, IDictionary<int, string>> modifiedFiles = null;
            if (!string.IsNullOrEmpty(modifyAttachments))
            {
                try
                {
                    modifiedFiles = Json2.ParseAs<Dictionary<int, IDictionary<int, string>>>(modifyAttachments);
                }
                catch
                {
                    return Json(new { error = "Thông tin chuyển văn bản không đúng. Vui lòng thử lại." });
                }
            }

            Dictionary<int, IDictionary<string, IDictionary<string, string>>> newFiles = null;
            if (!string.IsNullOrEmpty(files))
            {
                try
                {
                    newFiles = Json2.ParseAs<Dictionary<int, IDictionary<string, IDictionary<string, string>>>>(files);
                }
                catch
                {
                    return Json(new { error = "Thông tin chuyển văn bản không đúng. Vui lòng thử lại." });
                }
            }

            Dictionary<int, List<int>> removeAttachments = null;
            if (!string.IsNullOrEmpty(removeAttachmentIds))
            {
                try
                {
                    removeAttachments = Json2.ParseAs<Dictionary<int, List<int>>>(removeAttachmentIds);
                }
                catch
                {
                    return Json(new { error = "Thông tin chuyển văn bản không đúng. Vui lòng thử lại." });
                }
            }

            var documentCopies = _docCopyService.Gets(documentCopyIds, true);
            if (!documentCopies.Any())
            {
                return Json(new { error = "Văn bản không tồn tại, vui lòng thử lại." });
            }

            #endregion

            using (var trans = new TransactionScope())
            {
                foreach (var documentCopy in documentCopies)
                {
                    var transferResult = new Dictionary<int, DocumentCopy>();
                    var documentCopyId = documentCopy.DocumentCopyId;
                    var comment = transferComments == null ? "" : transferComments[documentCopyId];

                    var transferData = ParseTransferData(documentCopyId, documentCopy.DocumentCopyType, destinationModel, comment, isCreatingDocument: false, isDangPhanLoai: false);
                    var compendium = documentCopy.Document.Compendium;

                    if (modifiedFiles != null || newFiles != null || removeAttachments != null)
                    {
                        var modifyFiles = modifiedFiles[documentCopyId];
                        var newAttachments = newFiles[documentCopyId];
                        var removeFiles = removeAttachments[documentCopyId];

                        var hasChangeAttachment = HasChangeAttachmentPermission(documentCopy.ParentId);
                        _documentService.UpdateAttachments(documentCopy.Document, modifiedAttachment: modifyFiles, userUpdate: transferData.UserSend,
                                                newAttachments: newAttachments, removeAttachmentIds: removeFiles, hasChangeAttachment: hasChangeAttachment, hasSaveChange: true);
                    }

                    #region Do Transfer

                    if (transferData.UserXlcs.Count == 1)
                    {
                        transferResult = transferResult.Concat(TransferToXlc(documentCopy, transferData)).ToDictionary(x => x.Key, x => x.Value);
                    }
                    else
                    {
                        // transferResult = transferResult.Concat(TransferToOnlyDxls(documentCopy, model, transferData)).ToDictionary(x => x.Key, x => x.Value);
                    }

                    if (transferData.UserDxls.Any())
                    {
                        transferResult = transferResult.Concat(TransferToDxls(documentCopy, transferData)).ToDictionary(x => x.Key, x => x.Value);
                    }

                    #endregion Do Transfer

                    #region Kết thúc văn bản đến

                    if (!_generalSettings.FinishOriginalDocumentWhenAnswer &&
                            (transferData.NodeReceive.IsSaveRecordAndRelease || transferData.NodeReceive.IsSaveRecordAndInternalRelease))
                    {
                        // Kết thúc văn bản đến khi văn bản đi trả lời chuyển đến node phát hành.
                        var documentRelationReply = _documentService.GetDocRelations(r => r.DocumentCopyId == documentCopy.DocumentCopyId && r.RelationType == (int)RelationTypes.LienQuanTraLoi).SingleOrDefault();
                        if (documentRelationReply != null)
                        {
                            var originalDocumentCopy = _docCopyService.Get(documentRelationReply.RelationCopyId);
                            if (originalDocumentCopy != null)
                            {
                                var contentFinish = string.Format("Kết thúc văn bản sau khi đã trả lời bằng văn bản: {0}", compendium);
                                _docCopyService.Finish(originalDocumentCopy, transferData.DateSend, transferData.UserSend.UserId, contentFinish);
                            }
                        }
                    }

                    #endregion

                    #region Xử lý cache

                    ResetCache(documentCopy.DocumentCopyId);

                    #endregion

                    #region Notification

                    SaveNotificationToQueue(transferResult, compendium, transferData.DateSend, isCreatingDocument: false);

                    #endregion
                }

                trans.Complete();
            }

            return Json(new { success = "Chuyển văn bản thành công." });
        }

        [ValidateAntiForgeryToken]
        public JsonResult TransferAnswer(int documentCopyId, string files, string doc, List<int> removeAttachmentIds,
                                                  string modifiedFiles, string actionSpecial)
        {
            DocumentModel model;
            string comment;

            #region Kiểm tra quyền

            var transferAction = EnumHelper<ActionSpecial>.Parse(actionSpecial);
            if (transferAction != ActionSpecial.ChuyenYKienDongGopVbXinYKien &&
                transferAction != ActionSpecial.ChuyenYKienDongGopVbDxl)
            {
                return Json(new { success = "" });
            }

            model = Json2.ParseAsJs<DocumentModel>(doc);
            comment = model.Comments.Content ?? string.Empty;

            var documentCopy = _docCopyService.Get(documentCopyId);
            int userSendId;
            if (!_documentPermissionHelper.CheckForUyQuyenVaXuLyVanBan(documentCopy, CurrentUserId(), out userSendId) ||
                _documentPermissionHelper.Check(documentCopy, userSendId, DocumentPermissions.TraLoiYKien) != DocumentPermissions.TraLoiYKien)
            {
                return Json(new { error = "Không có quyền Trả lời ý kiến đóng góp." });
            }

            #endregion

            using (var trans = new TransactionScope(TransactionScopeOption.Required))
            {
                var dateCreated = DateTime.Now;

                var tempFiles = Json2.ParseAs<IDictionary<string, IDictionary<string, string>>>(files);
                var modifiedAttachment = Json2.ParseAs<IDictionary<int, string>>(modifiedFiles);

                // Cập nhật văn bản xin ý kiến, attachment và relation được cập nhật theo documentId nên sẽ lên hướng chính luôn.
                var hasChangeAttachment = HasChangeAttachmentPermission(documentCopy.ParentId);
                _documentHelper.UpdateDocumentDefault(model, tempFiles, null, modifiedAttachment, userSendId, false, hasChangeAttachment);

                var contentAuthorize = GetAuthorizeComment(userSendId);

                // Kết thúc văn bản dxl, xyk hiện tại

                //Id của người dùng xử lý chính
                int receivedUserId;
                var userFinish = _userService.GetFromCache(userSendId);
                var contentFinish = string.Format("Văn bản này được kết thúc bởi {0}. Thời gian kết thúc: {1}", userFinish.FullName, dateCreated.ToString("dd/MM/yyyy HH:mm:ss"));

                _docCopyService.SendCommentToParent(documentCopy, userSendId, comment, dateCreated, out receivedUserId, contentFinish, contentAuthorize: contentAuthorize);

                #region Xử lý cache

                ResetCache(documentCopy.DocumentCopyId);

                #endregion

                // Gửi notify đến người nhận (là người đang giữ ở hướng chính).
                var userReceivedIds = new List<int>() { receivedUserId };
                PushNotifyMessage(userReceivedIds, documentCopy, dateCreated);

                trans.Complete();
            }

            CreateActivityLog(ActivityLogType.ChuyenYKienDongGop, string.Format("{0} chuyển ý kiến đóng góp cho văn bản: {1}", User.GetUserNameWithDomain(), documentCopy.Document.Compendium));
            return Json(new { success = "Thao tác thực hiện thành công." });
        }

        private Dictionary<int, DocumentCopy> TransferToXlc(DocumentCopy documentCopy, TransferData transferData)
        {
            var result = new Dictionary<int, DocumentCopy>();
            if (!transferData.UserXlcs.Any()) return result;

            var userReceiveId = transferData.UserReceiveId;
            var userXlc = _userService.GetFromCache(userReceiveId);
            if (userXlc == null)
            {
                throw new Exception("Người nhận không tồn tại, vui lòng thử lại.");
            }

            var userSendId = transferData.UserSend.UserId;

            // Cập nhật thông tin người giữ
            documentCopy.UserSendId = userSendId;
            documentCopy.UserCurrentId = userReceiveId;
            documentCopy.UserCurrentName = userXlc.Username;
            documentCopy.NodeCurrentId = transferData.NodeReceive.Id;
            documentCopy.NodeCurrentName = transferData.NodeReceive.NodeName;
            documentCopy.CurrentDepartmentName = _departmentService.GetPrimaryDepartmentName(userReceiveId);
            documentCopy.NodeCurrentPermission = (int)transferData.NodeReceive.GetNodePermission();
            documentCopy.DateReceived = transferData.DateSend;

            if (documentCopy.HasJustCreated == true && transferData.UserSend.UserId != userReceiveId)
            {
                documentCopy.HasJustCreated = false;
            }

            _docCopyService.UpdateUserThamGia(documentCopy, new List<int>() { userReceiveId });

            // Bỏ người nhận ra khỏi danh sách đã xem
            _docCopyService.UpdateUserDaXem(documentCopy, new List<int>(), removedUserIds: new List<int>() { userReceiveId });

            _docCopyService.UpdateForTransfering(documentCopy, transferData.NodeSend, userSendId, transferData.NodeReceive,
                userReceiveId, transferData.DateSend, null, documentCopy.DateOverdue, isConverted: false);

            if (userSendId == userReceiveId)
            {
                _commentService.SendComment(documentCopy, userSendId, transferData.Comment, transferData.DateSend, transferData.AuthorizeComment);
            }
            else
            {
                _commentService.SendTransfer(documentCopy, userSendId, userReceiveId,
                                transferData.Comment, transferData.CommentTransfer, transferData.DateSend, transferData.AuthorizeComment, documentCopy.DateOverdue);
            }

            result.Add(userReceiveId, documentCopy);
            return result;
        }

        private Dictionary<int, DocumentCopy> TransferToDxls(DocumentCopy documentCopy, TransferData transferData)
        {
            var result = new Dictionary<int, DocumentCopy>();

            var userReceivedIds = transferData.UserDxls.Distinct();
            if (!userReceivedIds.Any()) return result;

            var documentCopyTypes = DocumentCopyTypes.DongXuLy;
            var nodeReceive = transferData.NodeReceive;
            var receiveNodeUsers = _workflowHelper.GetNodeUsers(nodeReceive);
            var workFlow = nodeReceive.Parent;
            var parentId = documentCopy.DocumentCopyId;

            foreach (var userReceiveId in userReceivedIds)
            {
                var exactNodeReceive = nodeReceive;
                if (!receiveNodeUsers.Contains(userReceiveId))
                {
                    // Kiểm tra trường hợp nếu người đồng gửi không thuộc node đến
                    // => Tìm trong quy trình lấy ra những node người đó thuộc vào và ưu tiên lấy node có quyền khởi tạo
                    var nodes = _workflowHelper.GetNodes(workFlow, userReceiveId).OrderBy(n => n.IsStart);
                    if (nodes.Any())
                    {
                        exactNodeReceive = nodes.First();
                    }
                }

                var newDocumentCopy = _docCopyService.Create(documentCopy.DocumentId, documentCopy.DocTypeId, transferData.NodeSend,
                                                                transferData.UserSend.UserId, exactNodeReceive, userReceiveId,
                                                                parentId, transferData.DateSend, documentCopyTypes,
                                                                DocumentStatus.DangXuLy, null, documentCopy.DateOverdue);

                result.Add(userReceiveId, newDocumentCopy);
            }

            return result;
        }
        private Dictionary<int, DocumentCopy> TransferSurveyToDxls(DocumentCopy documentCopy, TransferData transferData)
        {
            var result = new Dictionary<int, DocumentCopy>();

            var userReceivedIds = transferData.UserDxls.Distinct();
            var userReceiveIds = userReceivedIds.ToList();
            if (!userReceiveIds.Any()) return result;

            var documentCopyTypes = DocumentCopyTypes.DongXuLy;
            transferData.NodeSend = _workflowHelper.GetNode(transferData.Workflow.WorkflowId, documentCopy.NodeCurrentId ?? 0);

            transferData.NodeReceive = _workflowHelper.GetNode(transferData.Workflow.WorkflowId, documentCopy.NodeCurrentId ?? 0);
            var nodeReceive = transferData.NodeReceive;
            var receiveNodeUsers = nodeReceive != null ?_workflowHelper.GetNodeUsers(nodeReceive) : null;
            var workFlow = nodeReceive?.Parent;
            var parentId = documentCopy.DocumentCopyId;
            var iUserReceiveId = transferData.UserReceiveId;
            _commentService.SendSurvey(documentCopy, transferData.UserSend.UserId, iUserReceiveId, "", transferData.CommentTransfer, transferData.DateSend, CommentType.ReleasedSurvey, transferData.AuthorizeComment, documentCopy.DateOverdue);
            foreach (var userReceiveId in userReceiveIds)
            {
                var exactNodeReceive = nodeReceive;
                if (receiveNodeUsers != null && !receiveNodeUsers.Contains(userReceiveId))
                {
                    // Kiểm tra trường hợp nếu người đồng gửi không thuộc node đến
                    // => Tìm trong quy trình lấy ra những node người đó thuộc vào và ưu tiên lấy node có quyền khởi tạo
                    var nodes = _workflowHelper.GetNodes(workFlow, userReceiveId).OrderBy(n => n.IsStart);
                    if (nodes.Any())
                    {
                        exactNodeReceive = nodes.First();
                    }
                }

                var newDocumentCopy = _docCopyService.Create(documentCopy.DocumentId, documentCopy.DocTypeId, transferData.NodeSend,
                    transferData.UserSend.UserId, exactNodeReceive, userReceiveId,
                    parentId, transferData.DateSend, documentCopyTypes,
                    DocumentStatus.DangXuLy, null, documentCopy.DateOverdue, true);

                result.Add(userReceiveId, newDocumentCopy);
            }

            return result;
        }

        private Dictionary<int, DocumentCopy> TransferToXlcs(DocumentCopy documentCopy, DocumentModel model, TransferData transferData)
        {
            var document = documentCopy.Document;
            var result = new Dictionary<int, DocumentCopy>();

            documentCopy.Status = (int)DocumentStatus.KetThuc;

            document.Status = (int)DocumentStatus.KetThuc;
            document.DateFinished = transferData.DateSend;
            documentCopy.DateFinished = transferData.DateSend;

            foreach (var userId in transferData.UserXlcs)
            {
                var newModel = UpdateModelForCopy(model, transferData, userId);
                var newDocumentCopy = _documentHelper.CreateDocumentDefault(newModel, transferData.UserSend.UserId, null, transferData.NodeSend);

                // Link sang document gốc
                newDocumentCopy.ParentId = documentCopy.DocumentCopyId;
                newDocumentCopy.DocumentCopyParentPath = string.Format("{0}{1}\\", documentCopy.DocumentCopyParentPath, documentCopy.DocumentCopyId);

                var newDocument = newDocumentCopy.Document;

                var attachments = CopyAttachments(document.Attachments, newDocumentCopy.DocumentId, transferData.UserSend);

                newDocument.Attachments = attachments;

                transferData.UserReceiveId = userId;
                result = result.Concat(TransferToXlc(newDocumentCopy, transferData)).ToDictionary(x => x.Key, x => x.Value);
            }

            return result;
        }

        private DocumentModel UpdateModelForCopy(DocumentModel model, TransferData transferData, int userReceiveId)
        {
            var newModel = Json2.ParseAs<DocumentModel>(Json2.Stringify(model)); // clone
            var nodeReceive = transferData.NodeReceive;

            newModel.DocumentId = Guid.NewGuid();
            newModel.DocumentCopyId = 0;
            newModel.Status = (int)DocumentStatus.DangXuLy;

            // Trường hợp nhân bản văn bản liên thông đến thì những văn bản nhân ko dc tính là liên thông.
            newModel.Original = 1;

            return newModel;
        }

        private TransferData ParseTransferData(int documentCopyId, int documentCopyType, DestinationModel destinationModel, string comment, bool isCreatingDocument, bool isDangPhanLoai)
        {
            var result = new TransferData();
            var isUpdating = !isCreatingDocument && !isDangPhanLoai;

            #region Kiểm tra quyền bàn giao

            var currentUserId = CurrentUserId();
            var workFlow = _workflowService.GetFromCache(destinationModel.WorkflowId);
            var startNodes = _workflowHelper.GetStartNodes(workFlow, currentUserId);

            var userSendId = currentUserId;
            if (!HasDocumentTransferPermision(documentCopyId, startNodes, isUpdating, out userSendId))
            {
                throw new Exception("Không có quyền xử lý văn bản.");
            }

            #endregion

            result.Workflow = workFlow;
            result.NodeSend = isUpdating
                            ? _workflowHelper.GetNode(destinationModel.WorkflowId, destinationModel.CurrentNodeId)
                            : startNodes.First();

            result.NodeReceive = _workflowHelper.GetNode(destinationModel.WorkflowId, destinationModel.NextNodeId);
            result.UserSend = _userService.GetFromCache(userSendId);
            result.UserAuthorize = userSendId != currentUserId ? _userService.GetFromCache(currentUserId) : null;

            #region Kiểm tra điều kiện nơi nhận

            if (result.NodeReceive.GetNodePermission().HasFlag(NodePermissions.QuyenDungXuLy) ||
                result.NodeReceive.GetNodePermission().HasFlag(NodePermissions.QuyenTiepNhanBoSung))
            {
                if (!isCreatingDocument && documentCopyType != (int)DocumentCopyTypes.XuLyChinh)
                {
                    throw new Exception("Bản sao văn bản không có quyền yêu cầu Dừng xử lý/Bổ sung");
                }
            }

            #endregion

            #region Tiền xử lý danh sách cán bộ nhận văn bản

            if (destinationModel.UserIdXlc.HasValue)
            {
                // Có chọn người xử lý chính
                result.UserXlcs.Add(destinationModel.UserIdXlc.Value);
                result.UserReceiveId = destinationModel.UserIdXlc.Value;
                result.UserDxls.AddRange(destinationModel.UserIdsDxl);
            }
            else
            {
                // Gửi toàn xử lý chính
                result.UserXlcs.AddRange(destinationModel.UserIdsDxl);
            }

            result.UserTbs = destinationModel.UserTb;
            result.UserDxls.AddRange(ParseUserDgs(destinationModel.CommentTransfer)); // Chọn đồng gửi bất kỳ

            #endregion

            #region Xử lý ý kiến bàn giao

            List<CommentTransfer> commentTransfer;
            if (destinationModel.CommentTransfer != null && destinationModel.CommentTransfer.Any())
            {
                commentTransfer = destinationModel.CommentTransfer;
            }
            else
            {
                if (!result.UserXlcs.Any())
                {
                    LogException("Lỗi các hướng chuyển fix cứng mà không có người nhận (trả người gửi, Trả người khởi tạo, Trả người có quyền cho ý kiến đóng góp). ");
                    throw new Exception("Lỗi khi thực hiện bàn giao văn bản. Vui lòng thử lại.");
                }

                commentTransfer = NewCommentTranferList(result.UserXlcs.First(), "1", "");
            }

            // Xử lý chọn trùng người khi chuyển.
            result.UserDxls = result.UserDxls.Where(u => !result.UserXlcs.Contains(u)).Distinct().ToList();

            result.CommentTransfer = commentTransfer;
            result.CommentTransferText = _documentHelper.GetCommentTransferText(commentTransfer);
            result.Comment = comment;
            result.AuthorizeComment = GetAuthorizeComment(userSendId);

            #endregion

            return result;
        }
        private TransferData ParseTransferDataSurvey(int documentCopyId, int documentCopyType, DestinationModel destinationModel, string comment, bool isCreatingDocument, bool isDangPhanLoai)
        {
            var result = new TransferData();
            try
            {

                var isUpdating = !isCreatingDocument && !isDangPhanLoai;

                #region Kiểm tra quyền bàn giao

                var currentUserId = CurrentUserId();
                var workFlow = _workflowService.GetFromCache(destinationModel.WorkflowId);
                var startNodes = _workflowHelper.GetStartNodes(workFlow, currentUserId);

                var userSendId = currentUserId;
                if (!HasDocumentTransferPermision(documentCopyId, startNodes, isUpdating, out userSendId))
                {
                    throw new Exception("Không có quyền xử lý văn bản.");
                }

                #endregion

                result.Workflow = workFlow;
                //result.NodeSend = isUpdating
                //                ? _workflowHelper.GetNode(workFlow.WorkflowId, destinationModel.CurrentNodeId)
                //                : startNodes.First();

                //result.NodeReceive = _workflowHelper.GetNode(destinationModel.WorkflowId, destinationModel.NextNodeId);
                result.UserSend = _userService.GetFromCache(userSendId);
                result.UserAuthorize = userSendId != currentUserId ? _userService.GetFromCache(currentUserId) : null;

                #region Kiểm tra điều kiện nơi nhận

                //if (result.NodeReceive.GetNodePermission().HasFlag(NodePermissions.QuyenDungXuLy) ||
                //    result.NodeReceive.GetNodePermission().HasFlag(NodePermissions.QuyenTiepNhanBoSung))
                //{
                //    if (!isCreatingDocument && documentCopyType != (int)DocumentCopyTypes.XuLyChinh)
                //    {
                //        throw new Exception("Bản sao văn bản không có quyền yêu cầu Dừng xử lý/Bổ sung");
                //    }
                //}

                #endregion

                #region Tiền xử lý danh sách cán bộ nhận văn bản

                if (destinationModel.UserIdXlc.HasValue)
                {
                    // Có chọn người xử lý chính
                    result.UserXlcs.Add(destinationModel.UserIdXlc.Value);
                    result.UserReceiveId = destinationModel.UserIdXlc.Value;
                    result.UserDxls.AddRange(destinationModel.UserIdsDxl);
                }
                else
                {
                    // Gửi toàn xử lý chính
                    result.UserXlcs.AddRange(destinationModel.UserIdsDxl);
                }

                result.UserTbs = destinationModel.UserTb;
                result.UserDxls.AddRange(ParseUserDvns(destinationModel.CommentTransfer)); // Chọn đơn vị nhận bất kỳ

                #endregion

                #region Xử lý ý kiến bàn giao

                List<CommentTransfer> commentTransfer;
                if (destinationModel.CommentTransfer != null && destinationModel.CommentTransfer.Any())
                {
                    commentTransfer = destinationModel.CommentTransfer;
                }
                else
                {
                    if (!result.UserXlcs.Any())
                    {
                        LogException("Lỗi các hướng chuyển fix cứng mà không có người nhận (trả người gửi, Trả người khởi tạo, Trả người có quyền cho ý kiến đóng góp). ");
                        throw new Exception("Lỗi khi thực hiện bàn giao văn bản. Vui lòng thử lại.");
                    }

                    commentTransfer = NewCommentTranferList(result.UserXlcs.First(), "1", "");
                }

                // Xử lý chọn trùng người khi chuyển.
                result.UserDxls = result.UserDxls.Where(u => !result.UserXlcs.Contains(u)).Distinct().ToList();

                result.CommentTransfer = commentTransfer;
                result.CommentTransferText = _documentHelper.GetCommentTransferText(commentTransfer);
                result.Comment = comment;
                result.AuthorizeComment = GetAuthorizeComment(userSendId);

                #endregion

            }
            catch (Exception ex)
            {
                LogException(ex);
            }
            return result;

        }
        private string GetAuthorizeComment(int userSendId)
        {
            var currentUser = _userService.CurrentUser;
            var result = userSendId != currentUser.UserId
                        ? string.Format("Xử lý ủy quyền:{0} ({1})", currentUser.FullName, currentUser.Username)
                        : string.Empty;

            return result;
        }

        private List<Attachment> CopyAttachments(IEnumerable<Attachment> docAttachments, Guid newDocumentId, User userCreate)
        {
            var result = docAttachments.Where(a => !a.IsDeleted)
                    .Select(a =>
                    {
                        var ad = a.AttachmentDetails.Last();
                        var newAtt = new Attachment()
                        {
                            DocumentId = newDocumentId,
                            AttachmentName = a.AttachmentName,
                            Size = a.Size,
                            VersionAttachment = 1
                        };

                        newAtt.AttachmentDetails.Add(new AttachmentDetail()
                        {
                            FileName = ad.FileName,
                            CreatedByUserId = userCreate.UserId,
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

            return result;
        }

        private bool HasDocumentTransferPermision(int DocumentCopyId, IEnumerable<Node> startNodes, bool isUpdating, out int userSendId)
        {
            userSendId = CurrentUserId();
            if (!isUpdating)
            {
                if (!startNodes.Any())
                {
                    return false;
                }
            }
            else
            {
                var documentCopy = _docCopyService.Get(DocumentCopyId);
                if (!_documentPermissionHelper.CheckForUyQuyenVaXuLyVanBan(documentCopy, CurrentUserId(), out userSendId))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Xử lý model cho văn bản đang phân loại
        /// </summary>
        /// <param name="model"></param>
        /// <param name="destinationModel"></param>
        /// <param name="transferData"></param>
        /// <returns></returns>
        /// <remarks>
        /// - Phân loại văn bản đến liên thông, phát hành: văn bản đến đơn vị chưa thuộc loại văn bản nào.
        /// - Phân loại văn bản đến thông thường: văn bản chuyển xlc, dxl từ cấp trên xuống các đơn vị.
        /// - Đánh lại số đến văn bản
        /// </remarks>
        private DocumentModel UpdateDocumentWhenChangeDoctype(DocumentModel model, DestinationModel destinationModel, TransferData transferData, out bool hasUpdateDateAppointed)
        {
            hasUpdateDateAppointed = false;

            if (model.CategoryBusinessId == (int)CategoryBusinessTypes.VbDi) return model;

            var hasDoctype = !model.DocTypeId.Equals(Guid.Empty);
            var isLienThong = model.Original == 2;
            var codeId = model.CodeId;
            var dateNow = DateTime.Now;
            var hasChangeExpireProcess = model.ChangeExpireProcess || !hasDoctype;

            // Phân loại văn bản liên thông, phát hành đến.
            if (!hasDoctype)
            {
                // Không đánh lại số đến đối với văn bản liên thông, phát hành đển nhưng chưa phân loại
                model.HasChangeInoutCode = false;

                model.DocTypeId = destinationModel.NewDocTypeId;
                model.DocumentCopyModel.DocTypeId = destinationModel.NewDocTypeId;
                model.UserCreatedId = transferData.UserSend.UserId;
                model.UserCreatedName = transferData.UserSend.FullName;
            }

            if (model.HasChangeInoutCode && model.CategoryBusinessId == (int)CategoryBusinessTypes.VbDen)
            {
                /*
				 Đánh lại số đến xảy ra khi văn thư các đơn vị nhận dc văn bản từ Sở và vào lại sổ của đơn vị.
				 */

                model.DocTypeId = destinationModel.NewDocTypeId;
                model.DocumentCopyModel.DocTypeId = destinationModel.NewDocTypeId;
                model.UserCreatedId = transferData.UserSend.UserId;
                model.UserCreatedName = transferData.UserSend.FullName;

                var originalDocument = _documentService.Get(model.DocumentId);
                if (originalDocument == null)
                {
                    throw new Exception("Văn bản gốc không tồn tại.");
                }

                // Gán ID documentcopy hiện tại để lấy comment lên văn bản gốc
                model.DocumentCopyModel.DocumentCopyParentPath = string.Format("{0}\\", model.DocumentCopyId);

                // Chuyển thành tạo mới
                model.DocumentCopyId = 0;
                model.DocTypeId = destinationModel.NewDocTypeId;
                model.DocumentCopyModel.DocTypeId = destinationModel.NewDocTypeId;
                model.DocumentId = Guid.NewGuid();

                var attachments = CopyAttachments(originalDocument.Attachments, model.DocumentId, transferData.UserSend);
                model.AttachmentModels = attachments;
            }

            hasUpdateDateAppointed = hasChangeExpireProcess;

            if (hasChangeExpireProcess)
            {
                // Cập nhật hạn xử lý với văn bản phân loại liên thông hoặc có thay đổi hạn khi phân loại
                // Hạn xử lý ở đây đã được xử lý ở client nên ko cần tính lại nữa.
                model.DateAppointed = model.DateAppointed ?? _workTimeHelper.GetDateAppoint(model.DateCreated, transferData.Workflow.ExpireProcess); ;
            }

            var currentDepartment = _departmentService.GetPrimaryDepartment(transferData.UserSend.UserId);
            if (currentDepartment != null)
            {
                // Cập nhật đơn vị nhận văn bản về đơn vị hiện tại
                model.InOutPlace = currentDepartment.DepartmentPath;
            }

            // Điệu kiện phân loại + xin ý kiến --> thành văn bản ĐXL
            var isVanbanXinykien = model.DocumentCopyType == (int)DocumentCopyTypes.XinYKien;
            if (isVanbanXinykien)
            {
                model.DocumentCopyModel.DocumentCopyType = (int)DocumentCopyTypes.DongXuLy;
            }

            return model;
        }

        private DocumentModel UpdateModelForVbDen(DocumentModel model, bool isCreatingDocument, bool isDangPhanLoai)
        {
            /*
			 Cập nhật dữ liệu văn bản đến cho document hiện tại
				- Xử lý trùng số ký hiệu, số đến
				- Xử lý cấp số đến
			 */
            if (model.CategoryBusinessId != (int)CategoryBusinessTypes.VbDen) return model;

            var codeId = model.CodeId;
            var docCode = model.DocCode;
            var isLienThong = model.Original == 2;

            var isUpdating = !isCreatingDocument && !isDangPhanLoai;
            if (isUpdating)
            {
                return model;
            }

            if (DocCodeIsUsed(docCode, model.Organization, documentId: model.DocumentId, categoryBussiness: CategoryBusinessTypes.VbDen, inOutPlace: model.InOutPlace))
            {
                if (isLienThong || isCreatingDocument)
                {
                    throw new Exception("Số ký hiệu trùng, vui lòng nhập lại.");
                }
            }

            if (InOutCodeUsed(model.InOutCode, model.StoreId, model.DocumentId))
            {
                throw new Exception("Số đến trùng, vui lòng nhập lại.");
            }

            if (model.StoreId.HasValue)
            {
                if (string.IsNullOrEmpty(model.InOutCode))
                {
                    throw new Exception("Không có số đến, vui lòng nhập lại.");
                }

                if (!model.IsCustomCode && codeId == 0)
                {
                    throw new Exception("Thông tin cấp số không đúng. Vui lòng nhập lại.");
                }

                model.InOutCode = _codeService.ConfirmCode(codeId, DateTime.Now, null, model.InOutCode);
            }

            return model;
        }

        private DocumentModel UpdateModelForVbDi(DocumentModel model)
        {
            // Văn bản đi: trường hợp cấp số trước
            model.DocCode = string.IsNullOrEmpty(model.DocCode) ? "" : GetDocCodeForVbdi(model.DocCode, model.CodeId, model.StoreId ?? 0);

            if (model.StoreId.HasValue && model.StoreId > 0)
            {
                if (model.CodeId == 0)
                {
                    throw new Exception("Thông tin cấp số không đúng. Vui lòng nhập lại.");
                }

                model.DocCode = _codeService.ConfirmCode(model.CodeId, DateTime.Now, null, model.DocCode);
            }

            return model;
        }

        /// <summary>
        /// Trả về documentcopy gốc cho thao tác chuyển văn bản.
        /// Văn bản gốc là văn bản trước khi thực hiện chuyển. Xử lý cập nhật văn bản gốc về trạng thái mới nhât.
        /// Sau đó mới xử lý bàn giao trên văn bản này
        /// </summary>
        /// <returns></returns>
        private DocumentCopy UpdateDocumentTransfer(DocumentModel model, string files, List<int> removeAttachmentIds, string modifiedFiles,
                TransferData transferData, bool isCreatingDocument, bool isPhanLoai, int? documentCopyParentId = null, bool hasUpdateDateAppointed = false, bool isSaveDocDraft = false)
        {
            var newFiles = Json2.ParseAs<IDictionary<string, IDictionary<string, string>>>(files);
            var modifiedAttachment = Json2.ParseAs<IDictionary<int, string>>(modifiedFiles);
            DocumentCopy documentCopy = null;
            var userSend = transferData == null ? _userService.CurrentUser.ToUser() : transferData.UserSend;

            using (var trans = new TransactionScope(TransactionScopeOption.Required))
            {
                model = BuildDocumentModelTransfer(model, transferData, isCreatingDocument, isPhanLoai);

                documentCopy = isCreatingDocument
                                   ? _documentHelper.CreateDocumentDefault(model, userSend.UserId, newFiles)
                                   : _documentHelper.UpdateDocumentDefault(model, newFiles, removeAttachmentIds, modifiedAttachment, userSend.UserId, hasChangeAttachment: true, hasUpdateDateAppointed: hasUpdateDateAppointed, isSaveDocDraft: isSaveDocDraft);

                if (isPhanLoai && model.HasChangeInoutCode)
                {

                }

                trans.Complete();
            }

            return documentCopy;
        }

        private DocumentModel BuildDocumentModelTransfer(DocumentModel model, TransferData transferData, bool isCreatingDocument, bool isPhanLoai)
        {
            if (model.Status == (int)DocumentStatus.DuThao || model.Status == 0)
            {
                model.Status = (int)DocumentStatus.DangXuLy;
            }

            if (model.RelationModels != null)
            {
                foreach (var relation in model.RelationModels)
                {
                    relation.CitizenName = GlobalObject.unescape(relation.CitizenName);
                    relation.Compendium = GlobalObject.unescape(relation.Compendium);
                }
            }

            #region Cập nhật document copy

            var documentCopyModel = model.DocumentCopyModel;
            documentCopyModel.DateModified = DateTime.Now;

            // Xử lý DateOverdue, đưa hạn về cuối ngày dượcđặt
            if (model.DateOverdue.HasValue && model.HasDateOverdue)
            {
                var dateOverdue = model.DateOverdue.Value;
                documentCopyModel.DateOverdue = new DateTime(dateOverdue.Year, dateOverdue.Month, dateOverdue.Day, 23, 59, 59);
            }

            if (model.DateCreated == DateTime.MinValue)
            {
                model.DateCreated = DateTime.Now;
            }

            if (transferData != null)
            {
                documentCopyModel.WorkflowId = transferData.Workflow.WorkflowId;
                documentCopyModel.UserSendId = transferData.UserSend.UserId;

                if (transferData.UserAuthorize != null)
                {
                    // Cập nhật người hiện tại vào danh sách xử lý ủy quyền.
                    documentCopyModel.UserGiamSat = DocumentCopy.UserCompareString(transferData.UserAuthorize.UserId);
                }

                if (transferData.UserTbs != null && transferData.UserTbs.Any())
                {
                    documentCopyModel.UserThongBao = DocumentCopy.UserCompareString(transferData.UserTbs);
                }

                documentCopyModel.DocumentUsers += documentCopyModel.UserGiamSat + documentCopyModel.UserThongBao;
            }

            #endregion

            return model;
        }

        #endregion

        #region Hồ sơ một cửa

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult TransferTiepNhan(string doc, string files, int? storePrivateId)
        {
            var model = Json2.ParseAs<DocumentModel>(doc);
            var categoryBussiness = (CategoryBusinessTypes)model.CategoryBusinessId;

            if (categoryBussiness != CategoryBusinessTypes.Hsmc)
            {
                return Json(new { error = "Không có quyền khởi tạo hồ sơ này." });
            }

            var newAttachments = Json2.ParseAs<IDictionary<string, IDictionary<string, string>>>(files);
            var transferData = ParseCreateTransferDataHsmc(model);

            foreach (var relation in model.RelationModels)
            {
                // Khi tiếp nhận không có xác nhận AddNext nên gắn vào luôn
                relation.IsAddNext = true;
            }

            model = UpdateModelForHsmc(model, transferData);

            var doctype = _docTypeService.GetFromCache(model.DocTypeId);
            var models = new List<DocumentModel>();

            if (doctype.DocTypePermissionInEnum.HasValue
                    && EnumHelper<DocTypePermissions>.ContainFlags(doctype.DocTypePermissionInEnum.Value, DocTypePermissions.DuocPhepCongChung)
                    && model.DocPapers.Any() && model.DocPapers.Count() > 1)
            {
                // Trường hợp cấu hình công chứng: khởi tạo 1 hs sơ và đính kèm các giấy tờ công chứng vào.
                // Khi tiếp nhận mỗi giấy tờ sẽ được tách ra thành 1 hồ sơ rienge.

                var papers = model.DocPapers;
                foreach (var paper in papers)
                {
                    var newModel = BuildDocumentModelTransfer(model, transferData, true, isPhanLoai: false);
                    newModel.DocPapers = new List<DocPaperModel>() { paper };
                    models.Add(newModel);
                }
            }
            else
            {
                models.Add(BuildDocumentModelTransfer(model, transferData, true, isPhanLoai: false));
            }

            foreach (var newModel in models)
            {
                using (var trans = new TransactionScope())
                {
                    var dateNow = DateTime.Now;
                    var nodeReceive = transferData.NodeReceive;
                    nodeReceive.Parent.Id = transferData.Workflow.WorkflowId;

                    newModel.DocCode = _codeService.ConfirmCode(newModel.CodeId, dateNow, null, newModel.DocCode);
                    var documentCopy = _documentHelper.CreateDocumentDefault(newModel, transferData.UserSend.UserId, newAttachments);

                    TransferToXlc(documentCopy, transferData);

                    #region Cập nhật thông tin công dân vào hệ thống để autocomplete lần tiếp nhận hồ sơ tiếp theo

                    var citizen = _citizenService.Get(false, c => c.IdentityCard == newModel.IdentityCard);
                    if (citizen == null)
                    {
                        citizen = new Citizen
                        {
                            CitizenName = newModel.CitizenName,
                            Email = newModel.Email,
                            IdentityCard = newModel.IdentityCard,
                            Phone = newModel.Phone,
                            Address = newModel.Address,
                        };
                        _citizenService.Create(citizen);
                    }
                    else
                    {
                        citizen.CitizenName = newModel.CitizenName;
                        citizen.Email = newModel.Email;
                        citizen.Phone = newModel.Phone;
                        citizen.Address = newModel.Address;
                        _citizenService.Update(citizen);
                    }

                    #endregion

                    #region Gửi mail, sms thông báo

                    try
                    {
                        if (!string.IsNullOrEmpty(model.Email))
                        {
                            var userReveices = new List<string>() { model.Email };
                            _mailHelper.SendTiepNhan(documentCopy, userReveices, CurrentUserId());
                        }

                        if (!string.IsNullOrWhiteSpace(model.Phone))
                        {
                            _smsHelper.SendCreatedDocumentSms(documentCopy.Document, transferData.UserSend, documentCopy.DocumentCopyId);
                        }
                    }
                    catch (Exception ex)
                    {
                        LogException(ex);
                    }

                    #endregion

                    #region Lưu sổ cá nhân

                    if (storePrivateId.HasValue)
                    {
                        var storePrivate = _storePrivateService.CheckPermissionStorePrivate(storePrivateId.Value, CurrentUserId());
                        if (storePrivate != null && storePrivate.Status == (byte)StorePrivateStatus.IsActive)
                        {
                            _storePrivateService.AddDocumentToStore(storePrivate, documentCopy.DocumentCopyId, documentCopy.DocumentId, false);
                        }
                    }

                    #endregion

                    trans.Complete();
                }
            }

            return Json(new { success = "Chuyển văn bản thành công." });
        }

        private TransferData ParseCreateTransferDataHsmc(DocumentModel model)
        {
            var result = new TransferData();
            var categoryBussiness = (CategoryBusinessTypes)model.CategoryBusinessId;

            var doctype = _docTypeService.GetFromCache(model.DocTypeId);
            if (doctype == null || doctype.WorkflowId == 0)
            {
                throw new Exception("Loại văn bản hoặc quy trình không tồn tại.");
            }

            var currentUser = _userService.CurrentUser;
            var workFlow = _workflowService.GetFromCache(doctype.WorkflowId);
            var startNodes = _workflowHelper.GetStartNodes(workFlow, currentUser.UserId);

            result.Workflow = workFlow;
            result.NodeSend = startNodes.First();
            result.NodeReceive = result.NodeSend;

            result.UserSend = _userService.GetFromCache(currentUser.UserId);
            result.UserXlcs.Add(currentUser.UserId);
            result.UserReceiveId = currentUser.UserId;

            var commentTransfer = NewCommentTranferList(currentUser.UserId, "xulychinh", "viewXlc");
            result.CommentTransfer = commentTransfer;
            result.CommentTransferText = _documentHelper.GetCommentTransferText(commentTransfer);
            result.Comment = string.Format("{0} lúc: {1}", categoryBussiness == CategoryBusinessTypes.Hsmc ? "Tiếp nhận Hồ sơ" : "Khởi tạo Văn bản", DateTime.Now.ToString("hh:mm dd/MM/yy"));

            return result;
        }

        private DocumentModel UpdateModelForHsmc(DocumentModel model, TransferData transferData)
        {
            if (model.CategoryBusinessId != (int)CategoryBusinessTypes.Hsmc) return model;

            if (string.IsNullOrEmpty(model.Compendium))
            {
                model.Compendium = GlobalObject.unescape(model.CitizenName);
            }
            if (string.IsNullOrEmpty(model.CitizenName))
            {
                model.CitizenName = model.Compendium;
            }

            #region Xử lý ký duyệt

            var isSuccess = model.IsSuccess;
            if (isSuccess.HasValue && (!model.UserSuccessId.HasValue || (model.UserSuccessId.HasValue && model.UserSuccessId.Value != transferData.UserSend.UserId)))
            {
                // Có thông tin ký duyệt và người duyệt không phải là người hiện tại => cập nhật người ký duyệt mới là người hiện tại userSend
                model.UserSuccessId = transferData.UserSend.UserId;
                model.UserSuccessName = transferData.UserSend.FullName;
            }

            #endregion

            model.DocumentCopyModel.HasJustCreated = model.DocumentCopyId == 0;

            return model;
        }

        #endregion

        #region Bàn giao mobile

        /// <summary>
        /// Chuyển văn bản mobile
        /// </summary>
        /// <param name="documentCopyId">Id</param>
        /// <param name="destination">hướng chuyển</param>
        /// <param name="comment">ý kiến xử lys</param>
        /// <returns></returns>
        public JsonResult LightTransfer(int documentCopyId, string destination, string comment, string files, string newContent, string newCompendium, string modifiedFiles = "", string jsonFile = "")
        {
            try
            {
                var documentIdTransfering = Guid.Empty;
                var dateNow = DateTime.Now;
                var userSend = _userService.CurrentUser;

                #region Kiểm tra điều kiện đầu vào

                DestinationModel destinationModel;
                try
                {
                    destinationModel = Json2.ParseAs<DestinationModel>(destination);
                }
                catch (Exception)
                {
                    return Json(new { error = "Thông tin chuyển văn bản không đúng. Vui lòng thử lại." });
                }

                var documentCopy = _docCopyService.Get(documentCopyId);
                if (documentCopy == null)
                {
                    return Json(new { error = "Văn bản không tồn tại, vui lòng thử lại." });
                }

                var document = documentCopy.Document;

                var transferData = ParseTransferData(documentCopyId, documentCopy.DocumentCopyType, destinationModel, comment, isCreatingDocument: false, isDangPhanLoai: false);

                #endregion

                if (!string.IsNullOrEmpty(jsonFile))
                {
                    var jsonForm = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonFile);
                    var forms = _doctypeFormService.GetForms(d => new
                    {
                        d.Form.FormId,
                        d.Form.FormName,
                        d.Form.FormTypeId,
                        d.IsPrimary,
                        d.Form.EmbryonicPath,
                        d.Form.EmbryonicLocationName,
                        d.Form.Json
                    }, document.DocTypeId.Value);

                    if (forms != null && forms.Any())
                    {
                        var docContents = _docCopyService.GetDocumentContents(document.DocumentId).Where(c => !string.IsNullOrEmpty(c.ContentUrl));
                        if (docContents.Any())
                        {
                            var docContent = docContents.First();
                            var config = forms.FirstOrDefault().Json;
                            var path = CommonHelper.MapPath("~/" + docContent.ContentUrl);
                            var xlsxParser = new XlsxToJson(path);
                            xlsxParser.GenDataMobileFormFull(jsonForm, config, 1, 2, 1, 2);
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(files))
                {
                    var newFiles = Json2.ParseAs<IDictionary<string, IDictionary<string, string>>>(files);
                    var newAttachments = _attachmentService.AddAttachmentInDoc(newFiles, userSend.UserId, true);
                    foreach (var attachment in newAttachments)
                    {
                        document.Attachments.Add(attachment);
                    }
                }

                if (!string.IsNullOrWhiteSpace(newCompendium))
                {
                    document.Compendium = GlobalObject.unescape(newCompendium);
                    document.Compendium2 = document.Compendium.StripVietnameseChars();
                }

                if (!string.IsNullOrWhiteSpace(newContent))
                {
                    var documentContent = document.DocumentContents.First();
                    if (documentContent != null)
                    {
                        documentContent.Content = HttpUtility.HtmlDecode(newContent);
                        _documentContentService.Update(documentContent);
                    }
                }

                if (modifiedFiles.Length > 0)
                {
                    modifiedFiles = modifiedFiles.Replace('-', '+').Replace('_', '/');
                    var modifiedAttachment = Json2.ParseAs<IDictionary<int, string>>(modifiedFiles);

                    _documentService.UpdateAttachments(document, null, null, modifiedAttachment, transferData.UserSend, true);
                }

                using (var trans = new TransactionScope())
                {
                    var dateCreated = DateTime.Now;

                    #region Xác định văn bản gốc: cập nhật hoặc tạo mới văn bản đang thao tác

                    _docCopyService.Update(documentCopy);

                    #endregion

                    #region Do Transfer

                    var transferResult = new Dictionary<int, DocumentCopy>();

                    if (transferData.UserXlcs.Any())
                    {
                        transferResult = transferResult.Concat(TransferToXlc(documentCopy, transferData)).ToDictionary(x => x.Key, x => x.Value);
                    }
                    else
                    {
                        // transferResult = transferResult.Concat(TransferToOnlyDxls(documentCopy, model, transferData)).ToDictionary(x => x.Key, x => x.Value);
                    }

                    if (transferData.UserDxls.Any())
                    {
                        transferResult = transferResult.Concat(TransferToDxls(documentCopy, transferData)).ToDictionary(x => x.Key, x => x.Value);
                    }

                    #endregion Do Transfer

                    #region Kết thúc văn bản đến

                    if (!_generalSettings.FinishOriginalDocumentWhenAnswer &&
                            (transferData.NodeReceive.IsSaveRecordAndRelease || transferData.NodeReceive.IsSaveRecordAndInternalRelease))
                    {
                        // Kết thúc văn bản đến khi văn bản đi trả lời chuyển đến node phát hành.
                        var documentRelationReply = _documentService.GetDocRelations(r => r.DocumentCopyId == documentCopy.DocumentCopyId && r.RelationType == (int)RelationTypes.LienQuanTraLoi).SingleOrDefault();
                        if (documentRelationReply != null)
                        {
                            var originalDocumentCopy = _docCopyService.Get(documentRelationReply.RelationCopyId);
                            if (originalDocumentCopy != null)
                            {
                                var contentFinish = string.Format("Kết thúc văn bản sau khi đã trả lời bằng văn bản: {0}", documentCopy.Document.Compendium);
                                _docCopyService.Finish(originalDocumentCopy, dateCreated, transferData.UserSend.UserId, contentFinish);
                            }
                        }
                    }

                    #endregion

                    #region Xử lý cache

                    ResetCache(documentCopy.DocumentCopyId);

                    #endregion

                    #region Gửi Mail, Sms, Notification

                    var userReceivedIds = transferResult.Keys.Where(u => u != transferData.UserSend.UserId);
                    SendMailAndSmsTranfer(documentCopy.Document, transferData.UserSend, userReceivedIds, documentCopy.DocumentCopyId);

                    SaveNotificationToQueue(transferResult, document.Compendium, transferData.DateSend, isCreatingDocument: false);

                    #endregion

                    if (transferData.NodeSend.ReturnResult)
                    {
                        var clPublish = new PublishController(_codeService, _userService, _addressService, _documentPublishService,_documentPublishPlusService, _documentHelper, _documentCache, _commentService, _docCopyService, _documentPermissionHelper, _attachmentService,
                            _edocService, _anticipateService, _documentService, _mailHelper, _docTypeService, _doctypeFormService, _formService, _departmentService, _generalSettings, _reportConfigSettings);
                        clPublish.SendToLgsp(document.DocumentId, document);
                    }

                    trans.Complete();
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
                return Json(new { error = "Có lỗi trong quá trình chuyển văn bản: " + ex.Message });
            }

            return Json(new { success = "Chuyển văn bản thành công." });
        }

        #endregion

        #region Private Methods

        private void SendMailAndSmsTranfer(Document document, User userSend, IEnumerable<int> userReceiveIds, int documentCopyId)
        {
            try
            {
                var userReceiveds = _userService.GetAllCached()
                                        .Where(u => userReceiveIds.Contains(u.UserId) && !string.IsNullOrEmpty(u.Email))
                                        .Select(u => u.Email).Distinct().ToList();

                _mailHelper.SendTranferDocumentMail(document, userSend, userReceiveds, documentCopyId);

            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        private void ResetCache(int documentCopyId)
        {
            _documentCache.RemoveAll(documentCopyId);
        }

        /// <summary>
        /// Xử lý lại danh sách đồng gửi dưới client truyền lên.
        /// Chắc chắn rằng người nhận được văn bản đúng với targetcomment.
        /// Note: do có nơi phản ánh thỉnh thoảng mất văn bản nên chuyển xử lý như thế này.
        /// </summary>
        /// <param name="commentTransfers"></param>
        /// <returns></returns>
        private List<int> ParseUserDgs(List<CommentTransfer> commentTransfers)
        {
            var deptIds = new List<int>();
            var result = new List<int>();
            if (commentTransfers == null || !commentTransfers.Any())
            {
                return result;
            }

            foreach (var transfer in commentTransfers)
            {
                if (string.IsNullOrEmpty(transfer.Value))
                {
                    continue;
                }

                var valueParse = transfer.Value.Split(new char[] { '-' });
                if (valueParse.Length != 2)
                {
                    continue;
                }

                if (transfer.Type == "5")
                {
                    continue;
                }

                var isUserNode = transfer.Value.StartsWith("FilterDepartment-user", StringComparison.OrdinalIgnoreCase);
                var valueStr = isUserNode
                                    ? transfer.Value.Replace("FilterDepartment-user_", "")
                                    : transfer.Value.Replace("FilterDepartment-", "");

                int value;
                if (!Int32.TryParse(valueStr, out value))
                {
                    continue;
                }

                if (isUserNode)
                {
                    result.Add(value);
                }
                else
                {
                    deptIds.Add(value);
                }
            }

            if (deptIds.Any())
            {
                var userDeptIds = _departmentService.GetCacheAllUserDepartmentJobTitlesPosition()
                                    .Where(ud => deptIds.Contains(ud.DepartmentId)).Select(ud => ud.UserId);
                result.AddRange(userDeptIds);
            }

            return result.Distinct().ToList();
        }

        /// <summary>
        /// Xử lý lại danh sách đơn vị nhận dưới client truyền lên.
        /// Chắc chắn rằng người nhận được khảo sát đúng với targetcomment.
        /// </summary>
        /// <param name="commentTransfers"></param>
        /// <returns></returns>
        private List<int> ParseUserDvns(List<CommentTransfer> commentTransfers)
        {
            var deptIdExts = new List<string>();
            var jobtitleDeptIdExts = new List<string>();
            var positions = new List<int>();
            var result = new List<int>();
            if (commentTransfers == null || !commentTransfers.Any())
            {
                return result;
            }

            foreach (var transfer in commentTransfers)
            {
                if (string.IsNullOrEmpty(transfer.Value))
                {
                    continue;
                }

                if (transfer.Value == "all")
                {
                    var userIds = _departmentService.GetCacheAllUserDepartmentJobTitlesPosition().Select(ud => ud.UserId);
                    result.AddRange(userIds);
                    return result.Distinct().ToList();
                }

                var valueParse = transfer.Value.Split(new char[] { '-' });
                if (valueParse.Length != 2)
                {
                    continue;
                }

                if (transfer.Type == "5")
                {
                    continue;
                }

                var isFilterDepartment = valueParse[0].Equals("FilterDepartment", StringComparison.OrdinalIgnoreCase);
                if (isFilterDepartment)
                {
                    var isUserNode = valueParse[1].StartsWith("user_", StringComparison.OrdinalIgnoreCase);
                    var valueStr = isUserNode
                                    ? valueParse[1].Replace("user_", "")
                                    : valueParse[1];

                    int value;
                    if (!Int32.TryParse(valueStr, out value))
                    {
                        continue;
                    }

                    if (isUserNode)
                    {
                        result.Add(value);
                    }
                    else
                    {
                        deptIdExts.Add(valueStr);
                    }
                }
                else
                {
                    var jobtitleValueParse = valueParse[1].Split(new char[] { '_' });
                    if (valueParse.Length != 2)
                    {
                        continue;
                    }
                    jobtitleDeptIdExts.Add(jobtitleValueParse[0]);
                    positions = jobtitleValueParse[1].Split(new char[] { ',' }).Select(int.Parse).ToList();
                }
            }

            if (deptIdExts.Any())
            {
                var userDeptIds = _departmentService.GetCacheAllUserDepartmentJobTitlesPosition()
                                    .Where(ud => deptIdExts.Any(ud.DepartmentIdExt.Contains))
                                    .Select(ud => ud.UserId);
                result.AddRange(userDeptIds);
            }

            if (jobtitleDeptIdExts.Any())
            {
                var userDeptIds = _departmentService.GetCacheAllUserDepartmentJobTitlesPosition()
                                    .Where(ud => jobtitleDeptIdExts.Any(ud.DepartmentIdExt.StartsWith) && positions.Contains(ud.PositionId))
                                    .Select(ud => ud.UserId);
                result.AddRange(userDeptIds);
            }

            return result.Distinct().ToList();
        }

        private string GetDocCodeForHsmc(string code, int codeId, Guid? documentId = null)
        {
            if (string.IsNullOrEmpty(code))
            {
                throw new Exception("Số ký hiệu không đúng, vui lòng liên hệ quản trị cấu hình cấp số.");
            }

            var usedCodes = DocCodeIsUsed(code, organization: null, documentId: documentId, categoryBussiness: CategoryBusinessTypes.Hsmc);

            if (usedCodes)
            {
                throw new Exception("Số ký hiệu trùng. Vui lòng cấp lại số khác.");
            }

            return code;
        }

        public string GetDocCodeForVbdi(string code, int codeId, int storeId)
        {
            if (string.IsNullOrEmpty(code))
            {
                throw new Exception("Số ký hiệu không đúng, vui lòng liên hệ quản trị cấu hình cấp số.");
            }

            if (codeId == 0 || storeId == 0)
            {
                throw new Exception("Thông tin cấp số không đúng. Vui lòng nhập lại.");
            }

            var usedCode = _codeService.GetDocCodeUsed(code);
            if (usedCode.Any(c => c.StoreId == storeId))
            {
                throw new Exception("Số ký hiệu đã được cấp. Vui lòng nhập số khác.");
            }

            return _codeService.ConfirmCode(codeId, DateTime.Now, null, code);
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

        private bool InOutCodeUsed(string inOutCode, int? storeId, Guid? documentId = null)
        {
            if (string.IsNullOrEmpty(inOutCode) || !storeId.HasValue || storeId == 0)
            {
                return false;
            }

            return _codeService.CodeIsUsed(inOutCode, false, storeId.Value, CategoryBusinessTypes.VbDen, "", documentId);
        }

        #region Notify To Client

        /// <summary>
        /// Đẩy notify message đến người nhận văn bản, hồ sơ: bao gồm người nhận xử lý chính, đồng xử lý, và thông báo
        /// </summary>
        /// <param name="userReceiveds">Danh sách người nhận notify</param>
        /// <param name="documentCopy">Document Copy</param>
        /// <param name="compendium">Trích yếu</param>
        /// <param name="dateCreated">Ngày tạo</param>
        /// <param name="isCreatingDocument">Notify khi tạo document</param>
        private void PushNotifyMessage(IEnumerable<int> userReceiveds, DocumentCopy documentCopy, DateTime dateCreated, bool isCreatingDocument = false)
        {
            var notifyBody = "";
            var document = documentCopy.Document;

#if HoSoMotCuaEdition

            if (document.CategoryBusinessIdInEnum == CategoryBusinessTypes.Hsmc)
            {
                notifyBody = string.Format("{0} - {1}", document.DocCode, document.CitizenName);
            }

#endif
            if (string.IsNullOrEmpty(notifyBody))
            {
                notifyBody = document.Compendium;
            }

            try
            {
                _notificationHelper.PushNotifyMessage(userReceiveds, documentCopy, notifyBody, dateCreated, isCreatingDocument);
            }
            catch { }
        }

        /// <summary>
        /// Chuyển notify vào queue
        /// </summary>
        /// <param name="transferResult"></param>
        /// <param name="compendium"></param>
        /// <param name="dateSend"></param>
        /// <param name="isCreatingDocument"></param>
        private void SaveNotificationToQueue(Dictionary<int, DocumentCopy> transferResults, string compendium, DateTime dateSend, bool isCreatingDocument = false)
        {
            var notifyBody = "";
            foreach (var transferResult in transferResults)
            {
                var userReceive = transferResult.Key;
                var documentCopy = transferResult.Value;
                var document = documentCopy.Document;

#if HoSoMotCuaEdition

                if (document.CategoryBusinessIdInEnum == CategoryBusinessTypes.Hsmc)
                {
                    notifyBody = string.Format("{0} - {1}", document.DocCode, document.CitizenName);
                }

#endif
                if (string.IsNullOrEmpty(notifyBody))
                {
                    notifyBody = document.Compendium;
                }

                try
                {
                    _notificationHelper.PushNotifyMessage(new List<int>() { userReceive }, documentCopy, notifyBody, dateSend, isCreatingDocument);
                }
                catch { }
            }
        }

        #endregion

        private List<CommentTransfer> NewCommentTranferList(int userId, string type, string value)
        {
            var user = _userService.GetFromCache(userId);
            var commentTransfer = new CommentTransfer
            {
                Label = user.FullName,
                Type = type,
                Department = _userService.GetMainDepartmentName(userId),
                Value = value
            };
            return new List<CommentTransfer> { commentTransfer };
        }

        private bool HasChangeAttachmentPermission(int? docCopyParentId)
        {
            var result = true;
            if (!docCopyParentId.HasValue) return result;

            var docCopyParent = _docCopyService.Get(docCopyParentId.Value);
            if (docCopyParent == null) return result;

            if (docCopyParent.NodeCurrentPermission == null)
            {
                return result;
            }

            var nodePermission = (NodePermissions)docCopyParent.NodeCurrentPermission;
            if (EnumHelper<NodePermissions>.ContainFlags(nodePermission, NodePermissions.QuyenLuuSoPhatHanh)
                    || EnumHelper<NodePermissions>.ContainFlags(nodePermission, NodePermissions.QuyenLuuSoVaPhatHanhNoiBo))
            {
                return false;
            }

            if (docCopyParent.StatusInEnum != DocumentStatus.DangXuLy
                    && docCopyParent.StatusInEnum != DocumentStatus.DungXuLy)
            {
                return false;
            }

            return result;
        }

        private int CurrentUserId()
        {
            return _userService.CurrentUser.UserId;
        }

        #endregion
    }

    #region Transfer Data

    internal class TransferData
    {
        public TransferData()
        {
            UserXlcs = new List<int>();
            UserDxls = new List<int>();
            UserTbs = new List<int>();
            DateSend = DateTime.Now;
        }

        /// <summary>
        /// Id người nhận văn bản hiện tại
        /// </summary>
        public int UserReceiveId { get; internal set; }

        public List<int> UserXlcs { get; internal set; }

        public List<int> UserDxls { get; internal set; }

        public List<int> UserTbs { get; internal set; }

        public Workflow Workflow { get; internal set; }

        public Node NodeSend { get; internal set; }

        public Node NodeReceive { get; internal set; }

        public User UserSend { get; internal set; }

        public User UserAuthorize { get; internal set; }

        public string Comment { get; internal set; }

        public string AuthorizeComment { get; internal set; }

        public DateTime DateSend { get; set; }

        public List<CommentTransfer> CommentTransfer { get; internal set; }
        public string CommentTransferText { get; internal set; }
    }

    #endregion
}