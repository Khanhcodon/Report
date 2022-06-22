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
    /// Interface : IFeeDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 010812
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng Fee trong CSDL
    /// </summary>
    public interface IFeeDal
    {
        /// <summary>
        /// Lấy ra tất cả các lệ phí phù hợp với điều kiện truyền vào. Nếu điều kiện bằng null thì sẽ lấy ra tất cả các lệ phí
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện</param>
        /// <returns>Danh sách các lệ phí</returns>
        IEnumerable<Fee> Gets(Expression<Func<Fee, bool>> spec = null,
                                    Func<IQueryable<Fee>, IQueryable<Fee>> preFilter = null,
                                    params Func<IQueryable<Fee>, IQueryable<Fee>>[] postFilters);

        /// <summary>
        /// Lấy ra tất cả các lệ phí phù hợp với điều kiện truyền vào. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..
        /// </summary>
        /// <typeparam name="TOutput">Kiểu đầu ra.</typeparam>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện. </param>
        /// <returns>Danh sách các thực thể được ánh xạ</returns>
        IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<Fee, TOutput>> projector, Expression<Func<Fee, bool>> spec = null, Func<IQueryable<Fee>, IQueryable<Fee>> preFilter = null, params Func<IQueryable<Fee>, IQueryable<Fee>>[] postFilters);

        /// <summary>
        /// Lấy ra lệ phí theo id
        /// </summary>
        /// <param name="feeId">Id của lệ phí</param>
        /// <returns>Entity lệ phí</returns>
        Fee Get(int feeId);

        /// <summary>
        /// Tạo mới lệ phí
        /// </summary>
        /// <param name="fee">Entity lệ phí</param>
        void Create(Fee fee);

        /// <summary>
        /// Cập nhật thông tin lệ phí
        /// </summary>
        /// <param name="fee">Entity lệ phí</param>
        void Update(Fee fee);

        /// <summary>
        /// Xóa lệ phí
        /// </summary>
        /// <param name="fee">Entity lệ phí</param>
        void Delete(Fee fee);

        /// <summary>
        /// Kiểm tra sự tồn tại của lệ phí phù hợp với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>true: nếu có tồn tại ít nhất 1 nhảy số phù hợp, ngược lại: false</returns>
        bool Exist(Expression<Func<Fee, bool>> spec);

        /// <summary>
        /// Lấy ra tổng số bản ghi với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Tổng số bản ghi</returns>
        int Count(Expression<Func<Fee, bool>> spec = null);
    }
}
