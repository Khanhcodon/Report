using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// <para></para> <para></para> Bkav Corp. - BSO - eGov - eOffice team
    /// <para></para> Project: eGov Cloud v1.0
    /// <para></para> Class : CatalogValueDal - public - DAL
    /// <para></para> Access Modifiers: 
    /// <para></para>     * Inherit : DataAccessBase
    /// <para></para>     * Implement : ICatalogValueDal
    /// <para></para> Create Date : 270612
    /// <para></para> Author      : TrungVH
    /// <para></para> Editor      : TienBV
    /// <para></para> Description : Lớp cung cấp các API quản lý các đối tượng của danh mục.
    /// </summary>
    public class CatalogValueDal : DataAccessBase, ICatalogValueDal
    {
        private readonly IRepository<CatalogValue> _catalogRepository;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="context">Customer context</param>
        public CatalogValueDal(IDbCustomerContext context) : base(context)
        {
            _catalogRepository = context.GetRepository<CatalogValue>();
        }

        /// <summary> TienBV 201012
        /// Lấy danh sách các đối tượng của danh mục hệ thống phù hợp với điều kiện truyền vào.
        /// </summary>
        /// <param name="spec">Điều kiện truyền vào. Nếu = null trả về tất cả đối tượng của danh mục.</param>
        /// <example>
        ///     _catalogValueDal.Gets(c => c.Active);
        /// </example>
        /// <returns>Danh sách các đối tượng phù hợp.</returns>
        public IEnumerable<CatalogValue> Gets(Expression<Func<CatalogValue, bool>> spec = null)
        {
            return _catalogRepository.Find(spec);
        }

        /// <summary> Tienbv 221012
        /// Lấy giá trị của danh mục theo id.
        /// </summary>
        /// <param name="id">the catalogvalue id.</param>
        /// <returns></returns>
        public CatalogValue Get(Guid id)
        {
            return _catalogRepository.One(c => c.CatalogValueId == id);
        }

        /// <summary> Tienbv 221012
        /// Cập nhật một giá trị của danh mục.
        /// </summary>
        /// <param name="catalogValue">the catalog value obj.</param>
        public void Update(CatalogValue catalogValue)
        {
            _catalogRepository.Update(catalogValue);
        }

        /// <summary> TienBV 221012
        /// Xóa một đối tượng của danh mục.
        /// </summary>
        /// <param name="catalogValue">the catalog value obj.</param>
        public void Delete(CatalogValue catalogValue)
        {
            _catalogRepository.Delete(catalogValue);
        }
    }
}
