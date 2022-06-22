using Bkav.eGovCloud.Entities.Common;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : SmsSettings - public - Entity
    /// Access Modifiers:
    ///     * Implement: ISettings
    /// Create Date : 100812
    /// Author      : TienBV
    /// Description : Entity cho phần cấu hình email
    /// </summary>
    public class SmsSettings : ISettings
    {
        /// <summary>
        /// Lấy hoặc thiết lập địa chỉ service
        /// </summary>
        public string ServiceUrl { get; set; }

        /// <summary>
        /// Thiết lập nhà cung cấp dịch vụ
        /// </summary>
        public string SmsVendor { get; set; }

        /// <summary>
        /// Tài khoản truy cập service, do nhà mạng cấp
        /// </summary>
        public string ServiceUser { get; set; }

        /// <summary>
        /// Mật khẩu truy cập service, do nhà mạng cấp
        /// </summary>
        public string ServicePass { get; set; }

        /// <summary>
        /// Mã truy cập, do nhà mạng cấp
        /// </summary>
        public string ServiceCode { get; set; }

        /// <summary>
        /// Tên người gửi tin
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị xác định có gửi Sms khi hồ sơ nhận được thỏa mãn điều kiện dưới ko
        /// </summary>
        public bool SentWhenReceiveDocument { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập các điều kiện gửi Sms
        /// </summary>
        public string Conditions { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mẫu Sms tương ứng khi gửi mail thông báo nhận document
        /// </summary>
        public int SentDocumentTemplateId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị xác định gửi sms khi người nhận sắp có cuộc họp
        /// </summary>
        public bool SentWhenHasMeeting { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập số phút nhắn tin trước khi đến cuộc họp
        /// </summary>
        public int BeforeMinute { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mẫu tương ứng khi gửi mail thông báo cuộc họp
        /// </summary>
        public int SentMeetingTemplateId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập là khách hàng hay dùng nội bộ công ty
        /// </summary>
        public bool IsCustomer { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập có kích hoạt gửi sms hay không
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