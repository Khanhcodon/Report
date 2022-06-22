using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Entities.Enum
{
    /// <summary>
    /// Danh sách các mức thời gian để các hồ sơ xử lý gần nhất
    /// </summary>
    public enum DailyProcessTimeForSearch
    {
        /// <summary>
        /// Cả ngày
        /// </summary>
        [Description("egovcloud.enum.dailyprocesstimeforsearch.allday")]
        AllDay = 0,

        /// <summary>
        /// Cách 30 phút
        /// </summary>
        [Description("egovcloud.enum.dailyprocesstimeforsearch.thirtyminutes")]
        ThirtyMinutes = 1,

        /// <summary>
        /// Cách 1 tiếng
        /// </summary>
        [Description("egovcloud.enum.dailyprocesstimeforsearch.onehour")]
        OneHour = 2,

        /// <summary>
        /// Cách 2 tiếng
        /// </summary>
        [Description("egovcloud.enum.dailyprocesstimeforsearch.twohour")]
        TwoHour = 3,

        /// <summary>
        /// Cả buổi sáng
        /// </summary>
        [Description("egovcloud.enum.dailyprocesstimeforsearch.am")]
        Am = 4,

        /// <summary>
        /// Cả buổi chiều
        /// </summary>
        [Description("egovcloud.enum.dailyprocesstimeforsearch.pm")]
        Pm = 5
    }
}
