using Bkav.eGovCloud.Core.Lunar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Business.Objects
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class HolidayCached
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id ngày nghỉ
        /// </summary>
        public int HolidayId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên ngày nghỉ
        /// </summary>
        public string HolidayName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ngày nghỉ
        /// </summary>
        public DateTime HolidayDate { get; set; }

        private DateTime? _holidayDateInSolar;
        /// <summary>
        /// Lấy hoặc thiết lập ngày nghỉ Dương lịch
        /// </summary>
        public DateTime HolidayDateInSolar
        {
            get
            {
                if (_holidayDateInSolar != null)
                {
                    return _holidayDateInSolar.Value;
                }
                if (IsLunar)
                {
                    _holidayDateInSolar = LunarUtils.Lunar2Solar(new LunarDate(HolidayDate.Day, HolidayDate.Month, HolidayDate.Year, false));
                    _holidayDateInSolar = _holidayDateInSolar.Value.AddDays(HolidayRange);
                }
                else
                {
                    _holidayDateInSolar = HolidayDate;
                }
                return _holidayDateInSolar.Value;
            }
        }

        private LunarDate _holidayDateInLunar;
        /// <summary>
        /// Lấy hoặc thiết lập ngày nghỉ Âm lịch
        /// </summary>
        public LunarDate HolidayDateInLunar
        {
            get { return _holidayDateInLunar ?? (_holidayDateInLunar = LunarUtils.Solar2Lunar(HolidayDateInSolar)); }
        }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra ngày nghỉ có lặp lại theo năm
        /// </summary>
        public bool IsRepeated { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra ngày nghỉ này là thêm
        /// </summary>
        public bool IsExtendHoliday { get; set; }

        /// <summary>
        /// Là ngày tính theo lịch âm(ngược lại là lịch dương - đặt mặc định)
        /// </summary>
        public bool IsLunar { get; set; }

        /// <summary>
        /// Ngày nghỉ lễ trong khoảng(ví dụ như tết âm sẽ được nghỉ từ ngày đến ngày)
        /// </summary>
        public int HolidayRange { get; set; }

        /// <summary>
        /// Khác null (bằng Id của ngày nghỉ lễ) khi là ngày nghỉ bù. Bằng null khi là ngày nghỉ lẽ.
        /// </summary>
        public int? ParentHolidayId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập có phải là ngày nghỉ hay ngày làm việc bù
        /// <para> Hopcv</para>
        /// <para>CreateDate:030414</para>
        /// </summary>
        public bool IsHoliday { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Equals(HolidayCached holiday)
        {
            bool result;
            if (IsRepeated)
            {
                result = HolidayDate.Month == holiday.HolidayDate.Month &&
                         HolidayDate.Day == holiday.HolidayDate.Day &&
                         IsLunar == holiday.IsLunar &&
                         HolidayRange == holiday.HolidayRange;
            }
            else
            {
                result = HolidayDate.Date == holiday.HolidayDate.Date &&
                         IsLunar == holiday.IsLunar &&
                         HolidayRange == holiday.HolidayRange;
            }

            return result;
        }
    }
}
