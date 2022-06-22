using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Entities.Enum
{
    /// <summary>
    /// Type
    /// </summary>
    public enum TemplateKeysType
    {
        /// <summary>
        /// Tất cả
        /// </summary>
        [Description("egovcloud.enum.templateKeysType.all")]
        All = 0,
        /// <summary>
        /// khác
        /// </summary>
        [Description("egovcloud.enum.templateKeysType.other")]
        Other = 2,
        /// <summary>
        /// Theo truy vấn trực tiếp
        /// </summary>
        [Description("egovcloud.enum.templateKeysType.query")]
        Query = 4,
        /// <summary>
        ///  Theo mẫu hiển thị
        /// </summary>
        [Description("egovcloud.enum.templateKeysType.display")]
        Display = 8

    }
}
