using System;
using System.Collections.Generic;
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
    /// <para>Class : BusinessTypeBll - public - BLL</para>
    /// <para>Create Date : 171013</para>
    /// <para>Author      : DungHV</para>
    /// <para>Description : BLL tương ứng với bảng BusinessType trong CSDL</para>
    /// </summary>
    public class CityBll : ServiceBase
    {
        private readonly IRepository<City> _cityRepository;
        private readonly ResourceBll _resourceService;
        private readonly AdminGeneralSettings _generalSettings;
        private readonly DistrictBll _districtService;
        private readonly BusinessesBll _businessService;

        /// <summary>
        /// Khởi tạo class <see cref="CityBll"/>.
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="resourceBll">Bll tương ứng với bảng resource trong CSDL</param>
        /// <param name="generalSettings">Cấu hình chung</param>
        /// <param name="districtService">Bll tương ứng với bảng district trong CSDL</param>
        /// <param name="businessService">Bll tương ứng với bảng district trong CSDL</param>
        public CityBll(IDbCustomerContext context, ResourceBll resourceBll, AdminGeneralSettings generalSettings, DistrictBll districtService, BusinessesBll businessService)
            : base(context)
        {
            _cityRepository = Context.GetRepository<City>();
            _resourceService = resourceBll;
            _generalSettings = generalSettings;
            _districtService = districtService;
            _businessService = businessService;
        }

        /// <summary>
        /// Xóa 1 tỉnh/thành phố
        /// </summary>
        /// <param name="city">Thực thể tỉnh/thành phố</param>
        public void Delete(City city)
        {
            if (city == null)
            {
                throw new ArgumentNullException("city");
            }
            //kiểm tra ràng buộc dữ liệu trong bảng district
            var isUsed = _districtService.Exist(d => d.CityCode == city.CityCode);
            if (isUsed)
            {
                throw new EgovException(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.City.Delete.Exception.District"));
            }
            //kiểm tra ràng buộc dữ liệu trong bảng business
            isUsed &= _businessService.Exist(b => b.CityCode == city.CityCode);
            if (isUsed)
            {
                throw new EgovException(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.City.Delete.Exception.Business"));
            }
            _cityRepository.Delete(city);
            Context.SaveChanges();
        }

        /// <summary>
        /// Lất ra tất cả tỉnh/thành phố theo điều kiện. Kết quả chỉ để đọc
        /// </summary>
        /// <param name="spec">Điều kiện.</param>
        /// <returns></returns>
        public IEnumerable<City> Gets(Expression<Func<City, bool>> spec = null)
        {
            return _cityRepository.GetsReadOnly(spec);
        }

        /// <summary>
        /// Lất ra tất cả tỉnh/thành phố theo điều kiện. Kết quả chỉ để đọc
        /// </summary>
        /// <param name="projector"></param>
        /// <param name="spec">Điều kiện.</param>
        /// <returns></returns>
        public IEnumerable<T> GetsAs<T>(Expression<Func<City, T>> projector = null, Expression<Func<City, bool>> spec = null)
        {
            return _cityRepository.GetsAs(projector, spec);
        }

        /// <summary>
        /// Lấy ra một tỉnh/thành phố
        /// </summary>
        /// <param name="cityId">Id của tỉnh/thành phố</param>
        /// <returns>Entity tỉnh/thành phố</returns>
        public City Get(int cityId)
        {
            City city = null;
            if (cityId > 0)
            {
                city = _cityRepository.Get(cityId);
            }
            return city;
        }

        /// <summary>
        /// Thêm mới tỉnh/thành phố
        /// </summary>
        /// <param name="city">Thực thể tỉnh/thành phố</param>
        /// <returns></returns>
        public void Create(City city)
        {
            if (city == null)
            {
                throw new ArgumentNullException("city");
            }

            if (_cityRepository.Exist(CityQuery.WithName(city.CityName).Or(CityQuery.WithCode(city.CityCode))))
            {
                throw new EgovException(string.Format("Tỉnh/thành phố ({0}) đã tồn tại!", city.CityName));
            }
            _cityRepository.Create(city);
            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật thông tin tỉnh/thành phố
        /// </summary>
        /// <param name="city">Entity tỉnh/thành phố</param>
        /// <param name="oldCityName">Tên tỉnh/thành phố trước khi cập nhật</param>
        public void Update(City city, string oldCityName)
        {
            if (city == null)
            {
                throw new ArgumentNullException("city");
            }
            if (_cityRepository.Exist(CityQuery.WithName(city.CityName.Trim()).And(r => r.CityName.ToLower() != oldCityName.Trim().ToLower())))
            {
                throw new EgovException(string.Format("Tỉnh/tành phố ({0}) đã tồn tại!", city.CityName));
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Lấy ra tất cả các tỉnh/thành phố có phân trang
        /// </summary>
        /// <param name="totalRecords">Tổng số bản ghi</param>
        /// <param name="currentPage">Trang hiện tại</param>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="sortBy">Sắp xếp theo</param>
        /// <param name="isDescending">Sắp xếp từ lớn đến nhỏ: true, ngược lại false</param>
        /// <param name="cityname">Key của resource (dùng để tìm kiếm) Nếu để trống sẽ bỏ qua điều kiện tìm kiếm này, nếu có giá trị sẽ tìm tất cả các resource có key gần giống với key truyền vào</param>
        /// <returns>Danh sách các tỉnh/thành phố đã được phân trang</returns>
        public IEnumerable<City> Gets(out int totalRecords,
            int currentPage = 1,
            int? pageSize = null,
            string sortBy = "",
            bool isDescending = false,
            string cityname = "")
        {
            var spec = !string.IsNullOrWhiteSpace(cityname)
                        ? CityQuery.ContainsName(cityname)
                        : null;
            if (!pageSize.HasValue)
            {
                pageSize = _generalSettings.DefaultPageSize;
            }
            totalRecords = _cityRepository.Count(spec);
            var sort = Context.Filters.CreateSort<City>(isDescending, sortBy);
            return _cityRepository.GetsReadOnly(spec, sort, Context.Filters.Page<City>(currentPage, pageSize.Value));
        }
    }
}
