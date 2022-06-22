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
    /// Description : Entity cho phần cấu hình về phiên bản để thực hiện xóa cache khi mà thực hiện sửa thứ tự của tree
    /// </summary>
    public class VersionTreeSetting : ISettings
    {
        /// <summary>
        /// Lấy hoặc thiết lập trạng thái xác định có thay đổi trên server để yêu cầu client xóa cache và tải lại.
        /// </summary>
        /// <remarks>
        /// Thiết lập khi:
        /// - Build và update bản mới.
        /// - Khi có sự thay đổi về cache.
        /// 
        /// Dưới client định kỳ dùng ajax kiểm tra phiên bản, nếu khác thì xóa cache offline, cập nhật phiên bản mới.
        /// </remarks>
        public string CacheVersion { get; set; }
    }
}