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
    /// Interface : ICatalogDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 010812
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng Catalog trong CSDL
    /// </summary>
    public interface ICatalogDal
    {
        /// <summary> Tienbv 120919
        /// Tạo danh mục mới.
        /// </summary>
        /// <param name="catalog">the catalog object.</param>
        void Create(Catalog catalog);

        /// <summary> Tienbv 120919
        /// Lấy một danh mục.
        /// </summary>
        /// <param name="id">The catalog guid id.</param>
        /// <returns></returns>
        Catalog Get(Guid id);

        /// <summary> Tienbv 121019
        /// Cập nhật danh mục.
        /// </summary>
        /// <param name="catalog">the catalog.</param>
        void Update(Catalog catalog);

        /// <summary> Tienbv 201012
        /// Xóa danh mục.
        /// </summary>
        /// <param name="catalog">the catalog.</param>
        void Detele(Catalog catalog);

        /// <summary> Tienbv 231012
        /// Kiểm tra danh mục đã tồn tại hay chưa.
        /// </summary>
        /// <param name="name">The catalog name.</param>
        /// <returns></returns>
        bool Exist(string name);

        /// <summary> Tienbv 231012
        /// <para>Lấy số lượng danh mục phù hợp với điều kiện truyền vào.</para>
        /// <para>Dùng cho sort and paging.</para>
        /// </summary>
        /// <param name="spec">the spec.</param>
        /// <returns></returns>
        int Count(Expression<Func<Catalog, bool>> spec = null);

        /// <summary> Tienbv 151112
        /// Lấy ra tất cả danh mục theo danh sách id.
        /// </summary>
        /// <param name="ids">The catalog ids</param>
        /// <returns></returns>
        IEnumerable<Catalog> Gets(IEnumerable<Guid> ids);

        /// <summary> Tienbv 231012
        /// <para>Lấy ra tất cả các danh mục phù hợp với điều kiện truyền vào. </para>
        /// <para>Nếu điều kiện bằng null thì sẽ lấy ra tất cả các danh mục.</para>
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện</param>
        /// <returns>Danh sách các danh mục</returns>
        IEnumerable<Catalog> Gets(Expression<Func<Catalog, bool>> spec = null,
                                    Func<IQueryable<Catalog>, IQueryable<Catalog>> preFilter = null,
                                    params Func<IQueryable<Catalog>, IQueryable<Catalog>>[] postFilters);

    }
}
