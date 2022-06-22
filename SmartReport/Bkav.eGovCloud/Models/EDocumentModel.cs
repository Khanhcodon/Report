using System;

namespace Bkav.eGovCloud.Models
{
    /// <summary>
    /// Lưu trữ thông tin hồ sơ tra cứu được từ mã hồ sơ tra cứu do khách gửi
    /// </summary>
    public class EDocumentModel
    {
        public EDocumentModel()
        {
            DocCode = "MaHoSo";
            DeptName = "PhongBanXuLy";
            StaffName = "CanBoXuLy";
            CompleteDate = DateTime.MinValue;
            GivePerson = "TenCongDan";
            Status = "";
            IsCompleted = false;
        }

        /// <summary>
        /// Mã hồ sơ/văn bản
        /// </summary>
        public string DocCode { get; set; }

        /// <summary>
        /// Phòng ban xử lý
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// Cán bộ xử lý
        /// </summary>
        public string StaffName { get; set; }

        /// <summary>
        /// Ngày hoàn thành
        /// </summary>
        public DateTime CompleteDate { get; set; }

        /// <summary>
        /// Tên công dân
        /// </summary>
        public string GivePerson { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Đã hoàn thành
        /// </summary>
        public bool IsCompleted { get; set; }
    }
}