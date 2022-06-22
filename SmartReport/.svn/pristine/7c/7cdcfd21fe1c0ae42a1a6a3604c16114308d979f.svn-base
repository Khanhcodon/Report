using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : WardBll - public - BLL</para>
    /// <para>Create Date : 171013</para>
    /// <para>Author      : DungHV</para>
    /// <para>Description : BLL tương ứng với bảng Ward trong CSDL</para>
    /// </summary>
    public class WardBll : ServiceBase
    {
        private readonly IRepository<Ward> _wardRepository;
        private readonly AdminGeneralSettings _generalSettings;

        /// <summary>
        /// Khởi tạo class <see cref="WardBll"/>.
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="generalSettings">Cấu hình chung</param>
        public WardBll(IDbCustomerContext context, AdminGeneralSettings generalSettings)
            : base(context)
        {
            _wardRepository = Context.GetRepository<Ward>();
            _generalSettings = generalSettings;
        }

        /// <summary>
        /// Xóa 1 xã/phường
        /// </summary>
        /// <param name="ward">Thực thể xã/phường</param>
        public void Delete(Ward ward)
        {
            if (ward == null)
            {
                throw new ArgumentNullException("ward");
            }
            _wardRepository.Delete(ward);
            Context.SaveChanges();
        }

        /// <summary>
        /// Lất ra tất cả xã/phường theo điều kiện. Kết quả chỉ đọc
        /// </summary>
        /// <param name="spec">Điều kiện.</param>
        /// <returns></returns>
        public IEnumerable<Ward> Gets(Expression<Func<Ward, bool>> spec = null)
        {
            return _wardRepository.GetsReadOnly(spec);
        }

        /// <summary>
        /// Lấy ra một xã/phường
        /// </summary>
        /// <param name="wardId">Id của xã/phường</param>
        /// <returns>Entity xã/phường</returns>
        public Ward Get(int wardId)
        {
            Ward ward = null;
            if (wardId > 0)
            {
                ward = _wardRepository.Get(wardId);
            }
            return ward;
        }

        /// <summary>
        /// Thêm mới xã/phường
        /// </summary>
        /// <param name="ward">Thực thể xã/phường</param>
        /// <returns></returns>
        public void Create(Ward ward)
        {
            if (ward == null)
            {
                throw new ArgumentNullException("ward");
            }
            if (_wardRepository.Exist(WardQuery.WithName(ward.WardName)))
            {
                throw new EgovException(string.Format("Xã/phường ({0}) đã tồn tại!", ward.WardName));
            }
            _wardRepository.Create(ward);
            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật thông tin xã/phường
        /// </summary>
        /// <param name="ward">Entity xã/phường</param>
        /// <param name="oldWardName">Tên xã/phường trước khi cập nhật</param>
        public void Update(Ward ward, string oldWardName)
        {
            if (ward == null)
            {
                throw new ArgumentNullException("ward");
            }
            if (_wardRepository.Exist(p => p.WardName.Trim()
                      .ToLower().Equals(oldWardName.ToLower())))
            {
                throw new EgovException(string.Format("Quận/huyện ({0}) đã tồn tại!", ward.WardName));
            }

            Context.SaveChanges();
        }

        /// <summary>
        /// Lấy ra tất cả các xã/phường có phân trang. Kết quả chỉ đọc
        /// </summary>
        /// <param name="totalRecords">Tổng số bản ghi</param>
        /// <param name="currentPage">Trang hiện tại</param>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="sortBy">Sắp xếp theo</param>
        /// <param name="isDescending">Sắp xếp từ lớn đến nhỏ: true, ngược lại false</param>
        /// <param name="districtCode">Mã của quận/huyện. Nếu để trống sẽ bỏ qua điều kiện tìm kiếm này, nếu có giá trị sẽ tìm tất cả các xã/phường có mã quận/huyện là mã truyền vào</param>
        /// <returns>Danh sách các xã/phường đã được phân trang</returns>
        public IEnumerable<Ward> Gets(out int totalRecords,
                                                        int currentPage = 1,
                                                        int? pageSize = null,
                                                        string sortBy = "",
                                                        bool isDescending = false,
                                                        string districtCode = "")
        {
            var spec = !string.IsNullOrWhiteSpace(districtCode)
                        ? WardQuery.WithDistrictCode(districtCode)
                        : null;
            if (!pageSize.HasValue)
            {
                pageSize = _generalSettings.DefaultPageSize;
            }
            totalRecords = _wardRepository.Count(spec);
            var sort = Context.Filters.CreateSort<Ward>(isDescending, sortBy);
            return _wardRepository.GetsReadOnly(spec, sort, Context.Filters.Page<Ward>(currentPage, pageSize.Value));
        }
    }
}
