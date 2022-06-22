using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Interface : IUserDepartmentJobTitlesPositionDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 180912
    /// Author      : GiangPN
    /// Description : DAL tương ứng với bảng UserDepartmentJobTitlesPosition trong CSDL
    /// </summary>
    public interface IUserDepartmentJobTitlesPositionDal
    {
        /// <summary>
        /// Lấy ra tất cả các mapping giữa người dùng, phòng ban, chức danh và chức vụ truyền vào. Nếu điều kiện bằng null thì sẽ lấy ra tất cả mapping
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Danh sách mapping</returns>
        IEnumerable<UserDepartmentJobTitlesPosition> Gets(Expression<Func<UserDepartmentJobTitlesPosition, bool>> spec = null);

        /// <summary>
        /// Tạo mới mapping giữa người dùng, phòng ban và chức danh
        /// </summary>
        /// <param name="userDepartmentJobTitles">Entity UserDepartmentJobTitles</param>
        void Create(UserDepartmentJobTitlesPosition userDepartmentJobTitles);

        /// <summary>
        /// Tạo mới nhiều mapping giữa người dùng, phòng ban và chức danh
        /// </summary>
        /// <param name="userDepartmentJobTitlesPositions">Danh sách UserDepartmentJobTitles</param>
        void Create(IEnumerable<UserDepartmentJobTitlesPosition> userDepartmentJobTitlesPositions);

        /// <summary>
        /// Xóa mapping giữa người dùng,phòng ban, chức danh và chức vụ
        /// </summary>
        /// <param name="userDepartmentJobTitlesPosition">Entity UserDepartmentJobTitlesPosition</param>
        void Delete(UserDepartmentJobTitlesPosition userDepartmentJobTitlesPosition);

        /// <summary>
        /// Xóa nhiều mapping giữa người dùng, phòng ban, chức danh và chức vụ
        /// </summary>
        /// <param name="userDepartmentJobTitlesPositions">Danh sách entity userDepartmentJobTitlesPositions</param>
        void Delete(IEnumerable<UserDepartmentJobTitlesPosition> userDepartmentJobTitlesPositions);
    }
}
