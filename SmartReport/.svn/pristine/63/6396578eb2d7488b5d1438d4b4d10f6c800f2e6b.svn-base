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
    /// Interface : IKeyWordDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 221113
    /// Author      : DungHV
    /// Description : DAL tương ứng với bảng KeyWord trong CSDL
    /// </summary>
    public interface IKeyWordDal
    {
        /// <summary>
        /// Lấy ra tất cả từ khóa với điều kiện truyền vào. Nếu điều kiện bằng null thì sẽ lấy ra tất cả các từ khóa
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện</param>
        /// <returns>Danh sách từ khóa</returns>
        IEnumerable<KeyWord> Gets(Expression<Func<KeyWord, bool>> spec = null,
                                    Func<IQueryable<KeyWord>, IQueryable<KeyWord>> preFilter = null,
                                    params Func<IQueryable<KeyWord>, IQueryable<KeyWord>>[] postFilters);

        /// <summary>
        /// Lấy ra tất cả từ khóa phù hợp với điều kiện truyền vào. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..
        /// </summary>
        /// <typeparam name="TOutput">Kiểu đầu ra.</typeparam>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện. </param>
        /// <returns>Danh sách các thực thể được ánh xạ</returns>
        IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<KeyWord, TOutput>> projector, Expression<Func<KeyWord, bool>> spec = null, Func<IQueryable<KeyWord>, IQueryable<KeyWord>> preFilter = null, params Func<IQueryable<KeyWord>, IQueryable<KeyWord>>[] postFilters);

        /// <summary>
        /// Lấy ra từ khóa theo id
        /// </summary>
        /// <param name="keywordId">Id của từ khóa</param>
        /// <returns>Entity từ khóa</returns>
        KeyWord Get(int keywordId);

        /// <summary>
        /// Tạo mới từ khóa
        /// </summary>
        /// <param name="keyword">Entity từ khóa</param>
        void Create(KeyWord keyword);

        /// <summary>
        /// Cập nhật thông tin từ khóa
        /// </summary>
        /// <param name="keyword">Entity từ khóa</param>
        void Update(KeyWord keyword);

        /// <summary>
        /// Xóa từ khóa
        /// </summary>
        /// <param name="keyword">Entity từ khóa</param>
        void Delete(KeyWord keyword);

        /// <summary>
        /// Kiểm tra sự tồn tại của từ khóa phù hợp với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>true: nếu có tồn tại ít nhất 1 từ khóa phù hợp, ngược lại: false</returns>
        bool Exist(Expression<Func<KeyWord, bool>> spec);

        /// <summary>
        /// Lấy ra tổng số bản ghi với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Tổng số bản ghi</returns>
        int Count(Expression<Func<KeyWord, bool>> spec = null);
    }
}
