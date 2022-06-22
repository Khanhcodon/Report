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
    /// Interface : IWardDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 221013
    /// Author      : DungHV
    /// Description : DAL tương ứng với bảng Ward trong CSDL
    /// </summary>
    public interface IWardDal
    {
        /// <summary>
        /// Lấy ra tất cả xã/phường với điều kiện truyền vào. Nếu điều kiện bằng null thì sẽ lấy ra tất cả các xã/phường
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện</param>
        /// <returns>Danh sách xã/phường</returns>
        IEnumerable<Ward> Gets(Expression<Func<Ward, bool>> spec = null,
                                    Func<IQueryable<Ward>, IQueryable<Ward>> preFilter = null,
                                    params Func<IQueryable<Ward>, IQueryable<Ward>>[] postFilters);

        /// <summary>
        /// Lấy ra tất cả xã/phường phù hợp với điều kiện truyền vào. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..
        /// </summary>
        /// <typeparam name="TOutput">Kiểu đầu ra.</typeparam>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện. </param>
        /// <returns>Danh sách các thực thể được ánh xạ</returns>
        IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<Ward, TOutput>> projector, Expression<Func<Ward, bool>> spec = null, Func<IQueryable<Ward>, IQueryable<Ward>> preFilter = null, params Func<IQueryable<Ward>, IQueryable<Ward>>[] postFilters);

        /// <summary>
        /// Lấy ra xã/phường theo id
        /// </summary>
        /// <param name="wardId">Id của xã/phường</param>
        /// <returns>Entity xã/phường</returns>
        Ward Get(int wardId);

        /// <summary>
        /// Tạo mới xã/phường
        /// </summary>
        /// <param name="ward">Entity xã/phường</param>
        void Create(Ward ward);

        /// <summary>
        /// Cập nhật thông tin xã/phường
        /// </summary>
        /// <param name="ward">Entity xã/phường</param>
        void Update(Ward ward);

        /// <summary>
        /// Xóa xã/phường
        /// </summary>
        /// <param name="ward">Entity xã/phường</param>
        void Delete(Ward ward);

        /// <summary>
        /// Kiểm tra sự tồn tại của xã/phường phù hợp với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>true: nếu có tồn tại ít nhất 1 nhảy số phù hợp, ngược lại: false</returns>
        bool Exist(Expression<Func<Ward, bool>> spec);

        /// <summary>
        /// Lấy ra tổng số bản ghi với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Tổng số bản ghi</returns>
        int Count(Expression<Func<Ward, bool>> spec = null);
    }
}
