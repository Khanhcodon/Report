using System;
using Bkav.eGovCloud.Core.Document;

namespace Bkav.eGovCloud.Models
{
    public class UserNotifyModel
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id nhật ký hành động
        /// </summary>
        public int UserActivityLogId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập người gửi
        /// </summary>
        public int UserSendId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập người nhận
        /// </summary>
        public int UserReceiveId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trích yếu văn bản
        /// </summary>
        public string Compendium { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập hướng văn bản
        /// </summary>
        public int DocumentCopyId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập loại hướng chuyển
        /// </summary>
        public int DocumentCopyType { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập loại hướng chuyển enum
        /// </summary>
        public DocumentCopyTypes DocumentCopyTypeInEnum
        {
            get
            {
                return (DocumentCopyTypes)DocumentCopyType;
            }
        }

        /// <summary>
        /// Lấy hoặc thiết lập ngày gửi
        /// </summary>
        public DateTime SentDate { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ngày gửi dạng string
        /// </summary>
        public string SentDateStr { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái đã xem hay chưa
        /// </summary>
        public bool IsViewed { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên người gửi
        /// </summary>
        public string UserSend { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập account người gửi
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập nội dung hiển thị ra client
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập avatar người gửi
        /// </summary>
        public string UserSendAvatar { get; set; }
    }
}