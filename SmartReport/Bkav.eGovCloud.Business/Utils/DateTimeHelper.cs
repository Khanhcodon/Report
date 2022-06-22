using System;
using System.Globalization;

namespace Bkav.eGovCloud.Business
{
    /// <summary>
    /// 
    /// </summary>
    public static class DateTimeHelper
    {
        /// <summary>
        /// Offsets to move the day of the year on a week, allowing
        /// for the current year Jan 1st day of week, and the Sun/Mon 
        /// week start difference between ISO 8601 and Microsoft
        /// </summary>
        private static int[] moveByDays = { 6, 7, 8, 9, 10, 4, 5 };

        /// <summary>
        /// Get the Week number of the year
        /// (In the range 1..53)
        /// This conforms to ISO 8601 specification for week number.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns>Week of year</returns>
        public static int WeekOfYear(this DateTime dateTime)
        {
            var startOfYear = new DateTime(dateTime.Year, 1, 1);
            var endOfYear = new DateTime(dateTime.Year, 12, 31);
            // ISO 8601 weeks start with Monday 
            // The first week of a year includes the first Thursday 
            // This means that Jan 1st could be in week 51, 52, or 53 of the previous year...
            int numberDays = dateTime.Subtract(startOfYear).Days +
                            moveByDays[(int)startOfYear.DayOfWeek];
            int weekNumber = numberDays / 7;
            switch (weekNumber)
            {
                case 0:
                    // Before start of first week of this year - in last week of previous year
                    weekNumber = WeekOfYear(startOfYear.AddDays(-1));
                    break;
                case 53:
                    // In first week of next year.
                    if (endOfYear.DayOfWeek < DayOfWeek.Thursday)
                    {
                        weekNumber = 1;
                    }
                    break;
            }

            return weekNumber;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static int GetIso8601WeekOfYear(this DateTime dateTime)
        {
            var day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(dateTime);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                dateTime = dateTime.AddDays(3);
            }

            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(dateTime, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
    }
}