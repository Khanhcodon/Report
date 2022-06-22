using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Interface : IDocTypeStoreDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 290912
    /// Author      : GiangPN
    /// Description : DAL tương ứng với bảng DocTypeStore trong CSDL
    /// </summary>
    public interface IDocTypeStoreDal
    {
        /// <summary>
        /// Lấy ra tất cả các mapping giữa loại hồ sơ và sổ hồ sơ phù hợp với điều kiện truyền vào. Nếu điều kiện bằng null thì sẽ lấy ra tất cả mapping
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Danh sách mapping</returns>
        IEnumerable<DocTypeStore> Gets(Expression<Func<DocTypeStore, bool>> spec = null);

        /// <summary>
        /// Tạo mới mapping giữa loại hồ sơ và sổ hồ sơ
        /// </summary>
        /// <param name="docTypeStore">Entity DocTypeStore</param>
        void Create(DocTypeStore docTypeStore);

        /// <summary>
        /// Tạo mới nhiều mapping giữa loại hồ sơ và sổ hồ sơ
        /// </summary>
        /// <param name="docTypeStores">Danh sách entity DocTypeStore</param>
        void Create(IEnumerable<DocTypeStore> docTypeStores);

        /// <summary>
        /// Xóa mapping giữa loại hồ sơ và sổ hồ sơ
        /// </summary>
        /// <param name="docTypeStore">Entity DocTypeStore</param>
        void Delete(DocTypeStore docTypeStore);

        /// <summary>
        /// Xóa nhiều mapping giữa loại hồ sơ và sổ hồ sơ
        /// </summary>
        /// <param name="docTypeStores">Danh sách entity DocTypeStore</param>
        void Delete(IEnumerable<DocTypeStore> docTypeStores);
    }
}
