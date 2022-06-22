using System;
using System.Security.Principal;

namespace Bkav.eGovCloud.Entities
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : AdminPrincipal - public - Entity
    /// Access Modifiers: 
    ///     * Implement : IPrincipal
    /// Create Date : 050912
    /// Author      : TrungVH
    /// Description : 1 custom principal sử dụng cho hệ thống quản trị, thay thế cho principal mặc định
    /// </summary>
    public class AdminPrincipal : IPrincipal
    {
        private readonly AdminIdentity _identity;

        /// <summary>
        /// Khởi tạo class <see cref="AdminPrincipal"/>.
        /// </summary>
        /// <param name="identity">Identity.</param>
        public AdminPrincipal(AdminIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }
            _identity = identity;
        }

        /// <summary>
        /// Determines whether the current principal belongs to the specified role.
        /// </summary>
        /// <returns>
        /// true if the current principal is a member of the specified role; otherwise, false.
        /// </returns>
        /// <param name="role">The name of the role for which to check membership. </param>
        public bool IsInRole(string role)
        {
            return String.Equals(_identity.Role, role, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Gets the identity of the current principal.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Security.Principal.IIdentity"/> object associated with the current principal.
        /// </returns>
        public IIdentity Identity
        {
            get { return _identity; }
        }
    }
}
