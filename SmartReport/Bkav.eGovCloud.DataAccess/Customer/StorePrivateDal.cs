using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : StorePrivateDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : IStorePrivateDal
    /// Create Date : 270612
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng StorePrivate trong CSDL
    /// </summary>
    public class StorePrivateDal : DataAccessBase, IStorePrivateDal
    {
        private readonly IRepository<StorePrivate> _storePrivateRepository;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="context">Customer context</param>
        public StorePrivateDal(IDbCustomerContext context)
            : base(context)
        {
            _storePrivateRepository = Context.GetRepository<StorePrivate>();
        }

#pragma warning disable 1591
        public IEnumerable<StorePrivate> Gets(Expression<Func<StorePrivate, bool>> spec = null, Func<IQueryable<StorePrivate>, IQueryable<StorePrivate>> preFilter = null, params Func<IQueryable<StorePrivate>, IQueryable<StorePrivate>>[] postFilters)
        {
            return _storePrivateRepository.Find(spec, preFilter, postFilters);
        }

        public IEnumerable<T> GetsAs<T>(Expression<Func<StorePrivate, T>> projector, Expression<Func<StorePrivate, bool>> spec = null, Func<IQueryable<StorePrivate>, IQueryable<StorePrivate>> preFilter = null, params Func<IQueryable<StorePrivate>, IQueryable<StorePrivate>>[] postFilters)
        {
            return _storePrivateRepository.FindAs(projector, spec, preFilter, postFilters);
        }

        public IEnumerable<IDictionary<string, object>> GetDocumentsByStorePrivateId(string query, params object[] parameters)
        {
            return Context.RawQuery(query, parameters) as IEnumerable<IDictionary<string, object>>;
        }

        public StorePrivate Get(int id)
        {
            return _storePrivateRepository.One(id);
        }

        public StorePrivate Get(int id, int userId)
        {
            return _storePrivateRepository.One(s => s.StorePrivateId == id && s.CreatedByUserId == userId && s.Status != (byte)StorePrivateStatus.IsDelete);
        }

        public void Create(StorePrivate storeprivate)
        {
            _storePrivateRepository.Create(storeprivate);
        }

        public void Update(StorePrivate storeprivate)
        {
            _storePrivateRepository.Update(storeprivate);
        }

        public void Delete(StorePrivate storeprivate)
        {
            _storePrivateRepository.Delete(storeprivate);
        }

        public bool Exist(Expression<Func<StorePrivate, bool>> spec = null)
        {
            return _storePrivateRepository.Any(spec);
        }
    }
}
