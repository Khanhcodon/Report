using System;

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
    public static class LunarUtils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private static long Int(double d)
        {
            return (long)Math.Floor(d);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dd"></param>
        /// <param name="mm"></param>
        /// <param name="yy"></param>
        /// <returns></returns>
        private static long JdFromDate(int dd, int mm, int yy)
        {
            long a = Int((14 - mm) / 12);
            long y = yy + 4800 - a;
            long m = mm + 12 * a - 3;
            long jd = dd + Int((153 * m + 2) / 5) + 365 * y + Int(y / 4) - Int(y / 100) + Int(y / 400) - 32045;
            if (jd < 2299161)
            {
                jd = dd + Int((153 * m + 2) / 5) + 365 * y + Int(y / 4) - 32083;
            }
            return jd;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jd"></param>
        /// <returns></returns>
        private static DateTime JdToDate(long jd)
        {
            long a, b, c, d, e, m;
            if (jd > 2299160)
            { // After 5/10/1582, Gregorian calendar
                a = jd + 32044;
                b = Int((4 * a + 3) / 146097);
                c = a - Int((b * 146097) / 4);
            }
            else
            {
                b = 0;
                c = jd + 32082;
            }
            d = Int((4 * c + 3) / 1461);
            e = c - Int((1461 * d) / 4);
            m = Int((5 * e + 2) / 153);
            long day = e - Int((153 * m + 2) / 5) + 1;
            long month = m + 3 - 12 * Int(m / 10);
            long year = b * 100 + d - 4800 + Int(m / 10);
            return new DateTime((int)year, (int)month, (int)day);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="k"></param>
        /// <param name="timeZone"></param>
        /// <returns></returns>
        private static long GetNewMoonDay(long k, long timeZone)
        {
            double T = k / 1236.85; // Time in Julian centuries from 1900 January 0.5
            double T2 = T * T;
            double T3 = T2 * T;
            double dr = Math.PI / 180;
            double Jd1 = 2415020.75933 + 29.53058868 * k + 0.0001178 * T2 - 0.000000155 * T3;
            Jd1 = Jd1 + 0.00033 * Math.Sin((166.56 + 132.87 * T - 0.009173 * T2) * dr); // Mean new moon
            double M = 359.2242 + 29.10535608 * k - 0.0000333 * T2 - 0.00000347 * T3; // Sun's mean anomaly
            double Mpr = 306.0253 + 385.81691806 * k + 0.0107306 * T2 + 0.00001236 * T3; // Moon's mean anomaly
            double F = 21.2964 + 390.67050646 * k - 0.0016528 * T2 - 0.00000239 * T3; // Moon's argument of latitude
            double C1 = (0.1734 - 0.000393 * T) * Math.Sin(M * dr) + 0.0021 * Math.Sin(2 * dr * M);
            C1 = C1 - 0.4068 * Math.Sin(Mpr * dr) + 0.0161 * Math.Sin(dr * 2 * Mpr);
            C1 = C1 - 0.0004 * Math.Sin(dr * 3 * Mpr);
            C1 = C1 + 0.0104 * Math.Sin(dr * 2 * F) - 0.0051 * Math.Sin(dr * (M + Mpr));
            C1 = C1 - 0.0074 * Math.Sin(dr * (M - Mpr)) + 0.0004 * Math.Sin(dr * (2 * F + M));
            C1 = C1 - 0.0004 * Math.Sin(dr * (2 * F - M)) - 0.0006 * Math.Sin(dr * (2 * F + Mpr));
            C1 = C1 + 0.0010 * Math.Sin(dr * (2 * F - Mpr)) + 0.0005 * Math.Sin(dr * (2 * Mpr + M));
            double deltat = 0;
            if (T < -11)
            {
                deltat = 0.001 + 0.000839 * T + 0.0002261 * T2 - 0.00000845 * T3 - 0.000000081 * T * T3;
            }
            else
            {
                deltat = -0.000278 + 0.000265 * T + 0.000262 * T2;
            }
            double JdNew = Jd1 + C1 - deltat;
            return Int(JdNew + 0.5 + (double)((double)timeZone / 24));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jdn"></param>
        /// <param name="timeZone"></param>
        /// <returns></returns>
        private static long GetSunLongitude(long jdn, int timeZone)
        {
            double T = (jdn - 2451545.5 - timeZone / 24) / 36525; // Time in Julian centuries from 2000-01-01 12:00:00 GMT
            double T2 = T * T;
            double dr = Math.PI / 180; // degree to radian
            double M = 357.52910 + 35999.05030 * T - 0.0001559 * T2 - 0.00000048 * T * T2; // mean anomaly, degree
            double L0 = 280.46645 + 36000.76983 * T + 0.0003032 * T2; // mean longitude, degree
            double DL = (1.914600 - 0.004817 * T - 0.000014 * T2) * Math.Sin(dr * M);
            DL = DL + (0.019993 - 0.000101 * T) * Math.Sin(dr * 2 * M) + 0.000290 * Math.Sin(dr * 3 * M);
            double L = L0 + DL; // true longitude, degree
            // obtain apparent longitude by correcting for nutation and aberration
            double omega = 125.04 - 1934.136 * T;
            L = L - 0.00569 - 0.00478 * Math.Sin(omega * dr);
            L = L * dr;
            L = L - Math.PI * 2 * (Int(L / (Math.PI * 2))); // Normalize to (0, 2*PI)
            return Int(L / Math.PI * 6);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="yy"></param>
        /// <param name="timeZone"></param>
        /// <returns></returns>
        private static long GetLunarMonth11(int yy, int timeZone)
        {
            long off = JdFromDate(31, 12, yy) - 2415021;
            long k = Int(off / 29.530588853);
            long nm = GetNewMoonDay(k, timeZone);
            long sunLong = GetSunLongitude(nm, timeZone); // sun longitude at local midnight
            if (sunLong >= 9)
            {
                nm = GetNewMoonDay(k - 1, timeZone);
            }
            return nm;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a11"></param>
        /// <param name="timeZone"></param>
        /// <returns></returns>
        private static int GetLeapMonthOffset(long a11, int timeZone)
        {
            long k = Int((a11 - 2415021.076998695) / 29.530588853 + 0.5);
            long last = 0;
            int i = 1; // We start with the month following lunar month 11
            long arc = GetSunLongitude(GetNewMoonDay(k + i, timeZone), timeZone);
            do
            {
                last = arc;
                i = i + 1;
                arc = GetSunLongitude(GetNewMoonDay(k + i, timeZone), timeZone);
            } while (arc != last && i < 14);
            return i - 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtm"></param>
        /// <returns></returns>
        public static LunarDate Solar2Lunar(DateTime dtm)
        {
            return ConvertSolar2Lunar(dtm.Day, dtm.Month, dtm.Year, 7);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime Lunar2Solar(LunarDate date)
        {
            var dtm = ConvertLunar2Solar(date.Day, date.Month, date.Year, 1, 7);
            if (dtm == DateTime.MinValue)
                dtm = ConvertLunar2Solar(date.Day, date.Month, date.Year, 0, 7);
            return dtm;
        }

        /* Comvert solar date dd/mm/yyyy to the corresponding lunar date */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dd"></param>
        /// <param name="mm"></param>
        /// <param name="yy"></param>
        /// <param name="timeZone"></param>
        /// <returns></returns>
        private static LunarDate ConvertSolar2Lunar(int dd, int mm, int yy, int timeZone)
        {
            long dayNumber = JdFromDate(dd, mm, yy);
            long k = Int((dayNumber - 2415021.076998695) / 29.530588853);
            long monthStart = GetNewMoonDay(k + 1, timeZone);
            if (monthStart > dayNumber)
            {
                monthStart = GetNewMoonDay(k, timeZone);
            }
            long a11 = GetLunarMonth11(yy, timeZone);
            long b11 = a11;
            int lunarYear;
            if (a11 >= monthStart)
            {
                lunarYear = yy;
                a11 = GetLunarMonth11(yy - 1, timeZone);
            }
            else
            {
                lunarYear = yy + 1;
                b11 = GetLunarMonth11(yy + 1, timeZone);
            }
            long lunarDay = dayNumber - monthStart + 1;
            long diff = Int((monthStart - a11) / 29);
            bool lunarLeap = false;
            long lunarMonth = diff + 11;
            if (b11 - a11 > 365)
            {
                int leapMonthDiff = GetLeapMonthOffset(a11, timeZone);
                if (diff >= leapMonthDiff)
                {
                    lunarMonth = diff + 10;
                    if (diff == leapMonthDiff)
                    {
                        lunarLeap = true;
                    }
                }
            }
            if (lunarMonth > 12)
            {
                lunarMonth = lunarMonth - 12;
            }
            if (lunarMonth >= 11 && diff < 4)
            {
                lunarYear -= 1;
            }

            return new LunarDate((int)lunarDay, (int)lunarMonth, (int)lunarYear, lunarLeap);

            //return new int[] { (int)lunarDay, (int)lunarMonth, (int)lunarYear, lunarLeap };
        }

        /* Convert a lunar date to the corresponding solar date */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lunarDay"></param>
        /// <param name="lunarMonth"></param>
        /// <param name="lunarYear"></param>
        /// <param name="lunarLeap"></param>
        /// <param name="timeZone"></param>
        /// <returns></returns>
        private static DateTime ConvertLunar2Solar(int lunarDay, int lunarMonth, int lunarYear, int lunarLeap, int timeZone)
        {
            long a11, b11, leapOff, leapMonth;
            if (lunarMonth < 11)
            {
                a11 = GetLunarMonth11(lunarYear - 1, timeZone);
                b11 = GetLunarMonth11(lunarYear, timeZone);
            }
            else
            {
                a11 = GetLunarMonth11(lunarYear, timeZone);
                b11 = GetLunarMonth11(lunarYear + 1, timeZone);
            }
            long k = Int(0.5 + (a11 - 2415021.076998695) / 29.530588853);
            int off = lunarMonth - 11;
            if (off < 0)
            {
                off += 12;
            }
            if (b11 - a11 > 365)
            {
                leapOff = GetLeapMonthOffset(a11, timeZone);
                leapMonth = leapOff - 2;
                if (leapMonth < 0)
                {
                    leapMonth += 12;
                }
                if (lunarLeap != 0 && lunarMonth != leapMonth)
                {
                    return DateTime.MinValue;
                }
                else if (lunarLeap != 0 || off >= leapOff)
                {
                    off += 1;
                }
            }
            long monthStart = GetNewMoonDay(k + off, timeZone);
            return JdToDate(monthStart + lunarDay - 1);
        }
    }
}