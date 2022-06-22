using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Business.FileTransfer;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.FileSystem;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Customer;
using System.Web;
using System.Net;

namespace Bkav.eGovCloud.Business.Customer
{
	/// <summary>
	/// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
	/// <para>Project: eGov Cloud v1.0</para>
	/// <para>Class : AttachmentBll - public - BLL</para>
	/// <para>Access Modifiers:</para> 
	/// <para>Create Date : 140313</para>
	/// <para>Author      : TrungVH</para>
	/// <para>Description : BLL tương ứng với bảng Role trong CSDL</para>
	/// </summary>
	public class AttachmentBll : ServiceBase
	{
		private readonly IRepository<Attachment> _attachmentRepository;
		private readonly IRepository<AttachmentDetail> _attachmentDetailRepository;
		private readonly FileLocationBll _fileLocationService;
		private readonly FileLocationSettings _fileLocationSettings;
		private readonly FileManager _fileManager;
		private readonly UserBll _userService;
		private readonly StorePrivateBll _storePrivateService;
		private readonly DocumentPermissionHelper _documentPermissionHelper;

		/// <summary>
		/// Khởi tạo
		/// </summary>
		/// <param name="context">Context</param>
		/// <param name="fileLocationService">Dal tương ứng với bảng FileLocation trong CSDL</param>
		/// <param name="fileLocationSettings">Cấu hình cho vị trí lưu file</param>
		/// <param name="userService">Bll tương ứng với bản User trong CSDL </param>
		/// <param name="documentPermissionHelper">Bll nghiệp vụ check quyền trên văn bản/hồ sơ </param>
		/// <param name="storePrivateService">Bll nghiệp vụ sổ hồ sơ cá nhân</param>
		public AttachmentBll(
			IDbCustomerContext context,
			FileLocationBll fileLocationService,
			FileLocationSettings fileLocationSettings,
			UserBll userService,
			StorePrivateBll storePrivateService,
			DocumentPermissionHelper documentPermissionHelper)
			: base(context)
		{
			_attachmentRepository = Context.GetRepository<Attachment>();
			_attachmentDetailRepository = Context.GetRepository<AttachmentDetail>();
			_fileLocationService = fileLocationService;
			_userService = userService;
			_fileLocationSettings = fileLocationSettings;
			_fileManager = FileManager.Default;
			_documentPermissionHelper = documentPermissionHelper;
			_storePrivateService = storePrivateService;
		}

		/// <summary>
		/// Lấy ra tất cả các file đính kèm
		/// </summary>
		/// <param name="documentId">Id văn bản, hồ sơ</param>
		/// <returns>Danh sách file đính kèm</returns>
		public IEnumerable<Attachment> Gets(Guid documentId)
		{
			return _attachmentRepository.GetsReadOnly(a => a.DocumentId == documentId);
		}

		/// <summary>
		/// Lấy ra tất cả các file đính kèm
		/// </summary>
		/// <param name="spec">Bộ lọc</param>
		/// <returns>Danh sách file đính kèm</returns>
		public IEnumerable<Attachment> Gets(Expression<Func<Attachment, bool>> spec = null)
		{
			return _attachmentRepository.GetsReadOnly(spec);
		}

		/// <summary>
		/// Lấy ra tất cả các file đính kèm
		/// </summary>
		/// <param name="projector"></param>
		/// <param name="documentId">Id văn bản, hồ sơ</param>
		/// <returns>Danh sách file đính kèm</returns>
		public IEnumerable<T> GetsAs<T>(Expression<Func<Attachment, T>> projector, Guid documentId)
		{
			return _attachmentRepository.GetsAs(projector, a => a.DocumentId == documentId);
		}

