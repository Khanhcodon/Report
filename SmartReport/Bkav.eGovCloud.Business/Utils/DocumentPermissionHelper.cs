using System;
using System.Collections.Generic;
using System.Linq;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Document;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Core.Workflow;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Business.Objects.CacheObjects;

namespace Bkav.eGovCloud.Business
{
	/// <author>
	/// Bkav Corp. - BSO - eGov - Department 2
	/// Project: eGov Cloud v1.0
	/// Class : DocumentPermissionHelper - public - Core
	/// Access Modifiers:
	///     *Inherit: None
	/// Create Date : 230113
	/// Author      : CuongNT
	/// </author>
	/// <summary>
	/// <para>Kiểm tra quyền tác động lên văn bản/hồ sơ đối với 1 cán bộ nào đó.</para>
	/// <para>(CuongNT@bkav.com - 230113)</para>
	/// </summary>
	public class DocumentPermissionHelper : ServiceBase
	{
		#region Readonly &  Fields

		// TODO: Chuyển các dal này về dùng Bll
		private AdminGeneralSettings _generalSettings;

		private readonly IRepository<DocumentCopy> _documentCopyRepository;
		private readonly IRepository<Document> _documentRepository;
		private readonly AuthorizeBll _authorizeService;
		private readonly WorkflowHelper _workflowHelper;
		private readonly SearchSettings _searchSettings;
		private readonly UserBll _userService;
		private readonly IRepository<Store> _storeService;

		private readonly IRepository<DocTimeline> _docTimelineRepository;

		#endregion Readonly &  Fields

		#region C'tors

		/// <summary>
		/// Hàm khởi tạo
		/// </summary>
		/// <param name="context"></param>
		/// <param name="authorizeService"></param>
		/// <param name="workflowHelper"></param>
		/// <param name="generalSettings"></param>
		/// <param name="searchSettings"></param>
		/// <param name="userService"></param>
		public DocumentPermissionHelper(
			IDbCustomerContext context,
			AuthorizeBll authorizeService,
			WorkflowHelper workflowHelper,
			 AdminGeneralSettings generalSettings,
			SearchSettings searchSettings,
			UserBll userService)
			: base(context)
		{
			// _docFinishRepository = Context.GetRepository<DocFinish>();
			_documentCopyRepository = Context.GetRepository<DocumentCopy>();
			_documentRepository = Context.GetRepository<Document>();
			_workflowHelper = workflowHelper;
			_docTimelineRepository = Context.GetRepository<DocTimeline>();
			_authorizeService = authorizeService;
			_generalSettings = generalSettings;
			_searchSettings = searchSettings;
			_userService = userService;
			_storeService = Context.GetRepository<Store>();
		}

		#endregion C'tors

		#region Class Methods

		/// <summary>
		/// <para>Kiểm tra tất cả các chức năng được xử lý trên 1 văn bản/hồ sơ</para>
		/// <para>GiangPN@bkav.com - 030413</para>
		/// </summary>
		/// <param name="document">Văn bản cần kiểm tra</param>
		/// <param name="documentCopy">Hướng chuyển của cán bộ cần kiểm tra </param>
		/// <param name="userId">Id user cần kiểm tra (người đăng nhập/người ủy quyền)</param>
		/// <returns>
		/// Dạng enum - là các nghiệp vụ tác động lên văn bản/hồ sơ
		/// </returns>
		public DocumentPermissions CheckAll(Document document, DocumentCopy documentCopy, int userId)
		{
			return Check(document, documentCopy, userId, DocumentPermissions.AllPermission);
		}

		///<summary>
		/// <para>Hàm kiểm tra các quyền nghiệp vụ dùng cho contextmenu hiển thị đối với mỗi văn bản/hồ sơ</para>
		/// <para>GiangPN@bkav.com - 030413</para>
		///</summary>
		///<param name="document">Văn bản</param>
		///<param name="documentCopy">Văn </param>
		///<param name="userId">Id user cần kiểm tra (người đăng nhập/người ủy quyền)</param>
		///<returns>Dạng enum - là các nghiệp vụ tác động lên văn bản/hồ sơ</returns>
		public DocumentPermissions CheckForContextMenu(Document document, DocumentCopy documentCopy, int userId)
		{
			return Check(document, documentCopy, userId, DocumentPermissions.ContextPermission);
		}

		/// <summary>
		/// Trả về các nghiệp vụ dùng cho contextmenu khi kiểm tra nhiều văn bản
		/// </summary>
		/// <param name="document">Văn bản</param>
		/// <param name="documentCopy">Document copy</param>
		/// <param name="userId">Id user cần kiểm tra (người đăng nhập/ người ủy quyền)</param>
		/// <returns>Dạng enum - là các nghiệp vụ tác động lên văn bản/hồ sơ</returns>
		public DocumentPermissions CheckForContextMenuManyDocument(Document document, DocumentCopy documentCopy, int userId)
		{
			return Check(document, documentCopy, userId, DocumentPermissions.ContextPermissionForMany);
		}

		///<summary>
		/// <para>Hàm kiểm tra các quyền nghiệp vụ tác động lên văn bản hồ sơ(không có quyền khởi tạo, đính kèm, lưu số phát hành)</para>
		/// <para>GiangPN@bkav.com - 030413</para>
		///</summary>
		///<param name="document">Văn bản</param>
		///<param name="documentCopy">Văn </param>
		///<param name="userId">Id user cần kiểm tra (người đăng nhập/người ủy quyền)</param>
		///<returns>Dạng enum - là các nghiệp vụ tác động lên văn bản/hồ sơ</returns>
		public DocumentPermissions CheckForToolbar(Document document, DocumentCopy documentCopy, int userId)
		{
			return Check(document, documentCopy, userId, DocumentPermissions.ToolbarPermission);
		}

		/// <summary>
		/// Quyền mặc định khi xem văn bản
		/// </summary>
		/// <param name="document"></param>
		/// <param name="userId"></param>
		/// <returns></returns>
		public DocumentPermissions CheckForView(DocumentCached document, int userId)
		{
			var result = (DocumentPermissions)0;

			result |= DocumentPermissions.XemVanBan;
			result |= DocumentPermissions.KetThucXuLy;
			result |= DocumentPermissions.ThongBao;
			result |= DocumentPermissions.GuiYKien;
			result |= DocumentPermissions.LuuSo;
			result |= DocumentPermissions.LuuHoSoCaNhan;

			// Thêm thiết lập cho phép người khởi tạo có thể sửa hồ sơ, văn bản bất cứ lúc nào
			if (_generalSettings.UserCreatedHasChangeDocument && document.UserCreatedId == userId)
			{
				result |= DocumentPermissions.Luuvanban;
			}

			if (document.NodeCurrentPermission != null && EnumHelper<NodePermissions>.ContainFlags((NodePermissions)document.NodeCurrentPermission, NodePermissions.QuyenThayDoiNoiDung))
			{
				result |= DocumentPermissions.SuaVanBan;
			}

			return result;
		}


		///// <summary>
		///// <para>Kiểm tra quyền xem văn bản của cán bộ (userId) trên một văn bản (documentId) tại một hướng chuyển (documentCopyId) tương ứng</para>
		///// <para>GiangPN@bkav.com - 030413</para>
		///// </summary>
		///// <param name="documentId">Id hướng chuyển của văn bản</param>
		///// <param name="userId">Id user cần kiểm tra (người đăng nhập/người ủy quyền)</param>
		///// <returns></returns>
		//public bool CheckForQuyenXem(Guid documentId, int userId)
		//{
		//    var document = _documentRepository.Get(documentId);
		//    return CheckForQuyenXem(document, userId);
		//}

		/// <summary>
		/// <para>Kiểm tra quyền xem văn bản của cán bộ (userId) trên một văn bản (documentId) tại một hướng chuyển (documentCopyId) tương ứng</para>
		/// <para>GiangPN@bkav.com - 030413</para>
		/// </summary>
		/// <param name="document">Id hướng chuyển của văn bản</param>
		/// <param name="userId">Id user cần kiểm tra (người đăng nhập/người ủy quyền)</param>
		/// <returns></returns>
		public bool CheckForQuyenXem(Document document, int userId)
		{
			if (document == null)
			{
				throw new ArgumentNullException("document");
			}

			// Hệ thống không yêu cầu kiểm tra quyền xem văn bản.
			// => Cho phép tất cả mọi người xem dc văn bản của nhau
			if (!_generalSettings.HasCheckViewDocumentPermission)
			{
				return true;
			}

			// Kiểm tra người dùng hiện tại có được check quyền xem tất cả hay không.
			var user = _userService.GetFromCache(userId);
			if (user != null && (user.CanReadEveryDocument == true || _searchSettings.HasPermission(user)))
			{
				return true;
			}

			var documentId = document.DocumentId;

			var userStr = DocumentCopy.UserCompareString(userId);
			return _documentCopyRepository.Exist(dc => dc.DocumentId == documentId &&
						(dc.DocumentUsers != null && dc.DocumentUsers.Contains(userStr)));
		}

		/// <summary>
		/// <para>Kiểm tra quyền xem văn bản của cán bộ (userId) trên một văn bản (documentId) tại một hướng chuyển (documentCopyId) tương ứng</para>
		/// <para>GiangPN@bkav.com - 030413</para>
		/// </summary>
		/// <param name="document">Id hướng chuyển của văn bản</param>
		/// <param name="userId">Id user cần kiểm tra (người đăng nhập/người ủy quyền)</param>
		/// <returns></returns>
		public bool CheckForQuyenXem(DocumentCached document, int userId)
		{
			if (document == null)
			{
				throw new ArgumentNullException("document");
			}

			// Hệ thống không yêu cầu kiểm tra quyền xem văn bản.
			// => Cho phép tất cả mọi người xem dc văn bản của nhau
			if (!_generalSettings.HasCheckViewDocumentPermission)
			{
				return true;
			}

			// Kiểm tra người dùng hiện tại có được check quyền xem tất cả hay không.
			var user = _userService.GetFromCache(userId);
			if (user != null && (user.CanReadEveryDocument == true || _searchSettings.HasPermission(user)))
			{
				return true;
			}

			var documentId = document.DocumentId;
			var result = document.DocumentUserList().Any(u => u == userId);
			return result;
		}

		/// <summary>
		/// <para>Kiểm tra quyền xem văn bản của cán bộ (userId) trên một văn bản (documentId) 
		/// tại một hướng chuyển (documentCopyId) tương ứng hoặc ủy quyền xử lý</para>
		/// <para>HopCV@bkav.com - 011015</para>
		/// </summary>
		/// <param name="documentId">Id hướng chuyển của văn bản</param>
		/// <param name="userId">Id user cần kiểm tra (người đăng nhập/người ủy quyền)</param>
		/// <returns></returns>
		public bool CheckForQuyenXemAndUyQuyenXem(Guid documentId, int userId)
		{
			// Thêm quyền cho phép người bất kỳ có thể hủy văn bản, hồ sơ
			var user = _userService.GetFromCache(userId);
			if (user.CanReadEveryDocument == true)
			{
				return true;
			}

			var document = _documentRepository.Get(documentId);
			return CheckForQuyenXemAndUyQuyenXem(document, userId);
		}

