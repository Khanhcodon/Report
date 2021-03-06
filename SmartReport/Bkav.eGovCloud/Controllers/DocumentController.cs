#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Transactions;
using System.Web.Mvc;
using Bkav.eGovCloud.Business;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Business.Utils;
using Bkav.eGovCloud.Core.Document;
using Bkav.eGovCloud.Core.DynamicForm;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.FileSystem;
using Bkav.eGovCloud.Core.History;
using Bkav.eGovCloud.Core.ReadFile;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Entities.Enum;
using Bkav.eGovCloud.Models;
using Bkav.eGovCloud.Search;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovOnline.Business.Customer;
using Microsoft.AspNet.SignalR;
using Microsoft.JScript;
using Newtonsoft.Json.Linq;
using SolrNet.Utils;
using FeeType = Bkav.eGovCloud.Entities.FeeType;
using FormType = Bkav.eGovCloud.Entities.FormType;
using System.Data;
using Newtonsoft.Json;
using System.Xml;
using System.IO;
using Bkav.eGovCloud.Core.Workflow;
using Bkav.eGovCloud.Business.Objects;
using Bkav.eGovCloud.Business.Objects.CacheObjects;
using System.Dynamic;
using MySql.Data.MySqlClient;
using ActiveQueryBuilder.Web.Server;
using ActiveQueryBuilder.Core;
using System.Text;
using Bkav.eGovCloud.Areas.Admin.Models;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using HtmlAgilityPack;
using System.Collections;

#endregion

namespace Bkav.eGovCloud.Controllers
{
    //[RequireHttps]
    [EgovAuthorize]
    public class DocumentController : CustomerBaseController
    {
        #region Readonly & Static Fields

        private readonly DocumentOnlineBll _docOnlineService;
        private readonly ApproverBll _approverService;
        private readonly CategoryBll _categoryService;
        private readonly CodeBll _codeService;
        private readonly CommentBll _commentService;
        private readonly CommonCommentBll _commonCommentService;
        private readonly DepartmentBll _departmentService;
        private readonly DocTimelineBll _docTimelineService;
        private readonly DocTypeBll _docTypeService;
        private readonly DocFieldBll _docfieldService;
        private readonly DocumentContentBll _documentContentService;
        private readonly DocumentCopyBll _documentCopyService;
        private readonly DocumentPermissionHelper _documentPermissionHelper;
        private readonly DocumentBll _documentService;
        private readonly ExtensionTimeBll _extensionTimeService;
        private readonly DocumentPublishBll _publishService;
        private readonly DocumentPublishPlusBll _publishPlusService;
        private readonly FileUploadSettings _fileUploadSettings;
        private readonly FormHelper _formHelper;
        private readonly SupplementaryBll _supplementaryServcie;
        private readonly UserBll _userService;
        private readonly WorktimeHelper _workTimeHelper;
        private readonly StorePrivateBll _storePrivateService;
        private readonly WorkflowBll _workflowService;
        private readonly AddressBll _addressService;
        private readonly StoreBll _storeService;
        private readonly DocTypeFormBll _doctypeFormService;
        private readonly BusinessLicenseBll _businessLicenseService;
        private readonly AnticipateBll _anticipateService;
        private readonly FormBll _formService;
        private readonly DocFinishBll _docfinishService;
        private readonly AttachmentBll _attachmentService;
        private readonly EgovSearch _searchService;
        private readonly KeyWordBll _keyWordService;
        private readonly PaperBll _paperService;
        private readonly FeeBll _feeService;
        private readonly TransferTypeBll _transferTypeService;
        private readonly UserActivityLogBll _userActivityLogService;
        private readonly UserConnectionBll _userConnectionService;
        private readonly SearchSettings _searchSettings;
        private readonly ISearchInDatabase _searchInDatabaseService;
        private readonly ImageSettings _imageSettings;
        private readonly Helper.UserSetting _helperUserSetting;
        private readonly ResourceBll _resourceService;
        private readonly CitizenBll _citizenService;
        private readonly TemplateBll _templateService;
        private readonly InterfaceConfigBll _interfaceConfigService;
        private readonly SendSmsHelper _smsHelper;
        private readonly SendEmailHelper _mailHelper;
        private readonly WorkflowHelper _workflowHelper;
        private readonly TemplateHelper _templateHelper;
        private readonly LogBll _logService;
        private readonly TransferSettings _transferSetting;

        private readonly EmailSettings _emailSettings;
        private readonly SmsSettings _smsSettings;
        private readonly AdminGeneralSettings _adminSetting;
        private readonly PermissionBll _permissionSerice;
        private readonly AuthorizeBll _authorizeService;
        private readonly CatalogBll _catalogService;
        private string DateFormat = "dd/MM/yyyy";
        private readonly TemplateKeyBll _templateKeyService;

        private readonly AdminGeneralSettings _generalSettings;

        private const int FORM_CATEGORY_TARGET = 2;
        private const int FORM_CATEGORY_SUMMARY = 3;

        #endregion

        #region C'tors

        public DocumentController(
            DocumentOnlineBll docOnlineService,
            DocFieldBll docfieldService,
            DocTypeBll docTypeService,
            CategoryBll categoryService,
            FormBll formService,
            DocumentBll documentService,
            FormHelper formHelper,
            DocumentCopyBll documentCopyService,
            UserBll userService,
            DepartmentBll departmentService,
            CommentBll commentService,
            ApproverBll approverService,
            DocumentContentBll documentContentService,
            CodeBll codeService,
            DocumentPermissionHelper documentPermissionHelper,
            DocTimelineBll docTimelineService,
            CommonCommentBll commonCommentBll,
            SupplementaryBll supplementaryServcie,
            WorktimeHelper worktimeHelper,
            ExtensionTimeBll extensionTimeService,
            FileUploadSettings fileUploadSettings,
            StorePrivateBll storePrivateService,
            WorkflowBll workflowService,
            AddressBll addressService,
            StoreBll storeService,
            DocTypeFormBll docTypeFormService,
            BusinessLicenseBll businessLicenseService,
            AnticipateBll anticipateService,
            DocFinishBll docfinishService,
            AttachmentBll attachmentService,
            EgovSearch searchService,
            KeyWordBll keyWordService,
            TransferTypeBll transferTypeService,
            UserConnectionBll userConnectionService,
            SearchSettings searchSettings,
            PaperBll paperService,
            FeeBll feeService,
            ISearchInDatabase searchInDatabaseService,
            UserActivityLogBll userActivityLogService,
            ImageSettings imageSettings,
            Helper.UserSetting helperUserSetting,
            ResourceBll resourceService,
            CitizenBll citizenService,
            TemplateBll templateService,
            SendSmsHelper smsHelper,
            SendEmailHelper mailHelper,
            WorkflowHelper workflowHelper,
            TemplateHelper templateHelper,
            EmailSettings emailSettings,
            SmsSettings smsSettings,
            AdminGeneralSettings adminSetting,
            InterfaceConfigBll interfaceConfigService,
            DocumentPublishBll publishService,
            DocumentPublishPlusBll publishPlusService,
            LogBll logService,
            TransferSettings transferSetting, AuthorizeBll authorizeService,
            PermissionBll permissionSerice,
            CatalogBll catalogService,
            TemplateKeyBll templateKeyService)
        {
            _docOnlineService = docOnlineService;
            _docfieldService = docfieldService;
            _docTypeService = docTypeService;
            _categoryService = categoryService;
            _documentService = documentService;
            _formHelper = formHelper;
            _documentCopyService = documentCopyService;
            _userService = userService;
            _departmentService = departmentService;
            _commentService = commentService;
            _approverService = approverService;
            _documentContentService = documentContentService;
            _codeService = codeService;
            _documentPermissionHelper = documentPermissionHelper;
            _docTimelineService = docTimelineService;
            _commonCommentService = commonCommentBll;
            _supplementaryServcie = supplementaryServcie;
            _workTimeHelper = worktimeHelper;
            _extensionTimeService = extensionTimeService;
            _fileUploadSettings = fileUploadSettings;
            _storePrivateService = storePrivateService;
            _workflowService = workflowService;
            _addressService = addressService;
            _storeService = storeService;
            _doctypeFormService = docTypeFormService;
            _businessLicenseService = businessLicenseService;
            _anticipateService = anticipateService;
            _formService = formService;
            _docfinishService = docfinishService;
            _attachmentService = attachmentService;
            _searchService = searchService;
            _keyWordService = keyWordService;
            _paperService = paperService;
            _feeService = feeService;
            _transferTypeService = transferTypeService;
            _userConnectionService = userConnectionService;
            //_hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationHubs>();
            _searchSettings = searchSettings;
            _searchInDatabaseService = searchInDatabaseService;
            _userActivityLogService = userActivityLogService;
            _imageSettings = imageSettings;
            _helperUserSetting = helperUserSetting;
            _resourceService = resourceService;
            _citizenService = citizenService;
            _templateService = templateService;
            _smsHelper = smsHelper;
            _mailHelper = mailHelper;
            _workflowHelper = workflowHelper;
            _templateHelper = templateHelper;
            _emailSettings = emailSettings;
            _smsSettings = smsSettings;
            _adminSetting = adminSetting;
            _interfaceConfigService = interfaceConfigService;
            _publishService = publishService;
            _publishPlusService = publishPlusService;
            _permissionSerice = permissionSerice;
            _logService = logService;
            _transferSetting = transferSetting;
            _authorizeService = authorizeService;
            _catalogService = catalogService;
            _templateKeyService = templateKeyService;
        }

        #endregion

        #region Instance Methods

        #region Tạo mới, chỉnh sửa văn bản

        /// <summary>
        /// Lấy template giống hàm trên, trả về thêm id
        /// </summary>
        /// <param name="isCreate"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetDocumentTemplate(bool isCreate, int? categoryBusinessId, Guid? docTypeId, int? currentNodeId)
        {
            var result = string.Empty;
            var user = _userService.CurrentUser;

            if (!categoryBusinessId.HasValue)
            {
                if (!docTypeId.HasValue)
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }

                var docType = _docTypeService.GetFromCache(docTypeId.Value);
                categoryBusinessId = docType.CategoryBusinessId;
            }

            result = Business.Utils.DocumentHelper.GetDocumentTemplate((CategoryBusinessTypes)categoryBusinessId.Value);

