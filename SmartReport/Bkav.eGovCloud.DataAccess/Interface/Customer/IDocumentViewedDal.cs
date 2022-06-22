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
    /// Interface : IDocumentViewedDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 100513
    /// Author      : TienBV
    /// Description : DAL tương ứng với bảng DocumentViewed trong CSDL
    /// Lưu danh sách công văn người đã xem
    /// </summary>
    public interface IDocumentViewedDal
    {
        /// <summary>
        /// Kiểm tra user đã xem văn bản hiện tại chưa.
        /// </summary>
        /// <param name="documentCopyId">Documentcopy Id</param>
        /// <param name="userId">User id.</param>
        /// <returns>
        /// <para> - True: nếu đã xem.</para>
        /// <para> - False: nếu chưa xem.</para>
        /// </returns>
        bool IsViewed(int documentCopyId, int userId);

        /// <summary>
        /// Thêm vào danh sách đã xem
        /// </summary>
        /// <param name="entity">Entity</param>
        void Create(DocumentViewed entity);

        /// <summary>
        /// Xóa khỏi danh sách đã xem
        /// </summary>
        /// <param name="entity"></param>
        void Delete(DocumentViewed entity);
    }
}
