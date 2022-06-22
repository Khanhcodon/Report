namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : UserRolePermission - public - Entity
    /// Access Modifiers: 
    /// Create Date : 270612
    /// Author      : TrungVH
    /// Description : Entity tương ứng với bảng UserRolePermission trong CSDL
    /// </summary>
    public class UserRolePermission
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id quan hệ giữa người dùng, nhóm người dùng và quyền
        /// </summary>
        public int UserRolePermissionId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id quyền
        /// </summary>
        public int PermissionId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Mã quyền
        /// </summary>
        public string PermissionKey { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id nhóm người dùng
        /// </summary>
        public int? RoleId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Key nhóm người dùng
        /// </summary>
        public string RoleKey { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id người dùng
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên đăng nhập dạng email của người dùng
        /// </summary>
        public string UsernameEmailDomain { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra người dùng hoặc nhóm quyền có quyền này
        /// </summary>
        public bool AllowAccess { get; set; }
    
        /// <summary>
        /// Lấy hoặc thiết lập Quyền
        /// </summary>
        public virtual Permission Permission { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Nhóm người dùng
        /// </summary>
        public virtual Role Role { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Người dùng
        /// </summary>
        public virtual User User { get; set; }
    }
}
