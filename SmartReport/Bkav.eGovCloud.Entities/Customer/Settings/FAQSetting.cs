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
    /// Class : FAQSettings - public - Entity
    /// Access Modifiers:
    ///     * Implement: ISettings
    /// Create Date : 310317
    /// Author      : DungNVl
    /// Description : Entity cho phần cấu hình CBCL
    /// </summary>
    public class FAQSetting : ISettings
    {
        /// <summary>
        /// Trạng thái kích hoạt
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Lây hoặc thiết tên hiển thị
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Lây hoặc thiết lập api lấy dữ liệu
        /// </summary>
        public string ApiUrl { get; set; }

        /// <summary>
        /// Địa chỉ trang dịch vụ công trực tuyến
        /// </summary>
        public string OnlineLink { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập nhóm
        /// </summary>
        public int TreeGroupId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập nhóm
        /// </summary>
        public int? PermissionSettingId { get; set; }
    }
}
