using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Customer;
using System.Data.Entity.Validation;

namespace Bkav.eGovCloud.Business.Customer
{
    public class Ad_LocalityBll : ServiceBase
    {
        private readonly IRepository<Ad_Locality> _ad_localityRepository;
        private readonly ResourceBll _resourceService;

        /// <summary>
        /// Khởi tạo class <see cref="Ad_LocalityBll"/>.
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="resourceService"></param>
        public Ad_LocalityBll(IDbCustomerContext context, ResourceBll resourceService)
            : base(context)
        {
            _ad_localityRepository = Context.GetRepository<Ad_Locality>();
            _resourceService = resourceService;
        }

        /// <summary>
        /// Tạo mới chỉ tiêu phân tổ
        /// </summary>
        /// <param name="ad_locality">the catalog object.</param>
        public void Create(Ad_Locality ad_locality)
        {
            if (ad_locality == null)
            {
                throw new ArgumentNullException("Ad_Locality");
            }
            if (_ad_localityRepository.Exist(c => c.LocalityName == ad_locality.LocalityName))
            {
                throw new EgovException("Địa bàn này đã tồn tại!");
            }
            ad_locality.LocalityId = Guid.NewGuid();
            _ad_localityRepository.Create(ad_locality);
            try
            {
                Context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    throw new EgovException(eve.ToString());
                }
            }

        }

        /// <summary>
        /// Tạo mới chỉ tiêu phân tổ
        /// </summary>
        /// <param name="Ad_Locality">the catalog object.</param>
        /// <param name="ignoreExist"> True: Bỏ qua kiểm tra tồn tại; False: không kiểm tra tồn tại</param>
        public void Create(IEnumerable<Ad_Locality> ad_locality, bool ignoreExist = false)
        {
            if (ad_locality == null || !ad_locality.Any())
            {
                throw new ArgumentNullException("ad_locality");
            }

            var names = ad_locality.Select(c => c.LocalityName);
            var exists = _ad_localityRepository.GetsAs(p => p.LocalityName, p => names.Contains(p.LocalityName));
            if (exists != null && exists.Any())
            {
                if (!ignoreExist || exists.Count() == ad_locality.Count())
                {
                    throw new EgovException("Địa bàn này đã tồn tại!");
                }

                var list = ad_locality.Where(p => !exists.Contains(p.LocalityName));
                if (list == null || !list.Any())
                {
                    throw new EgovException("Địa bàn này đã tồn tại!");
                }
                Create(list);
            }
            else
            {
                Create(ad_locality);
            }
        }

        private void Create(IEnumerable<Ad_Locality> ad_locality)
        {
            foreach (var locality in ad_locality)
            {
                _ad_localityRepository.Create(locality);
            }
            Context.SaveChanges();
        }

        /// <summary> Tienbv 121019
        /// Lấy danh mục.
        /// </summary>
        /// <param name="id">the catalog id.</param>
        /// <returns>The Catalog object.</returns>
        public Ad_Locality Get(Guid id)
        {
            var result = _ad_localityRepository.Get(id);
            return result;
        }

        /// <summary> Tienbv 121019
        /// Lấy danh mục.
        /// </summary>
        /// <param name="id">the catalog id.</param>
        /// <returns>The Catalog object.</returns>
        public Ad_Locality GetParent(Guid? id)
        {
            var result = _ad_localityRepository.Get(id);
            return result;
        }

        /// <summary> Tienbv 261012
        /// Cập nhật danh mục.
        /// </summary>
        /// <param name="catalog">danh mục.</param>
        public void Update(Ad_Locality ad_locality)
        {
            if (ad_locality == null)
            {
                throw new ArgumentNullException("ad_locality");
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
        public void Delete(Ad_Locality ad_locality)
        {
            _ad_localityRepository.Delete(ad_locality);
            Context.SaveChanges();
        }

        /// <summary> TienBV 231012
        /// Lấy ra tất cả các danh mục có sắp xếp. Kết quả chỉ để đọc
        /// </summary>
        /// <param name="sortBy">Sắp xếp theo</param>
        /// <param name="isDescending">Sắp xếp từ lớn đến nhỏ: true, ngược lại false</param>
        /// <returns>Danh sách các tài nguyên đã được phân trang</returns>
        public IEnumerable<Ad_Locality> Gets(string sortBy = "", bool isDescending = false)
        {
            return _ad_localityRepository.GetsReadOnly(null, Context.Filters.CreateSort<Ad_Locality>(isDescending, sortBy));
        }

        /// <summary> Tienbv 011112
        /// Lấy ra tất cả catalog theo điều kiện kỹ thuật truyền vào. Kết quả chỉ để đọc
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <returns></returns>
        public IEnumerable<Ad_Locality> GetsAll(Expression<Func<Ad_Locality, bool>> spec = null)
        {
            return _ad_localityRepository.GetsReadOnly(spec);
        }

        /// <summary> Tienbv 011112
        /// Lấy ra tất cả catalog theo điều kiện kỹ thuật truyền vào. Kết quả chỉ để đọc
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <returns></returns>
        public IEnumerable<Ad_Locality> Gets(Expression<Func<Ad_Locality, bool>> spec = null)
        {
            return _ad_localityRepository.GetsReadOnly(spec);
        }

        /// <summary>
        /// Lấy ra tất cả các loại hồ sơ. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..
        /// </summary>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="spec">Điều kiện</param>
        /// <typeparam name="TOutput">Kiểu đầu ra.</typeparam>
        /// <returns>Danh sách các thực thể được ánh xạ</returns>
        public IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<Ad_Locality, TOutput>> projector, Expression<Func<Ad_Locality, bool>> spec = null)
        {
            return _ad_localityRepository.GetsAs(projector, spec);
        }
    }
}
