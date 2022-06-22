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
    /// Class : BusinessLicenseDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : IBusinessLicenseDal
    /// Create Date : 171013
    /// Author      : DungHV
    /// Description : DAL tương ứng với bảng BusinessLicense trong CSDL
    /// </summary>
    public class BusinessLicenseDal : DataAccessBase, IBusinessLicenseDal
    {
        private readonly IRepository<BusinessLicense> _businessLicenseRepository;
        /// <summary>
        /// Khởi tạo class <see cref="CodeDal"/>.
        /// </summary>
        /// <param name="context">Admin context</param>
        public BusinessLicenseDal(IDbCustomerContext context)
            : base(context)
        {
            _businessLicenseRepository = Context.GetRepository<BusinessLicense>();
        }

#pragma warning disable 1591
        public IEnumerable<BusinessLicense> Gets(Expression<Func<BusinessLicense, bool>> spec = null,
                                            Func<IQueryable<BusinessLicense>, IQueryable<BusinessLicense>> preFilter = null,
                                            params Func<IQueryable<BusinessLicense>, IQueryable<BusinessLicense>>[] postFilters)
        {
            return _businessLicenseRepository.Find(spec, preFilter, postFilters);
        }

        public IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<BusinessLicense, TOutput>> projector, Expression<Func<BusinessLicense, bool>> spec = null, Func<IQueryable<BusinessLicense>, IQueryable<BusinessLicense>> preFilter = null, params Func<IQueryable<BusinessLicense>, IQueryable<BusinessLicense>>[] postFilters)
        {
            return _businessLicenseRepository.FindAs(projector, spec, preFilter, postFilters);
        }

        public BusinessLicense Get(int id)
        {
            return _businessLicenseRepository.One(a => a.BusinessLicenseId == id);
        }

        public void Create(BusinessLicense businessLicense)
        {
            _businessLicenseRepository.Create(businessLicense);
        }

        public void Update(BusinessLicense businessLicense)
        {
            _businessLicenseRepository.Update(businessLicense);
        }

        public void Delete(BusinessLicense businessLicense)
        {
            _businessLicenseRepository.Delete(businessLicense);
        }

        public bool Exist(Expression<Func<BusinessLicense, bool>> spec)
        {
            return _businessLicenseRepository.Any(spec);
        }

        public int Count(Expression<Func<BusinessLicense, bool>> spec = null)
        {
            return _businessLicenseRepository.Count(spec);
        }
    }
}
