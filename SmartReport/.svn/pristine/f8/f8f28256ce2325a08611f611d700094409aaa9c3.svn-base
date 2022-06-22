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
    /// Class : PaperDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : IPaperDal
    /// Create Date : 270612
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng Paper trong CSDL
    /// </summary>
    public class PaperDal : DataAccessBase, IPaperDal
    {
        private readonly IRepository<Paper> _paperRepository;
        /// <summary>
        /// Khởi tạo class <see cref="CodeDal"/>.
        /// </summary>
        /// <param name="context">Admin context</param>
        public PaperDal(IDbCustomerContext context)
            : base(context)
        {
            _paperRepository = Context.GetRepository<Paper>();
        }

#pragma warning disable 1591
        public IEnumerable<Paper> Gets(Expression<Func<Paper, bool>> spec = null, Func<IQueryable<Paper>, IQueryable<Paper>> preFilter = null, params Func<IQueryable<Paper>, IQueryable<Paper>>[] postFilters)
        {
            return _paperRepository.Find(spec, preFilter, postFilters);
        }

        public IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<Paper, TOutput>> projector, Expression<Func<Paper, bool>> spec = null, Func<IQueryable<Paper>, IQueryable<Paper>> preFilter = null, params Func<IQueryable<Paper>, IQueryable<Paper>>[] postFilters)
        {
            return _paperRepository.FindAs(projector, spec, preFilter, postFilters);
        }

        public Paper Get(int id)
        {
            return _paperRepository.One(a => a.PaperId == id);
        }

        public void Create(Paper paper)
        {
            _paperRepository.Create(paper);
        }

        public void Update(Paper paper)
        {
            _paperRepository.Update(paper);
        }

        public void Delete(Paper paper)
        {
            _paperRepository.Delete(paper);
        }

        public bool Exist(Expression<Func<Paper, bool>> spec)
        {
            return _paperRepository.Any(spec);
        }

        public int Count(Expression<Func<Paper, bool>> spec = null)
        {
            return _paperRepository.Count(spec);
        }
    }
}
