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
    /// Interface : IIncreaseDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 010812
    /// Author      : DungHV
    /// Description : DAL tương ứng với bảng Increase trong CSDL
    /// </summary>
    public interface IIncreaseDal
    {
        /// <summary>
        /// Lấy ra tất cả các nhảy sốg phù hợp với điều kiện truyền vào. Nếu điều kiện bằng null thì sẽ lấy ra tất cả các nhảy số
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện</param>
        /// <returns>Danh sách các nhảy số</returns>
        IEnumerable<Increase> Gets(Expression<Func<Increase, bool>> spec = null,
                                    Func<IQueryable<Increase>, IQueryable<Increase>> preFilter = null,
                                    params Func<IQueryable<Increase>, IQueryable<Increase>>[] postFilters);

        /// <summary>
        /// Lấy ra tất cả các nhảy sốg phù hợp với điều kiện truyền vào. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..
        /// </summary>
        /// <typeparam name="T">Kiểu đầu ra.</typeparam>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Danh sách các nhảy số</returns>
        IEnumerable<T> GetsAs<T>(Expression<Func<Increase, T>> projector, Expression<Func<Increase, bool>> spec = null);

        /// <summary>
        /// Lấy ra nhảy số theo id
        /// </summary>
        /// <param name="increaseId">Id của nhảy số</param>
        /// <returns>Entity nhảy số</returns>
        Increase Get(int increaseId);

        /// <summary>
        /// Tạo mới nhảy số
        /// </summary>
        /// <param name="increase">Entity nhảy số</param>
        void Create(Increase increase);
                       
        /// <summary>
        /// Xóa nhảy số
        /// </summary>
        /// <param name="increase">Entity nhảy số</param>
        void Delete(Increase increase);

        /// <summary>
        /// Kiểm tra sự tồn tại của nhảy số phù hợp với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>true: nếu có tồn tại ít nhất 1 nhảy số phù hợp, ngược lại: false</returns>
        bool Exist(Expression<Func<Increase, bool>> spec);

        /// <summary>
        /// Lấy ra tổng số bản ghi với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Tổng số bản ghi</returns>
        int Count(Expression<Func<Increase, bool>> spec = null);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="increase"></param>
        void Update(Increase increase);
    }
}
