using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ResourceDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : IAuthorizeDal
    /// Create Date : 010812
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng Authorize trong CSDL
    /// </summary>
    public class AuthorizeDal : DataAccessBase, IAuthorizeDal
    {
        private readonly IRepository<Authorize> _authorizeRepository;
        /// <summary>
        /// Khởi tạo class <see cref="AuthorizeDal"/>.
        /// </summary>
        /// <param name="context">Admin context</param>
        public AuthorizeDal(IDbCustomerContext context)
            : base(context)
        {
            _authorizeRepository = Context.GetRepository<Authorize>();
        }

#pragma warning disable 1591
        public Authorize Get(int id)
        {
            return _authorizeRepository.One(a => a.AuthorizeId == id);
        }

        public IEnumerable<Authorize> Gets(Expression<Func<Authorize, bool>> spec = null, Func<IQueryable<Authorize>, IQueryable<Authorize>> preFilter = null, params Func<IQueryable<Authorize>, IQueryable<Authorize>>[] postFilters)
        {
            return _authorizeRepository.Find(spec, preFilter, postFilters);
        }

        public void Create(Authorize authorize)
        {
            _authorizeRepository.Create(authorize);
        }

        public void Update(Authorize authorize)
        {
            _authorizeRepository.Update(authorize);
        }

        public void Delete(Authorize authorize)
        {
            _authorizeRepository.Delete(authorize);
        }

        public int Count(Expression<Func<Authorize, bool>> spec = null)
        {
            return _authorizeRepository.Count(spec);
        }
    }
}
