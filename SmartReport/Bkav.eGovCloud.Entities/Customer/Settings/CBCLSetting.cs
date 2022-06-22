using Bkav.eGovCloud.Entities.Common;
using System.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : AuthenticationSettings - public - Entity
    /// Access Modifiers:
    ///     * Implement: ISettings
    /// Create Date : 140716
    /// Author      : DungNVl
    /// Description : Entity cho phần cấu hình CBCL
    /// </summary>
    public class CBCLSetting : ISettings
    {
        /// <summary>
        /// Trạng thái kích hoạt CBCL
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Tên các Account quản lý
        /// </summary>
        public string AccountsName { get; set; }
        /// <summary>
        /// Tên cấu hình hướng chuyển công văn
        /// </summary>
        public string DoctypeConfig { get; set; }

        /// <summary>
        /// Cấu hình mẫu văn bản gửi đi
        /// </summary>
        public string HtmlTemplate { get; set; }
    }
}
