using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : User - public - Entity
    /// Access Modifiers: 
    /// Create Date : 270612
    /// Author      : TrungVH
    /// Description : Entity tương ứng với bảng User trong CSDL
    /// </summary>
    public class User
    {
        private ICollection<UserDepartmentJobTitlesPosition> _userDepartmentJobTitless;
        private ICollection<UserRole> _userRoles;
        private ICollection<UserRolePermission> _userRolePermissions;
        private ICollection<UserPasswordHistory> _userPasswordHistories;
        // private ICollection<DocumentCopy> _documentCopys;

        /// <summary>
        /// 
        /// </summary>
        public User()
        {
        }

        /// <summary>
        /// Lấy hoặc thiết lập Id người dùng
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên đăng nhập
        /// </summary>
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
        /// Lấy hoặc thiết lập Muối của mật khẩu
        /// </summary>
        public byte[] PasswordSalt { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Mật khẩu đã được băm với muối
        /// </summary>
        public byte[] PasswordHash { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ngày thay đổi mật khẩu cuối cùng
        /// </summary>
        public DateTime? PasswordLastModifiedOnDate { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập OpenId
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Họ và tên
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Họ và tên đệm
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Giới tính: Nam (true), Nữ (false)
        /// </summary>
        public bool Gender { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Số điện thoại
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập địa chỉ email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Fax
        /// </summary>
        public string Fax { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Địa chỉ
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra người dùng này đã được kích hoạt
        /// </summary>
        public bool IsActivated { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra người dùng này đang bị khóa tạm thời do đăng nhập nhiều lần không thành công
        /// </summary>
        public bool IsLockedOut { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ngày giờ khóa tạm thời người dùng
        /// </summary>
        public DateTime? LastLockoutDate { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ngày giờ đăng nhập cuối cùng
        /// </summary>
        public DateTime? LastLoginDate { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Số lần đăng nhập thất bại
        /// </summary>
        public int? FailedPasswordAttemptCount { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ngày giờ bắt đầu đăng nhập thất bại
        /// </summary>
        public DateTime? FailedPasswordAttemptStart { get; set; }

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
        /// Lấy hoặc thiết lập Danh sách id nhóm người dùng
        /// </summary>
        public List<int> RoleIds { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Danh sách id phòng ban và chức vụ (được phân cách nhau = dấu ,)
        /// </summary>
        public List<string> DepartmentJobTitlesId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte[] VersionByte { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime VersionDateTime { get; set; }

        /// <summary>
        /// Có quyền đọc tất cả công văn
        /// </summary>
        public bool? CanReadEveryDocument { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập quyền xem report của cả cơ quan
        /// </summary>
        public bool? HasViewReport { get; set; }

        /// <summary>
        /// lấy hoặc thiết lập các câu hình của user dang json
        /// </summary>
        public string UserSetting { get; set; }

        /// <summary>
        /// Lưu thông tin các thiết bị được notify
        /// </summary>
        public string NotifyInfo { get; set; }

        /// <summary>
        /// Đối tượng parse từ notify info
        /// </summary>
        public NotifyInfo NotifyInfoModel
        {
            get
            {
                var result = new NotifyInfo();

                if (string.IsNullOrEmpty(NotifyInfo))
                {
                    return result;
                }

                try
                {
                    result = Json2.ParseAs<NotifyInfo>(NotifyInfo);
                }
                catch { }

                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool? HasLimitByMac { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public bool CanFinishDocument { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<UserDepartmentJobTitlesPosition> UserDepartmentJobTitless
        {
            get { return _userDepartmentJobTitless ?? (_userDepartmentJobTitless = new List<UserDepartmentJobTitlesPosition>()); }
            set { _userDepartmentJobTitless = value; }
        }

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

        /// <summary>
        /// 
        /// </summary>
        public ICollection<UserPasswordHistory> UserPasswordHistorys
        {
            get { return _userPasswordHistories ?? (_userPasswordHistories = new List<UserPasswordHistory>()); }
            set { _userPasswordHistories = value; }
        }

        ///// <summary>
        ///// 
        ///// </summary>
        //public ICollection<DocumentCopy> DocumentCopys
        //{
        //    get { return _documentCopys ?? (_documentCopys = new List<DocumentCopy>()); }
        //    set { _documentCopys = value; }
        //}
    }

    /// <summary>
    /// 
    /// </summary>
    public class NotifyInfo
    {
        /// <summary>
        /// Khởi tạo
        /// </summary>
        public NotifyInfo()
        {
            RemoveRead = false;
            HasShowDesktop = true;
            HasPlaySound = true;
            HasShowDocumentNotify = true;
            DocumentNotifyType = (int)Enum.DocumentNotifyType.ShowNotifyInProcess;
            HasShowMailNotify = true;
            HasShowChatNotify = true;
        }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái cho phép tự động xóa thông báo đã đọc.
        /// </summary>
        [JsonProperty("removeread")]
        public bool RemoveRead { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái cho phép hiển thị thông báo trên desktop
        /// </summary>
        [JsonProperty("hasshowdesktop")]
        public bool HasShowDesktop { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái cho phép bật âm thanh thông báo
        /// </summary>
        [JsonProperty("hasplaysound")]
        public bool HasPlaySound { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái cho phép hiển thị thông báo văn bản
        /// </summary>
        [JsonProperty("hasshowdocumentnotify")]
        public bool HasShowDocumentNotify { get; set; }

        /// <summary>
        /// Thiết lập hiển thị notify cho người  dùng
        /// 1 - Chỉ hiển thị notify văn bản chờ xử lý.
        /// 2 - Notify tất cả văn bản liên quan
        /// </summary>
        [JsonProperty("documentnotifytype")]
        public byte DocumentNotifyType { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái cho phép hiển thị thông báo mail
        /// </summary>
        [JsonProperty("hasshowmailnotify")]
        public bool HasShowMailNotify { get; set; }

        /// <summary>
        /// Danh sách mail được thiết lập xem thông báo
        /// </summary>
        [JsonProperty("mailfoldernotify")]
        public string MailFolderNotify { get; set; }

        /// <summary>
        /// lấy hoặc thiết lập hồ sơ mặc đinh của người dùng
        /// </summary>
        [JsonProperty("maillastesttoken")]
        public string MailLastestToken { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái cho phép hiển thị thông báo chat
        /// </summary>
        [JsonProperty("hasshowchatnotify")]
        public bool HasShowChatNotify { get; set; }

        /// <summary>
        /// lấy hoặc thiết lập hồ sơ mặc đinh của người dùng
        /// </summary>
        [JsonProperty("chatlastesttoken")]
        public string ChatLastestToken { get; set; }

        /// <summary>
        /// Danh sách các thiết bị kết nối 
        /// </summary>
        [JsonIgnore]
        public IEnumerable<MobileDevice> MobileDevices { get; set; }
    }
}
