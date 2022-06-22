using System;
namespace Bkav.eGovCloud.Entities.Admin.TimerJobSchedules
{
    /// <summary>
    /// Kiếu kích hoạt hàng phút
    /// </summary>
    public class MinutesSchedule : ScheduleBase
    {
        /// <summary>
        /// Lấy hoặc thiết lập số phút định kỳ kích hoạt.
        /// </summary>
        public int Minutes { get; set; }

        #region Methods

        /// <summary>
        /// Trả về khoảng thời gian kích hoạt lần chạy tiếp theo.
        /// </summary>
        /// <param name="dateLastJobRun">Thời gian chạy gần nhất.</param>
        /// <returns></returns>
        public override NextStartTime GetNextStartTime(DateTime dateLastJobRun)
        {
            return new NextStartTime
            {
                NextStartTimeAfter = dateLastJobRun.AddMinutes(Minutes),
                NextStartTimeBefore = dateLastJobRun.AddMinutes(Minutes)
            };
        }

        #endregion
    }
}
