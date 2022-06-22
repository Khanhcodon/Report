#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Core.Document;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using System.Data.SqlClient;

#endregion

namespace Bkav.eGovCloud.Business.Customer
{
	/// <author>
	///   <para> BSO - Phòng 2 - eGov </para>
	///   <para> Project: eGov Cloud - v1.0 </para>
	///   <para> Access Level(Class/Struct/Interface) : CommentBll - public - BLL </para>
	///   <para> Access Modifiers: </para>
	///   <para> * Inherit : [Class Name] </para>
	///   <para> * Implement : [Inteface Name], [Inteface Name], ... </para>
	///   <para> Create Date : 121225 </para>
	///   <para> Author : TienBV </para>
	/// </author>
	/// <summary>
	///   <para> Cung cấp các api quản lý ý kiến. </para>
	/// </summary>
	public class CommentBll : ServiceBase
	{
		#region Readonly & Static Fields

		private readonly IRepository<Comment> _commentRepository;
		private readonly IRepository<DocumentCopy> _documentCopyRepository;
		private readonly UserBll _userService;
		private readonly int _numberOfCommonComment = 25;

		#endregion

		#region C'tors

		/// <summary>
		///   Contructor
		/// </summary>
		public CommentBll(IDbCustomerContext context, UserBll userService)
			: base(context)
		{
			_documentCopyRepository = Context.GetRepository<DocumentCopy>();
			_commentRepository = Context.GetRepository<Comment>();
			_userService = userService;
		}

		#endregion

		#region Instance Methods

