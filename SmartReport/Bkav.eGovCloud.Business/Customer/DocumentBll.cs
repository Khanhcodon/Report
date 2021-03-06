#region

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Core.Document;
using Bkav.eGovCloud.Core.DynamicForm;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Entities.Enum;
using Microsoft.JScript;
using FormType = Bkav.eGovCloud.Entities.FormType;
using Bkav.eGovCloud.Core.History;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Core.ReadFile;
using Bkav.eGovCloud.Core.Workflow;
using Bkav.eGovCloud.Business.Objects;

#endregion

namespace Bkav.eGovCloud.Business.Customer
{
	/// <summary>
	///   <para> Bkav Corp. - BSO - eGov - eOffice team </para>
	///   <para> Project: eGov Cloud v1.0 </para>
	///   <para> Class : DocumentBll - public - BLL </para>
	///   <para> Access Modifiers: </para>
	///   <para> Create Date : 211112 </para>
	///   <para> Author : TrungVH </para>
	///   <para> Description : Nghiệp vụ xử lý văn bản hồ sơ </para>
	/// </summary>
	public class DocumentBll : ServiceBase
	{
		#region Readonly & Static Fields

		private readonly AttachmentBll _attachmentService;
		private readonly ApproverBll _approveService;
		private readonly DocTypeBll _docTypeService;
		private readonly CategoryBll _categoryService;

		private readonly IRepository<DocCatalog> _docCatalogRepository;
		private readonly IRepository<DocExtendField> _docExfieldRepository;
		private readonly IRepository<DocFee> _docFeeRepository;
		private readonly IRepository<DocPaper> _docPaperRepository;
		private readonly IRepository<DocumentContent> _documentContentRepository;
		private readonly IRepository<DocumentCopy> _documentCopyRepository;
		private readonly IRepository<Document> _documentRepository;
		private readonly IRepository<Fee> _feeRepository;
		private readonly IRepository<StorePrivateDocumentCopy> _storePrivateDocumentCopyRepository;
		private readonly IRepository<DoctypePaper> _doctypePaperRepository;

		private readonly StorePrivateBll _storePrivateService;
		private readonly DocumentPublishBll _docPublishService;
		private readonly LuceneBll _luceneService;
		private readonly IRepository<Paper> _paperRepository;
		private readonly IRepository<DocRelation> _docrelationRepository;
		private readonly SearchSettings _searchSettings;
		private readonly AdminGeneralSettings _generalSettings;
		private readonly UserBll _userService;
		private readonly IRepository<Entities.Customer.Address> _addressRepository;
		private readonly LogBll _logService;
		private readonly DailyProcessBll _dailyProcessService;
		private readonly DocTimelineBll _docTimeLineService;
		private readonly DepartmentBll _departmentBll;
		private readonly StoreBll _storeService;
		private readonly AuthorizeBll _authorizeService;
		private readonly ActionLevelBll _actionLevelService;

		#endregion

		#region C'tors

		/// <summary>
		///   Khởi tạo class <see cref="DocumentBll" />.
		/// </summary>
		/// <param name="context">Context</param>
		/// <param name="attachmentService"> Bll tương ứng với bảng Attachment </param>
		/// <param name="luceneService"> Bll tương ứng với bảng Lucene </param>
		/// <param name="doctypeService"></param>
		/// <param name="approveService">Bll ký duyệt</param>
		/// <param name="storePrivateService"></param>
		/// <param name="searchSettings"></param>
		/// <param name="generalSettings"></param>
		/// <param name="userService"></param>
		/// <param name="docPublishService"></param>
		/// <param name="logService"></param>
		/// <param name="dailyProcessService"></param>
		/// <param name="docTimelineService"></param>
		/// <param name="departmentBll"></param>
		/// <param name="storeService"></param>
		/// <param name="authorizeService"></param>
		/// <param name="categoryService"></param>
		/// <param name="actionLevelService"></param>
		public DocumentBll(
			IDbCustomerContext context,
			AttachmentBll attachmentService, DocTypeBll doctypeService, CategoryBll categoryService,
			LuceneBll luceneService,
			ApproverBll approveService,
			StorePrivateBll storePrivateService,
			SearchSettings searchSettings,
			AdminGeneralSettings generalSettings,
			DocumentPublishBll docPublishService,
			UserBll userService,
			LogBll logService,
			DailyProcessBll dailyProcessService,
			DocTimelineBll docTimelineService, AuthorizeBll authorizeService,
			DepartmentBll departmentBll, StoreBll storeService,
			ActionLevelBll actionLevelService)
			: base(context)
		{
			_documentRepository = Context.GetRepository<Document>();
			_attachmentService = attachmentService;
			_docTypeService = doctypeService;
			_paperRepository = Context.GetRepository<Paper>();
			_feeRepository = Context.GetRepository<Fee>();
			_documentContentRepository = Context.GetRepository<DocumentContent>();
			_docFeeRepository = Context.GetRepository<DocFee>();
			_docPaperRepository = Context.GetRepository<DocPaper>();
			_docCatalogRepository = Context.GetRepository<DocCatalog>();
			_docExfieldRepository = Context.GetRepository<DocExtendField>();
			_documentCopyRepository = Context.GetRepository<DocumentCopy>();
			_addressRepository = Context.GetRepository<Entities.Customer.Address>();
			_luceneService = luceneService;
			_storePrivateDocumentCopyRepository = Context.GetRepository<StorePrivateDocumentCopy>();
			_storePrivateService = storePrivateService;
			// _docFinishRepository = Context.GetRepository<DocFinish>();
			_docrelationRepository = Context.GetRepository<DocRelation>();
			_approveService = approveService;
			_searchSettings = searchSettings;
			_generalSettings = generalSettings;
			_userService = userService;
			_docPublishService = docPublishService;
			_logService = logService;
			_dailyProcessService = dailyProcessService;
			_doctypePaperRepository = Context.GetRepository<DoctypePaper>();
			_docTimeLineService = docTimelineService;
			_departmentBll = departmentBll;
			_storeService = storeService;
			_authorizeService = authorizeService;
			_categoryService = categoryService;
			_actionLevelService = actionLevelService;
		}

		#endregion

		#region Instance Methods

		/// <summary>
		///   Thêm mới công văn(hồ sơ)
		/// </summary>
		/// <param name="document"> Thực thể công văn(hồ sơ) </param>
		/// <param name="hasSaveChange"></param>
		/// <returns> </returns>
		public void Create(Document document, bool hasSaveChange = true)
		{
			if (document == null)
			{
				throw new ArgumentNullException("document");
			}

			if (document.IsHSMC && document.DocTypeId.HasValue)
			{
				document.DocCatalogs = AddCatalogsInDoc(document.DocumentContents);
				document.DocExtendFields = AddExfieldsInDoc(document.DocumentContents);
			}

			document.Compendium = GlobalObject.unescape(document.Compendium);
			document.Compendium2 = document.Compendium.StripVietnameseChars();

			_documentRepository.Create(document);

			if (hasSaveChange) Context.SaveChanges();
		}
        public IEnumerable<TimeKey> GetToDoctypeId(Guid doctypeId, string organizationCode, string timeKey, string year) {
            var docs = _documentRepository
                .GetsReadOnly(d => d.DocTypeId == doctypeId && d.OrganizationCode == organizationCode && d.TimeKey.Length == timeKey.Length && d.TimeKey.Contains(year))
                .Select(d => new TimeKey {
                        timekey = d.TimeKey,
                        document = d.DocumentId
                    } );
            return docs.Any() ? docs : null;
        }

