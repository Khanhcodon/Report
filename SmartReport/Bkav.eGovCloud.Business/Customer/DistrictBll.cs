using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : DistrictBll - public - BLL</para>
    /// <para>Create Date : 171013</para>
    /// <para>Author      : DungHV</para>
    /// <para>Description : BLL tương ứng với bảng district trong CSDL</para>
    /// </summary>
    public class DistrictBll : ServiceBase
    {
        private readonly IRepository<District> _districtRepository;
        private readonly AdminGeneralSettings _generalSettings;
        private readonly ResourceBll _resourceService;

        /// <summary>
        /// Khởi tạo class <see cref="DistrictBll"/>.
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="generalSettings">Cấu hình chung</param>
        /// <param name="resourceService"></param>
        public DistrictBll(IDbCustomerContext context,
            AdminGeneralSettings generalSettings,
             ResourceBll resourceService)
            : base(context)
        {
            _districtRepository = Context.GetRepository<District>();
            _generalSettings = generalSettings;
            _resourceService = resourceService;
        }

        /// <summary>
        /// Xóa 1 quận/huyện
        /// </summary>
        /// <param name="district">Thực thể quận/huyện</param>
        public void Delete(District district)
        {
            if (district == null)
            {
                throw new ArgumentNullException("district");
            }
            //TODO: Cần kiểm tra ràng buộc dữ liệu trong các bảng: category_code, store_category
            //var isUsed = _documentBll.Gets(d => d.CategoryId == category.CategoryId).Any();
            //if(isUsed)
            //{
            //    throw new EgovException(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Category.Delete.Exception.Document"));
            //}
            _districtRepository.Delete(district);
            Context.SaveChanges();
        }

        /// <summary>
        /// Kiểm tra xem có phần tử nào phù hợp với điều kiện truyền vào hay không
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public bool Exist(Expression<Func<District, bool>> spec)
        {
            return _districtRepository.Exist(spec);
        }

        /// <summary>
        /// Lất ra tất cả quận/huyện theo điều kiện. Kết quả chỉ đọc
        /// </summary>
        /// <param name="spec">Điều kiện.</param>
        /// <returns></returns>
        public IEnumerable<District> Gets(Expression<Func<District, bool>> spec = null)
        {
            return _districtRepository.GetsReadOnly(spec);
        }

        /// <summary>
        /// Lấy ra một quận/huyện
        /// </summary>
        /// <param name="districtId">Id của quận/huyện</param>
        /// <returns>Entity quận/huyện</returns>
        public District Get(int districtId)
        {
            District district = null;
            if (districtId > 0)
            {
                district = _districtRepository.Get(districtId);
            }
            return district;
        }

        /// <summary>
        /// Lấy ra một quận/huyện
        /// </summary>
        /// <param name="districtCode">Mã quận quyện</param>
        /// <returns>Entity quận/huyện</returns>
        public District Get(string districtCode)
        {
            return _districtRepository.GetReadOnly(d => d.DistrictCode == districtCode);
        }

        /// <summary>
        /// Thêm mới quận/huyện
        /// </summary>
        /// <param name="district">Thực thể quận/huyện</param>
        /// <returns></returns>
        public void Create(District district)
        {
            if (district == null)
            {
                throw new ArgumentNullException("district");
            }
            if (Exist(DistrictQuery.WithName(district.DistrictName)))
            {
                throw new EgovException(string.Format("Quận/huyện ({0}) đã tồn tại!", district.DistrictName));
            }
            _districtRepository.Create(district);
            Context.SaveChanges();
        }

        /// <summary>
        /// Thêm mới quận/huyện
        /// </summary>
        /// <param name="districts">Danh sách quận/huyện</param>
        /// <param name="ignoreExist">True: bỏ qua những quận huyện đã tồn tại; False: validate khi có quận huyện đã tồn tại</param>
        public void Create(IEnumerable<District> districts, bool ignoreExist)
        {
            if (districts == null || !districts.Any())
            {
                throw new ArgumentNullException("districts");
            }

            var names = districts.Select(x => x.DistrictName);
            var exist = _districtRepository.GetsAs(p => p.DistrictName, p => names.Contains(p.DistrictName));

            if (exist != null && exist.Any())
            {
                if (!ignoreExist)
                {
                    throw new EgovException(_resourceService.GetResource("District.Create.Exist"));
                }

                var list = districts.Where(p => !exist.Contains(p.DistrictName));
                if (list == null || !list.Any())
                {
                    throw new EgovException(_resourceService.GetResource("District.Create.Exist"));
                }
                Create(list);
            }
            else
            {
                Create(districts);
            }
        }

        private void Create(IEnumerable<District> districts)
        {
            Context.Configuration.AutoDetectChangesEnabled = false;
            foreach (var district in districts)
            {
                _districtRepository.Create(district);
            }
            Context.Configuration.AutoDetectChangesEnabled = true;
            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật thông tin quận/huyện
        /// </summary>
        /// <param name="district">Entity quận/huyện</param>
        /// <param name="oldDistrictName">Tên tỉnh/thành phố trước khi cập nhật</param>
        public void Update(District district, string oldDistrictName)
        {
            if (district == null)
            {
                throw new ArgumentNullException("district");
            }
            if (Exist(DistrictQuery.WithName(district.DistrictName.Trim()).And(r => r.DistrictName.ToLower() != oldDistrictName.Trim().ToLower())))
            {
                throw new EgovException(string.Format("Quận/huyện ({0}) đã tồn tại!", district.DistrictName));
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Lấy ra tất cả các quận/huyện có phân trang
        /// </summary>
        /// <param name="totalRecords">Tổng số bản ghi</param>
        /// <param name="currentPage">Trang hiện tại</param>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="sortBy">Sắp xếp theo</param>
        /// <param name="isDescending">Sắp xếp từ lớn đến nhỏ: true, ngược lại false</param>
        /// <param name="cityCode">Mã tỉnh/thành phố. Nếu để trống sẽ bỏ qua điều kiện tìm kiếm này, nếu có giá trị sẽ tìm tất cả các quận/huyện có mã tỉnh/thành phố là mã truyền vào</param>
        /// <returns>Danh sách các quận/huyện đã được phân trang</returns>
        public IEnumerable<District> Gets(out int totalRecords,
                                                        int currentPage = 1,
                                                        int? pageSize = null,
                                                        string sortBy = "",
                                                        bool isDescending = false,
                                                        string cityCode = "")
        {
            var spec = !string.IsNullOrWhiteSpace(cityCode)
                        ? DistrictQuery.WithCityCode(cityCode)
                        : null;
            if (!pageSize.HasValue)
            {
                pageSize = _generalSettings.DefaultPageSize;
            }
            totalRecords = _districtRepository.Count(spec);
            var sort = Context.Filters.CreateSort<District>(isDescending, sortBy);
            return _districtRepository.GetsReadOnly(spec, sort, Context.Filters.Page<District>(currentPage, pageSize.Value));
        }
    }
}
