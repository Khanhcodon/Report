using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Entities.Admin.TimerJobSchedules
{
    /// <summary>
    /// 
    /// </summary>
    public class WeeklyScheduleOutOfDate: ScheduleBaseOutOfDate
    {
        /// <summary>
        /// Lấy hoặc thiết lập khoảng thời gian (theo ngày trong tuần) kích hoạt lần hoạt động tiếp theo (from)
        /// </summary>
        public DayOfWeek FromDayOfWeek { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập khoảng thời gian (theo ngày trong tuần) kích hoạt lần hoạt động tiếp theo (to)
        /// </summary>
        public DayOfWeek ToDayOfWeek { get; set; }

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
            // Nếu mốc chạy nhỏ hơn thời điểm dự kiến --> giữ nguyên bằng dự kiến
            var nextTimeSpan = new TimeSpan((int)ToDayOfWeek, ToHour, ToMinute, 0);
            var currentTimSpan = new TimeSpan((int)dateLastJobRun.DayOfWeek, dateLastJobRun.Hour, dateLastJobRun.Minute, 0);
            var nextTime = currentTimSpan <= nextTimeSpan ? dateLastJobRun : dateLastJobRun.AddDays(7);

            var result = new NextStartTime();

            var tsAfter = new TimeSpan(ToHour, ToMinute, 0);
            result.NextStartTimeAfter = nextTime.Date.AddDays(ToDayOfWeek - nextTime.Date.DayOfWeek) + tsAfter;

            var tsBefore = new TimeSpan(FromHour, FromMinute, 0);
            result.NextStartTimeBefore = nextTime.Date.AddDays(FromDayOfWeek - nextTime.Date.DayOfWeek) + tsBefore;

            return result;
        }
    }
}
