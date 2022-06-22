using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Business.Objects
{
    /// <summary>
    /// Báo báo phat hành
    /// </summary>
    public class StatisticsCriteriaObject
    {
        /// <summary>
        /// 
        /// </summary>
        public StatisticsCriteriaObject()
        {
            RecordPerPage = 50;
        }

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

        /// <summary>
        /// 
        /// </summary>
        public bool IsVanBanDen { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsFirstLoad { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsGetAll { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? IsResponse { get; set; }
    }
}
