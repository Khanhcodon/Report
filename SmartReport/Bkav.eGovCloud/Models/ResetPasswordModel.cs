using Bkav.eGovCloud.Web.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bkav.eGovCloud.Models
{
    public class ResetPasswordModel
    {
        public ResetPasswordModel()
        {
            IsSendEmail = true;
        }
        /// <summary>
        /// Tài khoản cần đổi mật khẩu
        /// </summary>
        [LocalizationDisplayName("Tài khoản")]
        public string Account { get; set; }

        /// <summary>
        /// Email lưu tài khoản
        /// </summary>
        [LocalizationDisplayName("Email")]
        public string Email { get; set; }
        /// <summary>
        /// Mật khẩu hiện tại
        /// </summary>
        [LocalizationDisplayName("Gửi vào email cá nhân")]
        public bool IsSendEmail { get; set; }

        /// <summary>
        /// mật khẩu mới
        /// </summary>
        [LocalizationDisplayName("Gửi vào số điện thoại cá nhân")]
        public bool IsSendSms { get; set; }
    }
}