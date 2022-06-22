using System.ComponentModel;

namespace Bkav.eGovCloud.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public enum DisplayTreeType
    {
        /// <summary>
        /// 
        /// </summary>
        [Description("egovcloud.enum.displaytreetype.none")]
        None = 0,

        /// <summary>
        /// Văn bản chưa đọc
        /// </summary>
        [Description("egovcloud.enum.displaytreetype.unread")]
        Unread = 1,

        /// <summary>
        /// Chưa đọc / Tất cả
        /// </summary>
        [Description("egovcloud.enum.displaytreetype.unreadonall")]
        UnreadOnAll = 2 ,
        
        /// <summary>
        /// Tất cả
        /// </summary>
        [Description("egovcloud.enum.displaytreetype.all")]
        All = 3
    }
}
