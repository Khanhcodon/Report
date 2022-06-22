using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <author>
    /// <para> Bkav Corp. - BSO - eGov - eGate Team </para>
    /// <para> Project: eGov Cloud v1.0 </para>
    /// <para> Class : DocumentViewedDal - public - Dal </para>
    /// <para> Access Modifiers: </para>
    /// <para> Create Date : 100513</para>
    /// <para> Author : TienBV@bkav.com </para>
    /// </author>
    /// <summary>
    /// <para> Các hàm mở rộng xử lý danh sách công văn người đã xem </para>
    /// <para> ( TienBV@bkav.com - 100513) </para>
    /// </summary>
    public class DocumentViewedDal : DataAccessBase, IDocumentViewedDal
    {
        #region private fields

        private readonly IRepository<DocumentViewed> _documentViewed;

        #endregion private fields

        #region c'tor

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="context">Customer context</param>
        public DocumentViewedDal(IDbCustomerContext context)
            : base(context)
        {
            _documentViewed = context.GetRepository<DocumentViewed>();


        }
        #endregion c'tor

#pragma warning disable 1591

        #region public methods

        public bool IsViewed(int documentCopyId, int userId)
        {
            return _documentViewed.Any(i => i.DocumentCopyId == documentCopyId && i.UserId == userId);
        }

        public void Create(DocumentViewed entity)
        {
            _documentViewed.Create(entity);
        }

        public void Delete(DocumentViewed entity)
        {
            _documentViewed.Delete(entity);
        }

        #endregion public methods
    }
}