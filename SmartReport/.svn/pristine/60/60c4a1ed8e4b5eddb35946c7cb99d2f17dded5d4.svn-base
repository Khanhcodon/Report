using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// <para></para> Bkav Corp. - BSO - eGov - eOffice team
    /// <para></para> Project: eGov Cloud v1.0
    /// <para></para> Class : CatalogDal - public - DAL
    /// <para></para> Access Modifiers: 
    /// <para></para>     * Inherit : DataAccessBase
    /// <para></para>     * Implement : ICatalogDal
    /// <para></para> Create Date : 010812
    /// <para></para> Author      : TrungVH
    /// <para></para> Description : DAL tương ứng với bảng Catalog trong CSDL
    /// </summary>
    public class CatalogDal : DataAccessBase, ICatalogDal
    {
        private readonly IRepository<Catalog> _catalogRepository;

        /// <summary>
        /// Khởi tạo class <see cref="CatalogDal"/>.
        /// </summary>
        /// <param name="context">Customer context</param>
        public CatalogDal(IDbCustomerContext context)
            : base(context)
        {
            _catalogRepository = Context.GetRepository<Catalog>();
        }
        
        /// <summary> Tienbv 191012
        /// Tạo danh mục mới.
        /// </summary>
        /// <param name="catalog">the catalog object.</param>
        public void Create(Catalog catalog)
        {
            _catalogRepository.Create(catalog);
        }

        /// <summary> Tienbv 191012
        /// Lấy danh mục.
        /// </summary>
        /// <param name="guidId">The catalog guid id.</param>
        /// <returns></returns>
        public Catalog Get(Guid guidId)
        {
            return _catalogRepository.One(c => c.CatalogId == guidId);
        }

        /// <summary> Tienbv 191012
        /// Cập nhật danh mục.
        /// </summary>
        /// <param name="catalog">the catalog.</param>
        public void Update(Catalog catalog)
        {
            _catalogRepository.Update(catalog);
        }

        /// <summary> Tienbv 201012
        /// Xóa danh mục.
        /// </summary>
        /// <param name="catalog"></param>
        public void Detele(Catalog catalog)
        {
            _catalogRepository.Delete(catalog);
        }

        /// <summary> Tienbv 231012
        /// Kiểm tra danh mục đã tồn tại hay chưa.
        /// </summary>
        /// <param name="name">The catalog name.</param>
        /// <returns></returns>
        public bool Exist(string name)
        {
            return _catalogRepository.Any(c => c.CatalogName == name);
        }

        /// <summary> Tienbv 231012
        /// <para>Lấy số lượng danh mục phù hợp với điều kiện truyền vào.</para>
        /// <para>Dùng cho sort and paging.</para>
        /// </summary>
        /// <param name="spec">the spec.</param>
        /// <returns></returns>
        public int Count(Expression<Func<Catalog, bool>> spec = null)
        {
            return _catalogRepository.Count(spec);
        }

        /// <summary> Tienbv 151112
        /// Lấy ra tất cả danh mục theo danh sách id.
        /// </summary>
        /// <param name="ids">The catalog ids</param>
        /// <returns></returns>
        public IEnumerable<Catalog> Gets(IEnumerable<Guid> ids)
        {
            var result = new List<Catalog>();
            foreach (var id in ids)
            {
                var catalog = _catalogRepository.One(id);
                catalog.CatalogValues = catalog.CatalogValues.OrderBy(v => v.Order).ToList();
                result.Add(catalog);
            }
            return result;
        }

        /// <summary> Tienbv 231012
        /// <para>Lấy ra tất cả các danh mục phù hợp với điều kiện truyền vào. </para>
        /// <para>Nếu điều kiện bằng null thì sẽ lấy ra tất cả các danh mục.</para>
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện</param>
        /// <returns>Danh sách các danh mục</returns>
        public IEnumerable<Catalog> Gets(Expression<Func<Catalog, bool>> spec = null, Func<System.Linq.IQueryable<Catalog>, System.Linq.IQueryable<Catalog>> preFilter = null, params Func<System.Linq.IQueryable<Catalog>, System.Linq.IQueryable<Catalog>>[] postFilters)
        {
            return _catalogRepository.Find(spec, preFilter, postFilters);
        }
    }
}
