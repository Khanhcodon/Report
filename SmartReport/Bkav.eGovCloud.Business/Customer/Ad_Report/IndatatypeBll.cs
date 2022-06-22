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
    public class IndatatypeBll : ServiceBase
    {
        private readonly IRepository<Dim_indicatordatatype> _indatatypeRepository;
        private readonly ResourceBll _resourceService;
        public IndatatypeBll(IDbCustomerContext context, ResourceBll resourceService) : base(context)
        {
            _indatatypeRepository = Context.GetRepository<Dim_indicatordatatype>();
            _resourceService = resourceService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="indata"></param>
        public void Create(Dim_indicatordatatype indata)
        {
            if (indata == null)
            {
                throw new ArgumentNullException("IndicatorCatalog");
            }

            _indatatypeRepository.Create(indata);
            Context.SaveChanges();
        }


        public void Create(IEnumerable<Dim_indicatordatatype> indatas)
        {
            foreach (var indata in indatas)
            {
                _indatatypeRepository.Create(indata);
            }
            Context.SaveChanges();
        }

        /// <summary> Tienbv 121019
        /// Lấy danh mục.
        /// </summary>
        /// <param name="id">the catalog id.</param>
        /// <returns>The Catalog object.</returns>
        public Dim_indicatordatatype Get(int id)
        {
            var result = _indatatypeRepository.Get(id);
            return result;
        }

        /// <summary> Tienbv 261012
        /// Cập nhật danh mục.
        /// </summary>
        /// <param name="indata">danh mục.</param>
        public void Update(Dim_indicatordatatype indata)
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
        public void Detele(Dim_indicatordatatype indata)
        {
            _indatatypeRepository.Delete(indata);
            Context.SaveChanges();
        }

        public void Detele(IEnumerable<Dim_indicatordatatype> indatas)
        {
            foreach (var indata in indatas)
            {
                _indatatypeRepository.Delete(indata);
            }
            Context.SaveChanges();
        }

        /// <summary> TienBV 231012
        /// Lấy ra tất cả các danh mục có sắp xếp. Kết quả chỉ để đọc
        /// </summary>
        /// <param name="sortBy">Sắp xếp theo</param>
        /// <param name="isDescending">Sắp xếp từ lớn đến nhỏ: true, ngược lại false</param>
        /// <returns>Danh sách các tài nguyên đã được phân trang</returns>
        public IEnumerable<Dim_indicatordatatype> Gets(string sortBy = "", bool isDescending = false)
        {
            return _indatatypeRepository.GetsReadOnly(null, Context.Filters.CreateSort<Dim_indicatordatatype>(isDescending, sortBy));
        }

        /// <summary> Tienbv 011112
        /// Lấy ra tất cả catalog theo điều kiện kỹ thuật truyền vào. Kết quả chỉ để đọc
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <returns></returns>
        public IEnumerable<Dim_indicatordatatype> Gets(Expression<Func<Dim_indicatordatatype, bool>> spec = null)
        {
            return _indatatypeRepository.GetsReadOnly(spec);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public IEnumerable<Dim_indicatordatatype> Gets(bool isReadOnly = true, Expression<Func<Dim_indicatordatatype, bool>> spec = null)
        {
            return _indatatypeRepository.Gets(isReadOnly, spec);
        }
    }
}