            if (string.IsNullOrEmpty(result))
            {
                LogException("Chưa cấu hình giao diện cho nghiệp vụ");
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDataCompile(string timekey)
        {
            var currentId = _userService.CurrentUser;
            var department = _departmentService.GetPrimaryDepartment(currentId.UserId);
            var document = _documentService.GetDataTH<List<string>>(department.Emails, timekey);
            return Json(new { compile = document }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDocumentInfoForCreate(Guid id, int? documentCopyRelationId)
        {
            #region Kiểm tra đầu vào

            var doctype = _docTypeService.GetFromCache(id);
            if (doctype == null)
            {
                return Json(new { error = "Không khởi tạo được văn bản lúc này, vui lòng khởi động lại hệ thống!" }, JsonRequestBehavior.AllowGet);
            }

            if (doctype.WorkflowId == 0)
            {
                return Json(new { error = "Loại văn bản, hồ sơ chưa có quy trình xử lý!" }, JsonRequestBehavior.AllowGet);
            }

            var workflow = _workflowService.GetFromCache(doctype.WorkflowId);
            if (workflow == null)
            {
                return Json(new { error = "Quy trình xử lý loại văn bản, hồ sơ hiện không tồn tại!" }, JsonRequestBehavior.AllowGet);
            }

            #endregion

            #region Kiểm tra quyền khởi tạo văn bản

            var userSendId = CurrentUserId();
            var startNode = _documentPermissionHelper.CheckForQuyenKhoiTaoVanBan(workflow, userSendId);
            if (startNode == null || !startNode.Any())
            {
                return Json(new { error = "Không có quyền khởi tạo hồ sơ, văn bản theo quy trình!" }, JsonRequestBehavior.AllowGet);
            }

            #endregion

            #region Văn bản liên quan Trả lời

            var docRelationModel = GetDocumentAnswerRelation(documentCopyRelationId);

            #endregion

            var doccode = "";
            var listDocCodes = new List<string>();
            var inOutCodes = new Dictionary<int, string>();
            int cId = 0;

            var userSettings = _helperUserSetting.GetUserCurrentSetting();
            var categoryId = doctype.CategoryId;
            int? storeId = null;
            if (userSettings.StoreIds.ContainsKey(id))
            {
                storeId = userSettings.StoreIds[id];
            }

            var allStores = doctype.Stores;

#if !HoSoMotCuaEdition
            // Fix tạm cho BRVT
            allStores = allStores.Where(s => s.ListUserViewIds.Contains(userSendId));
#endif

            #region Lấy số đến đi

            if (doctype.CategoryBusinessIdInEnum == CategoryBusinessTypes.VbDen && allStores.Any())
            {
                var vbStores = allStores.Where(s => s.CategoryBusinessIdInEnum == CategoryBusinessTypes.VbDen);
                if (vbStores.Any())
                {
                    // Kiểm tra sổ tiếp nhận gần nhất thuộc danh sách sổ của loại văn bản hiện tại thì chọn mặc định luôn.
                    storeId = (storeId.HasValue && vbStores.Any(s => s.StoreId == storeId.Value)) ? storeId : vbStores.First().StoreId;

                    var codeIds = _codeService.GetCodeIds(storeId.Value, categoryId);
                    if (codeIds != null && codeIds.Any())
                    {
                        foreach (var codeId in codeIds)
                        {
                            inOutCodes.Add(codeId, _codeService.GetCode(codeId, DateTime.Now, CategoryBusinessTypes.VbDen, isDocCode: false, storeId: storeId.Value));
                        }

                        cId = codeIds.First();
                    }
                }
            }

            #endregion

            #region Lấy Số ký hiệu/ mã hồ sơ

            var hasGetDocCodeForCreate = false;

#if HoSoMotCuaEdition

			if (doctype.IsHsmc())
			{
				hasGetDocCodeForCreate = true;
			}

#endif

            if (!hasGetDocCodeForCreate)
            {
                hasGetDocCodeForCreate = startNode.First().ChoPhepCapSoKhiKhoiTao;
            }

            if (hasGetDocCodeForCreate)
            {
                if (allStores.Any())
                {
                    var codeIds = allStores.First().CodeIds;
                    if (codeIds.Any())
                    {
                        foreach (var codeId in codeIds)
                        {
                            listDocCodes.Add(_codeService.GetCode(codeId, DateTime.Now, doctype.CategoryBusinessIdInEnum));
                        }

                        cId = codeIds.First();
                        doccode = listDocCodes.First();
                    }
                }
            }

            #endregion

            var stores = allStores.Select(s => new
            {
                StoreName = s.StoreName,
                StoreId = s.StoreId,
                Codes = new List<string>()
            });

            var workflowTypes = workflow.WorkflowTypeJson != null ? Json2.ParseAs<List<WorkflowType>>(workflow.WorkflowTypeJson) : null;
            var formatDate = "yyyy-MM-ddTHH:mm:ss";
            var dateFormat = DateTime.Parse(DateTime.Now.ToString(formatDate));

            DateTime? dateAppointed = null;
            dateAppointed = _workTimeHelper.GetDateAppoint(DateTime.Now, workflow.ExpireProcess);

            stores = stores.OrderByDescending(s => storeId.HasValue ? s.StoreId == storeId.Value : false).ToList();
            var survey = _docTypeService.Get(id);
            var model = new DocumentModel
            {
                ExpireProcess = workflow.ExpireProcess,
                DateAppointed = dateAppointed,
                DateOverdue = dateAppointed,
                DateArrived = dateFormat,
                DatePublished = dateFormat,
                DateCreated = dateFormat,
                Compendium = doctype.CompendiumDefault ?? doctype.DocTypeName,
                DocCode = doccode,
                RelationModels = docRelationModel,
                DocumentContents = LoadFormDoctype(doctype.DocTypeId, CategoryBusinessTypes.Hsmc),
                DocFees = doctype.IsHsmc() ? GetDocFees(id, FeeType.TiepNhan) : new List<DocFeeModel>(),
                DocPapers = doctype.IsHsmc() ? GetDocPapers(id, PaperType.TiepNhan) : new List<DocPaperModel>(),
                DocTypeId = id,
                Stores = stores,
                InOutCodes = inOutCodes,
                InOutCode = inOutCodes.Any() ? inOutCodes.First().Value : "",
                DocCodes = listDocCodes,
                TransferType = (int)TransferTypes.TaoMoi,
                CategoryBusinessId = doctype.CategoryBusinessId,
                ActionLevel = doctype.ActionLevel,
                CategoryId = categoryId,
                IsAcknowledged = false,
                WorkflowTypes = workflowTypes,
                CodeId = cId,
                Note = "",
                TypeReturned = 1,
                SurveyConfig = survey.SurveyConfig,
                SurveyCriteria = survey.SurveyCriteria,
                SurveyReport = survey.SurveyReport,
                SurveyImg = survey.SurveyImg,
                SurveyImgPath = survey.SurveyImgPath,
                IsActivated = survey.IsActivated,
                DocTypeCode = survey.DocTypeCode,
                DocTypeName = survey.DocTypeName
            };

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCatalog(int docfieldId = 0)
        {
            var docfieldIds = string.Format(";{0};", docfieldId, ToString());
            var catalogs = _catalogService.Gets(d => d.DocfieldIds.Contains(docfieldIds));
            if (catalogs != null && catalogs.Any())
            {
                var jsonCatalog = catalogs;
                return Json(new { error = false, data = jsonCatalog }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { error = true }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTrangThaiLienThong(int id)
        {
            var result = _publishPlusService.GetDocPublishPlusByDocCopyId(id);
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
            //return new JsonResult {Data = result };
        }
        public JsonResult GetDocumentDetail(int id)
        {
            var currentUserId = CurrentUserId();
            var document = _documentCopyService.GetFromCache(id, currentUserId);
            if (document == null)
            {
                return Json(new { error = "Văn bản không mở được lúc này, vui lòng khởi động lại hệ thống." }, JsonRequestBehavior.AllowGet);
            }

            if (document.CategoryBusinessId == 32)
            {
                var doc = _documentCopyService.Get(id);
                document.StatusReport = _documentService.Get(document.DocumentId)?.StatusReport;
                document.Note = doc?.Note;
                document.ProcessInfo = doc?.ProcessInfoPlus;
                // Get all đơn vị nhận
                var parentId = document.ParentId ?? document.DocumentCopyId;
                document.ReceiveUnits = _documentCopyService.Gets(c => c.ParentId == parentId).Select(r => new ReceiveUnit { UserUnit = r.UserCurrentId, UserName = r.UserCurrentName, UserNote = r.Note, UserStatus = r.Status }).ToList();
            }
            int realUserId;
            if (!_documentPermissionHelper.CheckForUyQuyenVaXuLyVanBan(document, currentUserId, out realUserId))
            {
                if (!_documentPermissionHelper.CheckForQuyenXem(document, currentUserId))
                {
                    LogException("Không có quyền xem văn bản");
                    return Json(new { error = "Bạn không có quyền xem văn bản!" }, JsonRequestBehavior.AllowGet);
                }
            }

            #region Trạng thái đã xem

            if (!document.IsViewed(realUserId))
            {
                _documentCopyService.SetViewed(document.DocumentCopyId, realUserId, viewed: true);
                document.UserNguoiDaXem += realUserId + ";";
            }

            var configForm = "{}";
            var forms = _doctypeFormService.GetForms(d => new
            {
                d.Form.FormId,
                d.Form.FormName,
                d.Form.FormTypeId,
                d.IsPrimary,
                d.Form.EmbryonicPath,
                d.Form.EmbryonicLocationName,
                d.Form.Template,
                d.Form.Json,
                d.Form.FormCode
            }, document.DocTypeId.Value);

            var docContents = document.DocumentContents.Where(c => !string.IsNullOrEmpty(c.ContentUrl));

            if (forms != null && forms.Any())
            {
                var firstForm = forms.FirstOrDefault();
                configForm = firstForm.Json;

                // 20200113 VuHQ
                if (document.DocumentContents.Count() > 0)
                    document.DocumentContents.ElementAt(0).FormId = firstForm.FormId.ToString();
                else
                {
                    var tempDocContents = LoadFormDoctypePlus(Guid.Parse(document.DocTypeId.ToString()), CategoryBusinessTypes.Hsmc);
                    document.DocumentContents = tempDocContents;
                    document.DocumentContents.ElementAt(0).FormId = firstForm.FormId.ToString();
                    document.ProcessInfo = firstForm.FormCode;
                }
            }

            #endregion

            #region Dự kiến phát hành

            var publishPlan = GetPublishPlan(document.DocumentId);

            #endregion

            #region Tiến độ liên thông

            var lienthongs = GetPublisheds(id);

            #endregion

            var documentPermission = realUserId == document.UserCurrentId ? document.DocumentPermissions
                                                : (int)_documentPermissionHelper.CheckForView(document, realUserId);

            var hasPrivateSaveToStore = document.NodeCurrentPermission != null
                        ? EnumHelper<NodePermissions>.ContainFlags((NodePermissions)document.NodeCurrentPermission, NodePermissions.QuyenCapSoTruoc)
                        : false;

            var stores = GetStores(document.StoreId);
            document.Stores = stores;

            if (document.UserCreatedId != currentUserId)
            {

                if (document.DocumentContents.Count() > 0)
                {
                    document.DocumentContents.ElementAt(0).Content = document.Note;
                }
            }

            return Json(new
            {
                document = document,
                lienThongs = lienthongs,
                documentPermission = documentPermission,
                plan = new { publish = publishPlan },
                hasUpdatePermission = (documentPermission & (int)DocumentPermissions.Luuvanban) == (int)DocumentPermissions.Luuvanban,
                hasPrivateSaveToStore = hasPrivateSaveToStore,
                hasKhoiTao = !document.DocTypeId.HasValue || document.UserCreatedId == currentUserId
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDocumentDetails(List<int> ids)
        {
            var currentUserId = CurrentUserId();
            var documents = _documentCopyService.GetsFromCache(ids, currentUserId);
            if (documents == null || !documents.Any())
            {
                return Json(new { error = "Văn bản không mở được lúc này, vui lòng khởi động lại hệ thống." }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                documents = documents
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDocumentInfoForCreateMobile(Guid id, int? documentCopyRelationId)
        {
            #region Kiểm tra đầu vào

            var doctype = _docTypeService.GetFromCache(id);
            if (doctype == null)
            {
                return Json(new { error = "Không khởi tạo được văn bản lúc này, vui lòng khởi động lại hệ thống!" }, JsonRequestBehavior.AllowGet);
            }

            if (doctype.WorkflowId == 0)
            {
                return Json(new { error = "Loại văn bản, hồ sơ chưa có quy trình xử lý!" }, JsonRequestBehavior.AllowGet);
            }

            var workflow = _workflowService.GetFromCache(doctype.WorkflowId);
            if (workflow == null)
            {
                return Json(new { error = "Quy trình xử lý loại văn bản, hồ sơ hiện không tồn tại!" }, JsonRequestBehavior.AllowGet);
            }

            #endregion

            #region Kiểm tra quyền khởi tạo văn bản

            var userSendId = CurrentUserId();
            var startNode = _documentPermissionHelper.CheckForQuyenKhoiTaoVanBan(workflow, userSendId);
            if (startNode == null || !startNode.Any())
            {
                return Json(new { error = "Không có quyền khởi tạo hồ sơ, văn bản theo quy trình!" }, JsonRequestBehavior.AllowGet);
            }

            #endregion

            var doccode = "";
            var listDocCodes = new List<string>();
            var inOutCodes = new Dictionary<int, string>();
            int cId = 0;

            var userSettings = _helperUserSetting.GetUserCurrentSetting();
            var categoryId = userSettings.CategoryId ?? doctype.CategoryId;
            int? storeId = null;
            if (userSettings.StoreIds.ContainsKey(id))
            {
                storeId = userSettings.StoreIds[id];
            }

            var allStores = doctype.Stores;

            if (allStores.Any())
            {
                var codeIds = allStores.First().CodeIds;
                if (codeIds.Any())
                {
                    foreach (var codeId in codeIds)
                    {
                        listDocCodes.Add(_codeService.GetCode(codeId, DateTime.Now, doctype.CategoryBusinessIdInEnum));
                    }

                    cId = codeIds.First();
                    doccode = listDocCodes.First();
                }
            }

            var workflowTypes = workflow.WorkflowTypeJson != null ? Json2.ParseAs<List<WorkflowType>>(workflow.WorkflowTypeJson) : null;
            var formatDate = "yyyy-MM-ddTHH:mm:ss";
            var dateFormat = DateTime.Parse(DateTime.Now.ToString(formatDate));

            DateTime? dateAppointed = null;
            dateAppointed = _workTimeHelper.GetDateAppoint(DateTime.Now, workflow.ExpireProcess);
            var docContents = LoadFormDoctype(doctype.DocTypeId, CategoryBusinessTypes.Hsmc);
            var jsonForm = "{}";
            var configForm = "{}";
            var formCode = "";
            var forms = _doctypeFormService.GetForms(d => new
            {
                d.Form.FormId,
                d.Form.FormName,
                d.Form.FormTypeId,
                d.IsPrimary,
                d.Form.EmbryonicPath,
                d.Form.EmbryonicLocationName,
                d.Form.Template,
                d.Form.Json,
                d.Form.DefineValueJson,
                d.Form.FormCode
            }, id);

            if (docContents != null && docContents.Any())
            {
                // 20190103 VuHQ START
                //var docContent = docContents.FirstOrDefault();
                //var path = CommonHelper.MapPath("~/" + docContent.ContentUrl);
                //var xlsxParser = new XlsxToJson(path);
                //var excelData = xlsxParser.ConvertXlsxToJson(1, 1, 1, 3);
                //jsonForm = JsonConvert.SerializeObject(excelData);
                // 20190103 VuHQ END
            }

            if (forms != null && forms.Any())
            {
                var firstForm = forms.FirstOrDefault();
                configForm = firstForm.Json;
                dynamic defineValueConfig = JsonConvert.DeserializeObject(firstForm.DefineValueJson);
                jsonForm = JsonConvert.SerializeObject(defineValueConfig.data);
                formCode = firstForm.FormCode;
            }

            return Json(new
            {
                ExpireProcess = workflow.ExpireProcess,
                DateAppointed = dateAppointed,
                DateOverdue = dateAppointed,
                DateArrived = dateFormat,
                DatePublished = dateFormat,
                DateCreated = dateFormat,
                Compendium = doctype.CompendiumDefault ?? doctype.DocTypeName,
                DocCode = doccode,
                DocumentContents = docContents,
                DocFees = doctype.IsHsmc() ? GetDocFees(id, FeeType.TiepNhan) : new List<DocFeeModel>(),
                DocPapers = doctype.IsHsmc() ? GetDocPapers(id, PaperType.TiepNhan) : new List<DocPaperModel>(),
                DocTypeId = id,
                InOutCodes = inOutCodes,
                InOutCode = inOutCodes.Any() ? inOutCodes.First().Value : "",
                DocCodes = listDocCodes,
                TransferType = (int)TransferTypes.TaoMoi,
                CategoryBusinessId = doctype.CategoryBusinessId,
                CategoryId = categoryId,
                IsAcknowledged = false,
                WorkflowTypes = workflowTypes,
                CodeId = cId,
                Note = "",
                TypeReturned = 1,
                JsonForm = jsonForm,
                configForm = configForm,
                formCode = formCode
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDocumentInfoForMobile(int id, int? storePrivateId)
        {
            var currentUserId = CurrentUserId();
            var document = _documentCopyService.GetFromCache(id, currentUserId);
            if (document == null)
            {
                return Json(new { error = "Văn bản không mở được lúc này, vui lòng khởi động lại hệ thống." }, JsonRequestBehavior.AllowGet);
            }

            int realUserId;
            if (!_documentPermissionHelper.CheckForUyQuyenVaXuLyVanBan(document, currentUserId, out realUserId))
            {
                if (!_documentPermissionHelper.CheckForQuyenXem(document, currentUserId))
                {
                    LogException("Không có quyền xem văn bản");
                    return Json(new { error = "Bạn không có quyền xem văn bản!" }, JsonRequestBehavior.AllowGet);
                }
            }

            var expireProcess = document.ExpireProcess.HasValue ? document.ExpireProcess : 1;

            var documentPermission = realUserId == document.UserCurrentId ? document.DocumentPermissions
                                                : (int)_documentPermissionHelper.CheckForView(document, realUserId);

            #region Trạng thái đã xem

            if (!document.IsViewed(realUserId))
            {
                _documentCopyService.SetViewed(document.DocumentCopyId, realUserId, viewed: true);
                document.UserNguoiDaXem += realUserId + ";";
            }
            var jsonForm = "{}";
            var configForm = "{}";
            var formCode = "";
            var forms = _doctypeFormService.GetForms(d => new
            {
                d.Form.FormId,
                d.Form.FormName,
                d.Form.FormTypeId,
                d.IsPrimary,
                d.Form.EmbryonicPath,
                d.Form.EmbryonicLocationName,
                d.Form.Template,
                d.Form.Json,
                d.Form.FormCode
            }, document.DocTypeId.Value);

            var docContents = document.DocumentContents.Where(c => !string.IsNullOrEmpty(c.ContentUrl));
            if (docContents.Any())
            {
                // 20200116 VuHQ trường hợp cũ, với trường hợp mới ko có excel thì sẽ bỏ qua
                try
                {
                    var docContent = docContents.FirstOrDefault();

                    var path = CommonHelper.MapPath("~/" + docContent.ContentUrl);
                    var xlsxParser = new XlsxToJson(path);
                    var excelData = xlsxParser.ConvertXlsxToJson(1, 1, 1, 3);
                    jsonForm = JsonConvert.SerializeObject(excelData);
                }
                catch { }
            };

            if (forms != null && forms.Any())
            {
                var firstForm = forms.FirstOrDefault();
                configForm = firstForm.Json;

                // 20200113 VuHQ
                formCode = firstForm.FormCode;

                if (document.DocumentContents.Count() > 0)
                    document.DocumentContents.ElementAt(0).FormId = firstForm.FormId.ToString();

            }
            #endregion

            return Json(new
            {
                DocumentPermissions = documentPermission,
                CommentList = document.CommentList,
                Attachments = document.Attachments,
                DocTypeId = document.DocTypeId,
                DocTypeName = document.DocTypeName,
                DocumentContents = document.DocumentContents.ToList(),
                TransferType = (int)TransferTypes.CapNhat,
                Organization = document.Organization,
                DocCode = document.DocCode,
                InOutCode = document.InOutCode,
                DocumentId = document.DocumentId,
                DocumentCopyId = document.DocumentCopyId,
                Compendium = document.Compendium,
                Status = document.Status,
                UserCurrentId = document.UserCurrentId,
                DateReceived = document.DateReceived,
                JsonForm = jsonForm,
                configForm = configForm,
                FormCode = formCode,
                InOutPlace = document.InOutPlace,
                Note = document.Note
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetListToDoctype(Guid doctypeId, string organizationCode , string timeKey, string year) {
            var result = _documentService.GetToDoctypeId(doctypeId, organizationCode, timeKey, year) != null ? _documentService.GetToDoctypeId(doctypeId, organizationCode, timeKey, year)  : null ;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        private string GetPublishPlan(Guid documentId)
        {
            var publishPlan = _anticipateService.Get(documentId, AnticipateType.PhatHanh);
            return publishPlan == null ? "" : publishPlan.Destination;
        }

        private IEnumerable<LienThongTracesModel> GetPublisheds(int documentCopyId)
        {
            var result = new List<LienThongTracesModel>();
            var docPublishes = _publishService.GetPublishes(documentCopyId).OrderByDescending(d => d.DatePublished);
            var addesses = _addressService.GetsFromCache();
            foreach (var doc in docPublishes)
            {
                var address = addesses.SingleOrDefault(a => a.AddressId == doc.AddressId);
                var addressName = doc.AddressName;

                var note = GetLtStatus(doc);
                var hasRecalled = doc.Status == 0 && (doc.IsPending || (address == null ? false : address.HasRecalled));

                result.Add(new LienThongTracesModel()
                {
                    PublishId = doc.DocumentPublishId,
                    AddressName = doc.AddressName,
                    AddressId = doc.AddressId.Value,
                    DateSent = doc.DatePublished.ToString("hh:mm dd/MM/yyyy"),
                    DateAppointed = doc.DateAppointed.HasValue ? doc.DateAppointed.Value.ToString("hh:mm dd/MM/yyyy") : "",
                    IsResponsed = doc.IsResponsed,
                    IsPending = doc.IsPending,
                    Note = note,
                    DateResponsed = doc.DateResponsed.HasValue ? doc.DateResponsed.Value.ToString("hh:mm dd/MM/yyyy") : "",
                    Traces = GetLienThongTraces(doc.Traces),
                    Status = doc.Status,
                    HasRecalled = hasRecalled
                });
            }

            return result;
        }

        private string GetLtStatus(DocPublish doc)
        {
            if (doc.IsPending)
            {
                return "Đang gửi";
            }

            if (doc.IsResponsed)
            {
                return "Đã phản hồi";
            }

            if (doc.Status == 13)
            {
                return "Đã gửi y/c lấy lại";
            }

            if (doc.Status == 15)
            {
                return "Đã lấy lại";
            }

            if (doc.Status == 16)
            {
                return "Đã bị từ chối lấy lại";
            }

            return "Đã gửi";
        }

        private List<LienThongTraces> GetLienThongTraces(string traces)
        {
            var result = new List<LienThongTraces>();
            if (string.IsNullOrEmpty(traces) || traces == "null")
            {
                return result;
            }

            var lienThongTraces = Json2.ParseAs<List<Bkav.eGovCloud.Entities.Customer.eDoc.DocumentTrace>>(traces);
            foreach (var trace in lienThongTraces)
            {
                result.Add(new LienThongTraces()
                {
                    Content = trace.Comment,
                    UserName = trace.UserName,
                    DateCreated = trace.DateCreated.ToString("hh:mm dd/MM/yyyy"),
                    IsSuccess = true
                });
            }

            return result;
        }

        public JsonResult GetCodeTemp()
        {
            var currentUserId = CurrentUserId();
            var currenUser = _userService.GetFromCache(currentUserId);
            var codes = _codeService.GetCodeTemp(currentUserId);
            var result = new List<CodeTempModel>();

            foreach (var code in codes)
            {
                result.Add(new CodeTempModel()
                {
                    CodeTempId = code.CodeTempId,
                    Code = code.Code,
                    FullName = currenUser == null ? "" : currenUser.FullName + " (" + currenUser.Username + ")",
                    DateCreated = code.DateCreated.ToString("dd/MM/yyyy")
                });
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteCodeTemp(int codeTempId)
        {
            try
            {
                _codeService.DeleteCodeTemp(codeTempId);
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
            return Json(new { success = true });
        }

        [HttpPost]
        public JsonResult DeleteAllCodeTemp()
        {
            var currentUserId = CurrentUserId();
            try
            {
                _codeService.DeleteAllCodeTempCancelUsing(currentUserId);
            }
            catch
            {
                return Json(new { success = false });
            }
            return Json(new { success = true });
        }

        [HttpPost]
        public JsonResult EditContent(int contentId, string content)
        {
            var documentContent = _documentContentService.Get(contentId);
            if (documentContent != null)
            {
                documentContent.Content = HttpUtility.HtmlDecode(content);
                _documentContentService.Update(documentContent);
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        /// <summary>
        ///   Lấy ngày hẹn trả khi thay đổi số ngày thụ lý
        /// </summary>
        /// <param name="range"> </param>
        /// <returns> </returns>
        public JsonResult GetDateAppointed(int range, DateTime? dateCreated = null)
        {
            var date = _workTimeHelper.GetDateAppoint(dateCreated == null ? DateTime.Now : dateCreated.Value, range);

            return JsonNet(date, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDocumentContentVersion(int documentContentId, int version)
        {
            var result = _documentContentService.GetDetails(documentContentId, version);
            return Json(result.OrderByDescending(d => d.CreatedOnDate), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDocumentRelations(int id)
        {
            var documentCopy = _documentCopyService.Get(id);
            if (documentCopy == null)
            {
                ErrorNotification("Văn bản bản sao không tồn tại!");
                return Json(new { error = "Văn bản bản sao không tồn tại!" }, JsonRequestBehavior.AllowGet);
            }

            var currentUserId = CurrentUserId();
            int userSendId;
            if (!_documentPermissionHelper.CheckForUyQuyenVaXuLyVanBan(documentCopy, currentUserId, out userSendId))
            {
                if (documentCopy.Document.Original == 2 && documentCopy.UserCurrentId == 0)
                {
                    userSendId = currentUserId;
                }
                else
                {
                    if (_documentPermissionHelper.CheckForQuyenXem(documentCopy, currentUserId))
                    {
                        userSendId = currentUserId;
                    }
                    else
                    {
                        LogException("Không có quyền xem văn bản");
                        return Json(new { error = "Bạn không có quyền xem văn bản!" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }

            return Json(GetDocumentRelations(documentCopy.DocumentCopyId, userSendId), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Tao
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pages"></param>
        /// <returns></returns>
        public JsonResult CreateImagesFromBeginAndLastPdfPages(string id)
        {
            var pdfParser = new PdfParser(_imageSettings.ColorBits);
            var filePath = ResourceLocation.Default.FileUploadTemp + "\\" + id;
            var saveTo = Server.MapPath("/ImagesInDoc");
            if (!System.IO.Directory.Exists(saveTo))
            {
                try
                {
                    Directory.CreateDirectory(saveTo);
                }
                catch
                {
                    return null;
                }
            }

            var images = pdfParser.ConvertToImages(filePath, saveTo, _imageSettings.NumberStartPage, _imageSettings.NumberEndPage);
            for (var i = 0; i < images.Count; i++)
            {
                images[i] = "/ImagesInDoc//" + images[i];
            }

            DocumentInfoFromImage documentInfo = null;
            if (_helperUserSetting.GetUserCurrentSetting().AutoInsertDocumentInfoScan)
            {
                var tempPath = ResourceLocation.Default.FileUploadTemp + "\\";
                var imageInfo = pdfParser.ConvertToImages(filePath, tempPath, 1, 0, 300);
                documentInfo = new ImageToText().GetDocumentInfo(tempPath + imageInfo[0]);
            }

            return Json(new { images = images, documentInfo = documentInfo }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>change
        /// Tao
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pages"></param>
        /// <returns></returns>
        public JsonResult GetImageTemp(string id, string extension)
        {
            var saveTo = Server.MapPath("/ImagesInDoc") + "\\" + id + extension;
            var path = ResourceLocation.Default.FileUploadTemp + "\\" + id;
            System.IO.File.Copy(path, saveTo);
            var image = "/ImagesInDoc//" + id + extension;
            return Json(image, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSearchAdvanceForm()
        {
            //var categories = _categoryService.GetsFromCache();
            //var categoryIds = categories.Select(u => new SelectListItem
            //{
            //    Value = u.CategoryId.ToString(CultureInfo.InvariantCulture),
            //    Text = u.CategoryName
            //});

            // var urgentIds = _resourceService.EnumToSelectList<Urgent>();

            //var docFields = _docfieldService.GetsAs(f => new { f.DocFieldId, f.DocFieldName });
            //var docFieldIds = docFields.Select(u => new SelectListItem
            //{
            //    Value = u.DocFieldId.ToString(CultureInfo.InvariantCulture),
            //    Text = u.DocFieldName
            //});

            //var usersApprove = _userService.GetUsersApprove((u, udj) => new { u.UserId, u.FullName, u.Username }).OrderBy(u => u.Username);
            //var userApprove = usersApprove.Select(u => new
            //{
            //    value = u.UserId,
            //    label = u.Username + " - " + u.FullName,
            //    username = u.Username
            //}).StringifyJs();

            var storePrivate =
                _storePrivateService.GetsStorePrivate(CurrentUserId(), 0)
                    .Select(s => new SelectListItem { Value = s.StorePrivateId.ToString(), Text = s.StorePrivateName })
                    .ToList();

            var storeShare =
                _storePrivateService.GetsStoreShared(CurrentUserId(), 0)
                    .Select(s => new SelectListItem { Value = s.StorePrivateId.ToString(), Text = s.StorePrivateName });

            storePrivate.AddRange(storeShare);

            // var organization = _addressService.GetsAs(a => a.Name, "Name").Select(s => new { value = s, label = s }).StringifyJs();

            return Json(new
            {
                // CategoryIds = categoryIds,
                // UrgentIds = urgentIds,
                // DocFieldIds = docFieldIds,
                // UserApprove = userApprove,
                StorePrivateIds = storePrivate,
                // Organization = organization
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Template Comment

        /// <summary>
        /// Lấy danh sách các mẫu template ý kiến mà người dùng đã soạn mẫu trước
        /// </summary>
        /// <returns></returns>
        public JsonResult GetTemplateComments()
        {
            var userId = CurrentUserId();
            var listTemplateComent = _commonCommentService.Gets(p => p.UserId == userId);
            if (listTemplateComent.Any() && listTemplateComent != null)
            {
                foreach (var item in listTemplateComent)
                {
                    item.Content = GlobalObject.unescape(item.Content);
                }
            }
            return Json(listTemplateComent, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Lấy danh sách các mẫu template ý kiến mà người dùng đã soạn mẫu trước
        /// </summary>
        /// <param name="commentText"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult CreateTemplateComments(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return Json(new { success = false, message = "Nội dung không được trống. Vui lòng nhập lại!" });
            }

            var userId = CurrentUserId();
            var commonComment = new CommonComment
            {
                Content = GlobalObject.escape(content),
                UserId = userId
            };
            _commonCommentService.Create(commonComment);
            commonComment.Content = GlobalObject.unescape(content);
            return Json(new { success = true, data = commonComment, message = "Tạo mới thành công!" });
        }

        #endregion

        #region Xác nhận xử lý

        /// <summary>
        ///   Xác nhận xử lý văn bản
        /// </summary>
        /// <param name="documentCopyId"> </param>
        /// <returns> </returns>
        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "DocumentConfirmProcess")]
        [ValidateAntiForgeryToken]
        public JsonResult ConfirmProcess(int documentCopyId)
        {
            var documentCopy = _documentCopyService.Get(documentCopyId);
            int userSendId;
            // Check quyen Xac nhan xu ly
            if (!_documentPermissionHelper.CheckForUyQuyenVaXuLyVanBan(documentCopy, CurrentUserId(), out userSendId) ||
                _documentPermissionHelper.Check(documentCopy, userSendId, DocumentPermissions.XacNhanXuLy) != DocumentPermissions.XacNhanXuLy)
            {
                return Json(new { error = "Không có quyền Xác nhận xử lý!" });
            }

            try
            {
                string message;
                List<JsConfirmProcess> confirmProcesses;
                List<DocumentCopy> documentCopiesProcess;
                if (!ValidateConfirmProcess(documentCopy, out message, out confirmProcesses, out documentCopiesProcess))
                {
                    return Json(new { error = message });
                }

                DoConfirmProcess(documentCopy, documentCopiesProcess);
            }
            catch (Exception)
            {
                return Json(new { error = "Xác nhận xử lý không thành công. Vui lòng thực hiện lại thao tác." });
            }

            return Json(new { success = "Xác nhận xử lý thành công." });
        }

        /// <summary>
        /// Cập nhật dữ liệu khi xác nhận xử lý
        /// </summary>
        /// <param name="currentDocumentCopy"> </param>
        /// <param name="documentCopiesProcess"> Văn bản copy </param>
        private void DoConfirmProcess(DocumentCopy currentDocumentCopy, List<DocumentCopy> documentCopiesProcess)
        {
            using (var trans = new TransactionScope())
            {
                var isHuongBangiaoChinh = currentDocumentCopy.Histories.HistoryPath.Count > 1;
                if (isHuongBangiaoChinh)
                {
                    foreach (var documentCopy in documentCopiesProcess)
                    {
                        if (documentCopy.Histories.HistoryPath.Count != 1)
                        {
                            throw new ArgumentException("documentCopiesProcess");
                        }
                        _documentCopyService.Delete(documentCopy);
                    }
                }
                else
                {
                    // Tim huong xu ly chinh
                    var documentCopyXlcs =
                        documentCopiesProcess.Where(c => c.Histories.HistoryPath.Count > 1).Select(o => o).ToList();
                    if (documentCopyXlcs.Count() != 1)
                    {
                        throw new ArgumentException("documentCopiesProcess");
                    }
                    var documentCopyXlc = documentCopyXlcs.First();

                    // Cap nhat can bo giu van ban hien tai thanh xu ly chinh
                    _documentCopyService.ChangeUserXlc(documentCopyXlc, currentDocumentCopy.UserCurrentId);

                    var documentCopyOthers =
                        documentCopiesProcess.Where(c => c.Histories.HistoryPath.Count == 1).Select(o => o).ToList();
                    foreach (var documentCopy in documentCopyOthers)
                    {
                        _documentCopyService.Delete(documentCopy);
                    }
                    _documentCopyService.Delete(currentDocumentCopy);
                }
                trans.Complete();
            }
        }

        private bool ValidateConfirmProcess(DocumentCopy documentCopy, out string message,
                                    out List<JsConfirmProcess> confirmProcesses,
                                    out List<DocumentCopy> documentCopiesProcess)
        {
            message = "";
            confirmProcesses = new List<JsConfirmProcess>();
            documentCopiesProcess = new List<DocumentCopy>();

            var isHuongBangiaoChinh = documentCopy.IsHuongXuLyChinh();// IsHuongBangiaoXlc(documentCopy);
            var isHuongBangiaoDongxuly = documentCopy.Histories.HistoryPath.Count == 1 &&
                                         documentCopy.DocumentCopyTypeInEnum == DocumentCopyTypes.DongXuLy;

            HistoryPath historyPath;
            if (isHuongBangiaoChinh)
            {
                // Trường hợp này xử lý thế này luôn luôn đúng
                historyPath = documentCopy.Histories.HistoryPath[documentCopy.Histories.HistoryPath.Count - 2];
            }
            else if (isHuongBangiaoDongxuly)
            {
                Debug.Assert(documentCopy.ParentId != null, "documentCopy.ParentId != null");
                var parentDocumentCopy = _documentCopyService.Get(documentCopy.ParentId.Value);
                historyPath = parentDocumentCopy.Histories.HistoryPath[parentDocumentCopy.Histories.HistoryPath.Count - 2];
                // Neu huong chinh chua ban giao di
                var isChuaBangiao = historyPath.UserReceives.Any(userReceive => userReceive.DocumentCopyId == documentCopy.DocumentCopyId);
                if (!isChuaBangiao)
                {
                    // TODO: Sẽ xử lý khác sau này: vẫn mở form xác nhận xử lý lên bình thường, xong báo là ai đã xử lý văn bản rồi và không thể lấy lại được.
                    // TODO: Có thể báo rõ là ai đã xử lý (tức bàn giao đi) được ở chỗ này.
                    message = "Xac nhan xu ly khong thanh cong. Văn bản này đã được xử lý boi can bo khac.";
                    return false;
                }
            }
            else
            {
                LogException(string.Format("Chức năng xác nhận xử lý đang thực hiện chưa đúng. {0}", DateTime.Now));
                message = "Văn bản này không cần xác nhận xử lý.";
                return false;
            }

            if (historyPath.UserReceives.Count <= 1)
            {
                LogException(
                    string.Format(
                        "Chức năng xác nhận xử lý đang thực hiện chưa đúng. Khong can hien context menu xac nhan xu ly voi van ban nay {0}: {1}",
                        documentCopy.DocumentCopyId, DateTime.Now));
                message = "Văn bản này không cần xác nhận xử lý.";
                return false;
            }

            if (historyPath.UserReceives.Any(c => c.DateCreated != documentCopy.DateReceived))
            {
                LogException(
                    string.Format(
                        "Chức năng xác nhận xử lý đang thực hiện chưa đúng. Ghi log HistoryPath khi ban giao chua chinh xac. {0}",
                        DateTime.Now));
                message = "Co loi xay ra khi thuc hien chuc nang Xac nhan xu ly. Vui long thu lai";
                return false;
            }

            // Loai bo huong xu ly hien tai
            var userReceives = historyPath.UserReceives
                .Where(c => c.DocumentCopyId != documentCopy.DocumentCopyId).Select(o => o).ToList();

            documentCopiesProcess =
                _documentCopyService.Gets(userReceives.Select(c => c.DocumentCopyId).ToList()).ToList();
            var usersProcess = _userService.Gets(documentCopiesProcess.Select(t => t.UserCurrentId)).ToList();

            foreach (var userReceive in userReceives)
            {
                var documentCopyProcess =
                    documentCopiesProcess.Single(c1 => c1.DocumentCopyId == userReceive.DocumentCopyId);
                // Trong log ghi la huong chinh + la van ban Xlc, Dxl.
                var isHuongBangiaoChinhProcess = userReceive.IsXlc && documentCopyProcess.Histories.HistoryPath.Count > 1 &&
                                                 (documentCopyProcess.DocumentCopyTypeInEnum ==
                                                  DocumentCopyTypes.XuLyChinh ||
                                                  documentCopyProcess.DocumentCopyTypeInEnum ==
                                                  DocumentCopyTypes.DongXuLy);

                // Trong log ghi la khong phai huong chinh + la van ban Thong bao + la van ban DXL, chua ban giao di dau (histories.Count ==1)
                var isHuongBangiaoDongxulyProcess = !userReceive.IsXlc &&
                                                    ((documentCopyProcess.Histories.HistoryPath.Count == 1 &&
                                                      documentCopyProcess.DocumentCopyTypeInEnum ==
                                                      DocumentCopyTypes.DongXuLy) ||
                                                     (documentCopyProcess.DocumentCopyTypeInEnum ==
                                                      DocumentCopyTypes.ThongBao));

                if (isHuongBangiaoChinhProcess == isHuongBangiaoDongxulyProcess)
                {
                    message = "Xac nhan xu ly khong thanh cong. Văn bản này đã được xử lý boi can bo khac.";
                    return false;
                }

                var userProcess = usersProcess.Single(c => c.UserId == documentCopyProcess.UserCurrentId);
                var isViewed = documentCopyProcess.IsViewed(documentCopyProcess.UserCurrentId);
                confirmProcesses.Add(new JsConfirmProcess
                {
                    DocumentCopyId = userReceive.DocumentCopyId,
                    FullName = userProcess.FullName,
                    UserId = userProcess.UserId,
                    Username = userProcess.Username,
                    IsViewed = isViewed
                });
            }
            return true;
        }

        #endregion

        #region Cập nhật kết quả xử lý cuối

        [HttpPost]
        public JsonResult UpdateLastResult(bool result, string comment, int documentCopyId, bool isNotNeedSign = true)
        {
            var documentCopy = _documentCopyService.Get(documentCopyId);
            if (documentCopy == null)
            {
                LogException("UpdateLastResult: Văn bản đang xử lý không tồn tại");
                return Json(new { error = true }, JsonRequestBehavior.AllowGet);
            }

            var userSendId = CurrentUserId();
            if (!_documentPermissionHelper.CheckForQuyenXuly(documentCopy, userSendId) ||
               _documentPermissionHelper.Check(documentCopy, userSendId, DocumentPermissions.TraKetQua) != DocumentPermissions.TraKetQua)
            {
                LogException("UpdateLastResult: Không có quyền cập nhật kết quả xử lý cuối");
                return Json(new { error = "UpdateLastResult: Không có quyền cập nhật kết quả xử lý cuối" }, JsonRequestBehavior.AllowGet);
            }

            var document = documentCopy.Document;
            document.ResultStatus = result;
            document.DateResult = DateTime.Now;
            if (!isNotNeedSign)
            {
                var userSuccess = _userService.GetFromCache(userSendId);
                document.UserSuccessId = userSendId;
                document.UserSuccessName = userSuccess == null ? "" : userSuccess.FullName;
                document.IsSuccess = result;
            }
            else
            {
                document.ResultStatus = document.IsSuccess;
            }
            document.SuccessNote = comment;


            var commentLog = string.Format("{0} văn bản được cập nhật lúc {1} /n Nội dung: {2}", User.GetFullName(), document.DateResult.Value.ToString("dd/MM/yyyy HH:mm:ss"), comment); ;

            _documentService.UpdateResult(document, document.ResultStatus.Value, document.DateResult.Value);
            _documentCopyService.CommentUpdateResult(documentCopy, userSendId, document.DateResult.Value, commentLog);
            _documentCopyService.ClearCache(documentCopyId);

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteResult(int documentCopyId)
        {
            var documentCopy = _documentCopyService.Get(documentCopyId);
            if (documentCopy == null)
            {
                LogException("UpdateLastResult: Văn bản đang xử lý không tồn tại");
                return Json(new { error = true }, JsonRequestBehavior.AllowGet);
            }

            var userSendId = CurrentUserId();
            if (!_documentPermissionHelper.CheckForQuyenXuly(documentCopy, userSendId) ||
               _documentPermissionHelper.Check(documentCopy, userSendId, DocumentPermissions.CapNhatKetQuaXuLyCuoi) != DocumentPermissions.CapNhatKetQuaXuLyCuoi)
            {
                LogException("UpdateLastResult: Không có quyền xóa kết quả xử lý cuối");
                return Json(new { error = "UpdateLastResult: Không có quyền cập nhật kết quả xử lý cuối" }, JsonRequestBehavior.AllowGet);
            }

            var document = documentCopy.Document;
            document.ResultStatus = null;
            document.DateResult = DateTime.Now;
            document.SuccessNote = "";

            _documentService.Update(document);
            _documentCopyService.ClearCache(documentCopyId);

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Văn bản liên quan

        ///<summary>
        ///</summary>
        ///<param name="model"> </param>
        ///<returns> </returns>
        [HttpPost]
        public JsonResult SearchDocuments(SearchAdvangeModel model)
        {
            //HopCV: 160115
            // check khi trich yeu null
            if (string.IsNullOrWhiteSpace(model.Compendium))
            {
                return Json(new List<SearchViewModel>());
            }

            model.Page = 1;
            model.PageSize = _searchSettings.NumberSelected;

            var docs = _searchInDatabaseService.SearchAdvanceInDatabase(
                    CurrentUserId(), model.Compendium, model.Content, model.CategoryId, model.KeyWord,
                    model.DocCode, model.InOutCode, model.UrgentId, model.CategoryBusinessId,
                    model.StorePrivateId, model.CurrentUserId, model.FromDate,
                    model.ToDate, model.InOutPlaceId, model.OrganizationCreate,
                    model.DocFieldId, model.UserSuccessId, model.UserCreateId, model.Page, model.PageSize);

            var result = new List<SearchViewModel>();
            foreach (var item in docs.Items)
            {
                result.Add(new SearchViewModel
                {
                    Address = item.ExtendInfo.GetType().GetProperty("Address").GetValue(item.ExtendInfo, null),
                    CategoryName = item.ExtendInfo.GetType().GetProperty("CategoryName").GetValue(item.ExtendInfo, null),
                    CitizenName = item.ExtendInfo.GetType().GetProperty("CitizenName").GetValue(item.ExtendInfo, null),
                    Compendium = item.DocumentCompendium,
                    DateAppointed = item.ExtendInfo.GetType().GetProperty("DateAppointed").GetValue(item.ExtendInfo, null),
                    DateArrived = item.ExtendInfo.GetType().GetProperty("DateArrived").GetValue(item.ExtendInfo, null),
                    DateCreated = item.ExtendInfo.GetType().GetProperty("DateCreated").GetValue(item.ExtendInfo, null),
                    DocCode = item.ExtendInfo.GetType().GetProperty("DocCode").GetValue(item.ExtendInfo, null),
                    DocumentCopyId = item.DocumentCopyId,
                    DocumentId = item.DocumentId,
                    InOutCode = item.ExtendInfo.GetType().GetProperty("InOutCode").GetValue(item.ExtendInfo, null),
                    LastUserComment = item.ExtendInfo.GetType().GetProperty("LastUserComment").GetValue(item.ExtendInfo, null),
                    UserSuccess = item.ExtendInfo.GetType().GetProperty("UserSuccess").GetValue(item.ExtendInfo, null),
                    DateReceived = item.ExtendInfo.GetType().GetProperty("DateReceived").GetValue(item.ExtendInfo, null)
                });
            }
            //HopCv:160115
            //trich yeu van ban phai chua gia tri khi truyen vafo va sắp xếp theo ngày tạo văn bản
            //result = result.Where(
            //    r => r.DocumentConpendium.Contains(model.Compendium)
            //        )
            //    .OrderByDescending(d => d.DateCreated)
            //    .Take(_searchSettings.NumberSelected);

            return Json(result);
        }

        #endregion

        #region Ý kiến thường dùng

        /// <summary>
        /// Trả về danh sách các comment gần nhất.
        /// </summary>
        /// <returns></returns>
        public JsonResult GetCommonComments()
        {
            var result = _commentService.GetCommons();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Lấy danh sách các mẫu template ý kiến mà người dùng đã soạn mẫu trước
        /// </summary>
        /// <param name="id"></param>
        /// <param name="commentText"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateTemplateComments(int id, string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return Json(new { success = false, message = "Nội dung không được trống. Vui lòng nhập lại!" });
            }

            var userId = CurrentUserId();
            var commonComment = _commonCommentService.Get(id, userId);
            if (commonComment == null)
            {
                return Json(new { success = false, message = "Mẫu ý kiến không tồn tại.Vui long xem lại!" });
            }

            commonComment.Content = GlobalObject.escape(content);
            _commonCommentService.Update(commonComment);

            return Json(new { success = true, message = "Cập nhật thành công!" });
        }

        /// <summary>
        /// Lấy danh sách các mẫu template ý kiến mà người dùng đã soạn mẫu trước
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteTemplateComments(int id)
        {
            if (id > 0)
            {
                var userId = CurrentUserId();
                var commonComment = _commonCommentService.Get(id, userId);
                if (commonComment != null)
                {
                    _commonCommentService.Delete(commonComment);
                    return Json(new { success = true, message = "Xóa thành công!" });
                }
            }

            return Json(new { success = false, message = "Xóa thất bại!" });
        }

        #endregion

        #region Lấy lại văn bản

        /// <author>
        /// CuongNT@bkav.com - 020813: Sửa các lấy userId với văn bản đang xử lý thì cần check ủy quyền xử lý. Văn bản khác thì lấy userId đang đăng nhập.
        /// </author>
        /// <summary>
        /// Tra ve danh sach ContextItem casc huong lay lai van ban
        /// </summary>
        /// <param name="documentCopyId"></param>
        /// <returns></returns>
        public JsonResult GetContextItemForUndoTransfering(int documentCopyId)
        {
            var userId = CurrentUserId();
            var results = new List<JsLaylaivanbanItem>();
            var documentCopy = _documentCopyService.GetFromCache(documentCopyId, userId);

            // Nếu ở mục theo dõi
            var isVuaBangiao = CheckForVuaBanGiao(documentCopy, userId);
            if (isVuaBangiao && !documentCopy.IsViewed(documentCopy.UserCurrentId))
            {
                results.Add(new JsLaylaivanbanItem
                {
                    Name = "Lấy lại báo cáo bàn giao",
                });

                return JsonNet(results, JsonRequestBehavior.AllowGet);
            }

            return JsonNet(results, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///   Hàm xử lý chức năng lấy lại văn bản/hồ sơ
        /// </summary>
        /// <param name="?"> </param>
        /// <param name="documentCopyId"> </param>
        /// <param name="dateCreated"> </param>
        /// <returns> </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult UndoTransfering(int documentCopyId)
        {
            try
            {
                var documentCopy = _documentCopyService.Get(documentCopyId);
                var document = documentCopy.Document;

                HistoryPath HistoryForUndo;
                if (documentCopy.Histories.HistoryPath.Count == 1 && (document.Original == 2 || document.IsConverted))
                {
                    var ListHistoryForUndoASC = documentCopy.Histories.HistoryPath.OrderBy(x => x.NodeReceiveId).ToList();

                    //HistoryForUndo = documentCopy.Histories.HistoryPath[0];
                    HistoryForUndo = ListHistoryForUndoASC[0];
                }
                else
                {
                    var ListHistoryForUndoASC = documentCopy.Histories.HistoryPath.OrderBy(x => x.NodeReceiveId).ToList();

                    //HistoryForUndo = documentCopy.Histories.HistoryPath[documentCopy.Histories.HistoryPath.Count - 2];
                    HistoryForUndo = ListHistoryForUndoASC[ListHistoryForUndoASC.Count - 2];
                }

                using (var trans = new TransactionScope())
                {
                    DeleteNotViewedDocumentCopies(HistoryForUndo);

                    _documentCopyService.UpdateForUndoTransfering(documentCopy);
                    trans.Complete();
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
                return Json(new { error = "Lấy lại văn bản/hồ sơ bị lỗi." }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = "Lấy lại văn bản/hồ sơ thành công." }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Xóa các hướng chuyển chưa đọc văn bản khi lấy lại
        /// </summary>
        /// <param name="list"></param>
        private void DeleteNotViewedDocumentCopies(HistoryPath historyPaths)
        {
            if (historyPaths.UserReceives == null || historyPaths.UserReceives.Count() == 0)
            {
                throw new Exception("Hướng chuyển không đúng");
            }

            var deleteIds = historyPaths.UserReceives.Where(u => !u.IsXlc).Select(u => u.DocumentCopyId).ToList();
            if (deleteIds.Count() == 0)
            {
                return;
            }

            var documentCopies = _documentCopyService.Gets(deleteIds);
            foreach (var documentCopy in documentCopies)
            {
                if (documentCopy.IsViewed(documentCopy.UserCurrentId))
                {
                    continue;
                }

                _documentCopyService.Delete(documentCopy);
            }
        }

        public bool CheckForVuaBanGiao(DocumentCached documentCopy, int userId)
        {
            if (documentCopy == null)
            {
                throw new ArgumentNullException("documentCopy");
            }

            if (documentCopy.UserSendId.HasValue)
            {
                var result = documentCopy.UserSendId == userId && documentCopy.UserCurrentId != userId;

                if (!result && !string.IsNullOrEmpty(documentCopy.UserGiamSat) && documentCopy.DocTypeId.HasValue)
                {
                    var authorizeUsers = _authorizeService.GetAuthorizeUsers(userId, documentCopy.DocTypeId.Value);
                    var docAuthorizedUsers = documentCopy.UserUyQuyen();
                    result = authorizeUsers.Any(a => a == documentCopy.UserSendId) && docAuthorizedUsers.Contains(userId)
                                    && !docAuthorizedUsers.Contains(documentCopy.UserCurrentId);
                }

                return result;
            }

            var historyTransfer = documentCopy.Histories.HistoryPath.Last();
            return historyTransfer.UserSendId == userId && historyTransfer.UserReceiveId != userId && documentCopy.UserCurrentId != userId;
        }

        #endregion

        #region Context menu

        /// <summary>
        ///   Trả về quyền trên danh sách văn bản được chọn khi view bằng context
        /// </summary>
        /// <param name="documentCopyIds"> </param>
        /// <returns> </returns>
        public JsonResult GetDocumentPermission(List<int> documentCopyIds)
        {
            var result = new List<int>();
            try
            {
                if (!documentCopyIds.Any())
                {
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

                // Nếu là check quyền trên 1 hồ sơ
                if (documentCopyIds.Count == 1)
                {
                    // Xử lý các trường hợp contextmenu không theo lô
                    var documentCopyId = documentCopyIds[0];

                    int userSendId;
                    var documentCopy = _documentCopyService.Get(documentCopyId);
                    if (!_documentPermissionHelper.CheckForUyQuyenXuLy(documentCopy, CurrentUserId(), out userSendId))
                    {
                        userSendId = CurrentUserId();
                    }

                    var permission = _documentPermissionHelper.CheckForContextMenu(documentCopy.Document, documentCopy, userSendId);

                    // Nếu là 1 hồ sơ --> Check thêm quyền lấy lại văn bản
                    permission = permission | DocumentPermissions.LayLaiVanBan;

                    result.Add((int)permission);

                    return Json(result, JsonRequestBehavior.AllowGet);
                }

                var documentCopies = _documentCopyService.Gets(documentCopyIds, true);
                var documentPermission = Int32.MaxValue;
                foreach (var documentCopy in documentCopies)
                {
                    // CuongNT@bkav.com - 210613: Ủy quyền xử lý
                    int userSendId;
                    if (!_documentPermissionHelper.CheckForUyQuyenXuLy(documentCopy, CurrentUserId(), out userSendId))
                    {
                        userSendId = CurrentUserId();
                    }
                    // TODO: Sua lai cho nay, chi check lai cac quyen ma vong lap truoc co. Neu khong co quyen nao thoa man thi return luon.
                    documentPermission &= (int)_documentPermissionHelper.CheckForContextMenuManyDocument(documentCopy.Document, documentCopy, userSendId);

                    // TODO: Sua noi dung return lai la chi return int dai dien cho quyen dang xet, khong return list object the nay
                    // result.Add(documentPermission);
                }
                result.Add(documentPermission);
            }
            catch (Exception)
            {
                return Json(new { error = "Lấy context menu của văn bản/hồ sơ bị lỗi." }, JsonRequestBehavior.AllowGet);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "DocumentSaveDocumentToStorePrivate")]
        [ValidateAntiForgeryToken]
        public JsonResult SaveDocumentToStorePrivate(int storePrivateId, int documentCopyId)
        {
            #region Kiểm tra đầu vào

            var storePrivate = _storePrivateService.CheckPermissionStorePrivate(storePrivateId, CurrentUserId());
            if (storePrivate == null)
            {
                return Json(new { error = true, message = "Bạn không có quyền truy cập vào hồ sơ cá nhân này" });
            }
            if (storePrivate.Status != (byte)StorePrivateStatus.IsActive)
            {
                return Json(new { error = true, message = "Hồ sơ này đã bị xóa hoặc bị đóng" });
            }
            var documentId = _documentCopyService.GetAs(d => d.DocumentId, documentCopyId);
            if (documentId == new Guid())
            {
                return Json(new { error = true, message = "Không tìm thấy văn bản/ hồ sơ" });
            }

            #endregion

            #region Check quyền

            int userSendId;
            if (!_documentPermissionHelper.CheckForUyQuyenVaXuLyVanBan(documentCopyId, CurrentUserId(), out userSendId))
            {
                if (!_documentPermissionHelper.CheckForQuyenXem(documentCopyId, CurrentUserId()))
                {
                    LogException("Không có quyền xem van ban.");
                    return Json(new { error = "Không có quyền xem van ban." });
                }
            }

            #endregion

            _storePrivateService.AddDocumentToStore(storePrivate, documentCopyId, documentId);
            return Json(new { success = true });
        }

        public JsonResult ReOpenDocument(string docIds)
        {
            try
            {
                var docCopies = _documentCopyService.Gets(Json2.ParseAs<List<int>>(docIds), true);
                if (!docCopies.Any())
                {
                    return Json(new { error = true, message = _resourceService.GetResource("Document.NotExist") }, JsonRequestBehavior.AllowGet);
                }
                var user = _userService.CurrentUser;
                var dateCreated = DateTime.Now;
                foreach (var doc in docCopies)
                {
                    doc.DateFinished = null;
                    doc.Status = (int)DocumentStatus.DangXuLy;
                    doc.Document.Status = (int)DocumentStatus.DangXuLy;
                    doc.NodeCurrentPermission = _workflowHelper.GetNodePermission(doc.WorkflowId, doc.NodeCurrentId.Value);
                    var comment = string.Format(_resourceService.GetResource("Document.Reopen"), user.FullName, dateCreated.ToString("dd/MM/yyyy HH:mm:ss"));
                    _commentService.SendCommentCommon(doc, user.UserId, dateCreated, comment, CommentType.Reopen);
                    _documentCopyService.Update(doc);
                }
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { error = true, message = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Biểu mẫu động

        /// <summary>
        /// Trả về biểu mẫu động
        /// </summary>
        /// <param name="contentId">DocumentContentId nếu là mở văn bản, FormId nếu là tạo mới văn bản</param>
        /// <param name="isCreate">Trạng thái xác định lấy nội dung cho văn bản tạo mới hay chỉnh sửa</param>
        /// <param name="doctypeId">Loại hồ sơ của văn bản</param>
        /// <returns> </returns>
        public string GetFormContent(string contentId, bool isCreate, Guid doctypeId)
        {
            var result = string.Empty;
            if (!isCreate)
            {
                int id;
                if (int.TryParse(contentId, out id))
                {
                    var form = _documentContentService.Get(id);
                    if (form == null)
                    {
                        throw new ApplicationException("Bieu mau yeu cau khong ton tai!");
                    }
                    var content = form.Content;

                    switch (form.FormTypeIdInEnum)
                    {
                        case FormType.DynamicForm:
                            {
                                if (!string.IsNullOrEmpty(content))
                                {
                                    var token = JToken.Parse(content);
                                    if (token is JArray)
                                    {
                                        var dynamicResult = new List<string>();
                                        foreach (var json in token.Children<JObject>())
                                        {
                                            JsDocument jsDocument;
                                            if (DynamicFormHelper.TryParse(json.ToString(), out jsDocument))
                                            {
                                                var dynamicForm = "{}";
                                                var formjs = _formHelper.ParseFormModel(jsDocument);

                                                dynamicForm = formjs.StringifyJs();
                                                dynamicResult.Add(dynamicForm);
                                            }
                                        }
                                        result = "[" + string.Join(",", dynamicResult) + "]";
                                    }
                                    else
                                    {
                                        result = "{}";
                                        JsDocument jsDocument;
                                        if (DynamicFormHelper.TryParse(content, out jsDocument))
                                        {
                                            var formjs = _formHelper.ParseFormModel(jsDocument);
                                            result = formjs.StringifyJs();
                                        }
                                    }
                                }
                                else
                                {
                                    result = "{}";
                                }
                            }
                            break;

                        case FormType.HtmlForm:
                            result = GlobalObject.unescape(content);
                            break;
                    }
                }
            }
            else
            {
                Guid formid;
                if (Guid.TryParse(contentId, out formid))
                {
                    var form = _formService.Get(formid);
                    if (form == null)
                    {
                        throw new ApplicationException("Biểu mẫu yêu cầu không tồn tại!");
                    }
                    switch (form.FormTypeIdInEnum)
                    {
                        case FormType.DynamicForm:
                            result = _formHelper.ParseFormModel(form).StringifyJs();
                            break;

                        case FormType.HtmlForm:
                            var doctype = _docTypeService.GetFromCache(doctypeId);
                            if (doctype == null)
                            {
                                throw new Exception("Loại văn bản không tồn tại!");
                            }
                            result = ParseTemplate(form.Template ?? string.Empty, doctype.DocTypeId, doctype.DocTypeName, doctype.DocFieldName);
                            break;
                    }
                }
            }
            return result;
        }

        // TODO: Ham nay can day vao trong noi bo thu vien xu ly template
        private string ParseTemplate(string content, Guid doctypeId, string docTypeName, string docfieldName)
        {
            if (content.Contains("@doccode"))
            {
                var doccode = _docTypeService.GetAutoDocCode(doctypeId);
                content = content.Replace("@doccode", doccode);
            }

            content = content.Replace("@account", User.GetUserName());

            content = content.Replace("@username", User.GetFullName());
            if (content.Contains("@department"))
            {
                var user = _userService.GetFromCache(CurrentUserId());
                if (user != null)
                {
                    var userDepartmentJobTitless = user.UserDepartmentJobTitless;
                    if (userDepartmentJobTitless.Any())
                    {
                        var departmentString = "";
                        foreach (var item in userDepartmentJobTitless)
                        {
                            var department = item.Department;
                            if (department != null && department.IsActivated)
                            {
                                if (!departmentString.Contains(department.DepartmentPath))
                                {
                                    departmentString += department.DepartmentPath + "; ";
                                }
                            }
                        }

                        content = content.Replace("@department",
                            departmentString.Replace(_departmentService.GetRootDepartmentPath() + @"\", ""));
                    }

                }

            }

            content = content.Replace("@docfield", docfieldName);
            content = content.Replace("@doctype", docTypeName);
            content = content.Replace("@datereceive", DateTime.Now.ToShortDateString());
            return content;
        }

        #endregion

        #region Gia hạn xử lý

        [HttpPost]
        public JsonResult Renewals(int documentCopyId, string comment, DateTime newDate, int renewalsType)
        {
            var documentCopy = _documentCopyService.Get(documentCopyId);
            if (documentCopy == null)
            {
                return Json(new { error = true }, JsonRequestBehavior.AllowGet);
            }

            // Kiem tra quyen xin gia han
            var userSend = _userService.CurrentUser;

            // Văn bản gia hạn không có ủy quyền
            int userId;
            if (!_documentPermissionHelper.CheckForUyQuyenVaXuLyVanBan(documentCopy, userSend.UserId, out userId) ||
               _documentPermissionHelper.Check(documentCopy, userId, DocumentPermissions.XinGiaHanXuLy) != DocumentPermissions.XinGiaHanXuLy)
            {
                return Json(new { error = true, message = "Không có quyền gia hạn xử lý" }, JsonRequestBehavior.AllowGet);
            }

            var document = documentCopy.Document;
            if (document.DateCreated > newDate)
            {
                return Json(new { error = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                var dateCreate = DateTime.Now;

                using (var trans = new TransactionScope())
                {
                    DateTime? oldDateAppointed;
                    if (renewalsType == (int)RenewalsType.DocumentRenewals)
                    {
                        oldDateAppointed = documentCopy.Document.DateAppointed;
                        document.DateAppointed = newDate;
                        documentCopy.DateOverdue = newDate;
                        document.ExpireProcess = _workTimeHelper.GetWorkdays(document.DateCreated, newDate);
                    }
                    else
                    {
                        oldDateAppointed = documentCopy.DateOverdue;
                        documentCopy.DateOverdue = newDate;
                    }

                    var renewalsDays = !oldDateAppointed.HasValue
                                            ? 0
                                            : _workTimeHelper.GetWorkdays(oldDateAppointed.Value < newDate ? oldDateAppointed.Value : newDate
                                            , oldDateAppointed.Value < newDate ? newDate : oldDateAppointed.Value);

                    _extensionTimeService.Create(new Renewals
                    {
                        UserRequestedId = userId,
                        UserApprovedId = userId,
                        DocumentCopyId = documentCopy.DocumentCopyId,
                        OldDateAppointed = oldDateAppointed,
                        NewDateAppointed = newDate,
                        RenewalsDays = renewalsDays,
                        ApprovedRenewalsDays = 0,
                        IsApproved = true,
                        RenewalsType = renewalsType,
                        Reason = comment
                    });

                    comment = string.Format("Thay đổi hạn xử lý từ {0} sang {1}: {2}", oldDateAppointed.HasValue ? oldDateAppointed.Value.ToString("hh:mm dd/MM/yyyy") : "Không có",
                                        newDate.ToString("hh:mm dd/MM/yyyy"),
                                        comment);

                    _documentService.Update(document);

                    var uyQuyenComment = "";
                    if (userId != userSend.UserId)
                    {
                        var uQUser = _userService.GetFromCache(userId);
                        uyQuyenComment = "Xử lý ủy quyền: " + uQUser.FullName;
                    }
                    _commentService.SendCommentCommon(documentCopy, userSend.UserId, DateTime.Now, comment, CommentType.Common, uyQuyenComment);

                    trans.Complete();
                }
            }
            catch (Exception)
            {
                return Json(new { error = true }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = newDate }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Loại bỏ văn bản

        /// <summary>
        ///   Loại bỏ văn bản khỏi hồ sơ
        /// </summary>
        /// <param name="documentCopyIds"> </param>
        /// <returns> </returns>
        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "DocumentRemoveDocument")]
        [ValidateAntiForgeryToken]
        public JsonResult RemoveDocument(List<int> documentCopyIds)
        {
            if (documentCopyIds == null || !documentCopyIds.Any())
            {
                return Json(new { error = "Chưa chọn văn bản loại bỏ." });
            }
            var userId = CurrentUserId();
            foreach (var documentCopyId in documentCopyIds)
            {
                var documentCopy = _documentCopyService.Get(documentCopyId);
                if (_documentPermissionHelper.Check(documentCopy, userId, DocumentPermissions.HuyVanBan) != DocumentPermissions.HuyVanBan)
                {
                    return Json(new { error = "Không có quyền hủy văn bản." });
                }
            }

            try
            {
                using (var trans = new TransactionScope())
                {
                    documentCopyIds = documentCopyIds.OrderByDescending(c => c).ToList();
                    var dateFinish = DateTime.Now;
                    foreach (var documentCopyId in documentCopyIds)
                    {
                        var documentCopy = _documentCopyService.Get(documentCopyId);
                        RemoveDocument(documentCopy, dateFinish, userId);

                        //hopcv: 140714
                        _userActivityLogService.SetViewed(documentCopyId, userId);
                    }
                    trans.Complete();
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
                return Json(new { error = "Loại bỏ văn bản bị lỗi." });
            }

            return Json(new { success = "Loại bỏ văn bản thành công." });
        }

        /// <summary>
        ///   Hàm cập nhật dữ liệu khi loại bỏ văn bản khỏi hồ sơ
        /// </summary>
        /// <param name="documentCopy"> </param>
        /// <param name="dateFinish"> </param>
        private void RemoveDocument(DocumentCopy documentCopy, DateTime dateFinish, int userSendId)
        {
            _documentCopyService.Remove(documentCopy, userSendId, dateFinish);
            CreateActivityLog(ActivityLogType.HuyVanBan, string.Format("{0} hủy văn bản: {1}", User.GetUserNameWithDomain(), documentCopy.Document.Compendium));
        }

        #endregion

        #region Gửi ý kiến xử lý

        /// <summary>
        ///   Chức năng gửi ý kiến xử lý(trên contextmenu - toolbar)
        /// </summary>
        /// <param name="documentCopyId"> </param>
        /// <param name="comment"> </param>
        /// <param name="isToolbar"> </param>
        /// <returns> </returns>
        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "DocumentSendComment")]
        [ValidateAntiForgeryToken]
        public JsonResult SendComment(int documentCopyId, string comment)
        {
            var documentCopy = _documentCopyService.Get(documentCopyId);
            try
            {
                // Không xét ủy quyền khi gửi ý kiến
                var userSendId = CurrentUserId();
                if (!_documentPermissionHelper.CheckForQuyenXem(documentCopy, userSendId))
                {
                    return Json(new { error = "Không có quyền gửi ý kiến xử lý." }, JsonRequestBehavior.AllowGet);
                }

                //Check xem có phải người ủy quyền xử lý hay không?
                string contentAuthorize = userSendId != CurrentUserId() ?
                                         string.Format("Xử lý ủy quyền:{0} ({1})", User.GetFullName(), User.GetUserName())
                                         : string.Empty;

                var dateCreated = DateTime.Now;
                var commentResult = _commentService.SendComment(documentCopyId, userSendId, comment, dateCreated, contentAuthorize);
                _documentCopyService.ClearCache(documentCopy.DocumentCopyId);

                var result = new List<CommentModel> { commentResult.ToModel() };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { error = "Gửi ý kiến bị lỗi." }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Hồ sơ 1 cửa

        /// <summary>
        /// Tìm kiếm người dân trong db nộp hồ sơ 1 cửa
        /// </summary>
        /// <param name="account"></param>
        /// <param name="name"></param>
        /// <param name="identityCard"></param>
        /// <param name="phone"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public JsonResult FilterCitizen(string name = null, string identityCard = null, string phone = null, string email = null, string address = null)
        {
            var citizens = _citizenService.Filter(name, identityCard, phone, email, address);

            return Json(citizens, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Filter autocomplete cho chức năng đính kèm ảnh theo lô
        /// </summary>
        /// <param name="code">inoutcode hoặc doccode</param>
        /// <param name="compendium">trích yếu</param>
        /// <returns></returns>
        public JsonResult FilterInOutCode(string code, string compendium)
        {
            if (string.IsNullOrWhiteSpace(code) && string.IsNullOrWhiteSpace(compendium))
            {
                return Json(new { error = true }, JsonRequestBehavior.AllowGet);
            }
            var currentUser = _userService.CurrentUser;

            var docs = _documentCopyService.Gets(d => (
                d.Status == (int)DocumentStatus.DangXuLy || d.Status == (int)DocumentStatus.DuThao)
                && d.UserCurrentId == currentUser.UserId && d.Document != null
                && ((d.Document.InOutCode.ToLower().Contains(code.ToLower()) || d.Document.DocCode.ToLower().Contains(code.ToLower()))
                && (string.IsNullOrEmpty(compendium) || d.Document.Compendium.ToLower().Contains(compendium.ToLower())))
                ).Select(d => new
                {
                    DocumentCopyId = d.DocumentCopyId,
                    DocCode = d.Document.DocCode,
                    InOutCode = d.Document.InOutCode,
                    Compendium = d.Document.Compendium
                });
            return Json(docs, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteDocPaper(int id)
        {
            try
            {
                _documentService.DeleteDocPaper(id);
            }
            catch (Exception ex)
            {
                LogException(ex);
                return Json(new { error = true });
            }

            return Json(new { success = true });
        }

        public JsonResult DeleteDocFee(int id)
        {
            try
            {
                _documentService.DeleteDocFee(id);
            }
            catch (Exception ex)
            {
                LogException(ex);
                return Json(new { error = true });
            }

            return Json(new { success = true });
        }

        public JsonResult DeleteDoctypePaper(Guid doctypeId, int paperId)
        {
            try
            {
                _paperService.DeleteDocTypePaper(doctypeId, paperId);
            }
            catch (Exception ex)
            {
                LogException(ex);
                return Json(new { error = true });
            }

            return Json(new { success = true });
        }

        public JsonResult DeleteDoctypeFee(Guid doctypeId, int feeId)
        {
            try
            {
                _feeService.DeleteDocTypeFee(doctypeId, feeId);
            }
            catch (Exception ex)
            {
                LogException(ex);
                return Json(new { error = true });
            }

            return Json(new { success = true });
        }

        #endregion

        #region Thu hồi văn bản

        [HttpPost]
        public JsonResult AcceptThuHoi(int documentCopyId, bool isAccept, string comment)
        {
            var docCopy = _documentCopyService.Get(documentCopyId);
            if (docCopy == null)
            {
                return Json(new { error = true }, JsonRequestBehavior.AllowGet);
            }

            var document = docCopy.Document;
            document.DateModified = DateTime.Now;
            if (isAccept)
            {
                // Kêt thúc tất cả văn bản liên quan thu hồi
                var docRelations = _documentService.GetDocRelations(r => r.DocumentCopyId == docCopy.DocumentCopyId && r.RelationType == (int)RelationTypes.LienQuanThuHoi);
                if (!docRelations.Any())
                {
                    return Json(new { error = true }, JsonRequestBehavior.AllowGet);
                }

                var commentFinish = "Đã thu hồi văn bản";
                var docCopyIds = docRelations.Select(d => d.DocumentCopyId).Distinct().ToList();
                var docCopyThuHois = _documentCopyService.Gets(docCopyIds, isIncludeDocument: true);
                foreach (var dc in docCopyThuHois)
                {
                    _documentCopyService.Finish(docCopy, DateTime.Now, CurrentUserId(), commentFinish);
                }

                // Cập nhật lại văn bản yêu cầu thu hồi
                document.LienThongStatus = LienThongStatus.DongYThuHoi.ToString();
                //document.Note = commentFinish;
                _documentCopyService.Finish(docCopy, DateTime.Now, CurrentUserId(), commentFinish);

                _documentCopyService.ClearCache(docCopyIds);
            }
            else
            {
                var commentFinish = String.Format("Từ chối thu hồi, lý do: {0}", comment);

                document.LienThongStatus = LienThongStatus.TuChoiThuHoi.ToString();
                //document.Note = commentFinish;
                _documentCopyService.Finish(docCopy, DateTime.Now, CurrentUserId(), commentFinish);
                _documentCopyService.ClearCache(docCopy.DocumentCopyId);
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Viewed

        /// <summary>
        /// Set trạng thái đã xem văn bản (dạng thông báo hay chưa)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="viewed"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SetViewed(int id, bool viewed)
        {
            #region Kiểm tra đầu vào

            //var documentCopy = _documentCopyService.Get(id);
            //if (documentCopy == null)
            //{
            //	return Json(new { error = "Văn bản bản sao không tồn tại!" }, JsonRequestBehavior.AllowGet);
            //}

            #endregion

            var userSendId = CurrentUserId();
            bool result = false;
            return Json(new { success = result });

            // TIenBV: tạm bỏ do đã set khi lấy thông tin văn bản
            //try
            //{
            //    _documentCopyService.SetViewed(documentCopy, userSendId, viewed);
            //    result = true;
            //}
            //catch (Exception ex)
            //{
            //    LogException(ex);
            //}

            //return Json(new { success = result });
        }

        #endregion

        #region Lấy mẫu gửi mail, sms

        [HttpGet]
        public JsonResult GetMailTemplates(int documentCopyId)
        {
            var documentCopy = _documentCopyService.Get(documentCopyId);
            if (documentCopy == null)
            {
                throw new Exception("documentCopy is not exist.");
            }

            try
            {
                var temps = _templateService.GetAvaiableTemps(documentCopy, CurrentUserId(), TemplateType.Email);
                var result = temps.Select(p => new { p.TemplateId, p.Name });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogException(ex);
                return Json(new { error = _resourceService.GetResource("Document.Template.GetMailTemplate.Error") },
                    JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult EditTemplate(int templateId, Guid documentId, bool isMail = true)
        {
            var document = _documentService.Get(documentId);
            if (document == null)
            {
                return Json(new
                {
                    error = _resourceService.GetResource("Document.Template.EditTemplates.Document.NotExist")
                }, JsonRequestBehavior.AllowGet);
            }

            var template = _templateService.Get(templateId);
            if (template == null)
            {
                return Json(new
                {
                    error = _resourceService.GetResource("Document.Template.EditTemplates.Template.NotExist")
                }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                var userSend = _userService.CurrentUser;
                var content = _templateHelper.ParseContentNew(template, userSend.UserId, document, null);
                if (isMail)
                {
                    if (template.TypeInEnum != TemplateType.Email)
                    {
                        return Json(new
                        {
                            error = _resourceService.GetResource("Document.Template.EditTemplates.ValidTemplateMail")
                        }, JsonRequestBehavior.AllowGet);
                    }

                    return Json(new
                    {
                        mail = document.Email,
                        content = content
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    if (template.TypeInEnum != TemplateType.Sms)
                    {
                        return Json(new
                        {
                            error = _resourceService.GetResource("Document.Template.EditTemplates.ValidTemplateSms")
                        }, JsonRequestBehavior.AllowGet);
                    }

                    return Json(new
                    {
                        phone = document.Phone,
                        content = content.StripHtml(),
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
                return Json(new
                {
                    error = isMail
                    ? _resourceService.GetResource("Document.Template.EditTemplates.MailError")
                    : _resourceService.GetResource("Document.Template.EditTemplates.SmsError")
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult GetSmsTemplates(int documentCopyId)
        {
            var documentCopy = _documentCopyService.Get(documentCopyId);
            if (documentCopy == null)
            {
                throw new Exception("documentCopy is not exist.");
            }

            try
            {
                var temps = _templateService.GetAvaiableTemps(documentCopy, CurrentUserId(), TemplateType.Sms);
                var result = temps.Select(p => new { p.TemplateId, p.Name });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogException(ex);
                return Json(new { error = _resourceService.GetResource("Document.Template.GetSmsTemplate.Error") },
                    JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult SendMailToPeople(string subject, string email, string content)
        {
            if (string.IsNullOrEmpty(subject))
            {
                return Json(new
                {
                    error = _resourceService.GetResource("Document.Template.SendMail.ValidEmail")
                });
            }

            if (!email.IsEmailAddress())
            {
                return Json(new
                {
                    error = _resourceService.GetResource("Document.Template.SendMail.ValidEmail")
                });
            }

            if (string.IsNullOrEmpty(content))
            {
                return Json(new
                {
                    error = _resourceService.GetResource("Document.Template.SendMail.ValidContent")
                });
            }

            try
            {
                if (!_emailSettings.IsActivated)
                {
                    return Json(new
                    {
                        error = string.Format(_resourceService.GetResource("Document.Email.NotActivated"))
                    });
                }

                var userSend = _userService.CurrentUser.ToUser();
                _mailHelper.CreateQueueMail(subject, new List<string>() { email }, Microsoft.JScript.GlobalObject.unescape(content), userSend);

                return Json(new
                {
                    success = string.Format(_resourceService.GetResource("Document.Template.SendMail.Success"), email)
                });
            }
            catch (Exception ex)
            {
                LogException(ex);
                return Json(new { error = _resourceService.GetResource("Document.Template.SendMail.Error") });
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult SendSmsToPeople(string phone, string content)
        {
            if (!phone.IsPhoneNumber())
            {
                return Json(new
                {
                    error = _resourceService.GetResource("Document.Template.SendSms.ValidPhone")
                });
            }

            if (string.IsNullOrEmpty(content))
            {
                return Json(new
                {
                    error = _resourceService.GetResource("Document.Template.SendMail.ValidEmail")
                });
            }

            try
            {
                if (!_smsSettings.IsActivated)
                {
                    return Json(new
                    {
                        error = _resourceService.GetResource("Document.Sms.NotActivated")
                    });
                }

                var currentUser = _userService.CurrentUser.ToUser();
                content = Microsoft.JScript.GlobalObject.unescape(content);
                _smsHelper.CreateQueueSms(phone, content, currentUser, documentCopyId: null, documentId: null, type: null);

                return Json(new
                {
                    success = string.Format(_resourceService.GetResource("Document.Template.SendSms.Success"), phone)
                });
            }
            catch (Exception ex)
            {
                LogException(ex);
                return Json(new
                {
                    error = _resourceService.GetResource("Document.Template.SendSms.Error")
                });
            }
        }

        #endregion

        #region Import From XML

        [HttpPost]
        public JsonResult GetXMLFile2(string filename, Guid doctypeId)
        {
            try
            {
                var filePath = Server.MapPath("~/FileXML/" + filename);
                if (!System.IO.File.Exists(filePath))
                {
                    return Json(new { success = false, message = "Không đọc được file xml" }, JsonRequestBehavior.AllowGet);
                }

                var doctype = _docTypeService.GetFromCache(doctypeId);
                if (doctype == null)
                {
                    return Json(new { success = false, message = "Loại văn bản không tồn tại." }, JsonRequestBehavior.AllowGet);
                }

                var userId = CurrentUserId();

                var workFlow = _workflowService.GetFromCache(doctype.WorkflowId);
                var startNodes = _workflowHelper.GetStartNodes(workFlow, userId);
                var nodeSend = startNodes.First();

                var doc = new XmlDocument();
                var documents = new List<Document>();
                doc.Load(filePath);

                foreach (XmlNode node in doc.SelectNodes("/egov/document"))
                {
                    var newDocument = ToDocument(node, doctype.DocTypeId, doctype.CategoryBusinessId);
                    if (newDocument == null)
                    {
                        continue;
                    }

                    var tempFiles = new Dictionary<string, IDictionary<string, string>>();
                    var attachmentXmls = GetNodeAttachments(node);
                    foreach (var attachment in attachmentXmls)
                    {
                        if (string.IsNullOrEmpty(attachment.Key))
                        {
                            continue;
                        }

                        var data = System.Convert.FromBase64String(attachment.Value);
                        var stream = new MemoryStream(data);
                        var tempPath = ResourceLocation.Default.FileUploadTemp;
                        var fileInfo = FileManager.Default.Create(stream, tempPath);
                        var tempDic = new Dictionary<string, string>();
                        tempDic.Add("name", attachment.Key);
                        tempFiles.Add(fileInfo.Name, tempDic);
                    }

                    var attachments = _attachmentService.AddAttachmentInDoc(tempFiles, userId, true);
                    newDocument.Attachments = attachments;

                    documents.Add(newDocument);
                }

                if (documents != null && documents.Any())
                {
                    foreach (Document document in documents)
                    {
                        CreateComingDocumentXML(document, nodeSend);
                    }
                }

                return Json(new { success = true, message = "Import thành công." }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false, message = "Không đọc được file xml" }, JsonRequestBehavior.AllowGet);
            }
        }

        private Document ToDocument(XmlNode node, Guid doctypeId, int categoryBusinessId)
        {
            var result = new Document();
            result.DocumentId = Guid.NewGuid();
            result.DocCode = GetNodeValue(node, "DocCode");
            result.InOutCode = GetNodeValue(node, "InOutCode");
            result.Compendium = GetNodeValue(node, "Compendium", string.Empty);
            result.UrgentId = (byte)GetUrgentLevel(GetNodeValue(node, "UrgentId", "Thường"));
            result.SecurityId = GetSecurityLevel(GetNodeValue(node, "UrgentId", "Thường"));
            result.TotalPage = int.Parse(GetNodeValue(node, "DocPage", "1"));
            result.DateAppointed = null;
            result.UserCreatedId = CurrentUserId();
            result.UserCreatedName = User.GetFullName();
            result.DateModified = DateTime.Now;
            result.DateCreated = DateTime.ParseExact(GetNodeValue(node, "DateCreated", DateTime.Now.ToString(DateFormat)), DateFormat, CultureInfo.InvariantCulture);
            result.DocTypeId = doctypeId;
            result.CategoryBusinessId = categoryBusinessId;

            if (result.CategoryBusinessId == (int)CategoryBusinessTypes.VbDen)
            {
                result.DateArrived = result.DateCreated;
                result.Organization = GetNodeValue(node, "Organization", "");
            }
            else
            {
                result.InOutPlace = GetNodeValue(node, "Organization", "");
                result.Organization = GetNodeValue(node, "InOutPlace", "");
            }

            DateTime datePublish;
            if (DateTime.TryParseExact(GetNodeValue(node, "DatePublished", ""), DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out datePublish))
            {
                result.DatePublished = datePublish;
                result.SuccessNote = GetNodeValue(node, "Approver", string.Empty);
            }
            else
            {
                result.DatePublished = null;
            }

            var categoryName = GetNodeValue(node, "Category", "Công văn");
            var category = EnsureCategory(categoryName);
            if (category != null)
            {
                result.CategoryId = category.CategoryId;
            }

            return result;
        }

        private void CreateComingDocumentXML(Document document, Core.Workflow.Node currentNode)
        {
            int userCreateId = CurrentUserId();
            _documentService.Create(document);

            _documentCopyService.Create(document.DocumentId, document.DocTypeId.Value, currentNode, userCreateId, currentNode,
                userCreateId, null, document.DateCreated, DocumentCopyTypes.XuLyChinh, DocumentStatus.DangXuLy, null, null);
        }

        private string GetNodeValue(XmlNode node, string columnName, string defaultValue = null)
        {
            string result = defaultValue;
            if (node == null)
            {
                return result;
            }

            var nodeColumn = node[columnName];
            if (nodeColumn == null)
            {
                return result;
            }

            return nodeColumn.InnerText;
        }

        private Dictionary<string, string> GetNodeAttachments(XmlNode node)
        {
            var result = new Dictionary<string, string>();

            var attachmentsNode = node["Attachments"];
            if (attachmentsNode == null)
            {
                return result;
            }

            var attachmentNodes = attachmentsNode.SelectNodes("/attachment");
            if (attachmentsNode == null)
            {
                return result;
            }

            foreach (XmlNode attach in attachmentsNode)
            {
                result.Add(GetNodeValue(attach, "Name", ""), GetNodeValue(attach, "Value", ""));
            }

            return result;
        }

        private Category EnsureCategory(string name)
        {
            var category = _categoryService.Get(name);
            if (category != null)
            {
                return category;
            }

            category = new Category()
            {
                CategoryName = name,
                CategoryBusinessId = (int)CategoryBusinessTypes.VbDen,
                CodeIds = new List<int>()
            };

            _categoryService.Create(category);
            return category;
        }

        private int? GetSecurityLevel(string name)
        {
            switch (name.ToLower())
            {
                case "thường":
                    return (int)SecurityType.Thuong;
                case "mật":
                    return (int)SecurityType.Mat;
                case "tối mật":
                    return (int)SecurityType.ToiMat;
                case "tuyệt mật":
                    return (int)SecurityType.TuyetMat;
                default:
                    return null;
            }
        }

        private int GetUrgentLevel(string name)
        {
            switch (name.ToLower())
            {
                case "khẩn":
                    return (int)Urgent.Khan;
                case "hỏa tốc":
                    return (int)Urgent.HoaToc;
                default:
                    return (int)Urgent.Thuong;
            }
        }

        #endregion

        #region get dữ liệu chỉ tiêu
        //hai duong them string organizeKey va thay loadCompilationData => loadCompilationData_
        [HttpGet]
        public JsonResult GetCompilationData(Guid docTypeId, string timeKey, string organizeKey)
        {
            var forms = _doctypeFormService.GetForms(d => new
            {
                d.Form.FormId,
                d.Form.FormName,
                d.Form.FormTypeId,
                d.IsPrimary,
                d.Form.EmbryonicPath,
                d.Form.EmbryonicLocationName,
                d.Form.Template,
                d.Form.FormCode,
                d.Form.CompilationId,
                d.Form.ConfigFunction,
                d.Form.ChildCompilationId,
                d.Form.DefineFieldJson,
                d.Form.DefineConfigJson,
                d.Form.DefineValueJson,
                d.Form.FormHeader,
                d.Form.FormFooter,
                d.Form.ExplicitTemplate,
                d.Form.FormCodeCompilation,
                d.Form.TableName,
                d.Form.FormCategoryId,
                d.DocType.ActionLevel,
                d.Form.MappingMaDinhDanhCP
            }, docTypeId);

            DataTable dt = new DataTable();

            if (forms.Count() > 0)
            {
                dt = loadCompilationData_(forms.ElementAt(0), timeKey, organizeKey);
            }

            return Json(new
            {
                success = true,
                data = JsonConvert.SerializeObject(dt, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore })
            }, JsonRequestBehavior.AllowGet);

        }
        #endregion

        public JsonResult GetContentDocument(Guid documentId)
        {
            var content = _documentContentService.GetsByDocumentId(documentId).FirstOrDefault()?.Content;
            return Json(new { Success = true, Content = content }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDataTemplateKeys(string templateKeyCode, Guid formId, int timeKey)
        {

            var form = _formService.Get(formId);
            var templateKeyCodeDecode = HttpUtility.HtmlDecode(templateKeyCode);
            if (string.IsNullOrWhiteSpace(templateKeyCodeDecode)) return Json(new { Success = false, data = (IEnumerable<dynamic>)null }, JsonRequestBehavior.AllowGet);
            TemplateKey templateKey = null;

            if (templateKeyCodeDecode.IndexOf("@@", StringComparison.Ordinal) >= 0)
            {
                switch (templateKeyCodeDecode)
                {
                    case "@@PHONGBANHIENTAI@@":
                    case "@@PHÒNG BAN HIỆN TẠI@@":
                        {
                            var department = _departmentService.GetCacheAllUserDepartmentJobTitlesPosition().Where(p => p.UserId == User.GetUserId()).OrderBy(p => p.IsPrimary).FirstOrDefault();
                            return Json(new { Success = true, Type = "default", NewValue = department?.DepartmentName.ToUpper() }, JsonRequestBehavior.AllowGet);
                        }
                    case "@@Phongbanhientai@@":
                    case "@@Phòng ban hiện tại@@":
                        {
                            var department = _departmentService.GetCacheAllUserDepartmentJobTitlesPosition().Where(p => p.UserId == User.GetUserId()).OrderBy(p => p.IsPrimary).FirstOrDefault();
                            return Json(new { Success = true, Type = "default", NewValue = department?.DepartmentName }, JsonRequestBehavior.AllowGet);
                        }
                    case "@@dd@@":
                        {
                            var timeNow = DateTime.Now.ToString("dd");
                            return Json(new { Success = true, Type = "default", NewValue = timeNow }, JsonRequestBehavior.AllowGet);
                        }
                    case "@@MM@@":
                        {
                            var timeNow = DateTime.Now.ToString("MM");
                            return Json(new { Success = true, Type = "default", NewValue = timeNow }, JsonRequestBehavior.AllowGet);
                        }
                    case "@@yyyy@@":
                        {
                            var timeNow = DateTime.Now.ToString("yyyy");
                            return Json(new { Success = true, Type = "default", NewValue = timeNow }, JsonRequestBehavior.AllowGet);
                        }
                    case "@@dd-MM-yyyy@@":
                        {
                            var timeNow = DateTime.Now.ToString("dd-MM-yyyy");
                            return Json(new { Success = true, Type = "default", NewValue = timeNow }, JsonRequestBehavior.AllowGet);
                        }
                    case "@@dd-MM-yyyy hh:mm@@":
                        {
                            var timeNow = DateTime.Now.ToString("dd-MM-yyyy hh:mm");
                            return Json(new { Success = true, Type = "default", NewValue = timeNow }, JsonRequestBehavior.AllowGet);
                        }
                    default:
                        templateKeyCodeDecode = templateKeyCodeDecode.Replace("@@", "").Replace("@@", "");
                        templateKey = _templateKeyService.Gets(p => p.Code.Equals(templateKeyCodeDecode) || p.Name.Equals(templateKeyCodeDecode)).FirstOrDefault();
                        break;
                }
            }
            else if (templateKeyCodeDecode.IndexOf("##", StringComparison.Ordinal) >= 0)
            {
                templateKeyCodeDecode = templateKeyCodeDecode.Replace("##", "").Replace("##", "");
                templateKey = _templateKeyService.Gets(p => p.Code.Equals(templateKeyCodeDecode) || p.Name.Equals(templateKeyCodeDecode)).FirstOrDefault();
            }
            if (templateKey == null) return Json(new { Success = false, data = (IEnumerable<dynamic>)null }, JsonRequestBehavior.AllowGet);
            templateKey.HtmlTemplate = HttpUtility.HtmlDecode(templateKey.HtmlTemplate);
            var htmlEach = "<tr class=\"eachRow\" style=\"background: lightblue\"";
            if (templateKey.HtmlTemplate.Contains(htmlEach))
            {
                templateKey.HtmlTemplate = templateKey.HtmlTemplate.Replace(htmlEach, "{{each data}} <tr");
                templateKey.HtmlTemplate = templateKey.HtmlTemplate.Replace("</tr>\r\n\t</tbody>", "</tr> {{/each}}\r\n\t</tbody>");
            }
            var doc = new HtmlDocument();
            doc.LoadHtml(templateKey.HtmlTemplate);
            var nodes = doc.DocumentNode.Descendants();
            var temp = templateKey.HtmlTemplate;
            GetChildNodes(nodes, ref temp);
            templateKey.HtmlTemplate = temp;
            const string pattern = @"@([^=<> ')(\s]+)(|(?=\s)|$)";
            // Create a Regex  
            var regex = new Regex(pattern);
            var match = regex.Match(templateKey.Sql);

            var currentDepartment = _departmentService.GetCacheAllUserDepartmentJobTitlesPosition().Where(p => p.UserId == User.GetUserId()).OrderBy(p => p.IsPrimary).FirstOrDefault();
            var organizeKey = currentDepartment?.Emails;

            var paramTemplateKeys = new List<object>
            {
                new SqlParameter("@timekey", timeKey), new SqlParameter("@organize", organizeKey)
            };

            var arrPara = paramTemplateKeys.ToArray();
            IEnumerable<dynamic> list = _templateKeyService.GetListByQuery(templateKey, arrPara).ToList();
            if (templateKey.Type == System.Convert.ToInt32(TemplateKeysType.Display))
                return Json(new { Success = true, result = list, ExpiciteTemplate = form.ExplicitTemplate, Type = 8, HtmlTemplate = templateKey.HtmlTemplate }, JsonRequestBehavior.AllowGet);
            if (templateKey.Type != System.Convert.ToInt32(TemplateKeysType.Query))
                return Json(new { Success = true, ExpiciteTemplate = form.ExplicitTemplate },
                    JsonRequestBehavior.AllowGet);
            if (!list.Any())
                return Json(new { Success = true, ExpiciteTemplate = form.ExplicitTemplate },
                    JsonRequestBehavior.AllowGet);

            var keyValuePair = new Dictionary<string, object>(list.ToArray()[0]).FirstOrDefault();


            if (keyValuePair.Key == null)
                return Json(new { Success = false, ExpiciteTemplate = form.ExplicitTemplate }, JsonRequestBehavior.AllowGet);
            var obj = new TemplateKeyResult
            {
                Success = true,
                ExpiciteTemplate = form.ExplicitTemplate,
                HtmlTemplate = templateKey.HtmlTemplate,
                Type = 4,
                NewValue = keyValuePair.Value?.ToString()
            };
            return Json(obj, JsonRequestBehavior.AllowGet);

        }

        private static void GetChildNodes(IEnumerable<HtmlNode> nodes, ref string template)
        {
            template = nodes.Where(c => c.Attributes["class"] != null && c.Attributes["class"].Value == "loop")
                            .Aggregate(template, (current, item) => current.Replace(item.OuterHtml, " {{each data}} " + item.OuterHtml + " {{/each}} "));
        }
        #endregion

        #region Private method

        private List<DocRelationModel> GetDocumentRelations(int documentCopyId, int userSendId)
        {
            var result = new List<DocRelationModel>();

            var relations = _documentCopyService.GetDocRelations(documentCopyId, userSendId);
            if (relations != null && relations.Any())
            {
                foreach (var docRelation in relations)
                {
                    result.Add(new DocRelationModel
                    {
                        Compendium = docRelation.Compendium,
                        DocCode = docRelation.DocCode,
                        CitizenName = "",
                        DateCreated = docRelation.DateArrived,
                        RelationId = docRelation.RelationId,
                        RelationCopyId = docRelation.RelationCopyId,
                        RelationType = docRelation.RelationType,
                        CategoryName = "",
                        IsAddNext = true
                    });
                }
            }

            return result;
        }

        private List<DocRelationModel> GetDocumentAnswerRelation(int? documentCopyRelationId)
        {
            var result = new List<DocRelationModel>();
            if (!documentCopyRelationId.HasValue)
            {
                return result;
            }

            var documentCopyRelation = _documentCopyService.Get(documentCopyRelationId.Value);
            if (documentCopyRelation == null)
            {
                LogException("Văn bản trả lời không tồn tại!");
                throw new Exception("Văn bản trả lời không tồn tại!");
            }

            var documentRelation = documentCopyRelation.Document;

            result.Add(new DocRelationModel
            {
                RelationCopyId = documentCopyRelationId.Value,
                RelationId = documentCopyRelation.DocumentId,
                RelationType = (int)RelationTypes.LienQuanTraLoi,
                Compendium = documentRelation.Compendium,
                DateCreated = documentRelation.DateCreated,
                DocCode = documentRelation.DocCode,
                CitizenName = documentRelation.CitizenName,
                CategoryName = documentRelation.CategoryName
            });

            return result;
        }

        private ICollection<DocFeeModel> GetDocFees(Guid doctypeId, FeeType type)
        {
            var fees = _feeService.Gets(doctypeId, type, true);
            return fees.Select(f => new DocFeeModel
            {
                DocFeeId = f.FeeId,
                FeeName = f.FeeName,
                Price = f.Price,
                IsRequired = f.IsRequired
            }).ToList();
        }

        private ICollection<DocPaperModel> GetDocPapers(Guid doctypeId, PaperType type)
        {
            var papers = _paperService.Gets(doctypeId, type);

            if (!papers.Any())
            {
                return new List<DocPaperModel>();
            }

            return papers.Select(f => new DocPaperModel
            {
                DocPaperId = f.PaperId,
                PaperName = f.PaperName,
                Amount = f.Amount,
                IsRequired = f.IsRequired
            }).ToList();
        }

        private DataTable loadCompilationData_(dynamic docTypeForm, string timeKey, string organizeKey)
        {
            DataTable dt = null;
            // 20200214 VuHQ Phase 2 - REQ-0 START

            if (!string.IsNullOrEmpty(docTypeForm.FormCodeCompilation) && !string.IsNullOrEmpty(docTypeForm.TableName))
            {
                if (string.IsNullOrEmpty(timeKey))
                    timeKey = GetTimeKeyValue(docTypeForm.ActionLevel);

                if (!string.IsNullOrEmpty(docTypeForm.CompilationId) && docTypeForm.FormCategoryId == FORM_CATEGORY_TARGET)
                {
                    var compilationForm = _formService.Get(Guid.Parse(docTypeForm.CompilationId));
                    var timeKeyFieldName = GetTimeKeyFieldName(docTypeForm.ActionLevel);

                    if (compilationForm != null)
                    {
                        var asciiTableName = XlsxToJson.ConvertToAscii(compilationForm.TableName);
                        dynamic config = JsonConvert.DeserializeObject(docTypeForm.FormCodeCompilation).targetConfigJsonForm;

                        // generate query
                        StringBuilder query = new StringBuilder();
                        query.AppendFormat(" SELECT {0}, {1}", config.schema.Form_Compilation_Match["default"].ToString(), config.schema.Form_Compilation_Select["default"].ToString());
                        query.AppendFormat(" FROM fact_{0}", asciiTableName);
                        query.AppendFormat(" WHERE {0} = '{1}'", config.schema.Form_Compilation_Field["default"].ToString(), config.schema.Form_Compilation_Value["default"].ToString());

                        if (!string.IsNullOrEmpty(timeKey))
                        {
                            query.AppendFormat(" AND {0} = '{1}'", timeKeyFieldName, timeKey);
                        }
                        else
                        {
                            query.AppendFormat(" AND {0} = (SELECT MAX({0}) FROM fact_{1})", timeKeyFieldName, asciiTableName);
                        }

                        MySqlConnection dbConn = new MySqlConnection(_adminSetting.DashboardConnection);
                        MySqlCommand cmd = dbConn.CreateCommand();
                        cmd.CommandText = query.ToString();

                        try
                        {
                            dbConn.Open();
                            using (MySqlDataReader sdr = cmd.ExecuteReader())
                            {
                                //Create a new DataTable.
                                dt = new DataTable(asciiTableName);

                                //Load DataReader into the DataTable.
                                dt.Load(sdr);
                            }
                            dbConn.Close();
                        }
                        catch (Exception e)
                        {

                        }
                    }
                }
                else if (docTypeForm.FormCategoryId == FORM_CATEGORY_SUMMARY)
                {
                    dynamic config = JsonConvert.DeserializeObject(docTypeForm.FormCodeCompilation).summaryConfigJsonForm;
                    string query = string.Empty;
                    //StringBuilder query = new StringBuilder(config.schema.Form_sql["default"].ToString());

                    query = Regex.Replace(config.schema.Form_sql["default"].ToString(),
                        "@TimeKey", timeKey,
                        RegexOptions.IgnoreCase);
                    //query = config.schema.Form_sql["default"].ToString();
                    //query = query.Replace("@TimeKey", timeKey);

                    //var currentDepartment = _departmentService.GetCacheAllUserDepartmentJobTitlesPosition().Where(p => p.UserId == User.GetUserId()).OrderByDescending(p => p.IsPrimary).FirstOrDefault();
                    //var organizekey = currentDepartment.Emails;
                    var organizekey = organizeKey;
                    query = Regex.Replace(query,
                        "@OrganizeKey", "'" + organizekey + "'",
                        RegexOptions.IgnoreCase);
                    query = query.Replace("@OrganizeKey", "'" + organizekey + "'");

                    MySqlConnection dbConn = new MySqlConnection(_adminSetting.DashboardConnection);
                    MySqlCommand cmd = dbConn.CreateCommand();
                    cmd.CommandText = query.ToString();

                    try
                    {
                        dbConn.Open();
                        using (MySqlDataReader sdr = cmd.ExecuteReader())
                        {
                            //Create a new DataTable.
                            var temp = new DataTable("SummaryTable");

                            //Load DataReader into the DataTable.
                            temp.Load(sdr);

                            Dictionary<string, ConfigCompilation> schema = JsonConvert.DeserializeObject<Dictionary<string, ConfigCompilation>>(config.schema.ToString());
                            List<string> fieldNames = new List<string>();

                            // trường hợp cũ chưa có Form_Compilation_Match_Off thì sẽ bỏ qua 3 phần tử đầu
                            // nếu có thì sẽ bỏ qua 4 phần tử đầu
                            var numberFieldIgnore = 2;
                            if (schema.Where(p => p.Key == "Form_Compilation_Match_Off").Count() == 1)
                                numberFieldIgnore = 3;
                            for (var i = 0; i < schema.Count(); i++)
                            {
                                if (i > numberFieldIgnore)
                                {
                                    if (!string.IsNullOrWhiteSpace(schema.ElementAt(i).Value.text))
                                        fieldNames.Add(schema.ElementAt(i).Value.text.Trim());
                                }
                            }
                            if (!string.IsNullOrWhiteSpace(config.schema.Form_Compilation_Target_Match["default"].ToString().Trim()))
                                fieldNames.Add(config.schema.Form_Compilation_Target_Match["default"].ToString().Trim());

                            dt = temp.DefaultView.ToTable(false, fieldNames.ToArray());
                        }
                        dbConn.Close();
                    }
                    catch (Exception e)
                    {

                    }
                }

            }
            // 20200214 VuHQ Phase 2 - REQ-0 END
            return dt;
        }

        private DataTable loadCompilationData(dynamic docTypeForm, string timeKey)
        {
            DataTable dt = null;
            // 20200214 VuHQ Phase 2 - REQ-0 START

            if (!string.IsNullOrEmpty(docTypeForm.FormCodeCompilation) && !string.IsNullOrEmpty(docTypeForm.TableName))
            {
                if (string.IsNullOrEmpty(timeKey))
                    timeKey = GetTimeKeyValue(docTypeForm.ActionLevel);

                if (!string.IsNullOrEmpty(docTypeForm.CompilationId) && docTypeForm.FormCategoryId == FORM_CATEGORY_TARGET)
                {
                    var compilationForm = _formService.Get(Guid.Parse(docTypeForm.CompilationId));
                    var timeKeyFieldName = GetTimeKeyFieldName(docTypeForm.ActionLevel);

                    if (compilationForm != null)
                    {
                        var asciiTableName = XlsxToJson.ConvertToAscii(compilationForm.TableName);
                        dynamic config = JsonConvert.DeserializeObject(docTypeForm.FormCodeCompilation).targetConfigJsonForm;

                        // generate query
                        StringBuilder query = new StringBuilder();
                        query.AppendFormat(" SELECT {0}, {1}", config.schema.Form_Compilation_Match["default"].ToString(), config.schema.Form_Compilation_Select["default"].ToString());
                        query.AppendFormat(" FROM fact_{0}", asciiTableName);
                        query.AppendFormat(" WHERE {0} = '{1}'", config.schema.Form_Compilation_Field["default"].ToString(), config.schema.Form_Compilation_Value["default"].ToString());

                        if (!string.IsNullOrEmpty(timeKey))
                        {
                            query.AppendFormat(" AND {0} = '{1}'", timeKeyFieldName, timeKey);
                        }
                        else
                        {
                            query.AppendFormat(" AND {0} = (SELECT MAX({0}) FROM fact_{1})", timeKeyFieldName, asciiTableName);
                        }

                        MySqlConnection dbConn = new MySqlConnection(_adminSetting.DashboardConnection);
                        MySqlCommand cmd = dbConn.CreateCommand();
                        cmd.CommandText = query.ToString();

                        try
                        {
                            dbConn.Open();
                            using (MySqlDataReader sdr = cmd.ExecuteReader())
                            {
                                //Create a new DataTable.
                                dt = new DataTable(asciiTableName);

                                //Load DataReader into the DataTable.
                                dt.Load(sdr);
                            }
                            dbConn.Close();
                        }
                        catch (Exception e)
                        {

                        }
                    }
                }
                else if (docTypeForm.FormCategoryId == FORM_CATEGORY_SUMMARY)
                {
                    dynamic config = JsonConvert.DeserializeObject(docTypeForm.FormCodeCompilation).summaryConfigJsonForm;
                    string query = string.Empty;
                    //StringBuilder query = new StringBuilder(config.schema.Form_sql["default"].ToString());

                    query = Regex.Replace(config.schema.Form_sql["default"].ToString(),
                        "@TimeKey", timeKey,
                        RegexOptions.IgnoreCase);
                    //query = config.schema.Form_sql["default"].ToString();
                    //query = query.Replace("@TimeKey", timeKey);

                    var currentDepartment = _departmentService.GetCacheAllUserDepartmentJobTitlesPosition().Where(p => p.UserId == User.GetUserId()).OrderByDescending(p => p.IsPrimary).FirstOrDefault();
                    var organizekey = currentDepartment.Emails;
                    query = Regex.Replace(query,
                        "@OrganizeKey", "'" + organizekey + "'",
                        RegexOptions.IgnoreCase);
                    query = query.Replace("@OrganizeKey", "'" + organizekey + "'");

                    MySqlConnection dbConn = new MySqlConnection(_adminSetting.DashboardConnection);
                    MySqlCommand cmd = dbConn.CreateCommand();
                    cmd.CommandText = query.ToString();

                    try
                    {
                        dbConn.Open();
                        using (MySqlDataReader sdr = cmd.ExecuteReader())
                        {
                            //Create a new DataTable.
                            var temp = new DataTable("SummaryTable");

                            //Load DataReader into the DataTable.
                            temp.Load(sdr);

                            Dictionary<string, ConfigCompilation> schema = JsonConvert.DeserializeObject<Dictionary<string, ConfigCompilation>>(config.schema.ToString());
                            List<string> fieldNames = new List<string>();

                            // trường hợp cũ chưa có Form_Compilation_Match_Off thì sẽ bỏ qua 3 phần tử đầu
                            // nếu có thì sẽ bỏ qua 4 phần tử đầu
                            var numberFieldIgnore = 2;
                            if (schema.Where(p => p.Key == "Form_Compilation_Match_Off").Count() == 1)
                                numberFieldIgnore = 3;
                            for (var i = 0; i < schema.Count(); i++)
                            {
                                if (i > numberFieldIgnore)
                                {
                                    if (!string.IsNullOrWhiteSpace(schema.ElementAt(i).Value.text))
                                        fieldNames.Add(schema.ElementAt(i).Value.text.Trim());
                                }
                            }
                            if (!string.IsNullOrWhiteSpace(config.schema.Form_Compilation_Target_Match["default"].ToString().Trim()))
                                fieldNames.Add(config.schema.Form_Compilation_Target_Match["default"].ToString().Trim());

                            var mappingMaDinhDanhCP = docTypeForm.MappingMaDinhDanhCP;
                            if (!String.IsNullOrEmpty(mappingMaDinhDanhCP))
                            {
                                JObject rss = JObject.Parse(mappingMaDinhDanhCP);

                                var existingNode = rss[timeKey];
                                
                                var x = 0;
                                foreach (DataRow row in temp.DefaultView.Table.Rows)
                                {
                                    foreach (var node in existingNode)
                                    {
                                        if(node["madinhdanhdb"].ToString() == row["machitieu"].ToString() && !String.IsNullOrEmpty(node["madinhdanh"].ToString()) )
                                        {
                                            row["machitieu"] = node["madinhdanh"].ToString();
                                        }
                                        
                                    }
                                 
                                }
                               
                            }

                            
                            dt = temp.DefaultView.ToTable(false, fieldNames.ToArray());


                            //dt.Columns[0].ColumnName = "madinhdanh";
                            //dt.Columns[0].Caption = "madinhdanh";
                            //dt.Columns[1].ColumnName = "cacthuoctinhbaocao_kehoachnamcuakybaocao";
                            //dt.Columns[1].Caption = "cacthuoctinhbaocao_kehoachnamcuakybaocao";
                            //dt.Columns[2].ColumnName = "cacthuoctinhbaocao_kehoachnamsovoikehoachnamtruoc";
                            //dt.Columns[2].Caption = "cacthuoctinhbaocao_kehoachnamsovoikehoachnamtruoc";
                            //dt.Rows[0].ItemArray = new object[] { "KHDT_VHXH_ATGT", "222"};
                            //dt.Rows[1].ItemArray = new object[] { "KHDT_VHXH_ATGT", "222"};
                            //dt.DefaultView[0].Row.ItemArray = new object[] { "KHDT_VHXH_ASXHBTXH", "111", "1111" };
                            //dt.DefaultView[0].Row.ItemArray = 

                        }
                        dbConn.Close();
                    }
                    catch (Exception e)
                    {

                    }
                }

            }
            // 20200214 VuHQ Phase 2 - REQ-0 END
            return dt;
        }

        private string GetTimeKeyValue(int actionLevel)
        {
            var currentTimeNow = DateTime.Now;
            var _timeKeyValue = string.Empty;

            switch (actionLevel)
            {
                case 1:
                    // yearkey
                    _timeKeyValue = currentTimeNow.ToString("yyyy");
                    break;
                case 2:
                    // halfkey
                    _timeKeyValue = currentTimeNow.ToString("yyyy") + GetHalf(currentTimeNow);
                    break;
                case 3:
                    // quarterkey
                    _timeKeyValue = currentTimeNow.ToString("yyyy") + GetQuarter(currentTimeNow);
                    break;
                case 4:
                    // monthkey
                    _timeKeyValue = currentTimeNow.ToString("yyyyMM");
                    break;
                case 5:
                    // weekkey
                    _timeKeyValue = currentTimeNow.ToString("yyyy") + currentTimeNow.WeekOfYear();
                    break;
                case 6:
                    // datekey
                    _timeKeyValue = currentTimeNow.ToString("yyyyMMdd");
                    break;
                case 7:
                    // minutekey
                    _timeKeyValue = currentTimeNow.ToString("yyyyMMddHHmm");
                    break;
            }

            return _timeKeyValue;
        }

        private string GetTimeKeyFieldName(int actionLevel)
        {
            var _timeKeyFieldName = string.Empty;

            switch (actionLevel)
            {
                case 1:
                    // yearkey
                    _timeKeyFieldName = "yearkey";
                    break;
                case 2:
                    // halfkey
                    _timeKeyFieldName = "halfkey";
                    break;
                case 3:
                    // quarterkey
                    _timeKeyFieldName = "quarterkey";
                    break;
                case 4:
                    // monthkey
                    _timeKeyFieldName = "monthkey";
                    break;
                case 5:
                    // weekkey
                    _timeKeyFieldName = "weekkey";
                    break;
                case 6:
                    // datekey
                    _timeKeyFieldName = "datekey";
                    break;
                case 7:
                    // minutekey
                    _timeKeyFieldName = "minutekey";
                    break;
            }

            return _timeKeyFieldName;
        }

        private int GetHalf(DateTime date)
        {
            return (date.Month + 5) / 6;
        }
        private int GetQuarter(DateTime date)
        {
            return (date.Month + 2) / 3;
        }

        private List<DocumentContentModel> LoadFormDoctype(Guid doctypeId, CategoryBusinessTypes categoryBusiness)
        {
            var result = new List<DocumentContentModel>();

            if (categoryBusiness == CategoryBusinessTypes.Hsmc)
            {
                // VuHQ 20191125 REQ-5
                var forms = _doctypeFormService.GetForms(d => new
                {
                    d.Form.FormId,
                    d.Form.FormName,
                    d.Form.FormTypeId,
                    d.IsPrimary,
                    d.Form.EmbryonicPath,
                    d.Form.EmbryonicLocationName,
                    d.Form.Template,
                    d.Form.FormCode,
                    d.Form.CompilationId,
                    d.Form.ConfigFunction,
                    d.Form.ChildCompilationId,
                    d.Form.DefineFieldJson,
                    d.Form.DefineConfigJson,
                    d.Form.DefineValueJson,
                    d.Form.FormHeader,
                    d.Form.FormFooter,
                    d.Form.ExplicitTemplate,
                    d.Form.FormCodeCompilation,
                    d.Form.TableName,
                    d.Form.FormCategoryId,
                    d.DocType.ActionLevel,
                    d.Form.MappingMaDinhDanhCP
                }, doctypeId);

                DataTable dt = new DataTable();

                if (forms.Count() > 0)
                {
                    var docTypeForm = forms.ElementAt(0);
                    //dt = loadCompilationData(docTypeForm, string.Empty);
                }

                var compilation = new { data = JsonConvert.SerializeObject(dt), code = forms.Any() ? forms.ElementAt(0).FormCodeCompilation : null };

                result.AddRange(forms.Select(f => new DocumentContentModel
                {
                    FormId = f.FormId.ToString(),
                    ContentName = f.EmbryonicPath,
                    Content = string.Empty,
                    FormTypeId = f.FormTypeId,
                    IsMain = f.IsPrimary,
                    ContentUrl = "EmbryonicForm/" + f.EmbryonicLocationName,
                    Url = string.Format("d_{0}{0}", DateTime.Now.ToString("yyyyMMddhhmmss"), Guid.NewGuid().ToString()),
                    FormCode = f.FormCode,
                    CompilationId = f.CompilationId,
                    ConfigFunction = f.ConfigFunction,
                    ChildCompilationId = f.ChildCompilationId,
                    // VuHQ 20191125 REQ-5
                    DefineFieldJson = f.DefineFieldJson,
                    DefineConfigJson = f.DefineConfigJson,
                    DefineValueJson = f.DefineValueJson,
                    FormHeader = f.FormHeader,
                    FormFooter = f.FormFooter,
                    ExplicitTemplate = f.ExplicitTemplate,
                    Compilation = JsonConvert.SerializeObject(compilation, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }),
                    FormCategoryId = f.FormCategoryId

                }).ToList());
            }

            return result;
        }

        private IEnumerable<DocumentContentCached> LoadFormDoctypePlus(Guid doctypeId, CategoryBusinessTypes categoryBusiness)
        {
            var result = new List<DocumentContentCached>();

            if (categoryBusiness == CategoryBusinessTypes.Hsmc)
            {
                // VuHQ 20191125 REQ-5
                var forms = _doctypeFormService.GetForms(d => new
                {
                    d.Form.FormId,
                    d.Form.FormName,
                    d.Form.FormTypeId,
                    d.IsPrimary,
                    d.Form.EmbryonicPath,
                    d.Form.EmbryonicLocationName,
                    d.Form.Template,
                    d.Form.FormCode,
                    d.Form.CompilationId,
                    d.Form.ConfigFunction,
                    d.Form.ChildCompilationId,
                    d.Form.DefineFieldJson,
                    d.Form.DefineConfigJson,
                    d.Form.DefineValueJson,
                    d.Form.FormHeader,
                    d.Form.FormFooter,
                    d.Form.ExplicitTemplate,
                    d.Form.FormCodeCompilation,
                    d.Form.TableName,
                    d.Form.FormCategoryId,
                    d.DocType.ActionLevel
                }, doctypeId);

                DataTable dt = new DataTable();

                if (forms.Count() > 0)
                {
                    var docTypeForm = forms.ElementAt(0);
                    dt = loadCompilationData(docTypeForm, string.Empty);
                }

                var compilation = new { data = JsonConvert.SerializeObject(dt), code = forms.ElementAt(0).FormCodeCompilation };

                result.AddRange(forms.Select(f => new DocumentContentCached
                {
                    FormId = f.FormId.ToString(),
                    ContentName = f.EmbryonicPath,
                    Content = string.Empty,
                    FormTypeId = f.FormTypeId,
                    ContentUrl = "EmbryonicForm/" + f.EmbryonicLocationName,
                    Url = string.Format("d_{0}{0}", DateTime.Now.ToString("yyyyMMddhhmmss"), Guid.NewGuid().ToString()),
                    FormCode = f.FormCode,
                    CompilationId = f.CompilationId,
                    ConfigFunction = f.ConfigFunction,
                    ChildCompilationId = f.ChildCompilationId,
                    // VuHQ 20191125 REQ-5
                    DefineFieldJson = f.DefineFieldJson,
                    DefineConfigJson = f.DefineConfigJson,
                    DefineValueJson = f.DefineValueJson,
                    FormHeader = f.FormHeader,
                    FormFooter = f.FormFooter,
                    ExplicitTemplate = f.ExplicitTemplate,
                    Compilation = JsonConvert.SerializeObject(compilation, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }),
                    FormCategoryId = f.FormCategoryId

                }));
            }

            return result;
        }

        //private IEnumerable<Supplementary> GetSupplementary(Document doc)
        //{
        //    if (!doc.IsHSMC)
        //    {
        //        return null;
        //    }

        //    return _supplementaryServcie.Gets(doc.DocumentId, false);
        //}

        //private IEnumerable<Approver> GetApprovers(Document document)
        //{
        //    if (!document.IsSuccess.HasValue)
        //    {
        //        return new List<Approver>();
        //    }

        //    return _approverService.Gets(document.DocumentId);
        //}

        private List<Store> GetStores(int? storeId)
        {
            var result = new List<Store>();

            if (!storeId.HasValue || storeId.Value <= 0)
            {
                return result;
            }

            var store = _storeService.GetFromCache(storeId.Value);
            if (store == null)
            {
                return result;
            }

            result.Add(new Store()
            {
                StoreId = store.StoreId,
                StoreName = store.StoreName,
                CodeIds = store.CodeIds.Stringify()
            });

            return result;
        }

        private int CurrentUserId()
        {
            return _userService.CurrentUser.UserId;
        }

        #endregion

        public JsonResult GetInOutCode(int storeId = 0, int categoryId = 0)
        {
            var codes = new Dictionary<int, string>();
            var codeIds = _codeService.GetCodeIds(storeId, categoryId);
            if (codeIds != null && codeIds.Any())
            {
                var now = DateTime.Now;
                codes = _codeService.GetCodes(codeIds, now, CategoryBusinessTypes.VbDen, isDocCode: false, storeId: storeId);
            }

            return Json(codes, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetIsTransferPublish(int documentCopyId)
        {
            // Todo: Trong trường hợp thế này có thể tối ưu được như dưới:
            // - Trả về thông tin IsTransferPublish khi mở văn bản luôn, sao phải check lại thế này làm gì?
            // - Nếu cần check trên server sao ko truyền documentId mà lại đi truyền documentCopyId?

            var docCopy = _documentCopyService.Get(documentCopyId);
            if (docCopy != null)
            {
                var document = _documentService.Get(docCopy.DocumentId);
                if (document != null)
                {
                    return Json(new { error = false, data = document.IsTransferPublish }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new { error = true, data = "" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckDocCodeIsUsed(string doccode, string organization, Guid? documentId = null)
        {
            var isUsed = _codeService.CodeIsUsed(doccode, true, 0, CategoryBusinessTypes.VbDen, organization, documentId);
            var codes = isUsed ? _codeService.GetDocCodeUsed(doccode, documentId) : new List<CodeUseds>();
            return Json(new
            {
                isUsed = isUsed,
                codes = codes.Select(c => new
                {
                    c.Organization,
                    c.Compendium,
                    DateCreated = c.DateCreated.ToString("d")
                })
            }, JsonRequestBehavior.AllowGet);
        }

        public void CancelCode(string inOutCodes)
        {
            try
            {
                if (string.IsNullOrEmpty(inOutCodes)) return;

                var codes = Json2.ParseAs<IEnumerable<Dictionary<int, string>>>(inOutCodes);
                foreach (var code in codes)
                {
                    if (code.Values == null) continue;
                    _codeService.CancelCode(code.Values.First(), CurrentUserId());
                }
            }
            catch (Exception e)
            {
                // LogException(e);
            }
        }

        /// <summary>
        /// TienBV: hàm này dùng để fix lỗi tạm thời, lưu ý không sử dụng
        /// </summary>
        /// <returns></returns>
        public JsonResult UpdateDateAppointed()
        {
            //if (_permissionSerice.HasPemission("WelcomeIndex"))//admin
            //{
            //    var documents = _documentService.Gets(false, d => !d.DateAppointed.HasValue && d.Original == 2);
            //    var docExpire = 60;
            //    foreach (var doc in documents)
            //    {
            //        var dateAppointed = _workTimeHelper.GetDateAppoint(doc.DateCreated, docExpire);
            //        doc.DateAppointed = dateAppointed;
            //        doc.ExpireProcess = docExpire;
            //        _documentService.Update(doc);
            //    }
            //}

            return Json(new { success = "Thành công" }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string FixConvert(string u)
        {
            var userId = 0;
            if (!string.IsNullOrEmpty(u))
            {
                var user = _userService.GetAllCached().SingleOrDefault(i => i.Username.Equals(u, StringComparison.OrdinalIgnoreCase));
                if (user == null)
                {
                    return "Sai tài khoản người dùng";
                }

                userId = user.UserId;
            }
            try
            {
                _documentCopyService.CapNhatChoPhepXuLyVanBanConvert(userId);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "ok";
        }

        #region Nested type: JsConfirmProcess

        private class JsConfirmProcess
        {
            #region Instance Properties

            public int DocumentCopyId { get; set; }

            public string FullName { get; set; }

            public bool IsViewed { get; set; }

            public int UserId { get; set; }

            public string Username { get; set; }

            #endregion
        }

        private class JsLaylaivanbanItem
        {
            #region Instance Properties

            public string Name { get; set; }

            public DateTime DateCreated { get; set; }

            #endregion
        }

        #endregion
    }
}