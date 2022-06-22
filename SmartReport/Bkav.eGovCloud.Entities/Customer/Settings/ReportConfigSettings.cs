using Bkav.eGovCloud.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Entities.Customer.Settings
{
    /// <summary>
    /// Cấu hình bao cao
    /// </summary>
    public class ReportConfigSettings : ISettings
    {
        /// <summary>
        /// Lấy hoặc thiet lap url
        /// </summary>
        public string UrlService { get; set; }
        /// <summary>
        /// Lấy hoặc thiet lap token
        /// </summary>
        public string TokenService { get; set; }
        /// <summary>
        /// Lấy hoặc thiet lap gui truc tiep
        /// </summary>
        public bool SendDirectly { get; set; }
        /// <summary>
        /// Lấy hoặc thiet lap OzganizeKey
        /// </summary>
        public string OzganizeKey { get; set; }
    }
}
