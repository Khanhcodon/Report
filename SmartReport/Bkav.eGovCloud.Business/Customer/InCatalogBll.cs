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
    public class InCatalogBll : ServiceBase
    {
        private readonly IRepository<InCatalog> _inCatalogRepository;
        private readonly IRepository<InCatalogValue> _inCatalogValueRepository;
        private readonly ResourceBll _resourceService;

        /// <summary>
        /// Khởi tạo class <see cref="InCatalogBll"/>.
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="resourceService"></param>
        public InCatalogBll(IDbCustomerContext context, ResourceBll resourceService)
            : base(context)
        {
            _inCatalogRepository = Context.GetRepository<InCatalog>();
            _resourceService = resourceService;
        }

        /// <summary>
        /// Tạo danh mục mới.
        /// </summary>
        /// <param name="catalog">the catalog object.</param>
        public void Create(InCatalog catalog)
        {
            if (catalog == null)
            {
                throw new ArgumentNullException("InCatalog");
            }
            if (_inCatalogRepository.Exist(c => c.InCatalogName == catalog.InCatalogName))
            {
                throw new EgovException("Danh mục đã tồn tại!");
            }
            catalog.InCatalogId = Guid.NewGuid();
            _inCatalogRepository.Create(catalog);
            Context.SaveChanges();
        }

        /// <summary>
        /// Tạo danh mục mới.
        /// </summary>
        /// <param name="catalogs">the catalog object.</param>
        /// <param name="ignoreExist"> True: Bỏ qua kiểm tra tồn tại; False: không kiểm tra tồn tại</param>
        public void Create(IEnumerable<InCatalog> catalogs, bool ignoreExist = false)
        {
            if (catalogs == null || !catalogs.Any())
            {
                throw new ArgumentNullException("InCatalog");
            }

            var names = catalogs.Select(c => c.InCatalogName);
            var exists = _inCatalogRepository.GetsAs(p => p.InCatalogName, p => names.Contains(p.InCatalogName));
            if (exists != null && exists.Any())
            {
                if (!ignoreExist || exists.Count() == catalogs.Count())
                {
                    throw new EgovException("Danh mục chỉ tiêu đã tồn tại!");
                }

                var list = catalogs.Where(p => !exists.Contains(p.InCatalogName));
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

        private void Create(IEnumerable<InCatalog> catalogs)
        {
            foreach (var catalog in catalogs)
            {
                _inCatalogRepository.Create(catalog);
            }
            Context.SaveChanges();
        }

        /// <summary> Tienbv 121019
        /// Lấy danh mục.
        /// </summary>
        /// <param name="id">the catalog id.</param>
        /// <returns>The Catalog object.</returns>
        public InCatalog Get(Guid id)
        {
            var result = _inCatalogRepository.Get(id);
            return result;
        }

        /// <summary> Tienbv 261012
        /// Cập nhật danh mục.
        /// </summary>
        /// <param name="catalog">danh mục.</param>
        public void Update(InCatalog catalog)
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
        public void Delete(InCatalog catalog)
        {
            _inCatalogRepository.Delete(catalog);
            Context.SaveChanges();
        }

        /// <summary> TienBV 231012
        /// Lấy ra tất cả các danh mục có sắp xếp. Kết quả chỉ để đọc
        /// </summary>
        /// <param name="sortBy">Sắp xếp theo</param>
        /// <param name="isDescending">Sắp xếp từ lớn đến nhỏ: true, ngược lại false</param>
        /// <returns>Danh sách các tài nguyên đã được phân trang</returns>
        public IEnumerable<InCatalog> Gets(string sortBy = "", bool isDescending = false)
        {
            return _inCatalogRepository.GetsReadOnly(null, Context.Filters.CreateSort<InCatalog>(isDescending, sortBy));
        }

        /// <summary> Tienbv 011112
        /// Lấy ra tất cả catalog theo điều kiện kỹ thuật truyền vào. Kết quả chỉ để đọc
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <returns></returns>
        public IEnumerable<InCatalog> Gets(Expression<Func<InCatalog, bool>> spec = null)
        {
            return _inCatalogRepository.GetsReadOnly(spec);
        }

        /// <summary>
        /// Lấy ra tất cả các loại hồ sơ. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..
        /// </summary>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="spec">Điều kiện</param>
        /// <typeparam name="TOutput">Kiểu đầu ra.</typeparam>
        /// <returns>Danh sách các thực thể được ánh xạ</returns>
        public IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<InCatalog, TOutput>> projector, Expression<Func<InCatalog, bool>> spec = null)
        {
            return _inCatalogRepository.GetsAs(projector, spec);
        }


        /// <summary>
		/// Lấy dữ liêu thô của bảng InCatalog => join các bảng với nhau
		/// </summary>
		public IQueryable<InCatalog> Raw
        {
            get
            {
                return _inCatalogRepository.Raw;
            }
        }

        /// <summary>
		/// Trả về dữ liệu thô của bảng  map InCatalogValue => join lấy dữ liệu 
		/// </summary>
		public IQueryable<InCatalogValue> RawIncatalogValue
        {
            get
            {
                return _inCatalogValueRepository.Raw;
            }
        }
    }
}
