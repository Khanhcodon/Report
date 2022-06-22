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
    /// Interface : IUserDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 010812
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng User trong CSDL
    /// </summary>
    public interface IUserDal
    {
        /// <summary>
        /// Lấy ra tất cả các người dùng phù hợp với điều kiện truyền vào. Nếu điều kiện bằng null thì sẽ lấy ra tất cả các người dùng
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện</param>
        /// <returns>Danh sách các người dùng</returns>
        IEnumerable<User> Gets(Expression<Func<User, bool>> spec = null,
                                    Func<IQueryable<User>, IQueryable<User>> preFilter = null,
                                    params Func<IQueryable<User>, IQueryable<User>>[] postFilters);

        /// <summary>
        /// Lấy ra tất cả các người dùng phù hợp với điều kiện truyền vào. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..
        /// </summary>
        /// <typeparam name="TOutput">Kiểu đầu ra.</typeparam>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện. </param>
        /// <returns>Danh sách các thực thể được ánh xạ</returns>
        IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<User, TOutput>> projector,
                                           Expression<Func<User, bool>> spec = null,
                                           Func<IQueryable<User>, IQueryable<User>> preFilter = null,
                                           params Func<IQueryable<User>, IQueryable<User>>[] postFilters);

        /// <summary>
        /// Lấy ra người dùng theo id
        /// </summary>
        /// <param name="id">Id của người dùng</param>
        /// <param name="isActivated">Chỉ lấy tài khoản vẫn đang hoạt động (true) và ngược lại. Nếu bằng null thì sẽ bỏ qua điều kiện này</param>
        /// <returns>Entity người dùng</returns>
        User Get(int id, bool? isActivated = null);


        /// <summary>
        /// Lấy ra người dùng theo điều kiện truyền vào
        /// </summary>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Entity người dùng</returns>
        TOutput GetAs<TOutput>(Expression<Func<User, TOutput>> projector, Expression<Func<User, bool>> spec = null);
        /// <summary>
        /// Lấy ra người dùng theo tên đăng nhập
        /// </summary>
        /// <param name="username">Tên đăng nhập của người dùng</param>
        /// <param name="isActivated">Chỉ lấy tài khoản vẫn đang hoạt động (true) và ngược lại. Nếu bằng null thì sẽ bỏ qua điều kiện này</param>
        /// <returns>Entity người dùng</returns>
        User Get(string username, bool? isActivated = null);

        /// <summary>
        /// Lấy ra người dùng theo OpenID
        /// </summary>
        /// <param name="openId">OpenID</param>
        /// <param name="isActivated">Chỉ lấy tài khoản vẫn đang hoạt động (true) và ngược lại. Nếu bằng null thì sẽ bỏ qua điều kiện này</param>
        /// <returns>Entity người dùng</returns>
        User GetByOpenId(string openId, bool? isActivated = null);

        /// <summary>
        /// Tạo mới người dùng
        /// </summary>
        /// <param name="user">Entity người dùng</param>
        void Create(User user);

        /// <summary>
        /// Tạo mới nhiều người dùng 1 lúc
        /// </summary>
        /// <param name="users">Danh sách Entity người dùng</param>
        void Create(IEnumerable<User> users);

        /// <summary>
        /// Cập nhật thông tin người dùng
        /// </summary>
        /// <param name="user">Entity người dùng</param>
        void Update(User user);

        /// <summary>
        /// Xóa người dùng
        /// </summary>
        /// <param name="user">Entity người dùng</param>
        void Delete(User user);

        /// <summary>
        /// Kiểm tra sự tồn tại của người dùng phù hợp với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>true: nếu có tồn tại ít nhất 1 người dùng phù hợp, ngược lại: false</returns>
        bool Exist(Expression<Func<User, bool>> spec);

        /// <summary>
        /// Lấy ra tổng số bản ghi với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Tổng số bản ghi</returns>
        int Count(Expression<Func<User, bool>> spec = null);
    }
}
