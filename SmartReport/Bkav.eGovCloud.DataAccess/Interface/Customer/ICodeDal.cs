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
    /// Interface : ICodeDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 010812
    /// Author      : DungHV
    /// Description : DAL tương ứng với bảng Code trong CSDL
    /// </summary>
    public interface ICodeDal
    {
        /// <summary>
        /// Lấy ra tất cả các mã phù hợp với điều kiện truyền vào. Nếu điều kiện bằng null thì sẽ lấy ra tất cả các mã
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện</param>
        /// <returns>Danh sách các bảng mã</returns>
        IEnumerable<Code> Gets(Expression<Func<Code, bool>> spec = null,
                                    Func<IQueryable<Code>, IQueryable<Code>> preFilter = null,
                                    params Func<IQueryable<Code>, IQueryable<Code>>[] postFilters);

        /// <summary>
        /// Lấy ra tất cả các max phù hợp với điều kiện truyền vào. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..
        /// </summary>
        /// <typeparam name="TOutput">Kiểu đầu ra.</typeparam>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Danh sách các thực thể được ánh xạ</returns>
        IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<Code, TOutput>> projector,
                                           Expression<Func<Code, bool>> spec = null);

        /// <summary>
        /// Lấy ra mã theo id
        /// </summary>
        /// <param name="id">Id mã</param>
        /// <returns>Entity bảng mã</returns>
        Code Get(int id);
        
        /// <summary>
        /// Tạo mới mã
        /// </summary>
        /// <param name="code">Entity bảng mã</param>
        void Create(Code code);

        /// <summary>
        /// Cập nhật thông tin mã
        /// </summary>
        /// <param name="code">Entity bảng mã</param>
        void Update(Code code);

        /// <summary>
        /// Xóa mã
        /// </summary>
        /// <param name="code">Entity bảng mã</param>
        void Delete(Code code);

        /// <summary>
        /// Kiểm tra sự tồn tại của bảng mã với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>true: nếu có tồn tại ít nhất 1 mã phù hợp, ngược lại: false</returns>
        bool Exist(Expression<Func<Code, bool>> spec);

        /// <summary>
        /// Lấy ra tổng số bản ghi với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Tổng số bản ghi</returns>
        int Count(Expression<Func<Code, bool>> spec = null);
    }
}
