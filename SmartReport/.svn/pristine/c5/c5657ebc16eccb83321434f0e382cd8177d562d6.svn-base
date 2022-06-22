using System;
using System.Linq;
using System.Web.Mvc;
using Bkav.eGovCloud.Business;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Models;
using Bkav.eGovCloud.Web.Framework;
using System.Collections.Generic;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Business.Caching;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Entities.Customer.Settings;
using Bkav.eGovCloud.Business.Utils;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Controllers
{
	//[RequireHttps]
	public class FinishController : CustomerBaseController
	{
		private readonly DocumentCopyBll _docCopyService;
		private readonly PaperBll _paperService;
		private readonly ApproverBll _approverService;
		private readonly StorePrivateBll _storePrivateService;
		private readonly UserActivityLogBll _userActivityLogService;
		private readonly DocumentPermissionHelper _documentPermissionHelper;
		private readonly UserBll _userService;
		private readonly AddressBll _addressService;
        private readonly CodeBll _codeService;
        private readonly CommentBll _commentService;
        private readonly DepartmentBll _departmentService;
        private readonly DocTypeBll _docTypeService;
        private readonly DocumentBll _documentService;
        private readonly AttachmentBll _attachmentService;
        private readonly ExtensionTimeBll _extensionTimeService;
        private readonly DailyProcessBll _dailyProcessService;
        private readonly StoreBll _storeService;
        private readonly JobTitlesBll _jobTitlesService;
        private readonly KeyWordBll _keyWordService;
        private readonly IncreaseBll _increaseService;
        private readonly DocumentPublishBll _documentPublishService;
        private readonly DocumentPublishPlusBll _documentPublishPlusService;
        private readonly WorktimeHelper _workTimeHelper;
        private readonly AnticipateBll _anticipateService;
        private readonly NotificationBll _notificationService;
        private ReportConfigSettings _reportConfigSettings;
        private readonly MobileDeviceBll _mobileDeviceService;
        private readonly DocFinishBll _docFinishService;
        private readonly FormBll _formService;
        private readonly DocTypeFormBll _doctypeFormService;
        private readonly EdocBll _edocService;
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
        private readonly WorkflowBll _workflowService;
#pragma warning disable 618

        public FinishController(
            CommentBll commentService,
            EdocBll edocService,
            DocTypeBll docTypeService,
            DocumentBll documentService,
            DocumentCopyBll docCopyService,
			PaperBll paperService,
			ApproverBll approverService,
			StorePrivateBll storePrivateService,
			DocumentPermissionHelper documentPermissionHelper,
			UserActivityLogBll userActivityLogService,
			UserBll userService,
			AddressBll addressService,
            CodeBll codeService,
            DepartmentBll departmentService,
            AttachmentBll attachmentService,
            ExtensionTimeBll extensionTimeService,
            DailyProcessBll dailyProcessService,
            StoreBll storeService,
            JobTitlesBll jobTitlesService,
            KeyWordBll keyWordService,
            IncreaseBll increaseService,
            DocumentPublishBll documentPublishService,
            DocumentPublishPlusBll documentPublishPlusService,
            WorktimeHelper workTimeHelper,
            AnticipateBll anticipateService,
            DocFinishBll docFinishService,
            UserConnectionBll userConnectionService,        
            ResourceBll resourceService,
            Helper.UserSetting helperUserSetting,
            Helper.NotificationHelper notificationHelper,
            AdminGeneralSettings generalSettings,
            SendEmailHelper mailHelper,
            NotificationBll notificationService,
            MobileDeviceBll mobileDeviceService,
            DocumentContentBll documentContentService,
            CategoryBll categoryService, DocumentsCache documentCache,
            StoreDocBll storeDocService, DocumentHelper documentHelper,
            CitizenBll citizenService,
            FormBll formService,
             DocTypeFormBll doctypeFormService,
             ReportConfigSettings reportConfigSettings,
             WorkflowBll workflowService)
		{
            _commentService = commentService;
            _edocService = edocService;
            _docTypeService = docTypeService;
            _documentService = documentService;
            _docCopyService = docCopyService;
			_paperService = paperService;
			_approverService = approverService;
			_storePrivateService = storePrivateService;
			_documentPermissionHelper = documentPermissionHelper;
			_userActivityLogService = userActivityLogService;
			_userService = userService;
			_addressService = addressService;
            _codeService = codeService;
            _departmentService = departmentService;
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
            _userActivityLogService = userActivityLogService;
            _docFinishService = docFinishService;
            _userConnectionService = userConnectionService;
            _resourceService = resourceService;
            _generalSettings = generalSettings;
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
            _workflowService = workflowService;
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="docCopyId"></param>
		/// <returns></returns>
		public ActionResult Index(int docCopyId)
		{
			// CuongNT@bkav.com - 210613: Ủy quyền xử lý
			int userSendId;
			if (!_documentPermissionHelper.CheckForUyQuyenVaXuLyVanBan(docCopyId, CurrentUserId(), out userSendId))
			{
				return null;
			}

			var docCopy = _docCopyService.Get(docCopyId);
			if (docCopy == null)
			{
				return PartialView("Index", new DocumentModel());
			}
			// TODO: (Kết quả: TienBV-?. Reporter: CuongNT-050313). Cần viết hàm Gets(int doctypeid, PaperType papertypeId) thay vì lấy toàn bộ từ database lên như ở đây rồi mới lấy ra loại cần dùng.
			ViewBag.PaperReturns = _paperService.Gets(docCopy.Document.DocTypeId.Value, PaperType.TraCongDan);
			ViewBag.Approvers = _approverService.Gets(docCopyId);
			ViewBag.DocumentPermissions = _documentPermissionHelper.CheckAll(docCopy.Document, docCopy, userSendId);
			return PartialView("Index", docCopy.Document.ToModel());
		}
        [HttpPost]
        public JsonResult DinhChinh(int docCopyId) {
            var docCopy = _docCopyService.Get(docCopyId);
            var userNguoiThamGia = docCopy.UserNguoiThamGia;
            if (userNguoiThamGia == "")
            {
                return Json(new { error = true, message = "Không tồn tại người tham gia." });
            }
            string[] arrUser = userNguoiThamGia.Split(';');
            var userFristCreate = "1";
            var _userCreate = "1";
            if (arrUser.Length > 2)
            {
                userFristCreate = arrUser[arrUser.Length - 2];
                if (arrUser.Length > 3)
                {
                    _userCreate = arrUser[arrUser.Length - 3];
                }else
                {
                    _userCreate = arrUser[1];
                }
            }
            else {
                return Json(new { error = true, message = "Báo cáo lỗi từ người tham gia!" });
            }
            var workflowId = docCopy.WorkflowId;
            var workflow = _workflowService.Get(workflowId);
            var tmp = Json2.ParseAs<Bkav.eGovCloud.Core.Workflow.Path>(workflow.Json);
            if ( tmp == null ) {
                return Json(new { error = true, message = "Không thể đính chính do không xác định được luồng chuyển!" });
            }
           
            if (User.GetUserId() != Int32.Parse(_userCreate))
            {
                return Json(new { error = true, message = "Bạn không có quyền đính chính báo cáo!" });
            }
            if (docCopy == null) {
                return Json(new { error = true,  message = "Không tồn tại bản ghi DocCopy."});
            }
            var docId = docCopy.DocumentId;
            if (docId == Guid.Empty) {
                return Json(new { error = true, message = "Không tồn tại DocumentId" });
            }
            var doc = _documentService.Get(docId);
            if (docCopy.Status != 4 && doc.Status != 4) {
                return Json(new { error = true, message = "Báo cáo chưa được kết thúc!" });
            }
            if (doc.StatusReport != 4)
            {
                return Json(new { error = true, message = "Báo cáo chưa được phát hành!" });
            }

            var listUserCreate = _userService.Get(Int32.Parse(userFristCreate));
            var _listUserCreate = _userService.Get(Int32.Parse(_userCreate));
            if (listUserCreate == null) {
                return Json(new { error = true, message = "Không tồn tại người dùng" });
            }
            if (_listUserCreate == null)
            {
                return Json(new { error = true, message = "Không tồn tại người tạo" });
            }

            if (doc == null) {
                return Json(new { error = true, message = "Không tồn tại DocumentId" });
            }
            
            //update document
            var dateModifi = DateTime.Now;
            doc.StatusReport = 2;
            doc.Status = 2;
            doc.UserSuccessId = 0;
            doc.DateArrived = dateModifi;
            _documentService.Update(doc);
            //end update document

            //update documentCopy
            docCopy.Status = 2;
            docCopy.UserCurrentId = Int32.Parse(_userCreate);
            docCopy.UserSendId = null;
            docCopy.NodeCurrentName = tmp.Nodes[0].NodeName;
            docCopy.NodeCurrentId = 1;
            docCopy.NodeCurrentPermission = 5;
            docCopy.UserCurrentName = _listUserCreate.Username;
            docCopy.CurrentDepartmentName = listUserCreate.FullName;
            docCopy.DateCreated = dateModifi;
            docCopy.DateReceived = dateModifi;
            _docCopyService.Update(docCopy);
            //end update documentCopy
            _docCopyService.ClearCache(docCopyId);
            _commentService.SendComment(docCopyId, Int32.Parse(_userCreate), _listUserCreate.FullName + " đã đính chính báo cáo.", DateTime.Now, _listUserCreate.FullName + " đã đính chính báo cáo.");
            CreateActivityLog(ActivityLogType.DinhChinh, string.Format("{0} đã đính chính báo cáo {1} , kỳ -  {2} lúc: {3}", listUserCreate.FullName, doc.DocTypeName, doc.TimeKey, dateModifi.ToString()));
            return Json(new { success = true , message = "Đã đính chính,  báo cáo được chuyển đến chờ xử lý "});
        }
        /// <summary>
		/// Kết thúc xử lý
		/// </summary>
		/// <param name="documentCopyId"></param>
		/// <param name="storePrivateId"></param>
		/// <returns></returns>
		[HttpPost]
        // [ValidateAntiForgeryToken(Salt = "FinishUpdateFinish")]
        public JsonResult FinishAndPublish(int documentCopyId, string comment, int? storePrivateId, bool isThongBao = false)
        {
            try
            {

                bool handleDone = false;
                int currentUserId = CurrentUserId();
                int userSendId = currentUserId;
                var documentCopy = _docCopyService.Get(documentCopyId);

                try
                {
                    var clPublish = new PublishController(_codeService, _userService, _addressService, _documentPublishService, _documentPublishPlusService, _documentHelper, _documentCache, _commentService, _docCopyService, _documentPermissionHelper, _attachmentService,
                       _edocService, _anticipateService, _documentService, _mailHelper, _docTypeService, _doctypeFormService, _formService, _departmentService, _generalSettings, _reportConfigSettings);
                    if (documentCopy.Document.CategoryBusinessId == 4) {
                        documentCopy.Document.StatusReport = 4;
                        _documentService.Update(documentCopy.Document);
                        clPublish.SendToLgsp(documentCopy.Document.DocumentId, documentCopy.Document, isJSONDaTa: true);
                        handleDone = true;
                    }             
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    handleDone = false;
                    return Json(new { error = true, message = "Phát hành xử lý bị lỗi khi đẩy lên SonCD." });
                }

                if (handleDone) {
                    if (documentCopy == null)
                    {
                        return Json(new { error = true, message = "Văn bản không tồn tại" });
                    }
                    var finished = false;
                    if (isThongBao)
                    {
                        if (documentCopy.UserThongBaos().Contains(userSendId))
                        {
                            _docCopyService.UpdateUserThongBao(documentCopy, addUserIds: new List<int>(), removedUserIds: new List<int>() { userSendId }, hasSaveChange: true);
                        }

                        finished = true;
                    }

                    // Nếu là hồ sơ liên thông bị gửi nhầm( DocTypeId không tồn tại) sẽ cho kết thúc hồ sơ này tại đây
                    if (!finished && documentCopy.Document.Original == 2 && documentCopy.Document.DocTypeId == null && documentCopy.UserCurrentId == userSendId)
                    {
                        Finish(documentCopy, userSendId, comment);
                        finished = true;
                    }

                    // Kiểm tra điều kiện nếu mà văn bản này có thể kết thúc mà User hiện tại ko có quyền xử lý thì là văn bản thông báo
                    if (!finished && _documentPermissionHelper.CheckForQuyenXuly(documentCopy, userSendId))
                    {
                        Finish(documentCopy, userSendId, comment);
                        finished = true;
                    }

                    if (storePrivateId.HasValue)
                    {
                        var storePrivate = _storePrivateService.CheckPermissionStorePrivate(storePrivateId.Value, userSendId);
                        if (storePrivate != null && storePrivate.Status == (byte)StorePrivateStatus.IsActive)
                        {
                            _storePrivateService.AddDocumentToStore(storePrivate, documentCopy.DocumentCopyId, documentCopy.DocumentId);
                        }
                    }             
                }
                return Json(new { success = true, message = "Phát hành thành công." });
            }
            catch (Exception ex)
            {
                LogException(ex);
                return Json(new { error = true, message = "Kết thúc xử lý bị lỗi." });
            }
        }
        /// <summary>
        /// Kết thúc xử lý
        /// </summary>
        /// <param name="documentCopyId"></param>
        /// <param name="storePrivateId"></param>
        /// <returns></returns>
        [HttpPost]
		// [ValidateAntiForgeryToken(Salt = "FinishUpdateFinish")]
		[ValidateAntiForgeryToken]
		public JsonResult UpdateFinish(int documentCopyId, string comment, int? storePrivateId, bool isThongBao = false)
		{
			try
			{
				int currentUserId = CurrentUserId();
				int userSendId = currentUserId;
				var documentCopy = _docCopyService.Get(documentCopyId);
				if (documentCopy == null)
				{
					return Json(new { error = true, message = "Văn bản không tồn tại" });
				}

				if (!isThongBao && !_documentPermissionHelper.CheckForUyQuyenVaXuLyVanBan(documentCopy, currentUserId, out userSendId))
				{
					return null;
				}


				if (_documentPermissionHelper.CheckForKetThucVanBan(documentCopy, userSendId))
				{
					var finished = false;
					if (isThongBao)
					{
						if (documentCopy.UserThongBaos().Contains(userSendId))
						{
							_docCopyService.UpdateUserThongBao(documentCopy, addUserIds: new List<int>(), removedUserIds: new List<int>() { userSendId }, hasSaveChange: true);
						}

						finished = true;
					}

					// Nếu là hồ sơ liên thông bị gửi nhầm( DocTypeId không tồn tại) sẽ cho kết thúc hồ sơ này tại đây
					if (!finished && documentCopy.Document.Original == 2 && documentCopy.Document.DocTypeId == null && documentCopy.UserCurrentId == userSendId)
					{
						Finish(documentCopy, userSendId, comment);
						finished = true;
					}

					// Kiểm tra điều kiện nếu mà văn bản này có thể kết thúc mà User hiện tại ko có quyền xử lý thì là văn bản thông báo
					if (!finished && _documentPermissionHelper.CheckForQuyenXuly(documentCopy, userSendId))
					{
						Finish(documentCopy, userSendId, comment);
						finished = true;
					}

					if (storePrivateId.HasValue)
					{
						var storePrivate = _storePrivateService.CheckPermissionStorePrivate(storePrivateId.Value, userSendId);
						if (storePrivate != null && storePrivate.Status == (byte)StorePrivateStatus.IsActive)
						{
							_storePrivateService.AddDocumentToStore(storePrivate, documentCopy.DocumentCopyId, documentCopy.DocumentId);
						}
					}

					return Json(new { success = true });
				}

				return Json(new { error = true, message = "Không có quyền kết thúc văn bản." });
			}
			catch (Exception ex)
			{
				LogException(ex);
				return Json(new { error = true, message = "Kết thúc xử lý bị lỗi." });
			}
		}

		/// <summary>
		/// Chức năng lấy lại văn bản kết thúc
		/// </summary>
		/// <param name="documentCopyId"></param>
		/// <returns></returns>
		[HttpPost]
		//[ValidateAntiForgeryToken(Salt = "FinishUndoFinish")]
		public JsonResult UndoFinish(int documentCopyId)
		{
			var currentUserId = _userService.CurrentUser.UserId;
			var docCopy = _docCopyService.Get(documentCopyId);

			if (docCopy != null)
			{
				_docCopyService.UndoFinish(docCopy);
				return Json(new { error = false, data = new { } });
			}
			return Json(new { error = true, message = "Bản sao không tồn tại." });
		}

		/// <summary>
		/// Kết thúc xử lý văn bản
		/// </summary>
		/// <param name="documentCopy">văn bản copy</param>
		/// <param name="userSendId"></param>
		private void Finish(Entities.Customer.DocumentCopy documentCopy, int userSendId, string comment)
		{
			var dateCreated = DateTime.Now;

			var user = _userService.GetFromCache(userSendId);

			if (comment == "undefined")
			{
				comment = "";
			}

			if (documentCopy.StatusInEnum != DocumentStatus.KetThuc && documentCopy.IsCurrentUser(userSendId))
			{
				comment = string.Format("{0} kết thúc văn bản vào lúc {1}: {2}", user.FullName, dateCreated.ToString("dd/MM/yyyy HH:mm:ss"), comment);

				_docCopyService.Finish(documentCopy, dateCreated, userSendId, comment);

				_docCopyService.ClearCache(documentCopy.DocumentCopyId);
			}

			CreateActivityLog(ActivityLogType.KetThucVanBan, string.Format("{0} kết thúc văn bản: {1}", User.GetUserNameWithDomain(), documentCopy.Document.Compendium));
		}

		private int CurrentUserId()
		{
			return _userService.CurrentUser.UserId;
		}

	}
}
