using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models.Settings
{
    [FluentValidation.Attributes.Validator(typeof(EmailSettingsValidator))]
    public class EmailSettingsModel
    {
        /// <summary>
        /// Lấy hoặc thiết lập Tên máy chủ SMTP
        /// </summary>
        [LocalizationDisplayName("Setting.Email.Fields.SmtpServer.Label")]
        public string SmtpServer { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Cổng SMTP
        /// </summary>
        [LocalizationDisplayName("Setting.Email.Fields.SmtpPort.Label")]
        public int SmtpPort { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra email sẽ được gửi đi qua SSL
        /// </summary>
        [LocalizationDisplayName("Setting.Email.Fields.EnableSsl.Label")]
        public bool EnableSsl { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên đăng nhập SMTP
        /// </summary>
        [LocalizationDisplayName("Setting.Email.Fields.SmtpUsername.Label")]
        public string SmtpUsername { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Mật khẩu SMTP
        /// </summary>
        [LocalizationDisplayName("Setting.Email.Fields.SmtpPassword.Label")]
        public string SmtpPassword { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên hiển thị khi gửi mail
        /// </summary>
        [LocalizationDisplayName("Setting.Email.Fields.DisplayName.Label")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Header cho email
        /// </summary>
        [LocalizationDisplayName("Setting.Email.Fields.EmailHeader.Label")]
        public string EmailHeader { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Chữ ký cho email
        /// </summary>
        [LocalizationDisplayName("Setting.Email.Fields.EmailSignature.Label")]
        public string EmailSignature { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập có kích hoạt gửi mail hay không
        /// </summary>
        [LocalizationDisplayName("Setting.Email.Fields.IsActivated.Label")]
        public bool IsActivated { get; set; }

        /// <summary>
        /// lấy hoặc thiết lập link api
        /// </summary>
        [LocalizationDisplayName("Setting.Email.Fields.LinkApi.Label")]
        public string LinkApi { get; set; }

        /// <summary>
        /// lấy hoặc thiết lập token
        /// </summary>
        [LocalizationDisplayName("Setting.Email.Fields.TokenApi.Label")]
        public string TokenApi { get; set; }


        /// <summary>
        /// lấy hoặc thiết lập token
        /// </summary>
        [LocalizationDisplayName("Setting.Email.Fields.TitleName.Label")]
        public string TitleName { get; set; }
    }
}