using Bkav.eGovCloud.Web.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bkav.eGovCloud.Models
{
    public class OtpModel
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
        /// Thời hạn yêu cầu gửi request
        /// </summary>
        public float TimeLimit { get; set; }

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
        [LocalizationDisplayName("Mã kích hoạt")]
        public string ActivedCode { get; set; }

        /// <summary>
        /// Link kích hoạt
        /// </summary>
        public string ActivedUrl { get; set; }

        /// <summary>
        /// UserId tương ứng với bảng User
        /// </summary>
        public int UserId { get; set; }
    }
}