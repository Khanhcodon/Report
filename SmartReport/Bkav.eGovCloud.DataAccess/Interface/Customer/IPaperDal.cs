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
    /// Interface : IPaperDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 010812
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng Paper trong CSDL
    /// </summary>
    public interface IPaperDal
    {
        /// <summary>
        /// Lấy ra tất cả các giấy tờ phù hợp với điều kiện truyền vào. Nếu điều kiện bằng null thì sẽ lấy ra tất cả các giấy tờ
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện</param>
        /// <returns>Danh sách các giấy tờ</returns>
        IEnumerable<Paper> Gets(Expression<Func<Paper, bool>> spec = null,
                                    Func<IQueryable<Paper>, IQueryable<Paper>> preFilter = null,
                                    params Func<IQueryable<Paper>, IQueryable<Paper>>[] postFilters);

        /// <summary>
        /// Lấy ra tất cả các giấy tờ phù hợp với điều kiện truyền vào. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..
        /// </summary>
        /// <typeparam name="TOutput">Kiểu đầu ra.</typeparam>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện. </param>
        /// <returns>Danh sách các thực thể được ánh xạ</returns>
        IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<Paper, TOutput>> projector, Expression<Func<Paper, bool>> spec = null, Func<IQueryable<Paper>, IQueryable<Paper>> preFilter = null, params Func<IQueryable<Paper>, IQueryable<Paper>>[] postFilters);

        /// <summary>
        /// Lấy ra giấy tờ theo id
        /// </summary>
        /// <param name="paperId">Id của giấy tờ</param>
        /// <returns>Entity giấy tờ</returns>
        Paper Get(int paperId);

        /// <summary>
        /// Tạo mới giấy tờ
        /// </summary>
        /// <param name="paper">Entity giấy tờ</param>
        void Create(Paper paper);

        /// <summary>
        /// Cập nhật thông tin giấy tờ
        /// </summary>
        /// <param name="paper">Entity giấy tờ</param>
        void Update(Paper paper);

        /// <summary>
        /// Xóa giấy tờ
        /// </summary>
        /// <param name="paper">Entity giấy tờ</param>
        void Delete(Paper paper);

        /// <summary>
        /// Kiểm tra sự tồn tại của giấy tờ phù hợp với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>true: nếu có tồn tại ít nhất 1 nhảy số phù hợp, ngược lại: false</returns>
        bool Exist(Expression<Func<Paper, bool>> spec);

        /// <summary>
        /// Lấy ra tổng số bản ghi với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Tổng số bản ghi</returns>
        int Count(Expression<Func<Paper, bool>> spec = null);
    }
}
