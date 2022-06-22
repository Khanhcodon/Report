using System;
using System.Security.Principal;

namespace Bkav.eGovCloud.Entities
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : AdminIdentity - public - Entity
    /// Access Modifiers: 
    ///     * Implement : IIdentity
    /// Create Date : 050912
    /// Author      : TrungVH
    /// Description : 1 custom identity sử dụng cho hệ thống quản trị, thay thế cho identity mặc định
    /// </summary>
    public class AdminIdentity : IIdentity
    {
        #region Ctor

        /// <summary>
        /// Khởi tạo class <see cref="AdminIdentity"/>.
        /// </summary>
        /// <param name="name">Tên người dùng (thường là tên đăng nhập).</param>
        /// <param name="id">Id người dùng.</param>
        /// <param name="role">Tên nhóm người dùng</param>
        public AdminIdentity(string name, int id, string role)
        {
            Name = name;
            Id = id;
            Role = role;
        }

        /// <summary>
        /// Khởi tạo class <see cref="AdminIdentity"/>.
        /// </summary>
        /// <param name="name">Tên người dùng (thường là tên đăng nhập).</param>
        /// <param name="data">Chuỗi dữ liệu cookie.</param>
        public AdminIdentity(string name, string data)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }
            if (String.IsNullOrEmpty(data))
            {
                throw new ArgumentNullException("data");
            }

            Name = name;
            //var parts = data.Split(new[] { '|' });
            //if (parts.Length > 2)
            //{
            //    throw new ArgumentException("data");
            //}

            //Id = Int32.Parse(parts[0]);
            //Role = parts[1];
        }
        #endregion

        #region Implementation of IIdentity
        /// <summary>
        /// Gets the name of the current user.
        /// </summary>
        /// <value></value>
        /// <returns>The name of the user on whose behalf the code is running.</returns>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the type of authentication used.
        /// </summary>
        /// <value></value>
        /// <returns>The type of authentication used to identify the user.</returns>
        public string AuthenticationType { get { return "Custom"; } }

        /// <summary>
        /// Gets a value that indicates whether the user has been authenticated.
        /// </summary>
        /// <value></value>
        /// <returns>true if the user was authenticated; otherwise, false.</returns>
        public bool IsAuthenticated { get { return true; } }
        #endregion

        #region Business information
        /// <summary>
        /// Lấy hoặc thiết lập id người dùng.
        /// </summary>
        public int Id { get; private set; }//0

        /// <summary>
        /// Lấy hoặc thiết lập nhóm người dùng.
        /// </summary>
        public string Role { get; private set; }//1

        #endregion

        /// <summary>
        /// Sinh ra chuỗi dữ liệu để lưu vào cookie
        /// </summary>
        /// <returns></returns>
        public string ToCookieString()
        {
            return String.Join("|",
                                Id,//0
                                Role//1
                                );
        }
    }
}
