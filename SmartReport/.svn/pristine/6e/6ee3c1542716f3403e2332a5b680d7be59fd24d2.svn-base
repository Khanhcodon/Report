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
    /// Interface : IDistrictDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 221013
    /// Author      : DungHV
    /// Description : DAL tương ứng với bảng District trong CSDL
    /// </summary>
    public interface IDistrictDal
    {
        /// <summary>
        /// Lấy ra tất cả quận/huyện với điều kiện truyền vào. Nếu điều kiện bằng null thì sẽ lấy ra tất cả các loại doanh nghiệp
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện</param>
        /// <returns>Danh sách tỉnh/thành phố</returns>
        IEnumerable<District> Gets(Expression<Func<District, bool>> spec = null,
                                    Func<IQueryable<District>, IQueryable<District>> preFilter = null,
                                    params Func<IQueryable<District>, IQueryable<District>>[] postFilters);

        /// <summary>
        /// Lấy ra tất cả quận/huyện phù hợp với điều kiện truyền vào. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..
        /// </summary>
        /// <typeparam name="TOutput">Kiểu đầu ra.</typeparam>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện. </param>
        /// <returns>Danh sách các thực thể được ánh xạ</returns>
        IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<District, TOutput>> projector, Expression<Func<District, bool>> spec = null, Func<IQueryable<District>, IQueryable<District>> preFilter = null, params Func<IQueryable<District>, IQueryable<District>>[] postFilters);

        /// <summary>
        /// Lấy ra quận/huyện theo id
        /// </summary>
        /// <param name="districtId">Id của quận/huyện</param>
        /// <returns>Entity quận/huyện</returns>
        District Get(int districtId);

        /// <summary>
        /// Tạo mới quận/huyện
        /// </summary>
        /// <param name="district">Entity quận/huyện</param>
        void Create(District district);

        /// <summary>
        /// Cập nhật thông tin quận/huyện
        /// </summary>
        /// <param name="district">Entity quận/huyện</param>
        void Update(District district);

        /// <summary>
        /// Xóa quận/huyện
        /// </summary>
        /// <param name="district">Entity quận/huyện</param>
        void Delete(District district);

        /// <summary>
        /// Kiểm tra sự tồn tại của quận/huyện phù hợp với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>true: nếu có tồn tại ít nhất 1 nhảy số phù hợp, ngược lại: false</returns>
        bool Exist(Expression<Func<District, bool>> spec);

        /// <summary>
        /// Lấy ra tổng số bản ghi với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Tổng số bản ghi</returns>
        int Count(Expression<Func<District, bool>> spec = null);
    }
}
