using Bkav.eGovCloud.Entities;

namespace Bkav.eGovCloud.Api.Dto
{
    /// <summary>
    /// Thông báo
    /// </summary>
    public class NotificationDto
    {

        /// <summary>
        /// Loại thông báo
        /// </summary>
        public eGovNotificationTypes Type { get; set; }

        /// <summary>
        /// Số lựong
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Thông điệp thông báo
        /// </summary>
        public string Message { get; set; }

    }

}
