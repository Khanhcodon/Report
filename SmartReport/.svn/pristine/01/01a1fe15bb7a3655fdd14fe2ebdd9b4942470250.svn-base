using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models.Settings
{
	[FluentValidation.Attributes.Validator(typeof(AdminGeneralSettingsValidator))]
	public class AdminGeneralSettingsModel
	{
		public AdminGeneralSettingsModel()
		{
			HasUseHSMC = false;
			OnlyUserCreateChangeDateAppointed = false;
			HasCheckViewDocumentPermission = true;
			ShowApproverByDepartment = false;
			SaveUserActivity = false;
			ShowPlaceInOffice = true;
			DetectPdfChangeContent = true;
			AllowThuHoiVbLienThong = false;
		}

		#region Thiết lập chung

		/// <summary>
		/// Lấy hoặc thiết lập 1 giá trị chỉ ra sẽ tải tất cả resource rồi lưu vào cache khi hệ thống bắt đầu chạy
		/// </summary>
		[LocalizationDisplayName("Setting.General.Fields.IsLoadAllResourceOnStartup.Label")]
		public bool IsLoadAllResourceOnStartup { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Số bản ghi trên 1 trang mặc định
		/// </summary>
		[LocalizationDisplayName("Setting.General.Fields.DefaultPageSize.Label")]
		public int DefaultPageSize { get; set; }

		/// <summary>
		/// Lẩy hoặc thiết lập Danh sách page size (áp dụng cho phân trang để người dùng có thể chọn nhiều loại pagesize khác nhau)
		/// </summary>
		[LocalizationDisplayName("Setting.General.Fields.ListPageSizeParsed.Label")]
		public string ListPageSizeParsed { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập sử dụng chức năng load page dạng scroll (giống load page của facebook) hay dạng phân trang thông thường
		/// </summary>
		[LocalizationDisplayName("Setting.General.Fields.LoadPageScroll.Label")]
		public bool IsLoadPageScroll { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Số bản ghi trên 1 trang mặc định trên trang chủ
		/// </summary>
		[LocalizationDisplayName("Setting.General.Fields.DefaultPageSizeHome.Label")]
		public int DefaultPageSizeHome { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Danh sách page size (chỉ áp dụng cho phân trang để người dùng có thể chọn nhiều loại pagesize)
		/// </summary>
		[LocalizationDisplayName("Setting.General.Fields.ListPageSizeParsedHome.Label")]
		public string ListPageSizeParsedHome { get; set; }

		/// <summary>
		/// Định dạng tiền tệ: .000 hay .00
		/// </summary>
		[LocalizationDisplayName("Setting.General.Fields.MoneyFormat.Label")]
		public string MoneyFormat { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập hệ thống có sử dụng nghiệp vụ hồ sơ một cửa hay không
		/// </summary>
		public bool HasUseHSMC { get; set; }

		/// <summary>
		/// Lưu lại thao tác người dùng khi truy cập hệ thống
		/// </summary>
		public bool SaveUserActivity { get; set; }
        /// <summary>
		/// Cấu hình gửi nhanh
		/// </summary>
		public bool IsFastTransfer { get; set; }
        /// <summary>
		/// Cấu hình dinh kem file
		/// </summary>
        public bool IsFileTag { get; set; }
        /// <summary>
		/// Cấu hình kiem tra bao cao
		/// </summary>
        public bool IsCreatedForm { get; set; }

        #endregion

        #region Thiết lập xử lý văn bản

        /// <summary>
        /// Thiết lập kích hoạt là bỏ qua mọi config các đối tượng kết thúc trên trong các quy trình mà chỉ xét đối tượng khởi tạo là đựoc kết thúc.
        /// </summary>
        [LocalizationDisplayName("Setting.General.Fields.UserCreatetedHasClose.Label")]
		public bool UserCreatetedHasClose { get; set; }

		/// <summary>
		/// Cho phép người khởi tạo có thể sửa văn bản bất cứ lúc nào
		/// </summary>
		public bool UserCreatedHasChangeDocument { get; set; }

		/// <summary>
		/// Cho phép kết thúc văn bản đến khi trả lời bằng văn bản đi.
		/// </summary>
		public bool FinishOriginalDocumentWhenAnswer { get; set; }

		/// <summary>
		/// Có check quyền xem văn bản hay không.
		/// Nếu không: tất cả mọi người được phép mở văn bản xem kể cả không có luồng xử lý.
		/// Nếu có: Mặc định check quyền khi mở, và check cả quyền xem tất cả văn bản của người dùng ở cấu hình người dùng.
		/// </summary>
		public bool HasCheckViewDocumentPermission { get; set; }

		/// <summary>
		/// Bắt buộc chọn xlc khi xử lý văn bản.
		/// </summary>
		public bool RequireChooseXlc { get; set; }

		/// <summary>
		/// Bắt buộc nhập ý kiến khi kết thúc văn bản
		/// </summary>
		public bool RequireCommentWhenFinish { get; set; }


		#endregion

		#region Thiết lập xử lý hsmc

		/// <summary>
		/// Cho phép kết thúc luôn hồ sơ khi trả kết quả
		/// </summary>
		public bool HasFinishDocumentWhenReturnResult { get; set; }

		/// <summary>
		/// Chỉ cho phép người khởi tạo được sửa hạn xử lý.
		/// </summary>
		public bool OnlyUserCreateChangeDateAppointed { get; set; }

		/// <summary>
		/// Lẩy hoặc thiết lập cấu hình hiển thị hủy văn bản
		/// </summary>
		public bool IsNotAllowFinishDocument { get; set; }

		/// <summary>
		/// Lẩy hoặc thiết lập cấu hình hiển thị gia hạn văn bản
		/// </summary>
		public bool IsNotAllowRenewal { get; set; }

		#endregion

		#region Thiết lập phát hành văn bản

		/// <summary>
		/// Bắt buộc có dự kiến phát hành khi khởi tạo văn bản đi.
		/// </summary>
		public bool RequirePublishPlanWhenCreate { get; set; }

		/// <summary>
		/// Danh sách tài khoản ko yêu cầu nhập dự kiến phát hành
		/// </summary>
		public string IgnoreRequirePublishPlan { get; set; }

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
		/// Hiển thị nơi nhận trong đơn vị
		/// </summary>
		public bool ShowPlaceInOffice { get; set; }

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

        #endregion

        #region Thiết lập hiển thị người dùng phòng ban

        /// <summary>
        ///  Thiết lập hiển thị tên người dùng
        /// 1: Hiện thị tên đăng nhập
        /// 2: Hiển thị họ và tên
        /// 3 : Hiển thị cả họ tên và tên đăng nhập
        /// </summary>
        [LocalizationDisplayName("Setting.General.Fields.ShowAcountType.Label")]
		public byte ShowAcountType { get; set; }

		/// <summary>
		/// Thiết lập hiển thị phòng ban
		/// </summary>
		public byte ShowDepartmentType { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập đường dẫn anh đại diện của người dùng
		/// </summary>
		[LocalizationDisplayName("Setting.General.Fields.Avatar.Label")]
		public string Avatar { get; set; }

		#endregion

		#region Thiết lập khác

		/// <summary>
		/// Lấy hoặc thiết lập người duyệt lịch
		/// </summary>
		public string UserAcceptCalendarIds { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập id người giữ được bỏ qua khi tính quá hạn
		/// </summary>
		public int UserIgnoreOverdueId { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập ussername người giữ được bỏ qua khi tính quá hạn
		/// </summary>
		public string UserIgnoreOverdueName { get; set; }

		/// <summary>
		/// Cho phép cấu hình chữ ký số
		/// </summary>
		public string UrlSignature { get; set; }

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
        public string GovWorkFlowId { get; set; }

        /// <summary>
        /// Lĩnh vự (DocField) liên thông chính phủ
        /// </summary>
        public string GovDocFieldId { get; set; }

        /// <summary>
        /// Cấp hành chính (LevelId) liên thông chính phủ
        /// </summary>
        public string GovLevelId { get; set; }
        #endregion
    }
}