using System;
using System.Collections.Generic;
namespace Bkav.eGovCloud.Business.Objects
{
    /// <summary>
    /// Đối tượng notify
    /// </summary>
    public class Notify
    {
        /// <summary>
        /// Id
        /// </summary>
        public int NotificationId { get; set; }

        /// <summary>
        /// Tiêu đề notify
        /// </summary>
        public string NotificationTitle { get; set; }

        /// <summary>
        /// Nội dung notify
        /// </summary>
        public string NotificationBody { get; set; }

        /// <summary>
        /// Tổng số notify đang có
        /// </summary>
        public int TotalNotify { get; set; }

        /// <summary>
        /// Avatar người gửi
        /// </summary>
        public string SenderAvatar { get; set; }

        /// <summary>
        /// Lấy và thiết lập địa chỉ url khi click notify
        /// </summary>
        public string AccessLink { get; set; }

        /// <summary>
        /// Lấy và thiết lập tên ứng dụng
        /// </summary>
        public string AppName { get; set; }

        /// <summary>
        /// Device Id của người nhận notify: để gửi sang các thiết bị di động
        /// </summary>
        public IEnumerable<string> DeviceIds { get; set; }

        /// <summary>
        /// ConnectionId hiện tại của người nhận notify: để gửi xuống web client qua SignalR
        /// </summary>
        public IEnumerable<string> ConnectionIds { get; set; }

        /// <summary>
        /// Notify cần gửi luôn thay vì cho vào hàng đợi
        /// </summary>
        public bool IsRealTime { get; set; }

        /// <summary>
        /// Dữ liệu notify sử dụng cho webclient
        /// </summary>
        public string JsonData { get; set; }

        /// <summary>
        /// Ngày gửi notify
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Group
        /// </summary>
        public string GroupId { get; set; }

        /// <summary>
        /// Id người nhận
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Domain sử dụng
        /// </summary>
        public string Domain { get; set; }
		public string ActionUrl { get; set; }
	}
}
