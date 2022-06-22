using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Customer.Ad_Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Bkav.eGovCloud.Business.Customer.Ad_Report
{
    /// <summary>
    /// 
    /// </summary>
    public class Ad_TagetsBll : ServiceBase
    {
        private readonly IRepository<Ad_targets> _targetsRepository;
        private readonly ResourceBll _resourceService;
        public Ad_TagetsBll(IDbCustomerContext context, ResourceBll resourceService) : base(context)
        {
            _targetsRepository = Context.GetRepository<Ad_targets>();
            _resourceService = resourceService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="indata"></param>
        public void Create(Ad_targets indata)
        {
            if (indata == null)
            {
                throw new ArgumentNullException("IndicatorCatalog");
            }

            _targetsRepository.Create(indata);
            Context.SaveChanges();
        }


        public void Create(IEnumerable<Ad_targets> indatas)
        {
            foreach (var indata in indatas)
            {
                _targetsRepository.Create(indata);
            }
            Context.SaveChanges();
        }

        /// <summary> Tienbv 121019
        /// Lấy danh mục.
        /// </summary>
        /// <param name="id">the catalog id.</param>
        /// <returns>The Catalog object.</returns>
        public Ad_targets Get(int id)
        {
            var result = _targetsRepository.Get(id);
            return result;
        }

        /// <summary> Tienbv 261012
        /// Cập nhật danh mục.
        /// </summary>
        /// <param name="indata">danh mục.</param>
        public void Update(Ad_targets indata)
        {
            if (indata == null)
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
        /// <param name="indata">the catalog.</param>
        public void Detele(Ad_targets indata)
        {
            _targetsRepository.Delete(indata);
            Context.SaveChanges();
        }

        public void Detele(IEnumerable<Ad_targets> indatas)
        {
            foreach (var indata in indatas)
            {
                _targetsRepository.Delete(indata);
            }
            Context.SaveChanges();
        }

        public object Get(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <summary> TienBV 231012
        /// Lấy ra tất cả các danh mục có sắp xếp. Kết quả chỉ để đọc
        /// </summary>
        /// <param name="sortBy">Sắp xếp theo</param>
        /// <param name="isDescending">Sắp xếp từ lớn đến nhỏ: true, ngược lại false</param>
        /// <returns>Danh sách các tài nguyên đã được phân trang</returns>
        public IEnumerable<Ad_targets> Gets(string sortBy = "", bool isDescending = false)
        {
            return _targetsRepository.GetsReadOnly(null, Context.Filters.CreateSort<Ad_targets>(isDescending, sortBy));
        }

        /// <summary> Tienbv 011112
        /// Lấy ra tất cả catalog theo điều kiện kỹ thuật truyền vào. Kết quả chỉ để đọc
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <returns></returns>
        public IEnumerable<Ad_targets> Gets(Expression<Func<Ad_targets, bool>> spec = null)
        {
            return _targetsRepository.GetsReadOnly(spec);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public IEnumerable<Ad_targets> Gets(bool isReadOnly = true, Expression<Func<Ad_targets, bool>> spec = null)
        {
            return _targetsRepository.Gets(isReadOnly, spec);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="projector"></param>
        /// <param name="spec"></param>
        /// <returns></returns>
        public IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<Ad_targets, TOutput>> projector, Expression<Func<Ad_targets, bool>> spec = null)
        {
            return _targetsRepository.GetsAs(projector, spec);
        }
    }
}
