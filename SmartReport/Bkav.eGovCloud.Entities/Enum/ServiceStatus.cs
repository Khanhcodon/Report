using System.ComponentModel;
namespace Bkav.eGovCloud.Entities.Enum
{
    /// <summary>
    /// Trạng thái hoạt động của window service
    /// </summary>
    public enum ServiceStatus
    {
        /// <summary>
        /// Đang chạy
        /// </summary>
        [Description("egovcloud.enum.servicestatus.running")]
        Running,

        /// <summary>
        /// Đang dừng
        /// </summary>
        [Description("egovcloud.enum.servicestatus.stoped")]
        Stoped,

        /// <summary>
        /// Đang tạm dừng
        /// </summary>
        [Description("egovcloud.enum.servicestatus.paused")]
        Paused,

        /// <summary>
        /// Không có quyền truy cập service
        /// </summary>
        [Description("egovcloud.enum.servicestatus.accessdenied")]
        AccessDenied,

        /// <summary>
        /// Service chưa được cài đặt trên hệ thống
        /// </summary>
        [Description("egovcloud.enum.servicestatus.notfound")]
        NotFound
    }
}
