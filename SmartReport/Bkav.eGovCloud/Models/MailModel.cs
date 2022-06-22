using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Validator;

namespace Bkav.eGovCloud.Models
{
    /// <summary>
    /// 
    /// </summary>
    [FluentValidation.Attributes.Validator(typeof(MailValidator))]
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
            Smtp = null;
        }

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
        /// Địa chỉ email gửi CC (null: mặc định là ko gửi CC)
        /// </summary>
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
        /// Tệp đính kèm
        /// </summary>
        public List<System.Net.Mail.Attachment> Attachments
        {
            get
            {
                if (string.IsNullOrEmpty(AttachmentIdStr))
                    return null;

                try
                {
                    if (AttachmentIds != null && AttachmentIds.Any())
                    {
                        var result = new List<System.Net.Mail.Attachment>();
                        var attachments = _attachmentService.GetAttachments(AttachmentIds);
                        if (attachments != null && attachments.Any())
                        {
                            foreach (var item in attachments)
                            {
                                result.Add(new System.Net.Mail.Attachment(item.Value, item.Key));
                            }
                        }
                        return result;
                    }
                }
                catch { }

                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public System.Net.Mail.SmtpClient Smtp { get; set; }

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