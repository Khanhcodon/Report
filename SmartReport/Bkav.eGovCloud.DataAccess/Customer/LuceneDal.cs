using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : LuceneDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : ILuceneDal
    /// Create Date : 280313
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng Lucene trong CSDL
    /// </summary>
    public class LuceneDal : DataAccessBase, ILuceneDal
    {
        private readonly IRepository<Lucene> _luceneRepository;
        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="context">Customer context</param>
        public LuceneDal(IDbCustomerContext context) : base(context)
        {
            _luceneRepository = Context.GetRepository<Lucene>();
        }
#pragma warning disable 1591
        public IEnumerable<Lucene> Gets(Expression<Func<Lucene, bool>> spec = null)
        {
            return _luceneRepository.Find(spec);
        }

        public Lucene Get(int id)
        {
            return _luceneRepository.One(id);
        }

        public Lucene Get(Expression<Func<Lucene, bool>> spec)
        {
            return _luceneRepository.One(spec);
        }

        public void Create(Lucene lucene)
        {
            _luceneRepository.Create(lucene);
        }

        public void Create(IEnumerable<Lucene> lucene)
        {
            foreach (var item in lucene)
            {
                _luceneRepository.Create(item, false);
            }
            Context.SaveChanges();
        }

        public void Update(Lucene lucene)
        {
            _luceneRepository.Update(lucene);
        }

        public void Delete(Lucene lucene)
        {
            _luceneRepository.Delete(lucene);
        }
    }
}
