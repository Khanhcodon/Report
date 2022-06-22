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
    public class IndicatorValueDepartmentBll : ServiceBase
    {
        private readonly IRepository<IndicatorValueDepartment> _catalogRepository;
        private readonly ResourceBll _resourceService;

       /// <summary>
       /// 
       /// </summary>
       /// <param name="context"></param>
       /// <param name="resourceService"></param>
        public IndicatorValueDepartmentBll(IDbCustomerContext context, ResourceBll resourceService)
            : base(context)
        {
            _catalogRepository = Context.GetRepository<IndicatorValueDepartment>();
            _resourceService = resourceService;
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="catalog"></param>
        public void Create(IndicatorValueDepartment catalog)
        {
            if (catalog == null)
            {
                throw new ArgumentNullException("IndicatorCatalog");
            }
          
            _catalogRepository.Create(catalog);
            Context.SaveChanges();
        }


        public void Create(IEnumerable<IndicatorValueDepartment> catalogs)
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
        public IndicatorValueDepartment Get(int id)
        {
            var result = _catalogRepository.Get(id);
            return result;
        }

        /// <summary> Tienbv 261012
        /// Cập nhật danh mục.
        /// </summary>
        /// <param name="catalog">danh mục.</param>
        public void Update(IndicatorValueDepartment catalog)
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
        public void Detele(IndicatorValueDepartment catalog)
        {
            _catalogRepository.Delete(catalog);
            Context.SaveChanges();
        }

        public void Detele(IEnumerable<IndicatorValueDepartment> catalogs)
        {
            foreach (var catalog in catalogs)
            {
                _catalogRepository.Delete(catalog);
            }
            Context.SaveChanges();
        }

        /// <summary> TienBV 231012
        /// Lấy ra tất cả các danh mục có sắp xếp. Kết quả chỉ để đọc
        /// </summary>
        /// <param name="sortBy">Sắp xếp theo</param>
        /// <param name="isDescending">Sắp xếp từ lớn đến nhỏ: true, ngược lại false</param>
        /// <returns>Danh sách các tài nguyên đã được phân trang</returns>
        public IEnumerable<IndicatorValueDepartment> Gets(string sortBy = "", bool isDescending = false)
        {
            return _catalogRepository.GetsReadOnly(null, Context.Filters.CreateSort<IndicatorValueDepartment>(isDescending, sortBy));
        }

        /// <summary> Tienbv 011112
        /// Lấy ra tất cả catalog theo điều kiện kỹ thuật truyền vào. Kết quả chỉ để đọc
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <returns></returns>
        public IEnumerable<IndicatorValueDepartment> Gets(Expression<Func<IndicatorValueDepartment, bool>> spec = null)
        {
            return _catalogRepository.GetsReadOnly(spec);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public IEnumerable<IndicatorValueDepartment> Gets(bool isReadOnly = true, Expression<Func<IndicatorValueDepartment, bool>> spec = null)
        {
            return _catalogRepository.Gets(isReadOnly, spec);
        }

    }
}
