using System;
using System.Collections.Generic;
using System.Linq;

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
    /// <para>Cung cấp các hàm dùng để xử lý thời gian làm việc trên hệ thống eGov cloud.</para>
    /// (CuongNT@bkav.com - 191212)
    /// </summary>
    public class WorktimeUtil
    {
        /// <summary>
        /// CuongNT - 191212.
        /// Trả về số phút làm việc trong khoảng thời gian
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="worktimeOfDay">Các mốc thời điểm làm việc trong ngày</param>
        /// <param name="holidays"> </param>
        /// <returns></returns>
        public static int GetWorkminutes(DateTime fromDate, DateTime toDate, WorktimeOfDay worktimeOfDay, List<Holiday> holidays)
        {
            var minuteSubtractAM = GetMinuteSubtract(worktimeOfDay.AMStartWorking, worktimeOfDay.AMEndWorking);
            var minuteSubtractPM = GetMinuteSubtract(worktimeOfDay.PMStartWorking, worktimeOfDay.PMEndWorking);

            var numberMinutes = 0;
            var numberMinutes2 = 0;

            // Tính số giờ sử dụng buổi xử lý đầu tiên 
            //--> Buổi sáng
            if (fromDate.Hour < worktimeOfDay.AMEndWorking.Hour
                || (fromDate.Hour == worktimeOfDay.AMEndWorking.Hour
                && fromDate.Minute < worktimeOfDay.AMEndWorking.Minute))
            {
                numberMinutes = GetMinuteSubtract(fromDate.Hour, fromDate.Minute, worktimeOfDay.AMEndWorking.Hour, worktimeOfDay.AMEndWorking.Minute) + minuteSubtractPM;
            }

            // --> Buổi chiều
            else if (fromDate.Hour < worktimeOfDay.PMEndWorking.Hour || (fromDate.Hour == worktimeOfDay.PMEndWorking.Hour && fromDate.Minute < worktimeOfDay.PMEndWorking.Minute))
            {
                numberMinutes = GetMinuteSubtract(fromDate.Hour, fromDate.Minute, worktimeOfDay.PMEndWorking.Hour, worktimeOfDay.PMEndWorking.Minute);
            }

            // Tính số giờ sử dụng buổi xử lý cuối cùng
            // --> Buổi sáng
            if (toDate.Hour > worktimeOfDay.PMStartWorking.Hour
                || (toDate.Hour == worktimeOfDay.PMStartWorking.Hour
                && toDate.Minute > worktimeOfDay.PMStartWorking.Minute))
            {
                numberMinutes2 = GetMinuteSubtract(worktimeOfDay.PMStartWorking.Hour, worktimeOfDay.PMStartWorking.Minute, toDate.Hour, toDate.Minute) + minuteSubtractAM;
            }
            // --> Buổi chiều
            else if (toDate.Hour > worktimeOfDay.AMStartWorking.Hour || (toDate.Hour == worktimeOfDay.AMStartWorking.Hour && toDate.Minute > worktimeOfDay.AMStartWorking.Minute))
            {
                numberMinutes2 = GetMinuteSubtract(worktimeOfDay.AMStartWorking.Hour, worktimeOfDay.AMStartWorking.Minute, toDate.Hour, toDate.Minute);
            }

            // Lấy số ngày làm việc trong khoảng thời gian
            var subDay = GetWorkdays(fromDate, toDate, holidays);
            var totalTime = (subDay - 1) * (minuteSubtractPM + minuteSubtractAM) + numberMinutes + numberMinutes2;

            // Kiểm tra ngày cuối cùng (toDate) có phải là ngày nghỉ không. Nếu đúng thì trừ đi numHour2
            if (IsWeekendOrHoliday(toDate, holidays))
            {
                totalTime = totalTime - numberMinutes2;
            }
            if (totalTime < 0)
            {
                throw new ApplicationException("Lỗi tính thời gian xử lý hồ sơ::TimelineCtl::GetHourForWorking");
            }
            return totalTime;
        }

        /// <summary>
        /// Trả về ngày tiếp theo sau khi công thêm số ngày làm việc
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="workDays"></param>
        /// <param name="worktimeOfDay"> </param>
        /// <param name="holidays"></param>
        /// <param name="dateWorkOffset"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <remarks>
        /// Với tính ngày hẹn trả: 
        /// - Với hồ sơ giải quyết trong ngày (số ngày = 1): 
        ///   Nếu TN trước 3h chiều (TimePM) thì hẹn trả vào cuối ngày đó.
        ///   Nếu TN sau 3h chiều (TimePM)  thì hẹ trả vào sáng ngày hôm sau (TimeAM)
        /// - Hồ sơ có ngày giải quyết > 1: thì tiếp nhận ngày nào thì bắt đầu tính thời gian từ ngày đó.
        /// - Phần tính lại tg khi bổ sung cũng tương tự.
        /// </remarks>
        public static DateTime GetDateAppoint(DateTime startTime, int workDays, WorktimeOfDay worktimeOfDay, List<Holiday> holidays, List<Holiday> dateWorkOffset, WorktimeType type)
        {
            // Xử lý cho phép tính hạn nửa ngày
            if (workDays < 0)
            {
                throw new Exception("workDays phải lớn hơn hoặc bằng 0");
            }

            var addDays = workDays; // startTime.Hour >= 15 ? workDays : workDays - 1;
            DateTime result;
            //if (workDays == 0)
            //{
            //    if (startTime.Hour < worktimeOfDay.AMEndWorking.Hour)
            //    {
            //        result = new DateTime(startTime.Year, startTime.Month, startTime.Day, worktimeOfDay.PMEndWorking.Hour, worktimeOfDay.PMEndWorking.Minute, 0);
            //    }
            //    else
            //    {
            //        result = new DateTime(startTime.Year, startTime.Month, startTime.Day , worktimeOfDay.AMEndWorking.Hour, worktimeOfDay.AMEndWorking.Minute, 0);
            //        var dateCount = 0;
            //        while (dateCount < 1)
            //        {
            //            result = result.AddDays(1);
            //            // Nếu là ngày làm việc thì tăng 1
            //            if (!IsWeekendOrHoliday(result, holidays))
            //            {
            //                dateCount++;
            //            }
            //        }
            //    }
            //} else 
            if (workDays == 1)
            {
                // TienBV: nhiều đơn vị yêu cầu hẹn trả hết vào cuối buổi chiều nên tạm sửa lại, nếu có yêu cầu khác sửa lại sau.
                // Hồ sơ xử lý trong ngày
                //result = (startTime.Hour < 15 || type == WorktimeType.UsingCreatedDateAfter3PM)
                //        ? new DateTime(startTime.Year, startTime.Month, startTime.Day, worktimeOfDay.PMEndWorking.Hour, worktimeOfDay.PMEndWorking.Minute, 0)
                //        : new DateTime(startTime.Year, startTime.Month, startTime.Day, worktimeOfDay.AMStartWorking.Hour, worktimeOfDay.AMStartWorking.Minute, 0);

                if (type == WorktimeType.Hsmc24H)
                {
                    result = new DateTime(startTime.Year, startTime.Month, startTime.Day, startTime.Hour, startTime.Minute, 0);
                    //var dateCount24H = 0;
                    //while (dateCount24H < 1)
                    //{
                    //    result = result.AddDays(1);
                    //    // Nếu là ngày làm việc thì tăng 1
                    //    if (!IsWeekendOrHoliday(result, holidays))
                    //    {
                    //        dateCount24H++;
                    //    }
                    //}
                }
                else
                {
                    result = new DateTime(startTime.Year, startTime.Month, startTime.Day, worktimeOfDay.PMEndWorking.Hour, worktimeOfDay.PMEndWorking.Minute, 0);

                    if (startTime.Hour < 15 || type == WorktimeType.UsingCreatedDateAfter3PM)
                    {
                        addDays -= 1;
                    }
                }
            }
            else
            {
                // TienBV: nhiều đơn vị yêu cầu hẹn trả hết vào cuối buổi chiều nên tạm sửa lại, nếu có yêu cầu khác sửa lại sau.
                //result = startTime.Hour < 12
                //    ? new DateTime(startTime.Year, startTime.Month, startTime.Day, worktimeOfDay.AMEndWorking.Hour, worktimeOfDay.AMEndWorking.Minute, 0)
                //    : ((startTime.Hour < 15 || type == WorktimeType.UsingCreatedDateAfter3PM)
                //        ? new DateTime(startTime.Year, startTime.Month, startTime.Day, worktimeOfDay.PMEndWorking.Hour, worktimeOfDay.PMEndWorking.Minute, 0)
                //        : new DateTime(startTime.Year, startTime.Month, startTime.Day, worktimeOfDay.AMEndWorking.Hour, worktimeOfDay.AMEndWorking.Minute, 0));

                result = new DateTime(startTime.Year, startTime.Month, startTime.Day, worktimeOfDay.PMEndWorking.Hour, worktimeOfDay.PMEndWorking.Minute, 0);
                if (type == WorktimeType.Hsmc24H)
                {
                    result = new DateTime(startTime.Year, startTime.Month, startTime.Day, startTime.Hour, startTime.Minute, 0);
                    addDays += 1;
                } else if (startTime.Hour >= 15 && type != WorktimeType.UsingCreatedDateAfter3PM)
                {
                    addDays += 1;
                }
            }

            if (type == WorktimeType.IgnoreCreatedDate)
            {
                addDays += 1;
            }

            var countWorkdays = 0;
            if (workDays > 1)
            {
                addDays -= 1;
            }
            while (countWorkdays < addDays)
            {
                result = result.AddDays(1);
                // Nếu là ngày làm việc thì tăng 1
                // Nếu là ngày cuối tuần và là ngày làm bù thì tăng thêm 1
                if (!IsWeekendOrHoliday(result, holidays) || (IsWorkOffset(result, dateWorkOffset) && IsWeekendOrHoliday(result, holidays)))
                {
                    countWorkdays++;
                }
            }
            return result;
        }

        /// <summary> 
        /// CuongNT - 120825.
        /// Trả về số ngày làm việc trong khoảng thời gian (Không tính ngày nghỉ, ngày lễ...)
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="holidays"> </param>
        /// <returns></returns>
        public static int GetWorkdays(DateTime startTime, DateTime endTime, List<Holiday> holidays)
        {
            if (startTime.Date.CompareTo(endTime.Date) > 0)
            {
                throw new ArgumentException("Start time is less than end time or is equal with end time.");
            }
            if (startTime.Date.CompareTo(endTime.Date) == 0)
            {
                return 1;
            }

            var result = (endTime.Date - startTime.Date).Days + 1;
            if (startTime.Hour >= 15)
                result = result - 1;

            if (result < 0)
            {
                var tmpday = startTime;
                startTime = endTime;
                endTime = tmpday;
            }

            // Tính số ngày nghỉ phải trừ đi trong khoảng thời gian đã cho
            var countHolidays = 0;
            while (startTime.Date.CompareTo(endTime.Date) < 0)
            {
                startTime = startTime.AddDays(1);
                // Nếu là ngày nghỉ thì tăng 1
                if (IsWeekendOrHoliday(startTime, holidays))
                {
                    countHolidays++;
                }
            }

            result = result - countHolidays;
            if (result < 0)
            {
                throw new Exception("WorktimeUtil::GetNumberDays::Error::result nhỏ hơn 0");
            }
            return result == 0 ? 1 : result;
        }

        /// <summary>
        /// Trả về true nếu là ngày nghỉ trong năm
        /// </summary>
        /// <param name="date"></param>
        /// <param name="holidays">Danh sách ngày nghỉ làm trong năm hiện tại </param>
        /// <returns></returns>
        public static bool IsWeekendOrHoliday(DateTime date, List<Holiday> holidays)
        {
            var result = false;
            foreach (var holiday in holidays)
            {
                if (holiday.IsWeekend)
                {
                    if (!holiday.DayOfWeek.HasValue)
                    {
                        throw new ApplicationException("WorktimeUtil::IsNotWorkingDay::DayOfWeek::Danh sách ngày nghỉ làm không đúng");
                    }
                    if (holiday.DayOfWeek.Equals(date.DayOfWeek))
                    {
                        result = true;
                        break;
                    }
                }
                else
                {
                    if (!holiday.HolidayDate.HasValue)
                    {
                        throw new ApplicationException("WorktimeUtil::IsNotWorkingDay::Date::Danh sách ngày nghỉ làm không đúng");
                    }
                    if (holiday.IsRepeat)
                    {
                        if (holiday.HolidayDate.Value.Month.Equals(date.Month)
                            && holiday.HolidayDate.Value.Day.Equals(date.Day))
                        {
                            result = true;
                            break;
                        }
                    }
                    else
                    {
                        if (holiday.HolidayDate.Value.Date.Equals(date.Date))
                        {
                            result = true;
                            break;
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// CuongNT - 191212. Đang viết chưa xong.
        /// </summary>
        /// <param name="startHour"></param>
        /// <param name="startMinute"></param>
        /// <param name="endHour"></param>
        /// <param name="endMinute"></param>
        /// <returns></returns>
        private static int GetMinuteSubtract(int startHour, int startMinute, int endHour, int endMinute)
        {
            var result = ((endHour - startHour) * 60 + (endMinute - startMinute));
            if (result <= 0)
            {
                throw new ApplicationException("WorktimeUtil::GetMinuteSubtract::Lỗi:: starttime phải nhỏ hơn endtime");
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startHour"></param>
        /// <param name="endHour"></param>
        /// <returns></returns>
        private static int GetMinuteSubtract(HourMinute startHour, HourMinute endHour)
        {
            var result = ((endHour.Hour - startHour.Hour) * 60 + (endHour.Minute - startHour.Minute));
            if (result <= 0)
            {
                throw new ApplicationException("WorktimeUtil::GetMinuteSubtract::Lỗi:: starttime phải nhỏ hơn endtime");
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <param name="workOffsets"></param>
        /// <returns></returns>
        private static bool IsWorkOffset(DateTime date, List<Holiday> workOffsets)
        {
            return workOffsets.Any(d => d.HolidayDate.Value.ToShortDateString().Equals(date.ToShortDateString()));
        }
    }
}
