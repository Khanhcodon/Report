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
    /// Class : StorePrivateDocumentCopyDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : IStorePrivateUserDal
    /// Create Date : 081013
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng StorePrivateDocumentCopy trong CSDL
    /// </summary>
    public class StorePrivateDocumentCopyDal : DataAccessBase, IStorePrivateDocumentCopyDal
    {
        private readonly IRepository<StorePrivateDocumentCopy> _storePrivateDocumentCopyRepository;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="context">Customer context</param>
        public StorePrivateDocumentCopyDal(IDbCustomerContext context)
            : base(context)
        {
            _storePrivateDocumentCopyRepository = Context.GetRepository<StorePrivateDocumentCopy>();
        }

#pragma warning disable 1591
        public IEnumerable<StorePrivateDocumentCopy> Gets(Expression<Func<StorePrivateDocumentCopy, bool>> spec = null)
        {
            return _storePrivateDocumentCopyRepository.Find(spec);
        }

        public StorePrivateDocumentCopy Get(Expression<Func<StorePrivateDocumentCopy, bool>> spec = null)
        {
            return _storePrivateDocumentCopyRepository.One(spec);
        }

        public void Create(StorePrivateDocumentCopy storePrivateDocumentCopy)
        {
            _storePrivateDocumentCopyRepository.Create(storePrivateDocumentCopy);
        }

        public void Create(IEnumerable<StorePrivateDocumentCopy> storePrivateDocumentCopysCopies)
        {
            foreach (var storePrivateDocumentCopy in storePrivateDocumentCopysCopies)
            {
                _storePrivateDocumentCopyRepository.Create(storePrivateDocumentCopy, false);
            }
            Context.SaveChanges();
        }

        public void Delete(StorePrivateDocumentCopy storePrivateDocumentCopy)
        {
            _storePrivateDocumentCopyRepository.Delete(storePrivateDocumentCopy);
        }

        public void Delete(IEnumerable<StorePrivateDocumentCopy> storePrivateDocumentCopysCopies)
        {
            foreach (var storePrivateDocumentCopy in storePrivateDocumentCopysCopies)
            {
                _storePrivateDocumentCopyRepository.Delete(storePrivateDocumentCopy, false);
            }
            Context.SaveChanges();
        }

        public IQueryable<StorePrivateDocumentCopy> Raw()
        {
            return _storePrivateDocumentCopyRepository.Raw;
        }
    }
}
