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
    /// Class : DocumentDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : IDocumentDal
    /// Create Date : 270612
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng Document trong CSDL
    /// </summary>
    public class DocumentDal : DataAccessBase, IDocumentDal
    {
        private readonly IRepository<Document> _documentRepository;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="context">Customer context</param>
        public DocumentDal(IDbCustomerContext context)
            : base(context)
        {
            _documentRepository = Context.GetRepository<Document>();
        }

        #pragma warning disable 1591

        public IEnumerable<Document> Gets(Expression<Func<Document, bool>> spec = null, Func<IQueryable<Document>, IQueryable<Document>> preFilter = null, params Func<IQueryable<Document>, IQueryable<Document>>[] postFilters)
        {
            return _documentRepository.Find(spec, preFilter, postFilters);
        }

        public IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<Document, TOutput>> projector, Expression<Func<Document, bool>> spec = null, Func<IQueryable<Document>, IQueryable<Document>> preFilter = null, params Func<IQueryable<Document>, IQueryable<Document>>[] postFilters)
        {
            return _documentRepository.FindAs(projector, spec, preFilter, postFilters);
        }

        public Document Get(Guid id)
        {
            return _documentRepository.One(id);
        }

        public Document Get(int categoryId)
        {
            return _documentRepository.One(l => l.CategoryId == categoryId);
        }

        public Document Get(string docCode)
        {
            return _documentRepository.One(d => d.DocCode == docCode);
        }

        public void Create(Document document)
        {
            _documentRepository.Create(document);
        }

        public void Update(Document document)
        {
            _documentRepository.Update(document);
        }

        public void Delete(Document document)
        {
            _documentRepository.Delete(document);
        }

        public bool Exist(Expression<Func<Document, bool>> spec)
        {
            return _documentRepository.Any(spec);
        }

        public int Count(Expression<Func<Document, bool>> spec = null)
        {
            return _documentRepository.Count(spec);
        }

        public IQueryable<Document> Raw()
        {
            return _documentRepository.Raw;
        }
    }
}
