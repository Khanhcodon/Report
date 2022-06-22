using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Common;

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
    public class BusinessesBll : ServiceBase
    {
        private readonly IRepository<Entities.Customer.Business> _businessRepository;
        private readonly AdminGeneralSettings _generalSettings;

        /// <summary>
        /// Khởi tạo class <see cref="BusinessTypeBll"/>.
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="generalSettings">Cấu hình chung</param>
        public BusinessesBll(IDbCustomerContext context, AdminGeneralSettings generalSettings)
            : base(context)
        {
            _businessRepository = Context.GetRepository<Entities.Customer.Business>();
            _generalSettings = generalSettings;
        }

        /// <summary>
        /// Xóa 1 thể loại văn bản
        /// </summary>
        /// <param name="business">Thực thể thể loại văn bản</param>
        public void Delete(Entities.Customer.Business business)
        {
            if (business == null)
            {
                throw new ArgumentNullException("business");
            }
            //TODO: Cần kiểm tra ràng buộc dữ liệu trong các bảng: category_code, store_category
            //var isUsed = _documentBll.Gets(d => d.CategoryId == category.CategoryId).Any();
            //if(isUsed)
            //{
            //    throw new EgovException(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Category.Delete.Exception.Document"));
            //}
            _businessRepository.Delete(business);
            Context.SaveChanges();
        }

        /// <summary>
        /// Lất ra tất cả các doanh nghiệp theo điều kiện. Kết quả chỉ để đọc
        /// </summary>
        /// <param name="spec">Điều kiện.</param>
        /// <returns></returns>
        public IEnumerable<Entities.Customer.Business> Gets(Expression<Func<Entities.Customer.Business, bool>> spec = null)
        {
            return _businessRepository.GetsReadOnly(spec);
        }

        /// <summary>
        /// Lấy ra tất cả thể loại văn bản có phân trang và xắp xếp.
        /// </summary>
        /// <returns>Danh sách loại doanh nghiệp</returns>
        public bool Exist(Expression<Func<Entities.Customer.Business, bool>> spec = null)
        {
            return _businessRepository.Exist(spec);
        }

        /// <summary>
        /// Lấy ra một doanh nghiệp
        /// </summary>
        /// <param name="businessId">Id của doanh nghiệp</param>
        /// <returns>Entity doanh nghiệp</returns>
        public Entities.Customer.Business Get(int businessId)
        {
            Entities.Customer.Business business = null;
            if (businessId > 0)
            {
                business = _businessRepository.Get(businessId);
            }
            return business;
        }

        /// <summary>
        /// Lấy ra một doanh nghiệp. Kết quả chỉ đọc
        /// </summary>
        /// <param name="businessName">Tên của doanh nghiệp</param>
        /// <returns>Entity doanh nghiệp</returns>
        public Entities.Customer.Business Get(string businessName)
        {
            Entities.Customer.Business business = null;
            if (string.IsNullOrWhiteSpace(businessName))
            {
                business = _businessRepository.GetReadOnly(b => b.BusinessName == businessName);
            }
            return business;
        }

        /// <summary>
        /// Thêm mới doanh nghiệp
        /// </summary>
        /// <param name="business">Thực thể doanh nghiệp</param>
        /// <returns></returns>
        public void Create(Entities.Customer.Business business)
        {
            if (business == null)
            {
                throw new ArgumentNullException("business");
            }
            if (Exist(BusinessQuery.WithName(business.BusinessName)))
            {
                throw new EgovException(string.Format("Thể loại văn bản ({0}) đã tồn tại!", business.BusinessName));
            }
            _businessRepository.Create(business);
            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật thông tin doanh nghiệp
        /// </summary>
        /// <param name="business">Entity doanh nghiệp</param>
        /// <param name="oldbusinessName">Tên doanh nghiệp trước khi cập nhật</param>
        public void Update(Entities.Customer.Business business, string oldbusinessName)
        {
            if (business == null)
            {
                throw new ArgumentNullException("business");
            }
            if (Exist(BusinessQuery.WithName(business.BusinessName.Trim()).And(r => r.BusinessName.ToLower() != oldbusinessName.Trim().ToLower())))
            {
                throw new EgovException(string.Format("Thể loại văn bản ({0}) đã tồn tại!", business.BusinessName));
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Lấy ra tất cả các doanh nghiệp có phân trang
        /// </summary>
        /// <param name="totalRecords">Tổng số bản ghi</param>
        /// <param name="currentPage">Trang hiện tại</param>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="sortBy">Sắp xếp theo</param>
        /// <param name="isDescending">Sắp xếp từ lớn đến nhỏ: true, ngược lại false</param>
        /// <param name="businessTypeId">Id loại hình doanh nghiệp. Nếu để trống sẽ bỏ qua điều kiện tìm kiếm này, nếu có giá trị sẽ tìm tất cả các doanh nghiệp thuộc loại hình doanh nghiệp truyền vào</param>
        /// <param name="cityCode">Mã tỉnh thành. Nếu để trống sẽ bỏ qua điều kiện tìm kiếm này, nếu có giá trị sẽ tìm tất cả các doanh nghiệp có mã tỉnh thành là giá trị truyền vào</param>
        /// <param name="districtCode">mã quận huyện. Nếu để trống sẽ bỏ qua điều kiện tìm kiếm này, nếu có giá trị sẽ tìm tất cả các doanh nghiệp có mã quận huyện là giá trị truyền vào</param>
        /// <param name="wardId">Id xã phường. Nếu để trống sẽ bỏ qua điều kiện tìm kiếm này, nếu có giá trị sẽ tìm tất cả các doanh nghiệp có id xã phường là giá trị truyền vào</param>
        ///  <returns>Danh sách các doanh nghiệp đã được phân trang</returns>
        public IEnumerable<Entities.Customer.Business> Gets(out int totalRecords,
                                            int currentPage = 1,
                                            int? pageSize = null,
                                            string sortBy = "",
                                            bool isDescending = false,
                                            int? businessTypeId = null,
                                            string cityCode = "",
                                            string districtCode = "",
                                            int? wardId = null)
        {
            var spec = BusinessQuery.WithTypeId(businessTypeId).And(BusinessQuery.WithCityCode(cityCode)).And(BusinessQuery.WithDistrictCode(districtCode)).And(BusinessQuery.WithWardId(wardId));
            if (!pageSize.HasValue)
            {
                pageSize = _generalSettings.DefaultPageSize;
            }
            totalRecords = _businessRepository.Count(spec);
            var sort = Context.Filters.CreateSort<Entities.Customer.Business>(isDescending, sortBy);
            return _businessRepository.GetsReadOnly(spec, sort, Context.Filters.Page<Entities.Customer.Business>(currentPage, pageSize.Value));
        }
    }
}
