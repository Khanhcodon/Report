using System;
using Bkav.eGovCloud.Entities.Enum;

namespace Bkav.eGovCloud.Models
{
    /// <summary>
    /// Các điều kiện lấy thống kê
    /// </summary>
    public class StatisticsCriteriaModel
    {
        public StatisticsCriteriaModel()
        {
            RecordPerPage = 50;
        }

        /// <summary>
        /// Xem theo
        /// </summary>
        public ReportViewType ViewType { get; set; }

        /// <summary>
        /// Thời điểm bắt đầu so sánh dữ liệu báo cáo
        /// </summary>
        public DateTime From { get; set; }

        /// <summary>
        /// Thời điểm kết thúc so sánh dữ liệu báo cáo
        /// </summary>
        public DateTime To { get; set; }

        /// <summary>
        /// Lấy tồn
        /// </summary>
        public bool HasOldDocument { get; set; }

        /// <summary>
        /// Trang
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Trang
        /// </summary>
        public int RecordPerPage { get; set; }

        /// <summary>
        /// Sắp xếp
        /// </summary>
        public string OrderBy { get; set; }

        /// <summary>
        /// Nhóm theo
        /// </summary>
        public string GroupBy { get; set; }

        /// <summary>
        /// Nhóm theo
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Sổ văn bản id
        /// </summary>
        public bool HasPaging { get; set; }

        /// <summary>
        /// Sổ văn bản id
        /// </summary>
        public int StoreId { get; set; }

        /// <summary>
        /// Sổ văn bản id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Đã xử lý
        /// </summary>
        public bool IsProcess { get; set; }

        public bool overdues { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsVanBanDen { get; set; }

        public string ParentName { get; set; }
    }
}