        public IEnumerable<TimeKey> GetToDoctypeId_(Guid doctypeId, string organizationCode, string timeKey, string year)
        {
            var docs = _documentRepository
                .GetsReadOnly(d => d.DocTypeId == doctypeId && d.OrganizationCode == organizationCode && d.TimeKey.Length == timeKey.Length && d.TimeKey.Contains(timeKey))
                .Select(d => new TimeKey
                {
                    timekey = d.TimeKey,
                    document = d.DocumentId
                });
            return docs.Any() ? docs : null;
        }
        /// <summary>
        /// Tạo mới văn bản, hồ sơ một cửa dự thảo
        /// </summary>
        /// <param name="attachmentTemps">File đính kèm đang lưu tạm trong /Temp</param>
        /// <param name="document">Văn bản</param>
        /// <param name="createNode">Node tạo văn bản trên quy trình</param>
        /// <param name="relations">Danh sách văn bản liên quan</param>
        /// <param name="userThongBao">Danh sách người tham gia</param>
        /// <param name="documentCopyParentPath"></param>
        /// <returns></returns>
        public DocumentCopy Create(Document document, Node createNode, List<DocRelation> relations,
										IDictionary<string, IDictionary<string, string>> attachmentTemps, string userThongBao = null, string documentCopyParentPath = "")
		{
			DocumentCopy result;
			DocTypeCached docType = null;
			Category category = null;

			if (document.DateCreated == DateTime.MinValue)
			{
				document.DateCreated = DateTime.Now;
			}

			var userCreate = _userService.GetFromCache(document.UserCreatedId);
			var dateCreated = document.DateCreated;

			document.DocumentId = Guid.NewGuid();

			using (var trans = new TransactionScope())
			{
				#region Tạo Document Copy

				var currentDept = _departmentBll.GetPrimaryDepartmentName(document.UserCreatedId);
				result = new DocumentCopy()
				{
					DocTypeId = document.DocTypeId ?? Guid.Empty,
					WorkflowId = createNode.Parent.Id,
					UserCurrentId = document.UserCreatedId,
					UserCurrentName = userCreate == null ? "" : userCreate.FullName,
					CurrentDepartmentName = currentDept,
					Status = document.Status,
					DateCreated = dateCreated,
					DateReceived = dateCreated,
					DateFinished = document.DateFinished,
					DocumentCopyType = (int)DocumentCopyTypes.XuLyChinh,
					NodeCurrentId = createNode.Id,
					NodeCurrentName = createNode.NodeName,
					HasJustCreated = true,
					NodeCurrentPermission = (int)createNode.GetNodePermission(),
					UserNguoiThamGia = DocumentCopy.UserCompareString(document.UserCreatedId),
					DocumentUsers = DocumentCopy.UserCompareString(document.UserCreatedId),
					DocumentCopyParentPath = documentCopyParentPath,
					Note = document.Note,
					ProcessInfoPlus = document.ProcessInfo,
					FormId = document.FormId,
					TimeKey = document.TimeKey,
					OrganizationCode = document.OrganizationCode
				};

				if (userThongBao != null && userThongBao.Any())
				{
					result.UserThongBao += userThongBao;
					result.DocumentUsers += userThongBao;
				}

				#region Xử lý HistoryPath

				// Mặc định tạo history tự chuyển cho chính mình khi khởi tạo
				result.Histories = new HistoryProcess();
				result.Histories.HistoryPath.Add(new HistoryPath
				{
					ParentId = null,
					DateCreated = dateCreated.AddSeconds(-1),
					UserReceiveId = document.UserCreatedId,
					UserSendId = document.UserCreatedId,
					NodeReceiveId = createNode.Id,
					NodeSendId = createNode.Id,
					UserReceives = new List<UserReceives>(),
					WorkflowReceiveId = createNode.Parent.Id,
					WorkflowSendId = createNode.Parent.Id
				});
				result.History = Json2.Stringify(result.Histories);

				#endregion

				#endregion

				#region Tạo Document

				if (document.DocTypeId.HasValue)
				{
					docType = _docTypeService.GetFromCache(document.DocTypeId.Value);
				}

				if (document.CategoryId.HasValue)
				{
					category = _categoryService.GetFromCache(document.CategoryId.Value);
				}

				if (docType != null)
				{
					document.CategoryBusinessId = docType.CategoryBusinessId;
					document.DocTypePermission = docType.DocTypePermission;
					document.DocTypeName = docType.DocTypeName;
                    document.DocTypeCode = docType.DocTypeCode;
                    document.DateModified = dateCreated;
					if (docType.CategoryBusinessIdInEnum == CategoryBusinessTypes.Hsmc)
					{
						document.DocFieldIds = string.Format(";{0};", docType.DocFieldId);
					}

					if (docType.ActionLevel != null)
                    {
						var actionLevel = _actionLevelService.GetByCode(docType.ActionLevel.ToString());
						if (actionLevel != null)
                        {
							result.ActionLevelId = actionLevel.ActionLevelId;
							result.ActionLevelStartDate = actionLevel.StartTime;
							result.ActionLevelEndDate = actionLevel.EndTime;
						}
                    }
				}

				document.CategoryName = category == null ? "" : category.CategoryName;
				document.UserCreatedName = userCreate == null ? "" : userCreate.FullName;

				var attachments = _attachmentService.AddAttachmentInDoc(attachmentTemps, document.UserCreatedId, deleteTempfiles: true);
				document.Attachments = document.Attachments.Concat(attachments).ToList();

				document.Compendium = GlobalObject.unescape(document.Compendium);
				document.Compendium2 = document.Compendium.StripVietnameseChars();

				result.Document = document;

				#endregion
               
                _documentCopyRepository.Create(result);

                // Cần save change ở đây để sử dụng documentcopyid ở dưới
                Context.SaveChanges();

				foreach (var docrelation in relations)
				{
					docrelation.DocumentId = result.DocumentId;
					docrelation.DocumentCopyId = result.DocumentCopyId;
					_docrelationRepository.Create(docrelation);
				}

				Context.SaveChanges();
				trans.Complete();
			}
			return result;
		}

		/// <summary>
		/// Trả về văn bản gốc theo số ký hiệu
		/// </summary>
		/// <param name="docCode"></param>
		/// <returns></returns>
		public Document Get(string docCode)
		{
			return _documentRepository.Get(false, d => d.DocCode == docCode && d.Original != 2);
		}
        /// <summary>
		/// Trả về văn bản gốc theo số ký hiệu
		/// </summary>
		/// <param name="documentcopyid"></param>
		/// <returns></returns>
		public Document GetToDocId(Guid DocId)
        {
            return _documentRepository.Get(false, d => d.DocumentId == DocId );
        }
        /// <summary>
        /// Trả về văn bản gốc theo số ký hiệu
        /// </summary>
        /// <param name="docCode"></param>
        /// <returns></returns>
        public Document GetId(Guid? docCode)
        {
            return _documentRepository.Get(false, d => d.DocumentId == docCode && d.Original != 2);
        }

        /// <summary>
        ///   Cập nhật công văn(hồ sơ)
        /// </summary>
        /// <param name="document"> Thực thể công văn(hồ sơ) </param>
        /// <param name="newAttachments"> Danh sách các file tạm </param>
        /// <param name="docFees"> docfee </param>
        /// <param name="docPapers"> docpaper </param>
        /// <param name="removeAttachmentIds"> Danh sách các id file đính kèm bị loại bỏ </param>
        /// <param name="modifiedAttachment"> Danh sách các file đính kèm vào nội dung sửa đổi </param>
        /// <param name="userId"> Id user đang đăng nhập </param>
        /// <param name="hasChangeAttachment"> <c>True</c>Cho phép thực hiện thay đổi version khi không là văn bản chờ phát hành <c>False</c> ngược lại. </param>
        /// <returns> </returns>
        public void Update(Document document, IDictionary<string, IDictionary<string, string>> newAttachments,
							IEnumerable<DocFee> docFees,
							IEnumerable<DocPaper> docPapers, IEnumerable<int> removeAttachmentIds,
							IDictionary<int, string> modifiedAttachment, int userId, bool hasChangeAttachment = true)
		{
			if (document == null)
			{
				throw new ArgumentNullException("document");
			}

			document.Compendium = GlobalObject.unescape(document.Compendium);
			document.Compendium2 = document.Compendium.StripVietnameseChars();

			var user = _userService.GetFromCache(userId);
			UpdateAttachments(document, newAttachments, removeAttachmentIds, modifiedAttachment, user, hasChangeAttachment);
			UpdateDocFees(docFees, document.DocumentId);
			UpdateDocPapers(docPapers, document.DocumentId);
			UpdateContents(document.DocumentContents, document.DocumentId);

			document.DateModified = DateTime.Now;

			Context.SaveChanges();
		}

		/// <summary>
		/// Cập nhật file đính kèm
		/// </summary>
		/// <param name="document"></param>
		/// <param name="newAttachments"></param>
		/// <param name="removeAttachmentIds"></param>
		/// <param name="modifiedAttachment"></param>
		/// <param name="hasChangeAttachment"></param>
		/// <param name="hasSaveChange"></param>
		/// <param name="userUpdate"></param>
		public void UpdateAttachments(Document document, IDictionary<string, IDictionary<string, string>> newAttachments, IEnumerable<int> removeAttachmentIds,
							IDictionary<int, string> modifiedAttachment, User userUpdate, bool hasChangeAttachment = false, bool hasSaveChange = false)
		{

			//Xóa các file đính kèm cũ
			if (removeAttachmentIds != null && removeAttachmentIds.Any())
			{
				var attachments =
					document.Attachments.Where(a => removeAttachmentIds.Contains(a.AttachmentId)).ToList();
				if (attachments.Any())
				{
					foreach (var attachment in attachments)
					{
						attachment.IsDeleted = true;
						attachment.UserDeleted = string.Format("{0} - {1}", userUpdate.FullName, userUpdate.Username);
						attachment.DeletedDate = DateTime.Now;
					}
				}
			}

			// Thay đổi nội dung file đính kèm
			if (modifiedAttachment != null && modifiedAttachment.Any())
			{
				var attachments =
					document.Attachments.Where(a => modifiedAttachment.Keys.Contains(a.AttachmentId)).ToList();
				if (attachments.Any())
				{
					foreach (var attachment in attachments)
					{
						var contentBase64 = modifiedAttachment[attachment.AttachmentId];
						var contentStream = new MemoryStream(System.Convert.FromBase64String(contentBase64));
						var newVersion = _attachmentService.GetCountAttachmentDetail(attachment.AttachmentId) + 1;
						var attachmentDetail = _attachmentService.AddAttachmentDetail(contentStream, userUpdate.UserId,
																					  newVersion);
						attachment.AttachmentDetails.Add(attachmentDetail);

						if (hasChangeAttachment)
						{
							attachment.VersionAttachment = newVersion;
						}
						attachment.Size = attachmentDetail.Size;
					}
				}
			}

			//Thêm các file đính kèm mới
			var newAttachment = _attachmentService.AddAttachmentInDoc(newAttachments, userUpdate.UserId, deleteTempfiles: true);
			foreach (var attachment in newAttachment)
			{
				if (!document.Attachments.Any(a => a.AttachmentName == attachment.AttachmentName && a.Size == attachment.Size && !a.IsDeleted))
				{
					document.Attachments.Add(attachment);
				}
			}

			if (hasSaveChange)
			{
				Context.SaveChanges();
			}
		}

		/// <summary>
		///   Cập nhật công văn(hồ sơ)
		/// </summary>
		/// <param name="document"> Thực thể công văn(hồ sơ) </param>
		/// <returns> </returns>
		public void Update(Document document)
		{
			if (document == null)
			{
				throw new Exception("Hồ sơ không tồn tại");
			}

			document.Compendium = GlobalObject.unescape(document.Compendium);
			document.Compendium2 = document.Compendium2.StripVietnameseChars();

			Context.SaveChanges();
		}

