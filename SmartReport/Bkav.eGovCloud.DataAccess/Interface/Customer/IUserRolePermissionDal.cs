using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Interface : IUserRolePermissionDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 010812
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng UserRolePermission trong CSDL
    /// </summary>
    public interface IUserRolePermissionDal
    {
        /// <summary>
        /// Lấy ra tất cả các mapping phù hợp với điều kiện truyền vào. Nếu điều kiện băng null thì sẽ lấy ra tất cả các mapping
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Danh sách mapping</returns>
        IEnumerable<UserRolePermission> Gets(Expression<Func<UserRolePermission, bool>> spec = null);

        /// <summary>
        /// Tạo mới nhiều bản ghi
        /// </summary>
        /// <param name="userRolePermissions">Entity mapping</param>
        void Create(IEnumerable<UserRolePermission> userRolePermissions);

        /// <summary>
        /// Xóa nhiều bản ghi
        /// </summary>
        /// <param name="userRolePermissions">Entity mapping</param>
        void Delete(IEnumerable<UserRolePermission> userRolePermissions);
    }
}
