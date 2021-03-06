using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using Bkav.eGovCloud.Api.Dto;
using Bkav.eGovCloud.Api.Service;
using Bkav.eGovCloud.Business;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Document;
using Bkav.eGovCloud.Core.FileSystem;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Entities.Enum;
using Bkav.eGovCloud.Web.Framework;
using SolrNet.Utils;
using Newtonsoft.Json.Linq;
using Bkav.eGovCloud.Core.DynamicForm;
using Bkav.eGovCloud.Models;
using Bkav.eGovCloud.Business.Caching;
using Bkav.eGovCloud.Controllers;
using System.Dynamic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Bkav.eGovCloud.Core.Logging;
using System.Net.Http.Formatting;
using Bkav.eGovCloud.Entities.Customer.Settings;
using System.Data.SqlClient;
using ClosedXML.Excel;
using Bkav.eGovCloud.Core.Caching;
//using Spire.Xls;

namespace Bkav.eGovCloud.Api.Controller
{
	//[OAuthAuthorizeAttribute(Scope.Document)]
	public class DocumentController : EgovApiBaseController
	{
		private readonly AddressBll _addressService;
		private readonly AttachmentBll _attachmentService;
		private readonly ApproverBll _approverService;
		private readonly CommentBll _commentService;
		private readonly DailyProcessBll _dailyProcessService;
		private readonly DocumentBll _documentService;
		private readonly DocumentApiService _documentApiService;
		private readonly DocumentPublishBll _docPublishService;
		private readonly DocumentCopyBll _docCopyService;
		private readonly DocTimelineBll _docTimelineService;
		private readonly DocTypeBll _doctypeService;
		private readonly DepartmentBll _departmentService;
		private readonly PositionBll _positionService;
		private readonly InfomationBll _imfomationService;
		private readonly LogBll _logService;
		private readonly TransferSettings _transferSetting;
		private readonly SettingBll _settingService;
		private readonly SupplementaryBll _supplementaryService;
		private readonly UserBll _userService;
		private readonly WorkflowHelper _workflowHelper;
		private readonly CategoryBll _categoryService;
		private readonly FeeBll _feeService;
		private readonly PaperBll _paperService;
		private readonly FormHelper _formHelper;
		private readonly FormBll _formService;
		private readonly DocumentsCache _documentCache;
		private readonly CodeBll _codeService;
		private readonly DocumentHelper _documentHelper;
		private readonly DocTypeFormBll _docTypeFormService;
		private readonly TemplateKeyBll _templateService;
        private ReportConfigSettings _reportConfigSettings;
        private readonly SmsBll _smsService;
        private readonly MemoryCacheManager _cache;

        //mail sms
        private readonly Business.Utils.SendSmsHelper _smsHelper;
        private readonly Business.Utils.SendEmailHelper _mailHelper;

        private const int CACHE_TIME = 2 * 60; // 2 phút

		private const string DEFAULT_DATETIME_FORMAT = "dd/MM/yyyy";

		/// <summary>
		/// C'tor
		/// </summary>
		public DocumentController()
		{
			_documentService = DependencyResolver.Current.GetService<DocumentBll>();
			_docPublishService = DependencyResolver.Current.GetService<DocumentPublishBll>();
			_docCopyService = DependencyResolver.Current.GetService<DocumentCopyBll>();
			_doctypeService = DependencyResolver.Current.GetService<DocTypeBll>();
			_attachmentService = DependencyResolver.Current.GetService<AttachmentBll>();
			_addressService = DependencyResolver.Current.GetService<AddressBll>();
			_userService = DependencyResolver.Current.GetService<UserBll>();
			_supplementaryService = DependencyResolver.Current.GetService<SupplementaryBll>();
			_logService = DependencyResolver.Current.GetService<LogBll>();
			_transferSetting = DependencyResolver.Current.GetService<Bkav.eGovCloud.Entities.Customer.TransferSettings>();
			_dailyProcessService = DependencyResolver.Current.GetService<DailyProcessBll>();
			_imfomationService = DependencyResolver.Current.GetService<InfomationBll>();
			_settingService = DependencyResolver.Current.GetService<SettingBll>();
			_workflowHelper = DependencyResolver.Current.GetService<WorkflowHelper>();
			_documentApiService = DependencyResolver.Current.GetService<DocumentApiService>();
			_categoryService = DependencyResolver.Current.GetService<CategoryBll>();
			_feeService = DependencyResolver.Current.GetService<FeeBll>();
			_paperService = DependencyResolver.Current.GetService<PaperBll>();
			_formHelper = DependencyResolver.Current.GetService<FormHelper>();
			_formService = DependencyResolver.Current.GetService<FormBll>();
			_docTimelineService = DependencyResolver.Current.GetService<DocTimelineBll>();
			_commentService = DependencyResolver.Current.GetService<CommentBll>();
			_approverService = DependencyResolver.Current.GetService<ApproverBll>();
			_departmentService = DependencyResolver.Current.GetService<DepartmentBll>();
			_codeService = DependencyResolver.Current.GetService<CodeBll>();
			_documentCache = DependencyResolver.Current.GetService<DocumentsCache>();
			_positionService = DependencyResolver.Current.GetService<PositionBll>();
			_documentHelper = DependencyResolver.Current.GetService<DocumentHelper>();
			_docTypeFormService = DependencyResolver.Current.GetService<DocTypeFormBll>();
            _reportConfigSettings = DependencyResolver.Current.GetService<ReportConfigSettings>();
            _templateService = DependencyResolver.Current.GetService<TemplateKeyBll>();
            _smsHelper = DependencyResolver.Current.GetService<Business.Utils.SendSmsHelper>();
            _mailHelper = DependencyResolver.Current.GetService<Business.Utils.SendEmailHelper>();
            _smsService = DependencyResolver.Current.GetService<SmsBll>();
        }

		#region Api cho liên thông hồ sơ, văn bản

		/// <summary>
		/// Trả về danh sách các documentid chờ ban hành
		/// </summary>
		/// <returns>Danh sách DocumentId</returns>
		[System.Web.Http.HttpGet]
		[OutputCache(Duration = CACHE_TIME)]
		public List<string> GetPendingDocumentIds()
		{
			// Lấy văn bản chờ gửi liên thống.
			// Hsmc gửi trực tiếp
			var pending = _docPublishService.GetPending().Where(d => !d.IsHsmc);
			return pending.Select(p => p.DocumentPublishId.ToString()).Distinct().ToList();
		}

		/// <summary>
		/// Lấy thông tin chi tiết hồ sơ cần phát hành liên thông theo ID
		/// </summary>
		/// <param name="documentId">ID hồ sơ trả về từ GetPendingDocumentIds</param>
		/// <returns>Thông tin chi tiết hồ sơ</returns>
		/// 
		[System.Web.Http.HttpGet]
		[OutputCache(Duration = CACHE_TIME)]
		public DocumentDto GetDocument(string documentId)
		{
			var result = new DocumentDto();
			int documentPublishId;
			if (!Int32.TryParse(documentId, out documentPublishId))
			{
				throw new Exception("Tham số documentId truyền vào không đúng");
			}

			// swa hamf nay de tiep nhan 
			var currentOrgan = _addressService.GetCurrent();
			if (currentOrgan == null)
			{
				throw new Exception("Chưa thiết lập cơ quan liên thông hiện tại");
			}

			var docPublish = _docPublishService.Get(documentPublishId);
			if (docPublish == null)
			{
				throw new Exception("Thông tin phát hành không tồn tại: " + documentPublishId);
			}

			var codes = SplitDocCode(docPublish.DocCode);
			if (codes.Length != 2)
			{
				_docPublishService.UpdateSendFail(docPublish, "Số ký hiệu không đúng chuẩn.");
				return null;
			}

			if (string.IsNullOrEmpty(docPublish.AddressCode))
			{
				_docPublishService.UpdateSendFail(docPublish, "Cơ quan chưa có mã định danh.");
				return null;
			}

			var documentCopy = _docCopyService.Get(docPublish.DocumentCopyId);
			if (documentCopy == null)
			{
				throw new Exception("Hồ sơ gốc không tồn tại: " + docPublish.DocumentCopyId);
			}
			var document = documentCopy.Document;

			var userSuccessName = document.UserSuccessName;
			if (string.IsNullOrEmpty(userSuccessName) && document.UserSuccessId > 0)
			{
				var userSuccess = _userService.GetFromCache(document.UserSuccessId.Value);
				userSuccessName = userSuccess == null ? "" : userSuccess.FullName;
			}
			if (string.IsNullOrEmpty(userSuccessName))
			{
				_docPublishService.UpdateSendFail(docPublish, "Người ký không đúng.");
				return null;
			}

			var docRelations = _documentService.GetDocRelations(d => d.DocumentId == document.DocumentId); //  document.DocRelations;
			var relateds = new List<RelatedDocument>();
			if (docRelations.Any())
			{
				var hasThuHoi = docRelations.Any(d => d.RelationType == (int)RelationTypes.LienQuanThuHoi);
				if (hasThuHoi)
				{
					result.Bussiness = new Bussiness()
					{
						BussinessDocType = 2,
						BussinessDocReason = document.Compendium
					};
					result.ResponseFor = docRelations.Select(d => new ResponseFor()
					{
						Code = d.DocCode,
						OrganId = docPublish.AddressCode,
						PromulgationDate = docPublish.DatePublished.ToString("YYYY/MM/dd"),
						DocumentId = ""
					}).ToList();
				}
				else
				{
					// Hồ sơ liên quan
					relateds = docRelations.Where(d => !string.IsNullOrEmpty(d.DocCode)).Select(d =>
					{
						var codeValues = SplitDocCode(d.DocCode);
						var codeNum = codeValues.Length == 2 ? codeValues[0] : d.DocCode;
						var codeNotation = codeValues.Length == 2 ? codeValues[1] : "";
						return new RelatedDocument()
						{
							OrganId = currentOrgan.EdocId,
							PromulgationDate = !d.DateArrived.HasValue ? "" : d.DateArrived.Value.ToString(DEFAULT_DATETIME_FORMAT),
							Subject = d.Compendium,
							CodeNumber = codeNum,
							CodeNotation = codeNotation
						};
					}).ToList();
				}
			}

			var docAttachments = document.Attachments; // _attachmentService.Gets(document.DocumentId);
			if (!docAttachments.Any())
			{
				_docPublishService.UpdateSendFail(docPublish, "Văn bản không có file đính kèm.");
				return null;
			}

			var attachmentIds = docAttachments.Select(a => a.AttachmentId).ToList();
			var attachments = _attachmentService.DownloadAttachmentName(attachmentIds, 123, false, true);
			if (attachments.Any(a => string.IsNullOrEmpty(a.Value)))
			{
				_docPublishService.UpdateSendFail(docPublish, "File đính kèm không có nội dung. Vui lòng kiểm tra lại.");
				return null;
			}

			var attachmentDtos = attachments.Select(a => new AttachmentDto()
			{
				Description = "",
				Name = a.Key,
				Value = a.Value
			}).ToList();

			var organId = currentOrgan.EdocId;
			var organName = currentOrgan.AddressString;
			if (string.IsNullOrEmpty(organName))
			{
				_docPublishService.UpdateSendFail(docPublish, "Chưa cấu hình địa chỉ liên thông cho cơ quan hiện tại.");
				return null;
			}

			#region Thông tin hsmc

			// Thông tin công dân
			result.CitizenInfo = new CitizenDto()
			{
				IdCard = document.IdentityCard,
				Email = document.Email,
				Address = document.Address,
				Name = document.CitizenName,
				Phone = document.Phone
			};

			// Lệ phí
			result.Fees = document.DocFees.Select(df => new FeeDto()
			{
				Name = df.FeeName,
				Value = df.Price,
				Currency = "Đồng"
			}).ToList();

			// Giấy tờ
			result.DocumentPapers = document.DocPapers.Select(dp => new DocumentPaper()
			{
				Name = dp.PaperName,
				Amount = dp.Amount
			}).ToList();

			#endregion

			// Cơ quan gửi
			result.Sender = new Organization()
			{
				//OrganId = currentOrgan.EdocId,
				//OrganId = currentOrgan.EdocId,
				//OrganName = currentOrgan.Name
				OrganId = organId,
				OrganName = organName
			};

			// Cơ quan nhận
			result.Receivers = new List<Organization>(){new Organization(){
				OrganId = docPublish.AddressCode,
				OrganName = docPublish.AddressName
			}};

			result.Relateds = relateds;
			result.Attachments = attachmentDtos;
			result.DocumentId = documentPublishId.ToString();
			result.Subject = document.Compendium;
			result.CodeNumber = codes[0];

			// TienBV: Thêm đuôi phía sau để tránh trùng code khi gửi nhiều đơn vị.
			// Mỗi đơn vị 1 hạn xử lý riêng nên phải gửi ntn
			// Todo: cần tính lại chỗ này để tiết kiệm băng thông
			result.CodeNotation = string.Format("{0}#{1}", codes[1], docPublish.AddressCode);

			result.Place = currentOrgan.AddressString;
			result.PromulgationDate = document.DateSuccess.HasValue ? document.DateSuccess.Value.ToString(DEFAULT_DATETIME_FORMAT) : string.Empty;
			result.TypeCode = document.DocTypeId.ToString();
			result.TypeName = document.DocTypeName;
			result.DueDate = docPublish.DateAppointed.HasValue ? docPublish.DateAppointed.Value.ToString(DEFAULT_DATETIME_FORMAT) : string.Empty;
			result.Priority = 3;
			result.DateAppointed = docPublish.DateAppointed.HasValue ? docPublish.DateAppointed.Value.ToString(DEFAULT_DATETIME_FORMAT) : string.Empty;

			result.SignerName = userSuccessName;

			return result;
		}

