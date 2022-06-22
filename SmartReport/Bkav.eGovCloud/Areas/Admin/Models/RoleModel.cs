using System;
using System.Collections.Generic;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(RoleValidator))]
    public class RoleModel
    {
        private string _roleKey;
        /// <summary>
        /// Lấy hoặc thiết lập Id nhóm người dùng
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Key nhóm người dùng
        /// </summary>
        [LocalizationDisplayName("Role.CreateOrEdit.Fields.RoleKey.Label")]
        public string RoleKey
        {
            get
            {
                return _roleKey;
            }
            set { _roleKey = value.StripChars(' ').StripVietnameseChars(); }
        }

        /// <summary>
        /// Lấy hoặc thiết lập Tên nhóm người dùng
        /// </summary>
        [LocalizationDisplayName("Role.CreateOrEdit.Fields.RoleName.Label")]
        public string RoleName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Mô tả
        /// </summary>
        [LocalizationDisplayName("Role.CreateOrEdit.Fields.Description.Label")]
        public string Description { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra nhóm người dùng này sẽ được tự động gán cho 1 người dùng khi tạo mới
        /// </summary>
        [LocalizationDisplayName("Role.CreateOrEdit.Fields.IsAutoAssignment.Label")]
        public bool IsAutoAssignment { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra nhóm người dùng này đã được kích hoạt
        /// </summary>
        [LocalizationDisplayName("Role.CreateOrEdit.Fields.IsActivated.Label")]
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
    }
}