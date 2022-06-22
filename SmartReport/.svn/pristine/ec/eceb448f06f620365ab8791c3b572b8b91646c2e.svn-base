using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : StoreCodeDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : IStoreCodeDal
    /// Create Date : 011012
    /// Author      : GiangPN
    /// Description : DAL tương ứng với bảng StoreCode trong CSDL
    /// </summary>
    public class StoreCodeDal : DataAccessBase, IStoreCodeDal
    {
        private readonly IRepository<StoreCode> _storeCodeRepository;
        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="context">Customer context</param>
        public StoreCodeDal(IDbCustomerContext context)
            : base(context)
        {
            _storeCodeRepository = Context.GetRepository<StoreCode>();
        }
        #pragma warning disable 1591

        public IEnumerable<StoreCode> Gets(Expression<Func<StoreCode, bool>> spec = null)
        {
            return _storeCodeRepository.Find(spec);
        }

        public IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<StoreCode, TOutput>> projector, Expression<Func<StoreCode, bool>> spec = null)
        {
            return _storeCodeRepository.FindAs(projector, spec);
        }

        public void Create(StoreCode storeCode)
        {
            _storeCodeRepository.Create(storeCode);
        }

        public void Create(IEnumerable<StoreCode> storeCodes)
        {
            foreach (var storeCode in storeCodes)
            {
                _storeCodeRepository.Create(storeCode, false);
            }
            Context.SaveChanges();
        }

        public void Delete(StoreCode storeCode)
        {
            _storeCodeRepository.Delete(storeCode);
        }

        public void Delete(IEnumerable<StoreCode> storeCodes)
        {
            foreach (var storeCode in storeCodes)
            {
                _storeCodeRepository.Delete(storeCode, false);
            }
            Context.SaveChanges();
        }
    }
}
