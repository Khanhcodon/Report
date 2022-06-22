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
    /// Interface : IBusinessTypeDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 171013
    /// Author      : DungHV
    /// Description : DAL tương ứng với bảng BusinessType trong CSDL
    /// </summary>
    public interface IBusinessTypeDal
    {
        /// <summary>
        /// Lấy ra tất cả các loại doanh nghiệp với điều kiện truyền vào. Nếu điều kiện bằng null thì sẽ lấy ra tất cả các loại doanh nghiệp
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện</param>
        /// <returns>Danh sách các loại doanh nghiệp</returns>
        IEnumerable<BusinessType> Gets(Expression<Func<BusinessType, bool>> spec = null,
                                    Func<IQueryable<BusinessType>, IQueryable<BusinessType>> preFilter = null,
                                    params Func<IQueryable<BusinessType>, IQueryable<BusinessType>>[] postFilters);

        /// <summary>
        /// Lấy ra tất cả các loại doanh nghiệp phù hợp với điều kiện truyền vào. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..
        /// </summary>
        /// <typeparam name="TOutput">Kiểu đầu ra.</typeparam>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện. </param>
        /// <returns>Danh sách các thực thể được ánh xạ</returns>
        IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<BusinessType, TOutput>> projector, Expression<Func<BusinessType, bool>> spec = null, Func<IQueryable<BusinessType>, IQueryable<BusinessType>> preFilter = null, params Func<IQueryable<BusinessType>, IQueryable<BusinessType>>[] postFilters);

        /// <summary>
        /// Lấy ra loại doanh nghiệp theo id
        /// </summary>
        /// <param name="businesstypeId">Id của loại doanh nghiệp</param>
        /// <returns>Entity loại doanh nghiệp</returns>
        BusinessType Get(int businesstypeId);

        /// <summary>
        /// Tạo mới loại doanh nghiệp
        /// </summary>
        /// <param name="businessType">Entity loại doanh nghiệp</param>
        void Create(BusinessType businessType);

        /// <summary>
        /// Cập nhật thông tin loại doanh nghiệp
        /// </summary>
        /// <param name="businessType">Entity loại doanh nghiệp</param>
        void Update(BusinessType businessType);

        /// <summary>
        /// Xóa loại doanh nghiệp
        /// </summary>
        /// <param name="businessType">Entity loại doanh nghiệp</param>
        void Delete(BusinessType businessType);

        /// <summary>
        /// Kiểm tra sự tồn tại của loại doanh nghiệp phù hợp với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>true: nếu có tồn tại ít nhất 1 nhảy số phù hợp, ngược lại: false</returns>
        bool Exist(Expression<Func<BusinessType, bool>> spec);

        /// <summary>
        /// Lấy ra tổng số bản ghi với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Tổng số bản ghi</returns>
        int Count(Expression<Func<BusinessType, bool>> spec = null);
    }
}
