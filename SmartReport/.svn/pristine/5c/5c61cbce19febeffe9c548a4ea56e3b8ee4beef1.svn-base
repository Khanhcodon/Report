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
    /// Class : KeyWordDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : IKeyWordDal
    /// Create Date : 221113
    /// Author      : DungHV
    /// Description : DAL tương ứng với bảng KeyWord trong CSDL
    /// </summary>
    public class KeyWordDal : DataAccessBase, IKeyWordDal
    {
        private readonly IRepository<KeyWord> _keywordRepository;
        /// <summary>
        /// Khởi tạo class <see cref="KeyWordDal"/>.
        /// </summary>
        /// <param name="context">Admin context</param>
        public KeyWordDal(IDbCustomerContext context)
            : base(context)
        {
            _keywordRepository = Context.GetRepository<KeyWord>();
        }

#pragma warning disable 1591
        public IEnumerable<KeyWord> Gets(Expression<Func<KeyWord, bool>> spec = null,
                                            Func<IQueryable<KeyWord>, IQueryable<KeyWord>> preFilter = null,
                                            params Func<IQueryable<KeyWord>, IQueryable<KeyWord>>[] postFilters)
        {
            return _keywordRepository.Find(spec, preFilter, postFilters);
        }

        public IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<KeyWord, TOutput>> projector, Expression<Func<KeyWord, bool>> spec = null, Func<IQueryable<KeyWord>, IQueryable<KeyWord>> preFilter = null, params Func<IQueryable<KeyWord>, IQueryable<KeyWord>>[] postFilters)
        {
            return _keywordRepository.FindAs(projector, spec, preFilter, postFilters);
        }

        public KeyWord Get(int id)
        {
            return _keywordRepository.One(a => a.KeyWordId == id);
        }

        public void Create(KeyWord keyword)
        {
            _keywordRepository.Create(keyword);
        }

        public void Update(KeyWord keyword)
        {
            _keywordRepository.Update(keyword);
        }

        public void Delete(KeyWord keyword)
        {
            _keywordRepository.Delete(keyword);
        }

        public bool Exist(Expression<Func<KeyWord, bool>> spec)
        {
            return _keywordRepository.Any(spec);
        }

        public int Count(Expression<Func<KeyWord, bool>> spec = null)
        {
            return _keywordRepository.Count(spec);
        }
    }
}
