using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Customer;
using System;
using System.Collections.Generic;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// <para> Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para> Project: eGov Cloud v1.0</para>
    /// <para> Class : CatalogValueBll - public - BLL</para>
    /// <para> Access Modifiers: </para>
    /// <para> Create Date : 221012</para>
    /// <para> Author      : TienBV</para>
    /// <para> Description : Quản lý các đối tượng của danh mục egov.</para>
    /// <remarks>
    ///     <para> Quản lý những giá trị sẽ bind ra các select box của danh mục tương ứng khi soạn form động.</para>
    /// </remarks>
    /// </summary>
    public class CatalogValueBll : ServiceBase
    {
        private readonly IRepository<CatalogValue> _catalogValueRepository;

        /// <summary>
        /// Khởi tạo class <see cref="CatalogValueBll"/>.
        /// </summary>
        /// <param name="context">Context</param>
        public CatalogValueBll(IDbCustomerContext context)
            : base(context)
        {
            _catalogValueRepository = Context.GetRepository<CatalogValue>();
        }

        /// <summary> Tienbv 221012
        /// Lấy một giá trị của danh mục theo id.
        /// </summary>
        /// <param name="id">the catalog value id.</param>
        /// <returns></returns>
        public CatalogValue Get(Guid id)
        {
            return _catalogValueRepository.Get(id);
        }

        /// <summary> Tienbv 221012
        /// Cập nhật một giá trị của danh mục.
        /// </summary>
        /// <param name="catalogValue">the catalog value obj.</param>
        public void Update(CatalogValue catalogValue)
        {
            if (catalogValue == null)
            {
                throw new ArgumentNullException("catalogValue");
            }
            Context.SaveChanges();
        }

        /// <summary> TienBV 221012
        /// Xóa một đối tượng của danh mục.
        /// <para>
        /// Cấn kiểm tra xem đối tượng này đã được sử dụng trong form nào chưa, chỉ được xóa khi chưa sử dụng.
        /// </para>
        /// </summary>
        /// <param name="catalogValue">the catalog value obj.</param>
        public void Delete(CatalogValue catalogValue)
        {
            if (catalogValue == null)
            {
                throw new ArgumentNullException("catalogValue");
            }
            //Todo: (Kết quả: TienBV - chưa sửa - ngày sửa). Reporter: TienBv. Kiểm tra nếu đối tượng chưa được sử dụng thì mới cho phép xóa.
            var check = true;
            if (check)
            {
                _catalogValueRepository.Delete(catalogValue);
                Context.SaveChanges();
            }
            else
            {
                throw new EgovException("Đối tượng đang được sử dụng, bạn không có quyền xóa!");
            }
        }

        /// <summary> TienBV 261012
        /// Lấy ra các đối tượng của danh mục. Kết quả chỉ để đọc
        /// </summary>
        /// <param name="id">The catalog guid id.</param>
        public IEnumerable<CatalogValue> Gets(Guid id)
        {
            return _catalogValueRepository.GetsReadOnly(c => c.CatalogId == id,
                Context.Filters.Sort<CatalogValue, int?>(c => c.Order));
        }

        public IEnumerable<CatalogValue> GetCatalogValueDetails(List<string> catalogValueNames)
        {
            var catalogValueInfos = _catalogValueRepository.Gets(false, x => catalogValueNames.Contains(x.Value));

            return catalogValueInfos;
        }
    }
}
