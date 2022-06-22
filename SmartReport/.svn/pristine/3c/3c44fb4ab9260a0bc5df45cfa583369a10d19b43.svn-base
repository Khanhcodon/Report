using System;
using System.Collections.Generic;
using System.Linq;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Enum : HolidayExtension - public - BLL
    /// Access Modifiers: 
    /// Create Date : 250214
    /// Author      : TrungVH
    /// </author>
    /// <summary> 
    /// <para>Các hàm mở rộng cho Holiday</para>
    /// </summary>
    public static class HolidayExtension
    {
        /// <summary>
        ///   Lấy danh sách các ngày nghỉ trong năm.
        /// </summary>
        /// <param name="holidays">Danh sách các ngày nghỉ</param>
        /// <param name="year"> The year. </param>
        /// <returns> </returns>
        public static IEnumerable<Holiday> GetHolidaysInYear(this IEnumerable<Holiday> holidays, int year)
        {
            var result = holidays.Where(h => h.HolidayDate.Year == year && !h.IsRepeated).ToList();
            result.AddRange(GetRepeates(holidays, year));
            return result.OrderBy(h => h.HolidayDate);
        }

        /// <summary>
        ///   <para> Tra ve danh sach các ngày nghỉ dương được lặp lại</para>
        /// </summary>
        /// <param name="holidays">Danh sách các ngày nghỉ</param>
        /// <returns> </returns>
        public static IEnumerable<Holiday> GetRepeateSolars(this IEnumerable<Holiday> holidays)
        {
            return holidays.Where(h => h.IsRepeated && !h.IsLunar);
        }

        /// <summary>
        ///   <para> Tra ve danh sach </para>
        /// </summary>
        /// <param name="holidays">Danh sách các ngày nghỉ</param>
        /// <returns> </returns>
        private static IEnumerable<Holiday> GetRepeates(IEnumerable<Holiday> holidays)
        {
            return holidays.Where(h => h.IsRepeated);
        }

        /// <summary>
        ///   <para> Lấy danh sách các ngày được cấu hình lặp lại hàng năm. </para>
        /// </summary>
        /// <param name="holidays">Danh sách các ngày nghỉ</param>
        /// <param name="year">Năm</param>
        /// <returns> </returns>
        private static IEnumerable<Holiday> GetRepeates(IEnumerable<Holiday> holidays, int year)
        {
            holidays = GetRepeates(holidays).ToList();
            foreach (var holiday in holidays)
            {
                holiday.HolidayDate = new DateTime(year, holiday.HolidayDate.Month, holiday.HolidayDate.Day);
            }
            return holidays;
        }
    }
}
