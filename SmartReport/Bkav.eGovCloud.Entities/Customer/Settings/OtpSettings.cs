using Bkav.eGovCloud.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Entities.Customer.Settings
{
    /// <summary>
    /// Author : QuiBQ
    /// </summary>
    public class OtpSettings : ISettings
    {
        /// <summary>
        /// Thời gian kích hoạt
        /// </summary>
        public int TimeLimit { get; set; }

        /// <summary>
        /// Mã template cho Sms kích hoạt tài khoản
        /// </summary>
        public int ActiveSmsTemplateId { get; set; }

        /// <summary>
        /// Mã Template cho Email kích hoạt tài khoản
        /// </summary>
        public int ActiveMailTemplateId { get; set; }

        /// <summary>
        /// Mã template cho Sms reset mật khẩu
        /// </summary>
        public int ResetPassSmsTemplateId { get; set; }

        /// <summary>
        /// Mã Template cho Email reset mật khẩu
        /// </summary>
        public int ResetPassMailTemplateId { get; set; }
    }
}
