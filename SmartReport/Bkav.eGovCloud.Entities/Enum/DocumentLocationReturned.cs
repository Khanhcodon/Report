using System.ComponentModel;

namespace Bkav.eGovCloud.Entities.Enum
{
    /// <summary>
    /// Loại văn bản
    /// </summary>
    public enum DocumentLocationReturned
    {

        /// <summary>
        /// Trả hồ sơ quan bưu điện
        /// </summary>
        [Description("egovcloud.enum.documentlocationreturn.buudien")]
        BuuDien = 0,

        /// <summary>
        /// Trả hồ sơ trực tiếp
        /// </summary>
        [Description("egovcloud.enum.documentlocationreturn.tructiep")]
        TrucTiep = 1,
    }
}