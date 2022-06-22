using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Interface : IDoctypeFormCatalogDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 081112
    /// Author      : TienBV
    /// Description : DAL tương ứng với bảng Catalog trong CSDL
    /// </summary>
    public interface IDoctypeFormCatalogDal
    {
        /// <summary> Tienbv 081112
        /// <para></para> Cập nhật catalog vào bảng quan hệ doctype - form - catalog.
        /// <para></para> Có kiểm tra loại trừ những catalog đã có trước khi thêm.
        /// </summary>
        /// <param name="catalogIds">The catalog ids.</param>
        /// <param name="formId">The form id.</param>
        /// <param name="doctypeId">The doctype id.</param>
        void Add(IEnumerable<Guid> catalogIds, Guid formId, Guid doctypeId);

        /// <summary> TienBV 261012
        /// Kiểm tra danh mục đã được sử dụng hay chưa.
        /// </summary>
        /// <remarks>
        ///     <para>- Kiểm tra trong bảng doctype-form-catalog. </para>
        /// </remarks>
        /// <param name="catalogId">the catalog id.</param>
        /// <returns></returns>
        bool IsUsed(Guid catalogId);

        /// <summary> Tienbv 081112
        /// Lấy danh sách các record quan hệ giữa doctype - form - catalog theo điều kiện truyền vào.
        /// </summary>
        /// <param name="spec">the spec</param>
        IEnumerable<DoctypeFormCatalog> Gets(Expression<Func<DoctypeFormCatalog, bool>> spec);

        /// <summary> Tienbv 091112
        /// <para></para> Xóa các catalog trong form.
        /// <para></para> Sử dụng khi xóa một mẫu form chưa được sử dụng.
        /// </summary>
        /// <param name="formId">The form id.</param>
        void DeleteCatalogs(Guid formId);
    }
}
