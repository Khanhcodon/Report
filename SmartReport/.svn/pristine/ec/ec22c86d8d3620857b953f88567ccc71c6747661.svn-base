using System;
using System.Collections.Generic;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Api.Dto
{
    public class DocumentOnlineLookup
    {
        /// <summary>
        /// Id hồ sơ
        /// </summary>
        public Guid DocumentId { get; set; }

        /// <summary>
        /// Trích yếu hoặc tiêu đề hồ sơ
        /// </summary>
        public string Compendium { get; set; }

        /// <summary>
        /// Tên công dân
        /// </summary>
        public string CitizenName { get; set; }

        /// <summary>
        /// CMND
        /// </summary>
        public string IdCard { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Địa chỉ mail
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Số hồ sơ
        /// </summary>
        public string DocCode { get; set; }

        /// <summary>
        /// Ngày hẹn trả.
        /// </summary>
        public DateTime? DateAppointed { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ngày kí duyệt
        /// </summary>
        public DateTime? DateSuccess { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id người kí chính (eOffice), người kí duyệt (eGate)
        /// </summary>
        public string UserSuccess { get; set; }

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
        public string UserReturned { get; set; }

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
        /// <para> 0: văn bản dự thảo.</para>
        /// <para> 1: văn bản đang xử lý.</para>
        /// <para> 2: văn bản đã kết thúc.</para>
        /// <para> 4: văn bản đã hủy.</para>
        /// <para> 8: văn bản dừng xử lý.</para>
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

        public string CurrentUser { get; set; }

        public string CurrentDept { get; set; }

        public List<DocumentProcessDto> Progress { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập danh sách yêu cầu bổ sung
        /// </summary>
        public List<SupplementaryDto> Supplementaries { get; set; }

        public bool? IsSuccess { get; set; }

        public bool? IsSupplemented { get; set; }

        public DateTime DateReceived { get; set; }

        public string DocTypeName { get; set; }
    }

    public class DocumentDisplay
    {
        /// <summary>
        /// Id hồ sơ
        /// </summary>
        public Guid DocumentId { get; set; }

        /// <summary>
        /// Trích yếu hoặc tiêu đề hồ sơ
        /// </summary>
        public string Compendium { get; set; }

        /// <summary>
        /// Tên công dân
        /// </summary>
        public string CitizenName { get; set; }

        /// <summary>
        /// Số hồ sơ
        /// </summary>
        public string DocCode { get; set; }

        /// <summary>
        /// Ngày tiếp nhận hồ sơ
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Loai thu tuc
        /// </summary>
        public string DoctypeName { get; set; }
    }
}