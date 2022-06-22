using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : Sms - public - Entity
    /// Access Modifiers: 
    /// Create Date : 261015
    /// Author      : HopCV
    /// Description : Entity tương ứng với bảng Sms trong CSDL
    /// </summary>
    public class Sms
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
        public int? UserSendId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập người gửi tin nhắn
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập id hồ sơ
        /// </summary>
        public Guid? DocumentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập documentcopyId
        /// </summary>
        public int? DocumentCopyId { get; set; }

        /// <summary>
        /// Loại tin nhắn
        /// </summary>
        public string NotifyConfigType { get; set; }

        /// <summary>
        /// Guiwr looix
        /// </summary>
        public bool HasSendFail { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CountSendFail { get; set; }

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