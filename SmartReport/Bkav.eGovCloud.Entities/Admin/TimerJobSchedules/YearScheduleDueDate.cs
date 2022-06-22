using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Entities.Admin.TimerJobSchedules
{
    /// <summary>
    /// 
    /// </summary>
    public class YearScheduleDueDate : ScheduleBaseDueDate
    {
        /// <summary>
        /// Lấy hoặc thiết lập khoảng thời gian (theo tháng trong năm) kích hoạt lần hoạt động tiếp theo (from)
        /// </summary>
        public int FromMonth { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập khoảng thời gian (theo tháng trong năm) kích hoạt lần hoạt động tiếp theo (to)
        /// </summary>
        public int ToMonth { get; set; }

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
            var next = new DateTime(dateLastJobRun.Year, ToMonth, ToDayOfMonth, ToHour, ToMinute, 0);

            // Nếu mốc chạy nhỏ hơn thời điểm dự kiến -->  dự kiến + 1(năm sau)
            var nextTime = next <= dateLastJobRun ? next.AddYears(1) : next;

            result.NextStartTimeAfter = nextTime;
            result.NextStartTimeBefore = new DateTime(nextTime.Year, FromMonth, FromDayOfMonth, FromHour, FromMinute, 0);

            return result;
        }
    }
}
