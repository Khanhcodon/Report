using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : RoleDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : IRoleDal
    /// Create Date : 010812
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng Role trong CSDL
    /// </summary>
    public class RoleDal : DataAccessBase, IRoleDal
    {
        private readonly IRepository<Role> _roleRepository;

        /// <summary>
        /// Khởi tạo class  <see cref="RoleDal"/>.
        /// </summary>
        /// <param name="context">Customer context</param>
        public RoleDal(IDbCustomerContext context)
            : base(context)
        {
            _roleRepository = Context.GetRepository<Role>();
        }

        #pragma warning disable 1591

        public IEnumerable<Role> Gets(Expression<Func<Role, bool>> spec = null)
        {
            return _roleRepository.Find(spec);
        }

        public IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<Role, TOutput>> projector, Expression<Func<Role, bool>> spec = null)
        {
            return _roleRepository.FindAs(projector, spec);
        }

        public TOutput Get<TOutput>(Expression<Func<Role, TOutput>> projector, Expression<Func<Role, bool>> spec)
        {
            return _roleRepository.OneAs(projector, spec);
        }

        public Role Get(int roleId)
        {
            return _roleRepository.One(r => r.RoleId == roleId);
        }

        public Role Get(string roleKey)
        {
            return _roleRepository.One(r => r.RoleKey == roleKey);
        }

        public void Create(Role role)
        {
            _roleRepository.Create(role);
        }

        public void Update(Role role)
        {
            _roleRepository.Update(role);
        }

        public void Delete(Role role)
        {
            _roleRepository.Delete(role);
        }

        public bool Exist(Expression<Func<Role, bool>> spec = null)
        {
            return _roleRepository.Any(spec);
        }
    }
}
