#region

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;
using Bkav.eGovCloud.Business.Objects;
using Bkav.eGovCloud.Core.Caching;
using Bkav.eGovCloud.Core.Document;
using Bkav.eGovCloud.Core.History;
using Bkav.eGovCloud.Core.Statistic;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Core.Workflow;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Entities.Enum;
using Bkav.eGovCloud.Business.Objects.CacheObjects;
using Bkav.eGovCloud.Business.Caching;
using Bkav.eGovCloud.Core.ReadFile;
using Newtonsoft.Json;

#endregion

namespace Bkav.eGovCloud.Business.Customer
{
	/// <author>
	///   <para> BSO - Phòng 2 - eGov </para>
	///   <para> Project: eGov Cloud - v1.0 </para>
	///   <para> [Access Level(Class/Struct/Interface)] : DocumentCopyBll - public - BLL </para>
	///   <para> Access Modifiers: * Inherit : [Class Name] * Implement : [Inteface Name], [Inteface Name], ... </para>
	///   <para> Create Date : 121225 </para>
	///   <para> Author : TienBV </para>
	/// </author>
	/// <summary>
	///   <para>Quản lý bảng lưu vết. </para>
	/// </summary>
	/// <remarks>
	/// Tác động tới DocumentCopy trong các tình huống sau:
	/// - Tạo mới văn bản/hồ sơ + Bàn giao văn bản hồ sơ (DocumentCopyTypes)
	/// - Xác nhận xử lý
	/// - Loại bỏ văn bản
	/// - Lấy lại văn bản
	/// - Cập nhật lại ngày hẹn trả khi dừng xử lý???
	/// - Dừng xử lý
	/// - Kết thúc xử lý
	/// - Khi phân loại
	/// </remarks>
	public class DocumentCopyBll : ServiceBase
	{
        #region Readonly & Static Fields
        private const string GOV_SYNC_API_SEND_REPORT_NAME = "sndData";

		private const DocumentCopyTypes SpecialDocumentCopyType = DocumentCopyTypes.XinYKien | DocumentCopyTypes.ThongBao |
															DocumentCopyTypes.DuyetGiaHan;

		private const DocumentCopyTypes NormalDocumentCopyType = DocumentCopyTypes.XuLyChinh | DocumentCopyTypes.DongXuLy |
															DocumentCopyTypes.ChoKetQuaDungXuLy;

		private readonly AdminGeneralSettings _adminSettings;

		private readonly IRepository<DocRelation> _docRelationRepository;
		private readonly IRepository<DocumentCopy> _documentCopyRepository;
		private readonly IRepository<Supplementary> _supplementaryRepository;
		private readonly IRepository<DocumentContent> _documentContentRepository;
		private readonly IRepository<Approver> _approverRepository;
		private readonly IRepository<Attachment> _attachmentRepository;
        private readonly IRepository<Document> _documentRepository;
        private readonly IRepository<DocType> _docTypeRepository;
        private readonly IRepository<ReportModes> _reportModesRespository;

		private readonly DocTimelineBll _docTimelineService;
		private readonly UserBll _userService;
		private readonly DepartmentBll _departmentService;
		private readonly CommentBll _commentService;
		private readonly WorkflowHelper _workflowHelper;
		private readonly CodeBll _codeService;
		private readonly DocumentBll _documentService;
		private readonly WorkflowBll _workflowService;
		private readonly DocTypeBll _doctypeService;
		private readonly StoreBll _storeService;
		private readonly CategoryBll _categoryService;
		private readonly DailyProcessBll _dailyProcessService;
		private readonly DocumentPublishBll _docPublishService;
		private readonly AttachmentBll _attachmentService;
		private readonly DocFinishBll _docFinishService;

		private readonly DocumentsCache _cache;

		private readonly WorktimeHelper _worktimeHelper;
		private readonly DocumentPermissionHelper _documentPermissionHelper;

		#endregion

		#region C'tors

		/// <summary>
		///   Khởi tạo class <see cref="DocumentCopyBll" />.
		/// </summary>
		/// <param name="context">Context</param>
		/// <param name="docTimelineService"> Bll tương ứng với bảng DocTimeline trong CSDL </param>
		/// <param name="userService"> </param>
		/// <param name="commentService"> Bll tương ứng với nghiệp vụ xử lý lịch sử ý kiến</param>
		/// <param name="workflowHelper"> </param>
		/// <param name="codeService"> </param>
		/// <param name="worktimeHelper"> </param>
		/// <param name="documentService"></param>
		/// <param name="workflowService"></param>
		/// <param name="deptService"></param>
		/// <param name="doctypeService"></param>
		/// <param name="storeService"></param>
		/// <param name="categoryService"></param>
		/// <param name="dailyProcess"></param>
		/// <param name="docPublishService"></param>
		/// <param name="cacheManager"></param>
		/// <param name="adminSettings"></param>
		/// <param name="attachmentService"></param>
		/// <param name="docFinishService"></param>
		/// <param name="documentPermissionHelper"></param>
		public DocumentCopyBll(
			IDbCustomerContext context,
			DocTimelineBll docTimelineService,
			UserBll userService,
			DepartmentBll deptService,
			CommentBll commentService,
			WorkflowHelper workflowHelper,
			CodeBll codeService,
			WorktimeHelper worktimeHelper,
			DocumentBll documentService,
			WorkflowBll workflowService,
			DocTypeBll doctypeService,
			StoreBll storeService,
			CategoryBll categoryService,
			DailyProcessBll dailyProcess,
			DocumentPublishBll docPublishService,
			DocumentsCache cacheManager, DocFinishBll docFinishService,
			AdminGeneralSettings adminSettings, AttachmentBll attachmentService,
			DocumentPermissionHelper documentPermissionHelper)
			: base(context)
		{
			_documentCopyRepository = Context.GetRepository<DocumentCopy>();
			_docRelationRepository = Context.GetRepository<DocRelation>();
			_supplementaryRepository = Context.GetRepository<Supplementary>();
			_documentContentRepository = Context.GetRepository<DocumentContent>();
			_approverRepository = Context.GetRepository<Approver>();
			_attachmentRepository = Context.GetRepository<Attachment>();

			_docTimelineService = docTimelineService;
			_userService = userService;
			_commentService = commentService;
			_workflowHelper = workflowHelper;
			_codeService = codeService;
			_worktimeHelper = worktimeHelper;
			_documentService = documentService;
			_workflowService = workflowService;
			_doctypeService = doctypeService;
			_storeService = storeService;
			_dailyProcessService = dailyProcess;
			_cache = cacheManager;
			_docPublishService = docPublishService;
			_adminSettings = adminSettings;
			_categoryService = categoryService;
			_departmentService = deptService;
			_documentPermissionHelper = documentPermissionHelper;
			_attachmentService = attachmentService;
			_docFinishService = docFinishService;
		}

		#endregion

		#region Select

		/// <summary>
		///   Lấy ra một bản sao văn bản
		/// </summary>
		/// <param name="documentCopyId"> Id của bản sao văn bản </param>
		/// <returns> Entity bản sao văn bản </returns>
		public DocumentCopy Get(int documentCopyId)
		{
			if (documentCopyId <= 0)
			{
				return null;
			}

			return _documentCopyRepository.Get(documentCopyId);
		}

		/// <summary>
		/// Trả về thông tin chi tiết một văn bản, kết quả có lưu cache
		/// </summary>
		/// <param name="documentCopyId"></param>
		/// <param name="userOpenningId"></param>
		/// <returns></returns>
		/// <remarks>
		/// Lưu ý: hàm này chỉ sử dụng khi mở văn bản, tất cả những trường hợp sử dụng khác cần trao đổi lại với TienBV.
		/// </remarks>
		public DocumentCached GetFromCache(int documentCopyId, int userOpenningId)
		{
			var result = _cache.Get(documentCopyId, () =>
			{
				return GetDocumentCache(documentCopyId, userOpenningId);
			});

			return result;
		}

		/// <summary>
		///  Trả về thông tin chi tiết nhiều văn bản, kết quả có lưu cache
		/// </summary>
		/// <param name="documentCopyIds"></param>
		/// <param name="userOpenningId"></param>
		/// <returns></returns>
		public IEnumerable<DocumentCached> GetsFromCache(IEnumerable<int> documentCopyIds, int userOpenningId)
		{
			var result = documentCopyIds.Select(id => GetFromCache(id, userOpenningId));
			return result;
		}

		/// <summary>
		/// Remove cache documentcopy
		/// </summary>
		/// <param name="documentCopyId"></param>
		public void ClearCache(int documentCopyId)
		{
			RemoveCache(documentCopyId);
		}

		/// <summary>
		/// <para>Lấy danh sách các hồ sơ, văn bản liên quan của một hồ sơ, văn bản mà người sử dụng hiện tại được phép xem.</para>
		/// <para>TienBV@bkav.com - 061212</para>
		/// </summary>
		/// <param name="documentCopyId">The doc id.</param>
		/// <param name="userId">User Id</param>
		/// <returns></returns>
		public IEnumerable<DocRelation> GetDocRelations(int documentCopyId, int userId)
		{
			var result = new List<DocRelation>();
			var relations = _docRelationRepository.GetsReadOnly(d => d.DocumentCopyId == documentCopyId);
			if (!relations.Any())
			{
				return result;
			}

			if (!_adminSettings.HasCheckViewDocumentPermission)
			{
				return relations;
			}

			var relationCopyIds = relations.Select(r => r.RelationCopyId).ToList();
			var documentCopyRelations = _documentCopyRepository.GetsReadOnly(dc => relationCopyIds.Contains(dc.DocumentCopyId));

			foreach (var relation in relations)
			{
				var docCopy = documentCopyRelations.SingleOrDefault(dc => dc.DocumentCopyId == relation.RelationCopyId);
				if (docCopy != null && docCopy.HasQuyenXem(userId))
				{
					result.Add(relation);
				}
			}

			return result;
		}

		/// <summary>
		///   Lấy ra một bản sao văn bản
		/// </summary>
		/// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
		/// <param name="documentCopyId"> Id của bản sao văn bản </param>
		/// <returns> Entity bản sao văn bản </returns>
		public T GetAs<T>(Expression<Func<DocumentCopy, T>> projector, int documentCopyId)
		{
			return _documentCopyRepository.GetAs(projector, d => d.DocumentCopyId == documentCopyId);
		}

        /// <summary>
		///   Lấy ra docCopy theo docId
		/// </summary>
		/// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
		/// <param name="documentCopyId"> Id của bản sao văn bản </param>
		/// <returns> Entity bản sao văn bản </returns>
		public T GetAsDocId<T>(Expression<Func<DocumentCopy, T>> projector, Guid docID)
        {
            return _documentCopyRepository.GetAs(projector, d => d.DocumentId == docID );
        }

        /// <summary>
        ///   Trả về các hướng Chờ kết quả dừng xử lý được được gửi từ hướng hiện tại. Kết quả chỉ để đọc
        /// </summary>
        /// <param name="parentId">DocumenCopyId của hướng hiện tại</param>
        /// <param name="documentCopyTypes">Loại DocumentCopy cần trả về</param>
        /// <returns> </returns>
        public IEnumerable<DocumentCopy> GetChildren(int parentId, DocumentCopyTypes documentCopyTypes)
		{
			var spec = DocumentCopyQuery.IsChildWithType(parentId, documentCopyTypes);
			return _documentCopyRepository.GetsReadOnly(spec);
		}

		/// <summary>
		///   Lấy văn bản copy chính của 1 văn bản/hồ sơ
		/// </summary>
		/// <param name="documentId"> Id văn bản/hồ sơ </param>
		public DocumentCopy GetMain(Guid documentId)
		{
			return _documentCopyRepository.Get(false, dc =>
						(dc.DocumentCopyType == (int)DocumentCopyTypes.XuLyChinh || dc.DocumentCopyType == (int)DocumentCopyTypes.ChoKetQuaDungXuLy) && dc.DocumentId.Equals(documentId));
		}

		/// <summary>
		///   Trả về danh sách các document copy theo id
		/// </summary>
		/// <param name="documentCopyIds"> </param>
		/// <param name="isIncludeDocument">Có lấy thêm dữ liệu về document</param>
		/// <returns> </returns>
		public IEnumerable<DocumentCopy> Gets(List<int> documentCopyIds, bool isIncludeDocument = false)
		{
			return isIncludeDocument
				? _documentCopyRepository.Gets(false, d => documentCopyIds.Contains(d.DocumentCopyId),
					Context.Filters.Include<DocumentCopy>("Document"))
				: _documentCopyRepository.Gets(false, d => documentCopyIds.Contains(d.DocumentCopyId));
		}

		/// <summary>
		/// Trả về tất cả documentid mà user có quyền xem
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public IEnumerable<DocumentCopy> GetsByUser(int userId)
		{
			var userStr = DocumentCopy.UserCompareString(userId);
			return _documentCopyRepository.GetsReadOnly(d => d.UserNguoiThamGia.Contains(userStr) || d.UserThongBao.Contains(userStr));
		}

