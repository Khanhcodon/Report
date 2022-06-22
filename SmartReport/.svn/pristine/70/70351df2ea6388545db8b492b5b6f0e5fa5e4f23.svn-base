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
    /// 
    /// </summary>
    public class DisaggregationBll : ServiceBase
    {
        private readonly IRepository<Disaggregation> _indicatorRepository;
        private readonly ResourceBll _resourceService;

        /// <summary>
        /// Khởi tạo class <see cref="DisaggregationBll"/>.
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="resourceService"></param>
        public DisaggregationBll(IDbCustomerContext context, ResourceBll resourceService)
            : base(context)
        {
            _indicatorRepository = Context.GetRepository<Disaggregation>();
            _resourceService = resourceService;
        }

        /// <summary>
        /// Tạo mới chỉ tiêu phân tổ
        /// </summary>
        /// <param name="indicator">the catalog object.</param>
        public void Create(Disaggregation indicator)
        {
            if (indicator == null)
            {
                throw new ArgumentNullException("Indicator");
            }
            if (_indicatorRepository.Exist(c => c.IndicatorName == indicator.IndicatorName))
            {
                throw new EgovException("Chỉ tiêu phân tổ đã tồn tại!");
            }
            indicator.IndicatorId = Guid.NewGuid();
            _indicatorRepository.Create(indicator);
            Context.SaveChanges();
        }

        /// <summary>
        /// Tạo mới chỉ tiêu phân tổ
        /// </summary>
        /// <param name="Indicator">the catalog object.</param>
        /// <param name="ignoreExist"> True: Bỏ qua kiểm tra tồn tại; False: không kiểm tra tồn tại</param>
        public void Create(IEnumerable<Disaggregation> indicator, bool ignoreExist = false)
        {
            if (indicator == null || !indicator.Any())
            {
                throw new ArgumentNullException("indicator");
            }

            var names = indicator.Select(c => c.IndicatorName);
            var exists = _indicatorRepository.GetsAs(p => p.IndicatorName, p => names.Contains(p.IndicatorName));
            if (exists != null && exists.Any())
            {
                if (!ignoreExist || exists.Count() == indicator.Count())
                {
                    throw new EgovException("Danh mục chỉ tiêu phân tổ đã đã tồn tại!");
                }

                var list = indicator.Where(p => !exists.Contains(p.IndicatorName));
                if (list == null || !list.Any())
                {
                    throw new EgovException("Danh mục chỉ tiêu phân tổ đã tồn tại!");
                }
                Create(list);
            }
            else
            {
                Create(indicator);
            }
        }

        private void Create(IEnumerable<Disaggregation> indicator)
        {
            foreach (var indica in indicator)
            {
                _indicatorRepository.Create(indica);
            }
            Context.SaveChanges();
        }

        /// <summary> Tienbv 121019
        /// Lấy danh mục.
        /// </summary>
        /// <param name="id">the catalog id.</param>
        /// <returns>The Catalog object.</returns>
        public Disaggregation Get(Guid id)
        {
            var result = _indicatorRepository.Get(id);
            return result;
        }

        /// <summary> Tienbv 261012
        /// Cập nhật danh mục.
        /// </summary>
        /// <param name="catalog">danh mục.</param>
        public void Update(Disaggregation indicator)
        {
            if (indicator == null)
            {
                throw new ArgumentNullException("indicator");
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
        public void Delete(Disaggregation indicator)
        {
            _indicatorRepository.Delete(indicator);
            Context.SaveChanges();
        }

        /// <summary> TienBV 231012
        /// Lấy ra tất cả các danh mục có sắp xếp. Kết quả chỉ để đọc
        /// </summary>
        /// <param name="sortBy">Sắp xếp theo</param>
        /// <param name="isDescending">Sắp xếp từ lớn đến nhỏ: true, ngược lại false</param>
        /// <returns>Danh sách các tài nguyên đã được phân trang</returns>
        public IEnumerable<Disaggregation> Gets(string sortBy = "", bool isDescending = false)
        {
            return _indicatorRepository.GetsReadOnly(null, Context.Filters.CreateSort<Disaggregation>(isDescending, sortBy));
        }

        /// <summary> Tienbv 011112
        /// Lấy ra tất cả catalog theo điều kiện kỹ thuật truyền vào. Kết quả chỉ để đọc
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <returns></returns>
        public IEnumerable<Disaggregation> Gets(Expression<Func<Disaggregation, bool>> spec = null)
        {
            return _indicatorRepository.GetsReadOnly(spec);
        }

        /// <summary>
        /// Lấy ra tất cả các loại hồ sơ. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..
        /// </summary>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="spec">Điều kiện</param>
        /// <typeparam name="TOutput">Kiểu đầu ra.</typeparam>
        /// <returns>Danh sách các thực thể được ánh xạ</returns>
        public IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<Disaggregation, TOutput>> projector, Expression<Func<Disaggregation, bool>> spec = null)
        {
            return _indicatorRepository.GetsAs(projector, spec);
        }
    }
}
