using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Core.Utils
{
    /// <author>
    /// Bkav Corp. - BSO - eGov
    /// Project: eGov Cloud v1.0
    /// Class : DateTimeExtension - public - Core
    /// Access Modifiers: 
    /// Create Date : 26/02/2015
    /// Author      : TienBV
    /// </author>
    /// <summary>
    /// <para>1 thư viện ở rộng cho việc xử lý datetime</para>
    /// </summary>
    public static class DateTimeExtension
    {
        /// <summary>
        /// Parse Exact from str
        /// </summary>
        /// <param name="dateStr"></param>
        /// <returns></returns>
        public static DateTime parseUtcDateTime(string dateStr)
        {
            var partern = "MM/dd/yyyy HH:mm:ss 'UTC' K";
            return ParseExactDateTime(dateStr, partern);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateStr"></param>
        /// <returns></returns>
        public static DateTime ParseGmtDateTime(string dateStr)
        {
            var partern = "ddd MMM dd yyyy HH:mm:ss 'GMT'K";
            return ParseExactDateTime(dateStr, partern);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateStr"></param>
        /// <returns></returns>
        public static DateTime ParseIsoDateTime(string dateStr)
        {
            var partern = @"yyyy-MM-dd\THH:mm:ss.SSS\Z";
            return ParseExactDateTime(dateStr, partern, DateTimeStyles.RoundtripKind);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateStr"></param>
        /// <returns></returns>
        public static DateTime ParseFromClient(string dateStr)
        {
            return parseUtcDateTime(dateStr);
        }

        /// <summary>
        /// Trả về quý
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int Quarter(this DateTime date)
        {
            var result = Math.Floor((double)((date.Month - 1) / 3 + 1));
            return (int)result;
        }

        /// <summary>
        /// Thiết lập ngày hiện tại thành thời điểm đầu tiên của đơn vị truyền vào và trả về ngày mới tương ứng.
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        /// <remarks>
        /// Đơn vị:
        /// - "year": thiết lập ngày hiện tại về ngày đầu tiên trong năm với ngày hiện tại.
        /// - "quarter": thiết lập ngày hiện tại về ngày đầu tiên của quý với ngày hiện tại
        /// - "month": thiết lập ngày hiện tại về ngày đầu tiên trong tháng với ngày hiện tại.
        /// - "week": thiết lập ngày hiện tại về ngày đầu tiên trong tuần với ngày hiện tại.
        /// - "day": thiết lập ngày hiện tại về thời điểm 12:00:00 am của ngày hiện tại.
        /// - "hour": thiết lập ngày hiện tại về thời điểm 00:00 của giờ hiện tại.
        /// - "minute": thiết lập ngày hiện tại về thời điểm 00s của phút hiện tại.
        /// - "second": thiết lập ngày hiện tại về thời điểm 000ms của giây hiện tại.
        /// </remarks>
        public static DateTime StartOf(this DateTime date, DateTimeUnit unit)
        {
            if (date == null)
            {
                throw new ArgumentNullException("date");
            }

            DateTime result;

            var year = date.Year;
            var month = date.Month;
            var day = date.Day;
            var hour = date.Hour;
            var minute = date.Minute;
            var second = date.Second;
            var quarter = date.Quarter();

            switch (unit)
            {
                case DateTimeUnit.Year:
                    result = new DateTime(year, 1, 1);
                    break;
                case DateTimeUnit.Quarter:
                    month = quarter * 3 - 2;
                    result = new DateTime(year, month, 1);
                    break;
                case DateTimeUnit.Month:
                    result = new DateTime(year, month, 1);
                    break;
                case DateTimeUnit.Week:
                    var fdayOfWeek = date.Subtract(TimeSpan.FromDays((int)date.DayOfWeek));
                    result = new DateTime(fdayOfWeek.Year, fdayOfWeek.Month, fdayOfWeek.Day);
                    break;
                case DateTimeUnit.Day:
                    result = new DateTime(year, month, day);
                    break;
                case DateTimeUnit.Hour:
                    result = new DateTime(year, month, day, hour, 0, 0);
                    break;
                case DateTimeUnit.Minute:
                    result = new DateTime(year, month, day, hour, minute, 0);
                    break;
                case DateTimeUnit.Second:
                    result = date;
                    break;
                default:
                    result = date;
                    break;
            }

            return result;
        }

        /// <summary>
        /// Thiết lập ngày hiện tại thành thời điểm cuối cùng của đơn vị truyền vào và trả về ngày mới tương ứng.
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        /// <remarks>
        /// Đơn vị:
        /// - "year": thiết lập ngày hiện tại về ngày cuối cùng trong năm với ngày hiện tại.
        /// - "quarter": thiết lập ngày hiện tại về ngày cuối cùng của quý với ngày hiện tại
        /// - "month": thiết lập ngày hiện tại về ngày cuối cùng trong tháng với ngày hiện tại.
        /// - "week": thiết lập ngày hiện tại về ngày cuối cùng trong tuần với ngày hiện tại.
        /// - "day": thiết lập ngày hiện tại về thời điểm 23:59:59 của ngày hiện tại.
        /// - "hour": thiết lập ngày hiện tại về thời điểm 59:59 của giờ hiện tại.
        /// - "minute": thiết lập ngày hiện tại về thời điểm 59s của phút hiện tại.
        /// - "second": thiết lập ngày hiện tại về thời điểm 999ms của giây hiện tại.
        /// </remarks>
        public static DateTime EndOf(this DateTime date, DateTimeUnit unit)
        {
            if (date == null)
            {
                throw new ArgumentNullException("date");
            }

            DateTime result;
            int days;

            var year = date.Year;
            var month = date.Month;
            var day = date.Day;
            var hour = date.Hour;
            var minute = date.Minute;
            var second = date.Second;
            var quarter = date.Quarter();

            switch (unit)
            {
                case DateTimeUnit.Year:
                    result = new DateTime(year, 12, 31);
                    break;
                case DateTimeUnit.Quarter:
                    month = quarter * 3 - 2;
                    days = DateTime.DaysInMonth(year, month);
                    result = new DateTime(year, month, days, 23, 59, 59);
                    break;
                case DateTimeUnit.Month:
                    days = DateTime.DaysInMonth(year, month);
                    result = new DateTime(year, month, days, 23, 59, 59);
                    break;
                case DateTimeUnit.Week:
                    var ldayOfWeek = date.Subtract(TimeSpan.FromDays((int)date.DayOfWeek)).AddDays(6);
                    result = new DateTime(ldayOfWeek.Year, ldayOfWeek.Month, ldayOfWeek.Day, 23, 59, 59);
                    break;
                case DateTimeUnit.Day:
                    result = new DateTime(year, month, day, 23, 59, 59);
                    break;
                case DateTimeUnit.Hour:
                    result = new DateTime(year, month, day, hour, 59, 59);
                    break;
                case DateTimeUnit.Minute:
                    result = new DateTime(year, month, day, hour, minute, 59);
                    break;
                case DateTimeUnit.Second:
                    result = date;
                    break;
                default:
                    result = date;
                    break;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateStr"></param>
        /// <param name="partern"></param>
        /// <param name="dateTimeStyles"></param>
        /// <returns></returns>
        public static DateTime ParseExactDateTime(string dateStr, string partern, DateTimeStyles dateTimeStyles = DateTimeStyles.None)
        {
            return DateTime.ParseExact(dateStr, partern, CultureInfo.InvariantCulture, dateTimeStyles);
        }

        /// <summary>
        /// Trả về tên thứ trong tuần
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string DayOfWeekName(this DateTime date)
        {
            var dayOfWeek = date.DayOfWeek;
            var result = "";

            switch (dayOfWeek)
            {
                case DayOfWeek.Monday:
                    result = "Thứ Hai";
                    break;
                case DayOfWeek.Tuesday:
                    result = "Thứ Ba";
                    break;
                case DayOfWeek.Wednesday:
                    result = "Thứ Tư";
                    break;
                case DayOfWeek.Thursday:
                    result = "Thứ Năm";
                    break;
                case DayOfWeek.Friday:
                    result = "Thứ Sáu";
                    break;
                case DayOfWeek.Saturday:
                    result = "Thứ Bảy";
                    break;
                case DayOfWeek.Sunday:
                    result = "Chủ nhật";
                    break;
            }

            return result;
        }

        /// <summary>
        /// Trả về tên buổi trong ngày
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string DayOfDayName(this DateTime date)
        {
            var hour = date.Hour;
            var result = hour >= 12 ? "Chiều" : "Sáng";
            return result;
        }

        /// <summary>
        /// Trả về thời gian tương đối so với ngày hiện tại.
        /// </summary>
        /// <returns></returns>
        public static string ToAbsoluteDate(this DateTime input)
        {
            var now = DateTime.Now;
            var seconds = (now - input).TotalSeconds;
            var interval = Math.Floor(seconds / 31536000);

            if (interval > 1)
            {
                return interval + " năm trước";
            }

            interval = Math.Floor(seconds / 2592000);
            if (interval > 1)
            {
                return interval + " tháng trước";
            }

            interval = Math.Floor(seconds / 604800);
            if (interval > 1)
            {
                return interval + " tuần trước";
            }

            interval = Math.Floor(seconds / 86400);
            if (interval > 1)
            {
                return interval + " ngày trước";
            }

            interval = Math.Floor(seconds / 3600);
            if (interval > 1)
            {
                return interval + " giờ trước";
            }

            interval = Math.Floor(seconds / 60);
            if (interval > 1)
            {
                return interval + " phút trước";
            }

            return "Vừa mới";
        }
    }

    /// <summary>
    /// Các đơn vị thời gian
    /// </summary>
    public enum DateTimeUnit
    {
        /// <summary>
        /// Năm
        /// </summary>
        Year,

        /// <summary>
        /// Quý
        /// </summary>
        Quarter,

        /// <summary>
        /// Tháng
        /// </summary>
        Month,

        /// <summary>
        /// Tuần
        /// </summary>
        Week,

        /// <summary>
        /// Ngày trong tháng
        /// </summary>
        Day,

        /// <summary>
        /// Giờ
        /// </summary>
        Hour,

        /// <summary>
        /// Phút
        /// </summary>
        Minute,

        /// <summary>
        /// Giây
        /// </summary>
        Second
    }
}
