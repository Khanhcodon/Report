using Bkav.eGovCloud.Entities.Customer;
using System;
using System.Linq.Expressions;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocCatalogDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : IDocCatalogDal
    /// Create Date : 270612
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng DocCatalog trong CSDL
    /// </summary>
    public class DocCatalogDal : DataAccessBase, IDocCatalogDal
    {
        private IRepository<DocCatalog> _docCatalogRepository;
        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="context">Customer context</param>
        public DocCatalogDal(IDbCustomerContext context) : base(context)
        {
            _docCatalogRepository = context.GetRepository<DocCatalog>();
        }

#pragma warning disable 1591
        public void Update(Entities.Customer.DocCatalog entity)
        {
            _docCatalogRepository.Update(entity);
        }

        public Entities.Customer.DocCatalog Get(Expression<Func<DocCatalog, bool>> spec)
        {
            return _docCatalogRepository.One(spec);
        }
    }
}
