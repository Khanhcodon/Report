using System.Linq;
using Bkav.eGovCloud.Core.Document;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// BSO  - Phòng 2 - eGov
    /// Project: eGov Cloud - v1.0
    /// [Access Level(Class)] : DocumentCopyDal - public - DAL
    /// Access Modifiers:
    ///     * Inherit   : [Class Name]
    ///     * Implement : [Inteface Name], [Inteface Name], ...
    ///
    /// Create Date : 121225
    /// Author      : TienBV
    /// Description : ...
    /// </summary>
    public class DocumentCopyDal : DataAccessBase, IDocumentCopyDal
    {
        #region private fields

        private readonly IRepository<DocumentCopy> _documentCopyRepository;

        #endregion private fields

        #region c'tor

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="context"></param>
        public DocumentCopyDal(IDbCustomerContext context)
            : base(context)
        {
            _documentCopyRepository = context.GetRepository<DocumentCopy>();
        }

        #endregion c'tor

#pragma warning disable 1591

        #region public methods

        public DocumentCopy Get(Expression<Func<DocumentCopy, bool>> spec)
        {
            return _documentCopyRepository.One(spec);
        }

        public T GetAs<T>(Expression<Func<DocumentCopy, T>> projector, Expression<Func<DocumentCopy, bool>> spec)
        {
            return _documentCopyRepository.OneAs(projector, spec);
        }

        public DocumentCopy Get(int id)
        {
            return _documentCopyRepository.One(id);
        }

        public IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<DocumentCopy, TOutput>> projector, Expression<Func<DocumentCopy, bool>> spec = null, Func<IQueryable<DocumentCopy>, IQueryable<DocumentCopy>> preFilter = null, params Func<IQueryable<DocumentCopy>, IQueryable<DocumentCopy>>[] postFilters)
        {
            return _documentCopyRepository.FindAs(projector, spec, preFilter, postFilters);
        }

        public void Create(DocumentCopy documentCopy)
        {
            _documentCopyRepository.Create(documentCopy);
        }

        public void Update(DocumentCopy documentCopy)
        {
            _documentCopyRepository.Update(documentCopy);
        }

        public void Delete(DocumentCopy documentCopy)
        {
            _documentCopyRepository.Delete(documentCopy);
        }

        public void Delete(IList<DocumentCopy> documentCopys)
        {
            foreach (var documentCopy in documentCopys)
            {
                _documentCopyRepository.Delete(documentCopy, false);
            }
            Context.SaveChanges();
        }

        public IEnumerable<int> GetChildIds(int documentcopyId)
        {
            return _documentCopyRepository.FindAs(d => d.DocumentCopyId, d => d.ParentId == documentcopyId);
        }

        public IEnumerable<DocumentCopy> GetChilds(int documentCopyId)
        {
            return _documentCopyRepository.Find(d => d.ParentId == documentCopyId);
        }

        public IEnumerable<DocumentCopy> Gets(Expression<Func<DocumentCopy, bool>> spec = null, Func<IQueryable<DocumentCopy>, IQueryable<DocumentCopy>> preFilter = null, params Func<IQueryable<DocumentCopy>, IQueryable<DocumentCopy>>[] postFilters)
        {
            return _documentCopyRepository.Find(spec, preFilter, postFilters);
        }

        public IQueryable<DocumentCopy> Raw()
        {
            return _documentCopyRepository.Raw;
        }

        public DocumentCopy GetMain(Guid docId)
        {
            const int documentCopyType = (int)DocumentCopyTypes.XuLyChinh;
            return _documentCopyRepository.One(dc => dc.DocumentCopyType == documentCopyType && dc.DocumentId.Equals(docId));
        }

        #endregion public methods
    }
}