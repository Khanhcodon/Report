using System;

namespace Bkav.eGovCloud.Core.Worktime
{
    /// <author>
    /// Bkav Corp. - BSO - eGov
    /// Project: eGov Cloud v1.0
    /// Class : WorktimeUtil
    /// Access Modifiers: 
    /// Create Date : 191212
    /// Author      : CuongNT
    /// </author>
    /// <summary>
    /// <para>Ngày nghỉ làm việc trong năm</para>
    /// (CuongNT@bkav.com - 191212)
    /// </summary>
    public class Holiday
    {
        /// <summary>
        /// Ngày nghỉ làm là ngày lễ, bù. Khác ngày nghỉ làm cuối tuần.
        /// </summary>
        public DateTime? HolidayDate { get; set; }

        /// <summary>
        /// Trả về true nếu là ngày nghỉ lặp hàng năm
        /// </summary>
        public bool IsRepeat { get; set; }

        /// <summary>
        /// Trả về ngày nghỉ làm cuối tuần.
        /// </summary>
        public DayOfWeek? DayOfWeek { get; set; }

        /// <summary>
        /// Trả về true nếu là ngày nghỉ làm hàng tuần. False nếu là ngày nghỉ lễ và nghỉ bù.<para></para>
        /// </summary>
        public bool IsWeekend { get; set; }
    }
}