		/// <summary>
		///   <para> Xóa 1 bản ghi Comment </para>
		///   (GiangPN@bkav.com - 050613)
		/// </summary>
		/// <param name="comment"> entity Comment </param>
		public void Delete(Comment comment)
		{
			if (comment == null)
			{
				throw new ArgumentNullException("comment");
			}
			_commentRepository.Delete(comment);
			Context.SaveChanges();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="documentCopyId"> </param>
		/// <param name="dateCreated"></param>
		/// <returns></returns>
		public Comment Get(int documentCopyId, DateTime dateCreated)
		{
			return _commentRepository.Get(false, c => c.DocumentCopyId == documentCopyId && c.DateCreated == dateCreated);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="commentId"></param>
		/// <returns></returns>
		public Comment Get(int commentId)
		{
			return _commentRepository.Get(commentId);
		}

		/// <summary>
		/// Trả về ý kiến đóng góp đã gửi về hướng yêu cầu của bản sao documentCopyId. Kết quả chỉ dể đọc
		/// </summary>
		/// <param name="documentCopyId"></param>
		/// <returns></returns>
		public Comment GetAnswerForVanbanDxl(int documentCopyId)
		{
			const int commentType = (int)CommentType.Consulted;
			var consulted = _commentRepository.GetsReadOnly(c => c.CommentType == commentType && c.DocumentCopyId == documentCopyId);
			if (!consulted.Any())
			{
				return null;
			}
			if (consulted.Count() > 1)
			{
				throw new ApplicationException("Có nhiều hơn một ý kiến đóng góp trả lời của văn bản ĐXL.");
			}
			return consulted.First();

		}

		/// <summary>
		/// Trả về ý kiến trả lời đã gửi của bản sao documentCopyId.Kết quả chỉ dể đọc
		/// </summary>
		/// <param name="documentCopyId"></param>
		/// <returns></returns>
		public Comment GetAnswerForXinykien(int documentCopyId)
		{
			const int commentType = (int)CommentType.Contribution;
			var consulted = _commentRepository.GetsReadOnly(c => c.CommentType == commentType && c.DocumentCopyId == documentCopyId);
			if (!consulted.Any())
			{
				return null;
			}
			if (consulted.Count() > 1)
			{
				throw new ApplicationException("Có nhiều hơn một ý kiến đóng góp trả lời của văn bản ĐXL.");
			}
			return consulted.First();

		}

		/// <summary>
		///   TienBV 121225
		///   Lấy ra dánh sách tất cả các ý kiến xử lý của một hồ sơ, văn bản dựa theo vệt tương ứng.
		/// </summary>
		/// <remarks>
		///   <para> Cơ chế load các ý kiến xử lý: </para>
		///   <para> - Load ra các ý kiến thuộc vệt hiện tại (documentCopyId). </para>
		///   <para> - Load ra các ý kiến thuộc vệt cha của vệt hiện tại và có ngày tạo nhỏ hơn ngày tạo vệt hiện tại. </para>
		///   <para> - Cứ như thế load đến hướng xử lý chính. </para>
		/// </remarks>
		/// <param name="documentCopyId"> The document copy id. </param>
		/// <param name="userId">Current User Id</param>
		/// <returns> </returns>
		public IEnumerable<Comment> Gets(int documentCopyId, int userId)
		{
			var documentCopy = _documentCopyRepository.Get(documentCopyId);
			return Gets(documentCopy);
		}

		/// <summary>
		///   TrungVH 250214
		///   Lấy ra dánh sách tất cả các ý kiến xử lý của một hồ sơ, văn bản dựa theo vệt tương ứng. Kết quả chỉ để đọc
		/// </summary>
		/// <remarks>
		///   <para> Cơ chế load các ý kiến xử lý: </para>
		///   <para> - Load ra các ý kiến thuộc vệt hiện tại (documentCopyId). </para>
		///   <para> - Load ra các ý kiến thuộc vệt cha của vệt hiện tại và có ngày tạo nhỏ hơn ngày tạo vệt hiện tại. </para>
		///   <para> - Cứ như thế load đến hướng xử lý chính. </para>
		/// </remarks>
		/// <param name="documentCopy"> The document copy </param>
		/// <returns> </returns>
		public IEnumerable<Comment> Gets(DocumentCopy documentCopy)
		{
			if (documentCopy == null)
			{
				throw new ArgumentNullException("documentCopy");
			}

			var documentCopyParentIds = documentCopy.DocumentCopyParentIds();
			var documentId = documentCopy.DocumentId;
			var documentCopyId = documentCopy.DocumentCopyId;
			var documentCopyCreatedDate = documentCopy.DateCreated;

			var allDocumentComments = _commentRepository.Gets(true,
									   c => c.DocumentCopyId == documentCopyId
											   || documentCopyParentIds.Contains(c.DocumentCopyId)
											   || (c.DocumentCopyTargetId == documentCopyId && c.CommentType == (int)CommentType.Consulted));

			var result = allDocumentComments.Where(c => !c.ParentId.HasValue).ToList();
			
			return result.OrderByDescending(c => c.DateCreated);
		}

		/// <summary>
		/// Trả về tất cả các ý kiến của hồ sơ gốc: lấy tất cả các ý kiến của tất cả các hướng rẽ nhánh
		/// </summary>
		/// <param name="document">Hồ sơ</param>
		/// <remarks>
		/// Hàm này chỉ trả về kết quả nếu loại văn bản hiện tại cho quyền xem ý kiến của tất cả các hướng đồng xử lý liên quan.
		/// </remarks>
		/// <returns></returns>
		public IEnumerable<Comment> Gets(Document document)
		{
			var result = new List<Comment>();

			//var doctype = document.DocType;
			//if (doctype == null || !doctype.DocTypePermissionInEnum.HasValue)
			//{
			//    return result;
			//}
			//if (!EnumHelper<DocTypePermissions>.ContainFlags(doctype.DocTypePermissionInEnum.Value, DocTypePermissions.DuocPhepXemYKienCacHuongReNhanh))
			//{
			//    return result;
			//}

			var comments = _commentRepository.Gets(true, c => c.DocumentId.Equals(document.DocumentId) && !c.ParentId.HasValue
								&& (c.CommentType == (int)CommentType.Common
										|| c.CommentType == (int)CommentType.Consulted
										|| c.CommentType == (int)CommentType.Contribution
										|| c.CommentType == (int)CommentType.Finished));

			var parentIds = comments.Select(c => c.CommentId);
			var allChildren = _commentRepository.GetsReadOnly(c => c.ParentId.HasValue && parentIds.Contains(c.ParentId.Value));
			if (allChildren.Any())
			{
				foreach (var comment in comments)
				{
					comment.Children = allChildren.Where(c => c.ParentId.Value == comment.CommentId);
				}
			}

			result.AddRange(comments);

			return result;
		}

		/// <summary>
		/// Trả về danh sách các comment theo điều kiện truyền vào.
		/// </summary>
		/// <param name="isReadOnly">Trả kết kết quả chỉ đọc.</param>
		/// <param name="spec">Điều kiện.</param>
		/// <returns></returns>
		public IEnumerable<Comment> Gets(bool isReadOnly, Expression<Func<Comment, bool>> spec)
		{
			return _commentRepository.Gets(isReadOnly, spec);
		}

		/// <summary>
		/// Trả về danh sách các ý kiến thường dùng
		/// </summary>
		/// <returns></returns>
		public IEnumerable<string> GetCommons()
		{
			var userSendId = _userService.CurrentUser.UserId;
			var result = _commentRepository.GetsReadOnly(
				c =>
					c.UserSendId == userSendId
					&& c.UserReceiveId != null
					&& !string.IsNullOrEmpty(c.CommentText))
					.OrderByDescending(c => c.DateCreated)
					.GroupBy(p => p.CommentText.ToLower(), (key, g) => new
					{
						text = g.FirstOrDefault().CommentText
					})
					.Select(p => p.text)
					.Take(_numberOfCommonComment);

			return result;
		}

		/// <summary>
		///   Gửi ý kiến đóng góp (cho đồng xử lý).
		/// </summary>
		/// <param name="documentCopy"> Document copy id </param>
		/// <param name="userSendId"> User send id </param>
		/// <param name="userReceiveId"> User receive id </param>
		/// <param name="content"> Content </param>
		/// <param name="commentTransfer"> Target </param>
		/// <param name="dateCreated"> </param>
		/// <param name="content2"> Ghi log người ủy quyền thao tác</param>
		public void SendAnswerForVanbanDxl(DocumentCopy documentCopy, int userSendId, int userReceiveId, string content,
										   List<CommentTransfer> commentTransfer, DateTime dateCreated, string content2 = "")
		{
			if (!documentCopy.ParentId.HasValue)
			{
				throw new ArgumentException("documentCopy.ParentId không được phép null!");
			}
			string mainProcessDeparmentName, coProcessDeparmentName;
			var parseComment = ParseComment(content, commentTransfer, out mainProcessDeparmentName, out coProcessDeparmentName);

			var comment = new Comment
			{
				Content = parseComment,
				CommentText = content,
				UserSendId = userSendId,
				UserReceiveId = userReceiveId,
				DateCreated = dateCreated,
				DocumentCopyId = documentCopy.DocumentCopyId,
				DocumentId = documentCopy.DocumentId,
				CommentType = (byte)CommentType.Consulted,
				DocumentCopyTargetId = documentCopy.ParentId,
				Content2 = content2,
				MainProcessDepartmentName = mainProcessDeparmentName,
				CoProcessDepartmentName = coProcessDeparmentName
			};
			Create(comment);
		}

		/// <summary>
		///   Gửi ý kiến đóng góp (cho xin ý kiến)
		/// </summary>
		/// <param name="documentCopy"> Document copy id </param>
		/// <param name="userSendId"> User send id </param>
		/// <param name="userReceiveId"> User receive id </param>
		/// <param name="content"> Content </param>
		/// <param name="commentTransfer"> Target </param>
		/// <param name="dateCreated"> </param>
		/// <param name="content2"> Ghi log người ủy quyền thao tác</param>
		public void SendAnswerForXinykien(DocumentCopy documentCopy, int userSendId, int userReceiveId, string content,
										  List<CommentTransfer> commentTransfer, DateTime dateCreated, string content2 = "")
		{
			if (!documentCopy.ParentId.HasValue)
			{
				throw new ArgumentException("documentCopy.ParentId không được phép null!");
			}

			var xykComments = _commentRepository.Gets(false, c => c.UserSendId == userSendId && c.CommentType == (int)CommentType.Contribution
						&& c.DocumentCopyId == documentCopy.DocumentCopyId && c.ParentId.HasValue);

			Comment result = null;
			foreach (var xykComment in xykComments)
			{
				var parentId = xykComment.ParentId.Value;
				var parent = _commentRepository.Get(parentId);
				if (parent.DocumentCopyId == documentCopy.ParentId.Value)
				{
					result = xykComment;
					break;
				}
			}

			if (result != null)
			{
				string mainProcessDeparmentName, coProcessDeparmentName;
				var parseComment = ParseComment(content, commentTransfer, out mainProcessDeparmentName, out coProcessDeparmentName);

				result.DocumentCopyTargetId = documentCopy.ParentId;
				result.DateCreated = dateCreated;
				result.CommentText = content;
				result.Content = parseComment;
				result.UserReceiveId = userReceiveId;
				result.MainProcessDepartmentName = mainProcessDeparmentName;
				result.CoProcessDepartmentName = coProcessDeparmentName;
				Context.SaveChanges();
			}
		}

		/// <summary>
		///   Gửi ý kiến trong quá trình xử lý văn bản
		/// </summary>
		/// <param name="docCopyId"> </param>
		/// <param name="userSendId"> </param>
		/// <param name="content"> </param>
		/// <param name="dateCreated"> </param>
		/// <param name="content2"> Ghi log người ủy quyền thao tác</param>
		public Comment SendComment(int docCopyId, int userSendId, string content, DateTime dateCreated, string content2 = "")
		{
			var documentCopy = _documentCopyRepository.Get(docCopyId);
			return SendComment(documentCopy, userSendId, content, dateCreated, content2);
		}

		/// <summary>
		///   Gửi ý kiến trong quá trình xử lý văn bản
		/// </summary>
		/// <param name="documentCopy"> </param>
		/// <param name="userSendId"> </param>
		/// <param name="content"> </param>
		/// <param name="dateCreated"> </param>
		/// <param name="content2"> Ghi log người ủy quyền thao tác</param>
		public Comment SendComment(DocumentCopy documentCopy, int userSendId,
			string content, DateTime dateCreated, string content2 = "")
		{
			string mainProcessDeparmentName, coProcessDeparmentName;
			var parseComment = ParseComment(content, new List<CommentTransfer>(), out mainProcessDeparmentName, out coProcessDeparmentName);

			var comment = new Comment
			{
				Content = parseComment,
				CommentText = content,
				UserSendId = userSendId,
				UserReceiveId = null,
				DateCreated = dateCreated,
				DocumentCopyId = documentCopy.DocumentCopyId,
				DocumentId = documentCopy.DocumentId,
				CommentType = (byte)CommentType.Common,
				DocumentCopyTargetId = documentCopy.ParentId,
				Content2 = content2,
				MainProcessDepartmentName = mainProcessDeparmentName,
				CoProcessDepartmentName = coProcessDeparmentName,
                Diff = documentCopy.Diff
			};
			Create(comment);
			return comment;
		}

		/// <summary>
		/// Gửi comment như kết thúc/mở lại
		/// </summary>
		/// <param name="documentCopy"></param>
		/// <param name="userSendId"></param>
		/// <param name="dateCreated"></param>
		/// <param name="content"></param>
		/// <param name="content2"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public Comment SendCommentCommon(DocumentCopy documentCopy, int userSendId, DateTime dateCreated, string content, CommentType type, string content2 = "")
		{
			string mainProcessDeparmentName, coProcessDeparmentName;
			var parseComment = ParseComment(content, new List<CommentTransfer>(), out mainProcessDeparmentName, out coProcessDeparmentName);

			var comment = new Comment
			{
				Content = parseComment,
				UserSendId = userSendId,
				UserReceiveId = null,
				DateCreated = dateCreated,
				DocumentCopyId = documentCopy.DocumentCopyId,
				DocumentId = documentCopy.DocumentId,
				CommentType = (byte)type,
				DocumentCopyTargetId = documentCopy.ParentId,
				Content2 = content2,
				MainProcessDepartmentName = mainProcessDeparmentName,
				CoProcessDepartmentName = coProcessDeparmentName
			};
			Create(comment);
			return comment;
		}

		/// <summary>
		///   Gửi ý kiến xử lý bàn giao thông thường
		/// </summary>
		/// <param name="documentCopy"> Văn bản đang bàn giao</param>
		/// <param name="userSendId"> Người cho ý kiến</param>
		/// <param name="userReceiveId"> Người nhận ý kiến</param>
		/// <param name="content"> Nội dung ý kiến</param>
		/// <param name="commentTransfer"> Danh sách chuyển</param>
		/// <param name="dateCreated"> Ngày bàn giao</param>
		/// <param name="content2"> Ghi log người ủy quyền thao tác</param>
		/// <param name="dateOverdue">Hạn xử lý</param>
		public Comment SendTransfer(DocumentCopy documentCopy, int userSendId, int? userReceiveId, string content,
									List<CommentTransfer> commentTransfer, DateTime dateCreated, string content2 = "", DateTime? dateOverdue = null)
		{
			string mainProcessDeparmentName, coProcessDeparmentName;
			var parseComment = ParseComment(content, commentTransfer, out mainProcessDeparmentName, out coProcessDeparmentName, dateOverdue);

            var comment = new Comment
            {
                Content = parseComment,
                CommentText = content,
                UserSendId = userSendId,
                UserReceiveId = userReceiveId,
                DateCreated = dateCreated,
                DocumentCopyId = documentCopy.DocumentCopyId,
                DocumentId = documentCopy.DocumentId,
                CommentType = (byte)CommentType.Common,
                DocumentCopyTargetId = null,
                Content2 = content2,
                MainProcessDepartmentName = mainProcessDeparmentName,
                CoProcessDepartmentName = coProcessDeparmentName,
                Diff = documentCopy.Diff
			};

			Create(comment);

			return comment;
		}
        /// <summary>
        ///   Gửi ý kiến xử lý phiếu khảo sát
        /// </summary>
        /// <param name="documentCopy"> Văn bản đang bàn giao</param>
        /// <param name="userSendId"> Người cho ý kiến</param>
        /// <param name="userReceiveId"> Người nhận ý kiến</param>
        /// <param name="content"> Nội dung ý kiến</param>
        /// <param name="commentTransfer"> Danh sách chuyển</param>
        /// <param name="dateCreated"> Ngày bàn giao</param>
        /// <param name="content2"> Ghi log người ủy quyền thao tác</param>
        /// <param name="dateOverdue">Hạn xử lý</param>
        public Comment SendSurvey(DocumentCopy documentCopy, int userSendId, int? userReceiveId, string content,
            List<CommentTransfer> commentTransfer, DateTime dateCreated, CommentType type, string content2 = "", DateTime? dateOverdue = null)
        {
            string mainProcessDeparmentName, coProcessDeparmentName;
            var parseComment = ParseComment(content, commentTransfer, out mainProcessDeparmentName, out coProcessDeparmentName, dateOverdue);

            var comment = new Comment
            {
                Content = parseComment,
                CommentText = content,
                UserSendId = userSendId,
                UserReceiveId = userReceiveId,
                DateCreated = dateCreated,
                DocumentCopyId = documentCopy.DocumentCopyId,
                DocumentId = documentCopy.DocumentId,
                CommentType = (byte)type,
                DocumentCopyTargetId = null,
                Content2 = content2,
                MainProcessDepartmentName = mainProcessDeparmentName,
                CoProcessDepartmentName = coProcessDeparmentName,
                Diff = documentCopy.Diff
            };

            Create(comment);

            return comment;
        }
		/// <summary>
		///   Gửi ý kiến xử lý bàn giao xin y kien, thong bao
		/// </summary>
		/// <param name="docCopyId"> Document copy id </param>
		/// <param name="userSendId"> User send id </param>
		/// <param name="content"> Content </param>
		/// <param name="commentTransfer"> Target </param>
		/// <param name="dateCreated"> </param>
		/// <param name="content2"> Ghi log người ủy quyền thao tác</param>
		/// <param name="parentId"></param>
		public void SendTransfer2(int docCopyId, int userSendId, string content,
									 List<CommentTransfer> commentTransfer, DateTime dateCreated, string content2 = "", int? parentId = null)
		{
			var documentCopy = _documentCopyRepository.Get(docCopyId);
			SendTransfer2(documentCopy, userSendId, content, commentTransfer, dateCreated, content2, parentId);
		}

		/// <summary>
		///   Gửi ý kiến xử lý bàn giao xin y kien, thong bao
		/// </summary>
		/// <param name="documentCopy"> </param>
		/// <param name="userSendId"> </param>
		/// <param name="content"> </param>
		/// <param name="commentTransfer"> </param>
		/// <param name="dateCreated"> </param>
		/// <param name="content2"> Ghi log người ủy quyền thao tác</param>
		/// <param name="parentId"></param>
		public void SendTransfer2(DocumentCopy documentCopy, int userSendId, string content,
									 List<CommentTransfer> commentTransfer, DateTime dateCreated, string content2 = "", int? parentId = null)
		{
			var comment = new Comment
			{
				// CommentText = content,
				Content = "{\"Content\":\"Chưa cho ý kiến\",\"SubContent\":\"Chưa cho ý kiến\",\"Transfers\":[]}", // ParseComment(content, commentTransfer),
				UserSendId = userSendId,
				UserReceiveId = null,
				DateCreated = dateCreated,
				DocumentCopyId = documentCopy.DocumentCopyId,
				DocumentId = documentCopy.DocumentId,
				CommentType = (byte)CommentType.Contribution,
				DocumentCopyTargetId = null,
				Content2 = content2,
				ParentId = parentId
			};
			Create(comment);
		}

		/// <summary>
		/// </summary>
		/// <param name="comment"> </param>
		private void Create(Comment comment)
		{
			if (comment == null)
			{
				throw new ArgumentNullException("comment");
			}
			_commentRepository.Create(comment);
			Context.SaveChanges();
		}

		/// <summary>
		///   TienBV 121225
		///   Lấy ra tất cả những ý kiến đồng xử lý gửi về.
		/// </summary>
		/// <param name="documentCopyId"> The current document copy id. </param>
		/// <returns> </returns>
		private IEnumerable<Comment> GetAnswersForVanbanDxl(int documentCopyId)
		{
			var result = new List<Comment>();
			var spec = DocumentCopyQuery.IsChildWithType(documentCopyId, DocumentCopyTypes.DongXuLy);
			var docCopyChilds = _documentCopyRepository.GetsReadOnly(spec).ToList();
			if (!docCopyChilds.Any())
			{
				return new List<Comment>();
			}

			var answeredSpec = CommentQuery.IsCoProcessor(documentCopyId);
			var answered = _commentRepository.GetsReadOnly(answeredSpec).ToList();
			result.AddRange(answered);
			var notAnsweredChilds = docCopyChilds.Where(c => answered.All(a => a.DocumentCopyId != c.DocumentCopyId));
			result.AddRange(GetNotAnswers(notAnsweredChilds, (int)CommentType.Consulted));
			return result;
		}

		/// <summary>
		///   Tra ve danh sach xin y kien chua co tra loi
		/// </summary>
		/// <param name="notAnswerCopyIds"> </param>
		/// <param name="commentTypeId"> </param>
		/// <returns> </returns>
		private IEnumerable<Comment> GetNotAnswers(IEnumerable<DocumentCopy> notAnswerCopyIds, int commentTypeId)
		{
			var result = new List<Comment>();
			foreach (var itm in notAnswerCopyIds)
			{
				var newComemnt = new Comment
				{
					DocumentCopyId = itm.DocumentCopyId,
					Content = "{\"Content\":\"\",\"SubContent\":\"Chưa cho ý kiến\",\"Transfers\":[]}",
					DateCreated = itm.DateCreated,
					DocumentId = itm.DocumentId,
					UserReceiveId = 0, // không quan trọng
					UserSendId = itm.UserCurrentId,
					// Lấy người đầu tiên nhận văn bản đồng xử lý (người có quyền gửi ý kiến về hướng xử lý chính)
					//UserSend = _userService.Get(itm.Histories.HistoryPath.First().UserReceiveId),
					CommentType = (byte)commentTypeId
				};
				result.Add(newComemnt);
			}
			return result;
		}

		/// <summary>
		/// </summary>
		/// <param name="content"> </param>
		/// <param name="commentTransfer"></param>
		/// <param name="mainProcessDepartmentName"> </param>
		/// <param name="coProcessDepartmentName"> </param>
		/// <param name="dateOverdue"></param>
		/// <returns> </returns>
		private string ParseComment(string content, List<CommentTransfer> commentTransfer,
			out string mainProcessDepartmentName, out string coProcessDepartmentName,
			DateTime? dateOverdue = null)
		{
			var mainProcessDeparment = commentTransfer.Where(x => x.Type == "1");
			var coProcessDeparment = commentTransfer.Where(x => x.Type == "2");
			if (mainProcessDeparment.Any())
			{
				mainProcessDepartmentName = mainProcessDeparment.First().Department;
			}
			else
			{
				mainProcessDepartmentName = "";
			}

			if (coProcessDeparment.Any())
			{
				coProcessDepartmentName = coProcessDeparment.First().Department;
			}
			else
			{
				coProcessDepartmentName = "";
			}

			if (string.IsNullOrEmpty(content))
			{
				content = string.Empty;
			}

			var contentClass = new ContentEntity
			{
				SubContent = content.Length > 100
									 ? (content.Substring(0, 100) + "...")
									 : string.IsNullOrWhiteSpace(content) ? "..." : content,
				Content = content,
				DateOverdue = dateOverdue,
				Transfers = commentTransfer
			};
			return contentClass.StringifyJs();
		}

        #endregion

        //Get by DocId
        public IEnumerable<dynamic> GetByDocId(Guid docId)
        {
            var query = @"SELECT * FROM comment WHERE  DocumentId = @docId Order By CommentId";
            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("docId", docId));
            var result = Context.RawQuery(query, parameters.ToArray());
            return result;
        }
    }
}