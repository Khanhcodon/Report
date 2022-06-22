using System.Collections.Generic;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Interface : IWeekendDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 010812
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng Weekend trong CSDL
    /// </summary>
    public interface IWeekendDal
    {
        /// <summary> Tienbv 131112
        /// Lấy ra các ngày nghỉ trong tuần.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Weekend> Gets();

        /// <summary> Tienbv 131112
        /// Xóa ngày nghỉ cuối tuần.
        /// </summary>
        /// <param name="day">The day.</param>
        void Delete(Weekend day);

        /// <summary> Tienbv 131112
        /// Thêm ngày nghỉ cuối tuần.
        /// </summary>
        /// <param name="day">The day.</param>
        void Create(Weekend day);

        /// <summary>
        /// Lấy ra một ngày nghỉ cuối tuần
        /// </summary>
        /// <param name="dayId">the day id.</param>
        /// <returns></returns>
        Weekend Get(int dayId);
    }
}
