using System;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ActivityLog - public - Entity
    /// Access Modifiers:
    /// Create Date : 150414
    /// Author      : TienV
    /// Description : Entity tương ứng với bảng UserActivityLog trong CSDL
    /// </summary>
    public class UserActivityLog
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
        /// UserName của người gửi
        /// </summary>
        public string UserNameSend { get; set; }

        /// <summary>
        /// Họ tên người gửi
        /// </summary>
        public string FullNameSend { get; set; }

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
        /// Lấy hoặc thiết lập ngày gửi
        /// </summary>
        public DateTime SentDate { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái đã xem hay chưa
        /// </summary>
        public bool IsViewed { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái đã thông báo hay chưa
        /// </summary>
        public bool? IsNotified { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái khi IsViewed = true có hiển thị số lượng trên cái chuong hay không
        /// </summary>
        public bool HasDisplayNumberInBell { get; set; }

        /// <summary>
        /// Kiểu notify
        /// </summary>
        public int NotificationType { get; set; }
    }
}