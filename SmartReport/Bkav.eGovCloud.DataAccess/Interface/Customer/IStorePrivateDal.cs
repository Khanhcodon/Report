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
    /// Interface : IStorePrivateDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 010812
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng StorePrivate trong CSDL
    /// </summary>
    public interface IStorePrivateDal
    {
        /// <summary>
        /// Lấy ra tất cả các hồ sơ cá nhân phù hợp với điều kiện truyền vào. Nếu điều kiện bằng null thì sẽ lấy ra tất cả các hồ sơ cá nhân
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện</param>
        /// <returns>Danh sách các hồ sơ cá nhân</returns>
        IEnumerable<StorePrivate> Gets(Expression<Func<StorePrivate, bool>> spec = null,
                                    Func<IQueryable<StorePrivate>, IQueryable<StorePrivate>> preFilter = null,
                                    params Func<IQueryable<StorePrivate>, IQueryable<StorePrivate>>[] postFilters);

        /// <summary>
        /// Lấy ra tất cả các hồ sơ cá nhân phù hợp với điều kiện truyền vào. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..
        /// </summary>
        /// <typeparam name="T">Kiểu đầu ra.</typeparam>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện. </param>
        /// <returns>Danh sách các thực thể được ánh xạ</returns>
        IEnumerable<T> GetsAs<T>(Expression<Func<StorePrivate, T>> projector,
                                    Expression<Func<StorePrivate, bool>> spec = null,
                                    Func<IQueryable<StorePrivate>, IQueryable<StorePrivate>> preFilter = null,
                                    params Func<IQueryable<StorePrivate>, IQueryable<StorePrivate>>[] postFilters);

        /// <summary>
        /// Lấy ra danh sách văn bản thuộc hồ sơ
        /// </summary>
        /// <param name="query">Câu truy vấn</param>
        /// <param name="parameters">Các tham số</param>
        /// <returns></returns>
        IEnumerable<IDictionary<string, object>> GetDocumentsByStorePrivateId(string query, params object[] parameters);

        /// <summary>
        /// Lấy ra hồ sơ cá nhân theo id
        /// </summary>
        /// <param name="id">Id của hồ sơ cá nhân</param>
        /// <returns>Entity hồ sơ cá nhân</returns>
        StorePrivate Get(int id);

        /// <summary>
        /// Lấy ra hồ sơ cá nhân theo id
        /// </summary>
        /// <param name="id">Id của hồ sơ cá nhân</param>
        /// <param name="userId">Id người tạo</param>
        /// <returns>Entity hồ sơ cá nhân</returns>
        StorePrivate Get(int id, int userId);

        /// <summary>
        /// Tạo mới hồ sơ cá nhân
        /// </summary>
        /// <param name="storeprivate">Entity người dùng</param>
        void Create(StorePrivate storeprivate);

        /// <summary>
        /// Cập nhật thông tin hồ sơ cá nhân
        /// </summary>
        /// <param name="storeprivate">Entity người dùng</param>
        void Update(StorePrivate storeprivate);

        /// <summary>
        /// Xóa hồ sơ cá nhân
        /// </summary>
        /// <param name="storeprivate">Entity người dùng</param>
        void Delete(StorePrivate storeprivate);

        /// <summary>
        /// Kiểm tra sự tồn tại của hồ sơ cá nhân phù hợp với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>true: nếu có tồn tại ít nhất 1 hồ sơ phù hợp, ngược lại: false</returns>
        bool Exist(Expression<Func<StorePrivate, bool>> spec = null);
    }
}
