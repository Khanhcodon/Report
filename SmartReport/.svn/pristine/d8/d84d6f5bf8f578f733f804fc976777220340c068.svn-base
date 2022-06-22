using System;
namespace Bkav.eGovCloud.Entities.Admin.TimerJobSchedules
{
    /// <summary>
    /// Cấu hình lịch trình kích hoạt timer hàng tháng
    /// </summary>
    public class MonthlySchedule : ScheduleBase
    {
        /// <summary>
        /// Lấy hoặc thiết lập kiểu kích hoạt háng tháng: theo ngày trong tháng hoặc ngày trong tuần
        /// </summary>
        public ScheduleMonthlyType ByDayOfWeek { get; set; }

        #region Theo ngày trong tháng

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

        #endregion

        #region Theo ngày trong tuần

        /// <summary>
        /// Lấy hoặc thiết lập khoảng thời gian (theo ngày trong tuần) kích hoạt lần hoạt động tiếp theo (from)
        /// </summary>
        public DayOfWeek DayOfWeek { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập khoảng thời gian (theo tuần trong tháng) kích hoạt lần hoạt động tiếp theo (from)
        /// </summary>
        public int WeekOfMonth { get; set; }

        #endregion

        /// <summary>
        /// Trả về khoảng thời gian kích hoạt lần chạy tiếp theo.
        /// </summary>
        /// <param name="dateLastJobRun">Thời gian chạy gần nhất.</param>
        /// <returns></returns>
        public override NextStartTime GetNextStartTime(DateTime dateLastJobRun)
        {
            var result = new NextStartTime();

            if (ByDayOfWeek == ScheduleMonthlyType.Day)
            {
                var nextTimeSpan = new TimeSpan(ToDayOfMonth, ToHour, ToMinute, 0);
                var currentTimSpan = new TimeSpan(dateLastJobRun.Day, dateLastJobRun.Hour, dateLastJobRun.Minute, 0);

                // Nếu mốc chạy nhỏ hơn thời điểm dự kiến --> giữ nguyên bằng dự kiến
                var nextTime = currentTimSpan <= nextTimeSpan
                    ? dateLastJobRun : dateLastJobRun.AddMonths(1);

                var tsAfter = new TimeSpan(ToHour, ToMinute, 0);
                result.NextStartTimeAfter = nextTime.Date.AddDays(ToDayOfMonth - nextTime.Date.Day) + tsAfter;

                var tsBefore = new TimeSpan(FromHour, FromMinute, 0);
                result.NextStartTimeBefore = nextTime.Date.AddDays(FromDayOfMonth - nextTime.Date.Day) + tsBefore;
            }
            else
            {
                // Nếu mốc chạy nhỏ hơn thời điểm dự kiến --> giữ nguyên bằng dự kiến
                var startOnDayOfMonth = GetNthWeekofMonth(dateLastJobRun, WeekOfMonth, DayOfWeek);

                var nextTimeSpan = new TimeSpan(startOnDayOfMonth.Day, FromHour, FromMinute, 0);
                var currentTimSpan = new TimeSpan(dateLastJobRun.Day, dateLastJobRun.Hour, dateLastJobRun.Minute, 0);

                var nextTime = currentTimSpan <= nextTimeSpan
                    ? startOnDayOfMonth
                    : GetNthWeekofMonth(dateLastJobRun.AddMonths(1), WeekOfMonth, DayOfWeek);

                var tsOn = new TimeSpan(FromHour, FromMinute, 0);
                result.NextStartTimeAfter = nextTime + tsOn;
                result.NextStartTimeBefore = nextTime + new TimeSpan(23, 59, 59);
            }
            return result;
        }

        #region Private Methods

        private DateTime GetNthWeekofMonth(DateTime date, int nthWeek, DayOfWeek dayOfWeek)
        {
            DateTime firtDayOfMonth = FirstDayOfMonth(date);
            DateTime lastDayOfMonth = LastDayOfMonth(date);
            DateTime firtDayOfWeekInMonth = Next(firtDayOfMonth, dayOfWeek);

            int addDays = (nthWeek - 1) * 7;
            return lastDayOfMonth.Day >= firtDayOfWeekInMonth.Day + addDays
                       ? firtDayOfWeekInMonth.Date.AddDays(addDays)
                       : lastDayOfMonth.Date;
        }

        private DateTime FirstDayOfMonth(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }

        private DateTime LastDayOfMonth(DateTime dateTime)
        {
            var firstDayOfTheMonth = new DateTime(dateTime.Year, dateTime.Month, 1);
            return firstDayOfTheMonth.AddMonths(1).AddDays(-1);
        }

        private DateTime Next(DateTime date, DayOfWeek dayOfWeek)
        {
            return date.AddDays((dayOfWeek < date.DayOfWeek ? 7 : 0) + dayOfWeek - date.DayOfWeek);
        }

        #endregion
    }
}
