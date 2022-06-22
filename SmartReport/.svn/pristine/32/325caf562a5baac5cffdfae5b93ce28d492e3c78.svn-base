using System.Diagnostics;
using System.Security.Principal;

namespace Bkav.eGovCloud.Entities
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : CustomerPrincipalExtension - public - Entity
    /// Access Modifiers: 
    ///     * Implement : IIdentity
    /// Create Date : 050912
    /// Author      : TrungVH
    /// Description : Các hàm mở rộng cho customer principal
    /// </summary>
    public static class CustomerPrincipalExtension
    {
        /// <summary>
        /// Lấy ra tên đăng nhập của người dùng hiện tại
        /// </summary>
        /// <param name="user">Principal</param>
        /// <returns>Tên đăng nhập của người dùng hiện tại</returns>
        [DebuggerStepThrough]
        public static string GetUserName(this IPrincipal user)
        {
            var result = string.Empty;
            var principal = user as CustomerPrincipal;
            if (principal != null)
            {
                var identity = principal.Identity as CustomerIdentity;
                if (identity != null && identity.IsAuthenticated)
                {
                    result = identity.UserName;
                }
            }
            return result;
        }

        /// <summary>
        /// Lấy ra tên đăng nhập của người dùng hiện tại
        /// </summary>
        /// <param name="user">Principal</param>
        /// <returns>Tên đăng nhập của người dùng hiện tại</returns>
        [DebuggerStepThrough]
        public static string GetUserNameWithDomain(this IPrincipal user)
        {
            var result = string.Empty;
            var principal = user as CustomerPrincipal;
            if (principal != null)
            {
                var identity = principal.Identity as CustomerIdentity;
                if (identity != null && identity.IsAuthenticated)
                {
                    result = identity.UsernameWithDomain;
                }
            }
            return result;
        }

        /// <summary>
        /// Lấy ra tên đăng nhập của người dùng hiện tại
        /// </summary>
        /// <param name="user">Principal</param>
        /// <returns>Tên đăng nhập của người dùng hiện tại</returns>
        [DebuggerStepThrough]
        public static string GetFullName(this IPrincipal user)
        {
            var result = string.Empty;
            var principal = user as CustomerPrincipal;
            if (principal != null)
            {
                var identity = principal.Identity as CustomerIdentity;
                if (identity != null && identity.IsAuthenticated)
                {
                    result = identity.FullName;
                }
            }
            return result;
        }

        /// <summary>
        /// Lấy ra Id của người dùng hiện tại
        /// </summary>
        /// <param name="user">Principal</param>
        /// <returns>Id của người dùng hiện tại</returns>
        [DebuggerStepThrough]
        public static int GetUserId(this IPrincipal user)
        {
            var result = -1;
            var principal = user as CustomerPrincipal;
            if (principal != null)
            {
                var identity = principal.Identity as CustomerIdentity;
                if (identity != null && identity.IsAuthenticated)
                {
                    result = identity.UserId;
                }
            }
            return result;
        }

        /// <summary>
        /// Lấy ra Email của người dùng hiện tại
        /// </summary>
        /// <param name="user">Principal</param>
        /// <returns>Email của người dùng hiện tại</returns>
        [DebuggerStepThrough]
        public static string GetUserEmail(this IPrincipal user)
        {
            var result = string.Empty;
            var principal = user as CustomerPrincipal;
            if (principal != null)
            {
                var identity = principal.Identity as CustomerIdentity;
                if (identity != null && identity.IsAuthenticated)
                {
                    result = identity.Email;
                }
            }
            return result;
        }
    }
}
