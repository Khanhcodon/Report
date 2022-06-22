using System;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ActivityLog - public - Entity
    /// Access Modifiers: 
    /// Create Date : 150414
    /// Author      : TrungVH
    /// Description : Entity tương ứng với bảng ActivityLog trong CSDL
    /// </summary>
    public class ActivityLog
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id nhật ký hành động
        /// </summary>
        public int ActivityLogId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Loại nhật ký hành động
        /// </summary>
        public byte ActivityLogType { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Loại nhật ký hành động
        /// </summary>
        public ActivityLogType ActivityLogTypeInEnum
        {
            get { return (ActivityLogType)ActivityLogType; }
            set { ActivityLogType = (byte)value; }
        }

        /// <summary>
        /// Lấy hoặc thiết lập Id người dùng
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên đăng nhập người dùng
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Nội dung
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ip
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ngày tạo
        /// </summary>
        public DateTime CreatedOnDate { get; set; }
    }
}
