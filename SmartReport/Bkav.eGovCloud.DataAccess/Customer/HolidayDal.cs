#region

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

#endregion

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    ///   Bkav Corp. - BSO - eGov - eOffice team
    ///   Project: eGov Cloud v1.0
    ///   Class : HolidayDal - public - DAL
    ///   Access Modifiers: 
    ///   * Inherit : DataAccessBase
    ///   * Implement : IHolidayDal
    ///   Create Date : 270612
    ///   Author      : TrungVH
    ///   Description : DAL tương ứng với bảng Holiday trong CSDL
    /// </summary>
    public class HolidayDal : DataAccessBase, IHolidayDal
    {
        #region Readonly & Static Fields

        private readonly IRepository<Holiday> _holidayRepository;

        #endregion

        #region C'tors

        /// <summary>
        ///   Khởi tạo <see cref="HolidayDal" />
        /// </summary>
        /// <param name="context"> Customer context </param>
        public HolidayDal(IDbCustomerContext context)
            : base(context)
        {
            _holidayRepository = context.GetRepository<Holiday>();
        }

        #endregion

        #region IHolidayDal Members

        /// <summary>
        ///   Tienbv 131112
        ///   Lấy ra danh sách các ngày nghỉ theo điều kiện.
        /// </summary>
        /// <param name="spec"> Điều kiện. </param>
        /// <returns> </returns>
        public IEnumerable<Holiday> Gets(Expression<Func<Holiday, bool>> spec = null)
        {
            return _holidayRepository.Find(spec);
        }

        /// <summary>
        ///   Tienbv 131112
        ///   Thêm ngày nghỉ.
        /// </summary>
        /// <param name="holiday"> The holiday. </param>
        public void Create(Holiday holiday)
        {
            _holidayRepository.Create(holiday);
        }

        /// <summary>
        ///   Tienbv 131112
        ///   Lấy một ngày nghỉ.
        /// </summary>
        /// <param name="id"> The id. </param>
        public Holiday Get(int id)
        {
            return _holidayRepository.One(id);
        }

        /// <summary>
        ///   Tienbv 131112
        ///   Xóa ngày nghỉ.
        /// </summary>
        /// <param name="holiday"> The holiday </param>
        public void Delete(Holiday holiday)
        {
            _holidayRepository.Delete(holiday);
        }

        /// <summary>   
        ///   <para>Kiếm tra đã có ngày này trong danh sách ngày nghỉ chưa.</para>
        ///   <para>Tienbv - 131112</para>
        /// </summary>
        /// <param name="date"> The date </param>
        /// <param name="isLunar"> The isLunar </param>
        /// <param name="holidayRange"> </param>
        /// <returns> </returns>
        public bool Exist(DateTime date, bool isLunar, int holidayRange)
        {
            // Kiểm tra tồn tại ngày nghỉ cùng là ngày dương hoặc âm
            return _holidayRepository.Any(h => (h.HolidayDate.CompareTo(date) == 0
                                                    && !h.IsRepeated
                                                    && h.IsLunar == isLunar
                                                    && h.HolidayRange == holidayRange) ||
                                               (h.HolidayDate.Month == date.Month && h.HolidayDate.Day == date.Day
                                                    && h.IsRepeated
                                                    && h.IsLunar == isLunar
                                                    && h.HolidayRange == holidayRange));
        }

        /// <summary>
        ///   Tienbv 131112
        ///   Cập nhật ngày nghỉ.
        /// </summary>
        /// <param name="holiday"> The holiday. </param>
        public void Update(Holiday holiday)
        {
            _holidayRepository.Update(holiday);
        }
        
        /// <summary>
        ///   Tienbv 131112
        ///   Xóa nhiều ngày nghỉ.
        /// </summary>
        /// <param name="holidays"> </param>
        public void Delete(IEnumerable<Holiday> holidays)
        {
            foreach (var holiday in holidays)
            {
                _holidayRepository.Delete(holiday, false);
            }
            Context.SaveChanges();
        }

        #endregion
    }
}