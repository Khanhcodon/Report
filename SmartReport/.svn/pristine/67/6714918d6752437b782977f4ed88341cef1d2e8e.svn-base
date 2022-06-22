using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Entities
{
    /// <summary>
    /// Dữ liệu thống kê
    /// </summary>
    public class ProgressStatistic
    {
        /// <summary>
        /// Tên cơ quan, hoặc loại hồ sơ
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Tồn kỳ trước
        /// </summary>
        public int PreExtisting { get; set; }

        /// <summary>
        /// Nhận trong kỳ
        /// </summary>
        public int NewReception { get; set; }

        /// <summary>
        /// Tổng xử lý
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// Chưa xử lý
        /// </summary>
        public int TotalPending { get; set; }

        /// <summary>
        /// Chưa xử lý đúng hạn
        /// </summary>
        public int Pending { get; set; }

        /// <summary>
        /// Chưa xử lý quá hạn
        /// </summary>
        public int PendingLate { get; set; }

        /// <summary>
        /// Phần trăm chưa xử lý quá hạn
        /// </summary>
        public double PendingLatePercent
        {
            get
            {
                if (TotalPending == 0)
                {
                    return 0;
                }

                double percent = (PendingLate * 100.0) / TotalPending;
                return Math.Round(percent, 1);
            }
        }

        /// <summary>
        /// Phần trăm chưa xử lý đúng hạn
        /// </summary>
        public double PendingPercent
        {
            get
            {
                if (TotalPending == 0)
                {
                    return 0;
                }

                return 100.0 - PendingLatePercent;
            }
        }

        /// <summary>
        /// Đã xử lý
        /// </summary>
        public int TotalSolved { get; set; }

        /// <summary>
        /// Đã xử lý đúng hạn
        /// </summary>
        public int SolvedInTime { get; set; }

        /// <summary>
        /// Phần trăm đã xử lý đúng hạn
        /// </summary>
        public double SolvedInTimePercent
        {
            get
            {
                if (TotalSolved == 0)
                {
                    return 0;
                }

                double percent = (SolvedInTime * 100.0) / TotalSolved;
                return Math.Round(percent, 1);
            }
        }

        /// <summary>
        /// Đã xử lý quá hạn
        /// </summary>
        public int SolvedLate { get; set; }

        /// <summary>
        /// Phần trăm đã xử lý quá hạn
        /// </summary>
        public double SolvedLatePercent {
            get
            {
                if (TotalSolved == 0)
                {
                    return 0;
                }

                return 100.0 - SolvedInTimePercent;
            }
        }

        /// <summary>
        /// Đã xử lý quá hạn
        /// </summary>
        public int QuaHanVPUB { get; set; }

        /// <summary>
        /// Đã xử lý quá hạn
        /// </summary>
        public int TreHenVPUB { get; set; }
    }
}
