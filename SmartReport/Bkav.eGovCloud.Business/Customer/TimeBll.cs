#region

using System;
using System.Collections.Generic;
using System.Linq;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Core.Caching;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Business.Objects;

#endregion

namespace Bkav.eGovCloud.Business.Customer
{
    /// <author>
    ///   <para> Bkav Corp. - BSO - eGov - eOffice team </para>
    ///   <para> Project: eGov Cloud v1.0 </para>
    ///   <para> Class : CatalogBll - public - BLL </para>
    ///   <para> Access Modifiers: </para>
    ///   <para> Create Date : 141112 </para>
    ///   <para> Author : TienBV </para>
    ///   **************************************
    ///   <para> CuongNT@bkav.com - 220813: Cập nhật lại thư viện cho chuẩn hơn. </para>
    /// </author>
    /// <summary>
    ///   Quản lý các ngày nghỉ, lễ, ngày làm bù
    /// </summary>
    public class TimeBll : ServiceBase
    {
        #region Readonly & Static Fields

        private readonly IRepository<Holiday> _holidayRepository;
        private readonly IRepository<Weekend> _weekendRepository;
        private readonly MemoryCacheManager _cacheManager;
        private readonly ResourceBll _resourceService;

        #endregion

        #region C'tors

        /// <summary>
        ///   Khởi tạo <see cref="TimeBll" />
        /// </summary>
        /// <param name="context">Context </param>
        /// <param name="cacheManager"></param>
        /// <param name="resourceService"></param>
        public TimeBll(IDbCustomerContext context, MemoryCacheManager cacheManager, ResourceBll resourceService)
            : base(context)
        {
            _holidayRepository = Context.GetRepository<Holiday>();
            _weekendRepository = Context.GetRepository<Weekend>();
            _cacheManager = cacheManager;
            _resourceService = resourceService;
        }

        #endregion

        #region Instance Methods

        /// <summary>
        ///   <para> Tự động tạo mới các ngày nghỉ bù trong năm. </para>
        ///   <para> Tienbv - 131112 </para>
        /// </summary>
        /// <param name="year"> The year </param>
        /// <remarks>
        ///   Thuật toán tính ra ngày nghỉ bù và tự động tạo mới như sau:
        ///   - Lấy ra các ngày nghỉ lễ trùng vào ngày nghỉ cuối tuần
        ///   - Tính ra danh sách các ngày nghỉ bù theo phương án thêm vào cuối kì nghỉ
        ///   - Kiểm tra Db nếu các ngày nghỉ bù này đã có thì thôi, chưa có thì thêm mới, nếu trùng với ngày nghỉ lễ đã có thì tiếp tục tăng
        /// </remarks>
        public void CreateExtendHolidays(int year)
        {
            // Danh sách ngày nghỉ trong năm (nghỉ lễ + nghỉ bù)
            var holidays = GetHolidays(year).ToList();

            // Lọc ra danh sách ngày nghỉ lễ và danh sách ngày nghỉ bù
            var holidayNormals = holidays.Where(c => !c.IsExtendHoliday).Select(o => o).ToList();
            var holidayExtends = holidays.Where(c => c.IsExtendHoliday).Select(o => o).ToList();
            if (!holidayNormals.Any())
            {
                return;
            }

            // Danh sách ngày nghỉ cuối tuần
            var weekends = GetWeekends().ToList();
            var name = _resourceService.GetResource("Customer.Time.HolidayFormat");
            foreach (var holiday in holidayNormals)
            {
                var dateExtend = holiday.HolidayDateInSolar;
                if (!weekends.Contains(dateExtend.DayOfWeek) ||
                    holidayExtends.Exists(c => c.ParentHolidayId == holiday.HolidayId))
                {
                    continue;
                }
                // Nếu ngày nghỉ lễ này trùng ngày nghỉ cuối tuần
                do
                {
                    dateExtend = dateExtend.AddDays(1);
                } while (
                    // Ngày làm bù không được trùng vào ngày cuối tuần
                    weekends.Contains(dateExtend.DayOfWeek) ||
                    // Chưa có ngày này trong danh sách dc nghỉ
                    holidays.Exists(c => c.HolidayDateInSolar.Date == dateExtend.Date) //||
                    // Không phải là ngày lặp lại hàng năm
                    //ExistRepeated(new Holiday { HolidayDate = dateExtend })
                    );

                var extend = new Holiday
                    {
                        HolidayDate = dateExtend,
                        HolidayName = name + " " + holiday.HolidayName,
                        IsRepeated = false,
                        IsExtendHoliday = true,
                        IsLunar = false,
                        HolidayRange = 0,
                        ParentHolidayId = holiday.HolidayId,
                        IsHoliday = true
                    };
                CreateExtendHoliday(extend);
                holidays.Add(extend);
            }
            _cacheManager.Remove(CacheParam.HolidayAllKey);
        }

