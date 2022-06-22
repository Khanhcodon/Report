using System.ComponentModel;

namespace Bkav.eGovCloud.Entities.Enum
{
    /// <summary>
    /// Độ mật
    /// </summary>
    public enum SearchType
    {
        /// <summary>
        /// Tìm kiếm văn bản
        /// </summary>
        [Description("egovcloud.enum.searchtype.document")]
        Document = 1,

        /// <summary>
        /// Tìm kiếm trong file
        /// </summary>
        [Description("egovcloud.enum.searchtype.file")]
        File = 2,
    }
}