		/// <author> 
		/// <para>TrungVH@bkav.com</para>
		/// <para>CuongNT@bkav.com - 250613: thêm tham số deleteTempfiles</para>
		/// </author>
		/// <summary>
		/// Tạo mới file đính kèm cho văn bản, hồ sơ (chỉ trả về danh sách các entity sẽ được thêm mới chứ chưa lưu lại)
		/// </summary>
		/// <param name="tempfiles">Danh sách các file tạm</param>
		/// <param name="userId">Id người tạo</param>
		/// <param name="deleteTempfiles"><c>True</c> xóa tempfiles sau khi thực thi. <c>False</c> ngược lại. </param>
		public List<Attachment> AddAttachmentInDoc(IDictionary<string, IDictionary<string, string>> tempfiles, int userId, bool deleteTempfiles = true)
		{
			if (tempfiles == null || !tempfiles.Keys.Any())
			{
				return new List<Attachment>();
			}

			var tempPath = ResourceLocation.Default.FileUploadTemp;

			try
			{
				var currentFileLocation = GetFileLocation();
				var transfer = Transfer.GetTransfer(currentFileLocation, _fileLocationSettings);
				var attachments = new List<Attachment>();
				foreach (var key in tempfiles.Keys)
				{
					if (_fileManager.Exist(key, tempPath))
					{
						using (var stream = tempfiles[key].ContainsKey("content") && !string.IsNullOrWhiteSpace(tempfiles[key]["content"])
								? new MemoryStream(Convert.FromBase64String(tempfiles[key]["content"]))
								: _fileManager.Open(key, tempPath))
						{
							var fileInfo = transfer.Upload(stream, FileType.Attach);
							var attachment = new Attachment
							{
								AttachmentName = tempfiles[key].ContainsKey("name") ? tempfiles[key]["name"] : key,
								IsDeleted = false,
								VersionAttachment = 1,
								Size = (int)fileInfo.Length
							};
							var attachmentDetail = new AttachmentDetail
							{
								FileName = fileInfo.FileName,
								CreatedByUserId = userId,
								//Sửa GetCurrentId thành Get(userId) do người đang đăng nhập chưa chắc đã là người chèn file trong trường hợp là văn bản đến từ bên khác.
								CreatedByUserName = "", // userId != 0 ? _userService.Get(userId).Username : ""
								CreatedOnDate = fileInfo.CreatedDate,
								FileLocationId = currentFileLocation.FileLocationId,
								IdentityFolder = fileInfo.IdentityFolder,
								Size = (int)fileInfo.Length,
								VersionAttachmentDetail = 1,
								IsLink = false
							};
							if (!string.IsNullOrEmpty(fileInfo.RootFolder))
							{
								attachmentDetail.FileLocationKey = fileInfo.RootFolder;
							}
							attachment.AttachmentDetails.Add(attachmentDetail);
							attachments.Add(attachment);
							stream.Dispose();
						}
						// CuongNT@bkav.com - 250613: Thêm tham số deleteTempfiles phục vụ khi tạo mới hoặc update liên tiếp file đính kèm trên form cho một loại văn bản mới.
						if (deleteTempfiles)
						{
							try
							{
								_fileManager.Delete(key, tempPath);
							}
							catch
							{
								// Log exception 
							}
						}
					}
					else
					{
						int id = 0;
						if (Int32.TryParse(key, out id))
						{
							try
							{
								id = Convert.ToInt32(key);
								var existAttachment = Get(id);
								if (existAttachment != null)
								{
									var attachment = new Attachment
									{
										AttachmentName = existAttachment.AttachmentName,
										IsDeleted = false,
										VersionAttachment = 1,
										Size = existAttachment.Size
									};
									var attachmentDetail = new AttachmentDetail
									{
										FileName = existAttachment.AttachmentDetails.First().FileName,
										CreatedByUserId = userId,
										//Sửa GetCurrentId thành Get(userId) do người đang đăng nhập chưa chắc đã là người chèn file trong trường hợp là văn bản đến từ bên khác.
										CreatedByUserName = userId != 0 ? _userService.GetFromCache(userId).Username : "",
										CreatedOnDate = existAttachment.AttachmentDetails.First().CreatedOnDate,
										FileLocationId = currentFileLocation.FileLocationId,
										IdentityFolder = existAttachment.AttachmentDetails.First().IdentityFolder,
										Size = existAttachment.Size,
										VersionAttachmentDetail = 1,
										IsLink = false
									};
									attachment.AttachmentDetails.Add(attachmentDetail);
									attachments.Add(attachment);
								}

							}
							catch (Exception ex)
							{
								throw ex;
							}
						}
					}
				}

				return attachments;
			}
			catch (Exception ex)
			{
				//Nếu không tìm thấy thư mục lưu file đính kèm, xoá bỏ file trong fileTemp
				try
				{
					foreach (var key in tempfiles.Keys)
					{
						_fileManager.Delete(key, tempPath);
					}
				}
				catch
				{
					// Log exception 
				}

				throw ex;
			}
		}

		/// <summary>
		///  Thêm mới file văn văn bản đã được phát hành hoặc kết thúc
		/// </summary>
		/// <param name="fileName">Tên file đính kèm</param>
		/// <param name="stream">stream file</param>
		/// <param name="userId">Người tạo</param>
		/// <param name="document">Văn bản đính kèm file</param>
		/// <returns></returns>
		public Attachment AddAttachmentInDoc(string fileName, Stream stream, int userId, Document document)
		{
			if (string.IsNullOrEmpty(fileName))
			{
				throw new ArgumentNullException("fileName is null.");
			}

			if (document == null)
			{
				throw new ArgumentNullException("document is null.");
			}

			var currentFileLocation = GetFileLocation(); // _fileLocationService.GetCurrent();
			var transfer = Transfer.GetTransfer(currentFileLocation, _fileLocationSettings);
			var attachments = new List<Attachment>();
			var userName = userId != 0 ? _userService.GetFromCache(userId).Username : "";
			using (stream)
			{
				var fileInfo = transfer.Upload(stream, FileType.Attach);
				var attachment = new Attachment
				{
					AttachmentName = fileName,
					IsDeleted = false,
					VersionAttachment = 1,
					Size = (int)fileInfo.Length
				};

				var attachmentDetail = new AttachmentDetail
				{
					FileName = fileInfo.FileName,
					CreatedByUserId = userId,
					//Sửa GetCurrentId thành Get(userId) do người đang đăng nhập chưa chắc đã là người chèn file trong trường hợp là văn bản đến từ bên khác.
					CreatedByUserName = userName,
					CreatedOnDate = fileInfo.CreatedDate,
					FileLocationId = currentFileLocation.FileLocationId,
					IdentityFolder = fileInfo.IdentityFolder,
					Size = (int)fileInfo.Length,
					VersionAttachmentDetail = 1,
					IsLink = false
				};

				if (!string.IsNullOrEmpty(fileInfo.RootFolder))
				{
					attachmentDetail.FileLocationKey = fileInfo.RootFolder;
				}

				attachment.AttachmentDetails.Add(attachmentDetail);
				attachments.Add(attachment);

				document.Attachments.Add(attachment);
				Context.SaveChanges();
				stream.Dispose();

				return attachment;
			}
		}

