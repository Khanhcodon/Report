using Bkav.eGovCloud.Web.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bkav.eGovCloud.Areas.Admin.Models.Settings
{
    public class SSOAPISettingsModel
    {
        /// <summary>
        /// Lấy hoặc thiết tên hiển thị http address
        /// </summary>
        [LocalizationDisplayName("Setting.SSOAPISettings.Fields.HttpAddressSSO")]
        public string HttpAddressSSO { get; set; }

        /// <summary>
        /// Lấy hoặc thiết ClientId
        /// </summary>
        [LocalizationDisplayName("Setting.SSOAPISettings.Fields.ClientIdSSO")]
        public string ClientIdSSO { get; set; }

        /// <summary>
        /// Lấy hoặc thiết SecretKey
        /// </summary>
        [LocalizationDisplayName("Setting.SSOAPISettings.Fields.SecretKeySSO")]
        public string SecretKeySSO { get; set; }
    }
}