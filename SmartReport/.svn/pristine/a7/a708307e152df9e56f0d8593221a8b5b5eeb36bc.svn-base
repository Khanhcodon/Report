using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Api.Dto
{
    public class SearchResultDto
    {
        #region Columns

        /// <summary>
        /// Lấy hoặc thiết lập Id văn bản, hồ sơ
        /// </summary>
        public Guid DocumentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id loại văn bản, hồ sơ
        /// </summary>
        public Guid DocTypeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id thể loại
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Số kí hiệu (eOffice), mã hồ sơ (eGate)
        /// </summary>
        public string DocCode { get; set; }

        /// <summary>
        /// Tên công dân: dùng cho hs một cửa
        /// </summary>
        public string CitizenName { get; set; }

        /// <summary>
        /// Thông tin khác về công dân
        /// </summary>
        public string CitizenInfo { get; set; }

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
        /// Lấy hoặc thiết lập Trích yếu
        /// </summary>
        public string Compendium { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Trích yếu không dấu
        /// </summary>
        public string Compendium2 { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ngày tạo
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ngày hẹn trả (eGate), ngày giải quyết (eOffice)
        /// </summary>
        public DateTime? DateAppointed { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ngày cập nhật
        /// </summary>
        public DateTime DateModified { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id người tạo văn bản.
        /// </summary>
        public int UserCreatedId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái xử lý cuối cùng của hồ sơ
        /// <para> True: Đồng ý</para>
        /// <para> False: Từ chối</para>
        /// <para> Null: Chưa duyệt</para>
        /// </summary>
        public bool? IsSuccess { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ngày kí duyệt
        /// </summary>
        public DateTime? DateSuccess { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id người kí chính (eOffice), người kí duyệt (eGate)
        /// </summary>
        public int? UserSuccessId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ghi chú kí duyệt
        /// </summary>
        public string SuccessNote { get; set; }

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
        /// Lấy hoặc thiết lập ngày kết thúc xử lý văn bản
        /// </summary>
        public DateTime? DateFinished { get; set; }

        /// <summary>
        /// Trạng thái xử lý của văn bản. Sử dụng DocumentStatusMachineHelper.ValidateNewDocumentStatus() để kiểm tra trước khi set;
        /// <para> 1: văn bản dự thảo.</para>
        /// <para> 2: văn bản đang xử lý.</para>
        /// <para> 4: văn bản đã kết thúc.</para>
        /// <para> 8: văn bản đã hủy.</para>
        /// <para> 16: văn bản dừng xử lý.</para>
        /// </summary>
        public byte Status { get; set; }

        /// <summary>
        /// Văn bản hồi báo: True (đã được hồi báo), False (cần hồi báo).
        /// </summary>
        public bool IsAcknowledged { get; set; }

        /// <summary>
        /// Đang gửi liên thông.
        /// </summary>
        public bool IsGettingOut { get; set; }

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
        /// Văn bản được convert từ hệ thống cũ.
        /// </summary>
        public bool IsConverted { get; set; }

        /// <summary>
        /// Độ khẩn
        /// </summary>
        public byte UrgentId { get; set; }

        /// <summary>
        /// Số đến đi, mã đến đi.
        /// </summary>
        public string InOutCode { get; set; }

        /// <summary>
        /// Nơi đến đi.
        /// </summary>
        public string InOutPlace { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái có yêu cầu bổ sung hay không
        /// <para> True: Đã bổ sung</para>
        /// <para> False: Chưa bổ sung</para>
        /// <default> Null: Chưa từng có yêu cầu bổ sung </default>
        /// </summary>
        public bool? IsSupplemented { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Danh sách Id của lĩnh vực (lưu dạng ;id;id;)
        /// </summary>
        public string DocFieldIds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? DateResponsed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? DateResponsedOverdue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ProcessedMinutes { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ngày tạo mã hồ sơ/văn bản
        /// </summary>
        public DateTime? DateOfIssueCode { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ngày(thời hạn) hồi báo
        /// </summary>
        public DateTime? DateResponse { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id cơ quan ban hành
        /// </summary>
        public string Organization { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id sổ văn bản
        /// </summary>
        public int? StoreId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id độ mật
        /// </summary>
        public int? SecurityId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập số trang
        /// </summary>
        public int? TotalPage { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id từ khóa
        /// </summary>
        public string Keyword { get; set; }

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

        #endregion
    }
}
