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
    /// Interface : IStorePrivateDocumentCopyDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 081013
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng StorePrivateDocumentCopy trong CSDL
    /// </summary>
    public interface IStorePrivateDocumentCopyDal
    {
        /// <summary>
        /// Lấy ra tất cả các mapping giữa hồ sơ cá nhân và văn bản hồ sơ. Nếu điều kiện bằng null thì sẽ lấy ra tất cả mapping
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Danh sách mapping</returns>
        IEnumerable<StorePrivateDocumentCopy> Gets(Expression<Func<StorePrivateDocumentCopy, bool>> spec = null);

        /// <summary>
        /// Lấy ra  giữa hồ sơ cá nhân và văn bản hồ sơ.
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Danh sách mapping</returns>
        StorePrivateDocumentCopy Get(Expression<Func<StorePrivateDocumentCopy, bool>> spec = null);

        /// <summary>
        /// Tạo mới mapping giữa hồ sơ cá nhân và văn bản hồ sơ
        /// </summary>
        /// <param name="storePrivateDocumentCopy">Entity storePrivateDocumentCopy</param>
        void Create(StorePrivateDocumentCopy storePrivateDocumentCopy);

        /// <summary>
        /// Tạo mới nhiều mapping giữa hồ sơ cá nhân và văn bản hồ sơ
        /// </summary>
        /// <param name="storePrivateDocumentCopy">Danh sách storePrivateDocumentCopy</param>
        void Create(IEnumerable<StorePrivateDocumentCopy> storePrivateDocumentCopy);

        /// <summary>
        /// Xóa mapping giữa hồ sơ cá nhân và văn bản hồ sơ
        /// </summary>
        /// <param name="storePrivateDocumentCopy">Entity storePrivateDocumentCopy</param>
        void Delete(StorePrivateDocumentCopy storePrivateDocumentCopy);

        /// <summary>
        /// Xóa nhiều mapping giữa hồ sơ cá nhân và văn bản hồ sơ
        /// </summary>
        /// <param name="storePrivateDocumentCopy">Danh sách entity storePrivateDocumentCopy</param>
        void Delete(IEnumerable<StorePrivateDocumentCopy> storePrivateDocumentCopy);

        /// <summary>
        /// Lấy ra tất cả các bản ghi
        /// </summary>
        /// <returns></returns>
        IQueryable<StorePrivateDocumentCopy> Raw();
    }
}
