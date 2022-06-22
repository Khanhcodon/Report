using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : StorePrivateUserDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : IStorePrivateUserDal
    /// Create Date : 081013
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng StorePrivateUser trong CSDL
    /// </summary>
    public class StorePrivateUserDal : DataAccessBase, IStorePrivateUserDal
    {
        private readonly IRepository<StorePrivateUser> _storePrivateUserRepository;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="context">Customer context</param>
        public StorePrivateUserDal(IDbCustomerContext context)
            : base(context)
        {
            _storePrivateUserRepository = Context.GetRepository<StorePrivateUser>();
        }

#pragma warning disable 1591
        public IEnumerable<StorePrivateUser> Gets(Expression<Func<StorePrivateUser, bool>> spec = null)
        {
            return _storePrivateUserRepository.Find(spec);
        }

        public void Create(StorePrivateUser storePrivateUser)
        {
            _storePrivateUserRepository.Create(storePrivateUser);
        }

        public void Create(IEnumerable<StorePrivateUser> storePrivateUsers)
        {
            foreach (var storePrivateUser in storePrivateUsers)
            {
                _storePrivateUserRepository.Create(storePrivateUser, false);
            }
            Context.SaveChanges();
        }

        public void Delete(StorePrivateUser storePrivateUser)
        {
            _storePrivateUserRepository.Delete(storePrivateUser);
        }

        public void Delete(IEnumerable<StorePrivateUser> storePrivateUsers)
        {
            foreach (var storePrivateUser in storePrivateUsers)
            {
                _storePrivateUserRepository.Delete(storePrivateUser, false);
            }
            Context.SaveChanges();
        }
    }
}
