using System;
using Bkav.eGovCloud.Core.Utils;
namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// TienBV - 24032017: Lớp lưu các message notify xử lý cho người dùng
    /// </summary>
    public class Notifications
    {
        /// <summary>
        /// NotificationId
        /// </summary>
        public int NotificationId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập GroupId: nhóm các notify cùng một nội dung.
        /// </summary>
        /// <remarks>
        /// Gom nhóm các notify cùng nội dung: như notify của 1 người chat, ...
        /// </remarks>
        public string GroupId { get; set; }

        /// <summary>
        /// Id user nhận thông báo
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// Tiêu đề
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Nội dung tóm tắt
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Avatar người gửi
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// Ngày đưa thông báo lên hệ thống
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DateCreateStr
        {
            get
            {
                return DateCreated.ToAbsoluteDate();
            }
        }

        /// <summary>
        /// Tên ứng dụng gửi notify
        /// </summary>
        public string AppName { get; set; }

        /// <summary>
        /// Chuỗi json thông tin tiết của notify
        /// </summary>
        public string JsonData { get; set; }

        /// <summary>
        /// Là notify sinh ra của hệ thống
        /// </summary>
        public bool IsSystemNotify { get; set; }

        /// <summary>
        /// Trạng thái notify đã được gửi
        /// </summary>
        public bool IsSent { get; set; }

        /// <summary>
        /// Trạng thái notify đã được xem: khi click xem vào quả cầu hoặc click vào nút đóng các notify tương ứng
        /// </summary>
        public bool IsNew { get; set; }

        /// <summary>
        /// Trạng thái notify đã được đọc: đã click vào notify để xem
        /// </summary>
        public bool IsReaded { get; set; }

        /// <summary>
        /// Trạng thái notify đã được đọc: đã click vào notify để xem
        /// </summary>
        public bool IsDeleted { get; set; }

		/// <summary>
		/// Token
		/// </summary>
		public string Token { get; set; }
	}
}
