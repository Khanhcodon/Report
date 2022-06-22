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
    /// Interface : IRoleDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 010812
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng Role trong CSDL
    /// </summary>
    public interface IRoleDal
    {
        /// <summary>
        /// Lấy ra tất cả các nhóm người dùng phù hợp với điều kiện truyền vào. Nếu điều kiện băng null thì sẽ lấy ra tất cả các nhóm người dùng
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Danh sách nhóm người dùng</returns>
        IEnumerable<Role> Gets(Expression<Func<Role, bool>> spec = null);

        /// <summary>
        /// Lấy ra tất cả các nhóm người dùng phù hợp với điều kiện truyền vào. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..
        /// </summary>
        /// <typeparam name="TOutput">Kiểu đầu ra.</typeparam>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Danh sách các thực thể được ánh xạ</returns>
        IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<Role, TOutput>> projector,
                                           Expression<Func<Role, bool>> spec = null);

        /// <summary>
        /// Lấy ra tất cả các nhóm người dùng phù hợp với điều kiện truyền vào. Nếu điều kiện bằng null thì sẽ lấy ra tất cả các domain. Kết quả sẽ được ánh xạ sang 1 dạng khác bằng cách sử dụng 1 mapper do người dùng tự định nghĩa
        /// </summary>
        /// <remarks>
        /// Nên sử dụng hàm này để lấy ra những thuộc tính cần thiết để tránh việc lấy ra quá nhiều dữ liệu thừa
        /// </remarks>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="spec">Điều kiện</param>
        /// <typeparam name="TOutput">Kiểu dữ liệu được ánh xạ</typeparam>
        /// <returns>Kiểu dữ liệu được ánh xạ</returns>
        TOutput Get<TOutput>(Expression<Func<Role, TOutput>> projector,
                                           Expression<Func<Role, bool>> spec);

        /// <summary>
        /// Lấy ra nhóm người dùng theo id
        /// </summary>
        /// <param name="roleId">Id nhóm người dùng</param>
        /// <returns>Entity nhóm người dùng</returns>
        Role Get(int roleId);

        /// <summary>
        /// Lấy ra nhóm người dùng theo key
        /// </summary>
        /// <param name="roleKey">Key nhóm người dùng</param>
        /// <returns>Entity nhóm người dùng</returns>
        Role Get(string roleKey);

        /// <summary>
        /// Tạo mới nhóm người dùng
        /// </summary>
        /// <param name="role">Entity nhóm người dùng</param>
        void Create(Role role);

        /// <summary>
        /// Cập nhật thông tin nhóm người dùng
        /// </summary>
        /// <param name="role">Entity nhóm người dùng</param>
        void Update(Role role);

        /// <summary>
        /// Xóa nhóm người dùng
        /// </summary>
        /// <param name="role">Entity nhóm người dùng</param>
        void Delete(Role role);

        /// <summary>
        /// Kiểm tra sự tồn tại của nhóm người dùng phù hợp với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>true: nếu có tồn tại ít nhất 1 nhóm người dùng phù hợp, ngược lại: false</returns>
        bool Exist(Expression<Func<Role, bool>> spec = null);
    }
}
