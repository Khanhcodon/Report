using System.Collections.Generic;
using System.Linq;

namespace Bkav.eGovCloud.Entities.Common
{
	/// <summary>
	/// Bkav Corp. - BSO - eGov - eOffice team
	/// Project: eGov Cloud v1.0
	/// Class : AdminGeneralSettings - public - Entity
	/// Access Modifiers: 
	///     * Implement: ISettings
	/// Create Date : 140812
	/// Author      : TrungVH
	/// Description : Entity cho phần cấu hình chung
	/// </summary>
	public class AdminGeneralSettings : ISettings
	{
		private int _defaultPageSize;
		private int _defaultPageSizeHome;

		/// <summary>
		/// Khởi tạo class
		/// </summary>
		public AdminGeneralSettings()
		{
			ListPageSize = new List<int>();
			ListPageSizeHome = new List<int>();
			HasUsingGiamSat = false;
			HasFinishDocumentWhenReturnResult = true;
			OnlyUserCreateChangeDateAppointed = false;
			HasCheckViewDocumentPermission = true;
			ShowApproverByDepartment = false;
			SaveUserActivity = false;
			ShowPlaceInOffice = true;
			DetectPdfChangeContent = true;
			AllowThuHoiVbLienThong = false;
		}

		/// <summary>
		/// Lấy hoặc thiết lập 1 giá trị chỉ ra sẽ tải tất cả resource rồi lưu vào cache khi hệ thống bắt đầu chạy
		/// </summary>
		public bool IsLoadAllResourceOnStartup { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập sử dụng chức năng load page dạng scroll (giống load page của facebook) hay dạng phân trang thông thường
		/// </summary>
		public bool IsLoadPageScroll { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Số bản ghi trên 1 trang mặc định
		/// </summary>
		public int DefaultPageSize
		{
			get { return _defaultPageSize <= 0 ? 25 : _defaultPageSize; }
			set { _defaultPageSize = value; }
		}

		/// <summary>
		/// Lẩy hoặc thiết lập cấu hình hiển thị hủy văn bản
		/// </summary>
		public bool IsNotAllowFinishDocument { get; set; }

		/// <summary>
		/// Lẩy hoặc thiết lập cấu hình hiển thị gia hạn văn bản
		/// </summary>
		public bool IsNotAllowRenewal { get; set; }

		/// <summary>
		/// Lẩy hoặc thiết lập Danh sách page size (áp dụng cho phân trang để người dùng có thể chọn nhiều loại pagesize khác nhau)
		/// </summary>
		public List<int> ListPageSize { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Số bản ghi trên 1 trang mặc định trên trang chủ
		/// </summary>
		public int DefaultPageSizeHome
		{
			get { return _defaultPageSizeHome <= 0 ? 25 : _defaultPageSizeHome; }
			set { _defaultPageSizeHome = value; }
		}

		/// <summary>
		/// Lẩy hoặc thiết lập Danh sách page size trên trang chủ (áp dụng cho phân trang để người dùng có thể chọn nhiều loại pagesize khác nhau)
		/// </summary>
		public List<int> ListPageSizeHome { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập 1 giá trị hệ thống chạy SSL
		/// </summary>
		public bool EnableSsl { get; set; }

		/// <summary>
		/// Thiết lập kích hoạt là bỏ qua mọi config các đối tượng kết thúc trên trong các quy trình mà chỉ xét đối tượng khởi tạo là đựoc kết thúc.
		/// </summary>
		public bool UserCreatetedHasClose { get; set; }

		/// <summary>
		/// Định dạng tiền tệ: .000 hay .00
		/// </summary>
		public string MoneyFormat { get; set; }

		/// <summary>
		///  Thiết lập hiển thị tên người dùng
		/// 1: Hiện thị tên đăng nhập
		/// 2: Hiển thị họ và tên
		/// 3: Hiển thị cả họ tên và tên đăng nhập
		/// </summary>
		public byte ShowAcountType { get; set; }

		/// <summary>
		/// Thiết lập hiển thị phòng ban
		/// 1: Hiện thị phòng ban hiện tại
		/// 2: Hiển thị phòng ban hiện tại và cha của nó
		/// 3: Hiển thị phòng ban đầy đủ
		/// </summary>
		public byte ShowDepartmentType { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập hệ thống có sử dụng nghiệp vụ hồ sơ một cửa hay không
		/// </summary>
		public bool HasUseHSMC { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập đường dẫn anh đại diện của người dùng
		/// </summary>
		public string Avatar { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập trạng thái cho phép cấp số cho văn bản đi.
		/// </summary>
		public bool HasUsingCodeForVbDi { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập người duyệt lịch
		/// </summary>
		public string UserAcceptCalendarIds { get; set; }

		/// <summary>
		/// Danh sách người duyệt licchj
		/// </summary>
		public IEnumerable<int> UserAcceptCalendarList
		{
			get
			{
				if (string.IsNullOrEmpty(UserAcceptCalendarIds))
				{
					return new List<int>();
				}

				return UserAcceptCalendarIds.Split(new char[] { ',' }).Select(u => int.Parse(u));
			}
		}

		/// <summary>
		/// Lấy hoặc thiết lập người duyệt lịch
		/// </summary>
		public string UserAcceptCalendarName { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập id người giữ được bỏ qua khi tính quá hạn
		/// </summary>
		public int UserIgnoreOverdueId { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập ussername người giữ được bỏ qua khi tính quá hạn
		/// </summary>
		public string UserIgnoreOverdueName { get; set; }

		/// <summary>
		/// Cho phép người khởi tạo có thể sửa văn bản bất cứ lúc nào
		/// </summary>
		public bool UserCreatedHasChangeDocument { get; set; }

		/// <summary>
		/// Cho phép chọn người giám sát khi gửi văn bản
		/// </summary>
		public bool HasUsingGiamSat { get; set; }

		/// <summary>
		/// Cho phép kết thúc luôn hồ sơ khi trả kết quả
		/// </summary>
		public bool HasFinishDocumentWhenReturnResult { get; set; }

		/// <summary>
		/// Cho phép kết thúc văn bản đến khi trả lời bằng văn bản đi.
		/// </summary>
		public bool FinishOriginalDocumentWhenAnswer { get; set; }

		/// <summary>
		/// Cho phép cấu hình chữ ký số
		/// </summary>
		public string UrlSignature { get; set; }

		/// <summary>
		/// Có check quyền xem văn bản hay không.
		/// Nếu không: tất cả mọi người được phép mở văn bản xem kể cả không có luồng xử lý.
		/// Nếu có: Mặc định check quyền khi mở, và check cả quyền xem tất cả văn bản của người dùng ở cấu hình người dùng.
		/// </summary>
		public bool HasCheckViewDocumentPermission { get; set; }

		/// <summary>
		/// Chỉ cho phép người khởi tạo được sửa hạn xử lý.
		/// </summary>
		public bool OnlyUserCreateChangeDateAppointed { get; set; }

		/// <summary>
		/// Bắt buộc có dự kiến phát hành khi khởi tạo văn bản đi.
		/// </summary>
		public bool RequirePublishPlanWhenCreate { get; set; }

		/// <summary>
		/// Danh sách tài khoản ko yêu cầu nhập dự kiến phát hành
		/// </summary>
		public string IgnoreRequirePublishPlan { get; set; }

		/// <summary>
		/// Bắt buộc chọn xlc khi xử lý văn bản.
		/// </summary>
		public bool RequireChooseXlc { get; set; }

		/// <summary>
		/// Bắt buộc nhập ý kiến khi kết thúc văn bản
		/// </summary>
		public bool RequireCommentWhenFinish { get; set; }

		/// <summary>
		/// Hiển thị người ký theo đơn vị trực thuộc
		/// </summary>
		public bool ShowApproverByDepartment { get; set; }

		/// <summary>
		/// Lấy danh sách theo chức vụ hoặc chức danh
		/// chức vụ: 1
		/// chức danh: 0
		/// </summary>
		public int TypePositionTitleJob { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập trạng thái xác định có lưu lại xử lý của người dùng hay ko
		/// </summary>
		public bool SaveUserActivity { get; set; }

        /// <summary>
		/// Cau hinh gui nhanh
		/// </summary>
		public bool IsFastTransfer { get; set; }
        /// <summary>
		/// Cấu hình dinh kem file
		/// </summary>
        public bool IsFileTag { get; set; }

        /// <summary>
		/// Cấu hình kiem tra tao bao cao
		/// </summary>
        public bool IsCreatedForm { get; set; }
        /// <summary>
        /// Hiển thị nơi nhận trong đơn vị
        /// </summary>
        public bool ShowPlaceInOffice { get; set; }

		/// <summary>
		/// Trả về danh sách tên tài khoản bỏ qua yêu cầu phát hành
		/// </summary>
		public List<string> IgnoreRequirePublishPlanList
		{
			get
			{
				if (string.IsNullOrWhiteSpace(IgnoreRequirePublishPlan)) return new List<string>();

				return IgnoreRequirePublishPlan.Split(new char[] { ';' }).ToList();
			}
		}

		/// <summary>
		/// Trả về trạng thái xác định có detect thay đổi file pdf ở vị trí văn thư hay không
		/// </summary>
		public bool DetectPdfChangeContent { get; set; }

		/// <summary>
		/// Cho phép thu hồi văn bản liên thông
		/// </summary>
		public bool AllowThuHoiVbLienThong { get; set; }

        /// <summary>
        /// Đường dẫn gửi báo cáo
        /// </summary>
        public string BITranports { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DashboardConnection { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BIUrl { get; set; }

        /// <summary>
        /// WorkFlowId liên thông chính phủ
        /// </summary>
        public int? GovWorkFlowId { get; set; }

        /// <summary>
        /// Lĩnh vự (DocField) liên thông chính phủ
        /// </summary>
        public int? GovDocFieldId { get; set; }

        /// <summary>
        /// Cấp hành chính (LevelId) liên thông chính phủ
        /// </summary>
        public int? GovLevelId { get; set; }
    }
}