		/// <summary>
		/// <para>Kiểm tra quyền xem văn bản của cán bộ (userId) trên một văn bản (documentId) 
		/// tại một hướng chuyển (documentCopyId) tương ứng
		/// Hoặc ủy quyền xử lý
		/// </para>
		/// <para>HopCV@bkav.com - 011015</para>
		/// </summary>
		/// <param name="document">Id hướng chuyển của văn bản</param>
		/// <param name="userId">Id user cần kiểm tra (người đăng nhập/người ủy quyền)</param>
		/// <returns></returns>
		public bool CheckForQuyenXemAndUyQuyenXem(Document document, int userId)
		{
			if (document == null)
			{
				throw new ArgumentNullException("document");
			}

			if (CheckForQuyenXem(document, userId))
			{
				return true;
			}

			if (CheckForUyQuyenXem(document, userId))
			{
				return true;
			}

			return false;
		}

		/// <summary>
		/// <para>Kiểm tra quyền xem văn bản của cán bộ (userId) trên một văn bản (documentId) 
		/// tại một hướng chuyển (documentCopyId) tương ứng
		/// </para>
		/// <para>HopCV@bkav.com - 011015</para>
		/// </summary>
		/// <param name="document">Id hướng chuyển của văn bản</param>
		/// <param name="userId">Id user cần kiểm tra (người đăng nhập/người ủy quyền)</param>
		/// <returns></returns>
		private bool CheckForUyQuyenXem(Document document, int userId)
		{
			bool exist = false;
			var authorizeUserIds = _authorizeService.GetAuthorizeUsers(userId, document.DocTypeId.Value);
			if (authorizeUserIds != null && authorizeUserIds.Any())
			{
				foreach (var authoUserId in authorizeUserIds)
				{
					var userStr = DocumentCopy.UserCompareString(authoUserId);
					exist = _documentCopyRepository.Exist(dc => dc.DocumentId == document.DocumentId &&
						(dc.DocumentUsers != null && dc.DocumentUsers.Contains(userStr)));

					if (exist)
					{
						break;
					}
				}
			}

			return exist;
		}

		/// <summary>
		/// <para>Kiểm tra quyền xem văn bản của cán bộ (userId) trên một văn bản (documentId) 
		/// tại một hướng chuyển (documentCopyId) tương ứng ủy quyền xử lý
		/// </para>
		/// <para>HopCV@bkav.com - 011015</para>
		/// </summary>
		/// <param name="documentCopy"> văn bản</param>
		/// <param name="userId">Id user cần kiểm tra (người đăng nhập/người ủy quyền)</param>
		/// <returns></returns>
		private bool CheckForUyQuyenXem(DocumentCopy documentCopy, int userId)
		{
			//bổ sung check thêm phần ủy quyền của người dùng
			var result = false;
			var authorizeUserIds = _authorizeService.GetAuthorizeUsers(userId, documentCopy.DocTypeId);
			if (authorizeUserIds == null || !authorizeUserIds.Any())
			{
				return result;
			}

			foreach (var authorUserId in authorizeUserIds)
			{
				result = documentCopy.HasQuyenXem(authorUserId) || documentCopy.HasThamGiaXuLy(authorUserId);

				if (result)
				{
					break;
				}
			}

			return result;
		}

		/// <summary>
		/// <para>Kiểm tra quyền xem văn bản của cán bộ (userId) trên một văn bản (documentId) tại một hướng chuyển (documentCopyId) tương ứng</para>
		/// <para>GiangPN@bkav.com - 030413</para>
		/// </summary>
		/// <param name="documentCopyId">Id hướng chuyển của văn bản</param>
		/// <param name="userId">Id user cần kiểm tra (người đăng nhập/người ủy quyền)</param>
		/// <returns></returns>
		public bool CheckForQuyenXem(int documentCopyId, int userId)
		{
			var documentCopy = _documentCopyRepository.Get(documentCopyId);
			return CheckForQuyenXem(documentCopy, userId);
		}

		/// <summary>
		/// <para>Kiểm tra quyền xem văn bản của cán bộ (userId) trên một văn bản (documentId) tại một hướng chuyển (documentCopyId) tương ứng</para>
		/// <para>GiangPN@bkav.com - 030413</para>
		/// </summary>
		/// <param name="documentCopy">Id hướng chuyển của văn bản</param>
		/// <param name="userId">Id user cần kiểm tra (người đăng nhập/người ủy quyền)</param>
		/// <returns></returns>
		public bool CheckForQuyenXem(DocumentCopy documentCopy, int userId)
		{
			if (documentCopy == null)
			{
				throw new ArgumentNullException("documentCopy");
			}

			// Hệ thống không yêu cầu kiểm tra quyền xem văn bản.
			// => Cho phép tất cả mọi người xem dc văn bản của nhau
			if (!_generalSettings.HasCheckViewDocumentPermission)
			{
				return true;
			}

			// Kiểm tra người dùng hiện tại có được check quyền xem tất cả hay không.
			var user = _userService.GetFromCache(userId);
			if (user != null && (user.CanReadEveryDocument == true || _searchSettings.HasPermission(user)))
			{
				return true;
			}

			if (documentCopy.HasQuyenXem(userId))
			{
				return true;
			}

			// kiểm tra quyền khi xem cho ngưòi tạo sổ và xem sổ văn bản
			var storeId = documentCopy.Document.StoreId;
			if (storeId.HasValue)
			{
				var store = _storeService.Get(storeId);
				if (store != null && (store.ListUserViewIds.Contains(userId) || store.UserId == userId))
				{
					return true;
				}
			}

			//bổ sung check thêm phần ủy quyền của người dùng
			if (CheckForUyQuyenXem(documentCopy, userId))
			{
				return true;
			}

			return false;
		}

		/// <summary>
		/// <para>Kiểm tra quyền xem văn bản của cán bộ (userId) trên một văn bản (documentId) tại một hướng chuyển (documentCopyId) tương ứng</para>
		/// <para>GiangPN@bkav.com - 030413</para>
		/// </summary>
		/// <param name="documentCopyId">Id hướng chuyển của văn bản</param>
		/// <param name="userId">Id user cần kiểm tra (người đăng nhập/người ủy quyền)</param>
		/// <returns></returns>
		public bool CheckForQuyenXuly(int documentCopyId, int userId)
		{
			var documentCopy = _documentCopyRepository.Get(documentCopyId);
			return CheckForQuyenXuly(documentCopy, userId);
		}

		/// <summary>
		/// <para>Kiểm tra quyền xem văn bản của cán bộ (userId) trên một văn bản (documentId) tại một hướng chuyển (documentCopyId) tương ứng</para>
		/// <para>GiangPN@bkav.com - 030413</para>
		/// </summary>
		/// <param name="documentCopy">Id hướng chuyển của văn bản</param>
		/// <param name="userId">Id user cần kiểm tra (người đăng nhập/người ủy quyền)</param>
		/// <returns></returns>
		public bool CheckForQuyenXuly(DocumentCopy documentCopy, int userId)
		{
			if (documentCopy == null)
			{
				throw new ArgumentNullException("documentCopy");
			}
			
			//Nếu không có người giữ văn bản hiện tại => tất cả những người được set quyền nhìn thấy văn bản đều có quyền xử lý
			if (documentCopy.UserCurrentId == 0)
			{
				return true;
			}

			return documentCopy.UserCurrentId == userId;
		}

		/// <summary>
		/// <para>Kiểm tra quyền xem văn bản của cán bộ (userId) trên một văn bản (documentId) tại một hướng chuyển (documentCopyId) tương ứng</para>
		/// <para>CuongNT@bkav.com - 210613</para>
		/// </summary>
		/// <param name="documentCopyId">Id hướng chuyển của văn bản</param>
		/// <param name="userId">Id user cần kiểm tra (người đăng nhập/người ủy quyền)</param>
		/// <param name="realUserId">Id của cán bộ ủy quyền xử lý văn bản </param>
		/// <returns>True nếu có quyền xử lý văn bản do đang giữ hoặc được ủy quyền. False ngược lại.</returns>
		public bool CheckForUyQuyenXuLy(int documentCopyId, int userId, out int realUserId)
		{
			var documentCopy = _documentCopyRepository.Get(documentCopyId);
			return CheckForUyQuyenXuLy(documentCopy, userId, out realUserId);
		}

