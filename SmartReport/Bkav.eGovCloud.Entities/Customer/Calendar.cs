using System;
using System.Collections.Generic;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Đối tượng thể hiện một lịch làm việc
    /// </summary>
    public class Calendar
    {
        /// <summary>
        /// Key
        /// </summary>
        public int CalendarId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tiêu đề cuộc họp
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tiêu đề cuộc họp hiển thị publish
        /// </summary>
        public string TitlePublish { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập thời gian bắt đầu
        /// </summary>
        public DateTime? BeginTime { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập địa điểm
        /// </summary>
        public string Place { get; set; }

        /// <summary>
        /// Thiết lập địa điểm họp công khai
        /// </summary>
        public string PlacePublish { get; set; }

        /// <summary>
        /// Thiết lập người chủ trì công khai
        /// </summary>
        public string UserPrimaryPublish { get; set; }

        /// <summary>
        /// Ngày tạo lịch
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Người tạo lịch
        /// </summary>
        public int UserCreatedId { get; set; }

        /// <summary>
        /// Tên người tạo lịch
        /// </summary>
        public string UserCreatedName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái thể hiện lịch đã được duyệt hay chưa
        /// </summary>
        /// <remarks>
        /// null - Chưa duyệt; false - không duyệt; true - đã duyệt
        /// </remarks>
        public bool? IsAccepted { get; set; }

        /// <summary>
        /// Lịch cá nhân
        /// </summary>
        public bool IsPrivate { get; set; }

        /// <summary>
        /// Cho phép hiển thị ngoài màn hình ti vi
        /// </summary>
        public bool HasPublish { get; set; }

        /// <summary>
        /// Thứ tự lịch hiển thị lên màn hình công khai
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Phòng ban
        /// </summary>
        public string DepartmentIdExt { get; set; }

        /// <summary>
        /// Nội dung cuộc họp
        /// </summary>
        public IEnumerable<CalendarDetail> Contents { get; set; }
    }
}
