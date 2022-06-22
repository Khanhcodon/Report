using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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
    public class InCatalogValueBll : ServiceBase
    {
        private readonly IRepository<InCatalogValue> _inCatalogValueRepository;

        /// <summary>
        /// Khởi tạo class <see cref="CatalogValueBll"/>.
        /// </summary>
        /// <param name="context">Context</param>
        public InCatalogValueBll(IDbCustomerContext context)
            : base(context)
        {
            _inCatalogValueRepository = Context.GetRepository<InCatalogValue>();
        }

        /// <summary> Tienbv 221012
        /// Lấy một giá trị của danh mục theo id.
        /// </summary>
        /// <param name="id">the catalog value id.</param>
        /// <returns></returns>
        public InCatalogValue Get(Guid id)
        {
            return _inCatalogValueRepository.Get(id);
        }

        /// <summary> Tienbv 221012
        /// Lấy một giá trị của danh mục theo id.
        /// </summary>
        /// <param name="id">the catalog value id.</param>
        /// <returns></returns>
        public InCatalogValue GetsSelect(Guid? id)
        {
            return _inCatalogValueRepository.Get(id);
        }
        /// <summary> Tienbv 190912
        /// Tạo danh mục mới.
        /// </summary>
        /// <param name="catalog">the catalog object.</param>
        public void Create(InCatalogValue catalog)
        {
            if (catalog == null)
            {
                throw new ArgumentNullException("IndicatorCatalog");
            }
            //if (_inCatalogValueRepository.Exist(c => c.InCatalogValueName == catalog.InCatalogValueName))
            //{
            //    throw new EgovException("Danh mục đã tồn tại!");
            //}

            if (_inCatalogValueRepository.Exist(c => c.InCatalogValueCode == catalog.InCatalogValueCode))
            {
                throw new EgovException("Ma Danh mục đã tồn tại!" + catalog.InCatalogValueCode);
            }

            var oder = _inCatalogValueRepository.GetsReadOnly(c => c.ParentId == catalog.ParentId).Max(c => c.Order);
            catalog.InCatalogValueId = Guid.NewGuid();
            catalog.Order = oder + 1 ?? 0;
            _inCatalogValueRepository.Create(catalog);
            Context.SaveChanges();
        }
        /// <summary>
        /// Cập nhật một giá trị của danh mục.
        /// </summary>
        /// <param name="catalogValue">the catalog value obj.</param>
        public void Update(InCatalogValue inCatalogValue)
        {
            if (inCatalogValue == null)
            {
                throw new ArgumentNullException("InCatalogValue");
            }
            var oder = _inCatalogValueRepository.GetsReadOnly(c => c.ParentId == inCatalogValue.ParentId).Max(c => c.Order);
            inCatalogValue.Order = oder + 1 ?? 0;
            Context.SaveChanges();
        }

        /// <summary>
        /// Xóa một đối tượng của danh mục.
        /// <para>
        /// Cấn kiểm tra xem đối tượng này đã được sử dụng trong form nào chưa, chỉ được xóa khi chưa sử dụng.
        /// </para>
        /// </summary>
        /// <param name="inCatalogValue">the catalog value obj.</param>
        public void Delete(InCatalogValue inCatalogValue)
        {
            if (inCatalogValue == null)
            {
                throw new ArgumentNullException("InCatalogValue");
            }
            //Todo: (Kết quả: TienBV - chưa sửa - ngày sửa). Reporter: TienBv. Kiểm tra nếu đối tượng chưa được sử dụng thì mới cho phép xóa.
            var check = true;
            if (check)
            {
                _inCatalogValueRepository.Delete(inCatalogValue);
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
        /// <param name="inCatalogId">The catalog guid id.</param>
        public IEnumerable<InCatalogValue> Gets(Guid inCatalogId)
        {
            return _inCatalogValueRepository.GetsReadOnly(c => c.InCatalogId == inCatalogId,
                Context.Filters.Sort<InCatalogValue, int?>(c => c.Order));
        }

        public IEnumerable<InCatalogValue> GetsParent()
        {
            return _inCatalogValueRepository.GetsReadOnly(c => c.ParentId == null,
                Context.Filters.Sort<InCatalogValue, int?>(c => c.Order));
        }
        public IEnumerable<InCatalogValue> GetsChildByParent(Guid parentId)
        {
            return _inCatalogValueRepository.GetsReadOnly(c => c.ParentId == parentId, Context.Filters.Sort<InCatalogValue, int?>(c => c.Order));
        }
        public IEnumerable<InCatalogValue> Gets(Expression<Func<InCatalogValue, bool>> spec = null)
        {
            return _inCatalogValueRepository.GetsReadOnly(spec , Context.Filters.Sort<InCatalogValue, Guid?>(c => c.ParentId));
        }
        public IEnumerable<InCatalogValue> GetInCatalogValueDetails(List<string> catalogValueNames)
        {
            var inCatalogValueInfos = _inCatalogValueRepository.Gets(false, x => catalogValueNames.Contains(x.InCatalogValueName));

            return inCatalogValueInfos;
        }
        public IEnumerable<InCatalogValue> GetInCatalogValueByName(string catalogValueName)
        {
            var inCatalogValueInfos = _inCatalogValueRepository.Gets(false, x => x.InCatalogValueName == catalogValueName);

            return inCatalogValueInfos;
        }
    }
}
