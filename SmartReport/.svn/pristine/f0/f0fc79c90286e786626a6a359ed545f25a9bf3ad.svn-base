using System.Configuration;

namespace Bkav.eGovCloud.Entities.Common
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : AuthenticationSettings - public - Entity
    /// Access Modifiers:
    ///     * Implement: ISettings
    /// Create Date : 170812
    /// Author      : TrungVH
    /// Description : Entity cho phần cấu hình ldap
    /// </summary>
    public class AdminAuthenticationSettings : ISettings
    {
        private string _singleSignOnDomain;

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra hệ thống domain mặc định dùng khi đăng nhập
        /// </summary>
        public string DefaultDomain { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra hệ thống cho phép người dùng tự động đăng nhập
        /// </summary>
        public bool AllowUsersToAutomaticallyLogin { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra hệ thống cho phép người dùng lấy lại mật khẩu
        /// </summary>
        public bool AllowUsersToRequestForgottenPasswords { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lạp Domain của service single sign on (Nếu không thiết lập thì mặc định sẽ lấy trong web.config)
        /// </summary>
        public string SingleSignOnDomain
        {
            get
            {
                return string.IsNullOrWhiteSpace(_singleSignOnDomain)
                        ? ConfigurationManager.AppSettings.Get("single-sign-on.domain")
                        : _singleSignOnDomain;
            }
            set { _singleSignOnDomain = value; }
        }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra hệ thống cho phép người dùng đăng nhập qua LDAP
        /// </summary>
        public bool EnableLdap { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Loại server LDAP (Microsoft Active Directory Server, OpenLDAP...)
        /// </summary>
        public string LdapServer { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên máy chủ LDAP
        /// </summary>
        public string LdapHost { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Cổng LDAP (Mặc định là 389)
        /// </summary>
        public string LdapPort { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập DN cấp cao nhất của LDAP (dùng để tìm kiếm, thuộc tính này là tùy chọn)
        /// </summary>
        public string LdapBaseDn { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên đăng nhập vào LDAP
        /// </summary>
        public string LdapUsername { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Mật khẩu đăng nhập vào LDAP
        /// </summary>
        public string LdapPassword { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Bộ lọc để tìm kiếm thông tin xác thực người dùng
        /// </summary>
        public string LdapAuthenticationFilter { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra hệ thống cho phép import người dùng từ LDAP
        /// </summary>
        public bool LdapEnableImport { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Bộ lọc để tìm kiếm những người cần import từ ldap
        /// </summary>
        public string LdapImportUsersFromLdapFilter { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Key của tên người dùng trong Ldap
        /// </summary>
        public string LdapMappingFirstName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Key của họ người dùng trong Ldap
        /// </summary>
        public string LdapMappingLastName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Key của họ và tên người dùng trong Ldap
        /// </summary>
        public string LdapMappingFullName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Key của tên đăng nhập người dùng trong Ldap
        /// </summary>
        public string LdapMappingUsername { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Key của email người dùng trong Ldap
        /// </summary>
        public string LdapMappingEmail { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra hệ thống cho phép đăng nhập qua OpenID
        /// </summary>
        public bool EnableOpenId { get; set; }
    }
}