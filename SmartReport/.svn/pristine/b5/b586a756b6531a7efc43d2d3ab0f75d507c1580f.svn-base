using System;
using System.Collections.Generic;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : Role - public - Entity
    /// Access Modifiers: 
    /// Create Date : 270612
    /// Author      : TrungVH
    /// Description : Entity tương ứng với bảng Position trong CSDL
    /// </summary>
    public class Role
    {
        private ICollection<UserRole> _userRoles;
        private ICollection<UserRolePermission> _userRolePermissions;

        /// <summary>
        /// Lấy hoặc thiết lập Id nhóm người dùng
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Key nhóm người dùng
        /// </summary>
        public string RoleKey { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên nhóm người dùng
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Mô tả
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra nhóm người dùng này sẽ được tự động gán cho 1 người dùng khi tạo mới
        /// </summary>
        public bool IsAutoAssignment { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra nhóm người dùng này đã được kích hoạt
        /// </summary>
        public bool IsActivated { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id người tạo
        /// </summary>
        public int? CreatedByUserId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ngày tạo
        /// </summary>
        public DateTime? CreatedOnDate { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id người cập nhật cuối cùng
        /// </summary>
        public int? LastModifiedByUserId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ngày cập nhật cuối cùng
        /// </summary>
        public DateTime? LastModifiedOnDate { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Danh sách Id của những người dùng trong nhóm
        /// </summary>
        public List<int> UserIds { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Danh sách Id quyền được bỏ qua
        /// </summary>
        public List<int> IgnorePermissionIds { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Danh sách Id quyền được cho phép
        /// </summary>
        public List<int> GrantPermissionIds { get; set; } 

        /// <summary>
        /// 
        /// </summary>
        public byte[] VersionByte { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime VersionDateTime { get; set; }
    
        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<UserRole> UserRoles
        {
            get { return _userRoles ?? (_userRoles = new List<UserRole>()); }
            set { _userRoles = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<UserRolePermission> UserRolePermissions
        {
            get { return _userRolePermissions ?? (_userRolePermissions = new List<UserRolePermission>()); }
            set { _userRolePermissions = value; }
        }
    }
}
