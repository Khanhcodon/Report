using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : UserRoleDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : IUserRoleDal
    /// Create Date : 110912
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng UserRole trong CSDL
    /// </summary>
    public class UserRoleDal : DataAccessBase, IUserRoleDal
    {
        private readonly IRepository<UserRole> _userRoleRepository;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="context">Customer context</param>
        public UserRoleDal(IDbCustomerContext context) : base(context)
        {
            _userRoleRepository = Context.GetRepository<UserRole>();
        }

        #pragma warning disable 1591

        public IEnumerable<UserRole> Gets(Expression<Func<UserRole, bool>> spec = null)
        {
            return _userRoleRepository.Find(spec);
        }

        public void Create(UserRole userRole)
        {
            _userRoleRepository.Create(userRole);
        }

        public void Create(IEnumerable<UserRole> userRoles)
        {
            foreach (var userRole in userRoles)
            {
                _userRoleRepository.Create(userRole, false);
            }
            Context.SaveChanges();
        }

        public void Delete(UserRole userRole)
        {
            _userRoleRepository.Delete(userRole);
        }

        public void Delete(IEnumerable<UserRole> userRoles)
        {
            foreach (var userRole in userRoles)
            {
                _userRoleRepository.Delete(userRole, false);
            }
            Context.SaveChanges();
        }
    }
}
