using System;
namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : PermissionCache - public - BLL
    /// Access Modifiers: 
    /// Create Date : 180912
    /// Author      : TrungVH
    /// Description : Class chứa các thuộc tính cần thiết của 1 mapping permission để lưu vào cache
    /// </summary>
    [Serializable]
    public class PermissionCache
    {
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
    }
}
