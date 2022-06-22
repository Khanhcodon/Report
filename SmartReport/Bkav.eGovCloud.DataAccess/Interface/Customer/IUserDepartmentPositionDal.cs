using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Interface : IUserDepartmentPositionDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 180912
    /// Author      : GiangPN
    /// Description : DAL tương ứng với bảng UserDepartmentPosition trong CSDL
    /// </summary>
    public interface IUserDepartmentPositionDal
    {
        /// <summary>
        /// Lấy ra tất cả các mapping giữa người dùng, phòng ban và chức vụ truyền vào. Nếu điều kiện bằng null thì sẽ lấy ra tất cả mapping
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Danh sách mapping</returns>
        IEnumerable<UserDepartmentPosition> Gets(Expression<Func<UserDepartmentPosition, bool>> spec = null);

        /// <summary>
        /// Tạo mới mapping giữa người dùng, phòng ban và chức vụ
        /// </summary>
        /// <param name="userDepartmentPosition">Entity UserDepartmentPosition</param>
        void Create(UserDepartmentPosition userDepartmentPosition);

        /// <summary>
        /// Tạo mới nhiều mapping giữa người dùng, phòng ban và chức vụ
        /// </summary>
        /// <param name="userDepartmentPositions">Danh sách UserDepartmentPosition</param>
        void CreateMultipleRecords(IEnumerable<UserDepartmentPosition> userDepartmentPositions);

        /// <summary>
        /// Xóa mapping giữa người dùng,phòng ban và chức vụ
        /// </summary>
        /// <param name="userDepartmentPosition">Entity UserDepartmentPosition</param>
        void Delete(UserDepartmentPosition userDepartmentPosition);

        /// <summary>
        /// Xóa nhiều mapping giữa người dùng, phòng ban và chức vụ
        /// </summary>
        /// <param name="userDepartmentPositions">Danh sách entity userDepartmentPosition</param>
        void DeleteMultipleRecords(IEnumerable<UserDepartmentPosition> userDepartmentPositions);
    }
}
