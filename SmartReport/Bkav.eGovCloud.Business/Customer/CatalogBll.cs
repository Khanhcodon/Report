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
    public class CatalogBll : ServiceBase
    {
        private readonly IRepository<Catalog> _catalogRepository;
        private readonly IRepository<CatalogValue> _catalogValueRepository;
        private readonly ResourceBll _resourceService;

        /// <summary>
        /// Khởi tạo class <see cref="CatalogBll"/>.
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="resourceService"></param>
        //TODO: TienBV - Tham số đầu vào ICatalogValueDal catalogValueDal nên thay thành CatalogValueBll
        public CatalogBll(IDbCustomerContext context, ResourceBll resourceService)
            : base(context)
        {
            _catalogRepository = Context.GetRepository<Catalog>();
            _catalogValueRepository = Context.GetRepository<CatalogValue>();
            _resourceService = resourceService;
        }

        /// <summary> Tienbv 190912
        /// Tạo danh mục mới.
        /// </summary>
        /// <param name="catalog">the catalog object.</param>
        public void Create(Catalog catalog)
        {
            if (catalog == null)
            {
                throw new ArgumentNullException("catalog");
            }
            if (_catalogRepository.Exist(c => c.CatalogName == catalog.CatalogName))
            {
                throw new EgovException("Danh mục đã tồn tại!");
            }
            var values = catalog.ValueNames;
            var order = 0;
            catalog.CatalogId = Guid.NewGuid();
            catalog.CatalogKey = "";
            foreach (var value in values)
            {
                catalog.CatalogValues.Add(new CatalogValue
                {
                    Value = value,
                    Order = order++,
                    CatalogValueId = Guid.NewGuid(),
                    CatalogKey = catalog.CatalogKeys[order-1]
                });
            }
            _catalogRepository.Create(catalog);
            Context.SaveChanges();
        }

        /// <summary>
        /// Tạo danh mục mới.
        /// </summary>
        /// <param name="catalogs">the catalog object.</param>
        /// <param name="ignoreExist"> True: Bỏ qua kiểm tra tồn tại; False: không kiểm tra tồn tại</param>
        public void Create(IEnumerable<Catalog> catalogs, bool ignoreExist = false)
        {
            if (catalogs == null || !catalogs.Any())
            {
                throw new ArgumentNullException("catalog");
            }

            var names = catalogs.Select(c => c.CatalogName);
            var exists = _catalogRepository.GetsAs(p => p.CatalogName, p => names.Contains(p.CatalogName));
            if (exists != null && exists.Any())
            {
                if (!ignoreExist || exists.Count() == catalogs.Count())
                {
                    throw new EgovException(_resourceService.GetResource("Catalog.Create.Exist"));
                }

                var list = catalogs.Where(p => !exists.Contains(p.CatalogName));
                if (list == null || !list.Any())
                {
                    throw new EgovException(_resourceService.GetResource("Catalog.Create.Exist"));
                }
                Create(list);
            }
            else
            {
                Create(catalogs);
            }
        }

        private void Create(IEnumerable<Catalog> catalogs)
        {
            foreach (var catalog in catalogs)
            {
                var values = catalog.ValueNames;
                var order = 1;
                catalog.CatalogId = Guid.NewGuid();
                foreach (var value in values)
                {
                    catalog.CatalogValues.Add(new CatalogValue
                    {
                        Value = value,
                        Order = order++,
                        CatalogValueId = Guid.NewGuid()
                    });
                }
                _catalogRepository.Create(catalog);
            }
            Context.SaveChanges();
        }

        /// <summary> Tienbv 121019
        /// Lấy danh mục.
        /// </summary>
        /// <param name="id">the catalog id.</param>
        /// <returns>The Catalog object.</returns>
        public Catalog Get(Guid id)
        {
            var result = _catalogRepository.Get(id);
            return result;
        }

        /// <summary> Tienbv 261012
        /// Cập nhật danh mục.
        /// </summary>
        /// <param name="catalog">danh mục.</param>
        /// <param name="valueIds">danh sách id các đối tượng của danh mục: bao gồm các đối tượng đã có, thêm mới, xóa.</param>
        /// <param name="valueNames">danh sách tên các của các đối tượng tương ứng với id trên.</param>
        public void Update(Catalog catalog, List<Guid> valueIds, List<string> valueNames)
        {
            if (catalog == null)
            {
                throw new ArgumentNullException("catalog");
            }
            var order = 0;
            //var tmp = catalog.CatalogValues.ToList();
            catalog.CatalogValues = new List<CatalogValue>();
            catalog.CatalogKey = "";
            var index = 0;
            foreach (var valueId in valueIds)
            {
                var valueName = valueNames.ElementAt(index++);
                if (valueId == new Guid())
                {
                    if (!string.IsNullOrEmpty(valueName))
                    {
                        var catalogValue  = new CatalogValue
                        {
                            CatalogValueId = Guid.NewGuid(),
                            Value = valueName.Length > 255 ? valueName.Substring(0, 254) : valueName,
                            Order = order,
                            CatalogKey = catalog.CatalogKeys[index-1]
                        };
                        catalog.CatalogValues.Add(catalogValue);
                        //_catalogValueRepository.Create(catalogValue);
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    var catalogValue = _catalogValueRepository.Get(valueId);
                    if (catalogValue == null)
                    {
                        continue;
                    }
                    if (string.IsNullOrEmpty(valueName))
                    {
                        _catalogValueRepository.Delete(catalogValue);
                    }
                    else
                    {
                        catalogValue.Value = valueName;
                        catalogValue.Order = order;
                    }
                }
                order++;
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
        public void Detele(Catalog catalog)
        {
            var values = _catalogValueRepository.Gets(false, v => v.CatalogId == catalog.CatalogId);
            foreach (var val in values)
            {
                _catalogValueRepository.Delete(val);
            }
            _catalogRepository.Delete(catalog);
            Context.SaveChanges();
        }

        /// <summary> TienBV 231012
        /// Lấy ra tất cả các danh mục có sắp xếp. Kết quả chỉ để đọc
        /// </summary>
        /// <param name="sortBy">Sắp xếp theo</param>
        /// <param name="isDescending">Sắp xếp từ lớn đến nhỏ: true, ngược lại false</param>
        /// <returns>Danh sách các tài nguyên đã được phân trang</returns>
        public IEnumerable<Catalog> Gets(string sortBy = "", bool isDescending = false)
        {
            return _catalogRepository.GetsReadOnly(null, Context.Filters.CreateSort<Catalog>(isDescending, sortBy));
        }

        /// <summary> Tienbv 011112
        /// Lấy ra tất cả catalog theo điều kiện kỹ thuật truyền vào. Kết quả chỉ để đọc
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <returns></returns>
        public IEnumerable<Catalog> Gets(Expression<Func<Catalog, bool>> spec = null)
        {
            return _catalogRepository.GetsReadOnly(spec);
        }
    }
}
