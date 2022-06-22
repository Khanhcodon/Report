using System;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocTimeline - public - Entity
    /// Access Modifiers: 
    /// Create Date : 270612
    /// Author      : TrungVH
    /// Description : Entity tương ứng với bảng DocTimeline trong CSDL
    /// </summary>
    public class DocTimeline
    {
        /// <summary>
        /// 
        /// </summary>
        public int DocTimelineId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int DocumentCopyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid DocumentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? NodeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string NodeName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime FromDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? ToDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsWorkingTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int DocumentCopyType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? IsSuccess { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ProcessedMinutes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? ConfirmedOn { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Document Document { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Node gửi Id
        /// </summary>
        public int? NodeSendId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên node Gửi
        /// </summary>
        public string NodeSendName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập id người gửi
        /// </summary>
        public int UserSendId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập quy trình
        /// </summary>
        public int WorkFlowId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập thời hạn xử lý trên node hiện tại
        /// </summary>
        public int TimeInNode { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập hạn xử lý mà người giao đặt cho người nhận, nó khác với hạn giữ trong node
        /// </summary>
        public DateTime? DateOverdue { get; set; }
    }
}
