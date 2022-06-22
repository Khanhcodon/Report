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
    ///     * Implement : IAttachmentDetailDal
    /// Create Date : 010812
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng AttachmentDetail trong CSDL
    /// </summary>
    public class AttachmentDetailDal : DataAccessBase , IAttachmentDetailDal
    {
        private readonly IRepository<AttachmentDetail> _attachmentRepository;
        /// <summary>
        /// Khởi tạo class <see cref="AttachmentDetailDal"/>.
        /// </summary>
        /// <param name="context">Customer context</param>
        public AttachmentDetailDal(IDbCustomerContext context) : base(context)
        {
            _attachmentRepository = Context.GetRepository<AttachmentDetail>();
        }

#pragma warning disable 1591
        public IEnumerable<AttachmentDetail> Gets(Expression<Func<AttachmentDetail, bool>> spec = null)
        {
            return _attachmentRepository.Find(spec);
        }

        public AttachmentDetail Get(int id)
        {
            return _attachmentRepository.One(id);
        }

        public void Create(AttachmentDetail attachment)
        {
            _attachmentRepository.Create(attachment);
        }

        public void Update(AttachmentDetail attachment)
        {
            _attachmentRepository.Update(attachment);
        }

        public void Delete(AttachmentDetail attachment)
        {
            _attachmentRepository.Delete(attachment);
        }

        public bool Exist(Expression<Func<AttachmentDetail, bool>> spec)
        {
            return _attachmentRepository.Any(spec);
        }
    }
}