		/// <summary>
		///   <para>Cập nhật kết quả kí duyệt văn bản/hồ sơ</para>
		///   <para>CuongNT@bkav.com - 310713</para>
		/// </summary>
		/// <param name="docId">Id văn bản kí duyệt</param>
		/// <param name="userSigningId">Id cán bộ kí duyệt</param>
		/// <param name="isSuccess"><c>True</c> đồng ý kí duyệt. <c>False</c> từ chối kí duyệt</param>
		/// <param name="note">Ý kiến kí duyệt</param>
		/// <param name="dateSigning">Ngày thực hiện kí duyệt</param>
		public void UpdateForSigning(Guid docId, int userSigningId, bool isSuccess, string note, DateTime dateSigning)
		{
			// Lưu thông tin kí duyệt cuối cùng
			var document = _documentRepository.Get(docId);
			document.IsSuccess = isSuccess;
			document.DateSuccess = dateSigning;

			var userSuccess = _userService.GetFromCache(userSigningId);
			document.UserSuccessId = userSigningId;
			document.UserSuccessName = userSuccess == null ? "" : userSuccess.FullName;
			document.SuccessNote = note;
			Context.SaveChanges();

			// Lưu thông tin kết quả xử lý cuối cùng
			UpdateResult(docId, isSuccess, dateSigning);
		}

		/// <summary>
		/// Cập nhật kết quả ký duyệt của văn bản hồ sơ
		/// </summary>
		/// <param name="documentCopy">Document Copy</param>
		/// <param name="userSigning">Id người ký duyệt</param>
		/// <param name="isSuccess">Giá trị xác định trạng thái ký duyệt thành công</param>
		/// <param name="note">Note ký duyệt</param>
		/// <param name="date">Ngày tháng ký duyệt</param>
		public void UpdateForSigning(DocumentCopy documentCopy, User userSigning, bool? isSuccess, string note, DateTime date)
		{
			var approve = _approveService.Get(documentCopy.DocumentCopyId, userSigning.UserId);
			var document = documentCopy.Document;
			document.IsSuccess = isSuccess;

			if (!isSuccess.HasValue)
			{
				document.DateSuccess = null;
				document.UserSuccessId = null;
				document.UserSuccessName = "";
				document.SuccessNote = null;
				document.Status = (byte)DocumentStatus.DangXuLy;
				if (approve != null)
				{
					_approveService.Delete(approve);
				}
			}
			else
			{
				document.DateSuccess = date;
				document.UserSuccessId = userSigning.UserId;
				document.UserSuccessName = userSigning.FullName;
				document.SuccessNote = note;
				document.Status = (byte)DocumentStatus.DungXuLy;

				if (approve != null)
				{
					approve.IsSuccess = isSuccess.Value;
					approve.Content = note;
					approve.DateCreated = date;
					Context.SaveChanges();
				}
				else
				{
					_approveService.Create(new Approver()
					{
						DocumentCopyId = documentCopy.DocumentCopyId,
						DocumentId = document.DocumentId,
						IsSuccess = isSuccess.Value,
						UserSendId = userSigning.UserId,
						UserName = userSigning.Username,
						FullName = userSigning.FullName,
						DateCreated = date,
						Content = note,
						IsDraft = false
					});
				}
			}
			
			Context.SaveChanges();
		}

		/// <summary>
		/// Cập nhật hạn xử lý khi thực hiện đổi hạn xử lý của văn bản theo loại văn bản mới được phân loại.
		/// </summary>
		/// <param name="docId"></param>
		/// <param name="dateResult"></param>
		public void UpdateExpire(Guid docId, DateTime? dateResult)
		{
			var document = _documentRepository.Get(docId);
			if (document == null)
			{
				throw new ArgumentNullException("document");
			}
			document.DateAppointed = dateResult;
			Context.SaveChanges();
		}

		/// <summary>
		/// Lấy ra các văn bản liên quan theo điều kiện truyền vào. Kết quả chỉ đọc
		/// </summary>
		/// <param name="spec">Điều kiện</param>
		/// <returns></returns>
		public IEnumerable<DocRelation> GetDocRelations(Expression<Func<DocRelation, bool>> spec = null)
		{
			return _docrelationRepository.GetsReadOnly(spec);
		}

		/// <summary>
		/// Tạo mới văn bản liên quan
		/// </summary>
		/// <param name="docRelation">docRelation</param>
		/// <param name="isSaveChanges">Có savechange ngay khôgn</param>
		/// <returns></returns>
		public void CreateDocRelations(DocRelation docRelation, bool isSaveChanges = false)
		{
			if (_docrelationRepository.Exist(dl => dl.DocumentId == docRelation.DocumentId && dl.RelationId == docRelation.RelationId))
			{
				return;
			}

			_docrelationRepository.Create(docRelation);

			if (isSaveChanges)
			{
				Context.SaveChanges();
			}
		}

		/// <summary>
		/// Xóa văn bản liên quan
		/// </summary>
		/// <param name="documentCopyId">documentCopyId</param>
		/// <param name="relationCopyId">relationCopyId</param>
		/// <param name="hasSaveChanges">Có savechange ngay không</param>
		public void DeleteDocRelations(int documentCopyId, int relationCopyId, bool hasSaveChanges = false)
		{
			var docRelations = _docrelationRepository.Gets(false, d => d.DocumentCopyId == documentCopyId && d.RelationCopyId == relationCopyId);
			if (docRelations.Any())
			{
				foreach (var relation in docRelations)
				{
					_docrelationRepository.Delete(relation);
				}
			}

			if (hasSaveChanges)
			{
				Context.SaveChanges();
			}
		}

		/// <summary>
		/// Trả về văn bản liên thông đến theo mã định danh văn bản
		/// </summary>
		/// <param name="maDinhDanh">Mã định danh văn bản đến</param>
		/// <param name="docCode">Số ký hiệu</param>
		/// <param name="datePublish">Ngày văn bản</param>
		/// <returns></returns>
		public Document GetByMaDinhDanhVanBan(string maDinhDanh, string docCode, DateTime datePublish)
		{
			return _documentRepository.Get(false, d => d.Original == 2 && d.DocCode.Equals(docCode, StringComparison.OrdinalIgnoreCase) 
								&& d.Address.Equals(maDinhDanh, StringComparison.OrdinalIgnoreCase) 
								&& (d.DatePublished.HasValue && d.DatePublished.Value == datePublish));
		}

		/// <summary>
		///   <para> Copy một văn bản hiện có sang một văn bản mới </para>
		///   <para> CuongNT@bkav.com - 190613 </para>
		/// </summary>
		/// <param name="documentId"> Id văn bản cần copy </param>
		/// <param name="userSendId"> Id can bo copy van ban </param>
		/// <param name="status"> Trang thai ho so </param>
		/// <returns> Id văn bản mới vừa sinh ra </returns>
		public Document Copy(Guid documentId, int userSendId, DocumentStatus status)
		{
			var document = new Document();
			var documentDuplicate = Get(documentId);

			if (documentDuplicate.DocCatalogs.Any())
			{
				foreach (var tempFile in documentDuplicate.DocCatalogs)
				{
					document.DocCatalogs.Add(tempFile);
				}
			}
			if (documentDuplicate.DocExtendFields.Any())
			{
				foreach (var tempFile in documentDuplicate.DocExtendFields)
				{
					document.DocExtendFields.Add(tempFile);
				}
			}
			if (documentDuplicate.DocPapers.Any())
			{
				foreach (var tempFile in documentDuplicate.DocPapers)
				{
					document.DocPapers.Add(tempFile);
				}
			}

			if (documentDuplicate.DocFees.Any())
			{
				foreach (var tempFile in documentDuplicate.DocFees)
				{
					document.DocFees.Add(tempFile);
				}
			}

			if (documentDuplicate.Attachments.Any())
			{
				var copyAttactments = _attachmentService.CopyAttachment(documentDuplicate.Attachments, userSendId);
				foreach (var tempFile in copyAttactments)
				{
					document.Attachments.Add(tempFile);
				}
			}

			document.Address = documentDuplicate.Address;
			document.CategoryBusinessId = documentDuplicate.CategoryBusinessId;
			document.CategoryId = documentDuplicate.CategoryId;
			document.CategoryName = documentDuplicate.CategoryName;
			document.CitizenName = documentDuplicate.CitizenName;
			document.Compendium = documentDuplicate.Compendium;
			document.Compendium2 = documentDuplicate.Compendium2;
			document.DocFieldIds = documentDuplicate.DocFieldIds;
			document.DocTypeId = documentDuplicate.DocTypeId;
			document.DocTypeName = documentDuplicate.DocTypeName;
			document.DocTypePermission = documentDuplicate.DocTypePermission;
			document.Email = documentDuplicate.Email;
			document.IdentityCard = documentDuplicate.IdentityCard;
			document.Phone = documentDuplicate.Phone;
			document.UrgentId = documentDuplicate.UrgentId;
			document.DocCode = documentDuplicate.DocCode;
			document.InOutCode = documentDuplicate.InOutCode;
			document.StoreId = documentDuplicate.StoreId;
			document.DatePublished = documentDuplicate.DatePublished;
			document.DateAppointed = documentDuplicate.DateAppointed;
			document.DateArrived = documentDuplicate.DateArrived;
			document.DateResponse = documentDuplicate.DateResponse;
			document.Organization = documentDuplicate.Organization;
			document.InOutPlace = documentDuplicate.InOutPlace;

			ResetDocumentInfo(ref document, userSendId, status);
			document.DocumentId = Guid.NewGuid();

			if (documentDuplicate.DocumentContents.Any())
			{
				foreach (var content in documentDuplicate.DocumentContents)
				{
					document.DocumentContents.Add(new DocumentContent
					{
						DocumentId = document.DocumentId,
						ContentName = content.ContentName,
						Content = content.Content,
						FormTypeId = content.FormTypeId,
						IsMain = content.IsMain,
						Version = 1,
						Url = content.Url,
						ContentUrl = content.ContentUrl
					});
				}
			}
			Create(document);

			return document;
		}

