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
    /// Class : FeeDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : IFeeDal
    /// Create Date : 270612
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng Fee trong CSDL
    /// </summary>
    public class FeeDal : DataAccessBase, IFeeDal
    {
        private readonly IRepository<Fee> _feeRepository;
        /// <summary>
        /// Khởi tạo class <see cref="CodeDal"/>.
        /// </summary>
        /// <param name="context">Admin context</param>
        public FeeDal(IDbCustomerContext context)
            : base(context)
        {
            _feeRepository = Context.GetRepository<Fee>();
        }

#pragma warning disable 1591
        public IEnumerable<Fee> Gets(Expression<Func<Fee, bool>> spec = null,
                                            Func<IQueryable<Fee>, IQueryable<Fee>> preFilter = null,
                                            params Func<IQueryable<Fee>, IQueryable<Fee>>[] postFilters)
        {
            return _feeRepository.Find(spec, preFilter, postFilters);
        }

        public IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<Fee, TOutput>> projector, Expression<Func<Fee, bool>> spec = null, Func<IQueryable<Fee>, IQueryable<Fee>> preFilter = null, params Func<IQueryable<Fee>, IQueryable<Fee>>[] postFilters)
        {
            return _feeRepository.FindAs(projector, spec, preFilter, postFilters);
        }

        public Fee Get(int id)
        {
            return _feeRepository.One(a => a.FeeId == id);
        }

        public void Create(Fee fee)
        {
            _feeRepository.Create(fee);
        }

        public void Update(Fee fee)
        {
            _feeRepository.Update(fee);
        }

        public void Delete(Fee fee)
        {
            _feeRepository.Delete(fee);
        }

        public bool Exist(Expression<Func<Fee, bool>> spec)
        {
            return _feeRepository.Any(spec);
        }

        public int Count(Expression<Func<Fee, bool>> spec = null)
        {
            return _feeRepository.Count(spec);
        }
    }
}
