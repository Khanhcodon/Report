using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Web.Framework;
using System.Collections.Generic;

namespace Bkav.eGovCloud.Areas.Admin.Models.Settings
{
    //[FluentValidation.Attributes.Validator(typeof(EmailSettingsValidator))]
    public class SmsSettingsModel
    {
        /// <summary>
        /// Lấy hoặc thiết lập địa chỉ service
        /// </summary>
        [LocalizationDisplayName("Setting.Sms.Fields.SmtpServer.Label")]
        public string ServiceUrl { get; set; }

        /// <summary>
        /// Nhà cung cấp
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
        [LocalizationDisplayName("Setting.Sms.Fields.SentWhenReceiveDocument.Label")]
        public bool SentWhenReceiveDocument { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập các điều kiện gửi Sms
        /// </summary>
        public string Conditions { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mẫu Sms tương ứng khi gửi mail thông báo nhận document
        /// </summary>
        [LocalizationDisplayName("Setting.Sms.Fields.SentDocumentTemplateId.Label")]
        public int SentDocumentTemplateId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị xác định gửi sms khi người nhận sắp có cuộc họp
        /// </summary>
        [LocalizationDisplayName("Setting.Sms.Fields.SentWhenHasMeeting.Label")]
        public bool SentWhenHasMeeting { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập số phút nhắn tin trước khi đến cuộc họp
        /// </summary>
        [LocalizationDisplayName("Setting.Sms.Fields.BeforeMinute.Label")]
        public int BeforeMinute { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mẫu tương ứng khi gửi mail thông báo cuộc họp
        /// </summary>
        [LocalizationDisplayName("Setting.Sms.Fields.SentMeetingTemplateId.Label")]
        public int SentMeetingTemplateId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập là khách hàng hay dùng nội bộ công ty
        /// </summary>
        public bool IsCustomer { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập có kích hoạt gửi mail hay không
        /// </summary>
        [LocalizationDisplayName("Setting.Sms.Fields.IsActivated.Label")]
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