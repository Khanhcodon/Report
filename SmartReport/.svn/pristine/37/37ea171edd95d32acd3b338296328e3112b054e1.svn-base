using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class MailModel
    {
        private readonly AttachmentBll _attachmentService;

        /// <summary>
        /// 
        /// </summary>
        public MailModel()
        {
            _attachmentService = DependencyResolver.Current.GetService<AttachmentBll>();
            IsBodyHtml = true;
        }

        /// <summary>
        /// 
        /// </summary>
        public int MailId { get; set; }

        /// <summary>
        /// Tiêu đê mail
        /// </summary>
        [LocalizationDisplayName("Customer.Mail.CreateOrEdit.Fields.Subject.Label")]
        public string Subject { get; set; }

        /// <summary>
        /// Nộ dung mail
        /// </summary>
        [LocalizationDisplayName("Customer.Mail.CreateOrEdit.Fields.Body.Label")]
        public string Body { get; set; }

        /// <summary>
        /// Địa chỉ email nhận (sử dụng dấu , để gửi tới nhiều email)
        /// </summary>
        [LocalizationDisplayName("Customer.Mail.CreateOrEdit.Fields.SendTo.Label")]
        public string SendTo { get; set; }

        /// <summary>
        /// Chữ ký
        /// </summary>
        [LocalizationDisplayName("Customer.Mail.CreateOrEdit.Fields.Signature.Label")]
        public string Signature { get; set; }

        /// <summary>
        /// email header
        /// </summary>
        [LocalizationDisplayName("Customer.Mail.CreateOrEdit.Fields.Header.Label")]
        public string Header { get; set; }

        /// <summary>
        /// Địa chỉ email gửi
        /// </summary>
        [LocalizationDisplayName("Customer.Mail.CreateOrEdit.Fields.Sender.Label")]
        public string Sender { get; set; }

        /// <summary>
        /// Tên hiển thị cảu người gửi
        /// </summary>
        [LocalizationDisplayName("Customer.Mail.CreateOrEdit.Fields.SenderDisplayName.Label")]
        public string SenderDisplayName { get; set; }

        /// <summary>
        /// Nội dung mail là html
        /// </summary>
        [LocalizationDisplayName("Customer.Mail.CreateOrEdit.Fields.IsBodyHtml.Label")]
        public bool IsBodyHtml { get; set; }

        /// <summary>
        /// Địa chỉ email gửi CC (null: mặc định là ko gửi CC)
        /// </summary>
        public string CarbonCopysStr { get; set; }

        /// <summary>
        /// Địa chỉ email gửi CC (null: mặc định là ko gửi CC)
        /// </summary>
        [LocalizationDisplayName("Customer.Mail.CreateOrEdit.Fields.CarbonCopys.Label")]
        public List<string> CarbonCopys
        {
            get
            {
                var result = new List<string>();
                if (!string.IsNullOrEmpty(CarbonCopysStr))
                {
                    try
                    {
                        return Json2.ParseAs<List<string>>(CarbonCopysStr);
                    }
                    catch { }
                }
                return result;
            }
        }

        /// <summary>
        /// Tệp đính kèm id
        /// </summary>
        public string AttachmentIdStr { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [LocalizationDisplayName("Customer.Mail.CreateOrEdit.Fields.Attachment.Label")]
        public List<int> AttachmentIds
        {
            get
            {
                var result = new List<int>();
                if (!string.IsNullOrEmpty(AttachmentIdStr))
                {
                    try
                    {
                        result = Json2.ParseAs<List<int>>(AttachmentIdStr);
                    }
                    catch { }
                }
                return result;
            }
            set
            {
                AttachmentIdStr = value.StringifyJs();
            }
        }
        
        /// <summary>
        /// Lấy hoặc thiết lập trạng thái đã gửi sms hay chưa
        /// </summary>
        [LocalizationDisplayName("Customer.Mail.CreateOrEdit.Fields.IsSent.Label")]
        public bool IsSent { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập thời gian tao sms
        /// </summary>
        [LocalizationDisplayName("Customer.Mail.CreateOrEdit.Fields.DateCreated.Label")]
        public DateTime? DateCreated { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập thời gian gửi sms
        /// </summary>
        [LocalizationDisplayName("Customer.Mail.CreateOrEdit.Fields.DateSend.Label")]
        public DateTime? DateSend { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập id người gửi tin nhắn
        /// </summary>
        public int? UserSendId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập người gửi tin nhắn
        /// </summary>
        [LocalizationDisplayName("Customer.Mail.CreateOrEdit.Fields.UserName.Label")]
        public string UserName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập cc mail tới
        /// </summary>
        public string CC { get; set; }
    }
}