using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : BussinessDocFieldDocTypeGroupBll - public - BLL</para>
    /// <para>Access Modifiers:</para> 
    /// <para>Create Date : 150414</para>
    /// <para>Author      : HopCV</para>
    /// <para>Description : BLL tương ứng với bảng BussinessDocFieldDocTypeGroup trong CSDL</para>
    /// </summary>
    public class BussinessDocFieldDocTypeGroupBll : ServiceBase
    {
        private readonly IRepository<BussinessDocFieldDocTypeGroup> _bussinessDocFieldDocTypeGroupRepository;
        private readonly AdminGeneralSettings _generalSettings;

        /// <summary>
        ///  Khởi tạo
        /// </summary>
        /// <param name="context"></param>
        /// <param name="generalSettings"></param>
        public BussinessDocFieldDocTypeGroupBll(IDbCustomerContext context, AdminGeneralSettings generalSettings)
            : base(context)
        {
            _bussinessDocFieldDocTypeGroupRepository = Context.GetRepository<BussinessDocFieldDocTypeGroup>();
            _generalSettings = generalSettings;
        }

        /// <summary>
        /// Lấy ra danh sách các chữ ký của người dùng theo điều kiện kỹ thuật truyền vào. Kết quả chỉ để đọc
        /// </summary>
        /// <param name="spec">The spec.</param>
        public IEnumerable<BussinessDocFieldDocTypeGroup> Gets(Expression<Func<BussinessDocFieldDocTypeGroup, bool>> spec = null)
        {
            return _bussinessDocFieldDocTypeGroupRepository.GetsReadOnly(spec);
        }

        /// <summary>
        /// Lấy ra danh sách các chữ ký của người dùng theo điều kiện kỹ thuật truyền vào. 
        /// </summary>
        /// <typeparam name="T">Dữ liệu lấy ra</typeparam>
        /// <param name="totalRecords">Trả ra số lượng bản ghi</param>
        /// <param name="projector">Dữ liệu lấy ra</param>
        /// <param name="spec">Điều kiện truyển vào</param>
        /// <param name="currentPage">Trạng hiện tại</param>
        /// <param name="pageSize">Số lượng bản ghi cần láy</param>
        /// <param name="sortBy"></param>
        /// <param name="isDescending"></param>
        /// <returns></returns>
        public IEnumerable<T> Gets<T>(out int totalRecords,
            Expression<Func<BussinessDocFieldDocTypeGroup, T>> projector,
            Expression<Func<BussinessDocFieldDocTypeGroup, bool>> spec = null,
            int currentPage = 1,
            int? pageSize = null, string sortBy = "",
            bool isDescending = false)
        {
            if (!pageSize.HasValue)
            {
                pageSize = _generalSettings.DefaultPageSize;
            }

            totalRecords = _bussinessDocFieldDocTypeGroupRepository.Count(spec);
            var sort = Context.Filters.CreateSort<BussinessDocFieldDocTypeGroup>(isDescending, sortBy);
            var paging = Context.Filters.Page<BussinessDocFieldDocTypeGroup>(currentPage, pageSize.Value);
            return _bussinessDocFieldDocTypeGroupRepository.GetsAs(projector, spec, sort, paging);
        }

        /// <summary>
        /// Lấy ra danh sách các loại văn bản, biểu mẫu  - mẫu phôi  theo điều kiện kỹ thuật truyền vào.
        /// </summary>
        /// <param name="projector"></param>
        /// <param name="spec">The spec.</param>
        public IEnumerable<T> GetsAs<T>(Expression<Func<BussinessDocFieldDocTypeGroup, T>> projector,
            Expression<Func<BussinessDocFieldDocTypeGroup, bool>> spec = null)
        {
            return _bussinessDocFieldDocTypeGroupRepository.GetsAs(projector, spec);
        }

        /// <summary>
        /// Kiểm tra xem có phần tử nào phù hợp với điều kiện truyền vào hay không
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns></returns>
        public bool Exist(Expression<Func<BussinessDocFieldDocTypeGroup, bool>> spec)
        {
            return _bussinessDocFieldDocTypeGroupRepository.Exist(spec);
        }

        /// <summary>
        /// Tạo mới chữ ký người dùng
        /// </summary>
        /// <param name="bussinessDocFieldDocTypeGroup">Entity BussinessDocFieldDocTypeGroup</param>
        public void Create(BussinessDocFieldDocTypeGroup bussinessDocFieldDocTypeGroup)
        {
            if (bussinessDocFieldDocTypeGroup == null)
            {
                throw new ArgumentNullException("bussinessDocFieldDocTypeGroup");
            }
            _bussinessDocFieldDocTypeGroupRepository.Create(bussinessDocFieldDocTypeGroup);
            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật chữ ký người dùng
        /// </summary>
        /// <param name="bussinessDocFieldDocTypeGroup">Entity BussinessDocFieldDocTypeGroup</param>
        public void Update(BussinessDocFieldDocTypeGroup bussinessDocFieldDocTypeGroup)
        {
            if (bussinessDocFieldDocTypeGroup == null)
            {
                throw new ArgumentNullException("bussinessDocFieldDocTypeGroup");
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Xóa chữ ký người dùng
        /// </summary>
        /// <param name="bussinessDocFieldDocTypeGroup">Entity BussinessDocFieldDocTypeGroup</param>
        public void Delete(BussinessDocFieldDocTypeGroup bussinessDocFieldDocTypeGroup)
        {
            if (bussinessDocFieldDocTypeGroup == null)
            {
                throw new ArgumentNullException("bussinessDocFieldDocTypeGroup");
            }
            _bussinessDocFieldDocTypeGroupRepository.Delete(bussinessDocFieldDocTypeGroup);
            Context.SaveChanges();
        }

        /// <summary>
        /// Trả về BussinessDocFieldDocTypeGroup theo id
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public BussinessDocFieldDocTypeGroup Get(int id)
        {
            return _bussinessDocFieldDocTypeGroupRepository.Get(id);
        }
    }
}
