using System;
namespace Bkav.eGovCloud.Entities.Admin
{
    /// <summary>
    /// Quản lý các mẫu cảnh báo
    /// </summary>
    public class TimerTemplate
    {
        /// <summary>
        /// Key
        /// </summary>
        public int TimerTemplateId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên mẫu
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập câu query cho mẫu
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mẫu
        /// </summary>
        public string Template { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ngày tạo mẫu
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái sử dụng mẫu
        /// </summary>
        public bool IsActive { get; set; }
    }
}
