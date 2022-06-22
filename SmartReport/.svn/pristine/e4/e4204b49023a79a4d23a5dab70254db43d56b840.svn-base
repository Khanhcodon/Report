using System;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : Mail - public - Entity
    /// Access Modifiers: 
    /// Create Date : 261015
    /// Author      : HopCV
    /// Description : Entity tương ứng với bảng Mail trong CSDL
    /// </summary>
    public class Mail
    {
        /// <summary>
        /// 
        /// </summary>
        public int MailId { get; set; }

        /// <summary>
        /// Tiêu đê mail
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Nộ dung mail
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Địa chỉ email nhận (sử dụng dấu , để gửi tới nhiều email)
        /// </summary>
        public string SendTo { get; set; }

        /// <summary>
        /// Chữ ký
        /// </summary>
        public string Signature { get; set; }

        /// <summary>
        /// email header
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// Địa chỉ email gửi
        /// </summary>
        public string Sender { get; set; }

        /// <summary>
        /// Tên hiển thị cảu người gửi
        /// </summary>
        public string SenderDisplayName { get; set; }

        /// <summary>
        /// Nội dung mail là html
        /// </summary>
        public bool IsBodyHtml { get; set; }

        /// <summary>
        /// Địa chỉ email gửi CC (null: mặc định là ko gửi CC)
        /// </summary>
        public string CarbonCopysStr { get; set; }

        /// <summary>
        /// Tệp đính kèm id
        /// </summary>
        public string AttachmentIdStr { get; set; }

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
        public int? UserSendId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập người gửi tin nhắn
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Trạng thái gửi lỗi
        /// </summary>
        public bool HasSentFail { get; set; }

        /// <summary>
        /// Số lần gửi lỗi.
        /// </summary>
        public int CountSentFail { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid? LinkApi { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TokenApi { get; set; }
    }
}
