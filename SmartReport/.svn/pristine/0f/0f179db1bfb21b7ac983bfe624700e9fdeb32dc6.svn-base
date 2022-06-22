using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Business.Objects.CacheObjects;
using Bkav.eGovCloud.Core.Caching;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bkav.eGovCloud.Business.Caching
{
	/// <summary>
	/// Lớp nghiệp vụ xử lý riêng DocumentCached
	/// </summary>
	/// <remarks>
	/// - Lưu cache những văn bản đang xử lý.
	/// - Todo: Cần lưu vào memcached tách biệt với IIS để tránh bị reset.
	/// </remarks>
	public class DocumentsCache
	{
		private readonly ICacheManager _cache;
		private string _cacheKey = CacheParam.DocumentsCache;
		private int _cacheTimeout = CacheParam.DocumentsCacheTimeOut;

		/// <summary>
		/// 
		/// </summary>
		public DocumentsCache(ICacheManager cache)
		{
			_cache = cache;
		}

		/// <summary>
		/// Trả về thông tin văn bản từ cache
		/// </summary>
		/// <param name="documentCopyId"></param>
		/// <param name="acquirer"></param>
		/// <returns></returns>
		public DocumentCached Get(int documentCopyId, Func<DocumentCached> acquirer)
		{
			var allCached = GetAll();

			// Trường hợp chạy hàm Get lần 2 trong khi lần 1 chưa chạy xong, sẽ dẫn đến trường hợp có 2 documentcopyid.
			DocumentCached result = allCached == null ? null : allCached.LastOrDefault(d => d != null && d.DocumentCopyId == documentCopyId);

			if (result == null)
			{
				result = acquirer();
				Insert(result);
			}

			return result;
		}

		/// <summary>
		/// Thêm hoặc cập nhật document mới vào cache
		/// </summary>
		/// <param name="documentCopy"></param>
		/// <param name="document"></param>
		/// <param name="attachments"></param>
		/// <param name="relations"></param>
		/// <param name="documentContents"></param>
		/// <param name="comments"></param>
		/// <param name="supplmentaries"></param>
		/// <param name="docFees"></param>
		/// <param name="docPapers"></param>
		/// <param name="approvers"></param>
		/// <returns></returns>
		public DocumentCached Set(DocumentCopy documentCopy, Document document, IEnumerable<Attachment> attachments, IEnumerable<DocRelation> relations, IEnumerable<DocumentContent> documentContents,
							IEnumerable<Comment> comments, IEnumerable<Supplementary> supplmentaries = null, IEnumerable<DocFee> docFees = null, IEnumerable<DocPaper> docPapers = null,
							IEnumerable<Approver> approvers = null)
		{
			var newCache = ToCache(documentCopy, document, attachments, relations, documentContents, comments, supplmentaries, docFees, docPapers, approvers);
			Insert(newCache);

			return newCache;
		}

		/// <summary>
		/// Convert document to cache
		/// </summary>
		/// <param name="documentCopy"></param>
		/// <param name="document"></param>
		/// <param name="attachments"></param>
		/// <param name="relations"></param>
		/// <param name="documentContents"></param>
		/// <param name="comments"></param>
		/// <param name="supplmentaries"></param>
		/// <param name="docFees"></param>
		/// <param name="docPapers"></param>
		/// <param name="approvers"></param>
		/// <returns></returns>
		public DocumentCached ToCache(DocumentCopy documentCopy, Document document, IEnumerable<Attachment> attachments, IEnumerable<DocRelation> relations,
							IEnumerable<DocumentContent> documentContents, IEnumerable<Comment> comments, IEnumerable<Supplementary> supplmentaries = null, IEnumerable<DocFee> docFees = null, IEnumerable<DocPaper> docPapers = null,
							IEnumerable<Approver> approvers = null)
		{
			var newCache = MapToCache(documentCopy, document);

			newCache.Attachments = MapAttachment(attachments);
			newCache.RelationModels = MapRelations(relations);
			newCache.DocumentContents = MapDocumentContents(documentContents);
			newCache.CommentList = newCache.CommentList.Concat(MapComments(comments));
			
			if(newCache.LienThongStatus == LienThongStatus.YeuCauThuHoi.ToString())
			{
				var relation = relations.LastOrDefault(r => r.RelationType == (int)RelationTypes.LienQuanThuHoi);
				newCache.DateLienThongStatus = relation == null ? null : relation.DateArrived;
			}
			
			if (document.IsHSMC)
			{
				newCache.SupplementaryModel = AutoMapper.Mapper.Map<IEnumerable<Supplementary>, IEnumerable<SupplementaryCached>>(supplmentaries);
				newCache.DocFees = AutoMapper.Mapper.Map<IEnumerable<DocFee>, IEnumerable<DocFeeCached>>(docFees);
				newCache.DocPapers = AutoMapper.Mapper.Map<IEnumerable<DocPaper>, IEnumerable<DocPaperCached>>(docPapers);
				newCache.Approver = AutoMapper.Mapper.Map<IEnumerable<Approver>, IEnumerable<ApproverCached>>(approvers);

				newCache.TotalFee = newCache.DocFees.Select(f => f.Price).Sum();
			}

			return newCache;
		}

		/// <summary>
		/// Thêm cache document
		/// </summary>
		/// <param name="newCache"></param>
		/// <returns></returns>
		public bool Insert(DocumentCached newCache)
		{
			try
			{
				var allCached = GetAll();
				var id = newCache.DocumentCopyId;
				var existed = allCached.SingleOrDefault(c => c.DocumentCopyId == id);
				if (existed != null)
				{
					allCached.Remove(existed);
				}

				allCached.Add(newCache);
				_cache.Remove(_cacheKey);
				_cache.Set(_cacheKey, allCached, _cacheTimeout);
			}
			catch
			{
				// Log here

				return false;
			}
			return true;
		}

		/// <summary>
		/// Xoá cache document
		/// </summary>
		/// <param name="documentCopyId"></param>
		/// <returns></returns>
		/// <remarks>
		/// Sử dụng khi kết thúc văn bản
		/// </remarks>
		public bool RemoveAll(int documentCopyId)
		{
			var allCached = GetAll();

			var id = documentCopyId;
			var existed = allCached.FirstOrDefault(c => c.DocumentCopyId == id);
			if (existed == null)
			{
				return true;
			}

			var documentId = existed.DocumentId;
			allCached = allCached.Where(c => c.DocumentId != documentId).ToList();

			_cache.Remove(_cacheKey);
			_cache.Set(_cacheKey, allCached, _cacheTimeout);

			return true;
		}

		/// <summary>
		/// Xoá cache document
		/// </summary>
		/// <param name="documentCopyIds"></param>
		/// <returns></returns>
		/// <remarks>
		/// Sử dụng khi kết thúc văn bản
		/// </remarks>
		public bool RemoveAll(IEnumerable<int> documentCopyIds)
		{
			var allCached = GetAll();

			var existed = allCached.Where(c => documentCopyIds.Contains(c.DocumentCopyId));
			if (!existed.Any())
			{
				return true;
			}
			var documentIds = existed.Select(d => d.DocumentId);
			allCached = allCached.Where(c => !documentIds.Contains(c.DocumentId)).ToList();

			_cache.Remove(_cacheKey);
			_cache.Set(_cacheKey, allCached, _cacheTimeout);

			return true;
		}

		/// <summary>
		/// Map document to cache
		/// </summary>
		/// <param name="documentCopy"></param>
		/// <param name="document"></param>
		/// <returns></returns>
		private DocumentCached MapToCache(DocumentCopy documentCopy, Document document)
		{
			var result = AutoMapper.Mapper.Map<Document, DocumentCached>(document);

			result.ExpireProcess = document.ExpireProcess ?? 1;

			result.DocumentCopyId = documentCopy.DocumentCopyId;
			result.WorkflowId = documentCopy.WorkflowId;
			result.NodeCurrentId = documentCopy.NodeCurrentId;
			result.UserSendId = documentCopy.UserSendId;
			result.UserCurrentId = documentCopy.UserCurrentId;
			result.DocumentCopyType = documentCopy.DocumentCopyType;
			result.NodeCurrentPermission = documentCopy.NodeCurrentPermission;
			result.UserNguoiThamGia = documentCopy.UserNguoiThamGia;
			result.UserNguoiDaXem = documentCopy.UserNguoiDaXem;
			result.UserGiamSat = documentCopy.UserGiamSat;
			result.DocumentUsers = documentCopy.DocumentUsers;
			result.LastComment = documentCopy.LastComment;
			result.LastDateComment = documentCopy.LastDateComment;
			result.LastUserComment = documentCopy.LastUserComment;
			result.History = documentCopy.History;
			result.ParentId = documentCopy.ParentId;
			result.CurrentDepartmentName = documentCopy.CurrentDepartmentName;
			result.DateReceived = documentCopy.DateReceived;
			result.LienThongStatus = document.LienThongStatus;
            result.OrganizationCode = document.OrganizationCode;
            // VuHQ 20200626 Note trong documentcopy bị đè bởi dữ liệu document
            // result.Note = documentCopy.Note;

			if (document.CategoryBusinessIdInEnum == CategoryBusinessTypes.VbDi
					&& document.IsTransferPublish.HasValue)
			{
				result.InOutPlace = document.Organization;
			}

			return result;
		}

		private IEnumerable<CommentCached> MapComments(IEnumerable<Comment> comments)
		{
			var result = new List<CommentCached>();
			if (comments == null || !comments.Any())
			{
				return result;
			}

			result.AddRange(AutoMapper.Mapper.Map<IEnumerable<Comment>, IEnumerable<CommentCached>>(comments));

			return result;
		}

		private IEnumerable<DocumentContentCached> MapDocumentContents(IEnumerable<DocumentContent> documentContents)
		{
			var result = new List<DocumentContentCached>();
			if (documentContents == null || !documentContents.Any())
			{
				return result;
			}

			result.AddRange(AutoMapper.Mapper.Map<IEnumerable<DocumentContent>, IEnumerable<DocumentContentCached>>(documentContents));

			return result;
		}

		private IEnumerable<DocRelationCached> MapRelations(IEnumerable<DocRelation> relations)
		{
			var result = new List<DocRelationCached>();
			if (relations == null || !relations.Any())
			{
				return result;
			}

			result.AddRange(AutoMapper.Mapper.Map<IEnumerable<DocRelation>, IEnumerable<DocRelationCached>>(relations));

			return result;
		}

		private IEnumerable<AttachmentCached> MapAttachment(IEnumerable<Attachment> attachments)
		{
			var result = new List<AttachmentCached>();

			if (attachments == null || !attachments.Any())
			{
				return result;
			}

			result = attachments.OrderBy(a => a.IsDeleted)
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
								Versions = a.AttachmentDetails.Select(
											d =>
												new AttachmentDetailCache
												{
													Version = d.VersionAttachmentDetail,
													CreateDate = d.CreatedOnDate.ToString("G"),
													User = d.CreatedByUserName,
												}).OrderByDescending(d => d.Version).ToList()
							}).ToList();

			return result;
		}

		private List<DocumentCached> GetAll()
		{
			var result = _cache.Get<List<DocumentCached>>(_cacheKey, () =>
			{
				return new List<DocumentCached>();
			}, _cacheTimeout);

			OptimizeCached(result);

			return result;
		}

		private void OptimizeCached(List<DocumentCached> cached)
		{
			// Dự tính số lượng văn bản thường xuyên sử dụng trong một tháng gần nhất
			var maxItemCached = 5000;

			if (cached.Count > maxItemCached)
			{
				cached = cached.OrderByDescending(d => d.DateCreated).Take(maxItemCached).ToList();
			}
		}
	}
}
