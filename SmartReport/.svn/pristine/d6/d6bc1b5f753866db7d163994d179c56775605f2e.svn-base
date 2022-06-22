using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocTypeStoreDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : IDocTypeStoreDal
    /// Create Date : 290912
    /// Author      : GiangPN
    /// Description : DAL tương ứng với bảng DocTypeStore trong CSDL
    /// </summary>
    public class DocTypeStoreDal : DataAccessBase, IDocTypeStoreDal
    {
        private readonly IRepository<DocTypeStore> _docTypeStoreRepository;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="context">Customer context</param>
        public DocTypeStoreDal(IDbCustomerContext context)
            : base(context)
        {
            _docTypeStoreRepository = Context.GetRepository<DocTypeStore>();
        }

        #pragma warning disable 1591

        public IEnumerable<DocTypeStore> Gets(Expression<Func<DocTypeStore, bool>> spec = null)
        {
            return _docTypeStoreRepository.Find(spec);
        }

        public void Create(DocTypeStore docTypeStore)
        {
            _docTypeStoreRepository.Create(docTypeStore);
        }

        public void Create(IEnumerable<DocTypeStore> docTypeStores)
        {
            foreach (var docTypeStore in docTypeStores)
            {
                _docTypeStoreRepository.Create(docTypeStore, false);
            }
            Context.SaveChanges();
        }

        public void Delete(DocTypeStore docTypeStore)
        {
            _docTypeStoreRepository.Delete(docTypeStore);
        }

        public void Delete(IEnumerable<DocTypeStore> docTypeStores)
        {
            foreach (var docTypeStore in docTypeStores)
            {
                _docTypeStoreRepository.Delete(docTypeStore, false);
            }
            Context.SaveChanges();
        }
    }
}
