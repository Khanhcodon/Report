using System;
using Bkav.eGovCloud.Entities.Enum;
namespace Bkav.eGovCloud.Entities.Admin.TimerJobSchedules
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class ScheduleBaseOutOfDate
    {
        /// <summary>
        /// Kiểu kích hoạt lịch trình
        /// </summary>
        public DocTypeScheduleTypeOutOfDate Type { get; set; }

        /// <summary>
        /// Trả về mốc thời gian thực thi tiếp theo sau khi qua mốc này.
        /// </summary>
        /// <param name="dateLastJobRun"></param>
        /// <returns></returns>
        public abstract NextStartTime GetNextStartTime(DateTime dateLastJobRun);
    }
}
