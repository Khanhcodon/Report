using System.ComponentModel;

namespace Bkav.eGovCloud.Entities.Enum
{
    /// <summary>
    /// 
    /// </summary>
    public enum MailNotifyType
    {
        /// <summary>
        /// Không hiển thị
        /// </summary>
        [Description("egovcloud.enum.displaynotify.hide")]
        Hide = 0,

        /// <summary>
        /// Hiển thị notify cho hộp thư đến
        /// </summary>
        [Description("egovcloud.enum.displaynotify.inbox")]
        Inbox = 1,

        /// <summary>
        /// Hiển thị notify theo các mục đã xem
        /// </summary>
        [Description("egovcloud.enum.displaynotify.option")]
        Option = 2,

        /// <summary>
        /// Hiển thị toàn bộ thông báo
        /// </summary>
        [Description("egovcloud.enum.displaynotify.all")]
        All = 3
    }
}
