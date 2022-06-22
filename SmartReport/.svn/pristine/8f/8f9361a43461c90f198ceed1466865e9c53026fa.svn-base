using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Interface : IBusinessLicenseDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 261013
    /// Author      : DungHV
    /// Description : DAL tương ứng với bảng BusinessLicense trong CSDL
    /// </summary>
    public interface IBusinessLicenseDal
    {
        /// <summary>
        /// Lấy ra tất cả các giấy phép với điều kiện truyền vào. Nếu điều kiện bằng null thì sẽ lấy ra tất cả các giấy phép
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện</param>
        /// <returns>Danh sách các loại doanh nghiệp</returns>
        IEnumerable<BusinessLicense> Gets(Expression<Func<BusinessLicense, bool>> spec = null,
                                    Func<IQueryable<BusinessLicense>, IQueryable<BusinessLicense>> preFilter = null,
                                    params Func<IQueryable<BusinessLicense>, IQueryable<BusinessLicense>>[] postFilters);

        /// <summary>
        /// Lấy ra tất cả các giấy phép phù hợp với điều kiện truyền vào. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..
        /// </summary>
        /// <typeparam name="TOutput">Kiểu đầu ra.</typeparam>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện. </param>
        /// <returns>Danh sách các thực thể được ánh xạ</returns>
        IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<BusinessLicense, TOutput>> projector, Expression<Func<BusinessLicense, bool>> spec = null, Func<IQueryable<BusinessLicense>, IQueryable<BusinessLicense>> preFilter = null, params Func<IQueryable<BusinessLicense>, IQueryable<BusinessLicense>>[] postFilters);

        /// <summary>
        /// Lấy ra giấy phép theo id
        /// </summary>
        /// <param name="businesslicenseId">Id của giấy phép</param>
        /// <returns>Entity giấy phép</returns>
        BusinessLicense Get(int businesslicenseId);

        /// <summary>
        /// Tạo mới giấy phép
        /// </summary>
        /// <param name="businessLicense">Entity giấy phép</param>
        void Create(BusinessLicense businessLicense);

        /// <summary>
        /// Cập nhật thông tin giấy phép
        /// </summary>
        /// <param name="businessLicense">Entity giấy phép</param>
        void Update(BusinessLicense businessLicense);

        /// <summary>
        /// Xóa giấy phép
        /// </summary>
        /// <param name="businessLicense">Entity giấy phép</param>
        void Delete(BusinessLicense businessLicense);

        /// <summary>
        /// Kiểm tra sự tồn tại của giấy phép phù hợp với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>true: nếu có tồn tại ít nhất 1 nhảy số phù hợp, ngược lại: false</returns>
        bool Exist(Expression<Func<BusinessLicense, bool>> spec);

        /// <summary>
        /// Lấy ra tổng số bản ghi với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Tổng số bản ghi</returns>
        int Count(Expression<Func<BusinessLicense, bool>> spec = null);
    }
}