		#endregion

		#region Các hàm chung Get, Gets, GetsAs ...

		/// <summary>
		///   Tienbv 211112
		///   Lấy một document theo id.
		/// </summary>
		/// <param name="id"> The document id. </param>
		public Document Get(Guid id)
		{
			return _documentRepository.Get(id);
		}

		/// <summary>
		///   Tienbv 211112
		///   Lấy một document theo id.
		/// </summary>
		/// <param name="id"> The document id. </param>
		/// <param name="projector"></param>
		public T GetAs<T>(Guid id, Expression<Func<Document, T>> projector)
		{
			return _documentRepository.GetAs(projector, d => d.DocumentId == id);
		}

		/// <summary>
		///   Lấy ra mã màu cho từng văn bản
		/// </summary>
		/// <param name="ugentId"> Id độ khẩn </param>
		/// <param name="dateAppointed"> Ngày hẹn trả, ngày giải quyết </param>
		/// <param name="dateResponsed"> </param>
		/// <param name="dateResponsedOverdue"> </param>
		/// <param name="dateOverdue"> </param>
		/// <param name="documentCopyType"> </param>
		/// <returns> </returns>
		public int GetColor(int ugentId, DateTime? dateAppointed, DateTime? dateResponsed,
							DateTime? dateResponsedOverdue, DateTime? dateOverdue, int documentCopyType)
		{
			int result;
			var now = DateTime.Now;
			if (ugentId == 3)
			{
				result = 6;
			}
			else if (!dateResponsed.HasValue && (dateResponsedOverdue.HasValue && dateResponsedOverdue < now))
			{
				result = 5;
			}
			else if (ugentId == 2 || (dateOverdue.HasValue && dateOverdue < now) ||
					 (dateAppointed.HasValue && dateAppointed < now))
			{
				result = 4;
			}
			else if (dateOverdue.HasValue && dateOverdue.Value.Subtract(now).TotalDays <= 1 &&
					 dateOverdue.Value.Subtract(now).TotalDays > 0)
			{
				result = 3;
			}
			else if (documentCopyType == 2 || documentCopyType == 4)
			{
				result = 2;
			}
			else
			{
				result = 1;
			}
			return result;
		}

		/// <summary>
		///   Tienbv 301112
		///   Lấy các document theo điều  kiện kỹ thuật truyền vào.
		/// </summary>
		/// <param name="isReadOnly"></param>
		/// <param name="spec"> </param>
		/// <returns> </returns>
		public IEnumerable<Document> Gets(bool isReadOnly = true, Expression<Func<Document, bool>> spec = null)
		{
			return _documentRepository.Gets(isReadOnly, spec);
		}

		/// <summary>
		///   Lấy các document theo điều  kiện kỹ thuật truyền vào.
		/// </summary>
		/// <param name="projector"> Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table) </param>
		/// <param name="spec"> Điều kiện </param>
		/// <returns> </returns>
		public IEnumerable<T> GetsAs<T>(Expression<Func<Document, T>> projector,
										Expression<Func<Document, bool>> spec = null)
		{
			return _documentRepository.GetsAs(projector, spec);
		}

		#endregion

		#region Liên thông

		/// <summary>
		/// Trả về hồ sơ nhận liên thông
		/// </summary>
		/// <param name="docCode">Mã hồ sơ</param>
		/// <param name="organizationCode">Mã cơ quan gửi</param>
		/// <returns></returns>
		public Document GetLT(string docCode, string organizationCode)
		{
			var documents = _documentRepository.Gets(false, d => d.DocCode == docCode && d.Original == 2 && d.OrganizationCode == organizationCode);
			return documents.Any() ? documents.Last() : null;
		}

		/// <summary>
		/// Tiếp nhận văn bản đến từ hệ thống liên thông
		/// </summary>
		/// <param name="eGovdocument">Văn bản đến</param>
		/// <param name="userId">Người khởi tạo mặc định: lấy từ danh sách người được cấu hình tiếp nhận liên thông.</param>
		/// <param name="userReceives">Danh sách người được cấu hình tiếp nhận văn bản liên thông</param>
		/// <param name="responseRelations">Văn bản liên quan trả lời</param>
		public DocumentCopy CreateComingDocument(Document eGovdocument, int userId, List<int> userReceives, Dictionary<string, string> responseRelations)
		{
			DocumentCopy result = null;
			try
			{
				Create(eGovdocument);

				var currentUser = _userService.GetFromCache(userId);
				var currentDept = _departmentBll.GetPrimaryDepartmentName(userId);

				#region Xử lý HistoryPath

				var historyProcess = new HistoryProcess();

				/*
                 * Nếu là Khi tạo mới văn bản (hay tạo mới hướng văn bản xử lý chính) thì mặc định tạo một history là bàn giao cho chính mình,
                 * phục vụ việc lấy lại và sẽ chưa thông tin List<UserReceives>() đang bàn giao hiện tại.
                 */
				historyProcess.HistoryPath.Add(new HistoryPath
				{
					ParentId = null,
					DateCreated = DateTime.Now,//.AddSeconds(-5) De khong trung voi thoi gian ban giao thuc te, gay history co 2 moc giong het nhau
					UserReceiveId = userId,
					UserSendId = userId,
					NodeReceiveId = 0,
					NodeSendId = 0,
					UserReceives = new List<UserReceives>(),
					WorkflowReceiveId = 0,
					WorkflowSendId = 0
				});

				#endregion

				var docCopy = new Bkav.eGovCloud.Entities.Customer.DocumentCopy
				{
					DateCreated = DateTime.Now,
					DateReceived = DateTime.Now,
					DocumentId = eGovdocument.DocumentId,
					Status = (int)DocumentStatus.DangXuLy,
					UserCurrentId = userId,
					History = historyProcess.StringifyJs(),
					UserCurrentName = currentUser == null ? "" : currentUser.FullName,
					CurrentDepartmentName = currentDept,
					DocumentCopyType = (int)Bkav.eGovCloud.Core.Document.DocumentCopyTypes.XuLyChinh,
					UserNguoiThamGia = DocumentCopy.UserCompareString(userReceives),
					DocumentUsers = DocumentCopy.UserCompareString(userReceives)
				};

				_documentCopyRepository.Create(docCopy);

				Context.SaveChanges();

				if (responseRelations != null)
				{
					// Xử lý văn bản hồi báo
					AddRelations(responseRelations, docCopy, eGovdocument.DocCode);
				}

				Context.SaveChanges();
				result = docCopy;
			}
			catch (Exception ex)
			{
				_logService.Error("Tạo văn bản đến: ", ex);
			}

			return result;
		}

		/// <summary>
		/// Lưu lại document dọc từ xml
		/// </summary>
		/// <param name="eGovdocument"></param>
		/// <param name="userId"></param>
		/// <param name="currentUserId">user hien tai</param>
		/// <param name="workflowHelper"></param>
		public void CreateComingDocumentXML(Document eGovdocument, int userId, int currentUserId, WorkflowHelper workflowHelper)
		{
			try
			{
				Create(eGovdocument);

				var currentUser = _userService.GetFromCache(currentUserId);
				var currentDept = _departmentBll.GetPrimaryDepartmentName(currentUserId);
				var docCopy = new Bkav.eGovCloud.Entities.Customer.DocumentCopy
				{
					DateCreated = eGovdocument.DateCreated,
					DateReceived = DateTime.Now,
					DocumentId = eGovdocument.DocumentId,
					Status = eGovdocument.Status,
					UserCurrentId = currentUserId,
					UserCurrentName = currentUser == null ? "" : currentUser.FullName,
					CurrentDepartmentName = currentDept,
					DocumentCopyType = (int)DocumentCopyTypes.XuLyChinh,
					UserNguoiThamGia = DocumentCopy.UserCompareString(currentUserId),
					DocumentUsers = DocumentCopy.UserCompareString(currentUserId)
				};
				_documentCopyRepository.Create(docCopy);
				Context.SaveChanges();
			}
			catch (Exception ex)
			{
				_logService.Error("Lỗi đọc file XML: ", ex);
			}
		}

		#endregion

		#region Search

