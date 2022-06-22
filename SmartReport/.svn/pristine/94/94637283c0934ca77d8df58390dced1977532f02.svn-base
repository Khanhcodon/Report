using Bkav.eGovCloud.Core.Document;
using Bkav.eGovCloud.Core.History;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Business.Objects.CacheObjects
{
	/// <summary>
	/// Cache thông tin văn bản dung khi mở văn bản để xem
	/// </summary>
	public class DocumentCached
	{
		private string SEPARATOR_CHAR = ";";

		/// <summary>
		/// KT
		/// </summary>
		public DocumentCached()
		{
			CommentList = new List<CommentCached>();
		}

		/// <summary>
		/// Trạng thái xử lý:
		/// <para> True: thụ lý thành công.</para>
		/// <para> False: thụ lý không thành công.</para>
		/// <default> Null </default>
		/// </summary>
		public bool? ResultStatus { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Ngày có kết quả xử lý thành công
		/// </summary>
		public DateTime? DateResult { get; set; }

		/// <summary>
		/// DocumentCopyId
		/// Key
		/// </summary>
		public int DocumentCopyId { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Id văn bản, hồ sơ
		/// </summary>
		public Guid DocumentId { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Id loại văn bản, hồ sơ
		/// </summary>
		public Guid? DocTypeId { get; set; }

		/// <summary>
		/// Tên loại văn bản
		/// </summary>
		public string DocTypeName { get; set; }

        /// <summary>
        /// Mã loại văn bản
        /// </summary>
        public string DocTypeCode { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id thể loại
        /// </summary>
        public int? CategoryId { get; set; }

		/// <summary>
		/// Tên hình thức văn bản
		/// </summary>
		public string CategoryName { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Số kí hiệu (eOffice), mã hồ sơ (eGate)
		/// </summary>
		public string DocCode { get; set; }

		/// <summary>
		/// Số đến đi, mã đến đi.
		/// </summary>
		public string InOutCode { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Trích yếu
		/// </summary>
		public string Compendium { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Ngày tạo
		/// </summary>
		public DateTime DateCreated { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Ngày hẹn trả (eGate), ngày giải quyết (eOffice)
		/// </summary>
		public DateTime? DateAppointed { get; set; }

		/// <summary>
		/// Trạng thái xử lý của văn bản. Sử dụng DocumentStatusMachineHelper.ValidateNewDocumentStatus() để kiểm tra trước khi set;
		/// <para> 0: văn bản dự thảo.</para>
		/// <para> 1: văn bản đang xử lý.</para>
		/// <para> 2: văn bản đã kết thúc.</para>
		/// <para> 4: văn bản đã hủy.</para>
		/// <para> 8: văn bản dừng xử lý.</para>
		/// </summary>
		public byte Status { get; set; }
		
		/// <summary>
		/// Trạng thái văn bản liên thông đến đi
		/// </summary>
		public string LienThongStatus { get; set; }
		
		/// <summary>
		/// Ngày cập nhật trạng thái liên thông
		/// </summary>
		public DateTime? DateLienThongStatus { get; set; }

		/// <summary>
		/// <para>Trạng thái của văn bản copy</para>
		/// <para>(CuongNT@bkav.com - 050413)</para>
		/// </summary>
		public DocumentStatus StatusInEnum
		{
			get
			{
				return (DocumentStatus)Status;
			}
		}

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
		/// <para>Lấy hoặc thiết lập Id nghiệp vụ</para>
		/// <para>(CuongNT@bkav.com - 050413)</para>
		/// </summary>
		public CategoryBusinessTypes CategoryBusinessIdInEnum { get { return (CategoryBusinessTypes)CategoryBusinessId; } }

		/// <summary>
		/// Lấy hoặc thiết lập Id người tạo văn bản.
		/// </summary>
		public int UserCreatedId { get; set; }

		/// <summary>
		/// Tên người khởi tạo.
		/// </summary>
		public string UserCreatedName { get; set; }

		/// <summary>
		/// Độ khẩn
		/// </summary>
		public byte UrgentId { get; set; }

		/// <summary>
		/// Nơi đến đi.
		/// </summary>
		public string InOutPlace { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Id cơ quan ban hành
		/// </summary>
		public string Organization { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mã cơ quan ban hành
        /// </summary>
        public string OrganizationCode { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id sổ văn bản
        /// </summary>
        public int? StoreId { get; set; }

		/// <summary>
		/// Tên sổ hồ sơ
		/// </summary>
		public string StoreName { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập số trang
		/// </summary>
		public int? TotalPage { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Id hình thức gửi
		/// </summary>
		public int? SendTypeId { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập ngày đến của văn bản (văn bản đến)
		/// </summary>
		public DateTime? DateArrived { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập ngày văn bản
		/// </summary>
		public DateTime? DatePublished { get; set; }

		/// <summary>
		/// Thời hạn xử lý (theo ngày)
		/// </summary>
		public int? ExpireProcess { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập mã sử dụng
		/// </summary>
		public int? CodeId { get; set; }

		/// <summary>
		/// Ghi chú thông tin khác
		/// </summary>
		public string Note { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? WorkflowTypeId { get; set; }

		/// <summary>
		/// Văn bản hồi báo: True (đã được hồi báo), False (cần hồi báo).
		/// </summary>
		public bool IsAcknowledged { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Id độ mật
		/// </summary>
		public int? SecurityId { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Ghi chú kí duyệt
		/// </summary>
		public bool? IsSuccess { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Ghi chú kí duyệt
		/// </summary>
		public string SuccessNote { get; set; }

		/// <summary>
		/// Thông tin xử lý: thông tin phát hành với văn bản đi và thông tin chuyển xử lý khi tiếp nhận văn bản đên.
		/// </summary>
		public string ProcessInfo { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập phat hanh
		/// </summary>
		public bool? IsTransferPublish { get; set; }

		/// <summary>
		/// Tổng số tiền lệ phí của hồ sơ
		/// </summary>
		public int TotalFee { get; set; }

		#region Special

		/// <summary>
		/// Id người thực sự xử lý văn bản, có thể là người đang đăng nhập hoặc id người ủy quyền.
		/// </summary>
		public int UserProcessId { get; set; }

		/// <summary>
		/// Quyền xử lý văn bản
		/// </summary>
		public int DocumentPermissions { get; set; }

		#endregion

		#region DocumentCopy

		/// <summary>
		/// Lấy hoặc thiết lập Id bản sao văn bản cấp cha(khi có đồng xử lý)
		/// </summary>
		public int? ParentId { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Id luồng văn bản, hồ sơ
		/// </summary>
		public int WorkflowId { get; set; }

		/// <summary>
		/// Phòng ban hiện tại
		/// </summary>
		public string CurrentDepartmentName { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Id người đang giữ văn bản
		/// </summary>
		public int UserCurrentId { get; set; }

		/// <summary>
		/// Tên người đang giữ
		/// </summary>
		public string UserCurrentName { get; set; }

		/// <summary>
		/// <para> Lấy hoặc thiết lập loại vệt</para>
		/// <para> Notes:</para>
		/// <para>   - 1: Hướng xử lý chính.</para>
		/// <para>   - 2: Hướng xử lý đồng xử lý.</para>
		/// <para>   - 4: Hướng xử lý xin ý kiến.</para>
		/// </summary>
		public int DocumentCopyType { get; set; }

		/// <summary>
		/// <para>Lấy hoặc thiết lập loại vệt</para>
		/// </summary>
		[JsonIgnore]
		public DocumentCopyTypes DocumentCopyTypeInEnum
		{
			get { return (DocumentCopyTypes)DocumentCopyType; }
		}

		/// <summary>
		/// Lấy hoặc thiết lập Node hiện tại trong quy trình của văn bản
		/// </summary>
		public int? NodeCurrentId { get; set; }

		/// <summary>
		/// <para>Lấy hoặc thiết lập Quyền của Node hiện tại trong quy trình của văn bản.</para>
		/// <para>Dữ liệu thuộc kiểu: NodePermissions.</para>
		/// <para>(CuongNT - 240113)</para>
		/// </summary>
		public int? NodeCurrentPermission { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập ngày cho ý kiến cuối cùng
		/// </summary>
		public DateTime? LastDateComment { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập ý kiến cuối cùng
		/// </summary>
		public string LastComment { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Tên người cho ý kiến cuối cùng
		/// </summary>
		public string LastUserComment { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập danh sách người tham gia xử lý văn bản dạng: ;UserId1;UserId2;...
		/// </summary>
		public string UserNguoiThamGia { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập tất cả những người liên quan đến văn bản dạng: ;UserId1;UserId2;...
		/// </summary>
		public string DocumentUsers { get; set; }

		/// <summary>
		/// Danh sách người đã xem
		/// </summary>
		public string UserNguoiDaXem { get; set; }


		/// <summary>
		/// Ngày nhận văn bản
		/// </summary>
		public DateTime DateReceived { get; set; }

		/// <summary>
		/// <para>Lấy hoặc thiết lập Vết đường đi. Nhận giá trị có khuôn dạng List&lt;HistoryPath&gt; Histories.StringifyJs(false);</para>
		/// <para>Set: Sử dụng SetHistories(HistoryProcess history) để gán giá trị mới cho History.</para>
		/// </summary>
		public string History
		{
			get;
			set;
		}

		#endregion

		#region HSMC Column

		/// <summary>
		/// Tên công dân: dùng cho hs một cửa
		/// </summary>
		public string CitizenName { get; set; }

		/// <summary>
		/// CMND
		/// </summary>
		public string IdentityCard { get; set; }

		/// <summary>
		/// Địa chỉ công dân
		/// </summary>
		public string Address { get; set; }

		/// <summary>
		/// Email
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// Phone
		/// </summary>
		public string Phone { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập trạng thái đã trả kết quả
		/// <para> True: Đã trả</para>
		/// <para> False: Chưa trả</para>
		/// <para> Null: Không trả kết quả</para>
		/// </summary>
		public bool? IsReturned { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Ngày kết thúc xử lý (eOffice), ngày trả kết quả (eGate)
		/// </summary>
		public DateTime? DateReturned { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Id người trả kết quả (eGate)
		/// </summary>
		public int? UserReturnedId { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Ghi chú trả kết quả (thông tin công dân nhận kết quả)
		/// </summary>
		public string ReturnNote { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Số tờ trình
		/// </summary>
		public string Keyword { get; set; }

		/// <summary>
		/// Dùng trong tiếp nhận HSMC, khi mất điện/lỗi hệ thống ko tiếp nhận được cv, lúc sau nhập lại lý do
		/// </summary>
		public string DelayReason { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int UserSuccessId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string UserSuccessName { get; set; }

		/// <summary>
		/// Nơi trả hồ sơ khi tiếp nhận
		/// </summary>
		public int? TypeReturned { get; set; }

		#endregion

		#region Relations

		/// <summary>
		/// Danh sách file đính kèm
		/// </summary>
		public IEnumerable<AttachmentCached> Attachments { get; set; }

		/// <summary>
		/// Văn bản liên quan
		/// </summary>
		public IEnumerable<DocRelationCached> RelationModels { get; set; }

		/// <summary>
		/// Ý kiến xử lý
		/// </summary>l
		public IEnumerable<CommentCached> CommentList { get; set; }

		/// <summary>
		/// Nội dung văn bản
		/// </summary>
		public IEnumerable<DocumentContentCached> DocumentContents { get; set; }

		/// <summary>
		/// Người ký duyệt
		/// </summary>
		public IEnumerable<ApproverCached> Approver { get; set; }

		/// <summary>
		/// Lệ phí
		/// </summary>
		public IEnumerable<DocFeeCached> DocFees { get; set; }

		/// <summary>
		/// Giấy tờ
		/// </summary>
		public IEnumerable<DocPaperCached> DocPapers { get; set; }

		/// <summary>
		/// Yêu cầu bổ sung
		/// </summary>
		public IEnumerable<SupplementaryCached> SupplementaryModel { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public IEnumerable<Store> Stores
		{
			get;
			set;
		}

        /// <summary>
        /// 
        /// </summary>
        public string TimeKey { get; set; }

		#endregion

		#region Method

		private HistoryProcess _histories;

		/// <summary>
		/// Lấy hoặc thiết lập Vết đường đi
		/// </summary>
		public HistoryProcess Histories
		{
			get
			{
				if (_histories == null)
				{
					if (string.IsNullOrEmpty(History))
					{
						_histories = new HistoryProcess();
						return _histories;
					}

					_histories = Json2.ParseAs<HistoryProcess>(History);
					_histories.HistoryPath = _histories.HistoryPath.OrderBy(c => c.DateCreated).ToList();
					_histories.HistoryThongbao = _histories.HistoryThongbao.OrderBy(c => c.DateCreated).ToList();
					_histories.HistoryXinykien = _histories.HistoryXinykien.OrderBy(c => c.DateCreated).ToList();
				}
				return _histories;
			}
			set
			{
				var result = value;
				result.HistoryPath = result.HistoryPath.OrderBy(c => c.DateCreated).ToList();
				result.HistoryThongbao = result.HistoryThongbao.OrderBy(c => c.DateCreated).ToList();
				result.HistoryXinykien = result.HistoryXinykien.OrderBy(c => c.DateCreated).ToList();

				_histories = result;
			}
		}

		/// <summary>
		/// Trạng thái thể hiện văn bản là hồ sơ một cửa
		/// </summary>
		public bool IsHSMC
		{
			get
			{
#if HoSoMotCuaEdition
				return CategoryBusinessIdInEnum == CategoryBusinessTypes.Hsmc;
#else
                return false;
#endif
			}
		}

		/// <summary>
		/// Người gửi gần nhất
		/// </summary>
		public int? UserSendId { get; internal set; }

		/// <summary>
		/// Người ủy quyền xử lý
		/// </summary>
		public string UserGiamSat { get; internal set; }

		/// <summary>
		/// Trả về danh sách tất cả người liên quan đến văn bản.
		/// </summary>
		/// <returns></returns>
		public List<int> DocumentUserList()
		{
			return ParseUserIds(DocumentUsers);
		}

		private List<int> ParseUserIds(string userStr)
		{
			var result = new List<int>();
			if (string.IsNullOrEmpty(userStr))
			{
				return result;
			}

			var userIds = userStr.Split(new string[] { SEPARATOR_CHAR }, StringSplitOptions.RemoveEmptyEntries);
			result = userIds.Where(u => !string.IsNullOrEmpty(u)).Select(u => Int32.Parse(u)).ToList();

			return result.Distinct().ToList();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public List<int> UserUyQuyen()
		{
			return ParseUserIds(UserGiamSat);
		}

		/// <summary>
		/// Người đã xem
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public bool IsViewed(int userId)
		{
			return !string.IsNullOrEmpty(UserNguoiDaXem) && UserNguoiDaXem.IndexOf(string.Format(";{0};", userId)) >= 0;
		}

        #endregion
        public int? StatusReport { get; set; }
        public List<ReceiveUnit> ReceiveUnits { get; set; }
    }

	#region File Đính kèm

	/// <summary>
	/// Tệp đính kèm
	/// </summary>
	public class AttachmentCached
	{
		/// <summary>
		/// Lấy hoặc thiết lập Id của tệp đính kèm
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Tên tệp đính kèm
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Lấy ra đuôi tệp đính kèm
		/// </summary>
		public string Extension
		{
			get;
			set;
		}

		/// <summary>
		/// Lấy hoặc thiết lập Phiên bản của tệp đính kèm
		/// </summary>
		public int VersionAttachment { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập 1 giá trị chỉ ra tệp đính kèm này đã bị xóa
		/// </summary>
		/// <value>
		/// 	<c>true</c> nếu tệp đính kèm này đã được xóa; ngược lại, <c>false</c>.
		/// </value>
		[JsonProperty("isRemoved")]
		public bool IsRemoved { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Kích thước tệp đính kèm
		/// </summary>
		public string Size { get; set; }

		/// <summary>
		/// Lấy ra kích thước tệp đính kèm dạng chuỗi
		/// </summary>
		public int LastestVesion
		{
			get;
			set;
		}

		/// <summary>
		/// Cacs phieen banr
		/// </summary>
		public IEnumerable<AttachmentDetailCache> Versions { get; set; }

		/// <summary>
		/// Người xóa
		/// </summary>
		public string UserDeleted { get; set; }

		/// <summary>
		/// Ngày xóa
		/// </summary>
		public string DeletedDate { get; set; }
	}

	/// <summary>
	/// Phiên bản file đính kèm
	/// </summary>
	public class AttachmentDetailCache
	{
		/// <summary>
		/// Lấy hoặc thiết lập Id thông tin chi tiết tệp đính kèm
		/// </summary>
		public int Version { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Ngày tạo tệp đính kèm
		/// </summary>
		public string CreateDate { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Người tạo tệp đính kèm
		/// </summary>
		public string User { get; set; }
	}

	#endregion

	#region Văn bản liên quan

	/// <summary>
	/// 
	/// </summary>
	public class DocRelationCached
	{

		/// <summary>
		/// Lấy hoặc thiết lập Id quan hệ giữa các hồ sơ liên quan
		/// </summary>
		public int DocRelationId { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Id hồ sơ liên quan
		/// </summary>
		public Guid RelationId { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Id văn bản/hồ sơ copy liên quan
		/// </summary>
		public int RelationCopyId { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập trích yếu văn bản liên quan.
		/// </summary>
		public string Compendium { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập tên công dân
		/// </summary>
		public string CitizenName { get; set; }

		/// <summary>
		/// Số ký hiệu
		/// </summary>
		public string DocCode { get; set; }

		/// <summary>
		/// Loại liên quan
		/// </summary>
		public int RelationType { get; set; }
	}

	#endregion

	#region Ý kiến xử lý

	/// <summary>
	/// 
	/// </summary>
	public class CommentCached
	{
		/// <summary>
		/// Lấy hoặc thiết lập Id ý kiến
		/// </summary>
		public int CommentId { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Nội dung ý kiến
		/// </summary>
		public string Content { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập document copy của hướng nhận
		/// </summary>
		public int? DocumentCopyTargetId { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Ngày gửi ý kiến
		/// </summary>
		public DateTime DateCreated { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string DateCreatedString
		{
			get
			{
				var format = "HH:mm dd/MM";
				if (DateCreated.Year != DateTime.Now.Year)
				{
					format = "HH:mm dd/MM/yy";
				}

				return DateCreated.ToString(format);
			}
		}

		/// <summary>
		/// Lấy hoặc thiết lập Id người gửi ý kiến
		/// </summary>
		public int? UserSendId { get; set; }

		/// <summary>
		/// Người nhận ý kiến.
		/// </summary>
		public int? UserReceiveId { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Id loại ý kiến
		/// </summary>
		public byte CommentType { get; set; }

		/// <summary>
		/// Kết quả sử lý: sử dụng khi lấy ý kiến ý duyệt, bổ sung.
		/// </summary>
		public string Result { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập thông tin nội dung người ủy quyền xử lý văn bản
		/// </summary>
		public string Content2 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public List<CommentCached> Children { get; set; }

        /// <summary>
        /// 20200317 Json Diff
        /// </summary>
        public string Diff { get; set; }
	}

	#endregion

	#region Nội dung văn bản

	/// <summary>
	/// 
	/// </summary>
	public class DocumentContentCached
	{
		/// <summary>
		/// Key
		/// </summary>
		public int DocumentContentId { get; set; }

		/// <summary>
		/// Get or set the content name = form name
		/// </summary>
		public string ContentName { get; set; }

		/// <summary>
		/// Get or set the document's content: form or html.
		/// </summary>
		public string Content { get; set; }

		/// <summary>
		/// Get or set the type of form: dynamic form or html form.
		/// </summary>
		public int FormTypeId { get; set; }

        // 20200113 VuHQ START
        public string FormId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Url { get; set; }

		public string ContentUrl { get; set; }

        /// <summary>
        /// ContentUrl BWSS nếu là kqklmr
        /// </summary>
        public string FormCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ConfigFunction { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CompilationId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ChildCompilationId { get; set; }

        // 20191125 VuHQ START REQ-5
        /// <summary>
        /// DefineFieldJson
        /// </summary>
        public string DefineFieldJson { get; set; }

        /// <summary>
        /// DefineConfigJson
        /// </summary>
        public string DefineConfigJson { get; set; }

        /// <summary>
        /// DefineValueJson
        /// </summary>
        public string DefineValueJson { get; set; }

        /// <summary>
        /// FormHeader
        /// </summary>
        public string FormHeader { get; set; }

        /// <summary>
        /// FormFooter
        /// </summary>
        public string FormFooter { get; set; }
        // 20191125 VuHQ END REQ-5

        // 20200210 VuHQ Phase 2 - REQ 0
        public string ExplicitTemplate { get; set; }

        // 20200210 VuHQ Phase 2 - REQ 0
        public string Compilation { get; set; }

        public int? FormCategoryId { get; set; }
    }

	#endregion

	#region Ký duyệt

	/// <summary>
	/// 
	/// </summary>
	public class ApproverCached
	{
		/// <summary>
		/// Key
		/// </summary>
		public int ApproverId { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập account người ký
		/// </summary>
		public string UserName { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Tên người ký
		/// </summary>
		public string FullName { get; set; }

		/// <summary>
		/// Get or set the approve's content
		/// </summary>
		public string Content { get; set; }

		/// <summary>
		/// Get or set the approved datetime
		/// </summary>
		public DateTime DateCreated { get; set; }

		/// <summary>
		/// Get or set the approved result
		/// </summary>
		public bool IsSuccess { get; set; }
	}

	#endregion

	#region Lệ phí

	/// <summary>
	/// 
	/// </summary>
	public class DocFeeCached
	{
		/// <summary>
		/// Auto increment key
		/// </summary>
		public int DocFeeId { get; set; }

		/// <summary>
		/// Get or set the FeeName
		/// </summary>
		public string FeeName { get; set; }

		/// <summary>
		/// Get or set the price of Fee
		/// </summary>
		public int Price { get; set; }

		/// <summary>
		/// Get or set the doc-fee type
		/// </summary>
		public int Type { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Id lần yêu cầu bổ sung
		/// </summary>
		public int? SupplementaryId { get; set; }
	}

	#endregion

	#region Giấy tờ

	/// <summary>
	/// 
	/// </summary>
	public class DocPaperCached
	{
		/// <summary>
		/// Auto increment key.
		/// </summary>
		public int DocPaperId { get; set; }

		/// <summary>
		/// Get or set the PaperName.
		/// </summary>
		public string PaperName { get; set; }

		/// <summary>
		/// Get or ser the amount of Paper.
		/// </summary>
		public int Amount { get; set; }

		/// <summary>
		/// Get or set the doc-paper type
		/// </summary>
		public int Type { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Id lần yêu cầu bổ sung
		/// </summary>
		public int? SupplementaryId { get; set; }
	}

	#endregion

	#region Yêu cầu bổ sung

	/// <summary>
	/// 
	/// </summary>
	public class SupplementaryCached
	{
		/// <summary>
		/// Get or set the key
		/// </summary>
		public int SupplementaryId { get; set; }

		/// <summary>
		/// Get or set the document id.
		/// </summary>
		[JsonIgnore]
		public Guid DocumentId { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Id hướng chuyển đã Tiếp nhận bổ sung/Cập nhật kết quả dừng xử lý.
		/// </summary>
		public int? DocumentCopyId { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Id cán bộ yêu cầu bổ sung/dừng xử lý
		/// </summary>
		public int UserSendId { get; set; }

		/// <summary>
		/// <para>Danh sách các hướng chuyển nhận yêu cầu dừng xử lý (documentCopyId), dạng: ;23;18;584; </para>
		/// </summary>
		public string DocumentCopyIds { get; set; }

		/// <summary>
		/// Get or set the request comment
		/// </summary>
		public string CommentSend { get; set; }

		/// <summary>
		/// Get or set the request date.
		/// </summary>
		public DateTime DateSend { get; set; }

		/// <summary>
		/// Get or set the receive user id.
		/// </summary>
		public int? UserReceivedId { get; set; }

		/// <summary>
		/// Get or set the receive comment.
		/// </summary>
		public string CommentReceived { get; set; }

		/// <summary>
		/// Get or set the receive date.
		/// </summary>
		public DateTime? DateReceived { get; set; }

		/// <summary>
		/// Get or set the date process
		/// </summary>
		public DateTime? DateBeginProcess { get; set; }

		/// <summary>
		/// Get or set the supplement type.
		/// </summary>
		public int SupplementType { get; set; }

		/// <summary>
		/// Get or set the offset day: only used when SupplementType = SupplementType.AddDay.
		/// </summary>
		public int OffsetDay { get; set; }

		/// <summary>
		/// Get or set the result: success or no.
		/// </summary>
		public bool IsSuccess { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập trạng thái yêu cầu bổ sung đã được xử lý hay chưa
		/// </summary>
		public bool IsReceived { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập lần yêu cầu bổ sung
		/// </summary>
		public int Order { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập danh sách giấy tờ yêu cầu bổ sung
		/// </summary>
		public string PaperIds { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập danh sách lệ phí yêu cầu bổ sung
		/// </summary>
		public string FeeIds { get; set; }

		/// <summary>
		/// Danh sách giấy tờ
		/// </summary>
		public string Papers { get; set; }

		/// <summary>
		/// Danh sách lệ phí
		/// </summary>
		public string Fees { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập ngày hẹn trả cũ
		/// </summary>
		public DateTime? OldDateAppointed { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập ngày hẹn trả mới
		/// </summary>
		public DateTime? NewDateAppointed { get; set; }

		/// <summary>
		/// Ngày cập nhật với eGov Online gần nhất
		/// </summary>
		public DateTime? DateOnlineUpdate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string UserSendName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string UserReceiveName { get; set; }

		/// <summary>
		/// eGov SupplementaryId - dùng khi đồng bộ eGov Online
		/// </summary>
		public int LocalId { get; set; }

		/// <summary>
		/// Nội dung bổ sung - dùng khi đồng bộ eGov Online
		/// </summary>
		public string Details { get; set; }
	}

	#endregion

	#region HSMC



	#endregion

    public class ReceiveUnit
    {
        public int UserUnit { get; set; }
        public string UserNote { get; set; }
        public string UserName { get; set; }
        public int UserStatus { get; set; }
    }
}
