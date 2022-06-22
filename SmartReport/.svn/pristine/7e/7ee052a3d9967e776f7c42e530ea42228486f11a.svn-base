using System;
namespace Bkav.eGovCloud.Entities.Admin.TimerJobSchedules
{
    /// <summary>
    /// Lich trình kích hoạt theo giờ
    /// </summary>
    public class HourlySchedule : ScheduleBase
    {
        /// <summary>
        /// Lấy hoặc thiết lập khoảng thời gian kích hoạt (from)
        /// </summary>
        public int FromMinute { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập khoảng thời gian kích hoạt (to)
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
            var nextTime = dateLastJobRun.Minute < FromMinute ? dateLastJobRun : dateLastJobRun.AddHours(1);

            var result = new NextStartTime();
            var tsAfter = new TimeSpan(nextTime.Hour, ToMinute, 0);
            result.NextStartTimeAfter = nextTime.Date + tsAfter;

            var tsBefore = new TimeSpan(nextTime.Hour, FromMinute, 0);
            result.NextStartTimeBefore = nextTime.Date + tsBefore;

            return result;
        }
    }
}