		/// <summary>
		/// <para>Kiểm tra quyền xem văn bản của cán bộ (userId) trên một văn bản (documentId) tại một hướng chuyển (documentCopyId) tương ứng</para>
		/// <para>CuongNT@bkav.com - 210613</para>
		/// </summary>
		/// <param name="documentCopy">Id hướng chuyển của văn bản</param>
		/// <param name="userId">Id user cần kiểm tra (người đăng nhập/người ủy quyền)</param>
		/// <param name="realUserId">Id của cán bộ ủy quyền xử lý văn bản </param>
		/// <returns>True nếu có quyền xử lý văn bản do đang giữ hoặc được ủy quyền. False ngược lại.</returns>
		public bool CheckForUyQuyenXuLy(DocumentCopy documentCopy, int userId, out int realUserId)
		{
			realUserId = userId;
			if (documentCopy == null)
			{
				throw new ArgumentNullException("documentCopy");
			}

			if (documentCopy.DocumentCopyTypeInEnum == DocumentCopyTypes.XuLyChinh ||
				documentCopy.DocumentCopyTypeInEnum == DocumentCopyTypes.DongXuLy ||
				documentCopy.DocumentCopyTypeInEnum == DocumentCopyTypes.XinYKien)
			{
				var authorizeUserIds = _authorizeService.GetAuthorizeUsers(userId, documentCopy.DocTypeId);

				if ((authorizeUserIds != null && authorizeUserIds.Any())
						&& authorizeUserIds.Contains(documentCopy.UserCurrentId))
				{
					if (CheckForQuyenXuly(documentCopy, documentCopy.UserCurrentId))
					{
						realUserId = documentCopy.UserCurrentId;
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>
		/// <para>Trả về quyền xử lý văn bản theo ủy quyền nếu có, hoặc quyền xem văn bản</para>
		/// <para>CuongNT@bkav.com - 270613</para>
		/// </summary>
		/// <param name="documentCopyId"></param>
		/// <param name="userId"></param>
		/// <param name="realUserId"> </param>
		/// <returns></returns>
		public bool CheckForUyQuyenVaXuLyVanBan(int documentCopyId, int userId, out int realUserId)
		{
			var documentCopy = _documentCopyRepository.Get(documentCopyId);
			return CheckForUyQuyenVaXuLyVanBan(documentCopy, userId, out realUserId);
		}

		/// <summary>
		/// <para>Trả về quyền xử lý văn bản theo ủy quyền nếu có, hoặc quyền xem văn bản</para>
		/// <para>CuongNT@bkav.com - 270613</para>
		/// </summary>
		/// <param name="documentCopy"></param>
		/// <param name="userId"></param>
		/// <param name="realUserId"> </param>
		/// <returns></returns>
		public bool CheckForUyQuyenVaXuLyVanBan(DocumentCopy documentCopy, int userId, out int realUserId)
		{
			if (documentCopy == null)
			{
				throw new ArgumentNullException("documentCopy");
			}

			if (CheckForQuyenXuly(documentCopy, userId))
			{
				realUserId = userId;
				return true;
			}

			if (CheckForUyQuyenXuLy(documentCopy, userId, out realUserId))
			{
				return true;
			}

			return false;
		}

		/// <summary>
		/// Check quyền xử lý văn bản từ cache
		/// </summary>
		/// <param name="document"></param>
		/// <param name="userId"></param>
		/// <param name="realUserId"></param>
		/// <returns></returns>
		public bool CheckForUyQuyenVaXuLyVanBan(DocumentCached document, int userId, out int realUserId)
		{
			if (document == null)
			{
				throw new ArgumentNullException("documentCopy");
			}
			realUserId = userId;
			if (document.UserCurrentId == 0 || document.UserCurrentId == userId)
			{
				return true;
			}

			if (!document.DocTypeId.HasValue)
			{
				return false;
			}

			var authorizeUserIds = _authorizeService.GetAuthorizeUsers(userId, document.DocTypeId.Value);
			if ((authorizeUserIds != null && authorizeUserIds.Any())
					&& authorizeUserIds.Contains(document.UserCurrentId))
			{
				realUserId = document.UserCurrentId;
				return true;
			}

			return false;
		}

		///// <summary>
		///// <para>Tra ve quyen khoi tao van ban</para>
		///// </summary>
		///// <param name="docTypeId"></param>
		///// <param name="userId"> </param>
		///// <returns>True neu co quyen khoi tao. False neu nguoc lai.</returns>
		//public bool CheckForQuyenKhoiTaoVanBan(Guid docTypeId, int userId)
		//{
		//    int workflowId;
		//    var startNode = _workflowHelper.GetStartNodes(docTypeId, userId, out workflowId);
		//    return startNode != null && startNode.Any();
		//}

		/// <summary>
		/// <para>Tra ve quyen khoi tao van ban</para>
		/// </summary>
		/// <param name="workflow"></param>
		/// <param name="userId"> </param>
		/// <returns>True neu co quyen khoi tao. False neu nguoc lai.</returns>
		public IEnumerable<Node> CheckForQuyenKhoiTaoVanBan(Workflow workflow, int userId)
		{
			if (workflow == null)
			{
				throw new ArgumentNullException("workflow");
			}
			return _workflowHelper.GetStartNodes(workflow, userId);
		}

		/// <summary>
		/// <para>Kiểm tra có quyền tác động nghiệp vụ nào đó lên hồ sơ hay không</para>
		/// <para>GiangPN@bkav.com - 030413</para>
		/// </summary>
		/// <param name="documentCopy">Văn bản copy</param>
		/// <param name="permissionsNeedToCheck">Các nghiệp vụ văn bản/hồ sơ cần kiểm tra</param>
		/// <param name="userId">Id user đăng nhập</param>
		/// <returns>Các nghiệp vụ được phép thực hiện</returns>
		public DocumentPermissions Check(DocumentCopy documentCopy, int userId, DocumentPermissions permissionsNeedToCheck)
		{
			if (documentCopy == null)
			{
				throw new ArgumentNullException("documentCopy");
			}
			return Check(documentCopy.Document, documentCopy, userId, permissionsNeedToCheck);
		}

		/// <summary>
		/// <para>Kiểm tra có quyền tác động nghiệp vụ nào đó lên hồ sơ hay không</para>
		/// <para>GiangPN@bkav.com - 030413</para>
		/// </summary>
		/// <param name="document">Văn bản</param>
		/// <param name="documentCopy">Văn bản copy</param>
		/// <param name="permissionsNeedToCheck">Các nghiệp vụ văn bản/hồ sơ cần kiểm tra</param>
		/// <param name="userId">Id user đăng nhập</param>
		/// <returns>Các nghiệp vụ được phép thực hiện</returns>
		public DocumentPermissions Check(Document document, DocumentCopy documentCopy, int userId, DocumentPermissions permissionsNeedToCheck)
		{
			if (document == null)
			{
				throw new ArgumentNullException("document");
			}
			if (documentCopy == null)
			{
				throw new ArgumentNullException("documentCopy");
			}
			if (document.DocumentId != documentCopy.DocumentId)
			{
				throw new ArgumentException("Ban sao va ban chinh van ban khong khop.");
			}

			var result = (DocumentPermissions)0;

			//Nếu là văn bản đến và chưa được phân loại
			if (!documentCopy.Document.DocTypeId.HasValue)
			{
				result = result | DocumentPermissions.XemVanBan;
				result = result | DocumentPermissions.GuiYKien;
				result = result | DocumentPermissions.ThongBao;
				result = result | DocumentPermissions.Luuvanban;

				if (documentCopy.StatusInEnum == DocumentStatus.DangXuLy)
				{
					result = result | DocumentPermissions.DinhKem;
					result = result | DocumentPermissions.SuaVanBan;
					result = result | DocumentPermissions.XinYKien;
					result = result | DocumentPermissions.PhanLoai;
					result = result | DocumentPermissions.KetThucXuLy;
				}
				return result;
			}

			// 0
			if (EnumHelper<DocumentPermissions>.ContainFlags(permissionsNeedToCheck, DocumentPermissions.XemVanBan))
			{
				if (CheckForQuyenXem(documentCopy, userId))
				{
					result = result | DocumentPermissions.XemVanBan;
				}
				else
				{
					return 0;
				}
			}

			// 1
			if (EnumHelper<DocumentPermissions>.ContainFlags(permissionsNeedToCheck, DocumentPermissions.BanGiao))
			{
				if (CheckQuyenBanGiao(documentCopy, userId))
				{
					result = result | DocumentPermissions.BanGiao;
				}
			}

			// 2
			if (EnumHelper<DocumentPermissions>.ContainFlags(permissionsNeedToCheck, DocumentPermissions.DungXuLy))
			{
				if (CheckQuyenDungXuLy(document, documentCopy, userId))
				{
					result = result | DocumentPermissions.DungXuLy;
				}
			}

			// 3
			if (EnumHelper<DocumentPermissions>.ContainFlags(permissionsNeedToCheck, DocumentPermissions.XinGiaHanXuLy))
			{
				if (CheckQuyenXinGiaHanXuLy(documentCopy, userId))
				{
					result = result | DocumentPermissions.XinGiaHanXuLy;
				}
			}

			// 4
			if (EnumHelper<DocumentPermissions>.ContainFlags(permissionsNeedToCheck, DocumentPermissions.GuiYKien))
			{
				if (CheckQuyenGuiYKien(documentCopy))
				{
					result = result | DocumentPermissions.GuiYKien;
				}
			}

			// 5
			if (EnumHelper<DocumentPermissions>.ContainFlags(permissionsNeedToCheck, DocumentPermissions.HuyVanBan))
			{
				if (CheckQuyenHuyVanBan(document, documentCopy, userId))
				{
					result = result | DocumentPermissions.HuyVanBan;
				}
			}

			// 6
			if (EnumHelper<DocumentPermissions>.ContainFlags(permissionsNeedToCheck, DocumentPermissions.KetThucXuLy))
			{
				if (CheckQuyenKetThucXuLy(documentCopy, userId))
				{
					result = result | DocumentPermissions.KetThucXuLy;
				}
			}

			// 7
			if (EnumHelper<DocumentPermissions>.ContainFlags(permissionsNeedToCheck, DocumentPermissions.KiDuyet))
			{
				if (CheckQuyenKiDuyet(documentCopy, userId))
				{
					result = result | DocumentPermissions.KiDuyet;
				}
			}

			// 8
			//if (EnumHelper<DocumentPermissions>.ContainFlags(permissionsNeedToCheck, DocumentPermissions.LayLaiVanBan))
			//{
			//    if (CheckQuyenLayLaiVanBan(documentCopy, userId))
			//    {
			//        result = result | DocumentPermissions.LayLaiVanBan;
			//    }
			//}

			// 9
			if (EnumHelper<DocumentPermissions>.ContainFlags(permissionsNeedToCheck, DocumentPermissions.LuuHoSoCaNhan))
			{
				if (CheckQuyenLuuHoSoCaNhan(documentCopy))
				{
					result = result | DocumentPermissions.LuuHoSoCaNhan;
				}
			}

			// 10
			if (EnumHelper<DocumentPermissions>.ContainFlags(permissionsNeedToCheck, DocumentPermissions.PhanLoai))
			{
				if (CheckQuyenPhanLoai(documentCopy, userId))
				{
					result = result | DocumentPermissions.PhanLoai;
				}
			}

			//11
			if (EnumHelper<DocumentPermissions>.ContainFlags(permissionsNeedToCheck, DocumentPermissions.SuaVanBan))
			{
				if (CheckQuyenSuaVanBan(documentCopy, userId))
				{
					result = result | DocumentPermissions.SuaVanBan;
				}
			}

			// 12
			if (EnumHelper<DocumentPermissions>.ContainFlags(permissionsNeedToCheck, DocumentPermissions.ThongBao))
			{
				if (CheckQuyenThongBao(documentCopy))
				{
					result = result | DocumentPermissions.ThongBao;
				}
			}

			// 13
			if (EnumHelper<DocumentPermissions>.ContainFlags(permissionsNeedToCheck, DocumentPermissions.TiepNhanBoSung))
			{
				if (CheckQuyenTiepNhanBoSung(document, documentCopy, userId))
				{
					result = result | DocumentPermissions.TiepNhanBoSung;
				}
			}

			// 14
			if (EnumHelper<DocumentPermissions>.ContainFlags(permissionsNeedToCheck, DocumentPermissions.TraKetQua))
			{
				if (CheckQuyenTraKetQua(document, documentCopy, userId))
				{
					result = result | DocumentPermissions.TraKetQua;
				}
			}

			// 15
			if (EnumHelper<DocumentPermissions>.ContainFlags(permissionsNeedToCheck, DocumentPermissions.TraLoiVanBan))
			{
				if (CheckQuyenTraLoiVanBan(documentCopy, userId))
				{
					result = result | DocumentPermissions.TraLoiVanBan;
				}
			}

			// 16
			if (EnumHelper<DocumentPermissions>.ContainFlags(permissionsNeedToCheck, DocumentPermissions.XacNhanBanGiao))
			{
				if (CheckQuyenXacNhanBanGiao(documentCopy, userId))
				{
					result = result | DocumentPermissions.XacNhanBanGiao;
				}
			}

			// 17
			if (EnumHelper<DocumentPermissions>.ContainFlags(permissionsNeedToCheck, DocumentPermissions.XacNhanXuLy))
			{
				if (CheckQuyenXacNhanXuLy(document, documentCopy, userId))
				{
					result = result | DocumentPermissions.XacNhanXuLy;
				}
			}

			// 18
			if (EnumHelper<DocumentPermissions>.ContainFlags(permissionsNeedToCheck, DocumentPermissions.XinYKien))
			{
				if (CheckQuyenXinYKien(documentCopy, userId))
				{
					result = result | DocumentPermissions.XinYKien;
				}
			}

			// 19
			if (EnumHelper<DocumentPermissions>.ContainFlags(permissionsNeedToCheck, DocumentPermissions.YeuCauBoSung))
			{
				if (CheckQuyenYeuCauBoSung(document, documentCopy, userId))
				{
					result = result | DocumentPermissions.YeuCauBoSung;
				}
			}

			// 20
			if (EnumHelper<DocumentPermissions>.ContainFlags(permissionsNeedToCheck, DocumentPermissions.CapNhatKetQuaXuLyCuoi))
			{
				if (CheckQuyenCapNhatKetQuaXuLyCuoi(document, documentCopy, userId))
				{
					result = result | DocumentPermissions.CapNhatKetQuaXuLyCuoi;
				}
			}

			//21
			if (EnumHelper<DocumentPermissions>.ContainFlags(permissionsNeedToCheck, DocumentPermissions.Luuvanban))
			{
				if (CheckQuyenLuuvanban(documentCopy, userId))
				{
					result = result | DocumentPermissions.Luuvanban;
				}
			}

			//22
			if (EnumHelper<DocumentPermissions>.ContainFlags(permissionsNeedToCheck, DocumentPermissions.TraLoiYKien))
			{
				if (CheckQuyenTraLoiYKien(documentCopy, userId))
				{
					result = result | DocumentPermissions.TraLoiYKien;
				}
			}

			//23
			if (EnumHelper<DocumentPermissions>.ContainFlags(permissionsNeedToCheck, DocumentPermissions.CapPhep))
			{
				if (CheckQuyenCapPhep(document, documentCopy, userId))
				{
					result = result | DocumentPermissions.CapPhep;
				}
			}

			//24
			if (EnumHelper<DocumentPermissions>.ContainFlags(permissionsNeedToCheck, DocumentPermissions.DoiHanXuLyKhiPhanLoai))
			{
				if (CheckQuyenThayDoiHanXuLy(documentCopy, userId))
				{
					result = result | DocumentPermissions.DoiHanXuLyKhiPhanLoai;
				}
			}

			//25
			if (EnumHelper<DocumentPermissions>.ContainFlags(permissionsNeedToCheck, DocumentPermissions.PhatHanh))
			{
				if (CheckQuyenPhatHanh(documentCopy, userId))
				{
					result = result | DocumentPermissions.PhatHanh;
				}
			}

			//25
			if (EnumHelper<DocumentPermissions>.ContainFlags(permissionsNeedToCheck, DocumentPermissions.LuuSo))
			{
				if (CheckQuyenLuuSoNoiBo(documentCopy, userId))
				{
					result = result | DocumentPermissions.LuuSo;
				}
			}
			//26
			if (EnumHelper<DocumentPermissions>.ContainFlags(permissionsNeedToCheck, DocumentPermissions.MoLaiVanBan))
			{
				if (CheckForMoLaiVanBan(documentCopy, userId))
				{
					result = result | DocumentPermissions.MoLaiVanBan;
				}
			}
			////28
			//if (EnumHelper<DocumentPermissions>.ContainFlags(permissionsNeedToCheck, DocumentPermissions.HenTiep))
			//{
			//    if (CheckForHenTiep(document, documentCopy, userId))
			//    {
			//        result = result | DocumentPermissions.HenTiep;
			//    }
			//}

			if (EnumHelper<DocumentPermissions>.ContainFlags(permissionsNeedToCheck, DocumentPermissions.DanhLaiSoDen))
			{
				if (CheckForDanhLaiSoDen(document, documentCopy, userId))
				{
					result = result | DocumentPermissions.DanhLaiSoDen;
				}
			}

			return result;
		}

		/// <summary>
		/// QuangP: Kiểm tra có quyền mở lại văn bản hay không
		/// </summary>
		/// <param name="documentCopy">documentCopy</param>
		/// <param name="userId">Id user đăng nhập</param>
		public bool CheckForMoLaiVanBan(DocumentCopy documentCopy, int userId)
		{
			//Tạm để chỉ cho mở lại văn bản trong vòng 10 phút
			var time = -10;
			return (documentCopy.UserCurrentId == userId && documentCopy.StatusInEnum == DocumentStatus.KetThuc && documentCopy.DateFinished > DateTime.Now.AddMinutes(time));
		}

		/// <summary>
		/// Kiểm tra có quyền kết thúc văn bản hay không
		/// HopCV:021214
		/// </summary>
		/// <param name="documentCopyId">Id cuar vawn banr copy</param>
		/// <param name="userId">Id user đăng nhập</param>
		public bool CheckForKetThucVanBan(int documentCopyId, int userId)
		{
			var documentCopy = _documentCopyRepository.Get(documentCopyId);
			return CheckForKetThucVanBan(documentCopy, userId);
		}

		/// <summary>
		/// Kiểm tra có quyền kết thúc văn bản hay không
		/// HopCV:021214
		/// </summary>
		/// <param name="documentCopy">Văn bản copy</param>
		/// <param name="userId">Id user đăng nhập</param>
		/// <returns></returns>
		public bool CheckForKetThucVanBan(DocumentCopy documentCopy, int userId)
		{
			if (documentCopy == null)
				throw new ArgumentNullException("documentCopy");

			return CheckQuyenKetThucXuLy(documentCopy, userId);
		}

		private bool CheckQuyenLuuSoNoiBo(DocumentCopy documentCopy, int userId)
		{
			// Cán bộ đang xử lý văn bản chính
			const DocumentCopyTypes combined = DocumentCopyTypes.XuLyChinh;
			const DocumentStatus combined3 = DocumentStatus.DangXuLy;

			var documentCopyType = documentCopy.DocumentCopyTypeInEnum;

			// Nếu là văn bản chính
			if (EnumHelper<DocumentCopyTypes>.ContainFlags(combined, documentCopyType) &&
				EnumHelper<DocumentStatus>.ContainFlags(combined3, documentCopy.StatusInEnum) &&
				CheckForQuyenXuly(documentCopy, userId))//documentCopy.UserCurrentId == userId &&
			{
				if (documentCopy.NodeCurrentPermission == null)
				{
					return false;
				}

				//Kiểm tra Node hiện tại của văn bản/hồ sơ có quyền kết thúc xử lý hay không
				var nodeCurrentPermission = (NodePermissions)documentCopy.NodeCurrentPermission;
				if (EnumHelper<NodePermissions>.ContainFlags(nodeCurrentPermission, NodePermissions.QuyenCapSoTruoc))
				{
					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// Trả về giá trị xác định người đang xử lý văn bản hiện tại có quyền phát hành hay không.
		/// </summary>
		/// <param name="documentCopy">Document Copy</param>
		/// <param name="userId">User Id</param>
		/// <returns></returns>
		private bool CheckQuyenPhatHanh(DocumentCopy documentCopy, int userId)
		{
			// Cán bộ đang xử lý văn bản chính
			const DocumentCopyTypes combined = DocumentCopyTypes.XuLyChinh;
			const DocumentStatus combined3 = DocumentStatus.DangXuLy;

			var documentCopyType = documentCopy.DocumentCopyTypeInEnum;
			// Nếu là văn bản chính
			if (EnumHelper<DocumentCopyTypes>.ContainFlags(combined, documentCopyType) &&
				EnumHelper<DocumentStatus>.ContainFlags(combined3, documentCopy.StatusInEnum) &&
				CheckForQuyenXuly(documentCopy, userId))//documentCopy.UserCurrentId == userId &&
			{
				//Kiểm tra Node hiện tại của văn bản/hồ sơ có quyền kết thúc xử lý hay không
				var nodeCurrentPermission = (NodePermissions)documentCopy.NodeCurrentPermission;
				if (EnumHelper<NodePermissions>.ContainFlags(nodeCurrentPermission, NodePermissions.QuyenLuuSoPhatHanh))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Trả về quyền cho phép cấp phép hồ sơ hiện tại
		/// </summary>
		/// <param name="document">Document</param>
		/// <param name="documentCopy">Document Copy</param>
		/// <param name="userId">User Id</param>
		/// <returns></returns>
		private bool CheckQuyenCapPhep(Document document, DocumentCopy documentCopy, int userId)
		{
			//HopCV: 260914
			//Kiểm tra DocTypePermission nếu không có giá trị thì trả về false luôn
			if (!document.DocTypePermission.HasValue)
				return false;

			// Check quyền cấp phép trên loại hồ sơ
			var doctypePermission = (DocTypePermissions)document.DocTypePermission;
			if (!EnumHelper<DocTypePermissions>.ContainFlags(doctypePermission, DocTypePermissions.DuocPhepCapPhep))
			{
				return false;
			}

			// Hồ sơ là xử lý chính
			const DocumentCopyTypes combined = DocumentCopyTypes.XuLyChinh;

			// Đang ở trạng thái xử lý
			const DocumentStatus combined2 = DocumentStatus.DangXuLy;
			var documentCopyType = documentCopy.DocumentCopyTypeInEnum;

			// Và cán bộ hiện tại đang là người xử lý hồ sơ
			return EnumHelper<DocumentCopyTypes>.ContainFlags(combined, documentCopyType) && CheckForQuyenXuly(documentCopy, userId) &&
					EnumHelper<DocumentStatus>.ContainFlags(combined2, documentCopy.StatusInEnum);
		}

		/// <summary>
		/// <para>Kiểm tra quyền bàn giao văn bản của 1 user đối với 1 văn bản</para>
		/// <para>GiangPN@bkav.com - 030413</para>
		/// </summary>
		/// <param name="documentCopy">Văn bản copy</param>
		/// <param name="userId"> </param>
		/// <returns>True: là được phép, False: là không được phép</returns>
		/// <editor>
		/// <para> Tienbv@bkav.com 170413: thêm hiển thị nút bàn giao khi hồ sơ đang trong trạng thái dừng xử lý (khi này chỉ load ra những hướng chuyển dừng xử lý khác)</para>
		/// <para> Tienbv@bkav.com 250413: thêm hiển thị nút bàn giao khi hồ sơ đang trong trạng thái cập nhật kết quả dừng xử lý => chỉ load ra hướng chuyển cập nhật kết quả dừng xử lý.</para>
		/// </editor>
		/// <remarks>
		/// Văn bản hướng chính + đồng xử lý và đang giữ (dự thảo, đang xử lý) thì được phép bàn giao.
		/// </remarks>
		private bool CheckQuyenBanGiao(DocumentCopy documentCopy, int userId)
		{
			const DocumentCopyTypes combined = DocumentCopyTypes.XuLyChinh | DocumentCopyTypes.DongXuLy | DocumentCopyTypes.ChoKetQuaDungXuLy | DocumentCopyTypes.XinYKien;
			const DocumentStatus combined2 = DocumentStatus.DuThao | DocumentStatus.DangXuLy | DocumentStatus.DungXuLy;

			return EnumHelper<DocumentCopyTypes>.ContainFlags(combined, documentCopy.DocumentCopyTypeInEnum) &&
				   EnumHelper<DocumentStatus>.ContainFlags(combined2, documentCopy.StatusInEnum) &&
				   CheckForQuyenXuly(documentCopy, userId);
		}

		/// <summary>
		/// <para>Kiểm tra quyền cập nhật kết quả xử lý cuối của 1 user đối với 1 văn bản</para>
		/// <para>GiangPN@bkav.com - 030413</para>
		/// </summary>
		/// <param name="document">Văn bản</param>
		/// <param name="documentCopy">Văn bản copy</param>
		/// <param name="userId">Id cán bộ </param>
		/// <returns>True: là được phép, False: là không được phép</returns>
		/// <remarks>
		/// Cán bộ đang giữ văn bản chính thuộc loại văn bản bắt buộc phải cập nhật kết quả xử lý cuối tại nút có quyền
		/// này thì có quyền "Cập nhật kết quả xử lý cuối".
		/// </remarks>
		private bool CheckQuyenCapNhatKetQuaXuLyCuoi(Document document, DocumentCopy documentCopy, int userId)
		{
			if (documentCopy.NodeCurrentPermission == null)
			{
				return false;
			}

			const DocumentCopyTypes combined = DocumentCopyTypes.XuLyChinh;
			const DocumentStatus combined2 = DocumentStatus.DangXuLy | DocumentStatus.DungXuLy;
			var documentCopyType = documentCopy.DocumentCopyTypeInEnum;

			// Cán bộ đang giữ văn bản chính
			if (EnumHelper<DocumentCopyTypes>.ContainFlags(combined, documentCopyType) &&
				EnumHelper<DocumentStatus>.ContainFlags(combined2, documentCopy.StatusInEnum) &&
				CheckForQuyenXuly(documentCopy, userId))//documentCopy.UserCurrentId == userId
			{
				// Tại nút có quyền này
				// TienBV: khi hồ sơ được chuyển ra node Trả kết quả mà không qua ký duyệt thì phải cập nhật kết quả xử lý cuối trước.
				// Nhưng nếu check quyền QuyenCapNhatKetQuaXuLyCuoi nữa thì phải tick cả 2 quyền trên node => thừa và khó vận hành.
				// => Đổi thành check quyền trả kết quả.
				var nodeCurrentPermission = (NodePermissions)documentCopy.NodeCurrentPermission;
				//if (EnumHelper<NodePermissions>.ContainFlags(nodeCurrentPermission, NodePermissions.QuyenCapNhatKetQuaXuLyCuoi))
				if (EnumHelper<NodePermissions>.ContainFlags(nodeCurrentPermission, NodePermissions.QuyenTraKetQua | NodePermissions.QuyenCapNhatKetQuaXuLyCuoi))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// <para>Kiểm tra quyền dừng xử lý của 1 user đối với 1 văn bản</para>
		/// <para>GiangPN@bkav.com - 030413</para>
		/// </summary>
		/// <param name="document">Văn bản</param>
		/// <param name="documentCopy">Văn bản copy</param>
		/// <param name="userId"> </param>
		/// <returns>True: là được phép, False: là không được phép</returns>
		/// <remarks>
		/// Cán bộ đang giữ văn bản chính thuộc loại văn bản được phép yêu cầu bổ sung và tại nút có quyền
		/// này thì có quyền "Yêu cầu bổ sung" hay "Dừng xử lý".
		/// </remarks>
		private bool CheckQuyenDungXuLy(Document document, DocumentCopy documentCopy, int userId)
		{
			// Kiểm tra trường hợp đã trả kết quả hoặc đã có kết quả xử lý
			if (document.IsReturned == true)
			{
				return false;
			}

			if (documentCopy.NodeCurrentPermission == null)
			{
				return false;
			}

			// Cán bộ đang giữ văn bản chính
			const DocumentCopyTypes combined = DocumentCopyTypes.XuLyChinh;
			const DocumentStatus combined2 = DocumentStatus.DangXuLy | DocumentStatus.DungXuLy;

			var documentCopyType = documentCopy.DocumentCopyTypeInEnum;
			if (EnumHelper<DocumentCopyTypes>.ContainFlags(combined, documentCopyType) &&
				EnumHelper<DocumentStatus>.ContainFlags(combined2, documentCopy.StatusInEnum) &&
				CheckForQuyenXuly(documentCopy, userId))//documentCopy.UserCurrentId == userId
			{
				// TienBV: bỏ check Yêu cầu bổ sung, khi yêu cầu bổ sung thì dừng xử lý, nhưng ngược lại không đúng.
				// Thuộc loại văn được phép yêu cầu bổ sung
				//var docTypePermission = (DocTypePermissions)document.DocTypePermission;
				//if (EnumHelper<DocTypePermissions>.ContainFlags(docTypePermission, DocTypePermissions.DuocPhepYeuCauBoSung))
				//{
				// Kiểm tra Node hiện tại có quyền yêu cầu bổ sung hay không
				var nodeCurrentPermission = (NodePermissions)documentCopy.NodeCurrentPermission;
				if (EnumHelper<NodePermissions>.ContainFlags(nodeCurrentPermission, NodePermissions.QuyenDungXuLy))
				{
					return true;
				}
				// }
			}

			return false;
		}

		/// <summary>
		/// <para>Kiểm tra quyền Xin gia hạn xử lý của 1 user đối với 1 văn bản</para>
		/// <para>GiangPN@bkav.com - 030413</para>
		/// </summary>
		/// <param name="documentCopy">Văn bản copy</param>
		/// <param name="userId"> </param>
		/// <returns>True: là được phép, False: là không được phép</returns>
		/// <remarks>
		/// Cán bộ đang giữ văn bản chính thì có quyền xin Gia hạn xử lý.
		/// </remarks>
		private bool CheckQuyenXinGiaHanXuLy(DocumentCopy documentCopy, int userId)
		{
			if (!documentCopy.Document.DocTypeId.HasValue)
			{
				return false;
			}

			// Cán bộ đang giữ văn bản chính
			const DocumentCopyTypes combined = DocumentCopyTypes.XuLyChinh;
			const DocumentStatus combined2 = DocumentStatus.DangXuLy | DocumentStatus.DungXuLy;

			var documentCopyType = documentCopy.DocumentCopyTypeInEnum;
			if (_generalSettings.OnlyUserCreateChangeDateAppointed)
			{
				return EnumHelper<DocumentCopyTypes>.ContainFlags(combined, documentCopyType) &&
				   EnumHelper<DocumentStatus>.ContainFlags(combined2, documentCopy.StatusInEnum) &&
				   documentCopy.Document.UserCreatedId == userId;
			}

			return EnumHelper<DocumentCopyTypes>.ContainFlags(combined, documentCopyType) &&
				   EnumHelper<DocumentStatus>.ContainFlags(combined2, documentCopy.StatusInEnum) &&
				   CheckForQuyenXuly(documentCopy, userId);
		}

		/// <summary>
		/// <para>Kiểm tra quyền gửi ý kiến của 1 user đối với 1 văn bản</para>
		/// <para>GiangPN@bkav.com - 030413</para>
		/// </summary>
		/// <param name="documentCopy"> </param>
		/// <returns>True: là được phép, False: là không được phép</returns>
		/// <remarks>
		/// Cán bộ tham gia xử lý văn bản thì có quyền gửi ý kiến. Trừ văn bản dự thảo.
		/// </remarks>
		private bool CheckQuyenGuiYKien(DocumentCopy documentCopy)
		{
			return (documentCopy.StatusInEnum != DocumentStatus.DuThao) && documentCopy.Document.DocTypeId.HasValue;
		}

		/// <summary>
		/// <para>Kiểm tra quyền hủy văn bản</para>
		/// <para>GiangPN@bkav.com - 010213</para>
		/// </summary>
		/// <param name="document"> </param>
		/// <param name="documentCopy"></param>
		/// <param name="userId">Id cán bộ </param>
		/// <returns><c>True</c> nếu có quyền hủy văn bản. <c>False</c> nếu ngược lại.</returns>
		/// <remarks>
		/// Cán bộ khởi tạo văn bản và đang giữ bản chính thì mới có quyền hủy văn bản.
		/// </remarks>
		private bool CheckQuyenHuyVanBan(Document document, DocumentCopy documentCopy, int userId)
		{
			// Thêm quyền cho phép người bất kỳ có thể hủy văn bản, hồ sơ
			// var user = _userService.GetCacheAllUsers().SingleOrDefault(u => u.UserId == userId);
			//if (user != null && user.CanReadEveryDocument == true)
			//{
			//    return true;
			//}

			// Cán bộ đang giữ văn bản chính và là người tạo văn bản
			const DocumentCopyTypes combined = DocumentCopyTypes.XuLyChinh;

			var documentCopyType = documentCopy.DocumentCopyTypeInEnum;

			var isXlc = EnumHelper<DocumentCopyTypes>.ContainFlags(combined, documentCopyType);
			var currentUserIsCreator = document.UserCreatedId == userId;
			var hasQuyenXuLy = CheckForQuyenXuly(documentCopy, userId);

#if HoSoMotCuaEdition

			//TienBV: xử lý cho hồ sơ
			const DocumentStatus combined3 = DocumentStatus.DangXuLy | DocumentStatus.DuThao;

			if (document.CategoryBusinessIdInEnum == CategoryBusinessTypes.Hsmc)
			{
				return isXlc && hasQuyenXuLy && currentUserIsCreator &&
					(EnumHelper<DocumentStatus>.ContainFlags(combined3, documentCopy.StatusInEnum) &&
						documentCopy.UserCurrentId == userId);
				// documentCopy.Histories.HistoryPath.Count() == 2); // hồ sơ chưa được chuyển đi đâu
			}
#endif

			const DocumentStatus combined2 = DocumentStatus.DangXuLy | DocumentStatus.DuThao;

			return isXlc &&
					EnumHelper<DocumentStatus>.ContainFlags(combined2, documentCopy.StatusInEnum) &&
					hasQuyenXuLy && currentUserIsCreator;
		}

		/// <summary>
		/// <para>Kiểm tra quyền kết thúc xử lý của 1 user đối với 1 văn bản</para>
		/// <para>GiangPN@bkav.com - 030413</para>
		/// </summary>
		/// <param name="documentCopy">Văn bản copy</param>
		/// <param name="userId"> </param>
		/// <returns>True: là được phép, False: là không được phép</returns>
		private bool CheckQuyenKetThucXuLy(DocumentCopy documentCopy, int userId)
		{
			var document = documentCopy.Document;

			//Nếu là văn bản đến mà không có trường Doctype thì cho phép kết thúc hồ sơ rác
			if (document.CategoryBusinessId == (int)CategoryBusinessTypes.VbDen
						&& documentCopy.DocTypeId.Equals(Guid.Empty)
						&& documentCopy.UserCurrentId == userId)
			{
				return true;
			}

			// Cán bộ đang giữ văn bản chính, bản thông báo hoặc bản phát hành
			const DocumentCopyTypes combined = DocumentCopyTypes.XuLyChinh;
			const DocumentCopyTypes combined2 = DocumentCopyTypes.DongXuLy;
			const DocumentStatus combined3 = DocumentStatus.DangXuLy | DocumentStatus.DungXuLy | DocumentStatus.DuThao;

			var documentCopyType = documentCopy.DocumentCopyTypeInEnum;

			// Nếu là văn bản thông báo, chờ phát hành
			if (documentCopy.UserThongBaos().Contains(userId))
			{
				return true;
			}

			//Check quyen xu ly va trangj thai van ban co phai trang thai dang xu ly hay khong
			if (!CheckForQuyenXuly(documentCopy, userId)
				|| !EnumHelper<DocumentStatus>.ContainFlags(combined3, documentCopy.StatusInEnum))
			{
				return false;
			}

			// Trường hợp tạo mới và kết thúc luôn
			// Hệ thống tiến hành lưu dự thảo trước rồi mới kết thúc.
			if (documentCopy.StatusInEnum == DocumentStatus.DuThao && document.UserCreatedId == userId)
			{
				return true;
			}

			// Nếu là văn bản đồng xử lý
			if (EnumHelper<DocumentCopyTypes>.ContainFlags(combined2, documentCopyType) && documentCopy.IsCurrentUser(userId))
			{
				return true;
			}

			// Nếu là văn bản chính
			if (EnumHelper<DocumentCopyTypes>.ContainFlags(combined, documentCopyType))
			{
				if (_generalSettings.UserCreatetedHasClose)
				{
					if (documentCopy.Document.UserCreatedId == userId)
					{
						return true;
					}
					return false;
				}

				//Kiểm tra Node hiện tại của văn bản/hồ sơ có quyền kết thúc xử lý hay không
				if (documentCopy.NodeCurrentPermission == null)
				{
					if (documentCopy.UserCurrentId == userId)
					{
						return true;
					}
					return false;
				}

				var nodeCurrentPermission = (NodePermissions)documentCopy.NodeCurrentPermission;
				if (EnumHelper<NodePermissions>.ContainFlags(nodeCurrentPermission, NodePermissions.QuyenKetThucXuLy))
				{
					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// <para>Kiểm tra quyền kí duyệt của 1 user đối với 1 hồ sơ</para>
		/// <para>GiangPN@bkav.com - 010213</para>
		/// </summary>
		/// <param name="documentCopy">Văn bản/Hồ sơ copy</param>
		/// <param name="userId"> </param>
		/// <returns>True: là được phép, False: là không được phép</returns>
		/// <remarks>
		/// Cán bộ giữ văn bản chính hoặc đồng xử lý tại nút có quyền kí duyệt thì được phép kí duyệt
		/// </remarks>
		private bool CheckQuyenKiDuyet(DocumentCopy documentCopy, int userId)
		{
			//Kiểm tra Node hiện tại của văn bản/hồ sơ có quyền kí duyệt hay không
			if (documentCopy.NodeCurrentPermission == null)
			{
				return false;
			}

			// TienBV: sửa lại chỉ có văn bản chính mới dc ký duyệt
			const DocumentCopyTypes combined = DocumentCopyTypes.XuLyChinh; // | DocumentCopyTypes.DongXuLy;
			const DocumentStatus combined2 = DocumentStatus.DangXuLy;
			var documentCopyType = documentCopy.DocumentCopyTypeInEnum;

			// Nếu là văn bản chính
			if (EnumHelper<DocumentCopyTypes>.ContainFlags(combined, documentCopyType) && CheckForQuyenXuly(documentCopy, userId) &&
				EnumHelper<DocumentStatus>.ContainFlags(combined2, documentCopy.StatusInEnum))
			{
				// Và nút có quyền kí duyệt
				var nodeCurrentPermission = (NodePermissions)documentCopy.NodeCurrentPermission;
				if (EnumHelper<NodePermissions>.ContainFlags(nodeCurrentPermission, NodePermissions.QuyenKiDuyet))
				{
					return true;
				}
			}

			return false;
		}

		//// TODO: Bỏ quyền check lấy lại văn bản do thực sự khôgn cần thiết vì đây không hẳn là quyền
		///// <summary>
		///// <para>Kiểm tra quyền lấy lại văn bản của 1 user đối với 1 hồ sơ.</para>
		///// <para>Hàm này chỉ kiểm tra theo mục hồ sơ thuộc vào. Việc quyết định có context-item lấy lại nào sẽ do hàm lấy danh sách lấy lại thực hiện chi tiết sau.</para>
		///// <para>GiangPN@bkav.com - 010213</para>
		///// </summary>
		///// <param name="documentCopy">Văn bản copy</param>
		///// <param name="userId">Id của user đăng nhập</param>
		///// <returns>True: là được phép, False: là không được phép</returns>
		///// <remarks>
		///// Văn bản đang trong mục theo dõi + văn bản chưa được người nhận xem + người gửi là người đăng nhập
		///// </remarks>
		///// <remarks>
		///// // Văn bản đang xử lý: --> Lấy lại thông báo, xin ý kiến nếu thời gian gửi cách hiện tại dưới n phút quy định. n: cấu hình được.
		///// // Văn bản đang trong mục theo dõi: văn bản chưa được người nhận xem + người gửi là người đăng nhập --> Lấy lại bàn giao.
		///// </remarks>
		//private bool CheckQuyenLayLaiVanBan(DocumentCopy documentCopy, int userId)
		//{
		//    int userSendId;
		//    return CheckForUyQuyenVaXuLyVanBan(documentCopy, userId, out userSendId) || CheckForVuaBanGiao(documentCopy, userId);
		//}

		/// <summary>
		/// <para>Kiểm tra quyền lưu hồ sơ cá nhân của 1 user đối với 1 văn bản</para>
		/// <para>GiangPN@bkav.com - 010213</para>
		/// </summary>
		/// <param name="documentCopy"> </param>
		/// <returns></returns>
		private bool CheckQuyenLuuHoSoCaNhan(DocumentCopy documentCopy)
		{
			return documentCopy.StatusInEnum != DocumentStatus.DuThao;
		}

		/// <summary>
		/// <para>Kiểm tra quyền phân loại văn bản của 1 user đối với 1 văn bản</para>
		/// <para>GiangPN@bkav.com - 010213</para>
		/// </summary>
		/// <param name="documentCopy">Văn bản copy</param>
		/// <param name="userId"> </param>
		/// <returns>True: là được phép, False: là không được phép</returns>
		/// <remarks>
		///
		/// </remarks>
		private bool CheckQuyenPhanLoai(DocumentCopy documentCopy, int userId)
		{
			if (documentCopy.DocumentCopyTypeInEnum == DocumentCopyTypes.ThongBao)
			{
				return true;
			}

			//Nếu là văn bản liên thông đến, những ai được set quyền xử lý (văn thư) đều có thể phân loại
			if (documentCopy.Document.Original == 2 && !documentCopy.Document.DocTypeId.HasValue)
			{
				return true;
			}

			if (documentCopy.NodeCurrentPermission == null)
			{
				return false;
			}

			// TienBV: Thêm trường hợp đồng xử lý cũng được quyền phân loại lại theo quy trình của mình
			const DocumentCopyTypes combined = DocumentCopyTypes.XuLyChinh | DocumentCopyTypes.DongXuLy;
			const DocumentStatus combined2 = DocumentStatus.DangXuLy;
			var documentCopyType = documentCopy.DocumentCopyTypeInEnum;

			// Nếu là văn bản chính
			if (EnumHelper<DocumentCopyTypes>.ContainFlags(combined, documentCopyType) && CheckForQuyenXuly(documentCopy, userId) &&
				EnumHelper<DocumentStatus>.ContainFlags(combined2, documentCopy.StatusInEnum))
			{
				var nodeCurrentPermission = (NodePermissions)documentCopy.NodeCurrentPermission;
				return EnumHelper<NodePermissions>.ContainFlags(nodeCurrentPermission, NodePermissions.QuyenPhanLoai);
			}

			return false;
		}

		/// <summary>
		/// <para>Kiểm tra quyền thay đổi hạn xử lý văn bản của 1 user đối với 1 văn bản khi thay đổi luồng văn bản (chức năng phân loại)</para>
		/// <para>DungHV@bkav.com - 200214</para>
		/// </summary>
		/// <param name="documentCopy">Văn bản copy</param>
		/// <param name="userId"> </param>
		/// <returns>True: là được phép, False: là không được phép</returns>
		/// <remarks>
		///
		/// </remarks>
		private bool CheckQuyenThayDoiHanXuLy(DocumentCopy documentCopy, int userId)
		{
			var checkd = CheckQuyenPhanLoai(documentCopy, userId);
			if (checkd)
			{
				if (documentCopy.DocumentCopyTypeInEnum == DocumentCopyTypes.ThongBao)
				{
					return true;
				}

				if (documentCopy.Document.Original == 2 && !documentCopy.Document.DocTypeId.HasValue)
				{
					return true;
				}

				var nodeCurrentPermission = (NodePermissions)documentCopy.NodeCurrentPermission;
				return EnumHelper<NodePermissions>.ContainFlags(nodeCurrentPermission, NodePermissions.QuyenChoPhepCapNhatHanXuLyKhiThayDoiLuong);
			}
			return false;
		}

		/// <summary>
		/// <para>Kiểm tra quyền sửa của 1 user đối với 1 văn bản/hồ sơ</para>
		/// <para>GiangPN@bkav.com - 010213</para>
		/// </summary>
		/// <param name="documentCopy">Văn bản copy</param>
		/// <param name="userId"> </param>
		/// <returns></returns>
		private bool CheckQuyenSuaVanBan(DocumentCopy documentCopy, int userId)
		{
			//Kiểm tra Node hiện tại có quyền sửa văn bản không
			if (documentCopy.NodeCurrentPermission == null)
			{
				return false;
			}

			const DocumentCopyTypes combined = DocumentCopyTypes.XuLyChinh | DocumentCopyTypes.DongXuLy | DocumentCopyTypes.XinYKien;
			const DocumentStatus combined2 = DocumentStatus.DangXuLy | DocumentStatus.DungXuLy;
			var documentCopyType = documentCopy.DocumentCopyTypeInEnum;
			if (EnumHelper<DocumentCopyTypes>.ContainFlags(combined, documentCopyType) && CheckForQuyenXuly(documentCopy, userId) &&
				EnumHelper<DocumentStatus>.ContainFlags(combined2, documentCopy.StatusInEnum))
			{
				var nodeCurrentPermission = (NodePermissions)documentCopy.NodeCurrentPermission;
				if (EnumHelper<NodePermissions>.ContainFlags(nodeCurrentPermission, NodePermissions.QuyenThayDoiNoiDung))
				{
					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// <para>Kiểm tra quyền thông báo văn bản của 1 user đối với 1 văn bản</para>
		/// <para>GiangPN@bkav.com - 010213</para>
		/// </summary>
		/// <param name="documentCopy"> </param>
		/// <returns>True: là được phép, False: là không được phép</returns>
		/// <remarks>
		/// + Văn bản chính hoặc DXL thì phải là đang giữ mới được thông báo
		/// + Ngoài ra nếu là văn bản Thông báo thì luôn có quyền thông báo.
		/// </remarks>
		private bool CheckQuyenThongBao(DocumentCopy documentCopy)
		{
			return documentCopy.StatusInEnum != DocumentStatus.DuThao;
		}

		/// <summary>
		/// <para>Kiểm tra quyền tiếp nhận bổ sung của 1 user đối với 1 hồ sơ</para>
		/// <para>GiangPN@bkav.com - 010213</para>
		/// </summary>
		/// <param name="document">Hồ sơ</param>
		/// <param name="documentCopy">Văn bản copy</param>
		/// <param name="userId"> </param>
		/// <returns>True: là được phép, False: là không được phép</returns>
		private bool CheckQuyenTiepNhanBoSung(Document document, DocumentCopy documentCopy, int userId)
		{
#if HoSoMotCuaEdition

			if (documentCopy.NodeCurrentPermission == null)
			{
				return false;
			}

			if (document.CategoryBusinessIdInEnum != CategoryBusinessTypes.Hsmc)
			{
				return false;
			}

			// Chưa được phân loại
			if (documentCopy.Document.DocTypeId.HasValue)
			{
				return false;
			}

			// TODO: cần gộp hàm CheckQuyenTraKetQua() lên hàm này

			const DocumentCopyTypes combined = DocumentCopyTypes.XuLyChinh;

			// TienBV: hướng xử lý hiện tại đang ở trạng thái dừng xử lý
			// Khi có yêu cầu bổ sung hồ sơ vẫn được tính là đang xử lý nhưng không tính thời gian xử lý.
			// Tức là DocumentCopy.Status = DangXuLy && Document.Status = DungXuLy.
			// Khi gửi yêu cầu bổ sung, tiếp nhận bổ sung cần chú ý cập nhật đúng cho 2 trường này.

			const DocumentStatus combined2 = DocumentStatus.DangXuLy;
			var documentCopyType = documentCopy.DocumentCopyTypeInEnum;
			if (// Đang giữ văn bản chính
				EnumHelper<DocumentCopyTypes>.ContainFlags(combined, documentCopyType) &&
				CheckForQuyenXuly(documentCopy, userId) &&
				EnumHelper<DocumentStatus>.ContainFlags(combined2, documentCopy.StatusInEnum) &&
				// Có yêu cầu bổ sung chưa tiếp nhận. false : giá trị false là để luu tiếp nhận bổ sung ko thành công
				document.IsSupplemented != null &&
				// Dang dung xu ly
				document.StatusInEnum == DocumentStatus.DungXuLy &&
				// Chưa có kết quả xử lý cuối
				!document.ResultStatus.HasValue &&
				// Chưa trả kết quả cho dân
				document.IsReturned != true)
			{
				var nodePermission = (NodePermissions)documentCopy.NodeCurrentPermission;
				if (// Có quyền trả kết quả hoặc tiếp nhận bổ sung
					EnumHelper<NodePermissions>.ContainFlags(nodePermission, NodePermissions.QuyenTraKetQua) ||
					EnumHelper<NodePermissions>.ContainFlags(nodePermission, NodePermissions.QuyenTiepNhanBoSung))
				{
					return true;
				}
			}
#endif
			return false;
		}

		/// <summary>
		/// <para>Kiểm tra quyền đặt lịch hẹn tiếp công dân đối với văn bản</para>
		/// <para>TrinhNVd - 26042016</para>
		/// </summary>
		/// <param name="document"></param>
		/// <param name="documentCopy"></param>
		/// <param name="userId"></param>
		/// <returns>True: là được phép, False: là không được phép</returns>
		private bool CheckForHenTiep(Document document, DocumentCopy documentCopy, int userId)
		{
#if HoSoMotCuaEdition
			if (documentCopy.NodeCurrentPermission == null)
			{
				return false;
			}
			const DocumentCopyTypes combined = DocumentCopyTypes.XuLyChinh;
			const DocumentStatus combined2 = DocumentStatus.DangXuLy | DocumentStatus.DungXuLy;
			var documentCopyType = documentCopy.DocumentCopyTypeInEnum;
			if (// Đang giữ văn bản chính
				EnumHelper<DocumentCopyTypes>.ContainFlags(combined, documentCopyType) &&
				CheckForQuyenXuly(documentCopy, userId) &&
				EnumHelper<DocumentStatus>.ContainFlags(combined2, documentCopy.StatusInEnum))
			{
				var nodePermission = (NodePermissions)documentCopy.NodeCurrentPermission;
				//Kiểm tra quyền đặt lịch hẹn
				if (EnumHelper<NodePermissions>.ContainFlags(nodePermission, NodePermissions.QuyenDatLichTiepCongDanKhiKetThucHoSo))
				{
					return true;
				}
			}
#endif
			return false;
		}

		/// <summary>
		/// Check quyen danh lai so den (ket thuc van ban cu)
		/// </summary>
		/// <param name="documentCopy"></param>
		/// <param name="userId"></param>
		/// <param name="document"></param>
		/// <returns></returns>
		private bool CheckForDanhLaiSoDen(Document document, DocumentCopy documentCopy, int userId)
		{
			//Kiểm tra Node hiện tại có quyền sửa văn bản không
			if (documentCopy.NodeCurrentPermission == null)
			{
				return false;
			}

			if (document.CategoryBusinessIdInEnum != CategoryBusinessTypes.VbDen
					|| document.DocTypeId.HasValue)
			{
				// Chỉ sử dụng cho VB đến và không phải là văn bản đến liên thông. VBD liên thông phân loại như là tạo mới.
				return false;
			}

			if (documentCopy.HasJustCreated == true)
			{
				// Bỏ qua TH gửi toàn xử lý chính văn bản đến.
				return false;
			}

			// Là văn bản xlc và đang xử lý
			const DocumentCopyTypes combined = DocumentCopyTypes.XuLyChinh;
			const DocumentStatus combined2 = DocumentStatus.DangXuLy;
			var documentCopyType = documentCopy.DocumentCopyTypeInEnum;
			if (EnumHelper<DocumentCopyTypes>.ContainFlags(combined, documentCopyType) && CheckForQuyenXuly(documentCopy, userId) &&
				EnumHelper<DocumentStatus>.ContainFlags(combined2, documentCopy.StatusInEnum))
			{
				var nodeCurrentPermission = (NodePermissions)documentCopy.NodeCurrentPermission;
				if (EnumHelper<NodePermissions>.ContainFlags(nodeCurrentPermission, NodePermissions.QuyenDanhLaiSoDen))
				{
					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// <para>Kiểm tra quyền trả kết quả của 1 user đối với 1 văn bản</para>
		/// <para>GiangPN@bkav.com - 010213</para>
		/// </summary>
		/// <param name="document"> </param>
		/// <param name="documentCopy">Văn bản copy</param>
		/// <param name="userId"> </param>
		/// <returns>True: là được phép, False: là không được phép</returns>
		private bool CheckQuyenTraKetQua(Document document, DocumentCopy documentCopy, int userId)
		{
#if HoSoMotCuaEdition
			if (documentCopy.NodeCurrentPermission == null)
			{
				return false;
			}

			const DocumentCopyTypes combined = DocumentCopyTypes.XuLyChinh;
			const DocumentStatus combined2 = DocumentStatus.DangXuLy | DocumentStatus.DungXuLy;
			var documentCopyType = documentCopy.DocumentCopyTypeInEnum;
			if (// Đang giữ văn bản chính
				EnumHelper<DocumentCopyTypes>.ContainFlags(combined, documentCopyType) &&
				CheckForQuyenXuly(documentCopy, userId) &&
				EnumHelper<DocumentStatus>.ContainFlags(combined2, documentCopy.StatusInEnum) &&
				// Chưa trả kết quả cho dân
				document.IsReturned != true)
			{
				var nodePermission = (NodePermissions)documentCopy.NodeCurrentPermission;
				if (// Có quyền trả kết quả hoặc tiếp nhận bổ sung
					EnumHelper<NodePermissions>.ContainFlags(nodePermission, NodePermissions.QuyenTraKetQua))
				{
					return true;
				}
			}
#endif
			return false;
		}

		/// <summary>
		/// <para>Kiểm tra quyền trả lời văn bản của 1 user đối với 1 văn bản</para>
		/// <para>GiangPN@bkav.com - 010213</para>
		/// </summary>
		/// <param name="documentCopy">Văn bản copy</param>
		/// <param name="userId"> </param>
		/// <returns>True: là được phép, False: là không được phép</returns>
		private bool CheckQuyenTraLoiVanBan(DocumentCopy documentCopy, int userId)
		{
			// TienBV: bỏ check này thì mới bổ sung thêm chức năng trả lời cho HSMC.
			//if (documentCopy.Document.CategoryBusinessIdInEnum != CategoryBusinessTypes.VbDi 
			//    && documentCopy.Document.CategoryBusinessIdInEnum != CategoryBusinessTypes.VbDen)
			//{
			//    return false;
			//}

			// Văn bản đến chưa được phân loại
			if (!documentCopy.Document.DocTypeId.HasValue)
			{
				return false;
			}

			const DocumentCopyTypes combined = DocumentCopyTypes.XuLyChinh;
			const DocumentStatus combined2 = DocumentStatus.DangXuLy;
			var documentCopyType = documentCopy.DocumentCopyTypeInEnum;
			return EnumHelper<DocumentCopyTypes>.ContainFlags(combined, documentCopyType) && CheckForQuyenXuly(documentCopy, userId) &&
				EnumHelper<DocumentStatus>.ContainFlags(combined2, documentCopy.StatusInEnum);
		}

		/// <summary>
		/// <para>Kiểm tra quyền xác nhận bàn giao văn bản của 1 user đối với 1 văn bản</para>
		/// <para>GiangPN@bkav.com - 010213</para>
		/// </summary>
		/// <param name="documentCopy">Văn bản copy</param>
		/// <param name="userId"> </param>
		/// <returns>True: là được phép, False: là không được phép</returns>
		private bool CheckQuyenXacNhanBanGiao(DocumentCopy documentCopy, int userId)
		{
			//QuangP: tạm thời cấp quyền cho văn bản
			//-Có quyền xử lý (XLC hoặc DXL)
			//-Đang xử lý
			//-Là hồ sơ 1 cửa
			//=>Todo: cho admin cấu hình loại công văn nào có quyền xác nhận bàn giao
			var result = false;
#if HoSoMotCuaEdition
			const DocumentCopyTypes permissionCombined = DocumentCopyTypes.XuLyChinh | DocumentCopyTypes.DongXuLy;
			const DocumentStatus statusCombined = DocumentStatus.DangXuLy;
			var documentCopyType = documentCopy.DocumentCopyTypeInEnum;
			result = EnumHelper<DocumentCopyTypes>.ContainFlags(permissionCombined, documentCopyType) && CheckForQuyenXuly(documentCopy, userId) &&
				EnumHelper<DocumentStatus>.ContainFlags(statusCombined, documentCopy.StatusInEnum) && (documentCopy.Document.CategoryBusinessIdInEnum == CategoryBusinessTypes.Hsmc);
#endif
			return result;
		}

		/// <summary>
		/// <para>Kiểm tra quyền xác nhận xử lý văn bản của 1 user đối với 1 hồ sơ</para>
		/// <para>CuongNT@bkav.com - 280813</para>
		/// </summary>
		/// <param name="document">Hồ sơ</param>
		/// <param name="documentCopy">Văn bản copy</param>
		/// <param name="userId"> </param>
		/// <returns>True: là được phép, False: là không được phép</returns>
		/// <remarks>
		/// Nếu văn bản thuộc hướng chính: Tìm tất cả hướng ĐXL liên quan. Nếu có + chưa hướng nào bàn giao đi --> Được phép xác nhận xử lý. Ngược lại thì không.
		/// Nếu văn bản thuộc hướng ĐXL: Tìm tất cả hướng ĐXL liên quan. Nếu có + chưa hướng nào bàn giao đi --> Xét tiếp hướng chính: Thời điểm bàn giao là thuộc nút áp cuối của lịch sử bàn giao (Hướng chính cũng chưa bàn giao đi) --> Được phép xác nhận xử lý. Ngược lại thì không.
		/// </remarks>
		private bool CheckQuyenXacNhanXuLy(Document document, DocumentCopy documentCopy, int userId)
		{
#if !HoSoMotCuaEdition
            return false;
#else

			if (document.DocTypePermission == null)
			{
				return false;
			}

			const DocumentCopyTypes combined = DocumentCopyTypes.XuLyChinh | DocumentCopyTypes.DongXuLy;
			const DocumentStatus combined2 = DocumentStatus.DangXuLy | DocumentStatus.DungXuLy;
			var documentCopyType = documentCopy.DocumentCopyTypeInEnum;
			var docTypePermission = (DocTypePermissions)document.DocTypePermission;
			var result = EnumHelper<DocumentCopyTypes>.ContainFlags(combined, documentCopyType) &&
					CheckForQuyenXuly(documentCopy, userId) &&
					EnumHelper<DocumentStatus>.ContainFlags(combined2, documentCopy.StatusInEnum) &&
					EnumHelper<DocTypePermissions>.ContainFlags(docTypePermission, DocTypePermissions.DuocPhepXacNhanXuLy);
			if (!result)
			{
				return false;
			}
			// Nếu văn bản thuộc hướng chính: Tìm tất cả hướng ĐXL liên quan. Nếu có + chưa hướng nào bàn giao đi --> Được phép xác nhận xử lý. Ngược lại thì không.
			if (documentCopy.IsHuongXuLyChinh())
			{
				var documentCopies =
					_documentCopyRepository.GetsAs(d => d.History, d => d.ParentId == documentCopy.DocumentCopyId)
						.Select(history => new DocumentCopy { History = history });
				return documentCopies.Any() && documentCopies.All(copy => copy.Histories.HistoryPath.Count <= 1);
			}
			// Nếu văn bản thuộc hướng ĐXL: Tìm tất cả hướng ĐXL liên quan. Nếu không có || Nếu có + chưa hướng nào bàn giao đi --> Xét tiếp hướng chính: Vệt xử lý cuối cùng được tạo cùng thời gian copy bản sao (Hướng chính cũng chưa bàn giao đi) --> Được phép xác nhận xử lý. Ngược lại thì không.
			else
			{
				if (!documentCopy.ParentId.HasValue)
				{
					throw new ApplicationException("ParentId của bản sao documentCopy bị null");
				}
				var parentCopy = _documentCopyRepository.Get(documentCopy.ParentId.Value);
				// Lấy danh sách hướng ĐXL liên quan ngoại trừ hướng hiện tại
				var documentCopies = _documentCopyRepository.GetsAs(d => d.History, d => d.ParentId == parentCopy.DocumentCopyId && d.DocumentCopyId != documentCopy.DocumentCopyId).Select(history => new DocumentCopy { History = history });
				if (!documentCopies.Any() || documentCopies.All(copy => copy.Histories.HistoryPath.Count <= 1))
				{
					var lastHistory = parentCopy.Histories.HistoryPath.Last();
					if (lastHistory.DateCreated.Year == documentCopy.DateCreated.Year &&
						lastHistory.DateCreated.Month == documentCopy.DateCreated.Month &&
						lastHistory.DateCreated.Day == documentCopy.DateCreated.Day &&
						lastHistory.DateCreated.Minute == documentCopy.DateCreated.Minute &&
						lastHistory.DateCreated.Second == documentCopy.DateCreated.Second &&
						lastHistory.DateCreated.Hour == documentCopy.DateCreated.Hour)
					{
						return true;
					}
				}
				return false;
			}
#endif
		}

		/// <summary>
		/// <para>Kiểm tra quyền xin ý kiến của 1 user đối với 1 văn bản</para>
		/// <para>GiangPN@bkav.com - 030413</para>
		/// </summary>
		/// <param name="documentCopy"> </param>
		/// <param name="userId"> </param>
		/// <returns>True: là được phép, False: là không được phép</returns>
		private bool CheckQuyenXinYKien(DocumentCopy documentCopy, int userId)
		{
			if (!documentCopy.Document.DocTypeId.HasValue)
			{
				return false;
			}

			const DocumentStatus combined2 = DocumentStatus.DangXuLy | DocumentStatus.DungXuLy;
			const DocumentCopyTypes combined = DocumentCopyTypes.XuLyChinh | DocumentCopyTypes.DongXuLy;
			var documentCopyType = documentCopy.DocumentCopyTypeInEnum;
			return EnumHelper<DocumentCopyTypes>.ContainFlags(combined, documentCopyType) &&
					CheckForQuyenXuly(documentCopy, userId) &&
					EnumHelper<DocumentStatus>.ContainFlags(combined2, documentCopy.StatusInEnum);
		}

		/// <summary>
		///<para>Kiểm tra quyền yêu cầu bổ sung của 1 user đối với 1 hồ sơ</para>
		/// <para>GiangPN@bkav.com - 010213</para>
		/// </summary>
		/// <editors>
		/// <para></para>
		/// </editors>
		/// <param name="document">Văn bản</param>
		/// <param name="documentCopy">Văn bản copy</param>
		///<param name="userId"> </param>
		///<returns>True: là được phép, False: là không được phép</returns>
		private bool CheckQuyenYeuCauBoSung(Document document, DocumentCopy documentCopy, int userId)
		{
#if HoSoMotCuaEdition
			if (document.CategoryBusinessIdInEnum != CategoryBusinessTypes.Hsmc)
			{
				return false;
			}
#endif
			if (!document.DocTypeId.HasValue)
			{
				return false;
			}

			if (document.DocTypePermission != null)
			{
				//TienBV: Bỏ check quyền này do thấy không cần thiết và khó hiểu khi cấu hình

				//var docTypePermission = (DocTypePermissions)document.DocTypePermission;
				//if (!EnumHelper<DocTypePermissions>.ContainFlags(docTypePermission, DocTypePermissions.DuocPhepYeuCauBoSung))
				//{
				//    return false;
				//}
			}

			//Kiểm tra trường hợp đã trả kết quả, đã có kết quả xử lý
			if (document.IsReturned == true && document.ResultStatus != null)
			{
				return false;
			}

			//Kiểm tra Node hiện tại có quyền yêu cầu bổ sung hay không
			if (documentCopy.NodeCurrentPermission != null)
			{
				var nodeCurrentPermission = (NodePermissions)documentCopy.NodeCurrentPermission;
				if (!EnumHelper<NodePermissions>.ContainFlags(nodeCurrentPermission, NodePermissions.QuyenYeuCauBoSung))
				{
					return false;
				}
			}

			const DocumentStatus combined2 = DocumentStatus.DangXuLy | DocumentStatus.DungXuLy;

			// Todo: cần hỏi rõ thêm về nghiệp vụ cho phép các hướng xử lý song song yêu cầu bổ sung thực tế thế nào?
			// Nếu một hướng đang yêu cầu bổ sung thì các hướng khác trạng thái xử lý sẽ thế nào, việc tính bù thời gian xử lý sẽ ra sao?
			// SinhND: thực tế thì hồ sơ cũng có trường hợp xử lý song song ở một số bộ phận ( như quy trình iso)
			//          nhưng những trường hợp này thường đơn giản như là hồ sơ gửi thuế (bổ sung thuế) thì vẫn có thể gửi đến bộ phận in giấy CN chẳng hạn.
			// Trường hợp anh Sinh nói là vấn đề gửi liên thông, gửi dừng xử lý với cơ quan ngoài rồi,
			// Nên tạm thời chỉ để hướng xử lý chính là hướng có thể yêu cầu bổ sung.
			const DocumentCopyTypes combined = DocumentCopyTypes.XuLyChinh; // | DocumentCopyTypes.DongXuLy;
			var documentCopyType = documentCopy.DocumentCopyTypeInEnum;
			return EnumHelper<DocumentCopyTypes>.ContainFlags(combined, documentCopyType) && CheckForQuyenXuly(documentCopy, userId) &&
					EnumHelper<DocumentStatus>.ContainFlags(combined2, documentCopy.StatusInEnum);
		}

		/// <summary>
		/// Kiểm tra quyền được lưu văn bản khi đang xem chi tiết văn bản(edit)
		/// (GiangPN@bkav.com - 170413)
		/// </summary>
		/// <param name="documentCopy">Văn bản Copy</param>
		/// <param name="userId"></param>
		/// <returns></returns>
		private bool CheckQuyenLuuvanban(DocumentCopy documentCopy, int userId)
		{
			// Thêm thiết lập cho phép người khởi tạo có thể sửa hồ sơ, văn bản bất cứ lúc nào
			if (_generalSettings.UserCreatedHasChangeDocument && documentCopy.Document.UserCreatedId == userId)
			{
				return true;
			}

			const DocumentCopyTypes combined = DocumentCopyTypes.XuLyChinh | DocumentCopyTypes.DongXuLy;
			const DocumentStatus combined2 = DocumentStatus.DungXuLy | DocumentStatus.DangXuLy | DocumentStatus.KetThuc | DocumentStatus.DuThao;

			return
				EnumHelper<DocumentCopyTypes>.ContainFlags(combined, documentCopy.DocumentCopyTypeInEnum) &&
				EnumHelper<DocumentStatus>.ContainFlags(combined2, documentCopy.StatusInEnum) &&
				CheckForQuyenXuly(documentCopy, userId);
		}

		/// <summary>
		/// <para>Kiểm tra quyền được lưu văn bản khi đang xem chi tiết văn bản(edit)</para>
		/// <para>(CuongNT@bkav.com - 230813)</para>
		/// </summary>
		/// <param name="documentCopy">Văn bản Copy</param>
		/// <param name="userId"></param>
		/// <returns></returns>
		private bool CheckQuyenTraLoiYKien(DocumentCopy documentCopy, int userId)
		{
			const DocumentCopyTypes combined = DocumentCopyTypes.DongXuLy | DocumentCopyTypes.XinYKien;
			const DocumentStatus combined2 = DocumentStatus.DangXuLy;
			// var firstHistory = documentCopy.Histories.HistoryPath.First();

			return
				EnumHelper<DocumentCopyTypes>.ContainFlags(combined, documentCopy.DocumentCopyTypeInEnum) &&
				EnumHelper<DocumentStatus>.ContainFlags(combined2, documentCopy.StatusInEnum) &&
				CheckForQuyenXuly(documentCopy, userId);
		}

		#endregion Class Methods
	}
}