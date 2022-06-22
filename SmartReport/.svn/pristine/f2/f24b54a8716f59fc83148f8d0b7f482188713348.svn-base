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
    /// <para> Class : SurveyCatalogValueBll - public - BLL</para>
    /// <para> Access Modifiers: </para>
    /// <para> Create Date : 221012</para>
    /// <para> Author      : TienBV</para>
    /// <para> Description : Quản lý các đối tượng của danh mục egov.</para>
    /// <remarks>
    ///     <para> Quản lý những giá trị sẽ bind ra các select box của danh mục tương ứng khi soạn form động.</para>
    /// </remarks>
    /// </summary>
    public class SurveyCatalogValueBll : ServiceBase
    {
        private readonly IRepository<SurveyCatalogValue> _surveyCatalogValueRepository;

        /// <summary>
        /// Khởi tạo class <see cref="SurveyCatalogValueBll"/>.
        /// </summary>
        /// <param name="context">Context</param>
        public SurveyCatalogValueBll(IDbCustomerContext context)
            : base(context)
        {
            _surveyCatalogValueRepository = Context.GetRepository<SurveyCatalogValue>();
        }

        /// <summary> Tienbv 221012
        /// Lấy một giá trị của danh mục theo id.
        /// </summary>
        /// <param name="id">the survey catalog value id.</param>
        /// <returns></returns>
        public SurveyCatalogValue Get(Guid id)
        {
            return _surveyCatalogValueRepository.Get(id);
        }

        /// <summary> Tienbv 221012
        /// Cập nhật một giá trị của danh mục.
        /// </summary>
        /// <param name="surveyCatalogValue">the survey catalog value obj.</param>
        public void Update(SurveyCatalogValue surveyCatalogValue)
        {
            if (surveyCatalogValue == null)
            {
                throw new ArgumentNullException("surveyCatalogValue");
            }
            Context.SaveChanges();
        }

        /// <summary> TienBV 221012
        /// Xóa một đối tượng của danh mục.
        /// <para>
        /// Cấn kiểm tra xem đối tượng này đã được sử dụng trong form nào chưa, chỉ được xóa khi chưa sử dụng.
        /// </para>
        /// </summary>
        /// <param name="surveyCatalogValue">the survey catalog value obj.</param>
        public void Delete(SurveyCatalogValue surveyCatalogValue)
        {
            if (surveyCatalogValue == null)
            {
                throw new ArgumentNullException("surveyCatalogValue");
            }
            //Todo: (Kết quả: TienBV - chưa sửa - ngày sửa). Reporter: TienBv. Kiểm tra nếu đối tượng chưa được sử dụng thì mới cho phép xóa.
            var check = true;
            if (check)
            {
                _surveyCatalogValueRepository.Delete(surveyCatalogValue);
                Context.SaveChanges();
            }
            else
            {
                throw new EgovException("Đối tượng đang được sử dụng, bạn không có quyền xóa!");
            }
        }

        /// Tạo danh mục mới.
        /// </summary>
        /// <param name="catalog">the catalog object.</param>
        public void Create(SurveyCatalogValue catalog)
        {
            if (catalog == null)
            {
                throw new ArgumentNullException("SurveyCatalog");
            }

            var oder = _surveyCatalogValueRepository.GetsReadOnly(c => c.ParentId == catalog.ParentId).Max(c => c.Order);
            catalog.CatalogValueId = Guid.NewGuid();
            catalog.Order = oder + 1 ?? 0;
            _surveyCatalogValueRepository.Create(catalog);
            Context.SaveChanges();
        }

        /// <summary> TienBV 261012
        /// Lấy ra các đối tượng của danh mục. Kết quả chỉ để đọc
        /// </summary>
        /// <param name="id">The catalog guid id.</param>
        public IEnumerable<SurveyCatalogValue> Gets(Guid id)
        {
            return _surveyCatalogValueRepository.GetsReadOnly(c => c.CatalogId == id,
                Context.Filters.Sort<SurveyCatalogValue, int?>(c => c.Order));
        }        public IEnumerable<SurveyCatalogValue> GetsParent()
        {
            return _surveyCatalogValueRepository.GetsReadOnly(c => c.ParentId == null,
                Context.Filters.Sort<SurveyCatalogValue, int?>(c => c.Order));
        }
        public IEnumerable<SurveyCatalogValue> GetsChildByParent(Guid parentId)
        {
            return _surveyCatalogValueRepository.GetsReadOnly(c => c.ParentId == parentId, Context.Filters.Sort<SurveyCatalogValue, int?>(c => c.Order));
        }
        public IEnumerable<SurveyCatalogValue> Gets(Expression<Func<SurveyCatalogValue, bool>> spec = null)
        {
            return _surveyCatalogValueRepository.GetsReadOnly(spec);
        }
        public IEnumerable<SurveyCatalogValue> GetCatalogValueDetails(List<string> catalogValueNames)
        {
            var catalogValueInfos = _surveyCatalogValueRepository.Gets(false, x => catalogValueNames.Contains(x.Value));

            return catalogValueInfos;
        }
        public IEnumerable<SurveyCatalogValue> GetSurveyCatalogValueByName(string catalogValueName)
        {
            var surveyCatalogValueInfos = _surveyCatalogValueRepository.Gets(false, x => x.Value == catalogValueName);

            return surveyCatalogValueInfos;
        }
    }
}
