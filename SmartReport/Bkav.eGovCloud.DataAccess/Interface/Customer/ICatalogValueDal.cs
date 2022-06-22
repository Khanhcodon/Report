using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;
namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Interface : ICatalogValueDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 010812
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng CatalogValue trong CSDL
    /// </summary>
    public interface ICatalogValueDal
    {
        /// <summary> TienBV 201012
        /// Lấy danh sách các đối tượng của danh mục hệ thống phù hợp với điều kiện truyền vào.
        /// </summary>
        /// <param name="spec">Điều kiện truyền vào. Nếu = null trả về tất cả đối tượng của danh mục.</param>
        /// <example>
        ///     _catalogValueDal.Gets(c => c.Active);
        /// </example>
        /// <returns>Danh sách các đối tượng phù hợp.</returns>
        IEnumerable<CatalogValue> Gets(Expression<Func<CatalogValue, bool>> spec = null);

        /// <summary> Tienbv 221012
        /// Lấy một giá trị của danh mục theo id.
        /// </summary>
        /// <param name="id">the catalog value id.</param>
        /// <returns></returns>
        CatalogValue Get(Guid id);

        /// <summary> Tienbv 221012
        /// Cập nhật một giá trị của danh mục.
        /// </summary>
        /// <param name="catalogValue">the catalog value obj.</param>
        void Update(CatalogValue catalogValue);

        /// <summary> TienBV 221012
        /// Xóa một đối tượng của danh mục.
        /// </summary>
        /// <param name="catalogValue">the catalog value obj.</param>
        void Delete(CatalogValue catalogValue);
    }
}
