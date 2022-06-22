using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : Otp - public - Entity
    /// Access Modifiers: 
    /// Create Date : 05062017
    /// Author      : QuiBQ
    /// Description : Entity tương ứng với bảng Otp trong CSDL
    /// </summary>
    public class Otp
    {
        /// <summary>
        /// Id Otp auto increment
        /// </summary>
        public int OtpId { get; set; }

        /// <summary>
        /// Nội dung yêu cầu
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Địa chỉ Email người cần kích hoạt
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Số điện thoại gửi Tin nhắn
        /// </summary>
        public string Sms { get; set; }

        /// <summary>
        /// Trạng thái xác thực
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Mã kích hoạt
        /// </summary>
        public string ActivedCode { get; set; }

        /// <summary>
        /// Link kích hoạt
        /// </summary>
        public string ActivedUrl { get; set; }

        /// <summary>
        /// Thời gian bắt đầu gửi mail/ gửi tin nhắn
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Hạn kích hoạt
        /// </summary>
        public DateTime DateLimit { get; set; }

        /// <summary>
        /// UserId tương ứng với bảng user
        /// </summary>
        public int UserId { get; set; }
    }
}
