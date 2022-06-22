namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : UserRole - public - Entity
    /// Access Modifiers: 
    /// Create Date : 270612
    /// Author      : TrungVH
    /// Description : Entity tương ứng với bảng UserRole trong CSDL
    /// </summary>
    public class UserRole
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id quan hệ giữa người dùng và nhóm người dùng
        /// </summary>
        public int UserRoleId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id người dùng
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id nhóm người dùng
        /// </summary>
        public int RoleId { get; set; }
    
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
