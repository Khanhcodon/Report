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
    /// <para>Class : SurveyCatalogBll - public - BLL</para> 
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
    public class SurveyCatalogBll : ServiceBase
    {
        private readonly IRepository<SurveyCatalog> _surveyCatalogRepository;
        private readonly IRepository<SurveyCatalogValue> _surveyCatalogValueRepository;
        private readonly ResourceBll _resourceService;

        /// <summary>
        /// Khởi tạo class <see cref="SurveyCatalogBll"/>.
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="resourceService"></param>
        //TODO: TienBV - Tham số đầu vào ICatalogValueDal catalogValueDal nên thay thành SurveyCatalogValueBll
        public SurveyCatalogBll(IDbCustomerContext context, ResourceBll resourceService)
            : base(context)
        {
            _surveyCatalogRepository = Context.GetRepository<SurveyCatalog>();
            _surveyCatalogValueRepository = Context.GetRepository<SurveyCatalogValue>();
            _resourceService = resourceService;
        }

        /// <summary>
        /// Tạo danh mục mới.
        /// </summary>
        /// <param name="surveyCatalog">the catalog object.</param>
        public void Create(SurveyCatalog surveyCatalog)
        {
            if (surveyCatalog == null)
            {
                throw new ArgumentNullException("surveyCatalog");
            }
            if (_surveyCatalogRepository.Exist(c => c.CatalogName == surveyCatalog.CatalogName))
            {
                throw new EgovException("Danh mục đã tồn tại!");
            }
            surveyCatalog.CatalogId = Guid.NewGuid();
            _surveyCatalogRepository.Create(surveyCatalog);
            Context.SaveChanges();
        }

        /// <summary>
        /// Tạo danh mục mới.
        /// </summary>
        /// <param name="surveyCatalogs">the catalog object.</param>
        /// <param name="ignoreExist"> True: Bỏ qua kiểm tra tồn tại; False: không kiểm tra tồn tại</param>
        public void Create(IEnumerable<SurveyCatalog> surveyCatalogs, bool ignoreExist = false)
        {
            if (surveyCatalogs == null || !surveyCatalogs.Any())
            {
                throw new ArgumentNullException("surveyCatalogs");
            }

            var names = surveyCatalogs.Select(c => c.CatalogName);
            var exists = _surveyCatalogRepository.GetsAs(p => p.CatalogName, p => names.Contains(p.CatalogName));
            if (exists != null && exists.Any())
            {
                if (!ignoreExist || exists.Count() == surveyCatalogs.Count())
                {
                    throw new EgovException("Danh mục chỉ tiêu đã tồn tại!");
                }

                var list = surveyCatalogs.Where(p => !exists.Contains(p.CatalogName));
                if (list == null || !list.Any())
                {
                    throw new EgovException("Danh mục chỉ tiêu đã tồn tại!");
                }
                Create(list);
            }
            else
            {
                Create(surveyCatalogs);
            }
        }

        private void Create(IEnumerable<SurveyCatalog> surveyCatalogs)
        {
            foreach (var catalog in surveyCatalogs)
            {
                _surveyCatalogRepository.Create(catalog);
            }
            Context.SaveChanges();
        }

        /// <summary> Tienbv 121019
        /// Lấy danh mục.
        /// </summary>
        /// <param name="id">the catalog id.</param>
        /// <returns>The Catalog object.</returns>
        public SurveyCatalog Get(Guid id)
        {
            var result = _surveyCatalogRepository.Get(id);
            return result;
        }

        /// <summary> Tienbv 261012
        /// Cập nhật danh mục.
        /// </summary>
        /// <param name="surveyCatalog">danh mục.</param>
        public void Update(SurveyCatalog surveyCatalog)
        {
            if (surveyCatalog == null)
            {
                throw new ArgumentNullException("surveyCatalog");
            }

            Context.SaveChanges();
        }

        /// <summary> TienBV 201012
        /// <para> Xóa danh mục.</para>
        /// <para> Note:</para>
        /// <para> - Nếu danh mục và các đối đượng của nó đã được sử dụng trong form, hồ sơ thì không được xóa.</para>
        /// <para> - Khi xóa danh mục sẽ xóa hết tất cả các đối tượng của danh mục đó.</para>
        /// </summary>
        /// <param name="surveyCatalog">the catalog.</param>
        public void Delete(SurveyCatalog surveyCatalog)
        {
            _surveyCatalogRepository.Delete(surveyCatalog);
            Context.SaveChanges();
        }

        /// <summary> TienBV 231012
        /// Lấy ra tất cả các danh mục có sắp xếp. Kết quả chỉ để đọc
        /// </summary>
        /// <param name="sortBy">Sắp xếp theo</param>
        /// <param name="isDescending">Sắp xếp từ lớn đến nhỏ: true, ngược lại false</param>
        /// <returns>Danh sách các tài nguyên đã được phân trang</returns>
        public IEnumerable<SurveyCatalog> Gets(string sortBy = "", bool isDescending = false)
        {
            return _surveyCatalogRepository.GetsReadOnly(null, Context.Filters.CreateSort<SurveyCatalog>(isDescending, sortBy));
        }

        /// <summary> Tienbv 011112
        /// Lấy ra tất cả catalog theo điều kiện kỹ thuật truyền vào. Kết quả chỉ để đọc
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <returns></returns>
        public IEnumerable<SurveyCatalog> Gets(Expression<Func<SurveyCatalog, bool>> spec = null)
        {
            return _surveyCatalogRepository.GetsReadOnly(spec);
        }

        /// <summary>
        /// Lấy ra tất cả các loại hồ sơ. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..
        /// </summary>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="spec">Điều kiện</param>
        /// <typeparam name="TOutput">Kiểu đầu ra.</typeparam>
        /// <returns>Danh sách các thực thể được ánh xạ</returns>
        public IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<SurveyCatalog, TOutput>> projector, Expression<Func<SurveyCatalog, bool>> spec = null)
        {
            return _surveyCatalogRepository.GetsAs(projector, spec);
        }
    }
}