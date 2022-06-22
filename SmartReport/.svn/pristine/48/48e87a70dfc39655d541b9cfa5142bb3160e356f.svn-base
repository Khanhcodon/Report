using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Common;

namespace Bkav.eGovCloud.DataAccess.Common
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Interface : ILogDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 270812
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng Log trong CSDL
    /// </summary>
    public interface ILogDal
    {
        /// <summary>
        /// Lấy ra tất cả các nhật ký phù hợp với điều kiện truyền vào. Nếu điều kiện bằng null thì sẽ lấy ra tất cả các nhật ký
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện</param>
        /// <returns>Danh sách các nhật ký</returns>
        IEnumerable<Log> Gets(Expression<Func<Log, bool>> spec = null,
                                    Func<IQueryable<Log>, IQueryable<Log>> preFilter = null,
                                    params Func<IQueryable<Log>, IQueryable<Log>>[] postFilters);

        /// <summary>
        /// Lấy ra tất cả các nhật ký phù hợp với điều kiện truyền vào. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..
        /// </summary>
        /// <typeparam name="TOutput">Kiểu đầu ra.</typeparam>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện. </param>
        /// <returns>Danh sách các thực thể được ánh xạ</returns>
        IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<Log, TOutput>> projector,
                                           Expression<Func<Log, bool>> spec = null,
                                           Func<IQueryable<Log>, IQueryable<Log>> preFilter = null,
                                           params Func<IQueryable<Log>, IQueryable<Log>>[] postFilters);

        /// <summary>
        /// Lấy ra nhật ký theo id
        /// </summary>
        /// <param name="id">Id của nhật ký</param>
        /// <returns>Entity nhật ký</returns>
        Log Get(int id);

        /// <summary>
        /// Tạo mới nhật ký
        /// </summary>
        /// <param name="log">Entity nhật ký</param>
        void Create(Log log);

        /// <summary>
        /// Xóa nhật ký
        /// </summary>
        /// <param name="log">Entity nhật ký</param>
        void Delete(Log log);

        /// <summary>
        /// Xóa nhiều nhật ký
        /// </summary>
        /// <param name="logs">Danh sách nhật ký cần xóa</param>
        void Delete(IEnumerable<Log> logs);
        /// <summary>
        /// Xóa toàn bộ nhật ký
        /// </summary>
        void DeleteAll();
        /// <summary>
        /// Lấy ra tổng số bản ghi với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Tổng số bản ghi</returns>
        int Count(Expression<Func<Log, bool>> spec = null);
    }
}
