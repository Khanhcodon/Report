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
    /// Class : BusinessDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : IBusinessDal
    /// Create Date : 171013
    /// Author      : DungHV
    /// Description : DAL tương ứng với bảng Business trong CSDL
    /// </summary>
    public class BusinessDal : DataAccessBase, IBusinessDal
    {
        private readonly IRepository<Business> _businessRepository;

        /// <summary>
        /// Khởi tạo class <see cref="CodeDal"/>.
        /// </summary>
        /// <param name="context">Admin context</param>
        public BusinessDal(IDbCustomerContext context)
            : base(context)
        {
            _businessRepository = Context.GetRepository<Business>();
        }

#pragma warning disable 1591
        public IEnumerable<Business> Gets(Expression<Func<Business, bool>> spec = null,
                                            Func<IQueryable<Business>, IQueryable<Business>> preFilter = null,
                                            params Func<IQueryable<Business>, IQueryable<Business>>[] postFilters)
        {
            return _businessRepository.Find(spec, preFilter, postFilters);
        }

        public IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<Business, TOutput>> projector, Expression<Func<Business, bool>> spec = null, Func<IQueryable<Business>, IQueryable<Business>> preFilter = null, params Func<IQueryable<Business>, IQueryable<Business>>[] postFilters)
        {
            return _businessRepository.FindAs(projector, spec, preFilter, postFilters);
        }

        public Business Get(int id)
        {
            return _businessRepository.One(a => a.BusinessId == id);
        }

        public Business Get(string name)
        {
            return _businessRepository.One(a => a.BusinessName == name);
        }

        public void Create(Business fee)
        {
            _businessRepository.Create(fee);
        }

        public void Update(Business fee)
        {
            _businessRepository.Update(fee);
        }

        public void Delete(Business fee)
        {
            _businessRepository.Delete(fee);
        }

        public bool Exist(Expression<Func<Business, bool>> spec)
        {
            return _businessRepository.Any(spec);
        }

        public int Count(Expression<Func<Business, bool>> spec = null)
        {
            return _businessRepository.Count(spec);
        }
    }
}
