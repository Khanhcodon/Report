using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : PermissionDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : IPermissionDal
    /// Create Date : 270612
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng Permission trong CSDL
    /// </summary>
    public class PermissionDal : DataAccessBase, IPermissionDal
    {
        private readonly IRepository<Permission> _permissionRepository; 

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="context">Customer context</param>
        public PermissionDal(IDbCustomerContext context) : base(context)
        {
            _permissionRepository = Context.GetRepository<Permission>();
        }

        #pragma warning disable 1591

        public IEnumerable<Permission> Gets(Expression<Func<Permission, bool>> spec = null)
        {
            return _permissionRepository.Find(spec);
        }

        public IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<Permission, TOutput>> projector, Expression<Func<Permission, bool>> spec = null)
        {
            return _permissionRepository.FindAs(projector, spec);
        }
    }
}
