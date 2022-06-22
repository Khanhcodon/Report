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
    /// Interface : ITransferTypeDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 221113
    /// Author      : DungHV
    /// Description : DAL tương ứng với bảng TransferType trong CSDL
    /// </summary>
    public interface ITransferTypeDal
    {
        /// <summary>
        /// Lấy ra tất cả hình thức chuyển với điều kiện truyền vào. Nếu điều kiện bằng null thì sẽ lấy ra tất cả các hình thức chuyển
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện</param>
        /// <returns>Danh sách từ khóa</returns>
        IEnumerable<TransferType> Gets(Expression<Func<TransferType, bool>> spec = null,
                                    Func<IQueryable<TransferType>, IQueryable<TransferType>> preFilter = null,
                                    params Func<IQueryable<TransferType>, IQueryable<TransferType>>[] postFilters);

        /// <summary>
        /// Lấy ra tất cả hình thức chuyển phù hợp với điều kiện truyền vào. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..
        /// </summary>
        /// <typeparam name="TOutput">Kiểu đầu ra.</typeparam>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện. </param>
        /// <returns>Danh sách các thực thể được ánh xạ</returns>
        IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<TransferType, TOutput>> projector, Expression<Func<TransferType, bool>> spec = null, Func<IQueryable<TransferType>, IQueryable<TransferType>> preFilter = null, params Func<IQueryable<TransferType>, IQueryable<TransferType>>[] postFilters);

        /// <summary>
        /// Lấy ra hình thức chuyển theo id
        /// </summary>
        /// <param name="keywordId">Id của hình thức chuyển</param>
        /// <returns>Entity hình thức chuyển</returns>
        TransferType Get(int keywordId);

        /// <summary>
        /// Tạo mới hình thức chuyển
        /// </summary>
        /// <param name="tranferType">Entity hình thức chuyển</param>
        void Create(TransferType tranferType);

        /// <summary>
        /// Cập nhật thông tin hình thức chuyển
        /// </summary>
        /// <param name="tranferType">Entity hình thức chuyển</param>
        void Update(TransferType tranferType);

        /// <summary>
        /// Xóa hình thức chuyển
        /// </summary>
        /// <param name="tranferType">Entity hình thức chuyển</param>
        void Delete(TransferType tranferType);

        /// <summary>
        /// Kiểm tra sự tồn tại của từ khóa phù hợp với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>true: nếu có tồn tại ít nhất 1 từ khóa phù hợp, ngược lại: false</returns>
        bool Exist(Expression<Func<TransferType, bool>> spec);

        /// <summary>
        /// Lấy ra tổng số bản ghi với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Tổng số bản ghi</returns>
        int Count(Expression<Func<TransferType, bool>> spec = null);
    }
}