		///<summary>
		///</summary>
		///<param name="attachments"></param>
		///<param name="userId"></param>
		///<returns></returns>
		public ICollection<Attachment> CopyAttachment(ICollection<Attachment> attachments, int userId)
		{
			if (!attachments.Any())
			{
				return new List<Attachment>(); ;
			}

			var result = attachments.Where(a => !a.IsDeleted)
				.Select(a =>
				{
					var ad = a.AttachmentDetails.Last();
					var newAtt = new Attachment()
					{
						AttachmentName = a.AttachmentName,
						Size = a.Size,
						VersionAttachment = 1
					};

					newAtt.AttachmentDetails.Add(new AttachmentDetail()
					{
						FileName = ad.FileName,
						CreatedByUserId = ad.CreatedByUserId,
						CreatedByUserName = ad.CreatedByUserName,
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

		/// <summary>
		/// Tạo 1 file đính kèm chi tiết (chưa cập nhật vào db)
		/// </summary>
		/// <param name="stream">Stream</param>
		/// <param name="userId">Id người tạo</param>
		/// <param name="version">Phiên bản</param>
		/// <returns>AttachmentDetail</returns>
		public AttachmentDetail AddAttachmentDetail(Stream stream, int userId, int version)
		{
			AttachmentDetail attachmentDetail;
			var currentFileLocation = GetFileLocation(); // _fileLocationService.GetCurrent();
			var transfer = Transfer.GetTransfer(currentFileLocation, _fileLocationSettings);
			using (stream)
			{
				var fileInfo = transfer.Upload(stream, FileType.Attach);
				attachmentDetail = new AttachmentDetail
				{
					FileName = fileInfo.FileName,
					CreatedByUserId = userId,
					CreatedByUserName = _userService.CurrentUser.Username,
					CreatedOnDate = fileInfo.CreatedDate,
					FileLocationId = currentFileLocation.FileLocationId,
					IdentityFolder = fileInfo.IdentityFolder,
					Size = (int)fileInfo.Length,
					VersionAttachmentDetail = version,
					IsLink = false
				};
				if (!string.IsNullOrEmpty(fileInfo.RootFolder))
				{
					attachmentDetail.FileLocationKey = fileInfo.RootFolder;
				}
			}
			return attachmentDetail;
		}

		/// <summary>
		/// Lấy ra file đính kèm theo id
		/// </summary>
		/// <param name="id">Id file đính kèm</param>
		/// <returns></returns>
		public Attachment Get(int id)
		{
			Attachment attachment = null;
			if (id > 0)
			{
				attachment = _attachmentRepository.Get(id);
			}
			return attachment;
		}

		/// <summary>
		/// Tải tệp đính kèm
		/// </summary>
		/// <param name="fileName">Tên tệp đính kèm</param>
		/// <param name="id">Id tệp đính kèm</param>
		/// <param name="version">Phiên bản </param>
		/// <param name="storePrivateId"></param>
		/// <param name="userId">Id người tải file</param>
		/// <returns>Stream</returns>
		public Stream DownloadAttachment(out string fileName, int id, int? storePrivateId, int? version, int userId)
		{
			var attachment = Get(id);
			if (attachment == null)
			{
				throw new EgovException("Không tìm thấy tệp đính kèm");
			}

			if (attachment.IsDeleted)
			{
				throw new EgovException("Tệp đính kèm đã bị xóa");
			}

			fileName = attachment.AttachmentName;

			var viewPermission = _documentPermissionHelper.CheckForQuyenXemAndUyQuyenXem(attachment.DocumentId, userId);
			if (!viewPermission)
			{
				if (storePrivateId.HasValue && _storePrivateService.CheckPermissionStorePrivate(storePrivateId.Value, userId) != null)
				{
					viewPermission = true;
				}
			}

			if (!viewPermission)
			{
				throw new EgovException("Bạn không có quyền tải tệp đính kèm");
			}

			if (!version.HasValue)
			{
				version = attachment.VersionAttachment;
			}
			var attachmentDetail =
				attachment.AttachmentDetails.SingleOrDefault(
					a => a.VersionAttachmentDetail == version);

			if (attachmentDetail == null)
			{
				throw new EgovException("Không tìm thấy tệp đính kèm");
			}

			if (attachmentDetail.IsLink)
			{
				return DownloadFromLink(attachmentDetail);
			}

			var fileLocation = GetFileLocation(attachmentDetail.FileLocationId);
			if (fileLocation == null)
			{
				throw new EgovException("Không tìm thấy nơi lưu tệp đính kèm");
			}

			try
			{
				var transfer = Transfer.GetTransfer(fileLocation, _fileLocationSettings);
				var downloaded =
					transfer.Download(new FileTransferInfo
					{
						CreatedDate = attachmentDetail.CreatedOnDate,
						FileName = attachmentDetail.FileName,
						FileType = FileType.Attach,
						IdentityFolder = attachmentDetail.IdentityFolder,
						RootFolder = attachmentDetail.FileLocationKey
					});

				return downloaded;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		private Stream DownloadFromLink(AttachmentDetail attachmentDetail)
		{
			using (var request = new WebClient())
			{
				var result = request.DownloadData(attachmentDetail.AttachLink);
				return new MemoryStream(result);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="fileName"></param>
		/// <param name="id"></param>
		/// <returns></returns>
		public Stream TestDownloadAttachment(out string fileName, int id)
		{
			var attachment = Get(id);
			if (attachment == null)
			{
				throw new EgovException("Không tìm thấy tệp đính kèm");
			}
			if (attachment.IsDeleted)
			{
				throw new EgovException("Tệp đính kèm đã bị xóa");
			}

			fileName = attachment.AttachmentName;
			var attachmentDetail =
				attachment.AttachmentDetails.SingleOrDefault();
			if (attachmentDetail == null)
			{
				throw new EgovException("Không tìm thấy tệp đính kèm");
			}
			var fileLocation = GetFileLocation(attachmentDetail.FileLocationId); // _fileLocationService.Get(attachmentDetail.FileLocationId);
			if (fileLocation == null)
			{
				throw new EgovException("Không tìm thấy nơi lưu tệp đính kèm");
			}
			var transfer = Transfer.GetTransfer(fileLocation, _fileLocationSettings);
			var downloaded =
				transfer.Download(new FileTransferInfo
				{
					CreatedDate = attachmentDetail.CreatedOnDate,
					FileName = attachmentDetail.FileName,
					FileType = FileType.Attach,
					IdentityFolder = attachmentDetail.IdentityFolder,
					RootFolder = attachmentDetail.FileLocationKey
				});

			return downloaded;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="fileName"></param>
		/// <param name="id"></param>
		/// <returns></returns>
		public string GetAttachmentPath(out string fileName, int id)
		{
			var attachment = Get(id);
			if (attachment == null)
			{
				throw new EgovException("Không tìm thấy tệp đính kèm");
			}
			if (attachment.IsDeleted)
			{
				throw new EgovException("Tệp đính kèm đã bị xóa");
			}

			fileName = attachment.AttachmentName;
			var attachmentDetail =
				attachment.AttachmentDetails.FirstOrDefault(att => att.VersionAttachmentDetail == attachment.VersionAttachment);

			if (attachmentDetail == null)
			{
				throw new EgovException("Không tìm thấy tệp đính kèm");
			}
			var fileLocation = GetFileLocation(attachmentDetail.FileLocationId);// _fileLocationService.Get(attachmentDetail.FileLocationId);
			if (fileLocation == null)
			{
				throw new EgovException("Không tìm thấy nơi lưu tệp đính kèm");
			}
			var transfer = Transfer.GetTransfer(fileLocation, _fileLocationSettings);
			var path =
				transfer.GetPath(new FileTransferInfo
				{
					CreatedDate = attachmentDetail.CreatedOnDate,
					FileName = attachmentDetail.FileName,
					FileType = FileType.Attach,
					IdentityFolder = attachmentDetail.IdentityFolder,
					RootFolder = attachmentDetail.FileLocationKey
				});

			return path;
		}

		/// <summary>
		/// Tải tất cả các tệp của văn bản theo
		/// </summary>
		/// <param name="documentId"></param>
		/// <param name="userId"></param>
		/// <param name="version"></param>
		/// <param name="storePrivateId"></param>
		/// <returns></returns>
		public Dictionary<string, Stream> DownloadAttachments(Guid documentId, int userId, int? storePrivateId, int? version)
		{
			var viewPermission = _documentPermissionHelper.CheckForQuyenXemAndUyQuyenXem(documentId, userId);
			if (!viewPermission)
			{
				if (storePrivateId.HasValue && _storePrivateService.CheckPermissionStorePrivate(storePrivateId.Value, userId) != null)
				{
					viewPermission = true;
				}
			}

			if (!viewPermission)
			{
				throw new EgovException("Bạn không có quyền tải tệp đính kèm");
			}

			var attachments = Gets(p => p.DocumentId == documentId && !p.IsDeleted);
			if (attachments == null || !attachments.Any())
			{
				throw new EgovException("Không tìm thấy tệp đính kèm");
			}

			var results = new Dictionary<string, Stream>();
			var listLocation = new List<FileLocation>();

			foreach (var attachment in attachments)
			{
				int? tmpVersion = version.HasValue ? version.Value : attachment.VersionAttachment;
				var attachmentDetail = attachment.AttachmentDetails.FirstOrDefault(a =>
					a.VersionAttachmentDetail == tmpVersion);

				if (attachmentDetail == null)
					continue;

				FileLocation fileLocation = null;
				if (listLocation != null && listLocation.Any())
				{
					fileLocation = listLocation.FirstOrDefault(p => p.FileLocationId == attachmentDetail.FileLocationId);
					if (fileLocation == null)
					{
						fileLocation = GetFileLocation(attachmentDetail.FileLocationId); // _fileLocationService.Get(attachmentDetail.FileLocationId);
						listLocation.Add(fileLocation);
					}
				}
				else
				{
					fileLocation = GetFileLocation(attachmentDetail.FileLocationId); // _fileLocationService.Get(attachmentDetail.FileLocationId);
					listLocation.Add(fileLocation);
				}

				if (fileLocation == null)
				{
					continue;
				}

				var transfer = Transfer.GetTransfer(fileLocation, _fileLocationSettings);
				var downloaded = transfer.Download(new FileTransferInfo
				{
					CreatedDate = attachmentDetail.CreatedOnDate,
					FileName = attachmentDetail.FileName,
					FileType = FileType.Attach,
					IdentityFolder = attachmentDetail.IdentityFolder,
					RootFolder = attachmentDetail.FileLocationKey
				});
				var fileName = attachment.AttachmentName;

				results.Add(fileName, downloaded);
			}

			return results;
		}

		/// <summary>
		///  Tải tất cả các tệp của văn bản theo
		/// </summary>
		/// <param name="attchmentIds"></param>
		/// <param name="version"></param>
		/// <returns></returns>
		public Dictionary<string, Stream> GetAttachments(IEnumerable<int> attchmentIds, int? version = null)
		{
			if (attchmentIds == null || !attchmentIds.Any())
			{
				throw new ArgumentNullException("attchmentIds is null.");
			}

			var attachments = Gets(p => attchmentIds.Contains(p.AttachmentId) && !p.IsDeleted);
			if (attachments == null || !attachments.Any())
			{
				throw new EgovException("Không tìm thấy tệp đính kèm");
			}

			var results = new Dictionary<string, Stream>();
			var listLocation = new List<FileLocation>();

			foreach (var attachment in attachments)
			{
				int? tmpVersion = version.HasValue ? version.Value : attachment.VersionAttachment;
				var attachmentDetail = attachment.AttachmentDetails.FirstOrDefault(a =>
					a.VersionAttachmentDetail == tmpVersion);

				if (attachmentDetail == null)
					continue;

				FileLocation fileLocation = null;
				if (listLocation != null && listLocation.Any())
				{
					fileLocation = listLocation.FirstOrDefault(p => p.FileLocationId == attachmentDetail.FileLocationId);
					if (fileLocation == null)
					{
						fileLocation = GetFileLocation(attachmentDetail.FileLocationId); // _fileLocationService.Get(attachmentDetail.FileLocationId);
						listLocation.Add(fileLocation);
					}
				}
				else
				{
					fileLocation = GetFileLocation(attachmentDetail.FileLocationId); // _fileLocationService.Get(attachmentDetail.FileLocationId);
					listLocation.Add(fileLocation);
				}

				if (fileLocation == null)
				{
					continue;
				}

				var transfer = Transfer.GetTransfer(fileLocation, _fileLocationSettings);
				var downloaded = transfer.Download(new FileTransferInfo
				{
					CreatedDate = attachmentDetail.CreatedOnDate,
					FileName = attachmentDetail.FileName,
					FileType = FileType.Attach,
					IdentityFolder = attachmentDetail.IdentityFolder,
					RootFolder = attachmentDetail.FileLocationKey
				});
				var fileName = attachment.AttachmentName;

				results.Add(fileName, downloaded);
			}

			return results;
		}

		/// <summary>
		/// Hàm lấy version file đính kèm trong
		/// </summary>
		/// <param name="attachmentId"></param>
		/// <returns></returns>
		public int GetCountAttachmentDetail(int attachmentId)
		{
			return _attachmentDetailRepository.Count(a => a.AttachmentId == attachmentId);
		}

		/// <summary>
		/// Tải tệp đính kèm trả về 1 dictionary có key là Id của attachment, value là file base64
		/// </summary>
		/// <param name="ids">Id tệp đính kèm</param>
		/// <param name="userId">Id người tải file</param>
		/// <param name="convertWordTopdf">Chuyển file word thành file pdf</param>
		/// <param name="ignorePermission">Cho phép bỏ qua check quyền xem file. Lưu ý: chỉ dùng khi gọi từ liên thông</param>
		/// <returns>Stream</returns>
		public IDictionary<string, string> DownloadAttachment1(List<int> ids, int userId, bool convertWordTopdf = false, bool ignorePermission = false)
		{
			var attachments =
				_attachmentDetailRepository.GetsReadOnly(
					a => ids.Contains(a.AttachmentId) && a.VersionAttachmentDetail == a.Attachment.VersionAttachment,
					Context.Filters.Include<AttachmentDetail>("Attachment", "FileLocation"));
			if (attachments == null || !attachments.Any())
			{
				return null;
			}
			var result = new Dictionary<string, string>();
			foreach (var attachmentDetail in attachments)
			{
				if (attachmentDetail.Attachment.IsDeleted)
				{
					continue;
				}

				if (!ignorePermission)
				{
					var viewPermission = _documentPermissionHelper.CheckForQuyenXemAndUyQuyenXem(attachmentDetail.Attachment.DocumentId, userId);
					if (!viewPermission)
					{
						continue;
					}
				}

				var fileLocation = GetFileLocation(attachmentDetail.FileLocationId);
				var transfer = Transfer.GetTransfer(fileLocation, _fileLocationSettings);
				var downloaded =
					transfer.Download(new FileTransferInfo
					{
						CreatedDate = attachmentDetail.CreatedOnDate,
						FileName = attachmentDetail.FileName,
						FileType = FileType.Attach,
						IdentityFolder = attachmentDetail.IdentityFolder,
						RootFolder = attachmentDetail.FileLocationKey
					});
				var ext = attachmentDetail.Attachment.Extension;
				if (convertWordTopdf && (ext == "doc" || ext == "docx"))
				{
					var wordTemp = _fileManager.Create(downloaded, ResourceLocation.Default.FileTemp, attachmentDetail.FileName, ext);
					var pdfTemp = Path.Combine(ResourceLocation.Default.FileTemp,
						string.Format("{0}.pdf", attachmentDetail.FileName));
					if (!ConvertWordToPdf(wordTemp.FullName, pdfTemp))
					{
						//_fileManager.Delete(attachmentDetail.FileName, ResourceLocation.Default.FileTemp, ext);
						continue;
					}
					//_fileManager.Delete(attachmentDetail.FileName, ResourceLocation.Default.FileTemp, ext);
					downloaded = _fileManager.Open(pdfTemp);
				}
				var ms = new MemoryStream();
				using (downloaded)
				{
					var buffer = new byte[4096];
					while (true)
					{
						var count = downloaded.Read(buffer, 0, 4096);
						if (count != 0)
							ms.Write(buffer, 0, count);
						else
							break;
					}
				}
				result.Add(attachmentDetail.AttachmentId.ToString(CultureInfo.InvariantCulture), Convert.ToBase64String(ms.ToArray()));
			}

			return result;
		}

		/// <summary>
		/// Tải tệp đính kèm trả về 1 dictionary có key là Name của attachment, value là file base64
		/// </summary>
		/// <param name="ids">Id tệp đính kèm</param>
		/// <param name="userId">Id người tải file</param>
		/// <param name="convertWordTopdf">Chuyển file word thành file pdf</param>
		/// <param name="ignorePermission">Cho phép bỏ qua check quyền xem file. Lưu ý: chỉ dùng khi gọi từ liên thông</param>
		/// <returns>Stream</returns>
		public IDictionary<string, string> DownloadAttachmentName(List<int> ids, int userId, bool convertWordTopdf = false, bool ignorePermission = false)
		{
			var attachments =
				_attachmentDetailRepository.GetsReadOnly(
					a => ids.Contains(a.AttachmentId) && a.VersionAttachmentDetail == a.Attachment.VersionAttachment,
					Context.Filters.Include<AttachmentDetail>("Attachment", "FileLocation"));
			if (attachments == null || !attachments.Any())
			{
				return null;
			}
			var result = new Dictionary<string, string>();
			foreach (var attachmentDetail in attachments)
			{
				if (attachmentDetail.Attachment.IsDeleted)
				{
					continue;
				}

				if (!ignorePermission)
				{
					var viewPermission = _documentPermissionHelper.CheckForQuyenXemAndUyQuyenXem(attachmentDetail.Attachment.DocumentId, userId);
					if (!viewPermission)
					{
						continue;
					}
				}

				var fileLocation = GetFileLocation(attachmentDetail.FileLocationId);
				var transfer = Transfer.GetTransfer(fileLocation, _fileLocationSettings);
				var downloaded =
					transfer.Download(new FileTransferInfo
					{
						CreatedDate = attachmentDetail.CreatedOnDate,
						FileName = attachmentDetail.FileName,
						FileType = FileType.Attach,
						IdentityFolder = attachmentDetail.IdentityFolder,
						RootFolder = attachmentDetail.FileLocationKey
					});
				var ext = attachmentDetail.Attachment.Extension;
				if (convertWordTopdf && (ext == "doc" || ext == "docx"))
				{
					var wordTemp = _fileManager.Create(downloaded, ResourceLocation.Default.FileTemp, attachmentDetail.FileName, ext);
					var pdfTemp = Path.Combine(ResourceLocation.Default.FileTemp,
						string.Format("{0}.pdf", attachmentDetail.FileName));
					if (!ConvertWordToPdf(wordTemp.FullName, pdfTemp))
					{
						//_fileManager.Delete(attachmentDetail.FileName, ResourceLocation.Default.FileTemp, ext);
						continue;
					}
					//_fileManager.Delete(attachmentDetail.FileName, ResourceLocation.Default.FileTemp, ext);
					downloaded = _fileManager.Open(pdfTemp);
				}
				var ms = new MemoryStream();
				using (downloaded)
				{
					var buffer = new byte[4096];
					while (true)
					{
						var count = downloaded.Read(buffer, 0, 4096);
						if (count != 0)
							ms.Write(buffer, 0, count);
						else
							break;
					}
				}

				var name = attachmentDetail.Attachment.AttachmentName.ToString(CultureInfo.InvariantCulture);
				var idx = 1;
				while (result.Keys.Any(k => k.Equals(name)))
				{
					// Tránh trùng
					name += "_" + idx++;
				}

				result.Add(name, Convert.ToBase64String(ms.ToArray()));
			}

			return result;
		}

		/// <summary>
		/// Tải tệp đính kèm
		/// </summary>
		/// <param name="attachment">Tệp đính kèm</param>
		/// <returns>Stream</returns>
		public Stream DownloadAttachmentForCreateIndex(Attachment attachment)
		{
			if (attachment == null)
			{
				throw new ArgumentNullException("attachment");
			}
			if (attachment.IsDeleted)
			{
				return null;
			}
			var attachmentDetail =
				attachment.AttachmentDetails.SingleOrDefault(
					a => a.VersionAttachmentDetail == attachment.VersionAttachment);
			if (attachmentDetail == null)
			{
				throw new Exception("Không tìm thấy tệp đính kèm");
			}
			var fileLocation = GetFileLocation(attachmentDetail.FileLocationId); // _fileLocationService.Get(attachmentDetail.FileLocationId);
			if (fileLocation == null)
			{
				throw new Exception("Không tìm thấy nơi lưu tệp đính kèm");
			}
			var transfer = Transfer.GetTransfer(fileLocation, _fileLocationSettings);
			var downloaded =
				transfer.Download(new FileTransferInfo
				{
					CreatedDate = attachmentDetail.CreatedOnDate,
					FileName = attachmentDetail.FileName,
					FileType = FileType.Attach,
					IdentityFolder = attachmentDetail.IdentityFolder,
					RootFolder = attachmentDetail.FileLocationKey
				});

			return downloaded;
		}

		/// <summary>
		/// <para>Xoa file dinh kem luu tam</para>
		/// <para>CuongNT@bkav.com - 240713</para>
		/// </summary>
		/// <param name="tempfiles">Key: ten file luu tam. Value: Base64 du lieu file luu tam.</param>
		public void DeleteTempfiles(IDictionary<string, IDictionary<string, string>> tempfiles)
		{
			var tempPath = ResourceLocation.Default.FileUploadTemp;
			foreach (var key in tempfiles.Keys)
			{
				try
				{
					_fileManager.Delete(key, tempPath);
				}
				catch
				{
					// Log exception
				}
			}
		}

		/// <summary>
		/// Chuyển file word sang pdf
		/// </summary>
		/// <param name="pstrFileInput">Đường dẫn file word</param>
		/// <param name="pstrFileOutput">Đường dẫn file pdf</param>
		/// <returns></returns>
		public bool ConvertWordToPdf(string pstrFileInput, string pstrFileOutput)
		{
			return NativeMethods.ConvertWordToPDF(pstrFileInput, pstrFileOutput);
		}

		/// <summary>
		/// Xóa file đính kèm
		/// </summary>
		/// <param name="att">Tệp đính kèm</param>
		public void Delete(Attachment att)
		{
			if (att == null)
			{
				throw new ArgumentNullException("Attachment is null");
			}

			var attDetails = _attachmentDetailRepository.Gets(false, p => p.AttachmentId == att.AttachmentId);
			if (attDetails != null && attDetails.Any())
			{
				var currentFileLocation = GetFileLocation();// _fileLocationService.GetCurrent();
				var transfer = Transfer.GetTransfer(currentFileLocation, _fileLocationSettings);
				foreach (var detail in attDetails)
				{
					_attachmentDetailRepository.Delete(detail);

					try
					{
						transfer.Delete(new FileTransferInfo
						{
							CreatedDate = detail.CreatedOnDate,
							FileName = detail.FileName,
							FileType = FileType.Attach,
							IdentityFolder = detail.IdentityFolder,
							RootFolder = detail.FileLocationKey
						});
					}
					catch { }
				}
			}

			_attachmentRepository.Delete(att);
			Context.SaveChanges();
		}

		/// <summary>
		/// Xóa tất cả file vật lý của danh sách attachment, dùng khi roleback liên thông.
		/// </summary>
		/// <param name="attachments"></param>
		public void DeleteAttachmentInStore(List<Attachment> attachments)
		{
			if (attachments == null || !attachments.Any())
			{
				return;
			}

			foreach (var att in attachments)
			{
				var attachmentDetails = att.AttachmentDetails;
				var currentFileLocation = GetFileLocation();
				var transfer = Transfer.GetTransfer(currentFileLocation, _fileLocationSettings);
				foreach (var detail in attachmentDetails)
				{
					try
					{
						transfer.Delete(new FileTransferInfo
						{
							CreatedDate = detail.CreatedOnDate,
							FileName = detail.FileName,
							FileType = FileType.Attach,
							IdentityFolder = detail.IdentityFolder,
							RootFolder = detail.FileLocationKey
						});
					}
					catch { }
				}
			}
		}

		/// <summary>
		/// Xóa tất cả file cứng trong thư mục đính kèm không nằm trong danh sách attachment hiện tại.
		/// </summary>
		public void DeleteAttachmentError()
		{
			var fromDate = new DateTime(2016, 1, 1);
			var toDateTest = new DateTime(2018, 10, 1);
			var now = DateTime.Now;
			var allDetails = _attachmentDetailRepository.GetsAs(d => d.FileName, d => d.CreatedOnDate >= fromDate && d.CreatedOnDate <= now).ToList();

			var months = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
			var years = new List<string>() { "2016", "2017", "2018" };
			var currentFileLocation = GetFileLocation();

			foreach (var year in years)
			{
				foreach (var month in months)
				{
					var path = Path.Combine(new string[] { currentFileLocation.FileLocationAddress, "Attach", year, month });
					if (!Directory.Exists(path))
					{
						continue;
					}

					var directoryInfo = new DirectoryInfo(path);

					var allFiles = directoryInfo.GetFiles("*", SearchOption.AllDirectories);
					if (!allFiles.Any())
					{
						continue;
					}

					foreach (var file in allFiles)
					{
						var fileName = Path.GetFileNameWithoutExtension(file.FullName);

						var hasUsed = allDetails.Any(d => d.Equals(fileName));
						if (hasUsed)
						{
							continue;
						}

						if (file.CreationTime >= fromDate && file.CreationTime <= now)
						{
							try
							{
								System.IO.File.Delete(file.FullName);
							}
							catch { }
						}
					}
				}
			}
		}

        /// <summary>
		/// Xóa tất cả file cứng trong thư mục đính kèm không nằm trong danh sách attachment hiện tại.
		/// </summary>
		public void DeleteAttachmentError(int timeYear, int timeMonth = 0)
        {
            var fromDate = new DateTime(timeYear, 1, 1);
            var toDateTest = new DateTime(timeYear, 12, 31);
            var now = DateTime.Now;
            var allDetails = _attachmentDetailRepository.GetsAs(d => d.FileName, d => d.CreatedOnDate >= fromDate && d.CreatedOnDate <= now).ToList();

            var months = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
            var count = 0;
            var countIn = 0;
            if (timeMonth != 0 )
            {
                months = new List<string>() { timeMonth.ToString() };
            }

            var years = new List<string>() { timeYear.ToString() };
            var currentFileLocation = GetFileLocation();

            foreach (var year in years)
            {
                foreach (var month in months)
                {
                    var path = Path.Combine(new string[] { currentFileLocation.FileLocationAddress, "Attach", year, month });
                    if (!Directory.Exists(path))
                    {
                        continue;
                    }
                    LogService(new List<string>() { path });
                    var directoryInfo = new DirectoryInfo(path);

                    var allFiles = directoryInfo.GetFiles("*", SearchOption.AllDirectories);
                    if (!allFiles.Any())
                    {
                        continue;
                    }

                    foreach (var file in allFiles)
                    {
                        var fileName = Path.GetFileNameWithoutExtension(file.FullName);

                        var hasUsed = allDetails.Any(d => d.Equals(fileName));
                        if (hasUsed)
                        {
                            continue;
                        }
                        countIn++;
                        if (file.CreationTime >= fromDate && file.CreationTime <= now)
                        {
                            try
                            {
                                System.IO.File.Delete(file.FullName);
                                count++;
                            }
                            catch(Exception ex) {
                                LogService(new List<string>() { ex.Message });
                            }
                        }
                    }
                }
            }
            LogService(new List<string>() { count.ToString(), countIn.ToString() });
        }

        /// <summary>
        /// Ghi log hành động
        /// </summary>
        /// <param name="message"></param>
        private void LogService(List<string> message)
        {
            var logFolder = CommonHelper.MapPath("~/Logs");
            var logFile = Path.Combine(logFolder, "logservice_" + DateTime.Now.ToString("ddMMyyyy"));
            try
            {
                System.IO.File.AppendAllLines(logFile, message);
            }
            catch { }
        }

        /// <summary>
        /// Thêm mới attachment
        /// </summary>
        /// <param name="attachment"></param>
        public void Create(Attachment attachment)
		{
			_attachmentRepository.Create(attachment);
			Context.SaveChanges();
		}

		private FileLocation GetFileLocation(int id = 0)
		{
#if DEBUG
            return new FileLocation() { FileLocationId = 2, FileLocationType = false, IsActivated = true, FileLocationAddress = @"C:\Att" };
#else
			if (id == 0)
			{
				return _fileLocationService.GetCurrent();
			}

			return _fileLocationService.Get(id);
#endif
		}
	}

	internal static class NativeMethods
	{
		[DllImport("BkavWordToPDF.dll", SetLastError = true, CharSet = CharSet.Unicode)]
		internal static extern bool ConvertWordToPDF(string pstrFileInput, string pstrFileOutput);
	}
}
