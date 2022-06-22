using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Static Class : UserQuery - public - BLL
    /// Access Modifiers: 
    /// Create Date : 200812
    /// Author      : TrungVH
    /// Description : Các điều kiện truy vấn cho bảng user
    /// </summary>
    public static class UserQuery
    {
        /// <summary>
        /// Điều kiện Id của người dùng bằng với Id truyền vào
        /// </summary>
        /// <param name="userId">Id của người dùng.</param>
        /// <returns>Điều kiện truy vấn</returns>
        public static Expression<Func<User, bool>> WithId(int userId)
        {
            return s => s.UserId == userId;
        }

        /// <summary>
        /// Điều kiện Tên đăng nhập của người dùng bằng với tên đăng nhập truyền vào
        /// </summary>
        /// <param name="usernameEmailDomain">Tên đăng nhập dạng email (username@domain)</param>
        /// <returns>Điều kiện truy vấn</returns>
        public static Expression<Func<User, bool>> WithUsernameEmailDomain(string usernameEmailDomain)
        {
            return s => s.UsernameEmailDomain.Equals(usernameEmailDomain, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Điều kiện Tên đăng nhập của người dùng gần giống với tên đăng nhập truyền vào
        /// </summary>
        /// <param name="username">Tên đăng nhập. Nếu null sẽ bỏ qua điều kiện này</param>
        /// <returns>Điều kiện truy vấn</returns>
        public static Expression<Func<User, bool>> ContainsUsername(string username)
        {
            return u => username == null || username == string.Empty || u.Username.Contains(username);
        }

        /// <summary>
        /// Điều kiện Họ và tên của người dùng gần giống với họ và tên truyền vào
        /// </summary>
        /// <param name="fullname">Họ và tên. Nếu null sẽ bỏ qua điều kiện này</param>
        /// <returns>Điều kiện truy vấn</returns>
        public static Expression<Func<User, bool>> ContainsFullName(string fullname)
        {
            return u => fullname == null || fullname == string.Empty || u.FullName.Contains(fullname);
        }

        /// <summary>
        /// Điều kiện Trạng thái của người dùng bằng với trạng thái truyền vào
        /// </summary>
        /// <param name="isActivated">Kích hoạt: true và ngược lại. Nếu null sẽ bỏ qua điều kiện này</param>
        /// <returns>Điều kiện truy vấn</returns>
        public static Expression<Func<User, bool>> WithIsActivated(bool? isActivated)
        {
            return u => !isActivated.HasValue || u.IsActivated == isActivated;
        }

        /// <summary>
        /// Điều kiện OpenID của người dùng bằng với OpenID truyền vào
        /// </summary>
        /// <param name="openId">OpenID.</param>
        /// <returns>Điều kiện truy vấn</returns>
        public static Expression<Func<User, bool>> WithOpenId(string openId)
        {
            return s => s.OpenId == openId;
        }

        /// <summary>
        /// userid in (list userid)
        /// </summary>
        /// <param name="userDXLs"></param>
        /// <returns></returns>
        public static Expression<Func<User, bool>> WithIds(List<int> userDXLs)
        {
            return u => userDXLs.Contains(u.UserId);
        }
    }
}
