using System.Collections.Generic;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : Permission - public - Entity
    /// Access Modifiers: 
    /// Create Date : 270612
    /// Author      : TrungVH
    /// Description : Entity tương ứng với bảng Permission trong CSDL
    /// </summary>
    public class Permission
    {
        private ICollection<UserRolePermission> _userRolePermissionss; 
    
        /// <summary>
        /// Lấy hoặc thiết lập Id quyền
        /// </summary>
        public int PermissionId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Mã quyền
        /// </summary>
        public string PermissionKey { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên quyền
        /// </summary>
        public string PermissionName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên module
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Mô tả quyền
        /// </summary>
        public string Description { get; set; }
    
        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<UserRolePermission> UserRolePermissions
        {
            get { return _userRolePermissionss ?? (_userRolePermissionss = new List<UserRolePermission>()); }
            set { _userRolePermissionss = value; }
        }
    }
}