		/// <summary>
		///   TienBV 301112
		///   Lấy ra danh sách các document sử dụng cho tìm kiếm nhanh để add vào danh sách hồ sơ, văn bản liên quan.
		/// </summary>
		/// <remarks>
		///   Tìm các hồ sơ thỏa mãn đk: trích yếu, mã hồ sơ, tên công dân, email bắt đầu bằng từ khóa dc gõ.
		/// </remarks>
		/// <typeparam name="T">Kiểu đầu ra.</typeparam>
		/// <param name="projector"> Các thuộc tính cần lấy ra</param>
		/// <param name="userId"> Người tham gia</param>
		/// <param name="categoryId"> Hình thức văn bản</param>
		/// <param name="keyword"> Từ khóa </param>
		/// <param name="docCode"> Mã HS </param>
		/// <param name="inOutCode"> Số đến đi </param>
		/// <param name="urgentId"> Id độ khẩn </param>
		/// <param name="categoryBusinessId"> Id loại văn bản đến, văn bản đi, hồ sơ một ưửa </param>
		/// <param name="userCurrentId">Người đang giữ</param>
		/// <param name="fromDate"> Từ ngày </param>
		/// <param name="toDate"> Đến ngày </param>
		/// <param name="organizationCreate">Cơ quan ban hành</param>
		/// <param name="docfieldId"> Lĩnh vực </param>
		/// <param name="storePrivateId">Mã hồ ơ cá nhân</param>
		/// <param name="inOutPlaceId">Đơn vị soạn thảo</param>
		/// <param name="userSuccessId">Người ký</param>
		/// <param name="userCreatedId">Người khởi tạo</param>
		/// <param name="isMainProcess">Chỉ lấy xử lý chính</param>
		/// <returns> </returns>
		public IEnumerable<T> FindDocuments<T>(Expression<Func<DocumentCopy, Document, T>> projector,
													   int userId, int? categoryId = null,
													   string keyword = null, string docCode = null,
													   string inOutCode = null, int? urgentId = null,
													   int? categoryBusinessId = null, int? storePrivateId = null, int? userCurrentId = null,
													   DateTime? fromDate = null, DateTime? toDate = null,
													   int? inOutPlaceId = null, string organizationCreate = null,
														int? docfieldId = null, int? userSuccessId = null, int? userCreatedId = null, bool? isMainProcess = true)
		{
			var newToDate = !toDate.HasValue ? toDate : toDate.Value.AddDays(1).AddSeconds(-1);
			var docfieldIdStr = docfieldId.HasValue ? string.Format(";{0};", docfieldId.Value) : string.Empty;
			var isXlc = !isMainProcess.HasValue || isMainProcess.Value;
			var dept = inOutPlaceId.HasValue ? _departmentBll.GetbyId((int)inOutPlaceId) : null;
			string inOutPlace = (dept == null) ? null : dept.DepartmentPath;
			Expression<Func<DocumentCopy, bool>> specDocumentCopy = dc => (!fromDate.HasValue ||
																		   dc.DateCreated > fromDate.Value)
																		  && (!toDate.HasValue || dc.DateCreated < newToDate.Value)
																		  && (!userCurrentId.HasValue || dc.UserCurrentId == userCurrentId.Value);

			Expression<Func<DocumentCopy, bool>> specDocument = d =>
				(!categoryId.HasValue ||
				 d.Document.CategoryId == categoryId.Value)
				&&
				(string.IsNullOrEmpty(docCode) ||
				 d.Document.DocCode.ToLower().Contains(docCode))
				&&
				(string.IsNullOrEmpty(inOutCode) ||
				 d.Document.InOutCode.ToLower().Contains(inOutCode))
				&&
				(!urgentId.HasValue ||
				 d.Document.UrgentId == urgentId.Value)
				&&
				(string.IsNullOrEmpty(inOutPlace) ||
				 d.Document.InOutPlace.ToLower().Contains(inOutPlace))
				&&
				(string.IsNullOrEmpty(organizationCreate) ||
				 d.Document.Organization.ToLower().Contains(organizationCreate))
				&&
				(string.IsNullOrEmpty(docfieldIdStr) ||
				 d.Document.DocFieldIds.Contains(docfieldIdStr))
				&& (!userSuccessId.HasValue ||
					d.Document.UserSuccessId == userSuccessId.Value)
				&& (!userCreatedId.HasValue ||
					d.Document.UserCreatedId == userCreatedId.Value)
				&& (isXlc == false || (d.DocumentCopyType == (int)DocumentCopyTypes.XuLyChinh || d.DocumentCopyType == (int)DocumentCopyTypes.ChoKetQuaDungXuLy));

			Expression<Func<DocFinish, bool>> specDocFinish = df => df.UserId == userId;

			var user = _userService.Get(userId);
			IQueryable<DocumentCopy> documentCopies;
			var userStr = DocumentCopy.UserCompareString(userId);
			if (!_searchSettings.HasPermission(user))
			{
				specDocument = specDocument.And(d => !categoryBusinessId.HasValue || d.Document.CategoryBusinessId == categoryBusinessId.Value);
				specDocumentCopy.And(dc => (dc.UserNguoiThamGia != null && dc.UserNguoiThamGia.Contains(userStr))
											|| (dc.UserThongBao != null && dc.UserThongBao.Contains(userStr)));
				documentCopies = _documentCopyRepository.Raw.Where(specDocumentCopy.And(specDocument));

				// documentCopies = documentCopies.Join(_docFinishRepository.Raw.Where(specDocFinish), dc => dc.DocumentCopyId, df => df.DocumentCopyId, (dc, df) => dc);
			}
			else
			{
				specDocument = specDocument.And(d => !categoryBusinessId.HasValue || d.Document.CategoryBusinessId == categoryBusinessId.Value);
				documentCopies = _documentCopyRepository.Raw.Where(specDocumentCopy.And(specDocument));
			}

			if (storePrivateId.HasValue && storePrivateId.Value != 0)
			{
				var storePrivate = _storePrivateService.CheckPermissionStorePrivate(storePrivateId.Value, userId);
				if (storePrivate == null)
				{
					return null;
				}

				documentCopies = documentCopies.Join(
						_storePrivateDocumentCopyRepository.Raw
							.Where(s => s.StorePrivateId == storePrivate.StorePrivateId), dc => dc.DocumentCopyId,
						s => s.DocumentCopyId, (dc, s) => dc);
			}

			return documentCopies.Join(_documentRepository.Raw, dc => dc.DocumentId, d => d.DocumentId, projector).ToList();
		}


		/// <summary>
		///   TienBV 301112
		///   Lấy ra danh sách các document sử dụng cho tìm kiếm nhanh để add vào danh sách hồ sơ, văn bản liên quan.
		/// </summary>
		/// <remarks>
		///   Tìm các hồ sơ thỏa mãn đk: trích yếu, mã hồ sơ, tên công dân, email bắt đầu bằng từ khóa dc gõ.
		/// </remarks>
		/// <param name="searchTerm"> Từ khóa</param>
		/// <param name="userId"> Người tham gia</param>
		/// <param name="categoryId"> Hình thức văn bản</param>
		/// <param name="keyword"> Từ khóa </param>
		/// <param name="docCode"> Mã HS </param>
		/// <param name="inOutCode"> Số đến đi </param>
		/// <param name="urgentId"> Id độ khẩn </param>
		/// <param name="categoryBusinessId"> Id loại văn bản đến, văn bản đi, hồ sơ một ưửa </param>
		/// <param name="userCurrentId">Người đang giữ</param>
		/// <param name="fromDate"> Từ ngày </param>
		/// <param name="toDate"> Đến ngày </param>
		/// <param name="organizationCreate">Cơ quan ban hành</param>
		/// <param name="docfieldId"> Lĩnh vực </param>
		/// <param name="storePrivateId">Mã hồ sơ cá nhân</param>
		/// <param name="storeId">Sổ văn bản</param>
		/// <param name="inOutPlaceId">Đơn vị soạn thảo</param>
		/// <param name="userSuccessId">Người ký</param>
		/// <param name="userCreatedId">Người khởi tạo</param>
		/// <param name="isMainProcess">Chỉ lấy xử lý chính</param>
		/// <param name="fromPubDate"></param>
		/// <param name="toPubDate"></param>
		/// <param name="page"></param>
		/// <param name="pageSize"></param>
		/// <returns> </returns>
		public IEnumerable<dynamic> SearchDocument<T>(string searchTerm, int? userId, int? categoryId = null,
													   string keyword = null, string docCode = null,
													   string inOutCode = null, int? urgentId = null,
													   int? categoryBusinessId = null, int? storeId = null, int? storePrivateId = null, int? userCurrentId = null,
													   DateTime? fromDate = null, DateTime? toDate = null, DateTime? fromPubDate = null, DateTime? toPubDate = null,
													   int? inOutPlaceId = null, string organizationCreate = null,
														int? docfieldId = null, int? userSuccessId = null, int? userCreatedId = null, string docTypeCode = null,int? reportModeId = null,
                                                        Guid? doctypeId = null ,string inOutPlace = null,string userCreatedName = null,int? statusBC = null,int? actionLevelBC = null,
                                                        string timeKeys = null,bool? isMainProcess = false,int? page = 1, int? pageSize = 25, int? reportRuleIdOnly = null,
                                                        int? unitDelivery = null, int? unitReceive = null)
		{
			var currentUserId = userId;
			var hasViewAll = false;

            //if (!_generalSettings.HasCheckViewDocumentPermission)
            //{
            //    hasViewAll = true;
            //}

            if (/*!hasViewAll && */currentUserId != null)
			{
				var user = userId.HasValue ? _userService.Get(userId.Value) : null;
				// Nếu cán bộ hiện tại có quyền xem tất cả văn bản thì bỏ gán userid đi
				if (user != null && (_searchSettings.HasPermission(user) || user.CanReadEveryDocument == true))
				{
					hasViewAll = true;
				}
			}

			string lstCurUser = null;
			if (inOutPlaceId != null)
			{
				int deptId = (int)inOutPlaceId;
				var userInDept = _userService.GetUserByDept(deptId).ToList();
				if (userInDept != null)
				{
					foreach (var usr in userInDept)
					{
						lstCurUser += ";" + usr.UserId;
					}
					lstCurUser += ";";
				}
			}

			// searchTerm = string.Format("{0} {1} {2}", docCode, inOutCode, searchTerm);

			var parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@SearchTerm", !string.IsNullOrEmpty(searchTerm) ? searchTerm.Trim() : null));
			parameters.Add(new SqlParameter("@CategoryBusinessId", categoryBusinessId));
			parameters.Add(new SqlParameter("@docCode", !string.IsNullOrEmpty(docCode) ? docCode : null));
			parameters.Add(new SqlParameter("@InoutCode", !string.IsNullOrEmpty(inOutCode) ? inOutCode : null));
			parameters.Add(new SqlParameter("@UserId", currentUserId));
			parameters.Add(new SqlParameter("@CategoryId", categoryId));
			parameters.Add(new SqlParameter("@StoreId", storeId));
			parameters.Add(new SqlParameter("@StorePrivateId", storePrivateId));
			parameters.Add(new SqlParameter("@UserCurrentId", userCurrentId));
			parameters.Add(new SqlParameter("@UserSuccessId", userSuccessId));
			parameters.Add(new SqlParameter("@UserCreatedId", userCreatedId));
			parameters.Add(new SqlParameter("@UrgentId", urgentId));
			parameters.Add(new SqlParameter("@UsrInDept", lstCurUser));
			parameters.Add(new SqlParameter("@OrganizationCreate", !string.IsNullOrEmpty(organizationCreate) ? organizationCreate : null));
			parameters.Add(new SqlParameter("@FromDate", fromDate));
			parameters.Add(new SqlParameter("@ToDate", toDate));
			parameters.Add(new SqlParameter("@FromPubDate", fromPubDate));
			parameters.Add(new SqlParameter("@ToPubDate", toPubDate));
			parameters.Add(new SqlParameter("@isMainProcess", isMainProcess));
			parameters.Add(new SqlParameter("@hasViewAll", hasViewAll));
			parameters.Add(new SqlParameter("@Skip", (page - 1) * pageSize));
			parameters.Add(new SqlParameter("@Take", pageSize));
            parameters.Add(new SqlParameter("@DocTypeCode", !string.IsNullOrEmpty(docTypeCode) ? docTypeCode.Trim() : null));
            parameters.Add(new SqlParameter("@ReportModeId", reportModeId));
            parameters.Add(new SqlParameter("@DoctypeId", doctypeId));
            parameters.Add(new SqlParameter("@InOutPlace", !string.IsNullOrEmpty(inOutPlace) ? inOutPlace.Trim() : null));
            parameters.Add(new SqlParameter("@UserCreatedName", !string.IsNullOrEmpty(userCreatedName) ? userCreatedName.Trim() : null));
            parameters.Add(new SqlParameter("@Status", statusBC));
            parameters.Add(new SqlParameter("@ActionLevel", actionLevelBC));
            parameters.Add(new SqlParameter("@TimeKey", !string.IsNullOrEmpty(timeKeys) ? timeKeys.Trim() : null));
            parameters.Add(new SqlParameter("@ReportRuleId", reportRuleIdOnly));
            parameters.Add(new SqlParameter("@UnitDelivery", unitDelivery));
            parameters.Add(new SqlParameter("@unitReceive", unitReceive));

            var result = Context.RawProcedure("SearchProceduce", parameters.ToArray());
			return result;
		}

        
        public IEnumerable<dynamic> GetDataTH<T>(string organizationCode, string timekey)
        {
            int levelEdoc = 4;
            var leverCode = "";
            if (organizationCode.Contains("000.00.00"))
            {
                levelEdoc = 1;
                leverCode = "000.00";
                organizationCode = organizationCode.Replace("000.00.00", "");
            }

            if (organizationCode.Contains("000.00"))
            {
                levelEdoc = 2;
                leverCode = "000";
                organizationCode = organizationCode.Replace("000.00", "");
            }

            if (organizationCode.Contains("000"))
            {
                levelEdoc = 3;
                leverCode = "";
                organizationCode = organizationCode.Replace("000", "");
            }

            if (levelEdoc == 4)
            {
                return null;
            }

            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@timekey", organizationCode));
            parameters.Add(new SqlParameter("@organizationCode", timekey));
            parameters.Add(new SqlParameter("@leverCode", leverCode));
            var query = "SELECT CompilationData from document WHERE TimeKey = '{0}' and OrganizationCode LIKE '%{1}' and OrganizationCode LIKE '{2}%'";
            var qr = string.Format(query, timekey, organizationCode, leverCode);
            var result = Context.RawQuery(qr);
            return result;
        }

