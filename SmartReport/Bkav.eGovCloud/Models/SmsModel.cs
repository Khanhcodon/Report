using System;
using Bkav.eGovCloud.Validator;

namespace Bkav.eGovCloud.Models
{
    [FluentValidation.Attributes.Validator(typeof(SmsValidator))]
    public class SmsModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int SmsId { get; set; }

        /// <summary>
        /// Số diện thoại nhận tin nhắn
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Nội dung tin nhắn
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái đã gửi sms hay chưa
        /// </summary>
        public bool IsSent { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập thời gian tao sms
        /// </summary>
        public DateTime? DateCreated { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập thời gian gửi sms
        /// </summary>
        public DateTime? DateSend { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập id người gửi tin nhắn
        /// </summary>
        public int UserSendId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập người gửi tin nhắn
        /// </summary>
        public string UserName { get; set; }
    }
}