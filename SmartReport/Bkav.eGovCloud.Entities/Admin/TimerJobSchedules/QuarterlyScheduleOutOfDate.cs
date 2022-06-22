using Bkav.eGovCloud.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Entities.Admin.TimerJobSchedules
{
    /// <summary>
    /// 
    /// </summary>
    public class QuarterlyScheduleOutOfDate: ScheduleBaseOutOfDate
    {
        /// <summary>
        /// Kiểu kích hoạt lịch trình
        /// </summary>
        public new DocTypeScheduleTypeOutOfDate Type { get; set; }
        /// <summary>
        /// Lấy hoặc thiết lập khoảng thời gian (theo tháng trong quý) kích hoạt lần hoạt động tiếp theo (from)
        /// </summary>
        public int MonthOfQuarter { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập khoảng thời gian (theo ngày trong tháng) kích hoạt lần hoạt động tiếp theo (from)
        /// </summary>
        public int FromDayOfMonth { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập khoảng thời gian (theo ngày trong tháng) kích hoạt lần hoạt động tiếp theo (to)
        /// </summary>
        public int ToDayOfMonth { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập khoảng thời gian (theo giờ) kích hoạt lần hoạt động tiếp theo (from)
        /// </summary>
        public int FromHour { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập khoảng thời gian (theo giờ) kích hoạt lần hoạt động tiếp theo (to)
        /// </summary>
        public int ToHour { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập khoảng thời gian (theo phút) kích hoạt lần hoạt động tiếp theo (from)
        /// </summary>
        public int FromMinute { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập khoảng thời gian (theo phút) kích hoạt lần hoạt động tiếp theo (to)
        /// </summary>
        public int ToMinute { get; set; }

        /// <summary>
        /// Trả về khoảng thời gian kích hoạt lần chạy tiếp theo.
        /// </summary>
        /// <param name="dateLastJobRun">Thời gian chạy gần nhất.</param>
        /// <returns></returns>
        public override NextStartTime GetNextStartTime(DateTime dateLastJobRun)
        {
            var result = new NextStartTime();
            // TODO
            return result;
        }
    }
}
