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
    /// Class : BusinessTypeDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : IBusinessTypeDal
    /// Create Date : 171013
    /// Author      : DungHV
    /// Description : DAL tương ứng với bảng BusinessType trong CSDL
    /// </summary>
    public class BusinessTypeDal : DataAccessBase, IBusinessTypeDal
    {
        private readonly IRepository<BusinessType> _businessTypeRepository;
        /// <summary>
        /// Khởi tạo class <see cref="CodeDal"/>.
        /// </summary>
        /// <param name="context">Admin context</param>
        public BusinessTypeDal(IDbCustomerContext context)
            : base(context)
        {
            _businessTypeRepository = Context.GetRepository<BusinessType>();
        }

#pragma warning disable 1591
        public IEnumerable<BusinessType> Gets(Expression<Func<BusinessType, bool>> spec = null,
                                            Func<IQueryable<BusinessType>, IQueryable<BusinessType>> preFilter = null,
                                            params Func<IQueryable<BusinessType>, IQueryable<BusinessType>>[] postFilters)
        {
            return _businessTypeRepository.Find(spec, preFilter, postFilters);
        }

        public IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<BusinessType, TOutput>> projector, Expression<Func<BusinessType, bool>> spec = null, Func<IQueryable<BusinessType>, IQueryable<BusinessType>> preFilter = null, params Func<IQueryable<BusinessType>, IQueryable<BusinessType>>[] postFilters)
        {
            return _businessTypeRepository.FindAs(projector, spec, preFilter, postFilters);
        }

        public BusinessType Get(int id)
        {
            return _businessTypeRepository.One(a => a.BusinessTypeId == id);
        }

        public void Create(BusinessType businessType)
        {
            _businessTypeRepository.Create(businessType);
        }

        public void Update(BusinessType businessType)
        {
            _businessTypeRepository.Update(businessType);
        }

        public void Delete(BusinessType businessType)
        {
            _businessTypeRepository.Delete(businessType);
        }

        public bool Exist(Expression<Func<BusinessType, bool>> spec)
        {
            return _businessTypeRepository.Any(spec);
        }

        public int Count(Expression<Func<BusinessType, bool>> spec = null)
        {
            return _businessTypeRepository.Count(spec);
        }
    }
}
