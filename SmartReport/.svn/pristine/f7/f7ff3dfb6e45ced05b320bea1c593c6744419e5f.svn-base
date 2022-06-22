using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Admin;

namespace Bkav.eGovCloud.DataAccess.Admin
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Interface : IAccountDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 010812
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng Account trong CSDL
    /// </summary>
    public interface IAccountDal
    {
        /// <summary>
        /// Lấy ra tất cả các người dùng phù hợp với điều kiện truyền vào, nếu điều kiện bằng null sẽ lấy ra tất cả người dùng
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Danh sách người dùng</returns>
        IEnumerable<Account> Gets(Expression<Func<Account, bool>> spec = null);

        /// <summary>
        /// Lấy ra người dùng theo id
        /// </summary>
        /// <param name="id">Id người dùng</param>
        /// <returns>Entity người dùng</returns>
        Account Get(int id);

        /// <summary>
        /// Lấy ra người dùng theo tên đăng nhập
        /// </summary>
        /// <param name="usernameEmailDomain">Tên đăng nhập dạng email của người dùng (username@domain)</param>
        /// <param name="isActive">Kiểm tra thêm điều kiện tài khoản đang hoạt động : true và ngược lại: false. Nếu là null sẽ bỏ qua điều kiện này</param>
        /// <returns>Entity người dùng</returns>
        Account Get(string usernameEmailDomain, bool? isActive = null);

        /// <summary>
        /// Tạo mới người dùng
        /// </summary>
        /// <param name="account">Entity người dùng</param>
        void Create(Account account);

        /// <summary>
        /// Cập nhật thông tin người dùng
        /// </summary>
        /// <param name="account">Entity người dùng</param>
        void Update(Account account);

        /// <summary>
        /// Xóa người dùng
        /// </summary>
        /// <param name="account">Entity người dùng</param>
        void Delete(Account account);

        /// <summary>
        /// Kiểm tra sự tồn tại của người dùng phù hợp với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>true: nếu có tồn tại ít nhất 1 người dùng phù hợp, ngược lại: false</returns>
        bool Exist(Expression<Func<Account, bool>> spec);
    }
}
