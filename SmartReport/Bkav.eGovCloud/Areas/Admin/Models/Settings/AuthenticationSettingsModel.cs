using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models.Settings
{
    [FluentValidation.Attributes.Validator(typeof(AuthenticationSettingsValidator))]
    public class AuthenticationSettingsModel
    {
        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra hệ thống domain mặc định dùng khi đăng nhập
        /// </summary>
        [LocalizationDisplayName("Setting.Authentication.Fields.DefaultDomain.Label")]
        public string DefaultDomain { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra hệ thống cho phép người dùng tự động đăng nhập
        /// </summary>
        [LocalizationDisplayName("Setting.Authentication.Fields.AllowUsersToAutomaticallyLogin.Label")]
        public bool AllowUsersToAutomaticallyLogin { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra hệ thống cho phép người dùng lấy lại mật khẩu
        /// </summary>
        [LocalizationDisplayName("Setting.Authentication.Fields.AllowUsersToRequestForgottenPasswords.Label")]
        public bool AllowUsersToRequestForgottenPasswords { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lạp Domain của service single sign on
        /// </summary>
        [LocalizationDisplayName("Setting.Authentication.Fields.SingleSignOnDomain.Label")]
        public string SingleSignOnDomain { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra hệ thống cho phép người dùng đăng nhập qua LDAP
        /// </summary>
        [LocalizationDisplayName("Setting.Authentication.Fields.EnableLdap.Label")]
        public bool EnableLdap { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Loại server LDAP (Microsoft Active Directory Server, OpenLDAP...)
        /// </summary>
        [LocalizationDisplayName("Setting.Authentication.Fields.LdapServer.Label")]
        public string LdapServer { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên máy chủ LDAP
        /// </summary>
        [LocalizationDisplayName("Setting.Authentication.Fields.LdapHost.Label")]
        public string LdapHost { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Cổng LDAP (Mặc định là 389)
        /// </summary>
        [LocalizationDisplayName("Setting.Authentication.Fields.LdapPort.Label")]
        public string LdapPort { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập DN cấp cao nhất của LDAP (dùng để tìm kiếm, thuộc tính này là tùy chọn)
        /// </summary>
        [LocalizationDisplayName("Setting.Authentication.Fields.LdapBaseDn.Label")]
        public string LdapBaseDn { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên đăng nhập vào LDAP
        /// </summary>
        [LocalizationDisplayName("Setting.Authentication.Fields.LdapUsername.Label")]
        public string LdapUsername { get; set; }

        /// <summary>
        /// Domain Ldap
        /// </summary>
        [LocalizationDisplayName("Setting.Authentication.Fields.LdapDomain.Label")]
        public string LdapDomain { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Mật khẩu đăng nhập vào LDAP
        /// </summary>
        [LocalizationDisplayName("Setting.Authentication.Fields.LdapPassword.Label")]
        public string LdapPassword { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập LDAP sử dụng SSL
        /// </summary>
        [LocalizationDisplayName("Setting.Authentication.Fields.LdapSSL.Label")]
        public bool LdapSSL { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Bộ lọc để tìm kiếm thông tin xác thực người dùng
        /// </summary>
        [LocalizationDisplayName("Setting.Authentication.Fields.LdapAuthenticationFilter.Label")]
        public string LdapAuthenticationFilter { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra hệ thống cho phép import người dùng từ LDAP
        /// </summary>
        [LocalizationDisplayName("Setting.Authentication.Fields.LdapEnableImport.Label")]
        public bool LdapEnableImport { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Bộ lọc để tìm kiếm những người cần import từ ldap
        /// </summary>
        [LocalizationDisplayName("Setting.Authentication.Fields.LdapImportUsersFromLdapFilter.Label")]
        public string LdapImportUsersFromLdapFilter { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Key của tên người dùng trong Ldap
        /// </summary>
        [LocalizationDisplayName("Setting.Authentication.Fields.LdapMappingFirstName.Label")]
        public string LdapMappingFirstName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Key của họ người dùng trong Ldap
        /// </summary>
        [LocalizationDisplayName("Setting.Authentication.Fields.LdapMappingLastName.Label")]
        public string LdapMappingLastName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Key của họ và tên người dùng trong Ldap
        /// </summary>
        [LocalizationDisplayName("Setting.Authentication.Fields.LdapMappingFullName.Label")]
        public string LdapMappingFullName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Key của tên đăng nhập người dùng trong Ldap
        /// </summary>
        [LocalizationDisplayName("Setting.Authentication.Fields.LdapMappingUsername.Label")]
        public string LdapMappingUsername { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Key của email người dùng trong Ldap
        /// </summary>
        [LocalizationDisplayName("Setting.Authentication.Fields.LdapMappingEmail.Label")]
        public string LdapMappingEmail { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra hệ thống cho phép đăng nhập qua OpenID
        /// </summary>
        [LocalizationDisplayName("Setting.Authentication.Fields.EnableOpenId.Label")]
        public bool EnableOpenId { get; set; }

        /// <summary>
        /// Thiết lập eGov chỉ chạy trên HTTPS (port 443) hay không?
        /// </summary>
        [LocalizationDisplayName("Setting.Authentication.Fields.HttpsOnly.Label")]
        public bool HttpsOnly { get; set; }

        /// <summary>
        /// Giới hạn đăng nhập theo IP
        /// </summary>
        public bool LimitByIP { get; set; }

        /// <summary>
        /// Giới hạn đăng nhập theo MAC
        /// </summary>
        public bool LimitByMAC { get; set; }

        /// <summary>
        /// Xác thực qua POP3
        /// </summary>
        [LocalizationDisplayName("Setting.Authentication.Fields.UseLoginPOP3.Label")]
        public bool UseLoginMail { get; set; } //LOM

        /// <summary>
        /// Url POP3 xác thực mail
        /// </summary>
        [LocalizationDisplayName("Setting.Authentication.Fields.LOMPOP3Url.Label")]
        public string LOMUrl { get; set; }

        /// <summary>
        /// Port POP3 xác thực mail
        /// </summary>
        [LocalizationDisplayName("Setting.Authentication.Fields.LOMPOP3Port.Label")]
        public int LOMPort { get; set; }

        /// <summary>
        /// Domain Mail
        /// </summary>
        [LocalizationDisplayName("Setting.Authentication.Fields.LOMDomain.Label")]
        public string LOMDomain { get; set; }

        /// <summary>
        /// Sử dụng SSL
        /// </summary>
        [LocalizationDisplayName("Setting.Authentication.Fields.UseLOMPOP3SSL.Label")]
        public bool LOMUseSSL { get; set; }

    }
}