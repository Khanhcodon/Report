using System;
using System.Collections.Generic;
using System.Linq;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Validator;
using Bkav.eGovCloud.Web.Framework;
using Microsoft.JScript;

namespace Bkav.eGovCloud.Models
{
	[FluentValidation.Attributes.Validator(typeof(DocumentValidator))]
	public class DocumentModel
	{
		#region Fields

		private ICollection<DocumentContentModel> _docContents;
		private IEnumerable<DocFeeModel> _docFees;
		private IEnumerable<DocPaperModel> _docPapers;
		private List<DocRelationModel> _docRelationModel;

		#endregion Fields

		#region C'tor

		/// <summary>
		/// tạo 1 văn bản dựa theo hồ sơ đăng ký qua mạng
		/// </summary>
		/// <param name="doc"></param>
		public DocumentModel(DocumentOnline doc)
		{
			this.CitizenName = doc.PersonInfo;
			this.DateAppointed = doc.DateAppoint;
			this.DateReceived = doc.DateReceived;
			this.DocTypeId = doc.DocTypeId;
			this.Email = doc.Email;
			this.IdentityCard = doc.IdCard;
			this.IsViewed = false;
			this.Phone = doc.Phone;
			this.Status = (byte)DocumentStatus.DuThao;
			this.Address = doc.Address;
			this.DocCode = doc.DocCode;
			this.InOutPlace = "Hồ sơ đăng ký qua mạng";
			this.IsReturned = false;
			this.Original = 1;
			this.DateArrived = DateTime.Now;
			this.DocFieldIds = ";" + doc.Doctype.DocFieldId + ";";
			this.Compendium = doc.Compendium;
			this.DocPapers = doc.DocPapers.ToListModel();
		}

		public DocumentModel()
		{
			Original = 0;
			ResultStatus = null;
			UrgentId = (byte)Urgent.Thuong;
			Status = (int)DocumentStatus.DuThao;

			DocumentCopyModel = new DocumentCopyModel();
		}

		#endregion C'tor

		#region Instance Properties

		#region Columns

