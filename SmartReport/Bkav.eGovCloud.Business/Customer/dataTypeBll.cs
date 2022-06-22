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
    public class dataTypeBll : ServiceBase
    {
        private readonly IRepository<dataType> _dataTypeRepository;
        private readonly ResourceBll _resourceService;

        /// <summary>
        /// Khởi tạo class <see cref="dataTypeBll"/>.
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="resourceService"></param>
        public dataTypeBll(IDbCustomerContext context, ResourceBll resourceService)
            : base(context)
        {
            _dataTypeRepository = Context.GetRepository<dataType>();
            _resourceService = resourceService;
        }

        /// <summary>
        /// Tạo mới chỉ tiêu phân tổ
        /// </summary>
        /// <param name="dataType">the catalog object.</param>
        public void Create(dataType dataType)
        {
            if (dataType == null)
            {
                throw new ArgumentNullException("dataType");
            }
            if (_dataTypeRepository.Exist(c => c.dataTypeName == dataType.dataTypeName))
            {
                throw new EgovException("Tên số liệu đã tồn tại!");
            }
            dataType.dataTypeId = Guid.NewGuid();
            _dataTypeRepository.Create(dataType);
            Context.SaveChanges();
        }

        /// <summary>
        /// Tạo mới chỉ tiêu phân tổ
        /// </summary>
        /// <param name="dataType">the catalog object.</param>
        /// <param name="ignoreExist"> True: Bỏ qua kiểm tra tồn tại; False: không kiểm tra tồn tại</param>
        public void Create(IEnumerable<dataType> dataType, bool ignoreExist = false)
        {
            if (dataType == null || !dataType.Any())
            {
                throw new ArgumentNullException("dataType");
            }

            var names = dataType.Select(c => c.dataTypeName);
            var exists = _dataTypeRepository.GetsAs(p => p.dataTypeName, p => names.Contains(p.dataTypeName));
            if (exists != null && exists.Any())
            {
                if (!ignoreExist || exists.Count() == dataType.Count())
                {
                    throw new EgovException("Tên số liệu đã tồn tại!");
                }

                var list = dataType.Where(p => !exists.Contains(p.dataTypeName));
                if (list == null || !list.Any())
                {
                    throw new EgovException("Tên số liệu đã tồn tại!");
                }
                Create(list);
            }
            else
            {
                Create(dataType);
            }
        }

        private void Create(IEnumerable<dataType> dataType)
        {
            foreach (var datatype in dataType)
            {
                _dataTypeRepository.Create(datatype);
            }
            Context.SaveChanges();
        }

        /// <summary> Tienbv 121019
        /// Lấy danh mục.
        /// </summary>
        /// <param name="id">the catalog id.</param>
        /// <returns>The Catalog object.</returns>
        public dataType Get(Guid id)
        {
            var result = _dataTypeRepository.Get(id);
            return result;
        }

        /// <summary> Tienbv 221012
        /// Lấy một giá trị của danh mục theo id.
        /// </summary>
        /// <param name="id">the catalog value id.</param>
        /// <returns></returns>
        public dataType GetsSelect(Guid? id)
        {
            return _dataTypeRepository.Get(id);
        }

        /// <summary> Tienbv 261012
        /// Cập nhật danh mục.
        /// </summary>
        /// <param name="catalog">danh mục.</param>
        public void Update(dataType dataType)
        {
            if (dataType == null)
            {
                throw new ArgumentNullException("dataType");
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
        public void Delete(dataType dataType)
        {
            _dataTypeRepository.Delete(dataType);
            Context.SaveChanges();
        }

        /// <summary> TienBV 231012
        /// Lấy ra tất cả các danh mục có sắp xếp. Kết quả chỉ để đọc
        /// </summary>
        /// <param name="sortBy">Sắp xếp theo</param>
        /// <param name="isDescending">Sắp xếp từ lớn đến nhỏ: true, ngược lại false</param>
        /// <returns>Danh sách các tài nguyên đã được phân trang</returns>
        public IEnumerable<dataType> Gets(string sortBy = "", bool isDescending = false)
        {
            return _dataTypeRepository.GetsReadOnly(null, Context.Filters.CreateSort<dataType>(isDescending, sortBy));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public IEnumerable<dataType> GetsSelects(Expression<Func<dataType, bool>> spec = null)
        {
            return _dataTypeRepository.GetsReadOnly(spec, Context.Filters.Sort<dataType, Guid?>(c => c.dataTypeId));
        }

        /// <summary> Tienbv 011112
        /// Lấy ra tất cả catalog theo điều kiện kỹ thuật truyền vào. Kết quả chỉ để đọc
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <returns></returns>
        public IEnumerable<dataType> Gets(Expression<Func<dataType, bool>> spec = null)
        {
            return _dataTypeRepository.GetsReadOnly(spec);
        }

        /// <summary>
        /// Lấy ra tất cả các loại hồ sơ. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..
        /// </summary>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="spec">Điều kiện</param>
        /// <typeparam name="TOutput">Kiểu đầu ra.</typeparam>
        /// <returns>Danh sách các thực thể được ánh xạ</returns>
        public IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<dataType, TOutput>> projector, Expression<Func<dataType, bool>> spec = null)
        {
            return _dataTypeRepository.GetsAs(projector, spec);
        }
    }
}
