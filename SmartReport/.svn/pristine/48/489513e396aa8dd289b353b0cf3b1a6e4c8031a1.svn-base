using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <author>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para> 
    /// <para>Project: eGov Cloud v1.0</para> 
    /// <para>Class : CatalogBll - public - BLL</para> 
    /// <para>Access Modifiers: </para> 
    /// <para>Create Date : 181012</para> 
    /// <para>Author      : TienBV</para> 
    /// <para>Description : Quản lý danh mục egov.</para> 
    /// </author>
    /// <summary> 
    /// <para> Quản lý những nhóm giá trị sẽ bind ra các select box khi soạn form động.</para>
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    public class IndicatorCatalogBll : ServiceBase
    {
        private readonly IRepository<IndicatorCatalog> _catalogRepository;
        private readonly ResourceBll _resourceService;

        /// <summary>
        /// Khởi tạo class <see cref="IndicatorCatalogBll"/>.
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="resourceService"></param>
        //TODO: TienBV - Tham số đầu vào ICatalogValueDal catalogValueDal nên thay thành CatalogValueBll
        public IndicatorCatalogBll(IDbCustomerContext context, ResourceBll resourceService)
            : base(context)
        {
            _catalogRepository = Context.GetRepository<IndicatorCatalog>();
            _resourceService = resourceService;
        }

        /// <summary> Tienbv 190912
        /// Tạo danh mục mới.
        /// </summary>
        /// <param name="catalog">the catalog object.</param>
        public void Create(IndicatorCatalog catalog)
        {
            if (catalog == null)
            {
                throw new ArgumentNullException("IndicatorCatalog");
            }
            if (_catalogRepository.Exist(c => c.IndicatorCatalogName == catalog.IndicatorCatalogName))
            {
                throw new EgovException("Danh mục đã tồn tại!");
            }
          
            _catalogRepository.Create(catalog);
            Context.SaveChanges();
        }

        /// <summary>
        /// Tạo danh mục mới.
        /// </summary>
        /// <param name="catalogs">the catalog object.</param>
        /// <param name="ignoreExist"> True: Bỏ qua kiểm tra tồn tại; False: không kiểm tra tồn tại</param>
        public void Create(IEnumerable<IndicatorCatalog> catalogs, bool ignoreExist = false)
        {
            if (catalogs == null || !catalogs.Any())
            {
                throw new ArgumentNullException("IndicatorCatalog");
            }

            var names = catalogs.Select(c => c.IndicatorCatalogName);
            var exists = _catalogRepository.GetsAs(p => p.IndicatorCatalogName, p => names.Contains(p.IndicatorCatalogName));
            if (exists != null && exists.Any())
            {
                if (!ignoreExist || exists.Count() == catalogs.Count())
                {
                    throw new EgovException("Danh mục chỉ tiêu đã tồn tại!");
                }

                var list = catalogs.Where(p => !exists.Contains(p.IndicatorCatalogName));
                if (list == null || !list.Any())
                {
                    throw new EgovException("Danh mục chỉ tiêu đã tồn tại!");
                }
                Create(list);
            }
            else
            {
                Create(catalogs);
            }
        }

        private void Create(IEnumerable<IndicatorCatalog> catalogs)
        {
            foreach (var catalog in catalogs)
            { 
                _catalogRepository.Create(catalog);
            }
            Context.SaveChanges();
        }

        /// <summary> Tienbv 121019
        /// Lấy danh mục.
        /// </summary>
        /// <param name="id">the catalog id.</param>
        /// <returns>The Catalog object.</returns>
        public IndicatorCatalog Get(int id)
        {
            var result = _catalogRepository.Get(id);
            return result;
        }

        /// <summary> Tienbv 261012
        /// Cập nhật danh mục.
        /// </summary>
        /// <param name="catalog">danh mục.</param>
        public void Update(IndicatorCatalog catalog)
        {
            if (catalog == null)
            {
                throw new ArgumentNullException("catalog");
            }
            
            Context.SaveChanges();
        }

        /// <summary> TienBV 201012
        /// <para> Xóa danh mục.</para>
        /// <para> Note:</para>
        /// <para> - Nếu danh mục và các đối đượng của nó đã được sử dụng trong form, hồ sơ thì không được xóa.</para>
        /// <para> - Khi xóa danh mục sẽ xóa hết tất cả các đối tượng của danh mục đó.</para>
        /// </summary>
        /// <param name="catalog">the catalog.</param>
        public void Detele(IndicatorCatalog catalog)
        {
            _catalogRepository.Delete(catalog);
            Context.SaveChanges();
        }

        /// <summary> TienBV 231012
        /// Lấy ra tất cả các danh mục có sắp xếp. Kết quả chỉ để đọc
        /// </summary>
        /// <param name="sortBy">Sắp xếp theo</param>
        /// <param name="isDescending">Sắp xếp từ lớn đến nhỏ: true, ngược lại false</param>
        /// <returns>Danh sách các tài nguyên đã được phân trang</returns>
        public IEnumerable<IndicatorCatalog> Gets(string sortBy = "", bool isDescending = false)
        {
            return _catalogRepository.GetsReadOnly(null, Context.Filters.CreateSort<IndicatorCatalog>(isDescending, sortBy));
        }

        /// <summary> Tienbv 011112
        /// Lấy ra tất cả catalog theo điều kiện kỹ thuật truyền vào. Kết quả chỉ để đọc
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <returns></returns>
        public IEnumerable<IndicatorCatalog> Gets(Expression<Func<IndicatorCatalog, bool>> spec = null)
        {
            return _catalogRepository.GetsReadOnly(spec);
        }
    }
}
