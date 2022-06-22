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
    /// Class : StoreDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : IStoreDal
    /// Create Date : 270612
    /// Author      : TrungVH
    /// Modify Date : 180912
    /// Editor      : GiangPN
    /// Resons: Thêm các hàm nghiệp vụ cho lớp.
    /// Description : DAL tương ứng với bảng Store trong CSDL
    /// </summary>
    public class StoreDal : DataAccessBase, IStoreDal
    {

         private readonly IRepository<Store> _storeRepository;

        /// <summary>
        /// Khởi tạo class  <see cref="StoreDal"/>.
        /// </summary>
        /// <param name="context">Customer context</param>
        public StoreDal(IDbCustomerContext context)
            : base(context)
        {
            _storeRepository = Context.GetRepository<Store>();
        }

        #pragma warning disable 1591
        public IEnumerable<Store> Gets(Expression<Func<Store, bool>> spec = null, Func<IQueryable<Store>, IQueryable<Store>> preFilter = null, params Func<IQueryable<Store>, IQueryable<Store>>[] postFilters)
        {
            return _storeRepository.Find(spec, preFilter, postFilters);
        }

        public IEnumerable<Store> Gets(Expression<Func<Store, bool>> spec = null)
        {
            return _storeRepository.Find(spec);
        }

        public IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<Store, TOutput>> projector, Expression<Func<Store, bool>> spec = null)
        {
            return _storeRepository.FindAs(projector, spec);
        }

        public Store Get(int storeId)
        {
            return _storeRepository.One(r => r.StoreId == storeId);
        }

        public void Create(Store store)
        {
            _storeRepository.Create(store);
        }

        public void Update(Store store)
        {
            _storeRepository.Update(store);
        }

        public void Delete(Store store)
        {
            _storeRepository.Delete(store);
        }

        public bool Exist(Expression<Func<Store, bool>> spec = null)
        {
            return _storeRepository.Any(spec);
        }
        public int Count(Expression<Func<Store, bool>> spec = null)
        {
            return _storeRepository.Count(spec);
        }
    }
}