        /// <summary>
        ///   TienBV 301112
        ///   Lấy ra danh sách các document sử dụng cho tìm kiếm nhanh để add vào danh sách hồ sơ, văn bản liên quan.
        /// </summary>
        /// <remarks>
        ///   Tìm các hồ sơ thỏa mãn đk: trích yếu, mã hồ sơ, tên công dân, email bắt đầu bằng từ khóa dc gõ.
        /// </remarks>
        /// <param name="searchTerm"> Từ khóa</param>
        /// <param name="userId"> Người tham gia</param>
        /// <param name="isMainProcess"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns> </returns>
        public IEnumerable<dynamic> QuickSearchDocument<T>(string searchTerm, int? userId, bool isMainProcess, int? page = 1, int? pageSize = 100)
		{
			var currentUserId = userId;
			var hasViewAll = false;

			if (!_generalSettings.HasCheckViewDocumentPermission)
			{
				hasViewAll = true;
			}

			if (!hasViewAll && currentUserId != null)
			{
				var user = userId.HasValue ? _userService.Get(userId.Value) : null;

				// Nếu cán bộ hiện tại có quyền xem tất cả văn bản thì bỏ gán userid đi
				if (user != null && (_searchSettings.HasPermission(user) || user.CanReadEveryDocument == true))
				{
					hasViewAll = true;
				}
			}

			string userStoreIds = "", userAuthorizes = "";
			if (currentUserId > 0)
			{
				var storeIds = GetStoreIds(currentUserId.Value);
				userStoreIds = storeIds.Any() ? string.Join(",", storeIds) : "";
			}

			var parameters = new List<SqlParameter>();

			// Tìm 12 tháng gần nhất
			var fromDate = DateTime.Now.AddMonths(-12);

			parameters.Add(new SqlParameter("@UserId", currentUserId));
			parameters.Add(new SqlParameter("@SearchTerm", searchTerm));
			parameters.Add(new SqlParameter("@FromDate", fromDate));
			parameters.Add(new SqlParameter("@ToDate", null));
			parameters.Add(new SqlParameter("@skip", (page - 1) * pageSize));
			parameters.Add(new SqlParameter("@take", pageSize));
			parameters.Add(new SqlParameter("@StoreIds", userStoreIds));
			parameters.Add(new SqlParameter("@UserAuthorizes", userAuthorizes));
			parameters.Add(new SqlParameter("@isMainProcess", isMainProcess));
			parameters.Add(new SqlParameter("@hasViewAll", hasViewAll));

			var result = Context.RawProcedure("QuickSearch", parameters.ToArray());
			return result;
		}

		/// <summary>
		///   Lấy ra danh sách các document mà người dùng từng xử lý
		/// </summary>
		/// <typeparam name="T">Kiểu đầu ra.</typeparam>
		/// <param name="projector"> Các thuộc tính cần lấy ra</param>
		/// <param name="userId"> Người tham gia</param>
		/// <param name="documentIds"></param>
		/// <returns> </returns>
		public IEnumerable<T> FindDocuments<T>(Expression<Func<DocumentCopy, Document, T>> projector,
													   int userId, List<Guid> documentIds)
		{
			if (documentIds == null || !documentIds.Any())
			{
				return null;
			}
			Expression<Func<Document, bool>> specDocument = d => documentIds.Contains(d.DocumentId);
			Expression<Func<DocFinish, bool>> specDocFinish = df => df.UserId == userId;
			var userStr = DocumentCopy.UserCompareString(userId);

			var documentCopies = _documentCopyRepository.Raw.Where(dc => (dc.UserNguoiThamGia != null && dc.UserNguoiThamGia.Contains(userStr))
																		|| (dc.UserThongBao != null && dc.UserThongBao.Contains(userStr)));
			//_documentCopyRepository.Raw
			//    .Join(_docFinishRepository.Raw.Where(specDocFinish), dc => dc.DocumentCopyId, df => df.DocumentCopyId, (dc, df) => dc);

			return documentCopies.Join(_documentRepository.Raw.Where(specDocument), dc => dc.DocumentId, d => d.DocumentId, projector).ToList();
		}

        private IEnumerable<int> GetStoreIds(int userId)
		{
			return _storeService.GetsByUser(userId).Select(s => s.StoreId);
		}

		private List<int> GetAuthorize(int userId)
		{
			return new List<int>();
		}

		#endregion

		#region Xử lý Hồ sơ một cửa

#if HoSoMotCuaEdition

		/// <summary>
		/// Lấy một document theo mã hồ sơ.
		/// </summary>
		/// <param name="docCode"> Mã hồ sơ. </param>
		public Document GetHsmc(string docCode)
		{
			return _documentRepository.Get(false, d => d.DocCode.Equals(docCode, StringComparison.OrdinalIgnoreCase)
								&& d.Original != 2
								&& d.CategoryBusinessId == (int)CategoryBusinessTypes.Hsmc);
		}

		/// <summary>
		/// Lấy các hồ sơ mới nhất vừa được tiếp nhận để hiển thị trên màn hình lớn
		/// </summary>
		/// <param name="record"></param>
		/// <returns></returns>
		public IEnumerable<Document> GetNewestHsmc(int record)
		{
			var sort = Context.Filters.CreateSort<Document>(true, "DateCreated");
			return _documentRepository.GetsReadOnly(d => d.Original != 2 && d.Status == 2
				&& d.CategoryBusinessId == (int)CategoryBusinessTypes.Hsmc,
							sort, Context.Filters.Page<Document>(1, record));
		}

#endif

