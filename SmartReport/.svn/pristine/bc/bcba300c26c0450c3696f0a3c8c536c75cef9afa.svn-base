using System;
namespace Bkav.eGovCloud.Entities.Admin.TimerJobSchedules
{
    /// <summary>
    /// Cấu hình lịch trình kích hoạt timer hàng ngày
    /// </summary>
    public class DailySchedule : ScheduleBase
    {

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
        /// Lấy hoặc thiết lập khoảng thời gian (theo phút) kích hoạt lần hoạt động tiếp theo (from)
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
            var nextTimeSpan = new TimeSpan(ToHour, ToMinute, 0);
            var nextTime = dateLastJobRun.TimeOfDay <= nextTimeSpan
                ? dateLastJobRun : dateLastJobRun.AddDays(1);

            var result = new NextStartTime();

            var tsAfter = new TimeSpan(ToHour, ToMinute, 0);
            result.NextStartTimeAfter = nextTime.Date + tsAfter;

            var tsBefore = new TimeSpan(FromHour, FromMinute, 0);
            result.NextStartTimeBefore = nextTime.Date + tsBefore;

            return result;
        }
    }
}