		/// <summary>
		///   Lấy các documentCopy theo điều  kiện kỹ thuật truyền vào. Kết quả chỉ để đọc
		/// </summary>
		/// <param name="spec"> </param>
		/// <returns> </returns>
		public IEnumerable<DocumentCopy> Gets(Expression<Func<DocumentCopy, bool>> spec = null)
		{
			return _documentCopyRepository.Gets(false, spec);
		}

		/// <summary>
		///   <para> Lấy ra tất cả các loại hồ sơ. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp.. </para>
		///   <para> (GiangPN@bkav.com - 07022013) </para>
		/// </summary>
		/// <param name="projector"> Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table) </param>
		/// <param name="spec"> Điều kiện </param>
		/// <typeparam name="TOutput"> Kiểu đầu ra. </typeparam>
		/// <returns> Danh sách các thực thể được ánh xạ </returns>
		public IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<DocumentCopy, TOutput>> projector,
													Expression<Func<DocumentCopy, bool>> spec = null)
		{
			return _documentCopyRepository.GetsAs(projector, spec);
		}

		/// <summary>
		///  Trả về User xử lý chính
		/// </summary>
		/// <param name="documentId"> </param>
		/// <returns> </returns>
		public User GetCurrentUser(Guid? documentId)
		{
			// Todo: trao đổi lại với TienBV trước khi uncomment
			var user = new User();
			//if (documentId.HasValue)
			//{
			//    var mainDoc = _documentCopyRepository.GetReadOnly(p => p.DocumentId == documentId
			//    && p.DocumentCopyType == (int)DocumentCopyTypes.XuLyChinh);
			//    if (mainDoc != null)
			//    {
			//        user = _userService.Get(mainDoc.UserCurrentId);
			//    }
			//}

			return user;
		}

