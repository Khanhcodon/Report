using System;
using System.Collections.Generic;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(UserValidator))]
    public class UserModel
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id người dùng
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Danh sách id nhóm người dùng
        /// </summary>
        public List<int> RoleIds { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên đăng nhập
        /// </summary>
        [LocalizationDisplayName("User.CreateOrEdit.Fields.Username.Label")]
        public string Username { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên đăng nhập dạng email (@domainname)
        /// </summary>
        public string UsernameEmailDomain { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên domain
        /// </summary>
        public string DomainName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Mật khẩu
        /// </summary>
        [LocalizationDisplayName("User.CreateOrEdit.Fields.Password.Label")]
        public string Password { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Xác nhận mật khẩu
        /// </summary>
        [LocalizationDisplayName("User.CreateOrEdit.Fields.ConfirmPassword.Label")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập OpenId
        /// </summary>
        [LocalizationDisplayName("User.CreateOrEdit.Fields.OpenId.Label")]
        public string OpenId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Họ và tên
        /// </summary>
        [LocalizationDisplayName("User.CreateOrEdit.Fields.FullName.Label")]
        public string FullName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên
        /// </summary>
        [LocalizationDisplayName("User.CreateOrEdit.Fields.FirstName.Label")]
        public string FirstName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Họ và tên đệm
        /// </summary>
        [LocalizationDisplayName("User.CreateOrEdit.Fields.LastName.Label")]
        public string LastName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Giới tính: Nam (true), Nữ (false)
        /// </summary>
        [LocalizationDisplayName("User.CreateOrEdit.Fields.Gender.Label")]
        public bool Gender { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Số điện thoại
        /// </summary>
        [LocalizationDisplayName("User.CreateOrEdit.Fields.Phone.Label")]
        public string Phone { get; set; }
         
        /// <summary>
        /// Lấy hoặc thiết lập địa chỉ email
        /// </summary>
        [LocalizationDisplayName("User.CreateOrEdit.Fields.Email.Label")]
        public string Email { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Fax
        /// </summary>
        [LocalizationDisplayName("User.CreateOrEdit.Fields.Fax.Label")]
        public string Fax { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Địa chỉ
        /// </summary>
        [LocalizationDisplayName("User.CreateOrEdit.Fields.Address.Label")]
        public string Address { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra người dùng này đã được kích hoạt
        /// </summary>
        [LocalizationDisplayName("User.CreateOrEdit.Fields.IsActivated.Label")]
        public bool IsActivated { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ngày giờ khóa tạm thời người dùng
        /// </summary>
        public DateTime? LastLockoutDate { get; set; }

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

        //  public string Avatar { get; set; }
        /// <summary>
        /// Lấy hoặc thiết lập Danh sách Id quyền được bỏ qua
        /// </summary>
        public List<int> IgnorePermissionIds { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Danh sách Id quyền được cho phép
        /// </summary>
        public List<int> GrantPermissionIds { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Danh sách Id quyền không được cho phép
        /// </summary>
        public List<int> DenyPermissionIds { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Danh sách id phòng ban và chức vụ (được phân cách nhau = dấu ,)
        /// </summary>
        public List<string> DepartmentJobTitlesId { get; set; }

        /// <summary>
        /// Có quyền đọc tất cả công văn
        /// </summary>
        [LocalizationDisplayName("User.CreateOrEdit.Fields.CanReadEveryDocument.Label")]
        public bool CanReadEveryDocument { get; set; }

        /// <summary>
        /// Cho phép cán bộ xem giám sát của hệ thống tập trung
        /// </summary>
        public bool HasViewReport { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool HasLimitByMac { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        public bool CanFinishDocument { get; set; }
    }
}