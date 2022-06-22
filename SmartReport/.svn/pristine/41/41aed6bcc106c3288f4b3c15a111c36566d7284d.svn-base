using System;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Core.Lunar;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(HolidayValidator))]
    public class HolidayModel
    {
        /// <summary>
        /// Key.
        /// </summary>
        public int HolidayId { get; set; }

        /// <summary>
        /// Get or set the holiday's name.
        /// </summary>
        [LocalizationDisplayName("Holiday.CreateOrEdit.Fields.HolidayName.Label")]
        public string HolidayName { get; set; }

        /// <summary>
        /// Get or set the date of holiday
        /// </summary>
        [LocalizationDisplayName("Holiday.CreateOrEdit.Fields.HolidayDate.Label")]
        public DateTime HolidayDate { get; set; }

        private DateTime? _holidayDateInSolar;
        [LocalizationDisplayName("Holiday.CreateOrEdit.Fields.HolidayDate.Label")]
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
        [LocalizationDisplayName("Holiday.CreateOrEdit.Fields.HolidayDate.Label")]
        public LunarDate HolidayDateInLunar
        {
            get { return _holidayDateInLunar ?? (_holidayDateInLunar = LunarUtils.Solar2Lunar(HolidayDateInSolar)); }
        }

        /// <summary>
        /// Is repeate over year.
        /// </summary>
        [LocalizationDisplayName("Holiday.CreateOrEdit.Fields.IsRepeated.Label")]
        public bool IsRepeated { get; set; }

        /// <summary>
        /// Là ngày nghỉ bù.
        /// </summary>
        public bool IsExtendHoliday { get; set; }

        /// <summary>
        /// Là ngày tính theo lịch âm (ngược lại là lịch dương - đặt mặc định)
        /// </summary>
        [LocalizationDisplayName("Holiday.CreateOrEdit.Fields.IsLunar.Label")]
        public bool IsLunar { get; set; }

        /// <summary>
        /// Ngày nghỉ lễ trong khoảng(ví dụ như tết âm sẽ được nghỉ từ ngày đến ngày)
        /// </summary>
        public int HolidayRange { get; set; }

        /// <summary>
        /// -1: Trước ngày; 0: Ngày hiện tại; 1: Sau ngày.
        /// </summary>
        public int RangeLunar { get; set; }

        /// <summary>
        /// Trả về giá trị dương của HolidayRange
        /// </summary>
        public int UHolidayRang { get; set; }

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

        public HolidayModel()
        {
            HolidayDate = DateTime.Now;
        }
    }
}