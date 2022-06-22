using System.ComponentModel;

namespace Bkav.eGovCloud.Entities.Enum
{
    /// <summary>
    /// 
    /// </summary>
    public enum DocumentNotifyType
    {
        /// <summary>
        /// Không hiển thị notify
        /// </summary>
        [Description("egovcloud.enum.displaynotify.hide")]
        Hide = 0,

        /// <summary>
        /// Chỉ hiển thị notify văn bản chờ xử lý
        /// </summary>
        [Description("egovcloud.enum.displaynotify.shownotifyinprocess")]
        ShowNotifyInProcess = 1,

        /// <summary>
        /// Hiển thị tất cả notify văn bản có liên quan
        /// </summary>
        [Description("egovcloud.enum.displaynotify.all")]
        All = 2
    }
}
