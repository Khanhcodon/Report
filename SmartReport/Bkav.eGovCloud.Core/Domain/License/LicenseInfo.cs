using System;

namespace Bkav.eGovCloud.Core.License
{
    /// <summary>
    /// Thông tin licênse
    /// </summary>
    public class LicenseInfo
    {
        /// <summary>
        /// Mã license
        /// </summary>
        public string LicenseCode { get; set; }

        /// <summary>
        /// Trạng thái xác định licênse miễn phí
        /// </summary>
        public bool IsFreeMode { get; set; }

        /// <summary>
        /// Tên khách hàng
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Tổng số user trong hệ thống
        /// </summary>
        public int TotalUser { get; set; }

        /// <summary>
        /// ProcessorId của CPU
        /// </summary>
        public string CpuProcessorId { get; set; }

        /// <summary>
        /// Serial ổ cứng
        /// </summary>
        public string DiskDriveSerial { get; set; }

        /// <summary>
        /// Serial bo mạch chủ
        /// </summary>
        public string MotherBoardSerial { get; set; }

        /// <summary>
        /// Ngày hết hạn
        /// </summary>
        public DateTime ToDate { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Lần kiểm tra license gần nhất
        /// </summary>
        public DateTime LastUpdate { get; set; }

        /// <summary>
        /// Thông báo trạng thái license hiện tại.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Trạng thái license isValid
        /// </summary>
        public bool IsValid { get; set; }
    }
}
