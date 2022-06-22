using System;
using System.Security.Principal;
using System.Web.Security;
using Bkav.eGovCloud.Core.Utils;

namespace Bkav.eGovCloud.Entities
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : CustomIdentity - public - Entity
    /// Access Modifiers:
    ///     * Implement : IIdentity
    /// Create Date : 200612
    /// Author      : TrungVH
    /// Description : 1 custom identity, thay thế cho identity mặc định
    /// </summary>
    public class CustomerIdentity : IIdentity
    {
        private readonly FormsAuthenticationTicket _ticket;

        /// <summary>
        ///
        /// </summary>
        public FormsAuthenticationTicket Ticket
        {
            get { return _ticket; }
        }

        #region Ctor

        /// <summary>
        /// Khởi tạo class <see cref="CustomerIdentity"/>.
        /// </summary>
        /// <param name="ticket"></param>
        public CustomerIdentity(FormsAuthenticationTicket ticket)
        {
            _ticket = ticket;
            if (String.IsNullOrEmpty(ticket.Name) || string.IsNullOrEmpty(ticket.UserData))
            {
                throw new ArgumentNullException("ticket");
            }

            var cookieData = Json2.ParseAs<CustomerCookieData>(ticket.UserData);
            CookieData = cookieData;
        }

        #endregion Ctor

        #region Implementation of IIdentity

        /// <summary>
        /// <para>Gets the name of the current user.</para>
        /// <para>Chính là usernameWithDomain.</para>
        /// </summary>
        /// <value></value>
        /// <returns>The name of the user on whose behalf the code is running.</returns>
        public string Name
        {
            get { return _ticket.Name; }
        }

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

        #endregion Implementation of IIdentity

        #region Business information

        /// <summary>
        /// Lấy hoặc thiết lập các thông tin về người dùng trong cookie
        /// </summary>
        public CustomerCookieData CookieData { get; set; }

        /// <summary>
        /// Lấy ra tên đăng nhập của người dùng hiện tại
        /// </summary>
        /// <returns>Tên đăng nhập của người dùng hiện tại</returns>
        public string UserName
        {
            get
            {
                var startIndex = UsernameWithDomain.IndexOf('@');
                return startIndex > 0 ? UsernameWithDomain.Substring(0, startIndex) : UsernameWithDomain;
            }
        }

        /// <summary>
        /// Lấy ra tên đăng nhập của người dùng hiện tại
        /// </summary>
        /// <returns>Tên đăng nhập của người dùng hiện tại</returns>
        public string UsernameWithDomain
        {
            get
            {
                return CookieData.UsernameWithDomain;
            }
        }

        /// <summary>
        /// Lấy ra tên đăng nhập của người dùng hiện tại
        /// </summary>
        /// <returns>Tên đăng nhập của người dùng hiện tại</returns>
        public string FullName
        {
            get
            {
                return CookieData.FullName;
            }
        }

        /// <summary>
        /// Lấy ra Id của người dùng hiện tại
        /// </summary>
        /// <returns>Id của người dùng hiện tại</returns>
        public int UserId
        {
            get
            {
                return CookieData.UserId;
            }
        }

        /// <summary>
        /// Lấy ra Email của người dùng hiện tại
        /// </summary>
        /// <returns>Email của người dùng hiện tại</returns>
        public string Email
        {
            get
            {
                return CookieData.Email;
            }
        }

        /// <summary>
        /// Lấy ra Email của người dùng hiện tại
        /// </summary>
        /// <returns>Email của người dùng hiện tại</returns>
        public string Role
        {
            get
            {
                return CookieData.Role;
            }
        }

        #endregion Business information

        /// <summary>
        /// Sinh ra chuỗi dữ liệu để lưu vào cookie
        /// </summary>
        /// <returns></returns>
        public string ToCookieString()
        {
            return CookieData.Stringify();
        }
    }
}