        /// <summary>
        ///   <para> Thêm ngày nghỉ. </para>
        ///   <para> Tienbv - 131112 </para>
        /// </summary>
        /// <param name="holiday"> The holiday. </param>
        public void CreateHoliday(Holiday holiday)
        {
            if (holiday == null)
            {
                throw new ArgumentNullException("holiday");
            }
            if (holiday.IsExtendHoliday)
            {
                throw new ArgumentException("Không được phép tạo mới ngày nghỉ bù.", "holiday");
            }
            if (ExistHoliday(holiday))
            {
                throw new EgovException("Ngày này đã có trong danh sách nghỉ.");
            }
            // Nếu là nghỉ lặp --> reset năm về năm mặc định
            if (holiday.IsRepeated)
            {
                //TrinhNVd: Datetime trong SqlServer có dải dữ liệu thấp hơn => Để 1900
                //holiday.HolidayDate = new DateTime(DateTime.MinValue.Year, holiday.HolidayDate.Month,
                //                                   holiday.HolidayDate.Day);
                holiday.HolidayDate = new DateTime(1900, holiday.HolidayDate.Month,
                                                   holiday.HolidayDate.Day);
            }
            _holidayRepository.Create(holiday);
            Context.SaveChanges();
            _cacheManager.Remove(CacheParam.HolidayAllKey);
        }

        private void CreateExtendHoliday(Holiday holiday)
        {
            if (holiday == null)
            {
                throw new ArgumentNullException("holiday");
            }
            if (holiday.IsRepeated)
            {
                throw new EgovException("Ngày nghỉ bù không được phép lặp hàng năm.");
            }
            if (ValidateExtendHoliday(holiday))
            {
                throw new EgovException("Ngày này đã có trong danh sách nghỉ.");
            }
            _holidayRepository.Create(holiday);
            Context.SaveChanges();
        }

        /// <summary>
        ///   Tienbv 131112
        ///   Thêm ngày nghỉ cuối tuần.
        /// </summary>
        /// <param name="dayId"> The day id. </param>
        public void CreateWeekend(DayOfWeek dayId)
        {
            var day = new Weekend
                {
                    DayId = Convert.ToByte(dayId.GetHashCode()),
                    DayName = dayId.ToString()
                };
            _weekendRepository.Create(day);
            Context.SaveChanges();
            _cacheManager.Remove(CacheParam.WeekendAllKey);
        }

        /// <summary>
        ///   Tienbv 131112
        ///   Xóa ngày nghỉ cuối tuần.
        /// </summary>
        /// <param name="dayId"> The day id. </param>
        public void DeleteWeekend(DayOfWeek dayId)
        {
            var day = _weekendRepository.Get(dayId.GetHashCode());
            _weekendRepository.Delete(day);
            Context.SaveChanges();
            _cacheManager.Remove(CacheParam.WeekendAllKey);
        }

        /// <summary>
        ///   Tienbv 131112
        ///   Xóa ngày nghỉ lễ, ngày nghỉ bù
        /// </summary>
        /// <param name="holiday"> The holiday </param>
        public void DeteleHoliday(Holiday holiday)
        {
            if (holiday == null)
            {
                throw new ArgumentNullException("holiday");
            }
            // Xóa ngày nghỉ bù tương ứng nếu có
            var extendHoliday = GetExtendHoliday(holiday.HolidayId);
            if (extendHoliday != null)
            {
                _holidayRepository.Delete(extendHoliday);
            }
            // Xóa ngày nghỉ lễ
            _holidayRepository.Delete(holiday);
            Context.SaveChanges();
            _cacheManager.Remove(CacheParam.HolidayAllKey);
        }

        /// <summary>
        ///   <para> Trả về ngày nghỉ bù của ngày nghỉ lễ yêu cầu </para>
        ///   <para> CuongNT@bkav.com - 220813 </para>
        /// </summary>
        /// <param name="holidayId"> Id ngày nghỉ lễ </param>
        /// <returns> Ngày nghỉ bù </returns>
        public Holiday GetExtendHoliday(int holidayId)
        {
            var results = _holidayRepository.Gets(false, c => c.ParentHolidayId == holidayId).ToList();
            if (!results.Any())
            {
                return null;
            }
            if (results.Count() != 1)
            {
                throw new ApplicationException("Tồn tại nhiều hơn 1 ngày nghỉ bù cho 1 ngày nghỉ lễ.");
            }
            return results.First();
        }

