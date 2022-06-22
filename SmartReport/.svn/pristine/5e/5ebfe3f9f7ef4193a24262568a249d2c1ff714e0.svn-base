using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Interface : IUserRoleDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 010812
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng UserRole trong CSDL
    /// </summary>
    public interface IUserRoleDal
    {
        /// <summary>
        /// Lấy ra tất cả các mapping giữa nhóm người dùng và người dùng phù hợp với điều kiện truyền vào. Nếu điều kiện bằng null thì sẽ lấy ra tất cả mapping
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Danh sách mapping</returns>
        IEnumerable<UserRole> Gets(Expression<Func<UserRole, bool>> spec = null);

        /// <summary>
        /// Tạo mới mapping giữa người dùng và nhóm người dùng
        /// </summary>
        /// <param name="userRole">Entity user role</param>
        void Create(UserRole userRole);

        /// <summary>
        /// Tạo mới nhiều mapping giữa người dùng và nhóm người dùng
        /// </summary>
        /// <param name="userRoles">Danh sách entity userrole</param>
        void Create(IEnumerable<UserRole> userRoles);

        /// <summary>
        /// Xóa mapping giữa người dùng và nhóm người dùng
        /// </summary>
        /// <param name="userRole">Entity userrole</param>
        void Delete(UserRole userRole);

        /// <summary>
        /// Xóa nhiều mapping giữa người dùng và nhóm người dùng
        /// </summary>
        /// <param name="userRoles">Danh sách entity userrole</param>
        void Delete(IEnumerable<UserRole> userRoles);
    }
}