		/// <summary>
		/// Lấy ra tổng số văn bản đang chờ xử lí của 1 user
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public int GetTotalDocumentCurrentUser(int userId)
		{
			// Todo: Cần trao đổi lại với TienBV trước khi uncomment hàm này
			// return _documentCopyRepository.Gets(false, dc => dc.UserCurrentId == userId && dc.Status == (int)DocumentStatus.DangXuLy).Count();

			return 0;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="documentId"></param>
		/// <returns></returns>
		public int GetDocCopyIdByCurrentUser(Guid documentId)
		{
			// Todo: Cần trao đổi lại với TienBV trước khi uncomment hàm này

			//var user = _userService.CurrentUser;
			//var documentCopy = _documentCopyRepository.Get(true, x => x.DocumentId == documentId && x.UserCurrentId == user.UserId);
			//if (documentCopy != null)
			//{
			//    return documentCopy.DocumentCopyId;
			//}

			return 0;
		}

        public SendReport GetDataReportFromDocumentCopyId(int documentCopyId, int userId, string organizationCode = "")
        {
            var documentCopy = GetFromCache(documentCopyId, userId);
            var sendReport = new SendReport();
            sendReport.func = GOV_SYNC_API_SEND_REPORT_NAME;
            sendReport.access_token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJnaWFsYWlAMTIzIiwibmFtZSI6ImdpYWxhaS5zeW5jIiwiaWF0IjoxNTE2MjM5MDE4LCJ0ZW5hbnRfaWQiOjEwfQ.YlXA0ZK3pTlAAkgXEZNqEq8sm7mUW6kEiME-Q1IcZr4";
            // sendReport.msg_fr = documentCopy.Organization;
            // sendReport.msg_to = "000.00.00.G22"; // TBD
            // sendReport.period = documentCopy.TimeKey;
            // sendReport.user_name = "admin";
            // sendReport.pass_word = "9b050a0a0b3d45fed8b1f2f3c05b0754";
            // sendReport.rpt_code = documentCopy.DocTypeCode;

            var rpt_data = GetReportData(documentCopy, organizationCode);
            sendReport.data = rpt_data;// Base64Encode(rpt_data);

            return sendReport;
            //return JsonConvert.SerializeObject(sendReport);
        }

        #endregion

        #region Create

        /// <summary>
        ///   <para>Tạo hướng chuyển bàn giao theo quy trình: Xử lý chính, Đồng xử lý, Chờ kết quả Dừng xử lý</para>
        ///   <para>CuongNT@bkav.com - 180813</para>
        /// </summary>
        /// <param name="documentId"> </param>
        /// <param name="docTypeId"> </param>
        /// <param name="nodeSend"> </param>
        /// <param name="userSendId"> </param>
        /// <param name="nodeReceive"> </param>
        /// <param name="userReceiveId"> </param>
        /// <param name="parentId"> </param>
        /// <param name="dateCreated"> </param>
        /// <param name="documentCopyType"> </param>
        /// <param name="documentStatus"> </param>
        /// <param name="docRelations">Danh sách các văn bản liên quan cán bộ nhận (userReceiveId) được phép xem</param>
        /// <param name="dateOverdue"></param>
        /// <returns> </returns>
        /// <remarks>
        /// Phan biet 2 truong hop:
        /// 1. Chi gui cho duy nhat chinh minh (Tiep nhan, tiep nhan va tiep tuc): Xu ly binh thuong
        /// VS
        /// 2. Gui cho nhieu nguoi (co the co ca chinh minh trong do): Xu ly nhu gui cho duy nhat chinh minh truoc, roi xu ly ban giao bt sau.
        /// </remarks>
        public DocumentCopy Create(Guid documentId, Guid docTypeId, Node nodeSend, int userSendId, Node nodeReceive,
						  int userReceiveId, int? parentId, DateTime dateCreated, DocumentCopyTypes documentCopyType,
                          DocumentStatus documentStatus, List<DocRelation> docRelations, DateTime? dateOverdue = null, bool userDonViNhan = false)
		{
			if (!NormalDocumentCopyType.HasFlag(documentCopyType))
			{
				throw new ArgumentException("documentCopyType chỉ được phép thuộc một trong các kiểu DocumentCopyTypes.XuLyChinh | DocumentCopyTypes.DongXuLy | DocumentCopyTypes.ChoKetQuaDungXuLy");
			}

			if (nodeSend == null)
			{
				throw new ArgumentNullException("nodeSend");
			}

			if (nodeReceive == null)
			{
				throw new ArgumentNullException("nodeReceive");
			}

			if (documentCopyType != DocumentCopyTypes.XuLyChinh && !parentId.HasValue)
			{
				throw new ArgumentNullException("parentId");
			}

			DocumentCopy documentCopyParent = null;
			if (parentId.HasValue)
			{
				documentCopyParent = Get(parentId.Value);
			}

			#region Xử lý HistoryPath

			var historyProcess = new HistoryProcess();
			/*
             * Nếu là Khi tạo mới văn bản (hay tạo mới hướng văn bản xử lý chính) thì mặc định tạo một history là bàn giao cho chính mình,
             * phục vụ việc lấy lại và sẽ chưa thông tin List<UserReceives>() đang bàn giao hiện tại.
             */
			if (documentCopyType == DocumentCopyTypes.XuLyChinh)
			{
				historyProcess.HistoryPath.Add(new HistoryPath
				{
					ParentId = parentId,
					DateCreated = dateCreated,//.AddSeconds(-5) De khong trung voi thoi gian ban giao thuc te, gay history co 2 moc giong het nhau
					UserReceiveId = userSendId,
					UserSendId = userSendId,
					NodeReceiveId = nodeSend.Id,
					NodeSendId = nodeSend.Id,
					UserReceives = new List<UserReceives>(),
					WorkflowReceiveId = nodeSend.Parent.Id,
					WorkflowSendId = nodeSend.Parent.Id
				});
			}
			// History tiếp theo lưu thông tin người vừa nhận văn bản như bình thường
			historyProcess.HistoryPath.Add(new HistoryPath
			{
				ParentId = parentId,
				DateCreated = dateCreated,
				UserReceiveId = userReceiveId,
				UserSendId = userSendId,
				NodeReceiveId = nodeReceive.Id,
				NodeSendId = nodeSend.Id,
				UserReceives = new List<UserReceives>(),
				WorkflowReceiveId = nodeReceive.Parent.Id,
				WorkflowSendId = nodeSend.Parent.Id
			});

			#endregion

			#region Tao moi DocumentCopy

			if (!dateOverdue.HasValue && nodeReceive.TimeInNode > 0)
			{
				var docType = _doctypeService.GetAllFromCache().SingleOrDefault(dt => dt.DocTypeId.Equals(docTypeId));
				if (docType != null && docType.HasOverdueInNode)
				{
					dateOverdue = _worktimeHelper.GetDateAppoint(dateCreated, nodeReceive.TimeInNode / 24);
				}
			}

			var userCurrent = _userService.GetFromCache(userReceiveId);
			var currentDept = _departmentService.GetPrimaryDepartmentName(userReceiveId);
            var listId = new List<int>();
            if (documentCopyParent != null)
                listId.AddRange(new[] { documentCopyParent.UserCurrentId, documentCopyParent.Document.UserCreatedId });
			var documentCopy = new DocumentCopy
			{
				ParentId = parentId,
				DocumentId = documentId,
				DocTypeId = docTypeId,
				WorkflowId = nodeReceive.Parent.Id,
				UserCurrentId = userReceiveId,
				UserCurrentName = userCurrent == null ? "" : userCurrent.FullName,
				CurrentDepartmentName = currentDept,
				Status = (int)documentStatus,
				History = historyProcess.StringifyJs(),
				DateCreated = dateCreated,
				DateReceived = dateCreated,
				DocumentCopyType = (int)documentCopyType,
				NodeCurrentId = nodeReceive.Id,
				NodeCurrentName = nodeReceive.NodeName,
				HasJustCreated = true,
				DateOverdue = dateOverdue,
				NodeCurrentPermission = (int)nodeReceive.GetNodePermission(),
				UserNguoiThamGia = DocumentCopy.UserCompareString(userReceiveId),
                UserGiamSat = userDonViNhan ? DocumentCopy.UserCompareString(listId) : null,
                ProcessInfoPlus = userDonViNhan ? documentCopyParent?.ProcessInfoPlus : null,
				DocumentUsers = DocumentCopy.UserCompareString(userReceiveId),
				DocumentCopyParentPath = documentCopyParent != null
						? string.Format("{0}{1}\\", documentCopyParent.DocumentCopyParentPath, documentCopyParent.DocumentCopyId)
						: ""
			};

			_documentCopyRepository.Create(documentCopy);

			#endregion

			#region Xử lý ghi danh sách các hướng nhận bàn giao

			var userReceive = new UserReceives
			{
				DocumentCopyId = documentCopy.DocumentCopyId,
				DocumentCopyType = (int)documentCopyType,
				WorkflowId = nodeReceive.Parent.Id,
				IsXlc = documentCopyType == DocumentCopyTypes.XuLyChinh,
				UserReceiveId = userReceiveId,
				DateCreated = dateCreated
			};

			var documentCopyUpdate = documentCopyType == DocumentCopyTypes.XuLyChinh ? documentCopy : documentCopyParent;
			var histories = documentCopyUpdate.Histories;
			if (histories.HistoryPath.Count > 1)
			{
				histories.HistoryPath[histories.HistoryPath.Count - 2].UserReceives.Add(userReceive);
			}
			documentCopyUpdate.Histories = histories;

			#endregion

			#region DocFinish

			// Context.Configuration.AutoDetectChangesEnabled = false;

			var userThamGia = new List<int>();
			userThamGia.Add(userReceiveId);
			if (documentCopyType == DocumentCopyTypes.XuLyChinh)
			{
				userThamGia.Add(userSendId);
			}

			UpdateUserThamGia(documentCopy, userThamGia);

			#endregion

			#region DocTimeline

			if (documentCopyType == DocumentCopyTypes.XuLyChinh)
			{
				if (userSendId != userReceiveId)
				{
					// Nếu người nhận khác người gửi thì tự tạo 1 record tự chuyển cho chính mình
					_docTimelineService.Create(new DocTimeline
					{
						DocumentId = documentId,
						DocumentCopyType = documentCopy.DocumentCopyType,
						DocumentCopyId = documentCopy.DocumentCopyId,
						FromDate = dateCreated,
						IsWorkingTime = true,
						NodeId = nodeSend.Id,
						NodeName = nodeSend.NodeName,
						UserId = userSendId,
						ToDate = dateCreated,
						ProcessedMinutes = 0,
						IsSuccess = true,
						UserSendId = userSendId,
						NodeSendId = nodeSend.Id,
						NodeSendName = nodeSend.NodeName,
						TimeInNode = nodeSend.TimeInNode,
						WorkFlowId = nodeSend.Parent.Id,
						DateOverdue = dateOverdue
					}, false);
				}
			}

			// Tạo doctimeline hướng người nhận
			_docTimelineService.Create(new DocTimeline
			{
				DocumentId = documentId,
				DocumentCopyType = documentCopy.DocumentCopyType,
				DocumentCopyId = documentCopy.DocumentCopyId,
				FromDate = dateCreated,
				IsWorkingTime = !nodeReceive.StopProcess,
				NodeId = nodeReceive.Id,
				NodeName = nodeReceive.NodeName,
				UserId = userReceiveId,
				UserSendId = userSendId,
				NodeSendId = nodeSend.Id,
				NodeSendName = nodeSend.NodeName,
				TimeInNode = nodeReceive.TimeInNode,
				WorkFlowId = nodeReceive.Parent.Id,
				DateOverdue = dateOverdue
			}, false);

			#endregion

			#region Xử lý quyen xem văn bản liên quan

			// Todo: xem lại chổ liên quan này
			if (docRelations != null && docRelations.Any())
			{
				var userXem = new List<int>() { userReceiveId };

				if (documentCopyType == DocumentCopyTypes.XuLyChinh)
				{
					userXem.Add(userSendId);
				}

				UpdateRelationUserJoineds(docRelations, userXem, hasSaveChange: false);

				foreach (var docRelation in docRelations)
				{
					docRelation.DocumentCopyId = documentCopy.DocumentCopyId;
					_docRelationRepository.Create(docRelation);
				}
			}

			#endregion

			Context.SaveChanges();

			return documentCopy;
		}
		
		/// <summary>
		///   <para>Tạo hướng chuyển bàn giao không theo quy trình: Xin ý kiến, Thông báo, Chờ gia hạn</para>
		///   <para>CuongNT@bkav.com - 180813</para>
		/// </summary>
		/// <param name="parentDocumentCopy"> </param>
		/// <param name="nodeSend">Co the null, neu van ban hien tai la van ban thong bao, xin y kien, duyet gia han</param>
		/// <param name="userSendId"> </param>
		/// <param name="userReceiveId"> </param>
		/// <param name="dateCreated"> </param>
		/// <param name="documentCopyType"> </param>
		/// <param name="status"> </param>
		/// <param name="docRelations">Danh sách các văn bản liên quan cán bộ nhận (userReceiveId) được phép xem</param>
		/// <param name="isTransfering"><c>True</c> neu la dang ban giao. <c>False</c> neu la van ban van thuoc nguoi dang giu.</param>
		/// <param name="lastcomment">Ý kiến xử lý cuối cùng</param>
		/// <param name="userSendFullName">Tên người gửi ý kiến cuối cùng</param>
		/// <returns> </returns>
		public DocumentCopy CreateSpecial(DocumentCopy parentDocumentCopy, Node nodeSend, int userSendId, int userReceiveId,
								 DateTime dateCreated, DocumentCopyTypes documentCopyType,
								 DocumentStatus status, List<DocRelation> docRelations, bool isTransfering, string lastcomment, string userSendFullName)
		{
			if (!SpecialDocumentCopyType.HasFlag(documentCopyType))
			{
				throw new ArgumentException("documentCopyType chỉ được phép thuộc một trong các kiểu DocumentCopyTypes.XinYKien | DocumentCopyTypes.ThongBao | DocumentCopyTypes.DuyetGiaHan");
			}

			#region Xu ly HistoryPath

			var historyProcess = new HistoryProcess();
			historyProcess.HistoryPath.Add(
					new HistoryPath
					{
						ParentId = parentDocumentCopy.DocumentCopyId,
						DateCreated = dateCreated,
						UserReceiveId = userReceiveId,
						UserSendId = userSendId,
						NodeReceiveId = 0,
						NodeSendId = nodeSend == null ? 0 : nodeSend.Id,
						UserReceives = new List<UserReceives>(),
						WorkflowReceiveId = 0,
						WorkflowSendId = nodeSend == null ? 0 : nodeSend.Parent.Id
					});

			#endregion

			#region Tao moi DocumentCopy

			var userReceive = _userService.GetFromCache(userReceiveId);
			var currentDept = _departmentService.GetPrimaryDepartmentName(userReceiveId);

			var documentCopy = new DocumentCopy
			{
				ParentId = parentDocumentCopy.DocumentCopyId,
				DocumentId = parentDocumentCopy.DocumentId,
				DocTypeId = parentDocumentCopy.DocTypeId,
				WorkflowId = nodeSend == null ? 0 : nodeSend.Parent.Id,
				UserCurrentId = userReceiveId,
				UserCurrentName = userReceive == null ? "" : userReceive.FullName,
				CurrentDepartmentName = currentDept,
				Status = (int)status,
				History = historyProcess.StringifyJs(),
				DateCreated = dateCreated,
				DateReceived = dateCreated,
				DocumentCopyType = (int)documentCopyType,
				NodeCurrentId = null,
				NodeCurrentName = string.Empty,
				NodeCurrentPermission = null,
				LastDateComment = dateCreated,
				LastComment = lastcomment,
				LastUserIdComment = userSendId,
				LastUserComment = userSendFullName,
				DocumentCopyParentPath = string.Format("{0}{1}\\", parentDocumentCopy.DocumentCopyParentPath, parentDocumentCopy.DocumentCopyId)
			};
			_documentCopyRepository.Create(documentCopy);
			Context.SaveChanges();
			var newDocumentCopyId = documentCopy.DocumentCopyId;

			#endregion

			#region Xử lý ghi danh sách các hướng nhận bàn giao

			var histories = parentDocumentCopy.Histories;
			switch (documentCopyType)
			{
				case DocumentCopyTypes.XinYKien:
					{
						var newUserReceive = new UserReceiveXinykiens
						{
							DateCreated = dateCreated,
							DocumentCopyType = (int)documentCopyType,
							DocumentCopyId = newDocumentCopyId,
							UserReceiveId = userReceiveId
						};
						var historyXinykien = histories.HistoryXinykien.GetOne(userSendId, dateCreated);
						if (historyXinykien == null)
						{
							if (nodeSend == null)
							{
								throw new ArgumentNullException("nodeSend", "nodeSend không được phép null khi xin y kien.");
							}
							histories.HistoryXinykien.Add(new HistoryXinykien
							{
								DateCreated = dateCreated,
								NodeSendId = nodeSend.Id,
								ParentId = parentDocumentCopy.DocumentCopyId,
								UserSendId = userSendId,
								WorkflowSendId = nodeSend.Parent.Id,
								UserReceives = new List<UserReceiveXinykiens> { newUserReceive }
							});
						}
						else
						{
							historyXinykien.UserReceives.Add(newUserReceive);
						}
					}
					break;

				case DocumentCopyTypes.ThongBao:
					{
						var newUserReceive = new UserReceiveThongbaos
						{
							DateCreated = dateCreated,
							DocumentCopyType = (int)documentCopyType,
							DocumentCopyId = documentCopy.DocumentCopyId,
							UserReceiveId = userReceiveId
						};

						var historyThongbao = histories.HistoryThongbao.GetOne(userSendId, dateCreated);
						if (historyThongbao == null)
						{
							histories.HistoryThongbao.Add(new HistoryThongbao
							{
								DateCreated = dateCreated,
								ParentId = parentDocumentCopy.DocumentCopyId,
								UserSendId = userSendId,
								UserReceives = new List<UserReceiveThongbaos> { newUserReceive }
							});
						}
						else
						{
							historyThongbao.UserReceives.Add(newUserReceive);
						}
					}
					break;
			}
			parentDocumentCopy.Histories = histories;
			Update(parentDocumentCopy);

			#endregion

			#region DocFinish

			UpdateUserThamGia(documentCopy, new List<int>() { userReceiveId });

			#endregion

			#region DocTimeline

			_docTimelineService.Create(new DocTimeline
			{
				DocumentId = documentCopy.DocumentId,
				DocumentCopyType = documentCopy.DocumentCopyType,
				DocumentCopyId = documentCopy.DocumentCopyId,
				FromDate = dateCreated,
				IsWorkingTime = false,
				UserId = userReceiveId
			}, true);

			#endregion

			#region Xử lý văn bản liên quan

			UpdateRelationUserJoineds(docRelations, new List<int>() { userReceiveId }, hasSaveChange: true);

			#endregion

			return documentCopy;
		}

		#endregion

		#region Instance Methods

		/// <summary>
		/// Thay đổi người xử lý chính khi xác nhận xử lý
		/// </summary>
		/// <param name="documentCopyXlc"></param>
		/// <param name="userXlcChangeId"> </param>
		public void ChangeUserXlc(DocumentCopy documentCopyXlc, int userXlcChangeId)
		{
			var deleteUserXlcId = documentCopyXlc.UserCurrentId;

			var changedUser = _userService.GetFromCache(userXlcChangeId);
			var changedDept = _departmentService.GetPrimaryDepartmentName(userXlcChangeId);

			documentCopyXlc.UserCurrentId = userXlcChangeId;
			documentCopyXlc.UserCurrentName = changedUser == null ? "" : changedUser.FullName;
			documentCopyXlc.CurrentDepartmentName = changedDept;

			// Cap nhat lai Historypath huong chinh
			var historyProcess = documentCopyXlc.Histories;
			var history1 = historyProcess.HistoryPath[historyProcess.HistoryPath.Count - 1];
			var history2 = historyProcess.HistoryPath[historyProcess.HistoryPath.Count - 2];
			history1.UserReceiveId = userXlcChangeId;
			history2.UserReceives.Single(c => c.DocumentCopyId == documentCopyXlc.DocumentCopyId).UserReceiveId = userXlcChangeId;
			documentCopyXlc.Histories = historyProcess;
			Update(documentCopyXlc);

			// DocFinish
			// Kiem tra trong lich su ban giao can bo giu van ban hien tai tung tham gia xu ly bao gio chua. Chua --> Xoa, Roi --> Giu lai
			var hasThamgiaxuly = false;
			var newHistoryProcess = documentCopyXlc.Histories;
			foreach (var history in newHistoryProcess.HistoryPath)
			{
				if (history.UserReceiveId != deleteUserXlcId && history.UserSendId != deleteUserXlcId)
				{
					continue;
				}
				hasThamgiaxuly = true;
				break;
			}

			var removedUserXuly = new List<int>();
			if (!hasThamgiaxuly)
			{
				removedUserXuly.Add(documentCopyXlc.UserCurrentId);
			}

			UpdateUserThamGia(documentCopyXlc, new List<int>() { userXlcChangeId }, removedUserXuly);
		}

		/// <summary>
		/// Cập nhật người tham gia
		/// </summary>
		/// <param name="documentCopy">Document Copy</param>
		/// <param name="addUserIds">Danh sách người dùng thêm vào</param>
		/// <param name="removedUserIds">Danh sách người dùng bỏ đi</param>
		/// <param name="hasUpdateDocUser">Có cập nhật danh sách user liên quan đến document hay không</param>
		/// <param name="hasSaveChange">Cập nhật</param>
		public void UpdateUserThamGia(DocumentCopy documentCopy, List<int> addUserIds,
					List<int> removedUserIds = null, bool hasUpdateDocUser = true, bool hasSaveChange = false)
		{
			if (documentCopy == null)
			{
				throw new ArgumentNullException("documentCopy");
			}

			var result = new List<int>();
			var currents = documentCopy.UserThamGias();

			if (removedUserIds != null && removedUserIds.Any())
			{
				result.AddRange(currents.Where(u => !removedUserIds.Contains(u)));
			}
			else
			{
				result = currents;
			}

			if (addUserIds.Any())
			{
				result.AddRange(addUserIds);
			}

			documentCopy.UserNguoiThamGia = DocumentCopy.UserCompareString(result.Distinct().ToList());

			if (hasUpdateDocUser)
			{
				UpdateUserDocuments(documentCopy, addUserIds, removedUserIds, hasSaveChange);
			}

			if (hasSaveChange)
			{
				Context.SaveChanges();
			}
		}

		/// <summary>
		/// Cập nhật người nhận thông báo
		/// </summary>
		/// <param name="documentCopy">Document Copy</param>
		/// <param name="addUserIds"></param>
		/// <param name="removedUserIds"></param>
		/// <param name="hasUpdateDocUser">Có cập nhật danh sách user liên quan đến document hay không</param>
		/// <param name="hasSaveChange">Cập nhật</param>
		public void UpdateUserThongBao(DocumentCopy documentCopy, List<int> addUserIds, List<int> removedUserIds = null,
											bool hasUpdateDocUser = true, bool hasSaveChange = false)
		{
			if (documentCopy == null)
			{
				throw new ArgumentNullException("documentCopy");
			}

			var result = new List<int>();
			var currents = documentCopy.UserThongBaos();

			if (removedUserIds != null && removedUserIds.Any())
			{
				result.AddRange(currents.Where(u => !removedUserIds.Contains(u)));
			}
			else
			{
				result = currents;
			}

			if (addUserIds != null && addUserIds.Any())
			{
				result.AddRange(addUserIds);
			}

			documentCopy.UserThongBao = DocumentCopy.UserCompareString(result.Distinct().ToList());

			if (hasUpdateDocUser)
			{
				UpdateUserDocuments(documentCopy, addUserIds, removedUserIds, hasSaveChange);
			}

			if (hasSaveChange)
			{
				Context.SaveChanges();
			}
		}

		/// <summary>
		/// Cập nhật người đã xem văn bản
		/// </summary>
		/// <param name="documentCopy"></param>
		/// <param name="addUserIds"></param>
		/// <param name="removedUserIds"></param>
		/// <param name="hasSaveChange"></param>
		public void UpdateUserDaXem(DocumentCopy documentCopy, List<int> addUserIds, List<int> removedUserIds = null, bool hasSaveChange = false)
		{
			if (documentCopy == null)
			{
				throw new ArgumentNullException("documentCopy");
			}

			var result = new List<int>();
			var currents = documentCopy.UserDaXems();

			if (removedUserIds != null && removedUserIds.Any())
			{
				result.AddRange(currents.Where(u => !removedUserIds.Contains(u)));
			}
			else
			{
				result = currents;
			}

			if (addUserIds != null && addUserIds.Any())
			{
				result.AddRange(addUserIds);
			}

			documentCopy.UserNguoiDaXem = DocumentCopy.UserCompareString(result.Distinct().ToList());

			if (hasSaveChange)
			{
				Context.SaveChanges();
			}
		}

		/// <summary>
		/// Cập nhật người giám sát
		/// </summary>
		/// <param name="documentCopy"></param>
		/// <param name="addUserIds"></param>
		/// <param name="removedUserIds"></param>
		public void UpdateUserUyQuyen(DocumentCopy documentCopy, List<int> addUserIds, List<int> removedUserIds = null)
		{
			if (documentCopy == null)
			{
				throw new ArgumentNullException("documentCopy");
			}

			var result = new List<int>();
			var currents = documentCopy.UserUyQuyens();

			if (removedUserIds != null && removedUserIds.Any())
			{
				result.AddRange(currents.Where(u => !removedUserIds.Contains(u)));
			}
			else
			{
				result = currents;
			}

			if (addUserIds.Any())
			{
				result.AddRange(addUserIds);
			}

			documentCopy.UserGiamSat = DocumentCopy.UserCompareString(result);

			Context.SaveChanges();
		}

		/// <summary>
		/// Cập nhật danh sách những người liên quan đến văn bản.
		/// </summary>
		/// <param name="documentCopy"></param>
		/// <param name="addUserIds"></param>
		/// <param name="removedUserIds"></param>
		/// <param name="hasSaveChange"></param>
		public void UpdateUserDocuments(DocumentCopy documentCopy, List<int> addUserIds, List<int> removedUserIds = null, bool hasSaveChange = false)
		{
			if (documentCopy == null)
			{
				throw new ArgumentNullException("documentCopy");
			}

			var result = new List<int>();
			var currents = documentCopy.DocumentUserList();

			if (removedUserIds != null && removedUserIds.Any())
			{
				result.AddRange(currents.Where(u => !removedUserIds.Contains(u)));
			}
			else
			{
				result = currents;
			}

			if (addUserIds != null && addUserIds.Any())
			{
				result.AddRange(addUserIds);
			}

			documentCopy.DocumentUsers = DocumentCopy.UserCompareString(result.Distinct().ToList());

			if (hasSaveChange)
			{
				Context.SaveChanges();
			}
		}

		/// <summary>
		/// Cập nhật quyền xem cho các văn bản liên quan.
		/// </summary>
		/// <param name="relations">Danh sách văn bản liên quan</param>
		/// <param name="users">Danh sách người dùng có quyền xem</param>
		/// <param name="removedUsers">Danh sách người dùng bỏ quyền xem</param>
		/// <param name="hasSaveChange"></param>
		public void UpdateRelationUserJoineds(List<DocRelation> relations, List<int> users, List<int> removedUsers = null, bool hasSaveChange = false)
		{
			if (relations == null || !relations.Any())
			{
				return;
			}
			var relationCopyIds = relations.Select(d => d.RelationCopyId);
			UpdateRelationUserJoineds(relationCopyIds, users);

			//var docCopyRelations = Gets(relationCopyIds;

			//foreach (var relationCopy in docCopyRelations)
			//{
			//	UpdateUserThongBao(relationCopy, users, null, true, hasSaveChange);
			//}
		}

		/// <summary>
		/// Cập nhật người tham gia vào văn bản liên quan
		/// </summary>
		/// <param name="relationCopyId"></param>
		/// <param name="userJoinIds"></param>
		public void UpdateRelationUserJoineds(IEnumerable<int> relationCopyId, List<int> userJoinIds)
		{
			if (!relationCopyId.Any() || userJoinIds == null || !userJoinIds.Any()) return;

			var cmd = "UPDATE documentcopy SET UserThongBao = CONCAT(UserThongBao, @newUsers), DocumentUsers = CONCAT(DocumentUsers, @newUsers) WHERE DocumentCopyId in (@documentCopyIds);";
			var parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@newUsers", DocumentCopy.UserCompareString(userJoinIds)));
			cmd = cmd.Replace("@documentCopyIds", string.Join(",", relationCopyId));

			Context.RawModify(cmd, parameters.ToArray());
		}


		/// <summary>
		///   <para>Xóa các bản sao văn bản của 1 hướng chính(xóa các hướng đồng xử lý). </para>
		///   <para>Thuc hien chuc nang nguoc voi ham Create: Tao gi thi xoa day.</para>
		/// </summary>
		/// <param name="documentCopy">1 bản sao văn bản vừa tạo ra và chưa chuyển đi tiếp</param>
		public void Delete(DocumentCopy documentCopy)
		{
			if (documentCopy == null || !documentCopy.ParentId.HasValue)
			{
				throw new ArgumentNullException("documentCopy");
			}

			if (documentCopy.Histories.HistoryPath.Count != 1)
			{
				throw new ArgumentException("Chỉ được phép xóa bản sao văn bản (DocumentCopy) vừa tạo ra và chưa bàn giao đi tiếp. Tức documentCopy.Histories.Count == 1", "documentCopy");
			}

			using (var transaction = new TransactionScope(TransactionScopeOption.Required))
			{
				// Xóa trong HistoryPath của các hướng chuyển liên quan
				var parentDocumentCopy = Get(documentCopy.ParentId.Value);

				// TODO: Viết thêm class helper: HistoryPathHelper để xử lý các cách ghi thông tin như bên dưới.
				var histories = parentDocumentCopy.Histories;
				if (documentCopy.DocumentCopyTypeInEnum == DocumentCopyTypes.ThongBao)
				{
					var newHistory = histories.HistoryThongbao.GetOne(documentCopy.DateCreated);
					var userReceiveRemove = newHistory.UserReceives.SingleOrDefault(c => c.DocumentCopyId == documentCopy.DocumentCopyId);
					if (userReceiveRemove == null)
					{
						throw new ApplicationException("Ghi HistoryPath khi chuyển văn bản chưa chính xác.");
					}
					newHistory.UserReceives.Remove(userReceiveRemove);
					if (!newHistory.UserReceives.Any())
					{
						histories.HistoryThongbao.Remove(newHistory);
					}
				}
				else if (documentCopy.DocumentCopyTypeInEnum == DocumentCopyTypes.XinYKien)
				{
					var newHistory = histories.HistoryXinykien.GetOne(documentCopy.DateCreated);
					var userReceiveRemove = newHistory.UserReceives.SingleOrDefault(c => c.DocumentCopyId == documentCopy.DocumentCopyId);
					if (userReceiveRemove == null)
					{
						throw new ApplicationException("Ghi HistoryPath khi chuyển văn bản chưa chính xác.");
					}
					newHistory.UserReceives.Remove(userReceiveRemove);
					if (!newHistory.UserReceives.Any())
					{
						histories.HistoryXinykien.Remove(newHistory);
					}
				}
				else if (documentCopy.DocumentCopyTypeInEnum == DocumentCopyTypes.XuLyChinh ||
							documentCopy.DocumentCopyTypeInEnum == DocumentCopyTypes.DongXuLy)
				{
					// Bàn giao thông thường
					var newHistory = histories.HistoryPath[histories.HistoryPath.Count - 2];
					var userReceive = newHistory.UserReceives.SingleOrDefault(c => c.DocumentCopyId == documentCopy.DocumentCopyId);
					if (userReceive == null)
					{
						throw new ApplicationException("Ghi HistoryPath khi chuyển văn bản chưa chính xác.");
					}
					newHistory.UserReceives.Remove(userReceive);
					histories.HistoryPath[histories.HistoryPath.Count - 2] = newHistory;
				}
				else
				{
					throw new ApplicationException("Văn bản không cho phép lấy lại");
				}

				parentDocumentCopy.Histories = histories;
				Update(parentDocumentCopy);

				// Xóa thông tin văn bản DocTimeline theo từng văn bản copy
				_docTimelineService.DeleteByDocumentCopy(documentCopy.DocumentCopyId);

				// Xóa documentCopy
				// Đổi việc xóa hẳn trong db sang set trạng thái -1 (trạng thái ko có xử lý).
				// Do xóa hẳn trong db không update dc lên danh sách văn bản.
				documentCopy.Status = -1;
				Context.SaveChanges();
				// _documentCopyRepository.Delete(documentCopy);

				transaction.Complete();
			}
		}

		/// <summary>
		///   Kết thúc hướng xử lý. Đảm bảo chuyển ý kiến về hướng xử lý cha trước khi kết thúc.
		/// </summary>
		/// <param name="documentCopy">Bản sao văn bản cần kết thúc</param>
		/// <param name="dateFinished">Thời điểm kết kết</param>
		/// <param name="userFinishId"> </param>
		/// <param name="commentLog"></param>
		public void Finish(DocumentCopy documentCopy, DateTime dateFinished, int userFinishId, string commentLog = null)
		{
			if (documentCopy.DocumentCopyTypeInEnum == DocumentCopyTypes.XuLyChinh || documentCopy.DocumentCopyTypeInEnum == DocumentCopyTypes.ChoKetQuaDungXuLy)
			{
				// Nếu hướng xử lý là hướng chính thì kết thúc luôn hồ sơ.
				documentCopy.Document.Status = (int)DocumentStatus.KetThuc;
				documentCopy.Document.DateFinished = dateFinished;
				//documentCopy.Document.Note = commentLog;
				documentCopy.Document.DateModified = DateTime.Now;
				documentCopy.DocumentCopyType = (int)DocumentCopyTypes.XuLyChinh;
			}

			if (documentCopy.DocumentCopyTypeInEnum == DocumentCopyTypes.DongXuLy
				|| documentCopy.DocumentCopyTypeInEnum == DocumentCopyTypes.XinYKien)
			{
				// Kiểm tra điều kiện trả lời ý kiến đóng góp trước khi kết thúc
				if (!ExitCommentToParent(documentCopy))
				{
					var userFinish = _userService.GetFromCache(userFinishId);
					var content = "Văn bản này được kết thúc bởi " + userFinish.FullName + ". Thời gian kết thúc: " + dateFinished.ToString("dd/MM/yyyy HH:mm:ss");
					SendAnswerToParent(documentCopy, documentCopy.UserCurrentId, content, dateFinished);
				}
			}

			// Kết thúc hướng xử lý
			documentCopy.DateFinished = dateFinished;
			documentCopy.Status = (int)DocumentStatus.KetThuc;
			documentCopy.DateReceived = dateFinished;
			
			_commentService.SendCommentCommon(documentCopy, userFinishId, dateFinished, commentLog, CommentType.Finished);
			Context.SaveChanges();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="documentCopy"></param>
		public void UndoFinish(DocumentCopy documentCopy)
		{
			if (documentCopy.DocumentCopyType == (int)DocumentCopyTypes.XuLyChinh)
			{
				var doccument = _documentService.Get(documentCopy.DocumentId);
				doccument.Status = (int)DocumentStatus.DangXuLy;
				doccument.DateFinished = null;
			}

			var currentUser = documentCopy.UserCurrentId;
			var document = documentCopy.Document;

			// Kiểm tra điều kiện trả lời ý kiến đóng góp trước khi kết thúc
			if (ExitCommentToParent(documentCopy))
			{

				var comments = _commentService.Gets(documentCopy);
				if (comments != null && comments.Any())
				{
					var commentFinish = comments.Where(cm => cm.CommentType == (byte)CommentType.Finished && cm.DocumentCopyId == documentCopy.DocumentCopyId);
					if (documentCopy.DocumentCopyType == (int)DocumentCopyTypes.XinYKien || documentCopy.DocumentCopyType == (int)DocumentCopyTypes.DongXuLy)
					{
						// Nếu là văn bản đồng xử lý hoặc xin ý kiến:
						commentFinish = comments.Where(cm => (cm.CommentType == (byte)CommentType.Finished || cm.CommentType == (byte)CommentType.Consulted || cm.CommentType == (byte)CommentType.Contribution) && cm.DocumentCopyId == documentCopy.DocumentCopyId);
					}
					var commentIds = commentFinish.Select(cm => cm.CommentId);
					foreach (var commentId in commentIds)
					{
						var commentDelete = _commentService.Get(commentId);
						if (commentDelete != null)
						{
							_commentService.Delete(commentDelete);
						}
					}

				}
			}

			//
			var daily = _dailyProcessService.Gets(documentCopy.DocumentCopyId);

			_docPublishService.Deletes(documentCopy.DocumentCopyId);

			// Kết thúc hướng xử lý
			documentCopy.DateFinished = null;
			documentCopy.Status = (int)DocumentStatus.DangXuLy;

			// documentCopy.DateReceived = DateTime.Now;
			// _documentCopyRepository.Update(documentCopy);
			Context.SaveChanges();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="documentCopy"></param>
		/// <param name="addresses"></param>
		/// <param name="listAddress"></param>
		public void RePublish(DocumentCopy documentCopy, Dictionary<int, DateTime?> addresses, List<Bkav.eGovCloud.Entities.Customer.Address> listAddress)
		{
			var document = documentCopy.Document;
			if (document == null)
			{
				return;
			}

			var docCode = document.DocCode;
			var documentCopyId = documentCopy.DocumentCopyId;
			var documentId = documentCopy.DocumentId;
			var doctypeId = documentCopy.DocTypeId;
			var currentUser = documentCopy.UserCurrentId;
			var currentUserName = documentCopy.UserCurrentName;

			foreach (var address in addresses)
			{
				var addressObject = listAddress.SingleOrDefault(ad => ad.AddressId == address.Key);
				if (addressObject == null)
				{
					continue;
				}

				var docPublish = new DocPublish()
				{
					DocumentCopyId = documentCopyId,
					DocumentId = documentId,
					DoctypeId = doctypeId,
					DocCode = docCode,
					DatePublished = DateTime.Now,
					IsHsmc = document.IsHSMC,
					UserPublishId = currentUser,
					UserPublishName = currentUserName,
					AddressName = addressObject.Name,
					HasLienThong = !string.IsNullOrEmpty(addressObject.EdocId),
					IsPending = true,
					AddressId = address.Key,
					HasRequireResponse = address.Value.HasValue,
					AddressCode = addressObject.EdocId,
					DateAppointed = address.Value
				};

				_docPublishService.Create(docPublish);
			}

			Context.SaveChanges();
		}

		/// <summary>
		/// Loại bỏ một văn bản Xử lý chính
		/// </summary>
		/// <param name="documentCopy">Văn bản cần loại bỏ</param>
		/// <param name="userRemovedId">Id cán bộ đang thực hiện loại bỏ văn bản</param>
		/// <param name="dateRemoved"> </param>
		public void Remove(DocumentCopy documentCopy, int userRemovedId, DateTime dateRemoved)
		{
			// Chỉ có văn bản chính mới có quyền hủy
			if (documentCopy.DocumentCopyTypeInEnum != DocumentCopyTypes.XuLyChinh)
			{
				throw new ApplicationException("Chỉ có văn bản chính và văn bản thông báo mới có nghiệp vụ loại bỏ.");
			}

			//// Chỉ người khởi tạo văn bản mới được hủy văn bản
			//if (documentCopy.Document.UserCreatedId != userRemovedId)
			//{
			//    throw new ApplicationException("Chỉ người khởi tạo văn bản mới có quyền hủy văn bản.");
			//}

			// TODO: Config option cho hành động loại bỏ văn bản chính trong quản trị: hoặc loại bỏ luôn các văn bản sao, hoặc chỉ loại bỏ văn bản chính.
			/* Kết thúc tất cả các bản sao liên quan: dxl, tb, xyk, .... */
			//var childs = documentCopy.Document.DocumentCopys.Where(c => c.DocumentCopyTypeInEnum != DocumentCopyTypes.XuLyChinh).Select(o => o).ToList();

			//HOpCV:250914
			//Sửa lại chỗ lấy danh sách văn bản bản sao
			var childs = _documentCopyRepository.Gets(false, p => p.DocumentId == documentCopy.DocumentId
																&& p.DocumentCopyType != (int)DocumentCopyTypes.XuLyChinh);

			var userRenove = _userService.GetFromCache(userRemovedId);

			string contentFisnish = string.Format("{0} kết thúc văn bản vào lúc :{1}", userRenove.FullName, dateRemoved.ToString("dd/MM/yyyy HH:mm:ss"));
			foreach (var child in childs)
			{
				Finish(child, dateRemoved, userRemovedId, contentFisnish);
			}

			/* Loại bỏ văn bản/hồ sơ chính */
			documentCopy.Status = (int)DocumentStatus.LoaiBo;

			/* Loại bỏ văn bản/hồ sơ gốc */
			// Kiểm tra nếu là hồ sơ thì lấy lại mã hồ sơ đã được cấp(nếu có mã hồ sơ)
			if (documentCopy.Document.StoreId.HasValue)
			{
				var store = _storeService.Get(documentCopy.Document.StoreId.Value);
				if (store != null)
				{
					var storeCodes = store.StoreCodes;
					var codeId = storeCodes.First().CodeId;
					_codeService.ReuseFromDocument(documentCopy.Document);
				}
			}

			documentCopy.Document.DocCode = string.Empty;
			documentCopy.Document.DateOfIssueCode = null;
			documentCopy.Document.Status = (int)DocumentStatus.LoaiBo;
			Update(documentCopy);

			// TODO: Cần tạo một commentType riêng cho phần kết thúc xử lý
			var userRemove = _userService.GetFromCache(userRemovedId);
			var commentRemoved = string.Format("{0} đã loại bỏ văn bản. Thời gian loại bỏ: {1}", userRemove.FullName, dateRemoved);
			_commentService.SendComment(documentCopy, userRemovedId, commentRemoved, dateRemoved);

			RemoveCache(documentCopy.DocumentCopyId);
		}

		/// <summary>
		/// Thêm văn bản liên quan
		/// </summary>
		/// <param name="documentCopyId"></param>
		/// <param name="docRelationId"></param>
		/// <param name="documentId"></param>
		/// <param name="relation"></param>
		/// <param name="type"></param>
		public void AddDocRelations(int documentCopyId, int docRelationId, Guid documentId, Document relation, RelationTypes type)
		{
			if (_docRelationRepository.Exist(r => r.DocumentCopyId == documentCopyId && r.DocRelationId == docRelationId && r.RelationType == (int)type))
			{
				return;
			}

			var categoryName = "";
			if (relation.CategoryId.HasValue)
			{
				var category = _categoryService.GetFromCache(relation.CategoryId.Value);
				categoryName = category.CategoryName;
			}

			_docRelationRepository.Create(new DocRelation()
			{
				DocumentCopyId = documentCopyId,
				RelationCopyId = docRelationId,
				RelationType = (int)type,
				DocumentId = documentId,
				RelationId = relation.DocumentId,
				Compendium = relation.Compendium,
				DocCode = relation.DocCode,
				CategoryName = categoryName,
				CitizenName = relation.CitizenName,
				InOutCode = relation.InOutCode,
				DateArrived = DateTime.Now
			});
		}

		/// <summary>
		/// Gửi ý kiến cập nhật kết quả xử lý hồ sơ mc.
		/// </summary>
		/// <param name="documentCopy"></param>
		/// <param name="userUpdate"></param>
		/// <param name="dateCreate"></param>
		/// <param name="commentLog"></param>
		public void CommentUpdateResult(DocumentCopy documentCopy, int userUpdate, DateTime dateCreate, string commentLog)
		{
			_commentService.SendCommentCommon(documentCopy, userUpdate, dateCreate, commentLog, CommentType.Common);

			RemoveCache(documentCopy.DocumentCopyId);
		}

		/// <summary>
		///   Cập nhật thông tin bản sao văn bản
		/// </summary>
		/// <param name="documentCopy"> Entity bản sao văn bản </param>
		public void Update(DocumentCopy documentCopy)
		{
			if (documentCopy == null)
			{
				throw new ArgumentNullException("documentCopy");
			}
			Context.SaveChanges();
		}

		/// <summary>
		/// Cập nhật thời hạn xử lý trên node
		/// </summary>
		/// <param name="documentCopyTransfering">Văn bản cần cập nhật</param>
		/// <param name="nodeReceive">Node hiện tại</param>
		public void UpdateDateOverdue(DocumentCopy documentCopyTransfering, Node nodeReceive)
		{
			if (documentCopyTransfering == null)
			{
				throw new ArgumentNullException("documentCopyTransfering");
			}

			// Nếu node hiện tại là node đc cấu hình dừng xử lý, bỏ qua thời gian node này giữ công văn ở hạn tổng
			// TienBV: tạm thời không dùng đoạn này do việc tự động cộng thêm ngày không rõ ràng, ảnh hưởng đến những
			// Phiếu in cán bộ đã in trước đó.
			// => Khi có thay đổi hạn để người dùng tự cập nhật.
			// var stopProcessDays = 0;
			if (documentCopyTransfering.NodeCurrentId.HasValue)
			{
				//var count = documentCopyTransfering.Histories.HistoryPath.Count;
				//var preHistory = documentCopyTransfering.Histories.HistoryPath[count - 2];
				//var nodeSendId = preHistory.NodeReceiveId;
				//var nodeSend = _workflowHelper.GetNode(documentCopyTransfering.WorkflowId, nodeSendId);
				//if (nodeSend != null && nodeSend.StopProcess)
				//{
				//    stopProcessDays = (DateTime.Now - preHistory.DateCreated).Days;
				//}
			}

			var doc = _documentService.Get(documentCopyTransfering.DocumentId);

			if (doc.DateAppointed.HasValue)
			{
				//doc.DateAppointed = doc.DateAppointed.Value.AddDays(stopProcessDays);
				//_documentService.Update(doc);
			}

			// Todo: cần xem lại cách lưu thời giạn xử lý cho node trên quy trình dạng giờ
			DateTime? dateOverdue = null;
			var doctype = doc.DocTypeId.HasValue ? _doctypeService.GetFromCache(doc.DocTypeId.Value) : null;
			if (doctype != null && doctype.HasOverdueInNode && nodeReceive.TimeInNode > 0)
			{
				dateOverdue = _worktimeHelper.GetDateAppoint(DateTime.Now, nodeReceive.TimeInNode / 24);
			}

			// Nếu không đặt hạn giữ thì gán hạn tổng vào hạn giữ
			if (!dateOverdue.HasValue)
			{
				dateOverdue = doc.DateAppointed;
			}

			documentCopyTransfering.DateOverdue = dateOverdue;
		}

		/// <summary>
		/// Lấy tổng thời gian công văn ở node dừng xử lý
		/// </summary>
		/// <param name="doc">DocumentCopy</param>
		/// <param name="workflow">workflow</param>
		/// <returns></returns>
		public int GetStopProcessDays(DocumentCopy doc, Workflow workflow)
		{
			var histories = doc.Histories.HistoryPath;
			var stopProcessTime = 0;
			for (int i = 0; i < histories.Count; i++)
			{
				var pastNode = _workflowHelper.GetNode(workflow, (int)histories[i].NodeReceiveId);
				if (!pastNode.StopProcess)
				{
					continue;
				}

				if (i == histories.Count - 1)
				{
					stopProcessTime = stopProcessTime + (DateTime.Now - histories[i].DateCreated).Days;
					for (int j = 0; j < stopProcessTime; j++)
					{
						var date = histories[i].DateCreated.AddDays(j + 1);
						if (_worktimeHelper.IsWeekendOrHoliday(date))
						{
							stopProcessTime--;
						}
					}
				}
				else
				{
					stopProcessTime = stopProcessTime + (histories[i + 1].DateCreated - histories[i].DateCreated).Days;
					for (int j = 0; j < stopProcessTime; j++)
					{
						var date = histories[i].DateCreated.AddDays(j + 1);
						if (_worktimeHelper.IsWeekendOrHoliday(date))
						{
							stopProcessTime--;
						}
					}
				}
			}
			return stopProcessTime;
		}

		/// <summary>
		/// </summary>
		/// <param name="documentCopyUpdate"> </param>
		/// <param name="userReceiveId"> </param>
		/// <param name="userSendId"> </param>
		/// <param name="nodeReceive"> </param>
		/// <param name="nodeSend"> </param>
		/// <param name="dateCreated"> </param>
		/// <param name="docRelations">Danh sách các văn bản liên quan cán bộ nhận (userReceiveId) được phép xem</param>
		/// <param name="dateOverdue"></param>
		/// <param name="isConverted"></param>
		public void UpdateForTransfering(DocumentCopy documentCopyUpdate, Node nodeSend, int userSendId,
											Node nodeReceive, int userReceiveId, DateTime dateCreated, List<DocRelation> docRelations, DateTime? dateOverdue, bool isConverted)
		{
			if (nodeSend == null)
			{
				throw new ArgumentNullException("nodeSend");
			}

			if (nodeReceive == null)
			{
				throw new ArgumentNullException("nodeReceive");
			}

			#region DocTimeline

			if (string.IsNullOrEmpty(documentCopyUpdate.History) && isConverted)
			{
				documentCopyUpdate.Histories = new HistoryProcess()
				{
					HistoryPath = new List<HistoryPath>()
				};

				documentCopyUpdate.History = documentCopyUpdate.Histories.Stringify();
			}

			// Cập nhật thời gian ra khỏi nút
			if (documentCopyUpdate.Histories.HistoryPath.Any())
			{
				var dateOfTimeline = documentCopyUpdate.Histories.HistoryPath.Last().DateCreated;
				var docTimeline = _docTimelineService.Get(documentCopyUpdate.DocumentCopyId, userSendId, dateOfTimeline, nodeSend.Id);
				if (docTimeline == null)
				{
					//Todo: Xử lý null doctimeline ở đây
				}
				else
				{
					_docTimelineService.Update(docTimeline, dateCreated, false);
				}
			}

			// Tạo mới timeline cho người nhập hướng chính
			_docTimelineService.Create(new DocTimeline
			{
				DocumentId = documentCopyUpdate.DocumentId,
				DocumentCopyType = documentCopyUpdate.DocumentCopyType,
				DocumentCopyId = documentCopyUpdate.DocumentCopyId,
				FromDate = dateCreated,
				IsWorkingTime = !nodeReceive.StopProcess,
				NodeId = nodeReceive.Id,
				NodeName = nodeReceive.NodeName,
				UserId = userReceiveId,
				UserSendId = userSendId,
				NodeSendId = nodeSend.Id,
				NodeSendName = nodeSend.NodeName,
				TimeInNode = nodeSend.TimeInNode,
				WorkFlowId = nodeSend.Parent.Id,
				DateOverdue = dateOverdue
			}, false);

			#endregion

			#region Xu ly HistoryPath

			var histories = documentCopyUpdate.Histories;

			histories.HistoryPath.Add(new HistoryPath
			{
				DateCreated = dateCreated,
				ParentId = documentCopyUpdate.ParentId,
				UserReceiveId = userReceiveId,
				UserSendId = userSendId,
				NodeReceiveId = nodeReceive.Id,
				NodeSendId = nodeSend.Id,
				UserReceives = new List<UserReceives>(),
				WorkflowReceiveId = nodeReceive.Parent.Id,
				WorkflowSendId = nodeSend.Parent.Id
			});

			#endregion

			#region Xử lý ghi danh sách các hướng nhận bàn giao

			if (histories.HistoryPath.Count > 1)
			{
				// Chỉ đúng khi Hướng XLC được thực hiện trước, sau đó các hướng ĐXL mới ghi theo dạng -2.
				// Ngược lại, Hướng XLC thực hiện sau, thì các hướng ĐXL sẽ ghi theo dạng -1.
				// TODO: --> Các có cách xử lý chính xác hơn cách làm hiện tại này.
				var userReceive = new UserReceives
				{
					DocumentCopyId = documentCopyUpdate.DocumentCopyId,
					DocumentCopyType = documentCopyUpdate.DocumentCopyType,
					WorkflowId = nodeReceive.Parent.Id,
					IsXlc = true,
					UserReceiveId = userReceiveId,
					DateCreated = dateCreated
				};
				histories.HistoryPath[histories.HistoryPath.Count - 2].UserReceives.Add(userReceive);
			}

			#endregion

			documentCopyUpdate.Histories = histories;

			SaveChanges();
		}

		/// <summary>
		/// Thực hiện chức năng ngược lại UpdateForTransfering().
		/// </summary>
		/// <param name="documentCopyUpdate"></param>
		public void UpdateForUndoTransfering(DocumentCopy documentCopyUpdate)
		{
			var historyProcess = documentCopyUpdate.Histories;

			#region DocTimeline
			// TODO: Lưu ý trường hợp xóa timeline khi khởi tạo văn bản thì tự bàn giao cho cả chính mình --> Không có timeline sau khi lấy lại --> Lỗi.
			// Xử lý không xóa timeline nếu đó là timeline của người khởi tạo văn bản tại thời điểm khởi tạo.

			#endregion

			#region Xu ly HistoryPath

			var removeHistory = historyProcess.HistoryPath.Last();
			historyProcess.HistoryPath.Remove(removeHistory);

			#endregion

			#region Xử lý xoa danh sách các hướng nhận bàn giao

			var removeUserReceive = historyProcess.HistoryPath.Last().UserReceives.SingleOrDefault(c => c.IsXlc && c.DocumentCopyId == documentCopyUpdate.DocumentCopyId);
			historyProcess.HistoryPath.Last().UserReceives.Remove(removeUserReceive);

			#endregion

			#region Cap nhat DocumentCopy

			var currentUser = _userService.GetFromCache(removeHistory.UserSendId);
			var currentDept = _departmentService.GetPrimaryDepartmentName(removeHistory.UserSendId);

			documentCopyUpdate.Histories = historyProcess;
			documentCopyUpdate.UserCurrentId = removeHistory.UserSendId;
			documentCopyUpdate.CurrentDepartmentName = currentDept;
			documentCopyUpdate.UserCurrentName = currentUser == null ? "" : currentUser.FullName;

			documentCopyUpdate.NodeCurrentId = removeHistory.NodeSendId;
			documentCopyUpdate.NodeCurrentPermission = _workflowHelper.GetNodePermission(removeHistory.WorkflowSendId, removeHistory.NodeSendId);
			//documentCopyUpdate.DateReceived = historyProcess.HistoryPath.Last().DateCreated;

			// TienBV sửa: nếu gắn lại thời gian là cái cũ thì không lên đầu danh sách văn bản được
			// + khi lấy danh sách văn bản mới cũng không lấy được do chỉ lấy những văn bản có thời gian >= thời gian lấy gần nhất.
			documentCopyUpdate.DateReceived = DateTime.Now;

			#region Cập nhật lại ý kiến trước đó

			//Lay ra nguoi chuyen van ban toi truoc do
			var userSendId = documentCopyUpdate.UserSendId ?? documentCopyUpdate.Histories.HistoryPath.Last().UserSendId;

			//Lay comment gan nhat cua nguoi gui van ban de cap nhat ys kien xu ly cuoi cung
			// var comment = _commentService.Gets(documentCopyUpdate.DocumentCopyId, userSendId).OrderByDescending(p => p.DateCreated).FirstOrDefault();
			var comment = _commentService.Gets(false, c => c.DocumentCopyId == documentCopyUpdate.DocumentCopyId && c.UserSendId == userSendId)
									.OrderByDescending(p => p.DateCreated).FirstOrDefault();

			string lastComent = string.Empty;
			string lastUserComment = string.Empty;
			int? lastUserIdComment = null;
			DateTime? lastDateComment = null;
			if (comment != null)
			{
				var objContentComment = Json2.ParseAs<ContentEntity>(comment.Content);
				lastComent = objContentComment.Content;
				lastUserIdComment = comment.UserSendId;
				lastDateComment = comment.DateCreated;

				lastUserComment = _userService.GetFromCache(comment.UserSendId.Value).FullName;

				_commentService.Delete(comment);
			}
			
			#endregion

			#endregion

			#region DocFinish

			var thamGiaxuly =
				historyProcess.HistoryPath.Any(
					c => c.UserSendId == removeHistory.UserReceiveId || c.UserReceiveId == removeHistory.UserReceiveId);
			if (!thamGiaxuly)
			{
				UpdateUserThamGia(documentCopyUpdate, new List<int>(), new List<int>() { removeHistory.UserReceiveId });
			}

			#endregion

			#region Xử lý văn bản liên quan

			if (!thamGiaxuly)
			{
				var deleteRelations = GetDocRelations(documentCopyUpdate.DocumentCopyId, removeHistory.UserReceiveId).ToList();
				UpdateRelationUserJoineds(deleteRelations, null, new List<int>() { removeHistory.UserReceiveId }, hasSaveChange: true);
			}

			#endregion

			Update(documentCopyUpdate);

			_cache.RemoveAll(documentCopyUpdate.DocumentCopyId);
		}

		/// <summary>
		/// Chuyển ý kiến xử lý từ hướng ĐXL, Xin ý kiến về hướng xin ý kiến ban đầu
		/// </summary>
		/// <param name="documentCopy">Văn bản</param>
		/// <param name="userSendId">Id người gửi</param>
		/// <param name="comment">Nội dung</param>
		/// <param name="dateCreated">Ngày tạo</param>
		/// <param name="commentFinish">Nội dung ghi log khi kết thúc văn bản</param>
		/// <param name="contentAuthorize">Nội dung ghi ủy quyền</param>
		public void SendCommentToParent(DocumentCopy documentCopy, int userSendId, string comment, DateTime dateCreated, string commentFinish, string contentAuthorize = "")
		{
			// Gửi ý kiến trả lời
			SendAnswerToParent(documentCopy, userSendId, comment, dateCreated, contentAuthorize);
			// Kết thúc xử lý
			Finish(documentCopy, dateCreated, userSendId, commentFinish);
		}

		/// <summary>
		/// Chuyển ý kiến xử lý từ hướng ĐXL, Xin ý kiến về hướng xin ý kiến ban đầu
		/// </summary>
		/// <param name="documentCopy"></param>
		/// <param name="userSendId"></param>
		/// <param name="comment"></param>
		/// <param name="dateCreated"> </param>
		/// <param name="userParentId">HopCV: Lấy ra id người xử lý để tạo notification cho người đó biết</param>
		/// <param name="commentFinish"></param>
		/// <param name="contentAuthorize"></param>
		public void SendCommentToParent(DocumentCopy documentCopy, int userSendId, string comment, DateTime dateCreated, out int userParentId, string commentFinish, string contentAuthorize = "")
		{
			// Gửi ý kiến trả lời
			SendAnswerToParent(documentCopy, userSendId, comment, dateCreated, out userParentId, contentAuthorize);

			// Kết thúc xử lý
			Finish(documentCopy, dateCreated, userSendId, comment);
		}

		/// <summary>
		/// Thiết lập trạng thái xem văn bản
		/// </summary>
		/// <param name="documentCopyId">Văn bản copy muốn thiết lập trạng thái xem văn bản</param>
		/// <param name="userOpeningId">Id người đang mở văn bản</param>
		/// <param name="viewed">Trạng thái đã xem</param>
		/// <remarks>
		/// - Đang xử lý: Chỉ cho phép đổi trạng thái với người đang giữ, trạng thái người đang giữ và người gửi phải giống nhau.
		/// - Thông báo: Trạng thái đã xem set cho người hiện tại.
		/// - Kết thúc: Trạng thái đã xem set cho người hiện tại.
		/// </remarks>
		public void SetViewed(int documentCopyId, int userOpeningId, bool viewed)
		{
			var docCopy = Get(documentCopyId);
			SetViewed(docCopy, userOpeningId, viewed);
		}

		/// <summary>
		/// Thiết lập trạng thái xem văn bản
		/// </summary>
		/// <param name="documentCopy">Văn bản copy muốn thiết lập trạng thái xem văn bản</param>
		/// <param name="userOpeningId">Id người đang mở văn bản</param>
		/// <param name="viewed">Trạng thái đã xem</param>
		/// <remarks>
		/// - Đang xử lý: Chỉ cho phép đổi trạng thái với người đang giữ, trạng thái người đang giữ và người gửi phải giống nhau.
		/// - Thông báo: Trạng thái đã xem set cho người hiện tại.
		/// - Kết thúc: Trạng thái đã xem set cho người hiện tại.
		/// </remarks>
		public void SetViewed(DocumentCopy documentCopy, int userOpeningId, bool viewed)
		{
			if (documentCopy == null)
			{
				throw new ArgumentNullException("documentCopy is null or empty!");
			}

			var userToUpdates = new List<int>();

			if (documentCopy.StatusInEnum == DocumentStatus.DangXuLy)
			{
				// Lưu ý không dc đẩy điều kiện này lên and với điều kiện trên
				// Trường hợp văn bản đang xử lý thì người đang giữ mới dc quyền thay đổi trạng thái đã đọc.
				if (documentCopy.IsCurrentUser(userOpeningId))
				{
					userToUpdates.Add(userOpeningId);

					if (documentCopy.LastUserIdComment.HasValue)
					{
						userToUpdates.Add(documentCopy.LastUserIdComment.Value);
					}
				}
				else if (documentCopy.UserThongBaos().Contains(userOpeningId))
				{
					userToUpdates.Add(userOpeningId);
				}
			}
			else
			{
				userToUpdates.Add(userOpeningId);
			}

			if (viewed)
			{
				UpdateUserDaXem(documentCopy, addUserIds: userToUpdates, hasSaveChange: true);
			}
			else
			{
				UpdateUserDaXem(documentCopy, addUserIds: null, removedUserIds: userToUpdates, hasSaveChange: true);
			}
		}

		/// <summary>
		/// Thiết lập trạng thái xem văn bản dạng thông báo
		/// TrinhNVd: 171215
		/// Đưa nghiệp vụ từ controler xuống và thêm kiểm tra người đang giữ văn bản có phải là người đăng nhập hiện tại không
		/// thì mới cập nhật trạng thái cho những người gửi trước
		/// </summary>
		/// <param name="documentCopy">Văn bản copy muốn thiết lập trạng thái xem văn bản</param>
		/// <param name="userSendId">Id người gửi văn bản</param>
		public void SetNotifyViewed(DocumentCopy documentCopy, int userSendId)
		{
			if (documentCopy == null)
			{
				throw new ArgumentNullException("documentCopy is null or empty!");
			}

			//TienBV: chả hiểu hàm này viết với mục đích gì?

			// update đã xem: update cho các node có trạng thái văn bản là chờ xử lý
			// Todo: Các trạng thái khác sẽ do người nhận được khi mở văn bản lên sẽ tự update.
			// Mục đích là để người gửi biết được người nhận đã xem hay chưa.
			if (documentCopy.IsCurrentUser(userSendId))
			{
				documentCopy.DateReceived = DateTime.Now;
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="documentCopyId"></param>
		/// <param name="userId"></param>
		/// <param name="type"> </param>
		/// <returns></returns>
		public bool CheckIsViewed(int documentCopyId, int userId, DocFinishType type)
		{
			if (documentCopyId <= 0)
			{
				return false;
			}

			var documentCopy = Get(documentCopyId);

			return documentCopy != null && documentCopy.IsViewed(userId);
		}

		/// <summary>
		/// Thay đổi trạng thái của document.
		/// </summary>
		/// <param name="docCopyId">Document copy id.</param>
		/// <param name="status">Status in enum</param>
		public void ChangeStatus(int docCopyId, DocumentStatus status)
		{
			var documentCopy = Get(docCopyId);
			if (documentCopy == null)
			{
				throw new Exception("Hồ sơ không tồn tại.");
			}
			documentCopy.Status = (byte)status;
			// Nếu là bản chính thì thay đổi luôn trạng thái trên hồ sơ
			if (documentCopy.DocumentCopyTypeInEnum == DocumentCopyTypes.XuLyChinh)
			{
				documentCopy.Document.Status = (byte)status;
			}
			//_docCopyService.Update(documentCopy);
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="startDate"></param>
		/// <param name="endDate"></param>
		/// <returns></returns>
		public IEnumerable<IDictionary<string, object>> GetActiveDocuments(DateTime startDate, DateTime endDate)
		{
			var sql = "SELECT dc.DocumentCopyId,dc.DocumentId,dc.Status,dc.DateOverdue,dc.DateModified,dc.UserCurrentId,d.DocCode,d.Compendium,u.UserName from documentcopy dc inner join document d on dc.DocumentId=d.DocumentId inner join user u on d.UserCreatedId=u.UserId where dc.DateModified >= @startDate and dc.DateModified <= @endDate and dc.DocumentCopyType in (1, 2, 4, 32, 64)";
			var parameters = new List<Object>
			{
				new SqlParameter("@startDate", startDate),
				new SqlParameter("@endDate", endDate)
			};
			var result = Context.RawQuery(sql, parameters.ToArray()) as IEnumerable<IDictionary<string, object>>;
			return result;
		}

		/// <summary>
		/// Trả về danh sách các văn bản liên thông đến mới cập nhật
		/// </summary>
		/// <param name="lastUpdate">Lần cập nhật gần nhất</param>
		public IEnumerable<DocumentCopy> GetDocumentLienThongModified(DateTime? lastUpdate)
		{
			return _documentCopyRepository.Gets(true, d => (!lastUpdate.HasValue || (d.DateModified.HasValue
															&& d.DateModified.Value >= lastUpdate))
															&& d.DocumentCopyType == (int)DocumentCopyTypes.XuLyChinh
															&& d.Document.Original == 2);
		}

		/// <summary>
		/// Cập nhật kết quả dừng xử lý
		/// </summary>
		/// <param name="documentCopyId"></param>
		/// <param name="comment"></param>
		/// <param name="dateAppointed"></param>
		public void ContinueProcess(int documentCopyId, string comment, DateTime dateAppointed)
		{
			var documentCopy = Get(documentCopyId);
			if (documentCopy == null)
			{
				throw new Exception("Hồ sơ yêu cầu không tồn lại, vui lòng thử lại");
			}

			var document = documentCopy.Document;
			var docPublishes = _docPublishService.GetSentPublishes(documentCopyId);

			// Cập nhật các hướng đang liên thông
			foreach (var docPublish in docPublishes)
			{
				if (docPublish.IsResponsed)
				{
					continue;
				}

				docPublish.IsResponsed = true;
				docPublish.DateResponsed = DateTime.Now;
				docPublish.Note = comment;
			}

			// Cập nhật document
			documentCopy.DocumentCopyType = (int)DocumentCopyTypes.XuLyChinh;
			documentCopy.Status = (int)DocumentStatus.DangXuLy;
			document.Status = (int)DocumentStatus.DangXuLy;
			document.DateRequireSupplementary = null;
			document.DateAppointed = dateAppointed;
			SaveChanges();
		}

        /// <summary>
		/// Lấy dữ liêu thô của bảng documentCopy=> join các bảng với nhau
		/// </summary>
		public IQueryable<DocumentCopy> Raw
        {
            get
            {
                return _documentCopyRepository.Raw;
            }
        }

        /// <summary>
        /// Lấy dữ liêu thô của bảng document=> join các bảng với nhau
        /// </summary>
        public IQueryable<Document> RawDocument
        {
            get
            {
                return _documentRepository.Raw;
            }
        }

        /// <summary>
        /// Lấy dữ liêu thô của bảng DocType=> join các bảng với nhau
        /// </summary>
        public IQueryable<DocType> RawDocType
        {
            get
            {
                return _docTypeRepository.Raw;
            }
        }

        /// <summary>
        /// Lấy dữ liêu thô của bảng ReportModes=> join các bảng với nhau
        /// </summary>
        public IQueryable<ReportModes> RawReportModes
        {
            get
            {
                return _reportModesRespository.Raw;
            }
        }

        /// <summary>
        /// Xet trạng thái chưa đọc cho user hiện tại
        /// </summary>
        /// <param name="documentCopy">hồ sơ</param>
        public void SetCurrentUserUnread(DocumentCopy documentCopy)
		{
			if (documentCopy == null)
			{
				return;
			}

			UpdateUserDaXem(documentCopy, null, new List<int>() { documentCopy.UserCurrentId }, hasSaveChange: true);
		}

        #endregion

        #region Private

        #region GovSync
        private static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        private SendReportData GetReportData(DocumentCached documentCopy, string organizationCode = "")
        {
            var sendReportData = new SendReportData();
            var header = new SendReportDataHeader();
            header.Code = documentCopy.DocTypeCode;
            header.Org = string.IsNullOrEmpty(organizationCode) ? "000.00.00.G12" : organizationCode; //documentCopy.OrganizationCode;
            header.Period = documentCopy.TimeKey;
            sendReportData.Header = header;

            var datas = new List<SendReportDataData>();
            var data = new SendReportDataData();
            var eFormData = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(documentCopy.Note);
            var values = new List<string>();
            foreach (var item in eFormData)
            {
                data = new SendReportDataData();
                values = new List<string>();
                // VuHQ lấy madinhdanh ở vị trí thứ 1 (Sau STT)
                data.Indicator = item.ElementAt(1).Value;
                for (var i = 0; i < item.Count - 1; i++)
                {
                    // Bỏ qua các chỉ tiêu: STT, Tên chỉ tiêu, ĐVT
                    if (i == 0 || i == 1 || i == 2 || i == 3)
                        continue;
                    if (string.IsNullOrEmpty(item.ElementAt(i).Value))
                    {
                        values.Add(null);
                    }
                    else
                    {
                        values.Add(item.ElementAt(i).Value);
                    }
                }

                data.Value = values;
                datas.Add(data);
            }

            //    data = new SendReportDataData();
            //    values = new List<string>();
            //    data.Indicator = key;
            //    foreach (var item in eFormData)
            //    {
            //        string value;
            //        if (item.TryGetValue(key, out value))
            //            values.Add(value);
            //    }
            //    data.value = values;
            //    datas.Add(data);
            //    index++;
            //}

            sendReportData.Data = datas;

            var attachments = new List<SendReportDataAttachment>();
            var attachment = new SendReportDataAttachment();

            sendReportData.Attachments = attachments;

            return sendReportData;
            // return JsonConvert.SerializeObject(sendReportData);
        }

        public SendReportData GetReportData(string doctypeCode, string timekey, string organizationCode, string dataSet)
        {
            var sendReportData = new SendReportData();
            var header = new SendReportDataHeader();
            header.Code = doctypeCode;
            header.Org = string.IsNullOrEmpty(organizationCode) ? "000.00.00.G12" : organizationCode; //documentCopy.OrganizationCode;
            header.Period = timekey;
            sendReportData.Header = header;

            var datas = new List<SendReportDataData>();
            var data = new SendReportDataData();
            var eFormData = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(dataSet);
            var values = new List<string>();
            foreach (var item in eFormData)
            {
                data = new SendReportDataData();
                values = new List<string>();
                // VuHQ lấy madinhdanh ở vị trí thứ 1 (Sau STT)
                data.Indicator = item.ElementAt(1).Value;
                for (var i = 0; i < item.Count - 1; i++)
                {
                    // Bỏ qua các chỉ tiêu: STT, Tên chỉ tiêu, ĐVT
                    if (i == 0 || i == 1 || i == 2 || i == 3)
                        continue;
                    if (string.IsNullOrEmpty(item.ElementAt(i).Value))
                    {
                        values.Add(null);
                    }
                    else
                    {
                        values.Add(item.ElementAt(i).Value);
                    }
                }

                data.Value = values;
                datas.Add(data);
            }

            //    data = new SendReportDataData();
            //    values = new List<string>();
            //    data.Indicator = key;
            //    foreach (var item in eFormData)
            //    {
            //        string value;
            //        if (item.TryGetValue(key, out value))
            //            values.Add(value);
            //    }
            //    data.value = values;
            //    datas.Add(data);
            //    index++;
            //}

            sendReportData.Data = datas;

            var attachments = new List<SendReportDataAttachment>();
            var attachment = new SendReportDataAttachment();

            sendReportData.Attachments = attachments;

            return sendReportData;
            // return JsonConvert.SerializeObject(sendReportData);
        }
        #endregion GovSync

        #region For Cache

        private void RemoveCache(int documentCopyId)
		{
			_cache.RemoveAll(documentCopyId);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="documentCopyIds"></param>
		public void ClearCache(IEnumerable<int> documentCopyIds)
		{
			_cache.RemoveAll(documentCopyIds);
		}

		private DocumentCached GetDocumentCache(int documentCopyId, int userOpeningId = 0)
		{
			var documentCopy = Get(documentCopyId);
			if (documentCopy == null)
			{
				return null;
			}

            var document = documentCopy.Document;

			var documentId = document.DocumentId;

			// Văn bản liên quan
			var relations = GetRelations(documentId, documentCopyId);

			// File đính kèm
			var attachments = GetAttachments(documentId);

			// Content
			var contents = GetDocumentContents(documentId);

			// Ý kiến xử lý
			var comments = GetDocumentComments(documentCopy, document);

			IEnumerable<Supplementary> supplmentaries = null;
			IEnumerable<DocFee> docFees = null;
			IEnumerable<DocPaper> docPapers = null;
			IEnumerable<Approver> approvers = null;
			if (document.IsHSMC)
			{
				supplmentaries = GetSupplementary(documentId);
				docFees = _documentService.GetDocFees(documentId);
				docPapers = _documentService.GetDocPapers(documentId);

				if (document.IsSuccess.HasValue)
				{
					approvers = GetApproves(documentId);
				}
			}

			var result = _cache.ToCache(documentCopy, document, null, relations, contents, comments, supplmentaries, docFees, docPapers, approvers);
			result.Attachments = attachments;
			result.DocumentPermissions = (int)_documentPermissionHelper.CheckForToolbar(document, documentCopy, documentCopy.UserCurrentId);

            result.TimeKey = documentCopy.Document.TimeKey;

            return result;
		}

		/// <summary>
		/// Trả về danh sách văn bản liên quan theo document
		/// </summary>
		/// <param name="documentId"></param>
		/// <param name="documentCopyId"></param>
		/// <returns></returns>
		/// <remarks>
		/// Bỏ qua check quyền xem, mặc định cho hiển thị.
		/// </remarks>
		private IEnumerable<DocRelation> GetRelations(Guid documentId, int documentCopyId)
		{
			var result = _docRelationRepository.GetsReadOnly(d => d.DocumentId == documentId).GroupBy(d => d.RelationId).Select(g => g.First()).ToList();
			return result;
		}

		private IEnumerable<AttachmentCached> GetAttachments(Guid documentId)
		{
			var allUserDictionary = _userService.GetAllCached().ToDictionary(u => u.UserId, u => u.FullName);

			var result = _attachmentService.GetsAs(a =>
					new
					{
						a.AttachmentId,
						a.AttachmentName,
						a.IsDeleted,
						a.Size,
						a.AttachmentDetails,
						a.UserDeleted,
						a.DeletedDate,
						a.VersionAttachment
					}, documentId)
					.OrderBy(a => a.IsDeleted)
					.Select(
						a =>
							new AttachmentCached
							{
								Id = a.AttachmentId,
								Name = a.AttachmentName,
								Extension = System.IO.Path.GetExtension(a.AttachmentName).Replace(".", ""),
								IsRemoved = a.IsDeleted,
								Size = StringExtension.ReadFileSize(a.Size),
								LastestVesion = a.VersionAttachment,
								UserDeleted = a.UserDeleted,
								DeletedDate = a.DeletedDate.HasValue ? a.DeletedDate.Value.ToString("G") : "",
								Versions = a.AttachmentDetails.Select(
											d =>
												new AttachmentDetailCache
												{
													Version = d.VersionAttachmentDetail,
													CreateDate = d.CreatedOnDate.ToString("G"),
													User = string.Format("{0} ({1})", allUserDictionary.ContainsKey(d.CreatedByUserId) ? allUserDictionary[d.CreatedByUserId] : d.CreatedByUserName, d.CreatedByUserName)
												}).OrderByDescending(d => d.Version).ToList()
							});

			return result;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="documentId"></param>
		/// <returns></returns>
		public IEnumerable<DocumentContent> GetDocumentContents(Guid documentId)
		{
			var result = _documentContentRepository.GetsReadOnly(dc => dc.DocumentId == documentId);
			return result;
		}

		private IEnumerable<Comment> GetDocumentComments(DocumentCopy documentCopy, Document document)
		{
			IEnumerable<Comment> result;

			var doctypeId = document.DocTypeId;

			var doctype = doctypeId.HasValue ? _doctypeService.GetFromCache(doctypeId.Value) : null;

			if (doctype != null && doctype.DocTypePermission.HasValue &&
					EnumHelper<DocTypePermissions>.ContainFlags(doctype.DocTypePermissionInEnum.Value, DocTypePermissions.DuocPhepXemYKienCacHuongReNhanh))
			{
				result = _commentService.Gets(document);
			}
			else
			{
				result = _commentService.Gets(documentCopy);
			}

			result = result.OrderByDescending(c => c.DateCreated).ToList();

			return result;
		}

		private IEnumerable<Supplementary> GetSupplementary(Guid documentId)
		{
			var result = _supplementaryRepository.GetsReadOnly(s => s.DocumentId == documentId);

			return result;
		}

		private IEnumerable<Approver> GetApproves(Guid documentId)
		{
			var result = _approverRepository.GetsReadOnly(a => a.DocumentId == documentId);
			return result;
		}

		#endregion

		private bool HasPermission(DocumentCopy documentCopy, Document document, int userId, out int userProcessId)
		{
			if (_documentPermissionHelper.CheckForUyQuyenVaXuLyVanBan(documentCopy, userId, out userProcessId))
			{
				return true;
			}

			userProcessId = userId;

			// Văn bản nhận từ đkqm và chưa có người nhận.
			if (document.Original == 2 && documentCopy.UserCurrentId == 0)
			{
				return true;
			}

			if (_documentPermissionHelper.CheckForQuyenXem(documentCopy, userId))
			{
				return true;
			}

			// Kiểm tra lại quyền chổ này.
			//if (!storePrivateId.HasValue || _storePrivateService.CheckPermissionStorePrivate(storePrivateId.Value, userSendId) == null)
			//{
			//    LogException("Không có quyền xem văn bản");
			//    return Json(new { error = "Bạn không có quyền xem văn bản!" }, JsonRequestBehavior.AllowGet);
			//}

			return false;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="documentCopy"></param>
		/// <param name="userSendId"></param>
		/// <param name="comment"></param>
		/// <param name="dateCreated"></param>
		/// <param name="contentAuthorize"></param>
		private void SendAnswerToParent(DocumentCopy documentCopy, int userSendId, string comment, DateTime dateCreated, string contentAuthorize = "")
		{
			if (documentCopy.DocumentCopyTypeInEnum != DocumentCopyTypes.DongXuLy &&
				documentCopy.DocumentCopyTypeInEnum != DocumentCopyTypes.XinYKien)
			{
				throw new ApplicationException("Chỉ văn bản xin ý kiến và văn bản Đồng xử lý mới sử dụng nghiệp vụ này.");
			}

			// Xử lý chuyển ý kiến đóng góp văn bản Đồng xử lý
			var parentId = documentCopy.ParentId;
			if (!parentId.HasValue)
			{
				throw new ApplicationException(
					"documentCopy.ParentId không được phép null với văn bản ĐXL trả lời ý kiến đóng góp.");
			}

			var parent = Get(parentId.Value);
			if (parent == null)
			{
				throw new ApplicationException("Không tim thấy hướng chuyển cha documentCopy.ParentId.");
			}

			var transfers = new List<CommentTransfer>
				{
					new CommentTransfer
						{
							Label = _userService.GetFromCache(parent.UserCurrentId).FullName,
							Type = "1",
							Value = ""
						}
				};

			switch (documentCopy.DocumentCopyTypeInEnum)
			{
				case DocumentCopyTypes.XinYKien:
					_commentService.SendAnswerForXinykien(documentCopy, userSendId, parent.UserCurrentId, comment,
														  transfers, dateCreated, contentAuthorize);
					break;

				case DocumentCopyTypes.DongXuLy:
					_commentService.SendAnswerForVanbanDxl(documentCopy, userSendId, parent.UserCurrentId, comment,
														   transfers, dateCreated, contentAuthorize);
					break;
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="documentCopy"></param>
		/// <param name="userSendId"></param>
		/// <param name="comment"></param>
		/// <param name="dateCreated"></param>
		/// <param name="userParentId">HopCV: Lấy ra id người xử lý để tạo notification cho người đó biết</param>
		/// <param name="contentAuthorize">Thông tin ghi người ủy quyền xử lý</param>
		private void SendAnswerToParent(DocumentCopy documentCopy, int userSendId, string comment, DateTime dateCreated, out int userParentId, string contentAuthorize = "")
		{
			if (documentCopy.DocumentCopyTypeInEnum != DocumentCopyTypes.DongXuLy &&
				documentCopy.DocumentCopyTypeInEnum != DocumentCopyTypes.XinYKien)
			{
				throw new ApplicationException("Chỉ văn bản xin ý kiến và văn bản Đồng xử lý mới sử dụng nghiệp vụ này.");
			}

			// Xử lý chuyển ý kiến đóng góp văn bản Đồng xử lý
			var parentId = documentCopy.ParentId;
			if (!parentId.HasValue)
			{
				throw new ApplicationException(
					"documentCopy.ParentId không được phép null với văn bản ĐXL trả lời ý kiến đóng góp.");
			}

			var parent = Get(parentId.Value);
			if (parent == null)
			{
				throw new ApplicationException("Không tim thấy hướng chuyển cha documentCopy.ParentId.");
			}

			// Thêm người gửi ý kiến vào danh sách người tham gia xử lý văn bản chính.
			// Sau khi cho ý kiến văn bản sẽ hiển thị ở mục theo dõi.
			UpdateUserThamGia(parent, new List<int>() { userSendId });

			//Lấy id của chuyển giao văn bản trước
			userParentId = parent.UserCurrentId;
			var transfers = new List<CommentTransfer>
				{
					new CommentTransfer
						{
							Label = _userService.GetFromCache(parent.UserCurrentId).FullName,
							Type = "xulychinh",
							Value = "viewXlc"
						}
				};

			switch (documentCopy.DocumentCopyTypeInEnum)
			{
				case DocumentCopyTypes.XinYKien:
					_commentService.SendAnswerForXinykien(documentCopy, userSendId, parent.UserCurrentId, comment,
														  transfers, dateCreated, contentAuthorize);
					break;

				case DocumentCopyTypes.DongXuLy:
					_commentService.SendAnswerForVanbanDxl(documentCopy, userSendId, parent.UserCurrentId, comment,
														   transfers, dateCreated, contentAuthorize);
					break;
			}
		}


		/// <summary>
		/// <para>Kiểm tra hướng chuyển hiện tại đã trả lời ý kiến đóng góp về hướng cha chưa</para>
		/// <para>Các hướng xử lý: DocumentCopyTypes.XuLyChinh, DocumentCopyTypes.DuyetGiaHan, DocumentCopyTypes.ThongBao, DocumentCopyTypes.ChoKetQuaDungXuLy mặc định trả về true</para>
		/// </summary>
		/// <param name="documentCopy"></param>
		/// <returns></returns>
		private bool ExitCommentToParent(DocumentCopy documentCopy)
		{
			if (documentCopy.DocumentCopyTypeInEnum == DocumentCopyTypes.XuLyChinh ||
				documentCopy.DocumentCopyTypeInEnum == DocumentCopyTypes.DuyetGiaHan ||
				documentCopy.DocumentCopyTypeInEnum == DocumentCopyTypes.ThongBao ||
				documentCopy.DocumentCopyTypeInEnum == DocumentCopyTypes.ChoKetQuaDungXuLy)
			{
				return true;
			}

			Comment comment = null;
			if (documentCopy.DocumentCopyTypeInEnum == DocumentCopyTypes.DongXuLy)
			{
				comment = _commentService.GetAnswerForVanbanDxl(documentCopy.DocumentCopyId);
			}
			if (documentCopy.DocumentCopyTypeInEnum == DocumentCopyTypes.XinYKien)
			{
				comment = _commentService.GetAnswerForXinykien(documentCopy.DocumentCopyId);
			}
			if (comment == null)
			{
				return false;
			}
			if (comment.DocumentCopyTargetId != documentCopy.ParentId)
			{
				throw new ApplicationException("Lỗi nghiệp vụ trả lời ý kiến đóng góp. Nội dung trả lời không gửi về hướng yêu cầu.");
			}
			return true;
		}

		#endregion

		/// <summary>
		/// Ca
		/// </summary>
		/// <param name="userId"></param>
		public void CapNhatChoPhepXuLyVanBanConvert(int userId)
		{
			var converts = _documentCopyRepository.Gets(false, dc => dc.Status == 2 && dc.Document.IsConverted && (userId == 0 || dc.UserCurrentId == userId));
			if (!converts.Any())
			{
				return;
			}

			var workflows = _workflowService.GetsFromCache();
			var doctypes = _doctypeService.GetAllFromCache();
			foreach (var documentCopy in converts)
			{
				var document = documentCopy.Document;
				var doctypeId = document.DocTypeId;
				if (!doctypeId.HasValue)
				{
					continue;
				}

				var doctype = doctypes.SingleOrDefault(dt => dt.DocTypeId == doctypeId.Value);
				if (doctype == null)
				{
					continue;
				}

				var workflow = workflows.FirstOrDefault(w => w.WorkflowId == doctype.WorkflowId);
				if (workflow == null)
				{
					continue;
				}

				var currentNodes = _workflowHelper.GetStartNodes(workflow.WorkflowId, documentCopy.UserCurrentId);
				if (currentNodes == null || !currentNodes.Any())
				{
					continue;
				}
				var currentNode = currentNodes.First();

				documentCopy.WorkflowId = workflow.WorkflowId;
				documentCopy.NodeCurrentId = currentNode.Id;
				documentCopy.NodeCurrentName = currentNode.NodeName;
				documentCopy.NodeCurrentPermission = (int)currentNode.GetNodePermission();
			}

			SaveChanges();
		}

		/// <summary>
		/// 
		/// </summary>
		public void FixDocFinish()
		{
			var total = _documentCopyRepository.Count();
			var page = 0;
			var pageSize = 10000;
			var skip = page * pageSize;
			while (skip <= total)
			{
				var documentCopies = _documentCopyRepository.GetsAs(d => new
				{
					d.DocumentCopyId,
					d.DocumentId,
					d.UserNguoiThamGia,
					d.UserThongBao,
					d.UserNguoiDaXem
				}, null).Skip(skip).Take(pageSize);

				skip = (++page) * pageSize;

				foreach (var documentCopy in documentCopies)
				{
					var docUsers = ParseUserIds(documentCopy.UserNguoiThamGia);
					var vieweds = ParseUserIds(documentCopy.UserNguoiDaXem);
					var thongbaos = ParseUserIds(documentCopy.UserThongBao);

					var docfinishes = _docFinishService.Gets(documentCopy.DocumentCopyId);
					var newDocFinishes = new List<DocFinish>();
					foreach (var userId in docUsers)
					{
						if (docfinishes.Any(d => d.UserId == userId))
						{
							continue;
						}

						var docFinish = new DocFinish()
						{
							DocumentCopyId = documentCopy.DocumentCopyId,
							DocumentId = documentCopy.DocumentId,
							UserId = userId,
							DocFinishType = (int)DocFinishType.ThamGiaXuLy,
							IsViewed = vieweds.Exists(v => v == userId)
						};

						newDocFinishes.Add(docFinish);
					}

					foreach (var userId in thongbaos)
					{
						if (docfinishes.Any(d => d.UserId == userId) || newDocFinishes.Any(d => d.UserId == userId))
						{
							continue;
						}

						var docFinish = new DocFinish()
						{
							DocumentCopyId = documentCopy.DocumentCopyId,
							DocumentId = documentCopy.DocumentId,
							UserId = userId,
							DocFinishType = (int)DocFinishType.KhongThamGiaXuLy,
							IsViewed = vieweds.Exists(v => v == userId)
						};

						newDocFinishes.Add(docFinish);
					}

					if (newDocFinishes.Any())
					{
						_docFinishService.Create(newDocFinishes);
					}
				}
			}
		}

		private List<int> ParseUserIds(string userStr)
		{
			var result = new List<int>();
			if (string.IsNullOrEmpty(userStr))
			{
				return result;
			}

			var userIds = userStr.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
			result = userIds.Where(u => !string.IsNullOrEmpty(u)).Select(u => Int32.Parse(u)).ToList();

			return result.Distinct().ToList();
		}
	}
}