using System.Collections.Generic;
using Bkav.eGovCloud.Entities.Admin;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models.Settings
{
    /// <summary>
    /// Settings liên kết với các trang khác
    /// </summary>
    public class ConnectionSettingsModel
    {
        /// <summary>
        /// Cookie chứa thông tin người dùng
        /// </summary>
        [LocalizationDisplayName("Setting.ConnectionSetting.Fields.UserInfoCookie.Label")]
        public string UserInfoCookie { get; set; }

        /// <summary>
        /// Domain cha sử dụng chung
        /// </summary>
        [LocalizationDisplayName("Setting.ConnectionSetting.Fields.BkavSSOParentDomain.Label")]
        public string BkavSSOParentDomain { get; set; }

        /// <summary>
        /// Tên cookie SSO dùng chung
        /// </summary>
        [LocalizationDisplayName("Setting.ConnectionSetting.Fields.BkavSSOCookieName.Label")]
        public string BkavSSOCookieName { get; set; }

        /// <summary>
        /// Username đang đăng nhập dùng chung
        /// </summary>
        [LocalizationDisplayName("Setting.ConnectionSetting.Fields.BkavSSOCookieUsername.Label")]
        public string BkavSSOCookieUsername { get; set; }

        /// <summary>
        /// Version của secret key
        /// </summary>
        [LocalizationDisplayName("Setting.ConnectionSetting.Fields.BkavSSOKeyVersion.Label")]
        public int BkavSSOKeyVersion { get; set; }

        /// <summary>
        /// Secretkey dành cho việc giải mã cookie
        /// </summary>
        [LocalizationDisplayName("Setting.ConnectionSetting.Fields.BkavSSOSecretKey.Label")]
        public string BkavSSOSecretKey { get; set; }

        /// <summary>
        /// Hạn sử dụng của coookie
        /// </summary>
        [LocalizationDisplayName("Setting.ConnectionSetting.Fields.BkavSSOExpire.Label")]
        public int BkavSSOExpire { get; set; }

        /// <summary>
        /// Link iframe bmail
        /// </summary>
        [LocalizationDisplayName("Setting.ConnectionSetting.Fields.BmailLink.Label")]
        public string BmailLink { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [LocalizationDisplayName("Setting.ConnectionSetting.Fields.Apps.Label")]
        public string Apps { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<Apps> AppsParse { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AppOrder { get; set; }

        /// <summary>
        /// Kiểu mail
        /// </summary>
        [LocalizationDisplayName("Setting.ConnectionSetting.Fields.MailType.Label")]
        public int MailType { get; set; }
    }
}