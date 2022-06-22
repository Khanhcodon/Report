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
    /// Class : WardDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : IWardDal
    /// Create Date : 251013
    /// Author      : DungHV
    /// Description : DAL tương ứng với bảng Ward trong CSDL
    /// </summary>
    public class WardDal : DataAccessBase, IWardDal
    {
        private readonly IRepository<Ward> _wardRepository;
        /// <summary>
        /// Khởi tạo class <see cref="CodeDal"/>.
        /// </summary>
        /// <param name="context">Admin context</param>
        public WardDal(IDbCustomerContext context)
            : base(context)
        {
            _wardRepository = Context.GetRepository<Ward>();
        }

#pragma warning disable 1591
        public IEnumerable<Ward> Gets(Expression<Func<Ward, bool>> spec = null,
                                            Func<IQueryable<Ward>, IQueryable<Ward>> preFilter = null,
                                            params Func<IQueryable<Ward>, IQueryable<Ward>>[] postFilters)
        {
            return _wardRepository.Find(spec, preFilter, postFilters);
        }

        public IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<Ward, TOutput>> projector, Expression<Func<Ward, bool>> spec = null, Func<IQueryable<Ward>, IQueryable<Ward>> preFilter = null, params Func<IQueryable<Ward>, IQueryable<Ward>>[] postFilters)
        {
            return _wardRepository.FindAs(projector, spec, preFilter, postFilters);
        }

        public Ward Get(int id)
        {
            return _wardRepository.One(a => a.WardId == id);
        }

        public void Create(Ward city)
        {
            _wardRepository.Create(city);
        }

        public void Update(Ward city)
        {
            _wardRepository.Update(city);
        }

        public void Delete(Ward city)
        {
            _wardRepository.Delete(city);
        }

        public bool Exist(Expression<Func<Ward, bool>> spec)
        {
            return _wardRepository.Any(spec);
        }

        public int Count(Expression<Func<Ward, bool>> spec = null)
        {
            return _wardRepository.Count(spec);
        }
    }
}
