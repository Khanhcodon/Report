using System;
using System.Collections.Generic;

namespace Bkav.eGovCloud.Entities.Admin
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : Account - public - Entity
    /// Access Modifiers: 
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Entity tương ứng với bảng Account trong CSDL
    /// </summary>
    public class Account
    {
        private ICollection<AccountPasswordHistory> _accountPasswordHistories;
        private ICollection<AccountDomain> _accountDomains;

        /// <summary>
        /// Lấy hoặc thiết lập Id người dùng
        /// </summary>
        public int AccountId { get; set; }

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
        /// Lấy hoặc thiết lập Giới tính: Nam (true), Nữ (false)
        /// </summary>
        public bool Gender { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Số điện thoại
        /// </summary>
        public string Phone { get; set; }

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
        /// 
        /// </summary>
        public byte[] VersionByte { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime VersionDateTime { get; set; }

        /// <summary>
        /// Cho phép cán bộ xem giám sát của hệ thống tập trung
        /// </summary>
        public bool? HasViewReport { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập danh sách các lần thay đổi mật khẩu
        /// </summary>
        public virtual ICollection<AccountPasswordHistory> AccountPasswordHistorys
        {
            get { return _accountPasswordHistories ?? (_accountPasswordHistories = new List<AccountPasswordHistory>()); }
            set { _accountPasswordHistories = value; }
        }

        /// <summary>
        /// Lấy hoặc thiết lập danh sách các mapping giữa người dùng và domain
        /// </summary>
        public virtual ICollection<AccountDomain> AccountDomains
        {
            get { return _accountDomains ?? (_accountDomains = new List<AccountDomain>()); }
            set { _accountDomains = value; }
        }
    }
}
