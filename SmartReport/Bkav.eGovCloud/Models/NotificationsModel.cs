using System;
using Bkav.eGovCloud.Entities;

namespace Bkav.eGovCloud.Models
{
    /// <summary>
    /// TrinhNVd - 18102016: Lớp lưu các thông báo của người dùng
    /// </summary>
    public class NotificationModel
    {
        /// <summary>
        /// NotificationId
        /// </summary>
        public int NotificationId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public NotificationType NotificationTypeEnum { get; set; }

        /// <summary>
        /// Loại notify
        /// </summary>
        public int NotificationType { get; set; }

        /// <summary>
        /// Id user nhận thông báo
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// MailId (nếu có)
        /// </summary>
        public string MailId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FolderId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FolderLocation { get; set; }

        /// <summary>
        /// ChatId (nếu có)
        /// </summary>
        public int ChatId { get; set; }

        /// <summary>
        /// ChatterJID - dùng cho Chat
        /// </summary>
        public string ChatterJid { get; set; }

        /// <summary>
        /// DocumentCopyId (nếu có)
        /// </summary>
        public int DocumentCopyId { get; set; }

        /// <summary>
        /// Tiêu đề
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Nội dung tóm tắt
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Avatar người gửi
        /// </summary>
        public string SenderAvatar { get; set; }

        /// <summary>
        /// Username người gửi
        /// </summary>
        public string SenderUserName { get; set; }

        /// <summary>
        /// Tên đầy đủ người gửi
        /// </summary>
        public string SenderFullName { get; set; }

        /// <summary>
        /// Ngày lấy thông báo
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Ngày nhận thông báo
        /// </summary>
        public DateTime ReceiveDate { get; set; }

        /// <summary>
        /// Ngày xem thông báo
        /// </summary>
        public DateTime? ViewdDate { get; set; }

        public bool IsViewed
        {
            get
            {
                return this.ViewdDate.HasValue;
            }
        }

    }
}
