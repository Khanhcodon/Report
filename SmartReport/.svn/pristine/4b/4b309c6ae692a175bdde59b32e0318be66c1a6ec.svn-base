using System;
using System.Globalization;

namespace Bkav.eGovCloud.Core.Lunar
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : LunarYearUtils - public - Core
    /// Access Modifiers: 
    /// Create Date : 030713
    /// Author      : GiangPN
    /// </author>
    /// <summary> 
    /// <para>1 thư viện ở rộng cho việc xử lý lịch âm dương</para>
    /// (GiangPN@bkav.com - 030713)
    /// </summary>
    [Serializable]
    public class LunarDate
    {
        /// <summary>
        /// 
        /// </summary>
        public int Day { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsLeapYear { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public LunarDate() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="day"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <param name="leap"></param>
        public LunarDate(int day, int month, int year, bool leap)
        {
            Day = day;
            Month = month;
            Year = year;
            IsLeapYear = leap;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return Day.ToString("00") + "/" + Month.ToString("00") + "/" + Year.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeZone"></param>
        /// <returns></returns>
        public DateTime ToSolarDate(int timeZone)
        {
            return LunarUtils.Lunar2Solar(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DateTime ToSolarDate()
        {
            return ToSolarDate(7);
        }
    }
}
