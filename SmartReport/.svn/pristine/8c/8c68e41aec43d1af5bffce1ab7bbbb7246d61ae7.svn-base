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
    /// Class : IncreaseDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : IIncreaseDal
    /// Create Date : 40912
    /// Author      : DungHV
    /// Description : DAL tương ứng với bảng Increase trong CSDL
    /// </summary>
    public class IncreaseDal : DataAccessBase, IIncreaseDal
    {
        private readonly IRepository<Increase> _increaseRepository;
        /// <summary>
        /// Khởi tạo class <see cref="IncreaseDal"/>.
        /// </summary>
        /// <param name="context">Admin context</param>
        public IncreaseDal(IDbCustomerContext context)
            : base(context)
        {
            _increaseRepository = Context.GetRepository<Increase>();
        }

#pragma warning disable 1591
        public IEnumerable<Increase> Gets(Expression<Func<Increase, bool>> spec = null, Func<IQueryable<Increase>, IQueryable<Increase>> preFilter = null, params Func<IQueryable<Increase>, IQueryable<Increase>>[] postFilters)
        {
            return _increaseRepository.Find(spec, preFilter, postFilters);
        }

        public IEnumerable<T> GetsAs<T>(Expression<Func<Increase, T>> projector, Expression<Func<Increase, bool>> spec = null)
        {
            return _increaseRepository.FindAs(projector, spec);
        }

        public Increase Get(int id)
        {
            return _increaseRepository.One(a => a.IncreaseId == id);
        }

        public void Create(Increase increase)
        {
            _increaseRepository.Create(increase);
        }

        public void Delete(Increase increase)
        {
            _increaseRepository.Delete(increase);
        }

        public bool Exist(Expression<Func<Increase, bool>> spec)
        {
            return _increaseRepository.Any(spec);
        }

        public int Count(Expression<Func<Increase, bool>> spec = null)
        {
            return _increaseRepository.Count(spec);
        }

        public void Update(Increase increase)
        {
            _increaseRepository.Update(increase);
        }
    }
}
