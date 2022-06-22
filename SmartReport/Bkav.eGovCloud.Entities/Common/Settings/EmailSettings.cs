namespace Bkav.eGovCloud.Entities.Common
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : EmailSettings - public - Entity
    /// Access Modifiers:
    ///     * Implement: ISettings
    /// Create Date : 100812
    /// Author      : TrungVH
    /// Description : Entity cho phần cấu hình email
    /// </summary>
    public class EmailSettings : ISettings
    {
        /// <summary>
        /// Lấy hoặc thiết lập Tên máy chủ SMTP
        /// </summary>
        public string SmtpServer { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Cổng SMTP
        /// </summary>
        public int SmtpPort { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra email sẽ được gửi đi qua SSL
        /// </summary>
        public bool EnableSsl { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên đăng nhập SMTP
        /// </summary>
        public string SmtpUsername { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Mật khẩu SMTP
        /// </summary>
        public string SmtpPassword { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên hiển thị khi gửi mail
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Header cho email
        /// </summary>
        public string EmailHeader { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Chữ ký cho email
        /// </summary>
        public string EmailSignature { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập có kích hoạt gửi mail hay không
        /// </summary>
        public bool IsActivated { get; set; }

        /// <summary>
        /// lấy hoặc thiết lập link api
        /// </summary>
        public string LinkApi { get; set; }

        /// <summary>
        /// lấy hoặc thiết lập token
        /// </summary>
        public string TokenApi { get; set; }

        /// <summary>
        /// lấy hoặc thiết lập TitleName
        /// </summary>
        public string TitleName { get; set; }
    }
}