        /// <summary>
        ///   Tienbv 131112
        ///   Lấy một ngày nghỉ.
        /// </summary>
        /// <param name="holidayId"> The id. </param>
        public Holiday GetHoliday(int holidayId)
        {
            return _holidayRepository.Get(holidayId);
        }

        /// <summary>
        /// Trả về tất cả các ngày nghỉ lễ, nghỉ bù, làm bù. Kết quả có lưu cache.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Holiday> GetAllFromCache()
        {
            var allHolidays = _cacheManager.Get(CacheParam.HolidayAllKey, CacheParam.HolidayAllCacheTimeOut, () => { 
                var result = _holidayRepository.GetsReadOnly();
                return AutoMapper.Mapper.Map<IEnumerable<Holiday>, IEnumerable<HolidayCached>>(result);
            });

            return AutoMapper.Mapper.Map<IEnumerable<HolidayCached>, IEnumerable<Holiday>>(allHolidays);
        }

        /// <summary>
        ///   TienBV 131112
        ///   Lấy danh sách các ngày nghỉ. Kết quả chỉ đọc
        /// </summary>
        public IEnumerable<Holiday> GetHolidays()
        {
            return GetAllFromCache().Where(h => h.IsHoliday);
        }

        /// <summary>
        ///   TienBv 131112
        ///   Lấy danh sách các ngày nghỉ trong năm.
        /// </summary>
        /// <param name="year"> The year. </param>
        /// <param name="type"></param>
        /// <returns> </returns>
        public IEnumerable<Holiday> GetHolidays(int year, bool? type = null)
        {
            var result = GetHolidays().Where(h => h.HolidayDate.Year == year && !h.IsRepeated).ToList();

            result.AddRange(GetRepeates(year));
            if (type.HasValue)
            {
                return result.Where(p => type.Value == p.ParentHolidayId.HasValue).OrderBy(h => h.HolidayDate);
            }
            return result.OrderBy(h => h.HolidayDate);
        }

        /// <summary>
        ///   Lấy danh sách các ngày làm việc bù. Kết quả chỉ đọc
        ///   <para>   HopCV 030414</para>
        /// </summary>
        public IEnumerable<Holiday> GetDayWorkOffsets()
        {
            return GetAllFromCache().Where(p => !p.IsHoliday); // _holidayRepository.GetsReadOnly(p => !p.IsHoliday);
        }

        /// <summary>
        /// Lấy danh sách các ngày lam bù trong năm.
        /// <para>   HopCV 030414</para>
        /// </summary>
        /// <param name="year">năm</param>
        /// <returns></returns>
        public IEnumerable<Holiday> GetDayWorkOffsets(int year)
        {
            var result = GetAllFromCache().Where(h => !h.IsHoliday && h.HolidayDate.Year == year); 
            return result.OrderBy(h => h.HolidayDate);
        }

        /// <summary>
        ///   <para> Tra ve danh sach </para>
        /// </summary>
        /// <returns> </returns>
        public IEnumerable<Holiday> GetRepeateSolars()
        {
            return GetHolidays().Where(h => h.IsRepeated && !h.IsLunar).ToList();
        }

        /// <summary>
        ///   Tienbv 131112
        ///   Lấy ra các ngày nghỉ trong tuần. Kết quả chỉ đọc
        /// </summary>
        /// <returns> Trả về danh sách mã thứ trong tuần theo dịnh dạng tiếng anh: Mon, T </returns>
        public IEnumerable<DayOfWeek> GetWeekends()
        {
            var weekends = _cacheManager.Get(CacheParam.WeekendAllKey,
                CacheParam.WeekendAllCacheTimeOut,
                () => _weekendRepository.GetsReadOnly());

            return weekends.Select(c => (DayOfWeek)c.DayId);
        }