		/// <summary>
		/// Xác nhận đã phát hành hồ sơ thành công.
		/// Những DocumentId được confirm sẽ không xuất hiện trong kết quả trả về của GetPendingDocumentIds
		/// </summary>
		/// <param name="documentId">Id hồ sơ</param>
		/// <returns>True nếu confirm thành công; False nếu thất bại</returns>
		[System.Web.Http.HttpPost]
		public bool ConfirmSent(string documentId)
		{
			Int32 docpublishId;
			if (!Int32.TryParse(documentId, out docpublishId))
			{
				throw new Exception("Tham số documentId truyền vào không đúng");
			}

			try
			{
				_docPublishService.ConfirmSent(docpublishId);
				return true;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// Cập nhật trạng thái gửi lỗi
		/// </summary>
		/// <param name="documentId"></param>
		/// <param name="message"></param>
		/// <returns></returns>
		public bool ConfirmSentFail(int documentId, string message)
		{
			try
			{
				var docPublish = _docPublishService.Get(documentId);
				if (docPublish == null)
				{
					return false;
				}

				_docPublishService.UpdateSendFail(docPublish, message);
				return true;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// Lưu hồ sơ liên thông nhận về từ Hệ thống trung gian.
		/// Bao gồm cả lưu vào CSDL và lưu file đính kèm.
		/// </summary>
		/// <param name="document">Thông tin chi tiết hồ sơ</param>
		/// <returns>True nếu lưu thành công; False nếu thất bại</returns>
		[System.Web.Http.HttpPost]
		public bool SaveDocument([FromBody]DocumentDto document)
		{
			if (document == null)
			{
				throw new ArgumentNullException("document");
			}

			var attachments = new List<Attachment>();
			try
			{
				//var userReceivedDocumentIn = _transferSetting.UserReceiveId;
				var sender = document.Sender;
				var receivers = document.Receivers;
				Organization receiver;
				if (receivers == null || !receivers.Any())
				{
					var currentOrgan = _addressService.GetCurrent();
					if (currentOrgan == null)
					{
						throw new ArgumentNullException("Chưa cấu hình cơ quan hiện tại.");
					}

					receiver = new Organization()
					{
						OrganId = currentOrgan.EdocId,
						OrganName = currentOrgan.Name
					};
				}
				else
				{
					receiver = receivers.First();
				}

				var citizen = document.CitizenInfo;
				var isHsmc = (citizen != null && !string.IsNullOrEmpty(citizen.Name));  // đang lấy tạm cái này do eDoc chưa quy định trường văn bản hay hồ sơ

				var userReceiveds = isHsmc ? _transferSetting.GetUserReceiveHsmc(receiver.OrganId) : _transferSetting.GetUserReceiveVbDen(receiver.OrganId);
				if (userReceiveds == null || !userReceiveds.Any())
				{
					throw new Exception("Hệ thống chưa cấu hình tài khoản tiếp nhận liên thông.");
				}

				// Loại bỏ phần dư thừa khi gửi cho nhiều cơ quan trong số ký hiệu
				// Khi gửi mặc định thêm #Ma_dinh_danh# vào sau SKH
				var codeNotation = document.CodeNotation.Split('#').First();
				var docCode = string.Format("{0}/{1}", document.CodeNumber, codeNotation);

				// Lấy mặc định người đầu tiên trong danh sách cấu hình là người khởi tạo.
				// Sau khi phân loại tiếp nhận văn bản, hồ  sơ đến thì cần cập nhật lại người khởi tạo là người tiếp nhận lúc đó.
				// var userReceivedDocumentIn = userReceiveds.First();
				var userReceivedIds = userReceiveds.Select(u => u.UserId).Distinct().ToList();
				var inoutPlace = userReceiveds.First().DepartmentName;
				var userReceivedId = userReceiveds.First().UserId;

				var tempFiles = new Dictionary<string, IDictionary<string, string>>();
				foreach (var attachment in document.Attachments)
				{
					if (string.IsNullOrEmpty(attachment.Link))
					{
						var data = Convert.FromBase64String(attachment.Value);
						var stream = new MemoryStream(data);
						var tempPath = ResourceLocation.Default.FileUploadTemp;
						var fileInfo = FileManager.Default.Create(stream, tempPath);
						var tempDic = new Dictionary<string, string>();
						tempDic.Add("name", attachment.Name);
						tempFiles.Add(fileInfo.Name, tempDic);
					}
					else
					{
						attachments.Add(new Attachment()
						{
							AttachmentName = attachment.Name,
							Size = attachment.Value.Length,
							AttachmentDetails = new List<AttachmentDetail>()
							{
								new AttachmentDetail(){
									AttachLink = attachment.Link,
									IsLink = true,
									Size = attachment.Value.Length,
									CreatedByUserId = userReceivedId,
									CreatedOnDate = DateTime.Now,
									FileName = "",
									FileLocationId = 0,
									FileLocationKey = "",
									IdentityFolder = "0",
									CreatedByUserName = ""
								}
							}
						});
					}
				}

				if (tempFiles.Count > 0)
				{
					attachments.AddRange(_attachmentService.AddAttachmentInDoc(tempFiles, userReceivedId, true));
				}

				// Kiểm tra có văn bản đang liên thông thì update vào văn bản đó.

				IEnumerable<DocPublish> docPublishHasWaitingResults = new List<DocPublish>();

				if (isHsmc)
				{
					docPublishHasWaitingResults = _docPublishService.Gets(false,
														p => p.DocCode.Equals(docCode) && p.HasLienThong && !p.IsResponsed);
				}

				var docPublish = !docPublishHasWaitingResults.Any() ? null : docPublishHasWaitingResults.First();

				var originalDocument = docPublish == null ? null : _docCopyService.Get(docPublish.DocumentCopyId);
				if (originalDocument != null)
				{
					return UpdateOriginalDocument(originalDocument, document, attachments, docPublish);
				}

				var category = _categoryService.GetsFromCache().First();

				var userCreated = _userService.GetFromCache(userReceivedId);
				// Tạo document mới
				var eGovdocument = new Document()
				{
					CategoryBusinessId = (int)CategoryBusinessTypes.VbDen,
					CategoryId = category.CategoryId,
					CategoryName = category.CategoryName,
					DocumentId = Guid.NewGuid(),
					DocCode = docCode,
					DateCreated = DateTime.Now,
					Compendium = document.Subject,
					Original = 2,
					Organization = sender.OrganName,
					OrganizationCode = sender.OrganId,
					InOutPlace = inoutPlace,
					TotalPage = 1,
					UserCreatedId = userReceivedId,
					UserCreatedName = userCreated == null ? "" : userCreated.FullName,
					DateModified = DateTime.Now,
					DateArrived = DateTime.Now,
					DatePublished = DateTime.ParseExact(document.PromulgationDate, DEFAULT_DATETIME_FORMAT, CultureInfo.InvariantCulture),
					Attachments = attachments,

					// Mã định danh văn bản của văn bản đến
					Address = document.DocumentId
				};

				if (!string.IsNullOrEmpty(document.DateAppointed))
				{
					eGovdocument.DateResponse = DateTime.ParseExact(document.DateAppointed, DEFAULT_DATETIME_FORMAT, CultureInfo.InvariantCulture);
					eGovdocument.DateAppointed = DateTime.ParseExact(document.DateAppointed, DEFAULT_DATETIME_FORMAT, CultureInfo.InvariantCulture);
				}

				// Xử lý nếu là liên thông hồ sơ một cửa      
				if (isHsmc)
				{
#if HoSoMotCuaEdition
					// Nghiệp vụ
					eGovdocument.CategoryBusinessId = (int)CategoryBusinessTypes.Hsmc;

					// Thông tin công dân
					eGovdocument.CitizenName = citizen.Name;
					eGovdocument.Address = citizen.Address;
					eGovdocument.IdentityCard = citizen.IdCard;
					eGovdocument.Email = citizen.Email;
					eGovdocument.Phone = citizen.Phone;

					// Giấy tờ
					var papers = document.DocumentPapers;
					if (papers != null && papers.Any())
					{
						foreach (var paper in papers)
						{
							eGovdocument.DocPapers.Add(new Entities.Customer.DocPaper()
							{
								PaperName = paper.Name,
								Amount = paper.Amount,
								Type = 1
							});
						}
					}

					// Lệ phí
					var fees = document.Fees;
					if (fees != null && fees.Any())
					{
						foreach (var fee in fees)
						{
							eGovdocument.DocFees.Add(new Entities.Customer.DocFee()
							{
								FeeName = fee.Name,
								Price = fee.Value,
								Type = 1
							});
						}
					}
#endif
				}


				var relateds = new Dictionary<string, string>();

				foreach (var r in document.Relateds)
				{
					if (string.IsNullOrEmpty(r.OrganId))
					{
						continue;
					}
					var rDocCode = string.Format("{0}/{1}", r.CodeNumber, r.CodeNotation);

					if (relateds.ContainsKey(rDocCode))
					{
						continue;
					}

					relateds.Add(rDocCode, r.OrganId);
				}

				if (CheckDocCodeUsed(docCode, CategoryBusinessTypes.VbDen, eGovdocument.Organization, "")) // inoutplace
				{
					// Van ban da co tren he thong
					eGovdocument.Status = (int)DocumentStatus.LienThongTrungSo;
				}

				var newDocumentCopy = _documentService.CreateComingDocument(eGovdocument, userReceivedId, userReceivedIds, relateds);

				var createdComment = string.Format("Văn bản liên thông đến lúc: {0}.", DateTime.Now.ToString("HH:mm dd/MM/yyyy"));
				_commentService.SendCommentCommon(newDocumentCopy, 0, DateTime.Now, createdComment, CommentType.Common);

				_codeService.AddUsedCache(eGovdocument.DocumentId, eGovdocument.DocCode, "", eGovdocument.CategoryBusinessId, eGovdocument.Organization, null);

				if (document.Bussiness != null && document.Bussiness.BussinessDocType == 1)
				{
					ThuHoi(newDocumentCopy, document.ResponseFor);
				}

				return true;
			}
			catch (Exception ex)
			{
				// xoas file đã lưu
				_attachmentService.DeleteAttachmentInStore(attachments);
				foreach (var att in document.Attachments)
				{
					att.Value = att.Value.Substring(0, 200);
				}
				_logService.Error("Lỗi Tiếp nhận văn bản liên thông: {0}", ex, new string[] { document.Stringify() });
				return false;
			}
		}

		private void ThuHoi(DocumentCopy newDocumentCopy, List<ResponseFor> responseFor)
		{
			foreach (var res in responseFor)
			{
				var datePublish = DateTime.ParseExact(res.PromulgationDate, "", CultureInfo.InvariantCulture);

				// Văn bản cần thu hồi
				var originDocument = _documentService.GetByMaDinhDanhVanBan(res.DocumentId, res.Code, datePublish);
				if (originDocument == null)
				{
					continue;
				}

				var originDocCopy = _docCopyService.GetMain(originDocument.DocumentId);

				originDocument.LienThongStatus = LienThongStatus.YeuCauThuHoi.ToString();

				var docRelation = new DocRelation()
				{
					DocumentId = newDocumentCopy.DocumentId,
					DocumentCopyId = newDocumentCopy.DocumentCopyId,

					RelationType = (int)RelationTypes.LienQuanThuHoi,

					RelationCopyId = originDocCopy.DocumentCopyId,
					RelationId = originDocCopy.DocumentId,
					Compendium = originDocument.Compendium,
					CategoryName = originDocument.CategoryName,
					DateArrived = originDocument.DateArrived,
					DocCode = originDocument.DocCode,
					InOutCode = originDocument.InOutCode
				};

				_documentService.CreateDocRelations(docRelation, false);
			}

			newDocumentCopy.Document.LienThongStatus = LienThongStatus.YeuCauThuHoi.ToString();

			_documentService.SaveChanges();
		}

		/// <summary>
		/// Trả về danh sách trạng thái hiện tại của các văn bản liên thông đến
		/// </summary>
		/// <param name="from">Lấy từ khoảng thời gian</param>
		/// <param name="to">Đến khoảng thời gian</param>
		/// <returns></returns>
		[System.Web.Http.HttpGet]
		[OutputCache(Duration = CACHE_TIME)]
		public List<DocumentStatusDto> GetStatus(DateTime from, DateTime to)
		{
			var result = new List<DocumentStatusDto>();

			#region Xử lý văn bản đã từ chối

			// Các văn bản chưa phân loại đã kết thúc hoặc đã hủy
			var cancelDocs = _documentService.Gets(true, d => d.Original == 2 && d.DocTypeId == null && d.DateModified >= from && d.DateModified <= to
										 && (d.Status == (int)DocumentStatus.KetThuc || d.Status == (int)DocumentStatus.LoaiBo));
			if (cancelDocs.Any())
			{
				result.AddRange(cancelDocs.Select(d => new DocumentStatusDto()
				{
					DocCode = d.DocCode,
					OrganId = d.OrganizationCode,
					Status = 2,
					TimeStamp = d.DateFinished ?? d.DateModified,
					Message = d.Note,
					UserUpdate = "Văn thư",
					DocumentId = d.Address
				}));
			};

			#endregion

			#region Xử lý văn bản đã phân loại và gửi vào trong

			// Trả về timelines của các văn bản liên thông đến đang xử lý.
			// Các văn bản chưa phân loại và chuyển thì không có trong timeline này.
			// => Trạng thái 02 sẽ xử lý riêng (trạng thái 01 tool đã tự xử lý)
			var timelines = Json2.ParseAs<List<DocTimeLine>>(Json2.Stringify(_docTimelineService.GetDocOnlineTimelines(from, to)));
			if (timelines.Any())
			{
				var groups = timelines.GroupBy(tl => tl.DocumentId);

				var approvers = GetApprovers();

				foreach (var docGroup in groups)
				{
					var docTimelines = docGroup.Where(g => g.FromDate >= from && g.FromDate <= to).OrderBy(g => g.FromDate).ToList();

					// Do lấy theo thời gian cập nhật văn bản từ @from đến @to nên sẽ có những đoạn timeline nằm ngoài khoảng cần lấy.
					var current = docTimelines.LastOrDefault(g => g.FromDate <= to);
					if (current == null) continue;

					var lastTimeline = docGroup.LastOrDefault(g => g.FromDate < from);
					var lastStatus = lastTimeline == null ? 0 : lastTimeline.Status;

					var firstTime = docTimelines.FirstOrDefault();

					var status = current.Status;

					var docCode = current.DocCode;
					var organId = current.OrganizationCode;
					var userId = current.UserId;
					var user = _userService.GetFromCache(userId);

					var documentStatus = new DocumentStatusDto()
					{
						DocCode = docCode,
						OrganId = organId,
						UserUpdate = user == null ? "" : user.FullName,
						DocumentId = current.EDocumentId
					};

					// Thêm các trạng thái đã vào sổ, phân phân loại, đã tiếp nhận		
					var hasPhanLoai = firstTime.UserSendId == firstTime.UserCreatedId;
					if (hasPhanLoai)
					{
						var userReceive = _userService.GetFromCache(current.UserCreatedId);
						result.Add(new DocumentStatusDto()
						{
							DocCode = docCode,
							OrganId = organId,
							UserUpdate = userReceive == null ? "" : userReceive.FullName,
							Status = 3,
							TimeStamp = firstTime.FromDate,
							Message = String.Format("Người tiếp nhận: {0}", userReceive.FullName),
							DocumentId = current.EDocumentId
						});
					}

					// Trạng thái đã phân công
					var phanCong = docTimelines.FirstOrDefault(tl => approvers.Contains(tl.UserSendId));
					if (phanCong != null)
					{
						var userPc = _userService.GetFromCache(phanCong.UserSendId);
						result.Add(new DocumentStatusDto()
						{
							DocCode = docCode,
							OrganId = organId,
							UserUpdate = userPc == null ? "" : userPc.FullName,
							Status = 4,
							TimeStamp = phanCong.FromDate,
							Message = string.Format("Người phân công: {0}", userPc == null ? "" : userPc.FullName)
						});
					}

					// Trường hợp văn bản đã kết thúc hoặc đã hủy
					if (status == (int)DocumentStatus.KetThuc || status == (int)DocumentStatus.LoaiBo)
					{
						var dateFinish = current.DateFinished;
						documentStatus.Status = status == (int)DocumentStatus.KetThuc ? 6 : 2;
						documentStatus.TimeStamp = dateFinish ?? current.FromDate;
						documentStatus.Message = status == (int)DocumentStatus.KetThuc ? "Văn bản đã hoàn thành." : "Văn bản đã bị từ chối";
						result.Add(documentStatus);

						continue;
					}

					// Trạng thái đang xử lý
					var date = current.FromDate;
					documentStatus.Status = 5;
					documentStatus.TimeStamp = date;
					documentStatus.Message = string.Format("Văn bản đang xử lý bởi: {0}", user == null ? "" : user.FullName);
					result.Add(documentStatus);
				}
			}

			#endregion

			#region xử lý văn bản yêu cầu lấy lại

			var recalled = _docPublishService.Gets(true, p => p.Status == 13 && p.DateSent >= from && p.DateSent <= to);
			if (recalled.Any())
			{
				result.AddRange(recalled.Select(r => new DocumentStatusDto()
				{
					DocCode = r.DocCode,
					OrganId = r.AddressCode,
					Status = 13,
					TimeStamp = DateTime.Now,
					Message = r.Note,
					UserUpdate = r.UserPublishName,
					DocumentId = r.DocumentId.ToString("N")
				}));
			}

			#endregion

			#region Xử lý văn bản có yêu cầu thu hồi

			var ltStatus = new List<string>() { LienThongStatus.DongYThuHoi.ToString(), LienThongStatus.TuChoiThuHoi.ToString() };
			var thuhois = _documentService.Gets(true, d => d.Original == 2 && d.DateModified >= from && d.DateModified <= to
										 && ltStatus.Contains(d.LienThongStatus)
										 && (d.Status == (int)DocumentStatus.KetThuc || d.Status == (int)DocumentStatus.LoaiBo));
			if (thuhois.Any())
			{
				result.AddRange(thuhois.Select(d => new DocumentStatusDto()
				{
					DocCode = d.DocCode,
					OrganId = d.OrganizationCode,
					Status = d.LienThongStatus.Equals(LienThongStatus.DongYThuHoi.ToString()) ? 3 : 2,
					TimeStamp = d.DateFinished ?? d.DateModified,
					Message = d.Note,
					UserUpdate = "Văn thư",
					DocumentId = d.Address
				}));
			}

			#endregion

			return result;
		}

		/// <summary>
		/// Cập nhật trạng thái xử lý văn bản liên thông
		/// </summary>
		/// <param name="status"></param>
		/// <returns></returns>
		[System.Web.Http.HttpPost]
		public bool UpdateStatus(DocumentStatusDto status)
		{
			if (status == null)
			{
				throw new Exception("Đầu vào lỗi: status = null");
			}

			var document = _documentService.Gets(true, d => d.Address.Equals(status.DocumentId));
			if (document.Count() != 1)
			{
				throw new Exception("Văn bản không tồn tại với Id: " + status.DocumentId);
			}

			var docId = document.First().DocumentId;
			var result = _docPublishService.UpdateStatusLT(docId, status.OrganId, status.DocCode, status.Status, status.UserUpdate, status.Message);

			return result;
		}

		private IEnumerable<int> GetApprovers()
		{
			var positionApprovers = _positionService.GetAllApprovers().Select(a => a.PositionId);
			var allUserPosition = _departmentService.GetCacheAllUserDepartmentJobTitlesPosition();
			return allUserPosition.Where(a => positionApprovers.Contains(a.PositionId)).Select(a => a.UserId).Distinct();
		}

		/// <summary>
		/// Trả về tiến độ hồ sơ mới nhất
		/// </summary>
		/// <param name="lastUpdate">Thời gian cập nhật gần nhất</param>
		/// <returns></returns>
		[System.Web.Http.HttpGet]
		[OutputCache(Duration = CACHE_TIME)]
		public List<DocumentTrace> GetTraces(DateTime? lastUpdate)
		{
			var docModified = _docCopyService.GetDocumentLienThongModified(lastUpdate);
			if (!docModified.Any())
			{
				return new List<DocumentTrace>();
			}

			var result = new List<DocumentTrace>();
			var docCopyIds = docModified.Select(d => d.DocumentCopyId).ToList();
			var dailyProcess = _dailyProcessService.Gets(docCopyIds, lastUpdate).OrderBy(d => d.DateCreated).ToList();

			dailyProcess = dailyProcess
						.Where(d => EnumHelper<DocumentProcessType>.ContainFlags(
								DocumentProcessType.BanGiao | DocumentProcessType.KetThuc | DocumentProcessType.KyDuyet | DocumentProcessType.TraKetQua, d.ProcessTypeEnum)).ToList();

			foreach (var process in dailyProcess)
			{
				var doc = docModified.First(d => d.DocumentCopyId == process.DocumentCopyId);
				result.Add(new DocumentTrace()
				{
					DocCode = doc.Document.DocCode,
					DocumentCopyId = doc.DocumentCopyId,
					UserName = process.User.FullName,
					Comment = process.Note,
					OrganizationId = doc.Document.OrganizationCode,
					DateCreated = process.DateCreated,
					Status = doc.Document.Status
				});

				#region Kiểm tra tiến độ liên thông

				var docPublish = _docPublishService.GetSentPublishes(doc.DocumentCopyId);
				foreach (var publish in docPublish)
				{
					if (!string.IsNullOrEmpty(publish.Traces))
					{
						var traces = Json2.ParseAs<List<DocumentTrace>>(publish.Traces);
						result.AddRange(traces);
					}
				}

				#endregion
			}

			return result;
		}

		/// <summary>
		/// Lấy tiến độ của một hồ sơ
		/// </summary>
		/// <param name="docCode"></param>
		/// <param name="organizationCode"></param>
		/// <returns></returns>
		[System.Web.Http.HttpGet]
		[OutputCache(Duration = CACHE_TIME)]
		public List<DocumentTrace> GetListTraces(string docCode, string organizationCode)
		{
			var doc = _documentService.GetLT(docCode, organizationCode);
			if (doc == null)
			{
				// Trường hợp hồ sơ hồi báo về thì lấy theo hồ sơ gốc gửi đi (không phải là hồ sơ tiếp nhận liên thông).
				doc = _documentService.Get(docCode);
				if (doc == null)
				{
					return null;
				}
			}

			var documentCopy = _docCopyService.GetMain(doc.DocumentId);
			var result = new List<DocumentTrace>();

			var dailyProcess = _dailyProcessService.Gets(documentCopy.DocumentCopyId).OrderBy(d => d.DateCreated).ToList();
			dailyProcess = dailyProcess
						.Where(d => EnumHelper<DocumentProcessType>.ContainFlags(
								DocumentProcessType.BanGiao | DocumentProcessType.KetThuc | DocumentProcessType.KyDuyet | DocumentProcessType.TraKetQua, d.ProcessTypeEnum)).ToList();

			var status = doc.Status;
			foreach (var process in dailyProcess)
			{
				result.Add(new DocumentTrace()
				{
					DocCode = docCode,
					DocumentCopyId = documentCopy.DocumentCopyId,
					UserName = process.User.FullName,
					Comment = process.Note,
					OrganizationId = organizationCode,
					DateCreated = process.DateCreated,
					Status = status
				});
			}

			#region Kiểm tra tiến độ liên thông

			var docPublish = _docPublishService.GetSentPublishes(documentCopy.DocumentCopyId);
			foreach (var publish in docPublish)
			{
				if (string.IsNullOrEmpty(publish.Traces))
				{
					continue;
				}

				var traces = Json2.ParseAs<List<DocumentTrace>>(publish.Traces);
				if (traces != null)
				{
					result.AddRange(traces);
				}
			}

			#endregion

			return result;
		}

		/// <summary>
		/// Cập nhật tiến độ liên thông cho hồ sơ
		/// </summary>
		/// <param name="traces"></param>
		/// <returns></returns>
		[System.Web.Http.HttpPost]
		public bool SendTraces([FromBody]List<DocumentTrace> traces)
		{
			try
			{
				var groupByCodes = traces.GroupBy(d => new { d.DocCode, d.OrganizationId });
				foreach (var group in groupByCodes)
				{
					var docCode = group.Key.DocCode;
					var addressCode = group.Key.OrganizationId;
					var publishTraces = Json2.Stringify(group);

					_docPublishService.UpdateTraces(docCode, addressCode, publishTraces);
				}

				return true;
			}
			catch (Exception ex)
			{
				_logService.Error("Cập nhật tiến độ liên thông: ", ex);

				return false;
			}
		}

		#endregion

#if HoSoMotCuaEdition

		#region Api cho eGov Online

		//[System.Web.Http.HttpGet]
		//[OutputCache(Duration = CACHE_TIME)]
		//public DocumentOnlineLookup Lookup(string docCode)
		//{
		//	DocumentOnlineLookup result;
		//	docCode = HttpUtility.UrlDecode(docCode);
		//	//var doc = _documentService.GetHsmc(docCode);
		//	if (doc == null)
		//	{
		//		return null;
		//	}

		//	var documentcopy = _docCopyService.GetMain(doc.DocumentId);
		//	//var userSuccess = doc.UserSuccessId.HasValue ? _userService.GetFromCache(doc.UserSuccessId.Value).Username : "";
		//	var userReturn = doc.UserReturnedId.HasValue ? _userService.GetFromCache(doc.UserReturnedId.Value).Username : "";

		//	var currentUser = documentcopy.UserCurrentName;
		//	var currentDept = documentcopy.CurrentDepartmentName;

		//	var progress = GetProgress(documentcopy, doc);

		//	result = new DocumentOnlineLookup()
		//	{
		//		Compendium = doc.Compendium,
		//		DateReceived = doc.DateCreated,
		//		DateAppointed = doc.DateAppointed,
		//		DateFinished = doc.DateFinished,
		//		DateResult = doc.DateResult,
		//		DateReturned = doc.DateReturned,
		//		IsSuccess = doc.IsSuccess,
		//		IsSupplemented = doc.IsSupplemented,
		//		DateSuccess = doc.DateSuccess,
		//		DocCode = doc.DocCode,
		//		DocumentId = doc.DocumentId,
		//		IsReturned = doc.IsReturned,
		//		ResultStatus = doc.ResultStatus,
		//		ReturnNote = doc.ReturnNote,
		//		Status = doc.Status,
		//		SuccessNote = doc.SuccessNote,
		//		CitizenName = doc.CitizenName,
		//		Address = doc.Address,
		//		Phone = doc.Phone,
		//		IdCard = doc.IdentityCard,
		//		Email = doc.Email,
		//		UserReturned = userReturn,
		//		UserSuccess = doc.UserSuccessName,
		//		CurrentUser = currentUser,
		//		CurrentDept = currentDept,
		//		Progress = progress
		//	};

		//	result.DocTypeName = doc.DocTypeName ?? doc.Compendium;

		//	var supplementaries = _supplementaryService.Gets(doc.DocumentId, true);
		//	var suppModel = new List<SupplementaryDto>();
		//	if (supplementaries.Any())
		//	{
		//		foreach (var supplementary in supplementaries)
		//		{
		//			if (result.Status != (int)DocumentStatus.DungXuLy && !supplementary.IsReceived)
		//			{
		//				continue;
		//			}

		//			var supplementaryDto = supplementary.ToDto();
		//			supplementaryDto.Papers = Json2.ParseAs<IEnumerable<Paper>>(supplementary.Papers).ToDto();
		//			supplementaryDto.Fees = Json2.ParseAs<IEnumerable<Fee>>(supplementary.Fees).ToDto();
		//			// supplementaryDto.CommentSend = supplementary.SupplementaryDetail.First().Comment;
		//			//if (!string.IsNullOrEmpty(supplementary.PaperIds))
		//			//{
		//			//    supplementaryDto.Papers = papers.Where(p => supplementary.PaperIds.Contains(string.Format(";{0};", p.PaperId)))
		//			//        .ToDto();
		//			//}
		//			//if (!string.IsNullOrEmpty(supplementary.FeeIds))
		//			//{
		//			//    supplementaryDto.Fees = fees.Where(f => supplementary.FeeIds.Contains(string.Format(";{0};", f.FeeId))).ToDto();
		//			//}

		//			suppModel.Add(supplementaryDto);
		//		}

		//		result.Supplementaries = suppModel;
		//	}

		//	return result;
		//}

		///// <summary>
		///// Ham lay ra 50 ban ghi moi nhat de hien thi man hinh lon
		///// </summary>
		///// <returns></returns>
		//[System.Web.Http.HttpGet]
		//[OutputCache(Duration = CACHE_TIME)]
		//public IEnumerable<DocumentDisplay> GetsDocument()
		//{
		//	var documents = _documentService.GetNewestHsmc(50);
		//	var documentDisplays = new List<DocumentDisplay>();
		//	foreach (var document in documents)
		//	{
		//		var documentDisplay = new DocumentDisplay();
		//		documentDisplay.DocumentId = document.DocumentId;
		//		documentDisplay.DocCode = document.DocCode;
		//		documentDisplay.CitizenName = document.CitizenName;
		//		documentDisplay.Compendium = document.Compendium;
		//		documentDisplay.DateCreated = document.DateCreated;
		//		documentDisplay.DoctypeName = "";

		//		// Chỉ lấy 50 bản ghi và 5 phút chỉ lấy 1 lần duy nhất và lưu cache lên không cần xử lý lazy load
		//		documentDisplay.DoctypeName = document.DocTypeName;

		//		documentDisplays.Add(documentDisplay);
		//	}

		//	return documentDisplays;
		//}

		[System.Web.Http.HttpPost]
		public string GetFileBase64TemplateOnline([FromBody]DocTypeTemplateBase64 data)
		{
			if (!string.IsNullOrWhiteSpace(data.docDetail))
			{
				var formContent = data.docDetail;
				var token = JToken.Parse(formContent);
				if (token is JArray)
				{
					var dynamicResult = new List<string>();
					foreach (var json in token.Children<JObject>())
					{
						JsDocument jsDocument;
						if (DynamicFormHelper.TryParse(json.ToString(), out jsDocument))
						{
							var form = _formService.Get(Guid.Parse(jsDocument.FormId));
							if (form != null)
							{
								var filePath = CommonHelper.MapPath("/EmbryonicForm/") + form.EmbryonicLocationName;
								var streamDoc = _formHelper.ParseEmbryonic(jsDocument, filePath, data.IsPDF).ToBase64String();

								return streamDoc;
							}
						}
					}
				}
			}

			return "";
		}

		#endregion

#endif

		#region Private Methods

		private string[] SplitDocCode(string doccode)
		{
			var firstIndexOfS = doccode.IndexOf("/");
			if (firstIndexOfS < 0)
			{
				return new string[] { };
			}

			var code = doccode.Substring(0, firstIndexOfS);
			var codeNotation = doccode.Replace(code + "/", "");
			return new string[] { code, codeNotation.Trim() };
		}

		//private bool UpdateOriginalDocument(Entities.Customer.Document originalDocument, DocumentDto receivedDocument, List<Entities.Customer.Attachment> attachments)
		//{
		//    var docCopy = _docCopyService.GetMain(originalDocument.DocumentId);

		//    originalDocument.Status = (int)DocumentStatus.DangXuLy;
		//    originalDocument.DateRequireSupplementary = null;
		//    originalDocument.IsGettingOut = false;

		//    docCopy.DocumentCopyType = (int)DocumentCopyTypes.XuLyChinh;
		//    docCopy.DateReceived = DateTime.Now;

		//    var originalAttachments = originalDocument.Attachments;
		//    foreach (var attachment in attachments)
		//    {
		//        var checkAttachment = originalAttachments.Where(a => a.AttachmentName.Equals(attachment.AttachmentName));
		//        if (checkAttachment.Any())
		//        {
		//            //Todo: xem xử lý chổ này
		//        }
		//        else
		//        {
		//            attachment.DocumentId = originalDocument.DocumentId;
		//            attachment.AttachmentDetails.First().CreatedByUserId = docCopy.UserCurrentId;
		//            _attachmentService.Create(attachment);
		//        }
		//    }

		//    _docCopyService.Update(docCopy);

		//    var address = _addressService.GetByeDocId(receivedDocument.Sender.OrganId);
		//    if (address != null)
		//    {
		//        _docPublishService.UpdateResponsed(docCopy, address.AddressId, docCopy, receivedDocument.SignerName);
		//    }
		//    return true;
		//}

		private bool UpdateOriginalDocument(Entities.Customer.DocumentCopy docCopy, DocumentDto receivedDocument, List<Entities.Customer.Attachment> attachments, DocPublish docPublish)
		{
			var originalDocument = docCopy.Document;
			originalDocument.Status = (int)DocumentStatus.DangXuLy;
			originalDocument.DateRequireSupplementary = null;
			originalDocument.IsGettingOut = false;

			docCopy.DocumentCopyType = (int)DocumentCopyTypes.XuLyChinh;
			docCopy.DateReceived = DateTime.Now;

			var originalAttachments = originalDocument.Attachments;
			foreach (var attachment in attachments)
			{
				var checkAttachment = originalAttachments.Where(a => a.AttachmentName.Equals(attachment.AttachmentName));
				if (checkAttachment.Any())
				{
					//Todo: xem xử lý chổ này
				}
				else
				{
					attachment.DocumentId = originalDocument.DocumentId;
					attachment.AttachmentDetails.First().CreatedByUserId = docCopy.UserCurrentId;
					_attachmentService.Create(attachment);
				}
			}

			_docCopyService.Update(docCopy);
			_docCopyService.ClearCache(docCopy.DocumentCopyId);

			if (docPublish != null)
			{
				docPublish.IsResponsed = true;
				docPublish.DocumentCopyResponsed = docCopy.DocumentCopyId;
				docPublish.DocCodeResponsed = docCopy.Document.DocCode;
				docPublish.DateResponsed = DateTime.Now;
				//docPublish.Note = "";
			}

			_docPublishService.SaveChanges();

			return true;
		}

		private List<DocumentProcessDto> GetProgress(DocumentCopy documentCopy, Document doc)
		{
			var progress = new List<DocumentProcessDto>();
			if (documentCopy == null || documentCopy.Histories == null || !documentCopy.Histories.HistoryPath.Any())
			{
				return progress;
			}

			var historyPaths = documentCopy.Histories.HistoryPath;
			var lastDate = DateTime.Now;
			var currentOffice = _imfomationService.GetCurrentOfficeName();

			var userSentIds = historyPaths.Select(h => h.UserSendId).Distinct();
			var userSents = _userService.GetAllCached(isActivated: true).Where(u => userSentIds.Contains(u.UserId)).ToList();

			foreach (var history in historyPaths)
			{
				var userSend = userSents.SingleOrDefault(u => u.UserId == history.UserSendId);
				var dateCreate = history.DateCreated;
				var node = _workflowHelper.GetNode(history.WorkflowSendId, history.NodeSendId);
				var isSuccess = doc.IsSuccess == true
								|| (doc.IsReturned == true)
								|| (doc.Status == (int)DocumentStatus.KetThuc)
								|| history.UserReceives.Any();

				// Bỏ qua trường hợp tự chuyển đến cho mình ở node hiện tại.
				if ((history.UserSendId != history.UserReceiveId) && (history.NodeSendId != history.NodeReceiveId))
				{
					progress.Add(new DocumentProcessDto()
					{
						NodeName = node == null ? "" : node.NodeName,
						UserName = userSend == null ? "" : userSend.FullName,
						DateCreated = lastDate,
						IsSuccess = true,
						OfficeName = currentOffice
					});
				}

				if (!isSuccess)
				{
					var userReceive = _userService.GetFromCache(history.UserReceiveId);
					var nodeReceive = _workflowHelper.GetNode(history.WorkflowReceiveId, history.NodeReceiveId);
					progress.Add(new DocumentProcessDto()
					{
						NodeName = nodeReceive == null ? "" : nodeReceive.NodeName,
						UserName = userReceive == null ? "" : userReceive.FullName,
						DateCreated = dateCreate,
						IsSuccess = (doc.IsSuccess | doc.IsReturned) ?? false,
						OfficeName = currentOffice
					});
				}

				lastDate = dateCreate;
			}

			#region Kiếm tra tiến độ hồ sơ nếu là đang liên thông

			var docPublish = _docPublishService.GetSentPublishes(documentCopy.DocumentCopyId);
			if (docPublish.Any(p => !p.IsResponsed))
			{
				doc.Status = 16;
			}

			foreach (var publish in docPublish)
			{
				currentOffice = publish.AddressName;
				if (string.IsNullOrEmpty(publish.Traces))
				{
					progress.Add(new DocumentProcessDto()
					{
						NodeName = "Tiếp nhận",
						UserName = currentOffice,
						DateCreated = DateTime.Now,
						IsSuccess = publish.IsResponsed,
						OfficeName = currentOffice
					});
				}
				else
				{
					var traces = Json2.ParseAs<List<DocumentTrace>>(publish.Traces);
					progress.AddRange(traces.Select(t => new DocumentProcessDto()
					{
						DateCreated = t.DateCreated,
						NodeName = t.Comment,
						OfficeName = currentOffice,
						UserName = t.UserName,
						IsSuccess = true,
					}));
				}
			}

			#endregion

			return progress;
		}

		//private IEnumerable<Supplementary> GetSupplementary(DocumentCopy doc)
		//{
		//    return _supplementaryService.Gets(doc.DocumentId, false);
		//}

		//private IEnumerable<LienThongTracesModel> GetPublisheds(int documentCopyId)
		//{
		//    var docPublishes = _docPublishService.GetPublishes(documentCopyId).OrderByDescending(d => d.DatePublished);
		//    var result = new List<LienThongTracesModel>();
		//    foreach (var doc in docPublishes)
		//    {
		//        var addressName = doc.AddressName;
		//        var note = doc.IsPending
		//                        ? "Chưa nhận được hồ sơ, hồ sơ đang gửi."
		//                        : doc.IsResponsed
		//                            ? addressName + " đã phản hồi - " + doc.Note
		//                            : addressName + " đã nhận hồ sơ, đang xử lý";

		//        result.Add(new LienThongTracesModel()
		//        {
		//            PublishId = doc.DocumentPublishId,
		//            AddressName = doc.AddressName,
		//            AddressId = doc.AddressId.Value,
		//            DateSent = doc.DatePublished.ToString("hh:mm dd/MM/yyyy"),
		//            IsResponsed = doc.IsResponsed,
		//            Note = note,
		//            DateResponsed = doc.DateResponsed.HasValue ? doc.DateResponsed.Value.ToString("hh:mm dd/MM/yyyy") : "",
		//            Traces = GetLienThongTraces(doc.Traces)
		//        });
		//    }

		//    return result;
		//}

		//private List<LienThongTraces> GetLienThongTraces(string traces)
		//{
		//    var result = new List<LienThongTraces>();
		//    if (string.IsNullOrEmpty(traces) || traces == "null")
		//    {
		//        return result;
		//    }

		//    var lienThongTraces = Json2.ParseAs<List<Bkav.eGovCloud.Entities.Customer.eDoc.DocumentTrace>>(traces);
		//    foreach (var trace in lienThongTraces)
		//    {
		//        result.Add(new LienThongTraces()
		//        {
		//            Content = trace.Comment,
		//            UserName = trace.UserName,
		//            DateCreated = trace.DateCreated.ToString("hh:mm dd/MM/yyyy"),
		//            IsSuccess = true
		//        });
		//    }

		//    return result;
		//}

		//private IEnumerable<CommentModel> GetProcessComments(DocumentCopy documentCopy, int currentUserId)
		//{
		//    IEnumerable<Comment> result;
		//    result = _commentService.Gets(documentCopy);

		//    result = result.OrderByDescending(c => c.DateCreated).ToList();
		//    return result.ToListModel();
		//}

		private bool CheckDocCodeUsed(string docCode, CategoryBusinessTypes categoryBusiness, string organization, string inoutPlace)
		{
			var isUsed = _codeService.CodeIsUsed(docCode, true, 0, categoryBusiness, organization, inOutPlace: inoutPlace);

			return isUsed;
		}

		///// <summary>
		///// Hàm lấy các file đính kèm của 1 hồ sơ, và lấy theo phiên bản mới nhất
		///// </summary>
		///// <param name="documentId"></param>
		///// <returns></returns>
		//[System.Web.Http.HttpGet]
		//public List<AttachmentBWSSDto> GetAttachments(Guid documentId)
		//{
		//    var attachments = _attachmentService.Gets(documentId);
		//    var attachmentsDto = new List<AttachmentBWSSDto>();
		//    foreach (var attachment in attachments)
		//    {
		//        var attachmentDetail = attachment.AttachmentDetails.FirstOrDefault(a =>
		//          a.VersionAttachmentDetail == attachment.VersionAttachment);
		//        attachmentsDto.Add(new AttachmentBWSSDto
		//        {
		//            AttachmentId = attachment.AttachmentId,
		//            AttachmentName = attachment.AttachmentName,
		//            DocumentId = attachment.DocumentId,
		//            IsDelete = attachment.IsDeleted,
		//            Size = attachmentDetail.Size
		//        });
		//    }
		//    return attachmentsDto;
		//}

		#endregion

		/// <summary>
		/// 
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="attachmentId"></param>
		/// <returns></returns>
		[System.Web.Http.HttpPost]
		public Stream DownLoadAttachment(int userId, int attachmentId)
		{
			var fileName = "";
			return _attachmentService.DownloadAttachment(out fileName, attachmentId, null, null, userId);
		}

		[System.Web.Http.HttpGet]
		public dynamic GetDocumentDetail(Guid documentId)
		{
			var document = _documentService.Get(documentId);
			if (document != null)
			{
				var doctype = _doctypeService.GetFromCache(document.DocTypeId.Value);
				var category = document.CategoryId.HasValue ? _categoryService.GetFromCache(document.CategoryId.Value) : null;
				var documentJson = new DocumentBWSSDto
				{
					DocumentId = document.DocumentId.ToString(),
					DocCode = document.DocCode,
					DocTypeId = doctype == null ? "" : doctype.DocTypeId.ToString(),
					DocTypeName = doctype == null ? "" : doctype.DocTypeName,
					Compendium = document.Compendium,
					Organization = document.Organization,
					OrganizationCode = document.OrganizationCode,
					UrgentId = document.UrgentId,
					DatePublished = document.DatePublished,
					CategoryId = document.CategoryId == null ? "" : document.CategoryId.ToString(),
					CategoryName = category == null ? "" : category.CategoryName
				};
				return documentJson;
			}

			return new DocumentBWSSDto();
		}

		// Todo: cần xem lại hàm này.
		// Trao đổi lại với TienBV trước khi un comment
		[System.Web.Http.HttpGet]
		public int TotalDocumentCurrentUser(string userName)
		{
			//var user = _userService.GetByUserName(userName);
			//if (user != null)
			//{
			//    var totalDocument = _docCopyService.GetTotalDocumentCurrentUser(user.UserId);

			//    return totalDocument;
			//}

			return 0;
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="docTypeId"></param>
        /// <param name="datePublished"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public dynamic SaveDocDraftNew(Guid docTypeId, DateTime? datePublished = null, string data = null)
        {
            dynamic result = new ExpandoObject();
            result.success = true;

            try
            {
                var docType = _doctypeService.Get(docTypeId);
                if (docType != null)
                {
                    //var docTypeForm = _docTypeFormService.Get(docTypeId, true);
                    var docTypeForm = _docTypeFormService.Gets(x => x.DocTypeId == docTypeId
                    && (x.IsPrimary == true || x.IsPrimary == false) && x.IsActive)
                        .OrderByDescending(d => d.IsPrimary).FirstOrDefault();         
                    if (docTypeForm != null)
                    {
                        var form = _formService.Get(docTypeForm.FormId);
                        if (form != null)
                        {
                            var note = "";
                            if (data != null)
                            {
                                note = Json2.Stringify(data);
                            }
                            var documentModel = new DocumentModel
                            {
                                DocTypeId = docType.DocTypeId,
                                CategoryId = docType.CategoryId,
                                DocCode = "BC",
                                Compendium = docType.DocTypeName,
                                CitizenName = docType.DocTypeName,
                                Status = 1,
                                StatusReport = 1,
                                CategoryBusinessId = docType.CategoryBusinessId,
                                DocFieldIds = docType.DocFieldId.HasValue ? $";{docType.DocFieldId.ToString()};" : ";;",
                                DocTypePermission = 0,
                                Note = note,
                                ProcessInfo = form.FormCode,
                                DatePublished = datePublished ?? DateTime.Now,
                                FormId = form.FormId.ToString(),
                                TimeKey = GetTimeKey(docType.ActionLevel.Value, datePublished ?? DateTime.Now),
                            };
                            var workflow = _doctypeService.GetWorkflowActive(documentModel.DocTypeId);
                            var allUserIds = _userService.GetAllUserIds(true);
                            var userSendIds = allUserIds.Where(u => _workflowHelper.GetStartNodes(workflow, u).Any());


                            //var userSendId = allUserIds.Where(u => _workflowHelper.GetStartNodes(workflow, u).Any()).FirstOrDefault();                     
                            var documentCopies = new List<DocumentCopy>();

                            //var departmentUserId = _departmentService.GetsCurrentDepartEdocFirst(userSendId);
                            //foreach (var userDepartment in departmentUserId)
                            //{
                            //    documentModel.InOutPlace = userDepartment.DepartmentName;
                            //    documentModel.OrganizationCode = userDepartment.Emails;
                            //}

                            //var newDocumentCopy = _documentHelper.CreateDocumentDefault(documentModel, userSendId, null);
                            //if (newDocumentCopy != null)
                            //    documentCopies.Add(newDocumentCopy);

                            foreach (var userSendId in userSendIds)
                            {
                                var departmentUserId = _departmentService.GetsCurrentDepartEdocFirst(userSendId);

                                foreach (var userDepartment in departmentUserId)
                                {
                                    documentModel.InOutPlace = userDepartment.DepartmentName;
                                    documentModel.OrganizationCode = userDepartment.Emails;
                                }

                                var newDocumentCopy = _documentHelper.CreateDocumentDefault(documentModel, userSendId, null);
                                if (newDocumentCopy != null)
                                    documentCopies.Add(newDocumentCopy);
                                //send mail, sms
                                var userId_ = _userService.Get(userSendId);
                                var documentEmail_ = userId_.Email;
                                var documentPhone_ = userId_.Phone;

                                if (documentEmail_ != null && documentPhone_ != null)
                                {
                                    var document = newDocumentCopy.Document;
                                    document.Email = documentEmail_;
                                    document.Phone = documentPhone_;
                                    // báo cáo tự động
                                    try
                                    {
                                        _mailHelper.SendCreatedDocumentMailExpert(document, userId_, newDocumentCopy.DocumentCopyId);
                                    }
                                    catch (Exception ex)
                                    {

                                    }

                                    try
                                    {
                                        _smsHelper.SendCreateDocumentSmsExpert(document, userId_, newDocumentCopy.DocumentCopyId);
                                    }
                                    catch (Exception ex)
                                    {

                                    }
                                }
                            }
                            var firstDocumentCopy = documentCopies.First();
                            _documentHelper.PushNotifyMessage(userSendIds, firstDocumentCopy, null, firstDocumentCopy.DateCreated, true, true);

                            result.data = documentCopies.Select(d => d.UserCurrentName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logService.Error("Lỗi Tạo văn bản tự động: {0}", ex, new string[] { docTypeId.ToString() });
                result.success = false;
                result.message = ex.Message;
            }
            return result;
        }
        /// <summary>
        ///  sắp tới hạn chạy đầu tiên
        /// </summary>
        /// <param name="docTypeId"></param>
        /// <param name="isActive"></param>
        /// <param name="isActiveAlert"></param>
        /// <param name="isActiveAlertOut"></param>
        /// <param name="datePublished"></param>
        /// <param name="data"></param>
        /// <returns></returns>
		[System.Web.Http.HttpGet]
		public dynamic SaveDocDraftDueDate(Guid docTypeId, bool isActive, bool isActiveAlert, bool isActiveAlertOut, DateTime? datePublished = null, string data = null)
		{
			dynamic result = new ExpandoObject();
			result.success = true;

			try
			{
				var docType = _doctypeService.Get(docTypeId);
				if (docType != null)
				{
					var docTypeForm = _docTypeFormService.Get(docTypeId, true);
					if (docTypeForm != null)
					{
						var form = _formService.Get(docTypeForm.FormId);
						if (form != null)
						{
                            var note = "";
                            if (data != null)
                            {
                                note = Json2.Stringify(data);
                            }
                            var documentModel = new DocumentModel
                            {
                                DocTypeId = docType.DocTypeId,
                                CategoryId = docType.CategoryId,
                                DocCode = "BC",
                                Compendium = docType.DocTypeName,
                                CitizenName = docType.DocTypeName,
                                Status = 1,
                                StatusReport = 1,
                                CategoryBusinessId = docType.CategoryBusinessId,
                                DocFieldIds = docType.DocFieldId.HasValue ? $";{docType.DocFieldId.ToString()};" : ";;",
                                DocTypePermission = 0,
                                Note = note,
                                ProcessInfo = form.FormCode,
                                DatePublished = datePublished ?? DateTime.Now,
                                FormId = form.FormId.ToString(),
                                TimeKey = GetTimeKey(docType.ActionLevel.Value, datePublished ?? DateTime.Now),
							};
                            var workflow = _doctypeService.GetWorkflowActive(documentModel.DocTypeId);
							var allUserIds = _userService.GetAllUserIds(true);
							var userSendIds = allUserIds.Where(u => _workflowHelper.GetStartNodes(workflow, u).Any());

                            
                            //var userSendId = allUserIds.Where(u => _workflowHelper.GetStartNodes(workflow, u).Any()).FirstOrDefault();                     
							var documentCopies = new List<DocumentCopy>();

                            //var departmentUserId = _departmentService.GetsCurrentDepartEdocFirst(userSendId);
                            //foreach (var userDepartment in departmentUserId)
                            //{
                            //    documentModel.InOutPlace = userDepartment.DepartmentName;
                            //    documentModel.OrganizationCode = userDepartment.Emails;
                            //}
                             
                            //var newDocumentCopy = _documentHelper.CreateDocumentDefault(documentModel, userSendId, null);
                            //if (newDocumentCopy != null)
                            //    documentCopies.Add(newDocumentCopy);

                            foreach (var userSendId in userSendIds)
                            {
                                var departmentUserId = _departmentService.GetsCurrentDepartEdocFirst(userSendId);
                                 
                                foreach (var userDepartment in departmentUserId)
                                {
                                    documentModel.InOutPlace = userDepartment.DepartmentName;
                                    documentModel.OrganizationCode = userDepartment.Emails;
                                }

                                var newDocumentCopy = _documentHelper.CreateDocumentDefault(documentModel, userSendId, null);
                                if (newDocumentCopy != null)
                                    documentCopies.Add(newDocumentCopy);
                                //send mail, sms
                                var userId_ = _userService.Get(userSendId);
                                var documentEmail_ = userId_.Email;
                                var documentPhone_ = userId_.Phone;

                                if(documentEmail_ != null && documentPhone_ != null)
                                {
                                    var document = newDocumentCopy.Document;
                                    document.Email = documentEmail_;
                                    document.Phone = documentPhone_;
                                    document.DateCreated = DateTime.Now;
                                    document.Status = 0;
                                    newDocumentCopy.Status = 0;
                                    if(isActive == true)
                                    {
                                        // báo cáo tự động
                                        try
                                        {
                                            _mailHelper.SendCreatedDocumentMailExpert(document, userId_, newDocumentCopy.DocumentCopyId);
                                        }
                                        catch (Exception ex)
                                        {

                                        }

                                        try
                                        {
                                            _smsHelper.SendCreateDocumentSmsExpert(document, userId_, newDocumentCopy.DocumentCopyId);
                                        }
                                        catch (Exception ex)
                                        {

                                        }

                                    }
                                    
                                    if(isActiveAlert == true)
                                    {
                                        // báo cáo sắp tới hạn
                                        try
                                        {
                                            _mailHelper.SendCreateDocumentMailDueDate(document, userId_, newDocumentCopy.DocumentCopyId);
                                            
                                        }
                                        catch (Exception ex)
                                        {

                                        }

                                        try
                                        {
                                            _smsHelper.SendCreateDocumentSmsDueDate(document, userId_, newDocumentCopy.DocumentCopyId);
                                        }
                                        catch (Exception ex)
                                        {

                                        }
                                        
                                    }
                                    if(isActiveAlertOut == true)
                                    {
                                        // báo cáo quá hạn
                                        try
                                        {
                                            _mailHelper.SendCreateDocumentMailOutOfDate(document, userId_, newDocumentCopy.DocumentCopyId);
                                        }
                                        catch (Exception ex)
                                        {

                                        }

                                        try
                                        {
                                            _smsHelper.SendCreateDocumentSmsOutOfDate(document, userId_, newDocumentCopy.DocumentCopyId);
                                        }
                                        catch (Exception ex)
                                        {

                                        }
                                        
                                    }
                                }
                            }
                            var firstDocumentCopy = documentCopies.First();
                            _documentHelper.PushNotifyMessage(userSendIds, firstDocumentCopy, null, firstDocumentCopy.DateCreated, true, true);

                            result.data = documentCopies.Select(d => d.UserCurrentName);  
						}
					}
				}
			}
			catch (Exception ex)
			{
				_logService.Error("Lỗi Tạo văn bản tự động: {0}", ex, new string[] { docTypeId.ToString() });
				result.success = false;
				result.message = ex.Message;
			}
			return result;
		}

        // 
        [System.Web.Http.HttpGet]
        public dynamic SaveDocDraftDueOutOfDateNew(Guid docTypeId, bool isActive, bool isActiveAlert, bool isActiveAlertOut, DateTime? datePublished = null, string data = null)
        {
            try
            {
                var docTypeIdSms = _smsService.Gets(d => d.LinkApi == docTypeId && d.NotifyConfigType == "Document_WhenHasResult").Last();

                if (docTypeIdSms.NotifyConfigType == "Document_WhenHasResult")
                {
                    var documentId = docTypeIdSms.DocumentId;
                    var documentCopyId = docTypeIdSms.DocumentCopyId;

                    var documents = _documentService.GetId(documentId);
                    
                    var documnetCopys = _docCopyService.Get(Convert.ToInt32(documentCopyId));
                    

                    var workflow = _doctypeService.GetWorkflowActive(docTypeId);
                    var allUserIds = _userService.GetAllUserIds(true);
                    var userSendIds = allUserIds.Where(u => _workflowHelper.GetStartNodes(workflow, u).Any());
                    var documentCopies = new List<DocumentCopy>();


                    foreach (var userSendId in userSendIds)
                    {
                        //send mail, sms
                        var userId_ = _userService.Get(userSendId);
                        var documentEmail_ = userId_.Email;
                        var documentPhone_ = userId_.Phone;

                        if (documentEmail_ != null && documentPhone_ != null)
                        {
                            if (isActive == true)
                            {
                                // báo cáo tự động
                                documents.Status = 1;
                                documents.DateCreated = DateTime.Now;
                                documnetCopys.Status = 1;
                                try
                                {
                                    _mailHelper.SendCreatedDocumentMailExpert(documents, userId_, documentCopyId);
                                }
                                catch (Exception ex)
                                {

                                }

                                try
                                {
                                    _smsHelper.SendCreateDocumentSmsExpert(documents, userId_, documentCopyId);
                                }
                                catch (Exception ex)
                                {

                                }

                            }
                            if (isActiveAlertOut == true)
                            {
                                // báo cáo quá hạn
                                // đóng mở báo cáo
                                documents.StatusOpenClose = true;
                                _documentService.SaveChanges();
                                try
                                {
                                    _mailHelper.SendCreateDocumentMailOutOfDate(documents, userId_, documentCopyId);
                                }
                                catch (Exception ex)
                                {

                                }

                                try
                                {
                                    _smsHelper.SendCreateDocumentSmsOutOfDate(documents, userId_, documentCopyId);
                                }
                                catch (Exception ex)
                                {

                                }
                            }
                        }
                    }
                }
                    

            }
            catch (Exception ex)
            {
                _logService.Error("Lỗi Tạo cảnh báo quá hạn và sắp tới hạn: {0}", ex, new string[] { docTypeId.ToString() });
            }
            return true;
        }



        #region
        
        [System.Web.Http.HttpGet]
        public dynamic SaveDocDraftDueOutOfDate(Guid docTypeId, bool isActive, bool isActiveAlert, bool isActiveAlertOut, DateTime? datePublished = null, string data = null)
        {
            try {
                var docTypeIdSms = _smsService.Gets(d => d.LinkApi == docTypeId && d.NotifyConfigType == "Document_WhenHasResult").Last();    

                //foreach(var docTypeIds in docTypeIdSms)
                //{
                    if(docTypeIdSms.NotifyConfigType == "Document_WhenHasResult")
                    {
                        var documentId = docTypeIdSms.DocumentId;
                        var documentCopyId = docTypeIdSms.DocumentCopyId;
                        
                        var documents = _documentService.GetId(documentId);

                        var workflow = _doctypeService.GetWorkflowActive(docTypeId);
                        var allUserIds = _userService.GetAllUserIds(true);
                        var userSendIds = allUserIds.Where(u => _workflowHelper.GetStartNodes(workflow, u).Any());
                        var documentCopies = new List<DocumentCopy>();


                        foreach (var userSendId in userSendIds)
                        {
                            //send mail, sms
                            var userId_ = _userService.Get(userSendId);
                            var documentEmail_ = userId_.Email;
                            var documentPhone_ = userId_.Phone;

                            if (documentEmail_ != null && documentPhone_ != null)
                            {
                                if (isActive == true)
                                {
                                    // báo cáo tự động
                                    try
                                    {
                                        _mailHelper.SendCreatedDocumentMailExpert(documents, userId_, documentCopyId);
                                    }
                                    catch (Exception ex)
                                    {

                                    }

                                    try
                                    {
                                        _smsHelper.SendCreateDocumentSmsExpert(documents, userId_, documentCopyId);
                                    }
                                    catch (Exception ex)
                                    {

                                    }

                                } 
                                if (isActiveAlertOut == true)
                                {
                                    // báo cáo quá hạn
                                    try
                                    {
                                        _mailHelper.SendCreateDocumentMailOutOfDate(documents, userId_, documentCopyId);
                                    }
                                    catch (Exception ex)
                                    {

                                    }

                                    try
                                    {
                                        _smsHelper.SendCreateDocumentSmsOutOfDate(documents, userId_, documentCopyId);
                                    }
                                    catch (Exception ex)
                                    {

                                    }
                                }
                            }
                        }
                    }
                //}

            }
            catch (Exception ex)
            {
                _logService.Error("Lỗi Tạo cảnh báo quá hạn và sắp tới hạn: {0}", ex, new string[] { docTypeId.ToString() });
            }
            return true;
        }
        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataSend"></param>
        /// <returns></returns>
        //[System.Web.Http.HttpPost]
        //public dynamic SaveReportByFile([FromBody]DocTypeCreate dataSend)
        //{

        //    var dt = Guid.Parse(dataSend.docTypeId);

        //    var base64Decode = System.Convert.FromBase64String(dataSend.fileBase64);
        //    MemoryStream stream = new MemoryStream(base64Decode);

        //    var xlsxParser = new XlsxToJson(stream);
        //    var dpl = DateTime.Parse(dataSend.datePublished);
        //    var config = dataSend.headerConfig;
        //    var data = xlsxParser.GetDataToXlsx(config.indexSheet, config.start, config.headerStart, config.headerEnd, config.rows, config.columns);

        //    var document = CreateDocumentToExcel(dt, dpl, data);

        //    var documentCopies = new List<DocumentCopy>();
        //    return documentCopies;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataSend"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public dynamic SaveAndSendReportVPCPByFile([FromBody]DocTypeCreate dataSend)
        {
            var dt = Guid.Parse(dataSend.docTypeId);
            var dpl = DateTime.Parse(dataSend.datePublished);
            
            var documentCopies = CreateDocumentToExcel(dt, dpl, null, true, dataSend);
            StaticLog.LogLienThong(new List<string>() { "documentCopiesTienQD: " + JsonConvert.SerializeObject(documentCopies) });

            return documentCopies;
        }

        [System.Web.Http.HttpPost]
        public dynamic SendReportDataSourceRealTime(string docTypeId, string keyname, string dataSource, string timekey, bool isRealtime = false)
        {

            var dtId = Guid.Parse(docTypeId);
            var doctype = _doctypeService.Get(dtId);
            if (doctype == null)
            {
                return new
                {
                    error = 1,
                    msg = "Không tồn tại loại báo cáo"
                };
            }

            var data = GetDatatoDatasource(dataSource, timekey, keyname, doctype.DocTypeCode);

            if (data == null)
            {
                return new
                {
                    error = 1,
                    msg = "không tạo được data"
                };
            }

            if (isRealtime && keyname == "datekey")
            {
                data.data.Header.Period = timekey + DateTime.Now.ToString("HHmm");
            }
            var result = LienThongVPCPLive("sndData", data);

            return result;
        }

        private dynamic CreateDocumentToExcel(Guid docTypeId, DateTime? datePublished = null, List<object> data = null, bool isSendVPCP = false, DocTypeCreate dt = null)
        {
            dynamic result = new ExpandoObject();
            result.success = true;

            try
            {
                var docType = _doctypeService.Get(docTypeId);
                StaticLog.LogLienThong(new List<string>() { "docTypeTienQD: " + docTypeId });
                StaticLog.LogLienThong(new List<string>() { "docTypeTienQD: " + JsonConvert.SerializeObject(docType) });

                if (docType != null)
                {
                    var docTypeForm = _docTypeFormService.Get(docTypeId, true);
                    if (docTypeForm != null)
                    {
                        var form = _formService.Get(docTypeForm.FormId);
                        if (form != null)
                        {
                            var note = "";
                            var dataCV = AssignData(form, data, dt);
                            note = Json2.Stringify(dataCV);
                            var base64Decode = System.Convert.FromBase64String(dt.fileBase64);
                            MemoryStream stream = new MemoryStream(base64Decode);

                            var temp = FileManager.Default.Create(stream, ResourceLocation.Default.FileUploadTemp, null, "xlsx");

                            var fileTemp = new Dictionary<string, IDictionary<string, string>>();
                            var objname = new Dictionary<string, string>();
                            objname.Add("name", docType.DocTypeName + ".xlsx");
                            fileTemp.Add(temp.Name, objname);
                            StaticLog.LogLienThong(new List<string>() { "tempTienQD: " + JsonConvert.SerializeObject(temp) });

                            var documentModel = new DocumentModel
                            {
                                DocTypeId = docType.DocTypeId,
                                CategoryId = docType.CategoryId,
                                DocCode = "BC",
                                Compendium = docType.DocTypeName,
                                CitizenName = docType.DocTypeName,
                                Status = 4,
                                StatusReport = 4,
                                CategoryBusinessId = docType.CategoryBusinessId,
                                DocFieldIds = docType.DocFieldId.HasValue ? $";{docType.DocFieldId.ToString()};" : ";;",
                                DocTypePermission = 0,
                                Note = note,
                                ProcessInfo = form.FormCode,
                                DatePublished = datePublished ?? DateTime.Now,
                                FormId = form.FormId.ToString(),
                                TimeKey = GetTimeKey(docType.ActionLevel.Value, datePublished ?? DateTime.Now),
                                Original = 2
                            };

                            var workflow = _doctypeService.GetWorkflowActive(documentModel.DocTypeId);
                            if (workflow != null)
                            {
                                var allUserIds = _userService.GetAllUserIds(true);
                                var userSendIds = allUserIds.Where(u => _workflowHelper.GetStartNodes(workflow, u).Any());
                                //var userSendId = allUserIds.Where(u => _workflowHelper.GetStartNodes(workflow, u).Any()).FirstOrDefault();                     
                                var documentCopies = new List<DocumentCopy>();
                                var listInt = new List<int>();
                                if (userSendIds != null && userSendIds.Any())
                                {
                                    var Int = userSendIds.FirstOrDefault();
                                    listInt.Add(Int);
                                }
                                foreach (var userSendId in listInt)
                                {
                                    var departmentUserId = _departmentService.GetsCurrentDepartEdocFirst(userSendId);
                                    foreach (var userDepartment in departmentUserId)
                                    {
                                        documentModel.InOutPlace = userDepartment.DepartmentName;
                                        documentModel.OrganizationCode = userDepartment.Emails;
                                    }
                                    var departUser = departmentUserId.FirstOrDefault();
                                    var document = _documentService.GetToDoctypeId_(docType.DocTypeId, departUser.Emails, GetTimeKey(docType.ActionLevel.Value, datePublished ?? DateTime.Now), datePublished.Value.Year.ToString());
                                    DocumentCopy newDocumentCopy = null;
                                    if (document != null && document.Any())
                                    {
                                        var documentId = document.FirstOrDefault().document;
                                        var dcm = _documentService.Get(documentId);
                                        var docCopy = _docCopyService.GetMain(documentId);
                                        var dcmModel = dcm.ToModel();
                                        dcmModel.DocumentCopyId = docCopy.DocumentCopyId;
                                        dcmModel.Note = note;
                                        dcmModel.ProcessInfo = form.FormCode;
                                        newDocumentCopy = _documentHelper.UpdateDocumentDefault(dcmModel, fileTemp, null, null, userSendId);
                                        StaticLog.LogLienThong(new List<string>() { "dataUpdate: " + JsonConvert.SerializeObject(docType) });
                                        var clearCacheId = new List<int>();
                                        clearCacheId.Add(docCopy.DocumentCopyId);
                                        _docCopyService.ClearCache(clearCacheId);
                                    }
                                    else
                                    {
                                        newDocumentCopy = _documentHelper.CreateDocumentDefault(documentModel, userSendId, fileTemp);
                                        StaticLog.LogLienThong(new List<string>() { "dataCreate: " + JsonConvert.SerializeObject(docType) });
                                    }

                                    if (newDocumentCopy != null)
                                        documentCopies.Add(newDocumentCopy);

                                }

                                var firstDocumentCopy = documentCopies.First();

                                _documentHelper.PushNotifyMessage(userSendIds, firstDocumentCopy, null, firstDocumentCopy.DateCreated, true, true);

                                result.data = documentCopies.Select(d => d.DocumentCopyId);
                            }
                            else {
                                result.success = false;
                                result.message = "Lỗi không có workflow";
                            }
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logService.Error("Lỗi Tạo bao cao tự động: {0}", ex, new string[] { docTypeId.ToString() });
                result.success = false;
                result.message = ex.Message;
            }
            return result;
        }

        private string GetTimeKey(int kyBaoCao, DateTime datePublished)
		{
			switch (kyBaoCao)
			{
				case 1:
					return datePublished.ToString("yyyy");
				case 2:
					return datePublished.ToString("yyyy") + GetHalf(datePublished);
				case 3:
					return datePublished.ToString("yyyy") + GetQuarter(datePublished);
				case 4:
					return datePublished.ToString("yyyyMM");
				case 5:
                    return datePublished.ToString("yyyy") + datePublished.WeekOfYear().ToString();//.PadLeft(2, '0');
				case 6:
					return datePublished.ToString("yyyyMMdd");
				default:
					return datePublished.ToString("yyyyMMddHHmm");
			}
		}

		private int GetHalf(DateTime date)
		{
			return (date.Month + 5) / 6;
		}
		private int GetQuarter(DateTime date)
		{
			return (date.Month + 2) / 3;
		}

        private SendReport GetDatatoDatasource(string dataSource, string timekey, string keyname, string docTypeCode)
        {
            var query = string.Format("dashboard: select * from `{0}` where {1} =" + timekey, dataSource, keyname);
            var param = new List<object>
            {
                new SqlParameter("@timekey", timekey)
            };

            var arrPara = param.ToArray();
            var data = _templateService.GetDataByQuery(query, arrPara);
            var dataJson = JsonConvert.SerializeObject(data);
            var dataSend = _docCopyService.GetReportData(docTypeCode, timekey, _reportConfigSettings.OzganizeKey, dataJson);
            SendReport jsonVPCP = new SendReport();
            jsonVPCP.data = dataSend;
            jsonVPCP.func = "sndData";
            jsonVPCP.access_token = _reportConfigSettings.TokenService;

            return jsonVPCP;
        }

        private dynamic AssignData(Form form, List<object> dataFile, DocTypeCreate dataSend = null)
        {
            var configForm = form.FormCode;
            var dataConfig = JsonConvert.DeserializeObject<DataConfig>(configForm);
            var data = dataConfig.data;
            var base64Decode = System.Convert.FromBase64String(dataSend.fileBase64);
            try {          
                MemoryStream stream = new MemoryStream(base64Decode);
                var xlsxParser = new XlsxToJson(stream);

                var config = dataSend.headerConfig;
                var result = xlsxParser.GetDataToXlsx(config.indexSheet, config.start, config.headerStart, config.headerEnd, config.rows, config.columns, data);
                return result;
            } catch (Exception ex) {
                //var path = Path.Combine(ResourceLocation.Default.FileTemp, "input" + ".xls");
                //System.IO.File.WriteAllBytes(path, base64Decode);

                //Workbook workbook = new Workbook();
                //var path2 = Path.Combine(ResourceLocation.Default.FileTemp, "output" + ".xlsx");
                //workbook.LoadFromFile(path);
                //workbook.SaveToFile(path2, ExcelVersion.Version2013);
                //byte[] base64DecodeXLSX = ReadFile(path2);
                //MemoryStream stream = new MemoryStream(base64DecodeXLSX);
                //var xlsxParser = new XlsxToJson(stream);

                //var config = dataSend.headerConfig;
                //var result = xlsxParser.GetDataToXlsx(config.indexSheet, config.start, config.headerStart, config.headerEnd, config.rows, config.columns, data);
                //System.IO.File.Delete(path);
                //System.IO.File.Delete(path2);
                //return result;
                return new List<object>();
            }
        }
        byte[] ReadFile(string sourcePath)
        {
            byte[] data = null;
            FileInfo fileInfo = new FileInfo(sourcePath);
            long numBytes = fileInfo.Length;
            FileStream fileStream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fileStream);
            data = br.ReadBytes((int)numBytes);
            fileStream.Close();
            return data;
        }
        public string LienThongVPCPLive(string action, dynamic data)
        {
            var client = new HttpClient();
            var url = _reportConfigSettings.UrlService;

            StaticLog.LogLienThong(new List<string>() { "data: " + JsonConvert.SerializeObject(data) });

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

	public class DocTypeTemplateBase64
	{
		public string doctypeId { get; set; }

		public string docDetail { get; set; }

		// Lấy định dạng PDF khi thực hiện đính kèm hồ sơ vào mail để xác nhận đăng kí thành công
		// Lấy định dang docx gửi sang DVC để thực hiện kí số vào văn bản
		public bool IsPDF { get; set; }
	}

	public class DocTypeCreate
	{
		public string docTypeId { get; set; }

		public string datePublished { get; set; }

		public string fileBase64 { get; set; }

		public HeaderConfig headerConfig { get; set; }
	}

    public class DataConfig
    {
        public object header { get; set; }
        public List<Dictionary<string, object>> data { get; set; }
        public List<object> headerNested { get; set; }
        public object extra { get; set; }
        public List<int> colWidths { get; set; }
        public List<object> mergedCells { get; set; }
        public List<object> readOnlys { get; set; }
        public List<object> classCells { get; set; }

    }
    public class HeaderConfig
    {
        public int indexSheet { get; set; }

        public int start { get; set; }

        public int headerStart { get; set; }

        public int headerEnd { get; set; }

        public int rows { get; set; }

        public int columns { get; set; }
        
    }

    public class DocTimeLine
    {
        public Guid DocumentId { get; set; }

        public string EDocumentId { get; set; }

        public int Status { get; set; }

        public string DocCode { get; set; }

        public int UserCreatedId { get; set; }

        public string OrganizationCode { get; set; }

        public DateTime? DateFinished { get; set; }

        public int UserId { get; set; }

        public int UserSendId { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime? ToDate { get; set; }
    }
}