		/// <summary>
		/// Cập nhật trả kết quả văn bản hồ sơ
		/// </summary>
		/// <param name="doc">Document</param>
		/// <param name="userId"></param>
		/// <param name="returnNote"></param>
		/// <param name="documentCopy"></param>
		public void UpdateForReturning(Document doc, DocumentCopy documentCopy, int userId, string returnNote)
		{
			if (doc == null)
			{
				throw new Exception("Hồ sơ không tồn tại");
			}

			if (!doc.IsSuccess.HasValue)
			{
				throw new Exception("Hồ sơ cần được cập nhật kết quả xử lý cuối cùng");
			}

			doc.IsReturned = true;
			doc.DateReturned = DateTime.Now;
			doc.UserReturnedId = userId;
			doc.ReturnNote = returnNote;

			#region Cập nhật doctimeline của người hiện tại

			// var docTimeline = _docTimeLineService.Get(documentCopyId, userId, );

			// Cập nhật thời gian ra khỏi nút
			if (documentCopy.Histories.HistoryPath.Any())
			{
				var dateOfTimeline = documentCopy.Histories.HistoryPath.Last().DateCreated;
				var docTimeline = _docTimeLineService.Get(documentCopy.DocumentCopyId, userId, dateOfTimeline);
				if (docTimeline == null)
				{
					//Todo: Xử lý null doctimeline ở đây
				}
				else
				{
					_docTimeLineService.Update(docTimeline, DateTime.Now, false);
				}
			}

			#endregion

			// Cập nhật xử lý hồ sơ trong ngày
			var dailyProcess = new DailyProcess()
			{
				UserId = userId,
				DocumentCopyId = documentCopy.DocumentCopyId,
				DocumentId = doc.DocumentId,
				ProcessType = (int)DocumentProcessType.TraKetQua,
				DateCreated = DateTime.Now,
				CitizenName = doc.CitizenName,
				Receiver = "",
				Note = "Hồ sơ đã trả kết quả"
			};
			_dailyProcessService.Create(dailyProcess);

			doc.Compendium = GlobalObject.unescape(doc.Compendium);
			doc.Compendium2 = doc.Compendium2.StripVietnameseChars();

			Context.SaveChanges();
		}

		/// <summary>
		/// Cập nhật kết quả xử lý cuối cùng: Văn bản/hồ sơ xử lý thành công hay không.
		/// </summary>
		/// <param name="docId"></param>
		/// <param name="resultStatus"></param>
		/// <param name="dateResult"></param>
		public void UpdateResult(Guid docId, bool resultStatus, DateTime dateResult)
		{
			var document = _documentRepository.Get(docId);
			UpdateResult(document, resultStatus, dateResult);
		}

		/// <summary>
		/// Cập nhật kết quả xử lý cuối cùng: Văn bản/hồ sơ xử lý thành công hay không.
		/// </summary>
		/// <param name="document"></param>
		/// <param name="resultStatus"></param>
		/// <param name="dateResult"></param>
		public void UpdateResult(Document document, bool resultStatus, DateTime dateResult)
		{
			if (document == null)
			{
				throw new ArgumentNullException("document");
			}
			//document.ResultStatus = resultStatus;
			//document.DateResult = dateResult;
			document.Compendium = GlobalObject.unescape(document.Compendium);
			document.Compendium2 = document.Compendium2.StripVietnameseChars();
			Context.SaveChanges();
		}

		/// <summary>
		/// Trả về danh sách giấy tờ của hồ sơ
		/// </summary>
		/// <param name="documentId">Id hồ sơ</param>
		/// <param name="paperType">Loại giấy tờ</param>
		/// <returns></returns>
		public IEnumerable<DocPaper> GetDocPapers(Guid documentId, PaperType? paperType = null)
		{
			return _docPaperRepository.GetsReadOnly(d => d.DocumentId == documentId && (!paperType.HasValue || d.Type == (int)paperType.Value));
		}

		/// <summary>
		/// Trả về danh sách lệ phí của hồ sơ
		/// </summary>
		/// <param name="documentId">Id hồ sơ</param>
		/// <param name="feeType">Loại lệ phí</param>
		/// <returns></returns>
		public IEnumerable<DocFee> GetDocFees(Guid documentId, FeeType? feeType = null)
		{
			return _docFeeRepository.GetsReadOnly(f => f.DocumentId == documentId && (!feeType.HasValue || f.Type == (int)feeType.Value));
		}

		/// <summary>
		/// Xóa giấy tờ hồ sơ
		/// </summary>
		/// <param name="id"></param>
		public void DeleteDocPaper(int id)
		{
			var docPaper = _docPaperRepository.Get(id);
			if (docPaper != null)
			{
				_docPaperRepository.Delete(docPaper);
			}

			Context.SaveChanges();
		}