        /// <summary>
        ///   Tienbv 131112
        ///   Cập nhật ngày nghỉ.
        /// </summary>
        /// <param name="holiday"> The holiday. </param>
        public void Update(Holiday holiday)
        {
            if (holiday == null)
            {
                throw new ArgumentNullException("holiday");
            }
            var oldHoliday = GetHoliday(holiday.HolidayId);
            if (!holiday.Equals(oldHoliday))
            {
                if (holiday.IsExtendHoliday)
                {
                    if (ValidateExtendHoliday(holiday))
                    {
                        throw new EgovException("Ngày nghỉ bù này trùng với ngày nghỉ đã có trong năm.");
                    }
                }
                else
                {
                    if (ExistHoliday(holiday))
                    {
                        throw new EgovException("Ngày nghỉ lễ này đã có trong năm.");
                    }
                }
            }
            // Nếu là nghỉ lặp --> reset năm về năm mặc định
            if (oldHoliday.IsRepeated)
            {
                oldHoliday.HolidayDate = new DateTime(DateTime.MinValue.Year, holiday.HolidayDate.Month,
                                                   holiday.HolidayDate.Day);
            }
            oldHoliday.HolidayRange = holiday.HolidayRange;
            oldHoliday.HolidayName = holiday.HolidayName;
            Context.SaveChanges();
            _cacheManager.Remove(CacheParam.HolidayAllKey);
        }

        /// <summary>
        ///   <para> Kiếm tra đã có ngày NGHỈ LỄ này trong danh sách ngày nghỉ chưa. </para>
        ///   <para> Tienbv - 131112 </para>
        /// </summary>
        /// <param name="date"> The date </param>
        /// <returns> </returns>
        private bool ExistHoliday(Holiday date)
        {
            if (date.IsExtendHoliday)
            {
                throw new ApplicationException("Sử dụng hàm ValidateExtendHoliday() để kiểm tra với các ngày nghỉ bù.");
            }
            // Kiểm tra tồn tại ngày nghỉ cùng là dương hoặc âm trong database
            return _holidayRepository.Exist(h => ((h.HolidayDate.CompareTo(date.HolidayDate) == 0)
                                                && !h.IsRepeated
                                                && (h.IsLunar == date.IsLunar)
                                                && h.HolidayRange == date.HolidayRange) ||
                                               ((h.HolidayDate.Month == date.HolidayDate.Month)
                                                && (h.HolidayDate.Day == date.HolidayDate.Day)
                                                && h.IsRepeated
                                                && (h.IsLunar == date.IsLunar)
                                                && h.HolidayRange == date.HolidayRange));
            //_holidayRepository.Exist(date.HolidayDate, date.IsLunar, date.HolidayRange);
        }

        /// <summary>
        /// Kiểm tra xem ngày này có trong danh sách ngày nghỉ chưa
        /// <para>HopCV  030414</para>
        /// </summary>
        /// <param name="date">Ngày truyền vào</param>
        /// <returns></returns>
        public bool ExistDate(DateTime date)
        {
            return _holidayRepository.Exist(h => ((h.HolidayDate.Equals(date)
                                                 && !h.IsRepeated) ||
                                                ((h.HolidayDate.Month == date.Month)
                                                 && (h.HolidayDate.Day == date.Day)
                                                 && h.IsRepeated)));
        }

        /// <summary>
        ///   <para> Tra ve danh sach </para>
        /// </summary>
        /// <returns> </returns>
        private IEnumerable<Holiday> GetRepeates()
        {
            return GetHolidays().Where(h => h.IsRepeated);
        }

        /// <summary>
        ///   <para> Lấy danh sách các ngày được cấu hình lặp lại hàng năm. </para>
        ///   <para> Tienbv - 131112 </para>
        /// </summary>
        /// <returns> </returns>
        private IEnumerable<Holiday> GetRepeates(int year)
        {
            var holidays = GetRepeates().ToList();
            foreach (var holiday in holidays)
            {
                holiday.HolidayDate = new DateTime(year, holiday.HolidayDate.Month, holiday.HolidayDate.Day);
            }
            return holidays;
        }

        /// <summary>
        ///   <para> Kiếm tra ngày NGHỈ BÙ này có trùng với ngày nghỉ nào đã có trong năm chưa. </para>
        ///   <para> Tienbv - 131112 </para>
        /// </summary>
        /// <param name="date"> The date </param>
        /// <returns> </returns>
        private bool ValidateExtendHoliday(Holiday date)
        {
            if (!date.IsExtendHoliday)
            {
                throw new ApplicationException("Sử dụng hàm ExistHoliday() để kiểm tra với các ngày nghỉ lễ.");
            }

            // Kiểm tra ngày nghỉ bù có trùng với ngày nghỉ nào đã có trong năm chưa.
            var holidaysInYear = GetHolidays(date.HolidayDateInSolar.Year);
            return holidaysInYear.Any(c => c.HolidayDateInSolar.Date == date.HolidayDateInSolar.Date);
        }

        #endregion
    }
}