		/// <summary>
		/// Cấu hình key
		/// </summary>
		//public string ReportKey { get; set; }
		/// <summary>
		/// Lấy hoặc thiết lập Id văn bản, hồ sơ
		/// </summary>
		public Guid DocumentId { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Id vệt xử lý
		/// </summary>
		public int DocumentCopyId { get; set; }

		/// <summary>
		/// Địa chỉ công dân
		/// </summary>
		[LocalizationDisplayName("Document.Address.Field.Label")]
		public string Address { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Id thể loại
		/// </summary>
		[LocalizationDisplayName("Document.Category.Field.Label")]
		public int CategoryId { get; set; }

		/// <summary>
		/// Tên công dân: dùng cho hs một cửa
		/// </summary>
		[LocalizationDisplayName("Document.PersonGive.Field.Label")]
		public string CitizenName { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Trích yếu
		/// </summary>
		[LocalizationDisplayName("Document.Compendium.Field.Label")]
		public string Compendium { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Ngày hẹn trả (eGate), ngày giải quyết (eOffice)
		/// </summary>
		[LocalizationDisplayName("Document.DateAppointed.Field.Label")]
		public DateTime? DateAppointed { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập hạn giữ
		/// </summary>
		public DateTime? DateOverdue { get; set; }

		/// <summary>
		/// Láy hoặc thiết lập trạng thái cho thay đổi hạn giữ hay không
		/// </summary>
		public bool HasDateOverdue { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Ngày tạo
		/// </summary>
		public DateTime DateCreated { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Ngày cập nhật
		/// </summary>
		public DateTime DateModified { get; set; }

		/// <summary>
		/// Lấy list danh sách sổ văn bản
		/// </summary>
		public IEnumerable<dynamic> Stores { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Ngày tiếp nhận (eGate), ngày đến đi (eOffice)
		/// </summary>
		public DateTime? DateReceived { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Ngày ký duyệt
		/// </summary>
		public DateTime? DateSuccess { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Số ký hiệu (eOffice), mã hồ sơ (eGate)
		/// </summary>
		[LocalizationDisplayName("Document.DocCode.Field.Label")]
		public string DocCode { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập danh sách mã theo sổ khi khởi tạo
		/// </summary>
		public List<string> DocCodes { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Id loại văn bản, hồ sơ
		/// </summary>
		[LocalizationDisplayName("Document.DocType.Field.Label")]
		public Guid DocTypeId { get; set; }

		/// <summary>
		/// Email của công dân
		/// </summary>
		[LocalizationDisplayName("Document.Email.Field.Label")]
		public string Email { get; set; }

		/// <summary>
		/// CMND của công dân
		/// </summary>
		[LocalizationDisplayName("Document.CMND.Field.Label")]
		public string IdentityCard { get; set; }

		/// <summary>
		/// Số đến đi, mã đến đi.
		/// </summary>
		[LocalizationDisplayName("Document.InOutCode.Field.Label")]
		public string InOutCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Dictionary<int, string> InOutCodes { get; set; }

		/// <summary>
		/// Nơi đến đi.
		/// </summary>
		[LocalizationDisplayName("Document.DocAddress.Field.Label")]
		public string InOutPlace { get; set; }

		/// <summary>
		/// Văn bản hồi báo: True (đã được hồi báo), False (cần hồi báo).
		/// </summary>
		public bool IsAcknowledged { get; set; }

		/// <summary>
		/// Đánh dấu văn bản đã được xem.
		/// </summary>
		public bool IsViewed { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập trạng thái đã trả kết quả
		/// <para> True: Đã trả</para>
		/// <para> False: Chưa trả</para>
		/// <para> Null: Không trả kết quả</para>
		/// </summary>
		public bool? IsReturned { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập trạng thái kí duyệt
		/// <para> True: Đồng ý</para>
		/// <para> False: Từ chối</para>
		/// <para> Null: Chưa duyệt</para>
		/// </summary>
		public bool? IsSuccess { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập trạng thái có yêu cầu bổ sung hay không
		/// <para> True: Đã bổ sung</para>
		/// <para> False: Chưa bổ sung</para>
		/// <default> Null: Chưa từng có yêu cầu bổ sung </default>
		/// </summary>
		public bool? IsSupplemented { get; set; }

		/// <summary>
		/// Nguồn gốc văn bản:
		/// <para> 0: văn bản tạo trực tiếp.</para>
		/// <para> 1: văn bản nhận từ ĐKQM.</para>
		/// <para> 2: văn bản liên thông.</para>
		/// <value>
		/// Default: 0.
		/// </value>
		/// </summary>
		public byte Original { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Id nghiệp vụ
		/// </summary>
		public int CategoryBusinessId { get; set; }

        /// <summary>
        /// Kỳ báo cáo
        /// </summary>
        public int? ActionLevel { get; set; }

        /// <summary>
        /// Phone
        /// </summary>
        [LocalizationDisplayName("Document.Phone.Field.Label")]
		public string Phone { get; set; }

		/// <summary>
		/// Trạng thái xử lý:
		/// <para> True: thụ lý thành công.</para>
		/// <para> False: thụ lý không thành công.</para>
		/// <default> Null </default>
		/// </summary>
		public bool? ResultStatus { get; set; }

		/// <summary>
		/// Trạng thái xử lý của văn bản:
		/// <para> 0: văn bản dự thảo.</para>
		/// <para> 1: văn bản đang xử lý.</para>
		/// <para> 2: văn bản đã kết thúc.</para>
		/// <para> 4: văn bản đã hủy.</para>
		/// </summary>
		public byte Status { get; set; }

		/// <summary>
		/// Trạng thái xử lý của văn bản:
		/// <para> 0: văn bản dự thảo.</para>
		/// <para> 1: văn bản đang xử lý.</para>
		/// <para> 2: văn bản đã kết thúc.</para>
		/// <para> 4: văn bản đã hủy.</para>
		/// </summary>
		public byte DocumentCopyStatus { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Id người ký chính (eOffice), người ký duyệt (eGate)
		/// </summary>
		public int? UserSuccessId { get; set; }

		/// <summary>
		/// Độ khẩn
		/// </summary>
		[LocalizationDisplayName("Document.UrgentId.Field.Label")]
		public byte UrgentId { get; set; }

		/// <summary>
		/// Id người khởi tạo
		/// </summary>
		public int UserCreatedId { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Danh sách Id của lĩnh vực (lưu dạng ;id;id;)
		/// </summary>
		public string DocFieldIds { get; set; }

		/// <summary>
		/// <para>Lấy hoặc thiết lập Danh sách quyền tác động lên loại hồ sơ.</para>
		/// <para>Dữ liệu thộc kiểu: DocTypePermission</para>
		/// CuongNT - 240113
		/// </summary>
		public int? DocTypePermission { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Id cơ quan ban hành
		/// </summary>
		[LocalizationDisplayName("Document.Organization.Field.Label")]
		public string Organization { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Id sổ văn bản
		/// </summary>
		[LocalizationDisplayName("Document.StoreId.Field.Label")]
		public int? StoreId { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Id độ mật
		/// </summary>
		[LocalizationDisplayName("Document.SecurityId.Field.Label")]
		public int? SecurityId { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập số trang
		/// </summary>
		[LocalizationDisplayName("Document.TotalPage.Field.Label")]
		public int? TotalPage { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập số tờ trình
		/// </summary>
		public string Keyword { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Id hình thức gửi
		/// </summary>
		[LocalizationDisplayName("Document.SendTypeId.Field.Label")]
		public int? SendTypeId { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập ngày đến của văn bản (văn bản đến)
		/// </summary>
		[LocalizationDisplayName("Document.DateArrived.Field.Label")]
		public DateTime? DateArrived { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập ngày văn bản
		/// </summary>
		[LocalizationDisplayName("Document.DatePublished.Field.Label")]
		public DateTime? DatePublished { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập hạn xử lý
		/// </summary>
		public int? ExpireProcess { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập trạng thái cho phép thay đổi hạn xử lý (chức năng phân loại)
		/// <para> True: Có đổi hạn xử lý</para>
		/// <para> False: Không đổi hạn xử lý</para>
		/// </summary>
		public bool ChangeExpireProcess { get; set; }

		/// <summary>
		/// Trạng thái đánh lại số đến
		/// </summary>
		public bool HasChangeInoutCode { get; set; }

		/// <summary>
		/// WorkflowTypes
		/// </summary>
		public List<WorkflowType> WorkflowTypes { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int CodeId { get; set; }

		/// <summary>
		/// Ghi chú thông tin khác
		/// </summary>
		public string Note { get; set; }

        /// <summary>
		/// Ghi chú thông tin khác
		/// </summary>
		public string ReturnNote { get; set; }

        /// <summary>
        /// Thông tin xử lý: thông tin phát hành với văn bản đi và thông tin chuyển xử lý khi tiếp nhận văn bản đên.
        /// </summary>
        public string ProcessInfo { get; set; }

        /// <summary>
        /// ky bc
        /// </summary>
        public string TimeKey { get; set; }

        /// <summary>
        /// luu du lieu tong hop
        /// </summary>
        public string CompilationData { get; set; }

        /// <summary>
        /// Nơi nhận trả kết quả
        /// </summary>
        public int? TypeReturned { get; set; }

        /// <summary>
        /// trang thai bao cao
        /// </summary>
        public int? StatusReport { get; set; }

        /// <summary>
        /// Thông tin có phải trạng thái phát hành ra ngoài
        /// <para>true: phát hành ra ngoài</para>
        /// <para>false: phát hành nội bộ</para>
        /// <para>null: không phát hành</para>
        /// </summary>
        public bool? IsTransferPublish { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int DocumentCopyType { get; set; }

		#endregion Columns

		#region Bien trang thai

		/// <summary>
		/// Trạng thái văn bản bàn giao: tạo mới, cập nhật, trả lời, phân loại.
		/// </summary>
		public int TransferType { get; set; }

		/// <summary>
		/// Trạng thái văn bản bàn giao: tạo mới, cập nhật, trả lời, phân loại.
		/// </summary>
		public TransferTypes TransferTypeInEnum
		{
			get { return (TransferTypes)TransferType; }
			set { TransferType = (int)value; }
		}

		#endregion Bien trang thai

		#region Relations

		public DocumentCopyModel DocumentCopyModel { get; set; }

		public ICollection<DocumentContentModel> DocumentContents
		{
			get { return _docContents ?? (_docContents = new List<DocumentContentModel>()); }
			set { _docContents = value; }
		}

		public ICollection<Attachment> AttachmentModels { get; set; }

		[LocalizationDisplayName("Document.DocPapers.Field.Label")]
		public IEnumerable<DocPaperModel> DocPapers
		{
			get { return _docPapers ?? (_docPapers = new List<DocPaperModel>()); }
			set { _docPapers = value; }
		}

		[LocalizationDisplayName("Document.DocFees.Field.Label")]
		public IEnumerable<DocFeeModel> DocFees
		{
			get { return _docFees ?? (_docFees = new List<DocFeeModel>()); }
			set { _docFees = value; }
		}

		#endregion Relations

		#endregion Instance Properties

		#region Other fields

		/// <summary>
		/// Danh sách các content của hồ sơ, dùng để lấy nội dung form động, form html khi post qua model lên controller.
		/// </summary>
		public List<string> Contents { get; set; }

		/// <summary>
		/// Danh sách các hồ sơ liên quan, dùng để gán danh sách hồ sơ liên quan để post qua model lên controller.
		/// </summary>
		public List<DocRelationModel> RelationModels
		{
			get { return _docRelationModel ?? (_docRelationModel = new List<DocRelationModel>()); }
			set
			{
				foreach (var relation in value)
				{
					relation.Compendium = GlobalObject.unescape(relation.Compendium);
				}

				_docRelationModel = value;
			}
		}

		/// <summary>
		/// Ý kiến xử lý người dùng nhập lên.
		/// </summary>
		public CommentModel Comments { get; set; }

		/// <summary>
		/// Trạng thái xác định văn bản đang đánh số bằng tay
		/// </summary>
		public bool IsCustomCode { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập tên người ký
		/// </summary>
		public string UserSuccessName { get; internal set; }

		/// <summary>
		/// Lấy hoặc thiết lập note ký
		/// </summary>
		public string SuccessNote { get; internal set; }

		/// <summary>
		/// Lấy hoặc thiết lập mã cơ quan ban hành
		/// </summary>
		public string OrganizationCode { get; set; }

        /// <summary>
		/// Lấy hoặc thiết lập mã cơ quan ban hành
		/// </summary>
		public string FormId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ngày cấp số
        /// </summary>
        public DateTime DateOfIssueCode { get; internal set; }

		/// <summary>
		/// Lấy hoặc thiết lập ngày kết thúc văn bản
		/// </summary>
		public DateTime? DateFinished { get; internal set; }

		public DateTime DateRequireSupplementary { get; internal set; }

		public bool IsGettingOut { get; internal set; }

		public string UserCreatedName { get; internal set; }

		public string Id { get; set; }

        public string SurveyConfig { get; set; }
        public string SurveyReport { get; set; }
        public string SurveyCriteria { get; set; }
        public string SurveyImg { get; set; }
        public string SurveyImgPath { get; set; }
        public bool IsActivated { get; set; }
        public string DocTypeCode { get; set; }
        public string DocTypeName { get; set; }

        #endregion other fields
    }
}