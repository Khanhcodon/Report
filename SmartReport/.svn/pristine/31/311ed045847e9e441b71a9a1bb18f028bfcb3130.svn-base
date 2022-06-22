using System.Collections.Generic;

using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : WeekendDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : IWeekendDal
    /// Create Date : 270612
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng Weekend trong CSDL
    /// </summary>
    public class WeekendDal : DataAccessBase, IWeekendDal
    {
        private readonly IRepository<Weekend> _weekendRepository;
        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="context">Customer context</param>
        public WeekendDal(IDbCustomerContext context)
            : base(context)
        {
            _weekendRepository = context.GetRepository<Weekend>();
        }


        /// <summary> Tienbv 131112
        /// Lấy ra các ngày nghỉ trong tuần.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Weekend> Gets()
        {
            return _weekendRepository.Find();
        }


        /// <summary> Tienbv 131112
        /// Xóa ngày nghỉ cuối tuần.
        /// </summary>
        /// <param name="day">The day.</param>
        public void Delete(Weekend day)
        {
            _weekendRepository.Delete(day);
        }


        /// <summary> Tienbv 131112
        /// Thêm ngày nghỉ cuối tuần.
        /// </summary>
        /// <param name="day">The day.</param>
        public void Create(Weekend day)
        {
            _weekendRepository.Create(day);
        }

        /// <summary>
        /// Lấy ra một ngày nghỉ cuối tuần
        /// </summary>
        /// <param name="dayId">the day id.</param>
        /// <returns></returns>
        public Weekend Get(int dayId)
        {
            return _weekendRepository.One(dayId);
        }
    }
}
