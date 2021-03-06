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
    /// <summary>
    /// hiepns
    /// </summary>
    public class CategoryDisaggregationsBll : ServiceBase
    {
        private readonly IRepository<CategoryDisaggregations> _categoryDisaggreationsRepository;
        private readonly ResourceBll _resourceService;

        /// <summary>
        /// Khởi tạo class <see cref="CatalogValueBll"/>.
        /// </summary>
        /// <param name="context">Context</param>
        public CategoryDisaggregationsBll(IDbCustomerContext context, ResourceBll resourceService)
            : base(context)
        {
            _categoryDisaggreationsRepository = Context.GetRepository<CategoryDisaggregations>();
            _resourceService = resourceService;
        }

        /// <summary> Tienbv 221012
        /// Lấy một giá trị của danh mục theo id.
        /// </summary>
        /// <param name="id">the catalog value id.</param>
        /// <returns></returns>
        public CategoryDisaggregations Get(Guid id)
        {
            return _categoryDisaggreationsRepository.Get(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public IEnumerable<CategoryDisaggregations> GetsSelects(Expression<Func<CategoryDisaggregations, bool>> spec = null)
        {
            return _categoryDisaggreationsRepository.GetsReadOnly(spec, Context.Filters.Sort<CategoryDisaggregations, Guid?>(c => c.CategoryDisaggregationId));
        }

        /// <summary> Tienbv 190912
        /// Tạo danh mục mới.
        /// </summary>
        /// <param name="catalog">the catalog object.</param>
        public void Create(CategoryDisaggregations catedisaggregation)
        {
            if (catedisaggregation == null)
            {
                throw new ArgumentNullException("catedisaggregation");
            }  
            if (_categoryDisaggreationsRepository.Exist(c => c.CategoryDisaggregationName == catedisaggregation.CategoryDisaggregationName))
            {
                throw new EgovException("Tên tiêu thức danh mục đã tồn tại!");
            }
            if (_categoryDisaggreationsRepository.Exist(c => c.CategoryDisaggregationCode == catedisaggregation.CategoryDisaggregationCode))
            {
                throw new EgovException("Mã tiêu thức danh mục đã tồn tại!");
            }
            var getss = _categoryDisaggreationsRepository.GetsReadOnly(c => c.IndicatorId == catedisaggregation.IndicatorId);
            var countGet = getss.Count();
            if (countGet > 0)
            {
                var oder = _categoryDisaggreationsRepository.GetsReadOnly(c =>
               c.IndicatorId == catedisaggregation.IndicatorId).Max(c => c.OrderType);

                catedisaggregation.CategoryDisaggregationId = Guid.NewGuid();
                catedisaggregation.OrderType = oder >= 0 ? oder + 1 : 0;
                _categoryDisaggreationsRepository.Create(catedisaggregation);
                Context.SaveChanges();

            }else
            {
                catedisaggregation.CategoryDisaggregationId = Guid.NewGuid();
                catedisaggregation.OrderType =  0;
                _categoryDisaggreationsRepository.Create(catedisaggregation);
                Context.SaveChanges();
            }
            
        }
        /// <summary>
        /// Cập nhật một giá trị của danh mục.
        /// </summary>
        /// <param name="catalogValue">the catalog value obj.</param>
        public void Update(CategoryDisaggregations catedisaggregation)
        {
            if (catedisaggregation == null)
            {
                throw new ArgumentNullException("catedisaggregation");
            }
            //if (_categoryDisaggreationsRepository.Exist(c => c.CategoryDisaggregationName == catedisaggregation.CategoryDisaggregationName))
            //{
            //    throw new EgovException("Tên tiêu thức danh mục đã tồn tại!");
            //}
            //if (_categoryDisaggreationsRepository.Exist(c => c.CategoryDisaggregationCode == catedisaggregation.CategoryDisaggregationCode))
            //{
            //    throw new EgovException("Mã tiêu thức danh mục đã tồn tại!");
            //}
            Context.SaveChanges();
        }

        /// <summary>
        /// Xóa một đối tượng của danh mục.
        /// <para>
        /// Cấn kiểm tra xem đối tượng này đã được sử dụng trong form nào chưa, chỉ được xóa khi chưa sử dụng.
        /// </para>
        /// </summary>
        /// <param name="inCatalogValue">the catalog value obj.</param>
        public void Delete(CategoryDisaggregations catedisaggregation)
        {
            if (catedisaggregation == null)
            {
                throw new ArgumentNullException("catedisaggregation");
            }
            //Todo: (Kết quả: TienBV - chưa sửa - ngày sửa). Reporter: TienBv. Kiểm tra nếu đối tượng chưa được sử dụng thì mới cho phép xóa.
            var check = true;
            if (check)
            {
                _categoryDisaggreationsRepository.Delete(catedisaggregation);
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
        public IEnumerable<CategoryDisaggregations> Gets(Guid catedisaggregationId)
        {
            return _categoryDisaggreationsRepository.GetsReadOnly(c => c.CategoryDisaggregationId == catedisaggregationId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public IEnumerable<CategoryDisaggregations> Gets(Expression<Func<CategoryDisaggregations, bool>> spec = null)
        {
            return _categoryDisaggreationsRepository.GetsReadOnly(spec);
        }

        //public IEnumerable<CategoryDisaggregations> GetsParent()
        //{
        //    return _categoryDisaggreationsRepository.GetsReadOnly(c => c.CategoryDisaggregationCode == null,
        //        Context.Filters.Sort<InCatalogValue, int?>(c => c.Order));
        //}
        //public IEnumerable<InCatalogValue> GetsChildByParent(Guid parentId)
        //{
        //    return _inCatalogValueRepository.GetsReadOnly(c => c.ParentId == parentId, Context.Filters.Sort<InCatalogValue, int?>(c => c.Order));
        //}
        //public IEnumerable<CategoryDisaggregations> Gets(Expression<Func<CategoryDisaggregations, bool>> spec = null)
        //{
        //    return _categoryDisaggreationsRepository.GetsReadOnly(spec, Context.Filters.Sort<CategoryDisaggregations, Guid?>(c => c.ParentId));
        //}
        public IEnumerable<CategoryDisaggregations> GetInCatalogValueDetails(List<string> categoryDisaggregations)
        {
            var categoryDisaggregationInfos = _categoryDisaggreationsRepository.Gets(false, x => categoryDisaggregations.Contains(x.CategoryDisaggregationName));

            return categoryDisaggregationInfos;
        }
        public IEnumerable<CategoryDisaggregations> GetInCatalogValueByName(string categoryDisaggregationName)
        {
            var categoryDisaggregationInfos = _categoryDisaggreationsRepository.Gets(false, x => x.CategoryDisaggregationName == categoryDisaggregationName);

            return categoryDisaggregationInfos;
        }

        /// <summary>
        /// Lấy ra tất cả các loại hồ sơ. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..
        /// </summary>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="spec">Điều kiện</param>
        /// <typeparam name="TOutput">Kiểu đầu ra.</typeparam>
        /// <returns>Danh sách các thực thể được ánh xạ</returns>
        public IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<CategoryDisaggregations, TOutput>> projector, Expression<Func<CategoryDisaggregations, bool>> spec = null)
        {
            return _categoryDisaggreationsRepository.GetsAs(projector, spec);
        }
    }
}
