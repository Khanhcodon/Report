using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ResourceDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : IAttachmentDal
    /// Create Date : 010812
    /// Modify Date : 140313
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng Attachment trong CSDL
    /// </summary>
    public class AttachmentDal : DataAccessBase, IAttachmentDal
    {
        private readonly IRepository<Attachment> _attachmentRepository;

        /// <summary>
        /// Khởi tạo class <see cref="AttachmentDal"/>.
        /// </summary>
        /// <param name="context">Customer context</param>
        public AttachmentDal(IDbCustomerContext context)
            : base(context)
        {
            _attachmentRepository = Context.GetRepository<Attachment>();
        }

#pragma warning disable 1591
        public IEnumerable<Attachment> Gets(Expression<Func<Attachment, bool>> spec = null)
        {
            return _attachmentRepository.Find(spec);
        }

        public Attachment Get(int id)
        {
            return _attachmentRepository.One(id);
        }

        public void Create(Attachment attachment)
        {
            _attachmentRepository.Create(attachment);
        }

        public void Update(Attachment attachment)
        {
            _attachmentRepository.Update(attachment);
        }

        public void Delete(Attachment attachment)
        {
            _attachmentRepository.Delete(attachment);
        }
    }
}
