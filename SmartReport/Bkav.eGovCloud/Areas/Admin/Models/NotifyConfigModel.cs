using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Entities.Enum;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    public class NotifyConfigModel
    {
        public NotifyConfigModel()
        {
            HasAutoSendMail = true;
            HasAutoSendSms = true;
        }

        /// <summary>
        /// Id cấu hình
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập key (trong enum NotifyConfigType)
        /// </summary>
        [LocalizationDisplayName("NotifyConfig.CreateOrEdit.Fields.Key.Label")]
        public string Key { get; set; }

        /// <summary>
        /// Lấy giá tri enum
        /// </summary>
        public NotifyConfigType KeyInEnum
        {
            get
            {
                return (NotifyConfigType)Enum.Parse(typeof(NotifyConfigType), Key, true); ;
            }
        }

        /// <summary>
        /// Lấy hoặc thiết lập mô tả
        /// </summary>
        [LocalizationDisplayName("NotifyConfig.CreateOrEdit.Fields.Description.Label")]
        public string Description { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái có gửi mail tự động hay không
        /// </summary>
        [LocalizationDisplayName("NotifyConfig.CreateOrEdit.Fields.HasAutoSendMail.Label")]
        public bool HasAutoSendMail { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập  id mẫu gửi mail
        /// </summary>
        public int MailTemplateId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên mẫu gửi mail
        /// </summary>
        public string MailTemplateName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái có gửi sms tự động hay không
        /// </summary>
        [LocalizationDisplayName("NotifyConfig.CreateOrEdit.Fields.HasAutoSendSms.Label")]
        public bool HasAutoSendSms { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập id mẫu gửi tin nhăn
        /// </summary>
        public int SmsTemplateId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên mẫu gửi sms
        /// </summary>
        public string SmsTemplateName { get; set; }
    }
}