using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// 
    /// </summary>
    public class DoctypeFormCatalogDal : DataAccessBase, IDoctypeFormCatalogDal
    {
        private readonly IRepository<DoctypeFormCatalog> _doctypeCatalogRepository;

        /// <summary>
        /// Contructor <see cref="DoctypeFormCatalogDal"/>
        /// </summary>
        /// <param name="context">Customer context.</param>
        public DoctypeFormCatalogDal(IDbCustomerContext context)
            : base(context)
        {
            _doctypeCatalogRepository = context.GetRepository<DoctypeFormCatalog>();
        }

        /// <summary> Tienbv 081112
        /// Cập nhật catalog vào bảng quan hệ doctype - form - catalog.
        /// </summary>
        /// <param name="catalogIds">The catalog id list.</param>
        /// <param name="formId">The form id.</param>
        /// <param name="doctypeId">The doctype id.</param>
        public void Add(IEnumerable<Guid> catalogIds, Guid formId, Guid doctypeId)
        {
            var existedCatalogs = _doctypeCatalogRepository.Find(i => i.DoctypeId == doctypeId && i.FormId == formId).Select(i => i.CatalogId).ToList();
            var newCatalogIds = catalogIds.Where(c => !existedCatalogs.Contains(c)).Select(c => c);
            foreach (var catalogId in newCatalogIds)
            {
                var newItm = new DoctypeFormCatalog
                                 {
                                     CatalogId = catalogId,
                                     DoctypeId = doctypeId,
                                     FormId = formId
                                 };
                _doctypeCatalogRepository.Create(newItm);
            }
        }

        /// <summary> TienBV 261012
        /// Kiểm tra danh mục đã được sử dụng hay chưa.
        /// </summary>
        /// <remarks>
        ///     <para>- Kiểm tra trong bảng doctype-form-catalog. </para>
        /// </remarks>
        /// <param name="catalogId">the catalog id.</param>
        /// <returns></returns>
        public bool IsUsed(Guid catalogId)
        {
            return _doctypeCatalogRepository.Any(c => c.CatalogId == catalogId);
        }

        /// <summary> Tienbv 081112
        /// Lấy danh sách các record quan hệ giữa doctype - form - catalog theo điều kiện truyền vào.
        /// </summary>
        /// <param name="spec">the spec</param>
        public IEnumerable<DoctypeFormCatalog> Gets(Expression<Func<DoctypeFormCatalog, bool>> spec)
        {
            return _doctypeCatalogRepository.Find(spec);
        }

        /// <summary> Tienbv 091112
        /// <para></para> Xóa các catalog trong form.
        /// <para></para> Sử dụng khi xóa một mẫu form chưa được sử dụng.
        /// </summary>
        /// <param name="formId">The form id.</param>
        public void DeleteCatalogs(Guid formId)
        {
            var catalogs = _doctypeCatalogRepository.Find(c => c.FormId == formId);
            foreach(var catalog in catalogs)
            {
                _doctypeCatalogRepository.Delete(catalog);
            }
        }
    }
}
