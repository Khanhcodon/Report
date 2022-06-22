using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Entities
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Enum : LdapServerType - public - Entity
    /// Access Modifiers: 
    /// Create Date : 170812
    /// Author      : TrungVH
    /// Description : Loại server LDAP
    /// </summary>
    public static class SystemRole
    {
        /// <summary>
        /// Nhóm quản trị cao nhất của hệ thống
        /// </summary>
        public static readonly Role Administrator = new Role
        {
            RoleKey = "Administrator",
            RoleName = "Quản trị hệ thống",
            Description = "Nhóm cấp cao nhất, có thể quản trị được mọi chức năng trong hệ thống",
            IsActivated = true,
            IsAutoAssignment = false
        };

        /// <summary>
        /// Nhóm quản trị Bkav
        /// </summary>
        public static readonly Role AdminBkav = new Role
        {
            RoleKey = "AdminBkav",
            RoleName = "Quản trị Bkav",
            Description = "Quản trị của Bkav",
            IsActivated = true,
            IsAutoAssignment = false
        };

        /// <summary>
        /// Tất cả các key role mặc định của hệ thống
        /// </summary>
        public static Role[] EgovSystemRoles = new[] { Administrator, AdminBkav };
    }
}
