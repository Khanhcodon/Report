using Bkav.eGovCloud.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Entities.Customer.Settings
{
    /// <summary>
    /// Cấu hình thông tin kết nối đến phần giao nhiệm vụ
    /// </summary>
    public class MissionSettings : ISettings
    {
        /// <summary>
        /// Lấy hoặc thiết tên hiển thị
        /// </summary>
        public string ApiDomain { get; set; }

        /// <summary>
        /// Lấy hoặc thiết PartnerGUID
        /// </summary>
        public string PartnerGUID { get; set; }

        /// <summary>
        /// Lấy hoặc thiết PartnerToken
        /// </summary>
        public string PartnerToken { get; set; }


    }
}
