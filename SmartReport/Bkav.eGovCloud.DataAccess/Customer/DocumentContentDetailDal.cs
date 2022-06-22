using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocumentContentDetailDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : IDocumentContentDetailDal
    /// Create Date : 240214
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng DocumentContentDetailDal trong CSDL
    /// </summary>
    public class DocumentContentDetailDal : DataAccessBase, IDocumentContentDetailDal
    {
        private readonly IRepository<DocumentContentDetail> _documentContentDetailRepository;
        /// <summary>
        /// Khởi tạo class <see cref="AttachmentDetailDal"/>.
        /// </summary>
        /// <param name="context">Customer context</param>
        public DocumentContentDetailDal(IDbCustomerContext context)
            : base(context)
        {
            _documentContentDetailRepository = Context.GetRepository<DocumentContentDetail>();
        }

#pragma warning disable 1591

        public IEnumerable<DocumentContentDetail> Gets(Expression<Func<DocumentContentDetail, bool>> spec = null)
        {
            return _documentContentDetailRepository.Find(spec);
        }

        public DocumentContentDetail Get(int id)
        {
            return _documentContentDetailRepository.One(id);
        }

        public void Create(DocumentContentDetail documentContentDetail)
        {
            _documentContentDetailRepository.Create(documentContentDetail);
        }

        public void Update(DocumentContentDetail documentContentDetail)
        {
            _documentContentDetailRepository.Update(documentContentDetail);
        }

        public void Delete(DocumentContentDetail documentContentDetail)
        {
            _documentContentDetailRepository.Delete(documentContentDetail);
        }

        public bool Exist(Expression<Func<DocumentContentDetail, bool>> spec)
        {
            return _documentContentDetailRepository.Any(spec);
        }
    }
}
