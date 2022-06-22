
using System.ComponentModel;

namespace Bkav.eGovCloud.Entities
{
    /// <summary>
    /// Loại form
    /// </summary>
    public enum LicenseStatus
    {
        /// <summary>
        /// Giấy phép dạng cấp mới
        /// </summary>
        [Description("egovcloud.enum.licensestatus.capmoi")]
        Capmoi = 1,

        /// <summary>
        /// Giấy phép dạng cấp đổi, bổ sung
        /// </summary>
        [Description("egovcloud.enum.licensestatus.capdoi")]
        Capdoi = 2,

        /// <summary>
        /// Giấy phép bị thu hồi
        /// </summary>
        [Description("egovcloud.enum.licensestatus.thuhoi")]
        Thuhoi = 3
    }
}
