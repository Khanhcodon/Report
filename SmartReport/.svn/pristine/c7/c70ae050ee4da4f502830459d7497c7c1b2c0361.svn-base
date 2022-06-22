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
    /// Interface : IAnticipateDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 180214
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng Anticipate trong CSDL
    /// </summary>
    public interface IAnticipateDal
    {
        /// <summary>
        /// Thêm dự kiến
        /// </summary>
        /// <param name="anticipate">Dự kiến</param>
        void Create(Anticipate anticipate);

        /// <summary>
        /// Xóa dự kiến
        /// </summary>
        /// <param name="anticipate">Dự kiến</param>
        void Delete(Anticipate anticipate);

        /// <summary>
        /// Thay đổi thông tin dự kiến
        /// </summary>
        /// <param name="anticipate">Dự kiến</param>
        void Update(Anticipate anticipate);

        /// <summary>
        /// Trả về dự kiến theo Id
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns>Dự kiến</returns>
        Anticipate Get(int id);

        /// <summary>
        /// Trả về dự kiến theo điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Dự kiến</returns>
        Anticipate Get(Expression<Func<Anticipate, bool>> spec = null);

        /// <summary> 
        /// <para>Trả về danh sách tất cả các dự kiến phù hợp với điều kiện truyền vào. </para>
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện</param>
        /// <returns>Danh sách các dự kiến</returns>
        IEnumerable<Anticipate> Gets(Expression<Func<Anticipate, bool>> spec = null,
                                    Func<IQueryable<Anticipate>, IQueryable<Anticipate>> preFilter = null,
                                    params Func<IQueryable<Anticipate>, IQueryable<Anticipate>>[] postFilters);

        /// <summary>
        /// Lấy ra tất cả các dự kiến phù hợp với điều kiện truyền vào. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..
        /// </summary>
        /// <typeparam name="TOutput">Kiểu đầu ra.</typeparam>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện. </param>
        /// <returns>Danh sách các thực thể được ánh xạ</returns>
        IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<Anticipate, TOutput>> projector,
                                           Expression<Func<Anticipate, bool>> spec = null,
                                           Func<IQueryable<Anticipate>, IQueryable<Anticipate>> preFilter = null,
                                           params Func<IQueryable<Anticipate>, IQueryable<Anticipate>>[] postFilters);
    }
}
