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
    ///   Interface : IHolidayDal - public - DAL
    ///   Access Modifiers: 
    ///   Create Date : 010812
    ///   Author      : TrungVH
    ///   Description : DAL tương ứng với bảng Holiday trong CSDL
    /// </summary>
    public interface IHolidayDal
    {
        #region Instance Methods

        /// <summary>
        ///   Tienbv 131112
        ///   Thêm ngày nghỉ.
        /// </summary>
        /// <param name="holiday"> The holiday. </param>
        void Create(Holiday holiday);

        /// <summary>
        ///   Tienbv 131112
        ///   Xóa ngày nghỉ.
        /// </summary>
        /// <param name="holiday"> The holiday </param>
        void Delete(Holiday holiday);

        /// <summary>
        ///   Tienbv 131112
        ///   Xóa nhiều ngày nghỉ.
        /// </summary>
        /// <param name="holidays"> </param>
        void Delete(IEnumerable<Holiday> holidays);

        /// <summary>
        ///   Tienbv 131112
        ///   Kiếm tra đã có ngày này trong danh sách ngày nghỉ chưa.
        /// </summary>
        /// <param name="date"> The date </param>
        /// <param name="isLunar"> The isLunar </param>
        /// <param name="holidayRange"> </param>
        /// <returns> </returns>
        bool Exist(DateTime date, bool isLunar, int holidayRange);

        /// <summary>
        ///   Tienbv 131112
        ///   Lấy một ngày nghỉ.
        /// </summary>
        /// <param name="id"> The id. </param>
        Holiday Get(int id);
        
        /// <summary>
        ///   Tienbv 131112
        ///   Lấy ra danh sách các ngày nghỉ theo điều kiện.
        /// </summary>
        /// <param name="spec"> Điều kiện. </param>
        /// <returns> </returns>
        IEnumerable<Holiday> Gets(Expression<Func<Holiday, bool>> spec = null);

        /// <summary>
        ///   Tienbv 131112
        ///   Cập nhật ngày nghỉ.
        /// </summary>
        /// <param name="holiday"> The holiday. </param>
        void Update(Holiday holiday);

        #endregion
    }
}