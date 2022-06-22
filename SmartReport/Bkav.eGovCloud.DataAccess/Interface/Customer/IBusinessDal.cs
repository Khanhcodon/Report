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
    /// Interface : IBusinessDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 171013
    /// Author      : DungHV
    /// Description : DAL tương ứng với bảng Business trong CSDL
    /// </summary>
    public interface IBusinessDal
    {
        /// <summary>
        /// Lấy ra tất cả các doanh nghiệp phù hợp với điều kiện truyền vào. Nếu điều kiện bằng null thì sẽ lấy ra tất cả các doanh nghiệp
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện</param>
        /// <returns>Danh sách các doanh nghiệp</returns>
        IEnumerable<Business> Gets(Expression<Func<Business, bool>> spec = null,
                                    Func<IQueryable<Business>, IQueryable<Business>> preFilter = null,
                                    params Func<IQueryable<Business>, IQueryable<Business>>[] postFilters);

        /// <summary>
        /// Lấy ra tất cả các doanh nghiệp phù hợp với điều kiện truyền vào. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..
        /// </summary>
        /// <typeparam name="TOutput">Kiểu đầu ra.</typeparam>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện. </param>
        /// <returns>Danh sách các thực thể được ánh xạ</returns>
        IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<Business, TOutput>> projector, Expression<Func<Business, bool>> spec = null, Func<IQueryable<Business>, IQueryable<Business>> preFilter = null, params Func<IQueryable<Business>, IQueryable<Business>>[] postFilters);

        /// <summary>
        /// Lấy ra doanh nghiệp theo id
        /// </summary>
        /// <param name="businessId">Id của doanh nghiệp</param>
        /// <returns>Entity doanh nghiệp</returns>
        Business Get(int businessId);

        /// <summary>
        /// Lấy ra doanh nghiệp theo tên
        /// </summary>
        /// <param name="businessName">Id của doanh nghiệp</param>
        /// <returns>Entity doanh nghiệp</returns>
        Business Get(string businessName);

        /// <summary>
        /// Tạo mới doanh nghiệp
        /// </summary>
        /// <param name="businessId">Entity doanh nghiệp</param>
        void Create(Business businessId);

        /// <summary>
        /// Cập nhật thông tin doanh nghiệp
        /// </summary>
        /// <param name="businessId">Entity doanh nghiệp</param>
        void Update(Business businessId);

        /// <summary>
        /// Xóa doanh nghiệp
        /// </summary>
        /// <param name="businessId">Entity doanh nghiệp</param>
        void Delete(Business businessId);

        /// <summary>
        /// Kiểm tra sự tồn tại của doanh nghiệp phù hợp với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>true: nếu có tồn tại ít nhất 1 nhảy số phù hợp, ngược lại: false</returns>
        bool Exist(Expression<Func<Business, bool>> spec);

        /// <summary>
        /// Lấy ra tổng số bản ghi với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Tổng số bản ghi</returns>
        int Count(Expression<Func<Business, bool>> spec = null);
    }
}
