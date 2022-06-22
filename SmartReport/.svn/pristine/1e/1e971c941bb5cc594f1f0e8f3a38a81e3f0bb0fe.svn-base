
using System;

namespace Bkav.eGovCloud.Api.Controller
{
    public class ProgressStatisticResponse
    {
        /// <summary>
        /// Tên cơ quan, hoặc loại hồ sơ
        /// </summary>
        public string Name;

        /// <summary>
        /// Tồn kỳ trước
        /// </summary>
        public int PreExtisting;

        /// <summary>
        /// Nhận trong kỳ
        /// </summary>
        public int NewReception;

        /// <summary>
        /// Chưa xử lý
        /// </summary>
        public int TotalPending;

        /// <summary>
        /// Chưa xử lý đúng hạn
        /// </summary>
        public int Pending;

        /// <summary>
        /// Chưa xử lý quá hạn
        /// </summary>
        public int PendingLate;

        /// <summary>
        /// Phần trăm chưa xử lý quá hạn
        /// </summary>
        public string PendingLatePercent;

        /// <summary>
        /// Phần trăm chưa xử lý đúng hạn
        /// </summary>
        public string PendingPercent;

        /// <summary>
        /// Đã xử lý
        /// </summary>
        public int TotalSolved;

        /// <summary>
        /// Đã xử lý đúng hạn
        /// </summary>
        public int SolvedInTime;

        /// <summary>
        /// Phần trăm đã xử lý đúng hạn
        /// </summary>
        public string SolvedInTimePercent;

        /// <summary>
        /// Đã xử lý quá hạn
        /// </summary>
        public int SolvedLate;

        /// <summary>
        /// Phần trăm đã xử lý quá hạn
        /// </summary>
        public string SolvedLatePercent;
    }

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
            public int ViewType { get; set; }

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
            public int StoreId { get; set; }

            /// <summary>
            /// Đã xử lý
            /// </summary>
            public bool IsProcess { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public bool IsVanBanDen { get; set; }
        }
}
