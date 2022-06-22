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
    /// Interface : ICityDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 221013
    /// Author      : DungHV
    /// Description : DAL tương ứng với bảng City trong CSDL
    /// </summary>
    public interface ICityDal
    {
        /// <summary>
        /// Lấy ra tất cả tỉnh/thành phố với điều kiện truyền vào. Nếu điều kiện bằng null thì sẽ lấy ra tất cả các loại doanh nghiệp
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện</param>
        /// <returns>Danh sách tỉnh/thành phố</returns>
        IEnumerable<City> Gets(Expression<Func<City, bool>> spec = null,
                                    Func<IQueryable<City>, IQueryable<City>> preFilter = null,
                                    params Func<IQueryable<City>, IQueryable<City>>[] postFilters);

        /// <summary>
        /// Lấy ra tất cả tỉnh/thành phố phù hợp với điều kiện truyền vào. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..
        /// </summary>
        /// <typeparam name="TOutput">Kiểu đầu ra.</typeparam>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện. </param>
        /// <returns>Danh sách các thực thể được ánh xạ</returns>
        IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<City, TOutput>> projector, Expression<Func<City, bool>> spec = null, Func<IQueryable<City>, IQueryable<City>> preFilter = null, params Func<IQueryable<City>, IQueryable<City>>[] postFilters);

        /// <summary>
        /// Lấy ra tỉnh/thành phố theo id
        /// </summary>
        /// <param name="cityId">Id của tỉnh/thành phố</param>
        /// <returns>Entity tỉnh/thành phố</returns>
        City Get(int cityId);

        /// <summary>
        /// Tạo mới tỉnh/thành phố
        /// </summary>
        /// <param name="city">Entity tỉnh/thành phố</param>
        void Create(City city);

        /// <summary>
        /// Cập nhật thông tin tỉnh/thành phố
        /// </summary>
        /// <param name="city">Entity tỉnh/thành phố</param>
        void Update(City city);

        /// <summary>
        /// Xóa tỉnh/thành phố
        /// </summary>
        /// <param name="city">Entity tỉnh/thành phố</param>
        void Delete(City city);

        /// <summary>
        /// Kiểm tra sự tồn tại của tỉnh/thành phố phù hợp với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>true: nếu có tồn tại ít nhất 1 nhảy số phù hợp, ngược lại: false</returns>
        bool Exist(Expression<Func<City, bool>> spec);

        /// <summary>
        /// Lấy ra tổng số bản ghi với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Tổng số bản ghi</returns>
        int Count(Expression<Func<City, bool>> spec = null);
    }
}
