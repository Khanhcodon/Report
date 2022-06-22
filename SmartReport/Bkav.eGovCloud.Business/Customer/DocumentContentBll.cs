using System;
using System.Collections.Generic;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Customer;
namespace Bkav.eGovCloud.Business.Customer
{
    /// <author>
    /// <para> Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para> Project: eGov Cloud v1.0</para>
    /// <para> Interface : DocumentContentBll - public - Bll</para>
    /// <para> Access Modifiers: IDocumentContentDal</para>
    /// <para> Create Date : 290113</para>
    /// <para> Author      : TienBV</para>
    /// </author>
    /// <summary>
    /// <para> Description : Bll tương ứng với bảng DocumentContent trong CSDL </para>
    /// </summary>
    public class DocumentContentBll : ServiceBase
    {
        private readonly IRepository<DocumentContent> _documentContentRepository;
        private readonly IRepository<DocumentContentDetail> _documentContentDetailRepository;

        /// <summary>
        /// C'tor
        /// </summary>
        /// <param name="context">Context</param>
        public DocumentContentBll(IDbCustomerContext context)
            : base(context)
        {
            _documentContentRepository = Context.GetRepository<DocumentContent>();
            _documentContentDetailRepository = Context.GetRepository<DocumentContentDetail>();
        }

        /// <summary>
        /// <para>Lấy danh sách nội dung theo id văn bản, hồ sơ. Kết quả chỉ để đọc</para>
        /// </summary>
        /// <param name="id">Id văn bản, hồ sơ</param>
        /// <returns></returns>
        public IEnumerable<DocumentContent> GetsByDocumentId(Guid id)
        {
            var result = _documentContentRepository.GetsReadOnly(dc => dc.DocumentId == id);
            foreach (var dc in result)
            {
                dc.DocumentContentDetails = null;
            }

            return result;
        }

        /// <summary>
        /// <para>Lấy nội dung hồ sơ theo id</para>
        /// (Tienbv@bkav.com - 290113)
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public DocumentContent Get(int id)
        {
            return _documentContentRepository.Get(id);
        }

        /// <summary>
        /// Cập nhật nội dung hồ sơ
        /// </summary>
        /// <param name="entity"></param>
        public void Update(DocumentContent entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Trả về các phiên bản của documentcontent từ phiên bản mới nhất
        /// </summary>
        /// <param name="documentContentId"></param>
        /// <param name="maxVersion">Version phiên bản hiện tại</param>
        /// <returns></returns>
        public IEnumerable<DocumentContentDetail> GetDetails(int documentContentId, int maxVersion)
        {
            return _documentContentDetailRepository.Gets(true, d => d.DocumentContentId == documentContentId && d.Version < maxVersion);
        }
    }
}
