using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : UserRolePermissionDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : IUserRolePermissionDal
    /// Create Date : 270612
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng UserRolePermission trong CSDL
    /// </summary>
    public class UserRolePermissionDal : DataAccessBase, IUserRolePermissionDal
    {
        private readonly IRepository<UserRolePermission> _userRolePermissionRepository; 

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="context">Customer context</param>
        public UserRolePermissionDal(IDbCustomerContext context) : base(context)
        {
            _userRolePermissionRepository = Context.GetRepository<UserRolePermission>();
        }

        #pragma warning disable 1591

        public IEnumerable<UserRolePermission> Gets(Expression<Func<UserRolePermission, bool>> spec = null)
        {
            return _userRolePermissionRepository.Find(spec);
        }

        public void Create(IEnumerable<UserRolePermission> userRolePermissions)
        {
            foreach (var userRolePermission in userRolePermissions)
            {
                _userRolePermissionRepository.Create(userRolePermission, false);
            }
            Context.SaveChanges();
        }

        public void Delete(IEnumerable<UserRolePermission> userRolePermissions)
        {
            foreach (var userRolePermission in userRolePermissions)
            {
                _userRolePermissionRepository.Delete(userRolePermission, false);
            }
            Context.SaveChanges();
        }
    }
}