		/// <summary>
		/// Xóa lệ phí của hồ sơ
		/// </summary>
		/// <param name="id"></param>
		public void DeleteDocFee(int id)
		{
			var docFee = _docFeeRepository.Get(id);
			if (docFee != null)
			{
				_docFeeRepository.Delete(docFee);
			}

			Context.SaveChanges();
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="contents"></param>
		/// <returns></returns>
		private ICollection<DocCatalog> AddCatalogsInDoc(IEnumerable<DocumentContent> contents)
		{
			var result = new List<DocCatalog>();
			foreach (var content in contents)
			{
				if (content.FormTypeIdInEnum != FormType.DynamicForm)
				{
					continue;
				}

				JsDocument jsDocument;
				if (!DynamicFormHelper.TryParse(content.Content, out jsDocument))
				{
					//TODO: Vẫn xử lý continue như dưới, xong cần log vào hệ thống log để bên phát triển biết và tìm cách fix lỗi tận gốc
					continue;
				}

				foreach (var jsControl in jsDocument.DocFieldJson)
				{
					if (!jsControl.TypeId.Equals((int)ControlType.DropDownList) &&
						!jsControl.TypeId.Equals((int)ControlType.CheckBoxList))
					{
						continue;
					}

					if (!string.IsNullOrEmpty(jsControl.CatalogSelected) && jsControl.CatalogSelectedObject != null)
					{
						result.Add(new DocCatalog
						{
							CatalogId = jsControl.ControlId,
							CatalogValueId = Guid.Parse(jsControl.CatalogSelectedObject.Key),
							CatalogName = jsControl.DisplayName,
							CatalogValue = jsControl.CatalogSelectedObject.Value,
							FormId = Guid.Parse(jsDocument.FormId)
						});
					}
				}
			}
			return result;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="contents"></param>
		/// <returns></returns>
		private ICollection<DocExtendField> AddExfieldsInDoc(IEnumerable<DocumentContent> contents)
		{
			var result = new List<DocExtendField>();
			foreach (var content in contents)
			{
				if (content.FormTypeId != (int)FormType.DynamicForm)
				{
					continue;
				}

				JsDocument jsDocument;
				if (!DynamicFormHelper.TryParse(content.Content, out jsDocument))
				{
					//TODO: Vẫn xử lý continue như dưới, xong cần log vào hệ thống log để bên phát triển biết và tìm cách fix lỗi tận gốc
					continue;
				}

				foreach (var jsControl in jsDocument.DocFieldJson)
				{
					if (!jsControl.TypeId.Equals((int)ControlType.Textbox))
					{
						continue;
					}

					if (string.IsNullOrEmpty(jsControl.Value)) continue;
					result.Add(new DocExtendField
					{
						ExtendFieldId = jsControl.ControlId,
						ExtendFieldName = jsControl.DisplayName,
						ExtendFieldValue = jsControl.Value,
						FormId = Guid.Parse(jsDocument.FormId)
					});
				}
			}
			return result;
		}

		#endregion

		#region Privates

		/// <summary>
		///   Reset thông tin một hồ sơ về trạng thái mặc định như khi mới khởi tạo
		/// </summary>
		/// <param name="document"> Hồ sơ cần reset </param>
		/// <param name="userCreatedId"> Id của người sẽ được coi là khởi tạo hồ sơ reset </param>
		/// <param name="status"> Trang thai ho so </param>
		private void ResetDocumentInfo(ref Document document, int userCreatedId, DocumentStatus status)
		{
			var dateCreated = DateTime.Now;
			document.DateCreated = dateCreated;
			document.DateModified = dateCreated;
			document.DateOfIssueCode = null;
			document.DateResponse = null;
			document.DateResponsed = null;
			document.DateResult = null;
			document.DateReturned = null;
			document.DateSuccess = null;

			// Mặc định coi như các loại văn bản mà cấp mã khi tạo mới thì khi copy cũng sẽ không tạo mã vì những loại này chắc chắn sẽ không có được phép copy văn bản.
			// TODO: Kiểm tra và ném biệt lệ nếu đây là loại văn bản cần cấp mã ngay từ khi tạo mới (hiện mới chỉ có HSMC) thì sẽ không được phép dẫn tới tình huống copy văn bản này.
			//document.DocCode = string.Empty;
			document.IsReturned = null;
			document.IsSuccess = null;
			document.IsSupplemented = null;
			document.ProcessedMinutes = 0;
			document.ResultStatus = null;
			document.ReturnNote = string.Empty;
			document.Status = (byte)status;
			document.SuccessNote = string.Empty;
			document.UserReturnedId = null;

			var userCreated = _userService.GetFromCache(userCreatedId);
			document.UserCreatedId = userCreatedId;
			document.UserCreatedName = userCreated == null ? "" : userCreated.FullName;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="json"></param>
		/// <param name="docId"></param>
		private void UpdateCatalogInDoc(string json, Guid docId)
		{
			JsDocument jsDocument;
			if (DynamicFormHelper.TryParse(json, out jsDocument))
			{
				Guid formId;
				if (Guid.TryParse(jsDocument.FormId, out formId))
				{
					foreach (var jsControl in jsDocument.DocFieldJson)
					{
						if (jsControl.TypeId.Equals((int)ControlType.DropDownList) ||
							jsControl.TypeId.Equals((int)ControlType.CheckBoxList))
						{
							if (!string.IsNullOrEmpty(jsControl.CatalogSelected) &&
								jsControl.CatalogSelectedObject != null)
							{
								var spec = DocCatalogQuery.GetCatalogInDoc(docId, formId, jsControl.ControlId);
								var docCatalog = _docCatalogRepository.Get(false, spec);
								if (docCatalog == null ||
									docCatalog.CatalogValue == jsControl.CatalogSelectedObject.Value)
								{
									continue;
								}

								docCatalog.CatalogValue = jsControl.CatalogSelectedObject.Value;
								docCatalog.CatalogValueId = Guid.Parse(jsControl.CatalogSelectedObject.Key);
								//_docCatalogRepository.Update(docCatalog);
							}
						}
					}
				}
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="documentContent"></param>
		/// <param name="docId"></param>
		private void UpdateContents(IEnumerable<DocumentContent> documentContent, Guid docId)
		{
			foreach (var content in documentContent)
			{
				content.DocumentId = docId;
				if (content.FormTypeIdInEnum == FormType.DynamicForm)
				{
					UpdateCatalogInDoc(content.Content, docId);
					UpdateExfieldInDoc(content.Content, docId);
				}
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="modelFees"></param>
		/// <param name="docId"></param>
		private void UpdateDocFees(IEnumerable<DocFee> modelFees, Guid docId)
		{
			if (!modelFees.Any())
			{
				return;
			}

			var oldFees = _docFeeRepository.Gets(false, o => o.DocumentId == docId);
			foreach (var fee in modelFees)
			{
				if (fee.DocFeeId == 0)
				{
					fee.DocumentId = docId;
					_docFeeRepository.Create(fee);
				}
				else
				{
					var docFee = oldFees.SingleOrDefault(f => f.DocFeeId == fee.DocFeeId);
					if (docFee != null)
					{
						docFee.Price = fee.Price;
					}
				}
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="modelPapers"></param>
		/// <param name="docId"></param>
		private void UpdateDocPapers(IEnumerable<DocPaper> modelPapers, Guid docId)
		{
			if (!modelPapers.Any())
			{
				return;
			}

			var oldPapers = _docPaperRepository.Gets(false, o => o.DocumentId == docId);
			foreach (var paper in modelPapers)
			{
				if (paper.DocPaperId == 0)
				{
					paper.DocumentId = docId;
					_docPaperRepository.Create(paper);
				}
				else
				{
					var docPaper = oldPapers.SingleOrDefault(f => f.DocPaperId == paper.DocPaperId);
					if (docPaper != null)
					{
						docPaper.Amount = paper.Amount;
					}
				}
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="json"></param>
		/// <param name="docId"></param>
		private void UpdateExfieldInDoc(string json, Guid docId)
		{
			JsDocument jsDocument;
			if (DynamicFormHelper.TryParse(json, out jsDocument))
			{
				Guid formId;
				if (Guid.TryParse(jsDocument.FormId, out formId))
				{
					foreach (var jsControl in jsDocument.DocFieldJson)
					{
						if (jsControl.TypeId.Equals((int)ControlType.Textbox))
						{
							if (string.IsNullOrEmpty(jsControl.Value))
							{
								continue;
							}
							var spec = DocExtendFieldQuery.GetExfieldInDoc(docId, formId, jsControl.ControlId);
							var docExfield = _docExfieldRepository.Get(false, spec);
							if (docExfield == null || docExfield.ExtendFieldValue == jsControl.Value)
							{
								continue;
							}
							docExfield.ExtendFieldValue = jsControl.Value;
							docExfield.ExtendFieldId = jsControl.ControlId;
							//_docExfieldRepository.Update(docExfield);
						}
					}
				}
			}
		}

		private void AddRelations(Dictionary<string, string> relations, DocumentCopy docCopy, string docCodeResponsed)
		{
			if (relations == null || !relations.Any())
			{
				return;
			}

			var document = docCopy.Document;

			foreach (var relation in relations)
			{
				var docCode = relation.Key;
				var addressCode = relation.Value;

				var docRequireResponse = _docPublishService.Gets(false, d => d.DocCode.Equals(docCode) && d.AddressCode == addressCode && d.HasLienThong && !d.IsPending);

				DocPublish docpublish = null;
				if (docRequireResponse.Any())
				{
					docpublish = docRequireResponse.First();
				}

				if (docpublish == null)
				{
					continue;
				}

				docpublish.IsResponsed = true;
				docpublish.DateResponsed = docCopy.DateCreated;
				docpublish.DocCodeResponsed = docCodeResponsed;
				docpublish.DocumentCopyResponsed = docCopy.DocumentCopyId;

				Context.SaveChanges();

				// Tìm lại văn bản đã gửi phát hành trước để cập nhật liên quan hồi báo
				// Todo xem lại chổ này gọi HSMC không thôi?
				// 
				var docOrigin = Get(docpublish.DocumentId);
				if (docOrigin == null)
				{
					continue;
				}

				var docCopyOrigin = _documentCopyRepository.Get(docpublish.DocumentCopyId);
				if (docCopyOrigin == null)
				{
					continue;
				}

				// Đính kèm văn bản gốc vào văn bản mới
				_docrelationRepository.Create(new DocRelation()
				{
					DocumentCopyId = docCopyOrigin.DocumentCopyId,
					DocumentId = docOrigin.DocumentId,
					RelationId = docCopy.DocumentId,
					RelationCopyId = docCopy.DocumentCopyId,
					RelationType = (int)RelationTypes.LienQuanHoiBao,
					CategoryName = document.CategoryName,
					CitizenName = document.CitizenName,
					Compendium = document.Compendium,
					DateArrived = DateTime.Now,
					DocCode = document.DocCode,
					InOutCode = document.InOutCode
				});


				// Đính kèm văn bản mới vào văn bản gốc
				_docrelationRepository.Create(new DocRelation()
				{
					DocumentId = docCopy.DocumentId,
					DocumentCopyId = docCopy.DocumentCopyId,
					RelationCopyId = docCopyOrigin.DocumentCopyId,
					RelationId = docOrigin.DocumentId,
					RelationType = (int)RelationTypes.LienQuanHoiBao,
					CategoryName = docOrigin.CategoryName,
					CitizenName = docOrigin.CitizenName,
					Compendium = docOrigin.Compendium,
					DateArrived = DateTime.Now,
					DocCode = docOrigin.DocCode,
					InOutCode = docOrigin.InOutCode
				});

				if (docOrigin.IsHSMC)
				{
					docOrigin.Status = (int)DocumentStatus.DangXuLy;
					docOrigin.IsGettingOut = false;
					docCopy.DocumentCopyType = (int)DocumentCopyTypes.XuLyChinh;
					docCopy.Status = (int)DocumentStatus.DangXuLy;
				}

				Context.SaveChanges();
			}
		}
        public class TimeKey {
            public string timekey { get; set; }
            public Guid document { get; set; }
        }
        #region Bỏ

        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="fees"></param>
        ///// <param name="doctypeId"></param>
        //private void AddFees(IEnumerable<DocFee> fees, Guid doctypeId)
        //{
        //    if (fees.Any())
        //    {
        //        var newFeeName = fees.Select(f => f.FeeName);

        //        var existFeeName = _feeRepository.GetsAs(f => f.FeeName, f => newFeeName.Contains(f.FeeName));
        //        fees = fees.Where(f => !existFeeName.Contains(f.FeeName)).ToList();
        //        Context.Configuration.AutoDetectChangesEnabled = false;
        //        foreach (var fee in fees)
        //        {
        //            _feeRepository.Create(new Fee
        //            {
        //                FeeName = fee.FeeName,
        //                Price = fee.Price,
        //                DocTypeId = doctypeId,
        //                CreatedOnDate = DateTime.Now,
        //                FeeTypeId = 1,
        //                IsRequired = true
        //            });
        //        }
        //        Context.Configuration.AutoDetectChangesEnabled = true;
        //    }
        //}

        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="papers"></param>
        ///// <param name="doctypeId"></param>
        //private void AddPapers(IEnumerable<DocPaper> papers, Guid doctypeId)
        //{
        //    if (papers.Any())
        //    {
        //        var newPaperName = papers.Select(p => p.PaperName);

        //        var existPapers = _paperRepository.Gets(true, p => newPaperName.Contains(p.PaperName));
        //        var existPaperName = existPapers.Select(p => p.PaperName);

        //        papers = papers.Where(p => !existPaperName.Contains(p.PaperName)).ToList();

        //        Context.Configuration.AutoDetectChangesEnabled = false;

        //        // Add giấy tờ vào loại hồ sơ hiện tại
        //        foreach (var existPaper in existPapers)
        //        {
        //            if (!_doctypePaperRepository.Exist(dp => dp.DocTypeId == doctypeId && dp.PaperId == existPaper.PaperId))
        //            {
        //                _doctypePaperRepository.Create(new DoctypePaper()
        //                {
        //                    PaperId = existPaper.PaperId,
        //                    PaperName = existPaper.PaperName,
        //                    IsRequired = true,
        //                    Amount = existPaper.Amount,
        //                    DocTypeId = doctypeId
        //                });
        //            }
        //        }

        //        // Thêm giấy tờ mới vào danh sách
        //        foreach (var paper in papers)
        //        {
        //            var newPaper = new Paper
        //            {
        //                PaperName = paper.PaperName,
        //                Amount = paper.Amount,
        //                CreatedOnDate = DateTime.Now,
        //                DocTypeId = doctypeId,
        //                IsRequired = true,
        //                PaperTypeId = 1
        //            };

        //            _paperRepository.Create(newPaper);
        //            Context.SaveChanges();

        //            _doctypePaperRepository.Create(new DoctypePaper()
        //            {
        //                PaperId = newPaper.PaperId,
        //                DocTypeId = doctypeId
        //            });
        //        }

        //        Context.Configuration.AutoDetectChangesEnabled = true;
        //    }
        //}

        #endregion

        #endregion
    }
}